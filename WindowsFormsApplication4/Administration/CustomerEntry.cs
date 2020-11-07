using System;
using System.Collections.Generic;
using System.Data;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LOGISTIC.UI.Administration
{
    public partial class CustomerEntry : Form
    {
        private List<Customer> listCustomer = new List<Customer>();
        private Customer objCustomer = new Customer();

        private CustomerBll objBll = new CustomerBll();
        private AgentBLL agntBll = new AgentBLL();
       

        public CustomerEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);          
        }
                          
        private void CustomerEntry_Load(object sender, EventArgs e)
        {
            listCustomer = new List<Customer>();
            LoadAgent();
            PrepareGrid();
            LoadDataToGrid();
            ComboLoad();
            btndelete.Enabled = false;
        }

        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");           
            cmbSearch.Items.Insert(2, "CODE");
            cmbSearch.Items.Insert(3, "NAME");
          
            cmbSearch.SelectedIndex = 0;

        }

        private void LoadAgent()
        {

            var type = agntBll.Getall();
           
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.AgentId, t.AgentName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Agent --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlAgent.DataSource = dt_Types;
                ddlAgent.DisplayMember = "t_Name";
                ddlAgent.ValueMember = "t_ID";
            }
            ddlAgent.SelectedIndex = 0;

        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].HeaderText = "SL";

            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[1].HeaderText = "CODE";

            dataGridView1.Columns[2].Width = 160;
            dataGridView1.Columns[2].HeaderText = "CUSTOMER NAME";
         
            dataGridView1.Columns[3].Width = 160;
            dataGridView1.Columns[3].HeaderText = "AGENT NAME";

           
            dataGridView1.AllowUserToAddRows = false;


        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listCustomer = objBll.GetallWithAgent();
            if (listCustomer.Count > 0)
            {
                int index = 1;
                foreach (var item in listCustomer)
                {
                    dataGridView1.Rows.Add(index, item.CustomerCode, item.CustomerName, item.Agent.AgentName);
                    index = index + 1;
                }

            }           
            dataGridView1.ClearSelection();

        }

        private void LoadFilterDataToGrid(List<Customer> listCustomer)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
         
                int index = 1;
                foreach (var item in listCustomer)
                {
                dataGridView1.Rows.Add(index, item.CustomerCode, item.CustomerName, item.Agent.AgentName);
                index = index + 1;
                }

           
            dataGridView1.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = Validation();
            if (flag==true)
            {
                FillingData();
                SaveData();
                LoadDataToGrid();
                ClearForm();
            }
            

        }      

        private void btndelete_Click(object sender, EventArgs e)
        {
           
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
               "Confirm customer deletion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
               if (result == DialogResult.Yes)
               {
                  if (objCustomer.CustomerId != 0)
                   {

                    var status = objBll.Delete(objCustomer.CustomerId);
                    MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGrid();
                    
                   }
                
               }

               ClearForm(); 
          
            }
            
        private void btnCancel_Click(object sender, EventArgs e)
        {
          ClearForm();
        }    

        private void btnClose_Click(object sender, EventArgs e)
        {
        
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            string slect = cmbSearch.Text.Trim();
            string value = txtSearch.Text.ToString();

            if (slect == "Search By")
            {
                MessageBox.Show("Please select a search item !!");
                return;
            }
            else if (slect != "All" && value == "")
            {
                MessageBox.Show(" Search text can't be empty");
                return;
            }
            switch (slect)
            {
                
                case "CODE":
                    {

                        var filterobjlist = listCustomer.Where(item => item.CustomerCode.Contains(value)).ToList();
                        if (filterobjlist.Count > 0)
                        {
                            LoadFilterDataToGrid(filterobjlist);
                        }
                        else
                        {
                            MessageBox.Show("No MLO found !! !!", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }
                case "NAME":
                    {
                        var filterobjlist = listCustomer.Where(item => item.CustomerName.Contains(value)).ToList();
                        if (filterobjlist.Count > 0)
                        {
                            LoadFilterDataToGrid(filterobjlist);
                        }
                        else
                        {
                            MessageBox.Show("No MLO found !! !!", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }
                default:
                    {
                        var filterobjlist = listCustomer.ToList();
                        LoadFilterDataToGrid(filterobjlist);
                        break;
                    }
            }

        }

        private bool Validation()
        {
            var errMessage = "";
           
            if (txtCustomerCode.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter customer code !!\n";
            }           
            if (txtCustomerName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter customer name !!\n";  
            }
            if (Convert.ToInt32(ddlAgent.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a agent !!\n";                
            }
            if (errMessage != "")
            {
                MessageBox.Show(errMessage,"Error");                   
                return false;
            }
            else
            {
                return true;
            }
           
        }

        private void FillingData()
        {
            try
            {
                objCustomer.CustomerCode = txtCustomerCode.Text.Trim();
                objCustomer.CustomerName = txtCustomerName.Text.Trim();
                objCustomer.AgentId = Convert.ToInt32(ddlAgent.SelectedValue);
                objCustomer.Address = txtCustoemrAddress.Text.Trim();
                objCustomer.Email = txtEmail.Text.Trim();
                objCustomer.Fax = txtFax.Text.Trim();
                objCustomer.Mobile = txtMobile.Text.Trim();
                objCustomer.Telephone = txtTelephone.Text.Trim();
                objCustomer.RDA = txtRDA.Text.Trim();
                objCustomer.CarrierCode = txtCarrierCD.Text.Trim();               
                objCustomer.EntryDate = DateTime.Now;

              
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                var status = objBll.Insert(objCustomer);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (btnSave.Text == "Update")
            {
                var status = objBll.Update( objCustomer);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }      

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var index = Convert.ToInt32(selectedRow.Index);
            objCustomer = listCustomer.ElementAt(index);


            dateCustEntry.Text = objCustomer.EntryDate.ToString();
            txtCustomerCode.Text = Convert.ToString(objCustomer.CustomerCode);
            txtCustomerName.Text = Convert.ToString(objCustomer.CustomerName);
            ddlAgent.SelectedValue = objCustomer.AgentId;
            txtCustoemrAddress.Text = Convert.ToString(objCustomer.Address);
            txtTelephone.Text = Convert.ToString(objCustomer.Telephone);
            txtMobile.Text = Convert.ToString(objCustomer.Mobile);
            txtFax.Text = Convert.ToString(objCustomer.Fax);
            txtEmail.Text = Convert.ToString(objCustomer.Email);
            txtRDA.Text = Convert.ToString(objCustomer.RDA);
            txtCarrierCD.Text = Convert.ToString(objCustomer.CarrierCode);           
            btnSave.Text = "Update";
            btndelete.Enabled = true;
          
        }

        private void ClearForm()
        {
            txtCustomerCode.Text = "";
            txtCustomerName.Text = "";
            txtCustoemrAddress.Text = "";
            dateCustEntry.Value = DateTime.Now;
            ddlAgent.SelectedIndex = 0;
            txtEmail.Text = "";
            txtFax.Text = "";
            txtTelephone.Text = "";
            txtMobile.Text = "";
            txtRDA.Text = "";
            txtCarrierCD.Text = "";         
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";           
            dataGridView1.ClearSelection();
            objCustomer = new Customer();
            btndelete.Enabled = false;
        }

       
    }
}
