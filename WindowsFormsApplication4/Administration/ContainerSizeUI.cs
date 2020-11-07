using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;


namespace LOGISTIC.UI.Administration
{
    public partial class ContainerSizeUI : Form
    {
        private List<ContainerSize> objlist = new List<ContainerSize>();
        private ContainerSize objContainerSize = new ContainerSize();
        private ContainerSizeBll objBll = new ContainerSizeBll();
        private static int contSizeId;
        
        public ContainerSizeUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
           
        }


        private void ContainerSizeUI_Load(object sender, EventArgs e)
        {           
            GridLoad();
            objContainerSize = new ContainerSize();
            btnDelete.Enabled = false;
        }


       

        private void GridLoad()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dataGridView1.EnableHeadersVisualStyles = false;          
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;           
            dataGridView1.ColumnCount = 2;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].DataPropertyName = "ContainerSizeId";


            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[1].HeaderText = "Size Name";
            dataGridView1.Columns[1].DataPropertyName = "ContainerSize1";

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

      

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                           "Confirm Container Size deletion",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (contSizeId != 0)
                {

                    objBll.Delete(contSizeId);
                    GridLoad();
                    MessageBox.Show("Deleted successfully !! ");
                }

            }

            ClearForm();
        } 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
      
        private void btnTrailerClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            contSizeId = Convert.ToInt32(selectedRow.Cells[0].Value);
            objContainerSize = objBll.GetContSizeDetailsById(contSizeId);


            txtContainerSize.Text = Convert.ToString(objContainerSize.ContainerSize1);

            btnSave.Text = "Update";
            btnDelete.Enabled = true;


        }

        private bool Validation()
        {
            var errMessage = "";
           
            if (txtContainerSize.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter container size !!\n";
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
                objContainerSize.ContainerSize1 = Convert.ToInt32(txtContainerSize.Text.Trim());              
                
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
                int status = objBll.Insert(objContainerSize);
                if (status == 1)
                {
                    MessageBox.Show("Container Size has been inserted.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }

            }
            else if (btnSave.Text == "Update")
            {

                int status = objBll.Update(objContainerSize);
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

        private void ClearForm()
        {            
            txtContainerSize.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            objContainerSize = new ContainerSize();
            dataGridView1.ClearSelection();
            txtContainerSize.Focus();
            lblDepot.Focus();

        }

       
    }
    



}
