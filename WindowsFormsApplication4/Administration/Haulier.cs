using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
namespace LOGISTIC.UI.Administration
{
    public partial class CSDGateInOutData : Form
    {
        private List<Haulier> listHaulier = new List<Haulier>();
        private Haulier objHaulier = new Haulier();
        private HaulierBLL objBll = new HaulierBLL();
       
        
        public CSDGateInOutData()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
           
        }

        private void Haulier_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;          
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


            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[1].HeaderText = "Haulier Type";
           

        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listHaulier = objBll.GetHalrList();

            if (listHaulier.Count > 0)
            {
                int index = 1;
                foreach (var item in listHaulier)
                {
                    dataGridView1.Rows.Add(index, item.HaulierNo);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }             

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Haulier deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objHaulier.HaulierId != 0)
                {

                    objBll.Delete(objHaulier.HaulierId);                    
                    MessageBox.Show("Deleted successfully !! ");
                    LoadDataToGrid();
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

            var index = Convert.ToInt32(selectedRow.Index);
            objHaulier = listHaulier.ElementAt(index);
            
            txtHaulierName.Text = objHaulier.HaulierNo;
           
            btnSave.Text = "Update";
            btnDelete.Enabled = true;


        }
       
        private bool Validation()
        {
            var errMessage = "";

            if (txtHaulierName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter haulier type !!\n";
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
                objHaulier.HaulierNo = txtHaulierName.Text.Trim();                
                
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
                int status = objBll.Insert(objHaulier);
                if (status == 1)
                {
                    MessageBox.Show("Haulier has been inserted successfully.");
                }
                else
                {
                    MessageBox.Show("Something went wrong !!!.");
                }

            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.Update(objHaulier);
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
            txtHaulierName.Text = "";          
            btnSave.Text = "Save";          
            btnDelete.Enabled = false;
            objHaulier = new Haulier();
            dataGridView1.ClearSelection();
            txtHaulierName.Focus();

        }


    }
   

}
