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
    
    public partial class VoucherMaster
    {
        public VoucherMaster()
        {
            this.VoucherDetails = new HashSet<VoucherDetail>();
        }
    
        public int VoucherMstrId { get; set; }
        public string VoucherTypeId { get; set; }
        public string VoucherNumber { get; set; }
        public string RefNo { get; set; }
        public Nullable<System.DateTime> VoucherDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> IsAutoPosted { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<int> PostedStatus { get; set; }
        public Nullable<int> BranchId { get; set; }
    
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
    }
}
