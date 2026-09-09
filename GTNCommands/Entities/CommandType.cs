using ToolsCore.Tools;

namespace GTNCommands.Entities;

internal class CommandType : Enumeration<CommandType>
{
    public static readonly CommandType VVC = new("VVC", "vjazdová vlaková cesta");
    public static readonly CommandType OVC = new("OVC", "odchodová vlaková cesta");
    public static readonly CommandType Od =  new("Od ", "odchod vlaku");
    public static readonly CommandType Vj =  new("Vj ", "vjazd vlaku");

    private CommandType(string key, string name) : base(key,name)
    {
    }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"{Key} - {Name}";
    }
}