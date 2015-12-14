using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces.GoalRepository;
using ZC_IT_TimeTracking.Services.Interfaces;

namespace ZC_IT_TimeTracking.Services.GoalRuleServices
{
    public class GoalRuleService : ServiceBase,IGoalRuleServices
    {
        private IGoalRuleRepository _goalruleRepository;
        public GoalRuleService()
        {
            _goalruleRepository = ZC_IT_TimeTracking.DataAccess.Factory.RepositoryFactory.GetInstance().GetGoalRuleRepository();
            this.ValidationErrors = _goalruleRepository.ValidationErrors;
        }

        public List<GoalRule> GetGoalRules(int Goalid)
        {
            try
            {
                var GGRD = _goalruleRepository.GoalRuleDetailByIDDB(Goalid).ToList();
                if (GGRD.Count != 0)
                    return GGRD;
                else
                {
                    this.ValidationErrors.Add("NO_RUL_DEF", "No Rules Define for Goal!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Goal Rule!");
                return null;
            }
        }

        public bool InsertGoalRules(GoalRule gr)
        {
            GoalRule goalrule = new GoalRule();
            goalrule.Performance_RangeFrom = gr.Performance_RangeFrom;
            goalrule.Performance_RangeTo = gr.Performance_RangeTo;
            goalrule.Rating = gr.Rating;
            goalrule.GoalID = gr.GoalID;
            return this._goalruleRepository.InsertGoalRuleDB(goalrule);

        }

        public bool DeleteAllGoalRule(int goalid)
        {
            try
            {
                var delete_rule = _goalruleRepository.DeleteAllRulesOfGoalByGoalID(goalid);
                return true;
            }
            catch
            {
                this.ValidationErrors.Add("ERR_DEL_GOAL", "Error Ocurred while Deleting Goal Rule!");
                return false;
            }
        }
    }
}
