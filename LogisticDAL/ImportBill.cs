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
    
    public partial class ImportBill
    {
        public ImportBill()
        {
            this.ImportBillDetails = new HashSet<ImportBillDetail>();
        }
    
        public int ID { get; set; }
        public int IGMImportId { get; set; }
        public Nullable<int> ImporterId { get; set; }
        public string ImpInvoiceNumber { get; set; }
        public string BLNo { get; set; }
        public Nullable<int> CandFAgentId { get; set; }
        public Nullable<int> FreeDays { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<bool> IsPercentage { get; set; }
        public Nullable<decimal> VatPercent { get; set; }
        public Nullable<decimal> VatAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public System.DateTime BillPrepareDate { get; set; }
        public Nullable<System.DateTime> BillCalculateDate { get; set; }
        public int SavedById { get; set; }
        public Nullable<System.DateTime> Entrydate { get; set; }
        public Nullable<int> EditById { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public Nullable<int> ApprovedUserId { get; set; }
        public Nullable<System.DateTime> Approveddate { get; set; }
        public Nullable<int> Documentstatus { get; set; }
        public Nullable<bool> ApprovedStatus { get; set; }
    
        public virtual ICollection<ImportBillDetail> ImportBillDetails { get; set; }
    }
}
