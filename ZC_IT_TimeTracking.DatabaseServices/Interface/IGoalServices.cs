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

        //public JsonResponse GetGoalByID(int goalid)
        //{
        //    var GoalDetail = GetGoaldetail(goalid);
        //    var Quarter = GetGoalQuarter(goalid);
        //    var GoalRule = GetGoalRules(goalid);
        //    var quarterList = dbContext.Goal_Quarter.Select(s => new { s.GoalQuarter, s.QuarterYear }).ToList();

        //}

        public JsonResponse CreateGoal(Goal goal)
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
                return new JsonResponse { message = "Requested user data does not exist", success = false };
            }
            finally { }
        }

    }
}
