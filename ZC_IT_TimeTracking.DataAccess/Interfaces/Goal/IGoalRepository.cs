using System.Collections.Generic;
using System.Data.Objects;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Interfaces.Goal
{
    public interface IGoalRepository : IRepositoryBase<GoalMaster>
    {
        List<GoalMaster> SearchGoalByTitleDB(string title, int skip, int recordPerPage,int count);
        List<GoalMaster> GetSpecificRecordsOfGoalDB(int StartFrom, int PageSize, int count);
        GoalMaster GetGoalDetailsByIDDB(int goalID);
        bool InsertGoalMasterDB(GoalMaster gm);
        bool UpdateGoalMasterDB(GoalMaster gm);
    }
}
