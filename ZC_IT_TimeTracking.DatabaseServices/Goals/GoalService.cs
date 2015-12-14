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
using ZC_IT_TimeTracking.Services.Quarters;

namespace ZC_IT_TimeTracking.Services.Goals
{
    public class GoalService : ServiceBase, IGoalServices
    {
        private IGoalRepository _goalRepository;
        private IGoalRuleServices _ruleServices = new GoalRuleService();
        private IQuarterService _quarterServices = new QuarterService();
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
                if (CheckQuarter != null)
                {
                    goal.QuarterId = CheckQuarter.QuarterID;
                    int goalId = _goalRepository.InsertGoalMasterDB(goal);
                    if (goalId != -1)
                    {
                        foreach (GoalRule rule in goal.GoalRules)
                        {
                            GoalRule gr = new GoalRule();
                            gr.Performance_RangeFrom = rule.Performance_RangeFrom;
                            gr.Performance_RangeTo = rule.Performance_RangeTo;
                            gr.Rating = rule.Rating;
                            gr.GoalID = goalId;
                            _ruleServices.InsertGoalRules(gr);
                        }
                        return true;
                    }
                    else
                    {
                        this.ValidationErrors.Add("ERR_GOAL_ID", "Inserted goal id not found!");
                    }
                }
                return false;
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
                _goalRepository.UpdateGoalMasterDB(goal);
                _ruleServices.DeleteAllGoalRule(goal.Goal_MasterID);
                foreach (GoalRule rule in goal.GoalRules)
                {
                    GoalRule gr = new GoalRule();
                    gr.Performance_RangeFrom = rule.Performance_RangeFrom;
                    gr.Performance_RangeTo = rule.Performance_RangeTo;
                    gr.Rating = rule.Rating;
                    gr.GoalID = rule.GoalID;
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
    }
}
