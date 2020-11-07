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
    public partial class HoursReportTest : Form
    {

        private CustomerBll MLOBll = new CustomerBll();
      
        public HoursReportTest()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);


        }

        private void ContainerSearch_Load(object sender, EventArgs e)
        {
            LoadCustomer();
           // PrepareGrid();
            btnLoad.Enabled = false;
            btnExcel.Enabled = false;
            radioIn.Checked = true;
            labelControl1.Focus();

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
            dr[1] = "--Select Customer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                cmbClient.DataSource = dt_Types;
                cmbClient.DisplayMember = "t_Name";
                cmbClient.ValueMember = "t_ID";
            }
            cmbClient.SelectedIndex = 0;

        }


        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.DataSource = null;
            //dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.ColumnCount = 7;

            dataGridView1.Columns[0].Width = 50;
            //dataGridView1.Columns[0].HeaderText = "SL#";
            //dataGridView1.Columns[0].DataPropertyName = "SL";


            dataGridView1.Columns[2].Width = 60;
            //dataGridView1.Columns[1].HeaderText = "Container No";
            //dataGridView1.Columns[1].DataPropertyName = "ContNo";


            dataGridView1.Columns[3].Width = 60;
            

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            int ClientId = Convert.ToInt32(cmbClient.SelectedValue);
            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;
            string sDate = fromDate.ToString("dd MMM");
            string lDate = fromDate.ToString("dd MMM yyyy");
            //string sDate2 = "20 Aug 2017";

            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GetMLOWiseDailyContainerStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", ClientId);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    con.Open();                                  

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        string FileName = "D:\\24 Hours Report of CMA on " + lDate + ".xlsx";
                        Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                        if (xlApp == null)
                        {
                            MessageBox.Show("Excel is not properly installed!!");
                            return;
                        }


                        xlApp.DisplayAlerts = false;
                        string filePath = "D:\\24 Hours Report of CMA on " + lDate + ".xlsx";
                        //Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                        Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
                        Excel.Sheets worksheets = xlWorkBook.Worksheets;


                        List<string> SheetNames = new List<string>();
                        SheetNames.Add(sDate + " IN");
                        SheetNames.Add(sDate + " OUT");
                        SheetNames.Add(sDate + " STOCK");


                        try
                        {


                            #region IN Status

                            var xlIN = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            xlIN.Name = SheetNames[0];
                          
                            xlIN.Cells[1, 1].value = "Eastern Logistics Ltd.";
                            //ExcelWorkSheet.Cells[1, 1].FONT.NAME = "Calibri";
                            xlIN.Cells[1, 1].Font.Bold = true;
                            xlIN.Cells[1, 1].Font.Size = 15;
                            xlIN.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            xlIN.Range["A1:M1"].MergeCells = true;

                            xlIN.Cells[2, 1].value = " DAILY IMPORT CONTAINER RECEIVE REPORT ON " + lDate;
                            xlIN.Cells[2, 1].Font.Bold = true;
                            xlIN.Cells[2, 1].Font.Size = 10;
                            xlIN.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            xlIN.Range["A2:M2"].MergeCells = true;

                            xlIN.Cells[3, 1].value = "A/C: CMC - COMPAGINE MARITIME D'AFFRETMENT(CMA)";
                            xlIN.Cells[3, 1].Font.Bold = true;
                            xlIN.Cells[3, 1].Font.Size = 10;
                            xlIN.Cells[3, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            xlIN.Range["A3:M3"].MergeCells = true;

                            xlIN.Cells[5, 1].value = "SL#";
                            xlIN.Cells[5, 2].value = "CONTAINER NO";
                            xlIN.Cells[5, 3].value = "SIZE";
                            xlIN.Cells[5, 4].value = "TYPE";
                            xlIN.Cells[5, 5].value = "IN DATE";
                            //ExcelWorkSheet.Columns[5].ColumnWidth = 30;
                            xlIN.Cells[5, 6].value = "MLO";
                            xlIN.Cells[5, 7].value = "IMPORTER";
                            xlIN.Cells[5, 8].value = "IMPORT VESSEL";
                            xlIN.Cells[5, 9].value = "ROTATION";
                            xlIN.Cells[5, 10].value = "COMMODITY";
                            xlIN.Cells[5, 11].value = "SEAL NO";
                            xlIN.Cells[5, 12].value = "CONDITION";
                            xlIN.Cells[5, 13].value = "VEHICLE NO";
                            xlIN.Cells[5, 1].EntireRow.Font.Bold = true;
                            xlIN.Columns.AutoFit();


                            int r = 6;
                            while (reader.Read())
                            {

                                xlIN.Cells[r, 1].value = reader["SL"].ToString();
                                xlIN.Cells[r, 2].value = reader["ContainerNo"].ToString();
                                xlIN.Cells[r, 3].value = reader["Size"].ToString();
                                xlIN.Cells[r, 4].value = reader["ContType"].ToString();
                                xlIN.Cells[r, 5].value = reader["GateInDate"].ToString();
                                xlIN.Cells[r, 6].value = reader["CustomerName"].ToString();
                                xlIN.Cells[r, 7].value = reader["ImporterName"].ToString();
                                xlIN.Cells[r, 8].value = reader["VesselName"].ToString();
                                xlIN.Cells[r, 9].value = reader["Rotation"].ToString();
                                xlIN.Cells[r, 10].value = reader["CommodityName"].ToString();
                                xlIN.Cells[r, 11].value = reader["SealNo"].ToString();
                                xlIN.Cells[r, 12].value = reader["ConditionName"].ToString();
                                xlIN.Cells[r, 13].value = reader["HaulierNo"].ToString();
                                r++;
                            }
                            #endregion



                            #region OUT Status

                            if (reader.NextResult())
                            {

                                var xlOUT = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                                xlOUT.Name = SheetNames[1];

                                xlOUT.Cells[1, 1].value = "Eastern Logistics Ltd.";                            
                                xlOUT.Cells[1, 1].Font.Bold = true;
                                xlOUT.Cells[1, 1].Font.Size = 15;
                                xlOUT.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                xlOUT.Range["A1:M1"].MergeCells = true;

                                xlOUT.Cells[2, 1].value = " DAILY IMPORT CONTAINER DELIVERY REPORT ON " + lDate;
                                xlOUT.Cells[2, 1].Font.Bold = true;
                                xlOUT.Cells[2, 1].Font.Size = 10;
                                xlOUT.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                xlOUT.Range["A2:M2"].MergeCells = true;

                                xlOUT.Cells[3, 1].value = "A/C: CMC - COMPAGINE MARITIME D'AFFRETMENT(CMA)";
                                xlOUT.Cells[3, 1].Font.Bold = true;
                                xlOUT.Cells[3, 1].Font.Size = 10;
                                xlOUT.Cells[3, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                xlOUT.Range["A3:M3"].MergeCells = true;

                                xlOUT.Cells[5, 1].value = "SL#";
                                xlOUT.Cells[5, 2].value = "CONTAINER NO";
                                xlOUT.Cells[5, 3].value = "SIZE";
                                xlOUT.Cells[5, 4].value = "TYPE";
                                xlOUT.Cells[5, 5].value = "IN DATE";
                                xlOUT.Cells[5, 6].value = "MLO";
                                xlOUT.Cells[5, 7].value = "IMPORTER";
                                xlOUT.Cells[5, 8].value = "C&F NAME";
                                xlOUT.Cells[5, 9].value = "IMPORT VESSEL";
                                xlOUT.Cells[5, 10].value = "ROTATION";
                                xlOUT.Cells[5, 11].value = "COMMODITY";
                                xlOUT.Cells[5, 12].value = "SEAL NO";
                                xlOUT.Cells[5, 13].value = "OUT DATE";
                                xlOUT.Cells[5, 14].value = "DAYS";
                                xlOUT.Cells[5, 15].value = "CONDITION";
                                xlOUT.Cells[5, 16].value = "DELIVERY TYPE";
                                xlOUT.Cells[5, 1].EntireRow.Font.Bold = true;
                                xlOUT.Columns.AutoFit();

                                int rw = 6;

                                while (reader.Read())
                                {
                                    xlOUT.Cells[rw, 1].value = reader["SL"].ToString();
                                    xlOUT.Cells[rw, 2].value = reader["ContainerNo"].ToString();
                                    xlOUT.Cells[rw, 3].value = reader["Size"].ToString();
                                    xlOUT.Cells[rw, 4].value = reader["ContType"].ToString();
                                    xlOUT.Cells[rw, 5].value = reader["GateInDate"].ToString();
                                    xlOUT.Cells[rw, 6].value = reader["CustomerName"].ToString();
                                    xlOUT.Cells[rw, 7].value = reader["ImporterName"].ToString();
                                    xlOUT.Cells[rw, 8].value = reader["CFAgentName"].ToString();
                                    xlOUT.Cells[rw, 9].value = reader["VesselName"].ToString();
                                    xlOUT.Cells[rw, 10].value = reader["Rotation"].ToString();
                                    xlOUT.Cells[rw, 11].value = reader["CommodityName"].ToString();
                                    xlOUT.Cells[rw, 12].value = reader["SealNo"].ToString();
                                    xlOUT.Cells[rw, 13].value = reader["GateOutDate"].ToString();
                                    xlOUT.Cells[rw, 14].value = reader["Days"].ToString();
                                    xlOUT.Cells[rw, 15].value = reader["RemarksOut"].ToString();
                                    xlOUT.Cells[rw, 16].value = reader["DeliveryType"].ToString();
                                    rw++;
                                }

                            }

                            #endregion



                            #region STOCK Status

                            if (reader.NextResult())
                            {

                                var xlSTOCK = (Excel.Worksheet)worksheets.Add(Type.Missing, worksheets[1], Type.Missing, Type.Missing);
                                xlSTOCK.Name = SheetNames[2];


                                xlSTOCK.Cells[1, 1].value = "Eastern Logistics Ltd.";
                                xlSTOCK.Cells[1, 1].Font.Bold = true;
                                xlSTOCK.Cells[1, 1].Font.Size = 15;
                                xlSTOCK.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                xlSTOCK.Range["A1:M1"].MergeCells = true;

                                xlSTOCK.Cells[2, 1].value = " IMPORT LADEN CONTAINER STOCK REPORT ON " + lDate;
                                xlSTOCK.Cells[2, 1].Font.Bold = true;
                                xlSTOCK.Cells[2, 1].Font.Size = 10;
                                xlSTOCK.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                xlSTOCK.Range["A2:M2"].MergeCells = true;

                                xlSTOCK.Cells[3, 1].value = "A/C: CMC - COMPAGINE MARITIME D'AFFRETMENT(CMA)";
                                xlSTOCK.Cells[3, 1].Font.Bold = true;
                                xlSTOCK.Cells[3, 1].Font.Size = 10;
                                xlSTOCK.Cells[3, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                xlSTOCK.Range["A3:M3"].MergeCells = true;

                                xlSTOCK.Cells[5, 1].value = "SL#";
                                xlSTOCK.Cells[5, 2].value = "CONTAINER NO";
                                xlSTOCK.Cells[5, 3].value = "SIZE";
                                xlSTOCK.Cells[5, 4].value = "TYPE";
                                xlSTOCK.Cells[5, 5].value = "IN DATE";
                                xlSTOCK.Cells[5, 6].value = "MLO";
                                xlSTOCK.Cells[5, 7].value = "IMPORTER";
                                xlSTOCK.Cells[5, 8].value = "IMPORT VESSEL";
                                xlSTOCK.Cells[5, 9].value = "ROTATION";
                                xlSTOCK.Cells[5, 10].value = "COMMODITY";
                                xlSTOCK.Cells[5, 11].value = "SEAL NO";
                                xlSTOCK.Cells[5, 12].value = "DAYS";
                                xlSTOCK.Cells[5, 13].value = "CONDITION";
                                xlSTOCK.Cells[5, 1].EntireRow.Font.Bold = true;
                                xlSTOCK.Columns.AutoFit();                               

                                int rw = 6;

                                while (reader.Read())
                                {
                                    xlSTOCK.Cells[rw, 1].value = reader["SL"].ToString();
                                    xlSTOCK.Cells[rw, 2].value = reader["ContainerNo"].ToString();
                                    xlSTOCK.Cells[rw, 3].value = reader["Size"].ToString();
                                    xlSTOCK.Cells[rw, 4].value = reader["ContType"].ToString();
                                    xlSTOCK.Cells[rw, 5].value = reader["GateInDate"].ToString();
                                    xlSTOCK.Cells[rw, 6].value = reader["CustomerName"].ToString();
                                    xlSTOCK.Cells[rw, 7].value = reader["ImporterName"].ToString();
                                    xlSTOCK.Cells[rw, 8].value = reader["VesselName"].ToString();
                                    xlSTOCK.Cells[rw, 9].value = reader["Rotation"].ToString();
                                    xlSTOCK.Cells[rw, 10].value = reader["CommodityName"].ToString();
                                    xlSTOCK.Cells[rw, 11].value = reader["SealNo"].ToString();
                                    xlSTOCK.Cells[rw, 12].value = reader["Days"].ToString();
                                    xlSTOCK.Cells[rw, 13].value = reader["ConditionName"].ToString();
                                    rw++;
                                }

                            }

                            #endregion

                            //Slect worksheet;
                            xlIN = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);
                            xlIN.Select();

                            Excel.Sheets autoSheet = xlWorkBook.Worksheets;
                            autoSheet["Sheet1"].Delete();
                            autoSheet["Sheet2"].Delete();
                            autoSheet["Sheet3"].Delete();

                            xlWorkBook.SaveAs(filePath);
                            xlWorkBook.Close();
                            Marshal.ReleaseComObject(xlApp);
                            Marshal.ReleaseComObject(xlWorkBook);
                            Marshal.ReleaseComObject(xlIN);
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
                        }


                    }
                }



            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var clientId = cmbClient.SelectedValue;
            var typename = "";
            var fromDate = dateFrom.Value;
            var toDate = dateTo.Value;
            if (radioIn.Checked)
            {
                typename = "IN";
            }
            else if (radioOut.Checked)
            {
                typename = "OUT";
            }
            else { typename = "STOCK"; }


            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GetMLOWiseContainerStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    cmd.Parameters.AddWithValue("@TypeName", typename);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);                   
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = dt;
                    // dataGridView1.ScrollBars = ScrollBars.None;
                    dataGridView1.Columns[0].Width = 50;                    
                    dataGridView1.Columns[2].Width = 60;                    
                    dataGridView1.Columns[3].Width = 60;

                    dataGridView1.AllowUserToAddRows = false;
                    con.Close();                    

                }
            }
        }

        private void cmbClient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbClient.SelectedIndex == 0)
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


    }
}
