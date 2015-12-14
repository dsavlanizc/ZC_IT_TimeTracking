using System;
using System.Collections.Generic;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Resource;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;
using System.Data.Objects;
using ZC_IT_TimeTracking.DataAccess.Extensions;

namespace ZC_IT_TimeTracking.DataAccess.Repositories.ResourceRepository
{
    class ResourceRepository : RepositoryBase<Resources> , IResourceRepository
    {
        const string  _GetResourceByTeam = "GetResourceByTeam";
        const string _GetAllGoalsOfResource = "GetAllGoalsOfResource";
        const string _GetResourceGoalDetails = "GetResourceGoalDetails";

        public List<Resources> GetResourceByTeam(int teamId)
        {
                Resources res = new Resources();
                res.TeamID = teamId;
                return this.GetEntityCollection<Resources>(res, _GetResourceByTeam);  
        }

        public List<Resources> GetAllGoalsOfResource(int resId)
        {
            Resources res = new Resources();
            res.ResourceID = resId;
            return this.GetEntityCollection<Resources>(res, _GetAllGoalsOfResource);
        }

        //Completed
        public List<Resources> GetResourceGoalDetails(int resId, int goalId)
        {
            Resources res = new Resources();
            res.ResourceID = resId;
            res.Goal_MasterID = goalId ;
            return this.GetEntityCollection<Resources>(res, _GetResourceGoalDetails);  
        }
    }


}
