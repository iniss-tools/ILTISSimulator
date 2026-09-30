namespace GTNCommands;

internal static class Program
{
    /// <summary>
    ///     Hlavní vstupní bod aplikace.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // pred prvym oknom; predtym dpiAware v app.manifest
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FMain());
    }
}