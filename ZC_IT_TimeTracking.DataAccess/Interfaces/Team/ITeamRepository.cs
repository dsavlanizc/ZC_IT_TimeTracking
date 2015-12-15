using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Interfaces.Team
{
    public interface ITeamRepository : IRepositoryBase<Teams>
    {
        List<Teams> getAllTeams();
    }
}
