﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.Services.Goals;

namespace ZC_IT_TimeTracking.Test.Controllers
{
    [TestClass]
    public class HomeTest
    {
        GoalServices _goalServices = new GoalServices();
        DatabaseEntities dbCtx = new DatabaseEntities();
        [TestMethod]
        public void GetGoalByIDTest()
        {
            var goal = dbCtx.Goal_Master.OrderBy(o => o.Goal_MasterID).FirstOrDefault();
            var goalDetails = _goalServices.GetGoaldetail(goal.Goal_MasterID);
            Assert.AreEqual(goal.GoalTitle, goalDetails.GoalTitle);
        }

        [TestMethod]
        public void CreateGoalTest()
        {
            Goal goal = new Goal();
            goal.GoalTitle = "Testing goal";
            goal.GoalDescription = "goal testing description";
            goal.UnitOfMeasurement = "Days";
            goal.MeasurementValue = 12;
            goal.GoalQuarter = 4;
            goal.QuarterYear = 2015;
            GoalRule rule = new GoalRule();
            rule.Performance_RangeFrom = 10;
            rule.Performance_RangeTo = 50;
            rule.Rating = 60;
            goal.GoalRules = new List<GoalRule>();
            goal.GoalRules.Add(rule);
            bool isSuccess = _goalServices.CreateGoal(goal);
            Assert.AreEqual(true, isSuccess);
        }

        [TestMethod]
        public void UpdateGoalTest()
        {
            Goal goal = new Goal();
            goal.Goal_MasterID = dbCtx.Goal_Master.Select(s=>s.Goal_MasterID).FirstOrDefault();
            goal.GoalTitle = "Testing Update goal";
            goal.GoalDescription = "Add goal description here";
            goal.UnitOfMeasurement = "hours";
            goal.MeasurementValue = 40;
            goal.GoalQuarter = 4;
            goal.QuarterYear = 2015;
            GoalRule rule = new GoalRule();
            rule.Performance_RangeFrom = 70;
            rule.Performance_RangeTo = 90;
            rule.Rating = 80;
            goal.GoalRules = new List<GoalRule>();
            goal.GoalRules.Add(rule);
            bool isSuccess = _goalServices.UpdateGoal(goal);
            Assert.AreEqual(true, isSuccess);
        }

        [TestMethod]
        public void DeleteGoalMaster()
        {
            int[] goalId = { dbCtx.Goal_Master.Select(s => s.Goal_MasterID).FirstOrDefault() };
            JsonResponse obj = _goalServices.DeleteGoal(goalId);
            Assert.AreEqual(true, obj.success);
        }

        [TestMethod]
        public void AddQuarterTest()
        {
            GoalQuarters Quarter = new GoalQuarters();
            Quarter.GoalQuarter = 3;
            Quarter.QuarterYear = 2010;
            Quarter.GoalCreateFrom = DateTime.Today.AddYears(-5);
            Quarter.GoalCreateTo = DateTime.Today.AddYears(-5);
            JsonResponse obj = _goalServices.CreateQuarter(Quarter);
            Assert.AreEqual(true, obj.success);
        }
    }
}
