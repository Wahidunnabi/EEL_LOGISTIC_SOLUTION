using System;
using System.Collections.Generic;
using System.Data;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LOGISTIC.UI.Administration
{
    public partial class ImporterEntry : Form
    {
        private List<Importer> listImporter = new List<Importer>();
        private Importer objImporter = new Importer();
        private ImporterBll objBll = new ImporterBll();
 

        public ImporterEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
        }
        private void ImporterEntry_Load(object sender, EventArgs e)
        {
                       
            ComboLoad();
            PrepareGrid();
            LoadDataToGrid();
            btndelete.Enabled = false;

        }       

        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");           
            cmbSearch.Items.Insert(2, "Importer Code");
            cmbSearch.Items.Insert(3, "Importer Name");
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


            dataGridView1.Columns[1].HeaderText = "Importer Code";
            dataGridView1.Columns[1].DataPropertyName = "ImporterCode";

            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[2].HeaderText = "Importer Name";
            dataGridView1.Columns[2].DataPropertyName = "ImporterName";


            dataGridView1.Columns[3].HeaderText = "Telephone";
            dataGridView1.Columns[3].DataPropertyName = "Telephone";
           
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listImporter = objBll.Getall();
            if (listImporter.Count > 0)
            {
                int index = 1;
                foreach (var item in listImporter)
                {
                    dataGridView1.Rows.Add(index, item.ImporterCode, item.ImporterName, item.Telephone);
                    index = index + 1;
                }

            }
            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<Importer> listImporter)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listImporter.Count > 0)
            {
                int index = 1;
                foreach (var item in listImporter)
                {
                    dataGridView1.Rows.Add(index, item.ImporterCode, item.ImporterName, item.Telephone);
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
                          "Confirm Importer deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objImporter.ImporterId != 0)
                {
                    var status = objBll.Delete(objImporter.ImporterId);
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
                case "Importer Code":
                    {                       
                        var listItems = listImporter.Where(c => c.ImporterCode.Contains(value)).ToList();
                        LoadFilterDataToGrid(listItems);
                        break;
                    }
                case "Importer Name":
                    {

                        var listItems = listImporter.Where(item => item.ImporterName.Contains(value)).ToList();
                        LoadFilterDataToGrid(listItems);
                        break;
                    }               
                default:
                    {
                        LoadFilterDataToGrid(listImporter);
                        break;
                    }
            }

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objImporter = listImporter.ElementAt(index);

            txtImporterCode.Text = Convert.ToString(objImporter.ImporterCode);
            txtImporterName.Text = Convert.ToString(objImporter.ImporterName);
            txtImporterAddress.Text = Convert.ToString(objImporter.ImporterAddress);
            txtTelephone.Text = Convert.ToString(objImporter.Telephone);
            txtMobile.Text = Convert.ToString(objImporter.Mobile);
            txtFax.Text = Convert.ToString(objImporter.Fax);
            txtEmail.Text = Convert.ToString(objImporter.Email);
            dateIn.Value = Convert.ToDateTime(objImporter.EntryDate);

            btnSave.Text = "Update";
            btndelete.Enabled = true;



        }

        private bool Validation()
        {
            var errMessage = "";

            if (txtImporterName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter Importer name !!\n";
            }
            if (txtTelephone.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter telephone number !!\n";
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
                objImporter.ImporterCode = txtImporterCode.Text.Trim();
                objImporter.ImporterName = txtImporterName.Text.Trim();

                objImporter.ImporterAddress = txtImporterAddress.Text.Trim();
                objImporter.Email = txtEmail.Text.Trim();
                objImporter.Fax = txtFax.Text.Trim();
                objImporter.Mobile = txtMobile.Text.Trim();
                objImporter.Telephone = txtTelephone.Text.Trim();
                objImporter.CompanyId = 1;
                objImporter.EntryDate = DateTime.Now;
                objImporter.UserId = 1;

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
                var status = objBll.Insert(objImporter);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.Update(objImporter);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ClearForm()
        {
            txtImporterCode.Text = "";
            txtImporterName.Text = "";
            txtImporterAddress.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtMobile.Text = "";
            txtTelephone.Text = "";
            dateIn.Value = DateTime.Now;
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btndelete.Enabled = false;
            objImporter = new Importer();
            dataGridView1.ClearSelection();
            labelControl1.Focus();
        }

        
    }
}
