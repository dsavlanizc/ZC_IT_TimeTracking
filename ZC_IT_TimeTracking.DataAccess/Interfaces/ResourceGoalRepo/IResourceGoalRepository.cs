using System.Collections.Generic;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.BusinessEntities.Model;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Interfaces.ResourceGoalRepo
{
    public interface IResourceGoalRepository : IRepositoryBase<ResourceGoalModel>
    {
        List<ResourceGoalModel> GetAllGoalsOfResourceDB(int resourceId);
        bool DeleteResourceGoalDB(int resGoalId);
        ResourceGoalModel GetResourceGoalByIdDB(int resGoalId);
        bool UpdateResourceGoalDB(int ResourceId, int GoalID, int Weight);
        int AssignGoalToResourceDB(ResourceGoalModel rgm);
    }
}
