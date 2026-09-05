using System.ComponentModel;
using ExControls;
using GVDEditor.Entities;
using ILTISSimulator.Data;
using ILTISSimulator.Gtn;
using ILTISSimulator.Net;
using ILTISSimulator.Properties;
using ILTISSimulator.Simulation;
using ILTISSimulator.Tools;

namespace ILTISSimulator;

/// <summary>
///     Hlavné okno simulátora ILTISu.
/// </summary>
internal partial class FMain : Form
{
    private readonly Dictionary<TrainRun, int> _rowByRun = new();
    private ISet<string> _excludedTracks = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    private IReadOnlyList<DelayReason> _delayReasons = new List<DelayReason>();
    private GvdData _gvd;
    private SimulationEngine _engine;
    private GtnClient _client;
    private int _sentCount;
    private bool _manualUpdating;
    private string _manualGenerated = "";

    /// <summary>
    ///     Vytvorí hlavné okno a načíta uložené nastavenia.
    /// </summary>
    public FMain()
    {
        InitializeComponent();

        DpiTools.ScaleToDpi(this, DesignClientWidth);
        slLanguage.Size = new Size(DpiTools.Scaled(statusStrip, DesignLanguageWidth), slLanguage.Height);

        rtbLog.SetTheme(WindowsTheme.Default);
        InitGrid();

        _manualUpdating = true;

        tbGvdDir.Text = AppSettings.GvdDir;
        tbHost.Text = AppSettings.Host;
        nudPort.Value = AppSettings.Port;
        nudSeed.Value = AppSettings.Seed;
        nudFactor.Value = AppSettings.TimeFactor;
        nudOnTime.Value = AppSettings.OnTimePercent;
        nudMeanDelay.Value = AppSettings.MeanDelayMinutes;
        nudDiversion.Value = AppSettings.DiversionPercent;
        nudCancel.Value = AppSettings.CancellationPercent;
        nudReason.Value = AppSettings.DelayReasonPercent;

        dtpDay.Value = DateTime.Today;
        dtpFrom.Value = DateTime.Now;
        dtpManualTime.Value = DateTime.Now;
        dtpManualTime.Enabled = !cbManualNow.Checked;

        ApplyTexts();

        _manualUpdating = false;

        UpdateManualFields();
        UpdateManualPreview();
        UpdateButtons();

        if (!string.IsNullOrWhiteSpace(tbGvdDir.Text) && Directory.Exists(tbGvdDir.Text))
            LoadGvd(tbGvdDir.Text);
    }

    #region Rozmery

    /// <summary>
    ///     Šírka vnútra okna podľa návrhu - viď <see cref="DpiTools.ScaleToDpi" />.
    /// </summary>
    private const int DesignClientWidth = 1160;

    /// <summary>
    ///     Šírka zoznamu jazykov v stavovom riadku podľa návrhu.
    /// </summary>
    private const int DesignLanguageWidth = 130;

    /// <summary>
    ///     Zrovná okno na obrazovku a rozdelí plochu medzi plán a záznam. Až tu, lebo výšku deliča má zmysel
    ///     rátať z rozloženia, ktoré je hotové.
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        DpiTools.FitToScreen(this);

        if (splitMain.Height > splitMain.Panel1MinSize + splitMain.Panel2MinSize + splitMain.SplitterWidth)
            splitMain.SplitterDistance = splitMain.Height * 55 / 100;
    }

    #endregion

    #region Texty a jazyk

    /// <summary>
    ///     Znovu načíta texty okna zo zdrojov. Volá sa po prepnutí jazyka.
    /// </summary>
    /// <remarks>
    ///     Texty ovládacích prvkov sú v <c>FMain.resx</c> pod menom prvku, presne ako ich tam ukladá návrhár;
    ///     pri štarte ich dosadí <c>ApplyResources</c> v <c>InitializeComponent</c>, tu sa iba načítajú znovu
    ///     v novom jazyku. Zvyšok sú reťazce skladané za behu, tie sú v spoločných zdrojoch.
    /// </remarks>
    private void ApplyTexts()
    {
        var updating = _manualUpdating;
        _manualUpdating = true;
        
        var resources = new ComponentResourceManager(typeof(FMain));

        if (resources.GetString("$this.Text") is { Length: > 0 } title)
            Text = title;

        ApplyControlTexts(resources, this);

        ttMain.SetToolTip(bBrowseGvd, resources.GetString("bBrowseGvd.ToolTip"));
        ttMain.SetToolTip(tbManual, resources.GetString("tbManual.ToolTip"));
        slLanguage.ToolTipText = resources.GetString("slLanguage.ToolTipText");
        fbdGvd.Description = Resources.Main_ChooseGvdDir;

        FillLanguages();
        FillManualChoices();
        ApplyGridHeaders();
        UpdateStatus();

        if (_gvd is null)
            lGvdInfo.Text = Resources.Main_GvdNotLoaded;
        else
            UpdateGvdInfo();

        UpdateConnectionLabel();

        _manualUpdating = updating;

        if (!_manualUpdating)
        {
            FillGrid();
            UpdateManualPreview();
        }
    }

    /// <summary>
    ///     Dosadí prvkom texty zo zdrojov okna.
    /// </summary>
    /// <remarks>
    ///     Dosadzujú sa iba neprázdne texty - prvky, ktorých obsah vzniká za behu, v zdrojoch text nemajú
    ///     alebo ho majú prázdny a prepísať sa nesmú.
    /// </remarks>
    /// <param name="resources">Zdroje okna.</param>
    /// <param name="parent">Prvok, ktorého potomkovia sa prejdú.</param>
    private static void ApplyControlTexts(ComponentResourceManager resources, Control parent)
    {
        foreach (Control child in parent.Controls)
        {
            if (!string.IsNullOrEmpty(child.Name) &&
                resources.GetString(child.Name + ".Text") is { Length: > 0 } text)
                child.Text = text;

            ApplyControlTexts(resources, child);
        }
    }

    private void FillLanguages()
    {
        Refill(slLanguage.ComboBox, Enum.GetValues(typeof(AppLanguage)).Cast<AppLanguage>(), Of);
        Select(slLanguage.ComboBox, AppSettings.Language);
    }

    private void slLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_manualUpdating || slLanguage.SelectedItem is not Choice<AppLanguage> choice ||
            choice.Value == AppSettings.Language)
            return;

        AppSettings.Language = choice.Value;
        Texts.SetLanguage(choice.Value);
        ApplyTexts();
    }

    /// <summary>
    ///     Názov jazyka do zoznamu jazykov.
    /// </summary>
    /// <param name="language">Jazyk.</param>
    /// <returns>Názov jazyka.</returns>
    private static string Of(AppLanguage language) => language switch
    {
        AppLanguage.Czech => Resources.Main_LanguageCzech,
        _ => Resources.Main_LanguageSlovak
    };

    /// <summary>
    ///     Názov typu správy aj s jej kódom.
    /// </summary>
    /// <param name="type">Typ správy.</param>
    /// <returns>Text do zoznamu, napríklad <c>0802 - Príchod vlaku</c>.</returns>
    private static string Of(GtnMessageType type)
    {
        if (type is null)
            return "";

        var name = type.Key switch
        {
            "0802" => Resources.MessageType_Arrival,
            "0803" => Resources.MessageType_Departure,
            "0804" => Resources.MessageType_Route,
            "0805" => Resources.MessageType_DelayReason,
            "0806" => Resources.MessageType_Transfer,
            _ => type.Name
        };

        return $"{type.Key} - {name}";
    }

    /// <summary>
    ///     Názov druhu vlakovej cesty.
    /// </summary>
    /// <param name="kind">Druh vlakovej cesty.</param>
    /// <returns>Text do zoznamu.</returns>
    private static string Of(GtnRouteKind kind) => kind switch
    {
        GtnRouteKind.Exit => Resources.RouteKind_Exit,
        GtnRouteKind.TrainNumberSet => Resources.RouteKind_TrainNumberSet,
        _ => Resources.RouteKind_Entry
    };

    /// <summary>
    ///     Názov druhu hlásenia o prestupe.
    /// </summary>
    /// <param name="kind">Druh prestupu.</param>
    /// <returns>Text do zoznamu.</returns>
    private static string Of(GtnTransferKind kind) => kind == GtnTransferKind.Express
        ? Resources.TransferKind_Express
        : Resources.TransferKind_Normal;

    #endregion

    #region Tabuľka plánu

    private void InitGrid()
    {
        dgvPlan.Columns.Add(NewColumn("colTrain", 60, 50));
        dgvPlan.Columns.Add(NewColumn("colType", 45, 40));
        dgvPlan.Columns.Add(NewColumn("colRouting", 45, 40));
        dgvPlan.Columns.Add(NewColumn("colArrival", 60, 55));
        dgvPlan.Columns.Add(NewColumn("colArrivalDelay", 55, 50));
        dgvPlan.Columns.Add(NewColumn("colDeparture", 60, 55));
        dgvPlan.Columns.Add(NewColumn("colDepartureDelay", 55, 50));
        dgvPlan.Columns.Add(NewColumn("colTrack", 50, 45));
        dgvPlan.Columns.Add(NewColumn("colSource", 80, 70));
        dgvPlan.Columns.Add(NewColumn("colReason", 130, 90));
        dgvPlan.Columns.Add(NewColumn("colEvents", 60, 50));
        dgvPlan.Columns.Add(NewColumn("colName", 150, 80));
    }

    /// <remarks>
    ///     Stĺpce sa roztiahnu na šírku tabuľky v pomere <see cref="DataGridViewColumn.FillWeight" />, aby
    ///     napravo nezostávalo prázdne miesto. Minimálna šírka bráni tomu, aby sa v úzkom okne zlepili.
    /// </remarks>
    private static DataGridViewTextBoxColumn NewColumn(string name, int weight, int minimumWidth) => new()
    {
        Name = name,
        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
        FillWeight = weight,
        MinimumWidth = minimumWidth,
        SortMode = DataGridViewColumnSortMode.NotSortable
    };

    private void ApplyGridHeaders()
    {
        dgvPlan.Columns["colTrain"].HeaderText = Resources.Grid_Train;
        dgvPlan.Columns["colType"].HeaderText = Resources.Grid_Type;
        dgvPlan.Columns["colRouting"].HeaderText = Resources.Grid_Routing;
        dgvPlan.Columns["colArrival"].HeaderText = Resources.Grid_Arrival;
        dgvPlan.Columns["colArrivalDelay"].HeaderText = Resources.Grid_ArrivalDelay;
        dgvPlan.Columns["colDeparture"].HeaderText = Resources.Grid_Departure;
        dgvPlan.Columns["colDepartureDelay"].HeaderText = Resources.Grid_DepartureDelay;
        dgvPlan.Columns["colTrack"].HeaderText = Resources.Grid_Track;
        dgvPlan.Columns["colSource"].HeaderText = Resources.Grid_TrackSource;
        dgvPlan.Columns["colReason"].HeaderText = Resources.Grid_DelayReason;
        dgvPlan.Columns["colEvents"].HeaderText = Resources.Grid_Events;
        dgvPlan.Columns["colName"].HeaderText = Resources.Grid_Name;
    }

    private void FillGrid()
    {
        dgvPlan.Rows.Clear();
        _rowByRun.Clear();

        if (_engine?.Runs is null)
            return;

        foreach (var run in _engine.Runs)
        {
            var index = dgvPlan.Rows.Add(
                run.Train.Number,
                run.Train.Type?.Key,
                run.Train.Routing?.CharSymbol,
                Time(run.PlannedArrival),
                Delay(run.PlannedArrival, run.ArrivalDelay),
                Time(run.PlannedDeparture),
                Delay(run.PlannedDeparture, run.DepartureDelay),
                run.Cancelled ? "" : run.Track?.Key,
                run.Cancelled ? Resources.Source_Cancelled : Texts.Of(run.TrackSource),
                ReasonText(run.DelayReason),
                run.Events.Count,
                run.Train.Name);

            var row = dgvPlan.Rows[index];
            row.Tag = run;
            _rowByRun[run] = index;

            if (run.Cancelled)
                row.DefaultCellStyle.ForeColor = SystemColors.GrayText;
            else if (run.TrackSource == TrackSource.Conflict)
                row.DefaultCellStyle.ForeColor = Color.Firebrick;
            else if (run.TrackSource != TrackSource.Timetable)
                row.DefaultCellStyle.ForeColor = Color.DarkGoldenrod;
        }
    }

    private static string Time(DateTime? time) => time?.ToString("HH:mm") ?? "";

    private static string Delay(DateTime? time, TimeSpan delay) =>
        time is null || delay == TimeSpan.Zero ? "" : $"+{delay.TotalMinutes:0}";

    #endregion

    #region Grafikon

    private void bBrowseGvd_Click(object sender, EventArgs e)
    {
        if (Directory.Exists(tbGvdDir.Text))
            fbdGvd.SelectedPath = tbGvdDir.Text;

        if (fbdGvd.ShowDialog(this) == DialogResult.OK)
        {
            tbGvdDir.Text = fbdGvd.SelectedPath;
            LoadGvd(fbdGvd.SelectedPath);
        }
    }

    private void bLoadGvd_Click(object sender, EventArgs e) => LoadGvd(tbGvdDir.Text);

    private void LoadGvd(string dir)
    {
        try
        {
            _gvd = GvdLightReader.Read(dir);
        }
        catch (Exception e)
        {
            _gvd = null;
            AppendLog(LogKind.Error, Texts.F(Resources.Log_GvdLoadFailed, e.Message));
            lGvdInfo.Text = Resources.Main_GvdNotLoaded;
            UpdateStatus();
            UpdateButtons();
            return;
        }

        DisposeEngine();
        FillGrid();

        AppSettings.GvdDir = dir;
        _excludedTracks = AppSettings.GetExcludedTracks(_gvd.StationId);
        LoadDelayReasons(dir);

        UpdateGvdInfo();
        UpdateStatus();
        FillManualFromGvd();

        AppendLog(LogKind.Info,
            Texts.F(Resources.Log_GvdLoaded, dir, _gvd.Trains.Count, UsableTracks().Count));

        UpdateButtons();
    }

    private List<Track> UsableTracks() => _gvd.Tracks.Where(TrackAllocator.CanUse).ToList();

    private List<DelayReason> UsableReasons() => _delayReasons.Where(reason => reason.Usable).ToList();

    private void LoadDelayReasons(string gvdDir)
    {
        try
        {
            _delayReasons = DelayReasonReader.ReadForGvd(gvdDir);
        }
        catch (Exception e)
        {
            _delayReasons = new List<DelayReason>();
            AppendLog(LogKind.Warning, Texts.F(Resources.Log_ReasonsFailed, e.Message));
            return;
        }

        if (_delayReasons.Count == 0)
        {
            AppendLog(LogKind.Warning, Texts.F(Resources.Log_ReasonsMissing, DelayReasonReader.SoundGroup));
            return;
        }

        foreach (var reason in _delayReasons)
            AppendLog(LogKind.Info, reason.Usable
                ? Texts.F(Resources.Log_Reason, reason.Number.ToString("00"), reason.Key, reason.Text)
                : Texts.F(Resources.Log_ReasonUnusable, reason.Number.ToString("00"), reason.Key));
    }

    private string ReasonText(int number)
    {
        if (number == 0)
            return "";

        var reason = _delayReasons.FirstOrDefault(candidate => candidate.Number == number);

        return reason is null ? number.ToString("00") : reason.ToString();
    }

    private void UpdateGvdInfo()
    {
        if (_gvd is null)
            return;

        var usable = UsableTracks().Count;
        var reasons = Texts.Count(UsableReasons().Count, Resources.Main_ReasonCountOne,
            Resources.Main_ReasonCountFew, Resources.Main_ReasonCountMany);

        lGvdInfo.Text =
            Texts.F(Resources.Main_GvdInfoStation, _gvd.Info.ThisStation.Name, _gvd.StationId,
                StationCodec.Encode(_gvd.StationId)) + "\r\n" +
            Texts.F(Resources.Main_GvdInfoContent, _gvd.Trains.Count, usable - _excludedTracks.Count, usable,
                reasons) + "\r\n" +
            Texts.F(Resources.Main_GvdInfoValid, _gvd.Info.StartValidData, _gvd.Info.EndValidData);
    }

    private void bTracks_Click(object sender, EventArgs e)
    {
        if (_gvd is null)
            return;

        using var dialog = new FTracks(_gvd.Tracks, _excludedTracks);
        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        _excludedTracks = dialog.ExcludedKeys;
        AppSettings.SetExcludedTracks(_gvd.StationId, _excludedTracks);

        UpdateGvdInfo();
        AppendLog(LogKind.Info, _excludedTracks.Count == 0
            ? Resources.Log_NoExcludedTracks
            : Texts.F(Resources.Log_ExcludedTracks, string.Join(", ", _excludedTracks.OrderBy(key => key))));
    }

    #endregion

    #region Spojenie

    private void bConnect_Click(object sender, EventArgs e)
    {
        try
        {
            _client?.Dispose();
            _client = new GtnClient(tbHost.Text.Trim(), decimal.ToInt32(nudPort.Value));
            _client.Connect();

            AppSettings.Host = tbHost.Text.Trim();
            AppSettings.Port = decimal.ToInt32(nudPort.Value);

            AppendLog(LogKind.Connected, Texts.F(Resources.Log_Connected, _client.Host, _client.Port));
        }
        catch (Exception ex)
        {
            _client = null;
            AppendLog(LogKind.Error, Texts.F(Resources.Log_ConnectFailed, ex.Message));
        }

        UpdateConnectionLabel();
    }

    private void UpdateConnectionLabel()
    {
        var connected = _client is { IsConnected: true };

        lConnection.Text = connected ? Resources.Main_Connected : Resources.Main_Disconnected;
        lConnection.ForeColor = connected ? Color.Green : Color.Firebrick;
    }

    #endregion

    #region Simulácia

    private void bPlan_Click(object sender, EventArgs e)
    {
        if (_gvd is null)
            return;

        try
        {
            DisposeEngine();

            _engine = new SimulationEngine(_gvd, ReadOptions());
            _engine.Log += Engine_Log;
            _engine.EventDue += Engine_EventDue;

            var day = dtpDay.Value.Date;
            _engine.PlanDay(day, day + dtpFrom.Value.TimeOfDay);

            FillGrid();
            AppendLog(LogKind.Info,
                Texts.F(Resources.Log_Planned, _engine.Runs.Count, _engine.Timeline.Events.Count));

            _sentCount = 0;
        }
        catch (Exception ex)
        {
            AppendLog(LogKind.Error, Texts.F(Resources.Log_PlanFailed, ex.Message));
        }

        UpdateStatus();
        UpdateButtons();
    }

    private SimulationOptions ReadOptions() => new()
    {
        Seed = decimal.ToInt32(nudSeed.Value),
        TimeFactor = decimal.ToDouble(nudFactor.Value),
        OnTimeProbability = decimal.ToDouble(nudOnTime.Value) / 100.0,
        MeanDelayMinutes = decimal.ToDouble(nudMeanDelay.Value),
        TrackDiversionProbability = decimal.ToDouble(nudDiversion.Value) / 100.0,
        CancellationProbability = decimal.ToDouble(nudCancel.Value) / 100.0,
        DelayReasonProbability = decimal.ToDouble(nudReason.Value) / 100.0,
        DelayReasons = UsableReasons().Select(reason => reason.Number).ToList(),
        ExcludedTrackKeys = new HashSet<string>(_excludedTracks, StringComparer.OrdinalIgnoreCase)
    };

    private void bStart_Click(object sender, EventArgs e)
    {
        if (_engine is null)
            return;

        _engine.Start();
        tmrClock.Start();
        UpdateButtons();
    }

    private void bPause_Click(object sender, EventArgs e)
    {
        _engine?.Pause();
        UpdateButtons();
    }

    private void bStop_Click(object sender, EventArgs e)
    {
        _engine?.Stop();
        tmrClock.Stop();
        lVirtualTime.Text = @"--:--:--";
        UpdateButtons();
    }

    private void bJump_Click(object sender, EventArgs e)
    {
        if (_engine?.Timeline is null)
            return;

        _engine.JumpTo(dtpDay.Value.Date + dtpFrom.Value.TimeOfDay);
        UpdateClock();
    }

    private void nudFactor_ValueChanged(object sender, EventArgs e)
    {
        if (_engine is not null)
            _engine.Options.TimeFactor = decimal.ToDouble(nudFactor.Value);
    }

    private void tmrClock_Tick(object sender, EventArgs e)
    {
        UpdateClock();
        UpdateConnectionLabel();
    }

    private void UpdateClock()
    {
        if (_engine?.Timeline is null)
            return;

        lVirtualTime.Text = _engine.VirtualTime.ToString("dd.MM. HH:mm:ss");
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        slStation.Text = _gvd is null
            ? Resources.Main_StatusNoGvd
            : $@"{_gvd.Info.ThisStation.Name} ({_gvd.StationId})";

        slPlan.Text = _engine?.Timeline is null
            ? Resources.Main_StatusNoPlan
            : Texts.F(Resources.Main_StatusEvents, _engine.Timeline.Position, _engine.Timeline.Events.Count);

        slSent.Text = Texts.F(Resources.Main_StatusSent, _sentCount);
    }

    private void UpdateButtons()
    {
        var hasGvd = _gvd is not null;
        var hasPlan = _engine?.Timeline is not null;
        var running = _engine is { IsRunning: true };

        bPlan.Enabled = hasGvd;
        bTracks.Enabled = hasGvd;
        bStart.Enabled = hasPlan && !running;
        bPause.Enabled = running;
        bStop.Enabled = hasPlan;
        bJump.Enabled = hasPlan;
    }

    #endregion

    #region Ručná správa

    private void FillManualChoices()
    {
        Refill(cbManualType, GtnMessageType.GetValues(), Of);
        Refill(cbManualRouteKind, Enum.GetValues(typeof(GtnRouteKind)).Cast<GtnRouteKind>(), Of);
        Refill(cbManualTransferKind, Enum.GetValues(typeof(GtnTransferKind)).Cast<GtnTransferKind>(), Of);

        var reasons = new List<int> { 0 };
        reasons.AddRange(UsableReasons().Select(reason => reason.Number));
        Refill(cbManualDelayReason, reasons, ManualReasonText);
    }

    private string ManualReasonText(int number)
    {
        if (number == 0)
            return Resources.Manual_ReasonClear;

        var reason = _delayReasons.FirstOrDefault(candidate => candidate.Number == number);

        return reason is null ? number.ToString("00") : $"{reason.Number:00} - {reason.Text}";
    }

    /// <summary>
    ///     Naplní zoznam možnosťami a pokúsi sa zachovať doterajší výber.
    /// </summary>
    private static void Refill<T>(ComboBox box, IEnumerable<T> values, Func<T, string> text)
    {
        var selected = box.SelectedItem is Choice<T> choice ? choice.Value : default;

        box.Items.Clear();
        foreach (var value in values)
            box.Items.Add(new Choice<T>(value, text(value)));

        if (box.Items.Count == 0)
            return;

        Select(box, selected);
        if (box.SelectedIndex < 0)
            box.SelectedIndex = 0;
    }

    private static void Select<T>(ComboBox box, T value)
    {
        for (var i = 0; i < box.Items.Count; i++)
            if (box.Items[i] is Choice<T> choice && Equals(choice.Value, value))
            {
                box.SelectedIndex = i;
                return;
            }
    }

    private static T SelectedValue<T>(ComboBox box) => box.SelectedItem is Choice<T> choice ? choice.Value : default;

    /// <summary>
    ///     Doplní do ručnej správy hodnoty z načítaného grafikonu - stanicu, čísla vlakov a koľaje.
    /// </summary>
    private void FillManualFromGvd()
    {
        if (_gvd is null)
            return;

        var updating = _manualUpdating;
        _manualUpdating = true;

        tbManualStation.Text = StationCodec.Encode(_gvd.StationId);

        var trains = _gvd.Trains.Select(train => train.Number).Distinct()
            .OrderBy(number => number, StringComparer.OrdinalIgnoreCase).ToArray();
        cbManualTrain.Items.Clear();
        cbManualTrain.Items.AddRange(trains);
        if (cbManualTrain.Text.Length == 0 && trains.Length != 0)
            cbManualTrain.Text = trains[0];

        var tracks = UsableTracks().Select(track => track.Key).ToArray();
        foreach (var box in new[] { cbManualTrack, cbManualSecondTrack })
        {
            box.Items.Clear();
            box.Items.Add("");
            box.Items.AddRange(tracks);
        }

        if (cbManualTrack.Text.Length == 0 && tracks.Length != 0)
            cbManualTrack.Text = tracks[0];

        FillManualChoices();

        _manualUpdating = updating;
        UpdateManualPreview();
    }

    private void Manual_Changed(object sender, EventArgs e)
    {
        if (_manualUpdating)
            return;

        if (sender == cbManualType)
            UpdateManualFields();

        UpdateManualPreview();
    }

    private void cbManualNow_CheckedChanged(object sender, EventArgs e)
    {
        dtpManualTime.Enabled = !cbManualNow.Checked;

        if (cbManualNow.Checked)
            dtpManualTime.Value = DateTime.Now;

        Manual_Changed(sender, e);
    }

    /// <summary>
    ///     Zobrazí iba tie polia, ktoré vybraný typ správy nesie.
    /// </summary>
    private void UpdateManualFields()
    {
        var type = SelectedValue<GtnMessageType>(cbManualType);

        var hasTrack = type == GtnMessageType.Arrival || type == GtnMessageType.Departure ||
                       type == GtnMessageType.Route;

        fieldTrack.Visible = hasTrack;
        fieldSecondTrack.Visible = type == GtnMessageType.Departure;
        fieldRouteKind.Visible = type == GtnMessageType.Route;
        fieldDelayReason.Visible = type == GtnMessageType.DelayReason;
        fieldTransferKind.Visible = type == GtnMessageType.Transfer;
    }

    /// <summary>
    ///     Zloží správu z ovládacích prvkov ručnej správy.
    /// </summary>
    /// <returns>Správa alebo <see langword="null" />, ak nie je vybraný typ.</returns>
    private GtnMessage BuildManualMessage()
    {
        var type = SelectedValue<GtnMessageType>(cbManualType);
        if (type is null)
            return null;

        var message = new GtnMessage
        {
            Type = type,
            StationField = tbManualStation.Text.Trim(),
            TrainNumber = cbManualTrain.Text.Trim(),
            Time = cbManualNow.Checked ? DateTime.Now : dtpManualTime.Value
        };

        if (type == GtnMessageType.DelayReason)
        {
            message.DelayReason = GtnEncoder.FormatDelayReason(SelectedValue<int>(cbManualDelayReason));
            return message;
        }

        if (type == GtnMessageType.Transfer)
        {
            message.TransferKind = SelectedValue<GtnTransferKind>(cbManualTransferKind);
            return message;
        }

        message.Track = cbManualTrack.Text.Trim();

        if (type == GtnMessageType.Route)
            message.RouteKind = SelectedValue<GtnRouteKind>(cbManualRouteKind);

        if (type == GtnMessageType.Departure)
        {
            var second = cbManualSecondTrack.Text.Trim();
            message.SecondTrack = second.Length == 0 ? null : second;
        }

        return message;
    }

    private void UpdateManualPreview()
    {
        var message = BuildManualMessage();
        if (message is null)
            return;

        try
        {
            _manualGenerated = GtnEncoder.Encode(message);
        }
        catch (ArgumentException)
        {
            // Niektoré pole je dlhšie, než sa doň zmestí - text sa nechá tak, ako bol.
            return;
        }

        tbManual.Text = _manualGenerated;
    }

    private void bManualSend_Click(object sender, EventArgs e)
    {
        // Pri zapnutom "teraz" sa čas prepočíta tesne pred odoslaním, aby ho INISS nezahodil ako starý.
        // Ak si používateľ text prepísal, ponechá sa presne to, čo napísal.
        if (cbManualNow.Checked && tbManual.Text.Trim() == _manualGenerated.Trim())
            UpdateManualPreview();

        var text = tbManual.Text.Trim();
        if (text.Length == 0)
            return;

        if (GtnEncoder.TryDecode(text, out var decoded, out var error))
            AppendLog(LogKind.Info, Texts.F(Resources.Log_ManualDecoded, decoded));
        else
            AppendLog(LogKind.Warning, Texts.F(Resources.Log_ManualRejected, error));

        if (_client is null)
        {
            AppendLog(LogKind.Error, Resources.Log_NoConnection);
            return;
        }

        try
        {
            _client.SendRaw(text);
            Interlocked.Increment(ref _sentCount);
            UpdateStatus();
            AppendLog(LogKind.Sent, $"\"{text}\"");
        }
        catch (Exception ex)
        {
            AppendLog(LogKind.Error, Texts.F(Resources.Log_SendFailed, ex.Message));
        }
    }

    private sealed class Choice<T>
    {
        public Choice(T value, string text)
        {
            Value = value;
            Text = text;
        }

        public T Value { get; }

        private string Text { get; }

        public override string ToString() => Text;
    }

    #endregion

    #region Odosielanie

    private void Engine_EventDue(object sender, SimEventDueEventArgs e)
    {
        var wire = GtnEncoder.Encode(e.Message);
        string error = null;

        if (cbSend.Checked)
        {
            if (_client is null)
                error = Resources.Log_NoConnection;
            else
                try
                {
                    _client.Send(e.Message);
                    Interlocked.Increment(ref _sentCount);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
        }

        RunOnUi(() =>
        {
            if (error is null)
                AppendLog(cbSend.Checked ? LogKind.Sent : LogKind.Info, $"{e.SimEvent}  ->  \"{wire}\"");
            else
                AppendLog(LogKind.Error, Texts.F(Resources.Log_NotSent, e.SimEvent, error));

            UpdateStatus();
            HighlightRun(e.SimEvent.Run);
        });
    }

    private void Engine_Log(object sender, string text) => RunOnUi(() => AppendLog(LogKind.Info, text));

    private void HighlightRun(TrainRun run)
    {
        if (!_rowByRun.TryGetValue(run, out var index) || index >= dgvPlan.Rows.Count)
            return;

        dgvPlan.CurrentCell = dgvPlan.Rows[index].Cells[0];
    }

    private void RunOnUi(Action action)
    {
        if (!IsHandleCreated || IsDisposed)
            return;

        try
        {
            BeginInvoke(action);
        }
        catch (ObjectDisposedException)
        {
            // Okno sa medzitým zavrelo.
        }
    }

    #endregion

    #region Log

    private enum LogKind
    {
        Info,
        Sent,
        Warning,
        Error,
        Connected
    }

    private void AppendLog(LogKind kind, string text)
    {
        var color = kind switch
        {
            LogKind.Error => Color.Red,
            LogKind.Warning => Color.OrangeRed,
            LogKind.Connected => Color.Green,
            LogKind.Sent => Color.RoyalBlue,
            _ => rtbLog.ForeColor
        };

        rtbLog.SelectionStart = rtbLog.TextLength;
        rtbLog.SelectionLength = 0;
        rtbLog.SelectionColor = color;
        rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {text}\r\n");
        rtbLog.SelectionColor = rtbLog.ForeColor;
        rtbLog.ScrollToCaret();
    }

    #endregion

    private void cbOnTop_CheckedChanged(object sender, EventArgs e) => TopMost = cbOnTop.Checked;

    private void FMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        tmrClock.Stop();
        DisposeEngine();

        _client?.Dispose();
        _client = null;

        AppSettings.Seed = decimal.ToInt32(nudSeed.Value);
        AppSettings.TimeFactor = decimal.ToInt32(nudFactor.Value);
        AppSettings.OnTimePercent = decimal.ToInt32(nudOnTime.Value);
        AppSettings.MeanDelayMinutes = decimal.ToInt32(nudMeanDelay.Value);
        AppSettings.DiversionPercent = decimal.ToInt32(nudDiversion.Value);
        AppSettings.CancellationPercent = decimal.ToInt32(nudCancel.Value);
        AppSettings.DelayReasonPercent = decimal.ToInt32(nudReason.Value);
        AppSettings.Save();
    }

    private void DisposeEngine()
    {
        if (_engine is null)
            return;

        _engine.EventDue -= Engine_EventDue;
        _engine.Log -= Engine_Log;
        _engine.Dispose();
        _engine = null;
    }
}
