using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Resource;
using ZC_IT_TimeTracking.DataAccess.Library.Validations;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.Services.Resource
{
   public  class ResourceService : ServiceBase
    {
        private IResourceRepository _ResourceRepository;

       public ResourceService()
        {
            _ResourceRepository = ZC_IT_TimeTracking.DataAccess.Factory.RepositoryFactory.GetInstance().GetResourceRepository();
            this.ValidationErrors = _ResourceRepository.ValidationErrors;
        }
       
        public List<Resources> GetResourceByTeam(int teamId)
        {
            return _ResourceRepository.GetResourceByTeam(teamId);
        }

        public List<Resources> GetAllGoalsOfResource(int ResourceId)
        {
            return _ResourceRepository.GetAllGoalsOfResource(ResourceId);
        }

        //Completed
        public List<Resources> GetResourceGoalDetails(int Resourceid, int GoalId)
        {
            return _ResourceRepository.GetResourceGoalDetails(Resourceid, GoalId);
        }


    }
}
