using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.Services;
using ZC_IT_TimeTracking.BusinessEntities;

namespace ZC_IT_TimeTracking.Services.Goals
{
    public class GoalServices : ServiceBase
    {
        private DatabaseEntities dbContext = new DatabaseEntities();

        public List<GetQuarterFromYear_Result> GetQuarterFromYear(int year)
        {
            try
            {
                return dbContext.GetQuarterFromYear(year).ToList();
            }
            catch
            {
                this.ValidationErrors.Add("NO_QUA_AVL", "No Quarter Available!");
                return null;
            }
        }

        public List<Goal_Master> GetGoalIDandTitle()
        {
            try
            {
                return dbContext.Goal_Master.ToList();
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("NO_Goal_AVL", "No Goal Available!");
                return null;
            }
        }

        public List<GetSpecificRecordsOfGoal_Result> GetGoalDetail(int StartFrom, int PageSize, ObjectParameter count)
        {
            try
            {
                return dbContext.GetSpecificRecordsOfGoal(StartFrom, PageSize, count).ToList();
            }
            catch
            {
                this.ValidationErrors.Add("NO_SPF_GOAL_AVL", "Specific Goal Details are not Available!");
                return null;
            }   
        }

        public GetGoalDetails_Result GetGoaldetail(int id)
        {
            try
            {
                return dbContext.GetGoalDetails(id).FirstOrDefault();
            }
            catch
            {
                this.ValidationErrors.Add("NO_GOAL_AVL", "No Goal Details are Available!");
                return null;
            } 
        }

        public GetQuarterDetails_Result GetGoalQuarter(int id)
        {
            try
            {
                return dbContext.GetQuarterDetails(id).FirstOrDefault();
            }
            catch
            {
                this.ValidationErrors.Add("NO_QUA_AVL", "No Such Quarter are Available!");
                return null;
            }   
        }

        public List<GetGoalRuleDetails_Result> GetGoalRules(int Goalid)
        {
            try
            {
                return dbContext.GetGoalRuleDetails(Goalid).ToList();
            }
            catch
            {
                this.ValidationErrors.Add("NO_RUL_DEF", "No Rules Define for Goal!");
                return null;
            }
        }

        public bool CreateGoal(Goal goal)
        {
            try
            {
                var CheckQuarter = dbContext.CheckQuater(goal.GoalQuarter, goal.QuarterYear).FirstOrDefault();
                ObjectParameter insertedId = new ObjectParameter("CurrentInsertedId", typeof(int));
                dbContext.InsertGoalMaster(goal.GoalTitle, goal.GoalDescription, goal.UnitOfMeasurement, goal.MeasurementValue, goal.IsHigherValueGood, DateTime.Today, CheckQuarter.QuarterID, insertedId);
                Int32 goalId = Int32.Parse(insertedId.Value.ToString());
                foreach (GoalRule rule in goal.GoalRules)
                {
                    dbContext.InsertGoalRules(rule.Performance_RangeFrom, rule.Performance_RangeTo, rule.Rating, goalId);
                }
                return true;
            }
            catch
            {
                this.ValidationErrors.Add("ERR_ADD_GOAL", "Error ocurred while Creating Goal!");
                return false;
            }
        }

        public bool DeleteAllGoalRule(int id)
        {
            try
            {
                var delete_rule = dbContext.Delete_AllRulesOfGoal(id);
                return true;
            }
            catch
            {
                this.ValidationErrors.Add("ERR_DEL_GOAL", "Error Ocurred while Deleting Goal Rule!");
                return false;
            }
        }

        public bool UpdateGoal(Goal goal)
        {
            try
            {
                var quarter = dbContext.CheckQuater(goal.GoalQuarter, goal.QuarterYear).FirstOrDefault();
                dbContext.UpdateGoalMaster(goal.Goal_MasterID, goal.GoalTitle, goal.GoalDescription, goal.UnitOfMeasurement, goal.MeasurementValue, DateTime.Today, goal.IsHigherValueGood, quarter.QuarterID);
                dbContext.Delete_AllRulesOfGoal(goal.Goal_MasterID);
                foreach (GoalRule rule in goal.GoalRules)
                {
                    dbContext.InsertGoalRules(rule.Performance_RangeFrom, rule.Performance_RangeTo, rule.Rating, goal.Goal_MasterID);
                }
                return true;
            }
            catch
            {
                this.ValidationErrors.Add("ERR_EDIT_GOAL", "Error Ocurred while Updating Goal!");
                return false;
            }
        }

        public JsonResponse DeleteGoal(int[] goalid)
        {
            JsonResponse js = new JsonResponse();
            try
            {
                int count = 0;
                foreach (int i in goalid)
                {
                    int res = dbContext.DeleteGoalMaster(i);
                    if (res > 0) count++;
                }
                if (count == goalid.Length)
                {
                    js.message = "Goal(s) Deleted Successfully!";
                    js.success = true;
                    return js;
                }
                else if (count > 0){
                    js.message = "Some of goal(s) deleted!";
                    js.success = true;
                    return js;
                }
                else{
                    js.message = "No such goal exist!";
                    js.success = false;
                    return js;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_DEL_GOAL", "Error Occured while Deleting Goal!");
                return null;
            }
        }

        public bool CheckQuarter(int Quarter, int Year)
        {
            try
            {
                dbContext.CheckQuater(Quarter, Year).FirstOrDefault();
                return true;
            }
            catch
            {
                this.ValidationErrors.Add("NO_QUT_FOUND", "No such Quarter Available");
                return false;
            }
        }

        public JsonResponse CreateQuarter(GoalQuarters QuarterDetail)
        {
            JsonResponse js = new JsonResponse();
            try
            {
                var Cq = CheckQuarter(QuarterDetail.GoalQuarter, QuarterDetail.QuarterYear);
                if (!Cq)
                {
                    var qurter = dbContext.InsertGoalQuarter(QuarterDetail.GoalQuarter, QuarterDetail.QuarterYear, QuarterDetail.GoalCreateFrom, QuarterDetail.GoalCreateTo);
                    js.message = "Quarter created successfully!";
                    js.success = true;
                    return js;
                }
                {
                    js.message = "Quarter Is already Added!";
                    js.success = false;
                    return js;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_DEL_GOAL", "Error Occured while Creating Quarter!");
                return null;
            }
        }

        public GetGoalDescription GetGoalDescription(int GoalID)
        {
            GetGoalDescription GGD = new GetGoalDescription();
            try
            {
                GGD.GoalDescription = dbContext.GetGoalDetails(GoalID).Select(s => s.GoalDescription).FirstOrDefault();
                return GGD;
            }
            catch
            {
                this.ValidationErrors.Add("NO_DESC_AVL","No discription Available!");
                return null;
            }
        }
    }
}
