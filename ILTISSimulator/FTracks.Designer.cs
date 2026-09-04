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
        this.clbTracks = new System.Windows.Forms.CheckedListBox();
        this.lHint = new System.Windows.Forms.Label();
        this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
        this.lCount = new System.Windows.Forms.Label();
        this.flowSelect = new System.Windows.Forms.FlowLayoutPanel();
        this.bAll = new System.Windows.Forms.Button();
        this.bNone = new System.Windows.Forms.Button();
        this.flowDialog = new System.Windows.Forms.FlowLayoutPanel();
        this.bCancel = new System.Windows.Forms.Button();
        this.bOk = new System.Windows.Forms.Button();
        this.tlpBottom.SuspendLayout();
        this.flowSelect.SuspendLayout();
        this.flowDialog.SuspendLayout();
        this.SuspendLayout();
        // 
        // clbTracks
        // 
        this.clbTracks.CheckOnClick = true;
        resources.ApplyResources(this.clbTracks, "clbTracks");
        this.clbTracks.Name = "clbTracks";
        this.clbTracks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbTracks_ItemCheck);
        // 
        // lHint
        // 
        resources.ApplyResources(this.lHint, "lHint");
        this.lHint.Name = "lHint";
        // 
        // tlpBottom
        // 
        resources.ApplyResources(this.tlpBottom, "tlpBottom");
        this.tlpBottom.Controls.Add(this.lCount, 0, 0);
        this.tlpBottom.Controls.Add(this.flowSelect, 0, 1);
        this.tlpBottom.Controls.Add(this.flowDialog, 1, 1);
        this.tlpBottom.Name = "tlpBottom";
        // 
        // lCount
        // 
        resources.ApplyResources(this.lCount, "lCount");
        this.tlpBottom.SetColumnSpan(this.lCount, 2);
        this.lCount.Name = "lCount";
        // 
        // flowSelect
        // 
        resources.ApplyResources(this.flowSelect, "flowSelect");
        this.flowSelect.Controls.Add(this.bAll);
        this.flowSelect.Controls.Add(this.bNone);
        this.flowSelect.Name = "flowSelect";
        // 
        // bAll
        // 
        resources.ApplyResources(this.bAll, "bAll");
        this.bAll.Name = "bAll";
        this.bAll.UseVisualStyleBackColor = true;
        this.bAll.Click += new System.EventHandler(this.bAll_Click);
        // 
        // bNone
        // 
        resources.ApplyResources(this.bNone, "bNone");
        this.bNone.Name = "bNone";
        this.bNone.UseVisualStyleBackColor = true;
        this.bNone.Click += new System.EventHandler(this.bNone_Click);
        // 
        // flowDialog
        // 
        resources.ApplyResources(this.flowDialog, "flowDialog");
        this.flowDialog.Controls.Add(this.bCancel);
        this.flowDialog.Controls.Add(this.bOk);
        this.flowDialog.Name = "flowDialog";
        // 
        // bCancel
        // 
        resources.ApplyResources(this.bCancel, "bCancel");
        this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.bCancel.Name = "bCancel";
        this.bCancel.UseVisualStyleBackColor = true;
        // 
        // bOk
        // 
        resources.ApplyResources(this.bOk, "bOk");
        this.bOk.Name = "bOk";
        this.bOk.UseVisualStyleBackColor = true;
        this.bOk.Click += new System.EventHandler(this.bOk_Click);
        // 
        // FTracks
        // 
        this.AcceptButton = this.bOk;
        resources.ApplyResources(this, "$this");
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.bCancel;
        this.Controls.Add(this.clbTracks);
        this.Controls.Add(this.lHint);
        this.Controls.Add(this.tlpBottom);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FTracks";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.tlpBottom.ResumeLayout(false);
        this.tlpBottom.PerformLayout();
        this.flowSelect.ResumeLayout(false);
        this.flowSelect.PerformLayout();
        this.flowDialog.ResumeLayout(false);
        this.flowDialog.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
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
