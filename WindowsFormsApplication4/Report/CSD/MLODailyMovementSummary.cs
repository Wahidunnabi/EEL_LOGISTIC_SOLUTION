using System;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using LOGISTIC.CSD.BLL;
using System.Linq;

namespace LOGISTIC.UI.Report
{
    public partial class MLODailyMovementSummary : Form
    {

        private CustomerBll MLOBll = new CustomerBll();
        private CSDReportBLL objBll = new CSDReportBLL();
        private ContainerSizeBll csBll = new ContainerSizeBll();
        private ContainerTypeBll ctBll = new ContainerTypeBll();

        public MLODailyMovementSummary()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);


        }


        private void MLODailyReport_Load(object sender, EventArgs e)
        {
            LoadCustomer();
            LoadConType();
            LoadContSize();
            PrepareGrid();
            RadioIn.Checked = true;
            labelControl1.Focus();
            progressBar1.Visible = false;
        }
        private void LoadConType()
        {

            var type = ctBll.Getall();
            cmbConType.DisplayMember = "ContainerTypeName";
            cmbConType.ValueMember = "ContainerTypeId";

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


        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 7;

            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[0].HeaderText = "MLO Name";
            dataGridView1.Columns[0].DataPropertyName = "MLO Name";

            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[1].HeaderText = "Size";
            dataGridView1.Columns[1].DataPropertyName = "Size";

            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "Type";
            dataGridView1.Columns[2].DataPropertyName = "Type";

            dataGridView1.Columns[3].Width = 110;
            dataGridView1.Columns[3].HeaderText = "Box Quantity";
            dataGridView1.Columns[3].DataPropertyName = "Box";

            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[4].HeaderText = "Teus";
            dataGridView1.Columns[4].DataPropertyName = "Teus";

            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[5].HeaderText = "From/To Location";
            dataGridView1.Columns[5].DataPropertyName = "DepotName";

            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[6].HeaderText = "Haulage";
            dataGridView1.Columns[6].DataPropertyName = "Haulage";


            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[6].HeaderText = "Vehicle In No";
            dataGridView1.Columns[6].DataPropertyName = "TrailerInNo";

        }



        private void btnLoad_Click(object sender, EventArgs e)
         {
            var clientId = Convert.ToInt32(ddlClient.SelectedValue);
            int SizeId = Convert.ToInt32(cmbContSize.SelectedValue);
            int TypeId = Convert.ToInt32(cmbConType.SelectedValue);
            var fromDate = dateFrom.Value;
            var toDate = dateTo.Value;

            DataTable dt = new DataTable();

            if (RadioIn.Checked)
            {
                dt = objBll.GetDailyInwardMovementSummary(clientId, fromDate, toDate, SizeId, TypeId);
            }
            else if (radioOut.Checked)
            {
                dt = objBll.GetDailyOutwardMovementSummary(clientId, fromDate, toDate, SizeId, TypeId);
            }
            else
            {
                dt = objBll.GetDailyStockSummary(clientId);
            }

            int totalBox = dt.AsEnumerable().Sum(r => r.Field<int>("Box"));
            int totalTuse = dt.AsEnumerable().Sum(r => r.Field<int>("Teus"));




            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;


            dataGridView2.DataSource = dt;
            dataGridView2.AllowUserToAddRows = false;


            txtTotalBox.Text = Convert.ToString(totalBox);
            txtTotalTues.Text = Convert.ToString(totalTuse);

        }


        private void btnExcel_Click(object sender, EventArgs e)
        {
            var clientId = Convert.ToInt32(ddlClient.SelectedValue);
            int SizeId = Convert.ToInt32(cmbContSize.SelectedValue);
            int TypeId = Convert.ToInt32(cmbConType.SelectedValue);
            var fromDate = dateFrom.Value;
            var toDate = dateTo.Value;

            DataTable dt = new DataTable();
            string MLO = clientId > 0 ? ddlClient.Text.Trim() : "All MLO";
            string FileName = "";
            string reportName = "";

            if (RadioIn.Checked)
            {
                dt = objBll.GetDailyInwardMovementSummary(clientId, fromDate, toDate, SizeId, TypeId);
                reportName = "Inward Movement";
                FileName = "D:\\" + reportName + " Summary Report of " + MLO + " from " + fromDate.Date.ToString("dd MMM yy") + " to " + toDate.Date.ToString("dd MMM yy") + ".xlsx";               
            }
            else if (radioOut.Checked)
            {
                dt = objBll.GetDailyOutwardMovementSummary(clientId, fromDate, toDate, SizeId, TypeId);
                reportName = "Outward Movement";
                FileName = "D:\\" + reportName + " Summary Report of " + MLO + " from " + fromDate.Date.ToString("dd MMM yy") + " to " + toDate.Date.ToString("dd MMM yy") + ".xlsx";
            }
            else
            {
                dt = objBll.GetDailyStockSummary(clientId);
                reportName = "Stock Movement";
                FileName = "D:\\" + reportName + " Summary Report of " + MLO + ".xlsx";
            }


           


            Excel.Application xlApp = new Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }

            xlApp.DisplayAlerts = false;
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
            Excel.Sheets worksheets = xlWorkBook.Worksheets;



            try
            {
                var xlSheet = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                #region INWARD


                xlSheet.Name = reportName;

                xlSheet.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 30, 5, 80, 50);
                xlSheet.Cells[1, 1].value = "EASTERN LOGISTICS LIMITED.";
                //ExcelWorkSheet.Cells[1, 1].FONT.NAME = "Calibri";
                xlSheet.Cells[1, 1].Font.Bold = true;
                xlSheet.Cells[1, 1].Font.Size = 15;
                xlSheet.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A1:M1"].MergeCells = true;

                xlSheet.Cells[2, 1].value = " KATHGAR, NORTH PATENGA, CHITTAGONG.";
                //xlSheet.Cells[2, 1].Font.Bold = true;
                xlSheet.Cells[2, 1].Font.Size = 10;
                xlSheet.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A2:M2"].MergeCells = true;

                xlSheet.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: csd@easternlogisticsltd.com";
                //xlSheet.Cells[3, 1].Font.Bold = true;
                xlSheet.Cells[3, 1].Font.Size = 10;
                xlSheet.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A3:M3"].MergeCells = true;

                xlSheet.Cells[5, 1].value = reportName  + " Summary Report of " + ddlClient.Text + " from " + fromDate.Date.ToString("dd/MMM/yy") + " to " + toDate.Date.ToString("dd/MMM/yy");
                xlSheet.Cells[5, 1].Font.Bold = true;
                xlSheet.Cells[5, 1].Font.Size = 11;
                xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A5:M5"].MergeCells = true;

              
                // column headings
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                   
                    xlSheet.Cells[7, i+4].value = dt.Columns[i].ToString().ToUpper();
                                    
                }
                xlSheet.Cells[7, 1].EntireRow.Font.Bold = true;

                xlSheet.Columns.AutoFit();


                int r = 8;
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dt.Rows.Count;

                // rows
                for (var i = 0; i < dt.Rows.Count; i++)
                {                 
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        xlSheet.Cells[i + r, j + 4] = dt.Rows[i][j];
                    }
                    progressBar1.Value = i;
                }


                #endregion


                xlSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlSheet.Select();

                Excel.Sheets autoSheet = xlWorkBook.Worksheets;
                autoSheet["Sheet1"].Delete();
                autoSheet["Sheet2"].Delete();
                autoSheet["Sheet3"].Delete();

                xlWorkBook.SaveAs(FileName);
                xlWorkBook.Close();
                Marshal.ReleaseComObject(xlApp);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlSheet);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            ddlClient.SelectedIndex = 0;
            dateFrom.Value = DateTime.Now;
            dateTo.Value = DateTime.Now;
            RadioIn.Checked = true;
            txtTotalBox.Text = "";
            txtTotalTues.Text = "";
            //btnLoad.Enabled = false;          
            //btnExcel.Enabled = false;

        }
        private void LoadContSize()
        {


            var type = csBll.Getall();
            cmbContSize.DisplayMember = "ContainerSize1";
            cmbContSize.ValueMember = "ContainerSizeId";
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
        private void ddlClient_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlClient.SelectedIndex > 0)
            {
                int cusId = Convert.ToInt32(ddlClient.SelectedValue);

                var Customer = MLOBll.GetCustomerById(cusId);
                lblCustomerName.Text = Customer.CustomerName;
            }

        }
    }

}

            
      