using System;
using System.Collections.Generic;
using System.Data;
using LOGISTIC.BLL;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace LOGISTIC.UI.Administration
{
    public partial class CreateUser : Form
    {
        private List<UserInfo> listUser = new List<UserInfo>();
        private UserInfo objUser = new UserInfo();
        private UserBLL objBLL = new UserBLL();


        public CreateUser()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        private void CreateUser_Load(object sender, EventArgs e)
        {
            
            LoadUserType();
            ComboLoad();
            PrepareGrid();
            LoadDataToGrid();
            btndelete.Enabled = false;
            pictureBox1.Visible = false;
            lblImageAddress.Text = "";

        }


        private void LoadUserType()
        {
            ddlUserType.Items.Insert(0, "--User Type--");
            ddlUserType.Items.Insert(1, "Administrator");
            ddlUserType.Items.Insert(2, "General Manager");
            ddlUserType.Items.Insert(3, "Operation User");
            ddlUserType.Items.Insert(4, "Accounts User");
            ddlUserType.SelectedIndex = 0;

        }
        private void ComboLoad()
        {
            cmbSearch.Items.Insert(0, "Search By");
            cmbSearch.Items.Insert(1, "All");
            cmbSearch.Items.Insert(2, "First Name");
            cmbSearch.Items.Insert(3, "Last Name");
            cmbSearch.SelectedIndex = 0;

        }

        private void PrepareGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Goldenrod;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 5;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].HeaderText = "SL#";

            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[1].HeaderText = "First Name";

            //dataGridView1.Columns[2].Width = 210;
            dataGridView1.Columns[2].HeaderText = "Last Name";

            dataGridView1.Columns[3].HeaderText = "Email Address";

            dataGridView1.Columns[4].HeaderText = "Mobile";



        }

        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            listUser = objBLL.Getall();

            if (listUser.Count > 0)
            {
                int index = 1;
                foreach (var item in listUser)
                {
                    dataGridView1.Rows.Add(index, item.FirstName, item.LastName, item.Email, item.PhoneNo);
                    index = index + 1;
                }

            }

            dataGridView1.ClearSelection();
        }

        private void LoadFilterDataToGrid(List<UserInfo> listUser)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (listUser.Count > 0)
            {
                int index = 1;
                foreach (var item in listUser)
                {
                    dataGridView1.Rows.Add(index, item.FirstName, item.LastName, item.Email, item.PhoneNo);
                    index = index + 1;
                }

            }

            dataGridView1.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = Validation();
            if (flag == true)
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
                          "Confirm User deletion",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (objUser.UserId != 0)
                {
                    var status = objBLL.Delete(objUser.UserId);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {

            string slect = cmbSearch.Text.Trim();
            string value = txtSearch.Text.ToString();

            if (slect == "Search By")
            {
                MessageBox.Show("Please select a search item !!");
                return;
            }
            else if (slect != "All" && value == "")
            {
                MessageBox.Show(" Search text can't be empty");
                return;
            }
            switch (slect)
            {
                case "First Name":
                    {
                        var listItems = listUser.Where(c => c.FirstName.Equals(value)).ToList();
                        LoadFilterDataToGrid(listItems);
                        break;
                    }
                case "Last Name":
                    {

                        var listItems = listUser.Where(item => item.LastName.Contains(value)).ToList();
                        LoadFilterDataToGrid(listItems);
                        break;
                    }                
                default:
                    {
                        LoadFilterDataToGrid(listUser);
                        break;
                    }
            }

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var index = Convert.ToInt32(selectedRow.Index);
            objUser = listUser.ElementAt(index);
            
            txtFirstName.Text = Convert.ToString(objUser.FirstName);
            txtlastName.Text = Convert.ToString(objUser.LastName);
            ddlUserType.SelectedIndex = Convert.ToInt32(objUser.UserType);
            txtAddress.Text = Convert.ToString(objUser.Address);
            txtMobile.Text = Convert.ToString(objUser.PhoneNo);            
            txtEmail.Text = Convert.ToString(objUser.Email);
            txtLoginName.Text = Convert.ToString(objUser.LoginId);
            txtPassword.Text = Convert.ToString(objUser.Password);
            dateEntry.Text = Convert.ToString(objUser.EntryDate);
            if (objUser.Image != null)
            {
                MemoryStream ms = new MemoryStream(objUser.Image);
                pictureBox1.Image = Image.FromStream(ms);
                pictureBox1.Visible = true;
            }
            else
            {
                pictureBox1.Image = null;
                pictureBox1.Visible = false;
            }
            btnSave.Text = "Update";
            btndelete.Enabled = true;
           
        }       

        private bool Validation()
        {
            var errMessage = "";

            if (txtFirstName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter first name !!\n";
            }
            if (txtlastName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter last name !!\n";
            }
            if (txtLoginName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter login name !!\n";
            }
            if (txtPassword.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter password !!\n";
            }
            if (ddlUserType.SelectedIndex == 0)
            {
                errMessage = errMessage + "* Please select user type !!\n";
            }
            if (errMessage != "")
            {
                MessageBox.Show(errMessage, "Information required !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show(errMessage, "Error");
                return false;
            }
            else
            {
                return true;
            }

        }

        private void FillingData()
        {
            try
            {
                objUser.FirstName = txtFirstName.Text.Trim();
                objUser.LastName = txtlastName.Text.Trim();
                objUser.UserType = ddlUserType.SelectedIndex;
                objUser.Address = txtAddress.Text.Trim();
                objUser.Email = txtEmail.Text.Trim();
                objUser.PhoneNo = txtMobile.Text.Trim();
                objUser.LoginId = txtLoginName.Text.Trim();
                objUser.Password = txtPassword.Text.Trim();
                objUser.EntryDate = DateTime.Now;
                //OpenFileDialog OpenFD = new OpenFileDialog();
                //OpenFD.Title = "Select Files";
                //OpenFD.Filter = "Jpg|*.jpg|Jpge|*.jpge|Png|*.png|Gif|*.gif";
                //OpenFD.FileName = null;
                //string fileName;
                //if (OpenFD.ShowDialog() != DialogResult.Cancel)
                //{

                //    fileName = OpenFD.FileName;
                //    try
                //    {
                //        // show it to picturebox
                //        pictureBox1.Load(fileName);
                //        pictureBox1.Visible = true;
                //        lblImageAddress.Text = OpenFD.FileName;
                //        // Here get_image is a function and Big is the byte[] type
                //        objUser.Image = get_image(fileName);
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show("Error" + ex.Message.ToString());
                //    }
                //}


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void SaveData()
        {
            if (btnSave.Text == "Save")
            {
               var status = objBLL.Insert(objUser);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (btnSave.Text == "Update")
            {
                var status = objBLL.Update(objUser);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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

        
        private void chkUpdateImage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUpdateImage.Checked == true)
            {
                OpenFileDialog OpenFD = new OpenFileDialog();
                OpenFD.Title = "Select Files";
                OpenFD.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
                OpenFD.FileName = null;
                string fileName;
                if (OpenFD.ShowDialog() != DialogResult.Cancel)
                {

                    fileName = OpenFD.FileName;
                    try
                    {
                        // show it to picturebox
                        pictureBox1.Load(fileName);
                        pictureBox1.Visible = true;
                        lblImageAddress.Text = OpenFD.FileName;
                        // Here get_image is a function and Big is the byte[] type
                        objUser.Image = get_image(fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message.ToString());
                    }
                }

            }

        }

        private void ClearForm()
        {
            txtlastName.Text = "";
            txtFirstName.Text = "";
            ddlUserType.SelectedIndex = 0;
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtLoginName.Text = "";
            txtPassword.Text = "";
            dateEntry.Value = DateTime.Now;
            btnSave.Text = "Save";
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            chkUpdateImage.Checked = false;
            pictureBox1.Image = null;
            lblImageAddress.Text = "";
            pictureBox1.Visible = false;
            btndelete.Enabled = false;
            objUser = new UserInfo();
            dataGridView1.ClearSelection();
            txtFirstName.Focus();

        }


    }
}
