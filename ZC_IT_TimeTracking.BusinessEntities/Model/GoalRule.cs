using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.BusinessEntities
{
    public class GoalRule
    {
        public GoalRule()
        {
            this.GoalMaster = new GoalMaster();
        }
        public Int32 Goal_RuleID { get; set; }
        public double Performance_RangeFrom { get; set; }
        public double Performance_RangeTo { get; set; }
        public double Rating { get; set; }
        public Int32 GoalID { get; set; }

        public GoalMaster GoalMaster { get; set; }
    }
}