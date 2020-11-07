using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Windows.Forms;
using LOGISTIC.BLL;
using LOGISTIC.Export.BLL;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LOGISTIC.REPORT;

namespace LOGISTIC.UI.Export
{
    public partial class TerminalReceipt : Form
    {

        private TRBLL objBll = new TRBLL();
        private CargoReceivingBLL CRBll = new CargoReceivingBLL();
        private CustomerBll customerBll = new CustomerBll();
        private PortBLL portBll = new PortBLL();
        private UserBLL userBll = new UserBLL();


        private TRReportData objTR = new TRReportData();
        private List<TRReportData>  listTR = new List<TRReportData>();
        

        public TerminalReceipt()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            ddlEFRNo.Enabled = false;
            btnPrintTR.Enabled = false;

        }

        private void TerminalReceipt_Load(object sender, EventArgs e)
        {
            LoadPort();
            LoadMLO();
            PrepareGrid();
            LoadDatatoGrid();

        }

        private void LoadMLO()
        {

            var type = customerBll.Getall();          
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select MLO --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlMLO.DataSource = dt_Types;
                ddlMLO.DisplayMember = "t_Name";
                ddlMLO.ValueMember = "t_ID";
            }
            ddlMLO.SelectedIndex = 0;

        }


        private void LoadPort()
        {

            var type = portBll.Getall();
                      
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.PortOfLandId, t.PortName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Delivery Port --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlDelivery.DataSource = dt_Types;
                ddlDelivery.DisplayMember = "t_Name";
                ddlDelivery.ValueMember = "t_ID";
            }
            ddlDelivery.SelectedIndex = 0;

        }

        private void LoadEFRNo(int custId)
        {

            var type = CRBll.GetCargoReceivingList(custId);


            if (type.Count > 0)
            {

                DataTable dt_Types = new DataTable();
                dt_Types.Columns.Add("t_ID", typeof(int));
                dt_Types.Columns.Add("t_Name", typeof(string));
                foreach (var t in type)
                {
                    dt_Types.Rows.Add(t.CargoReceiveId, t.EFRNo.Trim());
                }
                DataRow dr = dt_Types.NewRow();
                dr[0] = 0;
                dr[1] = "--Select EFR No--";
                dt_Types.Rows.InsertAt(dr, 0);
                if (dt_Types.Rows.Count > 0)
                {
                    ddlEFRNo.DataSource = dt_Types;
                    ddlEFRNo.DisplayMember = "t_Name";
                    ddlEFRNo.ValueMember = "t_ID";
                }
                ddlEFRNo.Enabled = true;
            }
            else
            {
                ddlEFRNo.DataSource = null;
                //ddlEFRNo.Items.Clear();
                ddlEFRNo.Items.Insert(0, "No EFR Found !!");


            }

            ddlEFRNo.SelectedIndex = 0;

        }      

        public void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[1].HeaderText = "EFR No";

            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[2].HeaderText = "TR Number";

            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[3].HeaderText = "Date";

           
        }

        private void LoadDatatoGrid()
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listTR = objBll.Getall();

            int index = 1;
            foreach (var item in listTR)
            {

                dataGridView1.Rows.Add(index, item.EFRNo, item.TRNo, item.Date);
                index = index + 1;


            }

            dataGridView1.ClearSelection();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void BindSearchDatatoGrid(CargoRecieving objCR)
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            PrepareGrid();

            int index = 0;

            foreach (var item in objCR.CargoDetails)
            {

                dataGridView1.Rows.Add(item.CartoonId, item.Height, item.Length, item.Width, item.CubicMeter, (item.IsStuffed == true) ? "Yes" : "No");

                if (item.IsStuffed == true)
                {
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Khaki;
                }
                //else if (item.InOutStatus == 2)
                //{
                //    dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Goldenrod;
                //}
                index = index + 1;


            }

            dataGridView1.ClearSelection();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            //int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            //DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            //objCargoDetails = objCargoReceive.CargoDetails.ElementAt(selectedrowindex);

            ////var stuffed = Convert.ToString(selectedRow.Cells[5].Value);


            //if (objCargoDetails.IsStuffed == true)
            //{
            //    DialogResult result = MessageBox.Show("Do you want to update this stuffing ??",
            //              "Confirm Stuffing Update",
            //              MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //    if (result == DialogResult.Yes)
            //    {
            //        objStuffingDetails = stuffingBll.GetStuffingDetailsByCargoDetailsId(objCargoDetails.CargoDetailsId);
            //        if (objStuffingDetails.StuffingDetailsId > 0)
            //        {
            //            LoadCSDContainerByClientId(objCargoReceive.CustId);
            //            BindStuffingDataToField(objStuffingDetails);
            //            ddlCarrier.Enabled = false;
            //            btnSave.Text = "Update";
            //        }

            //    }
            //}
            //else
            //{
            //    DialogResult result = MessageBox.Show("Do you want to stuffing ??",
            //              "Confirm Stuffing",
            //              MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //    if (result == DialogResult.Yes)
            //    {
            //        ddlCarrier.SelectedValue = objCargoReceive.CustId;
            //        ddlCarrier.Enabled = false;
            //        ddlLocation.SelectedValue= objCargoReceive.Location;
            //        ddlLocation.Enabled = false;

            //        LoadCSDContainerByClientId(objCargoReceive.CustId);

            //        // selectedCDId = objCargoReceive.CargoDetails.ElementAt(selectedrowindex).CargoDetailsId;
            //        //txtRefNo.Text = stuffingBll.GetStuffingRefNo(objCargoReceive.CustId);
            //        //var IGMDetailsId = Convert.ToInt32(selectedRow.Cells[0].Value);
            //        //NavigateToGateOut(IGMDetailsId);
            //    }
        }


        //if (stuffed == "Yes")
        //{
        //    DialogResult result = MessageBox.Show("Do you want to update this stuffing ??",
        //              "Confirm Stuffing Update",
        //              MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //    if (result == DialogResult.Yes)
        //    {
        //        //var StuDetailsId = Convert.ToInt32(selectedRow.Cells[5].Value);
        //        //NavigateToGateIn(IGMDetailsId);
        //    }
        //}
        //else if (stuffed == "No")
        //{
        //    DialogResult result = MessageBox.Show("Do you want to stuffing ??",
        //              "Confirm Stuffing",
        //              MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //    if (result == DialogResult.Yes)
        //    {
        //        ddlClient.SelectedValue = objCargoReceive.CustId;                   
        //        LoadCSDContainerByClientId(objCargoReceive.CustId);
        //        selectedCDId = objCargoReceive.CargoDetails.ElementAt(selectedrowindex).CargoDetailsId;

        //        //txtRefNo.Text = stuffingBll.GetStuffingRefNo(objCargoReceive.CustId);
        //        //var IGMDetailsId = Convert.ToInt32(selectedRow.Cells[0].Value);
        //        //NavigateToGateOut(IGMDetailsId);
        //    }
        //}


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                bool flag = ValidateTR();
                if (flag == true)
                {

                    FillingTRData();
                    var status = objBll.Insert(objTR);
                    MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                    LoadDatatoGrid();
                    ClearForm();

                }
            }
            else if (btnSave.Text == "Update")
            {
                bool flag = ValidateTR();
                if (flag == true)
                {

                    FillingTRData();
                    var status = objBll.Update(objTR);
                    MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }

            }


        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {

            Close();
        }
      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();

        }

        private void btnPrintTR_Click(object sender, EventArgs e)
        {
            var TRId = objTR.ID;


            ReportDocument cryRpt = new ReportDocument();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            //crConnectionInfo.ServerName = "ELL-SERVER2";
            //crConnectionInfo.DatabaseName = "PORT_LOGISTIC";
            //crConnectionInfo.UserID = "sa";
            //crConnectionInfo.Password = "Sa@1234";

            crConnectionInfo.ServerName = LOGISTIC.UI.Properties.Settings.Default.DBSERVER;
            crConnectionInfo.DatabaseName = LOGISTIC.UI.Properties.Settings.Default.DATABASE;
            crConnectionInfo.UserID = LOGISTIC.UI.Properties.Settings.Default.USERID;
            crConnectionInfo.Password = LOGISTIC.UI.Properties.Settings.Default.PASSWORD;


            cryRpt.Load(@"D:\Report\" + "TRreport.rpt");
            cryRpt.SetParameterValue("@TRId", TRId);           
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

        //private void btnPrintTR_Click(object sender, EventArgs e)
        //{
        //    var TRId = objTR.ID;

        //    DataTable dt = new DataTable();
        //    dt = objBll.GetTRData(TRId);
        //    DataRow row = dt.Rows[0];

        //    try
        //    {

        //        Excel.Application xlApp = new Excel.Application();

        //        if (xlApp == null)
        //        {
        //            MessageBox.Show("Excel is not properly installed!!");
        //            return;
        //        }
        //        xlApp.DisplayAlerts = false;

        //        Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
        //        Excel.Sheets worksheets = xlWorkBook.Worksheets;

        //        var xlSummary = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);


        //        xlSummary.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 55, 1, 70, 45);
        //        xlSummary.Cells[1, 1].value = "EASTERN LOGISTICS LIMITED.";
        //        xlSummary.Cells[1, 1].Font.Bold = true;
        //        xlSummary.Cells[1, 1].Font.Size = 15;
        //        xlSummary.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        xlSummary.Range["A1:M1"].MergeCells = true;

        //        xlSummary.Cells[2, 1].value = " KATHGAR, NORTH PATENGA, CHITTAGONG.";
        //        xlSummary.Cells[2, 1].Font.Size = 10;
        //        xlSummary.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        xlSummary.Range["A2:M2"].MergeCells = true;

        //        xlSummary.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: import@easternlogisticsltd.com";
        //        xlSummary.Cells[3, 1].Font.Size = 10;
        //        xlSummary.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        xlSummary.Range["A3:M3"].MergeCells = true;



        //        xlSummary.Cells[5, 1].value = "TERMINAL RECEIPT";
        //        xlSummary.Cells[5, 1].Font.Bold = true;
        //        xlSummary.Cells[5, 1].Font.Size = 12;
        //        xlSummary.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        xlSummary.Range["A5:M5"].MergeCells = true;
        //        xlSummary.Range["A5:M5"].Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleSingle;




        //        string FileName = "D:\\TR Report of MBDL.xlsx";
        //        xlSummary.Name = "TR Report";


        //        DataSet ds = new DataSet();
        //        //ds = objBll.GetMLOWiseImportSummaryReport(ClientId, fromDate, toDate);

        //        int r = 7; // Initialize Excel Row Start Position  = 1


        //        xlSummary.Cells[r, 1].value = "TERMINAL RECEIPT NO.";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["TRNo"];
        //        xlSummary.Cells[r, 6].value = "DATE";
        //        xlSummary.Cells[r, 7].value = ":";
        //        xlSummary.Cells[r, 8].value = row["Date"];
        //        // xlSummary.get_Range("A" + r, "M" + r).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

        //        r = r + 2;

        //        xlSummary.Cells[r, 1].value = "STUFFING DATE";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["StuffingDate"];
        //        //xlSummary.get_Range("A" + r, "D" + r).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

        //        r = r + 2;


        //        xlSummary.Cells[r, 1].value = "SHIPPING BILL NO.";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["EFRNo"];
        //        xlSummary.Cells[r, 6].value = "DATE";
        //        xlSummary.Cells[r, 7].value = ":";
        //        xlSummary.Cells[r, 8].value = row["EFRDate"];

        //        r = r + 2;

        //        xlSummary.Cells[r, 1].value = "EXP NO.";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["ExpNo"];
        //        xlSummary.Cells[r, 6].value = "DATE";
        //        xlSummary.Cells[r, 7].value = ":";
        //        xlSummary.Cells[r, 8].value = row["ExpDate"];

        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "VAT";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = "VAT";

        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "FORWARDER/AGENT NAME";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["FreightForwarderName"];

        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "SHIPPER";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["ShipperName"];


        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "NEGOTIATING BANK IN BANGLADESH";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["BankName"];

        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "CONSIGNEE";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["ConsigneeName"];


        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "CARRIER";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["CarrierName"];

        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "BOOKING NO.";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["BookingNo"];


        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "C & F AGENT NAME";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["CFAgentName"];


        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "PLACE OF RECEIPT";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = "EASTERN LOGISTICS LTD.";
        //        xlSummary.Cells[r, 6].value = "PORT OF LOADING";
        //        xlSummary.Cells[r, 7].value = ":";
        //        xlSummary.Cells[r, 8].value = row["Port of Loading"];


        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "PORT OF DISCHARGE";
        //        xlSummary.Cells[r, 2].value = ":";
        //        xlSummary.Cells[r, 3].value = row["Port of Discharge"];
        //        xlSummary.Cells[r, 6].value = "PORT OF DELIVERY";
        //        xlSummary.Cells[r, 7].value = ":";
        //        xlSummary.Cells[r, 8].value = row["Port of Delivery"];

        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "RECEIVED AND STUFFED THE UNDER MENTIONED NUMBER OF PACKAGES SAID TO BE IN GOOD ORDER AND GOOD CONDITION.";
        //        xlSummary.Range["A" + r, "G" + r].MergeCells = true;
        //        //xlSummary.Range["A" + r, "M" + r].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

        //        r++;
        //        xlSummary.Cells[r, 1].value = "CONTENTS OF THE PACKAGES ARE UNKNOWN.";
        //        xlSummary.Range["A" + r, "D" + r].MergeCells = true;

        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "MARK AND NUMBER";
        //        xlSummary.Cells[r, 2].value = "NO OF PACKGES";
        //        xlSummary.Range["C" + r, "E" + r].Value = "GROSS WEIGHT (KG)";
        //        xlSummary.Range["C" + r, "E" + r].MergeCells = true;
        //        xlSummary.Range["F" + r, "G" + r].Value = "DESCIRIPTION OF PACKAGES AND GOODS";
        //        xlSummary.Range["F" + r, "G" + r].MergeCells = true;

        //        xlSummary.Range["A" + r, "G" + r].Borders.Color = Color.Black.ToArgb();
        //        xlSummary.Range["A" + r, "G" + r].Font.Size = 10;
        //        xlSummary.Range["A" + r, "G" + r].Font.Bold = true;

        //        r++;
        //        xlSummary.Cells[r, 1].value = "AS PER INVOICE";
        //        xlSummary.Cells[r, 2].value = row["Lot"];
        //        xlSummary.Range["C" + r, "E" + r].Value = row["Cargo Weight"] + " KGS";
        //        xlSummary.Range["C" + r, "E" + r].MergeCells = true;
        //        xlSummary.Range["F" + r, "G" + r].Value = row["Commodity"];
        //        xlSummary.Range["F" + r, "G" + r].MergeCells = true;

        //        xlSummary.Range["A" + r, "G" + r].Borders.Color = Color.Black.ToArgb();

        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "SEALED BY UNDERSIGNED IN PRESENCE OF PREVENTIVE OFFICER";
        //        xlSummary.Range["A" + r, "E" + r].MergeCells = true;

        //        r++;
        //        xlSummary.Cells[r, 1].value = DateTime.Now.Date.ToString("dd MMM yyyy");


        //        r = r + 2;
        //        xlSummary.Cells[r, 1].value = "SL";
        //        xlSummary.Cells[r, 2].value = "CONTAINER NO.";
        //        xlSummary.Cells[r, 3].value = "SIZE";
        //        xlSummary.Cells[r, 4].value = "TYPE";
        //        xlSummary.Cells[r, 5].value = "STATUS";
        //        xlSummary.Cells[r, 6].value = "TARE WEIGHT (KG)";
        //        xlSummary.Cells[r, 7].value = "GROSS WEIGHT (KG)";
        //        xlSummary.Cells[r, 8].value = "CBM";
        //        xlSummary.Cells[r, 9].value = "NO OF PACKAGES";
        //        xlSummary.Cells[r, 10].value = "SEAL NO.";
        //        xlSummary.Cells[r, 11].value = "RE-SEAL NO.";

        //        xlSummary.Range["A" + r, "K" + r].Borders.Color = Color.Black.ToArgb();
        //        xlSummary.Range["A" + r, "K" + r].Font.Size = 10;
        //        xlSummary.Range["A" + r, "K" + r].Font.Bold = true;
        //        xlSummary.Range["A" + r, "K" + r].Columns.AutoFit();

        //        r++;
        //        int Sl = 1;
        //        foreach (DataRow rows in dt.Rows)
        //        {
        //            xlSummary.Cells[r, 1].value = Sl.ToString();
        //            xlSummary.Cells[r, 2].value = rows["ContainerNo"];
        //            xlSummary.Cells[r, 3].value = rows["ContainerSize"];
        //            xlSummary.Cells[r, 4].value = rows["ContainerTypeName"];
        //            xlSummary.Cells[r, 5].value = rows["Status"];
        //            xlSummary.Cells[r, 6].value = rows["Tare Weighe"];
        //            xlSummary.Cells[r, 7].value = rows["Gross Weight"];
        //            xlSummary.Cells[r, 8].value = rows["CubicMeter"];
        //            xlSummary.Cells[r, 9].value = rows["No of Packages"];
        //            xlSummary.Cells[r, 10].value = rows["SealNo"];
        //            xlSummary.Cells[r, 11].value = " ";

        //            Sl++;
        //            r++;
        //        }



        //        //xlSummary.Range["A" + r, "K" + r].Borders.Color = Color.Black.ToArgb();
        //        //xlSummary.Range["A" + r, "K" + r].Font.Size = 12;
        //        //xlSummary.Range["A" + r, "K" + r].Font.Bold = true;
        //        //xlSummary.Range["A" + r, "K" + r].Columns.AutoFit();


        //        //Excel.Range SourceRange = (Excel.Range)xlSummary.get_Range("A" + r, "K" + r); // or whatever range you want here
        //        //FormatAsTable(SourceRange, "Table1", "TableStyleMedium15");



        //        //xlSummary.Cells[r, 3].value = "GROSS WEIGHT (KG)";
        //        //xlSummary.Cells[r, 4].value = "DESCIRIPTION OF PACKAGES AND GOODS";




        //        r = r + 5;
        //        xlSummary.Range["H" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Black;
        //        xlSummary.Range["H" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
        //        r++;
        //        xlSummary.Range["H" + r, "K" + r].Value = "AUTHORISED SIGNATURE AND STAMP OF";
        //        xlSummary.Range["H" + r, "K" + r].MergeCells = true;
        //        r++;
        //        xlSummary.Range["H" + r, "K" + r].Value = "TERMINAL OPERATOR";
        //        xlSummary.Range["H" + r, "K" + r].MergeCells = true;
        //        r++;
        //        xlSummary.Range["H" + r, "K" + r].Value = "CREATED BY : SYSTEM ADMINISTRATOR";
        //        xlSummary.Range["H" + r, "K" + r].MergeCells = true;
        //        r++;
        //        xlSummary.Range["A" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color.Black;
        //        xlSummary.Range["A" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
        //        r++;
        //        xlSummary.Cells[r, 2].value = "EXPORT VESSEL";
        //        xlSummary.Cells[r, 3].value = ":";
        //        xlSummary.Range["D" + r, "F" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Black;
        //        xlSummary.Range["D" + r, "F" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
        //        xlSummary.Cells[r, 7].value = "VOYAGE";
        //        xlSummary.Cells[r, 8].value = ":";
        //        xlSummary.Range["I" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Black;
        //        xlSummary.Range["I" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
        //        r = r + 2;
        //        xlSummary.Cells[r, 2].value = "ROTATION NO";
        //        xlSummary.Cells[r, 3].value = ":";
        //        xlSummary.Range["D" + r, "F" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Black;
        //        xlSummary.Range["D" + r, "F" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
        //        xlSummary.Cells[r, 7].value = "BL NO.";
        //        xlSummary.Cells[r, 8].value = ":";
        //        xlSummary.Range["I" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Black;
        //        xlSummary.Range["I" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;


        //        xlSummary.Range["A:A"].EntireColumn.AutoFit();

        //        // xlSummary.get_Range("A7", "M33").Font.Bold = true;
        //        //xlSummary.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


        //        //xlSummary.Columns.AutoFit();




        //        Excel.Sheets autoSheet = xlWorkBook.Worksheets;
        //        autoSheet[2].Delete();

        //        xlWorkBook.SaveAs(FileName);
        //        xlWorkBook.Close();
        //        xlApp.Quit();



        //        Marshal.ReleaseComObject(xlApp);
        //        Marshal.ReleaseComObject(xlWorkBook);
        //        Marshal.ReleaseComObject(xlSummary);
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Exception: " + ex.Message, "You got an Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {

        //        foreach (Process process in Process.GetProcessesByName("Excel"))
        //            process.Kill();

        //    }




        //}

        private void ddlMLO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var custId = Convert.ToInt32(ddlMLO.SelectedValue);
            if (custId > 0)
            {
                LoadEFRNo(custId);
            }
            else
            {
                ddlEFRNo.Items.Clear();
            }
        }       

        private void ddlEFRNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlEFRNo.SelectedValue) > 0)
            {
                int custId = Convert.ToInt32(ddlMLO.SelectedValue);
                txtTRnumber.Text = objBll.GetMLOWiseTRNumber(Convert.ToInt32(ddlMLO.SelectedValue));
                              
            }
        }


        private bool ValidateTR()
        {

            var errMessage = "";

            if (Convert.ToInt32(ddlEFRNo.SelectedValue) < 0)
            {
                errMessage = errMessage + "* Please select an EFR no !!\n";
            }
            if (txtTRnumber.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter TR no !!\n";
            }
            if (Convert.ToInt32(ddlDelivery.SelectedValue) < 0)
            {
                errMessage = errMessage + "* Please select delivery port!!\n";
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

        private void FillingTRData()
        {


            objTR.MLOId = Convert.ToInt32(ddlMLO.SelectedValue);
            objTR.EFRNo = ddlEFRNo.Text.Trim();
            objTR.CargoReceivingId = Convert.ToInt32(ddlEFRNo.SelectedValue);
            objTR.TRNo = txtTRnumber.Text.Trim();
            objTR.PortofDeliveryId = Convert.ToInt32(ddlDelivery.SelectedValue);
            objTR.Date = dateTR.Value;


        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            objTR = listTR.ElementAt(selectedrowindex);

            ddlMLO.SelectedValue = objTR.MLOId;
            ddlEFRNo.Text = objTR.EFRNo;
            txtTRnumber.Text = objTR.TRNo;
            ddlDelivery.SelectedValue = objTR.PortofDeliveryId;
            dateTR.Value = Convert.ToDateTime(objTR.Date);

            ddlMLO.Enabled = true;
            ddlEFRNo.Enabled = true;
            txtTRnumber.Enabled = true;
            btnPrintTR.Enabled = true;
            btnSave.Text = "Update";
            btnDelete.Enabled = true;

        }

        private void ClearForm()
        {
            ddlMLO.SelectedIndex = 0;
            ddlEFRNo.DataSource = null;
            ddlEFRNo.Text = "";
            txtTRnumber.Text = "";
            ddlDelivery.SelectedIndex = 0;
            dateTR.Value = DateTime.Now;
            objTR = new TRReportData();
            dataGridView1.ClearSelection();
            ddlMLO.Enabled = true;
            txtTRnumber.Enabled = true;
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            btnPrintTR.Enabled = false;

        }

        private void ddlClientSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void ddlClientSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.DroppedDown = true;
            string strFindStr = "";
            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }
            int intIdx = -1;
            // Search the string in the ComboBox list.
            intIdx = cb.FindString(strFindStr);
            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
                e.Handled = true;
        }

        private void ddlMLO_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.DroppedDown = true;
            string strFindStr = "";
            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }
            int intIdx = -1;
            // Search the string in the ComboBox list.
            intIdx = cb.FindString(strFindStr);
            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
                e.Handled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                         "Confirm Cargo Receiving deletion",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                var status = objBll.Insert(objTR);
                //var status = cargoBll.Delete(objCargoReceiving);
                MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ClearCargoReceive();
                ClearForm();
            }
        }
    }

}