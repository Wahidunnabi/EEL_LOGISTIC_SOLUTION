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
    
    public partial class Agent
    {
        public Agent()
        {
            this.Customers = new HashSet<Customer>();
        }
    
        public int AgentId { get; set; }
        public string AgentCode { get; set; }
        public string AgentName { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
    
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
