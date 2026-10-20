using ILTISSimulator.Gtn;
using ILTISSimulator.Simulation;

namespace ILTISSimulator.DocScreenshots;

/// <summary>
///     Snímky okien ILTISSimulatora pre jednu tému.
/// </summary>
internal sealed class Shots(Program.Options options, string theme, List<string> log)
{
    /// <summary>
    ///     Simulovaný deň - pondelok v platnosti ukážkového grafikonu.
    /// </summary>
    private static readonly DateTime Day = new(2027, 3, 15);

    private int _count;

    public int Run(string gvdDir)
    {
        using var main = new MainWindow(gvdDir);

        Shot("hlavne-okno/hlavne-okno", main.Form, () => main.PrepareRunningDay(Day));
        if (options.Check)
            main.CheckEditing(log);
        Shot("hlavne-okno/rucna-sprava", main.Form, () => main.ShowManualMessage(true));
        main.ShowManualMessage(false);

        // v nastaveniach ma byt predvolene spojenie, nie port falosneho INISSu
        Program.Session.Config.Connection.Port = 4000;
        foreach (var (slug, panel) in new[]
                 {
                     ("nastavenia-programu", "pGeneral"),
                     ("simulacia", "pSimulation"),
                     ("spojenie", "pConnection"),
                     ("komponenty", "pDesktopComponents"),
                     ("stlpce", "pDesktopColumns"),
                     ("lokalizacia", "pLocalization"),
                     ("klavesove-skratky", "pShortcuts"),
                     ("styly", "pStyles"),
                     ("pisma", "pFonts"),
                     ("logovanie", "pLogging")
                 })
            Dialog($"nastavenia-programu/{slug}", () => MainWindow.CreateSettings(panel));
        Dialog("plan/pridat-meskanie", () => MainWindow.CreateDialog("ILTISSimulator.FAddDelay", 2, Day.AddHours(9).AddMinutes(15)));
        Dialog("plan/kolaje", main.CreateTracks);
        Dialog("plan/vyluka-kolaje", () => main.CreateTrackClosure(Day));

        return _count;
    }

    private void Dialog(string name, Func<Form> create)
    {
        if (options.Only is { } only && !name.Contains(only, StringComparison.Ordinal))
            return;

        using var form = create();
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new Point(80, 80);
        form.Show();
        Pump.Events(40);
        form.Refresh();
        Pump.Events(20);

        Shot(name, form, () => { });
        form.Close();
    }

    private void Shot(string name, Form form, Action prepare)
    {
        if (options.Only is { } only && !name.Contains(only, StringComparison.Ordinal))
            return;

        prepare();
        Pump.Events();

        var file = Path.Combine(options.OutDir, $"{name}-{theme}.png");
        WindowCapture.Save(form, file);
        log.Add("snímka: " + file);
        _count++;
    }
}

/// <summary>
///     Hlavné okno simulátora ovládané cez reflexiu - okno je v programe interné.
/// </summary>
internal sealed class MainWindow : IDisposable
{
    private readonly string _gvdDir;
    private readonly FakeIniss _iniss;

    public MainWindow(string gvdDir)
    {
        _gvdDir = gvdDir;

        var type = typeof(SimulationEngine).Assembly.GetType("ILTISSimulator.FMain", true)!;
        Form = (Form)Activator.CreateInstance(type, Program.Session)!;

        // FMain_Load by argumenty harnessu bral ako cestu ku grafikonu a registroval jump list
        Form.Load -= (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), Form, "FMain_Load");
        Form.StartPosition = FormStartPosition.Manual;
        Form.Location = new Point(40, 40);
        Form.Size = new Size(1360, 780);
        Form.Show();
        Pump.Events();

        Invoke("LoadGvd", gvdDir);
        Pump.Events();

        var install = Path.GetDirectoryName(Path.GetDirectoryName(gvdDir))!;
        var tracks = Gvd.Tracks.Select(track => track.Key);
        _iniss = new FakeIniss(Path.Combine(install, "Logy", "GTN.LOG"), Planned, tracks);
    }

    public Form Form { get; }

    private Data.GvdData Gvd => (Data.GvdData)Field("_gvd")!;

    private SimulationEngine? Engine => (SimulationEngine?)Field("_engine");

    /// <summary>
    ///     Deň v behu: pripojený INISS, naplánované od rána, niekoľko ručných zmien a udalosti do 9:15 odoslané.
    /// </summary>
    public void PrepareRunningDay(DateTime day)
    {
        Program.Session.Config.Connection.Host = "127.0.0.1";
        Program.Session.Config.Connection.Port = _iniss.Port;
        Invoke("ToggleConnection");

        ((ExControls.ExDateTimePicker)Field("dtpDay")!).Value = day;
        ((ExControls.ExDateTimePicker)Field("dtpFrom")!).Value = day.AddHours(5);
        Invoke("Plan", day, day.AddHours(5), null, null);
        var engine = Engine!;
        var runs = engine.Runs!;

        // rucne zmeny - meskanie s dovodom, ina kolaj, vlak bez sprav
        var withArrival = runs.Where(run => run.PlannedArrival.HasValue && run.PlannedArrival > day.AddHours(9)).ToList();
        if (withArrival.Count > 0)
        {
            engine.Edit(withArrival[0], (run, context) => PlanEditing.SetArrivalDelay(run, TimeSpan.FromMinutes(12), context));
            engine.Edit(withArrival[0], (run, context) => PlanEditing.SetDelayReason(run, 3, context));
        }

        var later = runs.Where(run => (run.PlannedArrival ?? run.PlannedDeparture) > day.AddHours(10)).ToList();
        if (later.Count > 1)
        {
            var other = Gvd.Tracks.Where(TrackAllocator.CanUse).First(track => track.Key != later[0].Track.Key);
            engine.Edit(later[0], (run, context) => PlanEditing.SetTrack(run, other, context));
            engine.Edit(later[1], (run, context) => PlanEditing.SetSuppressed(run, true, context));
        }

        var until = day.AddHours(9).AddMinutes(15);
        var sent = engine.Pump(until);
        engine.JumpTo(until);

        Pump.Until(() => _iniss.Lines >= sent, 10000);
        Pump.Events(30);
        Invoke("RefreshAllRows");
        Invoke("UpdateStatus");
        Pump.Events();
    }

    /// <summary>
    ///     Panel ručnej správy s vybraným vlakom.
    /// </summary>
    public void ShowManualMessage(bool visible)
    {
        Invoke("SetManualVisible", visible);
        if (!visible)
            return;

        var grid = (DataGridView)Field("dgvPlan")!;
        var row = grid.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Tag is TrainRun { IsFinished: false, Suppressed: false });
        if (row is not null)
        {
            grid.ClearSelection();
            grid.CurrentCell = row.Cells[1];
            row.Selected = true;
        }

        Pump.Events();
    }

    public static Form CreateSettings(string page)
    {
        var form = CreateDialog("ILTISSimulator.FAppSettings", Program.Session);
        form.GetType().GetMethod("PreselectMenuItem", Program.Any)!.Invoke(form, [page]);
        return form;
    }

    private static readonly string[] ExcludedTrack = ["4"];

    public Form CreateTracks() => CreateDialog("ILTISSimulator.FTracks", Gvd.Tracks, ExcludedTrack);

    /// <summary>
    ///     Výluka koľaje, ktorú od 9:15 do 11:15 drží najviac vlakov.
    /// </summary>
    public Form CreateTrackClosure(DateTime day)
    {
        if (Engine?.Runs is null)
            PrepareRunningDay(day);

        var runs = Engine!.Runs!;
        var from = day.AddHours(9).AddMinutes(15);
        var closed = Gvd.Tracks.Where(TrackAllocator.CanUse)
            .MaxBy(track => TrackClosure.Affected(runs, track, from, from.AddHours(2)).Count)!;

        return CreateDialog("ILTISSimulator.FTrackClosure", Gvd.Tracks, runs, from, closed);
    }

    public static Form CreateDialog(string type, params object[] args) =>
        (Form)Activator.CreateInstance(typeof(SimulationEngine).Assembly.GetType(type, true)!, args)!;

    /// <summary>
    ///     Kontrola úprav v tabuľke bez klikania - zápis do buniek ako po úprave používateľom a porovnanie s jazdou.
    /// </summary>
    public void CheckEditing(List<string> log)
    {
        var grid = (DataGridView)Field("dgvPlan")!;
        var row = grid.Rows.Cast<DataGridViewRow>().First(r => r.Tag is TrainRun { IsFinished: false, Suppressed: false, PlannedArrival: not null } run
                                                             && !run.IsDone(SimEventKind.Arrival));
        var run = (TrainRun)row.Tag!;
        var other = Gvd.Tracks.Where(TrackAllocator.CanUse).First(track => track.Key != run.Track.Key);

        void Check(string what, bool ok) => log.Add($"kontrola {what}: {(ok ? "OK" : "CHYBA")}");

        row.Cells["cArrivalDelay"].Value = "+7";
        Pump.Events();
        Check("meškanie príchodu", run.ArrivalDelay == TimeSpan.FromMinutes(7));

        row.Cells["cTrack"].Value = other.Key;
        Pump.Events();
        Check("koľaj", run.Track.Key == other.Key && run.TrackSource == TrackSource.Manual);

        var reason = grid.Columns["cReason"] is DataGridViewComboBoxColumn combo ? combo.Items[2] as string : null;
        row.Cells["cReason"].Value = reason;
        Pump.Events();
        Check("dôvod", run.DelayReason == 2);

        row.Cells["cArrivalDelay"].Value = "x";
        Pump.Events();
        Check("zlé meškanie", run.ArrivalDelay == TimeSpan.FromMinutes(7) && row.Cells["cArrivalDelay"].ErrorText.Length > 0);

        grid.ClearSelection();
        row.Selected = true;
        Invoke("RevertSelected");
        Pump.Events();
        Check("vrátiť", !run.IsChanged);
    }

    private DateTime? Planned(string number, GtnMessageType type)
    {
        var run = Engine?.Runs?.FirstOrDefault(candidate => candidate.Train.Number == number);
        return type == GtnMessageType.Arrival ? run?.PlannedArrival : run?.PlannedDeparture;
    }

    private object? Field(string name) => Form.GetType().GetField(name, Program.Any)!.GetValue(Form);

    private object? Invoke(string name, params object?[] args)
    {
        var method = Form.GetType().GetMethods(Program.Any).Single(m => m.Name == name && m.GetParameters().Length == args.Length);
        return method.Invoke(Form, args);
    }

    public void Dispose()
    {
        Form.Close();
        Form.Dispose();
        _iniss.Dispose();
    }
}
