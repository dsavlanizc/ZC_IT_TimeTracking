using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;
using System.Collections.Generic;
using ZC_IT_TimeTracking.DataAccess.Extensions;
using System.Data.Objects;
using System;
using ZC_IT_TimeTracking.DataAccess.Interfaces.AssignGoals;

namespace ZC_IT_TimeTracking.DataAccess.Repositories.AssignGoalRepository
{
    public class AssignGoalRepository : RepositoryBase<AssignGoal>, IAssignGoalRepository
    {
        const string _AssignGoalToResource = "AssignGoalToResource";
        const string _GetResourceGoalDetails = "GetResourceGoalDetails";
        const string _GetAssignedGoalDetails = "GetAssignedGoalDetails";

        public bool AssignGoalDB(AssignGoal assignData)
        {
            AssignGoal ag = new AssignGoal();
            ag.ResourceID = assignData.ResourceID;
            ag.Goal_MasterID = assignData.Goal_MasterID;
            ag.Weight = assignData.Weight;
            ag.GoalAssignDate = DateTime.Now;
            return this.InsertOrUpdate<AssignGoal>(ag, _AssignGoalToResource);
        }

        public List<AssignGoal> GetResourceGoalDetailsDB(int Resourceid, int GoalId)
        {
            AssignGoal ag = new AssignGoal();
            ag.ResourceID = Resourceid;
            ag.Goal_MasterID = GoalId;
            return this.GetEntityCollection<AssignGoal>(ag,_GetResourceGoalDetails);
        }

        public List<AssignGoal> GetAssignedGoalDetailsDB(int AssignGoalId)
        {
            AssignGoal ag = new AssignGoal();
            ag.Resource_GoalID = AssignGoalId;
            return this.GetEntityCollection<AssignGoal>(ag,_GetAssignedGoalDetails);
        }
    }
}
