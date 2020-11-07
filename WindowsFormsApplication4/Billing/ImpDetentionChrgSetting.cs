using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace LOGISTIC.UI.Administration
{
    public partial class ImpDetentionChrgSetting : Form
    {      
        private List<Slab> lstSlab = new List<Slab>();
        private List<ContainerSize> lstSize = new List<ContainerSize>();

        private List<ImpDetentionCharg> lstDetentionChrg = new List<ImpDetentionCharg>();     
        private ImpDetentionCharg objDetentionChrg = new ImpDetentionCharg();

        private ContainerSizeBll objSizeBll = new ContainerSizeBll();
        private BillingBLL objBll = new BillingBLL();
     
        public ImpDetentionChrgSetting()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            txtRateTk.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRateTk.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtRateDllr.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRateDllr.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,1})?";

        }

        private void ImpDetentionChrgSetting_Load(object sender, EventArgs e)
        {
            lstDetentionChrg = objBll.GetAllDetentionCharg();
            lstSlab = objBll.GetAllSlab();
            lstSize = objSizeBll.Getall();
            LoadSlab();
            LoadSize();
            PrepareGrid();
            LoadDataToGrid();           
            btnDelete.Enabled = false;
        }     

        private void LoadSlab()
        {
                   
            var type = lstSlab;
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.SlabId, t.SlabName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Slab --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlSlab.DataSource = dt_Types;
                ddlSlab.DisplayMember = "t_Name";
                ddlSlab.ValueMember = "t_ID";
            }
            ddlSlab.SelectedIndex = 0;

        }


        private void LoadSize()
        {

            var type = lstSize;
           
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ContainerSizeId, t.ContainerSize1);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Size --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlContSize.DataSource = dt_Types;
                ddlContSize.DisplayMember = "t_Name";
                ddlContSize.ValueMember = "t_ID";
            }
            ddlContSize.SelectedIndex = 0;


        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 6;

            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[1].HeaderText = "Slab Name";

            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[2].HeaderText = "Slab Days";

            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[3].HeaderText = "Total Days";

            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[4].HeaderText = "Size";          

            dataGridView1.Columns[5].Width = 85;
            dataGridView1.Columns[5].HeaderText = "Rate(dollar)";

            dataGridView1.AllowUserToAddRows = false;

           
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            lstDetentionChrg = objBll.GetAllDetentionCharg();         
            if (lstDetentionChrg.Count > 0)
            {
                int index = 1;
                foreach (var item in lstDetentionChrg)
                {
                    dataGridView1.Rows.Add(index, lstSlab.Where(x=>x.SlabId==item.SlabId).First().SlabName, item.SlabDays, item.TotalDays, lstSize.Where(x=>x.ContainerSizeId== item.SizeId).First().ContainerSize1, item.RateInDlr );
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
               // Cancel();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Detention Charg deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objDetentionChrg.ID != 0)
                {

                    var status = objBll.DeleteDetentionCharg(objDetentionChrg.ID);
                    MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGrid();
                   
                }
            }
            Cancel();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
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
            objDetentionChrg = lstDetentionChrg.ElementAt(index);           

            ddlSlab.SelectedValue = objDetentionChrg.SlabId;
            ddlContSize.SelectedValue = Convert.ToInt32(objDetentionChrg.SizeId);
            txtSlubDay.Text = Convert.ToString(objDetentionChrg.SlabDays);
            txtTotalDays.Text = Convert.ToString(objDetentionChrg.TotalDays);
            txtRateTk.Text = Convert.ToString(objDetentionChrg.RateInTk); 
            txtRateDllr.Text = Convert.ToString(objDetentionChrg.RateInDlr);
          
            btnDelete.Enabled = true;
            btnSave.Text = "Update";

        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                var status = objBll.InsertDetentionCharg(objDetentionChrg);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.UpdateDetentionCharg(objDetentionChrg);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cancel();

            }

        }

        private bool Validation()
        {
            var errMessage = "";

            if (Convert.ToInt16(ddlSlab.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a service !!\n";
            }
            //if (txtDay.Text.Trim() == string.Empty)
            //{
            //    errMessage = errMessage + "* Slab days can not be null !!\n";
            //}
            if (txtRateDllr.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Service rate can not be null !!\n";
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
                objDetentionChrg.SlabId = Convert.ToInt32(ddlSlab.SelectedValue);
                objDetentionChrg.SizeId = Convert.ToInt32(ddlContSize.SelectedValue);
                objDetentionChrg.SlabDays = txtSlubDay.Text.Trim() != string.Empty ? Convert.ToInt32(txtSlubDay.Text.Trim()):0;
                objDetentionChrg.TotalDays = txtTotalDays.Text.Trim() != string.Empty ? Convert.ToInt32(txtTotalDays.Text.Trim()) : 0;
                objDetentionChrg.RateInTk = txtRateTk.Text.Trim() != string.Empty ? Convert.ToInt32(txtRateTk.Text.Trim()):0;
                objDetentionChrg.RateInDlr = txtRateDllr.Text.Trim() != string.Empty ? Convert.ToDouble(txtRateDllr.Text.Trim()): 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void ResetForm()
        {
          
            ddlContSize.SelectedIndex = 0;
            txtRateTk.Text = "";
            txtRateDllr.Text = "";          
            btnDelete.Enabled = false;
            objDetentionChrg = new ImpDetentionCharg();
            dataGridView1.ClearSelection();
            labelControl2.Focus();
        }
        private void Cancel()
        {
                     
            ddlSlab.SelectedIndex = 0;
            ddlContSize.SelectedIndex = 0;
            txtSlubDay.Text = "";
            txtRateTk.Text = "";
            txtRateDllr.Text = "";
            btnSave.Text = "Save";           
            btnDelete.Enabled = false;
            objDetentionChrg = new ImpDetentionCharg();
            dataGridView1.ClearSelection();
            labelControl2.Focus();

        }

        
    }
 
}
