using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities.Model;

namespace ZC_IT_TimeTracking.Services.Interfaces
{
    public interface IResourceGoalService : IServiceBase
    {
        List<ResourceGoalModel> GetAllGoalsOfResource(int resourceId);
        bool DeleteResourceGoal(int resGoalId);
        bool IsResourceGoalExist(int resGoalId);
        bool EditAssignedGoal(int Weight, int ResourceId, int GoalID);
        bool IsResourceGoalExistByResourceId(int resourceId);
    }
}
