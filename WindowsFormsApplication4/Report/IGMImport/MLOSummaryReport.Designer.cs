namespace LOGISTIC.UI.Report
{
    partial class MLOSummaryReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MLOSummaryReport));
            this.TrailerNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.cmbConType = new System.Windows.Forms.ComboBox();
            this.cmbContSize = new System.Windows.Forms.ComboBox();
            this.cmbSearch = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.txtTotalBox = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalTues = new DevExpress.XtraEditors.TextEdit();
            this.radioStock = new System.Windows.Forms.RadioButton();
            this.radioOut = new System.Windows.Forms.RadioButton();
            this.RadioIn = new System.Windows.Forms.RadioButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalTues.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.groupControl3.Controls.Add(this.progressBar1);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Controls.Add(this.txtSearch);
            this.groupControl3.Controls.Add(this.cmbConType);
            this.groupControl3.Controls.Add(this.cmbContSize);
            this.groupControl3.Controls.Add(this.cmbSearch);
            this.groupControl3.Controls.Add(this.panel2);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.btnExcel);
            this.groupControl3.Controls.Add(this.txtTotalBox);
            this.groupControl3.Controls.Add(this.txtTotalTues);
            this.groupControl3.Controls.Add(this.radioStock);
            this.groupControl3.Controls.Add(this.radioOut);
            this.groupControl3.Controls.Add(this.RadioIn);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.dateFrom);
            this.groupControl3.Controls.Add(this.dateTo);
            this.groupControl3.Controls.Add(this.dataGridView1);
            this.groupControl3.Controls.Add(this.cmbClient);
            this.groupControl3.Location = new System.Drawing.Point(6, 6);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1005, 536);
            this.groupControl3.TabIndex = 12;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(680, 484);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(74, 23);
            this.progressBar1.TabIndex = 129;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl4.Location = new System.Drawing.Point(478, 489);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 13);
            this.labelControl4.TabIndex = 128;
            this.labelControl4.Text = "Tues : ";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(204, 88);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(336, 20);
            this.txtSearch.TabIndex = 127;
            this.txtSearch.Visible = false;
            // 
            // cmbConType
            // 
            this.cmbConType.FormattingEnabled = true;
            this.cmbConType.Location = new System.Drawing.Point(204, 87);
            this.cmbConType.Name = "cmbConType";
            this.cmbConType.Size = new System.Drawing.Size(145, 21);
            this.cmbConType.TabIndex = 126;
            this.cmbConType.Visible = false;
            // 
            // cmbContSize
            // 
            this.cmbContSize.FormattingEnabled = true;
            this.cmbContSize.Location = new System.Drawing.Point(384, 87);
            this.cmbContSize.Name = "cmbContSize";
            this.cmbContSize.Size = new System.Drawing.Size(157, 21);
            this.cmbContSize.TabIndex = 125;
            this.cmbContSize.Visible = false;
            // 
            // cmbSearch
            // 
            this.cmbSearch.FormattingEnabled = true;
            this.cmbSearch.Location = new System.Drawing.Point(31, 87);
            this.cmbSearch.Name = "cmbSearch";
            this.cmbSearch.Size = new System.Drawing.Size(167, 21);
            this.cmbSearch.TabIndex = 124;
            this.cmbSearch.SelectedIndexChanged += new System.EventHandler(this.cmbSearch_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSlateGray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnLoad);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Location = new System.Drawing.Point(572, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 35);
            this.panel2.TabIndex = 99;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(167, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLoad.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnLoad.Appearance.Options.UseFont = true;
            this.btnLoad.Appearance.Options.UseForeColor = true;
            this.btnLoad.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnLoad.Location = new System.Drawing.Point(5, 6);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 90;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(86, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = " Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl3.Location = new System.Drawing.Point(337, 488);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(68, 13);
            this.labelControl3.TabIndex = 100;
            this.labelControl3.Text = "Total Boxs : ";
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExcel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Appearance.Options.UseForeColor = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnExcel.Location = new System.Drawing.Point(599, 484);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExcel.TabIndex = 13;
            this.btnExcel.Text = "Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // txtTotalBox
            // 
            this.txtTotalBox.Location = new System.Drawing.Point(407, 485);
            this.txtTotalBox.Name = "txtTotalBox";
            this.txtTotalBox.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalBox.Properties.Appearance.Options.UseFont = true;
            this.txtTotalBox.Properties.MaxLength = 11;
            this.txtTotalBox.Size = new System.Drawing.Size(60, 20);
            this.txtTotalBox.TabIndex = 99;
            // 
            // txtTotalTues
            // 
            this.txtTotalTues.Location = new System.Drawing.Point(521, 485);
            this.txtTotalTues.Name = "txtTotalTues";
            this.txtTotalTues.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalTues.Properties.Appearance.Options.UseFont = true;
            this.txtTotalTues.Properties.MaxLength = 11;
            this.txtTotalTues.Size = new System.Drawing.Size(60, 20);
            this.txtTotalTues.TabIndex = 98;
            // 
            // radioStock
            // 
            this.radioStock.AutoSize = true;
            this.radioStock.BackColor = System.Drawing.Color.Transparent;
            this.radioStock.Location = new System.Drawing.Point(760, 45);
            this.radioStock.Name = "radioStock";
            this.radioStock.Size = new System.Drawing.Size(61, 17);
            this.radioStock.TabIndex = 97;
            this.radioStock.TabStop = true;
            this.radioStock.Text = "STOCK";
            this.radioStock.UseVisualStyleBackColor = false;
            // 
            // radioOut
            // 
            this.radioOut.AutoSize = true;
            this.radioOut.BackColor = System.Drawing.Color.Transparent;
            this.radioOut.Location = new System.Drawing.Point(706, 45);
            this.radioOut.Name = "radioOut";
            this.radioOut.Size = new System.Drawing.Size(48, 17);
            this.radioOut.TabIndex = 96;
            this.radioOut.TabStop = true;
            this.radioOut.Text = "OUT";
            this.radioOut.UseVisualStyleBackColor = false;
            // 
            // RadioIn
            // 
            this.RadioIn.AutoSize = true;
            this.RadioIn.BackColor = System.Drawing.Color.Transparent;
            this.RadioIn.Location = new System.Drawing.Point(663, 45);
            this.RadioIn.Name = "RadioIn";
            this.RadioIn.Size = new System.Drawing.Size(37, 17);
            this.RadioIn.TabIndex = 95;
            this.RadioIn.TabStop = true;
            this.RadioIn.Text = "IN";
            this.RadioIn.UseVisualStyleBackColor = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl1.Location = new System.Drawing.Point(317, 46);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 13);
            this.labelControl1.TabIndex = 91;
            this.labelControl1.Text = "From";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl2.Location = new System.Drawing.Point(492, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(14, 13);
            this.labelControl2.TabIndex = 92;
            this.labelControl2.Text = "To";
            // 
            // dateFrom
            // 
            this.dateFrom.CustomFormat = "dd/MMM/yyyy";
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFrom.Location = new System.Drawing.Point(355, 41);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(126, 21);
            this.dateFrom.TabIndex = 90;
            // 
            // dateTo
            // 
            this.dateTo.CustomFormat = "dd/MMM/yyyy";
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTo.Location = new System.Drawing.Point(527, 41);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(126, 21);
            this.dateTo.TabIndex = 93;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeight = 22;
            this.dataGridView1.Location = new System.Drawing.Point(23, 128);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 18;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.Size = new System.Drawing.Size(948, 347);
            this.dataGridView1.TabIndex = 89;
            // 
            // cmbClient
            // 
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(204, 87);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(337, 21);
            this.cmbClient.TabIndex = 14;
            // 
            // MLOSummaryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 548);
            this.Controls.Add(this.groupControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MLOSummaryReport";
            this.Text = "Import : MLO Stock Summary Report";
            this.Load += new System.EventHandler(this.MLOSummaryReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalTues.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.RadioButton radioStock;
        private System.Windows.Forms.RadioButton radioOut;
        private System.Windows.Forms.RadioButton RadioIn;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtTotalBox;
        private DevExpress.XtraEditors.TextEdit txtTotalTues;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private System.Windows.Forms.ComboBox cmbConType;
        private System.Windows.Forms.ComboBox cmbContSize;
        private System.Windows.Forms.ComboBox cmbSearch;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.ProgressBar progressBar1;
    }

    #endregion
}
