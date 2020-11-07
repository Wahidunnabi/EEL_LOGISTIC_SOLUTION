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
    public partial class MonerReceiptEntry : Form
    {
      
        private static List<Importer> listImporter = new List<Importer>();
        private static List<Shipper> listShipper = new List<Shipper>();
        private static List<Service> listServise = new List<Service>();

        MoneyReceipt objMoneyReceipt = new MoneyReceipt();
        private MoneyReceiptBLL objBll = new MoneyReceiptBLL();

        private UserInfo user;   
        private ClearAndForwaderBll cafbll = new ClearAndForwaderBll();
        private ImporterBll impBll = new ImporterBll();
        private ShipperBLL shprBll = new ShipperBLL();
        private CustomerBll MLOBll = new CustomerBll();
        private BankBLL bankBll = new BankBLL();

        private static int index;
        private static int SLNo;

        decimal totalAmount = 0;
        decimal grandTotal = 0;


        public MonerReceiptEntry(UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;


            txtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtQuantity.Properties.Mask.EditMask = "\\d+";

            txtRate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRate.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtDiscountAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtDiscountAmount.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtVAT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtVAT.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,1})?";

            ddlImpExp.Enabled = false;
            lblAgentNm.Visible = false;
            lblAgentName.Visible = false;
            lblAgentCd.Visible = false;
            lblAgentCode.Visible = false;
            rdoCash.Checked = true;
            pnlBankDetails.Visible = false;

            btnAddMDetails.Enabled = false;
            btnDeleteMDetails.Enabled = false;

        }


        public MonerReceiptEntry(UserInfo user, MoneyReceipt moneyReceipt)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;
            this.objMoneyReceipt = moneyReceipt;
            this.totalAmount = moneyReceipt.TotalAmount;
            this.grandTotal = moneyReceipt.GrandTotal;

            txtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtQuantity.Properties.Mask.EditMask = "\\d+";

            txtRate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRate.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtDiscountAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtDiscountAmount.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtVAT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtVAT.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,1})?";

            ddlImpExp.Enabled = false;
            lblAgentNm.Visible = false;
            lblAgentName.Visible = false;
            lblAgentCd.Visible = false;
            lblAgentCode.Visible = false;
            pnlBankDetails.Visible = false;

            btnAddMDetails.Enabled = false;
            btnDeleteMDetails.Enabled = false;

        }

        private void MonerReceiptEntry_Load(object sender, EventArgs e)
        {
            listImporter = impBll.Getall();
            listShipper = shprBll.Getall();
            listServise = objBll.GetAllServices();
            GetMoneyReceiptNo();
            LoadCustomer();
            LoadCAFAgent();
            LoadBank();
            PrepareGrid();

            if (objMoneyReceipt.ID > 0)
            {
                ShowMRData();
                LoadGrid();
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
                           
            }
            else
            {
                btnDelete.Enabled = false;
            }

        }
        

        #region LOAD BASIC DATA

        private void GetMoneyReceiptNo()
        {
            SLNo = objBll.GetMoneyReceiptSLNo();
           
        }

        private void LoadCustomer()
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
            dr[1] = "-- Customer --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlCusCode.DataSource = dt_Types;
                ddlCusCode.DisplayMember = "t_Name";
                ddlCusCode.ValueMember = "t_ID";
            }
            ddlCusCode.SelectedIndex = 0;

        }    
        private void LoadCAFAgent()
        {

            var type = cafbll.Getall();
            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.ClearAndForwadingAgentId, t.CFAgentName.Trim());
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Select C&F Agent--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                ddlCandFAgent.DataSource = dt;
                ddlCandFAgent.DisplayMember = "t_Name";
                ddlCandFAgent.ValueMember = "t_ID";
            }
            ddlCandFAgent.SelectedIndex = 0;

        }

        private void LoadImporter()
        {
            ddlImpExp.DataSource = null;
           // ddlImpExp.Items.Clear();

            var type = listImporter;
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
                ddlImpExp.DataSource = dt_Types;
                ddlImpExp.DisplayMember = "t_Name";
                ddlImpExp.ValueMember = "t_ID";
            }
            ddlImpExp.SelectedIndex = 0;

        }

        private void LoadShipper()
        {
            ddlImpExp.DataSource = null;
           // ddlImpExp.Items.Clear();

            var type = listShipper;
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
                ddlImpExp.DataSource = dt_Types;
                ddlImpExp.DisplayMember = "t_Name";
                ddlImpExp.ValueMember = "t_ID";
            }
            ddlImpExp.SelectedIndex = 0;

        }

        private void LoadBank()
        {

            var type = bankBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.BankId, t.BankName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Bank --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlBank.DataSource = dt_Types;
                ddlBank.DisplayMember = "t_Name";
                ddlBank.ValueMember = "t_ID";
            }
            ddlBank.SelectedIndex = 0;

        }

        private void LoadServiceType(List<Service> listService)
        {
            ddlServiceType.DataSource = null;
           // ddlServiceType.Items.Clear();

            var type = listService;
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ID, t.ServiceName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Service Type --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlServiceType.DataSource = dt_Types;
                ddlServiceType.DisplayMember = "t_Name";
                ddlServiceType.ValueMember = "t_ID";
            }
            ddlServiceType.SelectedIndex = 0;
         
        }

        public void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 4;


            dataGridView1.Columns[0].Width = 140;
            dataGridView1.Columns[0].HeaderText = "Service Name";

            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[1].HeaderText = "Quantity";

            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "Rate";

            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[3].HeaderText = "Total";

        }

        #endregion


        private void LoadGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (MoneyReceiptDetail item in objMoneyReceipt.MoneyReceiptDetails)
            {
                dataGridView1.Rows.Add(listServise.Where(s => s.ID == item.ServiceId).FirstOrDefault().ServiceName, item.Quantity, String.Format("{0:0.00}", item.Rate), String.Format("{0:0.00}", (item.Quantity * item.Rate)));
            }
            dataGridView1.ClearSelection();
            // dataGridView1.AllowUserToAddRows = false;
        }

        private void ShowMRData()
        {
            if (objMoneyReceipt.MRType == 1)
            {
                rdoImport.Checked = true;
                ddlImpExp.SelectedValue = objMoneyReceipt.ImporterId;

            }
            if (objMoneyReceipt.MRType == 2)
            {
                rdoCSD.Checked = true;
               
            }
            if (objMoneyReceipt.MRType == 3)
            {
                rdoExport.Checked = true;               
                ddlImpExp.SelectedValue = objMoneyReceipt.ShipperId;
            }

            lblMoneyRecNo.Text = objMoneyReceipt.MoneyReceiptNo;
            txtInvRefNo.Text = objMoneyReceipt.ReferenceNo;
            ddlCandFAgent.SelectedValue = objMoneyReceipt.CandFAgentId;
            ddlCusCode.SelectedValue = objMoneyReceipt.CustomerId;
            txtRiskbondNo.Text = objMoneyReceipt.RiskbondNo;


            if (objMoneyReceipt.PaymentOption == 1)
            {
                rdoCash.Checked = true;                

            }
            if (objMoneyReceipt.PaymentOption == 2)
            {
                rdoPayOrder.Checked = true;
                ddlBank.SelectedValue = objMoneyReceipt.BankId;
                txtChequePayorderNo.Text = objMoneyReceipt.ChqPayorderNo.Trim();
                dateIssue.Value = Convert.ToDateTime(objMoneyReceipt.IssueDate);
                pnlBankDetails.Visible = true;

            }
            if (objMoneyReceipt.PaymentOption == 3)
            {
                rdoCheque.Checked = true;
                ddlBank.SelectedValue = objMoneyReceipt.BankId;
                txtChequePayorderNo.Text = objMoneyReceipt.ChqPayorderNo.Trim();
                dateIssue.Value = Convert.ToDateTime(objMoneyReceipt.IssueDate);
                pnlBankDetails.Visible = true;

            }

            lblTotalAmount.Text = Convert.ToString(objMoneyReceipt.TotalAmount);
            txtDiscountAmount.Text = String.Format("{0:0.00}", objMoneyReceipt.DiscountAmount);
            chkPercentage.Checked = objMoneyReceipt.IsPercentage == true ? true :false;
            txtVAT.Text = String.Format("{0:0.00}", objMoneyReceipt.VatPercent);
            lblGrandTotal.Text = Convert.ToString(objMoneyReceipt.GrandTotal);

        }

        private void rdoImport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoImport.Checked == true)
            {
                LoadImporter();
                LoadServiceType(listServise.Where(x => x.ServiceCategory == 1).OrderBy(x => x.ServiceName).ToList());
                lblMoneyRecNo.Text = "ELL/MR/Import/" + SLNo;
                ddlImpExp.Enabled = true;
                btnAddMDetails.Enabled = true;
            }
            
        }

        private void rdoCSD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCSD.Checked == true)
            {                
                LoadServiceType(listServise.Where(x => x.ServiceCategory == 2).OrderBy(x => x.ServiceName).ToList());
                lblMoneyRecNo.Text = "ELL/MR/Empty/" + SLNo;
                ddlImpExp.Enabled = false;
                btnAddMDetails.Enabled = true;
            }
           
        }

        private void rdoExport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoExport.Checked == true)
            {
                LoadShipper();
                LoadServiceType(listServise.Where(x => x.ServiceCategory == 3).OrderBy(x=>x.ServiceName).ToList());
                lblMoneyRecNo.Text = "ELL/MR/Export/" + SLNo;
                ddlImpExp.Enabled = true;
                btnAddMDetails.Enabled = true;

            }

        }

        private void ddlCusCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int custId = Convert.ToInt32(ddlCusCode.SelectedValue);
            if (custId > 0)
            {
                var obj = objBll.GetMLOAgentData(custId);

                Type myType = obj.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    var proName = prop.Name;
                    {
                        if (proName == "CustomerName")
                        {
                            lblAgentName.Text = Convert.ToString(prop.GetValue(obj, null));

                        }
                        else
                        {
                            lblAgentCode.Text = Convert.ToString(prop.GetValue(obj, null));
                        }
                    }
                }
                lblAgentNm.Visible = true;
                lblAgentName.Visible = true;
                lblAgentCd.Visible = true;
                lblAgentCode.Visible = true;

            }
            else
            {
                lblAgentName.Text = "";
                lblAgentCode.Text = "";

                lblAgentNm.Visible = false;
                lblAgentName.Visible = false;
                lblAgentCd.Visible = false;
                lblAgentCode.Visible = false;
            }
        }

        private void rdoCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCash.Checked == true)
            {
                pnlBankDetails.Visible = false;

            }
            else
                pnlBankDetails.Visible = true;

        }               
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                bool flag = ValidateMoneyReceipt();
                if (flag == true)
                {
                    if (objMoneyReceipt.MoneyReceiptDetails.Count == 0)
                    {

                        MessageBox.Show("No service details data !!", "Service Details Required",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }
                    FillingMasterData();
                    var status = objBll.Insert(objMoneyReceipt);
                    MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearMoneyRecept();
                    SLNo = SLNo + 1;
                }
            }
            else if (btnSave.Text == "Update")
            {
                bool flag = ValidateMoneyReceipt();
                if (flag == true)
                {
                    FillingMasterData();
                    var status = objBll.Update(objMoneyReceipt);
                    MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearMoneyRecept();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??", "Confirm Money Receipt deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var status = objBll.Delete(objMoneyReceipt.ID);
                MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearMoneyRecept();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearMoneyRecept();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool ValidateMoneyReceipt()
        {

            var errMessage = "";
           
            if (Convert.ToInt32(ddlCandFAgent.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select C&F agent !!\n";
            }
            if (Convert.ToInt32(ddlCusCode.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select MLO !!\n";
            }
            if (rdoExport.Checked == true)
            {
                if (Convert.ToInt32(ddlImpExp.SelectedValue) == 0)
                {
                    errMessage = errMessage + "* Please select exporter !!\n";
                }              
            }
            if (rdoImport.Checked == true)
            {
                if (Convert.ToInt32(ddlImpExp.SelectedValue) == 0)
                {
                    errMessage = errMessage + "* Please select importer !!\n";
                }
            }
            if (rdoCash.Checked == false)
            {
                if (Convert.ToInt32(ddlBank.SelectedValue) == 0)
                {
                    errMessage = errMessage + "* Please select a bank !!\n";
                }
                if (txtChequePayorderNo.Text.Trim() == string.Empty)
                {
                    errMessage = errMessage + "* Please enter Cheque/Pay Order no !!\n";
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
        private void FillingMasterData()
        {

            objMoneyReceipt.MoneyReceiptNo = lblMoneyRecNo.Text.Trim();
            objMoneyReceipt.ReferenceNo = txtInvRefNo.Text.Trim();
            objMoneyReceipt.CandFAgentId = Convert.ToInt32(ddlCandFAgent.SelectedValue);
            objMoneyReceipt.CustomerId = Convert.ToInt32(ddlCusCode.SelectedValue);          
            objMoneyReceipt.RiskbondNo = txtRiskbondNo.Text.Trim();
            objMoneyReceipt.RBDate = dateRB.Value;
            if (rdoImport.Checked == true)
            {
                objMoneyReceipt.ImporterId = Convert.ToInt32(ddlImpExp.SelectedValue);
                objMoneyReceipt.MRType = 1;
            }
            if (rdoCSD.Checked == true)
            {               
                objMoneyReceipt.MRType = 2;
            }
            if (rdoExport.Checked == true)
            {
                objMoneyReceipt.ShipperId = Convert.ToInt32(ddlImpExp.SelectedValue);
                objMoneyReceipt.MRType = 3;
            }
            if (rdoCash.Checked == true)
            {
                objMoneyReceipt.PaymentOption = 1;

            }
            else
            {
                objMoneyReceipt.PaymentOption = rdoPayOrder.Checked == true ? 2 : 3;
                objMoneyReceipt.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                objMoneyReceipt.ChqPayorderNo = txtChequePayorderNo.Text.Trim();
                objMoneyReceipt.IssueDate = dateIssue.Value;

            }

            objMoneyReceipt.TotalAmount = Convert.ToDecimal(lblTotalAmount.Text);
            objMoneyReceipt.DiscountAmount = txtDiscountAmount.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDiscountAmount.Text.Trim()) : 0;
            objMoneyReceipt.GrandTotal = Convert.ToDecimal(lblGrandTotal.Text.Trim());
            objMoneyReceipt.IsPercentage = chkPercentage.Checked;
            objMoneyReceipt.VatPercent = txtVAT.Text.Trim().Length > 0 ? Convert.ToDecimal(txtVAT.Text.Trim()) : 0;
            objMoneyReceipt.Date = DateTime.Now;
            objMoneyReceipt.SavedById = user.UserId;

        }
        private void ClearMoneyRecept()
        {
            lblMoneyRecNo.Text = "";
            txtInvRefNo.Text = "";
            rdoImport.Checked = false;
            rdoCSD.Checked = false;
            rdoExport.Checked = false;
            ddlCandFAgent.SelectedIndex = 0;
            ddlImpExp.DataSource = null;
            ddlImpExp.Items.Clear();
            ddlImpExp.Enabled = false;
            ddlCusCode.SelectedIndex = 0;          
            txtRiskbondNo.Text = "";
            dateRB.Value = DateTime.Now;
            rdoCash.Checked = true;
            ddlBank.SelectedIndex = 0;
            txtChequePayorderNo.Text = "";
            dateIssue.Value = DateTime.Now;
            lblAgentNm.Visible = false;
            lblAgentName.Visible = false;
            lblAgentCd.Visible = false;
            lblAgentCode.Visible = false;
            pnlBankDetails.Visible = false;
            dataGridView1.Rows.Clear();
            lblTotalAmount.Text = "00.00";
            txtDiscountAmount.Text = "";
            txtVAT.Text = "";
            lblGrandTotal.Text = "00.00";
            chkPercentage.Checked = false;
            objMoneyReceipt = new MoneyReceipt();
            btnSave.Text = "Save";
            labelControl7.Focus();
          
        }            
        private void CSDMonerReceipt_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearMoneyRecept();
        }
       

        #region MONEY RECEIPT DETAILS

        private void btnAddMDetails_Click(object sender, EventArgs e)
        {
            bool flag = ValidateDetails();
            if (flag == true)
            {
                MoneyReceiptDetail objDetails = FillingMoneyReceiptDetails();

                if (btnAddMDetails.Text == "Add") //Add Money Receipt details
                {
                    objMoneyReceipt.MoneyReceiptDetails.Add(objDetails);
                    BindDataToGrid(objDetails);

                }
                else
                {                
                    objMoneyReceipt.MoneyReceiptDetails.ElementAt(index).ServiceId = objDetails.ServiceId;
                    objMoneyReceipt.MoneyReceiptDetails.ElementAt(index).Quantity = objDetails.Quantity;
                    objMoneyReceipt.MoneyReceiptDetails.ElementAt(index).Size = objDetails.Size;
                    objMoneyReceipt.MoneyReceiptDetails.ElementAt(index).Rate = objDetails.Rate;
                    objMoneyReceipt.MoneyReceiptDetails.ElementAt(index).Total = objDetails.Total;                   
                    UpdateGrid(index, objDetails);
                }
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

                var obj = objMoneyReceipt.MoneyReceiptDetails.ElementAt(index);
                objMoneyReceipt.MoneyReceiptDetails.Remove(obj);
                //dataGridView1.Rows.RemoveAt(index);
                LoadGrid();
                ClearDetails();
                CalculateTotalAmount();

            }

        }
     
        private bool ValidateDetails()
        {
            var errMessage = "";

            if (Convert.ToInt32(ddlServiceType.SelectedIndex) == 0)
            {
                errMessage = errMessage + "* Please select bill type !!\n";
            }
            if (Convert.ToInt32(txtQuantity.Text.Trim().Length) == 0)
            {
                errMessage = errMessage + "* Please enter quantity !!\n";
            }
            if (Convert.ToInt32(txtRate.Text.Trim().Length) == 0)
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

        private MoneyReceiptDetail FillingMoneyReceiptDetails()
        {

            MoneyReceiptDetail objDetails = new MoneyReceiptDetail();

            objDetails.MoneyReceiptId = objMoneyReceipt.ID;
            objDetails.ServiceId = Convert.ToInt32(ddlServiceType.SelectedValue);
            objDetails.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            objDetails.Rate = Convert.ToDecimal(txtRate.Text.Trim());
            objDetails.Total = objDetails.Quantity * objDetails.Rate;
            if (rdo20.Checked == true)
            {
                objDetails.Size = 2;
            }
            if (rdo40.Checked == true)
            {
                objDetails.Size = 3;
            }
            if (rdo45.Checked == true)
            {
                objDetails.Size = 6;
            }

            return objDetails;

        }

        private void BindDataToGrid(MoneyReceiptDetail objDetails)
        {

            dataGridView1.Rows.Add(ddlServiceType.Text.Trim(), objDetails.Quantity, objDetails.Rate, (objDetails.Quantity * objDetails.Rate));
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            index = Convert.ToInt32(selectedRow.Index);
            var data = objMoneyReceipt.MoneyReceiptDetails.ElementAt(index);

            ddlServiceType.SelectedValue = Convert.ToInt32(data.ServiceId);
            txtQuantity.Text = Convert.ToString(data.Quantity);
            txtRate.Text = Convert.ToString(data.Rate);
            if (data.Size == 2)
            {
                rdo20.Checked = true;
            }
            if (data.Size == 3)
            {
                rdo40.Checked = true;
            }
            if (data.Size == 6)
            {
                rdo45.Checked = true;
            }

            btnAddMDetails.Text = "Update";
            btnDeleteMDetails.Enabled = true;

        }

        private void UpdateGrid(int index, MoneyReceiptDetail objDetails)
        {

            dataGridView1.Rows[index].Cells[0].Value = ddlServiceType.Text.Trim();
            dataGridView1.Rows[index].Cells[1].Value = objDetails.Quantity;
            dataGridView1.Rows[index].Cells[2].Value = String.Format("{0:0.00}", objDetails.Rate);
            dataGridView1.Rows[index].Cells[3].Value = String.Format("{0:0.00}", (objDetails.Quantity * objDetails.Rate));
            dataGridView1.ClearSelection();

        }

        private void ClearDetails()
        {

            ddlServiceType.SelectedIndex = 0;
            txtQuantity.Text = "";
            txtRate.Text = "";
            btnAddMDetails.Text = "Add";
            rdo20.Checked = false;
            rdo40.Checked = false;
            rdo45.Checked = false;
            btnDeleteMDetails.Enabled = false;
            labelControl1.Focus();

        }

        private void CalculateTotalAmount()
        {
            //decimal totalAmount = 0;
            //foreach (var item in objMoneyReceipt.MoneyReceiptDetails)
            //{
            //    var amount = item.Quantity * item.Rate;
            //    totalAmount = Convert.ToDecimal(totalAmount + amount);

            //}
            //lblTotalAmount.Text = Convert.ToString(totalAmount);

            totalAmount = 0;
            grandTotal = 0;
            foreach (var item in objMoneyReceipt.MoneyReceiptDetails)
            {
                var amount = item.Quantity * item.Rate;
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
                grandTotal = grandTotal + ((grandTotal * VatRate) / 100);
                
            }

            lblGrandTotal.Text = Convert.ToString(grandTotal);

        }

        private void txtDiscountAmount_KeyUp(object sender, KeyEventArgs e)
        {
            //decimal grandtotal = 0;
            //decimal totalPrice = Convert.ToDecimal(lblTotalAmount.Text);
            //if (totalPrice > 100)
            //{
            //    if (txtDiscountAmount.Text.Trim().Length > 0)
            //    {
            //        decimal discountValue = Convert.ToDecimal(txtDiscountAmount.Text.Trim());
            //        if (chkPercentage.Checked == true)
            //        {
            //            grandtotal = totalPrice - ((totalPrice * discountValue) / 100);
            //            lblGrandTotal.Text = Convert.ToString(grandtotal);
            //        }
            //        else
            //        {
            //            grandtotal = totalPrice - discountValue;
            //            lblGrandTotal.Text = Convert.ToString(grandtotal);
            //        }
            //    }
            //}

           
            if (totalAmount > 0)
            {
                if (txtDiscountAmount.Text.Trim().Length > 0)
                {
                    decimal discountValue = Convert.ToDecimal(txtDiscountAmount.Text.Trim());
                    if (chkPercentage.Checked == true)
                    {
                        grandTotal = totalAmount - ((totalAmount * discountValue) / 100);
                        lblGrandTotal.Text = Convert.ToString(grandTotal);
                    }
                    else
                    {
                        grandTotal = totalAmount - discountValue;
                        lblGrandTotal.Text = Convert.ToString(grandTotal);
                    }
                }
                else
                {
                    grandTotal = totalAmount;
                    lblGrandTotal.Text = Convert.ToString(grandTotal);
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
            //decimal grandtotal = Convert.ToDecimal(lblGrandTotal.Text);
            if (grandTotal > 0)
            {
                if (txtVAT.Text.Trim().Length > 0)
                {
                    decimal VatRate = Convert.ToDecimal(txtVAT.Text.Trim());
                    //decimal grandtotal = Convert.ToDecimal(lblGrandTotal.Text);
                    decimal netAmount = grandTotal + ((grandTotal * VatRate) / 100);
                    lblGrandTotal.Text = Convert.ToString(netAmount);
                }
                else
                {
                    lblGrandTotal.Text = Convert.ToString(grandTotal);
                }
            }
        }




        #endregion

        
    }
}
