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
        const string _GetAllGoalsOfQuarter = "GetAllGoalsOfQuarter";
        const string _TotalGoalCount = "GetTotalGoalCount";
        const string _SearchGoalByTitleCount = "SearchGoalByTitleCount";

        public List<GoalMaster> SearchGoalByTitleDB(string title, int skip, int recordPerPage)
        {
            GoalMaster gm = new GoalMaster();
            gm.GoalTitle = title;
            gm.startFrom = skip;
            gm.NoOfRecords = recordPerPage;
            return this.GetEntityCollection<GoalMaster>(gm, _SearchGoalByTitle);
        }
        public GoalMaster SearchGoalByTitleCount(string title)
        {
            GoalMaster gm = new GoalMaster();
            gm.GoalTitle = title;
            return this.GetEntity<GoalMaster>(gm, _SearchGoalByTitleCount);
        }
        public GoalMaster TotalRecordsOfGoal()
        {
            GoalMaster gm = new GoalMaster();
            return this.GetEntity<GoalMaster>(gm, _TotalGoalCount);
        }

        public List<GoalMaster> GetSpecificRecordsOfGoalDB(int StartFrom, int PageSize)
        {
            GoalMaster gm = new GoalMaster();
            gm.startFrom = StartFrom;
            gm.NoOfRecords = PageSize;
            return this.GetEntityCollection<GoalMaster>(gm, _GetSpecificRecordsOfGoalPagination);
        }

        public GoalMaster GetGoalDetailsByIDDB(int goalID)
        {
            GoalMaster gm = new GoalMaster();
            gm.Goal_MasterID = goalID;
            return this.GetEntity<GoalMaster>(gm, _GetGoalDetailsByGoalID);
        }

        public int InsertGoalMasterDB(GoalMaster gm)
        {
            gm.Creation_Date = DateTime.Today;
            var result = this.GetEntity<GoalMaster>(gm, _InsertGoalMaster);
            return result != null ? result.Goal_MasterID : -1;
        }

        public bool UpdateGoalMasterDB(GoalMaster gm)
        {                    
            gm.Creation_Date = DateTime.Today;            
            return this.InsertOrUpdate<GoalMaster>(gm, _UpdateGoalMaster);
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

        public List<GoalMaster> GoalListByQuarterDB(int goalQuarter, int quarterYear)
        {
            GoalMaster gm = new GoalMaster();
            gm.GoalQuarter = goalQuarter;
            gm.QuarterYear = quarterYear;
            return this.GetEntityCollection<GoalMaster>(gm, _GetAllGoalsOfQuarter);
        }
    }
}
