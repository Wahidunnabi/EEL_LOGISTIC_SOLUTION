using System;
using System.Collections.Generic;
using System.Data;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;

using System.Windows.Forms;

namespace LOGISTIC.UI.Administration
{
    public partial class VasselEntry : Form
    {
        private List<Vessel> listvessel = new List<Vessel>();
        private Vessel objVessel = new Vessel();
        private VesselBll vessBLL = new VesselBll();

        public VasselEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        private void VasselEntry_Load(object sender, EventArgs e)
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
            cmbSearch.Items.Insert(2, "Vessel Name");            
            cmbSearch.SelectedIndex = 0;

        }

        private void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Goldenrod;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 3;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[1].HeaderText = "Vessel Code";

            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[2].HeaderText = "Vessel Name";

        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listvessel = vessBLL.Getall();
            if (listvessel.Count > 0)
            {
                int index = 1;
                foreach (var item in listvessel)
                {
                    dataGridView1.Rows.Add(index, item.VesselCode, item.VesselName);
                    index = index + 1;
                }

            }
            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<Vessel> listvessel)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listvessel.Count > 0)
            {
                int index = 1;
                foreach (var item in listvessel)
                {
                    dataGridView1.Rows.Add(index, item.VesselCode, item.VesselName);
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
                           "Confirm vessel deletion",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objVessel.VesselId != 0)
                {

                    vessBLL.Delete(objVessel.VesselId);                    
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

        private void btnSearch_Click_1(object sender, EventArgs e)
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
                case "Vessel Name":
                    {
                        var listitems = listvessel.Where(item => item.VesselName.Contains(value)).ToList();
                        LoadFilterDataToGrid(listitems);
                        break;
                    }
                default:
                    {
                        LoadFilterDataToGrid(listvessel);
                        break;
                    }
            }

        }
 
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objVessel = listvessel.ElementAt(index);

            txtVasselCode.Text = Convert.ToString(objVessel.VesselCode);
            txtVasselName.Text = Convert.ToString(objVessel.VesselName);
            dateVessEntry.Value = Convert.ToDateTime(objVessel.EntryDate);

            btnSave.Text = "Update";
            btndelete.Enabled = true;

        }

        private bool Validation()
        {
            var errMessage = "";

            if (txtVasselName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter vessel name !!\n";
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

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                int status = vessBLL.Insert(objVessel);
                if (status == 1)
                {
                    MessageBox.Show("Vessel has been inserted.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }

            }
            else if (btnSave.Text == "Update")
            {
                var status = vessBLL.Update(objVessel);
                if (status == 1)
                {
                    MessageBox.Show("Vessel has been updated successfully..");
                }
                else { MessageBox.Show("Something went wrong !! Pls try again."); }

            }

        }
       
        private void FillingData()
        {
            try
            {
                objVessel.VesselCode = txtVasselCode.Text.Trim();
                objVessel.VesselName = txtVasselName.Text.Trim();
                objVessel.EntryDate = DateTime.Now;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void ClearForm()
        {
            txtVasselCode.Text = "";
            txtVasselName.Text = "";
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btndelete.Enabled = false;
            dateVessEntry.Value = DateTime.Now;
            objVessel = new Vessel();
            dataGridView1.ClearSelection();
            labelControl14.Focus();
        }

        
    }
}
