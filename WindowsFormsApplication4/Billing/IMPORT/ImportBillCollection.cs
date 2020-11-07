using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.CSD.BLL;
using System.Data;
using LOGISTIC.BLL;
using System.Reflection;
using System.Linq;

namespace LOGISTIC.UI
{
    public partial class ImportBillCollection : Form
    {

        private List<ServiceDetail> lstServiceDtls = new List<ServiceDetail>();
        private BillingBLL objBll = new BillingBLL();
        private ImporterBll impBll = new ImporterBll();
        private ClearAndForwaderBll CandFBll = new ClearAndForwaderBll();
        private IGMImportBLL IGMBll = new IGMImportBLL();
        private ContainerSizeBll objSizeBll = new ContainerSizeBll();
        private CustomerBll MLOBll = new CustomerBll();
        private static List<Service> listServices = new List<Service>();
        ImportBill objImportBill = new ImportBill();
        private UserInfo user;
        
        private static int index;
        private List<ContainerSize> lstSize = new List<ContainerSize>();
        decimal totalAmount = 0;
        decimal grandTotal = 0;
       
        public ImportBillCollection( UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;
            btnAdd.Enabled = false;
            btnDeleteDtls.Enabled = false;

        }

        public ImportBillCollection(ImportBill objbill, UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.objImportBill = objbill;
            this.user = user;
            totalAmount = objbill.TotalAmount;
            grandTotal = objbill.GrandTotal;
            
            btnSave.Text = "Update";           

        }


        private void ImportBillCollection_Load(object sender, EventArgs e)
        {
            FormSetup();
            listServices = objBll.GetAllImportServices();
            LoadImporter();
            LoadCandFAgent();
            LoadImportServices();
            PrepareGrid();
            PrepareGridContanerPosition();
            IndatewiseImportContainerPosition();
            LoadSize();
            LoadCustomer();

            if (objImportBill.ID > 0)
            {
                ShowImportBillData();

                if (objImportBill.ImportBillDetails.Count > 0)
                {
                    foreach (ImportBillDetail objDetails in objImportBill.ImportBillDetails)
                    {
                        grdBill.Rows.Add(listServices.Where(s => s.ID == objDetails.ServiceId).FirstOrDefault().ServiceName, objDetails.BillUnit, objDetails.Size, objDetails.Quantity, objDetails.Days, objDetails.RateInTk, objDetails.RateInDlr, objDetails.Total);
                    }
                    grdBill.ClearSelection();
                }

               btnDeleteDtls.Enabled = false;

            }
            else
            {
                ddlBLNo.Enabled = true;
                btnSearch.Enabled = false;
                ddlCFAgent.Enabled = false;
                ddlServiceName.Enabled = false;
                btnDelete.Enabled = false;
            }

            

        }

        #region LOAD BASIC DATA     
               
        private void FormSetup()
        {
            lbluptodate.Visible = false;
            dateUpto.Visible = false;
            lblFreeDays.Visible = false;
            txtFreedays.Visible = false;
            txtRatetk.Enabled = false;
            txtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtQuantity.Properties.Mask.EditMask = "\\d+";

            txtRatetk.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRatetk.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtRatedlr.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRatedlr.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,1})?";

            txtDiscountAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtDiscountAmount.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtVAT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtVAT.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,1})?";

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
                ddlMLO.DataSource = dt_Types;
                ddlMLO.DisplayMember = "t_Name";
                ddlMLO.ValueMember = "t_ID";
            }
            ddlMLO.SelectedIndex = 0;

        }
        private void LoadSize()
        {
            lstSize = objSizeBll.Getall();
            var type = lstSize;
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ContainerSizeId, t.ContainerSize1);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Size --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlContSize.DataSource = dt_Types;
                ddlContSize.DisplayMember = "t_Name";
                ddlContSize.ValueMember = "t_ID";
            }
            ddlContSize.SelectedIndex = 0;


        }
        private void LoadImporter()
        {
            
            var type = impBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ImporterId, t.ImporterName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Importer --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlImporter.DataSource = dt_Types;
                ddlImporter.DisplayMember = "t_Name";
                ddlImporter.ValueMember = "t_ID";
            }
            ddlImporter.SelectedIndex = 0;

        }


        private void LoadCandFAgent()
        {

            var type = CandFBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ClearAndForwadingAgentId, t.CFAgentName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select C&F Agent --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlCFAgent.DataSource = dt_Types;
                ddlCFAgent.DisplayMember = "t_Name";
                ddlCFAgent.ValueMember = "t_ID";
            }
            ddlCFAgent.SelectedIndex = 0;

        }

        private void LoadImportServices()
        {

            var type = listServices;
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ID, t.ServiceName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Service --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlServiceName.DataSource = dt_Types;
                ddlServiceName.DisplayMember = "t_Name";
                ddlServiceName.ValueMember = "t_ID";
            }
            ddlServiceName.SelectedIndex = 0;
         
        }

        public void PrepareGrid()
        {

            grdBill.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            grdBill.EnableHeadersVisualStyles = false;
            grdBill.AutoGenerateColumns = false;
            grdBill.AllowUserToAddRows = false;
            grdBill.ColumnCount = 10;


            grdBill.Columns[0].Width = 160;
            grdBill.Columns[0].HeaderText = "Service Name";


            //grdBill.Columns[1].Width = 80;
            //grdBill.Columns[1].HeaderText = "Bill Date";

            grdBill.Columns[1].Width = 60;
            grdBill.Columns[1].HeaderText = "Bill Unit";

            grdBill.Columns[2].Width = 60;
            grdBill.Columns[2].HeaderText = "Size";

            grdBill.Columns[3].Width = 50;
            grdBill.Columns[3].HeaderText = "Quantity";

            //grdBill.Columns[5].Width = 50;
            //grdBill.Columns[5].HeaderText = "Total Days";

            //grdBill.Columns[6].Width = 50;
            //grdBill.Columns[6].HeaderText = "Free Days";

            grdBill.Columns[4].Width = 50;
            grdBill.Columns[4].HeaderText = "Indate";

            grdBill.Columns[5].Width = 50;
            grdBill.Columns[5].HeaderText = "Outdate";

            grdBill.Columns[6].Width = 50;
            grdBill.Columns[6].HeaderText = "Days";

            grdBill.Columns[7].Width = 50;
            grdBill.Columns[7].HeaderText = "Rate(tk)";

            grdBill.Columns[8].Width = 50;
            grdBill.Columns[8].HeaderText = "Rate(dlr)";

            grdBill.Columns[9].Width = 80;
            grdBill.Columns[9].HeaderText = "Total(tk)";

        }
        public void PrepareGridContanerPosition()
        {

            gridcontianerPosition.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            gridcontianerPosition.EnableHeadersVisualStyles = false;
            gridcontianerPosition.AutoGenerateColumns = false;
            gridcontianerPosition.AllowUserToAddRows = false;
            gridcontianerPosition.ColumnCount = 5;


            gridcontianerPosition.Columns[0].Width = 50;
            gridcontianerPosition.Columns[0].HeaderText = "Sl No";
            gridcontianerPosition.Columns[0].DataPropertyName = "Slno";

            gridcontianerPosition.Columns[1].Width = 100;
            gridcontianerPosition.Columns[1].HeaderText = "Container No";
            gridcontianerPosition.Columns[1].DataPropertyName = "ContainerNo";
            

            gridcontianerPosition.Columns[2].Width = 100;
            gridcontianerPosition.Columns[2].HeaderText = "Size";
            gridcontianerPosition.Columns[2].DataPropertyName = "ContainerSize";

            gridcontianerPosition.Columns[3].Width = 100;
            gridcontianerPosition.Columns[3].HeaderText = "Type";
            gridcontianerPosition.Columns[3].DataPropertyName = "ContainerTypeName";

            gridcontianerPosition.Columns[4].Width = 100;
            gridcontianerPosition.Columns[4].HeaderText = "Gate Indate";
            gridcontianerPosition.Columns[4].DataPropertyName = "GateInDate";


            //gridcontianerPosition.Columns[5].Width = 100;
            //gridcontianerPosition.Columns[5].HeaderText = "Total Container";
            //gridcontianerPosition.Columns[5].DataPropertyName = "totalcon";

        }
        public void IndatewiseImportContainerPosition()
        {

            grdIndatewiseContainerPosition.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            grdIndatewiseContainerPosition.EnableHeadersVisualStyles = false;
            grdIndatewiseContainerPosition.AutoGenerateColumns = false;
            grdIndatewiseContainerPosition.AllowUserToAddRows = false;
            grdIndatewiseContainerPosition.ColumnCount = 5;


            grdIndatewiseContainerPosition.Columns[0].Width = 100;
            grdIndatewiseContainerPosition.Columns[0].HeaderText = "BL Nsumber";
            grdIndatewiseContainerPosition.Columns[0].DataPropertyName = "BLnumber";

            grdIndatewiseContainerPosition.Columns[1].Width = 80;
            grdIndatewiseContainerPosition.Columns[1].HeaderText = "Gate InDate";
            grdIndatewiseContainerPosition.Columns[1].DataPropertyName = "GateInDate";


            grdIndatewiseContainerPosition.Columns[2].Width = 30;
            grdIndatewiseContainerPosition.Columns[2].HeaderText = "20'";
            grdIndatewiseContainerPosition.Columns[2].DataPropertyName = "TwentyQty";

            grdIndatewiseContainerPosition.Columns[3].Width = 30;
            grdIndatewiseContainerPosition.Columns[3].HeaderText = "40'";
            grdIndatewiseContainerPosition.Columns[3].DataPropertyName = "FourtyQty";

            grdIndatewiseContainerPosition.Columns[4].Width = 30;
            grdIndatewiseContainerPosition.Columns[4].HeaderText = "45'";
            grdIndatewiseContainerPosition.Columns[4].DataPropertyName = "FourtyfiveQty";


            //gridcontianerPosition.Columns[5].Width = 100;
            //gridcontianerPosition.Columns[5].HeaderText = "Total Container";
            //gridcontianerPosition.Columns[5].DataPropertyName = "totalcon";

        }
        private void ShowImportBillData()
        {

            ddlImporter.SelectedValue = objImportBill.ImporterId;
            ddlBLNo.Text= objImportBill.BLNo;
            txtImportInvoice.Text = objImportBill.ImpInvoiceNumber;
            LoadGridContianerPosition(ddlBLNo.Text.ToString());
            Get_Indatewise_ImportContainer_Position(ddlBLNo.Text.ToString());
            ddlCFAgent.SelectedValue = objImportBill.CandFAgentId;
            dateUpto.Value = Convert.ToDateTime(objImportBill.BillCalculateDate);
            lblTotalAmount.Text = Convert.ToString(objImportBill.TotalAmount);
            txtDiscountAmount.Text = String.Format("{0:0.00}", objImportBill.DiscountAmount);
            chkPercentage.Checked = objImportBill.IsPercentage == true ? true : false;
            txtVAT.Text = String.Format("{0:0.00}", objImportBill.VatPercent);
            lblVatAmount.Text = Convert.ToString(objImportBill.VatAmount);
            lblGrandTotal.Text = Convert.ToString(objImportBill.GrandTotal);

            if (objImportBill.Documentstatus == 1)
            {
                rdoDraft.Checked = true;
            }
            if (objImportBill.Documentstatus == 2)
            {
                rdoInprogress.Checked = true;
            }
            if (objImportBill.Documentstatus == 3)
            {
                rdoPendingapproval.Checked = true;
            }
            if (objImportBill.Documentstatus == 4)
            {
                rdoApproved.Checked = true;
            }
            if (objImportBill.Documentstatus == 5)
            {
                rdoAdminAdjustment.Checked = true;
            }
            if (objImportBill.Documentstatus == 6)
            {
                rdoPaid.Checked = true;
            }
            ddlImporter.Enabled = false;
            ddlBLNo.Enabled = false;
            ddlCFAgent.Enabled = false;

        }

        #endregion

        private void ddlImporter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlImporter.SelectedValue) > 0)
            {
                int importerId = Convert.ToInt32(ddlImporter.SelectedValue);
                LoadImporterwiseBLNo(importerId);
                ddlBLNo.Enabled = true;
                btnSearch.Enabled = true;
            }
            else
            {
                ddlBLNo.DataSource = null;
                ddlBLNo.Items.Clear();
                ddlBLNo.Enabled = false;
                btnSearch.Enabled = false;
                ddlServiceName.Enabled = false;
                
            }
        }


        public void LoadImporterwiseBLNo(int importerId)
        {
            List<IGMImport> listIGM = IGMBll.GetAllIGMImportByImporterId(importerId);
            if (listIGM.Count > 0)
            {

                ddlBLNo.DataSource = null;
                DataTable dt_Types = new DataTable();
                dt_Types.Columns.Add("t_ID", typeof(int));
                dt_Types.Columns.Add("t_Name", typeof(string));
                foreach (var t in listIGM)
                {
                    dt_Types.Rows.Add(t.IGMImportId, t.BLnumber);
                }
                DataRow dr = dt_Types.NewRow();
                dr[0] = 0;
                dr[1] = "--Select BL Number--";
                dt_Types.Rows.InsertAt(dr, 0);
                if (dt_Types.Rows.Count > 0)
                {
                    ddlBLNo.DataSource = dt_Types;
                    ddlBLNo.DisplayMember = "t_Name";
                    ddlBLNo.ValueMember = "t_ID";
                }
                else

                { ddlBLNo.SelectedIndex = 0; }




            }
            else
            {
                ddlBLNo.DataSource = null;
                ddlBLNo.Refresh();
                ddlBLNo.Items.Clear();
                ddlBLNo.Items.Insert(0, "No data found");
                ddlBLNo.SelectedIndex = 0;
                ddlBLNo.Enabled = false;
                labelControl1.Focus();
            }



        }


        private void ddlBLNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlBLNo.SelectedValue) > 0)
            {
                ddlCFAgent.Enabled = true;
                ddlServiceName.Enabled = true;

                string value = ddlBLNo.SelectedValue.ToString();
               
            }
            else
            {
                ddlCFAgent.SelectedIndex = 0;
                ddlServiceName.SelectedIndex = 0;
                ddlCFAgent.Enabled = false;
                ddlServiceName.Enabled = false;
            }

            
            
        }
        private void LoadGridImportGateInPosition(string BLnumber)
        {
            List<IGMImportDetail> IgmImport = IGMBll.GetIGMImportDetailBlnum(BLnumber);
            
        }
        private void LoadGridContianerPosition(string BLnumber)
        {



            DataTable result = IGMBll.Get_Indate_and_Size_and_BL_WiseSummery(BLnumber);
            
            gridcontianerPosition.DataSource = result;
            lbltotal.Text = gridcontianerPosition.RowCount.ToString();
          
            LblPackageType.Text = result.Columns[3].ToString();
            
            decimal GrossWeight = (from DataRow dr in result.Rows
                    
                      select (decimal)dr["GrossWeight"]).FirstOrDefault();

            lblGrossWeight.Text = GrossWeight.ToString();


            string UnitOfMeasureName = null;
            UnitOfMeasureName = (from DataRow dr in result.Rows

                                   select (string)dr["UnitOfMeasureName"]).FirstOrDefault();
            if (String.IsNullOrEmpty(UnitOfMeasureName) != true) {
                LblPackageType.Text = UnitOfMeasureName.ToString();
                }
            


           

            int numberOfRecords = result.AsEnumerable().Where(x => x["ContainerSize"].ToString() == "20").ToList().Count;
            lbltwn.Text = numberOfRecords.ToString();
            numberOfRecords = result.AsEnumerable().Where(x => x["ContainerSize"].ToString() == "40").ToList().Count;
            lblforty.Text = numberOfRecords.ToString();
            numberOfRecords = result.AsEnumerable().Where(x => x["ContainerSize"].ToString() == "45").ToList().Count;
            lblfortyfive.Text = numberOfRecords.ToString();
           
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlBLNo.Text.Trim().Length > 0)
            {
                
                List<IGMImport> listIGM = IGMBll.GetIGMImportByBLNumber(ddlBLNo.Text.Trim(), Convert.ToInt32(ddlImporter.SelectedValue));
                
                if (listIGM.Count > 0)
                {

                    ddlBLNo.DataSource = null;
                    DataTable dt_Types = new DataTable();
                    dt_Types.Columns.Add("t_ID", typeof(int));
                    dt_Types.Columns.Add("t_Name", typeof(string));
                    foreach (var t in listIGM)
                    {
                        dt_Types.Rows.Add(t.IGMImportId, t.BLnumber);
                    }
                    DataRow dr = dt_Types.NewRow();
                    dr[0] = 0;

                    dr[1] = "--Select BL Number--";
                    dt_Types.Rows.InsertAt(dr, 0);
                    if (dt_Types.Rows.Count > 0)
                    {
                        ddlBLNo.DataSource = dt_Types;
                        ddlBLNo.DisplayMember = "t_Name";
                        ddlBLNo.ValueMember = "t_ID";
                    }
                    //ddlBLNo.SelectedIndex = 0;
                    
                }
                else
                {
                    MessageBox.Show("No IGM-BL data found !! ", "IGM-BL Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlBLNo.DataSource = null;
                    ddlBLNo.Refresh();
                    ddlBLNo.Items.Clear();
                    ddlBLNo.Items.Insert(0, "No data found");
                    ddlBLNo.SelectedIndex = 0;
                    ddlBLNo.Enabled = false;
                    labelControl1.Focus();
                }
            }
        }
        private void ddlServiceName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtQuantity.Text = "";
            txtRatetk.Text = "";
            txtRatedlr.Text = "";
            int serviceId = Convert.ToInt32(ddlServiceName.SelectedValue);

           
            if (serviceId > 0)
            {
                if (serviceId == 1 || serviceId == 2) /// Delivery Package Unstuffed and Delivery Package On Chassis 
                {

                    ddlContSize.SelectedIndex = 0;
                    ddlContSize.Enabled = true;

                    ddlContSize.SelectedIndex = 0;
                    txtQuantity.Enabled = false;
                    txtRatetk.Enabled = false;
                    txtRatedlr.Enabled = false;

                    lblRateDlr.Visible = true;
                    txtRatedlr.Visible = true;

                    lblFreeDays.Visible = false;
                    txtFreedays.Visible = false;
                    lbluptodate.Visible = false;
                    dateUpto.Visible = false;

                    btnAdd.Enabled = true;                    
                }
                if (serviceId == 3) /// Detentio charge
                {
                    txtQuantity.Enabled = false;
                    txtRatetk.Enabled = false;

                    lblRateDlr.Visible = false;
                    txtRatedlr.Visible = false;

                    lblFreeDays.Visible = true;
                    txtFreedays.Visible = true;
                    lbluptodate.Visible = true;
                    dateUpto.Visible = true;

                    btnAdd.Enabled = true;                  
                }
                if (serviceId == 4) /// Extra Movement Charge
                {

                    ddlContSize.SelectedIndex = 0;
                    ddlContSize.Enabled = true;


                    txtQuantity.Enabled = false;
                    txtRatetk.Enabled = false;

                    lblRateDlr.Visible = false;
                    txtRatedlr.Visible = false;
                    txtRatedlr.Enabled = true;
                    lblFreeDays.Visible = false;
                    txtFreedays.Visible = false;
                    lbluptodate.Visible = false;
                    dateUpto.Visible = false;

                    btnAdd.Enabled = true;
                }
                if (serviceId > 4) /// Others
                {

                    //int serviceId = Convert.ToInt32(ddlServiceName.SelectedValue);
                    var Serdetail = objBll.GetServiceDetailByServiceId(serviceId);
                    if (Serdetail != null)
                    { txtRatetk.Text = Convert.ToString(Serdetail.RateTk); }
                    else { }
                    ddlContSize.SelectedIndex = 0;
                    ddlContSize.Enabled = false;
                    txtQuantity.Enabled = true;
                    txtRatetk.Enabled = false;
                    txtRatedlr.Enabled = true;

                    lblRateDlr.Visible = true;
                    txtRatedlr.Visible = true;

                    lblFreeDays.Visible = false;
                    txtFreedays.Visible = false;
                    lbluptodate.Visible = false;
                    dateUpto.Visible = false;

                    btnAdd.Enabled = true;                   
                }                
            }
            else
            {
                lblFreeDays.Visible = false;
                txtFreedays.Visible = false;

                lblRateDlr.Visible = true;
                txtRatedlr.Visible = true;

                lbluptodate.Visible = false;
                dateUpto.Visible = false;

                btnAdd.Enabled = false;
            }
            //txtQuantity.Enabled = true;
            //txtRatetk.Enabled = true;
            //txtRatedlr.Enabled = true;
        }
        private void ddlContSize_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int serviceId = Convert.ToInt32(ddlServiceName.SelectedValue);
            int sizeid = Convert.ToInt32(ddlContSize.SelectedValue);


            if (serviceId > 0)
            {
                if (serviceId == 1 || serviceId == 2)
                {

                    var Serdetail = objBll.GetServiceDetailByServiceIdwithSizeId(serviceId, sizeid);
                    txtRatetk.Text = Convert.ToString(Serdetail.RateTk);
                }

                if (serviceId == 4 )
                {

                    var Serdetail = objBll.GetServiceDetailByServiceIdwithSizeId(serviceId, sizeid);
                    txtRatedlr.Text = Convert.ToString(Serdetail.RateDollar);
                    lblRateDlr.Visible = true;
                    txtRatedlr.Visible = true;
                    txtRatedlr.Enabled = false;
                }


            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                bool flag = ValidateBillCollection();
                if (flag == true)
                {
                    if (objImportBill.ImportBillDetails.Count == 0)
                    {

                        MessageBox.Show("No service details data !!", "Service Details Required",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }
                    FillingMasterData();
                    var status = objBll.InsertImportBill(objImportBill);
                    MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    //SLNo = SLNo + 1;
                }
            }
            else if (btnSave.Text == "Update")
            {

                if (objImportBill.ImportBillDetails.Count == 0)
                {

                    MessageBox.Show("No service details data !!", "Service Details Required",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                                
                objImportBill.TotalAmount = Convert.ToDecimal(lblTotalAmount.Text);
                objImportBill.DiscountAmount = txtDiscountAmount.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDiscountAmount.Text.Trim()) : 0;
                objImportBill.IsPercentage = chkPercentage.Checked;
                objImportBill.VatPercent = txtVAT.Text.Trim().Length > 0 ? Convert.ToDecimal(txtVAT.Text.Trim()) : 0;
                objImportBill.GrandTotal = Convert.ToDecimal(lblGrandTotal.Text.Trim());
                objImportBill.VatAmount = Convert.ToDecimal(lblVatAmount.Text.Trim());
                objImportBill.BillCalculateDate = dateUpto.Value;               
                objImportBill.EditById = user.UserId;
                objImportBill.Modifieddate = DateTime.Now;

                DocumentstatusSelect(objImportBill);

                var status = objBll.UpdateImportBill(objImportBill);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                ClearForm();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??", "Confirm Import Bill deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var status = objBll.DeleteImportBill(objImportBill.ID);
                MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
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

        private void FillingMasterData()
        {

            objImportBill.ImporterId = Convert.ToInt32(ddlImporter.SelectedValue);
            objImportBill.IGMImportId = Convert.ToInt32(ddlBLNo.SelectedValue);
            objImportBill.BLNo = ddlBLNo.Text.Trim();
            objImportBill.ImpInvoiceNumber = txtImportInvoice.Text.Trim();
            objImportBill.CandFAgentId = Convert.ToInt32(ddlCFAgent.SelectedValue);
            objImportBill.FreeDays = txtFreedays.Text.Trim().Length > 0 ? Convert.ToInt16(txtFreedays.Text.Trim()) : 0;
            objImportBill.TotalAmount = Convert.ToDecimal(lblTotalAmount.Text);
            objImportBill.DiscountAmount = txtDiscountAmount.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDiscountAmount.Text.Trim()) : 0;            
            objImportBill.IsPercentage = chkPercentage.Checked;
            objImportBill.VatPercent = txtVAT.Text.Trim().Length > 0 ? Convert.ToDecimal(txtVAT.Text.Trim()) : 0;
            objImportBill.GrandTotal = Convert.ToDecimal(lblGrandTotal.Text.Trim());
            objImportBill.VatAmount = Convert.ToDecimal(lblVatAmount.Text.Trim());
            objImportBill.BillCalculateDate = dateUpto.Value;
            objImportBill.BillPrepareDate = DateTime.Now;
            objImportBill.SavedById = user.UserId;
            objImportBill.Entrydate = DateTime.Now;

            DocumentstatusSelect(objImportBill);
          

        }

        private void DocumentstatusSelect(ImportBill objImportBill)
        {
            if (rdoDraft.Checked == true)
            {
                objImportBill.Documentstatus = 1; // for Draft Document;
            }
            if (rdoInprogress.Checked == true)
            {
                objImportBill.Documentstatus = 2; // In Progress;
            }
            if (rdoPendingapproval.Checked == true)
            {
                objImportBill.Documentstatus = 3; // Pending approval;
            }
            if (rdoApproved.Checked == true)
            {
                objImportBill.Documentstatus = 4; // Approved;
            }
            if (rdoAdminAdjustment.Checked == true)
            {
                objImportBill.Documentstatus = 5; // Admin Adjustment;
            }
            if (rdoPaid.Checked == true)
            {
                objImportBill.Documentstatus = 6; // Paid;
            }

        }

        private bool ValidateBillCollection()
        {

            var errMessage = "";
            if (Convert.ToInt32(ddlImporter.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select an Importer !!\n";
            }
            if (Convert.ToInt32(ddlBLNo.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a BL number !!\n";
            }
            if (Convert.ToInt32(ddlCFAgent.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a C&F agent !!\n";
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

        private void ClearForm()
        {
            ddlImporter.SelectedIndex = 0;
            ddlImporter.Enabled = true;
            ddlBLNo.DataSource = null;
            ddlBLNo.Items.Clear();
            ddlBLNo.Text = "";
            ddlBLNo.Enabled = false;
            ddlCFAgent.SelectedIndex = 0;
            ddlCFAgent.Enabled = false;
            ddlServiceName.SelectedIndex = 0;
            ddlServiceName.Enabled = false;
            txtQuantity.Text = "";
            txtRatetk.Text = "";
            txtRatedlr.Text = "";

            lblRateDlr.Visible = true;
            txtRatedlr.Visible = true;

            lblFreeDays.Visible = false;
            txtFreedays.Visible = false;
            lbluptodate.Visible = false;
            dateUpto.Visible = false;

            btnAdd.Text = "Add";
            btnAdd.Enabled = false;
            btnDeleteDtls.Enabled = false;
            grdBill.Rows.Clear();
            lblTotalAmount.Text = "00.00";
            txtDiscountAmount.Text = "";
            txtVAT.Text = "";
            lblVatAmount.Text = "00.00";
            lblGrandTotal.Text = "00.00";
            chkPercentage.Checked = false;
            objImportBill = new ImportBill();
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            labelControl7.Focus();



        }



        #region IMPORT BILL DETAILS


        private void btnAdd_Click(object sender, EventArgs e)
         {
            if (btnAdd.Text == "Add")
            {
                int igmIdAgainstbl = Convert.ToInt16(ddlBLNo.SelectedValue);
                int serviceid = Convert.ToInt16(ddlServiceName.SelectedValue);
                DateTime uptodate = DateTime.Now;
                

                DataTable dt = new DataTable();

                if (serviceid == 1 || serviceid == 2)
              
                {
                    decimal rateTk = txtRatetk.Text.Trim().Length > 0 ? Convert.ToInt16(txtRatetk.Text.Trim()) : 0;
                    dt = objBll.GetDeliveryPackageCharge(igmIdAgainstbl, serviceid, dateUpto.Value, rateTk);
                    foreach (DataRow row in dt.Rows)
                    {
                        ImportBillDetail objDetails = new ImportBillDetail();
                        objDetails.ServiceId = serviceid;
                        objDetails.BillUnit = "Per Container";
                        objDetails.Size = Convert.ToInt32(row["ContainerSize"].ToString());
                        objDetails.Quantity = Convert.ToInt32(row["Quantity"].ToString());
                        objDetails.RateInTk = Convert.ToInt32(row["RateTk"].ToString());
                        objDetails.Days = Convert.ToInt32(row["Totaldays"].ToString());
                        objDetails.ImportIndate = Convert.ToDateTime(row["GateInDate"].ToString());
                        objDetails.ImportOutdate = Convert.ToDateTime(row["GateOutDate"].ToString());
                        objDetails.Total = Convert.ToInt32(row["Total"].ToString());
                      
                        totalAmount = totalAmount + objDetails.Total;

                        objImportBill.ImportBillDetails.Add(objDetails);
                        BindDataToGrid(objDetails);
                    }
                }


                if (serviceid == 3) /// This Service ID 3 is for detention charge
                {
                    var errMessage = "";
                    decimal rateTk = txtRatetk.Text.Trim().Length > 0 ? Convert.ToInt16(txtRatetk.Text.Trim()) : 0;
                    int freeDays = txtFreedays.Text.Trim().Length > 0 ? Convert.ToInt16(txtFreedays.Text.Trim()) : 0;
                    if (rateTk <= 0)
                    {
                        errMessage = errMessage + "* Please enter dollar rate in tk. !!\n";
                        txtRatetk.Focus();
                    }
                    if (freeDays <= 0)
                    {
                        errMessage = errMessage + "* Please enter free days. !!\n";
                        txtFreedays.Focus();
                    }                   
                    if (errMessage != "")
                    {
                        MessageBox.Show(errMessage, "Input required !!");
                        return ;
                    }
                    else
                    {
                        dt = objBll.GetDetentionCharg(igmIdAgainstbl, freeDays, dateUpto.Value, rateTk);
                        foreach (DataRow row in dt.Rows)
                        {
                            ImportBillDetail objDetails = new ImportBillDetail();
                            objDetails.ServiceId = serviceid;
                            objDetails.BillUnit = Convert.ToString(row["Slab"].ToString());
                            objDetails.Size = Convert.ToInt32(row["ContainerSize"].ToString());
                            objDetails.Quantity = Convert.ToInt32(row["Quantity"].ToString());
                            objDetails.Days = Convert.ToInt32(row["StorageDays"].ToString());
                            objDetails.RateInDlr = Convert.ToDecimal(row["RateInDlr"].ToString());
                            objDetails.RateInTk = 78;
                            objDetails.Total = Convert.ToDecimal(row["TotalInTaka"].ToString());
                            totalAmount = totalAmount + objDetails.Total;

                            objImportBill.ImportBillDetails.Add(objDetails);
                            BindDataToGrid(objDetails);
                        }
                    }                                     
                }

                if (serviceid > 3)
                {

                    bool flag = ValidateDetails();
                    if (flag == true)
                    {
                        ImportBillDetail objDetails = FillingBillDetails();
                        totalAmount = totalAmount + objDetails.Total;
                        objImportBill.ImportBillDetails.Add(objDetails);
                        BindDataToGrid(objDetails);

                    }
                    else
                    {
                        return;
                    }
                }

                CalculateTotalAmount();
                ClearDetails();
                //lblTotalAmount.Text = Convert.ToString(totalAmount);
                //ddlServiceName.SelectedIndex = 0;
                //txtQuantity.Text = "";
                //txtRatetk.Text = "";
                //txtRatedlr.Text = "";
            }
            else
            {
                //Update bill details
                var data = objImportBill.ImportBillDetails.ElementAt(index);
                if (data.ServiceId <= 3)
                {
                    //No action

                }
                if (data.ServiceId > 3)
                {
                    ImportBillDetail objDetails = FillingBillDetails();
                    objImportBill.ImportBillDetails.ElementAt(index).ServiceId = objDetails.ServiceId;
                    objImportBill.ImportBillDetails.ElementAt(index).Quantity = objDetails.Quantity;
                    objImportBill.ImportBillDetails.ElementAt(index).RateInTk = objDetails.RateInTk;
                    objImportBill.ImportBillDetails.ElementAt(index).RateInDlr = objDetails.RateInDlr;
                    objImportBill.ImportBillDetails.ElementAt(index).Total = objDetails.Total;
                    UpdateGrid(index, objDetails);
                    CalculateTotalAmount();
                }
              
                ClearDetails();
            }
           

        }
        private void btnDeleteMDetails_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                                      "Confirm Service Charge Deletion",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                var obj = objImportBill.ImportBillDetails.ElementAt(index);

                if (obj.ServiceId <= 3)
                {


                    foreach (ImportBillDetail item in objImportBill.ImportBillDetails.ToList())
                    {
                        if (item.ServiceId == obj.ServiceId)
                        {
                            objImportBill.ImportBillDetails.Remove(item);
                        }
                    }
                }
                else
                {
                    objImportBill.ImportBillDetails.Remove(obj);
                }

                LoadGrid();
                ClearDetails();
                CalculateTotalAmount();

            }

        }
        private bool ValidateDetails()
        {
            var errMessage = "";

            if (Convert.ToInt32(txtQuantity.Text.Trim().Length) == 0)
            {
                errMessage = errMessage + "* Please enter quantity !!\n";
            }
            if (Convert.ToInt32(txtRatetk.Text.Trim().Length) == 0)
            {
                errMessage = errMessage + "* Please enter rate !!\n";
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
        private ImportBillDetail FillingBillDetails()
        {

            ImportBillDetail objDetails = new ImportBillDetail();
            objDetails.ServiceId = Convert.ToInt32(ddlServiceName.SelectedValue);           
            objDetails.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());           
            objDetails.RateInTk = Convert.ToDecimal(txtRatetk.Text.Trim());
            objDetails.RateInDlr = txtRatedlr.Text.Trim().Length > 0 ? Convert.ToDecimal(txtRatedlr.Text.Trim()) : 0;

            if (objDetails.RateInDlr > 0)
            {
                objDetails.Total = Convert.ToDecimal(objDetails.RateInTk * objDetails.RateInDlr) * objDetails.Quantity;
            }
            else
            {
                objDetails.Total = Convert.ToDecimal(txtRatetk.Text.Trim()) * objDetails.Quantity;
            }

            return objDetails;

        }
        private void BindDataToGrid(ImportBillDetail objDetails)
        {
            grdBill.Rows.Add(listServices.Where(s => s.ID == objDetails.ServiceId).FirstOrDefault().ServiceName, objDetails.BillUnit, objDetails.Size, objDetails.Quantity, objDetails.ImportIndate,objDetails.ImportOutdate, objDetails.Days, objDetails.RateInTk, objDetails.RateInDlr, objDetails.Total);
            grdBill.ClearSelection();
        }
        private void LoadGrid()
        {
            grdBill.Rows.Clear();
            foreach (ImportBillDetail objDetails in objImportBill.ImportBillDetails)
            {
                grdBill.Rows.Add(listServices.Where(s => s.ID == objDetails.ServiceId).FirstOrDefault().ServiceName, objDetails.BillUnit, objDetails.Size, objDetails.Quantity, objDetails.Days, objDetails.RateInTk, objDetails.RateInDlr, objDetails.Total);
            }
            grdBill.ClearSelection();
           
        }                       
        private void UpdateGrid(int index, ImportBillDetail objDetails)
        {

            grdBill.Rows[index].Cells[0].Value = ddlServiceName.Text.Trim();
            grdBill.Rows[index].Cells[3].Value = objDetails.Quantity;
            grdBill.Rows[index].Cells[5].Value = objDetails.RateInTk;
            grdBill.Rows[index].Cells[6].Value = objDetails.RateInDlr;
            grdBill.Rows[index].Cells[7].Value = objDetails.Total;
            grdBill.ClearSelection();

        }
        private void ClearDetails()
        {

            //ddlServiceName.SelectedIndex = 0;
            txtQuantity.Text = "";
            txtRatetk.Text = "";
            txtRatedlr.Text = "";
            lblRateDlr.Visible = true;
            txtRatedlr.Visible = true;
            lblFreeDays.Visible = false;
            txtFreedays.Visible = false;
            lbluptodate.Visible = false;
            dateUpto.Visible = false;
            btnAdd.Text = "Add";
            btnDeleteDtls.Enabled = false;
            labelControl1.Focus();


        }
        private void CalculateTotalAmount()
        {
           

            totalAmount = 0;
            grandTotal = 0;
            foreach (var item in objImportBill.ImportBillDetails)
            {
                var amount = item.Total;
                totalAmount = Convert.ToDecimal(totalAmount + amount);
            }
            grandTotal = totalAmount;
            lblTotalAmount.Text = Convert.ToString(totalAmount);

            //calculate total amount and grand total after discount 
            if (txtDiscountAmount.Text.Trim().Length > 0)
            {
                decimal discountValue = Convert.ToDecimal(txtDiscountAmount.Text.Trim());
                if (chkPercentage.Checked == true)
                {
                    grandTotal = totalAmount - ((totalAmount * discountValue) / 100);
                }
                else
                {
                    grandTotal = totalAmount - discountValue;
                }
            }

            if (txtVAT.Text.Trim().Length > 0)
            {
                var VatRate = Convert.ToDecimal(txtVAT.Text.Trim());
                decimal uptoTotal = grandTotal;
                grandTotal = grandTotal + ((grandTotal * VatRate) / 100);
                var VATAmount = grandTotal - uptoTotal;
                lblVatAmount.Text = VATAmount.ToString();

            }

            lblGrandTotal.Text = Convert.ToString(grandTotal);

        }
        private void grdBill_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = grdBill.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = grdBill.Rows[selectedrowindex];
            index = Convert.ToInt32(selectedRow.Index);
            var data = objImportBill.ImportBillDetails.ElementAt(index);

            if (data.ServiceId <= 3)
            {
                ddlServiceName.SelectedValue = Convert.ToInt32(data.ServiceId);

            }
            if (data.ServiceId > 3)
            {
                ddlServiceName.SelectedValue = Convert.ToInt32(data.ServiceId);
                txtQuantity.Text = Convert.ToString(data.Quantity);
                txtRatetk.Text = Convert.ToString(data.RateInTk);
                txtRatedlr.Text = data.RateInDlr > 0 ? Convert.ToString(data.RateInDlr) : "";

            }


            btnAdd.Text = "Update";
            btnDeleteDtls.Enabled = true;

        }
        private void txtDiscountAmount_KeyUp(object sender, KeyEventArgs e)
        {
            if (totalAmount > 0)
            {
                if (txtDiscountAmount.Text.Trim().Length > 0)
                {
                    decimal discountValue = Convert.ToDecimal(txtDiscountAmount.Text.Trim());
                    if (chkPercentage.Checked == true)
                    {
                        grandTotal = totalAmount - ((totalAmount * discountValue) / 100);
                        if (txtVAT.Text.Trim().Length > 0)
                        {
                            decimal VatRate = Convert.ToDecimal(txtVAT.Text.Trim());
                            decimal netAmount = grandTotal + ((grandTotal * VatRate) / 100);
                            decimal VATAmount = netAmount - grandTotal;
                            lblVatAmount.Text = Convert.ToString(VATAmount);
                            lblGrandTotal.Text = Convert.ToString(netAmount);
                        }
                        else
                        {
                            lblVatAmount.Text = "00.00";
                            lblGrandTotal.Text = Convert.ToString(grandTotal);
                        }

                        //lblGrandTotal.Text = Convert.ToString(grandTotal);
                    }
                    else
                    {
                        grandTotal = totalAmount - discountValue;

                        if (txtVAT.Text.Trim().Length > 0)
                        {
                            decimal VatRate = Convert.ToDecimal(txtVAT.Text.Trim());
                            decimal netAmount = grandTotal + ((grandTotal * VatRate) / 100);
                            decimal VATAmount = netAmount - grandTotal;
                            lblVatAmount.Text = Convert.ToString(VATAmount);
                            lblGrandTotal.Text = Convert.ToString(netAmount);
                        }
                        else
                        {
                            lblVatAmount.Text = "00.00";
                            lblGrandTotal.Text = Convert.ToString(grandTotal);
                        }

                       // lblGrandTotal.Text = Convert.ToString(grandTotal);
                    }
                }
                else
                {
                    grandTotal = totalAmount;

                    if (txtVAT.Text.Trim().Length > 0)
                    {
                        decimal VatRate = Convert.ToDecimal(txtVAT.Text.Trim());
                        decimal netAmount = grandTotal + ((grandTotal * VatRate) / 100);
                        decimal VATAmount = netAmount - grandTotal;
                        lblVatAmount.Text = Convert.ToString(VATAmount);
                        lblGrandTotal.Text = Convert.ToString(netAmount);
                    }
                    else
                    {
                        lblVatAmount.Text = "00.00";
                        lblGrandTotal.Text = Convert.ToString(grandTotal);
                    }

                    //lblGrandTotal.Text = Convert.ToString(grandTotal);
                }
            }
        }
        private void chkPercentage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPercentage.Checked == true)
            {
                var length = txtDiscountAmount.Text.Trim().Length;
                if (length > 0 && totalAmount > 0)
                {
                    decimal discountRate = Convert.ToDecimal(txtDiscountAmount.Text.Trim());
                    grandTotal = totalAmount - ((totalAmount * discountRate) / 100);
                    lblGrandTotal.Text = Convert.ToString(grandTotal);
                }
            }
            else
            {
                var length = txtDiscountAmount.Text.Trim().Length;
                if (length > 0 && totalAmount > 0)
                {
                    decimal discountAmount = Convert.ToDecimal(txtDiscountAmount.Text.Trim());
                    grandTotal = totalAmount - discountAmount;
                    lblGrandTotal.Text = Convert.ToString(grandTotal);
                }

            }
        }
        private void txtVAT_KeyUp(object sender, KeyEventArgs e)
        {
            if (grandTotal > 0)
            {
                if (txtVAT.Text.Trim().Length > 0)
                {
                    decimal VatRate = Convert.ToDecimal(txtVAT.Text.Trim());                   
                    decimal netAmount = grandTotal + ((grandTotal * VatRate) / 100);
                    decimal VATAmount = netAmount - grandTotal;
                    lblVatAmount.Text = Convert.ToString(VATAmount);
                    lblGrandTotal.Text = Convert.ToString(netAmount);
                }
                else
                {
                    lblVatAmount.Text = "00.00";
                    lblGrandTotal.Text = Convert.ToString(grandTotal);
                }
            }
        }


        #endregion

        private void ImportBillCollection_FormClosing(object sender, FormClosingEventArgs e)
        {
            objImportBill = new ImportBill();
        }

       

        private void ddlBLNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBLNo.SelectedIndex > 0)
            {
                LoadGridContianerPosition(ddlBLNo.Text.ToString());
                
                Get_Indatewise_ImportContainer_Position(ddlBLNo.Text.ToString());
                txtImportInvoice.Text = IGMBll.GetImportInvoicenumber("ImportInvoice");

                IGMImport objIgmImport = IGMBll.GetIGMImportByBL(ddlBLNo.Text.ToString());
                ddlMLO.SelectedValue = objIgmImport.CustomerId;
                rdoDraft.Checked = true;

            }
        }

        private void Get_Indatewise_ImportContainer_Position(string Blnumber)
        {
            DataTable result = IGMBll.Get_Indatewise_ImportContainer_Position(Blnumber);
            grdIndatewiseContainerPosition.DataSource = result;
        }

     
        private void btnEditRate_Click(object sender, EventArgs e)
        {
            txtRatetk.Enabled = true;
            txtRatedlr.Enabled = true;
            txtQuantity.Enabled = true;
        }

      
        private void ddlServiceName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ddlContSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
