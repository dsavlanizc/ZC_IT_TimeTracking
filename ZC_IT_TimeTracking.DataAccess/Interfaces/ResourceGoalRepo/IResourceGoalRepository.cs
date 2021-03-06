﻿using System.Collections.Generic;
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
        bool UpdateResourceGoalDB(int ResourceGoalId, int GoalID, int Weight);
        int AssignGoalToResourceDB(ResourceGoalModel rgm);
        ResourceGoalModel GetResourceGoalDetailsDB(int resourceId, int goalId);
        List<ResourceGoalModel> GetAllResourceGoalByResIdDB(int resourceID, int quarter, int year);
        bool InsertPerformanceDB(int goalID, int resID, float resPerformance);
        ResourceGoalModel GetAllOfResourceGoalPerformanceDB(int resGoalId);
        List<ResourceGoalModel> GetQuaterlyPerformanceByResIDDB(int resID, int quarterID);
    }
}
