namespace LOGISTIC.UI.Billing
{
    partial class BillDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillDetails));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TrailerNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtaddress = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnexporttop = new DevExpress.XtraEditors.SimpleButton();
            this.lbltotalRecord = new DevExpress.XtraEditors.LabelControl();
            this.lblTotal = new DevExpress.XtraEditors.LabelControl();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.ddlCusCode = new System.Windows.Forms.ComboBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnExporttoExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cmbSearch = new System.Windows.Forms.ComboBox();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
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
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.txtaddress);
            this.groupControl3.Controls.Add(this.txtName);
            this.groupControl3.Controls.Add(this.btnexporttop);
            this.groupControl3.Controls.Add(this.lbltotalRecord);
            this.groupControl3.Controls.Add(this.lblTotal);
            this.groupControl3.Controls.Add(this.progressBar1);
            this.groupControl3.Controls.Add(this.btnLoad);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.dateFrom);
            this.groupControl3.Controls.Add(this.dateTo);
            this.groupControl3.Controls.Add(this.ddlCusCode);
            this.groupControl3.Controls.Add(this.labelControl5);
            this.groupControl3.Controls.Add(this.btnExporttoExcel);
            this.groupControl3.Controls.Add(this.btnSearch);
            this.groupControl3.Controls.Add(this.cmbSearch);
            this.groupControl3.Controls.Add(this.txtSearch);
            this.groupControl3.Controls.Add(this.dataGridView1);
            this.groupControl3.Location = new System.Drawing.Point(6, 6);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1005, 536);
            this.groupControl3.TabIndex = 12;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl4.Location = new System.Drawing.Point(285, 59);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(46, 13);
            this.labelControl4.TabIndex = 200;
            this.labelControl4.Text = "Address";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl3.Location = new System.Drawing.Point(59, 61);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(32, 13);
            this.labelControl3.TabIndex = 199;
            this.labelControl3.Text = "Name";
            // 
            // txtaddress
            // 
            this.txtaddress.Location = new System.Drawing.Point(346, 57);
            this.txtaddress.Name = "txtaddress";
            this.txtaddress.Size = new System.Drawing.Size(288, 21);
            this.txtaddress.TabIndex = 198;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(103, 58);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(173, 21);
            this.txtName.TabIndex = 197;
            // 
            // btnexporttop
            // 
            this.btnexporttop.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnexporttop.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnexporttop.Appearance.Options.UseFont = true;
            this.btnexporttop.Appearance.Options.UseForeColor = true;
            this.btnexporttop.Image = ((System.Drawing.Image)(resources.GetObject("btnexporttop.Image")));
            this.btnexporttop.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnexporttop.Location = new System.Drawing.Point(837, 29);
            this.btnexporttop.Name = "btnexporttop";
            this.btnexporttop.Size = new System.Drawing.Size(127, 23);
            this.btnexporttop.TabIndex = 196;
            this.btnexporttop.Text = "Export with Top";
            this.btnexporttop.Click += new System.EventHandler(this.btnexporttop_Click);
            // 
            // lbltotalRecord
            // 
            this.lbltotalRecord.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbltotalRecord.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lbltotalRecord.Location = new System.Drawing.Point(785, 500);
            this.lbltotalRecord.Name = "lbltotalRecord";
            this.lbltotalRecord.Size = new System.Drawing.Size(0, 13);
            this.lbltotalRecord.TabIndex = 195;
            // 
            // lblTotal
            // 
            this.lblTotal.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblTotal.Location = new System.Drawing.Point(602, 500);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(72, 13);
            this.lblTotal.TabIndex = 194;
            this.lblTotal.Text = "Total Record";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(837, 59);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(127, 23);
            this.progressBar1.TabIndex = 193;
            // 
            // btnLoad
            // 
            this.btnLoad.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLoad.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnLoad.Appearance.Options.UseFont = true;
            this.btnLoad.Appearance.Options.UseForeColor = true;
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnLoad.Location = new System.Drawing.Point(649, 30);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(85, 23);
            this.btnLoad.TabIndex = 192;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl2.Location = new System.Drawing.Point(471, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 191;
            this.labelControl2.Text = "To Date";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl1.Location = new System.Drawing.Point(282, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 13);
            this.labelControl1.TabIndex = 190;
            this.labelControl1.Text = "From Date";
            // 
            // dateFrom
            // 
            this.dateFrom.CustomFormat = "dd-MM-yyyy";
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFrom.Location = new System.Drawing.Point(346, 31);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(111, 21);
            this.dateFrom.TabIndex = 189;
            // 
            // dateTo
            // 
            this.dateTo.CustomFormat = "dd-MM-yyyy";
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTo.Location = new System.Drawing.Point(534, 30);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(106, 21);
            this.dateTo.TabIndex = 188;
            // 
            // ddlCusCode
            // 
            this.ddlCusCode.FormattingEnabled = true;
            this.ddlCusCode.Location = new System.Drawing.Point(103, 31);
            this.ddlCusCode.Name = "ddlCusCode";
            this.ddlCusCode.Size = new System.Drawing.Size(173, 21);
            this.ddlCusCode.TabIndex = 16;
            this.ddlCusCode.SelectedIndexChanged += new System.EventHandler(this.ddlCusCode_SelectedIndexChanged);
            this.ddlCusCode.SelectionChangeCommitted += new System.EventHandler(this.ddlCusCode_SelectionChangeCommitted);
            this.ddlCusCode.SelectedValueChanged += new System.EventHandler(this.ddlCusCode_SelectedValueChanged);
            this.ddlCusCode.SizeChanged += new System.EventHandler(this.ddlCusCode_SizeChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl5.Location = new System.Drawing.Point(38, 35);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 13);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "MLO Code";
            // 
            // btnExporttoExcel
            // 
            this.btnExporttoExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExporttoExcel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnExporttoExcel.Appearance.Options.UseFont = true;
            this.btnExporttoExcel.Appearance.Options.UseForeColor = true;
            this.btnExporttoExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExporttoExcel.Image")));
            this.btnExporttoExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnExporttoExcel.Location = new System.Drawing.Point(746, 29);
            this.btnExporttoExcel.Name = "btnExporttoExcel";
            this.btnExporttoExcel.Size = new System.Drawing.Size(85, 23);
            this.btnExporttoExcel.TabIndex = 15;
            this.btnExporttoExcel.Text = "Export";
            this.btnExporttoExcel.Click += new System.EventHandler(this.btnExporttoExcel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.Appearance.Options.UseForeColor = true;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnSearch.Location = new System.Drawing.Point(538, 99);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 23);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbSearch
            // 
            this.cmbSearch.FormattingEnabled = true;
            this.cmbSearch.Location = new System.Drawing.Point(106, 99);
            this.cmbSearch.Name = "cmbSearch";
            this.cmbSearch.Size = new System.Drawing.Size(134, 21);
            this.cmbSearch.TabIndex = 12;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(246, 100);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtSearch.Size = new System.Drawing.Size(278, 22);
            this.txtSearch.TabIndex = 8;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 20;
            this.dataGridView1.Location = new System.Drawing.Point(38, 209);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 18;
            this.dataGridView1.Size = new System.Drawing.Size(956, 258);
            this.dataGridView1.TabIndex = 7;
            // 
            // BillDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 548);
            this.Controls.Add(this.groupControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BillDetails";
            this.Text = "Billing : CSD Bill Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BillDetails_FormClosing);
            this.Load += new System.EventHandler(this.BillDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn TrailerNumber;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.ComboBox cmbSearch;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevExpress.XtraEditors.SimpleButton btnExporttoExcel;
        private System.Windows.Forms.ComboBox ddlCusCode;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private System.Windows.Forms.ProgressBar progressBar1;
        private DevExpress.XtraEditors.LabelControl lbltotalRecord;
        private DevExpress.XtraEditors.LabelControl lblTotal;
        private DevExpress.XtraEditors.SimpleButton btnexporttop;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtaddress;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }

    #endregion
}
