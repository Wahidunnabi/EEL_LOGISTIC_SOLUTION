using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace LOGISTIC.UI.Report
{
    public partial class CSDUpcommingEmptyContainer : Form
    {

        private CustomerBll MLOBll = new CustomerBll();
        private ContainerTypeBll ctBll = new ContainerTypeBll();
        private ContainerSizeBll csBll = new ContainerSizeBll();

        private OpenFileDialog opd = new OpenFileDialog();
        private CsdGateInUpcommingBLL objBll = new CsdGateInUpcommingBLL();

        private List<CSDGateInUPComing> listUpcomingCont = new List<CSDGateInUPComing>();
        private CSDGateInUPComing objUpcomingCont = new CSDGateInUPComing();

        private UserInfo user;
        //private int index;

        public CSDUpcommingEmptyContainer(UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;

        }

        private void CSDUpcommingEmptyContainer_Load(object sender, EventArgs e)
        {
            LoadCustomer();
            LoadContSize();
            LoadConType();
            PrepareGrid();
            LoadSearchCustomer();
            LoadcmbSearch();
            LoadDataToGrid();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        private void LoadcmbSearch()
        {

            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "Container Number");
            cmbSearch.Items.Insert(3, "Size");
            cmbSearch.Items.Insert(4, "MLO");
            cmbSearch.SelectedIndex = 0;
        }

        private void LoadCustomer()
        {

            var type = MLOBll.Getall();
           
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Customer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlMLO.DataSource = dt_Types;
                ddlMLO.DisplayMember = "t_Name";
                ddlMLO.ValueMember = "t_ID";

                cmbmloSearch.DataSource = dt_Types;
                cmbmloSearch.DisplayMember = "t_Name";
                cmbmloSearch.ValueMember = "t_ID";
            }
            ddlMLO.SelectedIndex = 0;
            cmbmloSearch.SelectedIndex = 0;


        }
        private void LoadSearchCustomer()
        {

            var type = MLOBll.Getall();

            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.CustomerId, t.CustomerCode);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Customer--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                

                cmbmloSearch.DataSource = dt_Types;
                cmbmloSearch.DisplayMember = "t_Name";
                cmbmloSearch.ValueMember = "t_ID";
            }
            
            cmbmloSearch.SelectedIndex = 0;


        }
        private void LoadContSize()
        {
            var type = csBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ContainerSizeId, t.ContainerSize1);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Size--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlSize.DataSource = dt_Types;
                ddlSize.DisplayMember = "t_Name";
                ddlSize.ValueMember = "t_ID";


                cmbContSize.DataSource = dt_Types;
                cmbContSize.DisplayMember = "t_Name";
                cmbContSize.ValueMember = "t_ID";
            }
            ddlSize.SelectedIndex = 0;
            cmbContSize.SelectedIndex = 0;
          }

        private void LoadConType()
        {

            var type = ctBll.Getall();
            DataTable dt_Types = new DataTable();
            dt_Types.Columns.Add("t_ID", typeof(int));
            dt_Types.Columns.Add("t_Name", typeof(string));
            foreach (var t in type)
            {
                dt_Types.Rows.Add(t.ContainerTypeId, t.ContainerTypeName);
            }
            DataRow dr = dt_Types.NewRow();
            dr[0] = 0;
            dr[1] = "--Select Type--";
            dt_Types.Rows.InsertAt(dr, 0);
            if (dt_Types.Rows.Count > 0)
            {
                ddlType.DataSource = dt_Types;
                ddlType.DisplayMember = "t_Name";
                ddlType.ValueMember = "t_ID";


                cmbConType.DataSource = dt_Types;
                cmbConType.DisplayMember = "t_Name";
                cmbConType.ValueMember = "t_ID";
            }
            ddlType.SelectedIndex = 0;
            cmbConType.SelectedIndex = 0;

            

        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            dataGridView1.EnableHeadersVisualStyles = false;           
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 7;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";
          
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[1].HeaderText = "MLO CODE";          


            dataGridView1.Columns[2].Width = 130;
            dataGridView1.Columns[2].HeaderText = "CONTAINER NUMBER";        


            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[3].HeaderText = "SIZE";      

            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[4].HeaderText = "TYPE";       

            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[5].HeaderText = "VESSEL";        

            dataGridView1.Columns[6].Width = 97;
            dataGridView1.Columns[6].HeaderText = "REG NO.";         

            

        }

        private void LoadDataToGrid()
        {
           
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listUpcomingCont = objBll.Getall();
            
            
            // listUpcomingCont
            if (listUpcomingCont.Count > 0)
            {
                PopulareGrid(listUpcomingCont);
                CalculateBoxTues(listUpcomingCont);
            }
            dataGridView1.ClearSelection();
        }
        private void CalculateBoxTues(IEnumerable<CSDGateInUPComing> listUpcomingCont)
        {
            int box = listUpcomingCont.Count();
            var numberOfForty = listUpcomingCont.Where(x => Convert.ToInt32(x.SizeName) > 20);
            var numberOftwenty = listUpcomingCont.Where(x => Convert.ToInt32(x.SizeName) == 20);
            txtTotalTues.Text = Convert.ToString(Convert.ToInt32(numberOftwenty.Count()) * 1 + Convert.ToInt32(numberOfForty.Count()) * 2);
            txtTotalBox.Text = box.ToString();
        }
        private void Select_Click(object sender, EventArgs e)
        {

            object status = null;
            if (opd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = opd.FileName;
                string filePath = opd.FileName;
                string extension = Path.GetExtension(filePath);
                DataSet dataSet = ImportExcelFile(extension, filePath);

                status = objBll.CsdGateInUpcommingInsert(dataSet);              
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataToGrid();
            }


            else
             if (opd.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("Data Upload has Cance", "Data Upload Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public DataSet ImportExcelFile(string fileExt, string filePath)
        {
            OleDbConnection connection = null;
            var excelDataSet = new DataSet();

            if (fileExt == ".xls")
            {
                connection =
                    new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath +
                                        @";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'");
            }
            else if (fileExt == ".xlsx")
            {
                connection =
                    new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                                        ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'");
            }
            if (connection != null)
            {
                connection.Open();
                var dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dataTable != null)
                {
                    string getExcelSheetName = dataTable.Rows[0]["Table_Name"].ToString();
                    var excelCommand = new OleDbCommand(@"SELECT * FROM [" + getExcelSheetName + @"]", connection);
                    var excelAdapter = new OleDbDataAdapter(excelCommand);

                    excelAdapter.Fill(excelDataSet);
                }
                connection.Close();
            }
            return excelDataSet;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //int selectedrowindex = GridViewCom.SelectedCells[0].RowIndex;
            //DataGridViewRow selectedRow = GridViewCom.Rows[selectedrowindex];
            //var index = Convert.ToInt32(selectedRow.Index);
            //objtblExpense = listtblExpense.ElementAt(index);
            

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            var index = Convert.ToInt32(selectedRow.Index);
            objUpcomingCont = listUpcomingCont.ElementAt(index);
            ShowData(objUpcomingCont);
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          
                DialogResult result = MessageBox.Show("Do you want to Gate In ??", "Upcoming Container Gate In", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                    var index = Convert.ToInt32(selectedRow.Index);
                    objUpcomingCont = listUpcomingCont.ElementAt(index);
                    NavigateToCSDGateIn(objUpcomingCont);
                }
                else
                {
                    dataGridView1.ClearSelection();
                }
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm Upcoming Container deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (objUpcomingCont.Id != 0)
                {
                    objBll.Delete(objUpcomingCont.Id);
                    LoadDataToGrid();
                    MessageBox.Show("Deleted successfully !! ");
                }

            }

            ClearForm();
        }


        private void ClearForm()
        {
            txtPath.Text = "";
            ddlMLO.SelectedIndex = 0;
            txtContNumbe.Text = "";
            ddlSize.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            txtVessel.Text = "";
            txtREGNo.Text = "";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            objUpcomingCont = new CSDGateInUPComing();
            dataGridView1.ClearSelection();          

        }

        private void ShowData(CSDGateInUPComing objUpcomingCont)
        {
            ddlMLO.SelectedValue = objUpcomingCont.MLOID;
            txtContNumbe.Text = objUpcomingCont.ContainerNo;
            ddlSize.SelectedValue= objUpcomingCont.SizeId;
            ddlType.SelectedValue = objUpcomingCont.TypeId;
            txtVessel.Text = objUpcomingCont.ImportVasselName;
            txtREGNo.Text = objUpcomingCont.RotationNumber;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;          
            //dataGridView1.ClearSelection();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDataToGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NavigateToCSDGateIn(CSDGateInUPComing objUpcomingCont)
        {
            
            ContainerGateEntry f = new ContainerGateEntry(objUpcomingCont, user);
            f.MdiParent = this.ParentForm;
            f.Show();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int Size = Convert.ToInt32(cmbContSize.SelectedValue);
            int type = Convert.ToInt32(cmbConType.SelectedValue);
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            int searchBy = cmbSearch.SelectedIndex;
            string searchText = txtSearch.Text.Trim();

            if (searchBy == 0)
            {
                MessageBox.Show("Please select search type !!", "Selection Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            

            switch (searchBy)
            {
                case 1:
                    {
                        PopulareGrid(listUpcomingCont);
                        CalculateBoxTues(listUpcomingCont);
                        break;
                    }
                case 2:
                    {
                        //-----Container 
                        if (searchBy ==2  && searchText == "")
                        {
                            MessageBox.Show("Search text can't be empty !!", "Input Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        listUpcomingCont = listUpcomingCont.Where(x => x.ContainerNo == searchText).ToList();
                        PopulareGrid(listUpcomingCont);
                        CalculateBoxTues(listUpcomingCont);
                        break;
                    }
                case 3:
                    {
                        //---Size 
                       // listUpcomingCont = listUpcomingCont.Where(x => x.SizeId == Size && x.TypeId == type).ToList();
                       // PopulareGrid(listUpcomingCont);

                        if (listUpcomingCont.Count > 0)
                        {
                            if (Size != 0 && type != 0)
                            {
                                listUpcomingCont = listUpcomingCont.Where(x => x.SizeId == Size && x.TypeId == type).ToList();
                                PopulareGrid(listUpcomingCont);
                                CalculateBoxTues(listUpcomingCont);
                            }
                            else
                            if (Size != 0 && type == 0)
                            {
                                listUpcomingCont = listUpcomingCont.Where(x => x.SizeId == Size).ToList();
                                PopulareGrid(listUpcomingCont);
                                CalculateBoxTues(listUpcomingCont);
                            }
                            else
                            if (Size == 0 && type != 0)
                            {
                                listUpcomingCont = listUpcomingCont.Where(x => x.TypeId == type).ToList();
                                PopulareGrid(listUpcomingCont);
                                CalculateBoxTues(listUpcomingCont);
                            }
                        }
                        break;
                    }

                case 4:
                    {
                        /// MLO
                        listUpcomingCont = listUpcomingCont.Where(x => x.MLOCode == Convert.ToString(ddlMLO.SelectedValue)).ToList();
                        PopulareGrid(listUpcomingCont);
                        CalculateBoxTues(listUpcomingCont);
                        break;
                    }
                default:
                    {
                        break;

                    }



                    
            }
               

        }

        private void PopulareGrid(IEnumerable<CSDGateInUPComing> listUpcoming)
        {
            int index = 1;
            foreach (var item in listUpcoming)
            {
                dataGridView1.Rows.Add(index, item.MLOCode, item.ContainerNo, item.SizeName, item.Type, item.ImportVasselName, item.RotationNumber);
                index = index + 1;
            }
        }

        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearch.SelectedIndex == 2)
            {
                txtSearch.Visible = true;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                cmbmloSearch.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 3)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = true;
                cmbConType.Visible = true;
                cmbmloSearch.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 4)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                cmbmloSearch.Visible = true;
            }
        }
    }

}

