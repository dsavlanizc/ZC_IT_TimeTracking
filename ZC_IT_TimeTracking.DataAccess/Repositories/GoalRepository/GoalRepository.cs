﻿using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Goal;
using System.Collections.Generic;
using ZC_IT_TimeTracking.DataAccess.Extensions;
using System.Data.Objects;
using System;

namespace ZC_IT_TimeTracking.DataAccess.Repositories.Goal
{
    public class GoalRepository : RepositoryBase<GoalMaster>, IGoalRepository
    {
        const string _GetGoalDetailsByGoalID = "GetGoalDetails";
        const string _GetSpecificRecordsOfGoalPagination = "GetSpecificRecordsOfGoal";
        const string _SearchGoalByTitle = "SearchGoalByTitle";
        const string _InsertGoalMaster = "InsertGoalMaster";
        const string _UpdateGoalMaster = "UpdateGoalMaster";
        const string _DeleteGoalMasterByGoalID = "DeleteGoalMaster";

        public List<GoalMaster> SearchGoalByTitleDB(string title, int skip, int recordPerPage,int count)
        {
            GoalMaster gm = new GoalMaster();
            gm.GoalTitle = title;
            gm.skip = skip;
            gm.recordPerPage = recordPerPage;
            gm.count = count;
            return this.GetEntityCollection<GoalMaster>(gm,_SearchGoalByTitle);
        }

        public List<GoalMaster> GetSpecificRecordsOfGoalDB(int StartFrom, int PageSize, int count)
        {
            GoalMaster gm = new GoalMaster();
            gm.skip = StartFrom;
            gm.recordPerPage = PageSize;
            gm.count = count;
            return this.GetEntityCollection<GoalMaster>(gm,_GetSpecificRecordsOfGoalPagination);
        }

        public GoalMaster GetGoalDetailsByIDDB(int goalID)
        {
            GoalMaster gm = new GoalMaster();
            gm.Goal_MasterID = goalID;
            return this.GetEntity<GoalMaster>(gm, _GetGoalDetailsByGoalID);
        }

        public int InsertGoalMasterDB(GoalMaster gm)
        {
            GoalMaster goal = new GoalMaster();
            goal.GoalTitle = gm.GoalTitle;
            goal.GoalDescription = gm.GoalDescription;
            goal.UnitOfMeasurement = gm.UnitOfMeasurement;
            goal.MeasurementValue = gm.MeasurementValue;
            goal.IsHigherValueGood = gm.IsHigherValueGood;
            goal.Creation_Date = DateTime.Today;
            goal.QuarterId = gm.QuarterId;
            var result = this.GetEntity<GoalMaster>(goal, _InsertGoalMaster);
            return result != null ? result.Goal_MasterID : -1;
        }

        public bool UpdateGoalMasterDB(GoalMaster gm)
        {
            GoalMaster goal = new GoalMaster();
            goal.GoalTitle = gm.GoalTitle;
            goal.GoalDescription = gm.GoalDescription;
            goal.UnitOfMeasurement = gm.UnitOfMeasurement;
            goal.MeasurementValue = gm.MeasurementValue;
            goal.IsHigherValueGood = gm.IsHigherValueGood;
            goal.Creation_Date = DateTime.Today;
            goal.Quarters.QuarterID = gm.Quarters.QuarterID;
            return this.InsertOrUpdate<GoalMaster>(goal, _UpdateGoalMaster);
        }
    }
}
