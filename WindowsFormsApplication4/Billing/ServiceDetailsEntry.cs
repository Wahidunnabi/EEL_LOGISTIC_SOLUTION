using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace LOGISTIC.UI.Administration
{
    public partial class ServiceDetailsEntry : Form
    {

        private List<ContainerSize> lstSize = new List<ContainerSize>();
        private List<Service> lstServices = new List<Service>();
        private List<ServiceDetail> lstServiceDtls = new List<ServiceDetail>();
        private ServiceDetail objServiceDtls = new ServiceDetail();
        private ContainerSizeBll objSizeBll = new ContainerSizeBll();
        private BillingBLL objBll = new BillingBLL();
        private ClearAndForwaderBll CandFBll = new ClearAndForwaderBll();
        private ImporterBll importerBll = new ImporterBll();
        public ServiceDetailsEntry()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            txtRateTk.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRateTk.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,4})?";

            txtRateDllr.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRateDllr.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,4})?";

        }

       
        private void LoadImporter()
        {

            var type = importerBll.Getall();
            cmbImporter.ValueMember = "ImporterId";
            cmbImporter.DisplayMember = "ImporterName";
            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.ImporterId, t.ImporterName);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Importer--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                cmbImporter.DataSource = dt;
                cmbImporter.DisplayMember = "t_Name";
                cmbImporter.ValueMember = "t_ID";
            }
            cmbImporter.SelectedIndex = 0;

        }
        private void ServiceDetailsEntry_Load(object sender, EventArgs e)
        {
            lstServices = objBll.GetAllService();
            lstSize = objSizeBll.Getall();
            // LoadServices(lstServices.Where(x => x.ServiceCategory == 1).OrderBy(x => x.ServiceName).ToList());
            LoadSize();
            PrepareGrid();
            LoadDataToGrid();
            ddlServiceName.Enabled = false;
            btnDelete.Enabled = false;
            LoadImporter();
            LoadCandFAgent();

        }
        private void LoadCandFAgent()
        {

            var type = CandFBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ClearAndForwadingAgentId, t.CFAgentName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select C&F Agent --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlCFAgent.DataSource = dt_Types;
                ddlCFAgent.DisplayMember = "t_Name";
                ddlCFAgent.ValueMember = "t_ID";
            }
            ddlCFAgent.SelectedIndex = 0;

        }
        private void LoadServices(List<Service> listService)
        {

            ddlServiceName.DataSource = null;          
            var type = listService;
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
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
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 170;
            dataGridView1.Columns[1].HeaderText = "Service Name";

            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "Size";

            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[3].HeaderText = "Rate(tk.)";

            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[4].HeaderText = "Rate(dlr.)";

            dataGridView1.AllowUserToAddRows = false;

           
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            lstServiceDtls = objBll.GetAllServiceDetails();         
            if (lstServiceDtls.Count > 0)
            {
                int index = 1;
                foreach (var item in lstServiceDtls)
                {
                    dataGridView1.Rows.Add(index, item.Service.ServiceName, item.ContSizeId>0? lstSize.Where(x=>x.ContainerSizeId== item.ContSizeId).First().ContainerSize1.ToString():"", item.RateTk, item.RateDollar );
                    index = index + 1;
                }
                
            }
                      
            dataGridView1.ClearSelection();
        }


        private void rdoImport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoImport.Checked == true)
            {
                LoadServices(lstServices.Where(x => x.ServiceCategory == 1).OrderBy(x => x.ServiceName).ToList());
                ddlServiceName.Enabled = true;
                ddlContSize.SelectedIndex = 0;
                txtRateDllr.Text = "";
                txtRateTk.Text = "";
            }
        }

        private void rdoCSD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCSD.Checked == true)
            {
                LoadServices(lstServices.Where(x => x.ServiceCategory == 2).OrderBy(x => x.ServiceName).ToList());
                ddlServiceName.Enabled = true;
                ddlContSize.SelectedIndex = 0;
                txtRateDllr.Text = "";
                txtRateTk.Text = "";
            }

        }

        private void rdoExport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoExport.Checked == true)
            {
                LoadServices(lstServices.Where(x => x.ServiceCategory == 3).OrderBy(x => x.ServiceName).ToList());
                ddlServiceName.Enabled = true;
                ddlContSize.SelectedIndex = 0;
                txtRateDllr.Text = "";
                txtRateTk.Text = "";
            }
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
                          "Confirm Trailer Number deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objServiceDtls.ServiceDetailsId != 0)
                {

                    var status = objBll.DeleteServiceDetail(objServiceDtls.ServiceDetailsId);
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

            if (objServiceDtls.Service.ServiceCategory == 1)
            {
                rdoImport.Checked = true;               
            }
            if (objServiceDtls.Service.ServiceCategory == 2)
            {
                rdoCSD.Checked = true;
            }
            if (objServiceDtls.Service.ServiceCategory == 3)
            {
                rdoExport.Checked = true;                
            }

            ddlServiceName.SelectedValue = objServiceDtls.ServiceId;
            ddlContSize.SelectedValue = Convert.ToInt32(objServiceDtls.ContSizeId);
            txtRateTk.Text = Convert.ToString(objServiceDtls.RateTk); 
            txtRateDllr.Text = Convert.ToString(objServiceDtls.RateDollar);
            ddlServiceName.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Text = "Update";

        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                var status = objBll.InsertServiceDetail(objServiceDtls);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            else if (btnSave.Text == "Update")
            {

                var status = objBll.UpdateServiceDetail(objServiceDtls);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cancel();

            }

        }

        private bool Validation()
        {
            var errMessage = "";

            if (Convert.ToInt16(ddlServiceName.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a service !!\n";
            }
            if (txtRateTk.Text.Trim() == string.Empty && txtRateDllr.Text.Trim() == string.Empty)
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
                objServiceDtls.ServiceId = Convert.ToInt32(ddlServiceName.SelectedValue);
                objServiceDtls.ContSizeId = Convert.ToInt32(ddlContSize.SelectedValue);
                objServiceDtls.RateTk = txtRateTk.Text.Trim() != string.Empty ? Convert.ToDouble(txtRateTk.Text.Trim()):0.00;
                objServiceDtls.RateDollar = txtRateDllr.Text.Trim() != string.Empty ? Convert.ToDouble(txtRateDllr.Text.Trim()):0.00;
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
            objServiceDtls = new ServiceDetail();
            dataGridView1.ClearSelection();
            labelControl2.Focus();
        }
        private void Cancel()
        {
            rdoImport.Checked = false;
            rdoCSD.Checked = false;
            rdoExport.Checked = false;
            ddlServiceName.DataSource = null;
            ddlServiceName.Enabled = false;
            ddlContSize.SelectedIndex = 0;
            txtRateTk.Text = "";
            txtRateDllr.Text = "";
            btnSave.Text = "Save";           
            btnDelete.Enabled = false;
            objServiceDtls = new ServiceDetail();
            dataGridView1.ClearSelection();
            labelControl2.Focus();

        }

        private void ServiceDetailsEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            objServiceDtls = new ServiceDetail();
        }

        
    }
 
}
