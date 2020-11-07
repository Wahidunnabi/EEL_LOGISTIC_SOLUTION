using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace LOGISTIC.UI.Administration
{
    public partial class ServiceName : Form
    {
        private List<ChartOfService> listService = new List<ChartOfService>();
        private ChartOfService objService = new ChartOfService();
        private ServiceNameBLL objBll = new ServiceNameBLL();
     
        public ServiceName()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
           
        }

        private void ServiceName_Load(object sender, EventArgs e)
        {
           
           
            btnDelete.Enabled = false;           
            LoadServiceCategory();
            LoadServiceParent();
            PrepareGrid();
            LoadDataToGrid();
        }      

        private void LoadServiceCategory()
        {

            var type = objBll.GetAllServiceCategory();
            ddlServiceCategory.DisplayMember = "CategoryName";
            ddlServiceCategory.ValueMember = "CateId";

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CateId, t.CategoryName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Category--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlServiceCategory.DataSource = dt_Types;
                ddlServiceCategory.DisplayMember = "t_Name";
                ddlServiceCategory.ValueMember = "t_ID";
            }
            ddlServiceCategory.SelectedIndex = 0;


        }

        private void LoadServiceParent()
        {
            ddlServiceParent.DataSource = null;
            ddlServiceCategory.Refresh();
            ddlServiceParent.Items.Clear();

            var type = objBll.GetallParent();
            ddlServiceParent.DisplayMember = "ServiceName";
            ddlServiceParent.ValueMember = "ServiceId";

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ServiceId, t.ServiceName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Parent--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlServiceParent.DataSource = dt_Types;
                ddlServiceParent.DisplayMember = "t_Name";
                ddlServiceParent.ValueMember = "t_ID";
            }
            ddlServiceParent.SelectedIndex = 0;


        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[1].HeaderText = "Service Name";           
            
            dataGridView1.Columns[2].HeaderText = "Entry Date";

            dataGridView1.Columns[3].HeaderText = "Is Active";

            dataGridView1.AllowUserToAddRows = false;

           
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listService = objBll.Getall();         
            if (listService.Count > 0)
            {
                int index = 1;
                foreach (var item in listService)
                {
                    dataGridView1.Rows.Add(index, item.ServiceName, item.EntryDate, item.IsActive );
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
                if (!chkIsTransition.Checked)
                {
                    LoadServiceParent();
                }
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
                if (objService.ServiceId != 0)
                {

                    var status = objBll.Delete(objService.ServiceId);
                    MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGrid();
                   
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

       

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var index = Convert.ToInt32(selectedRow.Index);
            objService = listService.ElementAt(index);
           
            ddlServiceCategory.SelectedValue = Convert.ToInt32(objService.CateId);
            if (objService.ParentId != null)
            {
                ddlServiceParent.SelectedValue = Convert.ToInt32(objService.ParentId);
            }            
            txtServiceName.Text = Convert.ToString(objService.ServiceName);
            chkIsTransition.Checked = Convert.ToBoolean(objService.IsTransaction);
            chkIsActive.Checked = Convert.ToBoolean(objService.IsActive);

            btnSave.Text = "Update";
            btnDelete.Enabled = true;

        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                var status = objBll.Insert(objService);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.Update(objService);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private bool Validation()
        {
            var errMessage = "";

            var categoryId = Convert.ToInt32(ddlServiceCategory.SelectedValue);          


            if (categoryId == 0)
            {
                errMessage = errMessage + "* Please select a category !!\n";
            }
            if (txtServiceName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter service name !!\n";
            }
            //if (!chkParent.Checked)
            //{
            //    var parentId = Convert.ToInt32(ddlServiceParent.SelectedValue);

            //    if (parentId == 0)
            //    {
            //        errMessage = errMessage + "* Please select a parent !!\n";
            //    }

            //}                     
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
               
                objService.CateId = Convert.ToInt32(ddlServiceCategory.SelectedValue);
                objService.ParentId = Convert.ToInt32(ddlServiceParent.SelectedValue);
                objService.ServiceName = txtServiceName.Text.Trim();
                objService.IsTransaction = chkIsTransition.Checked;
                objService.IsActive = chkIsActive.Checked;
                objService.EntryDate = DateTime.Now;
               
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void Cancel()
        {
            ddlServiceCategory.SelectedIndex = 0;
            ddlServiceParent.SelectedIndex = 0;
            txtServiceName.Text = "";
            chkIsTransition.Checked = false;
            chkIsActive.Checked = false;
            btnSave.Text = "Save";           
            btnDelete.Enabled = false;
            objService = new ChartOfService();
            dataGridView1.ClearSelection();
            labelControl2.Focus();

        }

        private void chkParent_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkIsTransition.Checked)
            //{
            //    ddlServiceParent.SelectedIndex = 0;
            //    ddlServiceParent.Enabled = false;
                
            //}
            //else
            //{

            //    ddlServiceParent.Enabled = true;

            //}
        }
    }
 
}
