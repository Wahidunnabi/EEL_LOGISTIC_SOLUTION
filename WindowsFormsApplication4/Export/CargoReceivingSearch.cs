using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.Export.BLL;
using System.Configuration;
using System.Data.SqlClient;

namespace LOGISTIC.UI.Export
{
    public partial class CargoReceivingSearch : Form
    {


        private static CargoRecieving objCargoReceiving = new CargoRecieving();
        private static List<CargoRecieving> listCR = new List<CargoRecieving>();
        private CargoReceivingBLL objBll = new CargoReceivingBLL();

        private UserInfo user;
        static int PageSize = 15;

        public CargoReceivingSearch( UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;
            
        }

        private void CargoReceivingSearch_Load(object sender, EventArgs e)
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
            cmbSearch.Items.Insert(2, "SL No");
            cmbSearch.Items.Insert(3, "EFR No");          
            cmbSearch.SelectedIndex = 0;

        }

        private void cmbPageSizeLoad()
        {

            ddlPageSize.Items.Insert(0, 5);
            ddlPageSize.Items.Insert(1, 10);
            ddlPageSize.Items.Insert(2, 15);
            ddlPageSize.Items.Insert(3, 20);
            ddlPageSize.SelectedIndex = 2;


        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 12;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";
            dataGridView1.Columns[0].DataPropertyName = "SL";

            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[1].HeaderText = "Client";
            dataGridView1.Columns[1].DataPropertyName = "CustomerCode";

            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[2].HeaderText = "SL No";
            dataGridView1.Columns[2].DataPropertyName = "SLNo";

            dataGridView1.Columns[3].HeaderText = "Receiving Date";
            dataGridView1.Columns[3].DataPropertyName = "ReceivingDate";

            dataGridView1.Columns[4].HeaderText = "Shipper";
            dataGridView1.Columns[4].DataPropertyName = "ShipperCode";

            dataGridView1.Columns[5].HeaderText = "Consignee";
            dataGridView1.Columns[5].DataPropertyName = "ConsigneeCode";

            dataGridView1.Columns[6].Width = 120;
            dataGridView1.Columns[6].HeaderText = "Freight Forwarde";
            dataGridView1.Columns[6].DataPropertyName = "FreightForwarderCode";

            dataGridView1.Columns[7].HeaderText = "C&F Agent";
            dataGridView1.Columns[7].DataPropertyName = "CFAgentCode";

            dataGridView1.Columns[8].Width = 70;
            dataGridView1.Columns[8].HeaderText = "EFR No";
            dataGridView1.Columns[8].DataPropertyName = "EFRNo";

            dataGridView1.Columns[9].HeaderText = "Commodity";
            dataGridView1.Columns[9].DataPropertyName = "CommodityName";

            dataGridView1.Columns[10].HeaderText = "Location";
            dataGridView1.Columns[10].DataPropertyName = "LocationName";         

            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[11].DataPropertyName = "CargoReceiveId";

        }

        private void LoadDatatoGrid(int pageIndex)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllCargoReceivingData", con))
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

        private void ddlPageSize_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedItem);
            LoadDatatoGrid(1);
            labelControl4.Focus();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                DialogResult result = MessageBox.Show("Do you want to update this record ??",
                          "Cargo Receivid",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var CRId = Convert.ToInt32(selectedRow.Cells[11].Value);
                    NavigateToCargoReceiving(CRId);
                }
                else
                {
                    dataGridView1.ClearSelection();
                }
           
        }

        private void NavigateToCargoReceiving(int CRId)
        {
            CargoRecieving objCR = new CargoRecieving();
            objCR = objBll.GetCRById(CRId);

            Form fc = Application.OpenForms["CargoReceiving"];

            if (fc != null)
                fc.Close();

            CargoReceiving f = new CargoReceiving(objCR, user);
            f.MdiParent = this.ParentForm;
            f.Show();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            dataGridView1.ClearSelection();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
