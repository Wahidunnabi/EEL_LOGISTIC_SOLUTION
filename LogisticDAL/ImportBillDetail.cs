//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LOGISTIC
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImportBillDetail
    {
        public long ID { get; set; }
        public int BillId { get; set; }
        public int ServiceId { get; set; }
        public Nullable<System.DateTime> ImportIndate { get; set; }
        public Nullable<System.DateTime> ImportOutdate { get; set; }
        public string BillUnit { get; set; }
        public Nullable<int> Size { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> Days { get; set; }
        public Nullable<decimal> RateInTk { get; set; }
        public Nullable<decimal> RateInDlr { get; set; }
        public decimal Total { get; set; }
    
        public virtual ImportBill ImportBill { get; set; }
    }
}
