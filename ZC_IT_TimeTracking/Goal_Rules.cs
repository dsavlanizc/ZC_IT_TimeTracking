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
    
    public partial class Goal_Rules
    {
        public int Goal_RuleID { get; set; }
        public double Performance_RangeFrom { get; set; }
        public double Performance_RangeTo { get; set; }
        public double Rating { get; set; }
        public int GoalId { get; set; }
    
        public virtual Goal_Master Goal_Master { get; set; }
    }
}
