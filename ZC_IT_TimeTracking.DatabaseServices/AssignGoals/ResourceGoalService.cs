using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
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
                if (IsResourceGoalExist(resGoalId) != null)
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

        public ResourceGoalModel IsResourceGoalExist(int resGoalId)
        {
            return _repository.GetResourceGoalByIdDB(resGoalId);
        }

        public bool IsResourceGoalExistByResourceId(int resourceId)
        {
            var result = _repository.GetAllGoalsOfResourceDB(resourceId);
            if (result != null)
                return true;
            else
                return false;
        }

        public bool EditAssignedGoal(int Weight, int ResourceGoalId, int GoalID)
        {
            try
            {
                if (IsResourceGoalExist(ResourceGoalId) != null)
                {
                    return _repository.UpdateResourceGoalDB(ResourceGoalId, GoalID, Weight);
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

        public bool AssignGoal(AssignGoal AssignData)
        {
            try
            {
                int count = 0;
                bool MissingRes = false;
                foreach (int id in AssignData.ResourceID)
                {
                    var v = _repository.GetResourceGoalDetailsDB(id, AssignData.Goal_MasterID);
                    var ResGoal = GetAllGoalsOfResource(id);
                    int TotalWeight = 0;
                    if (ResGoal != null && ResGoal.Count > 0)
                    {
                        TotalWeight = ResGoal.Sum(s => s.Weight) + AssignData.Weight;
                    }
                    if (v == null)
                    {
                        if (TotalWeight >= 100)
                        {
                            MissingRes = true;
                            count++;
                        }
                        else
                        {
                            var resGoalModel = AutoMapper.Mapper.DynamicMap<AssignGoal, ResourceGoalModel>(AssignData);
                            resGoalModel.ResourceID = id;
                            resGoalModel.GoalAssignDate = DateTime.Now;
                            var AssignGoal = _repository.AssignGoalToResourceDB(resGoalModel);
                            if (AssignGoal != null)
                                count++;
                        }
                    }
                }
                if (count == AssignData.ResourceID.Count())
                {
                    if (MissingRes)
                    {
                        this.ClearValidationErrors();
                        this.ValidationErrors.Add("ERR_GRT_TRGT", "Some Resouce weight is greter than 100 !");
                        return false;
                    }
                    return true;
                }
                else
                {
                    this.ClearValidationErrors();
                    this.ValidationErrors.Add("GoalNotAssigned", "Not all Goal were assigned Succesfully!");
                    return false;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ExceptionGoalAssign", "Error occured while Assigning a Goal!");
                return false;
            }
        }
    }
}
