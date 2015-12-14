using System.Collections.Generic;
using System.Data.Objects;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Interfaces.AssignGoals
{
    public interface IAssignGoalRepository : IRepositoryBase<AssignGoal>
    {
        bool AssignGoalDB(AssignGoal assignData);
    }
}
