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
 	public partial class TeamLead
    {
        public int TeamLeadID { get; set; }
        public Nullable<int> TeamID { get; set; }
        public int TeamLeadResourceID { get; set; }
    
        public virtual Resource Resource { get; set; }
        public virtual Team Team { get; set; }
    }
}
