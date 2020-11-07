using System;
using System.Collections.Generic;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LOGISTIC.UI.Administration
{
    public partial class MeasurementUnitEntry : Form
    {
        private List<UnintOfMeasure> listUnit = new List<UnintOfMeasure>();
        private UnintOfMeasure objUnintOfMeasure = new UnintOfMeasure();
        private UnitofMeasureBll objBll = new UnitofMeasureBll();

        public MeasurementUnitEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        private void MeasurementUnitEntry_Load(object sender, EventArgs e)
        {
            btndelete.Enabled = false;
            PrepareGrid();
            LoadDataToGrid();
        }

        private void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Goldenrod;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 2;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";            
            dataGridView1.Columns[1].HeaderText = "Unit Name";           
           
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listUnit = objBll.Getall();
            if (listUnit.Count > 0)
            {
                int index = 1;
                foreach (var item in listUnit)
                {
                    dataGridView1.Rows.Add(index, item.UnitOfMeasureName);
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
                          "Confirm Measurement Unit deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objUnintOfMeasure.UnitOfMeasureId != 0)
                {

                    objBll.Delete(objUnintOfMeasure.UnitOfMeasureId);                    
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
            objUnintOfMeasure = listUnit.ElementAt(index);
            txtUnitName.Text = Convert.ToString(objUnintOfMeasure.UnitOfMeasureName);
            btnSave.Text = "Update";
            btndelete.Enabled = true;
           
        }


        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                int status = objBll.Insert(objUnintOfMeasure);
                if (status == 1)
                {
                    MessageBox.Show("Unit has been inserted.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }

            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.Update(objUnintOfMeasure);
                if (status == 1)
                {
                    MessageBox.Show("Unit has been updated successfully..");
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

            if (txtUnitName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter unit name !!\n";
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
              
                objUnintOfMeasure.UnitOfMeasureName = txtUnitName.Text.Trim();

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void ClearForm()
        {

            txtUnitName.Text = "";
            btnSave.Text = "Save";
            btndelete.Enabled = false;
            objUnintOfMeasure = new UnintOfMeasure();
            dataGridView1.ClearSelection();
            labelControl1.Focus();
        }

       
    }
}
