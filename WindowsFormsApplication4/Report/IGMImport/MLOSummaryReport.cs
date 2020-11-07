﻿using System;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;

namespace LOGISTIC.UI.Report
{
    public partial class MLOSummaryReport : Form
    {

        private CustomerBll MLOBll = new CustomerBll();
        private ImportReportBLL objBll = new ImportReportBLL();
        private ContainerTypeBll ctBll = new ContainerTypeBll();
        private ContainerSizeBll csBll = new ContainerSizeBll();

        public MLOSummaryReport()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            //btnLoad.Enabled = false;
            //btnExcel.Enabled = false;
            RadioIn.Checked = true;


        }

        private void MLOSummaryReport_Load(object sender, EventArgs e)
        {
            LoadCustomer();
            PrepareGrid();
            LoadcmbSearch();
            LoadContSize();
            LoadConType();
            progressBar1.Visible = false;
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

        private void LoadcmbSearch()
        {

            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Container Number");
            cmbSearch.Items.Insert(3, "Size");
            cmbSearch.Items.Insert(4, "MLO");
            cmbSearch.SelectedIndex = 0;
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
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].HeaderText = "MLO Code";
            dataGridView1.Columns[0].DataPropertyName = "MLO Code";

            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[1].HeaderText = "Size";
            dataGridView1.Columns[1].DataPropertyName = "Size";

            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[2].HeaderText = "Type";
            dataGridView1.Columns[2].DataPropertyName = "Type";

            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[3].HeaderText = "Box";
            dataGridView1.Columns[3].DataPropertyName = "Box";

            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[4].HeaderText = "Teus";
            dataGridView1.Columns[4].DataPropertyName = "Teus";
                        
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            var clientId = Convert.ToInt32(cmbClient.SelectedValue);
            var containerSize = "";
            if (cmbContSize.SelectedIndex == 0)
            {  containerSize = ""; }
            else {  containerSize = cmbContSize.Text.Trim(); }
            
            var ContainerNo = txtSearch.Text.Trim();
            var fromDate = dateFrom.Value;
            var toDate = dateTo.Value;

            DataTable dt = new DataTable();

            if (RadioIn.Checked)
            {
                dt = objBll.GetContainerSummaryReport(clientId, 1, fromDate, toDate, ContainerNo, containerSize);
            }
            else if (radioOut.Checked)
            {
                dt = objBll.GetContainerSummaryReport(clientId, 2, fromDate, toDate, ContainerNo, containerSize);
            }
            else
            {
                dt = objBll.GetContainerSummaryReport(clientId, 3, fromDate, toDate, ContainerNo, containerSize);
            }

            int totalBox = dt.AsEnumerable().Sum(r => r.Field<int>("Box"));
            int totalTuse = dt.AsEnumerable().Sum(r => r.Field<int>("Teus"));

            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();
            txtTotalBox.Text = Convert.ToString(totalBox);
            txtTotalTues.Text = Convert.ToString(totalTuse);
           
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //int ClientId = Convert.ToInt32(cmbClient.SelectedValue);
            //var containerSize = cmbContSize.Text.Trim();
            //var ContainerNo = txtSearch.Text.Trim();
            //DateTime fromDate = dateFrom.Value;
            //DateTime toDate = dateTo.Value;
            //string fDate = fromDate.ToString("dd MMM yy");
            //string tDate = toDate.ToString("dd MMM yy");
            int pgbar;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 2000;

            for (pgbar = 0; pgbar <= 200; pgbar++)
            {
                progressBar1.Value = pgbar;
            }

            var ClientId = Convert.ToInt32(cmbClient.SelectedValue);
            var containerSize = "";
            if (cmbContSize.SelectedIndex == 0)
            { containerSize = ""; }
            else { containerSize = cmbContSize.Text.Trim(); }

            var ContainerNo = txtSearch.Text.Trim();
            var fromDate = dateFrom.Value;
            var toDate = dateTo.Value;

            string fDate = fromDate.ToString("dd MMM yy");
            string tDate = toDate.ToString("dd MMM yy");

            try
            {

                List<string> SummType = new List<string>();
                SummType.Add("GATE IN");
                SummType.Add("DELIVERY");
                SummType.Add("STOCK");

                Excel.Application xlApp = new Excel.Application();

                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }
                xlApp.DisplayAlerts = false;

                Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
                Excel.Sheets worksheets = xlWorkBook.Worksheets;

                var xlSummary = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
               

                xlSummary.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 60, 0, 60, 40);
                xlSummary.Cells[1, 1].value = "EASTERN LOGISTICS LIMITED";
                xlSummary.Cells[1, 1].Font.Bold = true;
                xlSummary.Cells[1, 1].Font.Size = 15;
                xlSummary.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSummary.Range["A1:M1"].MergeCells = true;

                xlSummary.Cells[2, 1].value = " KATHGAR, NORTH PATENGA, CHATTOGRAM";
                xlSummary.Cells[2, 1].Font.Size = 10;
                xlSummary.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSummary.Range["A2:M2"].MergeCells = true;

                xlSummary.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: import@easternlogisticsltd.com";
                xlSummary.Cells[3, 1].Font.Size = 10;
                xlSummary.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSummary.Range["A3:M3"].MergeCells = true;

                if (ClientId > 0)  //Single MLO In Out and Stock datails
                {
                    DataSet ds = new DataSet();
                    ds = objBll.GetMLOWiseImportSummaryReport(ClientId, fromDate, toDate);

                    string FileName = "D:\\MLO wise Summary report from " + fDate + " to " + tDate + ".xlsx";
                    xlSummary.Name = "Summary report";

                    xlSummary.Cells[5, 1].value = " IMPORT CONTAINER SUMMARY OF " + cmbClient.Text.Trim();
                    xlSummary.Cells[5, 1].Font.Bold = true;
                    xlSummary.Cells[5, 1].Font.Size = 10;
                    xlSummary.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSummary.Range["A5:M5"].MergeCells = true;



                    int r = 8; // Initialize Excel Row Start Position  = 1

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {


                        xlSummary.Cells[r, 3].value = SummType[i];
                        xlSummary.Cells[r, 3].Font.Bold = true;
                        xlSummary.Cells[r, 3].Font.Size = 8;
                        xlSummary.Cells[r, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        //xlSummary.Range["A5:M5"].MergeCells = true;
                        //xlSummary.Range["C"+r+":D"+r].MergeCells = true;
                        r++;

                        //Writing Columns Header
                        xlSummary.Cells[r, 3].value = "MLO";
                        int col = 4;
                        for (int row = 0; row < ds.Tables[i].Rows.Count; row++)
                        {
                            string size = Convert.ToString(ds.Tables[i].Rows[row]["Size"]);
                            string type = Convert.ToString(ds.Tables[i].Rows[row]["Type"]);

                            xlSummary.Cells[r, col] = size.Trim() + " " + type.Trim();
                            col++;
                        }
                        xlSummary.Cells[r, col].value = "TOTAL BOXs";
                        col++;
                        xlSummary.Cells[r, col].value = "TOTAL TEUS";
                        xlSummary.Cells[r, 1].EntireRow.Font.Bold = true;
                        xlSummary.get_Range("A" + r, "M" + r).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        //End Columns Header
                        r++;


                        // Writing Rows into Excel Sheet
                        int totalBoxs = 0;
                        int totalTues = 0;

                        xlSummary.Cells[r, 3].value = cmbClient.Text.Trim();
                        int col2 = 4;
                        for (int row = 0; row < ds.Tables[i].Rows.Count; row++)
                        {
                            int box = Convert.ToInt32(ds.Tables[i].Rows[row]["BoxQty"]);
                            int teus = Convert.ToInt32(ds.Tables[i].Rows[row]["Teus"]);
                            xlSummary.Cells[r, col2] = box;


                            totalBoxs = totalBoxs + box;
                            totalTues = totalTues + teus;
                            col2++;
                        }
                        xlSummary.Cells[r, col2].value = totalBoxs;
                        col2++;

                        xlSummary.Cells[r, col2].value = totalTues;
                        col2++;

                        xlSummary.get_Range("A" + r, "M" + r).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        xlSummary.Columns.AutoFit();

                        // End of writing first row value
                        r = r + 2;

                    }
                    Excel.Sheets autoSheet = xlWorkBook.Worksheets;
                    
                    autoSheet["Sheet1"].Delete();
                    autoSheet["Sheet2"].Delete();
                    autoSheet["Sheet3"].Delete();
                    xlWorkBook.SaveAs(FileName);
                    xlWorkBook.Close();
                    xlApp.Quit();
                }
                else
                {
                    string xlFileName;
                    DataTable dt = new DataTable();

                    if (RadioIn.Checked)
                    {
                        dt = objBll.GetContainerSummaryReport(ClientId, 1, fromDate, toDate, ContainerNo, containerSize);
                        xlFileName = "GATE IN";
                    }
                    else if (radioOut.Checked)
                    {
                        dt = objBll.GetContainerSummaryReport(ClientId, 2, fromDate, toDate, ContainerNo, containerSize);
                        xlFileName = "DELIVERY";
                    }
                    else
                    {
                        dt = objBll.GetContainerSummaryReport(ClientId, 3, fromDate, toDate, ContainerNo, containerSize);
                        xlFileName = "STOCK";
                    }



                    string FileName = "D:\\Import Container Summary report from " + fDate + " to " + tDate + ".xlsx";
                    xlSummary.Name = xlFileName;

                    xlSummary.Cells[5, 1].value = " IMPORT CONTAINER "+ xlFileName + " SUMMARY ";
                    xlSummary.Cells[5, 1].Font.Bold = true;
                    xlSummary.Cells[5, 1].Font.Size = 10;
                    xlSummary.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSummary.Range["A5:M5"].MergeCells = true;
                                     

                    int r = 8; // Initialize Excel Row Start Position  = 1

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        xlSummary.Cells[r, i + 4].value = dt.Columns[i].ToString().ToUpper();

                    }
                    xlSummary.Cells[r, 1].EntireRow.Font.Bold = true;

                    xlSummary.Columns.AutoFit();


                    r++;

                    progressBar1.Visible = true;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = dt.Rows.Count;

                    // rows
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        for (var j = 0; j < dt.Columns.Count; j++)
                        {
                            xlSummary.Cells[i + r, j + 4] = dt.Rows[i][j];
                            progressBar1.Value = i;
                        }
                    }

                    Excel.Sheets autoSheet = xlWorkBook.Worksheets;
                    autoSheet[2].Delete();
                    xlWorkBook.SaveAs(FileName);
                    xlWorkBook.Close();
                    xlApp.Quit();

                }
                             
                Marshal.ReleaseComObject(xlApp);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlSummary);
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

            //MessageBox.Show("Successfully Exported");

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Clear()
        {
            dataGridView1.DataSource = null;
            txtTotalBox.Text = "";
            txtTotalTues.Text = "";
            cmbClient.SelectedValue = 0;
            cmbContSize.SelectedValue = 0;
            cmbConType.SelectedValue = 0;
            txtSearch.Text = "";
            RadioIn.Checked = true;
            dateFrom.Value = DateTime.Now;
            dateTo.Value = DateTime.Now;
               
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

        




       
    

