using ILTISSimulator.Tools;

namespace ILTISSimulator;

internal static class Program
{
    /// <summary>
    ///     Vstupný bod aplikácie.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // PerMonitorV2 pred prvym oknom - povodne App.config (DpiAwareness=PerMonitorV2); rozmery z navrhu
        // dopocitava Tools\DpiTools, ak ich Windows Forms nepreskaluju samy
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        AppSettings.Load();
        Texts.SetLanguage(AppSettings.Language);

        Application.Run(new FMain());
    }
}
