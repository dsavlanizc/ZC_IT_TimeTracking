using System.Collections.Generic;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Quarters;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;
using ZC_IT_TimeTracking.DataAccess.Extensions;

namespace ZC_IT_TimeTracking.DataAccess.Repositories.Quarters
{
    public class QuarterRepository : RepositoryBase<GoalQuarters>, IQuarterRepository
    {
        const string _CheckQuater = "CheckQuater";
        const string _GetQuarterFromYear = "GetQuarterFromYear";

        public List<GoalQuarters> GetQuarterFromYearDB(int year)
        {
            GoalQuarters quarters = new GoalQuarters();
            quarters.QuarterYear = year;
            return this.GetEntityCollection<GoalQuarters>(quarters, _GetQuarterFromYear);
        }
    }
}
