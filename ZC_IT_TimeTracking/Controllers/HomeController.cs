﻿using Newtonsoft.Json;
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
        private int GetQuarter()
        {
            DateTime date = DateTime.Now;
            if (date.Month >= 1 && date.Month <= 3)
                return 1;
            else if (date.Month >= 4 && date.Month <= 7)
                return 2;
            else if (date.Month >= 8 && date.Month <= 10)
                return 3;
            else
                return 4;
        }
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                int Quarter = GetQuarter();
                int Year = DateTime.Now.Year;
                var quarter = DbContext.CheckQuater(Quarter, Year).FirstOrDefault();
                if (quarter == null)
                    ViewData["AddQuarterRequest"] = "If you want To Create A Goal than first Add Quarter";
                var GoalList = DbContext.Goal_Master.ToList();
                return View(GoalList);
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
                    return Json(new { goal = goal, quarter = quarter, rules = rules, success = true });
                }
                return Json(new { message = "Requested user data does not exist", success = false });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Error occured while fetching user data", success = false });
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
                return Json(new { message = "Goal created successfully!", success = true });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Error occured while creating goal!", success = false });
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
                return Json(new { message = "Goal updated successfully!", success = true });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Error occured while updating goal!", success = false });
            }
        }

        [HttpPost]
        public JsonResult DeleteGoal(int id)
        {
            try
            {
                DbContext.DeleteGoalMaster(id);
                return Json(new { message = "Goal Deleted Successfully!", success = true });
            }
            catch
            {
                return Json(new { message = "Error occured while deleting!", success = false });
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
                    return Json(new { message = "Quarter created successfully!", success = true }); ;
                }
                else
                {
                    return Json(new { message = "Quarter Is already Added!", success = false }); ;
                }


            }
            catch (Exception e)
            {
                return Json(new { message = "Error occured while creating Quarter!", success = false });
            }
        }
    }
}