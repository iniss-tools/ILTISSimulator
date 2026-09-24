using ExControls;
using GVDEditor.Entities;
using ILTISSimulator.Properties;
using ILTISSimulator.Simulation;
using ILTISSimulator.Tools;

namespace ILTISSimulator;

/// <summary>
///     Výber koľají, na ktoré smie simulátor stavať vlaky.
/// </summary>
internal partial class FTracks : Form
{
    /// <summary>
    ///     Šírka vnútra okna podľa návrhu - viď <see cref="DpiTools.ScaleToDpi" />.
    /// </summary>
    private const int DesignClientWidth = 480;

    /// <summary>
    ///     Vytvorí dialóg nad koľajami stanice.
    /// </summary>
    /// <param name="tracks">Všetky koľaje z grafikonu.</param>
    /// <param name="excluded">Kľúče koľají, ktoré sú momentálne vylúčené.</param>
    public FTracks(IEnumerable<Track> tracks, IEnumerable<string> excluded)
    {
        InitializeComponent();

        DpiTools.ScaleToDpi(this, DesignClientWidth);
        DpiTools.FitToScreen(this);

        var excludedKeys = new HashSet<string>(excluded ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);

        foreach (var track in tracks.Where(TrackAllocator.CanUse))
            clbTracks.Items.Add(new Item(track), !excludedKeys.Contains(track.Key));

        // Základ nápovedy dosadil ApplyResources, tu sa iba dopĺňa poznámka o skrytých koľajach.
        var hidden = tracks.Count(track => !TrackAllocator.CanUse(track) && track != Track.None);
        if (hidden != 0)
            lHint.Text += "\r\n" + Texts.F(Resources.Tracks_HiddenReason, HiddenText(hidden));

        UpdateCount();
    }

    /// <summary>
    ///     Kľúče koľají, ktoré má simulátor vynechať.
    /// </summary>
    public ISet<string> ExcludedKeys { get; private set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    private void bAll_Click(object sender, EventArgs e) => SetAll(true);

    private void bNone_Click(object sender, EventArgs e) => SetAll(false);

    private void SetAll(bool used)
    {
        for (var i = 0; i < clbTracks.Items.Count; i++)
            clbTracks.SetItemChecked(i, used);
    }

    private void clbTracks_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        // ItemCheck sa volá ešte pred zmenou zoznamu označených položiek, preto sa počet prepočíta až
        // potom. Pri plnení zoznamu v konštruktore okno ešte nemá handle a rátať netreba.
        if (IsHandleCreated)
            BeginInvoke(new Action(UpdateCount));
    }

    private void UpdateCount() =>
        lCount.Text = Texts.F(Resources.Tracks_Count, clbTracks.CheckedItems.Count, clbTracks.Items.Count);

    private void bOk_Click(object sender, EventArgs e)
    {
        if (clbTracks.CheckedItems.Count == 0)
        {
            ExMessageBox.Show(this, Resources.Tracks_NeedOne, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var excluded = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (var i = 0; i < clbTracks.Items.Count; i++)
            if (!clbTracks.GetItemChecked(i) && clbTracks.Items[i] is Item item)
                excluded.Add(item.Track.Key);

        ExcludedKeys = excluded;
        DialogResult = DialogResult.OK;
        Close();
    }

    private static string HiddenText(int count) => Texts.Count(count, Resources.Tracks_HiddenOne,
        Resources.Tracks_HiddenFew, Resources.Tracks_HiddenMany);

    private sealed class Item
    {
        public Item(Track track) => Track = track;

        public Track Track { get; }

        public override string ToString()
        {
            var platform = Track.Platform is null || string.IsNullOrWhiteSpace(Track.Platform.Key) ||
                           Track.Platform.Key == "N"
                ? Resources.Tracks_NoPlatform
                : Texts.F(Resources.Tracks_WithPlatform, Track.Platform.Key);

            return Texts.F(Resources.Tracks_ItemFormat, Track.Key, Track.FullName, platform);
        }
    }
}
