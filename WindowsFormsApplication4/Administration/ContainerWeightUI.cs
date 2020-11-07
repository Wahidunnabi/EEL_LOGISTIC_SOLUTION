using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LOGISTIC.UI.Administration
{
    public partial class ContainerWeightUI : Form
    {
        private List<ContainerGrossWeight> listWeight = new List<ContainerGrossWeight>();
        private ContainerGrossWeight objContainerWeight = new ContainerGrossWeight();
        private ContainerWeightBll objBll = new ContainerWeightBll();


        public ContainerWeightUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);          
        }

        private void ContainerWeightUI_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            PrepareGrid();
            LoadDataToGrid();
        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 3;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].HeaderText = "Gross Weight";

            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].HeaderText = "ID";

            dataGridView1.AllowUserToAddRows = false;

 
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listWeight = objBll.Getall();
            if (listWeight.Count > 0)
            {
                int index = 1;
                foreach (var item in listWeight)
                {
                    dataGridView1.Rows.Add(index, item.GrossWeight);
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


        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Gross Weight deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objContainerWeight.ContGrossWeightId != 0)
                {

                    objBll.Delete(objContainerWeight);
                    LoadDataToGrid();
                    MessageBox.Show("Deleted successfully !! ");
                }

            }

            ClearForm();

        }

        private void btnTrailerClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
              

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objContainerWeight = listWeight.ElementAt(index);

            txtContainerWeight.Text = Convert.ToString(objContainerWeight.GrossWeight);
           
            btnSave.Text = "Update";
            btnDelete.Enabled = true;
        }

        private bool Validation()
        {
            var errMessage = "";

            if (txtContainerWeight.Text.Trim() == "")
            {
                errMessage = errMessage + "* Please type gross weight !!\n";
                txtContainerWeight.Focus();
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

        private void FillingData()
        {
            objContainerWeight.GrossWeight = Convert.ToDecimal(txtContainerWeight.Text.Trim());

        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                int status = objBll.Insert(objContainerWeight);

                if (status == 1)
                {
                    MessageBox.Show("Gross weight has been inserted.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }
            }
            else if (btnSave.Text == "Update")
            {
                int status = objBll.Update(objContainerWeight);
                if (status == 1)
                {
                    MessageBox.Show("Data has been updated.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }
            }

        }

        private void ClearForm()
        {
            txtContainerWeight.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            objContainerWeight = new ContainerGrossWeight();
            dataGridView1.ClearSelection();
            txtContainerWeight.Focus();
        }

    }
    
}
