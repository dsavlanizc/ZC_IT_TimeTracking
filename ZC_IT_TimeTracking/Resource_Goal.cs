//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZC_IT_TimeTracking
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resource_Goal
    {
        public Resource_Goal()
        {
            this.Resource_Goal_Performance = new HashSet<Resource_Goal_Performance>();
        }
    
        public int Resource_GoalID { get; set; }
        public int ResourceID { get; set; }
        public int Goal_MasterID { get; set; }
        public int Weight { get; set; }
        public Nullable<System.DateTime> GoalAssignDate { get; set; }
    
        public virtual Goal_Master Goal_Master { get; set; }
        public virtual Resource Resource { get; set; }
        public virtual ICollection<Resource_Goal_Performance> Resource_Goal_Performance { get; set; }
    }
}
