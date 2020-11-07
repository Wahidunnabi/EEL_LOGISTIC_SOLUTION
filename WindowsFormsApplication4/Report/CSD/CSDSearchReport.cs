using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using LOGISTIC.CSD.BLL;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using LOGISTIC.REPORT;
using System.Linq;
using System.Configuration;

namespace LOGISTIC.UI
{
    public partial class CSDSearchReport : Form
    {

        private static CSDContGateInOut objCSDInOut = new CSDContGateInOut();

        private ContainerTypeBll ctBll = new ContainerTypeBll();
        private ContainerSizeBll csBll = new ContainerSizeBll();
        private DepotBll depoBll = new DepotBll();
        private TrailerBll objTrailerBll = new TrailerBll();
        //private TrailerNumberBll tnBll = new TrailerNumberBll();
        private CustomerBll MLOBll = new CustomerBll();
        //private StatusBLL statusBll = new StatusBLL();
        private CSDGateInOutSearchBLL objBll = new CSDGateInOutSearchBLL();

        //private UserInfo user;
        //static int PageSize = 10;
        //int CSDId;
        public CSDSearchReport()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            txtContNumbe.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtContNumbe.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";


        }

        private void ContainerGateEntry_Load(object sender, EventArgs e)
        {

            
            LoadCustomer();
            LoadContSize();
            LoadConType();
            LoadDepot();
            LoadTrailerAccount();
            PrepareGrid();
        }


        #region Load Basic Data




        private void LoadCustomer()
        {

            var type = MLOBll.Getall();
            ddlCusCode.DisplayMember = "CustomerCode";
            ddlCusCode.ValueMember = "CustomerId";
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
            dr[1] = "-- From Location --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                cmbFrom.DataSource = dt_Types;
                cmbFrom.DisplayMember = "t_Name";
                cmbFrom.ValueMember = "t_ID";
            }
            cmbFrom.SelectedIndex = 0;



            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.DepotId, t.DepotCode);
            }
            DataRow dtr = dt.NewRow();
            dtr[0] = 0;
            dtr[1] = "-- To Location --";
            dt.Rows.InsertAt(dtr, 0);
            if (dt.Rows.Count > 0)
            {
                cmbTo.DataSource = dt;
                cmbTo.DisplayMember = "t_Name";
                cmbTo.ValueMember = "t_ID";
            }
            cmbTo.SelectedIndex = 0;           


        }

        private void LoadTrailerAccount()
        {


            var type = objTrailerBll.Getall();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.TrailerId, t.TrailerCode);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Trailer In --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                cmbTrailerIn.DataSource = dt_Types;
                cmbTrailerIn.DisplayMember = "t_Name";
                cmbTrailerIn.ValueMember = "t_ID";
            }
            cmbTrailerIn.SelectedIndex = 0;




            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.TrailerId, t.TrailerCode);
            }
            DataRow dtr = dt.NewRow();
            dtr[0] = 0;
            dtr[1] = "-- Trailer Out --";
            dt.Rows.InsertAt(dtr, 0);
            if (dt.Rows.Count > 0)
            {
                cmbTrailerOut.DataSource = dt;
                cmbTrailerOut.DisplayMember = "t_Name";
                cmbTrailerOut.ValueMember = "t_ID";
            }
            cmbTrailerOut.SelectedIndex = 0;

        }


        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 168;
            dataGridView1.Columns[0].HeaderText = "Customer";
            dataGridView1.Columns[0].DataPropertyName = "CustomerName";


            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[1].HeaderText = "Size";
            dataGridView1.Columns[1].DataPropertyName = "Size";

            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[2].HeaderText = "Type";
            dataGridView1.Columns[2].DataPropertyName = "Type";

            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[3].HeaderText = "Box Quantity";
            dataGridView1.Columns[3].DataPropertyName = "BoxQty";


            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[4].HeaderText = "Teus";
            dataGridView1.Columns[4].DataPropertyName = "Teus";


            dataGridView1.ClearSelection();

        }

        #endregion

        private void ddlCusCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ddlCusCode.SelectedIndex > 0)
            {
                var CustId = Convert.ToInt32(ddlCusCode.SelectedValue);
                //txtCusName.Text = objCSDBll.GetCustNameById(CustId);

            }
            else
            {
                txtCusName.Text = "";
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;
            int custId = Convert.ToInt32(ddlCusCode.SelectedValue);
            int SortBy = 1;
            if (rdoGateIn.Checked == true)
                SortBy = 1;
            if (rdoGateOut.Checked == true)
                SortBy = 2;
            if (rdoStock.Checked == true)
                SortBy = 3;
            string containerNo = txtContNumbe.Text.Trim();
            int size = Convert.ToInt32(cmbContSize.SelectedValue);
            int type = Convert.ToInt32(cmbConType.SelectedValue);
            int comeFrom = Convert.ToInt32(cmbFrom.SelectedValue);
            int outTo = Convert.ToInt32(cmbTo.SelectedValue);
            int trailerIn = Convert.ToInt32(cmbTrailerIn.SelectedValue);
            int trailerOut = Convert.ToInt32(cmbTrailerOut.SelectedValue);

            DataTable dt = new DataTable();         
            dt = objBll.GetFilteredCSDGateInOut(custId, fromDate, toDate, SortBy, containerNo, size, type, comeFrom, outTo, trailerIn, trailerOut);
            int totalBox = dt.AsEnumerable().Sum(r => r.Field<int>("BoxQty"));
            int totalTuse= dt.AsEnumerable().Sum(r => r.Field<int>("Teus"));

            dataGridView1.DataSource = dt;           
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.AllowUserToAddRows = false;
            txtTotalBox.Text = Convert.ToString(totalBox); 
            txtTotalTues.Text= Convert.ToString(totalTuse);
            //string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constring))
            //{
            //    using (SqlCommand cmd = new SqlCommand("CSD_DateRange_FilterReport", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@ClientId", custId > 0  ? (object)custId : DBNull.Value);
            //        cmd.Parameters.AddWithValue("@FromDate", fromDate);
            //        cmd.Parameters.AddWithValue("@ToDate", toDate);
            //        cmd.Parameters.AddWithValue("@SortBy", SortBy);
            //        cmd.Parameters.AddWithValue("@ContainerNo", txtContNumbe.Text.Trim().Length > 0 ? (object)txtContNumbe.Text.Trim() : DBNull.Value);
            //        cmd.Parameters.AddWithValue("@Size", size > 0 ? (object)size : DBNull.Value);
            //        cmd.Parameters.AddWithValue("@Type", type > 0 ? (object)type : DBNull.Value);
            //        cmd.Parameters.AddWithValue("@DepotFrom", comeFrom > 0 ? (object)comeFrom : DBNull.Value);
            //        cmd.Parameters.AddWithValue("@DepotTo", outTo > 0 ? (object)outTo : DBNull.Value);
            //        cmd.Parameters.AddWithValue("@TrailerInId", trailerIn > 0 ? (object)trailerIn : DBNull.Value);
            //        cmd.Parameters.AddWithValue("@TrailerOutId", trailerOut > 0 ? (object)trailerOut : DBNull.Value);

            //        con.Open();
            //        DataTable dt = new DataTable();
            //        dt.Load(cmd.ExecuteReader());

            //  int sum = dt.AsEnumerable().Sum(r => r.Field<int>("BoxQty"));

            //dataGridView1.DataSource = dt;
            //dataGridView1.ScrollBars = ScrollBars.None;
            //dataGridView1.AllowUserToAddRows = false;
            //con.Close();

        }






        private void btnCSDCancel_Click(object sender, EventArgs e)
        {
            ClearForm();

        }

        private void btnCSDClose_Click(object sender, EventArgs e)
        {
            ClearForm();
            Close();
        }

      







        private void ResetForm()
        {




        }

        private void ClearForm()
        {
            dateFrom.Value = DateTime.Now;
            dateTo.Value = DateTime.Now;
            ddlCusCode.SelectedIndex = 0;
            txtCusName.Text = "";
            rdoGateIn.Checked = true;
            txtContNumbe.Text = "";         
            cmbContSize.SelectedValue = 0;
            cmbConType.SelectedValue = 0;
            cmbFrom.SelectedValue = 0;
            cmbTo.SelectedIndex = 0;
            cmbTrailerIn.SelectedValue = 0;
            cmbTrailerOut.SelectedValue = 0;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            txtTotalBox.Text = "";
            txtTotalTues.Text = "";           
           // btnSearch.Enabled = false;
            labelControl1.Focus();


        }

        private void btnInward_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;
            int custId = Convert.ToInt32(ddlCusCode.SelectedValue);
            int SortBy = 1;
           
            string containerNo = txtContNumbe.Text.Trim();
            int size = Convert.ToInt32(cmbContSize.SelectedValue);
            int type = Convert.ToInt32(cmbConType.SelectedValue);
            int comeFrom = Convert.ToInt32(cmbFrom.SelectedValue);
            int outTo = Convert.ToInt32(cmbTo.SelectedValue);
            int trailerIn = Convert.ToInt32(cmbTrailerIn.SelectedValue);
            int trailerOut = Convert.ToInt32(cmbTrailerOut.SelectedValue);

            ReportDocument cryRpt = new ReportDocument();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            //crConnectionInfo.ServerName = "sarwar-pc";
            //crConnectionInfo.DatabaseName = "PORT_LOGISTIC";
            //crConnectionInfo.UserID = "sa";
            //crConnectionInfo.Password = "sa";

            crConnectionInfo.ServerName = LOGISTIC.UI.Properties.Settings.Default.DBSERVER;
            crConnectionInfo.DatabaseName = LOGISTIC.UI.Properties.Settings.Default.DATABASE;
            crConnectionInfo.UserID = LOGISTIC.UI.Properties.Settings.Default.USERID ;
            crConnectionInfo.Password = LOGISTIC.UI.Properties.Settings.Default.PASSWORD;

            cryRpt.Load(@"D:\Report\" + "InwardMovementDetails.rpt");          
            cryRpt.SetParameterValue("@ClientId", custId > 0 ? (object)custId : DBNull.Value);
            cryRpt.SetParameterValue("@FromDate", fromDate);
            cryRpt.SetParameterValue("@ToDate", toDate);
            cryRpt.SetParameterValue("@SortBy", SortBy);
            cryRpt.SetParameterValue("@ContainerNo", containerNo.Length > 0 ? (object)containerNo : DBNull.Value);
            cryRpt.SetParameterValue("@Size", size > 0 ? (object)size : DBNull.Value);
            cryRpt.SetParameterValue("@Type", type > 0 ? (object)type : DBNull.Value);
            cryRpt.SetParameterValue("@DepotFrom", comeFrom > 0 ? (object)comeFrom : DBNull.Value);
            cryRpt.SetParameterValue("@DepotTo", outTo > 0 ? (object)outTo : DBNull.Value);
            cryRpt.SetParameterValue("@TrailerInId", trailerIn > 0 ? (object)trailerIn : DBNull.Value);
            cryRpt.SetParameterValue("@TrailerOutId", trailerOut > 0 ? (object)trailerOut : DBNull.Value);


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

        private void btnOutward_Click(object sender, EventArgs e)
        {

            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;
            int custId = Convert.ToInt32(ddlCusCode.SelectedValue);
            int SortBy = 2;
          
            string containerNo = txtContNumbe.Text.Trim();
            int size = Convert.ToInt32(cmbContSize.SelectedValue);
            int type = Convert.ToInt32(cmbConType.SelectedValue);
            int comeFrom = Convert.ToInt32(cmbFrom.SelectedValue);
            int outTo = Convert.ToInt32(cmbTo.SelectedValue);
            int trailerIn = Convert.ToInt32(cmbTrailerIn.SelectedValue);
            int trailerOut = Convert.ToInt32(cmbTrailerOut.SelectedValue);

            ReportDocument cryRpt = new ReportDocument();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;
            //var APPPATH = Environment.CurrentDirectory + "\test.rpt";

            //crConnectionInfo.ServerName = "sarwar-pc";
            //crConnectionInfo.DatabaseName = "PORT_LOGISTIC";
            //crConnectionInfo.UserID = "sa";
            //crConnectionInfo.Password = "sa";
            crConnectionInfo.ServerName = LOGISTIC.UI.Properties.Settings.Default.DBSERVER;
            crConnectionInfo.DatabaseName = LOGISTIC.UI.Properties.Settings.Default.DATABASE;
            crConnectionInfo.UserID = LOGISTIC.UI.Properties.Settings.Default.USERID;
            crConnectionInfo.Password = LOGISTIC.UI.Properties.Settings.Default.PASSWORD;

            cryRpt.Load(@"D:\Report\" + "OutwardMovementDetails.rpt");
            cryRpt.SetParameterValue("@ClientId", custId > 0 ? (object)custId : DBNull.Value);
            cryRpt.SetParameterValue("@FromDate", fromDate);
            cryRpt.SetParameterValue("@ToDate", toDate);
            cryRpt.SetParameterValue("@SortBy", SortBy);
            cryRpt.SetParameterValue("@ContainerNo", containerNo.Length > 0 ? (object)containerNo : DBNull.Value);
            cryRpt.SetParameterValue("@Size", size > 0 ? (object)size : DBNull.Value);
            cryRpt.SetParameterValue("@Type", type > 0 ? (object)type : DBNull.Value);
            cryRpt.SetParameterValue("@DepotFrom", comeFrom > 0 ? (object)comeFrom : DBNull.Value);
            cryRpt.SetParameterValue("@DepotTo", outTo > 0 ? (object)outTo : DBNull.Value);
            cryRpt.SetParameterValue("@TrailerInId", trailerIn > 0 ? (object)trailerIn : DBNull.Value);
            cryRpt.SetParameterValue("@TrailerOutId", trailerOut > 0 ? (object)trailerOut : DBNull.Value);


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

        private void btnStock_Click(object sender, EventArgs e)
        {
            int custId = Convert.ToInt32(ddlCusCode.SelectedValue);
            int size = Convert.ToInt32(cmbContSize.SelectedValue);
            int type = Convert.ToInt32(cmbConType.SelectedValue);
            int comeFrom = Convert.ToInt32(cmbFrom.SelectedValue);

            ReportDocument cryRpt = new ReportDocument();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            //crConnectionInfo.ServerName = "sarwar-pc";
            //crConnectionInfo.DatabaseName = "PORT_LOGISTIC";
            //crConnectionInfo.UserID = "sa";
            //crConnectionInfo.Password = "sa";

            crConnectionInfo.ServerName = LOGISTIC.UI.Properties.Settings.Default.DBSERVER;
            crConnectionInfo.DatabaseName = LOGISTIC.UI.Properties.Settings.Default.DATABASE;
            crConnectionInfo.UserID = LOGISTIC.UI.Properties.Settings.Default.USERID;
            crConnectionInfo.Password = LOGISTIC.UI.Properties.Settings.Default.PASSWORD;

            cryRpt.Load(@"D:\Report\" + "CsdstockMovementReport.rpt");
            cryRpt.SetParameterValue("@ClientId", custId > 0 ? (object)custId : DBNull.Value);          
            cryRpt.SetParameterValue("@Size", size > 0 ? (object)size : DBNull.Value);
            cryRpt.SetParameterValue("@Type", type > 0 ? (object)type : DBNull.Value);
            cryRpt.SetParameterValue("@DepotFrom", comeFrom > 0 ? (object)comeFrom : DBNull.Value);
          
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
}
