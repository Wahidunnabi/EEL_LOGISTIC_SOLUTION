using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using LOGISTIC.CSD.BLL;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LOGISTIC.REPORT;

namespace LOGISTIC.UI
{
    public partial class ContainerGateOut : Form
    {
       
        private static CSDContGateInOut objCSDInOut = new CSDContGateInOut();     
        private DepotBll depoBll = new DepotBll();
        private TrailerBll objTrailerBll = new TrailerBll();
        private TrailerNumberBll tnBll = new TrailerNumberBll(); 
        private CSDGateInOutBLL objCSDBll = new CSDGateInOutBLL();

        private UserInfo user;
        int PageSize = 10;    
              
        public ContainerGateOut(UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;
            txtContainerNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtContainerNo.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";
            pnlGateInDetails.Visible = false;          
            btnCSDSave.Enabled = false;
            btnCSDDelete.Enabled = false;
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            btnChlnOutPrint.Enabled = false;



        }

        public ContainerGateOut(CSDContGateInOut objCSD, UserInfo user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(50, 0);
            this.user = user;
            txtContainerNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtContainerNo.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";
            //txtContainerNo.Visible = false;
            lblContNo.Visible = false;
            txtChallan.Focus();
            objCSDInOut = objCSD;          
            btnCSDDelete.Enabled = false;
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            btnChlnOutPrint.Enabled = false;



        }

        private void ContainerGateOut_Load(object sender, EventArgs e)
        {
            cmbSearchLoad();
            PrepareGrid();
            cmbPageSizeLoad();
            LoadDataToGrid(1);
            LoadDepot();
            LoadTrailerAccount();
            LoadHaular();
            LoadStatus();
            LoadCondition();
            if (objCSDInOut.ContainerGateEntryId > 0)
            {
                BindDataToField();
                btnCSDSave.Enabled = true;

            }

        }



        #region Load Basic Data
        private void LoadCondition()
        {
            cmbCondition.Items.Insert(0, "--Condition--");
            cmbCondition.Items.Insert(1, "Sound");
            cmbCondition.Items.Insert(2, "Damage");
            cmbCondition.SelectedIndex = 0;

        }

        private void cmbSearchLoad()
        {

            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Container Number");
            cmbSearch.Items.Insert(3, "Ref Number");
            cmbSearch.Items.Insert(4, "Challan In No");
            cmbSearch.Items.Insert(5, "Challan Out No");
            cmbSearch.SelectedIndex = 0;

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
                cmbOutTo.DataSource = dt_Types;
                cmbOutTo.DisplayMember = "t_Name";
                cmbOutTo.ValueMember = "t_ID";
            }


            cmbOutTo.SelectedIndex = 0;

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
            cmbStatusOut.Items.Insert(0, "--Status--");
            cmbStatusOut.Items.Insert(1, "Empty");
            cmbStatusOut.Items.Insert(2, "Load");
            cmbStatusOut.Items.Insert(3, "Dump");

            cmbStatusOut.SelectedIndex = 0;

        }

        private void cmbPageSizeLoad()
        {

            cmbGridRow.Items.Insert(0, 5);
            cmbGridRow.Items.Insert(1, 10);
            cmbGridRow.Items.Insert(2, 15);
            cmbGridRow.SelectedIndex = 1;

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
                    //PopulatePager(recordCount, pageIndex);
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
            LoadDataToGrid(int.Parse(btnPager.Name));
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

        #endregion
                            
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
                if (objCSDInOut != null)
                {


                    objCSDInOut.DateOut = null;
                    objCSDInOut.DepotTo = null;
                    objCSDInOut.ChallanOut = null;
                    objCSDInOut.TrailerOut = null;
                    objCSDInOut.TrailerOutNo = null;
                    objCSDInOut.ExpVssl = null;
                    objCSDInOut.RotExp = null;
                    objCSDInOut.HaulierOut = null;
                    objCSDInOut.LoadEmptyStatusOut = null;
                    objCSDInOut.BookingNoOut = null;
                    objCSDInOut.UserIdGateOut = null;
                    objCSDInOut.RemarkOut = null;
                    objCSDInOut.InOutStatus = 1;

                    var status = objCSDBll.Update(objCSDInOut);
                    MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();


                    //objCSDBll.Delete(objCSDInOut.ContainerGateEntryId);                    
                    //MessageBox.Show("Deleted successfully !! ");
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
                case 5:
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var CSDId = Convert.ToInt32(selectedRow.Cells[6].Value);                   
            objCSDInOut = objCSDBll.GetCSDById(CSDId);
            if (objCSDInOut != null)
            {
                BindDataToField();
                btnCSDSave.Enabled = true;
                btnCSDDelete.Enabled = true;

            }
            
            btnChlnOutPrint.Enabled = true;

        }

        private bool ValidateCSD()
        {
            var errMessage = "";

            //if (txtChallan.Text.Trim() == "")
            //{
            //    errMessage = errMessage + "* Challan number can't be null !!\n";
            //    txtChallan.Focus();

            //}
            //if (txtExpVessel.Text.Trim() == "")
            //{
            //    errMessage = errMessage + "* Enter vessel name !!\n";
            //    txtExpVessel.Focus();

            //}
            if (txtTrailerOutNo.Text.Trim() == "")
            {
                errMessage = errMessage + "* Please enter trailer number !!\n";
                txtTrailerOutNo.Focus();

            }
            if (Convert.ToInt32(cmbHaulier.SelectedIndex) == 0)
            {
                errMessage = errMessage + "* Please select haulier type !!\n";
                cmbHaulier.Focus();

            }
            if (Convert.ToInt32(cmbOutTo.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select out to location !!\n";
                cmbOutTo.Focus();

            }
            if (Convert.ToInt32(cmbStatusOut.SelectedIndex) == 0)
            {
                errMessage = errMessage + "* Please select container status !!\n";
                cmbOutTo.Focus();

            }
            if (errMessage != "")
            {
                MessageBox.Show(errMessage, "Input Required.",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

                objCSDInOut.DateOut = Convert.ToDateTime(dateOut.Text);
                objCSDInOut.DepotTo = Convert.ToInt32(cmbOutTo.SelectedValue);
                objCSDInOut.ChallanOut = txtChallan.Text.Trim();
                objCSDInOut.TrailerOut = Convert.ToInt32(cmbTrailer.SelectedValue);
                objCSDInOut.TrailerOutNo = txtTrailerOutNo.Text.Trim();
                objCSDInOut.ExpVssl = txtExpVessel.Text.Trim();
                objCSDInOut.RotExp = txtRotation.Text.Trim();
                objCSDInOut.HaulierOut = Convert.ToInt32(cmbHaulier.SelectedIndex);
                objCSDInOut.LoadEmptyStatusOut = Convert.ToInt32(cmbStatusOut.SelectedIndex);
                objCSDInOut.BookingNoOut = txtBookingNo.Text.Trim();
                objCSDInOut.UserIdGateOut = user.UserId;
                objCSDInOut.RemarkOut = txtRemarksOut.Text.Trim();
                
                objCSDInOut.InOutStatus = 2;

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
               
                objCSDInOut.InOutStatus = 2;
                var status = objCSDBll.Update(objCSDInOut);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }            
            else
            {
                var status = objCSDBll.Update(objCSDInOut);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
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
                txtTrailerOutNo.Text = "";
                txtTrailerOutNo.AutoCompleteCustomSource = null;
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
            txtTrailerOutNo.AutoCompleteCustomSource = source;
            txtTrailerOutNo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTrailerOutNo.AutoCompleteSource = AutoCompleteSource.CustomSource;


        }
                                          
        private void BindDataToField()
        {
                pnlGateInDetails.Visible = true;
                txtClientCode.Text = objCSDInOut.Customer.CustomerCode;
                lblCustomer.Text = objCSDInOut.Customer.CustomerName;
                lblContainerNo.Text = objCSDInOut.ContNo;
                lblConType.Text = Convert.ToString(objCSDInOut.ContainerType.ContainerTypeName);
                lblContSize.Text = Convert.ToString(objCSDInOut.ContainerSize.ContainerSize1);
                lblChalan.Text = objCSDInOut.ChallanNo;
                DateTime date = Convert.ToDateTime(objCSDInOut.DateIn);
                lblDateIn.Text = date.ToString("dd/MMM/yyyy");
                txtRefNo.Text = Convert.ToString(objCSDInOut.RefNo);
                txtContainerNo.Text = objCSDInOut.ContNo;
            if (objCSDInOut.InOutStatus == 1)
            {
                cmbStatusOut.SelectedIndex = Convert.ToInt32(objCSDInOut.LoadEmptyStatus);
               cmbCondition.SelectedIndex = Convert.ToInt32(objCSDInOut.ContInCondition);
            }
            else
            if (objCSDInOut.InOutStatus == 2)
            {

                dateOut.Value = Convert.ToDateTime(objCSDInOut.DateOut);
                txtChallan.Text = objCSDInOut.ChallanOut;
                txtExpVessel.Text = objCSDInOut.ExpVssl;
                txtRotation.Text = objCSDInOut.RotExp;
                cmbOutTo.SelectedValue = objCSDInOut.DepotTo;
                cmbStatusOut.SelectedIndex = Convert.ToInt32(objCSDInOut.LoadEmptyStatusOut);
                cmbTrailer.SelectedValue = objCSDInOut.TrailerOut;
                LoadTrailerNumberByTrailerId(Convert.ToInt32(objCSDInOut.TrailerOut));
                txtTrailerOutNo.Text = objCSDInOut.TrailerOutNo;               
                cmbHaulier.SelectedIndex = Convert.ToInt32(objCSDInOut.HaulierOut);
                txtBookingNo.Text = objCSDInOut.BookingNoOut;
                txtRemarksOut.Text = objCSDInOut.RemarkOut;
                btnCSDSave.Text = "Update";
                btnCSDDelete.Enabled = true;
            }
            else
            {
                txtChallan.Text = "";
                txtExpVessel.Text = "";
                txtRotation.Text = "";
                cmbOutTo.SelectedValue = 0;
                cmbStatusOut.SelectedIndex = 0;
                cmbTrailer.SelectedValue = 0;
                txtTrailerOutNo.Text = "";
                cmbHaulier.SelectedValue =0;
            }         
           
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

        private void cmbSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (cmbSearch.SelectedIndex == 0)
            {
                txtSearch.Enabled = false;
                btnSearch.Enabled = false;

            }
            else if (cmbSearch.SelectedIndex == 1)
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
       
        private void cmbGridRow_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(cmbGridRow.SelectedItem);
            LoadDataToGrid(1);
            labelControl1.Focus();
        }
      
        private void txtContainerNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtContainerNo.Text.Length == 11)
            {
                objCSDInOut = objCSDBll.GetCSDByContNumber(txtContainerNo.Text.Trim());
                if (objCSDInOut == null)
                {
                    MessageBox.Show("Container No " + txtContainerNo.Text.Trim() + " not found !!", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                else
                {
                    BindDataToField();
                    btnCSDSave.Enabled = true;
                }
            }
        }

        private void ResetForm()
        {

            txtContainerNo.Text = "";
            txtExpVessel.Text = "";
            txtChallan.Text = "";          
            txtRemarksOut.Text = "";
            txtBookingNo.Text = "";
            cmbOutTo.SelectedValue = 0;
            cmbStatusOut.SelectedIndex = 0;
            cmbTrailer.SelectedValue = 0;
            txtTrailerOutNo.Text = "";
            cmbHaulier.SelectedIndex = 0;
            btnCSDDelete.Enabled = false;
            dateOut.Value = DateTime.Now;
            pnlGateInDetails.Visible = false;
            dataGridView1.ClearSelection();
            objCSDInOut = new CSDContGateInOut();        
            txtContainerNo.Visible = true;
            lblContNo.Visible = true;
            btnCSDSave.Enabled = false;
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            txtContainerNo.Focus();


        }


        private void ClearForm()
        {

            txtContainerNo.Text = "";
            txtExpVessel.Text = "";
            txtChallan.Text = "";
            txtRotation.Text = "";
            txtBookingNo.Text = "";
            txtRemarksOut.Text = "";
            cmbOutTo.SelectedValue = 0;
            cmbStatusOut.SelectedIndex = 0;
            cmbTrailer.SelectedValue = 0;
            txtTrailerOutNo.Text = "";
            cmbHaulier.SelectedIndex = 0;
            btnCSDDelete.Enabled = false;
            dateOut.Value = DateTime.Now;
            pnlGateInDetails.Visible = false;
            dataGridView1.ClearSelection();
            objCSDInOut = new CSDContGateInOut();
            btnCSDSave.Text = "Save";
            txtContainerNo.Visible = true;
            lblContNo.Visible = true;
            btnCSDSave.Enabled = false;
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            txtContainerNo.Focus();
            btnChlnOutPrint.Enabled = false;


        }

        private void btnChlnOutPrint_Click(object sender, EventArgs e)
        {
            ReportDocument cryRpt = new ReportDocument();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            crConnectionInfo.ServerName = "ELL-SERVER2";
            crConnectionInfo.DatabaseName = "PORT_LOGISTIC";
            crConnectionInfo.UserID = "sa";
            crConnectionInfo.Password = "Sa@1234";
                    
            cryRpt.Load(@"D:\Report\" + "GateOutChallan.rpt");
            cryRpt.SetParameterValue("@CSDID", Convert.ToInt64(115));
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

        private void ContainerGateOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearForm();
        }

        private void dateOut_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
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

        private void cmbTrailer_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtTrailerOutNo_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtChallan_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtExpVessel_KeyUp(object sender, KeyEventArgs e)
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

        private void cmbOutTo_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void cmbStatusOut_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }

        private void txtBookingNo_KeyUp(object sender, KeyEventArgs e)
        {
            MovetoNextControl(e);
        }
    }
}
