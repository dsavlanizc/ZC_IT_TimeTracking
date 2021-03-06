﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;

namespace ZC_IT_TimeTracking.Services.Interfaces
{
    public interface IGoalServices : IServiceBase
    {
        //List<GoalQuarters> GetQuarterFromYear(int year);
        List<GoalMaster> SearchGoalByTitle(string title, int skip, int recordPerPage);
        GoalMaster GetGoaldetailByGoalID(int id);
        bool CreateGoal(GoalMaster goal);
        bool UpdateGoal(GoalMaster goal);
        int DeleteGoal(int[] goalid);
        List<GoalMaster> GetGoalIDandTitle();
        int TotalRecordsOfGoal();
    }
}
