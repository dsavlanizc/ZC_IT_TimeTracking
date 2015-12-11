using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Goal;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Quarters;
using ZC_IT_TimeTracking.DataAccess.Library.Validations;
using ZC_IT_TimeTracking.Services.Interfaces;

namespace ZC_IT_TimeTracking.Services.Goals
{
    public class GoalService : ServiceBase, IGoalServices
    {
        private IGoalRepository _goalRepository;
        public GoalService()
        {
            _goalRepository = ZC_IT_TimeTracking.DataAccess.Factory.RepositoryFactory.GetInstance().GetGoalRepository();
            this.ValidationErrors = _goalRepository.ValidationErrors;
        }

        //public List<GoalMaster> SearchGoalByTitle(string title, int skip, int recordPerPage, ref ObjectParameter count)
        //{
        //    try
        //    {
        //        var SGBT = dbContext.SearchGoalByTitle(title, skip, recordPerPage, count).ToList();
        //        if (SGBT.Count != 0)
        //            return SGBT;
        //        else
        //        {
        //            this.ValidationErrors.Add("NO_DATA_AVL", "String not found in any Title");
        //            return null;
        //        }
        //    }
        //    catch
        //    {
        //        this.ValidationErrors.Add("Goal_Search_Error", "Error occured while searching goal by id!");
        //        return null;
        //    }
        //}
    }
}
