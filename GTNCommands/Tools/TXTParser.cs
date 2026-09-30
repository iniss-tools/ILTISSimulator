using System.Globalization;
using GTNCommands.Entities;
using ToolsCore.Iniss.Tools;

namespace GTNCommands.Tools;

internal static class TxtParser
{
    private const string FORMAT_EX = "Chyba v súbore {0} na riadku {1}. ";

    public static List<Station> GetStations(string path)
    {
        var stanice = new List<Station>();

        using var staniceF = new CsvFileReader(path);
        var riadok = 1;
        var row = new CsvRow();
        while (true)
        {
            var status = staniceF.ReadRow(row);
            if (ParseUtils.LineIsEmpty(status))
            {
                riadok++;
                continue;
            }

            if (ParseUtils.LineIsEof(status))
                break;

            try
            {
                var id = int.Parse(row[0], CultureInfo.InvariantCulture);
                var idstr = id.ToString(CultureInfo.InvariantCulture);
                idstr = idstr.Substring(1);
                id = int.Parse(idstr, CultureInfo.InvariantCulture);

                var name = row[1].AnsiToUTF();
                var stanica = new Station(id, name);

                stanice.Add(stanica);
            }
            catch (Exception e)
            {
                throw new FormatException(string.Format(CultureInfo.InvariantCulture, FORMAT_EX, Path.GetFileName(path), riadok) + e.Message, e);
            }

            riadok++;
        }

        return stanice;
    }
}