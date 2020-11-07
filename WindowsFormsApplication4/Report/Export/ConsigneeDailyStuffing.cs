using System;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;

namespace LOGISTIC.UI.Report
{
    public partial class ConsigneeDailyStuffing : Form
    {

        private CustomerBll CustomerBll = new CustomerBll();

        private ContainerTypeBll ctBll = new ContainerTypeBll();
        private ContainerSizeBll csBll = new ContainerSizeBll();

        private FreightForwarderBLL forwarderBll = new FreightForwarderBLL();
        private ShipperBLL shipperBll = new ShipperBLL();
        List<Shipper> objShipperlist = new List<Shipper>();
        private ExportReportBLL objBll = new ExportReportBLL();
        public ConsigneeDailyStuffing()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);


        }

        private void ImportMLODailyReport_Load(object sender, EventArgs e)
        {
            LoadConsignee();
            PrepareGrid();                   
            labelControl1.Focus();
            LoadcmbSearch();
            LoadConType();
            LoadContSize();
            LoadShipper();
            LoadForwarder();
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
        private void LoadcmbSearch()
        {

            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Container Number");
            cmbSearch.Items.Insert(3, "Size");
            cmbSearch.Items.Insert(4, "MLO");
            cmbSearch.Items.Insert(5, "Shipper");
            cmbSearch.Items.Insert(6, "Freight Forwarder ");
            cmbSearch.Items.Insert(7, "EFR No");
            cmbSearch.SelectedIndex = 0;
        }
        private void LoadConsignee()
        {

            var type = CustomerBll.Getall();          
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = " All MLO ";
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
            dataGridView1.ColumnCount = 9;

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[0].HeaderText = "CONSIGNEE";
            dataGridView1.Columns[0].DataPropertyName = "Consignee";

            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[1].HeaderText = "SHIPPER";
            dataGridView1.Columns[1].DataPropertyName = "Shipper";
           
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "LOT";
            dataGridView1.Columns[2].DataPropertyName = "Lot";

            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[3].HeaderText = "PACKAGES";
            dataGridView1.Columns[3].DataPropertyName = "Packages";

            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[4].HeaderText = "PIECES";
            dataGridView1.Columns[4].DataPropertyName = "Pieces";

            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[5].HeaderText = "LOCATION";
            dataGridView1.Columns[5].DataPropertyName = "Location";

            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[6].HeaderText = "DESTINATION";
            dataGridView1.Columns[6].DataPropertyName = "Destination";

            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[7].HeaderText = "CGO. RECEIVED";
            dataGridView1.Columns[7].DataPropertyName = "CGO. Received";

            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[8].HeaderText = "DOC. RECEIVED";
            dataGridView1.Columns[8].DataPropertyName = "DOC. Received";

           

        }
       
        private void LoadShipper()
        {

            
            var type = shipperBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ShipperId, t.ShipperName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Shipper --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlShipper.DataSource = dt_Types;
                ddlShipper.DisplayMember = "t_Name";
                ddlShipper.ValueMember = "t_ID";
            }
            ddlShipper.SelectedIndex = 0;
        }
        
        private void LoadForwarder()
        {

            var type = forwarderBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.FreightForwarderId, t.FreightForwarderName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Forwarder --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlForwarder.DataSource = dt_Types;
                ddlForwarder.DisplayMember = "t_Name";
                ddlForwarder.ValueMember = "t_ID";
            }
            ddlForwarder.SelectedIndex = 0;

        }
        private void btnLoad_Click(object sender, EventArgs e)
        {

            int consigneeId = Convert.ToInt32(ddlConsignee.SelectedValue);
            int ShipperId = Convert.ToInt32(ddlShipper.SelectedValue);
            int frieghtForwarderId = Convert.ToInt32(ddlForwarder.SelectedValue);
            string EFRNO = txtEFRNO.Text.Trim();

            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;

            DataTable dt = new DataTable();
            dt = objBll.GetConsigneeWiseDailyStuffing(consigneeId, ShipperId, frieghtForwarderId, EFRNO, fromDate, toDate);
            DataTable dtunique = new DataTable();
            //RemoveDuplicateRows(dtunique,"Con")
           
            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();



        }
        
        private void btnExcel_Click(object sender, EventArgs e)
        {
            int consigneeId = Convert.ToInt32(ddlConsignee.SelectedValue);
            int ShipperId = Convert.ToInt32(ddlShipper.SelectedValue);
            int frieghtForwarderId = Convert.ToInt32(ddlForwarder.SelectedValue);
            string EFRNO = txtEFRNO.Text.Trim();

            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;
            string fDate = fromDate.ToString("dd MMM yyyy");
            string tDate = toDate.ToString("dd MMM yyyy");
            //string sDate = fromDate.ToString("dd MMM");
            
            DataTable dtduplicate = new DataTable();
            dtduplicate = objBll.GetConsigneeWiseDailyStuffing(consigneeId, ShipperId, frieghtForwarderId, EFRNO, fromDate, toDate);
            DataTable dt = new DataTable();
            //dt = RemoveDuplicateRows(dtduplicate, "CONTAINERNO");

            dt = FindDuplicateRows(dtduplicate, "CONTAINERNO");
            //dt = dtduplicate.DefaultView.ToTable("Distinct",true, "CONTAINERNO","SealNo", "Size", "Booking No", "CarrierCode", "Consignee");

            //string FileName = "D:\\Stuffing Report of " + ddlConsignee.Text.Trim() + " from " + fDate + " to " + tDate + ".xlsx";
            string FileName = "D:\\ MLO Wise Stuffing Report from " + fDate + " to " + tDate + ".xlsx";
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

               
                xlSheet.Name = "Stuffing Report";

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

                xlSheet.Cells[5, 1].value = "Stuffing Report of " + ddlConsignee.Text.Trim() + " from " + fromDate.ToString("dd MMM yyyy") + " to " + toDate.ToString("dd MMM yyyy");
                xlSheet.Cells[5, 1].Font.Bold = true;
                xlSheet.Cells[5, 1].Font.Size = 12;
                xlSheet.Cells[5, 1].Font.Color = Color.Blue;
                xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A5:M5"].MergeCells = true;


                // column headings
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    xlSheet.Cells[7, i + 1].value = dt.Columns[i].ToString().ToUpper();

                }
                xlSheet.Cells[7, 1].EntireRow.Font.Bold = true;
               
                int r = 8;


                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dt.Rows.Count;
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
        public DataTable FindDuplicateRows(DataTable table, string DistinctColumn)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string x = table.Rows[i][DistinctColumn].ToString();

                }
            }
            return table;
        }
        public DataTable RemoveDuplicateRows(DataTable table, string DistinctColumn)
        {
            try
            {
                ArrayList UniqueRecords = new ArrayList();
                ArrayList DuplicateRecords = new ArrayList();


                // Check if records is already added to UniqueRecords otherwise,
                // Add the records to DuplicateRecords
                foreach (DataRow dRow in table.Rows)
                {
                    if (UniqueRecords.Contains(dRow[DistinctColumn]))
                        DuplicateRecords.Add(dRow);

                    else
                        UniqueRecords.Add(dRow[DistinctColumn]);
                }

                // Remove duplicate rows from DataTable added to DuplicateRecords
                foreach (DataRow dRow in DuplicateRecords)
                {
                    table.Rows.Remove(dRow);
                }

                // Return the clean DataTable which contains unique records.
                return table;
            }
            catch (Exception ex)
            {
                return null;
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
            ddlShipper.SelectedIndex = 0;
            ddlForwarder.SelectedIndex = 0;
            txtEFRNO.Text = "";
            dataGridView1.DataSource = null;
            dateFrom.Value = DateTime.Now;
            dateTo.Value = DateTime.Now;
            labelControl1.Focus();

        }
        private void SearchClear()
        {
            ddlConsignee.SelectedIndex = 0;
            ddlShipper.SelectedIndex = 0;
            ddlForwarder.SelectedIndex = 0;
            txtEFRNO.Text = "";
        }

        private void btnAllexport_Click(object sender, EventArgs e)
        {
            int ClientId = Convert.ToInt32(ddlConsignee.SelectedValue);
            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;
            string fDate = fromDate.ToString("dd MMM yy");
            string tDate = toDate.ToString("dd MMM yy");
            //string sDate = fromDate.ToString("dd MMM");


            DataSet ds = new DataSet();
            ds = objBll.GetConsigneeWiseDailyStatus(ClientId, fromDate, toDate);

            string FileName = "D:\\Status Report of " + ddlConsignee.Text.Trim() + " from " + fDate + " to " + tDate + ".xlsx";
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

            SheetNames.Add("Balance Cargo");
            SheetNames.Add("Stuffing");
            SheetNames.Add("Cargo Receiving");


            try
            {


                #region IN Status


                for (int c = 0; c < ds.Tables.Count; c++)
                {

                    var xlSheet = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    xlSheet.Name = SheetNames[c];

                    xlSheet.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 150, 0, 60, 40);
                    xlSheet.Cells[1, 1].value = "Eastern Logistics Ltd.";
                    xlSheet.Cells[1, 1].Font.Bold = true;
                    xlSheet.Cells[1, 1].Font.Size = 15;
                    xlSheet.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSheet.Range["A1:M1"].MergeCells = true;

                    xlSheet.Cells[2, 1].value = " KATHGAR, NORTH PATENGA, CHITTAGONG.";
                    xlSheet.Cells[2, 1].Font.Size = 10;
                    xlSheet.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSheet.Range["A2:M2"].MergeCells = true;

                    xlSheet.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: documentation@easternlogisticsltd.com";
                    xlSheet.Cells[3, 1].Font.Size = 10;
                    xlSheet.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSheet.Range["A3:M3"].MergeCells = true;



                    if (xlSheet.Name == "Balance Cargo")
                    { 
                    xlSheet.Cells[5, 1].value = "Export " + SheetNames[0] + " Report of " + ddlConsignee.Text.Trim() + " from " + fDate + " to " + tDate;
                    xlSheet.Cells[5, 1].Font.Bold = true;
                    xlSheet.Cells[5, 1].Font.Size = 12;
                    xlSheet.Cells[5, 1].Font.color = Color.Blue;
                    xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSheet.Range["A5:M5"].MergeCells = true;
                    }
                    if (xlSheet.Name == "Stuffing")
                    {
                        xlSheet.Cells[5, 1].value = "Export Container " + SheetNames[1] + " Report of " + ddlConsignee.Text.Trim() + " from " + fDate + " to " + tDate;
                        xlSheet.Cells[5, 1].Font.Bold = true;
                        xlSheet.Cells[5, 1].Font.Size = 12;
                        xlSheet.Cells[5, 1].Font.color = Color.Blue;
                        xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        xlSheet.Range["A5:M5"].MergeCells = true;
                    }
                    if (xlSheet.Name == "Cargo Receiving")
                    {
                        xlSheet.Cells[5, 1].value = "Export " + SheetNames[2] + " Report of " + ddlConsignee.Text.Trim() + " from " + fDate + " to " + tDate;
                        xlSheet.Cells[5, 1].Font.Bold = true;
                        xlSheet.Cells[5, 1].Font.Size = 12;
                        xlSheet.Cells[5, 1].Font.color = Color.Blue;
                        xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        xlSheet.Range["A5:M5"].MergeCells = true;
                    }

                    DataTable dt = ds.Tables[c];

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        xlSheet.Cells[7, i + 1].value = dt.Columns[i].ToString().ToUpper();

                    }
                    xlSheet.Cells[7, 1].EntireRow.Font.Bold = true;

                    xlSheet.Columns.AutoFit();



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

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearch.SelectedIndex == 0)
            {
                //SearchClear();
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlConsignee.Visible = false;
                ddlShipper.Visible = false;
                ddlForwarder.Visible = false;
                txtEFRNO.Visible = false;
            }

            if (cmbSearch.SelectedIndex == 1)
            {
                SearchClear();
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlConsignee.Visible = false;
                ddlShipper.Visible = false;
                ddlForwarder.Visible = false;
                txtEFRNO.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 2)
            {
                SearchClear();
                txtSearch.Visible = true;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlConsignee.Visible = false;
                ddlShipper.Visible = false;
                ddlForwarder.Visible = false;
                txtEFRNO.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 3)
            {
                SearchClear();
                txtSearch.Visible = false;
                cmbContSize.Visible = true;
                cmbConType.Visible = true;
                ddlConsignee.Visible = false;
                ddlShipper.Visible = false;
                ddlForwarder.Visible = false;
                txtEFRNO.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 4)
            {
                SearchClear();
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlConsignee.Visible = true;
                ddlShipper.Visible = false;
                ddlForwarder.Visible = false;
                txtEFRNO.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 5)
            {
                SearchClear();
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlConsignee.Visible = false;
                ddlShipper.Visible = true;
                ddlForwarder.Visible = false;
                txtEFRNO.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 6)
            {
                SearchClear();
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlConsignee.Visible = false;
                ddlShipper.Visible = false;
                ddlForwarder.Visible = true;
                txtEFRNO.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 7)
            {
                SearchClear();
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlConsignee.Visible = false;
                ddlShipper.Visible = false;
                ddlForwarder.Visible = false;
                txtEFRNO.Visible = true;
            }
        }
    }
}
