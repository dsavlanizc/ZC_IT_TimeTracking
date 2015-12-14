using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZC_IT_TimeTracking.BusinessEntities.Model;

namespace ZC_IT_TimeTracking.BusinessEntities
{
    public class AssignGoal
    {
        public AssignGoal()
        {
            this.GoalMaster = new List<GoalMaster>();
            this.Resource = new Resources();
        }
        public Int32 Resource_GoalID { get; set; }
        public Int32[] ResourceID { get; set; }
        public Int32 Goal_MasterID { get; set; }
        public Int32 Weight { get; set; }
        public DateTime GoalAssignDate { get; set; }

        public List<GoalMaster> GoalMaster { get; set; }
        public Resources Resource { get; set; }   
    }

    public class AssignGoalResources
    {
        public AssignGoalResources()
        {
            this.GoalMaster = new List<GoalMaster>();
            this.Resource = new Resources();
        }
        public Int32 Resource_GoalID { get; set; }
        public Int32 ResourceID { get; set; }
        public Int32 Goal_MasterID { get; set; }
        public Int32 Weight { get; set; }
        public DateTime GoalAssignDate { get; set; }

        public List<GoalMaster> GoalMaster { get; set; }
        public Resources Resource { get; set; }
    }

    public class ResourceGoal
    {
        public int ResourceID { get; set; }
        public int Goal_MasterID { get; set; }
        public int weight { get; set; }
        public int Resource_GoalID { get; set; }
    }
}