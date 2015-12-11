using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.BusinessEntities.Model
{
    public class GoalMasters
    {
        public GoalMasters()
        {
            this.GoalRule = new HashSet<GoalRule>();
            this.ResourceGoal = new HashSet<ResourceGoal>();
        }

        public Int32 Goal_MasterID { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDescription { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double MeasurementValue { get; set; }
        public DateTime Creation_Date { get; set; }
        public bool IsHigherValueGood { get; set; }
        public Int32 QuarterId { get; set; }

        public GoalQuarters GoalQuarter { get; set; }

        public ICollection<GoalRule> GoalRule { get; set; }

        public ICollection<ResourceGoal> ResourceGoal { get; set; }
    }
}
