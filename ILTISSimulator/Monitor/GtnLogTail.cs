using ToolsCore.Iniss.Tools;

namespace ILTISSimulator.Monitor;

/// <summary>
///     Jeden riadok logu rozhrania INISSu (<c>GTN.LOG</c>): kedy INISS správu prijal, jej text a ako ju spracoval.
/// </summary>
/// <param name="Logged">Čas zápisu podľa hodín INISSu.</param>
/// <param name="Message">Text správy tak, ako prišla (bez ukončenia riadka).</param>
/// <param name="Result">Výsledok spracovania, napríklad <c>OK, delay 3 min., delay 3 min., track 2</c>.</param>
public sealed record GtnLogEntry(DateTime Logged, string Message, string Result)
{
    /// <summary>
    ///     INISS správu prijal a priradil vlaku.
    /// </summary>
    public bool Ok => Result.StartsWith("OK", StringComparison.Ordinal);

    /// <summary>
    ///     Výsledok bez opakovaných častí - INISS meškanie píše dvakrát (<c>OK, delay 3 min., delay 3 min., track 2</c>).
    /// </summary>
    public string Summary => string.Join(", ", Result.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        .Distinct(StringComparer.Ordinal));

    /// <summary>
    ///     Rozoberie riadok logu v tvare <c>dd.MM.yyyy HH:mm:ss: "správa" - výsledok</c>.
    /// </summary>
    /// <param name="line">Riadok logu.</param>
    /// <param name="entry">Rozobraný záznam.</param>
    /// <returns><see langword="true" />, ak riadok zodpovedá tvaru záznamu o správe.</returns>
    public static bool TryParse(string line, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out GtnLogEntry? entry)
    {
        entry = null;
        if (string.IsNullOrEmpty(line))
            return false;

        const string stampFormat = "dd.MM.yyyy HH:mm:ss";
        var quote = line.IndexOf(": \"", StringComparison.Ordinal);
        if (quote != stampFormat.Length ||
            !DateTime.TryParseExact(line[..quote], stampFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var logged))
            return false;

        // text spravy moze obsahovat aj uvodzovky - vysledok zacina az za poslednym '" - '
        var end = line.LastIndexOf("\" - ", StringComparison.Ordinal);
        if (end < quote + 3)
            return false;

        entry = new GtnLogEntry(logged, line[(quote + 3)..end], line[(end + 4)..].Trim());
        return true;
    }
}

/// <summary>
///     Priebežné čítanie logu rozhrania INISSu - pri každom volaní <see cref="Poll" /> vráti len riadky, ktoré
///     medzitým pribudli. INISS log pri prekročení veľkosti odloží (<c>GTN.BAK</c>) a začne nový; kratší súbor
///     sa preto číta znova od začiatku.
/// </summary>
public sealed class GtnLogTail
{
    private readonly Decoder _decoder = Encodings.Win1250.GetDecoder();
    private readonly StringBuilder _pending = new();
    private long _position;

    /// <summary>
    ///     Začne sledovať log. Riadky, ktoré v ňom už sú, sa preskočia.
    /// </summary>
    /// <param name="path">Cesta k logu.</param>
    public GtnLogTail(string path)
    {
        Path = path ?? throw new ArgumentNullException(nameof(path));
        _position = Length();
    }

    /// <summary>
    ///     Cesta k logu.
    /// </summary>
    public string Path { get; }

    /// <summary>
    ///     Či log existuje.
    /// </summary>
    public bool Exists => File.Exists(Path);

    /// <summary>
    ///     Prečíta riadky, ktoré pribudli od posledného volania. Neúplný posledný riadok počká na ďalšie volanie.
    /// </summary>
    /// <returns>Rozobrané záznamy o správach; iné riadky sa preskočia.</returns>
    public IReadOnlyList<GtnLogEntry> Poll()
    {
        var length = Length();
        if (length < _position)
        {
            // novy log po odlozeni stareho
            _position = 0;
            _pending.Clear();
            _decoder.Reset();
        }

        if (length == _position)
            return [];

        byte[] bytes;
        try
        {
            using var stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            stream.Seek(_position, SeekOrigin.Begin);
            bytes = new byte[length - _position];
            stream.ReadExactly(bytes);
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException)
        {
            return [];
        }

        _position = length;

        var chars = new char[_decoder.GetCharCount(bytes, 0, bytes.Length)];
        _decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
        _pending.Append(chars);

        var text = _pending.ToString();
        var last = text.LastIndexOf('\n');
        if (last < 0)
            return [];

        _pending.Clear();
        _pending.Append(text, last + 1, text.Length - last - 1);

        var entries = new List<GtnLogEntry>();
        foreach (var line in text[..last].Split('\n'))
            if (GtnLogEntry.TryParse(line.TrimEnd('\r'), out var entry))
                entries.Add(entry);

        return entries;
    }

    private long Length()
    {
        try
        {
            var info = new FileInfo(Path);
            return info.Exists ? info.Length : 0;
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException)
        {
            return 0;
        }
    }

    /// <summary>
    ///     Nájde log rozhrania v inštalácii INISSu, ku ktorej patrí grafikon (<c>&lt;INISS&gt;\DATA\stanica.rok</c>).
    ///     Prehľadá priečinok programu aj jeho podpriečinky (<c>Logy</c>, <c>DATA</c>) a tieň vo VirtualStore,
    ///     kam zapisuje INISS spustený bez práv správcu z <c>Program Files</c>. Z viacerých vyberie naposledy zapísaný.
    /// </summary>
    /// <param name="gvdDir">Priečinok grafikonu.</param>
    /// <returns>Cesta k logu, alebo <see langword="null" />, ak sa nenašiel.</returns>
    public static string? Find(string gvdDir)
    {
        ArgumentNullException.ThrowIfNull(gvdDir);

        var install = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(gvdDir)));
        if (install is null || !Directory.Exists(install))
            return null;

        var roots = new List<string> { install };
        var root = System.IO.Path.GetPathRoot(install);
        if (root is not null)
            roots.Add(System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "VirtualStore", install[root.Length..]));

        var options = new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive, RecurseSubdirectories = true, MaxRecursionDepth = 2, IgnoreInaccessible = true };

        return roots
            .Where(Directory.Exists)
            .SelectMany(dir => Directory.EnumerateFiles(dir, "GTN.LOG", options))
            .Select(file => new FileInfo(file))
            .OrderByDescending(file => file.LastWriteTimeUtc)
            .FirstOrDefault()?.FullName;
    }
}
