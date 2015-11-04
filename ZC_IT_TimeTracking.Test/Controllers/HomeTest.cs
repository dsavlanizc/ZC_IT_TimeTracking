using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZC_IT_TimeTracking.Controllers;
using ZC_IT_TimeTracking.Models;

namespace ZC_IT_TimeTracking.Test.Controllers
{
    [TestClass]
    public class HomeTest
    {
        [TestMethod]
        public void GetGoalByID()
        {
            HomeController home = new HomeController();
            Object obj = home.GetGoalById(4).Data;
            string success = obj.GetType().GetProperty("success").GetValue(obj, null).ToString();
            Assert.AreEqual("True", success);
        }

        [TestMethod]
        public void CreateGoalTest()
        {
            HomeController home = new HomeController();
            Goal goal = new Goal();
            goal.Title = "Testing goal";
            goal.Description = "goal testing description";
            goal.UnitOfMeasurement = "Days";
            goal.MeasurementValue = 12;
            goal.Quarter = 4;
            goal.Year = 2015;
            GoalRule rule = new GoalRule();
            rule.RangeFrom = 10;
            rule.RangeTo = 50;
            rule.Rating = 60;
            goal.GoalRules = new List<GoalRule>();
            goal.GoalRules.Add(rule);
            object asd = home.CreateGoal(goal).Data;
            string asdfasdf = asd.GetType().GetProperty("success").GetValue(asd, null).ToString();
            Assert.AreEqual("True", asdfasdf);
        }

        [TestMethod]
        public void UpdateGoalTest()
        {
            HomeController home = new HomeController();
            Goal goal = new Goal();
            goal.ID = 1004;
            goal.Title = "Testing Update goal";
            goal.Description = "Add goal description here";
            goal.UnitOfMeasurement = "hours";
            goal.MeasurementValue = 40;
            goal.Quarter = 4;
            goal.Year = 2015;
            GoalRule rule = new GoalRule();
            rule.RangeFrom =70;
            rule.RangeTo = 90;
            rule.Rating = 80;
            goal.GoalRules = new List<GoalRule>();
            goal.GoalRules.Add(rule);
            object obj = home.UpdateGoal(goal).Data;
            string success = obj.GetType().GetProperty("success").GetValue(obj, null).ToString();
            Assert.AreEqual("True",success);
        }

        [TestMethod]
        public void DeleteGoalMaster()
        {
            HomeController home = new HomeController();
            Object obj = home.DeleteGoal(1006).Data;
            string success = obj.GetType().GetProperty("success").GetValue(obj, null).ToString();
            Assert.AreEqual("True", success);
        }

        [TestMethod]
        public void AddQuarterTest()
        {
            HomeController home = new HomeController();
            GoalQuarters Quarter = new GoalQuarters();
            Quarter.GoalQuarter = 4;
            Quarter.QuarterYear = 2010;
            Quarter.GoalCreateFrom = DateTime.Today.AddYears(-5);
            Quarter.GoalCreateTo = DateTime.Today.AddYears(-5);
            object obj = home.AddQuarter(Quarter).Data;
            string success = obj.GetType().GetProperty("success").GetValue(obj, null).ToString();
            Assert.AreEqual("True", success);
  
        }

    }
}
