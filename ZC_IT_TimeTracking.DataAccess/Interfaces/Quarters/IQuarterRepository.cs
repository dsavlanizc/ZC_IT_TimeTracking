using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Interfaces.Quarters
{
    public interface IQuarterRepository : IRepositoryBase<GoalQuarters>
    {
        List<GoalQuarters> GetQuarterFromYearDB(int year);
        List<GoalQuarters> GetAllQuartersDB();
        GoalQuarters GetQuarterByIdDB(int id);
        bool CheckQuarterDB(int quarter, int year);
        bool CreateQuarterDB(GoalQuarters QuarterDetail);
    }
}
