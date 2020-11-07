namespace LOGISTIC.UI.Report
{
    partial class ConsigneeDailyStuffing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsigneeDailyStuffing));
            this.TrailerNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.ddlForwarder = new System.Windows.Forms.ComboBox();
            this.ddlShipper = new System.Windows.Forms.ComboBox();
            this.txtEFRNO = new DevExpress.XtraEditors.TextEdit();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.cmbConType = new System.Windows.Forms.ComboBox();
            this.cmbSearch = new System.Windows.Forms.ComboBox();
            this.cmbContSize = new System.Windows.Forms.ComboBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAllexport = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.lblContNo = new DevExpress.XtraEditors.LabelControl();
            this.ddlConsignee = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEFRNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.groupControl3.Controls.Add(this.ddlForwarder);
            this.groupControl3.Controls.Add(this.ddlShipper);
            this.groupControl3.Controls.Add(this.txtEFRNO);
            this.groupControl3.Controls.Add(this.txtSearch);
            this.groupControl3.Controls.Add(this.cmbConType);
            this.groupControl3.Controls.Add(this.cmbSearch);
            this.groupControl3.Controls.Add(this.cmbContSize);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.dataGridView2);
            this.groupControl3.Controls.Add(this.panel1);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.dateTo);
            this.groupControl3.Controls.Add(this.btnLoad);
            this.groupControl3.Controls.Add(this.dataGridView1);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.dateFrom);
            this.groupControl3.Controls.Add(this.lblContNo);
            this.groupControl3.Controls.Add(this.ddlConsignee);
            this.groupControl3.Location = new System.Drawing.Point(6, 6);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1005, 536);
            this.groupControl3.TabIndex = 12;
            // 
            // ddlForwarder
            // 
            this.ddlForwarder.FormattingEnabled = true;
            this.ddlForwarder.Location = new System.Drawing.Point(230, 29);
            this.ddlForwarder.Name = "ddlForwarder";
            this.ddlForwarder.Size = new System.Drawing.Size(324, 21);
            this.ddlForwarder.TabIndex = 138;
            // 
            // ddlShipper
            // 
            this.ddlShipper.FormattingEnabled = true;
            this.ddlShipper.Location = new System.Drawing.Point(229, 28);
            this.ddlShipper.Name = "ddlShipper";
            this.ddlShipper.Size = new System.Drawing.Size(324, 21);
            this.ddlShipper.TabIndex = 137;
            // 
            // txtEFRNO
            // 
            this.txtEFRNO.Location = new System.Drawing.Point(230, 30);
            this.txtEFRNO.Name = "txtEFRNO";
            this.txtEFRNO.Size = new System.Drawing.Size(324, 20);
            this.txtEFRNO.TabIndex = 136;
            this.txtEFRNO.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(231, 30);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(324, 20);
            this.txtSearch.TabIndex = 135;
            this.txtSearch.Visible = false;
            // 
            // cmbConType
            // 
            this.cmbConType.FormattingEnabled = true;
            this.cmbConType.Location = new System.Drawing.Point(230, 30);
            this.cmbConType.Name = "cmbConType";
            this.cmbConType.Size = new System.Drawing.Size(127, 21);
            this.cmbConType.TabIndex = 134;
            this.cmbConType.Visible = false;
            // 
            // cmbSearch
            // 
            this.cmbSearch.FormattingEnabled = true;
            this.cmbSearch.Location = new System.Drawing.Point(87, 28);
            this.cmbSearch.Name = "cmbSearch";
            this.cmbSearch.Size = new System.Drawing.Size(140, 21);
            this.cmbSearch.TabIndex = 133;
            this.cmbSearch.SelectedIndexChanged += new System.EventHandler(this.cmbSearch_SelectedIndexChanged);
            // 
            // cmbContSize
            // 
            this.cmbContSize.FormattingEnabled = true;
            this.cmbContSize.Location = new System.Drawing.Point(393, 30);
            this.cmbContSize.Name = "cmbContSize";
            this.cmbContSize.Size = new System.Drawing.Size(160, 21);
            this.cmbContSize.TabIndex = 132;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl3.Location = new System.Drawing.Point(606, 410);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 13);
            this.labelControl3.TabIndex = 95;
            this.labelControl3.Text = "MLO Name";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(671, 405);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(315, 125);
            this.dataGridView2.TabIndex = 94;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnAllexport);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Location = new System.Drawing.Point(178, 472);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 35);
            this.panel1.TabIndex = 93;
            // 
            // btnAllexport
            // 
            this.btnAllexport.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAllexport.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAllexport.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnAllexport.Appearance.Options.UseBackColor = true;
            this.btnAllexport.Appearance.Options.UseFont = true;
            this.btnAllexport.Appearance.Options.UseForeColor = true;
            this.btnAllexport.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnAllexport.Image = ((System.Drawing.Image)(resources.GetObject("btnAllexport.Image")));
            this.btnAllexport.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnAllexport.Location = new System.Drawing.Point(283, 5);
            this.btnAllexport.Name = "btnAllexport";
            this.btnAllexport.Size = new System.Drawing.Size(85, 23);
            this.btnAllexport.TabIndex = 20;
            this.btnAllexport.Text = "All Export";
            this.btnAllexport.Click += new System.EventHandler(this.btnAllexport_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(192, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(107, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = " Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExcel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Appearance.Options.UseForeColor = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnExcel.Location = new System.Drawing.Point(9, 5);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(85, 23);
            this.btnExcel.TabIndex = 13;
            this.btnExcel.Text = "Export";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl2.Location = new System.Drawing.Point(733, 31);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(14, 13);
            this.labelControl2.TabIndex = 92;
            this.labelControl2.Text = "To";
            // 
            // dateTo
            // 
            this.dateTo.CustomFormat = "dd/MMM/yyyy";
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTo.Location = new System.Drawing.Point(753, 28);
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
            this.btnLoad.Location = new System.Drawing.Point(898, 25);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(85, 23);
            this.btnLoad.TabIndex = 90;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeight = 20;
            this.dataGridView1.Location = new System.Drawing.Point(15, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(971, 343);
            this.dataGridView1.TabIndex = 89;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl1.Location = new System.Drawing.Point(568, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 13);
            this.labelControl1.TabIndex = 85;
            this.labelControl1.Text = "From";
            // 
            // dateFrom
            // 
            this.dateFrom.CustomFormat = "dd/MMM/yyyy";
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFrom.Location = new System.Drawing.Point(603, 28);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(124, 21);
            this.dateFrom.TabIndex = 84;
            // 
            // lblContNo
            // 
            this.lblContNo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblContNo.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblContNo.Location = new System.Drawing.Point(15, 32);
            this.lblContNo.Name = "lblContNo";
            this.lblContNo.Size = new System.Drawing.Size(70, 13);
            this.lblContNo.TabIndex = 83;
            this.lblContNo.Text = "Search Type";
            // 
            // ddlConsignee
            // 
            this.ddlConsignee.FormattingEnabled = true;
            this.ddlConsignee.Location = new System.Drawing.Point(230, 29);
            this.ddlConsignee.Name = "ddlConsignee";
            this.ddlConsignee.Size = new System.Drawing.Size(324, 21);
            this.ddlConsignee.TabIndex = 14;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(51, 478);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 139;
            // 
            // ConsigneeDailyStuffing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 548);
            this.Controls.Add(this.groupControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ConsigneeDailyStuffing";
            this.Text = "EXPORT : Consignee Daily Stuffing";
            this.Load += new System.EventHandler(this.ImportMLODailyReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEFRNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox ddlConsignee;
        private DevExpress.XtraEditors.LabelControl lblContNo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.DataGridView dataGridView2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnAllexport;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private System.Windows.Forms.ComboBox cmbConType;
        private System.Windows.Forms.ComboBox cmbSearch;
        private System.Windows.Forms.ComboBox cmbContSize;
        private System.Windows.Forms.ComboBox ddlShipper;
        private DevExpress.XtraEditors.TextEdit txtEFRNO;
        private System.Windows.Forms.ComboBox ddlForwarder;
        private System.Windows.Forms.ProgressBar progressBar1;
    }

    #endregion
}
