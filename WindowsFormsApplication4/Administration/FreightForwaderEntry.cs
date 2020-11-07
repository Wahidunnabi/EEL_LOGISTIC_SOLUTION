using System;
using System.Collections.Generic;
using System.Data;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LOGISTIC.UI.Administration
{
    public partial class FreightForwaderEntry : Form
    {
        private List<FreightForwarderAgent> listFreForwd = new List<FreightForwarderAgent>();
        private FreightForwarderAgent objFF = new FreightForwarderAgent();
        private FreightForwarderBLL objBLL = new FreightForwarderBLL();


        public FreightForwaderEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }             

        private void FreightForwaderEntry_Load(object sender, EventArgs e)
        {
            btndelete.Enabled = false;
            ComboLoad();
            PrepareGrid();
            LoadDataToGrid();
        }

        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Code No");
            cmbSearch.Items.Insert(3, "Name");
            cmbSearch.SelectedIndex = 0;

        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Goldenrod;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[1].HeaderText = "Code No";

            dataGridView1.Columns[2].Width = 210;
            dataGridView1.Columns[2].HeaderText = "Agent Name";

            dataGridView1.Columns[3].HeaderText = "Telephone";           
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listFreForwd = objBLL.Getall();

            if (listFreForwd.Count > 0)
            {
                int index = 1;
                foreach (var item in listFreForwd)
                {
                    dataGridView1.Rows.Add(index, item.FreightForwarderCode, item.FreightForwarderName, item.Telephone);
                    index = index + 1;
                }

            }

            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<FreightForwarderAgent> listFreForwd)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listFreForwd.Count > 0)
            {
                int index = 1;
                foreach (var item in listFreForwd)
                {
                    dataGridView1.Rows.Add(index, item.FreightForwarderCode, item.FreightForwarderName, item.Telephone);
                    index = index + 1;
                }

            }

            dataGridView1.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = Validation();
            if (flag == true)
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
                          "Confirm Freight Forwarder deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objFF.FreightForwarderId != 0)
                {

                    var status = objBLL.Delete(objFF.FreightForwarderId);
                    MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                case "Code No":
                    {
                        var listItems = listFreForwd.Where(c => c.FreightForwarderCode.Equals(value)).ToList();
                        LoadFilterDataToGrid(listItems);
                        break;
                    }
                case "Name":
                    {

                        var listItems = listFreForwd.Where(item => item.FreightForwarderName.Contains(value)).ToList();
                        LoadFilterDataToGrid(listItems);
                        break;
                    }                
                default:
                    {
                        LoadFilterDataToGrid(listFreForwd);
                        break;
                    }
            }

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var index = Convert.ToInt32(selectedRow.Index);
            objFF = listFreForwd.ElementAt(index);
            txtFreightForwardCode.Text = Convert.ToString(objFF.FreightForwarderCode);
            txtFreightForwardName.Text = Convert.ToString(objFF.FreightForwarderName);
            txtAddress.Text = Convert.ToString(objFF.Address);
            txtTelephone.Text = Convert.ToString(objFF.Telephone);
            txtMobile.Text = Convert.ToString(objFF.Mobile);
            txtFax.Text = Convert.ToString(objFF.Fax);
            txtEmail.Text = Convert.ToString(objFF.Email);
            dateIn.Text = Convert.ToString(objFF.EntryDate);
            btnSave.Text = "Update";
            btndelete.Enabled = true;
        }       

        private bool Validation()
        {
            var errMessage = "";

            if (txtFreightForwardCode.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter FF agent code !!\n";
            }
            if (txtFreightForwardName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter FF agent name !!\n";
            }
            if (errMessage != "")
            {
                MessageBox.Show(errMessage, "Error");
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
                objFF.FreightForwarderCode = txtFreightForwardCode.Text.Trim();
                objFF.FreightForwarderName = txtFreightForwardName.Text.Trim();

                objFF.Address = txtAddress.Text.Trim();
                objFF.Email = txtEmail.Text.Trim();
                objFF.Fax = txtFax.Text.Trim();
                objFF.Mobile = txtMobile.Text.Trim();
                objFF.Telephone = txtTelephone.Text.Trim();         
                objFF.EntryDate = DateTime.Now;

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
                var status = objBLL.Insert(objFF);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (btnSave.Text == "Update")
            {
                var status = objBLL.Update(objFF);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ClearForm()
        {
            txtFreightForwardCode.Text = "";
            txtFreightForwardName.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtTelephone.Text = "";
            dateIn.Value = DateTime.Now;
            txtMobile.Text = "";
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btndelete.Enabled = false;
            objFF = new FreightForwarderAgent();
            dataGridView1.ClearSelection();
            txtFreightForwardCode.Focus();
           
        }
       
       }
}
