namespace ILTISSimulator;

partial class FTracks
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTracks));
        clbTracks = new CheckedListBox();
        lHint = new Label();
        tlpBottom = new TableLayoutPanel();
        lCount = new Label();
        flowSelect = new FlowLayoutPanel();
        bAll = new Button();
        bNone = new Button();
        flowDialog = new FlowLayoutPanel();
        bCancel = new Button();
        bOk = new Button();
        tlpBottom.SuspendLayout();
        flowSelect.SuspendLayout();
        flowDialog.SuspendLayout();
        SuspendLayout();
        // 
        // clbTracks
        // 
        clbTracks.CheckOnClick = true;
        resources.ApplyResources(clbTracks, "clbTracks");
        clbTracks.Name = "clbTracks";
        clbTracks.ItemCheck += clbTracks_ItemCheck;
        // 
        // lHint
        // 
        resources.ApplyResources(lHint, "lHint");
        lHint.Name = "lHint";
        // 
        // tlpBottom
        // 
        resources.ApplyResources(tlpBottom, "tlpBottom");
        tlpBottom.Controls.Add(lCount, 0, 0);
        tlpBottom.Controls.Add(flowSelect, 0, 1);
        tlpBottom.Controls.Add(flowDialog, 1, 1);
        tlpBottom.Name = "tlpBottom";
        // 
        // lCount
        // 
        resources.ApplyResources(lCount, "lCount");
        tlpBottom.SetColumnSpan(lCount, 2);
        lCount.Name = "lCount";
        // 
        // flowSelect
        // 
        resources.ApplyResources(flowSelect, "flowSelect");
        flowSelect.Controls.Add(bAll);
        flowSelect.Controls.Add(bNone);
        flowSelect.Name = "flowSelect";
        // 
        // bAll
        // 
        resources.ApplyResources(bAll, "bAll");
        bAll.Name = "bAll";
        bAll.UseVisualStyleBackColor = true;
        bAll.Click += bAll_Click;
        // 
        // bNone
        // 
        resources.ApplyResources(bNone, "bNone");
        bNone.Name = "bNone";
        bNone.UseVisualStyleBackColor = true;
        bNone.Click += bNone_Click;
        // 
        // flowDialog
        // 
        resources.ApplyResources(flowDialog, "flowDialog");
        flowDialog.Controls.Add(bCancel);
        flowDialog.Controls.Add(bOk);
        flowDialog.Name = "flowDialog";
        // 
        // bCancel
        // 
        resources.ApplyResources(bCancel, "bCancel");
        bCancel.DialogResult = DialogResult.Cancel;
        bCancel.Name = "bCancel";
        bCancel.UseVisualStyleBackColor = true;
        // 
        // bOk
        // 
        resources.ApplyResources(bOk, "bOk");
        bOk.Name = "bOk";
        bOk.UseVisualStyleBackColor = true;
        bOk.Click += bOk_Click;
        // 
        // FTracks
        // 
        AcceptButton = bOk;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = bCancel;
        Controls.Add(clbTracks);
        Controls.Add(lHint);
        Controls.Add(tlpBottom);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FTracks";
        ShowIcon = false;
        ShowInTaskbar = false;
        tlpBottom.ResumeLayout(false);
        tlpBottom.PerformLayout();
        flowSelect.ResumeLayout(false);
        flowSelect.PerformLayout();
        flowDialog.ResumeLayout(false);
        flowDialog.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Label lHint;
    private System.Windows.Forms.CheckedListBox clbTracks;
    private System.Windows.Forms.TableLayoutPanel tlpBottom;
    private System.Windows.Forms.Label lCount;
    private System.Windows.Forms.FlowLayoutPanel flowSelect;
    private System.Windows.Forms.Button bAll;
    private System.Windows.Forms.Button bNone;
    private System.Windows.Forms.FlowLayoutPanel flowDialog;
    private System.Windows.Forms.Button bOk;
    private System.Windows.Forms.Button bCancel;
}
