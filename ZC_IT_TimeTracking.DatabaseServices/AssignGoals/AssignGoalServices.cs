using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.BusinessEntities.Model;
using System.Data.Entity;
using AutoMapper;


namespace ZC_IT_TimeTracking.Services.AssignGoals
{
    public class AssignGoalService : ServiceBase
    {
        private DatabaseEntities DbContext = new DatabaseEntities();

        public List<ResourcesByTeam> GetResourceByTeam(int teamId)
        {
            var lst = new List<ResourcesByTeam>();
            try
            {
                var data = DbContext.GetResourceByTeam(teamId).Select(s => new { s.ResourceID, Name = s.FirstName + " " + s.LastName }).ToList();
                Mapper.CreateMap<GetResourceByTeam_Result, ResourcesByTeam>();
                lst = Mapper.Map<List<ResourcesByTeam>>(data);
                return lst;
            }
            catch
            {
                this.ValidationErrors.Add("NO_TEAM_EXIST", "Error While fetching record");
                return null;
            }
        }

        public List<Team> GetTeam()
        {
            try
            {
                var team = DbContext.Teams.ToList();
                return team;
            }
            catch
            {
                this.ValidationErrors.Add("NO_TEAM_EXIST", "Error While fetching record");
                return null;
            }
        }

        public List<GetAllGoalsOfResource_Result> GetAllGoalsOfResource(int ResourceId)
        {
            try
            {
                return DbContext.GetAllGoalsOfResource(ResourceId).ToList();
            }
            catch
            {
                this.ValidationErrors.Add("NO_RES_GOAL_EXIST", "Error While fetching Resource Goal");
                return null;
            }
        }
        public List<GetAssignedGoalDetails_Result> GetAssignedGoalDetails(int AssignGoalId)
        {
            try
            {
                return DbContext.GetAssignedGoalDetails(AssignGoalId).ToList();
            }
            catch
            {
                this.ValidationErrors.Add("NO_ASGN_GOAL", "Error While fetching Assigned Goal Record");
                return null;
            }
        }

        public List<GetResourceGoalDetails_Result> GetResourceGoalDetails(int Resourceid, int GoalId)
        {
            try
            {
                return DbContext.GetResourceGoalDetails(Resourceid, GoalId).ToList();
            }
            catch
            {
                this.ValidationErrors.Add("NO_RES_GOAL", "Error While fetching Resouce Goal Record");
                return null;
            }
        }

        public bool AssignGoal(AssignGoal AssignData)
        {
            try
            {
                int count = 0;
                foreach (int id in AssignData.ResourceID)
                {
                    var v = GetResourceGoalDetails(id, AssignData.Goal_MasterID).FirstOrDefault();
                    if (v == null)
                    {
                        ObjectParameter insertedId = new ObjectParameter("CurrentInsertedId", typeof(int));
                        var AssignGoal = DbContext.AssignGoalToResource(id, AssignData.Goal_MasterID, AssignData.weight, DateTime.Now.Date, insertedId);
                        count++;
                    }
                }
                if (count == AssignData.ResourceID.Count())
                    return true;

                else
                {
                    this.ValidationErrors.Add("GoalNotAssigned", "Not all Goal were assigned Succesfully!");
                    return false;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ExceptionGoalAssign", "Error occured while Assigning a Goal!");
                return false;
            }
        }

        public ResourceGoal GetAssignedGoal(int AssignGoalId)
        {
            var GoalDetail = new ResourceGoal();
            try
            {
                var AssignedGoal = GetAssignedGoalDetails(AssignGoalId).FirstOrDefault();
                if (DbContext.Resource_Goal.Any(m => m.Resource_GoalID == AssignGoalId))
                {
                    Mapper.CreateMap<GetAssignedGoalDetails_Result, ResourceGoal>();
                    GoalDetail = Mapper.Map<ResourceGoal>(AssignedGoal);
                    return GoalDetail;
                }
                this.ValidationErrors.Add("GoalExistance", "No such goal does exist!");
                return null;
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("ExceptionGetGoal", "Error occured while Fetching Goal Details!");
                return null;
            }
        }

        public AssignGoal ViewAssignGoalToResource(int ResourceId)
        {
            var GoalDetail = new AssignGoal();
            try
            {
                var AssignedGoal = GetAllGoalsOfResource(ResourceId).FirstOrDefault();
                if (DbContext.Resource_Goal.Any(m => m.Resource_GoalID == AssignedGoal.Goal_MasterID))
                {
                    Mapper.CreateMap<GetResourceGoalDetails_Result, AssignGoal>();
                    GoalDetail = Mapper.Map<AssignGoal>(AssignedGoal);
                    return GoalDetail;
                }
                this.ValidationErrors.Add("GoalExistance", "No such goal exist!");
                return GoalDetail;
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("ExceptionGetGoal", "Error occured while Fetching Goal Details!");
                return GoalDetail;
            }
        }

        public List<AssignGoal> ViewAssignGoalToTeam(int TeamId)
        {
            var GoalDetails = new List<AssignGoal>();
            try
            {
                var TeamMember = GetResourceByTeam(TeamId);
                foreach (var member in TeamMember)
                {
                    int resourceId = member.ResourceID;
                    var ResGoal = ViewAssignGoalToResource(resourceId);
                    GoalDetails.Add(ResGoal);
                }
                return GoalDetails;
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("ExceptionGetGoal", "Error occured while Fetching Goal Details!");
                return GoalDetails;
            }
        }

        public bool EditAssignedGoal(int Weight, int ResourceId, int GoalID)
        {
            try
            {
                if (DbContext.Resource_Goal.Any(m => m.Goal_MasterID == GoalID) && DbContext.Resource_Goal.Any(m => m.ResourceID == ResourceId))
                {
                    DbContext.UpdateResourceGoal(ResourceId, GoalID, Weight);
                    return true;
                }
                this.ValidationErrors.Add("GoalExistance", "No such goal exist!");
                return false;
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("ExceptionGoalEdit", "Error occured while Editing a Goal Details!");
                return false;
            }
        }

        public bool DeleteAssignedGoal(int Id)
        {
            try
            {
                if (DbContext.Resource_Goal.Any(m => m.Resource_GoalID == Id))
                {
                    DbContext.DeleteResourceGoal(Id);
                    return true;
                }
                this.ValidationErrors.Add("GoalExistance", "No such goal exist!");
                return false;
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("ExceptionGoalEdit", "Error occured while Deleting a Goal!");
                return false;
            }
        }
    }
}
