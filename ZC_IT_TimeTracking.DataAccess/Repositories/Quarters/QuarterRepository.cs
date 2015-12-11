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

        //public GoalQuarters GetQuarterByIdDB(int id)
        //{
        //    GoalQuarters quarters = new GoalQuarters();
        //    return new GoalQuarters();//this.GetEntityCollection<GoalQuarters>(quarters, _getQuarterById);
        //}
    }
}
