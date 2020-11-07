using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LOGISTIC.UI.Administration
{
    public partial class TrailerUI : Form
    {
        private List<Trailer> objlist = new List<Trailer>();
        private Trailer objTrailer = new Trailer();
        private TrailerBll objBll = new TrailerBll();
        private static int trailerId;
        
        public TrailerUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
           
        }

        private void TrailerUI_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            GridLoad();
            ComboLoad();
        }


        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Trailer Code");
            cmbSearch.Items.Insert(3, "Trailer Name");

            cmbSearch.SelectedIndex = 0;

        }

        private void GridLoad()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "ID#";
            dataGridView1.Columns[0].DataPropertyName = "TrailerId";

            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[1].HeaderText = "Trailer Code";
            dataGridView1.Columns[1].DataPropertyName = "TrailerCode";

            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[2].HeaderText = "Company Name";
            dataGridView1.Columns[2].DataPropertyName = "TrailerName";

            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[3].HeaderText = "Owner Name";
            dataGridView1.Columns[3].DataPropertyName = "OwnerName";


            objlist = objBll.Getall();
            dataGridView1.DataSource = objlist;
            
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
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
               
                case "Trailer Code":
                    {
                        dataGridView1.DataSource = objlist.Where(item => item.TrailerCode.Contains(value)).ToList();
                        break;
                    }
                case "Trailer Name":
                    {
                        dataGridView1.DataSource = objlist.Where(item => item.TrailerName.Contains(value)).ToList();
                        break;
                    }
                default:
                    {
                        dataGridView1.DataSource = objlist.ToList();
                        break;
                    }
                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Trailer deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (trailerId != 0)
                {

                    objBll.Delete(trailerId);
                    GridLoad();
                    MessageBox.Show("Deleted successfully !! ");
                }

            }

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

            trailerId = Convert.ToInt32(selectedRow.Cells[0].Value);
            objTrailer = objBll.GetTrailerById(trailerId);

            txtCode.Text = Convert.ToString(objTrailer.TrailerCode);
            txtCompanyName.Text = Convert.ToString(objTrailer.TrailerName);
            txtOwnerName.Text = Convert.ToString(objTrailer.OwnerName);

            btnSave.Text = "Update";
            btnDelete.Enabled = true;
           

        }
            

        private bool Validation()
        {
            var errMessage = "";

            if (txtCompanyName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter trailer name !!\n";
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
                objTrailer.TrailerCode = txtCode.Text.Trim();
                objTrailer.OwnerName = txtOwnerName.Text.Trim();
                objTrailer.TrailerName = txtCompanyName.Text.Trim();                
                objTrailer.EntryDate = DateTime.Now;
                
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
                int status = objBll.Insert(objTrailer);
                if (status == 1)
                {
                    MessageBox.Show("Trailer has been inserted successfully.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }

            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.Update(objTrailer);
                if (status == 1)
                {
                    MessageBox.Show("Trailer has been updated successfully.");
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
            txtCompanyName.Text = "";
            txtOwnerName.Text = "";
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            objTrailer = new Trailer();
            dataGridView1.ClearSelection();
            txtCode.Focus();
            btnDelete.Enabled = false;

        }



    }


}
