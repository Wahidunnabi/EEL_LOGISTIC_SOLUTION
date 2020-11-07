namespace LOGISTIC.UI.Billing
{
    partial class BillSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillSummary));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TrailerNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rdoMLO = new DevExpress.XtraEditors.GroupControl();
            this.grdShortSummery = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.chkRef = new System.Windows.Forms.CheckBox();
            this.chkMLO = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCSDClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnCSDCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnCSDDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSummaryEdit = new DevExpress.XtraEditors.SimpleButton();
            this.ddlMLO = new System.Windows.Forms.ComboBox();
            this.txtRefNo = new DevExpress.XtraEditors.TextEdit();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.rdoMLO)).BeginInit();
            this.rdoMLO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdShortSummery)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRefNo.Properties)).BeginInit();
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
            // rdoMLO
            // 
            this.rdoMLO.Appearance.BackColor = System.Drawing.Color.Purple;
            this.rdoMLO.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.rdoMLO.Appearance.Options.UseBackColor = true;
            this.rdoMLO.Appearance.Options.UseFont = true;
            this.rdoMLO.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.rdoMLO.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.rdoMLO.AppearanceCaption.Options.UseFont = true;
            this.rdoMLO.AppearanceCaption.Options.UseForeColor = true;
            this.rdoMLO.AutoSize = true;
            this.rdoMLO.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.rdoMLO.Controls.Add(this.grdShortSummery);
            this.rdoMLO.Controls.Add(this.label1);
            this.rdoMLO.Controls.Add(this.btnSearch);
            this.rdoMLO.Controls.Add(this.chkDate);
            this.rdoMLO.Controls.Add(this.dateFrom);
            this.rdoMLO.Controls.Add(this.dateTo);
            this.rdoMLO.Controls.Add(this.chkRef);
            this.rdoMLO.Controls.Add(this.chkMLO);
            this.rdoMLO.Controls.Add(this.panel1);
            this.rdoMLO.Controls.Add(this.ddlMLO);
            this.rdoMLO.Controls.Add(this.txtRefNo);
            this.rdoMLO.Controls.Add(this.dataGridView1);
            this.rdoMLO.Location = new System.Drawing.Point(4, 4);
            this.rdoMLO.Name = "rdoMLO";
            this.rdoMLO.Size = new System.Drawing.Size(1010, 541);
            this.rdoMLO.TabIndex = 12;
            // 
            // grdShortSummery
            // 
            this.grdShortSummery.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdShortSummery.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdShortSummery.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdShortSummery.ColumnHeadersHeight = 20;
            this.grdShortSummery.Location = new System.Drawing.Point(725, 371);
            this.grdShortSummery.Name = "grdShortSummery";
            this.grdShortSummery.RowTemplate.Height = 18;
            this.grdShortSummery.Size = new System.Drawing.Size(244, 105);
            this.grdShortSummery.TabIndex = 190;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(275, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 189;
            this.label1.Text = "To";
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.BackColor = System.Drawing.Color.LightBlue;
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnSearch.Appearance.Options.UseBackColor = true;
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.Appearance.Options.UseForeColor = true;
            this.btnSearch.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnSearch.Location = new System.Drawing.Point(447, 56);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 24;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkDate
            // 
            this.chkDate.AutoSize = true;
            this.chkDate.BackColor = System.Drawing.Color.Transparent;
            this.chkDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDate.Location = new System.Drawing.Point(46, 62);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(95, 17);
            this.chkDate.TabIndex = 188;
            this.chkDate.Text = "Date Range.";
            this.chkDate.UseVisualStyleBackColor = false;
            // 
            // dateFrom
            // 
            this.dateFrom.CustomFormat = "dd-MMM-yyyy";
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFrom.Location = new System.Drawing.Point(147, 58);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(122, 21);
            this.dateFrom.TabIndex = 187;
            // 
            // dateTo
            // 
            this.dateTo.CustomFormat = "dd-MMM-yyyy";
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTo.Location = new System.Drawing.Point(312, 58);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(112, 21);
            this.dateTo.TabIndex = 186;
            // 
            // chkRef
            // 
            this.chkRef.AutoSize = true;
            this.chkRef.BackColor = System.Drawing.Color.Transparent;
            this.chkRef.Enabled = false;
            this.chkRef.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRef.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkRef.Location = new System.Drawing.Point(344, 31);
            this.chkRef.Name = "chkRef";
            this.chkRef.Size = new System.Drawing.Size(65, 17);
            this.chkRef.TabIndex = 184;
            this.chkRef.Text = "Ref No.";
            this.chkRef.UseVisualStyleBackColor = false;
            // 
            // chkMLO
            // 
            this.chkMLO.AutoSize = true;
            this.chkMLO.BackColor = System.Drawing.Color.Transparent;
            this.chkMLO.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMLO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkMLO.Location = new System.Drawing.Point(46, 31);
            this.chkMLO.Name = "chkMLO";
            this.chkMLO.Size = new System.Drawing.Size(50, 17);
            this.chkMLO.TabIndex = 183;
            this.chkMLO.Text = "MLO";
            this.chkMLO.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSalmon;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnCSDClose);
            this.panel1.Controls.Add(this.btnCSDCancel);
            this.panel1.Controls.Add(this.btnCSDDelete);
            this.panel1.Controls.Add(this.btnSummaryEdit);
            this.panel1.Location = new System.Drawing.Point(370, 490);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 40);
            this.panel1.TabIndex = 16;
            // 
            // btnCSDClose
            // 
            this.btnCSDClose.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCSDClose.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCSDClose.Appearance.Options.UseFont = true;
            this.btnCSDClose.Appearance.Options.UseForeColor = true;
            this.btnCSDClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnCSDClose.Image = ((System.Drawing.Image)(resources.GetObject("btnCSDClose.Image")));
            this.btnCSDClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnCSDClose.Location = new System.Drawing.Point(257, 8);
            this.btnCSDClose.Name = "btnCSDClose";
            this.btnCSDClose.Size = new System.Drawing.Size(75, 23);
            this.btnCSDClose.TabIndex = 23;
            this.btnCSDClose.Text = "Close";
            this.btnCSDClose.Click += new System.EventHandler(this.btnCSDClose_Click);
            // 
            // btnCSDCancel
            // 
            this.btnCSDCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCSDCancel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCSDCancel.Appearance.Options.UseFont = true;
            this.btnCSDCancel.Appearance.Options.UseForeColor = true;
            this.btnCSDCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnCSDCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCSDCancel.Image")));
            this.btnCSDCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnCSDCancel.Location = new System.Drawing.Point(176, 8);
            this.btnCSDCancel.Name = "btnCSDCancel";
            this.btnCSDCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCSDCancel.TabIndex = 22;
            this.btnCSDCancel.Text = " Cancel";
            this.btnCSDCancel.Click += new System.EventHandler(this.btnCSDCancel_Click);
            // 
            // btnCSDDelete
            // 
            this.btnCSDDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCSDDelete.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCSDDelete.Appearance.Options.UseFont = true;
            this.btnCSDDelete.Appearance.Options.UseForeColor = true;
            this.btnCSDDelete.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnCSDDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnCSDDelete.Image")));
            this.btnCSDDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnCSDDelete.Location = new System.Drawing.Point(93, 8);
            this.btnCSDDelete.Name = "btnCSDDelete";
            this.btnCSDDelete.Size = new System.Drawing.Size(75, 23);
            this.btnCSDDelete.TabIndex = 21;
            this.btnCSDDelete.Text = "Delete";
            // 
            // btnSummaryEdit
            // 
            this.btnSummaryEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSummaryEdit.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnSummaryEdit.Appearance.Options.UseFont = true;
            this.btnSummaryEdit.Appearance.Options.UseForeColor = true;
            this.btnSummaryEdit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnSummaryEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnSummaryEdit.Image")));
            this.btnSummaryEdit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnSummaryEdit.Location = new System.Drawing.Point(10, 8);
            this.btnSummaryEdit.Name = "btnSummaryEdit";
            this.btnSummaryEdit.Size = new System.Drawing.Size(75, 23);
            this.btnSummaryEdit.TabIndex = 20;
            this.btnSummaryEdit.Text = "Edit";
            this.btnSummaryEdit.Click += new System.EventHandler(this.btnSummaryEdit_Click);
            // 
            // ddlMLO
            // 
            this.ddlMLO.FormattingEnabled = true;
            this.ddlMLO.Location = new System.Drawing.Point(147, 27);
            this.ddlMLO.Name = "ddlMLO";
            this.ddlMLO.Size = new System.Drawing.Size(168, 21);
            this.ddlMLO.TabIndex = 12;
            // 
            // txtRefNo
            // 
            this.txtRefNo.Enabled = false;
            this.txtRefNo.Location = new System.Drawing.Point(445, 25);
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtRefNo.Size = new System.Drawing.Size(168, 22);
            this.txtRefNo.TabIndex = 8;
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
            this.dataGridView1.Location = new System.Drawing.Point(40, 110);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 18;
            this.dataGridView1.Size = new System.Drawing.Size(929, 255);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            // 
            // BillSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 548);
            this.Controls.Add(this.rdoMLO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BillSummary";
            this.Text = "BILLING : CSD Bill Summary";
            this.Load += new System.EventHandler(this.BillSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rdoMLO)).EndInit();
            this.rdoMLO.ResumeLayout(false);
            this.rdoMLO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdShortSummery)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRefNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn TrailerNumber;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraEditors.GroupControl rdoMLO;
        private System.Windows.Forms.ComboBox ddlMLO;
        private DevExpress.XtraEditors.TextEdit txtRefNo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnCSDClose;
        private DevExpress.XtraEditors.SimpleButton btnCSDCancel;
        private DevExpress.XtraEditors.SimpleButton btnCSDDelete;
        private DevExpress.XtraEditors.SimpleButton btnSummaryEdit;
        private System.Windows.Forms.CheckBox chkRef;
        private System.Windows.Forms.CheckBox chkMLO;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdShortSummery;
    }

    #endregion
}
