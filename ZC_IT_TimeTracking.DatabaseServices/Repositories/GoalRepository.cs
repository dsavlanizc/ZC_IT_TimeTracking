using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;

namespace ZC_IT_TimeTracking.Services.Repositories
{
    public class GoalRepository
    {
        private DatabaseEntities dbContext = new DatabaseEntities();

        const string _CheckQuaterforResouces = "CheckQuater";
        const string _AddGoalToGoalMaster = "InsertGoalMaster";
        const string _AddQuarterForGoal = "InsertGoalQuarter";
        const string _AddGoalRules = "InsertGoalRules";
        const string _UpdateGoal = "UpdateGoalMaster";
        const string _UpdateGoalRule = "UpdateGoalRules";
        const string _GetAllGoalsOfResource = "GetAllGoalsOfResource";
        const string _GetAllResourceForGoal = "GetAllResourceForGoal";
        const string _GetGoalDetails = "GetGoalDetails";
        const string _GetGoalRuleDetails = "GetGoalRuleDetails";
        const string _GetQuarterDetails = "GetQuarterDetails";
        const string _GetResouceDetails = "GetResouceDetails";
        const string _GetResourceByTeam = "GetResourceByTeam";
        const string _GetResourceGoalDetails = "GetResourceGoalDetails";
        const string _getYearForQuarter = "getYearForQuarter";
        const string _Delete_AllRulesOfGoal = "Delete_AllRulesOfGoal";
        const string _DeleteGoalMaster = "DeleteGoalMaster";
        const string _DeleteResourceGoal = "DeleteResourceGoal";
        const string _DeleteGoalRule = "DeleteGoalRule";
        const string _GetSpecificRecordsOfGoal = "GetSpecificRecordsOfGoal";
        const string _GetQuarterFromYear = "GetQuarterFromYear";
        const string _SearchGoalByTitle = "SearchGoalByTitle";

        //public override List<Goal_Quarter> GetQuarter(int year)
        //{
        //    var gy = dbContext.GetQuarterFromYear(year).ToList();
        //    return gy;
        //}
    }        
}
