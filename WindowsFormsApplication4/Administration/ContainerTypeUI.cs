using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LOGISTIC.BLL;

namespace LOGISTIC.UI.Administration
{
    public partial class ContainerTypeUI : Form
    {
        private List<ContainerType> listType = new List<ContainerType>();
        private ContainerType objContainerType = new ContainerType();
        private ContainerTypeBll objBll = new ContainerTypeBll();

        public ContainerTypeUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        private void ContainerTypeUI_Load(object sender, EventArgs e)
        {
           
            PrepareGrid();
            LoadDataToGrid();
            objContainerType = new ContainerType();
            btnDelete.Enabled = false;
        }
     

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Goldenrod;            
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 3;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";
            
            dataGridView1.Columns[1].HeaderText = "Type Name";
            dataGridView1.Columns[1].DataPropertyName = "ContainerTypeName";
          
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].HeaderText = "ID";

            dataGridView1.AllowUserToAddRows = false;


        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listType = objBll.Getall();
            if (listType.Count > 0)
            {
                int index = 1;
                foreach (var item in listType)
                {
                    dataGridView1.Rows.Add(index, item.ContainerTypeName, item.ContainerTypeId);
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
            //string slect = cmbSearch.Text.Trim();
            //string value = txtSearch.Text.ToString();

            //switch (slect)
            //{
            //    case "ID":
            //        {
            //            int valueint = Convert.ToInt32(value);
            //            dataGridView1. = objlist.where(item => item.DepotId.(valueint)).ToList();

            //            break;
            //        }
            //    case "Code":
            //        {
            //            dataGridView1.DataSource = listType.Where(item => item.ContainerTypeCode.Contains(value)).ToList();
            //            break;
            //        }
            //    case "Name":
            //        {
            //            dataGridView1.DataSource = listType.Where(item => item.ContainerTypeName.Contains(value)).ToList();
            //            break;
            //        }
            //    default:
            //        {
            //            //   System.Console.WriteLine("Other number");
            //            break;
            //        }

        //    }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Container Type deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objContainerType.ContainerTypeId != 0)
                {
                    objBll.Delete(objContainerType.ContainerTypeId);
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objContainerType = listType.ElementAt(index); 
                      
            txtContainerType.Text = Convert.ToString(objContainerType.ContainerTypeName);
            btnSave.Text = "Update";
            btnDelete.Enabled = true;

        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                int status = objBll.Insert(objContainerType);
                if (status == 1)
                {
                    MessageBox.Show("Container Type has been inserted.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }

            }
            else if (btnSave.Text == "Update")
            {

                int status = objBll.Update(objContainerType);
                if (status == 1)
                {
                    MessageBox.Show("Containe Type has been updated.");
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
                     
            if (txtContainerType.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter Container Type !!\n";
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
                objContainerType.ContainerTypeName = txtContainerType.Text.Trim();
               
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void ClearForm()
        {           
            txtContainerType.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            objContainerType = new ContainerType();
            dataGridView1.ClearSelection();
            txtContainerType.Focus();
                
        }
        
    }
}
