using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
                var goal = DbContext.Goal_Master.Where(w => w.Goal_MasterID == Id).Select(s => new {
                                                                                                s.GoalTitle,
                                                                                                s.GoalDescription,
                                                                                                s.UnitOfMeasurement,
                                                                                                s.MeasurementValue,
                                                                                                s.Goal_Quater.Quater,
                                                                                                s.Goal_Quater.Year,
                                                                                                s.IsHigherValueGood,
                                                                                                s.Goal_Rules
                                                                                        }).FirstOrDefault();
                string res = JsonConvert.SerializeObject(goal, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                });
                return Json(res);
            }
            return Json("{\"message\":\"Error while fetching data for the user.\"}");
        }
    }
}