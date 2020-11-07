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
    
    public partial class CSDBillDetail
    {
        public int Id { get; set; }
        public int CustId { get; set; }
        public string CustomerCode { get; set; }
        public string ContainerNo { get; set; }
        public string InVehicleNO { get; set; }
        public string OutVehicleNo { get; set; }
        public string ContCSDRefNo { get; set; }
        public int SizeId { get; set; }
        public string Size { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string BookingNoDump { get; set; }
        public System.DateTime GateInDate { get; set; }
        public Nullable<System.DateTime> GateOutDate { get; set; }
        public Nullable<System.DateTime> BillFrom { get; set; }
        public Nullable<System.DateTime> BillTo { get; set; }
        public Nullable<int> HaulierIn { get; set; }
        public Nullable<int> HaulierOut { get; set; }
        public Nullable<int> Days { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public string ServiceName { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string LoadedStatus { get; set; }
        public string BroughtFrom { get; set; }
        public string OutTo { get; set; }
        public string SummaryRefNo { get; set; }
    }
}
