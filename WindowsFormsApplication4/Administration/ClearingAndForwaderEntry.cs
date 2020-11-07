using System;
using System.Collections.Generic;
using System.Data;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LOGISTIC.UI.Administration
{
    public partial class ClearingAndForwaderEntry : Form
    {
        private List<ClearAndForwadingAgent> listCAF = new List<ClearAndForwadingAgent>();
        private ClearAndForwadingAgent objCAF = new ClearAndForwadingAgent();
        private ClearAndForwaderBll objBll = new ClearAndForwaderBll();


        public ClearingAndForwaderEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }             

        private void CustomerEntry_Load(object sender, EventArgs e)
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
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SlateGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[1].HeaderText = "Code No";  
                     
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[2].HeaderText = "C&F Name"; 
                 
            dataGridView1.Columns[3].HeaderText = "Telephone";


        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listCAF = objBll.Getall();
            if (listCAF.Count > 0)
            {
                int index = 1;
                foreach (var item in listCAF)
                {
                    dataGridView1.Rows.Add(index, item.CFAgentCode, item.CFAgentName, item.Telephone);
                    index = index + 1;
                }

            }
            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<ClearAndForwadingAgent> listCAF)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
           
            if (listCAF.Count > 0)
            {
                int index = 1;
                foreach (var item in listCAF)
                {
                    dataGridView1.Rows.Add(index, item.CFAgentCode, item.CFAgentName, item.Telephone);
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
                case "All":
                    {
                        LoadFilterDataToGrid(listCAF);
                        break;
                    }
                case "CODE":
                    {

                        var listItem = listCAF.Where(item => item.CFAgentCode.Contains(value)).ToList();
                        LoadFilterDataToGrid(listItem);
                        break;
                    }
                case "NAME":
                    {
                        var listItem = listCAF.Where(item => item.CFAgentName.Contains(value)).ToList();
                        LoadFilterDataToGrid(listItem);
                        break;
                    }
                default:
                    {
                        LoadFilterDataToGrid(listCAF);
                        break;
                    }
            }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm C&F agent deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objCAF.ClearAndForwadingAgentId != 0)
                {

                    var status = objBll.Delete(objCAF.ClearAndForwadingAgentId);
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var index = Convert.ToInt32(selectedRow.Index);
            objCAF = listCAF.ElementAt(index);
            txtCAFCode.Text = Convert.ToString(objCAF.CFAgentCode);
            txtCAFName.Text = Convert.ToString(objCAF.CFAgentName);
            txtCAFAddress.Text = Convert.ToString(objCAF.Address);
            txtTelephone.Text = Convert.ToString(objCAF.Telephone);
            txtMobile.Text = Convert.ToString(objCAF.Mobile);
            txtFax.Text = Convert.ToString(objCAF.Fax);
            txtEmail.Text = Convert.ToString(objCAF.Email);
            dateIn.Value = Convert.ToDateTime(objCAF.EntryDate);
            btnSave.Text = "Update";
            btndelete.Enabled = true;


        }      

        private bool Validation()
        {
            var errMessage = "";

            if (txtCAFCode.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter C&F agent code !!\n";
            }
            if (txtCAFName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter C&F agent name !!\n";
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
                objCAF.CFAgentCode = txtCAFCode.Text.Trim();
                objCAF.CFAgentName = txtCAFName.Text.Trim();

                objCAF.Address = txtCAFAddress.Text.Trim();
                objCAF.Email = txtEmail.Text.Trim();
                objCAF.Fax = txtFax.Text.Trim();
                objCAF.Mobile = txtMobile.Text.Trim();
                objCAF.Telephone = txtTelephone.Text.Trim();
                objCAF.EntryDate = DateTime.Now;

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
                var status = objBll.Insert(objCAF);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (btnSave.Text == "Update")
            {
              var status=  objBll.Update(objCAF);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ClearForm()
        {
            txtCAFCode.Text = "";
            txtCAFName.Text = "";
            txtCAFAddress.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtTelephone.Text = "";
            dateIn.Value = DateTime.Now;
            txtMobile.Text = "";
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btndelete.Enabled = false;
            dataGridView1.ClearSelection();
            objCAF = new ClearAndForwadingAgent();
            labelControl1.Focus();
           
        }
       
       }
}
