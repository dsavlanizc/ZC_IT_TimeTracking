using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using ZC_IT_TimeTracking.Services;
using ZC_IT_TimeTracking.Services.Services;
using ZC_IT_TimeTracking.ViewModels;
using ZC_IT_TimeTracking.BusinessEntities;

namespace ZC_IT_TimeTracking.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseEntities DbContext = new DatabaseEntities();
        IGoalService _goalServices = new Services.Services.GoalService();

        // GET: Home
        [Authorize]
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

        //Using Services
        [HttpPost]
        public JsonResult CreateGoal(Goal goal)
        {
            try
            {
                var IsCreate = _goalServices.CreateGoal(goal);
                if (IsCreate)
                {
                    return Json(new JsonResponse { message = "Goal created successfully!", success = true });
                }
                else
                {
                    return Json(new JsonResponse { message = "Error occured while creating goal!", success = false });
                    
                }
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse { message = "Error occured while creating goal!", success = false });
            }
        }

        //Using Services
        [HttpPost]
        public JsonResult UpdateGoal(Goal GoalData)
        {
            try
            {
                var IsUpdate = _goalServices.UpdateGoal(GoalData);
                if (IsUpdate)
                {
                    return Json(new JsonResponse { message = "Goal updated successfully!", success = true });
                }
                else
                {
                    return Json(new JsonResponse { message = "Error occured while updating goal!", success = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse { message = "Error occured while updating goal!", success = false });
            }
        }

        //Using Services
        [HttpPost]
        public JsonResult DeleteGoal(int[] id)
        {
            try
            {
                var Isdelete = _goalServices.DeleteGoal(id);
                return Json(new JsonResponse { message = Isdelete.message,success = Isdelete.success });                
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while deleting!", success = false });
            }
        }

        //using Services
        [HttpPost]
        public JsonResult AddQuarter(GoalQuarters QuarterData)
        {
            try
            {
                var IsCreate = _goalServices.CreateQuarter(QuarterData);
                return Json(new JsonResponse { message = IsCreate.message, success = IsCreate.success }); ;                
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

        //Using Sevices
        [HttpPost]
        public ActionResult GetDescription(int TitleID)
        {
            try
            {
                var Desc = _goalServices.GetGoalDescription(TitleID);
                if(Desc == null)
                    return Json(new JsonResponse { message = "Error occured while Getting Description!", success = false });
                else
                    return Json(new { TitleData = Desc.GoalDescription , success = true });
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
                var TeamMember = DbContext.GetResourceByTeam(TeamID).Select(s => new { s.ResourceID, Name = s.FirstName+" "+s.LastName }).ToList();
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

        [HttpPost]
        public JsonResult GetAssignedGoal(int AssignId)
        {
            try
            {
                if (DbContext.Resource_Goal.Any(m => m.Resource_GoalID == AssignId))
                {
                    var AssignedGoal = DbContext.GetAssignedGoalDetails(AssignId).FirstOrDefault();
                    //int id = AssignedGoal.Goal_MasterID;
                    return Json(new { Data = AssignedGoal, success = true });
                }
                return Json(new JsonResponse { message = "Requested Assigned goal does not exist", success = false });
            }
            catch (Exception)
            {
                return Json(new JsonResponse { message = "Error occured while fetching Assigned goal", success = false });
            }
        }

        [HttpPost]
        public ActionResult EditAssignedGoal(int Weight, int ResourceId, int GoalID)
        {
            try
            {
                DbContext.UpdateResourceGoal(ResourceId, GoalID, Weight);
                return Json(new JsonResponse { message = "Weight updated successfully!", success = true });
            }
            catch (Exception)
            {
                return Json(new JsonResponse { message = "Error occured while fetching Update Weight", success = false });
            }
        }

        [HttpPost]
        public ActionResult DeleteAssignedGoal(int Id)
        {
            try
            {
                if (DbContext.Resource_Goal.Any(m => m.Resource_GoalID == Id))
                {
                    DbContext.DeleteResourceGoal(Id);
                    return Json(new JsonResponse { message = "Deleted Successfully!", success = true });
                }
                return Json(new JsonResponse { message = "No Such Goal is Assigned", success = false });
            }
            catch (Exception)
            {
                return Json(new JsonResponse { message = "Error occured while fetching Delete Assigned Goal", success = false });                
            }
        }
    }
}