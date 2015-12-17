using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using ZC_IT_TimeTracking.Services;
using ZC_IT_TimeTracking.ViewModels;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.Services.Goals;
using ZC_IT_TimeTracking.Services.AssignGoals;
using ZC_IT_TimeTracking.Services.Quarters;
using ZC_IT_TimeTracking.Services.GoalRuleServices;
using ZC_IT_TimeTracking.Services.Resource;
using ZC_IT_TimeTracking.Services.Team;
using System.Collections.Generic;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace ZC_IT_TimeTracking.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        GoalService _goalServices = new GoalService();
        QuarterService _quarterService = new QuarterService();
        ResourceGoalService _assignGoalServices = new ResourceGoalService();
        GoalRuleService _ruleService = new GoalRuleService();
        ResourceService _resourceServices = new ResourceService();
        TeamServices _teamService = new TeamServices();

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
                var QY = _quarterService.GetQuarterFromYear(Year);
                if (!QY.Any(a => a.GoalQuarter == Quarter))
                {
                    ViewBag.Message = "There is no quarter available! Please create one";
                    return View("_AddQuarter");
                }

                //kendo grid
                ViewBag.GoalList = ReadGoals();
                //fetching data
                int skip = (page - 1) * Utilities.RecordPerPage;
                if (title == "")
                {
                    int count = _goalServices.TotalRecordsOfGoal();
                    int pageSize = Utilities.RecordPerPage;
                    var GoalList = _goalServices.GetGoalDetail(skip, pageSize);
                    if (GoalList == null && (page - 2) >= 0)
                    {
                        ViewBag.page = page - 1;
                        skip = (page - 2) * Utilities.RecordPerPage;
                        GoalList = _goalServices.GetGoalDetail(skip, pageSize);
                    }
                    ViewBag.TotalCount = _goalServices.TotalRecordsOfGoal();
                    return View(GoalList);
                }
                else
                {
                    var GoalList = _goalServices.SearchGoalByTitle(title, skip, Utilities.RecordPerPage);
                    if ((GoalList.Count() == 0) && (page - 2) >= 0)
                    {
                        ViewBag.page = page - 1;
                        skip = (page - 2) * Utilities.RecordPerPage;
                        GoalList = _goalServices.SearchGoalByTitle(title, skip, Utilities.RecordPerPage);
                    }
                    ViewBag.TotalCount = _goalServices.SearchGoalByTitleCount(title);
                    return View(GoalList);
                }
            }
            catch
            {
                return View("~/Views/Shared/_ErrorView.cshtml");
            }
        }

        public List<GoalMaster> ReadGoals()
        {
            var service = new ZC_IT_TimeTracking.Services.Goals.GoalService();
            var count = service.TotalRecordsOfGoal();
            List<GoalMaster> list = service.GetGoalDetail(0, count);
            if (list == null) list = new List<GoalMaster>();
            return list;
        }

        [HttpPost]
        public JsonResult GetGoalById(int Id)
        {
            try
            {
                if (_goalServices.IsGoalExist(Id))
                {
                    var goal = _goalServices.GetGoaldetailByGoalID(Id);
                    var quarter = _quarterService.GetQuarterById(goal.QuarterID);
                    var rules = _ruleService.GetGoalRules(Id);
                    var quarterList = _quarterService.GetAllQuarters();
                    return Json(new { goal = goal, quarter = quarter, rules = rules, quarterList = quarterList, success = true });
                }
                return Json(new JsonResponse { message = "Requested user data does not exist", success = false });
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while fetching user data", success = false });
            }
        }

        //Using Services
        [HttpPost]
        public JsonResult CreateGoal(GoalMaster GoalData)
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
            catch
            {
                return Json(new JsonResponse { message = "Error occured while creating goal!", success = false });
            }
        }

        //Using Services
        [HttpPost]
        public JsonResult UpdateGoal(GoalMaster GoalData)
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
            catch
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
                var IsCreate = _quarterService.CreateQuarter(QuarterData);
                return Json(new JsonResponse { message = IsCreate.message, success = IsCreate.success }); ;
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while creating Quarter!", success = false });
            }
        }

        //Using Services
        public ActionResult AssignGoal()
        {
            ViewBag.goal = _goalServices.GetGoalIDandTitle();
            ViewBag.Team = _teamService.GetTeam();
            return View("_AssignGoal");
        }

        //Using Sevices
        [HttpPost]
        public ActionResult GetDescription(int TitleID)
        {
            try
            {
                var Desc = _goalServices.GetGoaldetailByGoalID(TitleID);
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

        //done
        [HttpPost]
        public ActionResult GetTeamMember(int TeamID, int Weight, int GoalID)
        {
            try
            {
                var TeamMember = _resourceServices.GetResourceByTeam(TeamID);
                int count = TeamMember.Count;
                for (int i = 0; i < count; i++)
                {
                    var member = TeamMember.ElementAt(i);
                    var v = _resourceServices.GetResourceGoalDetails(member.ResourceID, GoalID);
                    if (v != null && v.Count > 0)
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

        //done
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ViewAssignGoal(int ResId = -1, int TeamID = -1)
        {
            ViewBag.Team = _teamService.GetTeam();
            if (ResId != -1)
            {
                ViewBag.AllGoalResourse = _assignGoalServices.GetAllGoalsOfResource(ResId);
                ViewBag.ResId = ResId;
            }
            if (TeamID != -1)
            {
                var TeamMember = _resourceServices.GetResourceByTeam(TeamID);
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
                var GoalExist = _assignGoalServices.IsResourceGoalExist(AssignId);
                if (GoalExist != null)
                {

                    return Json(new { Data = GoalExist, success = true });
                }
                return Json(new JsonResponse { message = "Requested Assigned goal does not exist", success = false });
            }
            catch (Exception)
            {
                return Json(new JsonResponse { message = "Error occured while fetching Assigned goal", success = false });
            }
        }

        //done
        [HttpPost]
        public ActionResult EditAssignedGoal(int Weight, int ResourceGoalId, int GoalID)
        {
            try
            {
                var IsUpdate = _assignGoalServices.EditAssignedGoal(Weight, ResourceGoalId, GoalID);
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

        //done
        [HttpPost]
        public ActionResult DeleteAssignedGoal(int Id)
        {
            try
            {
                var IsDelete = _assignGoalServices.DeleteResourceGoal(Id);
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

        public ActionResult AddPerformance()
        {
            ViewBag.TeamList = _teamService.GetTeam();
            return View("_AddPerformance");
        }

        [HttpPost]
        public JsonResult GetAllResourceGoal(int ResourceID)
        {
            try
            {
                int Quarter = Utilities.GetQuarter();
                int Year = DateTime.Now.Year;
                var AllGoalResourseList = _assignGoalServices.GetAllResourceGoalByResId(ResourceID, Quarter, Year);
                if (AllGoalResourseList.Count != 0)
                {
                    return Json(new { Data = AllGoalResourseList, success = true });
                }
                return Json(new JsonResponse { message = "No Goal Assign to this Resource", success = false });
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while fetching Data", success = false });
            }

        }

        [HttpPost]
        public JsonResult GetResourceGoalData(int TitleID)
        {
            try
            {
                var Desc = _goalServices.GetGoaldetailByGoalID(TitleID);
                if (Desc == null)
                    return Json(new JsonResponse { message = "No Description Available", success = false });
                else
                    return Json(new { TitleData = Desc, success = true });
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while Getting Description!", success = false });
            }
        }

        [HttpPost]
        public JsonResult InsertPerformance(int goalID, int resID, float resPerformance)
        {
            try
            {
                var isavl = _assignGoalServices.IsPerformanceExist(resID, goalID);
                if (isavl != null)
                {
                    var success = _assignGoalServices.InsertPerformance(goalID, resID, resPerformance);
                    if (success)
                        return Json(new JsonResponse { message = "Performance Added successfully!", success = true });
                    else
                        return Json(new JsonResponse { message = "Have some Error on Adding Performance", success = false });
                }
                return Json(new JsonResponse { message = "Performance Already Added", success = false });
            }
            catch
            {
                return Json(new JsonResponse { message = "Error occured while insert Performance", success = false });
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ViewPerformance(int ResId = -1, int QuarterId = -1)
        {
            int Year = DateTime.Now.Year;
            ViewBag.AllQuarters = _quarterService.GetQuarterFromYear(Year).OrderByDescending(s => s.GoalQuarter);
            ViewBag.Team = _teamService.GetTeam();
            if (ResId != -1 && QuarterId != -1)
            {
                var record = _assignGoalServices.GetQuaterlyPerformanceByResID(ResId, QuarterId);
                ViewBag.record = record;
                float TotalPerformance = 0;
                float weight = 0;
                if (record.Count != 0)
                {
                    foreach (var dt in record)
                    {
                        var fg = _resourceServices.CalCulateQuaterlyPerformance(dt.Resource_GoalID);
                        weight = weight + dt.Weight;
                        TotalPerformance = TotalPerformance + float.Parse(fg.Resource_Performance.ToString());
                    }
                    ViewBag.outofweight = weight;
                    ViewBag.TotalPerformance = TotalPerformance / 100;
                }

            }
            return View("_ViewPerformance");
        }
    }
}