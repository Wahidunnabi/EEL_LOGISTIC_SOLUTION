using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LOGISTIC.UI.Administration
{
    public partial class ServiceCategory : Form
    {
        private List<ChartOfServiceCategory>listCategory = new List<ChartOfServiceCategory>();
        private ChartOfServiceCategory objCategory = new ChartOfServiceCategory();
        private ServiceCategoryBLL objBll = new ServiceCategoryBLL();
       
        public ServiceCategory()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            btnDelete.Enabled = false;           
        }

        private void DepotUI_Load(object sender, EventArgs e)
        {                    
            PrepareGrid();
            LoadDataToGrid();
        }

      

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 2;

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].HeaderText = "Category Name";

            //dataGridView1.Columns[1].DataPropertyName = "DepotCode";
          
            //dataGridView1.Columns[2].Visible = false;
            //dataGridView1.Columns[2].HeaderText = "ID";

            dataGridView1.AllowUserToAddRows = false;


        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listCategory = objBll.Getall();
            if (listCategory.Count > 0)
            {
                int index = 1;
                foreach (var item in listCategory)
                {
                    dataGridView1.Rows.Add(index, item.CategoryName,  item.CateId);
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
                if (objCategory.CateId != 0)
                {

                    var status = objBll.Delete(objCategory.CateId);
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
               


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objCategory = listCategory.ElementAt(index);
            
            txtCategoryName.Text = objCategory.CategoryName;

            btnSave.Text = "Update";
            btnDelete.Enabled = true;

        }

        private bool Validation()
        {
            var errMessage = "";
           
            if (txtCategoryName.Text.Trim() == "")
            {
                errMessage = errMessage + "* Please Enter Category Name !!\n";
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
                objCategory.CategoryName = txtCategoryName.Text.Trim();
                              
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
                var status = objBll.Insert(objCategory);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (btnSave.Text == "Update")
            {
                var status = objBll.Update(objCategory);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ClearForm()
        {
           
            txtCategoryName.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;           
            dataGridView1.ClearSelection();
            objCategory = new ChartOfServiceCategory();
            txtCategoryName.Focus();

        }

       

        
    }
    


}
