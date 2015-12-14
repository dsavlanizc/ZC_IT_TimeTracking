using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Interfaces.Resource
{
    public interface IResourceRepository : IRepositoryBase<Resources>
    {
        List<Resources> GetResourceByTeam(int teamId);
        List<Resources> GetAllGoalsOfResource(int resId);
        List<Resources> GetResourceGoalDetails(int resId, int goalId);
    }
}
