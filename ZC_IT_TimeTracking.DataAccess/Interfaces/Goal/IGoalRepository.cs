﻿using System.Collections.Generic;
using System.Data.Objects;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Interfaces.Goal
{
    public interface IGoalRepository : IRepositoryBase<GoalMaster>
    {
        List<GoalMaster> SearchGoalByTitleDB(string title, int skip, int recordPerPage);
        List<GoalMaster> GetSpecificRecordsOfGoalDB(int StartFrom, int PageSize);
        GoalMaster GetGoalDetailsByIDDB(int goalID);
        int InsertGoalMasterDB(GoalMaster gm);
        bool UpdateGoalMasterDB(GoalMaster gm);
        int DeleteGoalMasterDB(int goalID);
        bool IsGoalExistDB(int goalID);
        List<GoalMaster> GoalListByQuarterDB(int goalQuarter, int quarterYear);
        GoalMaster TotalRecordsOfGoal();
        GoalMaster SearchGoalByTitleCount(string title);
    }
}
