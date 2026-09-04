using GVDEditor.Entities;
using ILTISSimulator.Data;
using ILTISSimulator.Properties;

namespace ILTISSimulator.Simulation;

/// <summary>
///     Zostaví plán jedného dňa: pre každý vlak grafikonu vylosuje meškanie a koľaj a vyrobí udalosti,
///     ktoré sa počas dňa odošlú INISSu.
/// </summary>
public sealed class DayPlanner
{
    private readonly GvdData _gvd;
    private readonly SimulationOptions _options;

    /// <summary>
    ///     Vytvorí plánovač nad grafikonom.
    /// </summary>
    /// <param name="gvd">Načítaný grafikon.</param>
    /// <param name="options">Nastavenia simulácie.</param>
    public DayPlanner(GvdData gvd, SimulationOptions options)
    {
        _gvd = gvd ?? throw new ArgumentNullException(nameof(gvd));
        _options = options ?? throw new ArgumentNullException(nameof(options));

        _options.Validate();
    }

    /// <summary>
    ///     Vlaky, ktoré posledné plánovanie vynechalo, lebo v ten deň ide viac záznamov s rovnakým číslom.
    /// </summary>
    /// <remarks>
    ///     INISS páruje správy s vlakom iba podľa čísla a stanice, takže z niekoľkých záznamov s rovnakým
    ///     číslom vezme ten svoj prvý. Overené naživo na vlaku 617 v grafikone Košíc, ktorý je v grafikone
    ///     dvakrát - ako končiaci a ako prechádzajúci pod pokračovaním 964. Správa poslaná podľa toho druhého
    ///     dostala od INISSu meškanie 43 minút, lebo si ju porovnal s časom toho prvého. Simulátor preto
    ///     posiela iba jednu súvislú sadu udalostí na číslo vlaku - záznam s najnižším identifikátorom,
    ///     rovnako ako si ho vyberá INISS.
    /// </remarks>
    public IReadOnlyList<Train> SkippedDuplicates { get; private set; } = new List<Train>();

    /// <summary>
    ///     Naplánuje jeden deň. Rovnaký deň a rovnaký <see cref="SimulationOptions.Seed" /> dajú vždy rovnaký
    ///     výsledok.
    /// </summary>
    /// <param name="day">Deň, ktorý sa má naplánovať.</param>
    /// <returns>Jazdy vlakov zoradené podľa prvej udalosti.</returns>
    public IReadOnlyList<TrainRun> Plan(DateTime day)
    {
        var random = new Random(_options.Seed);
        var delays = new DelayModel(_options, random);
        var allocator = new TrackAllocator(_gvd.Tracks, _options.ExcludedTrackKeys);

        if (allocator.Tracks.Count == 0)
            throw new InvalidOperationException(
                Resources.Error_NoTrackLeft);

        var byNumber = _gvd.TrainsOn(day)
            .OrderBy(train => train.ID)
            .GroupBy(train => train.Number)
            .ToList();

        SkippedDuplicates = byNumber.SelectMany(group => group.Skip(1)).ToList();

        var runs = byNumber
            .Select(group => group.First())
            .Select(train => new TrainRun(train, GvdData.ArrivalOn(train, day), GvdData.DepartureOn(train, day)))
            .Where(run => run.PlannedArrival.HasValue || run.PlannedDeparture.HasValue)
            .OrderBy(run => run.PlannedArrival ?? run.PlannedDeparture)
            .ThenBy(run => run.Train.ID)
            .ToList();

        foreach (var run in runs)
        {
            if (random.NextDouble() < _options.CancellationProbability)
            {
                run.Cancelled = true;
                run.Track = run.Train.Track;
                run.TrackSource = TrackSource.Timetable;
                continue;
            }

            AssignDelays(run, delays);
            AssignDelayReason(run, random);
            AssignTrack(run, allocator, random);
            AddEvents(run);
        }

        return runs;
    }

    private void AssignDelays(TrainRun run, DelayModel delays)
    {
        if (run.PlannedArrival.HasValue)
        {
            run.ArrivalDelay = delays.NextArrivalDelay();

            if (run.PlannedDeparture.HasValue)
            {
                var stay = run.PlannedDeparture.Value - run.PlannedArrival.Value;
                run.DepartureDelay = delays.NextDepartureDelay(run.ArrivalDelay, stay);
            }

            return;
        }

        // Východzí vlak nemá príchod, meškanie sa mu losuje priamo na odchode.
        run.DepartureDelay = delays.NextArrivalDelay();
    }

    private void AssignDelayReason(TrainRun run, Random random)
    {
        var delay = run.PlannedArrival.HasValue ? run.ArrivalDelay : run.DepartureDelay;

        if (_options.DelayReasons.Count == 0 || delay < _options.MinDelayForReason)
            return;

        if (random.NextDouble() >= _options.DelayReasonProbability)
            return;

        run.DelayReason = _options.DelayReasons[random.Next(_options.DelayReasons.Count)];
    }

    private void AssignTrack(TrainRun run, TrackAllocator allocator, Random random)
    {
        var start = (run.ActualArrival ?? run.ActualDeparture).Value;
        var end = (run.ActualDeparture ?? run.ActualArrival).Value;

        run.OccupiedFrom = start - _options.EntryRouteLead;
        run.OccupiedTo = end + _options.TrackReleaseMargin;

        var planned = GvdData.HasRealTrack(run.Train) && TrackAllocator.CanUse(run.Train.Track)
            ? run.Train.Track
            : null;

        if (planned is not null && !allocator.IsManaged(planned))
        {
            // Koľaj z grafikonu je vylúčená z prideľovania. Grafikon neprepisujeme, len ju neobsadzujeme.
            run.Track = planned;
            run.TrackSource = TrackSource.Timetable;
            return;
        }

        if (planned is null)
        {
            var assigned = allocator.ReserveAny(run.OccupiedFrom, run.OccupiedTo, random);
            run.Track = assigned ?? run.Train.Track ?? Track.None;
            run.TrackSource = assigned is null ? TrackSource.Conflict : TrackSource.Assigned;
            return;
        }

        if (random.NextDouble() < _options.TrackDiversionProbability)
        {
            var diverted = allocator.ReserveAny(run.OccupiedFrom, run.OccupiedTo, random, planned, planned);
            if (diverted is not null)
            {
                run.Track = diverted;
                run.TrackSource = TrackSource.Diverted;
                return;
            }
        }

        if (allocator.TryReserve(planned, run.OccupiedFrom, run.OccupiedTo))
        {
            run.Track = planned;
            run.TrackSource = TrackSource.Timetable;
            return;
        }

        // Koľaj z grafikonu je obsadená - vlak musí ísť inam.
        var replacement = allocator.ReserveAny(run.OccupiedFrom, run.OccupiedTo, random, planned, planned);

        run.Track = replacement ?? planned;
        run.TrackSource = replacement is null ? TrackSource.Conflict : TrackSource.Diverted;
    }

    private void AddEvents(TrainRun run)
    {
        if (run.PlannedArrival.HasValue)
        {
            run.AddEvent(SimEventKind.EntryRoute, run.PlannedArrival.Value - _options.EntryRouteLead,
                run.ActualArrival.Value - _options.EntryRouteLead);

            if (run.DelayReason != 0)
                run.AddEvent(SimEventKind.DelayReason, run.PlannedArrival.Value - _options.EntryRouteLead,
                    run.ActualArrival.Value - _options.EntryRouteLead);

            run.AddEvent(SimEventKind.Arrival, run.PlannedArrival.Value, run.ActualArrival.Value);
        }

        if (!run.PlannedDeparture.HasValue)
            return;

        if (run.DelayReason != 0 && !run.PlannedArrival.HasValue)
            run.AddEvent(SimEventKind.DelayReason, run.PlannedDeparture.Value - _options.ExitRouteLead,
                run.ActualDeparture.Value - _options.ExitRouteLead);

        run.AddEvent(SimEventKind.ExitRoute, run.PlannedDeparture.Value - _options.ExitRouteLead,
            run.ActualDeparture.Value - _options.ExitRouteLead);
        run.AddEvent(SimEventKind.Departure, run.PlannedDeparture.Value, run.ActualDeparture.Value);
    }
}
