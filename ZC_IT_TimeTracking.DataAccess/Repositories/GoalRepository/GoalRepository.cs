using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Goal;

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
    }
}
