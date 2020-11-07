using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
namespace LOGISTIC.UI.Administration
{
    public partial class CommodityEntry : Form
    {
        private List<Commodity> objComlist = new List<Commodity>();
        private Commodity objCom = new Commodity();
        private CommodityBLL comBLL = new CommodityBLL();
        private static int commodityID;

        public CommodityEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);          
        }


        private void CommodityEntry_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            GridLoad();
            ComboLoad();
        }

        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");            
            cmbSearch.Items.Insert(2, "Name");
            cmbSearch.SelectedIndex = 0;

        }

        private void GridLoad()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Goldenrod;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToAddRows = false;
            
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 2;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].DataPropertyName = "CommodityId";

            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[1].HeaderText = "Commodity Name";
            dataGridView1.Columns[1].DataPropertyName = "CommodityName";

           
            objComlist = comBLL.Getall();
            dataGridView1.DataSource = objComlist;            

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = Validation();
            if (flag == true)
            {
                FillingData();
                SaveData();
                GridLoad();
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
                             
                case "Name":
                    {
                        dataGridView1.DataSource = objComlist.Where(item => item.CommodityName.Contains(value)).ToList();
                        break;
                    }
                default:
                    {
                        dataGridView1.DataSource = objComlist.ToList();
                        break;
                    }
            }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm commodity deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (commodityID != 0)
                {

                    comBLL.Delete(commodityID);                    
                    MessageBox.Show("Deleted successfully !! ");
                    GridLoad();
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

        private void SaveData()
        {           
            if (btnSave.Text == "Save")
            {
                int status = comBLL.Insert(objCom);
                if (status == 1)
                {
                    MessageBox.Show("Commodity has been inserted.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }

            }
            else if (btnSave.Text == "Update")
            {
                objCom.CommodityId = commodityID;
                comBLL.Update(objCom.CommodityId, objCom);
                MessageBox.Show("Commodity has been updated successfully..");
            }

        }

        private bool Validation()
        {
            var errMessage = "";

            if (txtCommodityName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter commodity name !!\n";
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
                objCom.CommodityName = txtCommodityName.Text.Trim();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
              
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                commodityID = Convert.ToInt32(selectedRow.Cells[0].Value);
                Commodity objCom = comBLL.GetCommodityByID(commodityID);
                txtCommodityName.Text = Convert.ToString(objCom.CommodityName.Trim());
                
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
        }

        private void ClearForm()
        {
            txtCommodityName.Text = "";
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btnDelete.Enabled = false;
            dataGridView1.ClearSelection();
            objCom = new Commodity();
            txtCommodityName.Focus();

        }

        
    }
    


}
