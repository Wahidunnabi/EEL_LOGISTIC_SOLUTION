using System;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LOGISTIC.REPORT;
using System.Collections.Generic;
using System.Reflection;

namespace LOGISTIC.UI.Report
{
    public partial class MLODailyReport : Form
    {

        private CustomerBll MLOBll = new CustomerBll();
        private ContainerSizeBll csBll = new ContainerSizeBll();
        private ContainerTypeBll ctBll = new ContainerTypeBll();
        public MLODailyReport()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);


        }

      
        private void MLODailyReport_Load(object sender, EventArgs e)
        {
            LoadCustomer();
            LoadContSize();
            LoadConType();
            PrepareGrid();
            btnLoad.Enabled = false;
            Preview.Enabled = false;
            btnExcel.Enabled = false;
            RadioIn.Checked = true;
            labelControl1.Focus();

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
            dataGridView1.ColumnCount = 13;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";
            dataGridView1.Columns[0].DataPropertyName = "SL";

            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[1].HeaderText = "Container No";
            dataGridView1.Columns[1].DataPropertyName = "ContNo";

            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "Size";
            dataGridView1.Columns[2].DataPropertyName = "Size";

            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[3].HeaderText = "Type";
            dataGridView1.Columns[3].DataPropertyName = "Type";


            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[4].HeaderText = "From/To ";
            dataGridView1.Columns[4].DataPropertyName = "DepotCode";


            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[5].HeaderText = "Vessel";
            dataGridView1.Columns[5].DataPropertyName = "vessel";

            //dataGridView1.Columns[5].Width = 80;
            //dataGridView1.Columns[5].HeaderText = "DepotCode";
            //dataGridView1.Columns[5].DataPropertyName = "DepotCode";

      
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[6].HeaderText = "Reg. No";
            dataGridView1.Columns[6].DataPropertyName = "rotation";

            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[7].HeaderText = "Booking No";
            dataGridView1.Columns[7].DataPropertyName = "BookingNoDump";


            dataGridView1.Columns[8].Width = 120;
            dataGridView1.Columns[8].HeaderText = "Haulier";
            dataGridView1.Columns[8].DataPropertyName = "HaulierNo";

           

            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[9].HeaderText = "Trailer No";
            dataGridView1.Columns[9].DataPropertyName = "TrailerInNo";


            dataGridView1.Columns[10].Width = 100;
            dataGridView1.Columns[10].HeaderText = "Status";
            dataGridView1.Columns[10].DataPropertyName = "StatusName";


            dataGridView1.Columns[11].Width = 100;
            dataGridView1.Columns[11].HeaderText = "Condition";
            dataGridView1.Columns[11].DataPropertyName = "ConditionName";

            dataGridView1.Columns[12].Width = 100;
            dataGridView1.Columns[12].HeaderText = "Remarks";
            dataGridView1.Columns[12].DataPropertyName = "RemarkIn";


        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var clientId = ddlClient.SelectedValue;
            var SizeId = cmbContSize.SelectedValue;
            var TypeId = cmbConType.SelectedValue;
            int typename = 0;
            string FileName = "";
            var fromDate = dtpFrom.Value;
            var toDate = dtpTo.Value;
            if (RadioIn.Checked)
            {
                typename = 1;
                FileName = "Inward";
            }
            else if (radioOut.Checked)
            {
                typename = 2;
                FileName = "Outward";
            }
            else
            {
                typename = 3;
                FileName = "Stock";
            }

            string fDate = fromDate.ToString("dd MMM yyyy");
            string tDate = toDate.ToString("dd MMM yyyy");
            //string sDate2 = "20 Aug 2017";

            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

           
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CSD_MLO_Wise_DailyReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate.Date);
                    cmd.Parameters.AddWithValue("@ToDate", toDate.Date);
                    cmd.Parameters.AddWithValue("@TypeName", typename);
                    cmd.Parameters.AddWithValue("@SizeId", SizeId);
                    cmd.Parameters.AddWithValue("@TypeId", TypeId);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        FileName = "D:\\" + FileName + " Report of " + ddlClient.Text.Trim() + " from " + fDate + " to " + tDate + ".xlsx";
                        Excel.Application xlApp = new Excel.Application();

                        if (xlApp == null)
                        {
                            MessageBox.Show("Excel is not properly installed!!");
                            return;
                        }


                        xlApp.DisplayAlerts = false;
                        //string filePath = "D:\\Daily Inward Report.xlsx";
                        //Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                        Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
                        Excel.Sheets worksheets = xlWorkBook.Worksheets;


                        //List<string> SheetNames = new List<string>();
                        //SheetNames.Add(sDate + " Inward");
                        //SheetNames.Add(sDate + " OUT");
                        //SheetNames.Add(sDate + " STOCK");


                        try
                        {
                            var xlSheet = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                          
                            #region INWARD

                            if (typename == 1)
                            {
                                xlSheet.Name = "INWARD REPORT";

                                xlSheet.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 95, 5, 80, 50);
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

                    

                                xlSheet.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: csd@easternlogisticsltd.com";
                                //xlSheet.Cells[3, 1].Font.Bold = true;
                                xlSheet.Cells[3, 1].Font.Size = 10;
                                xlSheet.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A3:M3"].MergeCells = true;

                                xlSheet.Cells[5, 1].value = "Inward Movement Report of " + ddlClient.Text.Trim() + " from " + fDate + " to " + tDate;
                                xlSheet.Cells[5, 1].Font.Bold = true;
                                xlSheet.Cells[5, 1].Font.Size = 11;
                                xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A5:M5"].MergeCells = true;



                                xlSheet.Cells[7, 1].value = "A/C Name:  " + lblCustomerName.Text.Trim();
                                xlSheet.Cells[7, 1].Font.Bold = true;
                                xlSheet.Cells[7, 1].Font.Size = 12;
                                xlSheet.Cells[7, 1].Font.color = Color.Blue;
                                xlSheet.Cells[7, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A7:M7"].MergeCells = true;


                                //xlSheet.Cells[7, 1].value = " A/C Name." + lblCustomerName.Text.Trim();
                                //xlSheet.Cells[7, 1].Font.Bold = true;
                                //xlSheet.Cells[7, 1].Font.Size = 10;
                                //xlSheet.Cells[7, 1].Font.color = Color.Blue;
                                //xlSheet.Cells[7, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                //xlSheet.Range["A7:M7"].MergeCells = true;

                                //ExcelWorkSheet.Columns[5].ColumnWidth = 30;
                                xlSheet.Cells[9, 1].value = "SL#";
                                xlSheet.Cells[9, 2].value = "CONTAINER No.";
                                xlSheet.Cells[9, 3].value = "SIZE";
                                xlSheet.Cells[9, 4].value = "TYPE";
                                //xlSheet.Cells[7, 5].value = "IN DATE";
                                xlSheet.Cells[9, 5].value = "From";
                                xlSheet.Cells[9, 6].value = "IMPORT VESSEL";
                                xlSheet.Cells[9, 7].value = "REG. No.";                               
                                xlSheet.Cells[9, 8].value = "BOOKING No.";
                                xlSheet.Cells[9, 9].value = "HAULAGE";
                                xlSheet.Cells[9, 10].value = "TRAILER No.";
                                xlSheet.Cells[9, 11].value = "STATUS";
                                xlSheet.Cells[9, 12].value = "CONDITION";                                
                                xlSheet.Cells[9, 13].value = "REMARKS";
                                xlSheet.Cells[9, 14].EntireRow.Font.Bold = true;
                                xlSheet.Columns.AutoFit();


                                int r = 10;
                                while (reader.Read())
                                {

                                    xlSheet.Cells[r, 1].value = reader["SL"].ToString();
                                    xlSheet.Cells[r, 2].value = reader["ContNo"].ToString();
                                    xlSheet.Cells[r, 3].value = reader["Size"].ToString();
                                    xlSheet.Cells[r, 4].value = reader["Type"].ToString();
                                    //String dt = reader["Indate"].ToString();
                                    //xlSheet.Cells[r, 5].value = String.Format("{0:dddd, MMMM d, yyyy}", dt);
                                    //xlSheet.Cells[r, 5].value = Indate.ToString("dd MMM yyyy"); "dd MMM yyyy"

                                    xlSheet.Cells[r, 5].value = reader["DepotCode"].ToString();
                                    xlSheet.Cells[r, 6].value = reader["vessel"].ToString();
                                    xlSheet.Cells[r, 7].value = reader["Registration"].ToString();
                                    xlSheet.Cells[r, 8].value = reader["BookingNoDump"].ToString();
                                    xlSheet.Cells[r, 9].value = reader["HaulierNo"].ToString();
                                    xlSheet.Cells[r, 10].value = reader["TrailerInNo"].ToString();
                                    xlSheet.Cells[r, 11].value = reader["StatusName"].ToString();
                                    xlSheet.Cells[r, 12].value = reader["ConditionName"].ToString();
                                    xlSheet.Cells[r, 13].value = reader["RemarkIn"].ToString();

                                    r++;
                                }
                               

                            }

                            #endregion


                            #region OUTWARD

                            if (typename == 2)
                            {
                                xlSheet.Name = "OUTWARD REPORT";

                                xlSheet.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 95, 5, 80, 50);
                                xlSheet.Cells[1, 1].value = "EASTERN LOGISTICS LIMITED.";
                                //ExcelWorkSheet.Cells[1, 1].FONT.NAME = "Calibri";
                                xlSheet.Cells[1, 1].Font.Bold = true;
                                xlSheet.Cells[1, 1].Font.Size = 15;
                                xlSheet.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A1:M1"].MergeCells = true;

                                xlSheet.Cells[2, 1].value = " KATHGAR, NORTH PATENGA, CHATTOGRAM.";
                                xlSheet.Cells[2, 1].Font.Bold = true;
                                //xlSheet.Cells[2, 1].Font.Size = 10;
                                xlSheet.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A2:M2"].MergeCells = true;

                                xlSheet.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: csd@easternlogisticsltd.com";
                               // xlSheet.Cells[3, 1].Font.Bold = true;
                                xlSheet.Cells[3, 1].Font.Size = 10;
                                xlSheet.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A3:M3"].MergeCells = true;

                                xlSheet.Cells[5, 1].value = "Outward Movement Report of " + ddlClient.Text.Trim() + " from " + fDate + " to " + tDate;
                                xlSheet.Cells[5, 1].Font.Bold = true;
                                xlSheet.Cells[5, 1].Font.Size = 11;
                                xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A5:M5"].MergeCells = true;



                                xlSheet.Cells[7, 1].value = "A/C Name:  " + lblCustomerName.Text.Trim();
                                xlSheet.Cells[7, 1].Font.Bold = true;
                                xlSheet.Cells[7, 1].Font.Size = 12;
                                xlSheet.Cells[7, 1].Font.color = Color.Blue;
                                xlSheet.Cells[7, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A7:M7"].MergeCells = true;

                                //xlSheet.Cells[7, 1].value = " A/C Name." + lblCustomerName.Text.Trim();
                                //xlSheet.Cells[7, 1].Font.Bold = true;
                                //xlSheet.Cells[7, 1].Font.Size = 10;
                                //xlSheet.Cells[7, 1].Font.color = Color.Blue;

                                //xlSheet.Cells[7, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                //xlSheet.Range["A7:M7"].MergeCells = true;


                                //ExcelWorkSheet.Columns[5].ColumnWidth = 30;
                                xlSheet.Cells[9, 1].value = "SL#";
                                xlSheet.Cells[9, 2].value = "CONTAINER NO";
                                xlSheet.Cells[9, 3].value = "SIZE";
                                xlSheet.Cells[9, 4].value = "TYPE";
                                xlSheet.Cells[9, 5].value = "SEAL NO";
                                xlSheet.Cells[9, 6].value = "OUT TO";
                                xlSheet.Cells[9, 7].value = "EXPORT VESSEL";
                                xlSheet.Cells[9, 8].value = "REG NO";
                                xlSheet.Cells[9, 9].value = "HAULAGE";
                                xlSheet.Cells[9, 10].value = "TRAILER NO";
                                xlSheet.Cells[9, 11].value = "BOOKING NO";
                                xlSheet.Cells[9, 12].value = "STUFFING DATE";
                                xlSheet.Cells[9, 13].value = "STATUS";                              
                                xlSheet.Cells[9, 14].value = "REMARKS";
                                xlSheet.Cells[9, 1].EntireRow.Font.Bold = true;
                                xlSheet.Columns.AutoFit();


                                int r = 10;
                                while (reader.Read())
                                {

                                    xlSheet.Cells[r, 1].value = reader["SL"].ToString();
                                    xlSheet.Cells[r, 2].value = reader["ContNo"].ToString();
                                    xlSheet.Cells[r, 3].value = reader["Size"].ToString();
                                    xlSheet.Cells[r, 4].value = reader["Type"].ToString();
                                    xlSheet.Cells[r, 5].value = reader["SealNo"].ToString();
                                    xlSheet.Cells[r, 6].value = reader["DepotCode"].ToString();
                                    xlSheet.Cells[r, 7].value = reader["Vessel"].ToString();
                                    xlSheet.Cells[r, 8].value = reader["Registration"].ToString();
                                    xlSheet.Cells[r, 9].value = reader["HaulierNo"].ToString();
                                    xlSheet.Cells[r, 10].value = reader["TrailerOutNo"].ToString();
                                    xlSheet.Cells[r, 11].value = reader["BookingNoOut"].ToString();
                                    xlSheet.Cells[r, 12].value = reader["StuffingDate"].ToString();
                                    xlSheet.Cells[r, 13].value = reader["StatusName"].ToString();
                                    xlSheet.Cells[r, 14].value = reader["RemarkOut"].ToString();

                                    r++;
                                }


                            }

                            #endregion


                            #region STOCK

                            if (typename == 3)
                            {
                                xlSheet.Name = "STOCK REPORT";

                                xlSheet.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 95, 5, 80, 50);
                                xlSheet.Cells[1, 1].value = "EASTERN LOGISTICS LIMITED.";
                                //ExcelWorkSheet.Cells[1, 1].FONT.NAME = "Calibri";
                                xlSheet.Cells[1, 1].Font.Bold = true;
                                xlSheet.Cells[1, 1].Font.Size = 15;
                                xlSheet.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A1:M1"].MergeCells = true;

                                xlSheet.Cells[2, 1].value = " KATHGAR, NORTH PATENGA, CHATTOGRAM.";
                               // xlSheet.Cells[2, 1].Font.Bold = true;
                                xlSheet.Cells[2, 1].Font.Size = 8;
                                xlSheet.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A2:M2"].MergeCells = true;

                                xlSheet.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: csd@easternlogisticsltd.com";
                               // xlSheet.Cells[3, 1].Font.Bold = true;
                                xlSheet.Cells[3, 1].Font.Size = 8;
                                xlSheet.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A3:M3"].MergeCells = true;

                                xlSheet.Cells[5, 1].value = "Stock Report of " + ddlClient.Text.Trim() + " on " + " from " + fDate + " to " + tDate;
                                    //DateTime.Now.Date.ToString("dd MMM yyyy");
                                xlSheet.Cells[5, 1].Font.Bold = true;
                                xlSheet.Cells[5, 1].Font.Size = 11;
                                xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A5:M5"].MergeCells = true;




                                xlSheet.Cells[7, 1].value = "A/C Name:  " + lblCustomerName.Text.Trim();
                                xlSheet.Cells[7, 1].Font.Bold = true;
                                xlSheet.Cells[7, 1].Font.Size = 12;
                                xlSheet.Cells[7, 1].Font.color = Color.Blue;
                                xlSheet.Cells[7, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                xlSheet.Range["A7:M7"].MergeCells = true;

                                //xlSheet.Cells[7, 1].value = " A/C Name." + lblCustomerName.Text.Trim();
                                //xlSheet.Cells[7, 1].Font.Bold = true;
                                //xlSheet.Cells[7, 1].Font.Size = 10;
                                //xlSheet.Cells[7, 1].Font.color = Color.Blue;
                                //xlSheet.Cells[7, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                //xlSheet.Range["A7:M7"].MergeCells = true;



                                //ExcelWorkSheet.Columns[5].ColumnWidth = 30;
                                xlSheet.Cells[9, 1].value = "SL#";
                                xlSheet.Cells[9, 2].value = "CONTAINER NO";
                                xlSheet.Cells[9, 3].value = "SIZE";
                                xlSheet.Cells[9, 4].value = "TYPE";
                                xlSheet.Cells[9, 5].value = "RECEIVE FROM";
                                xlSheet.Cells[9, 6].value = "IN DATE";
                                xlSheet.Cells[9, 7].value = "STORAGE DAY";
                                xlSheet.Cells[9, 8].value = "IMPORT VESSEL";
                                xlSheet.Cells[9, 9].value = "REG NO";
                                xlSheet.Cells[9, 10].value = "HAULAGE";
                                xlSheet.Cells[9, 11].value = "DUMPING DATE";
                                xlSheet.Cells[9, 12].value = "DUMP BOOKING NO";
                                xlSheet.Cells[9, 13].value = "STUFFING DATE";
                                xlSheet.Cells[9, 14].value = "STUFFING BOOKING NO";
                                xlSheet.Cells[9, 15].value = "STATUS";
                                xlSheet.Cells[9, 16].value = "CONDITION";
                                xlSheet.Cells[9, 17].value = "REMARKS";
                                xlSheet.Cells[9, 1].EntireRow.Font.Bold = true;
                                xlSheet.Columns.AutoFit();


                                int r = 10;
                                while (reader.Read())
                                {

                                    xlSheet.Cells[r, 1].value = reader["SL"].ToString();
                                    xlSheet.Cells[r, 2].value = reader["ContNo"].ToString();
                                    xlSheet.Cells[r, 3].value = reader["Size"].ToString();
                                    xlSheet.Cells[r, 4].value = reader["Type"].ToString();
                                    xlSheet.Cells[r, 5].value = reader["DepotCode"].ToString();
                                    xlSheet.Cells[r, 6].value = reader["InDate"].ToString();
                                    xlSheet.Cells[r, 7].value = reader["daysStayed"].ToString();
                                    xlSheet.Cells[r, 8].value = reader["Vessel"].ToString();
                                    xlSheet.Cells[r, 9].value = reader["Registration"].ToString();
                                    xlSheet.Cells[r, 10].value = reader["HaulierNo"].ToString();
                                    xlSheet.Cells[r, 11].value = reader["DumpingDate"].ToString();
                                    xlSheet.Cells[r, 12].value = reader["BookingNoDump"].ToString();
                                    xlSheet.Cells[r, 13].value = reader["StuffingDate"].ToString();
                                    xlSheet.Cells[r, 14].value = reader["BookingNo"].ToString();
                                    xlSheet.Cells[r, 15].value = reader["StatusName"].ToString();
                                    xlSheet.Cells[r, 16].value = reader["ConditionName"].ToString();
                                    xlSheet.Cells[r, 17].value = reader["RemarkIn"].ToString();

                                    r++;
                                }


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
                            con.Close();

                            MessageBox.Show("Successfully Exported");
                        }


                    }
                }



            }
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
        
        private void btnLoad_Click(object sender, EventArgs e)
        {
            
            var clientId = ddlClient.SelectedValue;
            var SizeId = cmbContSize.SelectedValue;
            var TypeId = cmbConType.SelectedValue;
            int typename = 0;
            int numberOfForty = 0;
            int numberOftwenty = 0;
            DataTable dt = new DataTable();
            DataTable Searchtable = new DataTable();
            var fromDate = dtpFrom.Value;
            var toDate = dtpTo.Value;
            if (RadioIn.Checked)
            {
                typename = 1;
            }
           if (radioOut.Checked)
            {
                typename = 2;
            }
            if (radioStock.Checked)
            {
                typename = 3;
            }

            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CSD_MLO_Wise_DailyReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate.Date);
                    cmd.Parameters.AddWithValue("@ToDate", toDate.Date);
                    cmd.Parameters.AddWithValue("@TypeName", typename);
                    cmd.Parameters.AddWithValue("@SizeId", SizeId);
                    cmd.Parameters.AddWithValue("@TypeId", TypeId);

                    con.Open();
                    dt.Load(cmd.ExecuteReader());


                    //IEnumerable<DataRow> list = dt.AsEnumerable();
                    //Searchtable = dt.AsEnumerable()
                    //.Where(r => r.Field<Int32>("Size") == Convert.ToInt32(cmbContSize.Text)).CopyToDataTable();
                    //if (Searchtable != null)
                    //{
                    //    dataGridView1.DataSource = Searchtable;
                    //}
                    //else
                    //{
                    //    dataGridView1.DataSource = Searchtable;
                    //}
                    dataGridView1.DataSource = dt;

                    dataGridView2.DataSource = dt;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.ClearSelection();
                    dataGridView2.DataSource = dt;
                    dataGridView2.AllowUserToAddRows = false;

                    dataGridView2.ClearSelection();
                    con.Close();

                }
            }
            
            int box = dt.Rows.Count;
            //int tues = Convert.ToInt32(dt.Rows.Find("20"));

            DataRow[] result = dt.Select("Size > 20");

            numberOfForty = result.Length;

            DataRow[] result1 = dt.Select("Size <= 20");

            numberOftwenty = result1.Length;

            txtTotalTues.Text = Convert.ToString(numberOftwenty * 1 + numberOfForty * 2);
            txtTotalBox.Text = box.ToString();

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
        private void cmbClient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ddlClient.SelectedIndex == 0)
            {
                btnLoad.Enabled = false;
                Preview.Enabled = false;
                btnExcel.Enabled = false;

            }
            else
            {
                btnLoad.Enabled = true;
                Preview.Enabled = true;
                btnExcel.Enabled = true;
            }
            labelControl1.Focus();
        }

        private void Preview_Click(object sender, EventArgs e)
        {
            if (RadioIn.Checked == true)
            {

                ReportDocument cryRpt = new ReportDocument();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;
                var APPPATH = Environment.CurrentDirectory + "\test.rpt";

                //crConnectionInfo.ServerName = "ELL-SERVER2";
                //crConnectionInfo.DatabaseName = "PORT_LOGISTIC";
                //crConnectionInfo.UserID = "sa";
                //crConnectionInfo.Password = "Sa@1234";
                crConnectionInfo.ServerName = LOGISTIC.UI.Properties.Settings.Default.DBSERVER;
                crConnectionInfo.DatabaseName = LOGISTIC.UI.Properties.Settings.Default.DATABASE;
                crConnectionInfo.UserID = LOGISTIC.UI.Properties.Settings.Default.USERID;
                crConnectionInfo.Password = LOGISTIC.UI.Properties.Settings.Default.PASSWORD;

                cryRpt.Load(@"D:\Report\" + "InwarReport.rpt");
                cryRpt.SetParameterValue("@ClientId", Convert.ToInt32(ddlClient.SelectedValue));
                cryRpt.SetParameterValue("@FromDate", Convert.ToDateTime(dtpFrom.Value));
                cryRpt.SetParameterValue("@ToDate", Convert.ToDateTime(dtpTo.Value));
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
            else if
              (radioStock.Checked == true)
            {

                ReportDocument cryRpt = new ReportDocument();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;
                var APPPATH = Environment.CurrentDirectory + "\test.rpt";

                //crConnectionInfo.ServerName = "sarwar-pc";
                //crConnectionInfo.DatabaseName = "PORT_LOGISTIC";
                //crConnectionInfo.UserID = "sa";
                //crConnectionInfo.Password = "sa";

                //crConnectionInfo.ServerName = "ELL-SERVER2";
                //crConnectionInfo.DatabaseName = "PORT_LOGISTIC";
                //crConnectionInfo.UserID = "sa";
                //crConnectionInfo.Password = "Sa@1234";

                crConnectionInfo.ServerName = LOGISTIC.UI.Properties.Settings.Default.DBSERVER;
                crConnectionInfo.DatabaseName = LOGISTIC.UI.Properties.Settings.Default.DATABASE;
                crConnectionInfo.UserID = LOGISTIC.UI.Properties.Settings.Default.USERID;
                crConnectionInfo.Password = LOGISTIC.UI.Properties.Settings.Default.PASSWORD;

                cryRpt.Load(@"D:\Report\" + "CsdstockReport.rpt");

                cryRpt.SetParameterValue("@ClientId", Convert.ToInt32(ddlClient.SelectedValue));
                cryRpt.SetParameterValue("@FromDate", Convert.ToDateTime(dtpFrom.Value));
                cryRpt.SetParameterValue("@ToDate", Convert.ToDateTime(dtpTo.Value));
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
            else
          if (radioOut.Checked == true)
            {
                ReportDocument cryRpt = new ReportDocument();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;
                var APPPATH = Environment.CurrentDirectory + "\test.rpt";

                //crConnectionInfo.ServerName = "sarwar-pc";
                //crConnectionInfo.DatabaseName = "PORT_LOGISTIC";
                //crConnectionInfo.UserID = "sa";
                //crConnectionInfo.Password = "sa";

                crConnectionInfo.ServerName = LOGISTIC.UI.Properties.Settings.Default.DBSERVER;
                crConnectionInfo.DatabaseName = LOGISTIC.UI.Properties.Settings.Default.DATABASE;
                crConnectionInfo.UserID = LOGISTIC.UI.Properties.Settings.Default.USERID;
                crConnectionInfo.Password = LOGISTIC.UI.Properties.Settings.Default.PASSWORD;

                cryRpt.Load(@"D:\Report\" + "OutWardReport.rpt");

                cryRpt.SetParameterValue("@ClientId", Convert.ToInt32(ddlClient.SelectedValue));
                cryRpt.SetParameterValue("@FromDate", Convert.ToDateTime(dtpFrom.Value));
                cryRpt.SetParameterValue("@ToDate", Convert.ToDateTime(dtpTo.Value));
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
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            ddlClient.SelectedIndex = 0;
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            RadioIn.Checked = true;
            btnLoad.Enabled = false;
            Preview.Enabled = false;
            btnExcel.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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
