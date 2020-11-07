using System;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace LOGISTIC.UI.Report
{
    public partial class ConsigneeDailyReceived : Form
    {

        private ConsigneeBll consigneeBll = new ConsigneeBll();
        private ContainerTypeBll ctBll = new ContainerTypeBll();
        private ContainerSizeBll csBll = new ContainerSizeBll();
        private ExportReportBLL objBll = new ExportReportBLL();
        private CustomerBll MLOBll = new CustomerBll();
        public ConsigneeDailyReceived()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);


        }
        private void LoadcmbSearch()
        {

            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Container Number");
            cmbSearch.Items.Insert(3, "Size");
            cmbSearch.Items.Insert(4, "MLO");
            cmbSearch.SelectedIndex = 0;
        }
        private void ImportMLODailyReport_Load(object sender, EventArgs e)
        {
            LoadConsignee();
            ddlConsignee.Visible = false;
            PrepareGrid();
            LoadCustomer();
            LoadcmbSearch();
            LoadContSize();
            LoadConType();

            //btnLoad.Enabled = false;
            //btnExcel.Enabled = false;           
            labelControl1.Focus();
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
        private void LoadCustomer()
        {

            var type = MLOBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Customer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                cmbClient.DataSource = dt_Types;
                cmbClient.DisplayMember = "t_Name";
                cmbClient.ValueMember = "t_ID";
            }
            cmbClient.SelectedIndex = 0;

        }

       
        private void LoadConsignee()
        {

            var type = consigneeBll.Getall();          
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ConsigneeId, t.ConsigneeName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = " All Consignee ";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlConsignee.DataSource = dt_Types;
                ddlConsignee.DisplayMember = "t_Name";
                ddlConsignee.ValueMember = "t_ID";
            }
            ddlConsignee.SelectedIndex = 0;

        }


        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            dataGridView1.EnableHeadersVisualStyles = false;           
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 10;



            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[0].HeaderText = "Consignee Name";
            dataGridView1.Columns[0].DataPropertyName = "Consignee";



            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].HeaderText = "Customer Name";
            dataGridView1.Columns[1].DataPropertyName = "MLO";

            dataGridView1.Columns[2].Width = 130;
            dataGridView1.Columns[2].HeaderText = "SHIPPER";
            dataGridView1.Columns[2].DataPropertyName = "Shipper";
           
            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[3].HeaderText = "LOT";
            dataGridView1.Columns[3].DataPropertyName = "Lot";

            dataGridView1.Columns[4].Width = 70;
            dataGridView1.Columns[4].HeaderText = "PACKAGES";
            dataGridView1.Columns[4].DataPropertyName = "Packages";

            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[5].HeaderText = "PIECES";
            dataGridView1.Columns[5].DataPropertyName = "Pieces";

            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[6].HeaderText = "LOCATION";
            dataGridView1.Columns[6].DataPropertyName = "Location";

            dataGridView1.Columns[7].Width = 150;
            dataGridView1.Columns[7].HeaderText = "DESTINATION";
            dataGridView1.Columns[7].DataPropertyName = "Destination";

            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[8].HeaderText = "CGO. Rcv. Date";
            dataGridView1.Columns[8].DataPropertyName = "CGO. Rcv. Date";

            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[9].HeaderText = "DOC. Rcv. Date";
            dataGridView1.Columns[9].DataPropertyName = "DOC. Rcv. Date";

           

        }


        private void btnLoad_Click(object sender, EventArgs e)
        {

            int consigneeId = Convert.ToInt32(cmbClient.SelectedValue);
            var ContSize = Convert.ToString(cmbContSize.Text.Trim());
            var containerNo = Convert.ToString(txtSearch.Text.Trim()); 

            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;

            DataTable dt = new DataTable();
            dt = objBll.GetConsigneeWiseDailyReceiving(consigneeId, fromDate, toDate, ContSize, containerNo);
            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();



        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            int consigneeId = Convert.ToInt32(cmbClient.SelectedValue);
            var ContSize = Convert.ToString(cmbContSize.Text.Trim());
            var containerNo = Convert.ToString(txtSearch.Text.Trim());
            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;
            string fDate = fromDate.ToString("dd MMM yy");
            string tDate = toDate.ToString("dd MMM yy");
            //string sDate = fromDate.ToString("dd MMM");


            DataTable dt = new DataTable();
            dt = objBll.GetConsigneeWiseDailyReceiving(consigneeId, fromDate, toDate, containerNo, ContSize);

            //string FileName = "D:\\Stuffing Report of " + ddlConsignee.Text.Trim() + " from " + fDate + " to " + tDate + ".xlsx";
            string FileName = "D:\\ MLO Wise Daily Cargo Receiving Report from " + fDate + " to " + tDate + ".xlsx";
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

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

               
                xlSheet.Name = "Daily Receiving";

                xlSheet.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 100, 0, 70, 45);
                xlSheet.Cells[1, 1].value = "EASTERN LOGISTICS LIMITED.";
                //ExcelWorkSheet.Cells[1, 1].FONT.NAME = "Calibri";
                xlSheet.Cells[1, 1].Font.Bold = true;
                xlSheet.Cells[1, 1].Font.Size = 15;
                xlSheet.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A1:M1"].MergeCells = true;

                xlSheet.Cells[2, 1].value = " KATHGAR, NORTH PATENGA, CHATTOGRAM.";
                //xlSheet.Cells[2, 1].Font.Bold = true;
                xlSheet.Cells[2, 1].Font.Size = 10;
                xlSheet.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A2:M2"].MergeCells = true;

                xlSheet.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: documentation@easternlogisticsltd.com";
                //xlSheet.Cells[3, 1].Font.Bold = true;
                xlSheet.Cells[3, 1].Font.Size = 10;
                xlSheet.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A3:M3"].MergeCells = true;

                xlSheet.Cells[5, 1].value = "Daily Cargo Receiving " + " from " + fromDate.ToString("dd MMM yy") + " to " + toDate.ToString("dd MMM yy");
                xlSheet.Cells[5, 1].Font.Bold = true;
                xlSheet.Cells[5, 1].Font.Size = 11;
                xlSheet.Cells[5, 1].Font.Color = Color.Brown;
                xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A5:M5"].MergeCells = true;


                // column headings
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    xlSheet.Cells[7, i + 1].value = dt.Columns[i].ToString().ToUpper();

                }
                xlSheet.Cells[7, 1].EntireRow.Font.Bold = true;


                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dt.Rows.Count;

                int r = 8;

                // rows
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        xlSheet.Cells[i + r, j + 1] = dt.Rows[i][j];
                    }
                    progressBar1.Value = i;
                }

                xlSheet.Columns.AutoFit();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClearForm()
        {

            ddlConsignee.SelectedIndex = 0;
            dataGridView1.DataSource = null;
            dateFrom.Value = DateTime.Now;
            dateTo.Value = DateTime.Now;
            labelControl1.Focus();

        }

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearch.SelectedIndex == 1)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                cmbClient.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 2)
            {
                txtSearch.Visible = true;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                cmbClient.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 3)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = true;
                cmbConType.Visible = true;
                cmbClient.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 4)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                cmbClient.Visible = true;
            }
        }
    }
}
