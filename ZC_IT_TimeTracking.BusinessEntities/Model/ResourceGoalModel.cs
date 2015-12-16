using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.BusinessEntities.Model
{
    public class ResourceGoalModel
    {
        public int Resource_GoalID { get; set; }
        public DateTime GoalAssignDate { get; set; }
        public int ResourceID { get; set; }
        public int Goal_MasterID { get; set; }
        public string GoalTitle { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int GoalQuarter { get; set; }
        public int QuarterYear { get; set; }
        public int QuarterID { get; set; }
        public float Resource_Performance { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double MeasurementValue { get; set; }
        public string GoalDescription { get; set; }
    }
}
