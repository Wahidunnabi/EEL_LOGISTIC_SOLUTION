using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LOGISTIC.UI.Administration
{
    public partial class BankEntry : Form
    {
        private List<Bank> listBank = new List<Bank>();
        private Bank objBank = new Bank();
        private BankBLL objBll = new BankBLL();
       
        public BankEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            btnDelete.Enabled = false;           
        }

        private void BankEntry_Load(object sender, EventArgs e)
        {
            ComboLoad();
            PrepareGrid();
            LoadDataToGrid();
        }
       
        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");            
            cmbSearch.Items.Insert(2, "Bank Name");
            cmbSearch.Items.Insert(3, "Branch");
            cmbSearch.SelectedIndex = 0;
        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 170;
            dataGridView1.Columns[1].HeaderText = "Bank Name";
            dataGridView1.Columns[1].DataPropertyName = "BankName";

            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[2].HeaderText = "Branch";
            dataGridView1.Columns[2].DataPropertyName = "Branch";
           

            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[3].HeaderText = "BankId";

            dataGridView1.AllowUserToAddRows = false;


        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listBank = objBll.Getall();
            if (listBank.Count > 0)
            {
                int index = 1;
                foreach (var item in listBank)
                {
                    dataGridView1.Rows.Add(index, item.BankName, item.Branch, item.BankId);
                    index = index + 1;
                }

            }
            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<Bank> listBank)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listBank.Count > 0)
            {
                int index = 1;
                foreach (var item in listBank)
                {
                    dataGridView1.Rows.Add(index, item.BankName, item.Branch, item.BankId);
                    index = index + 1;
                }

            }
            else
            {
                dataGridView1.ClearSelection();
                MessageBox.Show("No data found !!", "Search result.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
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
                                     "Confirm Bank Name deletion",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objBank.BankId != 0)
                {

                    var status = objBll.Delete(objBank);
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
                case "Bank Name":
                    {

                        var filterobjlist = listBank.Where(c => c.BankName.Contains(value)).ToList();
                        LoadFilterDataToGrid(filterobjlist);
                        break;
                    }
                case "Branch":
                    {

                        var filterobjlist = listBank.Where(c => c.Branch.Contains(value)).ToList();
                        LoadFilterDataToGrid(filterobjlist);
                        break;
                    }
                case "All":
                    {
                        LoadFilterDataToGrid(listBank);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            //cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";

        }


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objBank = listBank.ElementAt(index);

            txtBank.Text = objBank.BankName;
            txtBranch.Text = objBank.Branch;

            btnSave.Text = "Update";
            btnDelete.Enabled = true;

        }

        private bool Validation()
        {
            var errMessage = "";

            if (txtBank.Text.Trim() == "")
            {
                errMessage = errMessage + "* Please type bank name !!\n";
            }
            if (txtBranch.Text.Trim() == "")
            {
                errMessage = errMessage + "* Please type branch name !!\n";
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
                objBank.BankName = txtBank.Text.Trim();
                objBank.Branch = txtBranch.Text.Trim();               
                
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
                var status = objBll.Insert(objBank);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (btnSave.Text == "Update")
            {
                var status = objBll.Update(objBank);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ClearForm()
        {
            txtBank.Text = "";
            txtBranch.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            dataGridView1.ClearSelection();
            objBank = new Bank();
            txtBank.Focus();
        }

       
    }
    


}
