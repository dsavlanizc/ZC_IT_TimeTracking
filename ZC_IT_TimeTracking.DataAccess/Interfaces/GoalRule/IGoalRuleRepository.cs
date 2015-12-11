using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Interfaces.GoalRepository
{
    public interface IGoalRuleRepository : IRepositoryBase<GoalRule>
    {
        List<GoalRule> GoalRuleDetailByIDDB(int goalID);
        bool InsertGoalRuleDB(GoalRule gr);
        bool DeleteAllRulesOfGoalByGoalID(int goalID);
    }
}
