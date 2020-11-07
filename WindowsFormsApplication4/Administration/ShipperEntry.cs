using System;
using System.Collections.Generic;
using System.Data;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LOGISTIC.UI.Administration
{
    public partial class ShipperEntry : Form
    {
        private List<Shipper> listShipper = new List<Shipper>();
        private Shipper objShipper = new Shipper();
        private ShipperBLL objBll = new ShipperBLL();


        public ShipperEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        private void ShipperEntry_Load(object sender, EventArgs e)
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
            cmbSearch.Items.Insert(2, "Shipper Code");
            cmbSearch.Items.Insert(3, "Shipper Name");

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

            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[1].HeaderText = "Shipper Code";

            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[2].HeaderText = "Shipper Name";

            dataGridView1.Columns[3].HeaderText = "Telephone";


        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listShipper = objBll.Getall();

            if (listShipper.Count > 0)
            {
                int index = 1;
                foreach (var item in listShipper)
                {
                    dataGridView1.Rows.Add(index, item.ShipperCode, item.ShipperName, item.Telephone);
                    index = index + 1;
                }

            }

            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<Shipper> listShipper)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listShipper.Count > 0)
            {
                int index = 1;
                foreach (var item in listShipper)
                {
                    dataGridView1.Rows.Add(index, item.ShipperCode, item.ShipperName, item.Telephone);
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
                case "ID":
                    {
                        int consID = Convert.ToInt32(value);
                        dataGridView1.DataSource = listShipper.Where(c => c.ShipperId.Equals(consID)).ToList();
                        break;
                    }
                case "CODE":
                    {

                        dataGridView1.DataSource = listShipper.Where(item => item.ShipperCode.Contains(value)).ToList();
                        break;
                    }
                case "NAME":
                    {
                        dataGridView1.DataSource = listShipper.Where(item => item.ShipperName.Contains(value)).ToList();
                        break;
                    }
                default:
                    {
                        dataGridView1.DataSource = listShipper.ToList();
                        break;
                    }
            }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                           "Confirm Shipper deletion",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objShipper.ShipperId != 0)
                {

                    objBll.Delete(objShipper.ShipperId);                   
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objShipper = listShipper.ElementAt(index);

            txtShipperCode.Text = Convert.ToString(objShipper.ShipperCode);
            txtShipperName.Text = Convert.ToString(objShipper.ShipperName);
            txtAddress.Text = Convert.ToString(objShipper.Address);
            txtTelephone.Text = Convert.ToString(objShipper.Telephone);
            txtMobile.Text = Convert.ToString(objShipper.Mobile);
            txtFax.Text = Convert.ToString(objShipper.Fax);
            txtEmail.Text = Convert.ToString(objShipper.Email);
            txtWebAddress.Text = Convert.ToString(objShipper.Web);

            btnSave.Text = "Update";
            btndelete.Enabled = true;

        }
       
        private bool Validation()
        {
            var errMessage = "";

            if (txtMobile.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter mobile no !!\n";
            }
            if (txtShipperName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter shipper name !!\n";
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
                objShipper.ShipperCode = txtShipperCode.Text.Trim();
                objShipper.ShipperName = txtShipperName.Text.Trim();
                objShipper.Address = txtAddress.Text.Trim();
                objShipper.Email = txtEmail.Text.Trim();
                objShipper.Fax = txtFax.Text.Trim();
                objShipper.Mobile = txtMobile.Text.Trim();
                objShipper.Telephone = txtTelephone.Text.Trim();
                objShipper.Web = txtWebAddress.Text.Trim();
               
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
                objBll.Insert(objShipper);
            }
            else if (btnSave.Text == "Update")
            {

                objBll.Update(objShipper);
                MessageBox.Show("Shipper has been updated successfully..");
            }

        }

        private void ClearForm()
        {
            txtShipperCode.Text = "";
            txtShipperName.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtTelephone.Text = "";
            txtMobile.Text = "";
            txtWebAddress.Text = "";
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btndelete.Enabled = false;
            objShipper = new Shipper();
            dataGridView1.ClearSelection();
            txtShipperCode.Focus();
        }

        
       }
}
