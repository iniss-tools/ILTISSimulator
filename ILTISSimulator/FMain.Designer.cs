namespace ILTISSimulator;

// Rozloženie je písané ručne a stojí na TableLayoutPanel/FlowLayoutPanel s AutoSize, aby sedelo pri
// každom DPI. Vizuálny návrhár Visual Studia ho pri uložení prepíše na pevné polohy - okno otvárajte
// radšej ako kód.
partial class FMain
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null)
            components.Dispose();

        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
        splitMain = new SplitContainer();
        dgvPlan = new DataGridView();
        rtbLog = new RichTextBox();
        panelTop = new FlowLayoutPanel();
        gbGvd = new GroupBox();
        tlpGvd = new TableLayoutPanel();
        tbGvdDir = new TextBox();
        flowGvdButtons = new FlowLayoutPanel();
        bBrowseGvd = new Button();
        bLoadGvd = new Button();
        lGvdInfo = new Label();
        bTracks = new Button();
        gbConnection = new GroupBox();
        tlpConnection = new TableLayoutPanel();
        lHost = new Label();
        tbHost = new TextBox();
        lPort = new Label();
        nudPort = new NumericUpDown();
        bConnect = new Button();
        lConnection = new Label();
        gbModel = new GroupBox();
        tlpModel = new TableLayoutPanel();
        lOnTime = new Label();
        nudOnTime = new NumericUpDown();
        lDiversion = new Label();
        nudDiversion = new NumericUpDown();
        lMeanDelay = new Label();
        nudMeanDelay = new NumericUpDown();
        lCancel = new Label();
        nudCancel = new NumericUpDown();
        lReason = new Label();
        nudReason = new NumericUpDown();
        lFactor = new Label();
        nudFactor = new NumericUpDown();
        lSeed = new Label();
        nudSeed = new NumericUpDown();
        panelRun = new FlowLayoutPanel();
        fieldDay = new FlowLayoutPanel();
        lDay = new Label();
        dtpDay = new ExControls.ExDateTimePicker();
        fieldFrom = new FlowLayoutPanel();
        lFrom = new Label();
        dtpFrom = new ExControls.ExDateTimePicker();
        bPlan = new Button();
        bStart = new Button();
        bPause = new Button();
        bStop = new Button();
        bJump = new Button();
        lVirtualTime = new Label();
        cbSend = new CheckBox();
        cbOnTop = new CheckBox();
        gbManual = new GroupBox();
        tlpManualSend = new TableLayoutPanel();
        lManualPreview = new Label();
        tbManual = new TextBox();
        bManualSend = new Button();
        flowManual = new FlowLayoutPanel();
        fieldType = new FlowLayoutPanel();
        lManualType = new Label();
        cbManualType = new ComboBox();
        fieldStation = new FlowLayoutPanel();
        lManualStation = new Label();
        tbManualStation = new TextBox();
        fieldTrain = new FlowLayoutPanel();
        lManualTrain = new Label();
        cbManualTrain = new ComboBox();
        fieldTime = new FlowLayoutPanel();
        lManualTime = new Label();
        dtpManualTime = new ExControls.ExDateTimePicker();
        cbManualNow = new CheckBox();
        fieldTrack = new FlowLayoutPanel();
        lManualTrack = new Label();
        cbManualTrack = new ComboBox();
        fieldSecondTrack = new FlowLayoutPanel();
        lManualSecondTrack = new Label();
        cbManualSecondTrack = new ComboBox();
        fieldRouteKind = new FlowLayoutPanel();
        lManualRouteKind = new Label();
        cbManualRouteKind = new ComboBox();
        fieldDelayReason = new FlowLayoutPanel();
        lManualDelayReason = new Label();
        cbManualDelayReason = new ComboBox();
        fieldTransferKind = new FlowLayoutPanel();
        lManualTransferKind = new Label();
        cbManualTransferKind = new ComboBox();
        statusStrip = new StatusStrip();
        slStation = new ToolStripStatusLabel();
        slPlan = new ToolStripStatusLabel();
        slSent = new ToolStripStatusLabel();
        slLanguage = new ToolStripComboBox();
        fbdGvd = new FolderBrowserDialog();
        ttMain = new ToolTip(components);
        tmrClock = new System.Windows.Forms.Timer(components);
        ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
        splitMain.Panel1.SuspendLayout();
        splitMain.Panel2.SuspendLayout();
        splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPlan).BeginInit();
        panelTop.SuspendLayout();
        gbGvd.SuspendLayout();
        tlpGvd.SuspendLayout();
        flowGvdButtons.SuspendLayout();
        gbConnection.SuspendLayout();
        tlpConnection.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudPort).BeginInit();
        gbModel.SuspendLayout();
        tlpModel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudOnTime).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudDiversion).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudMeanDelay).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudCancel).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudReason).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudFactor).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudSeed).BeginInit();
        panelRun.SuspendLayout();
        fieldDay.SuspendLayout();
        fieldFrom.SuspendLayout();
        gbManual.SuspendLayout();
        tlpManualSend.SuspendLayout();
        flowManual.SuspendLayout();
        fieldType.SuspendLayout();
        fieldStation.SuspendLayout();
        fieldTrain.SuspendLayout();
        fieldTime.SuspendLayout();
        fieldTrack.SuspendLayout();
        fieldSecondTrack.SuspendLayout();
        fieldRouteKind.SuspendLayout();
        fieldDelayReason.SuspendLayout();
        fieldTransferKind.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // splitMain
        // 
        resources.ApplyResources(splitMain, "splitMain");
        splitMain.Name = "splitMain";
        // 
        // splitMain.Panel1
        // 
        splitMain.Panel1.Controls.Add(dgvPlan);
        // 
        // splitMain.Panel2
        // 
        splitMain.Panel2.Controls.Add(rtbLog);
        // 
        // dgvPlan
        // 
        dgvPlan.AllowUserToAddRows = false;
        dgvPlan.AllowUserToDeleteRows = false;
        dgvPlan.AllowUserToResizeRows = false;
        dgvPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPlan.BackgroundColor = SystemColors.Window;
        dgvPlan.BorderStyle = BorderStyle.None;
        dgvPlan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        resources.ApplyResources(dgvPlan, "dgvPlan");
        dgvPlan.EditMode = DataGridViewEditMode.EditProgrammatically;
        dgvPlan.MultiSelect = false;
        dgvPlan.Name = "dgvPlan";
        dgvPlan.ReadOnly = true;
        dgvPlan.RowHeadersVisible = false;
        dgvPlan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        // 
        // rtbLog
        // 
        rtbLog.BackColor = SystemColors.Window;
        rtbLog.BorderStyle = BorderStyle.None;
        resources.ApplyResources(rtbLog, "rtbLog");
        rtbLog.Name = "rtbLog";
        rtbLog.ReadOnly = true;
        // 
        // panelTop
        // 
        resources.ApplyResources(panelTop, "panelTop");
        panelTop.Controls.Add(gbGvd);
        panelTop.Controls.Add(gbConnection);
        panelTop.Controls.Add(gbModel);
        panelTop.Name = "panelTop";
        // 
        // gbGvd
        // 
        resources.ApplyResources(gbGvd, "gbGvd");
        gbGvd.Controls.Add(tlpGvd);
        gbGvd.Name = "gbGvd";
        gbGvd.TabStop = false;
        // 
        // tlpGvd
        // 
        resources.ApplyResources(tlpGvd, "tlpGvd");
        tlpGvd.Controls.Add(tbGvdDir, 0, 0);
        tlpGvd.Controls.Add(flowGvdButtons, 1, 0);
        tlpGvd.Controls.Add(lGvdInfo, 0, 1);
        tlpGvd.Controls.Add(bTracks, 1, 1);
        tlpGvd.Name = "tlpGvd";
        // 
        // tbGvdDir
        // 
        resources.ApplyResources(tbGvdDir, "tbGvdDir");
        tbGvdDir.Name = "tbGvdDir";
        // 
        // flowGvdButtons
        // 
        resources.ApplyResources(flowGvdButtons, "flowGvdButtons");
        flowGvdButtons.Controls.Add(bBrowseGvd);
        flowGvdButtons.Controls.Add(bLoadGvd);
        flowGvdButtons.Name = "flowGvdButtons";
        // 
        // bBrowseGvd
        // 
        resources.ApplyResources(bBrowseGvd, "bBrowseGvd");
        bBrowseGvd.Name = "bBrowseGvd";
        ttMain.SetToolTip(bBrowseGvd, resources.GetString("bBrowseGvd.ToolTip"));
        bBrowseGvd.UseVisualStyleBackColor = true;
        bBrowseGvd.Click += bBrowseGvd_Click;
        // 
        // bLoadGvd
        // 
        resources.ApplyResources(bLoadGvd, "bLoadGvd");
        bLoadGvd.Name = "bLoadGvd";
        bLoadGvd.UseVisualStyleBackColor = true;
        bLoadGvd.Click += bLoadGvd_Click;
        // 
        // lGvdInfo
        // 
        resources.ApplyResources(lGvdInfo, "lGvdInfo");
        lGvdInfo.Name = "lGvdInfo";
        // 
        // bTracks
        // 
        resources.ApplyResources(bTracks, "bTracks");
        bTracks.Name = "bTracks";
        bTracks.UseVisualStyleBackColor = true;
        bTracks.Click += bTracks_Click;
        // 
        // gbConnection
        // 
        resources.ApplyResources(gbConnection, "gbConnection");
        gbConnection.Controls.Add(tlpConnection);
        gbConnection.Name = "gbConnection";
        gbConnection.TabStop = false;
        // 
        // tlpConnection
        // 
        resources.ApplyResources(tlpConnection, "tlpConnection");
        tlpConnection.Controls.Add(lHost, 0, 0);
        tlpConnection.Controls.Add(tbHost, 1, 0);
        tlpConnection.Controls.Add(lPort, 0, 1);
        tlpConnection.Controls.Add(nudPort, 1, 1);
        tlpConnection.Controls.Add(bConnect, 0, 2);
        tlpConnection.Controls.Add(lConnection, 1, 2);
        tlpConnection.Name = "tlpConnection";
        // 
        // lHost
        // 
        resources.ApplyResources(lHost, "lHost");
        lHost.Name = "lHost";
        // 
        // tbHost
        // 
        resources.ApplyResources(tbHost, "tbHost");
        tbHost.Name = "tbHost";
        // 
        // lPort
        // 
        resources.ApplyResources(lPort, "lPort");
        lPort.Name = "lPort";
        // 
        // nudPort
        // 
        resources.ApplyResources(nudPort, "nudPort");
        nudPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        nudPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudPort.Name = "nudPort";
        nudPort.Value = new decimal(new int[] { 4000, 0, 0, 0 });
        // 
        // bConnect
        // 
        resources.ApplyResources(bConnect, "bConnect");
        bConnect.Name = "bConnect";
        bConnect.UseVisualStyleBackColor = true;
        bConnect.Click += bConnect_Click;
        // 
        // lConnection
        // 
        resources.ApplyResources(lConnection, "lConnection");
        lConnection.Name = "lConnection";
        // 
        // gbModel
        // 
        resources.ApplyResources(gbModel, "gbModel");
        gbModel.Controls.Add(tlpModel);
        gbModel.Name = "gbModel";
        gbModel.TabStop = false;
        // 
        // tlpModel
        // 
        resources.ApplyResources(tlpModel, "tlpModel");
        tlpModel.Controls.Add(lOnTime, 0, 0);
        tlpModel.Controls.Add(nudOnTime, 1, 0);
        tlpModel.Controls.Add(lDiversion, 2, 0);
        tlpModel.Controls.Add(nudDiversion, 3, 0);
        tlpModel.Controls.Add(lMeanDelay, 0, 1);
        tlpModel.Controls.Add(nudMeanDelay, 1, 1);
        tlpModel.Controls.Add(lCancel, 2, 1);
        tlpModel.Controls.Add(nudCancel, 3, 1);
        tlpModel.Controls.Add(lReason, 0, 2);
        tlpModel.Controls.Add(nudReason, 1, 2);
        tlpModel.Controls.Add(lFactor, 2, 2);
        tlpModel.Controls.Add(nudFactor, 3, 2);
        tlpModel.Controls.Add(lSeed, 0, 3);
        tlpModel.Controls.Add(nudSeed, 1, 3);
        tlpModel.Name = "tlpModel";
        // 
        // lOnTime
        // 
        resources.ApplyResources(lOnTime, "lOnTime");
        lOnTime.Name = "lOnTime";
        // 
        // nudOnTime
        // 
        resources.ApplyResources(nudOnTime, "nudOnTime");
        nudOnTime.Name = "nudOnTime";
        nudOnTime.Value = new decimal(new int[] { 75, 0, 0, 0 });
        // 
        // lDiversion
        // 
        resources.ApplyResources(lDiversion, "lDiversion");
        lDiversion.Name = "lDiversion";
        // 
        // nudDiversion
        // 
        resources.ApplyResources(nudDiversion, "nudDiversion");
        nudDiversion.Name = "nudDiversion";
        nudDiversion.Value = new decimal(new int[] { 7, 0, 0, 0 });
        // 
        // lMeanDelay
        // 
        resources.ApplyResources(lMeanDelay, "lMeanDelay");
        lMeanDelay.Name = "lMeanDelay";
        // 
        // nudMeanDelay
        // 
        resources.ApplyResources(nudMeanDelay, "nudMeanDelay");
        nudMeanDelay.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
        nudMeanDelay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudMeanDelay.Name = "nudMeanDelay";
        nudMeanDelay.Value = new decimal(new int[] { 8, 0, 0, 0 });
        // 
        // lCancel
        // 
        resources.ApplyResources(lCancel, "lCancel");
        lCancel.Name = "lCancel";
        // 
        // nudCancel
        // 
        resources.ApplyResources(nudCancel, "nudCancel");
        nudCancel.Name = "nudCancel";
        nudCancel.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // lReason
        // 
        resources.ApplyResources(lReason, "lReason");
        lReason.Name = "lReason";
        // 
        // nudReason
        // 
        resources.ApplyResources(nudReason, "nudReason");
        nudReason.Name = "nudReason";
        nudReason.Value = new decimal(new int[] { 60, 0, 0, 0 });
        // 
        // lFactor
        // 
        resources.ApplyResources(lFactor, "lFactor");
        lFactor.Name = "lFactor";
        // 
        // nudFactor
        // 
        resources.ApplyResources(nudFactor, "nudFactor");
        nudFactor.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        nudFactor.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudFactor.Name = "nudFactor";
        nudFactor.Value = new decimal(new int[] { 60, 0, 0, 0 });
        nudFactor.ValueChanged += nudFactor_ValueChanged;
        // 
        // lSeed
        // 
        resources.ApplyResources(lSeed, "lSeed");
        lSeed.Name = "lSeed";
        // 
        // nudSeed
        // 
        resources.ApplyResources(nudSeed, "nudSeed");
        nudSeed.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
        nudSeed.Name = "nudSeed";
        nudSeed.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // panelRun
        // 
        resources.ApplyResources(panelRun, "panelRun");
        panelRun.Controls.Add(fieldDay);
        panelRun.Controls.Add(fieldFrom);
        panelRun.Controls.Add(bPlan);
        panelRun.Controls.Add(bStart);
        panelRun.Controls.Add(bPause);
        panelRun.Controls.Add(bStop);
        panelRun.Controls.Add(bJump);
        panelRun.Controls.Add(lVirtualTime);
        panelRun.Controls.Add(cbSend);
        panelRun.Controls.Add(cbOnTop);
        panelRun.Name = "panelRun";
        // 
        // fieldDay
        // 
        resources.ApplyResources(fieldDay, "fieldDay");
        fieldDay.Controls.Add(lDay);
        fieldDay.Controls.Add(dtpDay);
        fieldDay.Name = "fieldDay";
        // 
        // lDay
        // 
        resources.ApplyResources(lDay, "lDay");
        lDay.Name = "lDay";
        // 
        // dtpDay
        // 
        resources.ApplyResources(dtpDay, "dtpDay");
        dtpDay.Format = DateTimePickerFormat.Short;
        dtpDay.Name = "dtpDay";
        // 
        // fieldFrom
        // 
        resources.ApplyResources(fieldFrom, "fieldFrom");
        fieldFrom.Controls.Add(lFrom);
        fieldFrom.Controls.Add(dtpFrom);
        fieldFrom.Name = "fieldFrom";
        // 
        // lFrom
        // 
        resources.ApplyResources(lFrom, "lFrom");
        lFrom.Name = "lFrom";
        // 
        // dtpFrom
        // 
        resources.ApplyResources(dtpFrom, "dtpFrom");
        dtpFrom.Format = DateTimePickerFormat.Time;
        dtpFrom.Name = "dtpFrom";
        dtpFrom.ShowUpDown = true;
        // 
        // bPlan
        // 
        resources.ApplyResources(bPlan, "bPlan");
        bPlan.Name = "bPlan";
        bPlan.UseVisualStyleBackColor = true;
        bPlan.Click += bPlan_Click;
        // 
        // bStart
        // 
        resources.ApplyResources(bStart, "bStart");
        bStart.Name = "bStart";
        bStart.UseVisualStyleBackColor = true;
        bStart.Click += bStart_Click;
        // 
        // bPause
        // 
        resources.ApplyResources(bPause, "bPause");
        bPause.Name = "bPause";
        bPause.UseVisualStyleBackColor = true;
        bPause.Click += bPause_Click;
        // 
        // bStop
        // 
        resources.ApplyResources(bStop, "bStop");
        bStop.Name = "bStop";
        bStop.UseVisualStyleBackColor = true;
        bStop.Click += bStop_Click;
        // 
        // bJump
        // 
        resources.ApplyResources(bJump, "bJump");
        bJump.Name = "bJump";
        bJump.UseVisualStyleBackColor = true;
        bJump.Click += bJump_Click;
        // 
        // lVirtualTime
        // 
        resources.ApplyResources(lVirtualTime, "lVirtualTime");
        lVirtualTime.Name = "lVirtualTime";
        // 
        // cbSend
        // 
        resources.ApplyResources(cbSend, "cbSend");
        cbSend.Checked = true;
        cbSend.CheckState = CheckState.Checked;
        cbSend.Name = "cbSend";
        cbSend.UseVisualStyleBackColor = true;
        // 
        // cbOnTop
        // 
        resources.ApplyResources(cbOnTop, "cbOnTop");
        cbOnTop.Name = "cbOnTop";
        cbOnTop.UseVisualStyleBackColor = true;
        cbOnTop.CheckedChanged += cbOnTop_CheckedChanged;
        // 
        // gbManual
        // 
        resources.ApplyResources(gbManual, "gbManual");
        gbManual.Controls.Add(tlpManualSend);
        gbManual.Controls.Add(flowManual);
        gbManual.Name = "gbManual";
        gbManual.TabStop = false;
        // 
        // tlpManualSend
        // 
        resources.ApplyResources(tlpManualSend, "tlpManualSend");
        tlpManualSend.Controls.Add(lManualPreview, 0, 0);
        tlpManualSend.Controls.Add(tbManual, 1, 0);
        tlpManualSend.Controls.Add(bManualSend, 2, 0);
        tlpManualSend.Name = "tlpManualSend";
        // 
        // lManualPreview
        // 
        resources.ApplyResources(lManualPreview, "lManualPreview");
        lManualPreview.Name = "lManualPreview";
        // 
        // tbManual
        // 
        resources.ApplyResources(tbManual, "tbManual");
        tbManual.Name = "tbManual";
        ttMain.SetToolTip(tbManual, resources.GetString("tbManual.ToolTip"));
        // 
        // bManualSend
        // 
        resources.ApplyResources(bManualSend, "bManualSend");
        bManualSend.Name = "bManualSend";
        bManualSend.UseVisualStyleBackColor = true;
        bManualSend.Click += bManualSend_Click;
        // 
        // flowManual
        // 
        resources.ApplyResources(flowManual, "flowManual");
        flowManual.Controls.Add(fieldType);
        flowManual.Controls.Add(fieldStation);
        flowManual.Controls.Add(fieldTrain);
        flowManual.Controls.Add(fieldTime);
        flowManual.Controls.Add(fieldTrack);
        flowManual.Controls.Add(fieldSecondTrack);
        flowManual.Controls.Add(fieldRouteKind);
        flowManual.Controls.Add(fieldDelayReason);
        flowManual.Controls.Add(fieldTransferKind);
        flowManual.Name = "flowManual";
        // 
        // fieldType
        // 
        resources.ApplyResources(fieldType, "fieldType");
        fieldType.Controls.Add(lManualType);
        fieldType.Controls.Add(cbManualType);
        fieldType.Name = "fieldType";
        // 
        // lManualType
        // 
        resources.ApplyResources(lManualType, "lManualType");
        lManualType.Name = "lManualType";
        // 
        // cbManualType
        // 
        resources.ApplyResources(cbManualType, "cbManualType");
        cbManualType.DropDownStyle = ComboBoxStyle.DropDownList;
        cbManualType.Name = "cbManualType";
        cbManualType.SelectedIndexChanged += Manual_Changed;
        // 
        // fieldStation
        // 
        resources.ApplyResources(fieldStation, "fieldStation");
        fieldStation.Controls.Add(lManualStation);
        fieldStation.Controls.Add(tbManualStation);
        fieldStation.Name = "fieldStation";
        // 
        // lManualStation
        // 
        resources.ApplyResources(lManualStation, "lManualStation");
        lManualStation.Name = "lManualStation";
        // 
        // tbManualStation
        // 
        resources.ApplyResources(tbManualStation, "tbManualStation");
        tbManualStation.Name = "tbManualStation";
        tbManualStation.TextChanged += Manual_Changed;
        // 
        // fieldTrain
        // 
        resources.ApplyResources(fieldTrain, "fieldTrain");
        fieldTrain.Controls.Add(lManualTrain);
        fieldTrain.Controls.Add(cbManualTrain);
        fieldTrain.Name = "fieldTrain";
        // 
        // lManualTrain
        // 
        resources.ApplyResources(lManualTrain, "lManualTrain");
        lManualTrain.Name = "lManualTrain";
        // 
        // cbManualTrain
        // 
        resources.ApplyResources(cbManualTrain, "cbManualTrain");
        cbManualTrain.Name = "cbManualTrain";
        cbManualTrain.SelectedIndexChanged += Manual_Changed;
        cbManualTrain.TextChanged += Manual_Changed;
        // 
        // fieldTime
        // 
        resources.ApplyResources(fieldTime, "fieldTime");
        fieldTime.Controls.Add(lManualTime);
        fieldTime.Controls.Add(dtpManualTime);
        fieldTime.Controls.Add(cbManualNow);
        fieldTime.Name = "fieldTime";
        // 
        // lManualTime
        // 
        resources.ApplyResources(lManualTime, "lManualTime");
        lManualTime.Name = "lManualTime";
        // 
        // dtpManualTime
        // 
        resources.ApplyResources(dtpManualTime, "dtpManualTime");
        dtpManualTime.Format = DateTimePickerFormat.Custom;
        dtpManualTime.Name = "dtpManualTime";
        dtpManualTime.ShowUpDown = true;
        dtpManualTime.ValueChanged += Manual_Changed;
        // 
        // cbManualNow
        // 
        resources.ApplyResources(cbManualNow, "cbManualNow");
        cbManualNow.Checked = true;
        cbManualNow.CheckState = CheckState.Checked;
        cbManualNow.Name = "cbManualNow";
        cbManualNow.UseVisualStyleBackColor = true;
        cbManualNow.CheckedChanged += cbManualNow_CheckedChanged;
        // 
        // fieldTrack
        // 
        resources.ApplyResources(fieldTrack, "fieldTrack");
        fieldTrack.Controls.Add(lManualTrack);
        fieldTrack.Controls.Add(cbManualTrack);
        fieldTrack.Name = "fieldTrack";
        // 
        // lManualTrack
        // 
        resources.ApplyResources(lManualTrack, "lManualTrack");
        lManualTrack.Name = "lManualTrack";
        // 
        // cbManualTrack
        // 
        resources.ApplyResources(cbManualTrack, "cbManualTrack");
        cbManualTrack.Name = "cbManualTrack";
        cbManualTrack.SelectedIndexChanged += Manual_Changed;
        cbManualTrack.TextChanged += Manual_Changed;
        // 
        // fieldSecondTrack
        // 
        resources.ApplyResources(fieldSecondTrack, "fieldSecondTrack");
        fieldSecondTrack.Controls.Add(lManualSecondTrack);
        fieldSecondTrack.Controls.Add(cbManualSecondTrack);
        fieldSecondTrack.Name = "fieldSecondTrack";
        // 
        // lManualSecondTrack
        // 
        resources.ApplyResources(lManualSecondTrack, "lManualSecondTrack");
        lManualSecondTrack.Name = "lManualSecondTrack";
        // 
        // cbManualSecondTrack
        // 
        resources.ApplyResources(cbManualSecondTrack, "cbManualSecondTrack");
        cbManualSecondTrack.Name = "cbManualSecondTrack";
        cbManualSecondTrack.SelectedIndexChanged += Manual_Changed;
        cbManualSecondTrack.TextChanged += Manual_Changed;
        // 
        // fieldRouteKind
        // 
        resources.ApplyResources(fieldRouteKind, "fieldRouteKind");
        fieldRouteKind.Controls.Add(lManualRouteKind);
        fieldRouteKind.Controls.Add(cbManualRouteKind);
        fieldRouteKind.Name = "fieldRouteKind";
        // 
        // lManualRouteKind
        // 
        resources.ApplyResources(lManualRouteKind, "lManualRouteKind");
        lManualRouteKind.Name = "lManualRouteKind";
        // 
        // cbManualRouteKind
        // 
        resources.ApplyResources(cbManualRouteKind, "cbManualRouteKind");
        cbManualRouteKind.DropDownStyle = ComboBoxStyle.DropDownList;
        cbManualRouteKind.Name = "cbManualRouteKind";
        cbManualRouteKind.SelectedIndexChanged += Manual_Changed;
        // 
        // fieldDelayReason
        // 
        resources.ApplyResources(fieldDelayReason, "fieldDelayReason");
        fieldDelayReason.Controls.Add(lManualDelayReason);
        fieldDelayReason.Controls.Add(cbManualDelayReason);
        fieldDelayReason.Name = "fieldDelayReason";
        // 
        // lManualDelayReason
        // 
        resources.ApplyResources(lManualDelayReason, "lManualDelayReason");
        lManualDelayReason.Name = "lManualDelayReason";
        // 
        // cbManualDelayReason
        // 
        resources.ApplyResources(cbManualDelayReason, "cbManualDelayReason");
        cbManualDelayReason.DropDownStyle = ComboBoxStyle.DropDownList;
        cbManualDelayReason.Name = "cbManualDelayReason";
        cbManualDelayReason.SelectedIndexChanged += Manual_Changed;
        // 
        // fieldTransferKind
        // 
        resources.ApplyResources(fieldTransferKind, "fieldTransferKind");
        fieldTransferKind.Controls.Add(lManualTransferKind);
        fieldTransferKind.Controls.Add(cbManualTransferKind);
        fieldTransferKind.Name = "fieldTransferKind";
        // 
        // lManualTransferKind
        // 
        resources.ApplyResources(lManualTransferKind, "lManualTransferKind");
        lManualTransferKind.Name = "lManualTransferKind";
        // 
        // cbManualTransferKind
        // 
        resources.ApplyResources(cbManualTransferKind, "cbManualTransferKind");
        cbManualTransferKind.DropDownStyle = ComboBoxStyle.DropDownList;
        cbManualTransferKind.Name = "cbManualTransferKind";
        cbManualTransferKind.SelectedIndexChanged += Manual_Changed;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { slStation, slPlan, slSent, slLanguage });
        resources.ApplyResources(statusStrip, "statusStrip");
        statusStrip.Name = "statusStrip";
        // 
        // slStation
        // 
        slStation.Name = "slStation";
        resources.ApplyResources(slStation, "slStation");
        // 
        // slPlan
        // 
        slPlan.BorderSides = ToolStripStatusLabelBorderSides.Left;
        slPlan.Name = "slPlan";
        resources.ApplyResources(slPlan, "slPlan");
        // 
        // slSent
        // 
        slSent.BorderSides = ToolStripStatusLabelBorderSides.Left;
        slSent.Name = "slSent";
        resources.ApplyResources(slSent, "slSent");
        slSent.Spring = true;
        // 
        // slLanguage
        // 
        slLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
        slLanguage.Name = "slLanguage";
        resources.ApplyResources(slLanguage, "slLanguage");
        slLanguage.SelectedIndexChanged += slLanguage_SelectedIndexChanged;
        // 
        // fbdGvd
        // 
        fbdGvd.InitialDirectory = "C:\\WINDOWS\\system32";
        fbdGvd.ShowNewFolderButton = false;
        // 
        // tmrClock
        // 
        tmrClock.Interval = 250;
        tmrClock.Tick += tmrClock_Tick;
        // 
        // FMain
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(splitMain);
        Controls.Add(panelRun);
        Controls.Add(panelTop);
        Controls.Add(gbManual);
        Controls.Add(statusStrip);
        Name = "FMain";
        FormClosing += FMain_FormClosing;
        splitMain.Panel1.ResumeLayout(false);
        splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
        splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvPlan).EndInit();
        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        gbGvd.ResumeLayout(false);
        gbGvd.PerformLayout();
        tlpGvd.ResumeLayout(false);
        tlpGvd.PerformLayout();
        flowGvdButtons.ResumeLayout(false);
        flowGvdButtons.PerformLayout();
        gbConnection.ResumeLayout(false);
        gbConnection.PerformLayout();
        tlpConnection.ResumeLayout(false);
        tlpConnection.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudPort).EndInit();
        gbModel.ResumeLayout(false);
        gbModel.PerformLayout();
        tlpModel.ResumeLayout(false);
        tlpModel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudOnTime).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudDiversion).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudMeanDelay).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudCancel).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudReason).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudFactor).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudSeed).EndInit();
        panelRun.ResumeLayout(false);
        panelRun.PerformLayout();
        fieldDay.ResumeLayout(false);
        fieldDay.PerformLayout();
        fieldFrom.ResumeLayout(false);
        fieldFrom.PerformLayout();
        gbManual.ResumeLayout(false);
        gbManual.PerformLayout();
        tlpManualSend.ResumeLayout(false);
        tlpManualSend.PerformLayout();
        flowManual.ResumeLayout(false);
        flowManual.PerformLayout();
        fieldType.ResumeLayout(false);
        fieldType.PerformLayout();
        fieldStation.ResumeLayout(false);
        fieldStation.PerformLayout();
        fieldTrain.ResumeLayout(false);
        fieldTrain.PerformLayout();
        fieldTime.ResumeLayout(false);
        fieldTime.PerformLayout();
        fieldTrack.ResumeLayout(false);
        fieldTrack.PerformLayout();
        fieldSecondTrack.ResumeLayout(false);
        fieldSecondTrack.PerformLayout();
        fieldRouteKind.ResumeLayout(false);
        fieldRouteKind.PerformLayout();
        fieldDelayReason.ResumeLayout(false);
        fieldDelayReason.PerformLayout();
        fieldTransferKind.ResumeLayout(false);
        fieldTransferKind.PerformLayout();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.FlowLayoutPanel panelTop;
    private System.Windows.Forms.GroupBox gbGvd;
    private System.Windows.Forms.TableLayoutPanel tlpGvd;
    private System.Windows.Forms.TextBox tbGvdDir;
    private System.Windows.Forms.FlowLayoutPanel flowGvdButtons;
    private System.Windows.Forms.Button bBrowseGvd;
    private System.Windows.Forms.Button bLoadGvd;
    private System.Windows.Forms.Label lGvdInfo;
    private System.Windows.Forms.Button bTracks;
    private System.Windows.Forms.GroupBox gbConnection;
    private System.Windows.Forms.TableLayoutPanel tlpConnection;
    private System.Windows.Forms.Label lHost;
    private System.Windows.Forms.TextBox tbHost;
    private System.Windows.Forms.Label lPort;
    private System.Windows.Forms.NumericUpDown nudPort;
    private System.Windows.Forms.Button bConnect;
    private System.Windows.Forms.Label lConnection;
    private System.Windows.Forms.GroupBox gbModel;
    private System.Windows.Forms.TableLayoutPanel tlpModel;
    private System.Windows.Forms.Label lOnTime;
    private System.Windows.Forms.NumericUpDown nudOnTime;
    private System.Windows.Forms.Label lDiversion;
    private System.Windows.Forms.NumericUpDown nudDiversion;
    private System.Windows.Forms.Label lMeanDelay;
    private System.Windows.Forms.NumericUpDown nudMeanDelay;
    private System.Windows.Forms.Label lCancel;
    private System.Windows.Forms.NumericUpDown nudCancel;
    private System.Windows.Forms.Label lReason;
    private System.Windows.Forms.NumericUpDown nudReason;
    private System.Windows.Forms.Label lFactor;
    private System.Windows.Forms.NumericUpDown nudFactor;
    private System.Windows.Forms.Label lSeed;
    private System.Windows.Forms.NumericUpDown nudSeed;
    private System.Windows.Forms.FlowLayoutPanel panelRun;
    private System.Windows.Forms.FlowLayoutPanel fieldDay;
    private System.Windows.Forms.Label lDay;
    private ExControls.ExDateTimePicker dtpDay;
    private System.Windows.Forms.FlowLayoutPanel fieldFrom;
    private System.Windows.Forms.Label lFrom;
    private ExControls.ExDateTimePicker dtpFrom;
    private System.Windows.Forms.Button bPlan;
    private System.Windows.Forms.Button bStart;
    private System.Windows.Forms.Button bPause;
    private System.Windows.Forms.Button bStop;
    private System.Windows.Forms.Button bJump;
    private System.Windows.Forms.Label lVirtualTime;
    private System.Windows.Forms.CheckBox cbSend;
    private System.Windows.Forms.CheckBox cbOnTop;
    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.DataGridView dgvPlan;
    private System.Windows.Forms.RichTextBox rtbLog;
    private System.Windows.Forms.GroupBox gbManual;
    private System.Windows.Forms.TableLayoutPanel tlpManualSend;
    private System.Windows.Forms.Label lManualPreview;
    private System.Windows.Forms.TextBox tbManual;
    private System.Windows.Forms.Button bManualSend;
    private System.Windows.Forms.FlowLayoutPanel flowManual;
    private System.Windows.Forms.FlowLayoutPanel fieldType;
    private System.Windows.Forms.Label lManualType;
    private System.Windows.Forms.ComboBox cbManualType;
    private System.Windows.Forms.FlowLayoutPanel fieldStation;
    private System.Windows.Forms.Label lManualStation;
    private System.Windows.Forms.TextBox tbManualStation;
    private System.Windows.Forms.FlowLayoutPanel fieldTrain;
    private System.Windows.Forms.Label lManualTrain;
    private System.Windows.Forms.ComboBox cbManualTrain;
    private System.Windows.Forms.FlowLayoutPanel fieldTime;
    private System.Windows.Forms.Label lManualTime;
    private ExControls.ExDateTimePicker dtpManualTime;
    private System.Windows.Forms.CheckBox cbManualNow;
    private System.Windows.Forms.FlowLayoutPanel fieldTrack;
    private System.Windows.Forms.Label lManualTrack;
    private System.Windows.Forms.ComboBox cbManualTrack;
    private System.Windows.Forms.FlowLayoutPanel fieldSecondTrack;
    private System.Windows.Forms.Label lManualSecondTrack;
    private System.Windows.Forms.ComboBox cbManualSecondTrack;
    private System.Windows.Forms.FlowLayoutPanel fieldRouteKind;
    private System.Windows.Forms.Label lManualRouteKind;
    private System.Windows.Forms.ComboBox cbManualRouteKind;
    private System.Windows.Forms.FlowLayoutPanel fieldDelayReason;
    private System.Windows.Forms.Label lManualDelayReason;
    private System.Windows.Forms.ComboBox cbManualDelayReason;
    private System.Windows.Forms.FlowLayoutPanel fieldTransferKind;
    private System.Windows.Forms.Label lManualTransferKind;
    private System.Windows.Forms.ComboBox cbManualTransferKind;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel slStation;
    private System.Windows.Forms.ToolStripStatusLabel slPlan;
    private System.Windows.Forms.ToolStripStatusLabel slSent;
    private System.Windows.Forms.ToolStripComboBox slLanguage;
    private System.Windows.Forms.FolderBrowserDialog fbdGvd;
    private System.Windows.Forms.ToolTip ttMain;
    private System.Windows.Forms.Timer tmrClock;
}
