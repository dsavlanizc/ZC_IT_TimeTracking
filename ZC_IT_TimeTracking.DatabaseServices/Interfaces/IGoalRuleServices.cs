using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;

namespace ZC_IT_TimeTracking.Services.Interfaces
{
    public interface IGoalRuleServices :IServiceBase
    {
        List<GoalRule> GetGoalRules(int Goalid);
        bool InsertGoalRules(GoalRule gr);
        bool DeleteAllGoalRule(int goalid);
    }
}
