using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LOGISTIC.BLL;
using System.Linq;

namespace LOGISTIC.UI.Import
{
    public partial class ContainerSearch : Form
    {

       
        private IGMImportBLL objBll = new IGMImportBLL();

        private static List<IGMImportDetail> listIGMDetails = new List<IGMImportDetail>();
        public ContainerSearch()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);                      

        }
        private void ContainerSearch_Load(object sender, EventArgs e)
        {
           
            ComboLoad();
            PrepareGrid();
            LoadGrid();

            listIGMDetails = objBll.GetAllIGMImportDetails();
            CalculateBoxTues(listIGMDetails);
        }
        private void CalculateBoxTues(IEnumerable<IGMImportDetail> listUpcomingCont)
        {
            int box = listUpcomingCont.Count();
            var numberOfForty = listUpcomingCont.Where(x => Convert.ToInt32(x.ContainerSize.ContainerSize1) > 20);
            var numberOftwenty = listUpcomingCont.Where(x => Convert.ToInt32(x.ContainerSize.ContainerSize1) == 20);
            txtTotalTues.Text = Convert.ToString(Convert.ToInt32(numberOftwenty.Count()) * 1 + Convert.ToInt32(numberOfForty.Count()) * 2);
            txtTotalBox.Text = box.ToString();
        }
        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "All");
            cmbSearch.Items.Insert(1, "Container Number");
            cmbSearch.Items.Insert(2, "IGM Reference");
            cmbSearch.Items.Insert(3, "B/L Number");           
            cmbSearch.Items.Insert(4, "Rotation");           

            cmbSearch.SelectedIndex = 0;

        }

        public void PrepareGrid()
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 9;

            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].HeaderText = "Container No";

            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[2].HeaderText = "Size";

            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[3].HeaderText = "Type";

            dataGridView1.Columns[4].HeaderText = "Seal No";

            dataGridView1.Columns[5].HeaderText = "BL Number";

            dataGridView1.Columns[6].Width = 90;
            dataGridView1.Columns[6].HeaderText = "Line No";

            dataGridView1.Columns[7].HeaderText = "Entry Date";

            dataGridView1.Columns[8].HeaderText = "Status";

        }
        public void LoadGrid()
        {
            listIGMDetails = objBll.GetAllIGMImportDetails();
            dataGridView1.Rows.Clear();

            if (listIGMDetails.Count > 0)
            {
                int index = 0;
                foreach (var item in listIGMDetails)
                {
                    
                    dataGridView1.Rows.Add(index+1, item.ContainerNo, item.ContainerSize.ContainerSize1, item.ContainerType.ContainerTypeName, item.SealNo, item.IGMImport.BLnumber, item.IGMImport.LineNo, item.Date.Value.Date.ToString("dd/MMM/yy"), "No Action");
                    if (item.InOutStatus == 1)
                    {
                        dataGridView1.Rows[index].Cells[8].Value = "Gate In";
                        dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Khaki;                      
                    }
                   else if (item.InOutStatus == 2)
                    {
                        dataGridView1.Rows[index].Cells[8].Value = "Gate Out";
                        dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Goldenrod;
                    }
                    index = index + 1;
                }

            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string slect = cmbSearch.Text.Trim();
            string value = txtSearch.Text.Trim().ToString();

            
           if (slect != "All" && value == "")
            {
                MessageBox.Show(" Search text can't be empty");
                return;
            } 

            switch (slect)
            {             
                case "Container Number":
                    {
                        listIGMDetails = objBll.GetIGMImportDetailsByContNum(value);
                        if (listIGMDetails.Count > 0)
                        {
                            FilterGrid();
                        }
                        else
                        {
                            dataGridView1.Rows.Clear();
                            MessageBox.Show("No data found !!");
                        }
                           
                        break;
                    }
                case "B/L Number":
                    {
                       listIGMDetails = objBll.GetIGMImportDetailsBLNumber(value);
                        if (listIGMDetails.Count > 0)
                        {
                            FilterGrid();
                        }
                        else
                        {
                            dataGridView1.Rows.Clear();
                            MessageBox.Show("No data found !!");
                        }
                        break;
                    }
                case "IGM Reference":
                    {
                        listIGMDetails = objBll.GetIGMImportDetailsByIGMNumber(value);
                        if (listIGMDetails.Count > 0)
                        {
                            FilterGrid();
                        }
                        else
                        {
                            dataGridView1.Rows.Clear();
                            MessageBox.Show("No data found !!");
                        }
                        break;
                    }
                case "Rotation":
                    {
                        listIGMDetails = objBll.GetIGMImportDetailsByRotation(value);
                        if (listIGMDetails.Count > 0)
                        {
                            FilterGrid();
                        }
                        else
                        {
                            dataGridView1.Rows.Clear();
                            MessageBox.Show("No data found !!");
                        }
                        break;
                    }
                default:
                    {
                        LoadGrid();
                        break;
                    }
            }

        }

        public void FilterGrid()
        {
            dataGridView1.Rows.Clear();

            int index = 0;
            foreach (var item in listIGMDetails)
            {

                dataGridView1.Rows.Add(index + 1, item.ContainerNo, item.ContainerSize.ContainerSize1, item.ContainerType.ContainerTypeName, item.SealNo, item.IGMImport.BLnumber, item.IGMImport.LineNo, item.Date.Value.Date.ToString("dd/MMM/yy"), "No Action");
                if (item.InOutStatus == 1)
                {
                    dataGridView1.Rows[index].Cells[8].Value = "Gate In";
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Khaki;
                }
                else if (item.InOutStatus == 2)
                {
                    dataGridView1.Rows[index].Cells[8].Value = "Gate Out";
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Goldenrod;
                }
                index = index + 1;
            }
            dataGridView1.ClearSelection();

        }       

        private void ClearGrid()
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();            

        }      

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var index = Convert.ToInt32(selectedRow.Index);
            var objIDtls = listIGMDetails.ElementAt(index);

           // var contStatus = Convert.ToString(selectedRow.Cells[9].Value);

            if (objIDtls.InOutStatus == 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Gate In ??",
                          "Confirm IGM Import Gate In",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    //var IGMDetailsId = Convert.ToInt32(selectedRow.Cells[0].Value);
                    NavigateToGateIn(objIDtls.IGMDetailsId);                    
                }
            }
            else if (objIDtls.InOutStatus == 1)
            {
                DialogResult result = MessageBox.Show("Do you want to Gate Out ??",
                          "Confirm IGM Import Gate Out",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    //var IGMDetailsId = Convert.ToInt32(selectedRow.Cells[0].Value);
                    NavigateToGateOut(objIDtls.IGMDetailsId);
                }
            }
            else if (objIDtls.InOutStatus == 2)
            {
                MessageBox.Show("This container has already been Gate Out ??");
                dataGridView1.ClearSelection();                                                
            }

        }

        private void NavigateToGateIn(long IGMDetailsId)
        {
            IGMImportDetail objIGMImportDetails = new IGMImportDetail();
            objIGMImportDetails = objBll.GetIGMImportDetailById(IGMDetailsId);

            IGMGateIn f = new IGMGateIn(objIGMImportDetails);
            f.MdiParent = this.ParentForm;
            f.Show();

        }

        private void NavigateToGateOut(long IGMDetailsId)
        {
            IGMImportDetail objIGMdetails = new IGMImportDetail();
            objIGMdetails = objBll.GetIGMImportDetailById(IGMDetailsId);

            IGMGateOut f = new IGMGateOut(objIGMdetails);
            f.MdiParent = this.ParentForm;
            f.Show();

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadGrid();
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
