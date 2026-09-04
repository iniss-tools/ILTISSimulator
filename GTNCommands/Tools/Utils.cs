using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GTNCommands.Tools
{
    internal static class Utils
    {
        public const string REG_RECENT_DIRS = "RecentDirs";
        public const string REG_LAST_IP = "LastIP";
        public const string REG_LAST_PORT = "LastPort";

        public static string GetRecentPath()
        {
            var key = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\{Application.ProductName}");
            var val = key?.GetValue(REG_RECENT_DIRS);
            return val != null ? val.ToString() : "";
        }

        public static void SetRecentPath(string path)
        {
            var key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{Application.ProductName}");

            key?.SetValue(REG_RECENT_DIRS, path);
        }

        public static void GetLastConnection(out string ip, out int port)
        {
            var key = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\{Application.ProductName}");

            if (key != null)
            {
                var ipO = key.GetValue(REG_LAST_IP);
                var portO = key.GetValue(REG_LAST_PORT);

                ip = ipO == null ? "" : ipO.ToString();
                port = portO == null ? 0 : int.Parse(portO.ToString());
                return;
            }

            ip = "";
            port = 0;
        }

        public static void SetLastConnection(string ip, int port)
        {
            var key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{Application.ProductName}");

            key?.SetValue(REG_LAST_IP, ip);
            key?.SetValue(REG_LAST_PORT, port);
        }

        public static bool CheckIP(string ipstr) => IPAddress.TryParse(ipstr, out _);
    }
}