using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using LOGISTIC.BLL;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Collections;
using LOGISTIC.UI.Resource;

namespace LOGISTIC.UI.Billing
{
    public partial class BillDetails : Form
    {

              
        private BillingBLL objBll = new BillingBLL();
        private static List<CSDBillDetail> listBillDetails = new List<CSDBillDetail>();

        private CSDBillSummary billSummary = new CSDBillSummary();
        private CustomerBll custBll = new CustomerBll();
        private Customer customer = new Customer();
        private CustomerBll MLOBll = new CustomerBll();
        public BillDetails()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
                  }

        public BillDetails(CSDBillSummary objbillSummery)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            customer = custBll.GetCustomerById(objbillSummery.CustId);
            billSummary = objbillSummery;           
            
            listBillDetails = objBll.GetClientCSDBillDetailsByRefNo(customer.CustomerId, billSummary.SummaryRefNo);

        }
        
       

        private void BillDetails_Load(object sender, EventArgs e)
        {
            ComboLoad();
            //PrepareGrid();
            //LoadDataToGrid();
            LoadCustomer();
           // LoadDataToGrid();

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

        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "All");
            cmbSearch.Items.Insert(1, "Container Number");
            cmbSearch.Items.Insert(2, "IGM Reference");
            cmbSearch.Items.Insert(3, "B/L Number");           
            cmbSearch.Items.Insert(4, "Rotation");           

            cmbSearch.SelectedIndex = 0;

        }


        //public void PrepareGrid()
        //{

        //    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
        //    dataGridView1.EnableHeadersVisualStyles = false;
        //    dataGridView1.AutoGenerateColumns = false;
        //    dataGridView1.ColumnCount = 13;


        //    dataGridView1.Columns[0].Width = 50;
        //    dataGridView1.Columns[0].HeaderText = "SL#";

        //    dataGridView1.Columns[1].Width = 80;
        //    dataGridView1.Columns[1].HeaderText = "Customer";

        //    dataGridView1.Columns[2].HeaderText = "Container Number";

        //    dataGridView1.Columns[3].Width = 40;
        //    dataGridView1.Columns[3].HeaderText = "Size";

        //    dataGridView1.Columns[4].Width = 40;
        //    dataGridView1.Columns[4].HeaderText = "Type";

        //    //dataGridView1.Columns[5].HeaderText = "GateIn Date";

        //    //dataGridView1.Columns[6].HeaderText = "Out Date";

        //    dataGridView1.Columns[5].Width = 80;
        //    dataGridView1.Columns[5].HeaderText = "Bill From";

        //    dataGridView1.Columns[6].Width = 80;
        //    dataGridView1.Columns[6].HeaderText = "Bill To";

        //    dataGridView1.Columns[7].HeaderText = "Service Name";           

        //    dataGridView1.Columns[8].Width = 60;
        //    dataGridView1.Columns[8].HeaderText = "Rate";

        //    dataGridView1.Columns[9].Width = 50;
        //    dataGridView1.Columns[9].HeaderText = "Days";

        //    dataGridView1.Columns[10].Width = 70;
        //    dataGridView1.Columns[10].HeaderText = "Amount";

        //    dataGridView1.Columns[11].Width = 50;
        //    dataGridView1.Columns[11].HeaderText = "VAT";

        //    dataGridView1.Columns[12].Width = 90;
        //    dataGridView1.Columns[12].HeaderText = "Total Amount";

        //}

        public void LoadDataToGrid(int CusId, DateTime fromDate , DateTime Todate)
        {
           

            //if (listBillDetails.Count > 0)
            //{
            //    int index = 1;
            //    foreach (var item in listBillDetails)
            //    {
            //        dataGridView1.Rows.Add(index, item.CustomerCode, item.ContainerNo, item.Size, item.Type, item.BillFrom.Value.ToString("dd/MMM/yyyy"), item.BillTo.Value.ToString("dd/MMM/yyyy"), item.ServiceName, item.Rate, item.Days,  item.Amount, item.VAT, item.Total);
            //        index = index + 1;
            //    }

            //}
            //else
            //{

            // listBillDetails = objBll.GetAllCSDBillDetailsByDateRange();
            //List<BillDetailsEntity> BillDetaillist = new List<BillDetailsEntity>();
            //DataTable BillDetailtable = objBll.GetAllCSDBillDetailsByMLOId(CusId,fromDate,Todate);
            //BillDetaillist = ConvertDataTable<BillDetailsEntity>(BillDetailtable);
            //int index = 1;
            //foreach (var item in listBillDetails)
            //{
            //    dataGridView1.Rows.Add(index, item.CustomerCode, item.ContainerNo, item.Size, item.Type, item.BillFrom.Value.Date.ToShortDateString(), item.BillTo.Value.Date.Date.ToShortDateString(), item.ServiceName, item.Rate, item.Days, item.Amount, item.VAT, item.Total);
            //    index = index + 1;
            //}
            // dataGridView1.AutoGenerateColumns = false;

            DataTable BillDetailtable = objBll.GetAllCSDBillDetailsByMLOId(CusId, fromDate, Todate);
            
            dataGridView1.DataSource = BillDetailtable;
            lbltotalRecord.Text = BillDetailtable.Rows.Count.ToString();

            //}
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.ClearSelection();

        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        private void RemoveDuplicates(DataTable table, List<string> keyColumns)
        {
            var uniqueness = new HashSet<string>();
            StringBuilder sb = new StringBuilder();
            int rowIndex = 0;
            DataRow row;
            DataRowCollection rows = table.Rows;
            int i = rows.Count;
            while (rowIndex < i)
            {
                row = rows[rowIndex];
                sb.Length = 0;
                foreach (string colname in keyColumns)
                {
                    sb.Append(row[colname]);
                    sb.Append("|");
                }

                if (uniqueness.Contains(sb.ToString()))
                {
                    rows.Remove(row);
                }
                else
                {
                    uniqueness.Add(sb.ToString());
                    rowIndex++;
                }
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string slect = cmbSearch.Text.Trim();
            string value = txtSearch.Text.Trim().ToString();


            if (slect != "All" && value == "")
            {
                MessageBox.Show(" Search text can't be empty");
                return;
            }

            switch (slect)
            {
                case "Container Number":
                    {
                        //var data = objBll.GetIGMImportDetailsByContNum(value);
                        //if (data.Count > 0)
                        //{
                        //    FilterGrid(data);
                        //}
                        //else
                        //{
                        //    dataGridView1.Rows.Clear();
                        //    MessageBox.Show("No data found !!");
                        //}

                        break;
                    }
                case "B/L Number":
                    {
                        //var data = objBll.GetIGMImportDetailsBLNumber(value);
                        //if (data.Count > 0)
                        //{
                        //    FilterGrid(data);
                        //}
                        //else
                        //{
                        //    dataGridView1.Rows.Clear();
                        //    MessageBox.Show("No data found !!");
                        //}
                        break;
                    }
                case "IGM Reference":
                    {
                        //var data = objBll.GetIGMImportDetailsByIGMNumber(value);
                        //if (data.Count > 0)
                        //{
                        //    FilterGrid(data);
                        //}
                        //else
                        //{
                        //    dataGridView1.Rows.Clear();
                        //    MessageBox.Show("No data found !!");
                        //}
                        break;
                    }
                case "Rotation":
                    {
                        //var data = objBll.GetIGMImportDetailsByRotation(value);
                        //if (data.Count > 0)
                        //{
                        //    FilterGrid(data);
                        //}
                        //else
                        //{
                        //    dataGridView1.Rows.Clear();
                        //    MessageBox.Show("No data found !!");
                        //}
                        break;
                    }
                default:
                    {
                        //if (listBillSummary.Count > 0)
                        //{
                        //    FilterGrid(listBillSummary);
                        //}
                        //else
                        //{
                        //    dataGridView1.Rows.Clear();
                        //    MessageBox.Show("No data to show !!");
                        //}
                        break;
                    }
            }

        }

        //public void FilterGrid(List<IGMImportDetail> objIGM)
        //{
        //    dataGridView1.Rows.Clear();

        //    int index = 0;
        //    foreach (var item in objIGM)
        //    {
        //        dataGridView1.Rows.Add(item.IGMDetailsId, item.IGMImport.BLnumber, item.ContainerNo, item.LineNo, item.ContainerSize.ContainerSize1, item.ContainerType.ContainerTypeName, item.SealNo, item.IGMImport.Rotation, item.Date, item.InOutStatus);
        //        if (item.InOutStatus == 1)
        //        {
        //            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Khaki;
        //        }
        //        else if (item.InOutStatus == 2)
        //        {
        //            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Goldenrod;
        //        }
        //        index = index + 1;


        //    }
        //    dataGridView1.ClearSelection();

        //}       

        private void ClearGrid()
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();            

        }

        private void BillDetails_FormClosing(object sender, FormClosingEventArgs e)
        {

            listBillDetails = new List<CSDBillDetail>();

        }

        private void btnExporttoExcel_Click(object sender, EventArgs e)
        {
           // int consigneeId = Convert.ToInt32(ddlCusCode.SelectedValue);
            DateTime fromDate = Convert.ToDateTime(dateFrom.Value);
            DateTime toDate = Convert.ToDateTime(dateTo.Value);
            int CusId = Convert.ToInt32(ddlCusCode.SelectedValue);
            string Cusstring = Convert.ToString(ddlCusCode.Text.ToString());
            string fDate = fromDate.ToString("dd MMM yyyy");
            string tDate = toDate.ToString("dd MMM yyyy");
            //string sDate = fromDate.ToString("dd MMM");


            DataTable dt = new DataTable();
            //dt = (DataTable)dataGridView1.DataSource; ;
            dt = objBll.GetAllCSDBillDetails(CusId, fromDate, toDate);

            string FileName = "D:\\Stock Report of " + ddlCusCode.Text.Trim() + " from " + fDate + " to " + tDate + ".xlsx";
            //string FileName = "D:\\MLO from " + fDate + " to " + tDate + ".xlsx";
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


                xlSheet.Name = " "+ ddlCusCode.Text+" Monthly Bill";
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

                xlSheet.Cells[5, 1].value = "Empty Container Bill of "+ ddlCusCode.Text.Trim() + " from " + fromDate.ToString("dd MMM yyyy") + " to " + toDate.ToString("dd MMM yyyy");
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

                        progressBar1.Value = i;
                    }
                }

                xlSheet.Columns.AutoFit();
                Excel.Sheets autoSheet = xlWorkBook.Worksheets;
                autoSheet[2].Delete();
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

            //if (listBillDetails.Count() > 0)
            //{

            //    //string SfromDate = billFrom.ToString("dd MMM");
            //    // string SfromDate = billSummary.BillFrom.Value.ToString("dd MMM yy");
            //    // string StoDate = billSummary.BillTo.Value.ToString("dd MMM yy");
            //    DateTime SfromDate = Convert.ToDateTime("01 Aug 2019");
            //    DateTime StoDate = Convert.ToDateTime("31 Aug 2019");

            //    string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(constring))
            //    {
            //        using (SqlCommand cmd = new SqlCommand("CSDMonthlyBillDetailsExport", con))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            //cmd.Parameters.AddWithValue("@ClientId", customer.CustomerId);
            //            //cmd.Parameters.AddWithValue("@RefNo", billSummary.SummaryRefNo);
            //            con.Open();
            //            DataTable dt = new DataTable();
            //            dt.Load(cmd.ExecuteReader());
            //            con.Close();

            //            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            //            if (xlApp == null)
            //            {
            //                MessageBox.Show("Excel is not properly installed!!");
            //                return;
            //            }


            //            xlApp.DisplayAlerts = false;
            //            string filePath = "D:\\Bill of " +  " from " + SfromDate + " to " + StoDate + ".xlsx";
            //            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
            //            Excel.Sheets worksheets = xlWorkBook.Worksheets;


            //            try
            //            {

            //                var xlIN = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //                xlIN.Name = "Bill Details";


            //                xlIN.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 240, 0, 60, 40);
            //                xlIN.Cells[1, 1].value = "EASTERN LOGISTICS LIMITED.";
            //                xlIN.Cells[1, 1].Font.Bold = true;
            //                xlIN.Cells[1, 1].Font.Size = 15;
            //                xlIN.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //                xlIN.Range["A1:S1"].MergeCells = true;

            //                xlIN.Cells[2, 1].value = " KATHGAR, NORTH PATENGA, CHITTAGONG.";
            //                xlIN.Cells[2, 1].Font.Size = 10;
            //                xlIN.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //                xlIN.Range["A2:S2"].MergeCells = true;

            //                xlIN.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: csd@easternlogisticsltd.com";
            //                xlIN.Cells[3, 1].Font.Size = 10;
            //                xlIN.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //                xlIN.Range["A3:S3"].MergeCells = true;

            //                xlIN.Cells[5, 1].value = customer.CustomerName.Trim() + " (" + customer.Agent.AgentName + " )";
            //                xlIN.Cells[5, 1].Font.Bold = true;
            //                xlIN.Cells[5, 1].Font.Size = 12;
            //                xlIN.Cells[5, 1].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SaddleBrown);
            //                xlIN.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //                xlIN.Range["A5:S5"].MergeCells = true;

            //                if (listBillDetails.Count() > 0)
            //                {
            //                    xlIN.Cells[6, 1].value = "Monthly Bill Details From " + SfromDate + " To " + StoDate;
            //                    xlIN.Cells[6, 1].Font.Bold = true;
            //                    xlIN.Cells[6, 1].Font.Size = 9;
            //                    xlIN.Cells[6, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;                               
            //                    xlIN.Range["A6:S6"].MergeCells = true;
            //                }
            //                else
            //                {
            //                    xlIN.Cells[6, 1].value = "Gate Out Bill Details From " + SfromDate + " To " + StoDate;
            //                    xlIN.Cells[6, 1].Font.Bold = true;
            //                    xlIN.Cells[6, 1].Font.Size = 9;
            //                    xlIN.Cells[6, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;                            
            //                    xlIN.Range["A6:S6"].MergeCells = true;
            //                }

            //                int? totalDays = 0;
            //                decimal? totalStorage = 0;
            //                decimal? totalLftOff = 0;
            //                decimal? totalDocIn = 0;
            //                decimal? totalTlrIn = 0;
            //                decimal? totalLftOn = 0;
            //                decimal? totalDocOut = 0;
            //                decimal? totalTlrOut = 0;
            //                decimal? totalVat = 0;
            //                decimal? totalAmount = 0;

            //                int r = 8;


            //                // column headings
            //                for (int i = 0; i < dt.Columns.Count; i++)
            //                {

            //                    xlIN.Cells[r, i+1].value = dt.Columns[i].ToString().ToUpper();

            //                }
            //                xlIN.Cells[r, 1].EntireRow.Font.Bold = true; 

            //                r++;                         
            //                // rows
            //                for (var i = 0; i < dt.Rows.Count; i++)
            //                {
            //                    for (var j = 0; j < dt.Columns.Count; j++)
            //                    {
            //                        xlIN.Cells[r, j+1] = dt.Rows[i][j];
            //                    }                                                             
            //                    r++;
            //                }

            //                totalDays = dt.AsEnumerable().Sum(x => x.Field<int?>("days"));
            //                totalStorage = dt.AsEnumerable().Sum(x => x.Field<decimal?>("Storage"));
            //                totalLftOff = dt.AsEnumerable().Sum(x => x.Field<decimal?>("Lift Off"));
            //                totalDocIn = dt.AsEnumerable().Sum(x => x.Field<decimal?>("Doc In"));
            //                totalTlrIn = dt.AsEnumerable().Sum(x => x.Field<decimal?>("trailer In"));
            //                totalLftOn = dt.AsEnumerable().Sum(x => x.Field<decimal?>("Lift On"));
            //                totalDocOut = dt.AsEnumerable().Sum(x => x.Field<decimal?>("Doc Out"));
            //                totalTlrOut = dt.AsEnumerable().Sum(x => x.Field<decimal?>("trailer Out"));
            //                totalVat = dt.AsEnumerable().Sum(x => x.Field<decimal?>("VAT"));
            //                totalAmount = dt.AsEnumerable().Sum(x => x.Field<decimal?>("Total"));
                        

            //                xlIN.Cells[r, 10].value = totalDays;
            //                xlIN.Cells[r, 12].value = totalStorage;
            //                xlIN.Cells[r, 13].value = totalLftOff;
            //                xlIN.Cells[r, 14].value = totalDocIn;
            //                xlIN.Cells[r, 15].value = totalTlrIn;
            //                xlIN.Cells[r, 16].value = totalLftOn;
            //                xlIN.Cells[r, 17].value = totalDocOut;
            //                xlIN.Cells[r, 18].value = totalTlrOut;
            //                xlIN.Cells[r, 19].value = totalVat;
            //                xlIN.Cells[r, 20].value = totalAmount;
            //                xlIN.Cells[r, 1].EntireRow.Font.Bold = true;
                            
            //                xlIN.Columns.AutoFit();

            //                Excel.Sheets autoSheet = xlWorkBook.Worksheets;
            //                autoSheet[2].Delete();

            //                xlWorkBook.SaveAs(filePath);
            //                xlWorkBook.Close();
            //                Marshal.ReleaseComObject(xlApp);
            //                Marshal.ReleaseComObject(xlWorkBook);
            //                Marshal.ReleaseComObject(xlIN);
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show("Exception: " + ex.Message, "You got an Error",
            //                MessageBoxButtons.OK, MessageBoxIcon.Error);

            //            }
            //            finally
            //            {
            //                foreach (Process process in Process.GetProcessesByName("Excel"))
            //                    process.Kill();
            //                con.Close();
            //            }
            //        }
            //    }

            //}
            //else
            //{
            //    MessageBox.Show("No bill summary found !!", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
                     

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            int clientId = 0;
            DateTime fromDate = Convert.ToDateTime(dateFrom.Value);
            DateTime Todate = Convert.ToDateTime(dateTo.Value);
            //clientId = Convert.ToInt32(ddlCusCode.SelectedValue);




            var resulr = Convert.ToInt32(ddlCusCode.SelectedValue);
            if (resulr == 0)
            {
                clientId = -99;
            }
            else
            {
                clientId = Convert.ToInt32(resulr);
            }
            LoadDataToGrid(clientId,fromDate, Todate);
        }
        //public static string NumberToWords(Int32 number)
        //{
        //    if (number == 0) { return "zero"; }
        //    if (number < 0) { return "minus " + NumberToWords(Math.Abs(number)); }
        //    string words = "";
        //    if ((number / 10000000) > 0) { words += NumberToWords(number / 10000000) + " Crore "; number %= 10000000; }
        //    if ((number / 100000) > 0) { words += NumberToWords(number / 100000) + " Lakh "; number %= 100000; }
        //    if ((number / 1000) > 0) { words += NumberToWords(number / 1000) + " Thousand "; number %= 1000; }
        //    if ((number / 100) > 0) { words += NumberToWords(number / 100) + " Hundred "; number %= 100; }
        //    if (number > 0)
        //    {
        //        if (words != "") { words += "and "; }
        //        var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        //        var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "seventy", "Eighty", "Ninety" };
        //        if (number < 20) { words += unitsMap[number]; }
        //        else { words += tensMap[number / 10]; if ((number % 10) > 0) { words += "-" + unitsMap[number % 10]; } }
        //    }
        //    return words;
        //}
        private void btnexporttop_Click(object sender, EventArgs e)
        {
            // int consigneeId = Convert.ToInt32(ddlCusCode.SelectedValue);
            DateTime fromDate = Convert.ToDateTime(dateFrom.Value);
            DateTime toDate = Convert.ToDateTime(dateTo.Value);
            int CusId = Convert.ToInt32(ddlCusCode.SelectedValue);
            string Cusstring = Convert.ToString(ddlCusCode.Text.ToString());
            string Customername = txtName.Text.ToString();
            string Address = txtaddress.Text.ToString();
            string fDate = fromDate.ToString("dd MMM yyyy");
            string tDate = toDate.ToString("dd MMM yyyy");
            //string sDate = fromDate.ToString("dd MMM");


            DataSet ds = new DataSet();
            //dt = (DataTable)dataGridView1.DataSource; ;
            ds = objBll.GetAllCSDBillDetailswithTop(CusId, fromDate, toDate);

            string FileName = "D:\\Empty Container Bill of " + ddlCusCode.Text.Trim() + " from " + fDate + " to " + tDate + ".xlsx";
            //string FileName = "D:\\MLO from " + fDate + " to " + tDate + ".xlsx";
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
            SheetNames.Add("DetailsSheet");
            SheetNames.Add("TopSheet");
            



            try
            {


                #region SheetDesign


                for (int c = 0; c < ds.Tables.Count; c++)
               {
                string sheetName = SheetNames[c];
                var xlSheet = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlSheet.Name = SheetNames[c];


               
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

                xlSheet.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: hasnat@easternlogisticsltd.com";
                //xlSheet.Cells[3, 1].Font.Bold = true;
                xlSheet.Cells[3, 1].Font.Size = 10;
                xlSheet.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A3:M3"].MergeCells = true;

                xlSheet.Cells[5, 1].value = "Empty Container Bill of " + ddlCusCode.Text.Trim() + " from " + fromDate.ToString("dd MMM yyyy") + " to " + toDate.ToString("dd MMM yyyy");
                xlSheet.Cells[5, 1].Font.Bold = true;
                xlSheet.Cells[5, 1].Font.Size = 11;
                xlSheet.Cells[5, 1].Font.Color = Color.Brown;
                xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A5:M5"].MergeCells = true;

                DataTable dt = ds.Tables[c];

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                        if (sheetName.Equals("TopSheet"))
                        {

                            xlSheet.Cells[8, 2].value = "Bill No : ";
                            xlSheet.Cells[8, 2].Font.Bold = true;
                            xlSheet.Cells[8, 2].Font.Size = 11;
                            xlSheet.Cells[8, 2].Font.Color = Color.Black;
                            xlSheet.Cells[8, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            xlSheet.Range["B8:D8"].MergeCells = true;
                    

                            xlSheet.Cells[10, 2].value = Address;
                            xlSheet.Cells[10, 2].Font.Bold = true;
                            xlSheet.Cells[10, 2].Font.Size = 11;
                            xlSheet.Cells[10, 2].Font.Color = Color.Black;
                            xlSheet.Cells[10, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            xlSheet.Range["B10:D10"].MergeCells = true;


                            xlSheet.Cells[12, 2].value = "A/C : " + Cusstring;
                            xlSheet.Cells[12, 2].Font.Bold = true;
                            xlSheet.Cells[12, 2].Font.Size = 11;
                            xlSheet.Cells[12, 2].Font.Color = Color.Black;
                            xlSheet.Cells[12, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            xlSheet.Range["B12:D12"].MergeCells = true;

                            xlSheet.Cells[14, 2].value = "Subject : Empty Container Bill.";
                            xlSheet.Cells[14, 2].Font.Bold = true;
                            xlSheet.Cells[14, 2].Font.Size = 11;
                            xlSheet.Cells[14, 2].Font.Color = Color.Black;
                            xlSheet.Cells[14, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            xlSheet.Range["B14:D14"].MergeCells = true;

                            xlSheet.Cells[16, 2].value = "Bill Period:"+ fDate+ "To "+ tDate;
                            xlSheet.Cells[16, 2].Font.Bold = true;
                            xlSheet.Cells[16, 2].Font.Size = 11;
                            xlSheet.Cells[16, 2].Font.Color = Color.Black;
                            xlSheet.Cells[16, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            xlSheet.Range["B16:D16"].MergeCells = true;

                            xlSheet.Cells[20, i + 2].value = dt.Columns[i].ToString().ToUpper();
                            xlSheet.Cells[20, i + 2].Font.Bold = true;
                            xlSheet.Cells[20, i + 2].Font.Size = 12;
                            xlSheet.Cells[20, i + 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                            xlSheet.Cells[20, i + 2].Font.color = Color.Black;
                            xlSheet.Cells[20, i + 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        }
                        else if (sheetName.Equals("DetailsSheet"))
                        {
                            xlSheet.Cells[7, 2].value = "A/C :"+ Customername.ToUpper(); // Customer Ac Name / full name
                            xlSheet.Cells[9, i + 1].value = dt.Columns[i].ToString().ToUpper();
                            xlSheet.Cells[9, i + 1].Font.Bold = true;
                            xlSheet.Cells[9, i + 1].Font.Size = 12;
                            xlSheet.Cells[9, i + 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                            xlSheet.Cells[9, i + 1].Font.color = Color.Black;

                            xlSheet.Cells[9, i + 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                           
                        }

                    }
                    xlSheet.Cells[9, 1].EntireRow.Font.Bold = true;

                    xlSheet.Columns.AutoFit();
                    int r = 0; int totalrow = dt.Rows.Count+21 ; int totalcolumn = dt.Columns.Count-1;
                    progressBar1.Visible = true;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = dt.Rows.Count;
                   
                    if (sheetName.Equals("TopSheet"))
                    {

                        double amount = Convert.ToInt32(dt.Compute("SUM(Total)", string.Empty));
                        double Vat = (amount / 100) * 15;
                        double fullamount = amount + Vat;
                        string Inwords = NumberToWords.ConvertAmount(fullamount); 
                        //DataView view = new DataView(dt);
                        //DataTable distinctValues = dt.DefaultView.ToTable(true, "Slno","ChargeName", "ServiceName", "Size", "Rate", "Unit", "Total");
                        //DataTable distinctValues = RemoveDuplicateRows(dt, "ServiceName");
                        //dt.DefaultView.ToTable(true);
                        r = 21;
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            for (var j = 0; j < dt.Columns.Count; j++)
                            {
                                xlSheet.Cells[i + r, j + 2] = dt.Rows[i][j];
                                xlSheet.Cells[i + r, j + 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                progressBar1.Value = i;
                            }
                        }
                       
                        xlSheet.Cells[totalrow, totalcolumn] = "Sub-Total:" ;
                        xlSheet.Cells[totalrow, totalcolumn + 2] = amount;
                        xlSheet.Cells[totalrow, totalcolumn].Font.Bold = true;
                        xlSheet.Cells[totalrow, totalcolumn+2].Font.Bold = true;

                        xlSheet.Cells[totalrow + 1, totalcolumn] = "VAT(15%):";
                        xlSheet.Cells[totalrow + 1, totalcolumn].Font.Bold = true;
                        xlSheet.Cells[totalrow + 1, totalcolumn + 2] = Vat;
                        xlSheet.Cells[totalrow + 1, totalcolumn + 2].font.bold = true;
                       
                        xlSheet.Cells[totalrow +2, totalcolumn] = "Total:";
                        xlSheet.Cells[totalrow +2, totalcolumn].Font.Bold = true;

                        xlSheet.Cells[totalrow + 2, totalcolumn+2] = fullamount;
                        xlSheet.Cells[totalrow + 2, totalcolumn+2].Font.Bold = true;
                        xlSheet.Cells[totalrow + 4, 2] = "In Word";
                        xlSheet.Cells[totalrow + 4, 2].Font.Bold = true;
                        xlSheet.Cells[totalrow + 4, 3]= Inwords;
                        xlSheet.Cells[totalrow + 4, 3].Font.Bold = true; 
                    }                           
                    else
                    {
                       
                        r = 10;
                        double TotalAmount = Convert.ToDouble(dt.Compute("SUM(Total)", string.Empty));
                        double talVat = Convert.ToDouble(dt.Compute("SUM(VAT)", string.Empty));
                        int taotalrow = dt.Rows.Count;
                        int tatoalCol = dt.Columns.Count;
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            for (var j = 0; j < dt.Columns.Count; j++)
                            {
                                xlSheet.Cells[i + r, j + 1] = dt.Rows[i][j];
                                xlSheet.Cells[i + r, j + 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                                progressBar1.Value = i;
                            }
                           
                        }
                        xlSheet.Cells[taotalrow + r + 1, tatoalCol-2] = "Total :";
                        xlSheet.Cells[taotalrow + r + 1, tatoalCol - 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlSheet.Cells[taotalrow + r + 1, tatoalCol - 2].Font.Bold = true;
                        xlSheet.Cells[taotalrow + r + 1, tatoalCol - 0].value = TotalAmount;
                        xlSheet.Cells[taotalrow + r + 1, tatoalCol - 0].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlSheet.Cells[taotalrow + r + 1, tatoalCol - 0].Font.Bold = true;
                        xlSheet.Cells[taotalrow + r + 1, tatoalCol - 1].value = talVat;
                        xlSheet.Cells[taotalrow + r + 1, tatoalCol - 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlSheet.Cells[taotalrow + r + 1, tatoalCol - 1].Font.Bold = true;


                    }

               

                
                //for (var i = 0; i < dt.Rows.Count; i++)
                //{
                //    for (var j = 0; j < dt.Columns.Count; j++)
                //    {
                //        xlSheet.Cells[i + r, j + 1] = dt.Rows[i][j];
                //        progressBar1.Value = i;
                //    }
                //}


            }




                #endregion


                Excel.Sheets autoSheet = xlWorkBook.Worksheets;
                autoSheet["Sheet1"].Delete();
                autoSheet["Sheet2"].Delete();
                autoSheet["Sheet3"].Delete();
                //// Deleting existing File
                if (File.Exists(Path.Combine("D:\\", FileName)))
                {
                    File.Delete(Path.Combine("D:\\", FileName));
                }


                /// Creating a New File
                xlWorkBook.SaveAs(FileName);
                xlWorkBook.Close();
                Marshal.ReleaseComObject(xlApp);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(autoSheet);

            

    
               //xlSheet.Columns.AutoFit();
                //Excel.Sheets autoSheet = xlWorkBook.Worksheets;
                //autoSheet[2].Delete();
                //xlWorkBook.SaveAs(FileName);
                //xlWorkBook.Close();
                //Marshal.ReleaseComObject(xlApp);
                //Marshal.ReleaseComObject(xlWorkBook);
                //Marshal.ReleaseComObject(xlSheet);
                //Marshal.ReleaseComObject(Excel.Worksheet);
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

                //MessageBox.Show("Successfully Exported");
                progressBar1.Maximum = 0;
                progressBar1.Visible = false;
                GC.Collect();
                if (System.IO.File.Exists(FileName))
                { System.Diagnostics.Process.Start(FileName); }
            }
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
        private void ddlCusCode_SelectedValueChanged(object sender, EventArgs e)
        {
           
            //objCustomer = (Customer)objCustomerlist.Where(x => (x.CustomerId == Convert.ToInt32(ddlCusCode.SelectedValue)));
            //Person oPerson = listPersonsInCity.Find(e => (e.SSN == "203456876"));


        }

        private void ddlCusCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            List<Customer> objCustomerlist = MLOBll.Getall();
            //Customer objCustomer = new Customer();
            foreach (Customer objCustomer in objCustomerlist.Where(x => (x.CustomerId.Equals(ddlCusCode.SelectedValue))))
            {
                txtName.Text = objCustomer.CustomerName.ToString();
                txtaddress.Text = objCustomer.Address.ToString();
            }
        }

        private void ddlCusCode_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void ddlCusCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<Customer> objCustomerlist = MLOBll.Getall();
            ////Customer objCustomer = new Customer();
            //foreach (Customer objCustomer in objCustomerlist.TakeWhile(x => (x.CustomerId.Equals(ddlCusCode.SelectedValue))))
            //{
            //    txtName.Text = objCustomer.CustomerName.ToString();
            //    txtaddress.Text = objCustomer.Address.ToString();
            //}
        }





        //private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        //{

        //    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
        //    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];


        //   var contStatus = Convert.ToString(selectedRow.Cells[9].Value);

        //    if (contStatus == "0")
        //    {
        //        DialogResult result = MessageBox.Show("Do you want to Gate In ??",
        //                  "Confirm IGM Import Gate In",
        //                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //        if (result == DialogResult.Yes)
        //        {
        //            var IGMDetailsId = Convert.ToInt32(selectedRow.Cells[0].Value);
        //            NavigateToGateIn(IGMDetailsId);                    
        //        }
        //    }
        //    else if (contStatus == "1")
        //    {
        //        DialogResult result = MessageBox.Show("Do you want to Gate Out ??",
        //                  "Confirm IGM Import Gate Out",
        //                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //        if (result == DialogResult.Yes)
        //        {
        //            var IGMDetailsId = Convert.ToInt32(selectedRow.Cells[0].Value);
        //            NavigateToGateOut(IGMDetailsId);
        //        }
        //    }
        //    else if (contStatus == "2")
        //    {
        //        MessageBox.Show("This container has already been Gate Out ??");
        //        dataGridView1.ClearSelection();                                                
        //    }

        //}

        //private void NavigateToGateIn(int IGMDetailsId)
        //{
        //    IGMImportDetail objIGMImportDetails = new IGMImportDetail();
        //    objIGMImportDetails = objBll.GetIGMImportDetailById(IGMDetailsId);

        //    IGMGateIn f = new IGMGateIn(objIGMImportDetails);
        //    f.MdiParent = this.ParentForm;
        //    f.Show();

        //}

        //private void NavigateToGateOut(int IGMDetailsId)
        //{
        //    IGMImportDetail objIGMdetails = new IGMImportDetail();
        //    objIGMdetails = objBll.GetIGMImportDetailById(IGMDetailsId);

        //    IGMGateOut f = new IGMGateOut(objIGMdetails);
        //    f.MdiParent = this.ParentForm;
        //    f.Show();

        //}
    }
    public class BillDetailsEntity
    {
        public Int64 Slno { get; set; }
        public String ContainerNo { get; set; }
        public string Size { get; set; }
        public string Typee { get; set; }
        public DateTime InDate { get; set; }
        public string BroughtFrom { get; set; }
        public DateTime DateOut { get; set; }
        public string OutTo { get; set; }
        public string BookingNoDump { get; set; }
        public DateTime BillFromDate { get; set; }
        public DateTime BillTodate { get; set; }
        public int StorageDays { get; set; }
        public int Rate { get; set; }
        public Decimal StorageValue { get; set; }
        public Decimal LiftOFFAmount { get; set; }
        public Decimal DocInAmount { get; set; }
        public Decimal TransInAmount { get; set; }
        public Decimal LiftOnAmount { get; set; }
        public Decimal DocOutAmount { get; set; }
        public Decimal TransOutAmount { get; set; }
        //public Decimal TransOutAmount { get; set; }
        public Decimal VAT { get; set; }
        public Decimal Total { get; set; }
    }
}
