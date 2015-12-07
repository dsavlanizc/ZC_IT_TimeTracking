using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;

namespace ZC_IT_TimeTracking.Services.GoalServices
{
    public interface IGoalService : IServiceBase
    {
        List<GetQuarterFromYear_Result> GetQuarterFromYear(int year);
        List<GetSpecificRecordsOfGoal_Result> GetGoalDetail(int StartFrom, int PageSize, ObjectParameter count);
        GetGoalDetails_Result GetGoaldetail(int id);
        GetQuarterDetails_Result GetGoalQuarter(int id);
        List<GetGoalRuleDetails_Result> GetGoalRules(int Goalid);
        bool CreateGoal(Goal goal);
        bool DeleteAllGoalRule(int id);
        bool UpdateGoal(Goal goal);
        JsonResponse DeleteGoal(int[] goalid);
        bool CheckQuarter(int Quarter, int Year);
        JsonResponse CreateQuarter(GoalQuarters QuarterDetail);
        GetGoalDescription GetGoalDescription(int GoalID);
    }
}
