using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using LOGISTIC.BLL;
using System.Configuration;
using System.Data.SqlClient;

namespace LOGISTIC.UI.Import
{
    public partial class IGMBLSearch : Form
    {

       
        private IGMImportBLL objBll = new IGMImportBLL();
        static int PageSize = 10;

       // private static List<IGMImport> objListIGM = new List<IGMImport>();
        public IGMBLSearch()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            

        }
        private void IGMBLSearch_Load(object sender, EventArgs e)
        {
            ComboLoad();
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
        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");          
            cmbSearch.Items.Insert(2, "B/L Number");
            cmbSearch.Items.Insert(3, "Import Vessel");
            cmbSearch.Items.Insert(4, "Rotation");           

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

        public void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Khaki;
            dataGridView1.DefaultCellStyle.BackColor = Color.LightCyan;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.ColumnCount = 12;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL";
            dataGridView1.Columns[0].DataPropertyName = "SL";

          
            //dataGridView1.Columns[1].HeaderText = "IGM Number";
            //dataGridView1.Columns[1].DataPropertyName = "ReferanceNumber";

            
            dataGridView1.Columns[1].HeaderText = "BL Number";
            dataGridView1.Columns[1].DataPropertyName = "BLnumber";

            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[2].HeaderText = "Line No";
            dataGridView1.Columns[2].DataPropertyName = "LineNo";

            dataGridView1.Columns[3].HeaderText = "Quantity";
            dataGridView1.Columns[3].DataPropertyName = "Quantity";

            dataGridView1.Columns[4].HeaderText = "Commodity";
            dataGridView1.Columns[4].DataPropertyName = "CommodityName";

            dataGridView1.Columns[5].Width = 130;
            dataGridView1.Columns[5].HeaderText = "Importer";
            dataGridView1.Columns[5].DataPropertyName = "ImporterName"; ;

            dataGridView1.Columns[6].Width = 130;
            dataGridView1.Columns[6].HeaderText = "Import Vessel";
            dataGridView1.Columns[6].DataPropertyName = "VesselName";

            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[7].HeaderText = "Rotation";
            dataGridView1.Columns[7].DataPropertyName = "Rotation";
            
           
       
            dataGridView1.Columns[8].HeaderText = "BL Contains";
            dataGridView1.Columns[8].DataPropertyName = "BoxQuantity";


            dataGridView1.Columns[9].HeaderText = "Till Received";
            dataGridView1.Columns[9].DataPropertyName = "TillReceived";

            dataGridView1.Columns[10].HeaderText = "Entry Date";
            dataGridView1.Columns[10].DataPropertyName = "EntryDate";

            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[11].DataPropertyName = "IGMImportId";

            //objListIGM = objBll.GetAllIGMImport();

            //if (objListIGM.Count > 0)
            //{
            //    //int IGMContEntrySL = 1;
            //    foreach (var item in objListIGM)
            //    {
            //        dataGridView1.Rows.Add(item.IGMImportId, item.ReferanceNumber, item.BLnumber, item.Customer.AccountName, item.Importer.ImporterName, item.Vessel.VesselName, item.Rotation, item.NumberofPackage, item.EntryDate);
            //        //IGMContEntrySL = IGMContEntrySL + 1;
            //    }

            //}

            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.ClearSelection();

        }


        private void LoadDatatoGrid(int pageIndex)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllIGMImport", con))
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
                    //txtTotalBox.Text = Convert.ToString(totalBox.Count());
                    //txtTotalTues.Text = Convert.ToString(totalTuse.Count() * 2);
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


        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var IGMId = Convert.ToInt32(selectedRow.Cells[11].Value);
            //var objIGM = objListIGM.Where(x => x.IGMImportId == IGMId).SingleOrDefault();
            var objIGM = objBll.GetIGMImportByIGMId(IGMId);
            if (objIGM != null)
            {
                Form fc = Application.OpenForms["IGMInput"];

                if (fc != null)
                    fc.Close();
                IGMInput f = new IGMInput(objIGM);
                f.MdiParent = this.ParentForm;
                f.Show();

            }

            else MessageBox.Show("No Data Found !!");

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
                        txtSearch.Text = "";
                        break;
                    }
                default:
                    {
                        List<SerachIGMImportData_Result> listIGM = objBll.SearchIGMImportData(searchBy, searchText);
                        BindSearchDatatoGrid(listIGM);
                        break;
                    }
            }

        }

        

        private void BindSearchDatatoGrid(List<SerachIGMImportData_Result> listIGM)
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listIGM.Count > 0)
            {
                foreach (var objIGM in listIGM)
                {
                    dataGridView1.Rows.Add(objIGM.SL, objIGM.BLnumber, objIGM.LineNo, objIGM.Quantity, objIGM.CommodityName, objIGM.ImporterName, objIGM.VesselName, objIGM.Rotation, objIGM.BoxQuantity, objIGM.TillReceived, objIGM.EntryDate, objIGM.IGMImportId );
                   
                }
            }
            else
            {
                MessageBox.Show("No Record found !!");
            }
            txtSearch.Text = "";
        }

        private void ClearGrid()
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();            

        }

       
    }
}
