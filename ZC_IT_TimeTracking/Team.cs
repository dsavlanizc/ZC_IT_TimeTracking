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
 	public partial class Team
    {
        public Team()
        {
            this.Managers = new HashSet<Manager>();
            this.Resources = new HashSet<Resource>();
            this.TeamLeads = new HashSet<TeamLead>();
        }
    
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int DepartmentID { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual ICollection<Manager> Managers { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<TeamLead> TeamLeads { get; set; }
    }
}
