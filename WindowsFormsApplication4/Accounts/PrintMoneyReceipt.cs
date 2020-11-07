using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using LOGISTIC.CSD.BLL;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LOGISTIC.REPORT;

namespace LOGISTIC.UI
{
    public partial class PrintMoneyReceipt : Form
    {

        private static MoneyReceipt objMoneyRecept = new MoneyReceipt();
        private static List<MoneyReceipt> MoneyRecept = new List<MoneyReceipt>();
        private MoneyReceiptBLL objBll = new MoneyReceiptBLL();
        private UserInfo user=new UserInfo();
        int MRId = 0;
        static int PageSize = 10;
       
        public PrintMoneyReceipt( UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            btnPrint.Enabled = false;

        }

        private void PrintMoneyReceipt_Load(object sender, EventArgs e)
        {
            cmbSearchLoad();
            cmbPageSizeLoad();
            PrepareGrid();
            LoadDatatoGrid(1);

        }     

        public class Page
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
        }

        private void cmbSearchLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Money Recept No");
            cmbSearch.Items.Insert(3, "Date");                                         
            cmbSearch.SelectedIndex = 0;

        }

        private void cmbPageSizeLoad()
        {

            cmbGridRow.Items.Insert(0, 5);
            cmbGridRow.Items.Insert(1, 10);
            cmbGridRow.Items.Insert(2, 15);
            cmbGridRow.Items.Insert(3, 20);
            cmbGridRow.SelectedIndex = 1;

        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 9;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";
            dataGridView1.Columns[0].DataPropertyName = "SL";

            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[1].HeaderText = "Money Receipt No";
            dataGridView1.Columns[1].DataPropertyName = "MoneyReceiptNo";

            dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[2].HeaderText = "Invoice/Reference No.";
            dataGridView1.Columns[2].DataPropertyName = "ReferenceNo";

            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[3].HeaderText = "Customer";
            dataGridView1.Columns[3].DataPropertyName = "CustomerCode";

            dataGridView1.Columns[4].Width = 180;
            dataGridView1.Columns[4].HeaderText = "C&F Agent";
            dataGridView1.Columns[4].DataPropertyName = "CFAgentName";

            dataGridView1.Columns[5].Width = 110;
            dataGridView1.Columns[5].HeaderText = "Payment Option";
            dataGridView1.Columns[5].DataPropertyName = "PaymentOption";                     

            dataGridView1.Columns[6].Width = 90;
            dataGridView1.Columns[6].HeaderText = "Grand Total";
            dataGridView1.Columns[6].DataPropertyName = "GrandTotal";

            dataGridView1.Columns[7].Width = 90;
            dataGridView1.Columns[7].HeaderText = "Date";
            dataGridView1.Columns[7].DataPropertyName = "Date";


            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[8].DataPropertyName = "ID";

        }

        private void LoadDatatoGrid(int pageIndex)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllMoneyReceipt", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = dt;
                    dataGridView1.ScrollBars = ScrollBars.None;
                    dataGridView1.AllowUserToAddRows = false;
                    con.Close();
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    if (recordCount > PageSize)
                    {
                        PopulatePager(recordCount, pageIndex);
                    }
                    
                }
            }
        }       

        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            LoadDatatoGrid(int.Parse(btnPager.Name));
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString(), });
            }

            //Clear existing Pager Buttons.
            pnlPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            foreach (Page page in pages)
            {
                Button btnPage = new Button();

                btnPage.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
                btnPage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                btnPage.Margin = new System.Windows.Forms.Padding(5);
                if (page.Text == "First" || page.Text == "Last")
                {
                    if (page.Text == "First")
                    {
                        btnPage.Location = new System.Drawing.Point(38 * count, 5);
                        btnPage.Size = new System.Drawing.Size(50, 22);
                        btnPage.BackColor = Color.Beige;
                    }

                    if (page.Text == "Last")
                    {
                        btnPage.Location = new System.Drawing.Point((38 * count) + 15, 5);
                        btnPage.Size = new System.Drawing.Size(50, 22);
                        btnPage.BackColor = Color.Beige;
                    }

                }
                else
                {
                    btnPage.Location = new System.Drawing.Point((38 * count) + 15, 5);
                    btnPage.Size = new System.Drawing.Size(35, 22);
                }

                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                pnlPager.Controls.Add(btnPage);
                count++;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int searchBy = cmbSearch.SelectedIndex;
            string searchText = txtSearch.Text.Trim();

            if (searchBy == 0)
            {
                MessageBox.Show("Please select search type !!", "Selection Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                return;
            }
            else if (searchBy > 1 && searchText == "")
            {
                MessageBox.Show("Search text can't be empty !!", "Input Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                return;
            }            
            switch (searchBy)
            {
                
                case 1:
                    {
                        LoadDatatoGrid(1);                                            
                        break;
                    }
                case 2:  //By Container Number
                    {
                        //List<SerachCSDGateInOutData_Result> listCSD = objBll.SearchCSDGateInOutData(searchBy, searchText);
                        //BindSearchDatatoGrid(listCSD);
                        break;
                    }
                case 3:  //By Reference Number
                    {
                        try
                        {
                            //var refNo = Convert.ToInt64(searchText);
                            //List<SerachCSDGateInOutData_Result> listCSD = objBll.SearchCSDGateInOutData(searchBy, Convert.ToString(refNo));
                            //BindSearchDatatoGrid(listCSD);
                            break;
                        }
                        catch
                        {
                            MessageBox.Show("Reference number should be numeric !!", "Input Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }                      
                    }              
                default:
                    {
                        LoadDatatoGrid(1);
                        break;
                    }

            }
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
                    
        private void cmbGridRow_SelectionChangeCommitted(object sender, EventArgs e)
        {

            PageSize = Convert.ToInt32(cmbGridRow.SelectedItem);
            LoadDatatoGrid(1);
            labelControl4.Focus();

        }

        private void cmbSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            var Index = Convert.ToInt32(cmbSearch.SelectedIndex);
            if (Index == 0)
            {
                txtSearch.Enabled = false;
                btnSearch.Enabled = false;
            }
            else
            {
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
            }
            labelControl4.Focus();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            MRId = Convert.ToInt32(selectedRow.Cells[8].Value);
            btnPrint.Enabled = true;
          
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            DialogResult result = MessageBox.Show("Do you want to update ??", "Money Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                var MRID = Convert.ToInt32(selectedRow.Cells[8].Value);

                var objMoneyReceipt = objBll.GetMoneyReceiptById(MRID);
                if (objMoneyReceipt != null)
                {
                    Form fc = Application.OpenForms["MonerReceiptEntry"];

                    if (fc != null)
                        fc.Close();
                    MonerReceiptEntry f = new MonerReceiptEntry(user, objMoneyReceipt);
                    f.MdiParent = this.ParentForm;
                    f.Show();

                }

                else MessageBox.Show("No Data Found !!");

            }
            else
            {
                dataGridView1.ClearSelection();
            }
                                
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (MRId > 0)
            {
                ReportDocument cryRpt = new ReportDocument();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                crConnectionInfo.ServerName = LOGISTIC.UI.Properties.Settings.Default.DBSERVER;
                crConnectionInfo.DatabaseName = LOGISTIC.UI.Properties.Settings.Default.DATABASE;
                crConnectionInfo.UserID = LOGISTIC.UI.Properties.Settings.Default.USERID;
                crConnectionInfo.Password = LOGISTIC.UI.Properties.Settings.Default.PASSWORD;

                //crConnectionInfo.ServerName = "sarwar-pc";
                //crConnectionInfo.DatabaseName = "PORT_LOGISTIC";
                //crConnectionInfo.UserID = "sa";
                //crConnectionInfo.Password = "sa";

                cryRpt.Load(@"D:\Report\" + "PrintMonerReceipt.rpt");
                cryRpt.SetParameterValue("@MRId", Convert.ToInt32(MRId));

                CrTables = cryRpt.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }
                Viewer view = new Viewer();
                view.ReportView.ReportSource = cryRpt;
                view.ReportView.Refresh();
                view.Show();

            }
            else
            {
                MessageBox.Show("Please select an object ??", "Data Not Found !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
