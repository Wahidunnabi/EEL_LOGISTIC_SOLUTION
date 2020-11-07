namespace LOGISTIC.UI.Report
{
    partial class HoursReportTest
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
            this.TrailerNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.dateReport = new System.Windows.Forms.DateTimePicker();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.radioStock = new System.Windows.Forms.RadioButton();
            this.radioOut = new System.Windows.Forms.RadioButton();
            this.radioIn = new System.Windows.Forms.RadioButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtTotalBox = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalTues = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalTues.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // TrailerNumber
            // 
            this.TrailerNumber.Caption = "Trailer Number";
            this.TrailerNumber.FieldName = "Trailer Number";
            this.TrailerNumber.Name = "TrailerNumber";
            this.TrailerNumber.Visible = true;
            this.TrailerNumber.Width = 80;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.Visible = true;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.Visible = true;
            // 
            // groupControl3
            // 
            this.groupControl3.Appearance.BackColor = System.Drawing.Color.Purple;
            this.groupControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl3.Appearance.Options.UseBackColor = true;
            this.groupControl3.Appearance.Options.UseFont = true;
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl3.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl3.AutoSize = true;
            this.groupControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Controls.Add(this.txtTotalBox);
            this.groupControl3.Controls.Add(this.txtTotalTues);
            this.groupControl3.Controls.Add(this.dateReport);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.dateTo);
            this.groupControl3.Controls.Add(this.btnLoad);
            this.groupControl3.Controls.Add(this.dataGridView1);
            this.groupControl3.Controls.Add(this.radioStock);
            this.groupControl3.Controls.Add(this.radioOut);
            this.groupControl3.Controls.Add(this.radioIn);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.dateFrom);
            this.groupControl3.Controls.Add(this.cmbClient);
            this.groupControl3.Controls.Add(this.btnExcel);
            this.groupControl3.Location = new System.Drawing.Point(6, 6);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1005, 536);
            this.groupControl3.TabIndex = 12;
            this.groupControl3.Text = "Container Search";
            // 
            // dateReport
            // 
            this.dateReport.CustomFormat = "dd/MMM/yyyy";
            this.dateReport.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateReport.Location = new System.Drawing.Point(643, 469);
            this.dateReport.Name = "dateReport";
            this.dateReport.Size = new System.Drawing.Size(124, 21);
            this.dateReport.TabIndex = 94;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelControl3.Location = new System.Drawing.Point(439, 472);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(198, 13);
            this.labelControl3.TabIndex = 93;
            this.labelControl3.Text = "Generate MLO wise In,Out,Stock on";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl2.Location = new System.Drawing.Point(389, 59);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(14, 13);
            this.labelControl2.TabIndex = 92;
            this.labelControl2.Text = "To";
            // 
            // dateTo
            // 
            this.dateTo.CustomFormat = "dd/MMM/yyyy";
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTo.Location = new System.Drawing.Point(409, 56);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(124, 21);
            this.dateTo.TabIndex = 91;
            // 
            // btnLoad
            // 
            this.btnLoad.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLoad.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLoad.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnLoad.Appearance.Options.UseBackColor = true;
            this.btnLoad.Appearance.Options.UseFont = true;
            this.btnLoad.Appearance.Options.UseForeColor = true;
            this.btnLoad.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnLoad.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnLoad.Location = new System.Drawing.Point(742, 53);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(85, 23);
            this.btnLoad.TabIndex = 90;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(70, 83);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(880, 361);
            this.dataGridView1.TabIndex = 89;
            // 
            // radioStock
            // 
            this.radioStock.AutoSize = true;
            this.radioStock.BackColor = System.Drawing.Color.Transparent;
            this.radioStock.Location = new System.Drawing.Point(660, 59);
            this.radioStock.Name = "radioStock";
            this.radioStock.Size = new System.Drawing.Size(61, 17);
            this.radioStock.TabIndex = 88;
            this.radioStock.TabStop = true;
            this.radioStock.Text = "STOCK";
            this.radioStock.UseVisualStyleBackColor = false;
            // 
            // radioOut
            // 
            this.radioOut.AutoSize = true;
            this.radioOut.BackColor = System.Drawing.Color.Transparent;
            this.radioOut.Location = new System.Drawing.Point(606, 59);
            this.radioOut.Name = "radioOut";
            this.radioOut.Size = new System.Drawing.Size(48, 17);
            this.radioOut.TabIndex = 87;
            this.radioOut.TabStop = true;
            this.radioOut.Text = "OUT";
            this.radioOut.UseVisualStyleBackColor = false;
            // 
            // radioIn
            // 
            this.radioIn.AutoSize = true;
            this.radioIn.BackColor = System.Drawing.Color.Transparent;
            this.radioIn.Location = new System.Drawing.Point(563, 59);
            this.radioIn.Name = "radioIn";
            this.radioIn.Size = new System.Drawing.Size(37, 17);
            this.radioIn.TabIndex = 86;
            this.radioIn.TabStop = true;
            this.radioIn.Text = "IN";
            this.radioIn.UseVisualStyleBackColor = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl1.Location = new System.Drawing.Point(224, 58);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 13);
            this.labelControl1.TabIndex = 85;
            this.labelControl1.Text = "From";
            // 
            // dateFrom
            // 
            this.dateFrom.CustomFormat = "dd/MMM/yyyy";
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFrom.Location = new System.Drawing.Point(259, 56);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(124, 21);
            this.dateFrom.TabIndex = 84;
            // 
            // cmbClient
            // 
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(70, 54);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(139, 21);
            this.cmbClient.TabIndex = 14;
            this.cmbClient.SelectionChangeCommitted += new System.EventHandler(this.cmbClient_SelectionChangeCommitted);
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExcel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Appearance.Options.UseForeColor = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnExcel.Location = new System.Drawing.Point(865, 461);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(85, 23);
            this.btnExcel.TabIndex = 13;
            this.btnExcel.Text = "Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl4.Location = new System.Drawing.Point(72, 469);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(68, 13);
            this.labelControl4.TabIndex = 103;
            this.labelControl4.Text = "Total Boxs : ";
            // 
            // txtTotalBox
            // 
            this.txtTotalBox.Location = new System.Drawing.Point(144, 466);
            this.txtTotalBox.Name = "txtTotalBox";
            this.txtTotalBox.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalBox.Properties.Appearance.Options.UseFont = true;
            this.txtTotalBox.Properties.MaxLength = 11;
            this.txtTotalBox.Size = new System.Drawing.Size(60, 20);
            this.txtTotalBox.TabIndex = 102;
            // 
            // txtTotalTues
            // 
            this.txtTotalTues.Location = new System.Drawing.Point(209, 466);
            this.txtTotalTues.Name = "txtTotalTues";
            this.txtTotalTues.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalTues.Properties.Appearance.Options.UseFont = true;
            this.txtTotalTues.Properties.MaxLength = 11;
            this.txtTotalTues.Size = new System.Drawing.Size(60, 20);
            this.txtTotalTues.TabIndex = 101;
            // 
            // HoursReportTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 548);
            this.Controls.Add(this.groupControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "HoursReportTest";
            this.Text = "Import : 24 Hours Report";
            this.Load += new System.EventHandler(this.ContainerSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalTues.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn TrailerNumber;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.ComboBox cmbClient;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton radioStock;
        private System.Windows.Forms.RadioButton radioOut;
        private System.Windows.Forms.RadioButton radioIn;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.DateTimePicker dateReport;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtTotalBox;
        private DevExpress.XtraEditors.TextEdit txtTotalTues;
    }

    #endregion
}
