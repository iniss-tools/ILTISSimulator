using GTNCommands.Entities;
using ToolsCore.Iniss.Tools;

namespace GTNCommands.Tools;

internal static class TXTParser
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

            if (ParseUtils.LineIsEOF(status))
                break;

            try
            {
                var id = int.Parse(row[0]);
                var idstr = id.ToString();
                idstr = idstr.Substring(1);
                id = int.Parse(idstr);

                var name = row[1].ANSItoUTF();
                var stanica = new Station(id, name);

                stanice.Add(stanica);
            }
            catch (Exception e)
            {
                throw new FormatException(string.Format(FORMAT_EX, Path.GetFileName(path), riadok) + e.Message, e);
            }

            riadok++;
        }

        return stanice;
    }
}