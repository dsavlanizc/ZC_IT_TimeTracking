using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Team;

namespace ZC_IT_TimeTracking.Services.Team
{
    public class TeamServices : ServiceBase
    {
        ITeamRepository _repository;

        public TeamServices()
        {
            _repository = ZC_IT_TimeTracking.DataAccess.Factory.RepositoryFactory.GetInstance().GetTeamsRepository();
            this.ValidationErrors = _repository.ValidationErrors;
        }
        
        public List<Teams> GetTeam()
        {
            return _repository.getAllTeams();
        }
    }
}
