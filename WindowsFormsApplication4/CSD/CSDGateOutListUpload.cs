using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using LOGISTIC.CSD.BLL;

namespace LOGISTIC.UI
{
    public partial class CSDGateOutListUpload : Form
    {

        private CustomerBll MLOBll = new CustomerBll();       
        private DepotBll depotBll = new DepotBll();
        private ContainerSizeBll sizeBll = new ContainerSizeBll();
        private ContainerTypeBll typeBll = new ContainerTypeBll();
        private CSDGateInOutBLL objBll = new CSDGateInOutBLL();

        private List<Depot> listDepot = new List<Depot>();
        private List<CSDContGateInOut> listCSDGateIn = new List<CSDContGateInOut>();
        private List<CSDContGateInOut> listCSDGateOut = new List<CSDContGateInOut>();
        private List<ContainerSize> objSizelist = new List<ContainerSize>();
        private List<ContainerType> objTypelist = new List<ContainerType>();
        private CSDContGateInOut objCSD = new CSDContGateInOut();

        private UserInfo user;

        public CSDGateOutListUpload(UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;

        }

        private void CSDGateOutListUpload_Load(object sender, EventArgs e)
        {
            objSizelist = sizeBll.Getall();
            objTypelist = typeBll.Getall();
            listDepot = depotBll.Getall();
            LoadCustomer();
            LoadcmbSearch();
            LoadddlmloSearch();
            LoadContSize();
            LoadConType();
            PrepareGrid();
            btnImport.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
        }
        private void LoadContSize()
        {

            var type = sizeBll.Getall();
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
                cmbContSize.DataSource = dt_Types;
                cmbContSize.DisplayMember = "t_Name";
                cmbContSize.ValueMember = "t_ID";


                cmbContSize.DataSource = dt_Types;
                cmbContSize.DisplayMember = "t_Name";
                cmbContSize.ValueMember = "t_ID";
            }
            cmbContSize.SelectedIndex = 0;
            cmbContSize.SelectedIndex = 0;

        }
        private void LoadConType()
        {

            var type = typeBll.Getall();
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
                cmbConType.DataSource = dt_Types;
                cmbConType.DisplayMember = "t_Name";
                cmbConType.ValueMember = "t_ID";


                cmbConType.DataSource = dt_Types;
                cmbConType.DisplayMember = "t_Name";
                cmbConType.ValueMember = "t_ID";
            }
            cmbConType.SelectedIndex = 0;
            cmbConType.SelectedIndex = 0;



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
        
        private void LoadddlmloSearch()
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
                ddlMLOSearch.DataSource = dt_Types;
                ddlMLOSearch.DisplayMember = "t_Name";
                ddlMLOSearch.ValueMember = "t_ID";
            }
            ddlCusCode.SelectedIndex = 0;
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
                ddlCusCode.DataSource = dt_Types;
                ddlCusCode.DisplayMember = "t_Name";
                ddlCusCode.ValueMember = "t_ID";
            }
            ddlCusCode.SelectedIndex = 0;
        }
       
       
        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            dataGridView1.EnableHeadersVisualStyles = false;           
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 8;

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[0].HeaderText = "SL#";
                
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[1].HeaderText = "CONTAINER NO.";        

            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "SIZE";      

            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[3].HeaderText = "TYPE";

            dataGridView1.Columns[4].Width = 70;
            dataGridView1.Columns[4].HeaderText = "OUT TO";

            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[5].HeaderText = "TRAILER NO";        
            
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[6].HeaderText = "HAULAGE";

            dataGridView1.Columns[7].Width = 83;
            dataGridView1.Columns[7].HeaderText = "DATE";

        }

        private void btnLoadGateinData_Click(object sender, EventArgs e)
        {
            var custId = Convert.ToInt32(ddlCusCode.SelectedValue);
            listCSDGateIn = objBll.GetAllCSDGateInByMLOId(custId);
            if (listCSDGateIn.Count > 0)
            {
                MessageBox.Show(listCSDGateIn.Count.ToString()+" Record Found.", "Data Load Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnImport.Enabled = true;
            }
            else
            {
               MessageBox.Show("No Record Found.", "Data Load Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnImport.Enabled = false;

            }
            

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            if (opd.ShowDialog() == DialogResult.OK)
            {
                txtFileLocation.Text = opd.FileName;
                string filePath = opd.FileName;
                string extension = Path.GetExtension(filePath);
                DataSet dataSet = ImportExcelFile(extension, filePath);

                var status = CSDGateInListInsert(dataSet);
               MessageBox.Show(status, "Data Upload Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (opd.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("Cancel data upload !!", "Data Upload Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        public string CSDGateInListInsert(DataSet dataset)
        {
           
            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                CSDContGateInOut objCSDInOut = new CSDContGateInOut();
                try
                {
                    string slNo = row["SL#"].ToString().Trim();
                    string contNo = row["Container"].ToString().Trim();
                    string sz = row["SZ"].ToString().Trim();
                    string tp = row["TP"].ToString().Trim();                   
                    DateTime dateOut = Convert.ToDateTime(row["Date"]);
                    string outTo = row["Out To"].ToString().Trim();
                    string expVssl = row["Export Vessel"].ToString().Trim();
                    string rotationOut = row["Rotation"].ToString().Trim();                                    
                    string trailerNo = row["Trailer No"].ToString().Trim();
                    string haulage    = row["Haulage"].ToString().Trim();                   
                    string remrkOut = row["Remarks"].ToString().Trim();


                    objCSDInOut = listCSDGateIn.Where(x => x.ContNo == contNo).FirstOrDefault();

                    if (objCSDInOut != null)
                    {
                        objCSDInOut.ExpVssl = expVssl;
                        objCSDInOut.RotExp = rotationOut;

                        Depot objDepot = listDepot.Find(x => x.DepotCode.Trim() == outTo);
                        objCSDInOut.DepotTo = objDepot.DepotId;

                        if (haulage == "ELL")
                        {
                            objCSDInOut.TrailerOut = 1; //ELL
                            objCSDInOut.TrailerOutNo = "CMDHA-81-0547";
                            objCSDInOut.HaulierOut = 1; //ELL
                        }
                        else
                        {
                            objCSDInOut.TrailerOut = 18; //Others
                            objCSDInOut.TrailerOutNo = trailerNo;
                            objCSDInOut.HaulierOut = 2; //Others
                        }
                      
                        objCSDInOut.DateOut = dateOut;                       
                        objCSDInOut.LoadEmptyStatusOut = 1; //Empty

                        objCSDInOut.RemarkOut = remrkOut;
                        objCSDInOut.UserIdGateOut = user.UserId;
                        objCSDInOut.InOutStatus = 2;

                        listCSDGateOut.Add(objCSDInOut);
                        dataGridView1.Rows.Add(slNo, objCSDInOut.ContNo, sz, tp, outTo, trailerNo, haulage, dateOut.Date.ToString("dd/MMM/yy"));
                       // dataGridView1.Rows[dataGridView1.Rows.Count-1].DefaultCellStyle.BackColor = Color.Yellow;

                    }
                    else
                    {
                        dataGridView1.Rows.Add(slNo, contNo, sz, tp, outTo, trailerNo, haulage, dateOut.Date.ToString("dd/MMM/yy"));
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.OrangeRed;

                    }                    
                }
                catch (Exception ex)
                {
                    ClearForm();
                    return ex.Message.ToString();               
                }
                
            }

            dataGridView1.ClearSelection();
            btnSave.Enabled = true;
            return "Data  loaded successfully";

        }
     

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (listCSDGateOut.Count == 0)
            {

                MessageBox.Show("No data to update !!", "CSD Data Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var status = objBll.UpdateCSDList(listCSDGateOut);
            MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();

        }

       
        private void btnDelete_Click(object sender, EventArgs e)
        {
        //    DialogResult result = MessageBox.Show("Do you really want to Delete ??",
        //                  "Confirm CSD Gate-In deletion",
        //                  MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        //    if (result == DialogResult.Yes)
        //    {
        //        if (objCSD != null)
        //        {

        //            var obj = listCSDGateOut.ElementAt(index);
        //            listCSDGateOut.Remove(obj);                   
        //            dataGridView1.Rows.RemoveAt(index);
        //            //ResetForm();
        //        }

        //    }
            
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
            txtFileLocation.Text = "";
            ddlCusCode.SelectedValue = 0;           
            listCSDGateIn = new List<CSDContGateInOut>();
            listCSDGateOut = new List<CSDContGateInOut>();
            objCSD = new CSDContGateInOut();                   
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnImport.Enabled = false;

        }

        private void CSDGateOutListUpload_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearForm();
        }
        private void CalculateBoxTues(IEnumerable<CSDContGateInOut> listUpcomingCont)
        {
            int box = listUpcomingCont.Count();
            var numberOfForty = listUpcomingCont.Where(x => Convert.ToInt32(x.ContSize) > 20);
            var numberOftwenty = listUpcomingCont.Where(x => Convert.ToInt32(x.ContSize) == 20);
            txtTotalTues.Text = Convert.ToString(Convert.ToInt32(numberOftwenty.Count()) * 1 + Convert.ToInt32(numberOfForty.Count()) * 2);
            txtTotalBox.Text = box.ToString();
        }
        private void PopulareGrid(List<CSDContGateInOut> listUpcoming)
        {
            int index = 1;
            foreach (var item in listUpcoming)
            {
                //dataGridView1.Rows.Add(index, item.MLOCode, item.ContainerNo, item.SizeName, item.Type, item.ImportVasselName, item.RotationNumber);
                ContainerSize size = objSizelist.Find(x => x.ContainerSizeId == item.ContSize);
                ContainerType Type = objTypelist.Find(x => x.ContainerTypeId == item.ContType);

                dataGridView1.Rows.Add(item.RefNo, item.ContNo, size.ContainerSize1, Type.ContainerTypeName, item.ImpVssl, item.DateIn, item.TrailerInNo, item.ContInCondition == 1 ? "Sound" : "Damage");
                index = index + 1;
            }
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
                        PopulareGrid(listCSDGateOut);
                        CalculateBoxTues(listCSDGateOut);
                        break;
                    }
                case 2:
                    {
                        //-----Container 
                        if (searchBy == 2 && searchText == "")
                        {
                            MessageBox.Show("Search text can't be empty !!", "Input Required !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        var list = listCSDGateOut.Where(x => x.ContNo == searchText).ToList();
                        PopulareGrid(list);
                        CalculateBoxTues(list);
                        break;
                    }
                case 3:
                    {
                        //---Size 
                        // listUpcomingCont = listUpcomingCont.Where(x => x.SizeId == Size && x.TypeId == type).ToList();
                        // PopulareGrid(listUpcomingCont);

                        if (listCSDGateOut.Count > 0)
                        {
                            if (Size != 0 && type != 0)
                            {
                                var list = listCSDGateOut.Where(x => x.ContSize == Size && x.ContType == type).ToList();
                                PopulareGrid(list);
                                CalculateBoxTues(list);
                            }
                            else
                            if (Size != 0 && type == 0)
                            {
                                var list = listCSDGateOut.Where(x => x.ContSize == Size).ToList();
                                PopulareGrid(list);
                                CalculateBoxTues(list);
                            }
                            else
                            if (Size == 0 && type != 0)
                            {
                                var list = listCSDGateOut.Where(x => x.ContType == type).ToList();
                                PopulareGrid(list);
                                CalculateBoxTues(list);
                            }
                        }
                        break;
                    }

                case 4:
                    {
                        /// MLO
                        var list = listCSDGateOut.Where(x => x.CustId == Convert.ToInt32(ddlMLOSearch.SelectedValue)).ToList();
                        PopulareGrid(list);
                        CalculateBoxTues(list);
                        break;
                    }
                default:
                    {
                        break;

                    }
            }
        }

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearch.SelectedIndex == 2)
            {
                txtSearch.Visible = true;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlMLOSearch.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 3)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = true;
                cmbConType.Visible = true;
                ddlMLOSearch.Visible = false;
            }
            if (cmbSearch.SelectedIndex == 4)
            {
                txtSearch.Visible = false;
                cmbContSize.Visible = false;
                cmbConType.Visible = false;
                ddlMLOSearch.Visible = true;
            }
        }
    }
}
