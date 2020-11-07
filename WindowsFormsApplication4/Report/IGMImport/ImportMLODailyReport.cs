using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace LOGISTIC.UI.Report
{
    public partial class ImportMLODailyReport : Form
    {

        private CustomerBll MLOBll = new CustomerBll();
        private ImportReportBLL objBll = new ImportReportBLL();
        private ContainerTypeBll ctBll = new ContainerTypeBll();
        private ContainerSizeBll csBll = new ContainerSizeBll();
        public ImportMLODailyReport()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);


        }

        private void ImportMLODailyReport_Load(object sender, EventArgs e)
        {
            LoadCustomer();
            PrepareGrid();
            LoadcmbSearch();
            LoadConType();
            LoadContSize();
            LoadImporter();
            LoadCommodity();
            //btnLoad.Enabled = false;
            btnExcel.Enabled = false;
            RadioIn.Checked = true;
            progressBar1.Visible = false;
            labelControl1.Focus();
        }
        private void LoadcmbSearch()
        {

            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Container Number");
            cmbSearch.Items.Insert(3, "Size");
            cmbSearch.Items.Insert(4, "MLO");
            cmbSearch.Items.Insert(5, "BL No");
            cmbSearch.Items.Insert(6, "Commodity");
            cmbSearch.Items.Insert(7, "Importer");
            

            cmbSearch.SelectedIndex = 0;
        }
        private void LoadCustomer()
        {

            var type = MLOBll.Getall();
            ddlClient.DisplayMember = "CustomerCode";
            ddlClient.ValueMember = "CustomerId";
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Customer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlClient.DataSource = dt_Types;
                ddlClient.DisplayMember = "t_Name";
                ddlClient.ValueMember = "t_ID";
            }
            ddlClient.SelectedIndex = 0;

        }
        private ImporterBll importerBll = new ImporterBll();
        private void LoadImporter()
        {

            var type = importerBll.Getall();
            cmbImporter.ValueMember = "ImporterId";
            cmbImporter.DisplayMember = "ImporterName";
            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.ImporterId, t.ImporterName);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Importer--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                cmbImporter.DataSource = dt;
                cmbImporter.DisplayMember = "t_Name";
                cmbImporter.ValueMember = "t_ID";
            }
            cmbImporter.SelectedIndex = 0;

        }
        private CommodityBLL commodityBll = new CommodityBLL();
        private void LoadCommodity()
        {

            var type = commodityBll.Getall();
            cmbCommodity.ValueMember = "CommodityId";
            cmbCommodity.DisplayMember = "CommodityName";

            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.CommodityId, t.CommodityName);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Commodity Type--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                cmbCommodity.DataSource = dt;
                cmbCommodity.DisplayMember = "t_Name";
                cmbCommodity.ValueMember = "t_ID";
            }
            cmbCommodity.SelectedIndex = 0;

        }
        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            dataGridView1.EnableHeadersVisualStyles = false;           
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 17;

            dataGridView1.Columns[0].Width = 42;
            dataGridView1.Columns[0].HeaderText = "SL#";
            dataGridView1.Columns[0].DataPropertyName = "SL";


            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[1].HeaderText = "CONTAINER NO";
            dataGridView1.Columns[1].DataPropertyName = "ContainerNo";



            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "SIZE";
            dataGridView1.Columns[2].DataPropertyName = "Size";

            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[3].HeaderText = "TYPE";
            dataGridView1.Columns[3].DataPropertyName = "ContType";

            dataGridView1.Columns[4].Width = 90;
            dataGridView1.Columns[4].HeaderText = "SEAL NO";
            dataGridView1.Columns[4].DataPropertyName = "SealNo";


            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[5].HeaderText = "IN DATE";
            dataGridView1.Columns[5].DataPropertyName = "GateInDate";

            dataGridView1.Columns[6].Width = 120;
            dataGridView1.Columns[6].HeaderText = "MLO Code";
            dataGridView1.Columns[6].DataPropertyName = "CustomerCode";


          

            dataGridView1.Columns[7].Width = 120;
            dataGridView1.Columns[7].HeaderText = "Importer Name";
            dataGridView1.Columns[7].DataPropertyName = "ImporterName";


            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[8].HeaderText = "B/L NO";
            dataGridView1.Columns[8].DataPropertyName = "BLnumber";


            dataGridView1.Columns[9].Width = 160;
            dataGridView1.Columns[9].HeaderText = "IMPORT VESSEL";
            dataGridView1.Columns[9].DataPropertyName = "VesselName";


            dataGridView1.Columns[10].Width = 100;
            dataGridView1.Columns[10].HeaderText = "REG. NO.";
            dataGridView1.Columns[10].DataPropertyName = "Rotation";



            dataGridView1.Columns[11].Width = 120;
            dataGridView1.Columns[11].HeaderText = "CommodityName";
            dataGridView1.Columns[11].DataPropertyName = "CommodityName";


            dataGridView1.Columns[12].Width = 100;
            dataGridView1.Columns[12].HeaderText = "Vehicle No";
            dataGridView1.Columns[12].DataPropertyName = "TrailerNumber";


            dataGridView1.Columns[13].Width = 100;
            dataGridView1.Columns[13].HeaderText = "Condition";
            dataGridView1.Columns[13].DataPropertyName = "ConditionName";


            dataGridView1.Columns[14].Width = 100;
            dataGridView1.Columns[14].HeaderText = "Remarks";
            dataGridView1.Columns[14].DataPropertyName = "Remarks";


            //dataGridView1.Columns[7].Width = 160;
            //dataGridView1.Columns[7].HeaderText = "IMPORTER NAME";
            //dataGridView1.Columns[7].DataPropertyName = "ImporterName";

            dataGridView1.Columns[15].Width = 160;
            dataGridView1.Columns[15].HeaderText = "Gross Weight";
            dataGridView1.Columns[15].DataPropertyName = "GrossWeight";



            dataGridView1.Columns[16].Width = 160;
            dataGridView1.Columns[16].HeaderText = "Unite";
            dataGridView1.Columns[16].DataPropertyName = "UnitOfMeasureName";










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
            dr[1] = "--Select Type--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                cmbConType.DataSource = dt_Types;
                cmbConType.DisplayMember = "t_Name";
                cmbConType.ValueMember = "t_ID";


                cmbConType.DataSource = dt_Types;
                cmbConType.DisplayMember = "t_Name";
                cmbConType.ValueMember = "t_ID";
            }
            cmbConType.SelectedIndex = 0;
            cmbConType.SelectedIndex = 0;



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
            dr[1] = "--Select Size--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {

                cmbContSize.DataSource = dt_Types;
                cmbContSize.DisplayMember = "t_Name";
                cmbContSize.ValueMember = "t_ID";


                cmbContSize.DataSource = dt_Types;
                cmbContSize.DisplayMember = "t_Name";
                cmbContSize.ValueMember = "t_ID";
            }
            cmbContSize.SelectedIndex = 0;
            cmbContSize.SelectedIndex = 0;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            int ClientId = Convert.ToInt32(ddlClient.SelectedValue);
            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;
            string fDate = fromDate.ToString("dd MMM yy");
            string tDate = toDate.ToString("dd MMM yy");
            //string sDate = fromDate.ToString("dd MMM");


            DataSet ds = new DataSet();
            ds = objBll.GetMLOWiseDailyReport(ClientId, fromDate, toDate);

            string FileName = "D:\\24 Hours Report of " + ddlClient.Text.Trim() + " from " + fDate + " to " + tDate + ".xlsx";
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }


            xlApp.DisplayAlerts = false;
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
            Excel.Sheets worksheets = xlWorkBook.Worksheets;


            List<string> SheetNames = new List<string>();
            SheetNames.Add("STOCK");
            
            SheetNames.Add("DELIVERY");

            SheetNames.Add("GATE IN");

            try
            {


                #region IN Status


                for (int c = 0; c < ds.Tables.Count; c++)
                {
                    string sheetName = SheetNames[c];
                    var xlSheet = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    xlSheet.Name = SheetNames[c];

                    xlSheet.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 150, 0, 60, 40);
                    xlSheet.Cells[1, 1].value = "EASTERN LOGISTICS LIMITED";
                    xlSheet.Cells[1, 1].Font.Bold = true;
                    xlSheet.Cells[1, 1].Font.Size = 15;
                    xlSheet.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSheet.Range["A1:M1"].MergeCells = true;

                    xlSheet.Cells[2, 1].value = "KATHGAR, NORTH PATENGA, CHATTOGRAM";
                    xlSheet.Cells[2, 1].Font.Size = 10;
                    xlSheet.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSheet.Range["A2:M2"].MergeCells = true;

                    xlSheet.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: import@easternlogisticsltd.com";
                    xlSheet.Cells[3, 1].Font.Size = 10;
                    xlSheet.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSheet.Range["A3:M3"].MergeCells = true;

                    xlSheet.Cells[5, 1].value = "IMPORT CONTAINER " + sheetName + " REPORT OF " + ddlClient.Text.Trim() + " from " + fDate + " to " + tDate;
                    xlSheet.Cells[5, 1].Font.Bold = true;
                    xlSheet.Cells[5, 1].Font.Size = 10;
                    xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSheet.Range["A5:M5"].MergeCells = true;


                    xlSheet.Cells[7, 1].value = "A/C Name:  " + lblCustomerName.Text.Trim() ;
                    xlSheet.Cells[7, 1].Font.Bold = true;
                    xlSheet.Cells[7, 1].Font.Size = 12;
                    xlSheet.Cells[7, 1].Font.color = Color.Blue;
                    xlSheet.Cells[7, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSheet.Range["A7:M7"].MergeCells = true;

                    DataTable dt = ds.Tables[c];

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        xlSheet.Cells[9, i + 1].value = dt.Columns[i].ToString().ToUpper();

                    }
                    xlSheet.Cells[9, 1].EntireRow.Font.Bold = true;

                    xlSheet.Columns.AutoFit();


                    int r = 10;

                    progressBar1.Visible = true;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = dt.Rows.Count;

                    //for (pgbar = 0; pgbar <= 200; pgbar++)
                    //{
                    //    progressBar1.Value = pgbar;
                    //}
                    // rows
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        for (var j = 0; j < dt.Columns.Count; j++)
                        {
                            xlSheet.Cells[i + r, j + 1] = dt.Rows[i][j];
                            progressBar1.Value = i;
                        }
                    }


                }


              

                #endregion



                
                Excel.Sheets autoSheet = xlWorkBook.Worksheets;
                autoSheet["Sheet1"].Delete();
                autoSheet["Sheet2"].Delete();
                autoSheet["Sheet3"].Delete();

                xlWorkBook.SaveAs(FileName);
                xlWorkBook.Close();
                Marshal.ReleaseComObject(xlApp);
                Marshal.ReleaseComObject(xlWorkBook);
               // Marshal.ReleaseComObject(Excel.Worksheet);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "You got an Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                foreach (Process process in Process.GetProcessesByName("Excel"))
                    process.Kill();
                MessageBox.Show("Successfully Exported");
                progressBar1.Maximum = 0;
                progressBar1.Visible = false;
            }
           

        }
                                   
        private void btnLoad_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;
            var clientId = ddlClient.SelectedValue;
            var commdityId = cmbCommodity.SelectedValue;
            var ImporterId = cmbImporter.SelectedValue;
            var ContainerNo = txtSearch.Text.Trim();
            var Blno = txtblno.Text.Trim();
            var ContSize = "";
            if (cmbContSize.SelectedIndex == 0)
            {
                ContSize = "";
            }
            else { ContSize = cmbContSize.Text.Trim(); }
            
            //var ContainerNo = 
            int typename = 0;
            var fromDate = dateFrom.Value;
            var toDate = dateTo.Value;
            if (RadioIn.Checked)
            {
                typename = 1;
            }
            else if (radioOut.Checked)
            {
                typename = 2;
            }
            else
            {
                typename = 3;
            }
            DataTable dt = new DataTable();
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Import_MLO_Wise_DailyInOutStock", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    cmd.Parameters.AddWithValue("@ContainerNo", ContainerNo);
                    cmd.Parameters.AddWithValue("@ContainerSize", ContSize);
                    cmd.Parameters.AddWithValue("@Blno", Blno);
                    cmd.Parameters.AddWithValue("@commdityId", commdityId);
                    cmd.Parameters.AddWithValue("@ImporterId", ImporterId);

                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    cmd.Parameters.AddWithValue("@TypeName", typename);
                    con.Open();
                  
                    dt.Load(cmd.ExecuteReader());
                    
                    dataGridView1.DataSource = dt;                   
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.ClearSelection();

                    con.Close();

                }
            }


            DataTable dt3 = new DataTable();
            dt3 = (DataTable)dataGridView1.DataSource;
           
            DataRow[] result_for_fourty = dt3.Select("Size > 20");
            int numberOfRecords_forty = (result_for_fourty.Length)*2;
            DataRow[] result_for_twenty = dt3.Select("Size <= 20");
            int numberOfRecords_twenty =  (result_for_twenty.Length) * 1;
            //int totalTuse = result * 2;
            int totalBox = dt.Rows.Count;
            int tues = numberOfRecords_twenty + numberOfRecords_forty;

            txtTotalBox.Text = Convert.ToString(totalBox);
            txtTotalTues.Text = Convert.ToString(tues);
        }

        private void cmbClient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ddlClient.SelectedIndex == 0)
            {
                btnLoad.Enabled = false;
                btnExcel.Enabled = false;

            }
            else
            {
                btnLoad.Enabled = true;
                btnExcel.Enabled = true;
            }
            labelControl1.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            ddlClient.SelectedValue = 0;
            
            cmbContSize.SelectedValue = 0;
            cmbConType.SelectedValue = 0;
            txtSearch.Text = "";
            
            dateFrom.Value = DateTime.Now;
            dateTo.Value = DateTime.Now;
            RadioIn.Checked = true;
            btnLoad.Enabled = false;
            btnExcel.Enabled = false;
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbSearch.SelectedIndex == 1)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlClient.Visible = false;
                cmbCommodity.Visible = false;
                cmbImporter.Visible = false;
            }
            // txtSearch
            if (cmbSearch.SelectedIndex == 2)
            {
                txtSearch.Visible = true;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlClient.Visible = false;
                txtblno.Visible = false;
                cmbCommodity.Visible = false;
                cmbImporter.Visible = false;
            }
            // cmbContSize
            if (cmbSearch.SelectedIndex == 3)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = true;
                cmbConType.Visible = true;
                ddlClient.Visible = false;
                txtblno.Visible = false;
                cmbCommodity.Visible = false;
                cmbImporter.Visible = false;
            }
            // ddlClient
            if (cmbSearch.SelectedIndex == 4)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlClient.Visible = true;
                txtblno.Visible = false;
                cmbCommodity.Visible = false;
                cmbImporter.Visible = false;
            }
            // txtblno
            if (cmbSearch.SelectedIndex == 5)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlClient.Visible = false;
                txtblno.Visible = true;
                cmbCommodity.Visible = false;
                cmbImporter.Visible = false;
            }
            // cmbCommodity
            if (cmbSearch.SelectedIndex == 6)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlClient.Visible = false;
                txtblno.Visible = false;
                cmbCommodity.Visible = true;
                cmbImporter.Visible = false;
            }
            // cmbImporter
            if (cmbSearch.SelectedIndex == 7)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlClient.Visible = false;
                txtblno.Visible = false;
                cmbCommodity.Visible = false;
                cmbImporter.Visible = true;
            }
            // cmbImporter
            if (cmbSearch.SelectedIndex == 8)
             {
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlClient.Visible = false;
                txtblno.Visible = false;
                cmbCommodity.Visible = false;
                cmbImporter.Visible = true;
            }
            
        }

       

        private void ddlClient_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlClient.SelectedIndex > 0)
            {
                int cusId = Convert.ToInt32(ddlClient.SelectedValue);

                var Customer = MLOBll.GetCustomerById(cusId);
                lblCustomerName.Text = Customer.CustomerName;
            }
        }

        private void btnexportsingle_Click(object sender, EventArgs e)
        {

        }
    }
}
