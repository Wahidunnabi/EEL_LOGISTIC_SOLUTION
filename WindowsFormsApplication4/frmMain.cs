using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using LOGISTIC.UI.Administration;
using LOGISTIC.UI.Import;
using LOGISTIC.UI.Report;
using LOGISTIC.UI.Export;
using LOGISTIC.UI.Billing;
using System.IO;
using LOGISTIC.UI.Report.Accounts;

namespace LOGISTIC.UI
{
    public partial class frmMain : Form
    {
        //To move borderless form with a control
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        //End
        public static extern bool ReleaseCapture();

        private List<UserPermissionMapping> listPermission = new List<UserPermissionMapping>();
        UserInfo user;
        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(UserInfo user, List<UserPermissionMapping> listPermission)
        {
            InitializeComponent();
            this.user = user;
            this.listPermission = listPermission;
            lblUserName.Text = user.FirstName + " " + user.LastName;

            if (user.UserType==1)
            {
                lbluserType.Text = "Administrator";
                
            }
            if (user.UserType == 2)
            {
                lbluserType.Text = "General Manager";
                AdminModule.Visible = false;

            }
            if (user.UserType == 3)
            {
                lbluserType.Text = "Operation User";
                AdminModule.Visible = false;
            }
            if (user.UserType == 4)
            {
                lbluserType.Text = "Accounts User";
                AdminModule.Visible = false;
            }
            if (user.Image != null)
            {
                MemoryStream ms = new MemoryStream(user.Image);
                imgUser.Image = Image.FromStream(ms);
            }

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            
            MdiClient ctlMDI;

            // Loop through all of the form's controls looking
            // for the control of type MdiClient.

            foreach (Control ctl in this.Controls)
            {
                try
                {

                    ctlMDI = (MdiClient)ctl;
                    ctlMDI.BackColor = this.BackColor;
                }
                catch
                {

                }
            }

           
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

        }


        #region BASIC SETUP


        private void btlClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Log Out ??",
                       "Confirm Log Out",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {


                Application.Exit();        
                this.Close();
                frmLogIn f = new frmLogIn();
                f.Show();

            }
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal))
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
     

        private void navMLO_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(CustomerEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            CustomerEntry f = new CustomerEntry();
            f.MdiParent = this;
            f.Show();
        }
      

        private void navDepot_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(DepotUI))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            DepotUI f = new DepotUI();
            f.MdiParent = this;
            f.Show();
        }

        private void navTrailer_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(TrailerUI))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            TrailerUI f = new TrailerUI();
            f.MdiParent = this;
            f.Show();
        }

        private void navContainerSize_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
            {
            
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ContainerSizeUI))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ContainerSizeUI f = new ContainerSizeUI();
            f.MdiParent = this;
            f.Show();

        }

        private void navGrossWeight_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ContainerWeightUI))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ContainerWeightUI f = new ContainerWeightUI();
            f.MdiParent = this;
            f.Show();

        }

        private void navTrailerNumber_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(TrailerNumberUI))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            TrailerNumberUI f = new TrailerNumberUI();
            f.MdiParent = this;
            f.Show();
        }

        private void navContainerType_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ContainerTypeUI))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ContainerTypeUI f = new ContainerTypeUI();
            f.MdiParent = this;
            f.Show();
        }

        private void navImporter_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ImporterEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ImporterEntry f = new ImporterEntry();
            f.MdiParent = this;
            f.Show();

        }

        private void navUnitMeasurement_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(MeasurementUnitEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            MeasurementUnitEntry f = new MeasurementUnitEntry();
            f.MdiParent = this;
            f.Show();

        }

        private void navVessel_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(VasselEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            VasselEntry f = new VasselEntry();
            f.MdiParent = this;
            f.Show();

        }

        private void navCandF_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ClearingAndForwaderEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ClearingAndForwaderEntry f = new ClearingAndForwaderEntry();
            f.MdiParent = this;
            f.Show();

        }

        private void navConsignee_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ConsineeEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ConsineeEntry f = new ConsineeEntry();
            f.MdiParent = this;
            f.Show();
        }

        private void navCommodity_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(CommodityEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            CommodityEntry f = new CommodityEntry();
            f.MdiParent = this;
            f.Show();

        }

        private void navPort_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(PortEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            PortEntry f = new PortEntry();
            f.MdiParent = this;
            f.Show();

        }

        private void navShipper_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ShipperEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ShipperEntry f = new ShipperEntry();
            f.MdiParent = this;
            f.Show();

        }

        private void navFreightForwarder_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(FreightForwaderEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            FreightForwaderEntry f = new FreightForwaderEntry();
            f.MdiParent = this;
            f.Show();

        }

        private void navUser_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(CreateUser))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            CreateUser f = new CreateUser();
            f.MdiParent = this;
            f.Show();
        }

        private void navLocation_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(LocationEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            LocationEntry f = new LocationEntry();
            f.MdiParent = this;
            f.Show();
        }

        private void navBank_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(BankEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            BankEntry f = new BankEntry();
            f.MdiParent = this;
            f.Show();

        }


        private void navISO_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ISOMappingUI))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ISOMappingUI f = new ISOMappingUI();
            f.MdiParent = this;
            f.Show();

        }

        private void navCompany_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(CompanyInfoUI))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            CompanyInfoUI f = new CompanyInfoUI();
            f.MdiParent = this;
            f.Show();

        }

        private void navAgent_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(AgentEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            AgentEntry f = new AgentEntry();
            f.MdiParent = this;
            f.Show();
        }


        private void navPermissionMapping_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(UserPermissionEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            UserPermissionEntry f = new UserPermissionEntry();
            f.MdiParent = this;
            f.Show();
        }


        #endregion

        #region IGM IMPORT

        private void navIGMImport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "IGM Import").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(IGMInput))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }

                IGMInput f = new IGMInput();
                f.MdiParent = this;
                f.Show();

            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }             
            

        }

        private void navIGMGateIn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "IGM Gate In").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(IGMGateIn))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }

                IGMGateIn f = new IGMGateIn();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void navIGMGateOut_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "IGM Gate Out").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(IGMGateOut))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }

                IGMGateOut f = new IGMGateOut();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void navIGMBLSearch_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "IGM Search").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(IGMBLSearch))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                IGMBLSearch f = new IGMBLSearch();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void navContainerSearch_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "IGM Search").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(ContainerSearch))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                ContainerSearch f = new ContainerSearch();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void ContainerHistory_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ContainerHistory))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ContainerHistory f = new ContainerHistory();
            f.MdiParent = this;
            f.Show();
        }

        #endregion

        #region CSD 


        private void navCSDGateIn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "CSD Gate In").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(ContainerGateEntry))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                ContainerGateEntry f = new ContainerGateEntry(user);
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void navCSDGateOut_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "CSD Gate Out").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(ContainerGateOut))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                ContainerGateOut f = new ContainerGateOut(user);
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void navCSDContSearch_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "CSD Container Search").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(CSDGateInOutData))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                CSDGateInOutData f = new CSDGateInOutData(user);
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }     

        
        private void navUpcmngCont_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "CSD Upcoming Container").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(CSDUpcommingEmptyContainer))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                CSDUpcommingEmptyContainer f = new CSDUpcommingEmptyContainer(user);
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void navUploadCSDGateInList_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(CSDGateInListUpload))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            CSDGateInListUpload f = new CSDGateInListUpload(user);
            f.MdiParent = this;
            f.Show();

        }


        private void navUploadCSDGateOutList_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(CSDGateOutListUpload))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            CSDGateOutListUpload f = new CSDGateOutListUpload(user);
            f.MdiParent = this;
            f.Show();
            

        }


        #endregion

        #region EXPORT

        private void navCargoReceive_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "Cargo Receive").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(CargoReceiving))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                CargoReceiving f = new CargoReceiving(user);
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void navSearchCargo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "Search Cargo").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(CargoReceivingSearch))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                CargoReceivingSearch f = new CargoReceivingSearch(user);
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void navStuffingDetails_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "Stuffing Details").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(StauffingDetails))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                StauffingDetails f = new StauffingDetails(user);
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void navContainerHistory_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ContainerHistory))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ContainerHistory f = new ContainerHistory();
            f.MdiParent = this;
            f.Show();
        }

        private void navTR_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(TerminalReceipt))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            TerminalReceipt f = new TerminalReceipt();
            f.MdiParent = this;
            f.Show();
        }
     

        #endregion

        #region ACCOUNCE

        private void navChartofAccount_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "Chart of Account").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(ChartOfAccountEntry))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                ChartOfAccountEntry f = new ChartOfAccountEntry();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void navVoucher_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "Voucher Entry").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(VoucherEntry))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                VoucherEntry f = new VoucherEntry(user);
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void navVoucherMaster_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "Voucher Data").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(VoucherMasterData))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                VoucherMasterData f = new VoucherMasterData(user);
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void navMoneyReceipt_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(MonerReceiptEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            MonerReceiptEntry f = new MonerReceiptEntry(user);
            f.MdiParent = this;
            f.Show();
        }

        private void navPrntMoneyRcpt_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(PrintMoneyReceipt))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            PrintMoneyReceipt f = new PrintMoneyReceipt(user);
            f.MdiParent = this;
            f.Show();
        }

        #endregion

        #region BILLING

        //Bill Setting

        private void navServiceDetails_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ServiceDetailsEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ServiceDetailsEntry f = new ServiceDetailsEntry();
            f.MdiParent = this;
            f.Show();

        }

        private void navDtentionChrgSetting_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ImpDetentionChrgSetting))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ImpDetentionChrgSetting f = new ImpDetentionChrgSetting();
            f.MdiParent = this;
            f.Show();

        }

        private void navCSDBillSetup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(BillOptionsUI))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            BillOptionsUI f = new BillOptionsUI();
            f.MdiParent = this;
            f.Show();
        }

        private void navServiceCategory_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ServiceCategory))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ServiceCategory f = new ServiceCategory();
            f.MdiParent = this;
            f.Show();
        }

        private void navServiceName_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ServiceName))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ServiceName f = new ServiceName();
            f.MdiParent = this;
            f.Show();
        }


        private void navExportBillSetting_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ExportServiceDetailsEntry))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ExportServiceDetailsEntry f = new ExportServiceDetailsEntry();
            f.MdiParent = this;
            f.Show();

        }

        //End Bill Setting ----------------------------------------

        //Import Billing 
        private void navImpBillCollection_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ImportBillCollection))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ImportBillCollection f = new ImportBillCollection(user);
            f.MdiParent = this;
            f.Show();
        }

        private void navImpBillPrint_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ImportBillPrint))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ImportBillPrint f = new ImportBillPrint(user);
            f.MdiParent = this;
            f.Show();

        }
        private void navImportBillApprove_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ImportBillApprove))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ImportBillApprove f = new ImportBillApprove(user);
            f.MdiParent = this;
            f.Show();
        }
        //End

        //CSD Billing 
        private void navCSDBillProcess_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ProcessBillUI))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ProcessBillUI f = new ProcessBillUI();
            f.MdiParent = this;
            f.Show();
        }

        private void navCSDBillSummary_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(BillSummary))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            BillSummary f = new BillSummary();
            f.MdiParent = this;
            f.Show();
        }

        private void navCSDBillDetails_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(BillDetails))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            BillDetails f = new BillDetails();
            f.MdiParent = this;
            f.Show();

        }

        //End CSD Billing -----------------------------------------

        //Export Billing

        private void navExpBillProcess_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ExportBillCollection))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ExportBillCollection f = new ExportBillCollection(user);
            f.MdiParent = this;
            f.Show();

        }

        private void navExpBillDtls_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ExportBillPrint))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }

            ExportBillPrint f = new ExportBillPrint(user);
            f.MdiParent = this;
            f.Show();
        }

        private void navEFRBillDetails_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(EFRBillDetails))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }

            EFRBillDetails f = new EFRBillDetails();
            f.MdiParent = this;
            f.Show();

        }

        //End Export Billing -----------------------------------------
        #endregion

        #region REPORT

        private void navIGMDailyReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ImportMLODailyReport))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ImportMLODailyReport f = new ImportMLODailyReport();
            f.MdiParent = this;
            f.Show();

        }

        private void navIGMSummaryRpt_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(MLOSummaryReport))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            MLOSummaryReport f = new MLOSummaryReport();
            f.MdiParent = this;
            f.Show();
        }

        private void navCSDSummary_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var authrization = listPermission.Where(p => p.FormList.FormName == "CSD Summary Data").FirstOrDefault();
            if (authrization != null && authrization.View == true)
            {
                foreach (Form form in Application.OpenForms)
                {

                    if (form.GetType() == typeof(CSDSearchReport))
                    {

                        form.Activate();
                        form.BringToFront();
                        return;
                    }
                }


                CSDSearchReport f = new CSDSearchReport();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to this form !!\n Please talk to admin", "Authorization Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        private void navCSDMLODailyReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(MLODailyReport))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            MLODailyReport f = new MLODailyReport();
            f.MdiParent = this;
            f.Show();

        }

        private void navCSDDailyMvntSumry_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(MLODailyMovementSummary))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            MLODailyMovementSummary f = new MLODailyMovementSummary();
            f.MdiParent = this;
            f.Show();

        }


        private void navExportDailyReceiving_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ConsigneeDailyReceived))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ConsigneeDailyReceived f = new ConsigneeDailyReceived();
            f.MdiParent = this;
            f.Show();
        }

        private void navExportDailyStuffing_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ConsigneeDailyStuffing))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ConsigneeDailyStuffing f = new ConsigneeDailyStuffing();
            f.MdiParent = this;
            f.Show();

        }
        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(AccountsVoucherRDLC))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            AccountsVoucherRDLC f = new AccountsVoucherRDLC();
            f.MdiParent = this;
            f.Show();
        }








        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ExportMLOSummaryReport))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ExportMLOSummaryReport f = new ExportMLOSummaryReport();
            f.MdiParent = this;
            f.Show();
        }


        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {

                if (form.GetType() == typeof(ImportMLODailyReport))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }


            ImportMLODailyReport f = new ImportMLODailyReport();
            f.MdiParent = this;
            f.Show();
        }



        #endregion

        
    }
}
