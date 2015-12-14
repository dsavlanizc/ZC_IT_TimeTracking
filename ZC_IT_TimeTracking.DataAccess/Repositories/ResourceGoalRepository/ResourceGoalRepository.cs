using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;
using ZC_IT_TimeTracking.DataAccess.Extensions;
using ZC_IT_TimeTracking.DataAccess.Interfaces.ResourceGoalRepo;
using System.Collections.Generic;
using ZC_IT_TimeTracking.BusinessEntities.Model;

namespace ZC_IT_TimeTracking.DataAccess.Repositories.ResourceGoalRepository
{
    public class ResourceGoalRepository : RepositoryBase<ResourceGoalModel>, IResourceGoalRepository
    {
        const string _GetAllGoalsOfResource = "GetAllGoalsOfResource";
        const string _DeleteResourceGoal = "DeleteResourceGoal";
        const string _GetResourceGoalById = "GetResourceGoalById";
        const string _UpdateResourceGoal = "UpdateResourceGoal";

        public List<ResourceGoalModel> GetAllGoalsOfResourceDB(int resourceId)
        {
            ResourceGoalModel rg = new ResourceGoalModel();
            rg.ResourceID = resourceId;
            return this.GetEntityCollection<ResourceGoalModel>(rg, _GetAllGoalsOfResource);
        }

        public bool DeleteResourceGoalDB(int resGoalId)
        {
            ResourceGoalModel rg = new ResourceGoalModel();
            rg.Resource_GoalID = resGoalId;
            var result = this.Delete<ResourceGoalModel>(rg, _DeleteResourceGoal);
            return result;
        }

        public ResourceGoalModel GetResourceGoalByIdDB(int resGoalId)
        {
            ResourceGoalModel rg = new ResourceGoalModel();
            rg.Resource_GoalID = resGoalId;
            return this.GetEntity<ResourceGoalModel>(rg, _GetResourceGoalById);
        }

        public bool UpdateResourceGoalDB(int ResourceId, int GoalID, int Weight)
        {
            ResourceGoalModel rg = new ResourceGoalModel();
            rg.ResourceID = ResourceId;
            rg.Goal_MasterID = GoalID;
            rg.Weight = Weight;
            return this.InsertOrUpdate<ResourceGoalModel>(rg, _UpdateResourceGoal);
        }
    }
}
