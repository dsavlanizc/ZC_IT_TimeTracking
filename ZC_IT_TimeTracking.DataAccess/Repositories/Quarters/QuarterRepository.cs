using System.Collections.Generic;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Quarters;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;
using ZC_IT_TimeTracking.DataAccess.Extensions;

namespace ZC_IT_TimeTracking.DataAccess.Repositories.Quarters
{
    public class QuarterRepository : RepositoryBase<GoalQuarters>, IQuarterRepository
    {
        const string _getQuarterFromYear = "GetQuarterFromYear";
        const string _getAllQuarters = "sp_GetAllQuarters";
        const string _getQuarterById = "GetQuarterDetails";
        const string _isQuarterExist = "CheckQuarter";
        const string _createQuarter = "InsertGoalQuarter";

        public List<GoalQuarters> GetQuarterFromYearDB(int year)
        {
            GoalQuarters quarters = new GoalQuarters();
            quarters.QuarterYear = year;
            return this.GetEntityCollection<GoalQuarters>(quarters, _getQuarterFromYear);
        }

        public List<GoalQuarters> GetAllQuartersDB()
        {
            GoalQuarters quarters = new GoalQuarters();
            return this.GetEntityCollection<GoalQuarters>(quarters, _getAllQuarters);
        }

        public GoalQuarters GetQuarterByIdDB(int id)
        {
            GoalQuarters quarters = new GoalQuarters();
            quarters.QuarterID = id;
            return this.GetEntity<GoalQuarters>(quarters, _getQuarterById);
        }

        public bool CheckQuarterDB(int quarter, int year)
        {
            GoalQuarters quarters = new GoalQuarters();
            quarters.GoalQuarter = quarter;
            quarters.QuarterYear = year;

            var result = this.GetEntity<GoalQuarters>(quarters, _isQuarterExist);
            if (result == null)
                return false;
            else
                return true;
        }

        public bool CreateQuarterDB(GoalQuarters QuarterDetail)
        {
            var result = this.InsertOrUpdate<GoalQuarters>(QuarterDetail, _createQuarter);
            return result;
        }
    }
}
