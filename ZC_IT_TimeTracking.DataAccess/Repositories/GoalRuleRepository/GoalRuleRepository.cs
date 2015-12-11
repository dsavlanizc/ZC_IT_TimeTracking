using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;
using ZC_IT_TimeTracking.DataAccess.Extensions;
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

        public List<GoalRule> GoalRuleDetailByIDDB(int goalID)
        {
            GoalRule gr = new GoalRule();
            gr.GoalId = goalID;
            return this.GetEntityCollection<GoalRule>(gr, _GetGoalRuleDetailsByGoalID);
        }

        public bool InsertGoalRuleDB(GoalRule gr)
        {
            GoalRule goalRule = new GoalRule();
            goalRule.Performance_RangeFrom = gr.Performance_RangeFrom;
            goalRule.Performance_RangeTo = gr.Performance_RangeTo;
            goalRule.Rating = gr.Rating;
            goalRule.GoalId = gr.GoalId;
            return this.InsertOrUpdate<GoalRule>(goalRule, _InsertGoalRules);
        }

        public bool DeleteAllRulesOfGoalByGoalID(int goalID)
        {
            GoalRule gr = new GoalRule();
            gr.GoalId = goalID;
            return this.Delete<GoalRule>(gr,_DeleteAllRulesOfGoalByGoalID);
        }
    }
}
