using System.Collections.Generic;
using ZC_IT_TimeTracking.BusinessEntities;
namespace ZC_IT_TimeTracking.Services.Interfaces
{
    public interface IQuarterService : IServiceBase
    {
        List<GoalQuarters> GetQuarterFromYear(int year);
        List<GoalQuarters> GetAllQuarters();
        GoalQuarters GetQuarterById(int id);
        GoalQuarters CheckQuarter(int quarter, int year);
        JsonResponse CreateQuarter(GoalQuarters QuarterDetail);
    }
}
