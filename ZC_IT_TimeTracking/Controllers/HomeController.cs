using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlTypes;
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
                var quarter = DbContext.GetQuaterDetails(goal.QuaterId).FirstOrDefault();
                var rules = DbContext.GetGoalRuleDetails(Id).ToList();
                string res = JsonConvert.SerializeObject(new {goal = goal, quarter = quarter, rules = rules});
                return Json(res);
            }
            return Json("{\"message\":\"Error while fetching data for the user.\"}");
        }
    }
}