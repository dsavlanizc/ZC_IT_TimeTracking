using System.Collections.Generic;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Interfaces
{
    public interface IGoalRepository : IRepositoryBase<GoalQuarters>
    {
        List<GoalQuarters> GetQuarterFromYear(int year);
    }
}
