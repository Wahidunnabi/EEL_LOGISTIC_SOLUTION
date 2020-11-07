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
    public partial class CSDGateInListUpload : Form
    {

        private CustomerBll MLOBll = new CustomerBll();
        private ContainerSizeBll sizeBll = new ContainerSizeBll();
        private ContainerTypeBll typeBll = new ContainerTypeBll();

        private DepotBll depotBll = new DepotBll();
        private List<Depot> listDepot = new List<Depot>();

        private CSDGateInOutBLL objBll = new CSDGateInOutBLL();

        private List<ContainerSize> objSizelist = new List<ContainerSize>();
        private List<ContainerType> objTypelist = new List<ContainerType>();

        private static List<CSDContGateInOut> listCSD = new List<CSDContGateInOut>();
        private CSDContGateInOut objCSD = new CSDContGateInOut();

        private UserInfo user;
        private int index;

        public CSDGateInListUpload(UserInfo user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            this.user = user;

        }

        private void CSDGateInListUpload_Load(object sender, EventArgs e)
        {
            objSizelist = sizeBll.Getall();
            objTypelist = typeBll.Getall();
            listDepot = depotBll.Getall();
            LoadcmbSearch();
            LoadContSize();
            LoadConType();
            LoadCustomer();
            LoadCustomerSearch();
            PrepareGrid();
            Select.Enabled = false;
            btnSave.Enabled = false;
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

        private void LoadCustomerSearch()
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
        
        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 9;

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[0].HeaderText = "REF No.";

            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[1].HeaderText = "CONTAINER NO.";

            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].HeaderText = "SIZE";

            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[3].HeaderText = "TYPE";

            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[4].HeaderText = "VESSEL";

            dataGridView1.Columns[5].Width = 70;
            dataGridView1.Columns[5].HeaderText = "FROM";

            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[6].HeaderText = "TRAILER NO.";

            dataGridView1.Columns[7].Width = 83;
            dataGridView1.Columns[7].HeaderText = "CONDITION";

            dataGridView1.Columns[8].Width = 83;
            dataGridView1.Columns[8].HeaderText = "Status";



        }

        private void Select_Click(object sender, EventArgs e)
        {
            var custId = Convert.ToInt32(ddlCusCode.SelectedValue);


            if (custId > 0)
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
            else
            {
                MessageBox.Show("Please select an MLO ??", "Selection Required !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            int custId = Convert.ToInt32(ddlCusCode.SelectedValue);

            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                CSDContGateInOut objCSDInOut = new CSDContGateInOut();
                try
                {
                    objCSDInOut.CustId = custId;
                    objCSDInOut.RefNo = Convert.ToInt64(row["Ref No"].ToString().Trim());
                    objCSDInOut.ContNo = row["Container"].ToString().Trim();
                    objCSDInOut.DateIn = Convert.ToDateTime(row["Date In"].ToString().Trim());


                    int sz = Convert.ToInt32(row["SZ"]);
                    ContainerSize objSize = objSizelist.Find(x => x.ContainerSize1 == sz);
                    objCSDInOut.ContSize = objSize.ContainerSizeId;


                    string tp = row["TP"].ToString().Trim();
                    ContainerType objTyp = objTypelist.Find(x => x.ContainerTypeName.Trim() == tp);
                    objCSDInOut.ContType = objTyp.ContainerTypeId;


                    objCSDInOut.ISO = row["ISO"].ToString().Trim();

                    objCSDInOut.ImpVssl = row["Import Vessel"].ToString().Trim();
                    objCSDInOut.RotImp = row["Rotation"].ToString().Trim();

                    string dptFrom = row["From"].ToString().Trim();
                    Depot objDepot = listDepot.Find(x => x.DepotCode.Trim() == dptFrom);
                    objCSDInOut.DepotFrom = objDepot.DepotId;

                    string trailerNo = row["Trailer No"].ToString().Trim();
                    string haulage = row["Haulage"].ToString().Trim();

                    if (haulage == "ELL")
                    {
                        objCSDInOut.TrailerIn = 1; //ELL
                        objCSDInOut.TrailerInNo = "CMDHA-81-0546";
                        objCSDInOut.HaulierIn = 1; //ELL
                    }
                    else
                    {
                        objCSDInOut.TrailerIn = 18; //Others
                        objCSDInOut.TrailerInNo = trailerNo;
                        objCSDInOut.HaulierIn = 2; //Others
                    }

                    string cond = row["Condition"].ToString().Trim();
                    //objCSDInOut.ContInCondition = cond == "Sound" ? 1 : 2; 
                    objCSDInOut.ContInCondition = 1; // Always input as sound/ if any damage come operator will take care of it
                    objCSDInOut.InOutStatus = 1;
                    string LoadEmptystatus = row["Status"].ToString().Trim();
                    if (LoadEmptystatus == "Empty")
                    {
                        objCSDInOut.LoadEmptyStatus = 1;
                    } else if (LoadEmptystatus == "Load")
                    {
                        objCSDInOut.LoadEmptyStatus = 2;
                    }
                    //objCSDInOut.LoadEmptyStatus = cond == "LoadEmptystatus" ? 1 : 2; //Empty


                    if (objDepot.DepotId == 87)
                    {
                        objCSDInOut.LoadEmptyStatus = 3; // Dump  
                            
                    }

                    objCSDInOut.UserIdGateIn = user.UserId;
                    objCSDInOut.RemarkIn = row["Remarks"].ToString().Trim();

                    listCSD.Add(objCSDInOut);
                    dataGridView1.Rows.Add(objCSDInOut.RefNo, objCSDInOut.ContNo, sz.ToString(), tp, objCSDInOut.ImpVssl, dptFrom, objCSDInOut.TrailerInNo, objCSDInOut.ContInCondition == 1 ? "Sound" : "Damage", objCSDInOut.LoadEmptyStatus);

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


        private string ValidateCSD(CSDContGateInOut objCSDInOut)
        {

            var errMessage = "";

            if (objCSDInOut.ContNo.Length < 7 || objCSDInOut.ContNo == "")
            {
                errMessage = errMessage + "* Container number format error !!\n";

            }
            if (objCSDInOut.ContSize == 0 || objCSDInOut.ContSize == null)
            {
                errMessage = errMessage + "* Container size does not match !!\n";

            }
            if (objCSDInOut.ContType == 0 || objCSDInOut.ContType == null)
            {
                errMessage = errMessage + "* Container type does not match !!\n";

            }
            return errMessage;

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listCSD.Count == 0)
            {

                MessageBox.Show("No data to insert !!", "CSD Data Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var status = objBll.InsertCSDList(listCSD);
            MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();

        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
                          "Confirm CSD Gate-In deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (objCSD != null)
                {
                    var obj = listCSD.ElementAt(index);
                    listCSD.Remove(obj);
                    dataGridView1.Rows.RemoveAt(index);
                    ResetForm();
                }

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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            index = Convert.ToInt32(selectedRow.Index);
            objCSD = listCSD.ElementAt(index);
            btnDelete.Enabled = true;
        }


        private void ResetForm()
        {
            txtFileLocation.Text = "";
            ddlCusCode.SelectedValue = 0;
            index = 0;
            objCSD = new CSDContGateInOut();
            dataGridView1.ClearSelection();

            Select.Enabled = false;
            btnDelete.Enabled = false;

        }

        private void ClearForm()
        {
            dataGridView1.Rows.Clear();
            txtFileLocation.Text = "";
            ddlCusCode.SelectedValue = 0;

            index = 0;
            listCSD = new List<CSDContGateInOut>();
            objCSD = new CSDContGateInOut();
            dataGridView1.ClearSelection();

            Select.Enabled = false;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;

        }

        private void ddlCusCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlCusCode.SelectedValue) > 0)
            {
                Select.Enabled = true;
            }
            else
            {
                Select.Enabled = false;
            }
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
                        var list = listCSD;
                        PopulareGrid(list);
                        CalculateBoxTues(list);
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
                        var list = listCSD.Where(x => x.ContNo == searchText).ToList();
                        PopulareGrid(list);
                        CalculateBoxTues(list);
                        break;
                    }
                case 3:
                    {
                        //---Size 
                        // listUpcomingCont = listUpcomingCont.Where(x => x.SizeId == Size && x.TypeId == type).ToList();
                        // PopulareGrid(listUpcomingCont);

                        if (listCSD.Count > 0)
                        {
                            if (Size != 0 && type != 0)
                            {
                                var list = listCSD.Where(x => x.ContSize == Size && x.ContType == type).ToList();
                                PopulareGrid(list);
                                CalculateBoxTues(list);
                            }
                            else
                            if (Size != 0 && type == 0)
                            {
                                var list = listCSD.Where(x => x.ContSize == Size).ToList();
                                PopulareGrid(list);
                                CalculateBoxTues(list);
                            }
                            else
                            if (Size == 0 && type != 0)
                            {
                                var list = listCSD.Where(x => x.ContType == type).ToList();
                                PopulareGrid(list);
                                CalculateBoxTues(list);
                            }
                        }
                        break;
                    }

                case 4:
                    {
                        /// MLO
                        var list = listCSD.Where(x => x.CustId == Convert.ToInt32(cmbmloSearch.SelectedValue)).ToList();
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
        private void PopulareGrid(IEnumerable<CSDContGateInOut> listUpcoming)
        {
            int index = 1;
            foreach (var item in listUpcoming)
            {
                //dataGridView1.Rows.Add(index, item.MLOCode, item.ContainerNo, item.SizeName, item.Type, item.ImportVasselName, item.RotationNumber);
                ContainerSize size = objSizelist.Find(x => x.ContainerSizeId == item.ContSize);
                ContainerType Type = objTypelist.Find(x => x.ContainerTypeId == item.ContType);

                dataGridView1.Rows.Add(item.RefNo, item.ContNo,size.ContainerSize1, Type.ContainerTypeName, item.ImpVssl, item.DateIn, item.TrailerInNo, item.ContInCondition == 1 ? "Sound" : "Damage");
                index = index + 1;
            }
        }
        private void CalculateBoxTues(IEnumerable<CSDContGateInOut> listUpcomingCont)
        {
            int box = listUpcomingCont.Count();
            var numberOfForty = listUpcomingCont.Where(x => Convert.ToInt32(x.ContSize) > 20);
            var numberOftwenty = listUpcomingCont.Where(x => Convert.ToInt32(x.ContSize) == 20);
            txtTotalTues.Text = Convert.ToString(Convert.ToInt32(numberOftwenty.Count()) * 1 + Convert.ToInt32(numberOfForty.Count()) * 2);
            txtTotalBox.Text = box.ToString();
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
