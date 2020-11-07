using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.IO;
using System.Data.OleDb;

namespace LOGISTIC.UI.Import
{
    public partial class IGMInput : Form
    {
       

        IGMImport objIGMImport = new IGMImport();

        private IGMImportBLL IGMImportBll = new IGMImportBLL();
        private CustomerBll customerBll = new CustomerBll();
        private ImporterBll importerBll = new ImporterBll();
        private VesselBll vesselBll = new VesselBll();
        private CommodityBLL commodityBll = new CommodityBLL();
        private UnitofMeasureBll unitBll = new UnitofMeasureBll();
        private PortBLL portBll = new PortBLL();
        private ContainerTypeBll conTypeBll = new ContainerTypeBll();
        private ContainerSizeBll contSizeBll = new ContainerSizeBll();

        private OpenFileDialog opd = new OpenFileDialog();

        private List<Customer> objMLolist = new List<Customer>();
        private List<ContainerSize> objSizelist = new List<ContainerSize>();
        private List<ContainerType> objTypelist = new List<ContainerType>();
      
        private static int IGMContEntrySL = 1;
        private static int index;

      
      
        //public int totalRow = GateEntryBLL.GetTotalRow();

        public IGMInput()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);         
            txtContainerNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtContainerNo.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";

            txtNumofPkg.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtNumofPkg.Properties.Mask.EditMask = "\\d+";

            txtWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtWeight.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtBoxQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtBoxQuantity.Properties.Mask.EditMask = "\\d+";

        }

        public IGMInput(IGMImport objIGM)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);                       
            txtContainerNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtContainerNo.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";
            objIGMImport = objIGM;
            btnIGMSave.Text = "Update";

        }

        private void IGMInput_Load(object sender, EventArgs e)
        {
            objMLolist = customerBll.Getall();
            objTypelist = conTypeBll.Getall();
            objSizelist = contSizeBll.Getall();
                  
            LoadCustomer();
            LoadImporter();
            LoadVessel();
            LoadCommodity();
            LoadMeasurementUnit();
            LoadPortofDischarge();
            LoadPlaceofDelivery();
            LoadContainerType();
            LoadContainerSize();


            if (objIGMImport.IGMImportId > 0)
            {
                ShowIGMData(objIGMImport);

                objIGMImport = IGMImportBll.GetIGMDetailsByIGMId(objIGMImport.IGMImportId);

                if (objIGMImport.IGMImportDetails.Count > 0)
                {

                    PrepareGrid();
                    ClearGrid();
                    List<IGMImportDetail> listIGMDetails = IGMImportBll.GetAllIGMImportDetailbyIGMId(objIGMImport.IGMImportId);
                    int index = 0;
                    foreach (IGMImportDetail item in listIGMDetails)
                    {
                        dataGridView1.Rows.Add(IGMContEntrySL, item.ContainerNo, item.ContainerSize.ContainerSize1, item.ContainerType.ContainerTypeName, item.SealNo);
                        if (item.InOutStatus == 1)
                        {
                            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Khaki;
                        }
                        else if (item.InOutStatus == 2)
                        {
                            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Goldenrod;
                        }
                        index = index + 1;
                        IGMContEntrySL = IGMContEntrySL + 1;
                    }
                    dataGridView1.ClearSelection();
                    dataGridView1.AllowUserToAddRows = false;
                }

            }
            else
            {
                LoadAutoSuggestedBL();
                btnDelete.Enabled = false;
            }

            btnIGMContDelete.Enabled = false;

        }
              
        
       
        #region load form basic data

        private void LoadCustomer()
        {

            var type = objMLolist;
            ddlMLO.DisplayMember = "CustomerCode";
            ddlMLO.ValueMember = "CustomerId";
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode.Trim());
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

        private void LoadImporter()
        {

            var type = importerBll.Getall();
            ddlImporter.ValueMember = "ImporterId";
            ddlImporter.DisplayMember = "ImporterName";
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
                ddlImporter.DataSource = dt;
                ddlImporter.DisplayMember = "t_Name";
                ddlImporter.ValueMember = "t_ID";
            }
            ddlImporter.SelectedIndex = 0;

        }

        private void LoadVessel()
        {

            var type = vesselBll.Getall();
            ddlVessel.ValueMember = "VesselId";
            ddlVessel.DisplayMember = "VesselName";

            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.VesselId, t.VesselName);
            }
            DataRow dr = dt.NewRow();
            dr[0] = -1;
            dr[1] = "--Select Vessel--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                ddlVessel.DataSource = dt;
                ddlVessel.DisplayMember = "t_Name";
                ddlVessel.ValueMember = "t_ID";
            }
            ddlVessel.SelectedIndex = 0;

        }

        private void LoadCommodity()
        {

            var type = commodityBll.Getall();
            ddlCommodity.ValueMember = "CommodityId";
            ddlCommodity.DisplayMember = "CommodityName";

            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.CommodityId, t.CommodityName);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Commodity Type--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                ddlCommodity.DataSource = dt;
                ddlCommodity.DisplayMember = "t_Name";
                ddlCommodity.ValueMember = "t_ID";
            }
            ddlCommodity.SelectedIndex = 0;

        }

        private void LoadMeasurementUnit()
        {

            var type = unitBll.Getall();
            ddlMeasureUnit.ValueMember = "UnitOfMeasureId";
            ddlMeasureUnit.DisplayMember = "UnitOfMeasureName";

            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.UnitOfMeasureId, t.UnitOfMeasureName);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Unit--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                ddlMeasureUnit.DataSource = dt;
                ddlMeasureUnit.DisplayMember = "t_Name";
                ddlMeasureUnit.ValueMember = "t_ID";
            }
            ddlMeasureUnit.SelectedIndex = 0;

        }

        private void LoadPortofDischarge()
        {

            var type = portBll.Getall();

            ddlPortofDischarge.ValueMember = "PortOfLandId";
            ddlPortofDischarge.DisplayMember = "PortName";


            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.PortOfLandId, t.PortName);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Port Name--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                ddlPortofDischarge.DataSource = dt;

                ddlPortofDischarge.DisplayMember = "t_Name";
                ddlPortofDischarge.ValueMember = "t_ID";

            }

            ddlPortofDischarge.SelectedIndex = 0;

        }

        private void LoadPlaceofDelivery()
        {

            var type = portBll.Getall();

            ddlPlaceofDelivery.ValueMember = "PortOfLandId";
            ddlPlaceofDelivery.DisplayMember = "PortName";

            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.PortOfLandId, t.PortName);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Port Name--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {

                ddlPlaceofDelivery.DataSource = dt;

                ddlPlaceofDelivery.DisplayMember = "t_Name";
                ddlPlaceofDelivery.ValueMember = "t_ID";
            }

            ddlPlaceofDelivery.SelectedIndex = 0;

        }

        private void LoadContainerType()
        {

            var type = objTypelist;
            ddlConType.ValueMember = "ContainerTypeId";
            ddlConType.DisplayMember = "ContainerTypeName";

            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.ContainerTypeId, t.ContainerTypeName);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Type--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                ddlConType.DataSource = dt;
                ddlConType.DisplayMember = "t_Name";
                ddlConType.ValueMember = "t_ID";
            }
            ddlConType.SelectedIndex = 0;

        }

        private void LoadContainerSize()
        {

            var type = objSizelist;
            ddlContSize.ValueMember = "ContainerSizeId";
            ddlContSize.DisplayMember = "ContainerSize";

            DataTable dt = new DataTable();
            dt.Columns.Add("t_ID", typeof(int));
            dt.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt.Rows.Add(t.ContainerSizeId, t.ContainerSize1);
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "--Size--";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count > 0)
            {
                ddlContSize.DataSource = dt;
                ddlContSize.DisplayMember = "t_Name";
                ddlContSize.ValueMember = "t_ID";
            }
            ddlContSize.SelectedIndex = 0;

        }

        private void LoadAutoSuggestedBL()
        {

            var data=IGMImportBll.GetAllIGMImport();
            var source = new AutoCompleteStringCollection();
            foreach( var item in data)
            {

                source.Add(item.BLnumber);
            }
           txtBLNumber.AutoCompleteCustomSource = source;
           txtBLNumber.AutoCompleteMode = AutoCompleteMode.Suggest;
           txtBLNumber.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

       

        #endregion 



        #region IGM Import Entry

        private void btnIGMSave_Click(object sender, EventArgs e)
        {
            if (btnIGMSave.Text == "Save")
            {
                bool flag = ValidateIGMInput();              
                if (flag == true)
                {
                    if (objIGMImport.IGMImportDetails.Count == 0)
                    {
                        MessageBox.Show("No Master details data !!","IGM Details Required",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }
                    FillingIGMImportData();

                    DialogResult result = MessageBox.Show("Checked duplicate data entry ??","IGM-Import duplicate entry",MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        var message = IGMImportBll.CheckDuplicateEntry(objIGMImport);
                        if (message.ToString() == "")
                        {
                            var status = IGMImportBll.Insert(objIGMImport);
                            MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearIGMDetails();
                            ClearIGMImport();
                            ClearGrid();
                        }
                        else
                        {
                            MessageBox.Show(message.ToString(), "Duplicate Container Entry Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                       
                    }
                    else
                    {
                        var status = IGMImportBll.Insert(objIGMImport);
                        MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearIGMDetails();
                        ClearIGMImport();
                        ClearGrid();
                        //LoadAutoSuggestedBL();
                    }

                }
            }
            else if (btnIGMSave.Text == "Update")
            {
                bool flag = ValidateIGMInput();
                if (flag == true)
                {

                    FillingIGMImportData();
                    var status = IGMImportBll.Update(objIGMImport);
                    MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearIGMDetails();
                ClearIGMImport();                            
                ClearGrid();
            }          

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                           "Confirm IGM-Import deletion",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                var status = IGMImportBll.Delete(objIGMImport);
                MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearIGMImport();
                ClearGrid();

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearIGMImport();
            ClearIGMDetails();
            ClearGrid();
            labelControl1.Focus();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ClearIGMImport();
            ClearGrid();
            this.Close();
        }

        private void btnLoadBL_Click(object sender, EventArgs e)
        {
            string blnumber = txtBLNumber.Text.Trim();
            if (blnumber == "")
            {
                MessageBox.Show("* Please input B/L number !!", "Input required.");
                return;
            
            }

            btnIGMSave.Text = "Update";
            btnDelete.Enabled = true;

            objIGMImport = IGMImportBll.GetIGMImportByBL(blnumber);
            if (objIGMImport != null)
            {
                ShowIGMData(objIGMImport);
                if (objIGMImport.IGMImportDetails.Count > 0)
                {
                    PrepareGrid();
                    ClearGrid();
                    List<IGMImportDetail> listIGMDetails = IGMImportBll.GetAllIGMImportDetailbyIGMId(objIGMImport.IGMImportId);
                    int index = 0;
                    foreach (var item in listIGMDetails)
                    {
                        dataGridView1.Rows.Add(IGMContEntrySL, item.ContainerNo, item.ContainerSize.ContainerSize1, item.ContainerType.ContainerTypeName, item.SealNo);
                        if (item.InOutStatus == 1)
                        {
                            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Khaki;
                        }
                        else if (item.InOutStatus == 2)
                        {
                            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Goldenrod;
                        }
                        index = index + 1;
                        IGMContEntrySL = IGMContEntrySL + 1;
                    }
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.ClearSelection();
                }
                
            }
            else
            {
                MessageBox.Show("BL Number "+txtBLNumber.Text+" not found !!",
                          "Data Not Found.",
                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBLNumber.Text = "";
            }
        }

        private void bntNewIGM_Click(object sender, EventArgs e)
        {
            int totalIGMRow = IGMImportBll.GetIGMRowCount();
            totalIGMRow = totalIGMRow + 1;
            string currentMonth = DateTime.Now.Month.ToString();
            string currentYear = DateTime.Now.Year.ToString();
            txtReferenceNo.Text = "IGM" + currentYear + currentMonth + totalIGMRow;
        }

       
        private bool ValidateIGMInput()
        {
           
            var errMessage = "";

            if (txtReferenceNo.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter reference number !!\n";
            }
            if (txtBLNumber.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter B/L number !!\n";
            }
            if (Convert.ToInt32(ddlMLO.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select MLO !!\n";
            }
            if (Convert.ToInt32(ddlImporter.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select importer !!\n";
            }
            if (Convert.ToInt32(ddlVessel.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select vessel !!\n";
            }
            if (Convert.ToInt32(ddlCommodity.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select commodity !!\n";
            }
            if (txtWeight.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter gross weight !!\n";
            }
            if (Convert.ToInt32(ddlPortofDischarge.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select discharge port !!\n";
            }
            if (Convert.ToInt32(ddlPlaceofDelivery.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select delivery place !!\n";
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

        private void FillingIGMImportData()
        {

            objIGMImport.ReferanceNumber = txtReferenceNo.Text.Trim();
            objIGMImport.BLnumber = txtBLNumber.Text.Trim();
            objIGMImport.LineNo = txtIGMLineNo.Text.Trim();
            objIGMImport.CustomerId = Convert.ToInt32(ddlMLO.SelectedValue);
            objIGMImport.ImporterId = Convert.ToInt32(ddlImporter.SelectedValue);
            objIGMImport.VesselId = Convert.ToInt32(ddlVessel.SelectedValue);
            objIGMImport.Rotation = txtRotation.Text.Trim();
            objIGMImport.CommodityId = Convert.ToInt32(ddlCommodity.SelectedValue);
            objIGMImport.NumberofPackage = Convert.ToInt32(txtNumofPkg.Text.Trim());
            objIGMImport.UnitId = Convert.ToInt32(ddlMeasureUnit.SelectedValue);

            objIGMImport.GrossWeight = Convert.ToDecimal(txtWeight.Text.Trim());
            objIGMImport.PortofDischargeId = Convert.ToInt32(ddlPortofDischarge.SelectedValue);
            objIGMImport.PortofDeliveryId = Convert.ToInt32(ddlPlaceofDelivery.SelectedValue);

            objIGMImport.BoxQuantity = Convert.ToInt32(txtBoxQuantity.Text.Trim());
            objIGMImport.Remarks = txtRemarks.Text.Trim();
          
        }

        public void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[1].HeaderText = "Container No";

            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "Size";

            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[3].HeaderText = "Type";

            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Seal No";

        }

        private void ShowIGMData(IGMImport objIGM)
        {

            txtReferenceNo.Text = objIGM.ReferanceNumber;
            txtBLNumber.Text = objIGM.BLnumber;
            txtIGMLineNo.Text = objIGM.LineNo;
            ddlMLO.SelectedValue =Convert.ToInt32(objIGM.CustomerId);
            ddlImporter.SelectedValue = Convert.ToInt32(objIGM.ImporterId);
            ddlVessel.SelectedValue = Convert.ToInt32(objIGM.VesselId);
            txtRotation.Text = objIGM.Rotation;
            ddlCommodity.SelectedValue = Convert.ToInt32(objIGM.CommodityId);
            txtNumofPkg.Text =Convert.ToString(objIGM.NumberofPackage);
            ddlMeasureUnit.SelectedValue = Convert.ToInt32(objIGM.UnitId);

            txtWeight.Text = Convert.ToString(objIGM.GrossWeight);
            ddlPortofDischarge.SelectedValue = Convert.ToInt32(objIGM.PortofDischargeId);
            ddlPlaceofDelivery.SelectedValue = Convert.ToInt32(objIGM.PortofDeliveryId);

            txtBoxQuantity.Text = Convert.ToString(objIGM.BoxQuantity);
            txtRemarks.Text = Convert.ToString(objIGM.Remarks);
            txtContainerNo.Focus();
           
        }

        private void ClearIGMImport()
        {

            txtReferenceNo.Text = "";
            txtBLNumber.Text = "";
            txtIGMLineNo.Text = "";
            ddlMLO.SelectedIndex = 0;
            ddlImporter.SelectedIndex = 0;
            ddlVessel.SelectedIndex = 0;
            ddlCommodity.SelectedIndex = 0;
            txtRotation.Text = "";
            txtNumofPkg.Text = "";
            ddlMeasureUnit.SelectedIndex = 0;
            txtWeight.Text = "";          
            ddlPortofDischarge.SelectedIndex = 0;
            ddlPlaceofDelivery.SelectedIndex = 0;
            txtBoxQuantity.Text = "";
            txtRemarks.Text = "";
            txtFilePath.Text = "";
            IGMContEntrySL = 1;
            txtBLNumber.Focus();

            objIGMImport= new IGMImport();

            btnIGMSave.Text = "Save";
            btnDelete.Enabled = false;
           
        }



        #endregion



        #region IGM Container Details CODE


        private void btnAddIGMCont_Click(object sender, EventArgs e)
        {
            bool flag = ValidateIGMDetails();
            if (flag == true)
            {
                IGMImportDetail objIGMdetails = FillingIGMDetailsData();

                if (btnAddIGMCont.Text == "Add")
                {
                    //objIGMImport.BoxReceived = (objIGMImport.BoxReceived == null ? 0 : objIGMImport.BoxReceived) + 1;
                    var totalReceived = objIGMImport.BoxReceived == null ? 0 : objIGMImport.BoxReceived;
                    var totalReceiveable = txtBoxQuantity.Text.Trim();
                    if (totalReceiveable == string.Empty)
                    {

                        MessageBox.Show("Please mention total quantity first !!", "Input Required.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBoxQuantity.Focus();
                        return;
                    }
                    else
                    {
                        if (totalReceived < Convert.ToInt32(txtBoxQuantity.Text))
                        {
                            objIGMImport.IGMImportDetails.Add(objIGMdetails);
                            BindDataToGrid(objIGMdetails);
                            objIGMImport.BoxReceived = (objIGMImport.BoxReceived == null ? 0 : objIGMImport.BoxReceived) + 1;
                            if (objIGMImport.BoxReceived == Convert.ToInt32(txtBoxQuantity.Text))
                            {
                                MessageBox.Show("IGM Input total container entry completed !!", "IGM Input completed.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                           
                        }
                       if (totalReceived == Convert.ToInt32(txtBoxQuantity.Text))
                        {
                            MessageBox.Show("You can not add more container !!", "IGM Input completed.", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        if (totalReceived > Convert.ToInt32(txtBoxQuantity.Text))
                        {
                            MessageBox.Show("Total number of container entry overflowed !!", "IGM details overflow.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                                 
                }
                else
                {
                    //update IGM details
                   // objIGMImport.IGMImportDetails.ElementAt(index).LineNo = objIGMdetails.LineNo;
                    objIGMImport.IGMImportDetails.ElementAt(index).ContainerNo = objIGMdetails.ContainerNo;
                    objIGMImport.IGMImportDetails.ElementAt(index).SealNo = objIGMdetails.SealNo;
                    objIGMImport.IGMImportDetails.ElementAt(index).SizeId = objIGMdetails.SizeId;
                    objIGMImport.IGMImportDetails.ElementAt(index).TypeId = objIGMdetails.TypeId;
                    objIGMImport.IGMImportDetails.ElementAt(index).ISO = objIGMdetails.ISO;
                   // objIGMImport.IGMImportDetails.ElementAt(index).CommodityId = objIGMdetails.CommodityId;

                    GridUpdate(index, objIGMdetails);
                }

                ClearIGMDetails();
            }



        }

        private void btnIGMContDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                           "Confirm IGM-Import container deletion",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                var obj = objIGMImport.IGMImportDetails.ElementAt(index);
                objIGMImport.IGMImportDetails.Remove(obj);
                objIGMImport.BoxReceived = objIGMImport.BoxReceived - 1;
                //dataGridView1.Rows.RemoveAt(index);
                LoadGrid();
                ClearIGMDetails();

            }
        }

        private void LoadGrid()
        {

            dataGridView1.Rows.Clear();
            int index = 0;
            foreach (IGMImportDetail item in objIGMImport.IGMImportDetails)
            {
                dataGridView1.Rows.Add(IGMContEntrySL, item.ContainerNo, objSizelist.Where(x=>x.ContainerSizeId==item.SizeId).First().ContainerSizeId, objTypelist.Where(x=>x.ContainerTypeId==item.TypeId).First().ContainerTypeName, item.SealNo);
                if (item.InOutStatus == 1)
                {
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Khaki;
                }
                else if (item.InOutStatus == 2)
                {
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Goldenrod;
                }
                index = index + 1;
                IGMContEntrySL = IGMContEntrySL + 1;

            }

            dataGridView1.ClearSelection();
           
        }
        private bool ValidateIGMDetails()
        {
            var errMessage = "";

           
            if (txtContainerNo.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter container number !!\n";
            }
            if (txtSealNo.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter seal number !!\n";
            }
            if (Convert.ToInt32(ddlContSize.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select container size !!\n";
            }
            if (Convert.ToInt32(ddlConType.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select container type !!\n";
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

        private IGMImportDetail FillingIGMDetailsData()
        {

            IGMImportDetail objIGMIDetails = new IGMImportDetail();

            objIGMIDetails.IGMImportId = objIGMImport.IGMImportId;
            objIGMIDetails.Date = Convert.ToDateTime(dateContEntry.Text.Trim());
            objIGMIDetails.SealNo = txtSealNo.Text.Trim();
            objIGMIDetails.ContainerNo = txtContainerNo.Text.Trim();
            objIGMIDetails.Date = DateTime.Now;
            objIGMIDetails.SizeId = Convert.ToInt32(ddlContSize.SelectedValue);
            objIGMIDetails.TypeId = Convert.ToInt32(ddlConType.SelectedValue);
            objIGMIDetails.ISO = txtISO.Text.Trim();
            objIGMIDetails.InOutStatus = 0;

            return objIGMIDetails;


        }

        private void BindDataToGrid(IGMImportDetail objIGMDetails)
        {
            if (IGMContEntrySL == 1)
            {
                PrepareGrid();
            }

            this.dataGridView1.Rows.Add(IGMContEntrySL, objIGMDetails.ContainerNo, ddlContSize.Text.Trim(), ddlConType.Text.Trim(), objIGMDetails.SealNo);
            dataGridView1.AllowUserToAddRows = false;

            IGMContEntrySL = IGMContEntrySL + 1;
            dataGridView1.ClearSelection();

        }

        private void GridUpdate(int index, IGMImportDetail objIGMDetails)
        {

            dataGridView1.Rows[index].Cells[1].Value = objIGMDetails.ContainerNo;
            dataGridView1.Rows[index].Cells[2].Value = ddlContSize.Text.Trim();
            dataGridView1.Rows[index].Cells[3].Value = ddlConType.Text.Trim();
            dataGridView1.Rows[index].Cells[4].Value = objIGMDetails.SealNo;


            dataGridView1.ClearSelection();

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            index = Convert.ToInt32(selectedRow.Index);
            var data = objIGMImport.IGMImportDetails.ElementAt(index);

           // txtIGMLineNo.Text = Convert.ToString(data.LineNo);
            txtContainerNo.Text = Convert.ToString(data.ContainerNo);
            txtSealNo.Text = Convert.ToString(data.SealNo);
            txtISO.Text = Convert.ToString(data.ISO);
            dateContEntry.Value = Convert.ToDateTime(data.Date);
            ddlContSize.SelectedValue = Convert.ToInt32(data.SizeId);
            ddlConType.SelectedValue = Convert.ToInt32(data.TypeId);
           // ddlCommodity.SelectedValue = Convert.ToInt32(data.CommodityId);

            btnAddIGMCont.Text = "Update";
            btnIGMContDelete.Enabled = true;
        }

        private void ddlContSize_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int sizeId = Convert.ToInt32(ddlContSize.SelectedValue);

            if (sizeId > 0)
            {
                int typeId = Convert.ToInt32(ddlConType.SelectedValue);

                if (typeId > 0)
                {
                    txtISO.Text = "";
                    txtISO.Text = IGMImportBll.GetISOCode(sizeId, typeId);

                }
            }

        }

        private void ddlConType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int typeId = Convert.ToInt32(ddlConType.SelectedValue);

            if (typeId > 0)
            {
                int sizeId = Convert.ToInt32(ddlContSize.SelectedValue);

                if (sizeId > 0)
                {
                    txtISO.Text = "";
                    txtISO.Text = IGMImportBll.GetISOCode(sizeId, typeId);

                }
            }

        }

        private void ClearIGMDetails()
        {
            
            txtContainerNo.Text = "";
            txtSealNo.Text = "";
            txtISO.Text = "";
            dateContEntry.Value = DateTime.Now;
            ddlContSize.SelectedIndex = 0;
            ddlConType.SelectedIndex = 0;
            btnAddIGMCont.Text = "Add";
            btnIGMContDelete.Enabled = false;
            txtContainerNo.Focus();
            //dataGridView1.ClearSelection();

        }

        private void ClearGrid()
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            IGMContEntrySL = 1;

        }


        #endregion

        private void btnUpload_Click(object sender, EventArgs e)
        {

            object status = null;
            if (opd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = opd.FileName;
                string filePath = opd.FileName;
                string extension = Path.GetExtension(filePath);
                DataSet dataSet = ImportExcelFile(extension, filePath);

                UploadIGMData(dataSet);
                
            }
            else
             if (opd.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show(status.ToString(), "Data Upload has Cance", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        public DataSet ImportExcelFile(string fileExt, string filePath)
        {
            OleDbConnection connection = null;
            var excelDataSet = new DataSet();
            try
            {
                if (fileExt == ".xls")
                {
                    connection =
                        new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath +
                                            @";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'");
                }
                else if (fileExt == ".xlsx")
                {
                    connection =
                        new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                                            ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'");
                }
                if (connection != null)
                {
                    connection.Open();
                    var dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dataTable != null)
                    {
                        string getExcelSheetName = dataTable.Rows[0]["Table_Name"].ToString();
                        var excelCommand = new OleDbCommand(@"SELECT * FROM [" + getExcelSheetName + @"]", connection);
                        var excelAdapter = new OleDbDataAdapter(excelCommand);

                        excelAdapter.Fill(excelDataSet);
                    }
                    connection.Close();
                }
                return excelDataSet;
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.ToString(), "IGM Input completed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
        }

        public void UploadIGMData(DataSet dataset)
        {
            try
            {
               
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    IGMImportDetail objIGMIDetails = new IGMImportDetail();

                    objIGMIDetails.IGMImportId = objIGMImport.IGMImportId;
                    objIGMIDetails.Date = DateTime.Now;
                    objIGMIDetails.SealNo = row["Seal Number"].ToString().Trim();
                    objIGMIDetails.ContainerNo = row["Container Number"].ToString().Trim();
                    objIGMIDetails.Date = DateTime.Now;
                    int size = Convert.ToInt32(row["Size"]);
                    ContainerSize objSize = objSizelist.Find(x => x.ContainerSize1 == size);
                    objIGMIDetails.SizeId = objSize.ContainerSizeId;
                    string Typ = row["Type"].ToString().Trim();
                    ContainerType objTyp = objTypelist.Find(x => x.ContainerTypeName.Trim() == Typ.Trim());
                    objIGMIDetails.TypeId = objTyp.ContainerTypeId;                                       
                    objIGMIDetails.ISO = row["ISO Code"].ToString().Trim();
                    objIGMIDetails.InOutStatus = 0;
                    objIGMImport.IGMImportDetails.Add(objIGMIDetails);
                    objIGMImport.BoxReceived = (objIGMImport.BoxReceived == null ? 0 : objIGMImport.BoxReceived) + 1;
                    if (IGMContEntrySL == 1)
                    {
                        PrepareGrid();
                    }

                    dataGridView1.Rows.Add(IGMContEntrySL, objIGMIDetails.ContainerNo, objSize.ContainerSize1, objTyp.ContainerTypeName, objIGMIDetails.SealNo);
                    IGMContEntrySL = IGMContEntrySL + 1;

                }

               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "IGM Input completed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }


        }

        private void chkPermission_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPermission.Checked == true)
            {
                //txtContNumbe.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                txtContainerNo.Properties.Mask.EditMask = "[A-Z0-9]{7,11}";
            }
            else
            {
                //txtContNumbe.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                txtContainerNo.Properties.Mask.EditMask = "[A-Z]{4}[0-9]{7}";

            }
        }
    }
}
