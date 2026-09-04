
namespace GTNCommands
{
    partial class FMain
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCommand = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbStation = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbCommand = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.bSend = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mtbIP = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.nudTrainNum = new System.Windows.Forms.NumericUpDown();
            this.nudTrack = new System.Windows.Forms.NumericUpDown();
            this.cboxOnTop = new System.Windows.Forms.CheckBox();
            this.bSetStations = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.lConnecting = new System.Windows.Forms.Label();
            this.fOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.bckWorkerListening = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrainNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.46922F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.2357F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.57461F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.57461F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.57461F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.57123F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbCommand, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbStation, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.dtpDate, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.dtpTime, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tbCommand, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.bSend, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.rtbLog, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mtbIP, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudPort, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudTrainNum, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.nudTrack, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboxOnTop, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.bSetStations, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.bConnect, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lConnecting, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(677, 266);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 69);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Príkaz:";
            // 
            // cbCommand
            // 
            this.cbCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCommand.FormattingEnabled = true;
            this.cbCommand.Location = new System.Drawing.Point(10, 84);
            this.cbCommand.Margin = new System.Windows.Forms.Padding(2);
            this.cbCommand.Name = "cbCommand";
            this.cbCommand.Size = new System.Drawing.Size(170, 21);
            this.cbCommand.TabIndex = 9;
            this.cbCommand.SelectedIndexChanged += new System.EventHandler(this.cbCommand_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Stanica:";
            // 
            // cbStation
            // 
            this.cbStation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbStation.Location = new System.Drawing.Point(184, 84);
            this.cbStation.Margin = new System.Windows.Forms.Padding(2);
            this.cbStation.Name = "cbStation";
            this.cbStation.Size = new System.Drawing.Size(123, 21);
            this.cbStation.TabIndex = 11;
            this.cbStation.SelectedIndexChanged += new System.EventHandler(this.cbStation_SelectedIndexChanged);
            this.cbStation.TextChanged += new System.EventHandler(this.cbStation_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 69);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Číslo vlaku:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(400, 69);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Dátum:";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd.MM.";
            this.dtpDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(400, 84);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.ShowUpDown = true;
            this.dtpDate.Size = new System.Drawing.Size(85, 20);
            this.dtpDate.TabIndex = 15;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(489, 69);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Čas:";
            // 
            // dtpTime
            // 
            this.dtpTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(489, 84);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(85, 20);
            this.dtpTime.TabIndex = 17;
            this.dtpTime.ValueChanged += new System.EventHandler(this.dtpTime_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(578, 69);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Koľaj:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(10, 123);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 28);
            this.label9.TabIndex = 20;
            this.label9.Text = "Výsledný príkaz:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbCommand
            // 
            this.tbCommand.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.tbCommand, 4);
            this.tbCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCommand.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbCommand.Location = new System.Drawing.Point(184, 125);
            this.tbCommand.Margin = new System.Windows.Forms.Padding(2);
            this.tbCommand.Multiline = true;
            this.tbCommand.Name = "tbCommand";
            this.tbCommand.Size = new System.Drawing.Size(390, 24);
            this.tbCommand.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 151);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Log";
            // 
            // bSend
            // 
            this.bSend.AutoSize = true;
            this.bSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bSend.Location = new System.Drawing.Point(578, 125);
            this.bSend.Margin = new System.Windows.Forms.Padding(2);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(89, 24);
            this.bSend.TabIndex = 22;
            this.bSend.Text = "Odoslať";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.rtbLog, 6);
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(10, 166);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(2);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(657, 90);
            this.rtbLog.TabIndex = 24;
            this.rtbLog.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(10, 8);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "IP adresa INISS:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // mtbIP
            // 
            this.mtbIP.BeepOnError = true;
            this.mtbIP.Culture = new System.Globalization.CultureInfo("en-US");
            this.mtbIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtbIP.Location = new System.Drawing.Point(10, 31);
            this.mtbIP.Margin = new System.Windows.Forms.Padding(2);
            this.mtbIP.Mask = "###.###.###.###";
            this.mtbIP.Name = "mtbIP";
            this.mtbIP.Size = new System.Drawing.Size(170, 20);
            this.mtbIP.TabIndex = 1;
            this.mtbIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtbIP.ValidatingType = typeof(System.Net.IPAddress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(184, 8);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 21);
            this.label8.TabIndex = 2;
            this.label8.Text = "Port INISS:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // nudPort
            // 
            this.nudPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudPort.Location = new System.Drawing.Point(184, 31);
            this.nudPort.Margin = new System.Windows.Forms.Padding(2);
            this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(123, 20);
            this.nudPort.TabIndex = 3;
            // 
            // nudTrainNum
            // 
            this.nudTrainNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudTrainNum.Location = new System.Drawing.Point(311, 84);
            this.nudTrainNum.Margin = new System.Windows.Forms.Padding(2);
            this.nudTrainNum.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudTrainNum.Name = "nudTrainNum";
            this.nudTrainNum.Size = new System.Drawing.Size(85, 20);
            this.nudTrainNum.TabIndex = 13;
            this.nudTrainNum.ValueChanged += new System.EventHandler(this.nudTrainNum_ValueChanged);
            // 
            // nudTrack
            // 
            this.nudTrack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudTrack.Location = new System.Drawing.Point(578, 84);
            this.nudTrack.Margin = new System.Windows.Forms.Padding(2);
            this.nudTrack.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudTrack.Name = "nudTrack";
            this.nudTrack.Size = new System.Drawing.Size(89, 20);
            this.nudTrack.TabIndex = 19;
            this.nudTrack.ValueChanged += new System.EventHandler(this.nudTrack_ValueChanged);
            // 
            // cboxOnTop
            // 
            this.cboxOnTop.AutoSize = true;
            this.cboxOnTop.Location = new System.Drawing.Point(578, 10);
            this.cboxOnTop.Margin = new System.Windows.Forms.Padding(2);
            this.cboxOnTop.Name = "cboxOnTop";
            this.cboxOnTop.Size = new System.Drawing.Size(89, 17);
            this.cboxOnTop.TabIndex = 6;
            this.cboxOnTop.Text = "Vždy navrchu";
            this.cboxOnTop.UseVisualStyleBackColor = true;
            this.cboxOnTop.CheckedChanged += new System.EventHandler(this.cboxOnTop_CheckedChanged);
            // 
            // bSetStations
            // 
            this.bSetStations.Location = new System.Drawing.Point(578, 31);
            this.bSetStations.Margin = new System.Windows.Forms.Padding(2);
            this.bSetStations.Name = "bSetStations";
            this.bSetStations.Size = new System.Drawing.Size(85, 19);
            this.bSetStations.TabIndex = 7;
            this.bSetStations.Text = "Stanice...";
            this.bSetStations.UseVisualStyleBackColor = true;
            this.bSetStations.Click += new System.EventHandler(this.bSetStations_Click);
            // 
            // bConnect
            // 
            this.bConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bConnect.Location = new System.Drawing.Point(311, 10);
            this.bConnect.Margin = new System.Windows.Forms.Padding(2);
            this.bConnect.Name = "bConnect";
            this.tableLayoutPanel1.SetRowSpan(this.bConnect, 2);
            this.bConnect.Size = new System.Drawing.Size(85, 41);
            this.bConnect.TabIndex = 4;
            this.bConnect.Text = "Pripojiť";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // lConnecting
            // 
            this.lConnecting.AutoSize = true;
            this.lConnecting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lConnecting.Location = new System.Drawing.Point(400, 8);
            this.lConnecting.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lConnecting.Name = "lConnecting";
            this.tableLayoutPanel1.SetRowSpan(this.lConnecting, 2);
            this.lConnecting.Size = new System.Drawing.Size(85, 45);
            this.lConnecting.TabIndex = 5;
            this.lConnecting.Text = "Pripája sa...";
            this.lConnecting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lConnecting.Visible = false;
            // 
            // fOpenDialog
            // 
            this.fOpenDialog.FileName = "STANICE.TXT";
            this.fOpenDialog.Filter = "Textový súbor|*.txt";
            this.fOpenDialog.Title = "Otvorte súbor STANICE.TXT";
            // 
            // bckWorkerListening
            // 
            this.bckWorkerListening.WorkerReportsProgress = true;
            this.bckWorkerListening.WorkerSupportsCancellation = true;
            this.bckWorkerListening.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckWorkerListening_DoWork);
            this.bckWorkerListening.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bckWorkerListening_ProgressChanged);
            this.bckWorkerListening.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckWorkerListening_RunWorkerCompleted);
            // 
            // FMain
            // 
            this.AcceptButton = this.bSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 266);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(693, 305);
            this.Name = "FMain";
            this.Text = "GTNCommands";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FMain_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrainNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCommand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbStation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox mtbIP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbCommand;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.CheckBox cboxOnTop;
        private System.Windows.Forms.NumericUpDown nudTrainNum;
        private System.Windows.Forms.NumericUpDown nudTrack;
        private System.Windows.Forms.Label lConnecting;
        private System.Windows.Forms.Button bSetStations;
        private System.Windows.Forms.OpenFileDialog fOpenDialog;
        private System.ComponentModel.BackgroundWorker bckWorkerListening;
    }
}

