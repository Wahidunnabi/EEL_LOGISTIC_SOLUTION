using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LOGISTIC.BLL;
using LOGISTIC.Export.BLL;
using System.Reflection;

namespace LOGISTIC.UI.Export
{
    public partial class StauffingDetails : Form
    {

        private CustomerBll MLOBll = new CustomerBll();
        private ContainerWeightBll weightBll = new ContainerWeightBll();
        private UnitofMeasureBll unitBll = new UnitofMeasureBll();
        private LocationBLL locationBll = new LocationBLL();
        private BankBLL bankBll = new BankBLL();
        private PortBLL portBll = new PortBLL();

        private CargoStuffingBLL stuffingBll = new CargoStuffingBLL();
        private List<Customer> listCustomer = new List<Customer>();
        private List<Port> lstPort = new List<Port>();
        private StuffingDetail objStuffingDetails = new StuffingDetail();
        private CargoRecieving objCargoReceive = new CargoRecieving();
        private CargoDetail objCargoDetails = new CargoDetail();

        private UserInfo user;
        bool ReferContainer = false;
        int SizeId;
        int TypeId;



        public StauffingDetails(UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;
            lblStuffedBy.Text = user.FirstName + " " + user.LastName;
            lblSizeType.Text = "";
            lblPluginDate.Visible = false;
            lblPTIDate.Visible = false;
            datePlugIn.Visible = false;
            datePTI.Visible = false;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            

        }

        private void StauffingDetails_Load(object sender, EventArgs e)
        {

            listCustomer = MLOBll.Getall();
            lstPort = portBll.Getall();
            LoadCustomer();
            LoadContWeight();
            LoadMeasureUnit();
            LoadShift();
            LoadLocation();
            LoadStatus();
            LoadBank();
            LoadPortofLoading();
            LoadPortofDischarge();
            LoadClientSearch();
            ddlClient.Enabled = false;

        }

        #region Load Basic Data

        private void LoadCustomer()
        {
          
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in listCustomer)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select MLO--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlClient.DataSource = dt_Types;
                ddlClient.DisplayMember = "t_Name";
                ddlClient.ValueMember = "t_ID";
            }
            ddlClient.SelectedIndex = 0;

        }

        private void LoadCSDContainer(List<CSDContGateInOut> type)
        {

            ddlContainerNo.DataSource = null;
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ContainerGateEntryId, t.ContNo);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Container No--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlContainerNo.DataSource = dt_Types;
                ddlContainerNo.DisplayMember = "t_Name";
                ddlContainerNo.ValueMember = "t_ID";
            }
            ddlContainerNo.SelectedIndex = 0;

        }

        private void LoadContWeight()
        {

            var type = weightBll.Getall();         
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ContGrossWeightId, t.GrossWeight);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- TR Weight --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlContWeight.DataSource = dt_Types;
                ddlContWeight.DisplayMember = "t_Name";
                ddlContWeight.ValueMember = "t_ID";
            }
            ddlContWeight.SelectedIndex = 0;

        }

        private void LoadMeasureUnit()
        {

            var type = unitBll.Getall();         
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.UnitOfMeasureId, t.UnitOfMeasureName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Unit --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlStuffCat.DataSource = dt_Types;
                ddlStuffCat.DisplayMember = "t_Name";
                ddlStuffCat.ValueMember = "t_ID";
            }
            ddlStuffCat.SelectedIndex = 0;

        }       

        private void LoadClientSearch()
        {
        
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in listCustomer)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Customer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlClientSearch.DataSource = dt_Types;
                ddlClientSearch.DisplayMember = "t_Name";
                ddlClientSearch.ValueMember = "t_ID";
            }
            ddlClientSearch.SelectedIndex = 0;

        }

        private void LoadShift()
        {

            ddlShift.Items.Insert(0, "-- Shift --");

            ddlShift.Items.Insert(1, "Day");
            ddlShift.Items.Insert(2, "Night");
            ddlShift.SelectedIndex = 0;


        }

        private void LoadLocation()
        {

            var type = locationBll.Getall();         
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.LocationId, t.LocationName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Location--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlLocation.DataSource = dt_Types;
                ddlLocation.DisplayMember = "t_Name";
                ddlLocation.ValueMember = "t_ID";
            }
            ddlLocation.SelectedIndex = 0;

        }

        private void LoadStatus()
        {

            ddlStatus.Items.Insert(0, "-- Status --");
            ddlStatus.Items.Insert(1, "FCL");
            ddlStatus.Items.Insert(2, "LCL");
            ddlStatus.SelectedIndex = 0;


        }

        private void LoadBank()
        {

            var type = bankBll.Getall();           
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.BankId, t.BankName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Negotioting Bank --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlNegoBank.DataSource = dt_Types;
                ddlNegoBank.DisplayMember = "t_Name";
                ddlNegoBank.ValueMember = "t_ID";
            }
            ddlNegoBank.SelectedIndex = 0;

        }

        private void LoadPortofLoading()
        {

            var type = lstPort;           
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.PortOfLandId, t.PortName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Port of Loading --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlPoLo.DataSource = dt_Types;
                ddlPoLo.DisplayMember = "t_Name";
                ddlPoLo.ValueMember = "t_ID";
            }
            ddlPoLo.SelectedIndex = 0;

        }

        private void LoadPortofDischarge()
        {

            var type = lstPort;         
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.PortOfLandId, t.PortName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Port of Discharge --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlPodo.DataSource = dt_Types;
                ddlPodo.DisplayMember = "t_Name";
                ddlPodo.ValueMember = "t_ID";
            }
            ddlPodo.SelectedIndex = 0;

        }


        #endregion


        private void ddlClient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlClient.SelectedValue) > 0)
            {
                int clientId = Convert.ToInt32(ddlClient.SelectedValue);
                LoadCSDContainerByClientId(clientId);
                ddlContainerNo.Enabled = true;
            }
            else
            {

                ddlContainerNo.DataSource = null;
                ddlContainerNo.Enabled = false;
                txtRefNo.Text = "";

            }
        }

        private void ddlContainerNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
            if (Convert.ToInt32(ddlContainerNo.SelectedValue) > 0)
            {
                //Get CSD Container Ref No, Size and Type for stuffing entry
                var obj = stuffingBll.GetCSDContInformation(Convert.ToInt32(ddlContainerNo.SelectedValue));

                SizeId = Convert.ToInt32(obj.GetType().GetProperty("SizeId").GetValue(obj, null));
                TypeId = Convert.ToInt32(obj.GetType().GetProperty("Typeid").GetValue(obj, null));

                Type myType = obj.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                string size = "";
                string type = "";
                foreach (PropertyInfo prop in props)
                {
                    if (prop.Name == "RefNo")
                    {
                        txtRefNo.Text = Convert.ToString(prop.GetValue(obj, null));
                    }
                    if (prop.Name == "Size")
                    {
                        size = Convert.ToString(prop.GetValue(obj, null));
                        //SizeId = Convert.ToInt32(prop.GetType());

                    }
                    
                    if (prop.Name == "Type")
                    {
                        type = Convert.ToString(prop.GetValue(obj, null));
                      
                    }
                  
                }

                lblSizeType.Text = size + " " + type;  

                if (type == "RE" || type == "RF" || type == "HR")
                {
                    ReferContainer = true;
                    lblPluginDate.Visible = true;
                    lblPTIDate.Visible = true;
                    datePlugIn.Visible = true;
                    datePTI.Visible = true;
                }
                else
                {
                    ReferContainer = false;
                    lblPluginDate.Visible = false;
                    lblPTIDate.Visible = false;
                    datePlugIn.Visible = false;
                    datePTI.Visible = false;
                }

            }
            else
            {
                txtRefNo.Text = "";
            }
           
        }

        private void ddlClientSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {

            
            if (Convert.ToInt32(ddlClientSearch.SelectedValue) > 0)
            {
                int clientId = Convert.ToInt32(ddlClientSearch.SelectedValue);
                LoadClientwiseCargoReceives(clientId);
                ddlSLNo.Enabled = true;
            }
            else
            {


                ddlSLNo.DataSource = null;
                ddlSLNo.Items.Clear();
                ddlSLNo.Refresh();
                ddlSLNo.Enabled = false;

            }
        }

        public void LoadClientwiseCargoReceives(int clientId)
        {
            List<CargoRecieving> listCR = stuffingBll.GetClientwiseAllCargoReceive(clientId);
            if (listCR.Count > 0)
            {

                LoadSLNumber(listCR);

            }
            else
            {
                ddlSLNo.DataSource = null;
                ddlSLNo.Refresh();
                ddlSLNo.Items.Clear();
                ddlSLNo.Items.Insert(0, "No data found");
                ddlSLNo.SelectedIndex = 0;
                ddlSLNo.Enabled = false;
                labelControl1.Focus();
            }


        }

        private void LoadSLNumber(List<CargoRecieving> type)
        {

            ddlSLNo.DataSource = null;
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CargoReceiveId, t.SLNo);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--SL Number--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlSLNo.DataSource = dt_Types;
                ddlSLNo.DisplayMember = "t_Name";
                ddlSLNo.ValueMember = "t_ID";
            }
            ddlSLNo.SelectedIndex = 0;

        }

        private void ddlSLNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlSLNo.SelectedValue) > 0)
            {
                int cargoReceiveId = Convert.ToInt32(ddlSLNo.SelectedValue);
                objCargoReceive = stuffingBll.GetCargoRecevingByCargoReceiveId(cargoReceiveId);
                LoadDatatoGrid(objCargoReceive);

            }
        }


        #region Grid Function

        public void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 9;

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[0].HeaderText = "Cartoon ID";


            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[1].HeaderText = "Packages";

            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[2].HeaderText = "Pieces";

            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[3].HeaderText = "CargoWeight";



            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[4].HeaderText = "Height";

            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[5].HeaderText = "Length";

            dataGridView1.Columns[6].Width = 60;
            dataGridView1.Columns[6].HeaderText = "Width";

            dataGridView1.Columns[7].Width = 60;
            dataGridView1.Columns[7].HeaderText = "CBM";

            dataGridView1.Columns[8].Width = 60;
            dataGridView1.Columns[8].HeaderText = "Stuffed ?";

           

        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            objCargoDetails = objCargoReceive.CargoDetails.ElementAt(selectedrowindex);


            

            if (objStuffingDetails == null)
            {

            }

            if (objCargoDetails.IsStuffed == true)
            {
                DialogResult result = MessageBox.Show("Do you want to update this stuffing ??",
                          "Confirm Stuffing Update",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    //objCargoDetails.CargoDetailsId = 13;
                    objStuffingDetails = stuffingBll.GetStuffingDetailsByCargoDetailsId(objCargoDetails.CargoDetailsId);
                    if (objStuffingDetails.StuffingDetailsId > 0)
                    {
                        LoadCSDContainerByClientId(objCargoReceive.CustId);
                        BindStuffingDataToField(objStuffingDetails);
                        ddlClient.Enabled = false;
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                    }

                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you want to stuffing ??",
                          "Confirm Stuffing",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    LoadCSDContainerByClientId(objCargoReceive.CustId);
                    LoadCarrier();
                    ddlClient.SelectedValue = objCargoReceive.CustId;
                    ddlClient.Enabled = false;
                    ddlLocation.SelectedValue = objCargoReceive.Location;
                    ddlLocation.Enabled = false;
                    btnSave.Enabled = true;

                }
            }

        }

    
        private void LoadDatatoGrid(CargoRecieving objCR)
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            PrepareGrid();

            int index = 0;

            foreach (var item in objCR.CargoDetails)
            {

                dataGridView1.Rows.Add(item.CartoonId, item.Packages,item.Pieces,item.CargoWeight, item.Height, item.Length, item.Width, item.CubicMeter, (item.IsStuffed == true) ? "Yes" : "No");

                if (item.IsStuffed == true)
                {
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Khaki;
                }              
                index = index + 1;


            }

            dataGridView1.ClearSelection();
            dataGridView1.AllowUserToAddRows = false;
        }

        public void LoadCSDContainerByClientId(int clientId)
        {
            List<CSDContGateInOut> listCSDCont = stuffingBll.GetClientwiseCSDContainer(clientId);
            if (listCSDCont.Count > 0)
            {

                LoadCSDContainer(listCSDCont);

            }
            else
            {
                ddlContainerNo.DataSource = null;
                ddlContainerNo.Refresh();
                ddlContainerNo.Items.Clear();
                ddlContainerNo.Items.Insert(0, "No data found !");
                ddlContainerNo.SelectedIndex = 0;
                ddlContainerNo.Enabled = false;
            }


        }

        private void LoadCarrier()
        {
            if (Convert.ToString(ddlClientSearch.Text) == "MRSK")
            {              
                var source = new AutoCompleteStringCollection();
                source.Add("MKL");
                source.Add("SCL");
                source.Add("MCC");
               
                txtCarrier.AutoCompleteCustomSource = source;
                txtCarrier.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtCarrier.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else
            {
                txtCarrier.Text = ddlClientSearch.Text.Trim();
               
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            int clientId = Convert.ToInt32(ddlClientSearch.SelectedValue);
            int cargoReceiveId = Convert.ToInt32(ddlSLNo.SelectedValue);

            if (clientId == 0)
            {
                MessageBox.Show(" please select a Client", "Data Selection Required !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cargoReceiveId == 0)
            {
                MessageBox.Show(" please select a SL no", "Data Selection Required !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                objCargoReceive = stuffingBll.GetCargoRecevingByCargoReceiveId(cargoReceiveId);

                LoadDatatoGrid(objCargoReceive);
                ddlClientSearch.SelectedIndex = 0;
                btnSearch.Enabled = false;
                labelControl1.Focus();
            }


        }

        private void BindStuffingDataToField(StuffingDetail stuffingDetails)
        {

            ddlClient.SelectedValue = stuffingDetails.CustId;
            txtRefNo.Text = Convert.ToString(stuffingDetails.RefNo);                      
            ddlContainerNo.SelectedValue = stuffingDetails.CSDGateEntryId;
            if (stuffingDetails.IsReferContainer == true)
            {
                ReferContainer = true;
                datePlugIn.Value = Convert.ToDateTime(stuffingDetails.PluginDate);
                datePTI.Value = Convert.ToDateTime(stuffingDetails.PTIDate);
                lblPluginDate.Visible = true;
                lblPTIDate.Visible = true;
                datePlugIn.Visible = true;
                datePTI.Visible = true;

            }
            else
            {
                ReferContainer = false;               
                lblPluginDate.Visible = false;
                lblPTIDate.Visible = false;
                datePlugIn.Visible = false;
                datePTI.Visible = false;

            }
            txtSealNo.Text = stuffingDetails.SealNo;
            ddlContWeight.SelectedValue = stuffingDetails.TareWT;
            chkSub.Checked = Convert.ToBoolean(stuffingDetails.IsSub);
            ddlStuffCat.SelectedValue = stuffingDetails.StuffCategory;
            ddlShift.SelectedIndex =Convert.ToInt32(stuffingDetails.DayNightShift);
            ddlLocation.SelectedValue = stuffingDetails.Location;          
            txtBookingNo.Text = stuffingDetails.BookingNo;
            ddlStatus.SelectedIndex =Convert.ToInt32(stuffingDetails.Status);
            txtVGMWeight.Text = stuffingDetails.VGMWeight;
            txtCarrier.Text = stuffingDetails.CarrierName;
            txtExpNo.Text = stuffingDetails.ExpNo;
            dateExpire.Value =Convert.ToDateTime(stuffingDetails.ExpDate);
            ddlNegoBank.SelectedValue = stuffingDetails.NegotiatingBank;
            ddlPoLo.SelectedValue = stuffingDetails.PortofLoading;
            ddlPodo.SelectedValue = stuffingDetails.PortofDischarge;
            dateStuffing.Value =Convert.ToDateTime(stuffingDetails.StuffingDate);

        }

        #endregion

      

        #region Stuffing CRUD data



        private void btnSave_Click(object sender, EventArgs e)
        {

            if (btnSave.Text == "Save")
            {
                bool flag = ValidateStuffing();
                if (flag == true)
                {
                    var result = CheckDuplicateBookingNumber();

                    if (result == true)
                    {

                        DialogResult decision = MessageBox.Show(" This Booking Number is already in used !! Do you want to use it again ??",
                          "Confirm Reusing",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (decision == DialogResult.No)
                        {
                            txtBookingNo.Text = "";
                            txtBookingNo.Focus();
                            return;
                        }

                    }

                    FillingStuffingData();
                    var status = stuffingBll.Insert(objStuffingDetails);
                    MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    objCargoReceive = stuffingBll.GetCargoRecevingByCargoReceiveId(Convert.ToInt32(objCargoReceive.CargoReceiveId));
                    LoadDatatoGrid(objCargoReceive);
                    ClearStuffing();

                }
            }
            else if (btnSave.Text == "Update")
            {
                bool flag = ValidateStuffing();
                if (flag == true)
                {

                    FillingStuffingData();
                    var status = stuffingBll.Update(objStuffingDetails);
                    MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);                 
                    ClearStuffing();
                }
                
            }

        }

        private bool ValidateStuffing()
        {

            var errMessage = "";

            if (ddlContainerNo.Text.Trim().Length != 11)
            {
                errMessage = errMessage + "* Please select a container !!\n";
            }
            if (txtSealNo.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter seal number !!\n";
            }
            if (Convert.ToInt32(ddlStuffCat.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select stuff category !!\n";
            }
            if (Convert.ToInt32(ddlLocation.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a location !!\n";
            }
            if (txtBookingNo.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter booking no !!\n";
            }
            if (txtCarrier.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please select carrier name !!\n";
            }
            if (txtExpNo.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter exp number !!\n";
            }
            if (Convert.ToInt32(ddlNegoBank.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select negotiating bank!!\n";
            }
            if (Convert.ToInt32(ddlPoLo.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select port of loading !!\n";
            }
            if (Convert.ToInt32(ddlPodo.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select port of discharge !!\n";
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

        private bool CheckDuplicateBookingNumber()
        {

            var bookingNo = txtBookingNo.Text.Trim();
            var status = stuffingBll.CheckDuplicateBookingNo(bookingNo);
            return status;          

        }

        private void FillingStuffingData()
        {
           
            objStuffingDetails.CargoDetailsId = objCargoDetails.CargoDetailsId;
            objStuffingDetails.CustId = Convert.ToInt32(ddlClient.SelectedValue);
            objStuffingDetails.RefNo = Convert.ToInt64(txtRefNo.Text.Trim());
            objStuffingDetails.CSDGateEntryId = Convert.ToInt64(ddlContainerNo.SelectedValue);
            objStuffingDetails.ContainerNo =ddlContainerNo.Text.Trim();
            if (ReferContainer == true)
            {
                objStuffingDetails.IsReferContainer = true;
                objStuffingDetails.PluginDate = datePlugIn.Value;
                objStuffingDetails.PTIDate = datePTI.Value;
            }
            objStuffingDetails.SealNo = txtSealNo.Text.Trim();
            objStuffingDetails.TareWT = Convert.ToInt32(ddlContWeight.SelectedValue);
            objStuffingDetails.IsSub = chkSub.Checked;
            objStuffingDetails.StuffCategory = Convert.ToInt32(ddlStuffCat.SelectedValue);
            objStuffingDetails.DayNightShift = Convert.ToInt32(ddlShift.SelectedIndex);
            objStuffingDetails.Location = Convert.ToInt32(ddlLocation.SelectedValue);
            objStuffingDetails.StuffedById = user.UserId;
            objStuffingDetails.BookingNo = txtBookingNo.Text.Trim();
            objStuffingDetails.VGMWeight = txtVGMWeight.Text.Trim();
            objStuffingDetails.CarrierName = txtCarrier.Text.Trim();
            objStuffingDetails.Status = Convert.ToInt32(ddlStatus.SelectedIndex);
            objStuffingDetails.ExpNo = txtExpNo.Text.Trim();
            objStuffingDetails.ExpDate = dateExpire.Value;
            objStuffingDetails.NegotiatingBank = Convert.ToInt32(ddlNegoBank.SelectedValue);
            objStuffingDetails.PortofLoading = Convert.ToInt32(ddlPoLo.SelectedValue);
            objStuffingDetails.PortofDischarge = Convert.ToInt32(ddlPodo.SelectedValue);
            objStuffingDetails.StuffingDate = dateStuffing.Value;
            objStuffingDetails.SizeId = SizeId;
            objStuffingDetails.TypeId = TypeId;


        }


        #endregion


        private void btnClose_Click(object sender, EventArgs e)
        {
            ClearStuffing();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearStuffing();
            ddlStuffCat.SelectedIndex = 0;
            ddlShift.SelectedIndex = 0;
            ddlLocation.SelectedIndex = 0;
            txtBookingNo.Text = "";
            ddlStatus.SelectedIndex = 0;
            txtVGMWeight.Text = "";
            txtCarrier.Text = "";
            txtCarrier.AutoCompleteCustomSource = null;
            txtExpNo.Text = "";
            ddlNegoBank.SelectedIndex = 0;
            ddlPoLo.SelectedIndex = 0;
            ddlPodo.SelectedIndex = 0;
        }

        private void ClearStuffing()
        {
            ReferContainer = false;
            ddlClient.SelectedIndex = 0;
            ddlClient.Enabled = true;
            ddlContainerNo.DataSource = null;
            ddlContainerNo.Items.Clear();
            ddlContainerNo.Refresh();
            lblSizeType.Text = "";
            txtRefNo.Text = "";
            txtSealNo.Text = "";
            ddlContWeight.SelectedIndex = 0;
            chkSub.Checked = false;

            lblPluginDate.Visible = false;
            lblPTIDate.Visible = false;
            datePlugIn.Visible = false;
            datePTI.Visible = false;

            //ddlStuffCat.SelectedIndex = 0;
            //ddlShift.SelectedIndex = 0;
            //ddlLocation.SelectedIndex = 0;           
            //txtBookingNo.Text = "";
            //ddlStatus.SelectedIndex = 0;
            //txtVGMWeight.Text = "";
            //txtCarrier.Text = "";
            //txtCarrier.AutoCompleteCustomSource = null;
            //txtExpNo.Text = "";
            dateExpire.Value = DateTime.Now;

            //ddlNegoBank.SelectedIndex = 0;
            //ddlPoLo.SelectedIndex = 0;
            //ddlPodo.SelectedIndex = 0;
            dateStuffing.Value = DateTime.Now;
            dataGridView1.ClearSelection();
            objStuffingDetails = new StuffingDetail();
            objCargoDetails = new CargoDetail();
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            btnSave.Enabled = false;

        }

        private void StauffingDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            objStuffingDetails = new StuffingDetail();
            objCargoDetails = new CargoDetail();
        }

        private void ddlClient_SelectedValueChanged(object sender, EventArgs e)
        {
          
        }

        private void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(ddlClient.SelectedValue) > 0)
            if (ddlClient.SelectedIndex != 0)
            {
                int clientId = Convert.ToInt32(ddlClient.SelectedValue);
                LoadCSDContainerByClientId(clientId);
                ddlContainerNo.Enabled = true;
            }
            else
            {

                ddlContainerNo.DataSource = null;
                ddlContainerNo.Enabled = false;
                txtRefNo.Text = "";

            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {

        }

        private void ddlContainerNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ddlContainerNo_Leave(object sender, EventArgs e)
        {
           
        }

        private void ddlContainerNo_CursorChanged(object sender, EventArgs e)
        {

        }

        /*
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Stuffing  deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                var status = stuffingBll.Delete(objStuffingDetails);
                MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCargoReceive();

            }
        }
        */
    }

}