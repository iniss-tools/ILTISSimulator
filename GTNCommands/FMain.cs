using System.ComponentModel;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using ExControls;
using GTNCommands.Entities;
using GTNCommands.Tools;
using ToolsCore.Iniss.Tools;
using Utils = GTNCommands.Tools.Utils;

namespace GTNCommands;

internal partial class FMain : Form
{
    private TcpClient? _connection;
    private string? _command;
    private List<Station>? _stations;
    //private TcpListener _listener;
    private NetworkStream? _stream;

    public FMain()
    {
        InitializeComponent();

        cbCommand.DataSource = CommandType.GetValues();
        rtbLog.SetTheme(WindowsTheme.Default);

        Utils.GetLastConnection(out var ip, out var port);
        mtbIP.Text = ip;
        nudPort.Value = port;

        InitStationList(Utils.GetRecentPath());
    }

    private void InitStationList(string path)
    {
        try
        {
            _stations = TXTParser.GetStations(path);
        }
        catch (Exception e)
        {
            AppendTextLog(LogType.Error, e.Message);
            return;
        }

        cbStation.DataSource = _stations;

        var autocom = new AutoCompleteStringCollection();
        foreach (var station in _stations!)
        {
            autocom.Add(station.ID.ToString());
            autocom.Add(station.Name);
        }

        cbStation.AutoCompleteCustomSource = autocom;
    }

    private void FMain_FormClosed(object sender, FormClosedEventArgs e) => _connection?.Close();

    private void cboxOnTop_CheckedChanged(object sender, EventArgs e) => TopMost = cboxOnTop.Checked;

    private async void bConnect_Click(object sender, EventArgs e)
    {
        if (Utils.CheckIP(mtbIP.Text))
            try
            {
                lConnecting.Visible = true;

                var ip = mtbIP.Text;
                var port = decimal.ToInt32(nudPort.Value);

                Utils.SetLastConnection(ip,port);

                _connection?.Close();
                
                await Task.Run(() =>
                {
                    _connection = new TcpClient(ip, port);
                    _stream = _connection.GetStream();
                    Invoke(() => AppendTextLog(LogType.Connected, $"Pripojené na: {ip}, port: {port}"));
                });

                //_listener = TcpListener.Create(decimal.ToInt32(port));
                //AppendTextLog(LogType.Info, "Port listener has started");
                //if (!bckWorkerListening.IsBusy) 
                    //bckWorkerListening.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                AppendTextLog(LogType.Error, ex.Message);
            }
        else
            AppendTextLog(LogType.Error, "Neplatná IP adresa");

        lConnecting.Visible = false;
    }

    private async void bSend_Click(object sender, EventArgs e)
    {
        if (_connection is null)
        {
            AppendTextLog(LogType.Error, "Spojenie s INISSom sa dosiaľ neuskutočnilo");
            return;
        }

        if (!_connection.Connected)
        {
            try
            {
                await Task.Run(() =>
                {
                    _connection.Close();
                    _connection = new TcpClient(mtbIP.Text, decimal.ToInt32(nudPort.Value));
                    _stream = _connection.GetStream();
                });
            }
            catch (Exception exception)
            {
                AppendTextLog(LogType.Error, exception.Message);
            }
            
        }

        if (string.IsNullOrEmpty(_command))
        {
            AppendTextLog(LogType.Error, "Nebol zadaný príkaz");
            return;
        }

        await Task.Run(() =>
        {
            try
            {
                var buf = Encoding.ASCII.GetBytes(_command);
                _stream!.Write(buf, 0, buf.Length);
                _stream.Dispose();
                Invoke(() => AppendTextLog(LogType.Info, "Cmd send: " + _command));
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex)
            {
                Invoke(() => AppendTextLog(LogType.Error, ex.Message));
            }
        });
    }

    private void bckWorkerListening_DoWork(object sender, DoWorkEventArgs e)
    {
        /*_listener.Start();

        var received = "";

        var tcpClient = _listener.AcceptTcpClient();
        var stream = tcpClient.GetStream();
        var bytes = new byte[tcpClient.SendBufferSize];
        while (true)
        {
            var recv = stream.Read(bytes, 0, tcpClient.SendBufferSize);
            received += Encoding.ASCII.GetString(bytes, 0, recv);

            bckWorkerListening.ReportProgress(0, received);
            received = "";

            if (bckWorkerListening.CancellationPending) break;
        }*/
    }

    private void bckWorkerListening_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        //AppendTextLog(LogType.Text, "Received: " + e.UserState);
    }

    private void bckWorkerListening_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        //if (e.Cancelled) _listener?.Stop();
    }

    private void AppendTextLog(LogType logType, string text)
    {
        var fc = logType switch
        {
            LogType.Error => Color.Red,
            LogType.Warning => Color.OrangeRed,
            LogType.Connected => Color.Green,
            _ => rtbLog.ForeColor
        };

        var time = DateTime.Now;

        rtbLog.SelectionStart = rtbLog.TextLength;
        rtbLog.SelectionLength = 0;

        rtbLog.SelectionColor = fc;
        rtbLog.AppendText($"[{time:dd.MM.yyyy HH:mm:ss}] - {logType}: {text}\r\n");
        rtbLog.SelectionColor = rtbLog.ForeColor;
    }

    private void UpdateCmd()
    {
        var builder = new StringBuilder();

        string stationid;

        if (int.TryParse(cbStation.Text, out var num))
            stationid = num.PadZeros(6);
        else if (cbStation.SelectedItem is Station st)
            stationid = st.ID.PadZeros(6);
        else if (_stations != null && Station.TryParseName(_stations, cbStation.Text, out var station) && station.HasValue)
            stationid = station.Value.ID.PadZeros(6);
        else
            stationid = 0.PadZeros(6);

        var trainnum = nudTrainNum.Value.ToString(CultureInfo.InvariantCulture).PadRight(6);

        var month = dtpDate.Value.Month.PadZeros(2);
        var day = dtpDate.Value.Day.PadZeros(2);
        var hour = dtpTime.Value.Hour.PadZeros(2);
        var min = dtpTime.Value.Minute.PadZeros(2);
        var sec = dtpTime.Value.Second.PadZeros(2);
        var track = decimal.ToInt32(nudTrack.Value).ToString();

        var selected = (CommandType)cbCommand.SelectedItem!;

        if (selected == CommandType.VVC)
        {
            builder.Append("0804");
            builder.Append(stationid);
            builder.Append(trainnum);

            builder.Append(month);
            builder.Append(day);
            builder.Append(hour);
            builder.Append(min);
            builder.Append(sec);

            builder.Append('0');
            builder.Append(track);
        }
        else if (selected == CommandType.OVC)
        {
            builder.Append("0804");
            builder.Append(stationid);
            builder.Append(trainnum);

            builder.Append(month);
            builder.Append(day);
            builder.Append(hour);
            builder.Append(min);
            builder.Append(sec);

            builder.Append('1');
            builder.Append(track);
        }
        else if (selected == CommandType.Vj)
        {
            builder.Append("0802");
            builder.Append(stationid);
            builder.Append(trainnum);

            builder.Append(month);
            builder.Append(day);
            builder.Append(hour);
            builder.Append(min);
            builder.Append(sec);

            builder.Append(track);
        }
        else if (selected == CommandType.Od)
        {
            builder.Append("0803");
            builder.Append(stationid);
            builder.Append(trainnum);

            builder.Append(month);
            builder.Append(day);
            builder.Append(hour);
            builder.Append(min);
            builder.Append(sec);

            builder.Append(track);
        }

        _command = builder.ToString();
        tbCommand.Text = _command;
    }

    private void cbCommand_SelectedIndexChanged(object sender, EventArgs e) => UpdateCmd();

    private void cbStation_SelectedIndexChanged(object sender, EventArgs e) => UpdateCmd();

    private void cbStation_TextChanged(object sender, EventArgs e) => UpdateCmd();

    private void nudTrainNum_ValueChanged(object sender, EventArgs e) => UpdateCmd();

    private void dtpDate_ValueChanged(object sender, EventArgs e) => UpdateCmd();

    private void dtpTime_ValueChanged(object sender, EventArgs e) => UpdateCmd();

    private void nudTrack_ValueChanged(object sender, EventArgs e) => UpdateCmd();

    private void bSetStations_Click(object sender, EventArgs e)
    {
        var result = fOpenDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            Utils.SetRecentPath(fOpenDialog.FileName);
            InitStationList(fOpenDialog.FileName);
        }
    }

    private enum LogType
    {
        Info,
        Error,
        Warning,
        Connected,
        Text
    }
}