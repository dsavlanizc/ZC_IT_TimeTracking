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
    
    public partial class Goal_Master
    {
        public Goal_Master()
        {
            this.Goal_Rules = new HashSet<Goal_Rules>();
            this.Resource_Goal = new HashSet<Resource_Goal>();
        }
    
        public int Goal_MasterID { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDescription { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double MeasurementValue { get; set; }
        public Nullable<System.DateTime> Creation_Date { get; set; }
        public bool IsHigherValueGood { get; set; }
        public int QuarterId { get; set; }
    
        public virtual Goal_Quarter Goal_Quarter { get; set; }
        public virtual ICollection<Goal_Rules> Goal_Rules { get; set; }
        public virtual ICollection<Resource_Goal> Resource_Goal { get; set; }
    }
}
