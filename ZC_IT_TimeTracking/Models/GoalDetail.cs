using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.Models
{
    public partial class GoalDetail
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double MeasurementValue { get; set; }
        public bool IsHigher { get; set; }
        //public int Quarter { get; set; }
        //public int Year { get; set; }
        //public List<GoalRule> GoalRules { get; set; }
    }
}