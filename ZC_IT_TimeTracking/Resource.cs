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
    
    [global::System.CodeDom.Compiler.GeneratedCode("EntityFramework","4.0.0.0")] 
 	public partial class Resource
    {
        public Resource()
        {
            this.Managers = new HashSet<Manager>();
            this.Resource_Goal = new HashSet<Resource_Goal>();
            this.Resource_Performance = new HashSet<Resource_Performance>();
            this.TeamLeads = new HashSet<TeamLead>();
        }
    
        public int ResourceID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> TeamID { get; set; }
    
        public virtual ICollection<Manager> Managers { get; set; }
        public virtual ICollection<Resource_Goal> Resource_Goal { get; set; }
        public virtual ICollection<Resource_Performance> Resource_Performance { get; set; }
        public virtual Role Role { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<TeamLead> TeamLeads { get; set; }
    }
}
