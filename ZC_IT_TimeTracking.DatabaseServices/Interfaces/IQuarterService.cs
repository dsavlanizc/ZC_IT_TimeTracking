using System.Collections.Generic;
using ZC_IT_TimeTracking.BusinessEntities;
namespace ZC_IT_TimeTracking.Services.Interfaces
{
    public interface IQuarterService : IServiceBase
    {
        List<GoalQuarters> GetQuarterFromYear(int year);
    }
}
