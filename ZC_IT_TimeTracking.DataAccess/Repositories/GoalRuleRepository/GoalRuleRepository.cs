using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;
using ZC_IT_TimeTracking.DataAccess.Interfaces;
using ZC_IT_TimeTracking.DataAccess.Interfaces.GoalRepository;

namespace ZC_IT_TimeTracking.DataAccess.Repositories.GoalRuleRepository
{
    public class GoalRuleRepository : RepositoryBase<GoalRule>,IGoalRuleRepository
    {
        const string _GetGoalRuleDetailsByGoalID = "GetGoalRuleDetails";
        const string _InsertGoalRules = "InsertGoalRules";
        const string _UpdateGoalRules = "UpdateGoalRules";
        const string _DeleteAllRulesOfGoalByGoalID = "Delete_AllRulesOfGoal";
        const string _DeleteGoalRuleByGoalRuleID = "DeleteGoalRule";
    }
}
