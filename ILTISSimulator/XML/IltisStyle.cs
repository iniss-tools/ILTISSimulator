using ToolsCore.XML;

namespace ILTISSimulator.XML;

/// <summary>
///     Štýl programu (farby a písma) v <c>styles.xml</c>.
/// </summary>
public record IltisStyle : Style
{
    /// <inheritdoc />
    public IltisStyle()
    {
    }

    /// <inheritdoc />
    private IltisStyle(Style original) : base(original)
    {
    }

    /// <summary>
    ///     Predvolený svetlý štýl.
    /// </summary>
    public new static IltisStyle DefaultLightStyle => new(Style.DefaultLightStyle);

    /// <summary>
    ///     Predvolený tmavý štýl.
    /// </summary>
    public new static IltisStyle DefaultDarkStyle => new(Style.DefaultDarkStyle);

    /// <inheritdoc />
    public override string ToString() => Name;
}
