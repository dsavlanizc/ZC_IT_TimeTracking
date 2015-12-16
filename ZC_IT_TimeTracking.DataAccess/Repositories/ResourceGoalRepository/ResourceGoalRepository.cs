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
        const string _AssignGoalToResource = "AssignGoalToResource";
        const string _GetResourceGoalDetails = "GetResourceGoalDetails";
        const string _GetAllResourceGoalByResId = "GetAllResourceGoalByResId";
        const string _InsertPerformance = "calculateResourceGoalRating";
        const string _GetAllOfResourceGoalPerformance = "GetAllOfResourceGoalPerformance";

        public List<ResourceGoalModel> GetAllGoalsOfResourceDB(int resourceId)
        {
            ResourceGoalModel rg = new ResourceGoalModel();
            rg.ResourceID = resourceId;
            return this.GetEntityCollection<ResourceGoalModel>(rg, _GetAllGoalsOfResource);
        }

        public int AssignGoalToResourceDB(ResourceGoalModel rgm)
        {
            var result = this.GetEntity<ResourceGoalModel>(rgm, _AssignGoalToResource);
            return result.Resource_GoalID;
        }

        public ResourceGoalModel GetResourceGoalDetailsDB(int resourceId, int goalId)
        {
            ResourceGoalModel rgm = new ResourceGoalModel();
            rgm.ResourceID = resourceId;
            rgm.Goal_MasterID = goalId;
            return this.GetEntity<ResourceGoalModel>(rgm, _GetResourceGoalDetails);
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

        public bool UpdateResourceGoalDB(int ResourceGoalId, int GoalID, int Weight)
        {
            ResourceGoalModel rg = new ResourceGoalModel();
            rg.Resource_GoalID = ResourceGoalId;
            rg.Goal_MasterID = GoalID;
            rg.Weight = Weight;
            return this.InsertOrUpdate<ResourceGoalModel>(rg, _UpdateResourceGoal);
        }

        public List<ResourceGoalModel> GetAllResourceGoalByResIdDB(int resourceID,int quarter,int year)
        {
            ResourceGoalModel rgm = new ResourceGoalModel();
            rgm.ResourceID = resourceID;
            rgm.GoalQuarter = quarter;
            rgm.QuarterYear = year;
            return this.GetEntityCollection<ResourceGoalModel>(rgm, _GetAllResourceGoalByResId);
        }

        public bool InsertPerformanceDB(int goalID, int resID, float resPerformance)
        {
            ResourceGoalModel rgm = new ResourceGoalModel();
            rgm.Goal_MasterID = goalID;
            rgm.ResourceID = resID;
            rgm.Resource_Performance = resPerformance;
            return this.InsertOrUpdate<ResourceGoalModel>(rgm, _InsertPerformance);
        }

        public ResourceGoalModel GetAllOfResourceGoalPerformanceDB(int resGoalId)
        {
            ResourceGoalModel rgm = new ResourceGoalModel();
            rgm.Resource_GoalID = resGoalId;
            return this.GetEntity<ResourceGoalModel>(rgm, _GetAllOfResourceGoalPerformance);
        }
    }
}
