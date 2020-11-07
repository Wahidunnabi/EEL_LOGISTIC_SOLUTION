using System.Drawing;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace LOGISTIC.UI.Administration
{
    public partial class UserPermissionEntry : Form
    {
        private List<UserPermissionMapping> listPermission = new List<UserPermissionMapping>();
        private UserPermissionMapping objPermission = new UserPermissionMapping();
        private AuthenticationBLL objBll = new AuthenticationBLL();
     
        public UserPermissionEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
           
        }

        private void UserPermissionEntry_Load(object sender, EventArgs e)
        {
            LoadUserRole();
            LoadFormList();
            PrepareGrid();
            LoadDataToGrid();
            ComboLoad();            
            btnDelete.Enabled = false;

        }      

        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Role Name");
            cmbSearch.Items.Insert(3, "Form Name");           
            cmbSearch.SelectedIndex = 0;

        }

        private void LoadUserRole()
        {

            var type = objBll.GetAllUserRole();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.RoleId, t.RoleName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Role --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlRole.DataSource = dt_Types;
                ddlRole.DisplayMember = "t_Name";
                ddlRole.ValueMember = "t_ID";
            }
            ddlRole.SelectedIndex = 0;

        }

        private void LoadFormList()
        {

            var type = objBll.GetAllFormList();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.FormId, t.FormName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Form --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlFormName.DataSource = dt_Types;
                ddlFormName.DisplayMember = "t_Name";
                ddlFormName.ValueMember = "t_ID";
            }
            ddlFormName.SelectedIndex = 0;

        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[1].HeaderText = "Role Name";

            dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[2].HeaderText = "Form Name";

            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[3].HeaderText = "View";

            dataGridView1.AllowUserToAddRows = false;

           // dataGridView1.ClearSelection();
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listPermission = objBll.GetAllUserPermission();         
            if (listPermission.Count > 0)
            {
                int index = 1;
                foreach (var item in listPermission)
                {
                    dataGridView1.Rows.Add(index, item.UserRole.RoleName, item.FormList.FormName, item.View==true?"True":"False" );
                    index = index + 1;
                }
                
            }          
            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<UserPermissionMapping> objlist)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            int index = 1;
            foreach (var item in objlist)
            {
                dataGridView1.Rows.Add(index, item.UserRole.RoleName, item.FormList.FormName, item.View == true ? "True" : "False");
                index = index + 1;
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
                          "Confirm Permission mapping deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objPermission.Id != 0)
                {

                    var status = objBll.Delete(objPermission.Id);
                    MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGrid();                   
                }

            }

            ClearForm();
        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                var status = objBll.Insert(objPermission);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.Update(objPermission);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private bool Validation()
        {
            var errMessage = "";

            if (Convert.ToInt32(ddlRole.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select role type !!\n";
            }
            if (Convert.ToInt32(ddlFormName.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select form type !!\n";
            }
            if (errMessage != "")
            {
                MessageBox.Show(errMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                objPermission.RoleId = Convert.ToInt32(ddlRole.SelectedValue);
                objPermission.FormId = Convert.ToInt32(ddlFormName.SelectedValue);
                if (chkView.Checked == true)
                {
                    objPermission.View = true;
                }
                if (chkSave.Checked == true)
                {
                    objPermission.Save = true;
                }
                if (chkEdit.Checked == true)
                {
                    objPermission.Edit = true;
                }
                if (chkDelete.Checked == true)
                {
                    objPermission.Delete = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
         
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            int slect = cmbSearch.SelectedIndex;
            string value = txtSearch.Text.ToString();

            if (slect == 0)
            {
                MessageBox.Show("Please select a search item !!");
                return;
            }
            else if (slect != 1 && value == "")
            {
                MessageBox.Show(" Search text can't be empty");
                return;
            }
            switch (slect)
            {
                case 1:
                    {

                        var filterobjlist = listPermission.ToList();
                        LoadFilterDataToGrid(filterobjlist);
                        break;
                    }
                case 2:
                    {

                        var filterobjlist = listPermission.Where(c => c.UserRole.RoleName.Contains(value)).ToList();
                        if (filterobjlist.Count > 0)
                        {
                            LoadFilterDataToGrid(filterobjlist);
                           
                        }
                        else
                        {
                            MessageBox.Show("No data found");
                        }
                        break;
                    }
                case 3:
                    {
                        var filterobjlist = listPermission.Where(c => c.FormList.FormName.Contains(value)).ToList();
                        if (filterobjlist.Count > 0)
                        {
                            LoadFilterDataToGrid(filterobjlist);

                        }
                        else
                        {
                            MessageBox.Show("No data found");
                        }
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
            objPermission = listPermission.ElementAt(index);
            ddlRole.SelectedValue = objPermission.RoleId;
            ddlFormName.SelectedValue = objPermission.FormId;

            chkView.Checked = objPermission.View == true ? true : false;
            chkSave.Checked = objPermission.Save == true ? true : false;
            chkEdit.Checked = objPermission.Edit == true ? true : false;
            chkDelete.Checked = objPermission.Delete == true ? true : false;

            btnSave.Text = "Update";
            btnDelete.Enabled = true;

        }    

        private void ClearForm()
        {
            ddlRole.SelectedIndex = 0;
            ddlFormName.SelectedIndex = 0;
            chkView.Checked = false;
            chkSave.Checked = false;
            chkEdit.Checked = false;
            chkDelete.Checked = false;            
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            btnDelete.Enabled = false;
            objPermission = new UserPermissionMapping();
            dataGridView1.ClearSelection();
            btnSave.Text = "Save";


        }

       
    }
 
}
