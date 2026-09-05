namespace GTNCommands;

internal static class Program
{
    /// <summary>
    ///     Hlavní vstupní bod aplikace.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FMain());
    }
}