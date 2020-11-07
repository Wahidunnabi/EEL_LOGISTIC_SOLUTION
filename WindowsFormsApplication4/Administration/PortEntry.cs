using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
namespace LOGISTIC.UI.Administration
{
    public partial class PortEntry : Form
    {
        private List<Port> listPort = new List<Port>();
        private Port objPort = new Port();
        private PortBLL portBLL = new PortBLL();

        public PortEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);          
        }


        private void CommodityEntry_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            ComboLoad();
            PrepareGrid();
            LoadDataToGrid();
        }

        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Port Code");
            cmbSearch.Items.Insert(3, "Port Name");
            cmbSearch.Items.Insert(4, "Country");
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

            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[1].HeaderText = "Port Code";

            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[2].HeaderText = "Port Name";

            dataGridView1.Columns[3].Width = 127;
            dataGridView1.Columns[3].HeaderText = "Country";


        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listPort = portBLL.Getall();

            if (listPort.Count > 0)
            {
                int index = 1;
                foreach (var item in listPort)
                {
                    dataGridView1.Rows.Add(index, item.PortCode, item.PortName, item.PortCountry);
                    index = index + 1;
                }

            }

            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<Port> listPort)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            if (listPort.Count > 0)
            {
                int index = 1;
                foreach (var item in listPort)
                {
                    dataGridView1.Rows.Add(index, item.PortCode, item.PortName, item.PortCountry);
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
                case "Port Code":
                    {
                        
                       var listItem = listPort.Where(c => c.PortCode.Contains(value)).ToList();
                        LoadFilterDataToGrid(listItem);
                        break;
                    }
                case "Port Name":
                    {
                        var listItem = listPort.Where(item => item.PortName.Contains(value)).ToList();
                        LoadFilterDataToGrid(listItem);
                        break;
                    }
                case "Country":
                    {
                        var listItem = listPort.Where(item => item.PortCountry.Contains(value)).ToList();
                        LoadFilterDataToGrid(listItem);
                        break;
                    }
                default:
                    {
                        LoadFilterDataToGrid(listPort);
                        break;
                    }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Port deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objPort.PortOfLandId != 0)
                {

                    portBLL.Delete(objPort.PortOfLandId);
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
            objPort = listPort.ElementAt(index);

            txtPortCode.Text = Convert.ToString(objPort.PortCode);
            txtPortName.Text = Convert.ToString(objPort.PortName);
            txtPortCountry.Text = Convert.ToString(objPort.PortCountry);

            btnSave.Text = "Update";
            btnDelete.Enabled = true;
        }

        private bool Validation()
        {
            var errMessage = "";

            if (txtPortName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter port name !!\n";
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
                objPort.PortCode = txtPortCode.Text.Trim();
                objPort.PortName = txtPortName.Text.Trim();
                objPort.PortCountry = txtPortCountry.Text.Trim();
                
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
                int status = portBLL.Insert(objPort);
                if (status == 1)
                {
                    MessageBox.Show("Port has been inserted.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }

            }
            else if (btnSave.Text == "Update")
            {

                portBLL.Update(objPort);
                MessageBox.Show("Port has been updated successfully..");
            }

        }       

        private void ClearForm()
        {
            txtPortCode.Text = "";
            txtPortName.Text = "";
            txtPortCountry.Text = "";
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btnDelete.Enabled = false;
            objPort = new Port();
            dataGridView1.ClearSelection();
            txtPortCode.Focus();

        }

       
    }
    


}
