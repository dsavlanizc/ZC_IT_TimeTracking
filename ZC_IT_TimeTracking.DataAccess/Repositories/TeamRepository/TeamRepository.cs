using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Team;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;
using System.Data.Objects;
using ZC_IT_TimeTracking.DataAccess.Extensions;

namespace ZC_IT_TimeTracking.DataAccess.Repositories.TeamRepository
{
  class TeamRepository : RepositoryBase<Teams>, ITeamRepository
    {
        const string _getAllTeams = "getAllTeams";
        public List<Teams> getAllTeams()
        {
            Teams teamList = new Teams();
            return this.GetEntityCollection<Teams>(teamList , _getAllTeams);
        }
    }
}
