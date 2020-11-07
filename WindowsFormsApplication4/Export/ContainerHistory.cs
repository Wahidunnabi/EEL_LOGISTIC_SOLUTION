using LOGISTIC.BLL;
using LOGISTIC.CSD.BLL;
using LOGISTIC.UserDefinedModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LOGISTIC.UI.Administration
{
    public partial class ContainerHistory : Form
    {
        CSDGateInOutBLL objCSDBLL = new CSDGateInOutBLL();
        private List<CSDContGateInOut> listCSDInOut = new List<CSDContGateInOut>();

        ExportReportBLL reportBll = new ExportReportBLL();

        public ContainerHistory()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 0);
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void ContainerHistory_Load(object sender, EventArgs e)
        {
            cmbSearchLoad();
            PrepareGrid();
        }

        private void cmbSearchLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");          
            cmbSearch.Items.Insert(1, "Container Number");           
            cmbSearch.Items.Insert(2, "Reference No");
            cmbSearch.SelectedIndex = 0;

        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.ColumnCount = 9;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";


            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[1].HeaderText = "Container No";


            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[2].HeaderText = "Reference No";



            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[3].HeaderText = "Customer";
          


            dataGridView1.Columns[4].Width = 40;
            dataGridView1.Columns[4].HeaderText = "Type";
            


            dataGridView1.Columns[5].Width = 40;
            dataGridView1.Columns[5].HeaderText = "Size";


            dataGridView1.Columns[6].Width = 120;
            dataGridView1.Columns[6].HeaderText = "GateIn Date";


            dataGridView1.Columns[7].Width = 120;
            dataGridView1.Columns[7].HeaderText = "GateOut Date";
           

            dataGridView1.Columns[8].Width = 150;
            dataGridView1.Columns[8].HeaderText = "AV/DM Status";
           
          
           

            //dataGridView1.Columns[12].Visible = false;
            //dataGridView1.Columns[12].DataPropertyName = "ContainerGateEntryId";


        }
       
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int index = cmbSearch.SelectedIndex;
            string value = txtSearch.Text.Trim();

            if (index == 0)
            {

                MessageBox.Show("Please select a search item !!", "Selection Required !!", MessageBoxButtons.OK, MessageBoxIcon.Information);              
                return;
            }
            else if (index != 0 && value == "")
            {
                MessageBox.Show("Search text can't be empty !!", "Input Required !!", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                return;
            }
            switch (index)
            {

                case 1:  //Container Number
                    {
                       listCSDInOut = objCSDBLL.GetlistCSDByContNumber(value);

                        if (listCSDInOut.Count > 0)
                        {
                            LoadDataToGrid();

                        }
                        else
                        {
                            dataGridView1.Rows.Clear();
                            dataGridView1.Refresh();
                            MessageBox.Show("Container No : "+ value + " not found !", "Data Not Found !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }                       
                        break;
                    }
                case 2: //Reference No
                    {
                        try
                        {
                            var refNo = Convert.ToInt64(value);
                            listCSDInOut = objCSDBLL.GetListCSDByRefNumber(refNo);

                            if (listCSDInOut.Count > 0)
                            {
                                LoadDataToGrid();

                            }
                            else
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Refresh();
                                MessageBox.Show("Reference No : " + value + " not found !", "Data Not Found !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            break;
                        }
                        catch
                        {
                            MessageBox.Show("Reference number should be numeric value !!", "Input Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                    }                             
                default:
                    {                       
                        break;
                    }

            }
        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            int index = 1;
            foreach (var item in listCSDInOut)
            {
                dataGridView1.Rows.Add(index, item.ContNo, item.RefNo, item.Customer.CustomerName, item.ContainerType.ContainerTypeName, item.ContainerSize.ContainerSize1, item.DateIn.Value.ToString("dd/MMM/yyyy"), item.DateOut, item.ContInCondition == 1 ? "Sound" : "Damage");
                index = index + 1;
            }

            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var index = Convert.ToInt32(selectedRow.Index);
            CSDContGateInOut objCSD = listCSDInOut.ElementAt(index);
            var contHistory = reportBll.GetContainerHistory(objCSD.ContainerGateEntryId);          
            if (contHistory != null)
            {
                BindCSDDatatoField(contHistory);
            }
            else
            {
                ClearDataField();
                MessageBox.Show("Failed to load data!! May be tables do not have necessary data in joining relation.", "Loading Failed !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void BindCSDDatatoField(DataTable dt)
        {
            txtInDate.Text = dt.Rows[0].Field<string>("DateIn");
            txtInVessel.Text = dt.Rows[0].Field<string>("ImpVssl");
            txtInRotation.Text = dt.Rows[0].Field<string>("RotImp");
            txtBroughtFrom.Text = dt.Rows[0].Field<string>("depotFrom");
            txtHaulierIn.Text = dt.Rows[0].Field<string>("hulierIn");
            txtTrailerIn.Text = dt.Rows[0].Field<string>("TrailerInNo");
            txtChallanIn.Text = dt.Rows[0].Field<string>("ChallanNo");
            txtAVStatus.Text = dt.Rows[0].Field<string>("conditionIn");
            txtremarksIn.Text = dt.Rows[0].Field<string>("RemarkIn");
            txtGateInBy.Text = dt.Rows[0].Field<string>("UserGateIn");


            txtOutDate.Text = dt.Rows[0].Field<string>("DateOut");
            txtOutVessel.Text = dt.Rows[0].Field<string>("ExpVssl");
            txtExpRotation.Text = dt.Rows[0].Field<string>("RotExp");
            txtOutTo.Text = dt.Rows[0].Field<string>("depotTo");
            txtHaulierOut.Text = dt.Rows[0].Field<string>("haulierOut");
            txtTrailerOut.Text = dt.Rows[0].Field<string>("TrailerOutNo");
            txtChallanOut.Text = dt.Rows[0].Field<string>("ChallanOut");
            txtAvStatusOut.Text = dt.Rows[0].Field<string>("conditionOut");           
            txtRemarksOut.Text = dt.Rows[0].Field<string>("RemarkOut");
            txtGateOutBy.Text = dt.Rows[0].Field<string>("UserGateout");

            txtAccount.Text = dt.Rows[0].Field<string>("CustomerCode");
            txtStuffingDate.Text = dt.Rows[0].Field<string>("StuffingDate");
            txtSealNo.Text = dt.Rows[0].Field<string>("SealNo");
            txtLOC.Text = dt.Rows[0].Field<string>("LocationName");
            txtShift.Text = dt.Rows[0].Field<string>("Shift");
            txtTareWait.Text = dt.Rows[0].Field<string>("TareWT");
            txtPlugIn.Text = dt.Rows[0].Field<string>("PluginDate");
            txtStuffedBy.Text = dt.Rows[0].Field<string>("UserStuffed");
            
        }

        public void ClearDataField()
        {

            txtInDate.Text = "";
            txtInVessel.Text = "";
            txtInRotation.Text = "";
            txtBroughtFrom.Text = "";
            txtHaulierIn.Text = "";
            txtTrailerIn.Text = "";
            txtChallanIn.Text = "";
            txtAVStatus.Text = "";
            txtremarksIn.Text = "";
            txtGateInBy.Text = "";


            txtOutDate.Text = "";
            txtOutVessel.Text = "";
            txtExpRotation.Text = "";
            txtOutTo.Text = "";
            txtHaulierOut.Text = "";
            txtTrailerOut.Text = "";
            txtChallanOut.Text = "";
            txtAvStatusOut.Text = "";
            txtRemarksOut.Text = "";
            txtGateOutBy.Text = "";

            txtAccount.Text = "";
            txtStuffingDate.Text = "";
            txtSealNo.Text = "";
            txtLOC.Text = "";
            txtShift.Text = "";
            txtTareWait.Text = "";
            txtStuffedBy.Text = "";



        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";

            txtInDate.Text = "";
            txtInVessel.Text = "";
            txtInRotation.Text = "";
            txtBroughtFrom.Text = "";
            txtHaulierIn.Text = "";
            txtTrailerIn.Text = "";
            txtChallanIn.Text = "";
            txtAVStatus.Text = "";
            txtremarksIn.Text = "";
            txtGateInBy.Text = "";
            txtOutDate.Text = "";
            txtOutVessel.Text = "";
            txtExpRotation.Text = "";
            txtOutTo.Text = "";
            txtHaulierOut.Text = "";
            txtTrailerOut.Text = "";
            txtChallanOut.Text = "";
            txtAvStatusOut.Text = "";          
            txtRemarksOut.Text = "";
            txtGateOutBy.Text = "";
            txtAccount.Text = "";
            txtStuffingDate.Text = "";
            txtSealNo.Text = "";
            txtLOC.Text = "";
            txtShift.Text = "";          
            txtTareWait.Text = "";
            txtStuffedBy.Text = "";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
