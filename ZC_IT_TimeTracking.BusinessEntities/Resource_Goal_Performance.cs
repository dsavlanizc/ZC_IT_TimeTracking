//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZC_IT_TimeTracking.BusinessEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resource_Goal_Performance
    {
        public int Resource_Goal_PerformanceID { get; set; }
        public double Resource_Performance { get; set; }
        public Nullable<double> Resource_Rating { get; set; }
        public int Resource_GoalId { get; set; }
    
        public virtual Resource_Goal Resource_Goal { get; set; }
    }
}
