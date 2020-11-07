using LOGISTIC.BLL;
using LOGISTIC.UserDefinedModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LOGISTIC.UI.Report.Accounts
{
    public partial class AccountsVoucherRDLC : Form
    {
        public AccountsVoucherRDLC()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AccountsVoucherBLL objBll = new AccountsVoucherBLL();
            List<AccountsVoucherEntity> list = new List<AccountsVoucherEntity>();
            list = objBll.GetAccountsVoucher();

            this.reportViewer1.RefreshReport();

            AccountsVoucherEntityBindingSource.DataSource = list;
        }

        private void AccountsVoucherRDLC_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
