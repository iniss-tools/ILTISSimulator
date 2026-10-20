using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ILTISSimulator.Properties;
using ToolsCore.XML;

namespace ILTISSimulator.XML;

/// <summary>
///     Stĺpce tabuľky plánu - poradie, viditeľnosť a najmenšia šírka. V <c>config.xml</c> je každý stĺpec prvok
///     s názvom stĺpca (<c>&lt;Track o="8" v="true" mw="60" /&gt;</c>); stĺpec bez záznamu má predvolené
///     hodnoty, neznámy prvok sa pri načítaní preskočí.
/// </summary>
public sealed class DesktopColumns : IXmlSerializable
{
    // nazov stlpca sa cita az pri pouziti - konfiguracia sa nacita skor, nez sa nastavi jazyk programu
    private static readonly (string Id, Func<string> Name, int MinWidth, bool Visible)[] Definitions =
    [
        (State, () => Resources.Grid_State, 40, true),
        (Train, () => Resources.Grid_Train, 55, true),
        (Type, () => Resources.Grid_Type, 40, true),
        (Routing, () => Resources.Grid_Routing, 40, true),
        (Arrival, () => Resources.Grid_Arrival, 55, true),
        (ArrivalDelay, () => Resources.Grid_ArrivalDelay, 50, true),
        (Departure, () => Resources.Grid_Departure, 55, true),
        (DepartureDelay, () => Resources.Grid_DepartureDelay, 50, true),
        (Track, () => Resources.Grid_Track, 55, true),
        (TrackSource, () => Resources.Grid_TrackSource, 70, true),
        (Reason, () => Resources.Grid_DelayReason, 110, true),
        (Next, () => Resources.Grid_Next, 100, true),
        (Response, () => Resources.Grid_Response, 120, true),
        (Name, () => Resources.Grid_Name, 80, true)
    ];

    /// <summary>Stav jazdy (ikona).</summary>
    public const string State = "State";

    /// <summary>Číslo vlaku.</summary>
    public const string Train = "Train";

    /// <summary>Druh vlaku.</summary>
    public const string Type = "Type";

    /// <summary>Smer vlaku (ikona).</summary>
    public const string Routing = "Routing";

    /// <summary>Príchod podľa grafikonu.</summary>
    public const string Arrival = "Arrival";

    /// <summary>Meškanie na príchode.</summary>
    public const string ArrivalDelay = "ArrivalDelay";

    /// <summary>Odchod podľa grafikonu.</summary>
    public const string Departure = "Departure";

    /// <summary>Meškanie na odchode.</summary>
    public const string DepartureDelay = "DepartureDelay";

    /// <summary>Koľaj.</summary>
    public const string Track = "Track";

    /// <summary>Odkiaľ sa vzala koľaj.</summary>
    public const string TrackSource = "TrackSource";

    /// <summary>Dôvod meškania.</summary>
    public const string Reason = "Reason";

    /// <summary>Najbližšia udalosť.</summary>
    public const string Next = "Next";

    /// <summary>Ako INISS prijal poslednú správu o vlaku.</summary>
    public const string Response = "Response";

    /// <summary>Názov vlaku.</summary>
    public const string Name = "Name";

    private const string AttrOrder = "o";
    private const string AttrVisible = "v";
    private const string AttrMinWidth = "mw";

    private readonly Dictionary<string, DesktopColumn> _columns = new(StringComparer.Ordinal);

    /// <summary>
    ///     Predvolené stĺpce.
    /// </summary>
    public DesktopColumns()
    {
        for (var i = 0; i < Definitions.Length; i++)
        {
            var (id, _, minWidth, visible) = Definitions[i];
            _columns[id] = new DesktopColumn("", id, i, minWidth, visible);
        }
    }

    /// <summary>
    ///     Stĺpec podľa identifikátora.
    /// </summary>
    /// <param name="id">Identifikátor stĺpca, napr. <see cref="Track" />.</param>
    public DesktopColumn this[string id] => _columns[id];

    /// <summary>
    ///     Všetky stĺpce v poradí zobrazenia, s názvami v aktuálnom jazyku programu.
    /// </summary>
    /// <returns>Stĺpce.</returns>
    public IList<DesktopColumn> GetValues()
    {
        foreach (var (id, name, _, _) in Definitions)
            _columns[id].Name = name();

        return _columns.Values.OrderBy(column => column.Order).ToList();
    }

    /// <summary>
    ///     Prevezme stĺpce upravené v nastaveniach - poradie podľa poradia v zozname.
    /// </summary>
    /// <param name="columns">Stĺpce.</param>
    public void SetValues(IEnumerable<DesktopColumn> columns)
    {
        ArgumentNullException.ThrowIfNull(columns);

        var index = 0;
        foreach (var column in columns)
        {
            if (!_columns.ContainsKey(column.PropertyName))
                continue;

            _columns[column.PropertyName] = column with { Order = index++ };
        }
    }

    /// <summary>
    ///     Nezávislá kópia.
    /// </summary>
    /// <returns>Kópia stĺpcov.</returns>
    public DesktopColumns Clone()
    {
        var copy = new DesktopColumns();
        foreach (var (id, column) in _columns)
            copy._columns[id] = column with { };

        return copy;
    }

    XmlSchema? IXmlSerializable.GetSchema() => null;

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        var empty = reader.IsEmptyElement;
        reader.ReadStartElement();
        if (empty)
            return;

        reader.MoveToContent();
        while (reader.NodeType != XmlNodeType.EndElement && !reader.EOF)
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                if (_columns.TryGetValue(reader.LocalName, out var column))
                {
                    if (int.TryParse(reader.GetAttribute(AttrOrder), NumberStyles.Integer, CultureInfo.InvariantCulture, out var order))
                        column.Order = order;
                    if (bool.TryParse(reader.GetAttribute(AttrVisible), out var visible))
                        column.Visible = visible;
                    if (int.TryParse(reader.GetAttribute(AttrMinWidth), NumberStyles.Integer, CultureInfo.InvariantCulture, out var minWidth))
                        column.MinWidth = minWidth;
                }

                reader.Skip();
            }
            else
            {
                reader.Read();
            }

            reader.MoveToContent();
        }

        reader.ReadEndElement();
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
        foreach (var column in _columns.Values.OrderBy(column => column.Order))
        {
            writer.WriteStartElement(column.PropertyName);
            writer.WriteAttributeString(AttrOrder, column.Order.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString(AttrVisible, column.Visible ? "true" : "false");
            writer.WriteAttributeString(AttrMinWidth, column.MinWidth.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();
        }
    }
}
