using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.BusinessEntities
{
    public class Goal
    {
        public int Goal_MasterID { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDescription { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double MeasurementValue { get; set; }
        public bool IsHigherValueGood { get; set; }
        public int GoalQuarter { get; set; }
        public int QuarterYear { get; set; }
        public List<GoalRule> GoalRules { get; set; }
    }
}