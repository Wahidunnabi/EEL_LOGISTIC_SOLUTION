using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Data;
using LOGISTIC.BLL;
using System.Linq;
using System.Runtime.InteropServices;

namespace LOGISTIC.UI
{
    public partial class ExportBillCollection : Form
    {


        private ShipperBLL shipperBll = new ShipperBLL();
        private ClearAndForwaderBll CandFBll = new ClearAndForwaderBll();
        private CustomerBll MLOBll = new CustomerBll();
        private FreightForwarderBLL FFBll = new FreightForwarderBLL();
        private ContainerSizeBll sizeBll = new ContainerSizeBll();
        private ContainerTypeBll ctBll = new ContainerTypeBll();

        private static List<Service> listServices = new List<Service>();
        private static List<ContainerSize> listSize = new List<ContainerSize>();

        private BillingBLL objBll = new BillingBLL();
        ExportBill objExportBill = new ExportBill();
        private UserInfo user;
            
        private static int index;

        decimal totalAmount = 0;
        decimal vatableAmount = 0;
        decimal grandTotal = 0;
       
        public ExportBillCollection( UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;
            chkPrint.Visible = false;
            pnlPrint.Visible = false;
            pnlDateRange.Visible = false;
            ddlShipper.Enabled = false;
            ddlCFAgent.Enabled = false;
            ddlForwarder.Visible = false;
            ddlMLO.Enabled = false;                       
            btnAdd.Enabled = false;
            btnDeleteDtls.Enabled = false;
            btnDelete.Enabled = false;

        }

        public ExportBillCollection(ExportBill objbill, UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.objExportBill = objbill;
            this.user = user;

            totalAmount = objbill.TotalAmount;
            if (objbill.VatAmount > 0)
            {
                vatableAmount = Convert.ToDecimal((objbill.VatAmount * 100) / objbill.VatPercent);
            }           
            grandTotal = objbill.GrandTotal; ;

            btnSave.Text = "Update";           

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
        
        private void ExportBillCollection_Load(object sender, EventArgs e)
        {

            LoadBillType();
            FormSetup();
            listServices = objBll.GetAllExportServices();
            LoadShipper();
            LoadCandFAgent();
            LoadMLO();
            LoadFrdForwarder();
            LoadExportServices();
            LoadSize();
            PrepareGrid();
            LoadConType();
            //PrepareGridFOrMLOFarwaderSummerySum();
            txtImportInvoice.Text = objBll.GetImportInvoicenumber("ExportInvoice");
            if (objExportBill.ID > 0)
            {
                ShowExportBillData();

                if (objExportBill.ExportBillDetails.Count > 0)
                {
                    foreach (ExportBillDetail objDetails in objExportBill.ExportBillDetails)
                    {
                        grdBill.Rows.Add(objDetails.EFRNo, listServices.Where(s => s.ID == objDetails.ServiceId).FirstOrDefault().ServiceName, objDetails.Size, objDetails.Quantity, objDetails.Rate, objDetails.IsVAT==true?"YES":"NO", objDetails.Total);
                    }
                    grdBill.ClearSelection();
                }

                btnDeleteDtls.Enabled = false;

            }
            else
            {              
                //ddlCFAgent.Enabled = false;
                //ddlServiceName.Enabled = false;
                btnDelete.Enabled = false;
            }

        }


        #region LOAD BASIC DATA     

        private void FormSetup()
        {
           
            txtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtQuantity.Properties.Mask.EditMask = "\\d+";

            txtRatetk.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRatetk.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtDiscountAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtDiscountAmount.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtVAT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtVAT.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,1})?";

        }


        private void LoadBillType()
        {

            ddlBilltype.Items.Insert(0, "--Select Bill Type--");
            ddlBilltype.Items.Insert(1, "Shipper");
            ddlBilltype.Items.Insert(2, "FForwarder");
            ddlBilltype.Items.Insert(3, "MLO");
            ddlBilltype.SelectedIndex = 0;
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

        private void LoadMLO()
        {

            var type = MLOBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode.Trim());
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

        private void LoadFrdForwarder()
        {

            var type = FFBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.FreightForwarderId, t.FreightForwarderName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select F/Forwarder --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlForwarder.DataSource = dt_Types;
                ddlForwarder.DisplayMember = "t_Name";
                ddlForwarder.ValueMember = "t_ID";
            }
            ddlForwarder.SelectedIndex = 0;

        }

        private void LoadExportServices()
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

        private void LoadSize()
        {

            listSize = sizeBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in listSize)
            {
                dt_Types.Rows.Add(t.ContainerSizeId, t.ContainerSize1);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Size --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlSize.DataSource = dt_Types;
                ddlSize.DisplayMember = "t_Name";
                ddlSize.ValueMember = "t_ID";
            }
            ddlSize.SelectedIndex = 0;

        }

        public void PrepareGrid()
        {

            grdBill.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            grdBill.EnableHeadersVisualStyles = false;
            grdBill.AutoGenerateColumns = false;
            grdBill.AllowUserToAddRows = false;
            grdBill.ColumnCount = 7;

            grdBill.Columns[0].Width = 100;
            grdBill.Columns[0].HeaderText = "EFR No";

            grdBill.Columns[1].Width = 160;
            grdBill.Columns[1].HeaderText = "Service Name";
            
            grdBill.Columns[2].Width = 60;
            grdBill.Columns[2].HeaderText = "Size";

            grdBill.Columns[3].Width = 60;
            grdBill.Columns[3].HeaderText = "Quantity";

            grdBill.Columns[4].Width = 60;
            grdBill.Columns[4].HeaderText = "Rate";

            grdBill.Columns[5].Width = 60;
            grdBill.Columns[5].HeaderText = "VAT";

            grdBill.Columns[6].Width = 100;
            grdBill.Columns[6].HeaderText = "Total";

        }
        public void PrepareGridFOrMLOFarwaderSummery()
        {

            grdMLOFFarwaderSummery.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            grdMLOFFarwaderSummery.EnableHeadersVisualStyles = false;
            grdMLOFFarwaderSummery.AutoGenerateColumns = false;
            grdMLOFFarwaderSummery.AllowUserToAddRows = false;
            grdMLOFFarwaderSummery.ColumnCount = 7;

            grdMLOFFarwaderSummery.Columns[0].Width = 100;
            grdMLOFFarwaderSummery.Columns[0].HeaderText = "FreightForwarderName";

            grdMLOFFarwaderSummery.Columns[1].Width = 160;
            grdMLOFFarwaderSummery.Columns[1].HeaderText = "CustomerName";

            grdMLOFFarwaderSummery.Columns[2].Width = 60;
            grdMLOFFarwaderSummery.Columns[2].HeaderText = "ContainerNo";

            grdMLOFFarwaderSummery.Columns[3].Width = 60;
            grdMLOFFarwaderSummery.Columns[3].HeaderText = "DateIn";

            grdMLOFFarwaderSummery.Columns[4].Width = 60;
            grdMLOFFarwaderSummery.Columns[4].HeaderText = "StuffingDate";

            grdMLOFFarwaderSummery.Columns[5].Width = 60;
            grdMLOFFarwaderSummery.Columns[5].HeaderText = "DateOut";

            grdMLOFFarwaderSummery.Columns[6].Width = 100;
            grdMLOFFarwaderSummery.Columns[6].HeaderText = "Storage Days";

        }
        public void PrepareGridFOrMLOFarwaderSummerySum()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = true;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].HeaderText = "Container Size";

            dataGridView1.Columns[1].Width = 160;
            dataGridView1.Columns[1].HeaderText = "Container Type";

            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[2].HeaderText = "Storage days";

            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[3].HeaderText = "Total Container";


        }
        private void ShowExportBillData()
        {
            int billtype = objExportBill.BillType;
            ddlBilltype.SelectedIndex = billtype;
            ddlBilltype.Enabled = false;
            chkPrint.Visible = false;
            pnlPrint.Visible = false;
            if (billtype == 1)
            {
                ddlShipper.SelectedValue = objExportBill.ShipperId;
                lblAgentName.Text = "C && F Agent";
                ddlCFAgent.SelectedValue = objExportBill.CandFAgentId;
                ddlCFAgent.Visible = true;
                ddlForwarder.Visible = false;
                ddlMLO.SelectedValue = objExportBill.CustId;
                txtEFRNo.Text = objExportBill.EFRNo;
                pnlQSR.Visible = true;
                pnlDateRange.Visible = false;           
            }
            if (billtype == 2)
            {
                ddlShipper.Enabled = false;                              
                ddlMLO.Enabled = false;
                ddlForwarder.Visible = true;
                txtEFRNo.Text = objExportBill.EFRNo;
                lblAgentName.Text = "F/Forwarder";               
                ddlCFAgent.Visible = false;
                ddlForwarder.SelectedValue = objExportBill.FredForderId;
                ddlForwarder.Visible = true;
                pnlQSR.Visible = true;
                pnlDateRange.Visible = false;

            }
            if (billtype == 3)
            {
                ddlShipper.Enabled = false;               
                ddlCFAgent.Enabled = false;
                ddlMLO.Enabled = true;
                ddlForwarder.Visible = false;
                txtEFRNo.Enabled = false;
                lblAgentName.Text = "C && F Agent";                
                ddlCFAgent.Visible = true;
                ddlMLO.SelectedValue = objExportBill.CustId;
                ddlForwarder.Visible = false;
                pnlQSR.Visible = false;
                pnlDateRange.Visible = true;

            }
           
            lblTotalAmount.Text = String.Format("{0:0.00}", objExportBill.TotalAmount);
            txtDiscountAmount.Text = String.Format("{0:0.00}", objExportBill.DiscountAmount);
            chkPercentage.Checked = objExportBill.IsPercentage == true ? true : false;
            txtVAT.Text = String.Format("{0:0.00}", objExportBill.VatPercent);
            lblVatAmount.Text = String.Format("{0:0.00}", objExportBill.VatAmount);
            lblGrandTotal.Text = Convert.ToString(objExportBill.GrandTotal);

           
           


        }

        #endregion


        private void ddlBilltype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkPrint.Checked = false;
            txtEFRNo.Text = "";
            ddlServiceName.SelectedIndex = 0;
            txtQuantity.Text = "";
            ddlSize.SelectedIndex = 0;
            txtRatetk.Text = "";
            chkVAT.Checked = false;
            objExportBill = new ExportBill();
            grdBill.Rows.Clear();

            int billType = Convert.ToInt32(ddlBilltype.SelectedIndex);

            if (billType > 0)
            {
                if (billType == 1) //Shipper or C&F Agent
                {
                    chkPrint.Visible = false;                   
                    ddlShipper.Enabled = true;
                    lblAgentName.Text = "C && F Agent";
                    ddlCFAgent.Visible = true;
                    ddlCFAgent.Enabled = true;
                    ddlForwarder.Visible = false;
                    ddlMLO.SelectedIndex = 0;
                    ddlMLO.Enabled = true;
                    txtEFRNo.Enabled = true;
                    pnlQSR.Visible = true;
                    pnlDateRange.Visible = false;
                                      
                }
                if (billType == 2) //Freight Forwarder 
                {
                    chkPrint.Visible = true;                    
                    ddlShipper.SelectedIndex = 0;
                    ddlShipper.Enabled = false;
                    lblAgentName.Text = "F/Forwarder";
                    ddlCFAgent.Visible = false;
                    ddlForwarder.Visible = true;
                    ddlMLO.SelectedIndex = 0;
                    ddlMLO.Enabled = false;
                    txtEFRNo.Enabled = true;
                    pnlQSR.Visible = true;
                    pnlDateRange.Visible = false;

                }
                if (billType == 3) //MLO
                {
                    chkPrint.Visible = true;                  
                    ddlShipper.Enabled = false;
                    lblAgentName.Text = "C && F Agent";
                    ddlCFAgent.Visible = true;
                    ddlCFAgent.Enabled = false;
                    ddlMLO.Enabled = true;
                    ddlForwarder.Visible = false;
                    txtEFRNo.Enabled = false;
                    pnlQSR.Visible = true;
                    pnlDateRange.Visible = true;
                }
            }
            else
            {
                chkPrint.Visible = false;               
                ddlShipper.SelectedIndex = 0;
                ddlShipper.Enabled = false;
                lblAgentName.Text = "C && F Agent";
                ddlCFAgent.Visible = true;
                ddlCFAgent.Enabled = false;
                ddlCFAgent.SelectedIndex = 0;
                ddlMLO.Enabled = false;
                ddlMLO.SelectedIndex = 0;
                ddlForwarder.Visible = false;
                pnlQSR.Visible = true;
                pnlDateRange.Visible = false;
            }
            
        }

        private void ddlServiceName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtQuantity.Text = "";
            txtRatetk.Text = "";           
            int serviceId = Convert.ToInt32(ddlServiceName.SelectedValue);

            if (serviceId > 0)
            {
                btnAdd.Enabled = true;                   
            }
            else
            {
                btnAdd.Enabled = false;
            }

        }

        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrint.Checked == true)
            {
                pnlPrint.Visible = true;
            }
            else
            {
                pnlPrint.Visible = false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                bool flag = ValidateBillCollection();
                if (flag == true)
                {
                    if (objExportBill.ExportBillDetails.Count == 0)
                    {

                        MessageBox.Show("No service details data !!", "Service Details Required",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }
                    FillingMasterData();
                    var status = objBll.InsertExportBill(objExportBill);
                    MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    //SLNo = SLNo + 1;
                }
            }
            else if (btnSave.Text == "Update")
            {


                if (objExportBill.ExportBillDetails.Count == 0)
                {

                    MessageBox.Show("No service details data !!", "Service Details Required",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                
                objExportBill.ShipperId = Convert.ToInt32(ddlShipper.SelectedValue);
                objExportBill.CandFAgentId = Convert.ToInt32(ddlCFAgent.SelectedValue);
                objExportBill.FredForderId = Convert.ToInt32(ddlForwarder.SelectedValue);
                objExportBill.CustId = Convert.ToInt32(ddlMLO.SelectedValue);
                objExportBill.EFRNo = txtEFRNo.Text.Trim().Length > 0 ? Convert.ToString(txtEFRNo.Text.Trim()) : "";                                
                objExportBill.TotalAmount = Convert.ToDecimal(lblTotalAmount.Text);
                objExportBill.DiscountAmount = txtDiscountAmount.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDiscountAmount.Text.Trim()) : 0;
                objExportBill.IsPercentage = chkPercentage.Checked;
                objExportBill.VatPercent = txtVAT.Text.Trim().Length > 0 ? Convert.ToDecimal(txtVAT.Text.Trim()) : 0;
                objExportBill.GrandTotal = Convert.ToDecimal(lblGrandTotal.Text.Trim());
                objExportBill.VatAmount = Convert.ToDecimal(lblVatAmount.Text.Trim());                          
                objExportBill.EditById = user.UserId;

                var status = objBll.UpdateExportBill(objExportBill);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                ClearForm();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??", "Confirm Import Bill deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var status = objBll.DeleteExportBill(objExportBill.ID);
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

            objExportBill.BillType = Convert.ToInt32(ddlBilltype.SelectedIndex);
            objExportBill.ShipperId = Convert.ToInt32(ddlShipper.SelectedValue);
            objExportBill.CandFAgentId = Convert.ToInt32(ddlCFAgent.SelectedValue);
            objExportBill.FredForderId = Convert.ToInt32(ddlForwarder.SelectedValue);
            objExportBill.CustId = Convert.ToInt32(ddlMLO.SelectedValue);
            objExportBill.EFRNo = txtEFRNo.Text.Trim();
            objExportBill.TotalAmount = Convert.ToDecimal(lblTotalAmount.Text);
            objExportBill.DiscountAmount = txtDiscountAmount.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDiscountAmount.Text.Trim()) : 0;
            objExportBill.IsPercentage = chkPercentage.Checked;
            objExportBill.VatPercent = txtVAT.Text.Trim().Length > 0 ? Convert.ToDecimal(txtVAT.Text.Trim()) : 0;
            objExportBill.GrandTotal = Convert.ToDecimal(lblGrandTotal.Text.Trim());
            objExportBill.VatAmount = Convert.ToDecimal(lblVatAmount.Text.Trim());        
            objExportBill.BillPrepareDate = DateTime.Now;
            objExportBill.SavedById = user.UserId;

        }

        private bool ValidateBillCollection()
        {

            var errMessage = "";
            var billtype = Convert.ToInt32(ddlBilltype.SelectedIndex);
            if (billtype == 0)
            {
                errMessage = errMessage + "* Please select an Importer !!\n";
            }           
            if (billtype == 1) // Shipper wise bill
            {
                if (Convert.ToInt32(ddlShipper.SelectedValue) == 0)
                {
                    errMessage = errMessage + "* Please select a Shipper !!\n";
                }
                if (Convert.ToInt32(ddlCFAgent.SelectedValue) == 0)
                {
                    errMessage = errMessage + "* Please select a C&F agent !!\n";
                }
                if (Convert.ToInt32(ddlMLO.SelectedValue) == 0)
                {
                    errMessage = errMessage + "* Please select an MLO !!\n";
                }
                if (Convert.ToString(txtEFRNo.Text.Trim()).Length == 0)
                {
                    errMessage = errMessage + "* Please input EFR no !!\n";
                }
            }
            if (billtype == 2) // Freight Forwarder wise bill
            {
                if (Convert.ToInt32(ddlForwarder.SelectedValue) == 0)
                {
                    errMessage = errMessage + "* Please select a F/Forwarder agent !!\n";
                }               
            }
            if (billtype == 3) // MLO wise bill
            {
                if (Convert.ToInt32(ddlMLO.SelectedValue) == 0)
                {
                    errMessage = errMessage + "* Please select an MLO !!\n";
                }
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
            chkPrint.Checked = false;
            ddlBilltype.SelectedIndex = 0;
            ddlShipper.SelectedIndex = 0;
            ddlShipper.Enabled = false;            
            lblAgentName.Text = "C&F Agent";
            ddlCFAgent.SelectedIndex = 0;
            ddlCFAgent.Enabled = false;
            ddlCFAgent.Visible = true;
            ddlForwarder.Visible = false;           
            ddlServiceName.SelectedIndex = 0;         
            chkVAT.Checked = false;
            txtEFRNo.Text = "";
            txtQuantity.Text = "";
            ddlSize.SelectedIndex = 0;
            txtRatetk.Text = "";
            pnlQSR.Visible = true;
            pnlDateRange.Visible = false;
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
            objExportBill = new ExportBill();
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            lblbilltype.Focus();


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string MLOFFname = "";
            int MloFordId = 0;
            int billType = Convert.ToInt32(ddlBilltype.SelectedIndex);

            if (billType == 2)
            {
                MloFordId = Convert.ToInt32(ddlForwarder.SelectedValue);
                if (MloFordId == 0)
                {
                    MessageBox.Show("Please select a F/Forwarder !!", "Data Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MLOFFname = Convert.ToString(ddlForwarder.Text.Trim());
            }
            else
            {
                MloFordId = Convert.ToInt32(ddlMLO.SelectedValue);
                if (MloFordId == 0)
                {
                    MessageBox.Show("Please select a MLO !!", "Data Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MLOFFname = Convert.ToString(ddlMLO.Text.Trim());
            }

            var fromDate = dateFrom.Value;
            var toDate = dateTo.Value;
            var EFRNO = txtEFRNo.Text.ToString();
            string reportName = "Export Stuffing Details";
            string FileName = "D:\\" + reportName + " of " + MLOFFname + " from " + fromDate.Date.ToString("dd MMM yy") + " to " + toDate.Date.ToString("dd MMM yy") + ".xlsx";
                     
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

                xlSheet.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 130, 5, 60, 35);
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

                xlSheet.Cells[5, 1].value = "Export Stuffing Details of " + MLOFFname + " from " + fromDate.Date.ToString("dd/MMM/yy") + "  to  " + toDate.Date.ToString("dd/MMM/yy");
                xlSheet.Cells[5, 1].Font.Bold = true;
                xlSheet.Cells[5, 1].Font.Size = 10;
                xlSheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlSheet.Range["A5:M5"].MergeCells = true;



                DataTable dt = new DataTable();
                dt = objBll.GetExportStuffingDetails(billType, MloFordId, EFRNO, fromDate, toDate);

                // column headings
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    xlSheet.Cells[7, i + 1].value = dt.Columns[i].ToString().ToUpper();

                }
                xlSheet.Cells[7, 1].EntireRow.Font.Bold = true;

                xlSheet.Columns.AutoFit();


                int r = 8;

                // rows
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        xlSheet.Cells[i + r, j + 1] = dt.Rows[i][j];
                    }
                }


                #endregion

                xlSheet.Columns.AutoFit();

                xlSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlSheet.Select();

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

            }

            chkPrint.Checked = false;
        }


        #region EXPORT BILL DETAILS

        private void btnAdd_Click(object sender, EventArgs e)
       {
            if (btnAdd.Text == "Add")
            {
                int billType = Convert.ToInt16(ddlBilltype.SelectedIndex);               

                if (billType == 1)  //Shipper or C&F wise bill
                {

                    bool flag = ValidateBillDetails(1);
                    if (flag == true)
                    {
                        ExportBillDetail objDetails = FillingBillDetails();
                        totalAmount = totalAmount + objDetails.Total;
                        if (objDetails.IsVAT == true)
                        {
                            vatableAmount = vatableAmount + objDetails.Total;
                        }
                        objExportBill.ExportBillDetails.Add(objDetails);
                        BindDataToGrid(objDetails);
                    }
                    else
                    {
                        return;
                    }                  
                }


                if (billType == 2) // Freight Forwarder wise bill
                {
                    bool flag = ValidateBillDetails(2);
                    if (flag == true)
                    {
                        ExportBillDetail objDetails = FillingBillDetails();
                        totalAmount = totalAmount + objDetails.Total;
                        if (objDetails.IsVAT == true)
                        {
                            vatableAmount = vatableAmount + objDetails.Total;
                        }
                        objExportBill.ExportBillDetails.Add(objDetails);
                        BindDataToGrid(objDetails);
                    }
                    else
                    {
                        return;
                    }                    
                }

                if (billType == 3) // MLO wise bill
                {

                    bool flag = ValidateBillDetails(3);
                    if (flag == true)
                    {
                        int MLoId = Convert.ToInt32(ddlMLO.SelectedValue);
                        int serviceId = Convert.ToInt32(ddlServiceName.SelectedValue);
                        DateTime fromDate = dtbillFrom.Value;
                        DateTime toDate = dtbillTo.Value;

                        DataTable dt = new DataTable();
                        dt = objBll.GetMLOWiseServiceCharg(MLoId, serviceId, fromDate, toDate);
                        foreach (DataRow row in dt.Rows)
                        {
                            ExportBillDetail objDetails = new ExportBillDetail();
                            objDetails.BillId = objExportBill.ID;
                            objDetails.ServiceId = serviceId;
                            objDetails.EFRNo = Convert.ToString(row["EFRNo"].ToString());
                            objDetails.Size = Convert.ToInt32(row["ContainerSizeId"].ToString());
                            objDetails.Quantity = Convert.ToInt32(row["Quantity"].ToString());
                            objDetails.Rate = Convert.ToDecimal(row["Rate"].ToString());
                            objDetails.Total = Convert.ToDecimal(row["Total"].ToString());
                            objDetails.IsVAT = chkVAT.Checked == true ? true : false;
                            totalAmount = totalAmount + objDetails.Total;
                            if (objDetails.IsVAT == true)
                            {
                                vatableAmount = vatableAmount + objDetails.Total;
                            }
                            objExportBill.ExportBillDetails.Add(objDetails);
                            BindDataToGrid(objDetails);
                        }
                    }
                    else
                    {
                        return;
                    }                   
                }

                CalculateTotalAmount();
                ClearDetails();

            }
            else
            {
                //Update bill details
                var data = objExportBill.ExportBillDetails.ElementAt(index);

                ExportBillDetail objDetails = FillingBillDetails();
                objExportBill.ExportBillDetails.ElementAt(index).EFRNo = objDetails.EFRNo;
                objExportBill.ExportBillDetails.ElementAt(index).ServiceId = objDetails.ServiceId;
                objExportBill.ExportBillDetails.ElementAt(index).Quantity = objDetails.Quantity;
                objExportBill.ExportBillDetails.ElementAt(index).Rate = objDetails.Rate;
                objExportBill.ExportBillDetails.ElementAt(index).Total = objDetails.Total;
                objExportBill.ExportBillDetails.ElementAt(index).IsVAT = objDetails.IsVAT;

                UpdateGrid(index, objDetails);
                CalculateTotalAmount();
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

                var obj = objExportBill.ExportBillDetails.ElementAt(index);

                if (obj.ServiceId <= 3)
                {


                    foreach (ExportBillDetail item in objExportBill.ExportBillDetails.ToList())
                    {
                        if (item.ServiceId == obj.ServiceId)
                        {
                            objExportBill.ExportBillDetails.Remove(item);
                        }
                    }
                }
                else
                {
                    objExportBill.ExportBillDetails.Remove(obj);
                }

                LoadGrid();
                ClearDetails();
                CalculateTotalAmount();

            }

        }
        private bool ValidateBillDetails(int billtype)
        {
            var errMessage = "";
                       
            if (billtype > 0)
            {
                if (billtype == 1) //Shipper or C&F wise bill
                {

                    if (Convert.ToInt32(ddlShipper.SelectedValue) == 0)
                    {
                        errMessage = errMessage + "* Please select a shipper !!\n";
                    }
                    if (Convert.ToInt32(ddlCFAgent.SelectedValue) == 0)
                    {
                        errMessage = errMessage + "* Please select a C&F agent !!\n";
                    }
                    if (Convert.ToString(txtEFRNo.Text.Trim()) == "")
                    {
                        errMessage = errMessage + "* Please input EFR number !!\n";
                    }
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

                if (billtype == 2) // Freight Forwarder wise bill
                {
                    if (Convert.ToInt32(ddlForwarder.SelectedValue) == 0)
                    {
                        errMessage = errMessage + "* Please select a Freight Forwarder !!\n";
                    }
                    if (Convert.ToString(txtEFRNo.Text.Trim()) == "")
                    {
                        errMessage = errMessage + "* Please input EFR number !!\n";
                    }
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

                if (billtype == 3) // MLO wise bill
                {
                    if (Convert.ToInt32(ddlMLO.SelectedValue) == 0)
                    {
                        errMessage = errMessage + "* Please select an MLO !!\n";
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
                return false;
            }
            else
            {
                errMessage = errMessage + "* Please select a bill type !!\n";
                MessageBox.Show(errMessage, "Input required !!");
                return false;

            }

            
        }
        private ExportBillDetail FillingBillDetails()
        {

            ExportBillDetail objDetails = new ExportBillDetail();
            objDetails.BillId = objExportBill.ID;
            objDetails.EFRNo = txtEFRNo.Text.Trim();
            objDetails.ServiceId = Convert.ToInt32(ddlServiceName.SelectedValue);           
            objDetails.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            objDetails.Size = Convert.ToInt32(ddlSize.SelectedValue);
            objDetails.Rate = Convert.ToDecimal(txtRatetk.Text.Trim());
            objDetails.Total = Convert.ToDecimal(objDetails.Rate) * objDetails.Quantity;
            objDetails.TypeId = Convert.ToInt32(cmbConType.SelectedValue);
            objDetails.IsVAT = chkVAT.Checked == true ? true : false;

            return objDetails;

        }
        private void BindDataToGrid(ExportBillDetail objDetails)
        {
            grdBill.Rows.Add(objDetails.EFRNo, listServices.Where(s => s.ID == objDetails.ServiceId).FirstOrDefault().ServiceName, objDetails.Size > 0 ? listSize.Where(s=>s.ContainerSizeId== objDetails.Size).FirstOrDefault().ContainerSize1:null, objDetails.Quantity, objDetails.Rate, objDetails.IsVAT==true?"Yes":"No", objDetails.Total);
            grdBill.ClearSelection();
        }
        private void LoadGrid()
        {
            grdBill.Rows.Clear();
            foreach (ExportBillDetail objDetails in objExportBill.ExportBillDetails)
            {
                grdBill.Rows.Add(objDetails.EFRNo, listServices.Where(s => s.ID == objDetails.ServiceId).FirstOrDefault().ServiceName, objDetails.Size > 0 ? listSize.Where(s => s.ContainerSizeId == objDetails.Size).FirstOrDefault().ContainerSize1 : null, objDetails.Quantity, objDetails.Rate, objDetails.IsVAT == true ? "Yes" : "No", objDetails.Total);
            }
            grdBill.ClearSelection();
           
        }                       
        private void UpdateGrid(int index, ExportBillDetail objDetails)
        {

            grdBill.Rows[index].Cells[0].Value = objDetails.EFRNo;
            grdBill.Rows[index].Cells[1].Value = ddlServiceName.Text.Trim();
            grdBill.Rows[index].Cells[2].Value = objDetails.Size > 0 ? listSize.Where(s => s.ContainerSizeId == objDetails.Size).FirstOrDefault().ContainerSize1 : null;
            grdBill.Rows[index].Cells[3].Value = objDetails.Quantity;
            grdBill.Rows[index].Cells[4].Value = objDetails.Rate;
            grdBill.Rows[index].Cells[5].Value = objDetails.IsVAT==true?"Yes":"No";
            grdBill.Rows[index].Cells[6].Value = objDetails.Total;
            grdBill.ClearSelection();

        }
        private void ClearDetails()
        {

            ddlServiceName.SelectedIndex = 0;
            txtQuantity.Text = "";
            ddlSize.SelectedIndex = 0;
            txtRatetk.Text = "";
            chkVAT.Checked = false;
            btnAdd.Text = "Add";
            btnAdd.Enabled = false;
            btnDeleteDtls.Enabled = false;
            lblbilltype.Focus();


        }
       
        private void grdBill_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = grdBill.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = grdBill.Rows[selectedrowindex];
            index = Convert.ToInt32(selectedRow.Index);
            var data = objExportBill.ExportBillDetails.ElementAt(index);

            txtEFRNo.Text = Convert.ToString(data.EFRNo);
            ddlServiceName.SelectedValue = Convert.ToInt32(data.ServiceId);           
            txtQuantity.Text = Convert.ToString(data.Quantity);
            ddlSize.SelectedValue = Convert.ToInt32(data.Size);           
            txtRatetk.Text = Convert.ToString(data.Rate);           
            if (data.IsVAT == true)
            {
                chkVAT.Checked = true;
            }
            else
            {
                chkVAT.Checked = false;
            }

            btnAdd.Text = "Update";
            btnAdd.Enabled = true;
            btnDeleteDtls.Enabled = true;

        }

        private void CalculateTotalAmount()
        {

            vatableAmount = 0;
            totalAmount = 0;            
            grandTotal = 0;

            foreach (var item in objExportBill.ExportBillDetails)
            {
                if (item.IsVAT == true)
                {
                    vatableAmount = vatableAmount + item.Total;
                }               
                totalAmount = totalAmount + item.Total;
            }
            grandTotal = totalAmount;
            lblTotalAmount.Text = String.Format("{0:0.00}", totalAmount);

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
                var VATAmount = ((vatableAmount * VatRate) / 100);
                grandTotal = grandTotal + VATAmount;
                lblVatAmount.Text = String.Format("{0:0.00}", VATAmount);

            }

            lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);

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
                            decimal VATAmount = ((vatableAmount * VatRate) / 100);
                            decimal netAmount = grandTotal + VATAmount;                                                      
                            lblVatAmount.Text = String.Format("{0:0.00}", VATAmount);
                            lblGrandTotal.Text = String.Format("{0:0.00}", netAmount);
                        }
                        else
                        {
                            lblVatAmount.Text = "00.00";
                            lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
                        }

                        //lblGrandTotal.Text = Convert.ToString(grandTotal);
                    }
                    else
                    {
                        grandTotal = totalAmount - discountValue;

                        if (txtVAT.Text.Trim().Length > 0)
                        {
                            decimal VatRate = Convert.ToDecimal(txtVAT.Text.Trim());
                            decimal VATAmount = ((vatableAmount * VatRate) / 100);
                            decimal netAmount = grandTotal + VATAmount;                        
                            lblVatAmount.Text = String.Format("{0:0.00}", VATAmount);
                            lblGrandTotal.Text = String.Format("{0:0.00}", netAmount);
                        }
                        else
                        {
                            lblVatAmount.Text = "00.00";
                            lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
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
                        decimal VATAmount = ((vatableAmount * VatRate) / 100);
                        decimal netAmount = grandTotal + VATAmount;
                       
                        lblVatAmount.Text = String.Format("{0:0.00}", VATAmount);
                        lblGrandTotal.Text = String.Format("{0:0.00}", netAmount);
                    }
                    else
                    {
                        lblVatAmount.Text = "00.00";
                        lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
                    }

                    //lblGrandTotal.Text = Convert.ToString(grandTotal);
                }
            }
        }
        private void chkPercentage_CheckedChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();          
        }
        private void txtVAT_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();

            //if (vatableAmount > 0)
            //{
            //    if (txtVAT.Text.Trim().Length > 0)
            //    {
            //        decimal VatRate = Convert.ToDecimal(txtVAT.Text.Trim());
            //        decimal VATAmount = ((vatableAmount * VatRate) / 100);
            //        decimal netAmount = grandTotal + VATAmount;                   
            //        lblVatAmount.Text = Convert.ToString(VATAmount);
            //        lblGrandTotal.Text = Convert.ToString(netAmount);
            //    }
            //    else
            //    {
            //        lblVatAmount.Text = "00.00";
            //        lblGrandTotal.Text = Convert.ToString(grandTotal);
            //    }
            //}
        }

        private void ExportBillCollection_FormClosing(object sender, FormClosingEventArgs e)
        {
            objExportBill = new ExportBill();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

            int MLOID = Convert.ToInt32(ddlMLO.SelectedValue);
            int FFID = Convert.ToInt32(ddlForwarder.SelectedValue);
            DataTable dt = new DataTable();
            dt = objBll.Export_MLO_FFarwarder_SummeryGrid(MLOID, FFID, dtbillFrom.Value.Date, dtbillTo.Value.Date);
            grdMLOFFarwaderSummery.DataSource = dt;
            DataTable dtsum = new DataTable();
            dtsum = objBll.Export_MLO_FFarwarder_SummeryGridSum(MLOID, FFID, dtbillFrom.Value.Date, dtbillTo.Value.Date);
            dataGridView1.DataSource = dtsum;




        }
    }
}
