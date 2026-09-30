using GVDEditor.Domain.Entities;
using ILTISSimulator.Properties;
using ILTISSimulator.Tools;

namespace ILTISSimulator.Simulation;

/// <summary>
///     Jedna naplánovaná udalosť simulácie - to, čo sa v danom čase odošle INISSu.
/// </summary>
public sealed class SimEvent
{
    internal SimEvent(TrainRun run, SimEventKind kind, DateTime plannedTime, DateTime time, Track track)
    {
        Run = run;
        Kind = kind;
        PlannedTime = plannedTime;
        Time = time;
        Track = track;
    }

    /// <summary>
    ///     Jazda vlaku, ku ktorej udalosť patrí.
    /// </summary>
    public TrainRun Run { get; }

    /// <summary>
    ///     Druh udalosti.
    /// </summary>
    public SimEventKind Kind { get; }

    /// <summary>
    ///     Čas udalosti podľa grafikonu.
    /// </summary>
    public DateTime PlannedTime { get; }

    /// <summary>
    ///     Čas, ktorý pôjde v správe. Rozdiel oproti grafikonu je to, čo si INISS pri správach 080-2 a 080-3
    ///     odvodí ako meškanie.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    ///     Koľaj, ktorá pôjde v správe.
    /// </summary>
    public Track Track { get; }

    /// <summary>
    ///     Posun oproti grafikonu.
    /// </summary>
    public TimeSpan Delay => Time - PlannedTime;

    /// <inheritdoc />
    public override string ToString() =>
        Texts.F(Resources.Event_Format, Time, Texts.Of(Kind), Run.Train.Number, Track?.Key) +
        (Delay == TimeSpan.Zero ? "" : Texts.F(Resources.Event_Delay, Delay.TotalMinutes));
}
