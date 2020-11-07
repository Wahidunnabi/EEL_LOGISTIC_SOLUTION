using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LOGISTIC.UI.Administration
{
    public partial class DepotUI : Form
    {
        private List<Depot>listDepot = new List<Depot>();
        private Depot objdepot = new Depot();
        private DepotBll objBll = new DepotBll();
       
        public DepotUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            btnDelete.Enabled = false;           
        }

        private void DepotUI_Load(object sender, EventArgs e)
        {          
            ComboLoad();
            PrepareGrid();
            LoadDataToGrid();
        }

        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Code No");
            cmbSearch.Items.Insert(3, "Depot Name");
            cmbSearch.SelectedIndex = 0;
        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].HeaderText = "Code No";
            dataGridView1.Columns[1].DataPropertyName = "DepotCode";

            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[2].HeaderText = "Depot Name";
            dataGridView1.Columns[2].DataPropertyName = "DepotCode";

            dataGridView1.Columns[3].HeaderText = "Entry Date";
            dataGridView1.Columns[3].DataPropertyName = "EntryDate";

            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[4].HeaderText = "ID";

            dataGridView1.AllowUserToAddRows = false;


        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listDepot = objBll.Getall();
            if (listDepot.Count > 0)
            {
                int index = 1;
                foreach (var item in listDepot)
                {
                    dataGridView1.Rows.Add(index, item.DepotCode, item.DepotName, item.EntryDate, item.DepotId);
                    index = index + 1;
                }

            }
            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<Depot> listDepot)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listDepot.Count > 0)
            {
                int index = 1;
                foreach (var item in listDepot)
                {
                    dataGridView1.Rows.Add(index, item.DepotCode, item.DepotName, item.EntryDate, item.DepotId);
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
                                     "Confirm Trailer Number deletion",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objdepot.DepotId != 0)
                {

                    objBll.Delete(objdepot);
                    LoadDataToGrid();
                    MessageBox.Show("Deleted successfully !! ");
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
            Close();
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

                        var filterobjlist = listDepot.Where(c => c.DepotCode.Contains(value)).ToList();
                        LoadFilterDataToGrid(filterobjlist);
                        break;
                    }
                case "Depot Name":
                    {

                        var filterobjlist = listDepot.Where(c => c.DepotName.Contains(value)).ToList();
                        LoadFilterDataToGrid(filterobjlist);
                        break;
                    }
                case "All":
                    {
                        LoadFilterDataToGrid(listDepot);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objdepot = listDepot.ElementAt(index);

            txtCode.Text = objdepot.DepotCode;
            txtDepot.Text = objdepot.DepotName;

            btnSave.Text = "Update";
            btnDelete.Enabled = true;

        }

        private bool Validation()
        {
            var errMessage = "";
           
            if (txtDepot.Text.Trim() == "")
            {
                errMessage = errMessage + "* Please type depot name !!\n";
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
            try
            {
                objdepot.DepotCode = txtCode.Text.Trim();
                objdepot.DepotName = txtDepot.Text.Trim();
                objdepot.EntryDate = DateTime.Now;
                
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
                int status = objBll.Insert(objdepot);

                if (status == 1)
                {
                    MessageBox.Show("Depot has been inserted.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }
            }
            else if (btnSave.Text == "Update")
            {
                int status = objBll.Update(objdepot);
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
            txtCode.Text = "";
            txtDepot.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            dataGridView1.ClearSelection();
            objdepot = new Depot();
            txtCode.Focus();
        }

       

        
    }
    


}
