using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.Models
{
    public class GoalQuarters
    {
        public int QuarterID { get; set; }
        public int GoalQuarter { get; set; }
        public int QuarterYear { get; set; }
        public DateTime GoalCreateFrom { get; set; }
        public DateTime GoalCreateTo { get; set; }
    }
}