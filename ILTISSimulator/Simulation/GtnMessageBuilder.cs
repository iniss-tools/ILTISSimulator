using ILTISSimulator.Gtn;
using ILTISSimulator.Properties;
using ILTISSimulator.Tools;

namespace ILTISSimulator.Simulation;

/// <summary>
///     Prevod udalosti simulácie na správu rozhrania ILTIS-INISS.
/// </summary>
public sealed class GtnMessageBuilder
{
    /// <summary>
    ///     Vytvorí staviteľa správ pre stanicu.
    /// </summary>
    /// <param name="stationId">Identifikátor stanice z grafikonu (napr. 5613600).</param>
    /// <param name="lineTrack">Traťová koľaj, ktorá ide v odchode; <see langword="null" /> alebo prázdna = odchod
    ///     bez nej - viď <see cref="SimulationOptions.SendLineTrack" />.</param>
    public GtnMessageBuilder(int stationId, string? lineTrack = null)
    {
        StationId = stationId;
        StationField = StationCodec.Encode(stationId);
        LineTrack = string.IsNullOrWhiteSpace(lineTrack) ? null : lineTrack.Trim();
    }

    /// <summary>
    ///     Traťová koľaj, ktorá ide v odchode, alebo <see langword="null" />, ak sa odchod posiela bez nej.
    /// </summary>
    public string? LineTrack { get; }

    /// <summary>
    ///     Identifikátor stanice z grafikonu.
    /// </summary>
    public int StationId { get; }

    /// <summary>
    ///     Pole stanice tak, ako ide do každej správy.
    /// </summary>
    public string StationField { get; }

    /// <summary>
    ///     Zostaví správu k udalosti.
    /// </summary>
    /// <param name="simEvent">Udalosť simulácie.</param>
    /// <returns>Správa pripravená na odoslanie.</returns>
    public GtnMessage Build(SimEvent simEvent)
    {
        ArgumentNullException.ThrowIfNull(simEvent);

        var message = new GtnMessage
        {
            StationField = StationField,
            TrainNumber = simEvent.Run.Train.Number,
            Time = simEvent.Time,
            Track = simEvent.Track?.Key ?? ""
        };

        switch (simEvent.Kind)
        {
            case SimEventKind.DelayReason:
                message.Type = GtnMessageType.DelayReason;
                message.DelayReason = GtnEncoder.FormatDelayReason(simEvent.DelayReason);
                break;
            case SimEventKind.EntryRoute:
                message.Type = GtnMessageType.Route;
                message.RouteKind = GtnRouteKind.Entry;
                break;
            case SimEventKind.ExitRoute:
                message.Type = GtnMessageType.Route;
                message.RouteKind = GtnRouteKind.Exit;
                break;
            case SimEventKind.Arrival:
                message.Type = GtnMessageType.Arrival;
                break;
            case SimEventKind.Departure:
                message.Type = GtnMessageType.Departure;
                if (LineTrack is not null && message.Track.Length > 0)
                    message.LineTrack = LineTrack;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(simEvent), Texts.F(Resources.Error_UnknownEventKind, simEvent.Kind));
        }

        return message;
    }
}
