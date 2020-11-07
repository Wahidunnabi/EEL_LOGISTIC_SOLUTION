using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using LOGISTIC.BLL;
using System.Linq;

namespace LOGISTIC.UI
{
    public partial class ImportBillApprove : Form
    {

        //private static MoneyReceipt objMoneyRecept = new MoneyReceipt();
        //private static List<MoneyReceipt> MoneyRecept = new List<MoneyReceipt>();
        private static List<Service> listServices = new List<Service>();
        private BillingBLL objBll = new BillingBLL();
        private UserInfo user=new UserInfo();
        DataGridViewRow selectedRow ;
        //int billId = 0;
        static int PageSize = 10;
       
        public ImportBillApprove( UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            btnPrint.Enabled = false;

        }

        private void ImportBillApprove_Load(object sender, EventArgs e)
        {
            listServices = objBll.GetAllImportServices();
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
            cmbSearch.Items.Insert(2, "BL No");
            cmbSearch.Items.Insert(3, "Bill Date");                                         
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
            dataGridView1.ColumnCount = 11;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";
            dataGridView1.Columns[0].DataPropertyName = "SL";

            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[1].HeaderText = "BL Number";
            dataGridView1.Columns[1].DataPropertyName = "BLNo";

            dataGridView1.Columns[2].Width = 160;
            dataGridView1.Columns[2].HeaderText = "Importer Name";
            dataGridView1.Columns[2].DataPropertyName = "ImporterName";

            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[3].HeaderText = "MLO Code";
            dataGridView1.Columns[3].DataPropertyName = "CustomerCode";

           

            // dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Total Amount";
            dataGridView1.Columns[4].DataPropertyName = "TotalAmount";

            //dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].HeaderText = "VAT Amount";
            dataGridView1.Columns[5].DataPropertyName = "VATAmount";

                              
            //dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].HeaderText = "Grand Total";
            dataGridView1.Columns[6].DataPropertyName = "GrandTotal";

            //dataGridView1.Columns[6].Width = 90;
            dataGridView1.Columns[7].HeaderText = "Upto Bill Date";
            dataGridView1.Columns[7].DataPropertyName = "Upto Date";

            //dataGridView1.Columns[7].Width = 90;
            dataGridView1.Columns[8].HeaderText = "Bill Date";
            dataGridView1.Columns[8].DataPropertyName = "Bill Date";

            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[9].DataPropertyName = "CFAgentName";

            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[10].DataPropertyName = "ID";

        }

        private void LoadDatatoGrid(int pageIndex)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllImportBillforApprove", con))
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
                    dataGridView1.Rows.Add(objCSD.SL, objCSD.ContNo, objCSD.CustomerCode, objCSD.ContainerTypeName,
                        objCSD.ContainerSize, objCSD.ChallanNo, objCSD.DepotName, objCSD.TrailerInNo, objCSD.HaulierNo,
                        objCSD.DateIn, objCSD.InOutStatus, objCSD.InOutStatus, objCSD.ContainerGateEntryId);
                  
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
            //DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
             selectedRow = dataGridView1.Rows[selectedrowindex];
           // billId = Convert.ToInt32(selectedRow.Cells[8].Value);
            btnPrint.Enabled = true;
          
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //DialogResult result = MessageBox.Show("Do you want to update ??", "Import Bill Details", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (result == DialogResult.Yes)
            //{
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                var billId = Convert.ToInt32(selectedRow.Cells[10].Value);

                var objImportBill = objBll.GetImportBillById(billId);
                if (objImportBill != null)
                {
                    Form fc = Application.OpenForms["ImportBillCollection"];

                    if (fc != null)
                        fc.Close();
                    ImportBillCollection f = new ImportBillCollection(objImportBill, user);
                    f.MdiParent = this.ParentForm;
                    f.Show();

                }

                else MessageBox.Show("No Data Found !!");

            //}
            //else
            //{
                dataGridView1.ClearSelection();
                btnPrint.Enabled = false;
            //}

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var billId = Convert.ToInt32(selectedRow.Cells[10].Value);
            var BLno = Convert.ToString(selectedRow.Cells[1].Value).Trim();
            var importer = Convert.ToString(selectedRow.Cells[2].Value).Trim();
            var MLO = Convert.ToString(selectedRow.Cells[3].Value).Trim();
            var CandF = Convert.ToString(selectedRow.Cells[9].Value).Trim();

            if (billId > 0)
            {               
                ImportBill objBill = objBll.GetImportBillById(billId);

                try
                {
                    Excel.Application xlApp = new Excel.Application();

                    if (xlApp == null)
                    {
                        MessageBox.Show("Excel is not properly installed!!");
                        return;
                    }
                    xlApp.DisplayAlerts = false;

                    Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
                    Excel.Sheets worksheets = xlWorkBook.Worksheets;

                    var xlSummary = (Excel.Worksheet)worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                    xlSummary.Shapes.AddPicture("E:\\ELL_logo.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 100, 1, 60, 40);
                    xlSummary.Cells[1, 1].value = "EASTERN LOGISTICS LIMITED";
                    xlSummary.Cells[1, 1].Font.Bold = true;
                    xlSummary.Cells[1, 1].Font.Size = 15;
                    xlSummary.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSummary.Range["A1:M1"].MergeCells = true;

                    xlSummary.Cells[2, 1].value = " KATHGAR, NORTH PATENGA, CHITTAGONG";
                    xlSummary.Cells[2, 1].Font.Size = 10;
                    xlSummary.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSummary.Range["A2:M2"].MergeCells = true;

                    xlSummary.Cells[3, 1].value = "Phone: +88-031-2503341-4, Email: import@easternlogisticsltd.com";
                    xlSummary.Cells[3, 1].Font.Size = 10;
                    xlSummary.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSummary.Range["A3:M3"].MergeCells = true;



                    xlSummary.Cells[5, 1].value = "IMPORT BILL OF "+ importer.ToUpper();
                    xlSummary.Cells[5, 1].Font.Bold = true;
                    xlSummary.Cells[5, 1].Font.Size = 10;
                    xlSummary.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlSummary.Range["A5:M5"].MergeCells = true;
                    xlSummary.Range["A5:M5"].Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleSingle;




                    string FileName = "D:\\Import bill of "+ importer+ ".xlsx";
                    xlSummary.Name = importer;


                    int r = 7; // Initialize Excel Row Start Position  = 1


                    xlSummary.Cells[r, 2].value = "BL NO";
                    xlSummary.Cells[r, 3].value = BLno;
                    xlSummary.Cells[r, 6].value = "DATE :";
                    xlSummary.Cells[r, 7].value = DateTime.Now.Date.ToString("dd MMM yy");
                   

                    r = r + 2;
                    xlSummary.Cells[r, 2].value = "IMPORTER";                  
                    xlSummary.Cells[r, 3].value = importer;
                   

                    r = r + 2;
                    xlSummary.Cells[r, 2].value = "C&F AGENT";                   
                    xlSummary.Cells[r, 3].value = CandF;

                    r = r + 2;
                    xlSummary.Cells[r, 2].value = "MLO CODE";                   
                    xlSummary.Cells[r, 3].value = MLO;
                  
                    r = r + 2;
                    xlSummary.Cells[r, 4].value = "Indate";
                    xlSummary.Cells[r, 5].value = "Outdate";
                    xlSummary.Cells[r, 6].value = "SIZE";
                    xlSummary.Cells[r, 7].value = "QUANTITY";
                    xlSummary.Cells[r, 8].value = "DAYS";
                    xlSummary.Cells[r, 9].value = "RATE Tk.";
                    xlSummary.Cells[r, 10].value = "RATE Dlr";
                    xlSummary.Cells[r, 11].value = "TOTAL";

                    xlSummary.Range["D" + r, "K" + r].Borders.Color = Color.Black.ToArgb();
                    xlSummary.Range["D" + r, "K" + r].Font.Size = 10;
                    xlSummary.Range["D" + r, "K" + r].Font.Bold = true;

                    r = r + 1;
                    xlSummary.Cells[r, 2].value = "Delivery Packages";

                    // foreach (ImportBillDetail item in objBill.ImportBillDetails.Where(b=>b.ServiceId==1 || b.ServiceId==2).ToList())
                    foreach (ImportBillDetail item in objBill.ImportBillDetails.Where(b => b.ServiceId != 3).ToList())
                    {
                        xlSummary.Cells[r, 4].value = item.ImportIndate.Value.ToString("dd MMM yy"); 
                        xlSummary.Cells[r, 5].value = item.ImportOutdate.Value.ToString("dd MMM yy");
                        xlSummary.Cells[r, 6].value = item.Size.ToString();
                        xlSummary.Cells[r, 7].value = item.Quantity.ToString();
                        xlSummary.Cells[r, 8].value = item.Days.ToString();
                        xlSummary.Cells[r, 9].value = item.RateInTk.ToString();
                        xlSummary.Cells[r, 10].value = item.RateInDlr.ToString();
                        xlSummary.Cells[r, 11].value = item.Total.ToString();
                        objBill.ImportBillDetails.Remove(item);
                        r = r + 1;
                    }


                    r = r + 2;
                    xlSummary.Cells[r, 2].value = "Ground Rent/Detention:";

                    foreach (ImportBillDetail item in objBill.ImportBillDetails.Where(b => b.ServiceId == 3).ToList())
                    {
                        xlSummary.Cells[r, 5].value = item.BillUnit.ToString();
                        xlSummary.Cells[r, 6].value = item.Size.ToString();
                        xlSummary.Cells[r, 7].value = item.Quantity.ToString();
                        xlSummary.Cells[r, 8].value = item.Days.ToString();
                        xlSummary.Cells[r, 9].value = item.RateInTk.ToString();
                        xlSummary.Cells[r, 10].value = item.RateInDlr.ToString();
                        xlSummary.Cells[r, 11].value = item.Total.ToString();
                        objBill.ImportBillDetails.Remove(item);
                        r = r + 1;
                    }

                    r = r + 1;
                    foreach (ImportBillDetail item in objBill.ImportBillDetails.ToList())
                    {
                        xlSummary.Cells[r, 2].value = listServices.Where(s => s.ID == item.ServiceId).FirstOrDefault().ServiceName;                      
                        xlSummary.Cells[r, 7].value = item.Quantity.ToString();                      
                        xlSummary.Cells[r, 9].value = item.RateInTk.ToString().Length > 0? item.RateInTk.ToString(): null;
                        xlSummary.Cells[r, 10].value = item.RateInDlr.ToString().Length > 0 ? item.RateInDlr.ToString() : null;
                        xlSummary.Cells[r, 11].value = item.Total.ToString();
                        objBill.ImportBillDetails.Remove(item);
                        r = r + 1;
                    }

                    xlSummary.Cells[r, 10].value = "TOTAL:";
                    xlSummary.Cells[r, 11].value = objBill.TotalAmount.ToString();

                    xlSummary.Range["J" + r, "K" + r].Font.Size = 10;
                    xlSummary.Range["J" + r, "K" + r].Font.Bold = true;
                    xlSummary.Range["K" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color.Black;
                    xlSummary.Range["K" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

                    r = r + 1;
                    xlSummary.Cells[r, 2].value = "Discount Amount";
                    xlSummary.Cells[r, 11].value = objBill.DiscountAmount.ToString();

                    r = r + 1;
                    xlSummary.Cells[r, 2].value = "VAT (as per VAT act. 1991)";
                    xlSummary.Cells[r, 11].value = objBill.VatAmount.ToString();

                    r = r + 1;
                    xlSummary.Cells[r, 10].value = "GRAND TOTAL:";
                    xlSummary.Cells[r, 11].value = objBill.GrandTotal.ToString();

                    xlSummary.Range["J" + r, "K" + r].Font.Size = 10;
                    xlSummary.Range["J" + r, "K" + r].Font.Bold = true;
                    xlSummary.Range["K" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color.Black;
                    xlSummary.Range["K" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;


                    r = r + 1;                  
                    //xlSummary.Range["J" + r, "K" + r].Font.Size = 10;
                    //xlSummary.Range["J" + r, "K" + r].Font.Bold = true;
                    //xlSummary.Range["K" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color.Black;
                    xlSummary.Range["K" + r, "K" + r].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                    r = r + 6;
                    xlSummary.Cells[r, 3].value = "Authorized Signature.";                   
                    xlSummary.Range["C" + r, "C" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Black;
                    xlSummary.Range["C" + r, "C" + r].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                   


                    xlSummary.Range["A:A"].EntireColumn.AutoFit();

                    // xlSummary.get_Range("A7", "M33").Font.Bold = true;
                    //xlSummary.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                    xlSummary.Columns.AutoFit();




                    Excel.Sheets autoSheet = xlWorkBook.Worksheets;
                    autoSheet[2].Delete();

                    xlWorkBook.SaveAs(FileName);
                    xlWorkBook.Close();
                    xlApp.Quit();



                    Marshal.ReleaseComObject(xlApp);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlSummary);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message, "You got an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    foreach (Process process in Process.GetProcessesByName("Excel"))
                        process.Kill();
                }                
            }
            else
            {
                MessageBox.Show("Please select an object ??", "Data Not Found !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        
    }
}
