using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using LOGISTIC.CSD.BLL;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using LOGISTIC.REPORT;

namespace LOGISTIC.UI
{
    public partial class ContainerGateEntry : Form
    {
        
        private static CSDContGateInOut objCSDInOut = new CSDContGateInOut();
        
        private ContainerTypeBll ctBll = new ContainerTypeBll();
        private ContainerSizeBll csBll = new ContainerSizeBll();
        private DepotBll depoBll = new DepotBll();
        private TrailerBll objTrailerBll = new TrailerBll();
        private TrailerNumberBll tnBll = new TrailerNumberBll();
        private CustomerBll MLOBll = new CustomerBll();      
        private StatusBLL statusBll = new StatusBLL();
        private CSDGateInOutBLL objCSDBll = new CSDGateInOutBLL();

        private CSDGateInUPComing objUpcomingCont = new CSDGateInUPComing();
        private UserInfo user;
        static int PageSize = 10;
        int CSDId;
        public ContainerGateEntry(UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;
            txtContNumbe.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtContNumbe.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";
           
            lblBookingNo.Visible = false;
            lblDumpDate.Visible = false;
            txtBookingNo.Visible = false;
            dateDump.Visible = false;

            btnChlnPrint.Visible = false;
            btnCSDDelete.Enabled = false;
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;

        }


        public ContainerGateEntry(CSDGateInUPComing objUpcomingCont, UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.objUpcomingCont = objUpcomingCont;
            this.user = user;
            txtContNumbe.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtContNumbe.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";

            lblBookingNo.Visible = false;
            lblDumpDate.Visible = false;
            txtBookingNo.Visible = false;
            dateDump.Visible = false;

            btnChlnPrint.Visible = false;
            btnCSDDelete.Enabled = false;
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;

        }

        private void ContainerGateEntry_Load(object sender, EventArgs e)
        {
            LoadcmbSearch();
            PrepareGrid();
            cmbPagerLoad();
            LoadDataToGrid(1);          
            LoadCustomer();
            LoadContSize();
            LoadConType();
            LoadDepot();
            LoadTrailerAccount();
            LoadHaular();
            LoadStatus();
            LoadCondition();
            if (objUpcomingCont.Id > 0)
            {
                BindDataToField();
                GetMLONameRefNo();
                GEtISONumber();

            }

        }


        #region Load Basic Data

        private void LoadcmbSearch()
        {

            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Container Number");
            cmbSearch.Items.Insert(3, "Ref Number");
            cmbSearch.Items.Insert(4, "Challan Number");
            cmbSearch.SelectedIndex = 0;
        }

        private void cmbPagerLoad()
        {

            cmbGridRow.Items.Insert(0, 5);
            cmbGridRow.Items.Insert(1, 10);
            cmbGridRow.Items.Insert(2, 15);           
            cmbGridRow.SelectedIndex = 1;


        }

        private void LoadCustomer()
        {

            var type = MLOBll.Getall();           
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Customer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlCusCode.DataSource = dt_Types;
                ddlCusCode.DisplayMember = "t_Name";
                ddlCusCode.ValueMember = "t_ID";
            }
            ddlCusCode.SelectedIndex = 0;

        }

        private void LoadContSize()
        {

            var type = csBll.Getall();        
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ContainerSizeId, t.ContainerSize1);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--sz--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                cmbContSize.DataSource = dt_Types;
                cmbContSize.DisplayMember = "t_Name";
                cmbContSize.ValueMember = "t_ID";
            }
            cmbContSize.SelectedIndex = 0;


        }

        private void LoadConType()
        {

            var type = ctBll.Getall();           
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ContainerTypeId, t.ContainerTypeName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--type--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                cmbConType.DataSource = dt_Types;
                cmbConType.DisplayMember = "t_Name";
                cmbConType.ValueMember = "t_ID";
            }
            cmbConType.SelectedIndex = 0;

        }

        private void LoadDepot()
        {
            var type = depoBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.DepotId, t.DepotCode);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Depot--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                cmbFrom.DataSource = dt_Types;
                cmbFrom.DisplayMember = "t_Name";
                cmbFrom.ValueMember = "t_ID";
            }
            cmbFrom.SelectedIndex = 0;
        }

        private void LoadTrailerAccount()
        {

            var type = objTrailerBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.TrailerId, t.TrailerName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Trailer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                cmbTrailer.DataSource = dt_Types;
                cmbTrailer.DisplayMember = "t_Name";
                cmbTrailer.ValueMember = "t_ID";
            }
            cmbTrailer.SelectedIndex = 0;
        }

        
        private void LoadHaular()
        {

            cmbHaulier.Items.Insert(0, "--Select Haulier--");
            cmbHaulier.Items.Insert(1, "ELL");
            cmbHaulier.Items.Insert(2, "Others");
            cmbHaulier.Items.Insert(3, "N/A");
            cmbHaulier.SelectedIndex = 0;            

        }

        private void LoadStatus()
        {
            cmbStatus.Items.Insert(0, "--Status--");
            cmbStatus.Items.Insert(1, "Empty");
            cmbStatus.Items.Insert(2, "Load");
            cmbStatus.Items.Insert(3, "Dump");
            cmbStatus.SelectedIndex = 0;

        }


        private void LoadCondition()
        {
            cmbCondition.Items.Insert(0, "--Condition--");
            cmbCondition.Items.Insert(1, "Sound");
            cmbCondition.Items.Insert(2, "Damage");
            cmbCondition.SelectedIndex = 0;

        }


        private void BindDataToField()
        {

            ddlCusCode.SelectedValue = Convert.ToInt32(objUpcomingCont.MLOID);
            txtContNumbe.Text = objUpcomingCont.ContainerNo.Trim();
            cmbContSize.SelectedValue = Convert.ToInt32(objUpcomingCont.SizeId);
            cmbConType.SelectedValue = Convert.ToInt32(objUpcomingCont.TypeId);
            txtImpVessel.Text = objUpcomingCont.ImportVasselName.Trim();
            txtRotation.Text = objUpcomingCont.RotationNumber.Trim();

        }

        #endregion


        #region Grid Pager Data

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 7;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";
            dataGridView1.Columns[0].DataPropertyName = "SL";


            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[1].HeaderText = "Container No";
            dataGridView1.Columns[1].DataPropertyName = "ContNo";

            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[2].HeaderText = "Reference No";
            dataGridView1.Columns[2].DataPropertyName = "RefNo";

            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[3].HeaderText = "Customer";
            dataGridView1.Columns[3].DataPropertyName = "CustomerName";


            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[4].HeaderText = "Type";
            dataGridView1.Columns[4].DataPropertyName = "ContainerTypeName";


            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[5].HeaderText = "Size";
            dataGridView1.Columns[5].DataPropertyName = "ContainerSize";
         
           

            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[6].DataPropertyName = "ContainerGateEntryId";

            dataGridView1.ClearSelection();

        }

        private void LoadDataToGrid(int pageIndex)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GetCSDGateInData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = dt;
                    dataGridView1.ScrollBars = ScrollBars.None;
                    dataGridView1.AllowUserToAddRows = false;                  
                    con.Close();
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    if (recordCount > PageSize)
                    {
                        PopulatePager(recordCount, pageIndex);
                    }
                    
                    
                }
            }
        }
       
        public class Page
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
        }

        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            this.LoadDataToGrid(int.Parse(btnPager.Name));
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan =5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString(), });
            }

            //Clear existing Pager Buttons.
            pnlPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            foreach (Page page in pages)
            {
                Button btnPage = new Button();
               
                btnPage.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
                btnPage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                btnPage.Margin = new System.Windows.Forms.Padding(5);
                if (page.Text == "First" || page.Text == "Last")
                {
                    if (page.Text == "First")
                    {
                        btnPage.Location = new System.Drawing.Point(38 * count, 5);
                        btnPage.Size = new System.Drawing.Size(50, 22);
                        btnPage.BackColor = Color.Beige;
                    }

                    if (page.Text == "Last")
                    {
                        btnPage.Location = new System.Drawing.Point((38 * count) + 15, 5);
                        btnPage.Size = new System.Drawing.Size(50, 22);
                        btnPage.BackColor = Color.Beige;
                    }
                    
                }
                else
                {
                    btnPage.Location = new System.Drawing.Point((38 * count)+15, 5);
                    btnPage.Size = new System.Drawing.Size(35, 22);
                }

                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                pnlPager.Controls.Add(btnPage);
                count++;
            }
        }

        #endregion


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {           

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

             CSDId = Convert.ToInt32(selectedRow.Cells[6].Value);
            objCSDInOut = GetCSDInOutById(CSDId);
            if (objCSDInOut != null)
            {
                BindDataToField(objCSDInOut);

            }
           
        }   
               
        private void btnCSDSave_Click(object sender, EventArgs e)
        {
            bool flag = ValidateCSD();
            if (flag == true)
            {
                FillingData();               
                SaveData();
                LoadDataToGrid(1);               
                                
            }

        }

        private void btnCSDDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                             "Confirm CSD deletion",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objCSDInOut.ContainerGateEntryId > 0)
                {


                    //objCSDInOut.CustId = null;
                    objCSDInOut.RefNo = null;
                    objCSDInOut.ContNo = null;
                    objCSDInOut.InPermission = null;
                    objCSDInOut.DateIn = null ;
                    objCSDInOut.ContSize = null;
                    objCSDInOut.ContType = null;
                    objCSDInOut.ISO = null;
                    objCSDInOut.DepotFrom = null;
                    objCSDInOut.ChallanNo = null ;
                    objCSDInOut.TrailerIn = null;
                    objCSDInOut.TrailerInNo = null;
                    objCSDInOut.ImpVssl = null;
                    objCSDInOut.RotImp = null;
                    objCSDInOut.HaulierIn = null;
                    objCSDInOut.ContInCondition = null;

                    objCSDInOut.LoadEmptyStatus = null;

                        objCSDInOut.DumpingDate = null;
                        objCSDInOut.BookingNoDump = null;

                  


                    objCSDInOut.UserIdGateIn = null;
                    objCSDInOut.RemarkIn = null;







                    var status = objCSDBll.Update(objCSDInOut);



                    //var status = objCSDBll.Delete(objCSDInOut.ContainerGateEntryId);
                    //MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //LoadDataToGrid(1);
                    //ClearForm();
                }

            }
        }

        private void btnCSDCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
           
        }

        private void btnCSDClose_Click(object sender, EventArgs e)
        {
            //ClearForm();
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            int searchBy = cmbSearch.SelectedIndex;
            string searchText = txtSearch.Text.Trim();

            if (searchBy == 0)
            {
                MessageBox.Show("Please select search type !!", "Selection Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (searchBy > 1 && searchText == "")
            {
                MessageBox.Show("Search text can't be empty !!", "Input Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            switch (searchBy)
            {
                case 1:
                    {
                        LoadDataToGrid(1);
                        break;
                    }
                case 2:
                    {
                       
                        List<SerachCSDGateInOutData_Result> listCSD = objCSDBll.SearchCSDGateInOutData(searchBy, searchText);
                        BindSearchDatatoGrid(listCSD);
                        break;
                    }
                case 3:
                    {
                        List<SerachCSDGateInOutData_Result> listCSD = objCSDBll.SearchCSDGateInOutData(searchBy, searchText);
                        BindSearchDatatoGrid(listCSD);
                        break;
                    }

                case 4:
                    {
                        List<SerachCSDGateInOutData_Result> listCSD = objCSDBll.SearchCSDGateInOutData(searchBy, searchText);
                        BindSearchDatatoGrid(listCSD);
                        break;
                    }
                default:
                    {
                        break;
                        
                    }
            }
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            labelControl1.Focus();
        }

        private void btnChlnPrint_Click(object sender, EventArgs e)
        {
            ReportDocument cryRpt = new ReportDocument();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;
           
         
  

            crConnectionInfo.ServerName = LOGISTIC.UI.Properties.Settings.Default.DBSERVER;
            crConnectionInfo.DatabaseName = LOGISTIC.UI.Properties.Settings.Default.DATABASE;
            crConnectionInfo.UserID = LOGISTIC.UI.Properties.Settings.Default.USERID;
            crConnectionInfo.Password = LOGISTIC.UI.Properties.Settings.Default.PASSWORD;

            cryRpt.Load(@"D:\Report\" + "GateInChallan.rpt");          
            cryRpt.SetParameterValue("@CSDID", Convert.ToInt64(CSDId));         
            CrTables = cryRpt.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            Viewer view = new Viewer();
            view.ReportView.ReportSource = cryRpt;
            view.ReportView.Refresh();
            view.Show();

        }

        private bool ValidateCSD()
        {
            var errMessage = "";

            if (Convert.ToInt32(ddlCusCode.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select Customer !!\n";
                ddlCusCode.Focus();                            
            }           
            if (txtContNumbe.Text.Trim() == "")
            {
                errMessage = errMessage + "* Container number can't be null !!\n";
                txtContNumbe.Focus();
                                             
            }
            if (Convert.ToInt32(cmbContSize.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select container size !!\n";
                cmbContSize.Focus();
                                         
            }
            if (Convert.ToInt32(cmbConType.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select container type !!\n";
                cmbConType.Focus();
                             
            }
          
            if (txtTrailerInNo.Text.Trim() == "")
            {
                errMessage = errMessage + "* Please enter trailer number !!\n";
                txtTrailerInNo.Focus();
               
                
            }
            if (Convert.ToInt32(cmbStatus.SelectedIndex) == 0)
            {
                errMessage = errMessage + "* Please select container status !!\n";
                cmbStatus.Focus();


            }
            if (Convert.ToInt32(cmbFrom.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select from location !!\n";
                cmbFrom.Focus();
                             
            }           
            if (errMessage != "")
            {
                MessageBox.Show(errMessage, "Input required",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            else
            {
                return true;
            }
           
        }

        private void FillingData()
        {
            try
            {
                objCSDInOut.CustId = Convert.ToInt32(ddlCusCode.SelectedValue);
                objCSDInOut.RefNo = Convert.ToInt64(txtReferenceNo.Text.Trim());
                objCSDInOut.ContNo = txtContNumbe.Text.Trim();
                objCSDInOut.InPermission = chkPermission.Checked;
                objCSDInOut.DateIn = Convert.ToDateTime(dateIn.Text);                
                objCSDInOut.ContSize = Convert.ToInt32(cmbContSize.SelectedValue);
                objCSDInOut.ContType = Convert.ToInt32(cmbConType.SelectedValue);
                objCSDInOut.ISO = txtISO.Text.Trim();
                objCSDInOut.DepotFrom = Convert.ToInt32(cmbFrom.SelectedValue);
                objCSDInOut.ChallanNo = txtChallaNo.Text.Trim();
                objCSDInOut.TrailerIn = Convert.ToInt32(cmbTrailer.SelectedValue);
                objCSDInOut.TrailerInNo = txtTrailerInNo.Text.Trim();
                objCSDInOut.ImpVssl = txtImpVessel.Text.Trim();
                objCSDInOut.RotImp = txtRotation.Text.Trim();
                objCSDInOut.HaulierIn = Convert.ToInt32(cmbHaulier.SelectedIndex);              
                objCSDInOut.ContInCondition = Convert.ToInt32(cmbCondition.SelectedIndex);

                objCSDInOut.LoadEmptyStatus = Convert.ToInt32(cmbStatus.SelectedIndex);

                if (Convert.ToInt32(cmbStatus.SelectedIndex) == 3)
                {
                    objCSDInOut.DumpingDate = dateDump.Value;
                    objCSDInOut.BookingNoDump = txtBookingNo.Text.Trim();

                }


                objCSDInOut.UserIdGateIn = user.UserId;
                objCSDInOut.RemarkIn = memoEdit1.Text.Trim();


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void SaveData()
        {
            if (btnCSDSave.Text == "Save")
            {

                objCSDInOut.InOutStatus = 1;
                var status = objCSDBll.Insert(objCSDInOut, objUpcomingCont.Id);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();

            }
            else if (btnCSDSave.Text == "Update")
            {
                
                var status = objCSDBll.Update(objCSDInOut);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                ClearForm();
                
            }

        }

        //private void SaveData()
        //{
        //    if (btnCSDSave.Text == "Save")
        //    {

        //        objCSDInOut.InOutStatus = 1;
        //        var status = objCSDBll.Insert(objCSDInOut, objUpcomingCont.Id);
        //        if (status.ToString() == "Ok")
        //        {
        //            MessageBox.Show("Data has been saved successfully.", "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            DeleteCSDGateInUPComing();
        //        }
        //        else
        //        {
        //            MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }

        //        ResetForm();

        //    }
        //    else if (btnCSDSave.Text == "Update")
        //    {

        //        var status = objCSDBll.Update(objCSDInOut);
        //        MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        ClearForm();

        //    }

        //}

        private void ddlCusCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ddlCusCode.SelectedIndex > 0)
            {
                var CustId = Convert.ToInt32(ddlCusCode.SelectedValue);
                //var obj = objCSDBll.GetCustNameRefNo(CustId);
                var obj = objCSDBll.SetCSDRefNo(CustId);
                Type myType = obj.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    var proName = prop.Name;
                    {
                        if (proName == "CustomerName")
                        {
                            txtCusName.Text = Convert.ToString(prop.GetValue(obj,null));

                        }
                        else
                        {
                            txtReferenceNo.Text =Convert.ToString(prop.GetValue(obj, null));
                        }
                    }                  
                }                    
            }
            else
            {
                txtCusName.Text = "";
                txtReferenceNo.Text = "";
            }
            //labelControl1.Focus();     
        }


        private void GetMLONameRefNo()
        {
            if (ddlCusCode.SelectedIndex > 0)
            {
                var CustId = Convert.ToInt32(ddlCusCode.SelectedValue);
                var obj = objCSDBll.SetCSDRefNo(CustId);
                Type myType = obj.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    var proName = prop.Name;
                    {
                        if (proName == "CustomerName")
                        {
                            txtCusName.Text = Convert.ToString(prop.GetValue(obj, null));

                        }
                        else
                        {
                            txtReferenceNo.Text = Convert.ToString(prop.GetValue(obj, null));
                        }
                    }
                }
            }
            else
            {
                txtCusName.Text = "";
                txtReferenceNo.Text = "";
            } 
        }

        private void cmbContSize_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int sizeId = Convert.ToInt32(cmbContSize.SelectedValue);

            if (sizeId > 0)
            {
                int typeId = Convert.ToInt32(cmbConType.SelectedValue);

                if (typeId > 0)
                {

                    txtISO.Text = objCSDBll.GetISOCode(sizeId, typeId);

                }
            }

        }

        private void cmbConType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int typeId = Convert.ToInt32(cmbConType.SelectedValue);

            if (typeId > 0)
            {
                int sizeId = Convert.ToInt32(cmbContSize.SelectedValue);

                if (typeId > 0)
                {

                    txtISO.Text = objCSDBll.GetISOCode(sizeId, typeId);

                }
            }

        }

        private void DeleteCSDGateInUPComing()
        {
            if (objUpcomingCont.Id > 0)
            {
                objCSDBll.DeleteUpComingContainer(objUpcomingCont.Id);

            }


        }

        private void GEtISONumber()
        {
            int typeId = Convert.ToInt32(cmbConType.SelectedValue);

            if (typeId > 0)
            {
                int sizeId = Convert.ToInt32(cmbContSize.SelectedValue);

                if (typeId > 0)
                {

                    txtISO.Text = objCSDBll.GetISOCode(sizeId, typeId);

                }
            }

        }

        private void cmbTrailer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbTrailer.SelectedValue) == 1)
            {
                int trailerId = Convert.ToInt32(cmbTrailer.SelectedValue);
                LoadTrailerNumberByTrailerId(trailerId);                             
            }
            else
            {
                txtTrailerInNo.Text = "";
                txtTrailerInNo.AutoCompleteCustomSource = null;
            }
        }

        public void LoadTrailerNumberByTrailerId( int trailerId)
        {
            
            var data = objCSDBll.GetAllTrailerNumber(trailerId);
            var source = new AutoCompleteStringCollection();
            foreach (var item in data)
            {

                source.Add(item.TrailerNumber1);
            }
            txtTrailerInNo.AutoCompleteCustomSource = source;
            txtTrailerInNo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTrailerInNo.AutoCompleteSource = AutoCompleteSource.CustomSource;


        }

        private void cmbStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbStatus.SelectedIndex) == 3)
            {
                lblBookingNo.Visible = true;
                lblDumpDate.Visible = true;
                txtBookingNo.Visible = true;
                dateDump.Visible = true;
            }
            else
            {
                lblBookingNo.Visible = false;
                lblDumpDate.Visible = false;
                txtBookingNo.Visible = false;
                dateDump.Visible = false;

            }

        }

        private void chkGEtryEnability_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGEtryEnability.Checked)
            {
                dateIn.Enabled = false;

            }
            else
            {
                dateIn.Enabled = true;
            }
        }
                    

        private CSDContGateInOut GetCSDInOutById(int csdId)
        {
            CSDContGateInOut objCSD = objCSDBll.GetCSDById(csdId);
            return objCSD;
           
        }

        private void BindDataToField(CSDContGateInOut objCSD)
        {

            ddlCusCode.SelectedValue = objCSD.CustId;
            txtCusName.Text = objCSD.Customer.CustomerName;
            txtReferenceNo.Text = Convert.ToString(objCSD.RefNo);
            chkPermission.Checked =Convert.ToBoolean(objCSD.InPermission);
            txtChallaNo.Text = objCSD.ChallanNo;
            txtContNumbe.Text = objCSD.ContNo;
            txtISO.Text = objCSD.ISO;
            dateIn.Value =Convert.ToDateTime(objCSD.DateIn);
            cmbContSize.SelectedValue = objCSD.ContSize;
            cmbConType.SelectedValue = objCSD.ContType;           
            cmbFrom.SelectedValue = objCSD.DepotFrom;                
            cmbTrailer.SelectedValue = objCSD.TrailerIn;

            if (objCSD.TrailerIn == 1)
            {
                LoadTrailerNumberByTrailerId(Convert.ToInt32(objCSD.TrailerIn));
            }
            
            txtTrailerInNo.Text = objCSD.TrailerInNo;                            
            cmbStatus.SelectedIndex = Convert.ToInt32(objCSD.LoadEmptyStatus);

            if (objCSD.LoadEmptyStatus == 3)
            {
                txtBookingNo.Text = objCSD.BookingNoDump;
                dateDump.Value = Convert.ToDateTime(objCSD.DumpingDate);
                lblBookingNo.Visible = true;
                lblDumpDate.Visible = true;
                txtBookingNo.Visible = true;
                dateDump.Visible = true;
            }
            else
            {
                lblBookingNo.Visible = false;
                lblDumpDate.Visible = false;
                txtBookingNo.Visible = false;
                dateDump.Visible = false;
            }

            cmbCondition.SelectedIndex = Convert.ToInt32(objCSD.ContInCondition);
            cmbHaulier.SelectedIndex = Convert.ToInt32(objCSD.HaulierIn);
            txtReferenceNo.Text = Convert.ToString(objCSD.RefNo);
            txtImpVessel.Text = Convert.ToString(objCSD.ImpVssl);
            txtRotation.Text = Convert.ToString(objCSD.RotImp);
            memoEdit1.Text = Convert.ToString(objCSD.RemarkIn);

            btnCSDSave.Text = "Update";
            btnChlnPrint.Visible = true;
            btnCSDDelete.Enabled = true;


        }       

        private void BindSearchDatatoGrid(List<SerachCSDGateInOutData_Result> listCSD)
        {

            

            if (listCSD.Count > 0)
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                foreach (var objCSD in listCSD)
                {                   
                    dataGridView1.Rows.Add(objCSD.SL, objCSD.ContNo, objCSD.RefNo, objCSD.CustomerCode, objCSD.ContainerTypeName, objCSD.ContainerSize, objCSD.ContainerGateEntryId);
                }
            }
            else
            {
                MessageBox.Show("No Record found !!", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void cmbGridRow_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                PageSize = Convert.ToInt32(cmbGridRow.SelectedItem);
                LoadDataToGrid(1);
                labelControl1.Focus();
           
        }

        private void cmbSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (cmbSearch.SelectedIndex == 0)
            {
                txtSearch.Enabled = false;
                btnSearch.Enabled = false;

            }
            else if(cmbSearch.SelectedIndex == 1)
            {
                txtSearch.Enabled = false;
                btnSearch.Enabled = true;

            }
            else
            {
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
            }
            labelControl1.Focus();
        }


        private void ResetForm()
        {

                       
            txtChallaNo.Text = "";
            txtContNumbe.Text = "";           
            txtISO.Text = "";
            memoEdit1.Text = "";
            chkPermission.Checked = false;           
            cmbContSize.SelectedValue = 0;
            cmbConType.SelectedValue = 0;
            cmbFrom.SelectedValue = 0;           
            cmbTrailer.SelectedValue = 0;
            txtTrailerInNo.Text = "";           
            cmbHaulier.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            txtBookingNo.Text = "";
            dateDump.Value = DateTime.Now;
            lblBookingNo.Visible = false;
            lblDumpDate.Visible = false;
            txtBookingNo.Visible = false;
            dateDump.Visible = false;
            btnCSDDelete.Enabled = false;
            dateIn.Value = DateTime.Now;
            var refNo = Convert.ToInt32(txtReferenceNo.Text);
            txtReferenceNo.Text = Convert.ToString(refNo + 1);
            objCSDInOut = new CSDContGateInOut();
            txtContNumbe.Focus();


        }

        private void ClearForm()
        {

            txtImpVessel.Text = "";
            txtCusName.Text = "";
            txtChallaNo.Text = "";
            txtContNumbe.Text = "";
            txtRotation.Text = "";
            txtISO.Text = "";
            memoEdit1.Text = "";
            chkPermission.Checked = false;
            ddlCusCode.SelectedValue = 0;
            cmbContSize.SelectedValue = 0;
            cmbConType.SelectedValue = 0;
            cmbFrom.SelectedValue = 0;
            cmbStatus.SelectedIndex = 0;
            cmbCondition.SelectedIndex = 0;
            cmbTrailer.SelectedValue = 0;
            txtTrailerInNo.Text = "";           
            cmbHaulier.SelectedIndex = 0;
            txtBookingNo.Text = "";
            dateDump.Value = DateTime.Now;
            lblBookingNo.Visible = false;
            lblDumpDate.Visible = false;
            txtBookingNo.Visible = false;
            dateDump.Visible = false;
            btnChlnPrint.Visible = false;
            btnCSDDelete.Enabled = false;
            dateIn.Value = DateTime.Now;
            txtReferenceNo.Text = "";
            objCSDInOut = new CSDContGateInOut();
            dataGridView1.ClearSelection();
            btnCSDSave.Text = "Save";
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            labelControl1.Focus();


        }

        private void ContainerGateEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearForm();
        }

        private void chkPermission_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPermission.Checked == true)
            {
                //txtContNumbe.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                txtContNumbe.Properties.Mask.EditMask = "[A-Z0-9]{7,11}";
            }
            else
            {
                //txtContNumbe.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                txtContNumbe.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";

            }
        }

        private void txtCusName_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (this.ActiveControl != null)
            //    {
            //        this.SelectNextControl(this.ActiveControl, true, true, true, true);
            //    }
            //    e.Handled = true; // Mark the event as handled
            //}
        }
        private void MovetoNextControl(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl != null)
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
                e.Handled = true; // Mark the event as handled
            }
        }

        private void txtReferenceNo_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtContNumbe_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void cmbContSize_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void cmbConType_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtISO_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void ddlCusCode_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtChallaNo_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void cmbTrailer_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtImpVessel_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtTrailerInNo_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtRotation_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void cmbHaulier_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void cmbCondition_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void cmbFrom_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void cmbStatus_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtBookingNo_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void dateDump_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void memoEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }
    }
}
