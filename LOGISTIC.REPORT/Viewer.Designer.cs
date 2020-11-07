namespace LOGISTIC.REPORT
{
    partial class Viewer
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.ReportView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.CrystalReport11 = new LOGISTIC.REPORT.CrystalReport1();
            this.SuspendLayout();
            // 
            // ReportView
            // 
            this.ReportView.ActiveViewIndex = 0;
            this.ReportView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportView.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportView.Location = new System.Drawing.Point(0, 0);
            this.ReportView.Name = "ReportView";
            this.ReportView.ReportSource = this.CrystalReport11;
            this.ReportView.Size = new System.Drawing.Size(799, 566);
            this.ReportView.TabIndex = 0;
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 566);
            this.Controls.Add(this.ReportView);
            this.Name = "Viewer";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer ReportView;
        private CrystalReport1 CrystalReport11;
    }
}

