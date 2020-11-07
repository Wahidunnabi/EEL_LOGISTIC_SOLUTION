using LOGISTIC.UserDefinedModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LOGISTIC
{
    public class AccountsVoucherDal
    {

        string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        List<AccountsVoucherEntity> listid = new List<AccountsVoucherEntity>();
        public List<AccountsVoucherEntity> AccountsVoucherRdlc()
        {
            using (SqlConnection con = new SqlConnection(constring))
            {

                con.Open();

                using (SqlCommand cmd = new SqlCommand("sp_rdlc_AccountsVoucher", con))
                                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                AccountsVoucherEntity voucher = new AccountsVoucherEntity();
                                voucher.VoucherDate = Convert.ToDateTime(reader["VoucherDate"]);
                                voucher.VoucherNumber = reader["VoucherNumber"].ToString();
                                voucher.Description = reader["Description"].ToString();
                                voucher.COAID = Convert.ToInt32(reader["COAID"].ToString());
                                voucher.AccountName = Convert.ToString(reader["AccountName"]);
                                voucher.TranMode = Convert.ToString(reader["TranMode"]);
                                voucher.DrAmount = Convert.ToDecimal(reader["DrAmount"]);
                                voucher.CrAmount = Convert.ToDecimal(reader["CrAmount"]);

                                listid.Add(voucher);
                            }
                        }
                    } // reader closed and disposed up here

                } // command disposed here

            } //connecti
            return listid;
        }
    }
  

 
}
