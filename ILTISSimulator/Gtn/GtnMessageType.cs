using ToolsCore.Tools;

namespace ILTISSimulator.Gtn;

/// <summary>
///     Typ správy rozhrania ILTIS-INISS (GTN). INISS pozná iba tieto tri typy, ostatné zaloguje ako
///     <c>unknown message type</c>.
/// </summary>
public sealed class GtnMessageType : Enumeration<GtnMessageType>
{
    /// <summary>
    ///     080-2 Príchod vlaku (vznik vlaku).
    /// </summary>
    public static readonly GtnMessageType Arrival = new("0802", "Príchod vlaku");

    /// <summary>
    ///     080-3 Odchod vlaku. Ako jediná správa nesie druhú koľaj (pre ďalšie nájdené vlaky).
    /// </summary>
    public static readonly GtnMessageType Departure = new("0803", "Odchod vlaku");

    /// <summary>
    ///     080-4 Vlaková cesta. Ako jediná správa nesie príznak druhu vlakovej cesty.
    /// </summary>
    public static readonly GtnMessageType Route = new("0804", "Vlaková cesta");

    /// <summary>
    ///     080-5 Dôvod meškania. Pozná ju až INISS 3.39.
    /// </summary>
    public static readonly GtnMessageType DelayReason = new("0805", "Dôvod meškania");

    /// <summary>
    ///     080-6 Hlásenie o prestupe. Pozná ju až INISS 3.39.
    /// </summary>
    public static readonly GtnMessageType Transfer = new("0806", "Hlásenie o prestupe");

    private GtnMessageType(string key, string name) : base(key, name)
    {
    }

    /// <summary>
    ///     Vráti typ správy podľa štvorznakového kódu na začiatku správy.
    /// </summary>
    /// <param name="code">Kód typu správy (napr. <c>0802</c>).</param>
    /// <returns>Typ správy alebo <see langword="null" />, ak kód nezodpovedá žiadnemu známemu typu.</returns>
    public static GtnMessageType FromCode(string code) => GetValues().FirstOrDefault(type => type.Key == code);

    /// <inheritdoc />
    public override string ToString() => $"{Key} - {Name}";
}
