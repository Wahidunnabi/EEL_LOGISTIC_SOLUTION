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
    
    public partial class ChartOfServiceCategory
    {
        public ChartOfServiceCategory()
        {
            this.ChartOfServices = new HashSet<ChartOfService>();
        }
    
        public int CateId { get; set; }
        public string CategoryName { get; set; }
    
        public virtual ICollection<ChartOfService> ChartOfServices { get; set; }
    }
}
