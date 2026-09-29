using ILTISSimulator.Properties;
using ToolsCore.Iniss.Tools;

namespace ILTISSimulator.Gtn;

/// <summary>
///     Zakódovanie identifikátora stanice do šesťznakového poľa správy GTN.
/// </summary>
/// <remarks>
///     <para>
///         INISS z poľa číta iba prvých päť znakov, prečíta ich ako číslo a pripočíta
///         <see cref="InissBase" />. Šiesty znak poľa nemá na výsledok vplyv. Na stranu odosielateľa z toho
///         vychádza pravidlo: do poľa ide <strong>posledných päť číslic identifikátora stanice</strong>
///         z grafikonu, teda <c>ID mod 100 000</c>.
///     </para>
/// </remarks>
public static class StationCodec
{
    /// <summary>
    ///     Konštanta, ktorú INISS pripočítava k prečítanému číslu stanice.
    /// </summary>
    public const int InissBase = 5400000;

    /// <summary>
    ///     Počet znakov poľa stanice, ktoré INISS naozaj číta.
    /// </summary>
    public const int SignificantDigits = 5;

    /// <summary>
    ///     Rozsah, na ktorý sa identifikátor stanice cestou oreže.
    /// </summary>
    public const int Modulus = 100000;

    /// <summary>
    ///     Výplňový znak na šiestej pozícii poľa. INISS ho ignoruje.
    /// </summary>
    public const char Filler = '0';

    /// <summary>
    ///     Zakóduje identifikátor stanice do šesťznakového poľa správy.
    /// </summary>
    /// <param name="stationId">Identifikátor stanice z grafikonu (napr. 5613600).</param>
    /// <returns>Šesťznakové pole stanice (napr. <c>136000</c>).</returns>
    /// <exception cref="ArgumentOutOfRangeException">Ak je identifikátor záporný.</exception>
    public static string Encode(int stationId)
    {
        if (stationId < 0)
            throw new ArgumentOutOfRangeException(nameof(stationId), Resources.Error_StationId);

        return (stationId % Modulus).PadZeros(SignificantDigits) + Filler;
    }

    /// <summary>
    ///     Vráti hodnotu, na ktorú sa identifikátor stanice cez toto rozhranie zobrazí. Dve stanice s rovnakým
    ///     výsledkom nie je možné v správach GTN rozlíšiť.
    /// </summary>
    /// <param name="stationId">Identifikátor stanice z grafikonu.</param>
    /// <returns>Hodnota, ktorú INISS zo správy prečíta.</returns>
    public static int NormalizeAsIniss(int stationId) => InissBase + stationId % Modulus;

    /// <summary>
    ///     Dekóduje pole stanice presne tak, ako to robí INISS: vezme prvých päť znakov, prečíta ich ako číslo
    ///     a pripočíta <see cref="InissBase" />.
    /// </summary>
    /// <param name="field">Pole stanice zo správy.</param>
    /// <param name="stationId">Hodnota, ako jej rozumie INISS.</param>
    /// <returns>
    ///     <see langword="true" />, ak sa pole podarilo prečítať; inak <see langword="false" />, čo INISS
    ///     zaloguje ako <c>unknown station number format</c>.
    /// </returns>
    public static bool TryDecodeAsIniss(string field, out int stationId)
    {
        stationId = 0;

        if (string.IsNullOrEmpty(field))
            return false;

        var significant = field.Length > SignificantDigits ? field.Substring(0, SignificantDigits) : field;
        if (!int.TryParse(significant, NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture, out var number))
            return false;

        stationId = InissBase + number;
        return true;
    }

    /// <summary>
    ///     Zistí, či pole stanice zo správy adresuje danú stanicu.
    /// </summary>
    /// <param name="field">Pole stanice zo správy.</param>
    /// <param name="stationId">Identifikátor stanice z grafikonu.</param>
    /// <returns><see langword="true" />, ak INISS správu priradí tejto stanici.</returns>
    public static bool Matches(string field, int stationId) =>
        TryDecodeAsIniss(field, out var decoded) && decoded == NormalizeAsIniss(stationId);
}
