using System.Diagnostics;
using System.Threading;
using ILTISSimulator.Data;
using ILTISSimulator.Gtn;
using ILTISSimulator.Properties;
using ILTISSimulator.Tools;

namespace ILTISSimulator.Simulation;

/// <summary>
///     Jadro simulátora: drží virtuálne hodiny, naplánovaný deň a vydáva udalosti v okamihu, keď na ne
///     príde čas. Samotné odoslanie správy nechá na volajúceho, ktorý si na <see cref="EventDue" /> pripojí
///     napríklad <see cref="Net.GtnClient" />.
/// </summary>
public sealed class SimulationEngine : IDisposable
{
    private const int TickMilliseconds = 200;

    private readonly object _sync = new();
    private readonly GvdData _gvd;
    private readonly Stopwatch _clock = new();

    private Timer _timer;
    private DateTime _virtualStart;
    private int _ticking;
    private bool _disposed;

    /// <summary>
    ///     Vytvorí jadro simulátora nad grafikonom.
    /// </summary>
    /// <param name="gvd">Načítaný grafikon.</param>
    /// <param name="options">Nastavenia simulácie.</param>
    public SimulationEngine(GvdData gvd, SimulationOptions options)
    {
        _gvd = gvd ?? throw new ArgumentNullException(nameof(gvd));
        Options = options ?? throw new ArgumentNullException(nameof(options));
        Options.Validate();

        Messages = new GtnMessageBuilder(gvd.StationId);
    }

    /// <summary>
    ///     Nastavenia simulácie.
    /// </summary>
    public SimulationOptions Options { get; }

    /// <summary>
    ///     Staviteľ správ pre stanicu grafikonu.
    /// </summary>
    public GtnMessageBuilder Messages { get; }

    /// <summary>
    ///     Naplánované jazdy vlakov, alebo <see langword="null" />, kým sa deň nenaplánoval.
    /// </summary>
    public IReadOnlyList<TrainRun> Runs { get; private set; }

    /// <summary>
    ///     Časová os naplánovaného dňa.
    /// </summary>
    public SimulationTimeline Timeline { get; private set; }

    /// <summary>
    ///     Deň, ktorý je naplánovaný.
    /// </summary>
    public DateTime Day { get; private set; }

    /// <summary>
    ///     Či simulácia práve beží.
    /// </summary>
    public bool IsRunning
    {
        get
        {
            lock (_sync)
                return _timer is not null;
        }
    }

    /// <summary>
    ///     Aktuálny virtuálny čas.
    /// </summary>
    public DateTime VirtualTime
    {
        get
        {
            lock (_sync)
                return CurrentTime();
        }
    }

    /// <summary>
    ///     Udalosť, ktorej čas nastal, aj s hotovou správou. Volá sa z vlákna časovača.
    /// </summary>
    public event EventHandler<SimEventDueEventArgs> EventDue;

    /// <summary>
    ///     Priebežné hlásenia jadra.
    /// </summary>
    public event EventHandler<string> Log;

    /// <summary>
    ///     Naplánuje deň a nastaví virtuálne hodiny.
    /// </summary>
    /// <param name="day">Deň, ktorý sa má simulovať.</param>
    /// <param name="startAt">Čas, od ktorého sa má začať. Predvolene začiatok dňa; udalosti spred tohto času
    ///     sa preskočia, aby sa INISS nezaplavil dávno prešlými správami.</param>
    public void PlanDay(DateTime day, DateTime? startAt = null)
    {
        lock (_sync)
        {
            ThrowIfDisposed();
            StopTimer();

            Day = day.Date;

            var planner = new DayPlanner(_gvd, Options);
            Runs = planner.Plan(Day);
            Timeline = new SimulationTimeline(Runs);

            _virtualStart = startAt ?? Day;
            _clock.Reset();

            var skipped = Timeline.SeekTo(_virtualStart);

            OnLog(Texts.F(Resources.Engine_Planned, Day, Runs.Count, Timeline.Events.Count, skipped, Options.Seed));

            if (planner.SkippedDuplicates.Count > 0)
                OnLog(Texts.F(Resources.Engine_SkippedDuplicates,
                    string.Join(", ", planner.SkippedDuplicates.Select(train => train.Number).Distinct())));
        }
    }

    /// <summary>
    ///     Spustí alebo obnoví beh simulácie.
    /// </summary>
    public void Start()
    {
        lock (_sync)
        {
            ThrowIfDisposed();

            if (Timeline is null)
                throw new InvalidOperationException(Resources.Error_PlanFirst);

            if (_timer is not null)
                return;

            _clock.Start();
            _timer = new Timer(Tick, null, 0, TickMilliseconds);

            OnLog(Texts.F(Resources.Engine_Running, CurrentTime(), Options.TimeFactor));
        }
    }

    /// <summary>
    ///     Pozastaví beh. Virtuálny čas sa zastaví tam, kde je.
    /// </summary>
    public void Pause()
    {
        lock (_sync)
        {
            if (_timer is null)
                return;

            StopTimer();
            OnLog(Texts.F(Resources.Engine_Paused, CurrentTime()));
        }
    }

    /// <summary>
    ///     Zastaví beh a vráti časovú os na začiatok dňa.
    /// </summary>
    public void Stop()
    {
        lock (_sync)
        {
            StopTimer();

            _clock.Reset();
            _virtualStart = Day;
            Timeline?.Reset();

            OnLog(Resources.Engine_Stopped);
        }
    }

    /// <summary>
    ///     Presunie virtuálny čas na zadaný okamih. Udalosti, ktoré tým preskočí, sa neodošlú.
    /// </summary>
    /// <param name="time">Nový virtuálny čas.</param>
    public void JumpTo(DateTime time)
    {
        lock (_sync)
        {
            ThrowIfDisposed();

            if (Timeline is null)
                throw new InvalidOperationException(Resources.Error_PlanFirst);

            var running = _timer is not null;
            StopTimer();

            _virtualStart = time;
            _clock.Reset();

            var skipped = Timeline.SeekTo(time);
            OnLog(Texts.F(Resources.Engine_Jumped, time, skipped));

            if (running)
            {
                _clock.Start();
                _timer = new Timer(Tick, null, 0, TickMilliseconds);
            }
        }
    }

    /// <summary>
    ///     Vydá udalosti, ktorých čas už nastal. Volá sa z časovača, ale dá sa zavolať aj ručne - vtedy
    ///     simulácia beží krokovo bez vlákien.
    /// </summary>
    /// <param name="time">Čas, po ktorý sa majú udalosti vydať.</param>
    /// <returns>Počet vydaných udalostí.</returns>
    public int Pump(DateTime time)
    {
        IReadOnlyList<SimEvent> due;

        lock (_sync)
        {
            if (Timeline is null)
                return 0;

            due = Timeline.DueUpTo(time);
        }

        foreach (var simEvent in due)
        {
            try
            {
                EventDue?.Invoke(this, new SimEventDueEventArgs(simEvent, Messages.Build(simEvent)));
            }
            catch (Exception e)
            {
                OnLog(Texts.F(Resources.Engine_EventFailed, simEvent, e.Message));
            }
        }

        return due.Count;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        lock (_sync)
        {
            if (_disposed)
                return;

            StopTimer();
            _disposed = true;
        }
    }

    private void Tick(object state)
    {
        // Ak sa predchádzajúci tik ešte nedobehol, tento sa preskočí - udalosti nezmiznú, len počkajú.
        if (Interlocked.CompareExchange(ref _ticking, 1, 0) != 0)
            return;

        try
        {
            Pump(VirtualTime);
        }
        catch (Exception e)
        {
            OnLog(Texts.F(Resources.Engine_RunFailed, e.Message));
        }
        finally
        {
            Interlocked.Exchange(ref _ticking, 0);
        }
    }

    private DateTime CurrentTime() => _virtualStart + TimeSpan.FromTicks((long)(_clock.Elapsed.Ticks * Options.TimeFactor));

    private void StopTimer()
    {
        _clock.Stop();

        if (_timer is null)
            return;

        _timer.Dispose();
        _timer = null;
    }

    private void OnLog(string text) => Log?.Invoke(this, text);

    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(SimulationEngine));
    }
}

/// <summary>
///     Údaje udalosti <see cref="SimulationEngine.EventDue" />.
/// </summary>
public sealed class SimEventDueEventArgs : EventArgs
{
    internal SimEventDueEventArgs(SimEvent simEvent, GtnMessage message)
    {
        SimEvent = simEvent;
        Message = message;
    }

    /// <summary>
    ///     Udalosť simulácie.
    /// </summary>
    public SimEvent SimEvent { get; }

    /// <summary>
    ///     Hotová správa pripravená na odoslanie.
    /// </summary>
    public GtnMessage Message { get; }
}
