using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.Services.AssignGoals;
using ZC_IT_TimeTracking.Services.Goals;

namespace ZC_IT_TimeTracking.Test.Controllers
{
    [TestClass]
    public class HomeTest
    {
        GoalServices _goalServices = new GoalServices();
        AssignGoalService _goalAssignServices = new AssignGoalService();
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

        [TestMethod]
        public void AssignGoalTest()
        {
            int[] ResId = { dbCtx.Resources.Select(s => s.ResourceID).FirstOrDefault() };
            AssignGoal Goal = new AssignGoal();
            Goal.Goal_MasterID = 3;
            Goal.ResourceID = ResId ;
            Goal.weight = 60;
            bool  isAssigned = _goalAssignServices.AssignGoal(Goal);
            Assert.AreEqual(isAssigned , true);
        }

        [TestMethod]
        public void EditAssignedGoalTest()
        {
            int Weight = 10;
            int ResId = dbCtx.Resource_Goal.Select(s => s.ResourceID).FirstOrDefault();
            int GoalId = dbCtx.Resource_Goal.Select(s => s.Goal_MasterID).FirstOrDefault();
            bool IsUpdated = _goalAssignServices.EditAssignedGoal(Weight, ResId, GoalId);
            Assert.AreEqual(IsUpdated, true);
        }

        [TestMethod]
        public void DeleteAssignedGoalTest()
        { 
        int Resource_GoalId=dbCtx.Resource_Goal.Select(m=>m.Resource_GoalID).FirstOrDefault();
        bool IsDeleted = _goalAssignServices.DeleteAssignedGoal(Resource_GoalId);
        Assert.AreEqual(IsDeleted , true);
        }

        //[TestMethod]
        //public void GetAssignedGoalTest()
        //{
        //    int AssignedGoalId = dbCtx.Resource_Goal.Select(s => s.Resource_GoalID).FirstOrDefault();
        //    ResourceGoal AssignedGoal = _goalAssignServices.GetAssignedGoal(AssignedGoalId);
        //    int GoalId =dbCtx.Resource_Goal.Where(s => s.Resource_GoalID == AssignedGoalId).Select(s => s.Goal_MasterID);
        //    int ResId = dbCtx.Resource_Goal.Where(s => s.Resource_GoalID == AssignedGoalId).Select(s => s.ResourceID);
        //    int Weight = dbCtx.Resource_Goal.Where(s => s.Resource_GoalID == AssignedGoalId).Select(s => s.Weight);
        //    Assert.AreEqual(AssignedGoal.Goal_MasterID, GoalId);
        //    Assert.AreEqual(AssignedGoal.ResourceID, ResId);
        //    Assert.AreEqual(AssignedGoal.weight, Weight);
        //}

    }
}
