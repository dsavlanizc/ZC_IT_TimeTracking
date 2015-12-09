using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.BusinessEntities
{
    public class AssignGoal
    {
        public int[] ResourceID { get; set; }
        public int Goal_MasterID { get; set; }
        public int weight { get; set; }
    }

    public class ResourceGoal
    {
        public int ResourceID { get; set; }
        public int Goal_MasterID { get; set; }
        public int weight { get; set; }
        public int Resource_GoalID { get; set; }
    }
}