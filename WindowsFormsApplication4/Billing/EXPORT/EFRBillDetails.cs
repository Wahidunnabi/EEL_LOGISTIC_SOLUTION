using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Linq;
using System.Data;

namespace LOGISTIC.UI.Billing
{
    public partial class EFRBillDetails : Form
    {

        private BillingBLL objBll = new BillingBLL();
        public EFRBillDetails()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

        }

        private void EFRBillDetails_Load(object sender, EventArgs e)
        {
            PrepareGrid();
        }

        public void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 6;

            
            dataGridView1.Columns[0].Width = 110;
            dataGridView1.Columns[0].HeaderText = "EFR NO.";
            dataGridView1.Columns[0].DataPropertyName = "EFRNo";

            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[1].HeaderText = "Service Name";
            dataGridView1.Columns[1].DataPropertyName = "ServiceName";

            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "Size";
            dataGridView1.Columns[2].DataPropertyName = "ContainerSize";

            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[3].HeaderText = "Quantity";
            dataGridView1.Columns[3].DataPropertyName = "Quantity";

            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[4].HeaderText = "Rate";
            dataGridView1.Columns[4].DataPropertyName = "Rate";

            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[5].HeaderText = "Total";
            dataGridView1.Columns[5].DataPropertyName = "Total";

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int searchBy = rdoEFRNo.Checked == true ? 1 : 2;
            string searchText = txtSearch.Text.Trim();

            if (rdoEFRNo.Checked == false && rdoDateRange.Checked == false)
            {
                MessageBox.Show("Please select search type !!", "Selection Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (rdoEFRNo.Checked == true && searchText == "")
            {
                MessageBox.Show("Search text can't be empty !!", "Input Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
          
            switch (searchBy)
            {

                case 1:  //By EFR Number
                    {
                        List<EXPORT_EFRWiseBillDetails_Result> listCSD = objBll.GetEFRWiseBillDetails(searchText, dateFrom.Value, dateTo.Value);
                        BindSearchDatatoGrid(listCSD);
                        break;
                    }
                case 2:  //By Date Range
                    {
                        List<EXPORT_EFRWiseBillDetails_Result> listCSD = objBll.GetEFRWiseBillDetails(null, dateFrom.Value, dateTo.Value);
                        BindSearchDatatoGrid(listCSD);
                        break;
                    }             
                default:
                    {
                        //LoadDatatoGrid(1);
                        break;
                    }

            }
        }

        private void BindSearchDatatoGrid(List<EXPORT_EFRWiseBillDetails_Result> listBillDetails)
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listBillDetails.Count > 0)
            {
                foreach (var objDetails in listBillDetails)
                {
                    dataGridView1.Rows.Add(objDetails.EFRNo, objDetails.ServiceName, objDetails.ContainerSize, objDetails.Quantity, objDetails.Rate, objDetails.Total);                   
                }
            }
            else
            {
                MessageBox.Show("No Record found !!");
            }

        }   

        private void rdoEFRNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoEFRNo.Checked == true)
            {
                txtSearch.Enabled = true;

            }
            else
            {
                txtSearch.Text = "";
                txtSearch.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
     
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClearForm()
        {

            dataGridView1.Rows.Clear();
            txtSearch.Text = "";
            dateFrom.Value = DateTime.Now;
            dateTo.Value = DateTime.Now;

        }

    }
}
