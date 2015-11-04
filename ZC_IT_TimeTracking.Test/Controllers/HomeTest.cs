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

        //For Index
        [TestMethod]
        public void GoalList()
        {
            HomeController idx = new HomeController();
            ViewResult result = idx.Index() as ViewResult;
            Assert.AreNotEqual("_ErrorView", result.ViewName);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        //For DeleteGoal 
        [TestMethod]
        public void DeleteGoals()
        {
            HomeController Del = new HomeController();
            object delGoal = Del.DeleteGoal(1).Data;
            string Success = delGoal.GetType().GetProperty("success").GetValue(delGoal, null).ToString();
            Assert.AreEqual("True", Success);
        }

        //For AddQuarter
        [TestMethod]
        public void Addquarter()
        {
            HomeController qua = new HomeController();
            GoalQuarters goalqua = new GoalQuarters();
            DateTime dt = new DateTime(2015,1,10); 
            goalqua.GoalQuarter = 1;
            goalqua.QuarterYear = 2015;
            goalqua.GoalCreateFrom = DateTime.Now.Date;
            goalqua.GoalCreateTo = dt;
            object AddQua = qua.AddQuarter(goalqua).Data;
            string Success = AddQua.GetType().GetProperty("success").GetValue(AddQua, null).ToString();
            Assert.AreEqual("True", Success);
        }
    }
}
