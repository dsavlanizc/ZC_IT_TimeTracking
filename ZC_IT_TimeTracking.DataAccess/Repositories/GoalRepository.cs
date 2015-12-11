using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.DataAccess.Extensions;
using ZC_IT_TimeTracking.DataAccess.Interfaces;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Repositories
{
    public class GoalRepository : RepositoryBase<GoalQuarters>, IGoalRepository
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
