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
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
        this.splitMain = new System.Windows.Forms.SplitContainer();
        this.dgvPlan = new System.Windows.Forms.DataGridView();
        this.rtbLog = new System.Windows.Forms.RichTextBox();
        this.panelTop = new System.Windows.Forms.FlowLayoutPanel();
        this.gbGvd = new System.Windows.Forms.GroupBox();
        this.tlpGvd = new System.Windows.Forms.TableLayoutPanel();
        this.tbGvdDir = new System.Windows.Forms.TextBox();
        this.flowGvdButtons = new System.Windows.Forms.FlowLayoutPanel();
        this.bBrowseGvd = new System.Windows.Forms.Button();
        this.bLoadGvd = new System.Windows.Forms.Button();
        this.lGvdInfo = new System.Windows.Forms.Label();
        this.bTracks = new System.Windows.Forms.Button();
        this.gbConnection = new System.Windows.Forms.GroupBox();
        this.tlpConnection = new System.Windows.Forms.TableLayoutPanel();
        this.lHost = new System.Windows.Forms.Label();
        this.tbHost = new System.Windows.Forms.TextBox();
        this.lPort = new System.Windows.Forms.Label();
        this.nudPort = new System.Windows.Forms.NumericUpDown();
        this.bConnect = new System.Windows.Forms.Button();
        this.lConnection = new System.Windows.Forms.Label();
        this.gbModel = new System.Windows.Forms.GroupBox();
        this.tlpModel = new System.Windows.Forms.TableLayoutPanel();
        this.lOnTime = new System.Windows.Forms.Label();
        this.nudOnTime = new System.Windows.Forms.NumericUpDown();
        this.lDiversion = new System.Windows.Forms.Label();
        this.nudDiversion = new System.Windows.Forms.NumericUpDown();
        this.lMeanDelay = new System.Windows.Forms.Label();
        this.nudMeanDelay = new System.Windows.Forms.NumericUpDown();
        this.lCancel = new System.Windows.Forms.Label();
        this.nudCancel = new System.Windows.Forms.NumericUpDown();
        this.lReason = new System.Windows.Forms.Label();
        this.nudReason = new System.Windows.Forms.NumericUpDown();
        this.lFactor = new System.Windows.Forms.Label();
        this.nudFactor = new System.Windows.Forms.NumericUpDown();
        this.lSeed = new System.Windows.Forms.Label();
        this.nudSeed = new System.Windows.Forms.NumericUpDown();
        this.panelRun = new System.Windows.Forms.FlowLayoutPanel();
        this.fieldDay = new System.Windows.Forms.FlowLayoutPanel();
        this.lDay = new System.Windows.Forms.Label();
        this.dtpDay = new System.Windows.Forms.DateTimePicker();
        this.fieldFrom = new System.Windows.Forms.FlowLayoutPanel();
        this.lFrom = new System.Windows.Forms.Label();
        this.dtpFrom = new System.Windows.Forms.DateTimePicker();
        this.bPlan = new System.Windows.Forms.Button();
        this.bStart = new System.Windows.Forms.Button();
        this.bPause = new System.Windows.Forms.Button();
        this.bStop = new System.Windows.Forms.Button();
        this.bJump = new System.Windows.Forms.Button();
        this.lVirtualTime = new System.Windows.Forms.Label();
        this.cbSend = new System.Windows.Forms.CheckBox();
        this.cbOnTop = new System.Windows.Forms.CheckBox();
        this.gbManual = new System.Windows.Forms.GroupBox();
        this.tlpManualSend = new System.Windows.Forms.TableLayoutPanel();
        this.lManualPreview = new System.Windows.Forms.Label();
        this.tbManual = new System.Windows.Forms.TextBox();
        this.bManualSend = new System.Windows.Forms.Button();
        this.flowManual = new System.Windows.Forms.FlowLayoutPanel();
        this.fieldType = new System.Windows.Forms.FlowLayoutPanel();
        this.lManualType = new System.Windows.Forms.Label();
        this.cbManualType = new System.Windows.Forms.ComboBox();
        this.fieldStation = new System.Windows.Forms.FlowLayoutPanel();
        this.lManualStation = new System.Windows.Forms.Label();
        this.tbManualStation = new System.Windows.Forms.TextBox();
        this.fieldTrain = new System.Windows.Forms.FlowLayoutPanel();
        this.lManualTrain = new System.Windows.Forms.Label();
        this.cbManualTrain = new System.Windows.Forms.ComboBox();
        this.fieldTime = new System.Windows.Forms.FlowLayoutPanel();
        this.lManualTime = new System.Windows.Forms.Label();
        this.dtpManualTime = new System.Windows.Forms.DateTimePicker();
        this.cbManualNow = new System.Windows.Forms.CheckBox();
        this.fieldTrack = new System.Windows.Forms.FlowLayoutPanel();
        this.lManualTrack = new System.Windows.Forms.Label();
        this.cbManualTrack = new System.Windows.Forms.ComboBox();
        this.fieldSecondTrack = new System.Windows.Forms.FlowLayoutPanel();
        this.lManualSecondTrack = new System.Windows.Forms.Label();
        this.cbManualSecondTrack = new System.Windows.Forms.ComboBox();
        this.fieldRouteKind = new System.Windows.Forms.FlowLayoutPanel();
        this.lManualRouteKind = new System.Windows.Forms.Label();
        this.cbManualRouteKind = new System.Windows.Forms.ComboBox();
        this.fieldDelayReason = new System.Windows.Forms.FlowLayoutPanel();
        this.lManualDelayReason = new System.Windows.Forms.Label();
        this.cbManualDelayReason = new System.Windows.Forms.ComboBox();
        this.fieldTransferKind = new System.Windows.Forms.FlowLayoutPanel();
        this.lManualTransferKind = new System.Windows.Forms.Label();
        this.cbManualTransferKind = new System.Windows.Forms.ComboBox();
        this.statusStrip = new System.Windows.Forms.StatusStrip();
        this.slStation = new System.Windows.Forms.ToolStripStatusLabel();
        this.slPlan = new System.Windows.Forms.ToolStripStatusLabel();
        this.slSent = new System.Windows.Forms.ToolStripStatusLabel();
        this.slLanguage = new System.Windows.Forms.ToolStripComboBox();
        this.fbdGvd = new System.Windows.Forms.FolderBrowserDialog();
        this.ttMain = new System.Windows.Forms.ToolTip(this.components);
        this.tmrClock = new System.Windows.Forms.Timer(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
        this.splitMain.Panel1.SuspendLayout();
        this.splitMain.Panel2.SuspendLayout();
        this.splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvPlan)).BeginInit();
        this.panelTop.SuspendLayout();
        this.gbGvd.SuspendLayout();
        this.tlpGvd.SuspendLayout();
        this.flowGvdButtons.SuspendLayout();
        this.gbConnection.SuspendLayout();
        this.tlpConnection.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
        this.gbModel.SuspendLayout();
        this.tlpModel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.nudOnTime)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudDiversion)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudMeanDelay)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudCancel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudReason)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudFactor)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudSeed)).BeginInit();
        this.panelRun.SuspendLayout();
        this.fieldDay.SuspendLayout();
        this.fieldFrom.SuspendLayout();
        this.gbManual.SuspendLayout();
        this.tlpManualSend.SuspendLayout();
        this.flowManual.SuspendLayout();
        this.fieldType.SuspendLayout();
        this.fieldStation.SuspendLayout();
        this.fieldTrain.SuspendLayout();
        this.fieldTime.SuspendLayout();
        this.fieldTrack.SuspendLayout();
        this.fieldSecondTrack.SuspendLayout();
        this.fieldRouteKind.SuspendLayout();
        this.fieldDelayReason.SuspendLayout();
        this.fieldTransferKind.SuspendLayout();
        this.statusStrip.SuspendLayout();
        this.SuspendLayout();
        // 
        // splitMain
        // 
        resources.ApplyResources(this.splitMain, "splitMain");
        this.splitMain.Name = "splitMain";
        // 
        // splitMain.Panel1
        // 
        this.splitMain.Panel1.Controls.Add(this.dgvPlan);
        // 
        // splitMain.Panel2
        // 
        this.splitMain.Panel2.Controls.Add(this.rtbLog);
        // 
        // dgvPlan
        // 
        this.dgvPlan.AllowUserToAddRows = false;
        this.dgvPlan.AllowUserToDeleteRows = false;
        this.dgvPlan.AllowUserToResizeRows = false;
        this.dgvPlan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvPlan.BackgroundColor = System.Drawing.SystemColors.Window;
        this.dgvPlan.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.dgvPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        resources.ApplyResources(this.dgvPlan, "dgvPlan");
        this.dgvPlan.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
        this.dgvPlan.MultiSelect = false;
        this.dgvPlan.Name = "dgvPlan";
        this.dgvPlan.ReadOnly = true;
        this.dgvPlan.RowHeadersVisible = false;
        this.dgvPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        // 
        // rtbLog
        // 
        this.rtbLog.BackColor = System.Drawing.SystemColors.Window;
        this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
        resources.ApplyResources(this.rtbLog, "rtbLog");
        this.rtbLog.Name = "rtbLog";
        this.rtbLog.ReadOnly = true;
        // 
        // panelTop
        // 
        resources.ApplyResources(this.panelTop, "panelTop");
        this.panelTop.Controls.Add(this.gbGvd);
        this.panelTop.Controls.Add(this.gbConnection);
        this.panelTop.Controls.Add(this.gbModel);
        this.panelTop.Name = "panelTop";
        // 
        // gbGvd
        // 
        resources.ApplyResources(this.gbGvd, "gbGvd");
        this.gbGvd.Controls.Add(this.tlpGvd);
        this.gbGvd.Name = "gbGvd";
        this.gbGvd.TabStop = false;
        // 
        // tlpGvd
        // 
        resources.ApplyResources(this.tlpGvd, "tlpGvd");
        this.tlpGvd.Controls.Add(this.tbGvdDir, 0, 0);
        this.tlpGvd.Controls.Add(this.flowGvdButtons, 1, 0);
        this.tlpGvd.Controls.Add(this.lGvdInfo, 0, 1);
        this.tlpGvd.Controls.Add(this.bTracks, 1, 1);
        this.tlpGvd.Name = "tlpGvd";
        // 
        // tbGvdDir
        // 
        resources.ApplyResources(this.tbGvdDir, "tbGvdDir");
        this.tbGvdDir.Name = "tbGvdDir";
        // 
        // flowGvdButtons
        // 
        resources.ApplyResources(this.flowGvdButtons, "flowGvdButtons");
        this.flowGvdButtons.Controls.Add(this.bBrowseGvd);
        this.flowGvdButtons.Controls.Add(this.bLoadGvd);
        this.flowGvdButtons.Name = "flowGvdButtons";
        // 
        // bBrowseGvd
        // 
        resources.ApplyResources(this.bBrowseGvd, "bBrowseGvd");
        this.bBrowseGvd.Name = "bBrowseGvd";
        this.ttMain.SetToolTip(this.bBrowseGvd, resources.GetString("bBrowseGvd.ToolTip"));
        this.bBrowseGvd.UseVisualStyleBackColor = true;
        this.bBrowseGvd.Click += new System.EventHandler(this.bBrowseGvd_Click);
        // 
        // bLoadGvd
        // 
        resources.ApplyResources(this.bLoadGvd, "bLoadGvd");
        this.bLoadGvd.Name = "bLoadGvd";
        this.bLoadGvd.UseVisualStyleBackColor = true;
        this.bLoadGvd.Click += new System.EventHandler(this.bLoadGvd_Click);
        // 
        // lGvdInfo
        // 
        resources.ApplyResources(this.lGvdInfo, "lGvdInfo");
        this.lGvdInfo.Name = "lGvdInfo";
        // 
        // bTracks
        // 
        resources.ApplyResources(this.bTracks, "bTracks");
        this.bTracks.Name = "bTracks";
        this.bTracks.UseVisualStyleBackColor = true;
        this.bTracks.Click += new System.EventHandler(this.bTracks_Click);
        // 
        // gbConnection
        // 
        resources.ApplyResources(this.gbConnection, "gbConnection");
        this.gbConnection.Controls.Add(this.tlpConnection);
        this.gbConnection.Name = "gbConnection";
        this.gbConnection.TabStop = false;
        // 
        // tlpConnection
        // 
        resources.ApplyResources(this.tlpConnection, "tlpConnection");
        this.tlpConnection.Controls.Add(this.lHost, 0, 0);
        this.tlpConnection.Controls.Add(this.tbHost, 1, 0);
        this.tlpConnection.Controls.Add(this.lPort, 0, 1);
        this.tlpConnection.Controls.Add(this.nudPort, 1, 1);
        this.tlpConnection.Controls.Add(this.bConnect, 0, 2);
        this.tlpConnection.Controls.Add(this.lConnection, 1, 2);
        this.tlpConnection.Name = "tlpConnection";
        // 
        // lHost
        // 
        resources.ApplyResources(this.lHost, "lHost");
        this.lHost.Name = "lHost";
        // 
        // tbHost
        // 
        resources.ApplyResources(this.tbHost, "tbHost");
        this.tbHost.Name = "tbHost";
        // 
        // lPort
        // 
        resources.ApplyResources(this.lPort, "lPort");
        this.lPort.Name = "lPort";
        // 
        // nudPort
        // 
        resources.ApplyResources(this.nudPort, "nudPort");
        this.nudPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        this.nudPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.nudPort.Name = "nudPort";
        this.nudPort.Value = new decimal(new int[] { 4000, 0, 0, 0 });
        // 
        // bConnect
        // 
        resources.ApplyResources(this.bConnect, "bConnect");
        this.bConnect.Name = "bConnect";
        this.bConnect.UseVisualStyleBackColor = true;
        this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
        // 
        // lConnection
        // 
        resources.ApplyResources(this.lConnection, "lConnection");
        this.lConnection.Name = "lConnection";
        // 
        // gbModel
        // 
        resources.ApplyResources(this.gbModel, "gbModel");
        this.gbModel.Controls.Add(this.tlpModel);
        this.gbModel.Name = "gbModel";
        this.gbModel.TabStop = false;
        // 
        // tlpModel
        // 
        resources.ApplyResources(this.tlpModel, "tlpModel");
        this.tlpModel.Controls.Add(this.lOnTime, 0, 0);
        this.tlpModel.Controls.Add(this.nudOnTime, 1, 0);
        this.tlpModel.Controls.Add(this.lDiversion, 2, 0);
        this.tlpModel.Controls.Add(this.nudDiversion, 3, 0);
        this.tlpModel.Controls.Add(this.lMeanDelay, 0, 1);
        this.tlpModel.Controls.Add(this.nudMeanDelay, 1, 1);
        this.tlpModel.Controls.Add(this.lCancel, 2, 1);
        this.tlpModel.Controls.Add(this.nudCancel, 3, 1);
        this.tlpModel.Controls.Add(this.lReason, 0, 2);
        this.tlpModel.Controls.Add(this.nudReason, 1, 2);
        this.tlpModel.Controls.Add(this.lFactor, 2, 2);
        this.tlpModel.Controls.Add(this.nudFactor, 3, 2);
        this.tlpModel.Controls.Add(this.lSeed, 0, 3);
        this.tlpModel.Controls.Add(this.nudSeed, 1, 3);
        this.tlpModel.Name = "tlpModel";
        // 
        // lOnTime
        // 
        resources.ApplyResources(this.lOnTime, "lOnTime");
        this.lOnTime.Name = "lOnTime";
        // 
        // nudOnTime
        // 
        resources.ApplyResources(this.nudOnTime, "nudOnTime");
        this.nudOnTime.Name = "nudOnTime";
        this.nudOnTime.Value = new decimal(new int[] { 75, 0, 0, 0 });
        // 
        // lDiversion
        // 
        resources.ApplyResources(this.lDiversion, "lDiversion");
        this.lDiversion.Name = "lDiversion";
        // 
        // nudDiversion
        // 
        resources.ApplyResources(this.nudDiversion, "nudDiversion");
        this.nudDiversion.Name = "nudDiversion";
        this.nudDiversion.Value = new decimal(new int[] { 7, 0, 0, 0 });
        // 
        // lMeanDelay
        // 
        resources.ApplyResources(this.lMeanDelay, "lMeanDelay");
        this.lMeanDelay.Name = "lMeanDelay";
        // 
        // nudMeanDelay
        // 
        resources.ApplyResources(this.nudMeanDelay, "nudMeanDelay");
        this.nudMeanDelay.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
        this.nudMeanDelay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.nudMeanDelay.Name = "nudMeanDelay";
        this.nudMeanDelay.Value = new decimal(new int[] { 8, 0, 0, 0 });
        // 
        // lCancel
        // 
        resources.ApplyResources(this.lCancel, "lCancel");
        this.lCancel.Name = "lCancel";
        // 
        // nudCancel
        // 
        resources.ApplyResources(this.nudCancel, "nudCancel");
        this.nudCancel.Name = "nudCancel";
        this.nudCancel.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // lReason
        // 
        resources.ApplyResources(this.lReason, "lReason");
        this.lReason.Name = "lReason";
        // 
        // nudReason
        // 
        resources.ApplyResources(this.nudReason, "nudReason");
        this.nudReason.Name = "nudReason";
        this.nudReason.Value = new decimal(new int[] { 60, 0, 0, 0 });
        // 
        // lFactor
        // 
        resources.ApplyResources(this.lFactor, "lFactor");
        this.lFactor.Name = "lFactor";
        // 
        // nudFactor
        // 
        resources.ApplyResources(this.nudFactor, "nudFactor");
        this.nudFactor.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        this.nudFactor.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.nudFactor.Name = "nudFactor";
        this.nudFactor.Value = new decimal(new int[] { 60, 0, 0, 0 });
        this.nudFactor.ValueChanged += new System.EventHandler(this.nudFactor_ValueChanged);
        // 
        // lSeed
        // 
        resources.ApplyResources(this.lSeed, "lSeed");
        this.lSeed.Name = "lSeed";
        // 
        // nudSeed
        // 
        resources.ApplyResources(this.nudSeed, "nudSeed");
        this.nudSeed.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
        this.nudSeed.Name = "nudSeed";
        this.nudSeed.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // panelRun
        // 
        resources.ApplyResources(this.panelRun, "panelRun");
        this.panelRun.Controls.Add(this.fieldDay);
        this.panelRun.Controls.Add(this.fieldFrom);
        this.panelRun.Controls.Add(this.bPlan);
        this.panelRun.Controls.Add(this.bStart);
        this.panelRun.Controls.Add(this.bPause);
        this.panelRun.Controls.Add(this.bStop);
        this.panelRun.Controls.Add(this.bJump);
        this.panelRun.Controls.Add(this.lVirtualTime);
        this.panelRun.Controls.Add(this.cbSend);
        this.panelRun.Controls.Add(this.cbOnTop);
        this.panelRun.Name = "panelRun";
        // 
        // fieldDay
        // 
        resources.ApplyResources(this.fieldDay, "fieldDay");
        this.fieldDay.Controls.Add(this.lDay);
        this.fieldDay.Controls.Add(this.dtpDay);
        this.fieldDay.Name = "fieldDay";
        // 
        // lDay
        // 
        resources.ApplyResources(this.lDay, "lDay");
        this.lDay.Name = "lDay";
        // 
        // dtpDay
        // 
        resources.ApplyResources(this.dtpDay, "dtpDay");
        this.dtpDay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.dtpDay.Name = "dtpDay";
        // 
        // fieldFrom
        // 
        resources.ApplyResources(this.fieldFrom, "fieldFrom");
        this.fieldFrom.Controls.Add(this.lFrom);
        this.fieldFrom.Controls.Add(this.dtpFrom);
        this.fieldFrom.Name = "fieldFrom";
        // 
        // lFrom
        // 
        resources.ApplyResources(this.lFrom, "lFrom");
        this.lFrom.Name = "lFrom";
        // 
        // dtpFrom
        // 
        resources.ApplyResources(this.dtpFrom, "dtpFrom");
        this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
        this.dtpFrom.Name = "dtpFrom";
        this.dtpFrom.ShowUpDown = true;
        // 
        // bPlan
        // 
        resources.ApplyResources(this.bPlan, "bPlan");
        this.bPlan.Name = "bPlan";
        this.bPlan.UseVisualStyleBackColor = true;
        this.bPlan.Click += new System.EventHandler(this.bPlan_Click);
        // 
        // bStart
        // 
        resources.ApplyResources(this.bStart, "bStart");
        this.bStart.Name = "bStart";
        this.bStart.UseVisualStyleBackColor = true;
        this.bStart.Click += new System.EventHandler(this.bStart_Click);
        // 
        // bPause
        // 
        resources.ApplyResources(this.bPause, "bPause");
        this.bPause.Name = "bPause";
        this.bPause.UseVisualStyleBackColor = true;
        this.bPause.Click += new System.EventHandler(this.bPause_Click);
        // 
        // bStop
        // 
        resources.ApplyResources(this.bStop, "bStop");
        this.bStop.Name = "bStop";
        this.bStop.UseVisualStyleBackColor = true;
        this.bStop.Click += new System.EventHandler(this.bStop_Click);
        // 
        // bJump
        // 
        resources.ApplyResources(this.bJump, "bJump");
        this.bJump.Name = "bJump";
        this.bJump.UseVisualStyleBackColor = true;
        this.bJump.Click += new System.EventHandler(this.bJump_Click);
        // 
        // lVirtualTime
        // 
        resources.ApplyResources(this.lVirtualTime, "lVirtualTime");
        this.lVirtualTime.Name = "lVirtualTime";
        // 
        // cbSend
        // 
        resources.ApplyResources(this.cbSend, "cbSend");
        this.cbSend.Checked = true;
        this.cbSend.CheckState = System.Windows.Forms.CheckState.Checked;
        this.cbSend.Name = "cbSend";
        this.cbSend.UseVisualStyleBackColor = true;
        // 
        // cbOnTop
        // 
        resources.ApplyResources(this.cbOnTop, "cbOnTop");
        this.cbOnTop.Name = "cbOnTop";
        this.cbOnTop.UseVisualStyleBackColor = true;
        this.cbOnTop.CheckedChanged += new System.EventHandler(this.cbOnTop_CheckedChanged);
        // 
        // gbManual
        // 
        resources.ApplyResources(this.gbManual, "gbManual");
        this.gbManual.Controls.Add(this.tlpManualSend);
        this.gbManual.Controls.Add(this.flowManual);
        this.gbManual.Name = "gbManual";
        this.gbManual.TabStop = false;
        // 
        // tlpManualSend
        // 
        resources.ApplyResources(this.tlpManualSend, "tlpManualSend");
        this.tlpManualSend.Controls.Add(this.lManualPreview, 0, 0);
        this.tlpManualSend.Controls.Add(this.tbManual, 1, 0);
        this.tlpManualSend.Controls.Add(this.bManualSend, 2, 0);
        this.tlpManualSend.Name = "tlpManualSend";
        // 
        // lManualPreview
        // 
        resources.ApplyResources(this.lManualPreview, "lManualPreview");
        this.lManualPreview.Name = "lManualPreview";
        // 
        // tbManual
        // 
        resources.ApplyResources(this.tbManual, "tbManual");
        this.tbManual.Name = "tbManual";
        this.ttMain.SetToolTip(this.tbManual, resources.GetString("tbManual.ToolTip"));
        // 
        // bManualSend
        // 
        resources.ApplyResources(this.bManualSend, "bManualSend");
        this.bManualSend.Name = "bManualSend";
        this.bManualSend.UseVisualStyleBackColor = true;
        this.bManualSend.Click += new System.EventHandler(this.bManualSend_Click);
        // 
        // flowManual
        // 
        resources.ApplyResources(this.flowManual, "flowManual");
        this.flowManual.Controls.Add(this.fieldType);
        this.flowManual.Controls.Add(this.fieldStation);
        this.flowManual.Controls.Add(this.fieldTrain);
        this.flowManual.Controls.Add(this.fieldTime);
        this.flowManual.Controls.Add(this.fieldTrack);
        this.flowManual.Controls.Add(this.fieldSecondTrack);
        this.flowManual.Controls.Add(this.fieldRouteKind);
        this.flowManual.Controls.Add(this.fieldDelayReason);
        this.flowManual.Controls.Add(this.fieldTransferKind);
        this.flowManual.Name = "flowManual";
        // 
        // fieldType
        // 
        resources.ApplyResources(this.fieldType, "fieldType");
        this.fieldType.Controls.Add(this.lManualType);
        this.fieldType.Controls.Add(this.cbManualType);
        this.fieldType.Name = "fieldType";
        // 
        // lManualType
        // 
        resources.ApplyResources(this.lManualType, "lManualType");
        this.lManualType.Name = "lManualType";
        // 
        // cbManualType
        // 
        resources.ApplyResources(this.cbManualType, "cbManualType");
        this.cbManualType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbManualType.Name = "cbManualType";
        this.cbManualType.SelectedIndexChanged += new System.EventHandler(this.Manual_Changed);
        // 
        // fieldStation
        // 
        resources.ApplyResources(this.fieldStation, "fieldStation");
        this.fieldStation.Controls.Add(this.lManualStation);
        this.fieldStation.Controls.Add(this.tbManualStation);
        this.fieldStation.Name = "fieldStation";
        // 
        // lManualStation
        // 
        resources.ApplyResources(this.lManualStation, "lManualStation");
        this.lManualStation.Name = "lManualStation";
        // 
        // tbManualStation
        // 
        resources.ApplyResources(this.tbManualStation, "tbManualStation");
        this.tbManualStation.Name = "tbManualStation";
        this.tbManualStation.TextChanged += new System.EventHandler(this.Manual_Changed);
        // 
        // fieldTrain
        // 
        resources.ApplyResources(this.fieldTrain, "fieldTrain");
        this.fieldTrain.Controls.Add(this.lManualTrain);
        this.fieldTrain.Controls.Add(this.cbManualTrain);
        this.fieldTrain.Name = "fieldTrain";
        // 
        // lManualTrain
        // 
        resources.ApplyResources(this.lManualTrain, "lManualTrain");
        this.lManualTrain.Name = "lManualTrain";
        // 
        // cbManualTrain
        // 
        resources.ApplyResources(this.cbManualTrain, "cbManualTrain");
        this.cbManualTrain.Name = "cbManualTrain";
        this.cbManualTrain.SelectedIndexChanged += new System.EventHandler(this.Manual_Changed);
        this.cbManualTrain.TextChanged += new System.EventHandler(this.Manual_Changed);
        // 
        // fieldTime
        // 
        resources.ApplyResources(this.fieldTime, "fieldTime");
        this.fieldTime.Controls.Add(this.lManualTime);
        this.fieldTime.Controls.Add(this.dtpManualTime);
        this.fieldTime.Controls.Add(this.cbManualNow);
        this.fieldTime.Name = "fieldTime";
        // 
        // lManualTime
        // 
        resources.ApplyResources(this.lManualTime, "lManualTime");
        this.lManualTime.Name = "lManualTime";
        // 
        // dtpManualTime
        // 
        resources.ApplyResources(this.dtpManualTime, "dtpManualTime");
        this.dtpManualTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dtpManualTime.Name = "dtpManualTime";
        this.dtpManualTime.ShowUpDown = true;
        this.dtpManualTime.ValueChanged += new System.EventHandler(this.Manual_Changed);
        // 
        // cbManualNow
        // 
        resources.ApplyResources(this.cbManualNow, "cbManualNow");
        this.cbManualNow.Checked = true;
        this.cbManualNow.CheckState = System.Windows.Forms.CheckState.Checked;
        this.cbManualNow.Name = "cbManualNow";
        this.cbManualNow.UseVisualStyleBackColor = true;
        this.cbManualNow.CheckedChanged += new System.EventHandler(this.cbManualNow_CheckedChanged);
        // 
        // fieldTrack
        // 
        resources.ApplyResources(this.fieldTrack, "fieldTrack");
        this.fieldTrack.Controls.Add(this.lManualTrack);
        this.fieldTrack.Controls.Add(this.cbManualTrack);
        this.fieldTrack.Name = "fieldTrack";
        // 
        // lManualTrack
        // 
        resources.ApplyResources(this.lManualTrack, "lManualTrack");
        this.lManualTrack.Name = "lManualTrack";
        // 
        // cbManualTrack
        // 
        resources.ApplyResources(this.cbManualTrack, "cbManualTrack");
        this.cbManualTrack.Name = "cbManualTrack";
        this.cbManualTrack.SelectedIndexChanged += new System.EventHandler(this.Manual_Changed);
        this.cbManualTrack.TextChanged += new System.EventHandler(this.Manual_Changed);
        // 
        // fieldSecondTrack
        // 
        resources.ApplyResources(this.fieldSecondTrack, "fieldSecondTrack");
        this.fieldSecondTrack.Controls.Add(this.lManualSecondTrack);
        this.fieldSecondTrack.Controls.Add(this.cbManualSecondTrack);
        this.fieldSecondTrack.Name = "fieldSecondTrack";
        // 
        // lManualSecondTrack
        // 
        resources.ApplyResources(this.lManualSecondTrack, "lManualSecondTrack");
        this.lManualSecondTrack.Name = "lManualSecondTrack";
        // 
        // cbManualSecondTrack
        // 
        resources.ApplyResources(this.cbManualSecondTrack, "cbManualSecondTrack");
        this.cbManualSecondTrack.Name = "cbManualSecondTrack";
        this.cbManualSecondTrack.SelectedIndexChanged += new System.EventHandler(this.Manual_Changed);
        this.cbManualSecondTrack.TextChanged += new System.EventHandler(this.Manual_Changed);
        // 
        // fieldRouteKind
        // 
        resources.ApplyResources(this.fieldRouteKind, "fieldRouteKind");
        this.fieldRouteKind.Controls.Add(this.lManualRouteKind);
        this.fieldRouteKind.Controls.Add(this.cbManualRouteKind);
        this.fieldRouteKind.Name = "fieldRouteKind";
        // 
        // lManualRouteKind
        // 
        resources.ApplyResources(this.lManualRouteKind, "lManualRouteKind");
        this.lManualRouteKind.Name = "lManualRouteKind";
        // 
        // cbManualRouteKind
        // 
        resources.ApplyResources(this.cbManualRouteKind, "cbManualRouteKind");
        this.cbManualRouteKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbManualRouteKind.Name = "cbManualRouteKind";
        this.cbManualRouteKind.SelectedIndexChanged += new System.EventHandler(this.Manual_Changed);
        // 
        // fieldDelayReason
        // 
        resources.ApplyResources(this.fieldDelayReason, "fieldDelayReason");
        this.fieldDelayReason.Controls.Add(this.lManualDelayReason);
        this.fieldDelayReason.Controls.Add(this.cbManualDelayReason);
        this.fieldDelayReason.Name = "fieldDelayReason";
        // 
        // lManualDelayReason
        // 
        resources.ApplyResources(this.lManualDelayReason, "lManualDelayReason");
        this.lManualDelayReason.Name = "lManualDelayReason";
        // 
        // cbManualDelayReason
        // 
        resources.ApplyResources(this.cbManualDelayReason, "cbManualDelayReason");
        this.cbManualDelayReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbManualDelayReason.Name = "cbManualDelayReason";
        this.cbManualDelayReason.SelectedIndexChanged += new System.EventHandler(this.Manual_Changed);
        // 
        // fieldTransferKind
        // 
        resources.ApplyResources(this.fieldTransferKind, "fieldTransferKind");
        this.fieldTransferKind.Controls.Add(this.lManualTransferKind);
        this.fieldTransferKind.Controls.Add(this.cbManualTransferKind);
        this.fieldTransferKind.Name = "fieldTransferKind";
        // 
        // lManualTransferKind
        // 
        resources.ApplyResources(this.lManualTransferKind, "lManualTransferKind");
        this.lManualTransferKind.Name = "lManualTransferKind";
        // 
        // cbManualTransferKind
        // 
        resources.ApplyResources(this.cbManualTransferKind, "cbManualTransferKind");
        this.cbManualTransferKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbManualTransferKind.Name = "cbManualTransferKind";
        this.cbManualTransferKind.SelectedIndexChanged += new System.EventHandler(this.Manual_Changed);
        // 
        // statusStrip
        // 
        this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.slStation, this.slPlan, this.slSent, this.slLanguage });
        resources.ApplyResources(this.statusStrip, "statusStrip");
        this.statusStrip.Name = "statusStrip";
        // 
        // slStation
        // 
        this.slStation.Name = "slStation";
        resources.ApplyResources(this.slStation, "slStation");
        // 
        // slPlan
        // 
        this.slPlan.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
        this.slPlan.Name = "slPlan";
        resources.ApplyResources(this.slPlan, "slPlan");
        // 
        // slSent
        // 
        this.slSent.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
        this.slSent.Name = "slSent";
        resources.ApplyResources(this.slSent, "slSent");
        this.slSent.Spring = true;
        // 
        // slLanguage
        // 
        this.slLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.slLanguage.Name = "slLanguage";
        resources.ApplyResources(this.slLanguage, "slLanguage");
        this.slLanguage.SelectedIndexChanged += new System.EventHandler(this.slLanguage_SelectedIndexChanged);
        // 
        // fbdGvd
        // 
        this.fbdGvd.InitialDirectory = "C:\\WINDOWS\\system32";
        this.fbdGvd.ShowNewFolderButton = false;
        // 
        // tmrClock
        // 
        this.tmrClock.Interval = 250;
        this.tmrClock.Tick += new System.EventHandler(this.tmrClock_Tick);
        // 
        // FMain
        // 
        resources.ApplyResources(this, "$this");
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.splitMain);
        this.Controls.Add(this.panelRun);
        this.Controls.Add(this.panelTop);
        this.Controls.Add(this.gbManual);
        this.Controls.Add(this.statusStrip);
        this.Name = "FMain";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
        this.splitMain.Panel1.ResumeLayout(false);
        this.splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
        this.splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dgvPlan)).EndInit();
        this.panelTop.ResumeLayout(false);
        this.panelTop.PerformLayout();
        this.gbGvd.ResumeLayout(false);
        this.gbGvd.PerformLayout();
        this.tlpGvd.ResumeLayout(false);
        this.tlpGvd.PerformLayout();
        this.flowGvdButtons.ResumeLayout(false);
        this.flowGvdButtons.PerformLayout();
        this.gbConnection.ResumeLayout(false);
        this.gbConnection.PerformLayout();
        this.tlpConnection.ResumeLayout(false);
        this.tlpConnection.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
        this.gbModel.ResumeLayout(false);
        this.gbModel.PerformLayout();
        this.tlpModel.ResumeLayout(false);
        this.tlpModel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.nudOnTime)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudDiversion)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudMeanDelay)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudCancel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudReason)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudFactor)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.nudSeed)).EndInit();
        this.panelRun.ResumeLayout(false);
        this.panelRun.PerformLayout();
        this.fieldDay.ResumeLayout(false);
        this.fieldDay.PerformLayout();
        this.fieldFrom.ResumeLayout(false);
        this.fieldFrom.PerformLayout();
        this.gbManual.ResumeLayout(false);
        this.gbManual.PerformLayout();
        this.tlpManualSend.ResumeLayout(false);
        this.tlpManualSend.PerformLayout();
        this.flowManual.ResumeLayout(false);
        this.flowManual.PerformLayout();
        this.fieldType.ResumeLayout(false);
        this.fieldType.PerformLayout();
        this.fieldStation.ResumeLayout(false);
        this.fieldStation.PerformLayout();
        this.fieldTrain.ResumeLayout(false);
        this.fieldTrain.PerformLayout();
        this.fieldTime.ResumeLayout(false);
        this.fieldTime.PerformLayout();
        this.fieldTrack.ResumeLayout(false);
        this.fieldTrack.PerformLayout();
        this.fieldSecondTrack.ResumeLayout(false);
        this.fieldSecondTrack.PerformLayout();
        this.fieldRouteKind.ResumeLayout(false);
        this.fieldRouteKind.PerformLayout();
        this.fieldDelayReason.ResumeLayout(false);
        this.fieldDelayReason.PerformLayout();
        this.fieldTransferKind.ResumeLayout(false);
        this.fieldTransferKind.PerformLayout();
        this.statusStrip.ResumeLayout(false);
        this.statusStrip.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
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
    private System.Windows.Forms.DateTimePicker dtpDay;
    private System.Windows.Forms.FlowLayoutPanel fieldFrom;
    private System.Windows.Forms.Label lFrom;
    private System.Windows.Forms.DateTimePicker dtpFrom;
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
    private System.Windows.Forms.DateTimePicker dtpManualTime;
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
