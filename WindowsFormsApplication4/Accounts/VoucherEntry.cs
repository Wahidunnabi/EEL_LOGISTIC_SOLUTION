using System;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using LOGISTIC.BLL;
using System.Windows.Forms;
using System.Linq;

namespace LOGISTIC.UI
{
    public partial class VoucherEntry : Form
    {


        VoucherMaster objVoucher = new VoucherMaster();
        private List<ChartOfAccount> listChartOfAccount = new List<ChartOfAccount>();
        private AccounceBLL objBll = new AccounceBLL();
        private UserInfo user;
        private static int index;
        int nextSlNo = 0;
        decimal totalDebit = 0;
        decimal totalCredit = 0;

        public VoucherEntry(UserInfo user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
            this.user = user;
            txtAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtAmount.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";
            btnDelete.Enabled = false;
            btnDltVchrDtls.Enabled = false;

        }

        public VoucherEntry(VoucherMaster objVoucherMaster, UserInfo user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
            objVoucher = objVoucherMaster;
            this.user = user;
            txtAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtAmount.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";
            btnDelete.Enabled = false;
            btnDltVchrDtls.Enabled = false;
            btnSave.Text = "Update";

        }

        private void VoucherEntry_Load(object sender, EventArgs e)
        {
           // objVoucher = new VoucherMaster();
            listChartOfAccount = objBll.GetAllTransactionHead();
            LoadVoucherType();
            LoadAccountHead();
            PrepareGrid();
            var slNo = objBll.GetCurrentVoucherMasterSlNo();
            nextSlNo = slNo + 1;           
            lblPreparedBy.Text = user.FirstName + " " + user.LastName;

            if (objVoucher.VoucherDetails.Count > 0)
            {

                ShowVoucherMasterData();


                foreach (VoucherDetail item in objVoucher.VoucherDetails)
                {
                    if (item.DrCr == "Dr")
                    {
                        totalDebit = totalDebit + item.Amount;
                    }
                    else
                    {
                        totalCredit = totalCredit + item.Amount;
                    }
                   
                    dataGridView1.Rows.Add( listChartOfAccount.Where(x=>x.ID== item.COAId).FirstOrDefault().ac_name, item.DrCr == "Dr" ? item.Amount.ToString() : "", item.DrCr == "Cr" ? item.Amount.ToString() : "");

                }
                dataGridView1.Rows.Add("Total", totalDebit, totalCredit);
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Khaki;
                dataGridView1.ClearSelection();
                dataGridView1.AllowUserToAddRows = false;
            }


        }

        #region Load Basic Data

        private void LoadVoucherType()
        {
            var type = objBll.GetAllVoucherType();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(string));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.VoucherTypeId, t.TypeName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Voucher Type --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlVoucherType.DataSource = dt_Types;
                ddlVoucherType.DisplayMember = "t_Name";
                ddlVoucherType.ValueMember = "t_ID";
            }

            ddlVoucherType.SelectedIndex = 0;

            //ddlVoucherType.Items.Insert(0, "--Select Voucher Type--");
            //ddlVoucherType.Items.Insert(1, "Debit/Payment Voucher");
            //ddlVoucherType.Items.Insert(2, "Credit/Receive Voucher");
            //ddlVoucherType.Items.Insert(3, "Journal Voucher");
            //ddlVoucherType.Items.Insert(4, "Contra Voucher");
            //ddlVoucherType.Items.Insert(5, "Bank Receive Voucher");
            //ddlVoucherType.Items.Insert(6, "Bank Payment Voucher");
            //ddlVoucherType.SelectedIndex = 0;


        }

        private void LoadAccountHead()
        {

            //var type = objBll.GetAllTransactionHead();
            var type = listChartOfAccount;
            ddlTransactionHead.DisplayMember = "ac_name";
            ddlTransactionHead.ValueMember = "ID";
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ID, t.ac_name);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Head --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlTransactionHead.DataSource = dt_Types;
                ddlTransactionHead.DisplayMember = "t_Name";
                ddlTransactionHead.ValueMember = "t_ID";
            }
            ddlTransactionHead.SelectedIndex = 0;

        }

        public void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 3;

            dataGridView1.Columns[0].Width = 250;
            dataGridView1.Columns[0].HeaderText = "PARTICULARS";

            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[1].HeaderText = "Dr.";

            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[2].HeaderText = "Cr.";

        }


        #endregion


        #region Voucher Details Data

        private void btnAddVouDtls_Click(object sender, EventArgs e)
        {

            bool flag = ValidateVoucherDetails();
            if (flag == true)
            {
                VoucherDetail objVoucherDetails = FillingVoucherDetails();

                if (btnAddVouDtls.Text == "Add")
                {

                    objVoucher.VoucherDetails.Add(objVoucherDetails);
                    BindDataToGrid(objVoucherDetails);
                }
                else
                {
                    //update voucher details

                    if (objVoucher.VoucherDetails.ElementAt(index).DrCr == "Dr")
                    {
                        totalDebit = totalDebit - objVoucher.VoucherDetails.ElementAt(index).Amount;
                    }
                    else
                    {
                        totalCredit = totalCredit - objVoucher.VoucherDetails.ElementAt(index).Amount;

                    }

                    objVoucher.VoucherDetails.ElementAt(index).COAId = objVoucherDetails.COAId;
                    objVoucher.VoucherDetails.ElementAt(index).Amount = objVoucherDetails.Amount;
                    objVoucher.VoucherDetails.ElementAt(index).DrCr = objVoucherDetails.DrCr;


                    if (objVoucherDetails.DrCr == "Dr")
                    {
                        totalDebit = totalDebit + objVoucherDetails.Amount;
                    }
                    else
                    {
                        totalCredit = totalCredit + objVoucherDetails.Amount;
                    }

                    GridUpdate(index, objVoucherDetails);
                }

                ClearVoucherDetails();
            }


        }

        private void btnDltVchrDtls_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                           "Confirm voucher master deletion",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                var obj = objVoucher.VoucherDetails.ElementAt(index);

                if (obj.DrCr == "Dr")
                {
                    totalDebit = totalDebit - obj.Amount;
                }
                else
                {
                    totalCredit = totalCredit - obj.Amount;
                }

                objVoucher.VoucherDetails.Remove(obj);
                dataGridView1.Rows.RemoveAt(index);
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                dataGridView1.Rows.Add("Total", totalDebit, totalCredit);
                ClearVoucherDetails();

            }
        }

        private void btnCancleDtls_Click(object sender, EventArgs e)
        {
            ClearVoucherDetails();
        }

        private void ddlTransactionHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransactionHead.SelectedIndex > 0)
            {
                txtAmount.Focus();
            }
        }

        private bool ValidateVoucherDetails()
        {
            var errMessage = "";

            if (Convert.ToInt32(ddlTransactionHead.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select transaction head !!\n";
            }
            if ( Convert.ToDecimal(txtAmount.Text.Trim()) <= 0)
            {
                errMessage = errMessage + "* Amount can not be null !!\n";
                txtAmount.Focus();
            }
            if (rdoDebit.Checked == false && rdoCredit.Checked == false)
            {
                errMessage = errMessage + "* Please check transaction type !!\n";
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

        private VoucherDetail FillingVoucherDetails()
        {

            VoucherDetail objVoucherDetails = new VoucherDetail();

            objVoucherDetails.VoucherMstrId = objVoucher.VoucherMstrId;
            objVoucherDetails.COAId = Convert.ToInt32(ddlTransactionHead.SelectedValue);
            objVoucherDetails.Amount = Convert.ToDecimal(txtAmount.Text.Trim());
            objVoucherDetails.DrCr = rdoDebit.Checked == true ? "Dr" : "Cr";


            return objVoucherDetails;


        }

        private void BindDataToGrid(VoucherDetail objVoucherDetails)
        {
            if (objVoucherDetails.DrCr == "Dr")
            {
                totalDebit = totalDebit + objVoucherDetails.Amount;
            }
            else
            {
                totalCredit = totalCredit + objVoucherDetails.Amount;
            }

            var totalRow = dataGridView1.RowCount;
            if (totalRow > 1)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                dataGridView1.Rows.Add(ddlTransactionHead.Text, objVoucherDetails.DrCr == "Dr" ? txtAmount.Text.Trim() : null, objVoucherDetails.DrCr == "Cr" ? txtAmount.Text.Trim() : null);
                dataGridView1.Rows.Add("Total", totalDebit, totalCredit);
                //totalRow++;
            }
            else
            {
                dataGridView1.Rows.Add(ddlTransactionHead.Text, objVoucherDetails.DrCr == "Dr" ? txtAmount.Text.Trim() : null, objVoucherDetails.DrCr == "Cr" ? txtAmount.Text.Trim() : null);
                dataGridView1.Rows.Add("Total", totalDebit, totalCredit);
            }
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Khaki;


            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();


        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            index = Convert.ToInt32(selectedRow.Index);
            var totalrow = dataGridView1.Rows.Count - 1;
            if (index != totalrow)
            {
                var data = objVoucher.VoucherDetails.ElementAt(index);
                txtAmount.Text = Convert.ToString(data.Amount);
                ddlTransactionHead.SelectedValue = Convert.ToInt32(data.COAId);
                if (data.DrCr == "Dr")
                {
                    rdoDebit.Checked = true;
                }
                else
                {
                    rdoCredit.Checked = true;
                }

                btnAddVouDtls.Text = "Update";
                btnDltVchrDtls.Enabled = true;
            }
            else
            {
                ClearVoucherDetails();
            }

        }

        private void GridUpdate(int index, VoucherDetail objVoucherDetails)
        {

            dataGridView1.Rows[index].Cells[0].Value = ddlTransactionHead.Text;
            dataGridView1.Rows[index].Cells[1].Value = objVoucherDetails.DrCr == "Dr" ? txtAmount.Text.Trim() : "";
            dataGridView1.Rows[index].Cells[2].Value = objVoucherDetails.DrCr == "Cr" ? txtAmount.Text.Trim() : "";

            //Update footer row
            dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            dataGridView1.Rows.Add("Total", totalDebit, totalCredit);

            dataGridView1.ClearSelection();

        }

        private void ClearVoucherDetails()
        {
            ddlTransactionHead.SelectedIndex = 0;
            txtAmount.Text = "";
            rdoCredit.Checked = false;
            rdoDebit.Checked = false;
            btnAddVouDtls.Text = "Add";
            btnDltVchrDtls.Enabled = false;
            dataGridView1.ClearSelection();

        }





        #endregion

        #region Voucher Master Data
        private void ShowVoucherMasterData()
        {

            ddlVoucherType.SelectedValue = objVoucher.VoucherTypeId;
            txtVocherNo.Text = objVoucher.VoucherNumber;
            dateVoucher.Value = Convert.ToDateTime(objVoucher.VoucherDate);                    
            txtNarration.Text = objVoucher.Description;
        }
        private void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVoucherType.SelectedIndex > 0)
            {
                txtVocherNo.Text = ddlVoucherType.SelectedValue + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year.ToString().Substring(2, 2) + "/" + nextSlNo.ToString();
                ddlTransactionHead.Focus();
            }
            else
            {
                txtVocherNo.Text = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                bool flag = ValidateVoucherMaster();
                if (flag == true)
                {
                    if (objVoucher.VoucherDetails.Count == 0)
                    {

                        MessageBox.Show("No Master details data !!", "Voucher Details Required",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }
                    FillingVoucherMasterData();
                    var status = objBll.InsertVoucher(objVoucher);
                    MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearVoucherMaster();
                    nextSlNo = nextSlNo + 1;
                    //ClearIGMImport();
                    //ClearGrid();

                }
            }
            else if (btnSave.Text == "Update")
            {
                bool flag = ValidateVoucherMaster();
                if (flag == true)
                {

                    FillingVoucherMasterData();
                    var status = objBll.UpdateVoucher(objVoucher);
                    MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearVoucherMaster();
                }

                
                //ClearIGMImport();
                //ClearGrid();
            }

        }

        private bool ValidateVoucherMaster()
        {

            var errMessage = "";

            if (totalCredit != totalDebit)
            {
                errMessage = errMessage + "* Total debit amount and total credit amount should be same !!\n";
            }
            if (ddlVoucherType.SelectedValue.ToString().Length < 0)
            {
                errMessage = errMessage + "* Please select voucher type !!\n";
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

        private void FillingVoucherMasterData()
        {

            objVoucher.VoucherTypeId = Convert.ToString(ddlVoucherType.SelectedValue);
            objVoucher.VoucherNumber = txtVocherNo.Text.Trim();
            objVoucher.VoucherDate = dateVoucher.Value;
            objVoucher.Amount = totalCredit;
            objVoucher.Description = txtNarration.Text.Trim();
            objVoucher.CreateUser = user.UserId;

        }

        private void ClearVoucherMaster()
        {

            ddlVoucherType.SelectedIndex = 0;
            txtVocherNo.Text = "";
            dateVoucher.Value = DateTime.Now;
            txtNarration.Text = "";
            objVoucher = new VoucherMaster();
            totalDebit = 0;
            totalCredit = 0;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            btnSave.Text = "Save";
            btnDelete.Enabled = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearVoucherMaster();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {           
            Close();
        }


        #endregion

        private void VoucherEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearVoucherMaster();
        }
    }
}

