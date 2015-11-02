using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ZC_IT_TimeTracking.Models;

namespace ZC_IT_TimeTracking.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseEntities DbContext = new DatabaseEntities();
        // GET: Home
        public ActionResult Index()
        {
            var GoalList = DbContext.Goal_Master.ToList();
            return View(GoalList);
        }
        [HttpPost]
        public JsonResult GetGoalById(int Id)
        {
            if (DbContext.Goal_Master.Any(a => a.Goal_MasterID == Id))
            {
                var goal = DbContext.GetGoalDetails(Id).FirstOrDefault();
                var quarter = DbContext.GetQuarterDetails(goal.QuarterId).FirstOrDefault();
                var rules = DbContext.GetGoalRuleDetails(Id).ToList();
                string res = JsonConvert.SerializeObject(new { goal = goal, quarter = quarter, rules = rules });
                return Json(res);
            }
            return Json("{\"message\":\"Error while fetching data for the user.\"}");
        }

        [HttpPost]
        public JsonResult CreateGoal(Goal GoalData)
        {
            try
            {
                var quarter = DbContext.CheckQuater(GoalData.Quarter, GoalData.Year).FirstOrDefault();
                ObjectParameter insertedId = new ObjectParameter("CurrentInsertedId", typeof(int));
                DbContext.InsertGoalMaster(GoalData.Title, GoalData.Description, GoalData.UnitOfMeasurement, GoalData.MeasurementValue, GoalData.IsHigher, DateTime.Today, quarter.QuarterID, insertedId);
                Int32 goalId = Int32.Parse(insertedId.Value.ToString());

                foreach (GoalRule rule in GoalData.GoalRules)
                {
                    DbContext.InsertGoalRules(rule.RangeFrom, rule.RangeTo, rule.Rating, goalId);
                }
                return Json(new { message = "Added Successfully", success = true });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Error occured!", success = false });
            }
        }
    }
}