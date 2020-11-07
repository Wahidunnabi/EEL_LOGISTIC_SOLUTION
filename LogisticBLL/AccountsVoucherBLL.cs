using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;
using LOGISTIC.UserDefinedModel;

namespace LOGISTIC.BLL
{
    public class AccountsVoucherBLL
    {
        AccountsVoucherDal objDal = new AccountsVoucherDal();
        public List<AccountsVoucherEntity> GetAccountsVoucher()
        {
            List<AccountsVoucherEntity> objlist = new List<AccountsVoucherEntity>();
            objlist = objDal.AccountsVoucherRdlc();
            return objlist;
        }
    }
}
