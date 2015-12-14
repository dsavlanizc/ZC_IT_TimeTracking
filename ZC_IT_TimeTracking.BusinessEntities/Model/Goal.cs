using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.BusinessEntities
{
    public class GoalMaster
    {
        public GoalMaster()
        {
            this.GoalRules = new List<GoalRule>();
            this.Quarters = new GoalQuarters();
        }
        public Int32 Goal_MasterID { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDescription { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double MeasurementValue { get; set; }
        public DateTime Creation_Date { get; set; }
        public bool IsHigherValueGood { get; set; }

        public Int32 GoalQuarter { get; set; }
        public Int32 QuarterYear { get; set; }
        
        public GoalQuarters Quarters { get; set; }
        public List<GoalRule> GoalRules { get; set; }

        #region NonTableProperty

        public int startFrom{ get; set; }
        public int NoOfRecords { get; set; }
        public int totalRecords { get; set; }
        #endregion
    }
}