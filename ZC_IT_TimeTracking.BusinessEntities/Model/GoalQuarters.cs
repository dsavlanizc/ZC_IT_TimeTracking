using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.BusinessEntities
{
    public class GoalQuarters
    {
        public Int32 QuarterID { get; set; }
        public Int32 GoalQuarter { get; set; }
        public Int32 QuarterYear { get; set; }
        public DateTime GoalCreateFrom { get; set; }
        public DateTime GoalCreateTo { get; set; }
    }
}