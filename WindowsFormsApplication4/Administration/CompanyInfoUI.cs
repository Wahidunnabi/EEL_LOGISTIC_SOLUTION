using System;
using System.Collections.Generic;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace LOGISTIC.UI.Administration
{
    public partial class CompanyInfoUI : Form
    {
        private List<CompanyInfo> listCompanyinfo = new List<CompanyInfo>();
        private CompanyInfo objCompanyinfo = new CompanyInfo();

        private CompanyInfoBLL objBll = new CompanyInfoBLL();   
       

        public CompanyInfoUI()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);          
        }

        private void CompanyInfoUI_Load(object sender, EventArgs e)
        {
          
            PrepareGrid();
            LoadDataToGrid();
            btndelete.Enabled = false;

        }
     

       
        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].HeaderText = "SL";
           

            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[1].HeaderText = "Code";
           

            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[2].HeaderText = "Name";

            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[3].HeaderText = "Telephone";

           // dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[4].HeaderText = "Address";
           


            dataGridView1.AllowUserToAddRows = false;


        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listCompanyinfo = objBll.Getall();
            if (listCompanyinfo.Count > 0)
            {
                int index = 1;
                foreach (var item in listCompanyinfo)
                {
                    dataGridView1.Rows.Add(index, item.CompanyCode, item.Name, item.Telephone, item.Address);
                    index = index + 1;
                }

            }
           
            dataGridView1.ClearSelection();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = Validation();
            if (flag==true)
            {
                FillingData();
                SaveData();
                LoadDataToGrid();
                ClearForm();
            }
            

        }      

        private void btndelete_Click(object sender, EventArgs e)
        {
           
            DialogResult result = MessageBox.Show("Do you really want to Delete ??",
               "Confirm customer deletion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
               if (result == DialogResult.Yes)
               {
                  if (objCompanyinfo.CompanyId != 0)
                   {

                    var status = objBll.Delete(objCompanyinfo.CompanyId);
                    MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGrid();
                    
                   }
                
               }

               ClearForm(); 
          
            }
            
        private void btnCancel_Click(object sender, EventArgs e)
        {
          ClearForm();
        }    

        private void btnClose_Click(object sender, EventArgs e)
        {
        
            this.Close();
        }

       

        private bool Validation()
        {
            //var errMessage = "";

            //if (txtCompanyCode.Text.Trim() == string.Empty)
            //{
            //    errMessage = errMessage + "* Please enter customer code !!\n";
            //}           
            //if (txtCompanyName.Text.Trim() == string.Empty)
            //{
            //    errMessage = errMessage + "* Please enter customer name !!\n";  
            //}            
            //if (errMessage != "")
            //{
            //    MessageBox.Show(errMessage,"Error");                   
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}

            return true;
        }

        private void FillingData()
        {
            objCompanyinfo.CompanyCode = txtCompanyCode.Text.Trim();
            objCompanyinfo.Name = txtCompanyName.Text.Trim();
            objCompanyinfo.Telephone = txtTelephone.Text.Trim();
            objCompanyinfo.Address = txtAddress.Text.Trim();
            objCompanyinfo.Email = txtEmail.Text.Trim();
            objCompanyinfo.EntryDate = dateEntry.Value;

            OpenFileDialog OpenFD = new OpenFileDialog();
            OpenFD.Title = "Select Files";
            OpenFD.Filter = "Jpg|*.jpg|Jpge|*.jpge|Png|*.png|Gif|*.gif";
            OpenFD.FileName = null;
            string fileName;
            if (OpenFD.ShowDialog() != DialogResult.Cancel)
            {
              
                fileName = OpenFD.FileName;
                try
                {
                    // show it to picturebox
                    pictureBox1.Load(fileName);
                    lblImageAddress.Text = OpenFD.FileName;
                    // Here get_image is a function and Big is the byte[] type
                    objCompanyinfo.Logo = get_image(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message.ToString());
                }
            }



           // objCompanyinfo.Logo = big;

            //try
            //{
            //    objCompanyinfo.CompanyCode = txtCompanyCode.Text.Trim();
            //    objCompanyinfo.Name = txtCompanyName.Text.Trim();
            //    objCompanyinfo.Telephone = txtTelephone.Text.Trim();
            //    objCompanyinfo.Address = txtAddress.Text.Trim();
            //    objCompanyinfo.Email = txtEmail.Text.Trim();               
            //    objCompanyinfo.EntryDate = DateTime.Now;


            //    // open file dialog   
            //    OpenFileDialog open = new OpenFileDialog();
            //    // image filters  
            //    open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            //    if (open.ShowDialog() == DialogResult.OK)
            //    {
            //        // display image in picture box  
            //        pictureBox1.Image = new Bitmap(open.FileName);
            //        // image file path  
            //        txtImageName.Text = open.FileName;
            //    }


            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}


        }

        public byte[] get_image(string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            byte[] photo = reader.ReadBytes((int)stream.Length);
            reader.Close();
            stream.Close();

            return photo;
        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
                var status = objBll.Insert(objCompanyinfo);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (btnSave.Text == "Update")
            {
                var status = objBll.Update(objCompanyinfo);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var index = Convert.ToInt32(selectedRow.Index);
            objCompanyinfo = listCompanyinfo.ElementAt(index);


            dateEntry.Value = Convert.ToDateTime(objCompanyinfo.EntryDate);
            txtCompanyCode.Text = objCompanyinfo.CompanyCode;
            txtCompanyName.Text = objCompanyinfo.Name;          
            txtEmail.Text = objCompanyinfo.Email;
            txtTelephone.Text = objCompanyinfo.Telephone;
            txtAddress.Text = objCompanyinfo.Address;
            MemoryStream ms = new MemoryStream(objCompanyinfo.Logo);
            pictureBox1.Image = Image.FromStream(ms);
            btnSave.Text = "Update";
            btndelete.Enabled = true;

        }

        private void ClearForm()
        {
            txtCompanyCode.Text = "";
            txtCompanyName.Text = "";
            txtEmail.Text = "";
            dateEntry.Value = DateTime.Now;
            txtTelephone.Text = "";
            txtAddress.Text = "";            
            btnSave.Text = "Save";          
            dataGridView1.ClearSelection();
            objCompanyinfo = new CompanyInfo();
            btndelete.Enabled = false;
        }

        
    }
}
