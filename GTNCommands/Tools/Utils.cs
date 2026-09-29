using System.Globalization;
using System.Net;
using Microsoft.Win32;

namespace GTNCommands.Tools;

internal static class Utils
{
    public const string RegRecentDirs = "RecentDirs";
    public const string RegLastIp = "LastIP";
    public const string RegLastPort = "LastPort";

    public static string GetRecentPath()
    {
        var key = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\{Application.ProductName}");
        var val = key?.GetValue(RegRecentDirs);
        return val != null ? val.ToString() ?? "" : "";
    }

    public static void SetRecentPath(string path)
    {
        var key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{Application.ProductName}");

        key?.SetValue(RegRecentDirs, path);
    }

    public static void GetLastConnection(out string ip, out int port)
    {
        var key = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\{Application.ProductName}");

        if (key != null)
        {
            var ipO = key.GetValue(RegLastIp);
            var portO = key.GetValue(RegLastPort);

            ip = ipO == null ? "" : ipO.ToString() ?? "";
            port = portO == null ? 0 : int.Parse(portO.ToString() ?? "0", CultureInfo.InvariantCulture);
            return;
        }

        ip = "";
        port = 0;
    }

    public static void SetLastConnection(string ip, int port)
    {
        var key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{Application.ProductName}");

        key?.SetValue(RegLastIp, ip);
        key?.SetValue(RegLastPort, port);
    }

    public static bool CheckIp(string ipstr) => IPAddress.TryParse(ipstr, out _);
}