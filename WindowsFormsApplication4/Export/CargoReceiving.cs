using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LOGISTIC.BLL;
using LOGISTIC.Export.BLL;
using System.Collections.Generic;

namespace LOGISTIC.UI.Export
{
    public partial class CargoReceiving : Form
    {

        private static CargoRecieving objCargoReceiving = new CargoRecieving();

        private CustomerBll MLOBll = new CustomerBll();
        private ShipperBLL shipperBll = new ShipperBLL();
        private ConsigneeBll consigneeBll = new ConsigneeBll();
        private FreightForwarderBLL forwarderBll = new FreightForwarderBLL();
        private ClearAndForwaderBll cafBll = new ClearAndForwaderBll();       
        private PortBLL portBll = new PortBLL();
        private LocationBLL locationBll = new LocationBLL();
       // private UserBLL userBll = new UserBLL();
        private CommodityBLL commodityBll = new CommodityBLL();
        private CargoReceivingBLL cargoBll = new CargoReceivingBLL();
        List<Shipper> objShipperlist = new List<Shipper>();
        private UserInfo user;
     
        //private static int cargoDetailsSL = 1;

        private static int CartoonId = 1;
        private static int index;

        public CargoReceiving(UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;
            SetMask();
            lblReceiver.Text = user.FirstName + " " + user.LastName;
            txtCartoonId.Text = Convert.ToString(CartoonId);
            pnlGarments.Visible = false;
           

        }


        public CargoReceiving(CargoRecieving objCR, UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            objCargoReceiving = objCR;
            this.user = user;
            SetMask();
            lblReceiver.Text = user.FirstName + " " + user.LastName;
            CartoonId = CartoonId + objCR.CargoDetails.Count;
            txtCartoonId.Text = Convert.ToString(CartoonId);
            pnlGarments.Visible = false;
            btnSave.Text = "Update";

        }


        private void CargoReceiving_Load(object sender, EventArgs e)
        {
            objShipperlist = shipperBll.Getall();
            LoadCustomer();
            LoadShipper();
            LoadConsignee();
            LoadForwarder();
            LoadCAF();
            LoadPort();
            LoadLocation();
            LoadDestination();
            LoadCommodity();
            LoadVAT();
            PrepareGrid();
           
            if (objCargoReceiving.CargoReceiveId > 0)
            {
                BindCRDatatoFIeld(objCargoReceiving);             
                if (objCargoReceiving.CargoDetails.Count > 0)
                {

                    foreach (CargoDetail item in objCargoReceiving.CargoDetails)
                    {
                        dataGridView1.Rows.Add(item.CartoonId, item.Packages, item.Pieces, item.CubicMeter, item.CargoWeight);                                                                      
                    }
                    dataGridView1.ClearSelection();
                    dataGridView1.AllowUserToAddRows = false;
                }

            }
            else
            {
                btnDelete.Enabled = false;
            }

            btnDeleteCargoDetails.Enabled = false;

        }


        private void BindCRDatatoFIeld(CargoRecieving objCR)
        {

            ddlClient.SelectedValue = objCR.CustId;
            txtSLNumber.Text = objCR.SLNo;
            dateReceiving.Value = Convert.ToDateTime(objCR.ReceivingDate);
            ddlShipper.SelectedValue = objCR.ShipperId;
            ddlConsignee.SelectedValue = objCR.ConsigneeId;
            ddlForwarder.SelectedValue = objCR.FredForwarderId;
            ddlCandF.SelectedValue = objCR.ClearForwarderId;
            txtEFRNo.Text = objCR.EFRNo;
            dateEFR.Value = Convert.ToDateTime(objCR.EFRDate);
            txtLOT.Text = Convert.ToString(objCR.Lot);
            dateLOTClosing.Value = Convert.ToDateTime(objCR.LotClosingDate);
            ddlLocation.SelectedValue = objCR.Location;
            ddlDestination.SelectedValue = objCR.Destination;
            txtDcode.Text = objCR.DCode;
            ddlCommodity.SelectedValue = Convert.ToInt32(objCR.CommodityId);
            ddlPortofReceived.SelectedValue = Convert.ToInt32(objCR.PORId);
            dateDocReceived.Value = Convert.ToDateTime(objCR.DocReceiveDate);
            //ddlUser.SelectedValue = Convert.ToInt32(objCR.ReceivedBy);
            ddlVAT.SelectedIndex = Convert.ToInt32(objCR.VAT);
            txtRemarks.Text = objCR.Remarks;
            txtCBM.Focus();

        }


        #region Load Basic Data


        public void SetMask()
        {

            txtLOT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtLOT.Properties.Mask.EditMask = "\\d+";

            txtLength.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtLength.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtHeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtHeight.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtWidth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtWidth.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";

            txtPackage.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtPackage.Properties.Mask.EditMask = "\\d+";

            txtPieces.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtPieces.Properties.Mask.EditMask = "\\d+";

            txtweight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtweight.Properties.Mask.EditMask = @"-?\d+(\R.\d{0,2})?";


        }


        private void LoadCustomer()
        {

            var type = MLOBll.Getall();          
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Customer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlClient.DataSource = dt_Types;
                ddlClient.DisplayMember = "t_Name";
                ddlClient.ValueMember = "t_ID";
            }
            ddlClient.SelectedIndex = 0;

        }

        private void LoadShipper()
        {

            objShipperlist = shipperBll.Getall();
            //objShipperlist.Add(0, "");
            //ddlShipper.Items.Add("Select");
            ddlShipper.DataSource = objShipperlist;
            ddlShipper.DisplayMember = "ShipperName";
            ddlShipper.ValueMember = "ShipperId";
            /*
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ShipperId, t.ShipperName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Shipper--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlShipper.DataSource = dt_Types;
                ddlShipper.DisplayMember = "t_Name";
                ddlShipper.ValueMember = "t_ID";
            }
            ddlShipper.SelectedIndex = 0;
            */

        }

        private void LoadConsignee()
        {

            var type = consigneeBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ConsigneeId, t.ConsigneeName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Consignee --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlConsignee.DataSource = dt_Types;
                ddlConsignee.DisplayMember = "t_Name";
                ddlConsignee.ValueMember = "t_ID";
            }
            ddlConsignee.SelectedIndex = 0;

        }

        private void LoadForwarder()
        {

            var type = forwarderBll.Getall();          
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.FreightForwarderId, t.FreightForwarderName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Forwarder --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlForwarder.DataSource = dt_Types;
                ddlForwarder.DisplayMember = "t_Name";
                ddlForwarder.ValueMember = "t_ID";
            }
            ddlForwarder.SelectedIndex = 0;

        }

        private void LoadCAF()
        {

            var type = cafBll.Getall();          
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ClearAndForwadingAgentId, t.CFAgentName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Clear & Forwarder --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlCandF.DataSource = dt_Types;
                ddlCandF.DisplayMember = "t_Name";
                ddlCandF.ValueMember = "t_ID";
            }
            ddlCandF.SelectedIndex = 0;

        }

        private void LoadPort()
        {

            var type = portBll.Getall();           
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.PortOfLandId, t.PortCode.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Port --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlPortofReceived.DataSource = dt_Types;
                ddlPortofReceived.DisplayMember = "t_Name";
                ddlPortofReceived.ValueMember = "t_ID";
            }
            ddlPortofReceived.SelectedIndex = 0;

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
            dr[1] = "-- Select Location --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlLocation.DataSource = dt_Types;
                ddlLocation.DisplayMember = "t_Name";
                ddlLocation.ValueMember = "t_ID";
            }
            ddlLocation.SelectedIndex = 0;

        }


        private void LoadDestination()
        {

            var type = portBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.PortOfLandId, t.PortName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Destination --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlDestination.DataSource = dt_Types;
                ddlDestination.DisplayMember = "t_Name";
                ddlDestination.ValueMember = "t_ID";
            }
            ddlDestination.SelectedIndex = 0;

        }

        private void LoadCommodity()
        {

            var type = commodityBll.Getall();          
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CommodityId, t.CommodityName.Trim());
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Commodity --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlCommodity.DataSource = dt_Types;
                ddlCommodity.DisplayMember = "t_Name";
                ddlCommodity.ValueMember = "t_ID";
            }
            ddlCommodity.SelectedIndex = 0;

        }
       
        private void LoadVAT()
        {
            ddlVAT.Items.Insert(0, "Void");
            ddlVAT.Items.Insert(1, "Yes");
            ddlVAT.Items.Insert(2, "No");
            ddlVAT.SelectedIndex = 0;

        }

        public void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].HeaderText = "Cartoon ID";

            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[1].HeaderText = "Packages";

            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[2].HeaderText = "Pieces";

            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[3].HeaderText = "CBM";

            // dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Cargo Weight";

        }

        #endregion


        #region Cargo Receiving

        private void ddlClient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ddlClient.SelectedIndex > 0)
            {
                DateTime ReceivingDate = dateReceiving.Value;
                var CustId = Convert.ToInt32(ddlClient.SelectedValue);
                string SLNo = cargoBll.GetCustSLNo(CustId, ReceivingDate);
                txtSLNumber.Text = SLNo;

            }
            else
            {
                txtSLNumber.Text = "";
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (btnSave.Text == "Save")
            {
                bool flag = ValidateCargoReceive();
                if (flag == true)
                {
                    if (objCargoReceiving.CargoDetails.Count == 0)
                    {

                        MessageBox.Show("No Master details data !!", "Cargo Details Required",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }
                    FillingCargoReceiveData();
                    var status = cargoBll.Insert(objCargoReceiving);
                    MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearCargoReceive();
                   

                }
            }
            else if (btnSave.Text == "Update")
            {
                bool flag = ValidateCargoReceive();
                if (flag == true)
                {
                    FillingCargoReceiveData();
                   var status = cargoBll.Update(objCargoReceiving);
                   MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ClearCargoReceive();
            }



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Cargo Receiving deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                var status = cargoBll.Delete(objCargoReceiving);
                MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCargoReceive();
                               
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearCargoReceive();
            ClearCargoDetails();
                      
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ClearCargoReceive();
            ClearCargoDetails();
            Close();

        }

        private void CargoReceiving_FormClosed(object sender, FormClosedEventArgs e)
        {
            objCargoReceiving = new CargoRecieving();
            //ClearCargoReceive();
            //ClearCargoDetails();

        }

        private bool ValidateCargoReceive()
        {

            var errMessage = "";

            if (Convert.ToInt32(ddlClient.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a Client !!\n";
            }
            if (Convert.ToInt32(ddlShipper.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a Shipper !!\n";
            }
            if (Convert.ToInt32(ddlConsignee.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a Consignee !!\n";
            }
            //if (Convert.ToInt32(ddlForwarder.SelectedValue) == 0)
            //{
            //    errMessage = errMessage + "* Please select a Forwarder !!\n";
            //}
            if (Convert.ToInt32(ddlCandF.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a C&F agent !!\n";
            }
            if (txtEFRNo.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter EFR number !!\n";
            }
            if (txtLOT.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter LOT number !!\n";
            }           
            if (Convert.ToInt32(ddlLocation.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select a  loaction !!\n";
            }
            if (Convert.ToInt32(ddlDestination.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select destination !!\n";
            }
            if (Convert.ToInt32(ddlCommodity.SelectedValue) == 0)
            {
                errMessage = errMessage + "* Please select Commodity !!\n";
            }
            //if (Convert.ToInt32(ddlPortofReceived.SelectedValue) == 0)
            //{
            //    errMessage = errMessage + "* Please select port !!\n";
            //}           
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


        private void FillingCargoReceiveData()
        {

            objCargoReceiving.CustId = Convert.ToInt32(ddlClient.SelectedValue);
            objCargoReceiving.SLNo = txtSLNumber.Text.Trim();
            objCargoReceiving.ReceivingDate = Convert.ToDateTime(dateReceiving.Value);
            objCargoReceiving.ShipperId = Convert.ToInt32(ddlShipper.SelectedValue);
            objCargoReceiving.ConsigneeId = Convert.ToInt32(ddlConsignee.SelectedValue);
            objCargoReceiving.FredForwarderId = Convert.ToInt32(ddlForwarder.SelectedValue);
            objCargoReceiving.ClearForwarderId = Convert.ToInt32(ddlCandF.SelectedValue);
            objCargoReceiving.EFRNo = txtEFRNo.Text.Trim();
            objCargoReceiving.EFRDate = Convert.ToDateTime(dateEFR.Value);
            objCargoReceiving.Lot =Convert.ToInt32(txtLOT.Text.Trim());
            objCargoReceiving.LotClosingDate = Convert.ToDateTime(dateLOTClosing.Value);

            objCargoReceiving.Location = Convert.ToInt32(ddlLocation.SelectedValue);
            objCargoReceiving.Destination = Convert.ToInt32(ddlDestination.SelectedValue);

            objCargoReceiving.DCode = txtDcode.Text.Trim();
            objCargoReceiving.CommodityId = Convert.ToInt32(ddlCommodity.SelectedValue);
            objCargoReceiving.PORId = Convert.ToInt32(ddlPortofReceived.SelectedValue);
            objCargoReceiving.DocReceiveDate = Convert.ToDateTime(dateDocReceived.Value);
            objCargoReceiving.VAT = Convert.ToInt32(ddlVAT.SelectedIndex);
            objCargoReceiving.ReceivedBy = user.UserId;
            //objCargoReceiving.ReceivedBy = Convert.ToInt32(ddlUser.SelectedValue);
            objCargoReceiving.GrossWeight = Convert.ToDecimal(txtGrossWeight.Text.Trim());
            objCargoReceiving.Remarks = txtRemarks.Text.Trim();

            
        }


        private void ClearCargoReceive()
        {

            ddlClient.SelectedIndex = 0;
            txtSLNumber.Text = "";
            dateReceiving.Value = DateTime.Now;
            ddlShipper.SelectedIndex = 0;
            ddlConsignee.SelectedIndex = 0;
            ddlForwarder.SelectedIndex = 0;
            ddlCandF.SelectedIndex = 0;           
            txtEFRNo.Text = "";
            dateEFR.Value = DateTime.Now;
            txtLOT.Text = "";
            dateLOTClosing.Value = DateTime.Now;
            ddlLocation.SelectedIndex = 0;
            ddlDestination.SelectedIndex = 0;
            txtDcode.Text = "";
            ddlCommodity.SelectedIndex = 0;
            ddlPortofReceived.SelectedIndex = 0;
            dateDocReceived.Value = DateTime.Now;
            //ddlUser.SelectedValue = user.UserId;
            ddlVAT.SelectedIndex = 0;
            txtRemarks.Text = "";
            CartoonId = 1;
            txtCartoonId.Text =Convert.ToString(CartoonId);
            objCargoReceiving = new CargoRecieving();
            dataGridView1.Rows.Clear();
            btnSave.Text = "Save";
            btnDelete.Enabled = false;                    

        }

       

        #endregion




        #region Cargo Details

       
        private void btnAddCargoDetails_Click(object sender, EventArgs e)
        {
            bool flag = ValidateCargoDetails();
            if (flag == true)
            {
                CargoDetail ObjCargoDetails = FillingObjCargoDetails();

                if (btnAddCargoDetails.Text == "Add")
                {
                    objCargoReceiving.CargoDetails.Add(ObjCargoDetails);
                    var sumpack = objCargoReceiving.CargoDetails.Sum(x => x.Packages);
                    var sumWeight = objCargoReceiving.CargoDetails.Sum(x => x.CargoWeight);
                    Boolean Add = true;
                    if (sumWeight > Convert.ToDecimal(txtGrossWeight.Text))
                    {
                        DialogResult result = MessageBox.Show("Weight is More then Gross Weith !!!!!!!");
                        Add = false;
                    }
                    if (sumpack > Convert.ToInt64(txtLOT.Text))
                    {
                        DialogResult result = MessageBox.Show("Singl Package is More then Total LOT !!!!!!!");
                        Add = false;
                    }
                    if (Add == true)
                    {
                        BindDataToGrid(ObjCargoDetails);
                    }
                    CartoonId = CartoonId + 1;
                }
                else
                {
                    //update Cargo details
                    objCargoReceiving.CargoDetails.ElementAt(index).CartoonId = ObjCargoDetails.CartoonId;
                    objCargoReceiving.CargoDetails.ElementAt(index).ReceiveDate = ObjCargoDetails.ReceiveDate;
                    objCargoReceiving.CargoDetails.ElementAt(index).Length = ObjCargoDetails.Length;
                    objCargoReceiving.CargoDetails.ElementAt(index).Width = ObjCargoDetails.Width;
                    objCargoReceiving.CargoDetails.ElementAt(index).Height = ObjCargoDetails.Height;
                    objCargoReceiving.CargoDetails.ElementAt(index).Packages = ObjCargoDetails.Packages;
                    objCargoReceiving.CargoDetails.ElementAt(index).Pieces = ObjCargoDetails.Pieces;
                    objCargoReceiving.CargoDetails.ElementAt(index).CubicMeter = ObjCargoDetails.CubicMeter;
                    objCargoReceiving.CargoDetails.ElementAt(index).IsPCS = ObjCargoDetails.IsPCS;
                    objCargoReceiving.CargoDetails.ElementAt(index).IsCBM = ObjCargoDetails.IsCBM;
                    objCargoReceiving.CargoDetails.ElementAt(index).IsKGs = ObjCargoDetails.IsKGs;
                    objCargoReceiving.CargoDetails.ElementAt(index).CargoWeight = ObjCargoDetails.CargoWeight;
                    objCargoReceiving.CargoDetails.ElementAt(index).IsGarmentItem = ObjCargoDetails.IsGarmentItem;
                    objCargoReceiving.CargoDetails.ElementAt(index).SONumber = ObjCargoDetails.SONumber;
                    objCargoReceiving.CargoDetails.ElementAt(index).PONumber = ObjCargoDetails.PONumber;
                    objCargoReceiving.CargoDetails.ElementAt(index).Item = ObjCargoDetails.Item;
                    objCargoReceiving.CargoDetails.ElementAt(index).TPN = ObjCargoDetails.TPN;
                    objCargoReceiving.CargoDetails.ElementAt(index).Style = ObjCargoDetails.Style;

                    objCargoReceiving.CargoDetails.ElementAt(index).REF = ObjCargoDetails.REF;
                    objCargoReceiving.CargoDetails.ElementAt(index).CAT = ObjCargoDetails.CAT;
                    objCargoReceiving.CargoDetails.ElementAt(index).InvoiceNo = ObjCargoDetails.InvoiceNo;
                    objCargoReceiving.CargoDetails.ElementAt(index).DIV = ObjCargoDetails.DIV;
                    objCargoReceiving.CargoDetails.ElementAt(index).DEPT = ObjCargoDetails.DEPT;
                    objCargoReceiving.CargoDetails.ElementAt(index).Remarks = ObjCargoDetails.Remarks;


                    UpdateGridRow(index, ObjCargoDetails);
                }

                ClearCargoDetails();
                txtLength.Focus();
                txtCartoonId.Text = Convert.ToString(CartoonId);

            }

        }

        private void btnDeleteCargoDetails_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Cargo Details deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                var obj = objCargoReceiving.CargoDetails.ElementAt(index);
                objCargoReceiving.CargoDetails.Remove(obj);
                dataGridView1.Rows.RemoveAt(index);
                ClearCargoDetails();

            }

        }

        private void btnCalculateCBM_Click(object sender, EventArgs e)
        {
            var errMessage = "";

            if (txtLength.Text == "")
            {
                errMessage = errMessage + "* Please enter length !!\n";
                txtLength.Focus();
            }
            if (txtWidth.Text == "")
            {
                errMessage = errMessage + "* Please enter width !!\n";
                txtWidth.Focus();
            }
            if (txtHeight.Text == "")
            {
                errMessage = errMessage + "* Please enter height !!\n";
                txtHeight.Focus();
            }
            if (errMessage != "")
            {
                MessageBox.Show(errMessage, "Input required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                decimal length = Convert.ToDecimal(txtLength.Text.Trim());
                decimal width = Convert.ToDecimal(txtWidth.Text.Trim());
                decimal height = Convert.ToDecimal(txtHeight.Text.Trim());
                var cbm = Convert.ToDecimal((length * width * height));
                var packages = txtPackage.Text.Trim();

                if (packages != "")
                {
                    txtCBM.Text = (cbm * Convert.ToInt32(packages)).ToString();
                }
                else
                {
                    txtCBM.Text = cbm.ToString();
                }
                
            }

        }

        private bool ValidateCargoDetails()
        {
            var errMessage = "";

            if (txtCartoonId.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter Cartoon Id !!\n";
            }
            if (txtPackage.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter packages quantity !!\n";
            }
            if (txtPieces.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter no of pieces !!\n";
            }
            /* This part is Blocked due to Mr Sawwan Requirements
            if (txtCBM.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Cubic Meter (CBM) value needed !!\n";
            }
            if (txtweight.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter cargo weight !!\n";
            }
            if (txtSONo.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter S/O number !!\n";
            }
            if (txtInvoice.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter invoice number !!\n";
            }
            */
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

        private CargoDetail FillingObjCargoDetails()
        {

            CargoDetail ObjCargoDetails = new CargoDetail();

            ObjCargoDetails.CargoReceiveId = objCargoReceiving.CargoReceiveId;
            // ObjCargoDetails.CartoonId = txtCartoonId.Text.Trim();
            ObjCargoDetails.CartoonId = Convert.ToString(CartoonId);
            ObjCargoDetails.ReceiveDate = dateReceivingDetails.Value;
            if (txtLength.Text.Trim() != string.Empty)
            { ObjCargoDetails.Length = Convert.ToDecimal(txtLength.Text.Trim()); }
            else
            {
                ObjCargoDetails.Length = null;
            }
            if (txtWidth.Text.Trim() != string.Empty)
            { ObjCargoDetails.Width = Convert.ToDecimal(txtWidth.Text.Trim()); }
            else
            {
                ObjCargoDetails.Width = null;
            }
            if (txtHeight.Text.Trim() != string.Empty)

            { ObjCargoDetails.Height = Convert.ToDecimal(txtHeight.Text.Trim()); }
            else
            {
                ObjCargoDetails.Height = null;
            }

            if (txtCBM.Text.Trim() != string.Empty)
            { ObjCargoDetails.CubicMeter = Convert.ToDecimal(txtCBM.Text.Trim()); }
            else
            {
                ObjCargoDetails.CubicMeter = null;
            }

            ObjCargoDetails.Packages = Convert.ToInt32(txtPackage.Text.Trim());
            ObjCargoDetails.Pieces = Convert.ToInt32(txtPieces.Text.Trim());
           
            ObjCargoDetails.IsPCS = chkPcs.Checked;
            ObjCargoDetails.IsCBM = chkCBM.Checked;
            ObjCargoDetails.IsKGs = chkKGs.Checked;
            //ObjCargoDetails.CubicMeter = Convert.ToDecimal(txtCBM.Text.Trim());
            ObjCargoDetails.CargoWeight = Convert.ToDecimal(txtweight.Text.Trim());

            if (chkGarmentsItem.Checked == true)
            {
                ObjCargoDetails.IsGarmentItem = chkGarmentsItem.Checked;
                ObjCargoDetails.SONumber = txtSONo.Text.Trim();
                ObjCargoDetails.PONumber = txtPONo.Text.Trim();
                ObjCargoDetails.Item = txtItem.Text.Trim();
                ObjCargoDetails.TPN = txtTPN.Text.Trim();
                ObjCargoDetails.Style = txtStyle.Text.Trim();
                ObjCargoDetails.REF = txtREF.Text.Trim();
                ObjCargoDetails.CAT = txtCAT.Text.Trim();
                ObjCargoDetails.InvoiceNo = txtInvoice.Text.Trim();
                ObjCargoDetails.DIV = txtDIV.Text.Trim();
                ObjCargoDetails.DEPT = txtDEPT.Text.Trim();
                ObjCargoDetails.Remarks = txtRemarksDetails.Text.Trim();

            }

           
           
            return ObjCargoDetails;


        }

        private void BindDataToGrid(CargoDetail ObjCargoDetails)
        {
          
            dataGridView1.Rows.Add(ObjCargoDetails.CartoonId, ObjCargoDetails.Packages, ObjCargoDetails.Pieces, ObjCargoDetails.CubicMeter, ObjCargoDetails.CargoWeight);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();

        }


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            index = Convert.ToInt32(selectedRow.Index);
            var data = objCargoReceiving.CargoDetails.ElementAt(index);

            txtCartoonId.Text = data.CartoonId;
            dateReceivingDetails.Value = Convert.ToDateTime(data.ReceiveDate);
            txtLength.Text = Convert.ToString(data.Length);
            txtWidth.Text = Convert.ToString(data.Width);
            txtHeight.Text = Convert.ToString(data.Height);
            txtPackage.Text = Convert.ToString(data.Packages);
            txtPieces.Text = Convert.ToString(data.Pieces);
            txtCBM.Text = Convert.ToString(data.CubicMeter);

            chkPcs.Checked =Convert.ToBoolean(data.IsPCS);
            chkCBM.Checked = Convert.ToBoolean(data.IsCBM);
            chkKGs.Checked = Convert.ToBoolean(data.IsKGs);

            txtweight.Text = Convert.ToString(data.CargoWeight);

            if (data.IsGarmentItem == true)
            {
                pnlGarments.Visible = true;
                chkGarmentsItem.Checked = Convert.ToBoolean(data.IsGarmentItem);
                txtSONo.Text = Convert.ToString(data.SONumber);
                txtPONo.Text = Convert.ToString(data.PONumber);
                txtItem.Text = Convert.ToString(data.Item);
                txtTPN.Text = Convert.ToString(data.TPN);
                txtStyle.Text = Convert.ToString(data.Style);
                txtREF.Text = Convert.ToString(data.REF);
                txtCAT.Text = Convert.ToString(data.CAT);
                txtInvoice.Text = Convert.ToString(data.InvoiceNo);
                txtDIV.Text = Convert.ToString(data.DIV);
                txtDEPT.Text = Convert.ToString(data.DEPT);
                txtRemarksDetails.Text = Convert.ToString(data.Remarks);
            }
            else
            {
                chkGarmentsItem.Checked = Convert.ToBoolean(data.IsGarmentItem);
                pnlGarments.Visible = false;

            }         

            btnAddCargoDetails.Text = "Update";
            btnDeleteCargoDetails.Enabled = true;

        }


        private void UpdateGridRow(int index, CargoDetail ObjCargoDetails)
        {

            dataGridView1.Rows[index].Cells[1].Value = ObjCargoDetails.CartoonId;
            dataGridView1.Rows[index].Cells[2].Value = ObjCargoDetails.Packages;
            dataGridView1.Rows[index].Cells[3].Value = ObjCargoDetails.CubicMeter;
            dataGridView1.Rows[index].Cells[4].Value = ObjCargoDetails.CargoWeight;


            dataGridView1.ClearSelection();

        }


        private void ClearCargoDetails()
        {

            txtCartoonId.Text = Convert.ToString(CartoonId);
            dateReceivingDetails.Value=DateTime.Now;
            txtLength.Text = "";
            txtWidth.Text = "";
            txtHeight.Text = "";
            txtPackage.Text = "";
            txtPieces.Text = "";
            txtCBM.Text = "";
            chkPcs.Checked = false;
            chkCBM.Checked = false;
            chkKGs.Checked = false;
            chkGarmentsItem.Checked = false;
            txtCBM.Text = "";
            txtweight.Text = "";
            txtSONo.Text = "";
            txtPONo.Text = "";
            txtItem.Text = "";
            txtTPN.Text = "";
            txtStyle.Text = "";
            txtREF.Text = "";
            txtCAT.Text = "";
            txtInvoice.Text = "";
            txtDIV.Text = "";
            txtDEPT.Text = "";
            txtRemarksDetails.Text = "";
            pnlGarments.Visible = false;
            btnAddCargoDetails.Text = "Add";
            btnDeleteCargoDetails.Enabled = false;
            dataGridView1.ClearSelection();

        }


        private void chkGarmentsItem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGarmentsItem.Checked == true)
            {
                pnlGarments.Visible = true;

            }
            else
            {
                pnlGarments.Visible = false;
            }
        }


        #endregion

        private void ddlClient_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.DroppedDown = true;
            string strFindStr = "";
            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }
            int intIdx = -1;
            // Search the string in the ComboBox list.
            intIdx = cb.FindString(strFindStr);
            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
                e.Handled = true;
        }

        
        private void ddlShipper_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string ss = ddlShipper.Text.ToString();
            //List<Shipper> objlist = new List<Shipper>();

            //objlist  = (List<Shipper>)objShipperlist.Where(x =>x.ShipperName.Contains(ss));
            /*
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ShipperId, t.ShipperName.Trim());
            }
            //DataRow dr = dt_Types.NewRow();
            //dr[0] = 0;
            //dr[1] = "-- Select Shipper--";
           // dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlShipper.DataSource = dt_Types;
                ddlShipper.DisplayMember = "t_Name";
                ddlShipper.ValueMember = "t_ID";
            }
            ddlShipper.SelectedIndex = 0;

            */
            //bool blnLimitToList = true;
            //AutoComplete(ddlShipper,  e, blnLimitToList);
        }
        // AutoComplete
        public void AutoComplete(ComboBox cb, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.AutoComplete(cb, e, false);
        }

        public void AutoComplete(ComboBox cb,
               System.Windows.Forms.KeyPressEventArgs e, bool blnLimitToList)
        {
            string strFindStr = "";

            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }

            int intIdx = -1;

            // Search the string in the ComboBox list.

            intIdx = cb.FindString(strFindStr);

            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
            {
                e.Handled = blnLimitToList;
            }
        }

        private void ddlShipper_TextChanged(object sender, EventArgs e)
        {
            //if (ddlShipper.SelectedText != "")
            //{ 
            //string ddlFiter = ddlShipper.Text.ToString();
            //List<Shipper> objlist = new List<Shipper>();

            //if (ddlFiter != string.Empty)
            //{
            //    objlist = (List<Shipper>)objShipperlist.Where(x => x.ShipperName.Contains(ddlFiter));
            //}
            //}


            /*
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ShipperId, t.ShipperName.Trim());
            }
            //DataRow dr = dt_Types.NewRow();
            //dr[0] = 0;
            //dr[1] = "-- Select Shipper--";
           // dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlShipper.DataSource = dt_Types;
                ddlShipper.DisplayMember = "t_Name";
                ddlShipper.ValueMember = "t_ID";
            }
            ddlShipper.SelectedIndex = 0;

            */
            //bool blnLimitToList = true;
            //AutoComplete(ddlShipper,  e, blnLimitToList);
        }
    }
}
