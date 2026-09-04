using GVDEditor.Entities;

namespace ILTISSimulator.Data;

/// <summary>
///     Grafikon načítaný v rozsahu, ktorý potrebuje simulátor ILTISu - bez zvukovej banky, tabúľ a hlásení.
/// </summary>
public sealed class GvdData
{
    internal GvdData(GVDInfo info, List<TrainType> trainTypes, List<Track> tracks, List<Train> trains,
        Dictionary<int, BitArray> trainDays)
    {
        Info = info;
        TrainTypes = trainTypes;
        Tracks = tracks;
        Trains = trains;
        TrainDays = trainDays;
    }

    /// <summary>
    ///     Informácie o grafikone.
    /// </summary>
    public GVDInfo Info { get; }

    /// <summary>
    ///     Kategórie vlakov.
    /// </summary>
    public List<TrainType> TrainTypes { get; }

    /// <summary>
    ///     Koľaje v stanici vrátane <see cref="Track.None" /> na prvom mieste.
    /// </summary>
    public List<Track> Tracks { get; }

    /// <summary>
    ///     Vlaky grafikonu, zoradené podľa identifikátora.
    /// </summary>
    public List<Train> Trains { get; }

    /// <summary>
    ///     Dátumové obmedzenie vlaku ako bitové pole dní, kľúčom je <see cref="Train.ID" />. Bit s indexom
    ///     <c>i</c> platí pre deň <see cref="Train.ZaciatokPlatnosti" /> plus <c>i</c> dní.
    /// </summary>
    public Dictionary<int, BitArray> TrainDays { get; }

    /// <summary>
    ///     Identifikátor stanice, pre ktorú je grafikon urobený.
    /// </summary>
    public int StationId => int.Parse(Info.ThisStation.ID, CultureInfo.InvariantCulture);

    /// <summary>
    ///     Zistí, či vlak ide v zadaný deň.
    /// </summary>
    /// <param name="train">Vlak.</param>
    /// <param name="day">Deň.</param>
    /// <returns><see langword="true" />, ak vlak v ten deň ide.</returns>
    public bool RunsOn(Train train, DateTime day)
    {
        if (train is null)
            throw new ArgumentNullException(nameof(train));

        if (!TrainDays.TryGetValue(train.ID, out var days))
            return false;

        var index = (int)(day.Date - train.ZaciatokPlatnosti.Date).TotalDays;
        return index >= 0 && index < days.Length && days[index];
    }

    /// <summary>
    ///     Vráti vlaky, ktoré idú v zadaný deň.
    /// </summary>
    /// <param name="day">Deň.</param>
    /// <returns>Vlaky idúce v ten deň.</returns>
    public IEnumerable<Train> TrainsOn(DateTime day) => Trains.Where(train => RunsOn(train, day));

    /// <summary>
    ///     Vráti čas príchodu vlaku v zadaný deň.
    /// </summary>
    /// <param name="train">Vlak.</param>
    /// <param name="day">Deň.</param>
    /// <returns>Čas príchodu, alebo <see langword="null" /> pri východzom vlaku.</returns>
    public static DateTime? ArrivalOn(Train train, DateTime day)
    {
        if (train is null)
            throw new ArgumentNullException(nameof(train));

        return train.Arrival.HasValue ? day.Date + train.Arrival.Value.TimeOfDay : (DateTime?)null;
    }

    /// <summary>
    ///     Vráti čas odchodu vlaku v zadaný deň. Ak vlak stojí cez polnoc, odchod pripadne na nasledujúci deň.
    /// </summary>
    /// <param name="train">Vlak.</param>
    /// <param name="day">Deň.</param>
    /// <returns>Čas odchodu, alebo <see langword="null" /> pri končiacom vlaku.</returns>
    public static DateTime? DepartureOn(Train train, DateTime day)
    {
        if (train is null)
            throw new ArgumentNullException(nameof(train));

        if (!train.Departure.HasValue)
            return null;

        var departure = day.Date + train.Departure.Value.TimeOfDay;
        var arrival = ArrivalOn(train, day);

        return arrival.HasValue && departure < arrival.Value ? departure.AddDays(1) : departure;
    }

    /// <summary>
    ///     Zistí, či má vlak v grafikone priradenú skutočnú koľaj. Vlak bez nej (predvolená alebo nedefinovaná
    ///     koľaj) dostane koľaj až od simulátora.
    /// </summary>
    /// <param name="train">Vlak.</param>
    /// <returns><see langword="true" />, ak má vlak priradenú skutočnú koľaj.</returns>
    public static bool HasRealTrack(Train train)
    {
        if (train is null)
            throw new ArgumentNullException(nameof(train));

        return train.Track is not null && train.Track != Track.None && !string.IsNullOrWhiteSpace(train.Track.TrackName);
    }
}
