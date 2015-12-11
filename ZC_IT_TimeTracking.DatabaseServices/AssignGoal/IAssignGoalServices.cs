using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;

namespace ZC_IT_TimeTracking.Services
{
    public interface IAssignGoalServices : IServiceBase
    {
        List<GetResourceByTeam_Result> GetResourceByTeam(int teamId);
        List<GetAllGoalsOfResource_Result> GetAllGoalsOfResource(int ResourceId);
        List<GetAssignedGoalDetails_Result> GetAssignedGoalDetails(int AssignGoalId);
        bool AssignGoal(AssignGoal AssignData);
        AssignGoal GetAssignedGoal(int AssignGoalId);
        AssignGoal ViewAssignGoalToResource(int ResourceId);
        List<AssignGoal> ViewAssignGoalToTeam(int TeamId);
        bool EditAssignedGoal(int Weight, int ResourceId, int GoalID);
        bool DeleteAssignedGoal(int Id);
        List<Team> GetTeam();
    }
}
