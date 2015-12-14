﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Goal;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Quarters;
using ZC_IT_TimeTracking.DataAccess.Library.Validations;
using ZC_IT_TimeTracking.Services.GoalRuleServices;
using ZC_IT_TimeTracking.Services.Interfaces;

namespace ZC_IT_TimeTracking.Services.Goals
{
    public class GoalService : ServiceBase, IGoalServices
    {
        private IGoalRepository _goalRepository;
        private IGoalRuleServices _ruleServices;
        private IQuarterService _quarterServices;
        public GoalService()
        {
            _goalRepository = ZC_IT_TimeTracking.DataAccess.Factory.RepositoryFactory.GetInstance().GetGoalRepository();
            this.ValidationErrors = _goalRepository.ValidationErrors;
        }

        public List<GoalMaster> SearchGoalByTitle(string title, int skip, int recordPerPage,int count)
        {
            try
            {
                var SGBT = _goalRepository.SearchGoalByTitleDB(title, skip, recordPerPage, count).ToList();
                if (SGBT.Count != 0)
                    return SGBT;
                else
                {
                    this.ValidationErrors.Add("NO_DATA_AVL", "String not found in any Title");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("Goal_Search_Error", "Error occured while searching goal by id!");
                return null;
            }
        }

        public List<GoalMaster> GetGoalDetail(int StartFrom, int PageSize, int count)
        {
            try
            {
                var GSROG = _goalRepository.GetSpecificRecordsOfGoalDB(StartFrom, PageSize, count).ToList();
                if (GSROG.Count != 0)
                    return GSROG;
                else
                {
                    this.ValidationErrors.Add("NO_SPF_GOAL_AVL", "Specific Goal Details are not Available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Goal List!");
                return null;
            }
        }

        public GoalMaster GetGoaldetailByGoalID(int id)
        {
            try
            {
                var GGDR = _goalRepository.GetGoalDetailsByIDDB(id);
                if (GGDR != null)
                    return GGDR;
                else
                {
                    this.ValidationErrors.Add("NO_GOAL_AVL", "No Goal Details are Available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Goal Detail!");
                return null;
            }
        }

        public bool CreateGoal(GoalMaster goal)
        {
            try
            {
                var CheckQuarter = _quarterServices.CheckQuarter(goal.GoalQuarter, goal.QuarterYear);
                ObjectParameter insertedId = new ObjectParameter("CurrentInsertedId", typeof(int));
                _goalRepository.InsertGoalMasterDB(goal.GoalTitle, goal.GoalDescription, goal.UnitOfMeasurement, goal.MeasurementValue, goal.IsHigherValueGood, DateTime.Today, CheckQuarter.QuarterID, insertedId);
                Int32 goalId = Int32.Parse(insertedId.Value.ToString());
                foreach (GoalRule rule in goal.GoalRules)
                {
                    GoalRule gr = new GoalRule();
                    gr.Performance_RangeFrom = rule.Performance_RangeFrom;
                    gr.Performance_RangeTo = rule.Performance_RangeTo;
                    gr.Rating = rule.Rating;
                    gr.GoalId = rule.GoalId;
                    _ruleServices.InsertGoalRules(gr);
                }
                return true;
            }
            catch
            {
                this.ValidationErrors.Add("ERR_ADD_GOAL", "Error ocurred while Creating Goal!");
                return false;
            }
        }

        public bool UpdateGoal(GoalMaster goal)
        {
            try
            {
                var quarter = _quarterServices.CheckQuarter(goal.GoalQuarter, goal.QuarterYear);
                _goalRepository.UpdateGoalMasterDB(goal.Goal_MasterID, goal.GoalTitle, goal.GoalDescription, goal.UnitOfMeasurement, goal.MeasurementValue, DateTime.Today, goal.IsHigherValueGood, quarter.QuarterID);
                _ruleServices.DeleteAllGoalRule(goal.Goal_MasterID);
                foreach (GoalRule rule in goal.GoalRules)
                {
                    GoalRule gr = new GoalRule();
                    gr.Performance_RangeFrom = rule.Performance_RangeFrom;
                    gr.Performance_RangeTo = rule.Performance_RangeTo;
                    gr.Rating = rule.Rating;
                    gr.GoalId = rule.GoalId;
                    _ruleServices.InsertGoalRules(gr);
                }
                return true;
            }
            catch
            {
                this.ValidationErrors.Add("ERR_EDIT_GOAL", "Error Ocurred while Updating Goal!");
                return false;
            }
        }

        public JsonResponse DeleteGoal(int[] goalid)
        {
            JsonResponse js = new JsonResponse();
            try
            {
                int count = 0;
                foreach (int i in goalid)
                {
                    int res = _goalRepository.DeleteGoalMasterDB(i);
                    if (res > 0) count++;
                }
                if (count == goalid.Length)
                {
                    js.message = "Goal(s) Deleted Successfully!";
                    js.success = true;
                    return js;
                }
                else if (count > 0)
                {
                    js.message = "Some of goal(s) deleted!";
                    js.success = true;
                    return js;
                }
                else
                {
                    js.message = "No such goal exist!";
                    js.success = false;
                    return js;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_DEL_GOAL", "Error Occured while Deleting Goal!");
                return null;
            }
        }

        public bool IsGoalExist(int goalID)
        {
            try
            {
                var IGE = _goalRepository.IsGoalExistDB(goalID);
                return IGE;
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Goal!");
                return false;
            }
        }

        public List<GoalMaster> GetGoalIDandTitle()
        {
            try
            {
                int Quarter = Utilities.GetQuarter();
                int Year = DateTime.Now.Year;
                var Glist = _goalRepository.GoalListByQuarterDB(Quarter,Year);
                if (Glist.Count != 0)
                    return Glist;
                else
                {
                    this.ValidationErrors.Add("NO_Goal_AVL", "No Goal Available!");
                    return null;
                }
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Goal List!");
                return null;
            }
        }
    }
}
