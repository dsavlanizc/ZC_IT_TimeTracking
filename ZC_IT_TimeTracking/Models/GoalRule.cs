using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.Models
{
    public class GoalRule
    {
        public int Goal_RuleID { get; set; }
        public int Performance_RangeFrom { get; set; }
        public int Performance_RangeTo { get; set; }
        public int Rating { get; set; }
    }
}