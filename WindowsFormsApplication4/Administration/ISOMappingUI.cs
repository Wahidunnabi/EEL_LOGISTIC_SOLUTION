using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace LOGISTIC.UI.Administration
{
    public partial class ISOMappingUI : Form
    {
        private List<ISOMapping> listISO = new List<ISOMapping>();
        private ISOMapping objISO = new ISOMapping();

        private ContainerSizeBll sizeBll = new ContainerSizeBll();
        private ContainerTypeBll typeBll = new ContainerTypeBll();
        private ISOMappingBLL objBll = new ISOMappingBLL();
       
        public ISOMappingUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
           
        }

      
        private void ISOMappingUI_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            LoadContSize();
            LoadContType();
            PrepareGrid();
            LoadDataToGrid();

        }


        private void LoadContSize()
        {


            var type = sizeBll.Getall();
           
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ContainerSizeId, t.ContainerSize1);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Container Size--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlSize.DataSource = dt_Types;
                ddlSize.DisplayMember = "t_Name";
                ddlSize.ValueMember = "t_ID";
            }
            ddlSize.SelectedIndex = 0;


        }

        private void LoadContType()
        {

            var type = typeBll.Getall();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ContainerTypeId, t.ContainerTypeName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Container Size--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlType.DataSource = dt_Types;
                ddlType.DisplayMember = "t_Name";
                ddlType.ValueMember = "t_ID";
            }
            ddlType.SelectedIndex = 0;


        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[1].HeaderText = "Size";

            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[2].HeaderText = "Type";

            dataGridView1.Columns[3].HeaderText = "ISO Code";

            dataGridView1.AllowUserToAddRows = false;

        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listISO = objBll.Getall();
            if (listISO.Count > 0)
            {
                int index = 1;
                foreach (var item in listISO)
                {
                    dataGridView1.Rows.Add(index, item.ContainerSize.ContainerSize1, item.ContainerType.ContainerTypeName, item.ISOCode);
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
                           "Confirm Container Size deletion",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objISO.ID > 0)
                {

                    var status = objBll.Delete(objISO.ID);                 
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
            
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var index = Convert.ToInt32(selectedRow.Index);
            objISO = listISO.ElementAt(index);


            ddlSize.SelectedValue = objISO.SizeId;
            ddlType.SelectedValue = objISO.TypeId;
            txtISOCode.Text = objISO.ISOCode;

            btnSave.Text = "Update";
            btnDelete.Enabled = true;


        }

        private bool Validation()
        {
            var errMessage = "";

            if (Convert.ToInt32(ddlSize.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select container size !!\n";
                ddlSize.Focus();
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select container type !!\n";
                ddlType.Focus();
            }
            if (txtISOCode.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter ISO code !!\n";
                txtISOCode.Focus();
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
            objISO.SizeId = Convert.ToInt32(ddlSize.SelectedValue);
            objISO.TypeId = Convert.ToInt32(ddlType.SelectedValue);
            objISO.ISOCode = txtISOCode.Text.Trim();

        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                var status = objBll.Insert(objISO);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();

            }
            else
            {
                var status = objBll.Update(objISO);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }

        }

        private void ClearForm()
        {            
            txtISOCode.Text = "";
            ddlSize.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            objISO = new ISOMapping();
            dataGridView1.ClearSelection();
            labelControl1.Focus();
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
    



}
