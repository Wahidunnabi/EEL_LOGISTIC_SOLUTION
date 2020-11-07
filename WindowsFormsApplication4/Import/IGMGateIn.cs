using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Configuration;
using System.Data.SqlClient;

namespace LOGISTIC.UI.Import
{
    public partial class IGMGateIn : Form
    {

        
        private ContainerTypeBll conTypeBll = new ContainerTypeBll();
        private ContainerSizeBll contSizeBll = new ContainerSizeBll();
        private TrailerBll objTrailerBll = new TrailerBll();
        private TrailerNumberBll tnBll = new TrailerNumberBll();

        private IGMImportBLL IGMBll = new IGMImportBLL();
        private CustomerBll MLOBll = new CustomerBll();
        private static List<IGMContGateInOut> listIGMGateIn = new List<IGMContGateInOut>();
        private static IGMImportDetail objIGMImportDetail = new IGMImportDetail();
        private static IGMContGateInOut objIGMGateIn = new IGMContGateInOut();

        static int PageSize = 10;

        public IGMGateIn()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);        
            txtContNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtContNumber.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
           
        }

        public IGMGateIn(IGMImportDetail IGMImportDetail)
        {            
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            txtContNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtContNumber.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";
            txtRefNo.Text = IGMBll.SetContGateInOutRefNo();
            objIGMImportDetail = IGMImportDetail;                  
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
           
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
        private void IGMGateIn_Load(object sender, EventArgs e)
        {
            ComboLoad();
            cmbPageSizeLoad();
            CoditionLoad();
            LoadSize();
            LoadType();
            CPALocationLoad();
            LoadTrailerAccount();
            PrepareGrid();
            LoadDataToGrid(1);
            LoadCustomer();
            if (objIGMImportDetail.IGMDetailsId > 0)
            {
                BindIGMDetailData();
                btnGateInDelete.Enabled = false;

            }
            else
            {
                btnGateInSave.Enabled = false;
                btnGateInDelete.Enabled = false;

            }

        }

        #region Load Basic Data

        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Container No");
            cmbSearch.Items.Insert(3, "BL Number");
            cmbSearch.Items.Insert(4, "Vessel Name");
            cmbSearch.Items.Insert(5, "Reg No");
            cmbSearch.SelectedIndex = 0;
        }

        private void cmbPageSizeLoad()
        {

            cmbGridRow.Items.Insert(0, 5);
            cmbGridRow.Items.Insert(1, 10);
            cmbGridRow.Items.Insert(2, 15);           
            cmbGridRow.SelectedIndex = 1;


        }

        private void LoadType()
        {

            var type = conTypeBll.Getall();           
            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.ContainerTypeId, t.ContainerTypeName);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Type--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                ddlType.DataSource = dt;
                ddlType.DisplayMember = "t_Name";
                ddlType.ValueMember = "t_ID";
            }
            ddlType.SelectedIndex = 0;
        }

        private void LoadSize()
        {

            var type = contSizeBll.Getall();         
            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.ContainerSizeId, t.ContainerSize1);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Size--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                ddlSize.DataSource = dt;
                ddlSize.DisplayMember = "t_Name";
                ddlSize.ValueMember = "t_ID";
            }
            ddlSize.SelectedIndex = 0;
        }

        private void CoditionLoad()
        {
            ddlCondition.Items.Insert(0, "--Condition--");
            ddlCondition.Items.Insert(1, "Sound");
            ddlCondition.Items.Insert(2, "Damage");
            ddlCondition.SelectedIndex = 0;
        }

        private void CPALocationLoad()
        {
            ddlCPALocation.Items.Insert(0, "--CPA Location--");
            ddlCPALocation.Items.Insert(1, "Hook");
            ddlCPALocation.Items.Insert(2, "Yeard");
            ddlCPALocation.SelectedIndex = 0;
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
                ddlTrailer.DataSource = dt_Types;
                ddlTrailer.DisplayMember = "t_Name";
                ddlTrailer.ValueMember = "t_ID";
            }

            ddlTrailer.SelectedIndex = 0;
        }

       
        #endregion


        #region GRID PAGER CODE

        public void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SlateGray;
            dataGridView1.EnableHeadersVisualStyles = false;


            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 7;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";
            dataGridView1.Columns[0].DataPropertyName = "SL";

            dataGridView1.Columns[1].HeaderText = "Container No";
            dataGridView1.Columns[1].DataPropertyName = "ContainerNo";

            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "Size";
            dataGridView1.Columns[2].DataPropertyName = "ContainerSize";

            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[3].HeaderText = "Type";
            dataGridView1.Columns[3].DataPropertyName = "ContainerTypeName";

            dataGridView1.Columns[4].HeaderText = "Seal No";
            dataGridView1.Columns[4].DataPropertyName = "SealNo";

            dataGridView1.Columns[5].Width = 96;
            dataGridView1.Columns[5].HeaderText = "Date In";
            dataGridView1.Columns[5].DataPropertyName = "GateInDate";


            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[6].DataPropertyName = "IGMContGateInOutId";

            dataGridView1.ClearSelection();


        }
       
        private void LoadDataToGrid(int pageIndex)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllIGMGateInData", con))
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
                    //int totalBox = dt.AsEnumerable().Sum(r => r.Field<int>("ContainerSize"));
                    var totalBox = dt.AsEnumerable().Where(x => x.Field<int>("ContainerSize") == 20).ToList();
                    var totalTuse = dt.AsEnumerable().Where(x => x.Field<int>("ContainerSize") >= 40).ToList();
                    //int totalTuse = dt.AsEnumerable().Sum(r => r.Field<int>("Teus"));

                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    if (recordCount > PageSize)
                    {
                        PopulatePager(recordCount, pageIndex);
                    }
                    txtTotalBox.Text = Convert.ToString(totalBox.Count());
                    txtTotalTues.Text = Convert.ToString(totalTuse.Count()*2);

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
            int pagerSpan = 5;

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
                    btnPage.Location = new System.Drawing.Point((38 * count) + 15, 5);
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            long IGMContGateInOutId = Convert.ToInt32(selectedRow.Cells[6].Value);
            objIGMGateIn = IGMBll.GetIGMContGateInOutByID(IGMContGateInOutId);

            txtContNumber.Text = Convert.ToString(objIGMGateIn.IGMImportDetail.ContainerNo);
            ddlSize.SelectedValue = Convert.ToInt32(objIGMGateIn.IGMImportDetail.SizeId);
            ddlType.SelectedValue = Convert.ToInt32(objIGMGateIn.IGMImportDetail.TypeId);
            txtISO.Text = Convert.ToString(objIGMGateIn.IGMImportDetail.ISO);
            txtRefNo.Text = Convert.ToString(objIGMGateIn.RefNo);
            ddlCPALocation.SelectedIndex = Convert.ToInt32(objIGMGateIn.CPALockId);
            ddlCusCode.SelectedValue = Convert.ToInt32(objIGMGateIn.MLIId);
            ddlTrailer.SelectedValue = objIGMGateIn.TrailerId;
            if (objIGMGateIn.TrailerId == 1)
            {
                LoadTrailerNumberByTrailerId(Convert.ToInt32(objIGMGateIn.TrailerId));
            }           
            txtTrailerInNo.Text = objIGMGateIn.TrailerNumber;

            ddlCondition.SelectedIndex = Convert.ToInt32(objIGMGateIn.GateInCondition);
            txtRemarks.Text = objIGMGateIn.RemarksIn;
            dateIn.Value = Convert.ToDateTime(objIGMGateIn.GateInDate);
            btnGateInSave.Text = "Update";
            btnGateInSave.Enabled = true;
            btnGateInDelete.Enabled = true;           

        }

        private void cmbGridRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(cmbGridRow.SelectedItem);
            LoadDataToGrid(1);
            labelControl1.Focus();
        }

        #endregion
       

        private void BindIGMDetailData()
        {

            txtContNumber.Text = objIGMImportDetail.ContainerNo;
            ddlSize.SelectedValue = Convert.ToInt32(objIGMImportDetail.SizeId);
            ddlType.SelectedValue = Convert.ToInt32(objIGMImportDetail.TypeId);
            txtISO.Text = objIGMImportDetail.ISO;
            ddlCusCode.SelectedValue = Convert.ToInt32(objIGMImportDetail.IGMImport.CustomerId);


        }

        private void BindIGMContGateInData(IGMContGateInOut objIGMGateIn)
        {

            txtRefNo.Text = Convert.ToString(objIGMGateIn.RefNo);
            ddlCPALocation.SelectedIndex = Convert.ToInt32(objIGMGateIn.CPALockId);
            ddlTrailer.SelectedValue = objIGMGateIn.TrailerId;
            LoadTrailerNumberByTrailerId(Convert.ToInt32(objIGMGateIn.TrailerId));
            txtTrailerInNo.Text = objIGMGateIn.TrailerNumber;
            ddlCondition.SelectedIndex = Convert.ToInt32(objIGMGateIn.GateInCondition);
            dateIn.Value = Convert.ToDateTime(objIGMGateIn.GateInDate);
            txtRemarks.Text = objIGMGateIn.RemarksIn;

        }     

        private void btnGateInSave_Click(object sender, EventArgs e)
        {
            bool flag = ValidateGateInData();
            if (flag == true)
            {
                SaveIGMContGateInData();
                LoadDataToGrid(1);
                ClearForm();

            }
        }

        private void btnGateInDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                             "Confirm IGM Gate In deletion",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objIGMGateIn.IGMContGateInOutId > 0)
                {

                    var status = IGMBll.DeleteIGMContGateInOut(objIGMGateIn);
                    MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGrid(1);
                    ClearForm();
                }

            }

        }

        private void btnGateInCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnGateInClose_Click(object sender, EventArgs e)
        {          
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
                        txtSearch.Text = "";
                        break;
                    }               
                default:
                    {
                        List<SerachIGMGateInOutData_Result> listInOut = IGMBll.SearchIGMGateInOutData(searchBy, searchText);
                        BindSearchDatatoGrid(listInOut);
                        break;
                    }
            }

        }

        private void BindSearchDatatoGrid(List<SerachIGMGateInOutData_Result> listInOut)
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listInOut.Count > 0)
            {
                foreach (var objInOut in listInOut)
                {
                    dataGridView1.Rows.Add(objInOut.SL, objInOut.ContainerNo, objInOut.ContainerSize, objInOut.ContainerTypeName, objInOut.SealNo, objInOut.GateInDate, objInOut.IGMContGateInOutId);

                }
            }
            else
            {
                MessageBox.Show("No Record found !!");
            }
            txtSearch.Text = "";
        }

        private void cmbSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSearch.SelectedIndex == 0)
            {               
                txtSearch.Enabled = false;
                txtSearch.Visible = true;
                btnSearch.Enabled = false;            
               
            }
           else if (cmbSearch.SelectedIndex == 1)
            {
                txtSearch.Enabled = false;
                txtSearch.Visible = true;
                btnSearch.Enabled = true;               
               
            }           
          else  if (cmbSearch.SelectedIndex == 4)
            {
                txtSearch.Visible = false;              
                btnSearch.Enabled = true;
            }
            else
            {
                txtSearch.Visible = true;
                txtSearch.Enabled = true;             
                btnSearch.Enabled = true;
            }
            labelControl1.Focus();
        }    

        private void txtContNumber_KeyUp(object sender, KeyEventArgs e)
        {

            if (txtContNumber.Text.Length == 11)
            {
                objIGMImportDetail = IGMBll.GetIGMImportDetailstoGateIn(txtContNumber.Text.Trim());
                if (objIGMImportDetail == null)
                {
                    MessageBox.Show("No Container Found to Gate-In !!", "IGM Container Search", MessageBoxButtons.OK, MessageBoxIcon.Information);                 
                    ResetForm();
                }
                else
                {
                    BindIGMDetailData();
                    txtRefNo.Text = IGMBll.SetContGateInOutRefNo();
                    btnGateInSave.Enabled = true;

                    //if (objIGMImportDetail.InOutStatus == 0)
                    //{
                    //    BindIGMDetailData();
                    //    txtRefNo.Text = IGMBll.SetContGateInOutRefNo();
                    //    btnGateInSave.Enabled = true;
                    //}
                    //if (objIGMImportDetail.InOutStatus > 0)
                    //{

                    //    objIGMGateIn = objIGMImportDetail.IGMContGateInOuts.First();
                    //    BindIGMDetailData();
                    //    BindIGMContGateInData(objIGMGateIn);
                    //    btnGateInSave.Text = "Update";
                    //    btnGateInSave.Enabled = true;
                    //}

                }
            }
            else
            {
                ResetForm();
            }        
        }
                 
        private bool ValidateGateInData()
        {
            var errMessage = "";                      
            if (Convert.ToInt32(ddlCPALocation.SelectedIndex) == 0)
            {
                errMessage = errMessage + "* Please select CPA location !!\n";
            }
            if (Convert.ToInt32(ddlTrailer.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a trailer !!\n";
            }
            if (txtTrailerInNo.Text.Trim() == "")
            {
                errMessage = errMessage + "* Please enter trailer number !!\n";
            }
            if (Convert.ToInt32(ddlCondition.SelectedIndex) == 0)
            {
                errMessage = errMessage + "* Please select Gate In condition !!\n";
            }
            if (errMessage != "")
            {
                MessageBox.Show(errMessage, "Input required !!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SaveIGMContGateInData()
        {
            if (btnGateInSave.Text == "Save")
            {  
                IGMContGateInOut objGateInOut = new IGMContGateInOut();
                objGateInOut.IGMDetailsId = objIGMImportDetail.IGMDetailsId;
                objGateInOut.RefNo = Convert.ToInt64(txtRefNo.Text.Trim());
                objGateInOut.CPALockId = Convert.ToInt32(ddlCPALocation.SelectedIndex);
                objGateInOut.TrailerId = Convert.ToInt32(ddlTrailer.SelectedValue);
                objGateInOut.TrailerNumber = txtTrailerInNo.Text.Trim();
                objGateInOut.GateInCondition = Convert.ToInt32(ddlCondition.SelectedIndex);
                objGateInOut.RemarksIn = txtRemarks.Text.Trim();
                objGateInOut.GateInDate = dateIn.Value;
                objGateInOut.MLIId = Convert.ToInt32(ddlCusCode.SelectedValue);

                var status = IGMBll.SaveIGMContGateIn(objGateInOut);
                MessageBox.Show(status.ToString(), "Data Insertion Status.",MessageBoxButtons.OK,MessageBoxIcon.Information);
              
            }
            else
            {
                
                objIGMGateIn.CPALockId = Convert.ToInt32(ddlCPALocation.SelectedIndex);
                objIGMGateIn.TrailerId = Convert.ToInt32(ddlTrailer.SelectedValue);
                objIGMGateIn.TrailerNumber = txtTrailerInNo.Text.Trim();
                objIGMGateIn.GateInCondition = Convert.ToInt32(ddlCondition.SelectedIndex);
                objIGMGateIn.RemarksIn = txtRemarks.Text.Trim();
                objIGMGateIn.GateInDate = dateIn.Value;
                objIGMGateIn.MLIId = Convert.ToInt32(ddlCusCode.SelectedValue);
                var status = IGMBll.UpdateIGMContGateInOut(objIGMGateIn);

                MessageBox.Show(status.ToString(), "Data Update Status.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        public void ClearForm()
        {
            txtContNumber.Text = "";
            txtRefNo.Text = "";
            dateIn.Value = DateTime.Now;
            ddlSize.SelectedValue = 0;
            ddlType.SelectedValue = 0;
            txtISO.Text = "";
            txtSearch.Text = "";
            txtRemarks.Text = "";
            ddlCPALocation.SelectedIndex = 0;

            ddlTrailer.SelectedValue = 0;
            txtTrailerInNo.Text = "";         
            ddlCondition.SelectedIndex = 0;          
            cmbSearch.SelectedIndex = 0;
            dataGridView1.ClearSelection();
            objIGMImportDetail = new IGMImportDetail();
            objIGMGateIn = new IGMContGateInOut();
            btnGateInSave.Text = "Save";
            btnGateInSave.Enabled = false;            
            txtContNumber.Focus();
            ddlCusCode.SelectedValue = 0;

        }

        public void ResetForm()
        {
            
            dateIn.Value = DateTime.Now;
            txtRefNo.Text = "";
            ddlSize.SelectedValue = 0;
            ddlType.SelectedValue = 0;
            txtISO.Text = "";
            ddlCPALocation.SelectedIndex = 0;
            ddlTrailer.SelectedValue = 0;
            txtTrailerInNo.Text = "";
            ddlCondition.SelectedIndex = 0;
            txtRemarks.Text = "";
            objIGMImportDetail = new IGMImportDetail();
            btnGateInSave.Text = "Save";
            btnGateInSave.Enabled = false;

        }

        private void IGMGateIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClearForm();
        }

        private void ddlTrailer_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (Convert.ToInt32(ddlTrailer.SelectedValue) == 1)
            {
                int trailerId = Convert.ToInt32(ddlTrailer.SelectedValue);
                LoadTrailerNumberByTrailerId(trailerId);
            }
            else
            {
                txtTrailerInNo.Text = "";
                txtTrailerInNo.AutoCompleteCustomSource = null;

            }
        }


        public void LoadTrailerNumberByTrailerId(int trailerId)
        {

            var data = tnBll.GetAllTrailernumberBytrailerId(trailerId);
            var source = new AutoCompleteStringCollection();
            foreach (var item in data)
            {

                source.Add(item.TrailerNumber1);
            }
            txtTrailerInNo.AutoCompleteCustomSource = source;
            txtTrailerInNo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTrailerInNo.AutoCompleteSource = AutoCompleteSource.CustomSource;


        }

        private void IGMGateIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearForm();
        }
       
    }
}
