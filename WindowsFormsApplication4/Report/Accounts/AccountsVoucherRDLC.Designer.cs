namespace LOGISTIC.UI.Report.Accounts
{
    partial class AccountsVoucherRDLC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.button2 = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AccountsVoucherEntityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AccountsVoucherEntityBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(759, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "AccountsVoucher";
            reportDataSource1.Value = this.AccountsVoucherEntityBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "LOGISTIC.UI.Report.Accounts.AccountsVoucher.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 31);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1226, 509);
            this.reportViewer1.TabIndex = 4;
            // 
            // AccountsVoucherEntityBindingSource
            // 
            this.AccountsVoucherEntityBindingSource.DataSource = typeof(LOGISTIC.UserDefinedModel.AccountsVoucherEntity);
            // 
            // AccountsVoucherRDLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 552);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.button2);
            this.Name = "AccountsVoucherRDLC";
            this.Text = "AccountsVoucherRDLC";
            this.Load += new System.EventHandler(this.AccountsVoucherRDLC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AccountsVoucherEntityBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource AccountsVoucherEntityBindingSource;
    }
}