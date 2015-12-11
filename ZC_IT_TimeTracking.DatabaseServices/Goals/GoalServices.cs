using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Quarters;
using ZC_IT_TimeTracking.DataAccess.Library.Validations;
using ZC_IT_TimeTracking.Services.Interfaces;

namespace ZC_IT_TimeTracking.Services.Goals
{
    public class GoalServices : ServiceBase
    {
        private DatabaseEntities dbContext = new DatabaseEntities();


        public List<GetQuarterFromYear_Result> GetQuarterFromYear(int year)
        {
            try
            {
                var GQFY = dbContext.GetQuarterFromYear(year).ToList();
                if (GQFY.Count != 0)
                    return GQFY;
                else
                {
                    this.ValidationErrors.Add("NO_QUA_AVL", "No Quarter Available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error whle fetching data!");
                return null;
            }
        }

        public List<GoalQuarters> GetAllQuarters()
        {
            try
            {
                var GAQ = dbContext.Goal_Quarter.Select(s => new GoalQuarters { GoalQuarter = s.GoalQuarter, QuarterYear = s.QuarterYear }).ToList();
                if (GAQ.Count != 0)
                    return GAQ;
                else
                {
                    this.ValidationErrors.Add("NO_QUA_AVL", "No Quarters are available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("Quarter_FETCH_ERROR", "");
                return null;
            }
        }

        //completed SearchGoalByTitleDB()
        public List<SearchGoalByTitle_Result> SearchGoalByTitle(string title, int skip, int recordPerPage, ref ObjectParameter count)
        {
            try
            {
                var SGBT = dbContext.SearchGoalByTitle(title, skip, recordPerPage, count).ToList();
                if (SGBT.Count != 0)
                    return SGBT;
                else
                {
                    this.ValidationErrors.Add("NO_DATA_AVL", "String not found in any Title");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("Goal_Search_Error", "Error occured while searching goal by id!");
                return null;
            }
        }

        public List<Goal_Master> GetGoalIDandTitle()
        {
            try
            {
                int Quarter = Utilities.GetQuarter();
                int Year = DateTime.Now.Year;
                int QuaterId = dbContext.Goal_Quarter.Where(q => q.GoalQuarter == Quarter && q.QuarterYear == Year).Select(q => q.QuarterID).FirstOrDefault();
                var Glist = dbContext.Goal_Master.Where(g => g.QuarterId == QuaterId).ToList();
                if (Glist.Count != 0)
                    return Glist;
                else
                {
                    this.ValidationErrors.Add("NO_Goal_AVL", "No Goal Available!");
                    return null;
                }
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Goal List!");
                return null;
            }
        }

        //completed GetSpecificRecordsOfGoalDB()
        public List<GetSpecificRecordsOfGoal_Result> GetGoalDetail(int StartFrom, int PageSize, ObjectParameter count)
        {
            try
            {
                var GSROG = dbContext.GetSpecificRecordsOfGoal(StartFrom, PageSize, count).ToList();
                if (GSROG.Count != 0)
                    return GSROG;
                else
                {
                    this.ValidationErrors.Add("NO_SPF_GOAL_AVL", "Specific Goal Details are not Available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Goal List!");
                return null;
            }
        }

        public bool IsGoalExist(int id)
        {
            try
            {
                var IGE = dbContext.Goal_Master.Any(a => a.Goal_MasterID == id);
                return IGE;
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Goal!");
                return false;
            }
        }

        //Completed GetGoaldetailByGoalID()
        public GetGoalDetails_Result GetGoaldetail(int id)
        {
            try
            {
                var GGDR = dbContext.GetGoalDetails(id).FirstOrDefault();
                if (GGDR != null)
                    return GGDR;
                else
                {
                    this.ValidationErrors.Add("NO_GOAL_AVL", "No Goal Details are Available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Goal Detail!");
                return null;
            }
        }

        public GetQuarterDetails_Result GetGoalQuarter(int id)
        {
            try
            {
                var GQD = dbContext.GetQuarterDetails(id).FirstOrDefault();
                if (GQD != null)
                    return GQD;
                else
                {
                    this.ValidationErrors.Add("NO_QUA_AVL", "No Such Quarter are Available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Quarter!");
                return null;
            }
        }

        //completed GoalRuleDetailByIDDB
        public List<GetGoalRuleDetails_Result> GetGoalRules(int Goalid)
        {
            try
            {
                var GGRD = dbContext.GetGoalRuleDetails(Goalid).ToList();
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

        public bool CreateGoal(GoalMaster goal)
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

        public bool UpdateGoal(GoalMaster goal)
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
                else if (count > 0)
                {
                    js.message = "Some of goal(s) deleted!";
                    js.success = true;
                    return js;
                }
                else
                {
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
                return dbContext.Goal_Quarter.Any(a => a.QuarterYear == Year && a.GoalQuarter == Quarter);
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
                else
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
                this.ValidationErrors.Add("NO_DESC_AVL", "No discription Available!");
                return null;
            }
        }
    }
}
