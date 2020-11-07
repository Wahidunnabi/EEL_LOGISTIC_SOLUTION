using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace LOGISTIC.UI
{
    public partial class frmLogIn : Form
    {
      
        AuthenticationBLL objBLL = new AuthenticationBLL();
        public frmLogIn()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool flag = Validation();
            if (flag == true)
            {
                var loginName = txtLoginName.Text.Trim();
                var password = txtPassword.Text.Trim();
                var user = objBLL.GetCustByUserNamePass(loginName, password);
                if (user != null)
                {
                    var listPermission = objBLL.GetAllUserPermissionByRoleId(user.UserType);
                    this.Hide();
                    frmMain f = new frmMain(user, listPermission);
                    f.Show();

                }
                else
                {
                    MessageBox.Show("Either username or Password is wrong !!", "Credential Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtLoginName.Text = "";
                    txtPassword.Text = "";
                    txtLoginName.Focus();
                }

            }
            else
            {

            }
                
            
        }

        private bool Validation()
        {
            var errMessage = "";

            if (txtLoginName.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter login name !!\n";
                txtLoginName.Focus();

            }
            if (txtPassword.Text.Trim() == string.Empty)
            {
                errMessage = errMessage + "* Please enter password !!\n";

                if (txtLoginName.Text.Trim() == string.Empty)
                {
                    txtLoginName.Focus(); 
                }
                else
                {
                    txtPassword.Focus();
                }
            }          
            if (errMessage != "")
            {
                MessageBox.Show(errMessage, "Input required !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);               
                return false;
            }
            else
            {
                return true;
            }

        }

       
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtLoginName.Text.Length >= 4)
            {
                var user = objBLL.GetUserByUserName(txtLoginName.Text.Trim());
                if (user != null && user.Image != null)
                {
                    MemoryStream ms = new MemoryStream(user.Image);
                    imgUser.Image = Image.FromStream(ms);

                }
                //else
                //{
                //    imgUser.Image = null;
                //}

            }
            //Login();

        }

        private void frmLogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {

        }

       

        private void Login()
        {
            bool flag = Validation();
            if (flag == true)
            {
                var loginName = txtLoginName.Text.Trim();
                var password = txtPassword.Text.Trim();
                var user = objBLL.GetCustByUserNamePass(loginName, password);
                if (user != null)
                {
                    var listPermission = objBLL.GetAllUserPermissionByRoleId(user.UserType);
                    this.Hide();
                    frmMain f = new frmMain(user, listPermission);
                    f.Show();

                }
                else
                {
                    MessageBox.Show("Either username or Password is wrong !!", "Credential Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtLoginName.Text = "";
                    txtPassword.Text = "";
                    txtLoginName.Focus();
                }

            }
            else
            {

            }

        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {



            if (e.KeyCode == Keys.Enter)
            {
                Login();
                //MessageBox.Show("Enter Key Pressed ");
            }
            
        }
    }
}
