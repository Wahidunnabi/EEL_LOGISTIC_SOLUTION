using System;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using LOGISTIC.BLL;
using System.Windows.Forms;
using System.Linq;

namespace LOGISTIC.UI.Billing
{
    public partial class BillOptionsUI : Form
    {

        ClientBillSetup objBillSetup = new ClientBillSetup();
        List<ClientBillSetup> listBillSetup = new List<ClientBillSetup>();
       
        private ChargeSetupBLL objBll = new ChargeSetupBLL();
        private CustomerBll MLOBll = new CustomerBll();
        private ContainerSizeBll csBll = new ContainerSizeBll();
        private TrailerBll objTrailerBll = new TrailerBll();

        public static int serviceId = 0;

        public BillOptionsUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            btnDelete.Enabled = false;
        }

        private void BillOptionsUI_Load(object sender, EventArgs e)
        {
            objBillSetup = new ClientBillSetup();
            LoadTree();
            LoadCustomer();
            LoadContSize();
            LoadTrailerAccount();
            LoadLocation();
            LoadTarrif();
            PrepareGrid();
            SetMask();
        }

        #region Load Basic Data
        public void LoadTree()
        {

            // String connectionString;
            // connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlConnection  conn = new SqlConnection(connectionString);
            // String Sequel = "select * from ChartOfService where IsTransaction=0";
            // SqlDataAdapter da = new SqlDataAdapter(Sequel, conn);
            // DataTable dt = new DataTable();
            // conn.Open();
            // da.Fill(dt);
            var listCharge = objBll.GetallParent();
            foreach (var item in listCharge)
            {
                //TreeNode parentNode = treeService.Nodes.Add(dr["ServiceId"].ToString(), dr["ServiceName"].ToString());
                //parentNode.ForeColor = Color.Green;
                //PopulateTreeView(Convert.ToInt32(dr["ServiceId"].ToString()), parentNode);

                TreeNode parentNode = treeService.Nodes.Add(item.ServiceId.ToString(), item.ServiceName.ToString());
                parentNode.ForeColor = Color.Green;
                PopulateTreeView(Convert.ToInt32(item.ServiceId), parentNode);

            }
            treeService.ExpandAll();

        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            //String connectionString;
            //connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlConnection conn = new SqlConnection(connectionString);
            //String Seqchildc = "select * from ChartOfService WHERE ParentId=" + parentId + "";
            //SqlDataAdapter dachildmnuc = new SqlDataAdapter(Seqchildc, conn);
            //DataTable dtchildc = new DataTable();
            //dachildmnuc.Fill(dtchildc);
            //foreach (DataRow dr in dtchildc.Rows)
                var listCharge = objBll.GetallChildByParentId(parentId);

            TreeNode childNode;
            foreach (var item in listCharge)
            {
                //TreeNode childNode = new TreeNode();
                //childNode.Text = dr["ServiceName"].ToString();
                //childNode.no = dr["ServiceName"].ToString();
                // parentNode.Nodes.
                //childNode = parentNode.Nodes.Add(dr["ServiceName"].ToString());

                childNode = parentNode.Nodes.Add(item.ServiceId.ToString(), item.ServiceName.ToString());

            }
        }


        private void LoadCustomer()
        {

            var type = MLOBll.Getall();          
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Customer Code--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlClient.DataSource = dt_Types;
                ddlClient.DisplayMember = "t_Name";
                ddlClient.ValueMember = "t_ID";
            }
            ddlClient.SelectedIndex = 0;

        }


        private void LoadContSize()
        {


            var type = csBll.Getall();          
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


        private void LoadTrailerAccount()
        {


            var type = objTrailerBll.Getall();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.TrailerId, t.TrailerName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Trailer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddltrailer.DataSource = dt_Types;
                ddltrailer.DisplayMember = "t_Name";
                ddltrailer.ValueMember = "t_ID";
            }


            ddltrailer.SelectedIndex = 0;

        }


        private void LoadLocation()
        {

            ddlLocation.Items.Insert(0, "-- Select location --");
            ddlLocation.Items.Insert(1, "CFS");
            ddlLocation.Items.Insert(2, "YRD");
            ddlLocation.SelectedIndex = 0;


        }


        private void LoadTarrif()
        {

            ddlTarrif.Items.Insert(0, "-- Select Tariff --");
            ddlTarrif.Items.Insert(1, "Tariff 1");
            ddlTarrif.Items.Insert(2, "Tariff 2");

            ddlTarrif.SelectedIndex = 0;

        }


        public void SetMask()
        {

            txtAmend.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtAmend.Properties.Mask.EditMask = "\\d+";          

            txtAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtAmount.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

 
        }


        #endregion




        #region CRUD Operation


        private void ddlClient_SelectionChangeCommitted_1(object sender, EventArgs e)
        {

            if (ddlClient.SelectedIndex > 0)
            {
                var CustId = Convert.ToInt32(ddlClient.SelectedValue);
                txtClient.Text = objBll.GetClientNameById(CustId);


            }
            else
            {
                txtClient.Text = "";
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (btnSave.Text == "Save")
            {
                bool flag = ValidateBillSetup();
                if (flag == true)
                {

                    FillingChargeSetup();
                    var status = objBll.Insert(objBillSetup);
                    MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);                  
                    ResetBillSetup();                   
                    ReloadGrid();

                }
            }
            else if (btnSave.Text == "Update")
            {
                bool flag = ValidateBillSetup();
                if (flag == true)
                {

                    FillingChargeSetup();
                    var status = objBll.Update(objBillSetup);
                    MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetBillSetup();
                    ReloadGrid();
                }
               // ClearBillSetup();
            
            }



        }


        private void btnDelete_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Cargo Receiving deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                var status = objBll.Delete(objBillSetup.BillSetupId);
                MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                ResetBillSetup();
                ReloadGrid();


            }

        }

       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearBillSetup();
            ClearGrid();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ClearBillSetup();
            Close();

        }     


        private bool ValidateBillSetup()
        {

            var errMessage = "";

            if (Convert.ToInt32(ddlClient.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a Client !!\n";
            }
            if (Convert.ToInt32(ddlSize.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select container size !!\n";
            }            
            if (txtAmount.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter charge rate !!\n";
            }
            if (serviceId == 0)
            {
                errMessage = errMessage + "* Please select service type !!\n";
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


        private void FillingChargeSetup()
        {

            objBillSetup.ServiceId = serviceId;
            objBillSetup.CustId = Convert.ToInt32(ddlClient.SelectedValue);

            var amend =txtAmend.Text.Trim();
            if (amend != "")
            {
                objBillSetup.Amend = Convert.ToInt32(txtAmend.Text.Trim());

            }           
            objBillSetup.SizeId = Convert.ToInt32(ddlSize.SelectedValue);
            objBillSetup.Rate = Convert.ToDecimal(txtAmount.Text.Trim());
            objBillSetup.TrailerId = Convert.ToInt32(ddltrailer.SelectedValue);
            objBillSetup.Location = Convert.ToInt32(ddlLocation.SelectedIndex);
            objBillSetup.Tariff = Convert.ToInt32(ddlTarrif.SelectedIndex);
            objBillSetup.Entrydate = Convert.ToDateTime(dateEntry.Value);
           
        }      


        #endregion



        private void treeService_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var key = treeService.SelectedNode.Name.ToString();
            var text = treeService.SelectedNode.Text.ToString();
            serviceId = Convert.ToInt32(key);           

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlClient.SelectedValue) > 0)
            {
                listBillSetup = objBll.GetAllBillSetupByClientId(Convert.ToInt32(ddlClient.SelectedValue));

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                if (listBillSetup.Count > 0)
                {
                    // PrepareGrid();
                    BindDataToGrid();

                }
                else
                {
                    MessageBox.Show("No data found !!", "Data Search Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }


        public void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].HeaderText = "Service Name";

            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "Size";

            //dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[3].HeaderText = "Rate";

            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Entry Date";

            

        }



        private void BindDataToGrid()
        {

            int SlNo = 1;
            foreach (var item in listBillSetup)
            {
                dataGridView1.Rows.Add(SlNo, item.ChartOfService.ServiceName, item.ContainerSize.ContainerSize1, item.Rate, item.Entrydate);
                SlNo = SlNo + 1;
            }
                     
            dataGridView1.ClearSelection();
           

        }


        private void ReloadGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listBillSetup = objBll.GetAllBillSetupByClientId(Convert.ToInt32(ddlClient.SelectedValue));
            int SlNo = 1;
            foreach (var item in listBillSetup)
            {
                dataGridView1.Rows.Add(SlNo, item.ChartOfService.ServiceName, item.ContainerSize.ContainerSize1, item.Rate, item.Entrydate);
                SlNo = SlNo + 1;
            }

            dataGridView1.ClearSelection();
            dataGridView1.AllowUserToAddRows = false;

        }

        
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var index = Convert.ToInt32(selectedRow.Index);
            objBillSetup = listBillSetup.ElementAt(index);

            ddlClient.SelectedValue = objBillSetup.CustId;
            txtAmend.Text = Convert.ToString(objBillSetup.Amend);
            ddlSize.SelectedValue = objBillSetup.SizeId;
            txtAmount.Text = Convert.ToString(objBillSetup.Rate);
            ddltrailer.SelectedValue = objBillSetup.TrailerId;
            ddlLocation.SelectedIndex = Convert.ToInt32(objBillSetup.Location);
            ddlTarrif.SelectedIndex = Convert.ToInt32(objBillSetup.Tariff);
            dateEntry.Value = Convert.ToDateTime(objBillSetup.Entrydate);

            btnSave.Text = "Update";
            btnDelete.Enabled = true;

        }


        private void ClearGrid()
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();


        }

        private void ResetBillSetup()
        {

            txtAmend.Text = "";
            ddlSize.SelectedIndex = 0;
            txtAmount.Text = "";
            ddltrailer.SelectedIndex = 0;
            ddlLocation.SelectedIndex = 0;
            ddlTarrif.SelectedIndex = 0;
            dateEntry.Value = DateTime.Now;
            serviceId = 0;
            objBillSetup = new ClientBillSetup();
            //listBillSetup = new List<ClientBillSetup>();
            treeService.SelectedNode = null;
            btnSave.Text = "Save";
            btnDelete.Enabled = false;

        }

        private void ClearBillSetup()
        {

            ddlClient.SelectedIndex = 0;
            txtClient.Text = "";
            txtAmend.Text = "";
            ddlSize.SelectedIndex = 0;
            txtAmount.Text = "";
            ddltrailer.SelectedIndex = 0;
            ddlLocation.SelectedIndex = 0;
            ddlTarrif.SelectedIndex = 0;
            dateEntry.Value = DateTime.Now;
            serviceId = 0;

            objBillSetup = new ClientBillSetup();
            listBillSetup = new List<ClientBillSetup>();

            btnSave.Text = "Save";
            btnDelete.Enabled = false;



        }

    }
    }

