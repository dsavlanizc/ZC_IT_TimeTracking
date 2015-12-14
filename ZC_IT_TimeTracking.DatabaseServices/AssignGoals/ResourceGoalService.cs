using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities.Model;
using ZC_IT_TimeTracking.DataAccess.Interfaces.ResourceGoalRepo;
using ZC_IT_TimeTracking.Services.Interfaces;

namespace ZC_IT_TimeTracking.Services.AssignGoals
{
    public class ResourceGoalService : ServiceBase, IResourceGoalService
    {
        IResourceGoalRepository _repository;
        public ResourceGoalService()
        {
            _repository = ZC_IT_TimeTracking.DataAccess.Factory.RepositoryFactory.GetInstance().GetResourceGoalRepository();
            this.ValidationErrors = _repository.ValidationErrors;
        }
        public List<ResourceGoalModel> GetAllGoalsOfResource(int resourceId)
        {
            return _repository.GetAllGoalsOfResourceDB(resourceId);
        }

        public bool DeleteResourceGoal(int resGoalId)
        {
            try
            {
                if (IsResourceGoalExist(resGoalId))
                {
                    return _repository.DeleteResourceGoalDB(resGoalId);
                }
                else
                    this.ValidationErrors.Add("GoalExistance", "No such goal exist!");
                return false;
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("ExceptionGoalEdit", "Error occured while Deleting a Goal!");
                return false;
            }
        }

        public bool IsResourceGoalExist(int resGoalId)
        {
            var result = _repository.GetResourceGoalByIdDB(resGoalId);
            if (result == null)
                return false;
            else
                return true;
        }

        public bool IsResourceGoalExistByResourceId(int resourceId)
        {
            var result = _repository.GetAllGoalsOfResourceDB(resourceId);
            if (result != null)
                return true;
            else
                return false;
        }

        public bool EditAssignedGoal(int Weight, int ResourceId, int GoalID)
        {
            try
            {
                if (IsResourceGoalExistByResourceId(ResourceId))
                {
                    ResourceGoalModel rg = new ResourceGoalModel();
                    return _repository.UpdateResourceGoalDB(ResourceId, GoalID, Weight);
                }
                this.ValidationErrors.Add("GoalExistance", "No such goal exist!");
                return false;
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("ExceptionGoalEdit", "Error occured while Editing a Goal Details!");
                return false;
            }
        }
    }
}
