using ZC_IT_TimeTracking.BusinessEntities;
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
        const string _GetAllGoalsOfQuarter = "GetAllGoalsOfQuarter";

        public List<GoalMaster> SearchGoalByTitleDB(string title, int skip, int recordPerPage, int count)
        {
            GoalMaster gm = new GoalMaster();
            gm.GoalTitle = title;
            gm.startFrom = skip;
            gm.NoOfRecords = recordPerPage;
            gm.totalRecords = count;
            return this.GetEntityCollection<GoalMaster>(gm, _SearchGoalByTitle);
        }

        public List<GoalMaster> GetSpecificRecordsOfGoalDB(int StartFrom, int PageSize, int count)
        {
            GoalMaster gm = new GoalMaster();
            gm.startFrom = StartFrom;
            gm.NoOfRecords = PageSize;
            gm.totalRecords = count;
            return this.GetEntityCollection<GoalMaster>(gm, _GetSpecificRecordsOfGoalPagination);
        }

        public GoalMaster GetGoalDetailsByIDDB(int goalID)
        {
            GoalMaster gm = new GoalMaster();
            gm.Goal_MasterID = goalID;
            return this.GetEntity<GoalMaster>(gm, _GetGoalDetailsByGoalID);
        }

        public bool InsertGoalMasterDB(GoalMaster gm)
        {
            GoalMaster goal = new GoalMaster();
            goal.GoalTitle = gm.GoalTitle;
            goal.GoalDescription = gm.GoalDescription;
            goal.UnitOfMeasurement = gm.UnitOfMeasurement;
            goal.MeasurementValue = gm.MeasurementValue;
            goal.IsHigherValueGood = gm.IsHigherValueGood;
            goal.Creation_Date = DateTime.Today;
            goal.Quarters.QuarterID = gm.Quarters.QuarterID;
            return this.InsertOrUpdate<GoalMaster>(goal, _InsertGoalMaster);
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

        public int DeleteGoalMasterDB(int goalID)
        {
            GoalMaster gm = new GoalMaster();
            gm.Goal_MasterID = goalID;
            return this.Deletes<GoalMaster>(gm, _DeleteGoalMasterByGoalID);
        }

        public bool IsGoalExistDB(int goalID)
        {
            GoalMaster gm = new GoalMaster();
            gm.Goal_MasterID = goalID;
            var dt = this.GetEntity<GoalMaster>(gm, _GetGoalDetailsByGoalID);
            if (dt != null)
                return true;
            else
                return false;
        }

        public List<GoalMaster> GoalListByQuarterDB(int goalQuarter,int quarterYear)
        {
            GoalMaster gm = new GoalMaster();
            gm.GoalQuarter = goalQuarter;
            gm.QuarterYear = quarterYear;
            return this.GetEntityCollection<GoalMaster>(gm, _GetAllGoalsOfQuarter);
        }
    }
}
