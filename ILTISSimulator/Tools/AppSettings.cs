using Microsoft.Win32;

namespace ILTISSimulator.Tools;

/// <summary>
///     Nastavenia aplikácie uložené v registri pod <c>HKCU\SOFTWARE\ILTISSimulator</c>, aby si program
///     pamätal, kde skončil.
/// </summary>
internal static class AppSettings
{
    private static readonly string KeyPath = $"SOFTWARE\\{Application.ProductName}";

    /// <summary>
    ///     Jazyk používateľského rozhrania.
    /// </summary>
    public static AppLanguage Language { get; set; } = AppLanguage.Slovak;

    /// <summary>
    ///     Naposledy použitý priečinok s grafikonom.
    /// </summary>
    public static string GvdDir { get; set; } = "";

    /// <summary>
    ///     Adresa INISSu.
    /// </summary>
    public static string Host { get; set; } = "127.0.0.1";

    /// <summary>
    ///     Port, na ktorom INISS počúva.
    /// </summary>
    public static int Port { get; set; } = 4000;

    /// <summary>
    ///     Seed generátora náhody.
    /// </summary>
    public static int Seed { get; set; } = 1;

    /// <summary>
    ///     Zrýchlenie virtuálneho času.
    /// </summary>
    public static int TimeFactor { get; set; } = 60;

    /// <summary>
    ///     Pravdepodobnosť príchodu načas v percentách.
    /// </summary>
    public static int OnTimePercent { get; set; } = 75;

    /// <summary>
    ///     Stredné meškanie v minútach.
    /// </summary>
    public static int MeanDelayMinutes { get; set; } = 8;

    /// <summary>
    ///     Pravdepodobnosť odklonu na inú koľaj v percentách.
    /// </summary>
    public static int DiversionPercent { get; set; } = 7;

    /// <summary>
    ///     Pravdepodobnosť zrušenia vlaku v percentách.
    /// </summary>
    public static int CancellationPercent { get; set; } = 1;

    /// <summary>
    ///     Pravdepodobnosť, že meškajúci vlak dostane dôvod meškania, v percentách.
    /// </summary>
    public static int DelayReasonPercent { get; set; } = 60;

    /// <summary>
    ///     Načíta nastavenia z registra. Chýbajúce hodnoty zostanú predvolené.
    /// </summary>
    public static void Load()
    {
        using var key = Registry.CurrentUser.OpenSubKey(KeyPath);
        if (key is null)
            return;

        Language = Enum.TryParse<AppLanguage>(key.GetValue(nameof(Language))?.ToString(), out var language)
            ? language
            : Language;
        GvdDir = ReadString(key, nameof(GvdDir), GvdDir);
        Host = ReadString(key, nameof(Host), Host);
        Port = ReadInt(key, nameof(Port), Port);
        Seed = ReadInt(key, nameof(Seed), Seed);
        TimeFactor = ReadInt(key, nameof(TimeFactor), TimeFactor);
        OnTimePercent = ReadInt(key, nameof(OnTimePercent), OnTimePercent);
        MeanDelayMinutes = ReadInt(key, nameof(MeanDelayMinutes), MeanDelayMinutes);
        DiversionPercent = ReadInt(key, nameof(DiversionPercent), DiversionPercent);
        CancellationPercent = ReadInt(key, nameof(CancellationPercent), CancellationPercent);
        DelayReasonPercent = ReadInt(key, nameof(DelayReasonPercent), DelayReasonPercent);
    }

    /// <summary>
    ///     Uloží nastavenia do registra.
    /// </summary>
    public static void Save()
    {
        using var key = Registry.CurrentUser.CreateSubKey(KeyPath);
        if (key is null)
            return;

        key.SetValue(nameof(Language), Language.ToString());
        key.SetValue(nameof(GvdDir), GvdDir ?? "");
        key.SetValue(nameof(Host), Host ?? "");
        key.SetValue(nameof(Port), Port);
        key.SetValue(nameof(Seed), Seed);
        key.SetValue(nameof(TimeFactor), TimeFactor);
        key.SetValue(nameof(OnTimePercent), OnTimePercent);
        key.SetValue(nameof(MeanDelayMinutes), MeanDelayMinutes);
        key.SetValue(nameof(DiversionPercent), DiversionPercent);
        key.SetValue(nameof(CancellationPercent), CancellationPercent);
        key.SetValue(nameof(DelayReasonPercent), DelayReasonPercent);
    }

    /// <summary>
    ///     Vráti kľúče koľají vylúčených z plánovania pre danú stanicu. Ukladá sa zvlášť pre každú stanicu,
    ///     lebo označenia koľají sa medzi grafikonmi líšia.
    /// </summary>
    /// <param name="stationId">Identifikátor stanice.</param>
    /// <returns>Kľúče vylúčených koľají.</returns>
    public static ISet<string> GetExcludedTracks(int stationId)
    {
        using var key = Registry.CurrentUser.OpenSubKey(KeyPath);
        var value = key?.GetValue(ExcludedTracksName(stationId))?.ToString() ?? "";

        return new HashSet<string>(value.Split(',', StringSplitOptions.RemoveEmptyEntries),
            StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Uloží kľúče koľají vylúčených z plánovania pre danú stanicu.
    /// </summary>
    /// <param name="stationId">Identifikátor stanice.</param>
    /// <param name="keys">Kľúče vylúčených koľají.</param>
    public static void SetExcludedTracks(int stationId, IEnumerable<string> keys)
    {
        using var key = Registry.CurrentUser.CreateSubKey(KeyPath);
        key?.SetValue(ExcludedTracksName(stationId), string.Join(",", keys));
    }

    private static string ExcludedTracksName(int stationId) => $"ExcludedTracks.{stationId}";

    private static string ReadString(RegistryKey key, string name, string fallback) =>
        key.GetValue(name)?.ToString() is { Length: > 0 } value ? value : fallback;

    private static int ReadInt(RegistryKey key, string name, int fallback) =>
        int.TryParse(key.GetValue(name)?.ToString(), out var value) ? value : fallback;
}
