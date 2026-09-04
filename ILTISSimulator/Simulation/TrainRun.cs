using GVDEditor.Entities;
using ILTISSimulator.Properties;
using ILTISSimulator.Tools;

namespace ILTISSimulator.Simulation;

/// <summary>
///     Jazda jedného vlaku stanicou v jeden deň tak, ako ju simulátor vylosoval.
/// </summary>
public sealed class TrainRun
{
    private readonly List<SimEvent> _events = [];

    internal TrainRun(Train train, DateTime? plannedArrival, DateTime? plannedDeparture)
    {
        Train = train;
        PlannedArrival = plannedArrival;
        PlannedDeparture = plannedDeparture;
    }

    /// <summary>
    ///     Vlak z grafikonu.
    /// </summary>
    public Train Train { get; }

    /// <summary>
    ///     Príchod podľa grafikonu, alebo <see langword="null" /> pri východzom vlaku.
    /// </summary>
    public DateTime? PlannedArrival { get; }

    /// <summary>
    ///     Odchod podľa grafikonu, alebo <see langword="null" /> pri končiacom vlaku.
    /// </summary>
    public DateTime? PlannedDeparture { get; }

    /// <summary>
    ///     Vylosované meškanie na príchode.
    /// </summary>
    public TimeSpan ArrivalDelay { get; internal set; }

    /// <summary>
    ///     Vylosované meškanie na odchode. Môže byť menšie než meškanie na príchode, ak vlak skrátil pobyt.
    /// </summary>
    public TimeSpan DepartureDelay { get; internal set; }

    /// <summary>
    ///     Skutočný príchod, teda čas, ktorý pôjde v správe 080-2.
    /// </summary>
    public DateTime? ActualArrival => PlannedArrival + ArrivalDelay;

    /// <summary>
    ///     Skutočný odchod, teda čas, ktorý pôjde v správe 080-3.
    /// </summary>
    public DateTime? ActualDeparture => PlannedDeparture + DepartureDelay;

    /// <summary>
    ///     Koľaj, na ktorú simulátor vlak postavil.
    /// </summary>
    public Track Track { get; internal set; }

    /// <summary>
    ///     Odkiaľ sa koľaj vzala.
    /// </summary>
    public TrackSource TrackSource { get; internal set; }

    /// <summary>
    ///     Vylosovaný dôvod meškania - poradové číslo v skupine <c>dz</c> zvukovej banky. Nula znamená, že
    ///     sa dôvod neposiela.
    /// </summary>
    public int DelayReason { get; internal set; }

    /// <summary>
    ///     Vlak bol zrušený - nepošle sa oň žiadna správa a neobsadí koľaj.
    /// </summary>
    public bool Cancelled { get; internal set; }

    /// <summary>
    ///     Od kedy vlak drží koľaj - od postavenia vjazdovej cesty.
    /// </summary>
    public DateTime OccupiedFrom { get; internal set; }

    /// <summary>
    ///     Dokedy vlak drží koľaj - po odchode plus rezerva.
    /// </summary>
    public DateTime OccupiedTo { get; internal set; }

    /// <summary>
    ///     Udalosti jazdy zoradené podľa času.
    /// </summary>
    public IReadOnlyList<SimEvent> Events => _events;

    internal void AddEvent(SimEventKind kind, DateTime plannedTime, DateTime time) =>
        _events.Add(new SimEvent(this, kind, plannedTime, time, Track));

    /// <inheritdoc />
    public override string ToString() =>
        Texts.F(Resources.Run_Format, Train.Type?.Key, Train.Number, Train.Routing?.CharSymbol, Track?.Key,
            Texts.Of(TrackSource)) + (Cancelled ? Resources.Run_Cancelled : "");
}
