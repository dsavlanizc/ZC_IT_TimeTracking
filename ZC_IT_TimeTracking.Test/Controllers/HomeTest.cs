﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.Controllers;
using ZC_IT_TimeTracking.Services.AssignGoals;
using ZC_IT_TimeTracking.Services.Goals;
using ZC_IT_TimeTracking.Services.Interfaces;
using ZC_IT_TimeTracking.Services.Quarters;
using ZC_IT_TimeTracking.Services.Resource;

namespace ZC_IT_TimeTracking.Test.Controllers
{
    [TestClass]
    public class HomeTest
    {
        IQuarterService _quarterService = new QuarterService();
        GoalService _goalServices = new GoalService();
        ResourceGoalService _rgService = new ResourceGoalService();
        ResourceService _resService = new ResourceService();

        [TestMethod]
        public void RepoTest()
        {
            var isExist = _quarterService.CheckQuarter(4, 2015);
            var asdf = _rgService.EditAssignedGoal(7, 1004, 3);
            var asas = _goalServices.TotalRecordsOfGoal();
            var qbyid = _quarterService.GetQuarterById(1);
            var allq = _quarterService.GetAllQuarters();
            var asdfd = _quarterService.GetQuarterFromYear(2015);
        }

        [TestMethod]
        public void PerformanceTest()
        {
            var Insert = _rgService.InsertPerformance(62,3050,13);
            //HomeController hr = new HomeController();
            //hr.InsertPerformance(62,3050,13);
        }

        [TestMethod]
        public void GetGoalByIDTest()
        {
            //var goal = dbCtx.Goal_Master.OrderBy(o => o.Goal_MasterID).FirstOrDefault();
            ////var goalDetails = _goalServices.GetGoaldetail(goal.Goal_MasterID);
            ////Assert.AreEqual(goal.GoalTitle, goalDetails.GoalTitle);
        }

        [TestMethod]
        public void CreateGoalTest()
        {
            GoalMaster goal = new GoalMaster();
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
            GoalMaster goal = new GoalMaster();
            goal.Goal_MasterID = 12;
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
            int[] goalId = {21};
            int obj = _goalServices.DeleteGoal(goalId);
            Assert.AreEqual(true, obj);
        }

        [TestMethod]
        public void AddQuarterTest()
        {
            GoalQuarters Quarter = new GoalQuarters();
            Quarter.GoalQuarter = 4;
            Quarter.QuarterYear = 2010;
            Quarter.GoalCreateFrom = DateTime.Today.AddYears(-5);
            Quarter.GoalCreateTo = DateTime.Today.AddYears(-5);
            JsonResponse obj = _quarterService.CreateQuarter(Quarter);
            Assert.AreEqual(true, obj.success);
        }

        [TestMethod]
        public void AssignGoalTest()
        {
            int[] ResId = { 12 };
            AssignGoal Goal = new AssignGoal();
            Goal.Goal_MasterID = 3;
            Goal.ResourceID = ResId;
            Goal.Weight = 60;
            bool isAssigned = _rgService.AssignGoal(Goal);
            Assert.AreEqual(isAssigned, true);
        }

        [TestMethod]
        public void EditAssignedGoalTest()
        {
            int Weight = 10;
            int ResId = 12;
            int GoalId = 54;
            bool IsUpdated = _rgService.EditAssignedGoal(Weight, ResId, GoalId);
            Assert.AreEqual(IsUpdated, true);
        }

        [TestMethod]
        public void DeleteAssignedGoalTest()
        {
            int Resource_GoalId = 3048;
            bool IsDeleted = _rgService.DeleteResourceGoal(Resource_GoalId);
            Assert.AreEqual(IsDeleted, true);
        }

        [TestMethod]
        public void GetAssignedGoalTest()
        {
            int AssignedGoalId = 3034;
            var AssignedGoal = _rgService.IsResourceGoalExist(AssignedGoalId);
            //int GoalId = dbCtx.Resource_Goal.Where(s => s.Resource_GoalID == AssignedGoalId).Select(s => s.Goal_MasterID).FirstOrDefault();
            //int ResId = dbCtx.Resource_Goal.Where(s => s.Resource_GoalID == AssignedGoalId).Select(s => s.ResourceID).FirstOrDefault();
            //int Weight = dbCtx.Resource_Goal.Where(s => s.Resource_GoalID == AssignedGoalId).Select(s => s.Weight).FirstOrDefault();
            //Assert.AreEqual(AssignedGoal.Goal_MasterID, GoalId);
            //Assert.AreEqual(AssignedGoal.ResourceID, ResId);
            //Assert.AreEqual(AssignedGoal.Weight, Weight);
            Console.WriteLine("GoalID :" + AssignedGoal.Goal_MasterID);
            Console.WriteLine("ResourceID :" + AssignedGoal.ResourceID);
            Console.WriteLine("weight :" + AssignedGoal.Weight);
            Assert.IsNotNull(AssignedGoal.Goal_MasterID);
            Assert.IsNotNull( AssignedGoal.ResourceID);
            Assert.IsNotNull( AssignedGoal.Weight);
        }

        [TestMethod]
        public void ViewAssignGoalToResourceTest()
        {
            //int count = 0;
            //int ResId = dbCtx.Resource_Goal.Select(s => s.ResourceID).FirstOrDefault();
            //var Goals = _rgService.GetAllGoalsOfResource(ResId);
            //int[] GoalId = dbCtx.Resource_Goal.Where(s => s.ResourceID == ResId).Select(s => s.Goal_MasterID).ToArray();
            //foreach (var i in Goals)
            //{
            //    Assert.AreEqual(GoalId[count], Goals[count].Goal_MasterID);
            //    count++;
            //}
        }

        [TestMethod]
        public void ViewAssignGoalToTeamTest()
        {
            //int cnt = 0;
            //int TeamId = dbCtx.Teams.Select(s => s.TeamID).FirstOrDefault();
            ////var Goals = _goalAssignServices.ViewAssignGoalToTeam(TeamId);
            //int[] ResId = dbCtx.GetResourceByTeam(TeamId).Select(s => s.ResourceID).ToArray();
            //foreach (int i in ResId)
            //{
            //    int[] GoalId = dbCtx.Resource_Goal.Where(s => s.ResourceID == i).Select(s => s.Goal_MasterID).ToArray();
            //    int count = 0;
            //    foreach (var j in GoalId)
            //    {
            //        Assert.AreEqual(GoalId[count], Goals[cnt].Goal_MasterID);
            //        count++;
            //        cnt++;
            //    }
            //}
        }
        [TestMethod]
        public void GetResourceByTeamTest()
        {
            //int count = 0;
            //int TeamId = dbCtx.Teams.Select(s => s.TeamID).FirstOrDefault();
            //var TeamDetails = _resService.GetResourceByTeam(TeamId);
            //int[] ResId = dbCtx.Resources.Where(s => s.TeamID == TeamId).Select(s => s.ResourceID).ToArray();
            //foreach (var i in TeamDetails)
            //{
            //    Assert.AreEqual(ResId[count], TeamDetails[count].ResourceID);
            //    count++;
            //}
        }
    }
}
