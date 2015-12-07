using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.Services;
using ZC_IT_TimeTracking.Models;

namespace ZC_IT_TimeTracking.Services.Interface
{
    public class IGoalServices : ServiceBase
    {
        private DatabaseEntities dbContext = new DatabaseEntities();
        private IServiceBase Error;
        public List<GetQuarterFromYear_Result> GetQuarterFromYear(int year)
        {
            try
            {
                return dbContext.GetQuarterFromYear(year).ToList();
            }
            finally { }
        }

        public List<GetSpecificRecordsOfGoal_Result> GetGoalDetail(int StartFrom, int PageSize, ObjectParameter count)
        {
            try
            {
                return dbContext.GetSpecificRecordsOfGoal(StartFrom, PageSize, count).ToList();
            }
            finally { }
        }

        public GetGoalDetails_Result GetGoaldetail(int id)
        {
            try
            {
                return dbContext.GetGoalDetails(id).FirstOrDefault();
            }
            finally { }
        }

        public GetQuarterDetails_Result GetGoalQuarter(int id)
        {
            try
            {
                return dbContext.GetQuarterDetails(id).FirstOrDefault();
            }
            finally { }
        }

        public List<GetGoalRuleDetails_Result> GetGoalRules(int Goalid)
        {
            try
            {
                return dbContext.GetGoalRuleDetails(Goalid).ToList();
            }
            finally { }
        }

        public bool CreateGoal(Goal goal)
        {
            try
            {
                var CheckQuarter = dbContext.CheckQuater(goal.Quarter, goal.Year).FirstOrDefault();
                ObjectParameter insertedId = new ObjectParameter("CurrentInsertedId", typeof(int));
                dbContext.InsertGoalMaster(goal.Title, goal.Description, goal.UnitOfMeasurement, goal.MeasurementValue, goal.IsHigher, DateTime.Today, CheckQuarter.QuarterID, insertedId);
                Int32 goalId = Int32.Parse(insertedId.Value.ToString());

                foreach (GoalRule rule in goal.GoalRules)
                {
                    dbContext.InsertGoalRules(rule.RangeFrom, rule.RangeTo, rule.Rating, goalId);
                }
                return true;
            }
            catch
            {
                Error.ValidationErrors.Add("ERR_ADD_GOAL", "Error ocurred while Creating Goal!");
                return false;
            }
            finally { }
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
                Error.ValidationErrors.Add("ERR_DEL_GOAL", "Error Ocurred while Deleting Goal Rule!");
                return false;
            }
        }

        public bool UpdateGoal(Goal goal)
        {
            try
            {
                var quarter = dbContext.CheckQuater(goal.Quarter, goal.Year).FirstOrDefault();
                dbContext.UpdateGoalMaster(goal.ID, goal.Title, goal.Description, goal.UnitOfMeasurement, goal.MeasurementValue, DateTime.Today, goal.IsHigher, quarter.QuarterID);
                dbContext.Delete_AllRulesOfGoal(goal.ID);
                foreach (GoalRule rule in goal.GoalRules)
                {
                    dbContext.InsertGoalRules(rule.RangeFrom, rule.RangeTo, rule.Rating, goal.ID);
                }
                return true;
            }
            catch {
                Error.ValidationErrors.Add("ERR_EDIT_GOAL", "Error Ocurred while Updating Goal!");
                return false;
            }
            finally { }
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
                js.message = "Error occured while deleting!";
                js.success = false;
                return js;
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
                Error.ValidationErrors.Add("No_QUT_FOUND", "Error Ocurred while Finding Querter!");
                return false;
            }
        }

        public JsonResponse CreateQuarter(Goal_Quarter QuarterDetail)
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
                js.message = "Error occured while Creating Quarter!";
                js.success = false;
                return js;
            }
        }
    }
}
