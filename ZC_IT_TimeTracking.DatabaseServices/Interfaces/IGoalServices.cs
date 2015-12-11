using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;

namespace ZC_IT_TimeTracking.Services.Interfaces
{
    public interface IGoalServices : IServiceBase
    {
        List<GoalQuarters> GetQuarterFromYear(int year);
    }
}
