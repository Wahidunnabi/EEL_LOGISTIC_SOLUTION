using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Linq;
using System.Data;

namespace LOGISTIC.UI.Billing
{
    public partial class BillSummary : Form
    {


        //private IGMImportBLL IGMBll = new IGMImportBLL();
        private BillingBLL objBll = new BillingBLL();

        private CustomerBll mloBll = new CustomerBll();
        public static List<Customer> listMLO = new List<Customer>();

        private CSDBillSummary objBllSummary = new CSDBillSummary();
        public static List<CSDBillSummary> listBillSummary = new List<CSDBillSummary>();
        public BillSummary()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            btnSummaryEdit.Enabled = false;

        }

        private void BillSummary_Load(object sender, EventArgs e)
        {
            LoadCustomer();
            LoadGrid();
        }
        private void LoadCustomer()
        {

            listMLO = mloBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in listMLO)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Customer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlMLO.DataSource = dt_Types;
                ddlMLO.DisplayMember = "t_Name";
                ddlMLO.ValueMember = "t_ID";
            }
            ddlMLO.SelectedIndex = 0;

        }

        public void LoadGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 10;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[1].HeaderText = "Customer";

            dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[2].HeaderText = "Reference No.";

            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[3].HeaderText = "Size";
           
            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[4].HeaderText = "Quantity";

            dataGridView1.Columns[5].HeaderText = "Bill From";

            dataGridView1.Columns[6].HeaderText = "Bill To";

            dataGridView1.Columns[7].HeaderText = "Amount";                       

            dataGridView1.Columns[8].HeaderText = "VAT";
          
            dataGridView1.Columns[9].HeaderText = "Total Amount";           


            listBillSummary = objBll.GetAllCSDBillSummary();

            if (listBillSummary.Count > 0)
            {
                int index = 1;
                foreach (var item in listBillSummary)
                {                    
                    dataGridView1.Rows.Add(index, item.CustomerCode, item.SummaryRefNo, item.Size, item.Quantity, item.BillFrom.Value.ToString("dd/MMM/yyyy"), item.BillTo.Value.ToString("dd/MMM/yyyy"), item.Amount, item.VAT, item.Total);                   
                    index = index + 1;
                }

            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();

        }

        public void LoadShortSummeryGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 10;

            grdShortSummery.Columns[0].Width = 50;
            grdShortSummery.Columns[0].HeaderText = "SL#";

            grdShortSummery.Columns[1].Width = 70;
            grdShortSummery.Columns[1].HeaderText = "Customer";

            grdShortSummery.Columns[3].Width = 50;
            grdShortSummery.Columns[3].HeaderText = "Size";

            grdShortSummery.Columns[4].Width = 60;
            grdShortSummery.Columns[4].HeaderText = "Quantity";


            grdShortSummery.Columns[4].Width = 60;
            grdShortSummery.Columns[5].HeaderText = "Bill From";

            grdShortSummery.Columns[4].Width = 60;
            grdShortSummery.Columns[6].HeaderText = "Bill To";


            grdShortSummery.Columns[4].Width = 60;
            grdShortSummery.Columns[7].HeaderText = "Amount";

            grdShortSummery.Columns[4].Width = 60;
            grdShortSummery.Columns[8].HeaderText = "VAT";

            grdShortSummery.Columns[4].Width = 60;
            grdShortSummery.Columns[9].HeaderText = "Total";

        }


        private void ClearGrid()
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();


            grdShortSummery.DataSource = null;
            grdShortSummery.Rows.Clear();
            grdShortSummery.Refresh();

            

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
                               
            if (chkMLO.Checked == true && chkDate.Checked == true)
            {
                int MLOId = Convert.ToInt32(ddlMLO.SelectedValue);
                DateTime fromdate = dateFrom.Value;
                DateTime Todate = dateTo.Value;
                if (MLOId == 0)
                {
                    MessageBox.Show("Please select an MLO !!", "Selection required",
                                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                  
                    listBillSummary = objBll.GetCSDBillSummaryByMLO(MLOId, fromdate, Todate);

                    if (listBillSummary.Count > 0)
                    {
                        dataGridView1.Rows.Clear();
                        int index = 1;
                        foreach (var item in listBillSummary)
                        {
                            dataGridView1.Rows.Add(index, item.CustomerCode, item.SummaryRefNo, item.Size, item.Quantity, item.BillFrom.Value.ToString("dd/MMM/yyyy"), item.BillTo.Value.ToString("dd/MMM/yyyy"), item.Amount, item.VAT, item.Total);
                            index = index + 1;
                        }

                    }
                    else
                    {
                        MessageBox.Show("No data found !!", "Search result",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
               // return;
            }


            if (chkMLO.Checked == true && chkDate.Checked == false)
            {
                int MLOId = Convert.ToInt32(ddlMLO.SelectedValue);
                if (MLOId == 0)
                {
                    MessageBox.Show("Please select an MLO !!", "Selection required",
                                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    
                    DateTime fromdate = dateFrom.Value;
                    DateTime Todate = dateTo.Value;


                    listBillSummary = objBll.GetCSDBillSummaryByMLO(MLOId);

                    if (listBillSummary.Count > 0)
                    {
                        dataGridView1.Rows.Clear();
                        int index = 1;
                        foreach (var item in listBillSummary)
                        {
                            dataGridView1.Rows.Add(index, item.CustomerCode, item.SummaryRefNo, item.Size, item.Quantity, item.BillFrom.Value.ToString("dd/MMM/yyyy"), item.BillTo.Value.ToString("dd/MMM/yyyy"), item.Amount, item.VAT, item.Total);
                            index = index + 1;
                        }

                    }

                    DataTable dt = objBll.GetAllCSDBillShortSummeryByMlOId(MLOId, fromdate, Todate);
                    if (dt.Rows.Count > 0)
                    {


                        grdShortSummery.DataSource = dt;


                        

                        //dataGridView1.Rows.Clear();
                        //int index = 1;
                        //foreach (var item in listBillSummary)
                        //{
                        //    dataGridView1.Rows.Add(index, item.CustomerCode, item.SummaryRefNo, item.Size, item.Quantity, item.BillFrom.Value.ToString("dd/MMM/yyyy"), item.BillTo.Value.ToString("dd/MMM/yyyy"), item.Amount, item.VAT, item.Total);
                        //    index = index + 1;
                        //}

                    }


                    else
                    {
                        MessageBox.Show("No data found !!", "Search result",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                //return;
            }



            if (chkRef.Checked == true )
            {

                string refNo = txtRefNo.Text.Trim();
                if (refNo.Length == 0)
                {
                    MessageBox.Show("Please input reference number !!", "Input required",
                                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    listBillSummary = objBll.GetCSDBillSummaryByRefNo(refNo);

                    if (listBillSummary.Count > 0)
                    {
                        dataGridView1.Rows.Clear();
                        int index = 1;
                        foreach (var item in listBillSummary)
                        {
                            dataGridView1.Rows.Add(index, item.CustomerCode, item.SummaryRefNo, item.Size, item.Quantity, item.BillFrom.Value.ToString("dd/MMM/yyyy"), item.BillTo.Value.ToString("dd/MMM/yyyy"), item.Amount, item.VAT, item.Total);
                            index = index + 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found !!", "Search result",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;

                }
            }

            if (chkMLO.Checked == false && chkDate.Checked == true)
            {
                DateTime fromDate = dateFrom.Value;
                DateTime toDate = dateTo.Value;

                listBillSummary = objBll.GetCSDBillSummaryByDateRange(fromDate, toDate);

                if (listBillSummary.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                    int index = 1;
                    foreach (var item in listBillSummary)
                    {
                        dataGridView1.Rows.Add(index, item.CustomerCode, item.SummaryRefNo, item.Size, item.Quantity, item.BillFrom.Value.ToString("dd/MMM/yyyy"), item.BillTo.Value.ToString("dd/MMM/yyyy"), item.Amount, item.VAT, item.Total);
                        index = index + 1;
                    }
                }
                else
                {
                    MessageBox.Show("No data found !!", "Search result",
                                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
               // MessageBox.Show("Please check search category !!", "Selection required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
               
           
        }
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            int index = Convert.ToInt32(selectedRow.Index);
            objBllSummary = listBillSummary.ElementAt(index);
            NavigateToBillDetails(objBllSummary);
          
        }

        private void NavigateToBillDetails(CSDBillSummary objbillSummary)
        {
           
            BillDetails f = new BillDetails(objbillSummary);
            f.MdiParent = this.ParentForm;
            f.Show();

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            int index = Convert.ToInt32(selectedRow.Index);
            objBllSummary = listBillSummary.ElementAt(index);

            btnSummaryEdit.Enabled = true;

        }

        private void btnSummaryEdit_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(objBllSummary.CustId) > 0)
            {
                ProcessBillUI f = new ProcessBillUI(objBllSummary);
                f.MdiParent = this.ParentForm;
                f.Show();

            }
            else
            {
                MessageBox.Show("Please select a summary !!", "Selection Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void btnCSDClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCSDCancel_Click(object sender, EventArgs e)
        {
            //ClearGrid();
        }





        //private void NavigateToGateOut(int IGMDetailsId)
        //{
        //    IGMImportDetail objIGMdetails = new IGMImportDetail();
        //    objIGMdetails = objBll.GetIGMImportDetailById(IGMDetailsId);

        //    IGMGateOut f = new IGMGateOut(objIGMdetails);
        //    f.MdiParent = this.ParentForm;
        //    f.Show();

        //}



    }
}
