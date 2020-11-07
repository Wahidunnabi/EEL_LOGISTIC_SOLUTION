using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGISTIC.UserDefinedModel
{
    public class AccountsVoucherEntity

    {
        public DateTime VoucherDate { get; set; }
        public string Description { get; set; }
        public string VoucherNumber { get; set; }
        public int COAID { get; set; }
        public string AccountName { get; set; }
        public string TranMode { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        //public AccountsVoucher(DateTime VoucherDate, string Description, int COAID, string Account,string TranMode, decimal DrAmount, decimal CrAmount)
        //   {
        //    this.VoucherDate = VoucherDate;
        //    this.Description = Description;
        //    this.COAID = COAID;
        //    this.Account = Account;
        //    this.TranMode = TranMode;
        //    this.DrAmount = DrAmount;
        //    this.CrAmount = CrAmount;

        //}

    }
}
