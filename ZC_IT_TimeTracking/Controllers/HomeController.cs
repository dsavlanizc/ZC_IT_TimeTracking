using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using ZC_IT_TimeTracking.Services;
using ZC_IT_TimeTracking.ViewModels;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.Services.Goals;
using ZC_IT_TimeTracking.Services.AssignGoals;

namespace ZC_IT_TimeTracking.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        GoalServices _goalServices = new GoalServices();
        AssignGoalService _assignGoalServices = new AssignGoalService();

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
                var QY = _goalServices.GetQuarterFromYear(Year);
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
                    int pageSize = Utilities.RecordPerPage;
                    var GoalList = _goalServices.GetGoalDetail(skip, pageSize, count);
                    if ((GoalList.Count() == 0) && (page - 2) >= 0)
                    {
                        ViewBag.page = page - 1;
                        skip = (page - 2) * Utilities.RecordPerPage;
                        GoalList = _goalServices.GetGoalDetail(skip, pageSize, count);
                    }
                    ViewBag.TotalCount = Convert.ToInt32(count.Value);
                    return View(GoalList);
                }
                else
                {
                    ObjectParameter count = new ObjectParameter("MatchedRecords", typeof(int));
                    var GoalList = _goalServices.SearchGoalByTitle(title,skip,Utilities.RecordPerPage,ref count);
                    if ((GoalList.Count() == 0) && (page - 2) >= 0)
                    {
                        ViewBag.page = page - 1;
                        skip = (page - 2) * Utilities.RecordPerPage;
                        GoalList = _goalServices.SearchGoalByTitle(title, skip, Utilities.RecordPerPage,ref count);
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
                if (_goalServices.IsGoalExist(Id))
                {
                    var goal = _goalServices.GetGoaldetail(Id);
                    var quarter = _goalServices.GetGoalQuarter(goal.QuarterId);
                    var rules = _goalServices.GetGoalRules(Id);
                    var quarterList = _goalServices.GetAllQuarters();
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
        public JsonResult CreateGoal(Goal GoalData)
        {
            try
            {
                var IsCreate = _goalServices.CreateGoal(GoalData);
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
                return Json(new JsonResponse { message = Isdelete.message, success = Isdelete.success });
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

        //Using Services
        public ActionResult AssignGoal()
        {
            ViewBag.goal = _goalServices.GetGoalIDandTitle();
            ViewBag.Team = _assignGoalServices.GetTeam();
            return View("_AssignGoal");
        }

        //Using Sevices
        [HttpPost]
        public ActionResult GetDescription(int TitleID)
        {
            try
            {
                var Desc = _goalServices.GetGoalDescription(TitleID);
                if (Desc == null)
                    return Json(new JsonResponse { message = "Error occured while Getting Description!", success = false });
                else
                    return Json(new { TitleData = Desc.GoalDescription, success = true });
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while Getting Description!", success = false });
            }
        }

        //Using Services
        [HttpPost]
        public ActionResult GetTeamMember(int TeamID, int Weight, int GoalID)
        {
            try
            {
                var TeamMember = _assignGoalServices.GetResourceByTeam(TeamID);
                int count = TeamMember.Count;
                for (int i = 0; i < count; i++)
                {
                    var member = TeamMember.ElementAt(i);
                    var v = _assignGoalServices.GetResourceGoalDetails(member.ResourceID, GoalID);
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

        //using Services
        [HttpPost]
        public JsonResult AssignGoal(AssignGoal AssignData)
        {
            try
            {
                _assignGoalServices.ClearValidationErrors();
                var ISAssign = _assignGoalServices.AssignGoal(AssignData);
                if (ISAssign)
                {                   
                    return Json(new JsonResponse { message = "Assign Goal Succesfully", success = true });
                }
                else
                {
                    return Json(new JsonResponse { message = _assignGoalServices.ValidationErrors.Errors[0].ErrorDescription, success = false });
                }
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while Assign a Goal", success = false });
            }
        }

        //Using Services
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ViewAssignGoal(int ResId = -1, int TeamID = -1)
        {
            ViewBag.Team = _assignGoalServices.GetTeam();
            if (ResId != -1)
            {
                ViewBag.AllGoalResourse = _assignGoalServices.GetAllGoalsOfResource(ResId);
            }
            if (TeamID != -1)
            {
                var TeamMember = _assignGoalServices.GetResourceByTeam(TeamID);
                return Json(new { TeamMember = TeamMember, success = true });
            }
            //ViewBag.Resource = DbContext.Resources.Select(s => new { s.ResourceID, Name = s.FirstName + " " + s.LastName }).ToList();
            //ViewBag.AllResourceGoal = DbContext.GetAllResourceForGoal(2).ToList();
            return View("_ViewAssignGoal");
        }

        //Using Services
        [HttpPost]
        public JsonResult GetAssignedGoal(int AssignId)
        {
            try
            {
                var AssignedGoal = _assignGoalServices.GetAssignedGoal(AssignId);
                if (AssignedGoal != null)
                {
                    return Json(new { Data = AssignedGoal, success = true });
                }
                return Json(new JsonResponse { message = "Requested Assigned goal does not exist", success = false });
            }
            catch (Exception)
            {
                return Json(new JsonResponse { message = "Error occured while fetching Assigned goal", success = false });
            }
        }

        //Using Services
        [HttpPost]
        public ActionResult EditAssignedGoal(int Weight, int ResourceId, int GoalID)
        {
            try
            {
                var IsUpdate = _assignGoalServices.EditAssignedGoal(Weight, ResourceId, GoalID);
                if (IsUpdate)
                    return Json(new JsonResponse { message = "Weight updated successfully!", success = true });
                else
                    return Json(new JsonResponse { message = "No such goal exist!", success = false });
            }
            catch (Exception)
            {
                return Json(new JsonResponse { message = "Error occured while fetching Update Weight", success = false });
            }
        }

        //Using Services
        [HttpPost]
        public ActionResult DeleteAssignedGoal(int Id)
        {
            try
            {
                var IsDelete = _assignGoalServices.DeleteAssignedGoal(Id);
                if (IsDelete)
                    return Json(new JsonResponse { message = "Deleted Successfully!", success = true });
                else
                    return Json(new JsonResponse { message = "No Such Goal is Assigned", success = false });
            }
            catch (Exception)
            {
                return Json(new JsonResponse { message = "Error occured while fetching Delete Assigned Goal", success = false });
            }
        }
    }
}