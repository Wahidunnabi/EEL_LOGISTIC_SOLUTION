using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Linq;
using System.Data;

namespace LOGISTIC.UI
{
    public partial class VoucherMasterData : Form
    {

        private static VoucherMaster objVoucherMaster = new VoucherMaster();
        private static List<VoucherMaster> listVoucher = new List<VoucherMaster>();
        private AccounceBLL objBll = new AccounceBLL();
        private UserInfo user;
       
       
        public VoucherMasterData(UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.user = user;           
            //btnSearch.Enabled = false;
            
            
        }
   

        private void VoucherMasterData_Load(object sender, EventArgs e)
        {
            objVoucherMaster = new VoucherMaster();
            LoadVoucherType();
            PrepareGrid();
            LoadDataToGrid();
            txtVoucher.Enabled = false;
            txtReference.Enabled = false;
            dateFrom.Enabled = false;
            dateTo.Enabled = false;
        }


        private void LoadVoucherType()
        {
            var type = objBll.GetAllVoucherType();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(string));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.VoucherTypeId, t.TypeName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "-- Select Voucher Type --";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlVoucherType.DataSource = dt_Types;
                ddlVoucherType.DisplayMember = "t_Name";
                ddlVoucherType.ValueMember = "t_ID";
            }

            ddlVoucherType.SelectedIndex = 0;

           
        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";
            dataGridView1.Columns[0].DataPropertyName = "VoucherMstrId";


            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[1].HeaderText = "Voucher Type";
            dataGridView1.Columns[1].DataPropertyName = "VoucherTypeId";

            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[2].HeaderText = "Voucher Number";
            dataGridView1.Columns[2].DataPropertyName = "VoucherNumber";

            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[3].HeaderText = "Amount";
            dataGridView1.Columns[3].DataPropertyName = "Amount";


            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Date";
            dataGridView1.Columns[4].DataPropertyName = "VoucherDate";            


            //dataGridView1.Columns[13].Visible = false;
            //dataGridView1.Columns[13].DataPropertyName = "ContainerGateEntryId";
           

        }


        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listVoucher = objBll.GetAllVoucherMaster();
            dataGridView1.DataSource = listVoucher;
            
            //if (listCustomer.Count > 0)
            //{
            //    int index = 1;
            //    foreach (var item in listCustomer)
            //    {
            //        dataGridView1.Rows.Add(index, item.CustomerCode, item.CustomerName, item.Agent.AgentName);
            //        index = index + 1;
            //    }

            //}
            dataGridView1.ClearSelection();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //int searchBy = cmbSearch.SelectedIndex;
            //string searchText = txtSearch.Text.Trim();

            //if (searchBy == 0)
            //{
            //    MessageBox.Show("Please select search type !!", "Selection Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);               
            //    return;
            //}
            //else if (searchBy > 1 && searchText == "")
            //{
            //    MessageBox.Show("Search text can't be empty !!", "Input Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);               
            //    return;
            //}            
            //switch (searchBy)
            //{
                
            //    case 1:
            //        {
            //            LoadDatatoGrid(1);                                            
            //            break;
            //        }
            //    case 2:  //By Container Number
            //        {
            //            List<SerachCSDGateInOutData_Result> listCSD = objBll.SearchCSDGateInOutData(searchBy, searchText);
            //            BindSearchDatatoGrid(listCSD);
            //            break;
            //        }
            //    case 3:  //By Reference Number
            //        {
            //            try
            //            {
            //                var refNo = Convert.ToInt64(searchText);
            //                List<SerachCSDGateInOutData_Result> listCSD = objBll.SearchCSDGateInOutData(searchBy, Convert.ToString(refNo));
            //                BindSearchDatatoGrid(listCSD);
            //                break;
            //            }
            //            catch
            //            {
            //                MessageBox.Show("Reference number should be numeric !!", "Input Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                return;
            //            }                      
            //        }
            //    case 4:   //By Challan In Number
            //        {

            //            List<SerachCSDGateInOutData_Result> listCSD = objBll.SearchCSDGateInOutData(searchBy, searchText);
            //            BindSearchDatatoGrid(listCSD);
            //            break;

            //        }
            //    case 5:   //By Challan Out Number
            //        {

            //            List<SerachCSDGateInOutData_Result> listCSD = objBll.SearchCSDGateInOutData(searchBy, searchText);
            //            BindSearchDatatoGrid(listCSD);
            //            break;

            //        }
            //    default:
            //        {
            //            LoadDatatoGrid(1);
            //            break;
            //        }

            //}
        }

        private void BindSearchDatatoGrid(List<SerachCSDGateInOutData_Result> listCSD)
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listCSD.Count > 0)
            {
                foreach (var objCSD in listCSD)
                {
                    dataGridView1.Rows.Add(objCSD.SL, objCSD.ContNo, objCSD.CustomerCode, objCSD.ContainerTypeName, objCSD.ContainerSize, objCSD.ChallanNo, objCSD.DepotName, objCSD.TrailerInNo, objCSD.HaulierNo, objCSD.DateIn, objCSD.InOutStatus, objCSD.InOutStatus, objCSD.ContainerGateEntryId);
                    
                }                
            }
            else
            {
                MessageBox.Show("No Record found !!");
            }
            
        }


      

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];


           


            DialogResult result = MessageBox.Show("Do you want to update ??",
                      "Voucher Master data",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var masterId = Convert.ToInt32(selectedRow.Cells[0].Value);
                objVoucherMaster = listVoucher.Where(x => x.VoucherMstrId == masterId).FirstOrDefault();
                if (objVoucherMaster != null)
                {
                    VoucherEntry f = new VoucherEntry(objVoucherMaster, user);
                    f.MdiParent = this.ParentForm;
                    f.Show();

                }              

            }
            else
            {
                dataGridView1.ClearSelection();
            }
                        
        }
                      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ddlVoucherType.SelectedIndex = 0;
            txtVoucher.Text = "";
            txtReference.Text = "";
            chkVoucher.Checked = false;
            chkRef.Checked = false;
            chkDateRange.Checked = false;
        }

        private void chkRef_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRef.Checked == true)
            {
                txtReference.Enabled = true;
                txtReference.Focus();
                chkVoucher.Checked = false;
                chkDateRange.Checked = false;
            }
            else
            {
                txtReference.Enabled = false;
            }
        }

        private void chkVoucher_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVoucher.Checked == true)
            {
                txtVoucher.Enabled = true;
                txtVoucher.Focus();
                chkRef.Checked = false;
                chkDateRange.Checked = false;
            }
            else
            {
                txtVoucher.Enabled = false;
            }

        }

        private void chkDateRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDateRange.Checked == true)
            {
                dateFrom.Enabled = true;
                dateTo.Enabled = true;
                chkVoucher.Checked = false;
                chkRef.Checked = false;
            }
            else
            {
                dateFrom.Enabled = false;
                dateTo.Enabled = false;
            }

        }
    }
}
