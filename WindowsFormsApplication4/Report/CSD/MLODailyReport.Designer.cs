namespace LOGISTIC.UI.Report
{
    partial class MLODailyReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MLODailyReport));
            this.TrailerNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.cmbConType = new System.Windows.Forms.ComboBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cmbContSize = new System.Windows.Forms.ComboBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTotalBox = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalTues = new DevExpress.XtraEditors.TextEdit();
            this.lblCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.Preview = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.radioStock = new System.Windows.Forms.RadioButton();
            this.radioOut = new System.Windows.Forms.RadioButton();
            this.RadioIn = new System.Windows.Forms.RadioButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.ddlClient = new System.Windows.Forms.ComboBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalTues.Properties)).BeginInit();
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
            this.groupControl3.Controls.Add(this.dataGridView2);
            this.groupControl3.Controls.Add(this.cmbConType);
            this.groupControl3.Controls.Add(this.labelControl5);
            this.groupControl3.Controls.Add(this.cmbContSize);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.txtTotalBox);
            this.groupControl3.Controls.Add(this.txtTotalTues);
            this.groupControl3.Controls.Add(this.lblCustomerName);
            this.groupControl3.Controls.Add(this.panel1);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.dtpTo);
            this.groupControl3.Controls.Add(this.dataGridView1);
            this.groupControl3.Controls.Add(this.radioStock);
            this.groupControl3.Controls.Add(this.radioOut);
            this.groupControl3.Controls.Add(this.RadioIn);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.dtpFrom);
            this.groupControl3.Controls.Add(this.ddlClient);
            this.groupControl3.Controls.Add(this.btnExcel);
            this.groupControl3.Location = new System.Drawing.Point(6, 6);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1005, 536);
            this.groupControl3.TabIndex = 12;
            this.groupControl3.Text = "MLO Daily Report";
            // 
            // dataGridView2
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(59, 132);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(879, 348);
            this.dataGridView2.TabIndex = 106;
            // 
            // cmbConType
            // 
            this.cmbConType.FormattingEnabled = true;
            this.cmbConType.Location = new System.Drawing.Point(693, 46);
            this.cmbConType.Name = "cmbConType";
            this.cmbConType.Size = new System.Drawing.Size(76, 21);
            this.cmbConType.TabIndex = 105;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl5.Location = new System.Drawing.Point(67, 51);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 13);
            this.labelControl5.TabIndex = 104;
            this.labelControl5.Text = "MLO Code";
            // 
            // cmbContSize
            // 
            this.cmbContSize.FormattingEnabled = true;
            this.cmbContSize.Location = new System.Drawing.Point(600, 47);
            this.cmbContSize.Name = "cmbContSize";
            this.cmbContSize.Size = new System.Drawing.Size(82, 21);
            this.cmbContSize.TabIndex = 103;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl4.Location = new System.Drawing.Point(617, 491);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(27, 13);
            this.labelControl4.TabIndex = 101;
            this.labelControl4.Text = "Tues";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl3.Location = new System.Drawing.Point(489, 491);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(53, 13);
            this.labelControl3.TabIndex = 100;
            this.labelControl3.Text = "Total Box";
            // 
            // txtTotalBox
            // 
            this.txtTotalBox.Location = new System.Drawing.Point(548, 488);
            this.txtTotalBox.Name = "txtTotalBox";
            this.txtTotalBox.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalBox.Properties.Appearance.Options.UseFont = true;
            this.txtTotalBox.Properties.MaxLength = 11;
            this.txtTotalBox.Size = new System.Drawing.Size(63, 20);
            this.txtTotalBox.TabIndex = 99;
            // 
            // txtTotalTues
            // 
            this.txtTotalTues.Location = new System.Drawing.Point(650, 488);
            this.txtTotalTues.Name = "txtTotalTues";
            this.txtTotalTues.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalTues.Properties.Appearance.Options.UseFont = true;
            this.txtTotalTues.Properties.MaxLength = 11;
            this.txtTotalTues.Size = new System.Drawing.Size(70, 20);
            this.txtTotalTues.TabIndex = 98;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblCustomerName.Location = new System.Drawing.Point(125, 74);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(59, 13);
            this.lblCustomerName.TabIndex = 97;
            this.lblCustomerName.Text = "MLO Name";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSlateGray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.Preview);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Location = new System.Drawing.Point(609, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 35);
            this.panel1.TabIndex = 96;
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
            this.btnClose.Location = new System.Drawing.Point(247, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Preview
            // 
            this.Preview.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Preview.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Preview.Appearance.Options.UseFont = true;
            this.Preview.Appearance.Options.UseForeColor = true;
            this.Preview.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.Preview.Image = ((System.Drawing.Image)(resources.GetObject("Preview.Image")));
            this.Preview.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.Preview.Location = new System.Drawing.Point(87, 5);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(75, 23);
            this.Preview.TabIndex = 95;
            this.Preview.Text = "Preview";
            this.Preview.Click += new System.EventHandler(this.Preview_Click);
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
            this.btnCancel.Location = new System.Drawing.Point(167, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = " Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnLoad.Location = new System.Drawing.Point(7, 5);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 90;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl2.Location = new System.Drawing.Point(456, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(14, 13);
            this.labelControl2.TabIndex = 92;
            this.labelControl2.Text = "To";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MMM/yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(472, 47);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(116, 21);
            this.dtpTo.TabIndex = 91;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(59, 132);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(879, 105);
            this.dataGridView1.TabIndex = 89;
            this.dataGridView1.Visible = false;
            // 
            // radioStock
            // 
            this.radioStock.AutoSize = true;
            this.radioStock.BackColor = System.Drawing.Color.Transparent;
            this.radioStock.Location = new System.Drawing.Point(876, 49);
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
            this.radioOut.Location = new System.Drawing.Point(822, 49);
            this.radioOut.Name = "radioOut";
            this.radioOut.Size = new System.Drawing.Size(48, 17);
            this.radioOut.TabIndex = 87;
            this.radioOut.TabStop = true;
            this.radioOut.Text = "OUT";
            this.radioOut.UseVisualStyleBackColor = false;
            // 
            // RadioIn
            // 
            this.RadioIn.AutoSize = true;
            this.RadioIn.BackColor = System.Drawing.Color.Transparent;
            this.RadioIn.Location = new System.Drawing.Point(779, 49);
            this.RadioIn.Name = "RadioIn";
            this.RadioIn.Size = new System.Drawing.Size(37, 17);
            this.RadioIn.TabIndex = 86;
            this.RadioIn.TabStop = true;
            this.RadioIn.Text = "IN";
            this.RadioIn.UseVisualStyleBackColor = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelControl1.Location = new System.Drawing.Point(275, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 13);
            this.labelControl1.TabIndex = 85;
            this.labelControl1.Text = "From";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MMM/yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(307, 48);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(124, 21);
            this.dtpFrom.TabIndex = 84;
            // 
            // ddlClient
            // 
            this.ddlClient.FormattingEnabled = true;
            this.ddlClient.Location = new System.Drawing.Point(124, 47);
            this.ddlClient.Name = "ddlClient";
            this.ddlClient.Size = new System.Drawing.Size(145, 21);
            this.ddlClient.TabIndex = 14;
            this.ddlClient.SelectionChangeCommitted += new System.EventHandler(this.cmbClient_SelectionChangeCommitted);
            this.ddlClient.SelectedValueChanged += new System.EventHandler(this.ddlClient_SelectedValueChanged);
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
            this.btnExcel.Location = new System.Drawing.Point(733, 486);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(85, 23);
            this.btnExcel.TabIndex = 13;
            this.btnExcel.Text = "Export";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(832, 487);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 134;
            // 
            // MLODailyReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 548);
            this.Controls.Add(this.groupControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MLODailyReport";
            this.Text = "CSD : 24 Hours Report";
            this.Load += new System.EventHandler(this.MLODailyReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalTues.Properties)).EndInit();
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
        private System.Windows.Forms.ComboBox ddlClient;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton radioStock;
        private System.Windows.Forms.RadioButton radioOut;
        private System.Windows.Forms.RadioButton RadioIn;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private DevExpress.XtraEditors.SimpleButton Preview;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblCustomerName;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtTotalBox;
        private DevExpress.XtraEditors.TextEdit txtTotalTues;
        private System.Windows.Forms.ComboBox cmbContSize;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.ComboBox cmbConType;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ProgressBar progressBar1;
    }

    #endregion
}
