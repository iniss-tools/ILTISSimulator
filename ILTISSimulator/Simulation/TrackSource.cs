namespace ILTISSimulator.Simulation;

/// <summary>
///     Odkiaľ sa vzala koľaj, na ktorú simulátor vlak postavil.
/// </summary>
public enum TrackSource
{
    /// <summary>
    ///     Koľaj z grafikonu.
    /// </summary>
    Timetable,

    /// <summary>
    ///     Vlak bol odklonený na inú koľaj, hoci koľaj z grafikonu mal.
    /// </summary>
    Diverted,

    /// <summary>
    ///     Vlak nemal v grafikone koľaj, simulátor mu vybral voľnú.
    /// </summary>
    Assigned,

    /// <summary>
    ///     V danom čase nebola voľná žiadna koľaj. Vlak zostal na koľaji z grafikonu a jazdí na ňu súbežne
    ///     s iným vlakom.
    /// </summary>
    Conflict
}
