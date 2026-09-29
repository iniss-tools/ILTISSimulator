namespace ILTISSimulator.Simulation;

/// <summary>
///     Udalosti celého dňa zoradené podľa času, s pozíciou, po ktorú sa už odoslali. Neobsahuje žiadne
///     vlákna ani hodiny - posun času jej dodáva volajúci.
/// </summary>
public sealed class SimulationTimeline
{
    private readonly List<SimEvent> _events;
    private int _position;

    /// <summary>
    ///     Zostaví časovú os z jázd vlakov.
    /// </summary>
    /// <param name="runs">Jazdy vlakov.</param>
    public SimulationTimeline(IEnumerable<TrainRun> runs)
    {
        ArgumentNullException.ThrowIfNull(runs);

        _events = runs
            .Where(run => !run.Cancelled)
            .SelectMany(run => run.Events)
            .OrderBy(simEvent => simEvent.Time)
            .ThenBy(simEvent => simEvent.Kind)
            .ToList();
    }

    /// <summary>
    ///     Všetky udalosti dňa zoradené podľa času.
    /// </summary>
    public IReadOnlyList<SimEvent> Events => _events;

    /// <summary>
    ///     Počet už vydaných udalostí.
    /// </summary>
    public int Position => _position;

    /// <summary>
    ///     Či sú všetky udalosti dňa za nami.
    /// </summary>
    public bool IsFinished => _position >= _events.Count;

    /// <summary>
    ///     Čas najbližšej nevydanej udalosti.
    /// </summary>
    public DateTime? NextTime => IsFinished ? (DateTime?)null : _events[_position].Time;

    /// <summary>
    ///     Vydá všetky udalosti, ktorých čas už nastal, a posunie pozíciu za ne.
    /// </summary>
    /// <param name="time">Aktuálny virtuálny čas.</param>
    /// <returns>Udalosti na odoslanie, zoradené podľa času.</returns>
    public IReadOnlyList<SimEvent> DueUpTo(DateTime time)
    {
        var due = new List<SimEvent>();

        while (_position < _events.Count && _events[_position].Time <= time)
        {
            due.Add(_events[_position]);
            _position++;
        }

        return due;
    }

    /// <summary>
    ///     Preskočí udalosti po zadaný čas bez toho, aby sa vydali. Používa sa pri štarte simulácie uprostred
    ///     dňa, aby sa nezaplavil INISS udalosťami, ktoré už dávno prešli.
    /// </summary>
    /// <param name="time">Čas, na ktorý sa má os nastaviť.</param>
    /// <returns>Počet preskočených udalostí.</returns>
    public int SeekTo(DateTime time)
    {
        var skipped = 0;

        while (_position < _events.Count && _events[_position].Time <= time)
        {
            _position++;
            skipped++;
        }

        return skipped;
    }

    /// <summary>
    ///     Vráti os na začiatok dňa.
    /// </summary>
    public void Reset() => _position = 0;
}
