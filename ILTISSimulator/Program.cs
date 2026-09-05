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
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        AppSettings.Load();
        Texts.SetLanguage(AppSettings.Language);

        Application.Run(new FMain());
    }
}
