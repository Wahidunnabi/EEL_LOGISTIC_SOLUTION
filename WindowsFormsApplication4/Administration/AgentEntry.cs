using System;
using System.Collections.Generic;
using System.Data;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LOGISTIC.UI.Administration
{
    public partial class AgentEntry : Form
    {
        private List<Agent> listAgent = new List<Agent>();
        private Agent objAgent = new Agent();
        private AgentBLL objBll = new AgentBLL();


        public AgentEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        private void AgentEntry_Load(object sender, EventArgs e)
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

            dataGridView1.Columns[1].HeaderText = "Agent Code";

            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[2].HeaderText = "Agent Name";

            dataGridView1.Columns[3].HeaderText = "Entry Date";

        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listAgent = objBll.Getall();
            if (listAgent.Count > 0)
            {
                int index = 1;
                foreach (var item in listAgent)
                {
                    dataGridView1.Rows.Add(index, item.AgentCode, item.AgentName, item.EntryDate);
                    index = index + 1;
                }

            }
            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<Agent> listAgent)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listAgent.Count > 0)
            {
                int index = 1;
                foreach (var item in listAgent)
                {
                    dataGridView1.Rows.Add(index, item.AgentCode, item.AgentName, item.EntryDate);
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
                if (objAgent.AgentId != 0)
                {

                    var status = objBll.Delete(objAgent.AgentId);
                    MessageBox.Show(status.ToString(), "Data Deletion Status.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        var listitems = listAgent.Where(item => item.AgentCode.Contains(value)).ToList();
                        LoadFilterDataToGrid(listitems);
                        break;
                    }
                case "NAME":
                    {
                        var listitems = listAgent.Where(item => item.AgentName.Contains(value)).ToList();
                        LoadFilterDataToGrid(listitems);
                        break;
                    }
                default:
                    {
                        LoadFilterDataToGrid(listAgent);
                        break;
                    }
            }

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objAgent = listAgent.ElementAt(index);

            txtAgentCode.Text = Convert.ToString(objAgent.AgentCode);
            txtAgentName.Text = Convert.ToString(objAgent.AgentName);           
            dateIn.Value = Convert.ToDateTime(objAgent.EntryDate);
            btnSave.Text = "Update";
            btndelete.Enabled = true;

        }
        
        private bool Validation()
        {
            var errMessage = "";

            if (txtAgentName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter agent name !!\n";
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
                objAgent.AgentCode = txtAgentCode.Text.Trim();
                objAgent.AgentName = txtAgentName.Text.Trim();               
                objAgent.EntryDate = DateTime.Now;


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
                var status = objBll.Insert(objAgent);
                MessageBox.Show(status.ToString(), "Data Insertion Status.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.Update(objAgent);
                MessageBox.Show(status.ToString(), "Data Update Status.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ClearForm()
        {
            txtAgentCode.Text = "";
            txtAgentName.Text = "";           
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btndelete.Enabled = false;
            objAgent = new Agent();
            dataGridView1.ClearSelection();

        }

        
    }
}
