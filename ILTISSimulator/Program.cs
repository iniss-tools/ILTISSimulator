using ILTISSimulator.XML;
using ToolsCore;

namespace ILTISSimulator;

internal static class Program
{
    /// <summary>
    ///     Vstupný bod aplikácie - nastavenia programu sa načítajú tu a hlavné okno ich dostane v konštruktore.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        var session = AppInit.Initialization<IltisConfig, IltisStyle>();

        AppInit.Run(session.Config, () => new FMain(session));
    }
}
