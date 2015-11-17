using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using ZC_IT_TimeTracking.Models;

namespace ZC_IT_TimeTracking.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseEntities DbContext = new DatabaseEntities();

        // GET: Home
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(int page = 1, string title = "")
        {
            try
            {
                int Quarter = Utilities.GetQuarter();
                int Year = DateTime.Now.Year;
                ViewBag.Year = Year;
                ViewBag.page = page;
                ViewBag.SearchString = title;
                ViewBag.Quarter = Quarter;
                var QY = DbContext.GetQuarterFromYear(Year);
                if (!QY.Any(a => a.GoalQuarter == Quarter))
                {
                    ViewBag.Message = "There is no quarter available! Please create one";
                    return View("_AddQuarter");
                }
                //fetching data
                int skip = (page - 1) * Utilities.RecordPerPage;
                if (title == "")
                {
                    ObjectParameter count = new ObjectParameter("totalRecords", typeof(int));
                    var GoalList = DbContext.GetSpecificRecordsOfGoal(skip, Utilities.RecordPerPage, count).ToList();
                    if ((GoalList.Count() == 0) && (page - 2) >= 0)
                    {
                        ViewBag.page = page - 1;
                        skip = (page - 2) * Utilities.RecordPerPage;
                        GoalList = DbContext.GetSpecificRecordsOfGoal(skip, Utilities.RecordPerPage, count).ToList();
                    }
                    ViewBag.TotalCount = Convert.ToInt32(count.Value);
                    return View(GoalList);
                }
                else
                {
                    ObjectParameter count = new ObjectParameter("MatchedRecords", typeof(int));
                    var GoalList = DbContext.SearchGoalByTitle(title, skip, Utilities.RecordPerPage, count).ToList();
                    if ((GoalList.Count() == 0) && (page - 2) >= 0)
                    {
                        ViewBag.page = page - 1;
                        skip = (page - 2) * Utilities.RecordPerPage;
                        GoalList = DbContext.SearchGoalByTitle(title, skip, Utilities.RecordPerPage, count).ToList();
                    }
                    ViewBag.TotalCount = Convert.ToInt32(count.Value);
                    return View(GoalList);
                }
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/_ErrorView.cshtml");
            }
        }

        [HttpPost]
        public JsonResult GetGoalById(int Id)
        {
            try
            {
                if (DbContext.Goal_Master.Any(a => a.Goal_MasterID == Id))
                {
                    var goal = DbContext.GetGoalDetails(Id).FirstOrDefault();
                    var quarter = DbContext.GetQuarterDetails(goal.QuarterId).FirstOrDefault();
                    var rules = DbContext.GetGoalRuleDetails(Id).ToList();
                    var quarterList = DbContext.Goal_Quarter.Select(s => new { s.GoalQuarter, s.QuarterYear }).ToList();
                    return Json(new { goal = goal, quarter = quarter, rules = rules, quarterList = quarterList, success = true });
                }
                return Json(new JsonResponse { message = "Requested user data does not exist", success = false });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse { message = "Error occured while fetching user data", success = false });
            }
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
                return Json(new JsonResponse { message = "Goal created successfully!", success = true });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse { message = "Error occured while creating goal!", success = false });
            }
        }

        [HttpPost]
        public JsonResult UpdateGoal(Goal GoalData)
        {
            try
            {
                var quarter = DbContext.CheckQuater(GoalData.Quarter, GoalData.Year).FirstOrDefault();
                DbContext.UpdateGoalMaster(GoalData.ID, GoalData.Title, GoalData.Description, GoalData.UnitOfMeasurement, GoalData.MeasurementValue, DateTime.Today, GoalData.IsHigher, quarter.QuarterID);
                DbContext.Delete_AllRulesOfGoal(GoalData.ID);
                foreach (GoalRule rule in GoalData.GoalRules)
                {
                    DbContext.InsertGoalRules(rule.RangeFrom, rule.RangeTo, rule.Rating, GoalData.ID);
                }
                return Json(new JsonResponse { message = "Goal updated successfully!", success = true });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse { message = "Error occured while updating goal!", success = false });
            }
        }

        [HttpPost]
        public JsonResult DeleteGoal(int[] id)
        {
            try
            {
                int count = 0;
                foreach (int i in id)
                {
                    int res = DbContext.DeleteGoalMaster(i);
                    if (res > 0) count++;
                }
                if (count == id.Length)
                    return Json(new JsonResponse { message = "Goal(s) Deleted Successfully!", success = true });
                else if (count > 0)
                    return Json(new JsonResponse { message = "Some of goal(s) deleted!", success = true });
                else
                    return Json(new JsonResponse { message = "No such goal exist!", success = false });
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while deleting!", success = false });
            }
        }

        [HttpPost]
        public JsonResult AddQuarter(GoalQuarters QuarterData)
        {
            try
            {
                var Quater = DbContext.CheckQuater(QuarterData.GoalQuarter, QuarterData.QuarterYear).FirstOrDefault();
                if (Quater == null)
                {
                    var qurter = DbContext.InsertGoalQuarter(QuarterData.GoalQuarter, QuarterData.QuarterYear, QuarterData.GoalCreateFrom, QuarterData.GoalCreateTo);
                    return Json(new JsonResponse { message = "Quarter created successfully!", success = true }); ;
                }
                else
                {
                    return Json(new JsonResponse { message = "Quarter Is already Added!", success = false }); ;
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse { message = "Error occured while creating Quarter!", success = false });
            }
        }

        public ActionResult AssignGoal()
        {
            ViewBag.goal = DbContext.Goal_Master.ToList();
            ViewBag.Team = DbContext.Teams.ToList();
            return View("_AssignGoal");
        }

        [HttpPost]
        public ActionResult GetDescription(int TitleID)
        {
            try
            {
                var TitleIDdata = DbContext.GetGoalDetails(TitleID).Select(s => s.GoalDescription).FirstOrDefault();
                return Json(new { TitleData = TitleIDdata, success = true });
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while Getting Description!", success = false });
            }
        }

        [HttpPost]
        public ActionResult GetTeamMember(int TeamID, int Weight, int GoalID)
        {
            try
            {
                var TeamMember = DbContext.GetResourceByTeam(TeamID).Select(s => new { s.ResourceID, s.FirstName }).ToList();
                int count = TeamMember.Count;
                for (int i = 0; i < count; i++)
                {
                    var member = TeamMember.ElementAt(i);
                    var v = DbContext.GetResourceGoalDetails(member.ResourceID, GoalID).FirstOrDefault();
                    if (v != null)
                    { TeamMember.RemoveAt(i); i--; count--; }

                }
                return Json(new { TeamMember = TeamMember, success = true });
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while Getting Team MemberList", success = false });
            }
        }

        [HttpPost]
        public JsonResult AssignGoal(AssignGoal AssignData)
        {
            try
            {
                int count = 0;
                foreach (int id in AssignData.ResourceID)
                {
                    var v = DbContext.GetResourceGoalDetails(id, AssignData.Goal_MasterID).FirstOrDefault();
                    if (v == null)
                    {
                        ObjectParameter insertedId = new ObjectParameter("CurrentInsertedId", typeof(int));
                        var AssignGoal = DbContext.AssignGoalToResource(id, AssignData.Goal_MasterID, AssignData.weight, DateTime.Now.Date, insertedId);
                        count++;
                    }
                }
                if (count == AssignData.ResourceID.Count())
                    return Json(new JsonResponse { message = "Assign Goal Succesfully", success = true });
                else
                    return Json(new JsonResponse { message = "Not all Goal were assigned Succesfully", success = false });

            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while Assign a Goal", success = false });
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ViewAssignGoal(int ResId = -1, int TeamID = -1)
        {
            ViewBag.Team = DbContext.Teams.ToList();
            if (ResId != -1)
            {
                ViewBag.AllGoalResourse = DbContext.GetAllGoalsOfResource(ResId).ToList();
            }            
            if (TeamID != -1)
            {
                var TeamMember = DbContext.GetResourceByTeam(TeamID).Select(s => new { s.ResourceID, Name = s.FirstName + " " + s.LastName }).ToList();
                return Json(new { TeamMember = TeamMember, success = true });
            }
            //ViewBag.Resource = DbContext.Resources.Select(s => new { s.ResourceID, Name = s.FirstName + " " + s.LastName }).ToList();
            //ViewBag.AllResourceGoal = DbContext.GetAllResourceForGoal(2).ToList();
            return View("_ViewAssignGoal");
        }
    }
}