using System;
using System.Collections.Generic;
using System.Data;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LOGISTIC.UI.Administration
{
    public partial class ConsineeEntry : Form
    {
        private List<Consignee> listConsignee = new List<Consignee>();
        private Consignee objConsignee = new Consignee();
        private ConsigneeBll objBll = new ConsigneeBll();


        public ConsineeEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        private void ConsineeEntry_Load(object sender, EventArgs e)
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
            cmbSearch.Items.Insert(2, "CODE");
            cmbSearch.Items.Insert(3, "NAME");
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

            dataGridView1.Columns[1].HeaderText = "Code No";

            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[2].HeaderText = "Consigne Name";

            dataGridView1.Columns[3].HeaderText = "Telephone";

        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listConsignee = objBll.Getall();
            if (listConsignee.Count > 0)
            {
                int index = 1;
                foreach (var item in listConsignee)
                {
                    dataGridView1.Rows.Add(index, item.ConsigneeCode, item.ConsigneeName, item.Telephone);
                    index = index + 1;
                }

            }
            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<Consignee> listConsignee)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listConsignee.Count > 0)
            {
                int index = 1;
                foreach (var item in listConsignee)
                {
                    dataGridView1.Rows.Add(index, item.ConsigneeCode, item.ConsigneeName, item.Telephone);
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
                           "Confirm Consignee deletion",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objConsignee.ConsigneeId != 0)
                {

                    objBll.Delete(objConsignee.ConsigneeId);
                    MessageBox.Show("Deleted successfully !! ");
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

                        var listitems = listConsignee.Where(item => item.ConsigneeCode.Contains(value)).ToList();
                        LoadFilterDataToGrid(listitems);
                        break;
                    }
                case "NAME":
                    {
                        var listitems = listConsignee.Where(item => item.ConsigneeName.Contains(value)).ToList();
                        LoadFilterDataToGrid(listitems);
                        break;
                    }
                default:
                    {
                        LoadFilterDataToGrid(listConsignee);
                        break;
                    }
            }

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objConsignee = listConsignee.ElementAt(index);

            txtConsigneeCode.Text = Convert.ToString(objConsignee.ConsigneeCode);
            txtConsigneeName.Text = Convert.ToString(objConsignee.ConsigneeName);
            txtConsigneeAddress.Text = Convert.ToString(objConsignee.Address);
            txtTelephone.Text = Convert.ToString(objConsignee.Telephone);
            txtMobile.Text = Convert.ToString(objConsignee.Mobile);
            txtFax.Text = Convert.ToString(objConsignee.Fax);
            txtEmail.Text = Convert.ToString(objConsignee.Email);
            dateIn.Value = Convert.ToDateTime(objConsignee.EntryDate);
            btnSave.Text = "Update";
            btndelete.Enabled = true;

        }
        
        private bool Validation()
        {
            var errMessage = "";

            if (txtConsigneeName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter consignee name !!\n";
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
                objConsignee.ConsigneeCode = txtConsigneeCode.Text.Trim();
                objConsignee.ConsigneeName = txtConsigneeName.Text.Trim();

                objConsignee.Address = txtConsigneeAddress.Text.Trim();
                objConsignee.Email = txtEmail.Text.Trim();
                objConsignee.Fax = txtFax.Text.Trim();
                objConsignee.Mobile = txtMobile.Text.Trim();
                objConsignee.Telephone = txtTelephone.Text.Trim();
                objConsignee.EntryDate = DateTime.Now;


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
                var status = objBll.Insert(objConsignee);
                if (status == 1)
                {
                    MessageBox.Show("Consignee has been inserted.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }
            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.Update(objConsignee);
                if (status == 1)
                {
                    MessageBox.Show("Consignee has been updated.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }
            }

        }

        private void ClearForm()
        {
            txtConsigneeCode.Text = "";
            txtConsigneeName.Text = "";
            txtConsigneeAddress.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtTelephone.Text = "";
            txtMobile.Text = "";
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btndelete.Enabled = false;
            objConsignee = new Consignee();
            dataGridView1.ClearSelection();
            labelControl1.Focus();
        }
                 
       
       }
}
