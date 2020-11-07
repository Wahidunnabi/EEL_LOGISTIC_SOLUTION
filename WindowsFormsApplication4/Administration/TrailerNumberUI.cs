using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LOGISTIC.UI.Administration
{
    public partial class TrailerNumberUI : Form
    {
        private List<TrailerNumber> listTrailerNumber = new List<TrailerNumber>();
        private TrailerNumber objTrailerNumber = new TrailerNumber();
        private TrailerNumberBll objBll = new TrailerNumberBll();
     
        public TrailerNumberUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
           
        }


        private void TrailerNumberUI_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            PrepareGrid();
            LoadDataToGrid();
            ComboLoad();
            LoadcmbTrailer();
        }

        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Trailer Number");
            cmbSearch.Items.Insert(3, "Trailer Name");           
            cmbSearch.SelectedIndex = 0;

        }

        private void LoadcmbTrailer()
        {
            Trailer objTr = new Trailer();
            objTr.TrailerId = 0;
            objTr.TrailerName = "--Select a Trailer--";

            comboTrailer.Items.Add(objTr);

            List<Trailer> objTrailer = new List<Trailer>();
            objTrailer = objBll.GetComboTrailer();
            foreach (var item in objTrailer)
            {
                comboTrailer.Items.Add(item);
                comboTrailer.DisplayMember = "TrailerName";
                comboTrailer.ValueMember = "TrailerId";

            }

            comboTrailer.SelectedIndex = 0;

        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";
   
            dataGridView1.Columns[1].HeaderText = "Trailer Number";

            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[2].HeaderText = "Trailer Name";
            
            dataGridView1.Columns[3].HeaderText = "Entry Date";

            dataGridView1.AllowUserToAddRows = false;

           // dataGridView1.ClearSelection();
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listTrailerNumber = objBll.Getall();         
            if (listTrailerNumber.Count > 0)
            {
                int index = 1;
                foreach (var item in listTrailerNumber)
                {
                    dataGridView1.Rows.Add(index, item.TrailerNumber1, item.Trailer.TrailerName, item.EntryDate );
                    index = index + 1;
                }
                
            }
           // dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<TrailerNumber> objlist)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
           
            if (objlist.Count > 0)
            {
                int index = 1;
                foreach (var item in objlist)
                {
                    dataGridView1.Rows.Add(index, item.TrailerNumber1, item.Trailer.TrailerName, item.EntryDate);
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
                Cancel();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Trailer Number deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objTrailerNumber.TrailerNumberId != 0)
                {

                    objBll.Delete(objTrailerNumber.TrailerNumberId);
                    LoadDataToGrid();
                    MessageBox.Show("Deleted successfully !! ");
                }

            }

            Cancel();
        }

        private void btnTrailerClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
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
                case "Trailer Number":
                    {

                        var filterobjlist = listTrailerNumber.Where(c => c.TrailerNumber1.Equals(value)).ToList();
                        LoadFilterDataToGrid(filterobjlist);
                        break;
                    }
                case "Trailer Name":
                    {

                        var filterobjlist = listTrailerNumber.Where(c => c.Trailer.TrailerName.Contains(value)).ToList();
                        LoadFilterDataToGrid(filterobjlist);
                        break;
                    }
                case "All":
                    {
                        LoadFilterDataToGrid(listTrailerNumber);                        
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
            objTrailerNumber = listTrailerNumber.ElementAt(index);
            comboTrailer.Text = Convert.ToString(objTrailerNumber.Trailer.TrailerName);
            txttrailerNumber.Text = Convert.ToString(objTrailerNumber.TrailerNumber1);           
            btnSave.Text = "Update";
            btnDelete.Enabled = true;

        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                int status = objBll.Insert(objTrailerNumber);
                if (status == 1)
                {
                    MessageBox.Show("Trailer Numbe has been inserted.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }

            }
            else if (btnSave.Text == "Update")
            {

                int status = objBll.Update(objTrailerNumber);
                if (status == 1)
                {
                    MessageBox.Show("Trailer Numbe has been updated.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }
            }

        }

        private bool Validation()
        {
            var errMessage = "";
            var trailer = (Trailer)comboTrailer.SelectedItem;
            int trailerId = trailer.TrailerId;

            if (trailerId == 0)
            {
                errMessage = errMessage + "* Please select a trailer account !!\n";
            }
            if (txttrailerNumber.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter Trailer Number !!\n";
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
                var trailer = (Trailer)comboTrailer.SelectedItem;
                objTrailerNumber.TrailerId = trailer.TrailerId;
                objTrailerNumber.TrailerNumber1 = txttrailerNumber.Text.Trim();
                objTrailerNumber.EntryDate = DateTime.Now;
                objTrailerNumber.UserId = 1;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void Cancel()
        {
            comboTrailer.SelectedIndex = 0;
            txttrailerNumber.Text = "";
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btnDelete.Enabled = false;
            objTrailerNumber = new TrailerNumber();
            dataGridView1.ClearSelection();
            labelControl2.Focus();

        }

        
    }
 
}
