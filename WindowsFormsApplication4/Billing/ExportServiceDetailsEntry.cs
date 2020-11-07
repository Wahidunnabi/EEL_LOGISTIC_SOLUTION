using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace LOGISTIC.UI.Administration
{
    public partial class ExportServiceDetailsEntry : Form
    {

        private List<Customer> lstMLO = new List<Customer>();
        private List<ContainerSize> lstSize = new List<ContainerSize>();
        private List<Service> lstServices = new List<Service>();
        private List<ExportServiceDetail> lstServiceDtls = new List<ExportServiceDetail>();
        private ExportServiceDetail objServiceDtls = new ExportServiceDetail();
        private CustomerBll MLOBll = new CustomerBll();
        private ContainerSizeBll objSizeBll = new ContainerSizeBll();
        private BillingBLL objBll = new BillingBLL();
     
        public ExportServiceDetailsEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            txtRateTk.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRateTk.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

        }
     
        private void ExportServiceDetailsEntry_Load(object sender, EventArgs e)
        {                       
            LoadMLO();
            LoadServices();
            LoadSize();
            PrepareGrid();
            LoadDataToGrid();           
            btnDelete.Enabled = false;

        }

       

        private void LoadMLO()
        {

            lstMLO = MLOBll.Getall();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in lstMLO)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select MLO--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlMLO.DataSource = dt_Types;
                ddlMLO.DisplayMember = "t_Name";
                ddlMLO.ValueMember = "t_ID";
            }
            ddlMLO.SelectedIndex = 0;


        }

        private void LoadServices()
        {

            lstServices = objBll.GetAllExportServices();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in lstServices)
            {
                dt_Types.Rows.Add(t.ID, t.ServiceName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Service --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlServiceName.DataSource = dt_Types;
                ddlServiceName.DisplayMember = "t_Name";
                ddlServiceName.ValueMember = "t_ID";
            }
            ddlServiceName.SelectedIndex = 0;

        }

        private void LoadSize()
        {

            lstSize = objSizeBll.Getall();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in lstSize)
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
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[1].HeaderText = "MLO Code";

            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[2].HeaderText = "Service Name";

            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[3].HeaderText = "Size";

            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[4].HeaderText = "Rate";       

            dataGridView1.AllowUserToAddRows = false;

           
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            lstServiceDtls = objBll.GetAllExportServiceDetail();         
            if (lstServiceDtls.Count > 0)
            {
                int index = 1;
                foreach (var item in lstServiceDtls)
                {
                    dataGridView1.Rows.Add(index, lstMLO.Where(c => c.CustomerId == item.CustomerId).FirstOrDefault().CustomerCode, lstServices.Where(s=>s.ID== item.ServiceId).FirstOrDefault().ServiceName, lstSize.Where(x=>x.ContainerSizeId== item.SizeId).First().ContainerSize1, item.Rate);
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
                          "Confirm Bill Details deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objServiceDtls.ServiceDetailsId != 0)
                {

                    var status = objBll.DeleteExportServiceDetail(objServiceDtls.ServiceDetailsId);
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
            objServiceDtls = lstServiceDtls.ElementAt(index);

            ddlMLO.SelectedValue = objServiceDtls.CustomerId;
            ddlServiceName.SelectedValue = objServiceDtls.ServiceId;
            ddlContSize.SelectedValue = Convert.ToInt32(objServiceDtls.SizeId);
            txtRateTk.Text = Convert.ToString(objServiceDtls.Rate); 
           
            ddlServiceName.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Text = "Update";

        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                var status = objBll.InsertExportServiceDetail(objServiceDtls);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.UpdateExportServiceDetail(objServiceDtls);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cancel();

            }

        }

        private bool Validation()
        {
            var errMessage = "";

            if (Convert.ToInt16(ddlMLO.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a MLO !!\n";
            }
            if (Convert.ToInt16(ddlServiceName.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a service !!\n";
            }
            if (txtRateTk.Text.Trim() == string.Empty)
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
                objServiceDtls.CustomerId = Convert.ToInt32(ddlMLO.SelectedValue);
                objServiceDtls.ServiceId = Convert.ToInt32(ddlServiceName.SelectedValue);
                objServiceDtls.SizeId = Convert.ToInt32(ddlContSize.SelectedValue);
                objServiceDtls.Rate =  Convert.ToDecimal(txtRateTk.Text.Trim());               
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
            btnDelete.Enabled = false;
            objServiceDtls = new ExportServiceDetail();
            dataGridView1.ClearSelection();
            labelControl2.Focus();
        }
        private void Cancel()
        {

            ddlMLO.SelectedIndex = 0;
            ddlServiceName.SelectedIndex = 0;       
            ddlContSize.SelectedIndex = 0;
            txtRateTk.Text = "";          
            btnSave.Text = "Save";           
            btnDelete.Enabled = false;
            objServiceDtls = new ExportServiceDetail();
            dataGridView1.ClearSelection();
            labelControl2.Focus();

        }

        private void ServiceDetailsEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            objServiceDtls = new ExportServiceDetail();
        }

       
    }
 
}
