using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LOGISTIC.UI.Billing
{
    public partial class ProcessBillUI : Form
    {
        private CustomerBll MLOBll = new CustomerBll();
        BillingBLL objBll = new BillingBLL();

        int customerId = 0;
        public  ProcessBillUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);

        }

        public ProcessBillUI(CSDBillSummary objbillSummary)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            customerId = objbillSummary.CustId;
            lblRefnoValue.Text = objbillSummary.SummaryRefNo;
            dateBillFrom.Value = Convert.ToDateTime(objbillSummary.BillFrom);
            datebillTo.Value = Convert.ToDateTime(objbillSummary.BillTo);

            if (objbillSummary.BillType == 1)
            {
                rdoMonthlyBasis.Checked = true;
            }
            else
            {
                rdoGateoutBasis.Checked = true;
            }

        }

        private void ProcessBillUI_Load(object sender, EventArgs e)
        {
            LoadAllServiceCategory();
            LoadAllService();
            LoadCustomer();
            if (customerId > 0)
            {
                ddlClient.SelectedValue = customerId;
                ddlClient.Enabled = false;
                lblRefNo.Visible = true;
                lblRefnoValue.Visible = true;
                btnBillProcess.Text = "Update";
                btnDelete.Enabled = true;
            }
            else
            {
                lblRefNo.Visible = false;
                lblRefnoValue.Visible = false;
                btnDelete.Enabled = false;

            }
        }

        private void LoadAllServiceCategory()
        {

            var type = objBll.GetAllServiceCategory();
                       
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CateId, t.CategoryName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Service Category--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlServCategory.DataSource = dt_Types;
                ddlServCategory.DisplayMember = "t_Name";
                ddlServCategory.ValueMember = "t_ID";
            }
            ddlServCategory.SelectedIndex = 0;

        }

        private void LoadAllService()
        {

            var type = objBll.GetAllChartofService();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ServiceId, t.ServiceName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = " ";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlServicelist.DataSource = dt_Types;
                ddlServicelist.DisplayMember = "t_Name";
                ddlServicelist.ValueMember = "t_ID";
            }
            ddlServicelist.SelectedIndex = 0;

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
            dr[1] = "-- Select MLO --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlClient.DataSource = dt_Types;
                ddlClient.DisplayMember = "t_Name";
                ddlClient.ValueMember = "t_ID";
            }
            ddlClient.SelectedIndex = 0;

        }

        private void ddlServCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlServCategory.SelectedValue) > 0)
            {
                if (lstChartofService.Items.Count == 0)
                {
                    lstChartofService.Columns.Add("Service Name", 225, HorizontalAlignment.Center);
                    // lstChartofService.Columns.Add("SERVICE NAME", 100, HorizontalAlignment.Center);
                    lstChartofService.View = View.Details;
                    //lstChartofService.GridLines = true;
                    //lstChartofService.BackColor = Color.Gray;
                    // lstChartofService.ForeColor = Color.Blue;

                }

                List<ChartOfService> listService = objBll.GetAllServiceByCategoryId(Convert.ToInt32(ddlServCategory.SelectedValue));

                ListViewItem item1 = lstChartofService.FindItemWithText(listService[0].ServiceName.ToString());

                if (item1 == null)
                    for (int i = 0; i < listService.Count; i++)
                    {
                        ListViewItem listitem = new ListViewItem(listService[i].ServiceId.ToString());

                        listitem.Tag = listService[i].ServiceId.ToString();

                        listitem.Text = listService[i].ServiceName.ToString();

                        //listitem.SubItems.Add(listService[i].ServiceId.ToString());

                        //listitem.SubItems.Add(listService[i].ServiceName.ToString());

                        lstChartofService.Items.Add(listitem);

                    }
                else
                {
                    MessageBox.Show("Service already added");

                }

            }
        }       

        private void lstChartofService_DoubleClick(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Do you want to remove this item ??",
                             "Confirm Item Remove",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                for (int i = 0; i < lstChartofService.Items.Count; i++)
                {

                    if (lstChartofService.Items[i].Selected)
                    {
                        lstChartofService.Items[i].Remove();                       

                    }

                }

            }
        }

        private void btnBillProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnBillProcess.Text == "Process")
                {
                    int? clientId = 0;
                    List<Customer> objlistCustomer = new List<Customer>();
                    objlistCustomer = MLOBll.Getall();
                    bool flag = ValidateBillProcess();
                    if (flag == true)
                    {
                        DateTime fromDate = dateBillFrom.Value.Date;
                        DateTime toDate = datebillTo.Value.Date;

                       

                        string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(constring))
                        {
                            if (rdoMonthlyBasis.Checked == true)
                            {
                               
                                
                                var resulr = Convert.ToInt32(ddlClient.SelectedValue);
                                if (resulr == 0)
                                {
                                    clientId = -99;
                                }
                                else
                                {
                                    clientId = Convert.ToInt32(resulr);

                                }

                                foreach (Customer obj in objlistCustomer)
                                {
                                    if (obj.BillProcessMonthly == 1)
                                    {
                                        clientId = obj.CustomerId;

                                        using (SqlCommand cmd = new SqlCommand("CSDMonthlyBILLProcess", con))
                                        {
                                            cmd.CommandTimeout = 600;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@ClientID", clientId);
                                            cmd.Parameters.AddWithValue("@Fromdate", Convert.ToDateTime(dateBillFrom.Value.Date));
                                            cmd.Parameters.AddWithValue("@Todate", Convert.ToDateTime(datebillTo.Value.Date));
                                            cmd.Parameters.AddWithValue("@VATPerchant", Convert.ToDecimal(txtVat.Text.Trim()));
                                            con.Open();
                                            cmd.ExecuteReader();
                                            con.Close();
                                         }
                                      }
                                }
                                NavigateToBillSummary();

                            }
                            else
                            if (rdoMLO.Checked == true)
                            {
                               // int? clientId = 0;

                                var resulr = Convert.ToInt32(ddlClient.SelectedValue);
                                if (resulr == 0)
                                {
                                    clientId = -99;
                                }
                                else
                                {
                                    clientId = Convert.ToInt32(resulr);
                                }
                                using (SqlCommand cmd = new SqlCommand("CSDMonthlyBILLProcess", con))
                                {
                                    cmd.CommandTimeout = 600;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ClientID", clientId);
                                    cmd.Parameters.AddWithValue("@Fromdate", Convert.ToDateTime(dateBillFrom.Value.Date));
                                    cmd.Parameters.AddWithValue("@Todate", Convert.ToDateTime(datebillTo.Value.Date));
                                    cmd.Parameters.AddWithValue("@VATPerchant", Convert.ToDecimal(txtVat.Text.Trim()));
                                    con.Open();
                                    cmd.ExecuteReader();
                                    con.Close();
                                    NavigateToBillSummary();


                                }

                            }
                            else
                            {

                                using (SqlCommand cmd = new SqlCommand("CSDOutBILLProcess", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ClientID", Convert.ToInt32(ddlClient.SelectedValue));
                                    cmd.Parameters.AddWithValue("@Fromdate", dateBillFrom.Value);
                                    cmd.Parameters.AddWithValue("@Todate", datebillTo.Value);
                                    cmd.Parameters.AddWithValue("@VATPerchant", Convert.ToDecimal(txtVat.Text.Trim()));

                                    con.Open();
                                    cmd.ExecuteReader();
                                    con.Close();
                                    NavigateToBillSummary();

                                }

                            }

                        }
                    }


                }
                else
                {

                    //Update Bill

                    bool flag = ValidateBillProcess();
                    if (flag == true)
                    {
                        string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(constring))
                        {
                            if (rdoMonthlyBasis.Checked == true)
                            {

                                int clientId = Convert.ToInt32(ddlClient.SelectedValue);
                                string refNo = lblRefnoValue.Text.Trim();

                                using (SqlCommand cmd = new SqlCommand("CSDMonthlyBIllUpdate", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ClientID", clientId);
                                    cmd.Parameters.AddWithValue("@RefNo", refNo);
                                    cmd.Parameters.AddWithValue("@Fromdate", dateBillFrom.Value);
                                    cmd.Parameters.AddWithValue("@Todate", datebillTo.Value);
                                    cmd.Parameters.AddWithValue("@VATPerchant", Convert.ToDecimal(txtVat.Text.Trim()));

                                    con.Open();
                                    cmd.ExecuteReader();
                                    con.Close();
                                    NavigateToBillSummary();

                                }

                            }
                            else
                            {

                                using (SqlCommand cmd = new SqlCommand("CSDOutBILLProcess", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ClientID", Convert.ToInt32(ddlClient.SelectedValue));
                                    cmd.Parameters.AddWithValue("@Fromdate", dateBillFrom.Value);
                                    cmd.Parameters.AddWithValue("@Todate", datebillTo.Value);
                                    cmd.Parameters.AddWithValue("@VATPerchant", Convert.ToDecimal(txtVat.Text.Trim()));

                                    con.Open();
                                    cmd.ExecuteReader();
                                    con.Close();
                                    NavigateToBillSummary();

                                }

                            }

                        }
                    }



                }
               // NavigateToBillSummary();
            }
            catch (System.Data.SqlClient.SqlException sqlException )
            {

                System.Windows.Forms.MessageBox.Show(sqlException.Message);
            

                  //throw ex;
            }
            
          

        }

        private void NavigateToBillSummary()
        {
            //BillSummary objBillSummary = new BillSummary();
            //objIGMImportDetails = objBll.GetIGMImportDetailById(IGMDetailsId);

            BillSummary f  = new BillSummary();
            f.MdiParent = this.ParentForm;
            f.Show();

        }

        private bool ValidateBillProcess()
        {
            var errMessage = "";

            if (rdoGateoutBasis.Checked == false && rdoMonthlyBasis.Checked == false && rdoMLO.Checked == false)
            {
                errMessage = errMessage + "* Please select a bill type !!\n";

            }
            if (rdoGateoutBasis.Checked == true)
            {
                if (Convert.ToInt32(ddlClient.SelectedValue) == 0)
                {
                    errMessage = errMessage + "* Please select a MLO !!\n";
                    ddlClient.Focus();

                }

            }
            if (txtVat.Text.Trim() == "")
            {
                errMessage = errMessage + "* VAT amount can't be null !!\n";
                txtVat.Focus();

            }                    
            if (errMessage != "")
            {
                MessageBox.Show(errMessage, "Input required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                return true;
            }

        }


        //private bool ValidateUpdateBillProcess()
        //{
        //    var errMessage = "";

        //    if (lblRefnoValue.Text.Trim().Length < 10)
        //    {
        //        errMessage = errMessage + "* Invalid reference no !!\n";

        //    }
        //    if (rdoGateoutBasis.Checked == false && rdoMonthlyBasis.Checked == false)
        //    {
        //        errMessage = errMessage + "* Please select a bill type !!\n";

        //    }
        //    if (rdoGateoutBasis.Checked == true)
        //    {
        //        if (Convert.ToInt32(ddlClient.SelectedValue) == 0)
        //        {
        //            errMessage = errMessage + "* Please select a MLO !!\n";
        //            ddlClient.Focus();

        //        }

        //    }
        //    if (txtVat.Text.Trim() == "")
        //    {
        //        errMessage = errMessage + "* VAT amount can't be null !!\n";
        //        txtVat.Focus();

        //    }
        //    if (errMessage != "")
        //    {
        //        MessageBox.Show(errMessage, "Input required", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lblRefnoValue.Text = "";
            lblRefNo.Visible = false;
            lblRefnoValue.Visible = false;
            ddlClient.SelectedIndex = 0;
            ddlClient.Enabled = true;
           // rdoMonthlyBasis.Checked = true;
            dateBillFrom.Value = DateTime.Now;
            datebillTo.Value = DateTime.Now;           
            btnBillProcess.Text = "Process";
            btnDelete.Enabled = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        
        private void ddlClient_KeyPress(object sender, KeyPressEventArgs e)
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

        private void rdoMonthlyBasis_CheckedChanged(object sender, EventArgs e)
        {
            ddlClient.Enabled = false;
        }

        private void rdoMLO_CheckedChanged(object sender, EventArgs e)
        {
            ddlClient.Enabled = true;
        }
    }


 }

