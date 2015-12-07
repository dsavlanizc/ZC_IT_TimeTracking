using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities.Model;
using ZC_IT_TimeTracking.Services;
//using ZC_IT_TimeTracking.Models;
using System.Data.Entity;
using AutoMapper;


namespace ZC_IT_TimeTracking.Services.AssignGoals
{
    class AssignGoalService : ServiceBase
    {
        private DatabaseEntities DbContext = new DatabaseEntities();

           public List<GetResourceByTeam_Result> GetResourceByTeam(int teamId)
        {
            try
            {
                return DbContext.GetResourceByTeam(teamId).ToList();
            }
            finally { }
        }
         public List<GetAllGoalsOfResource_Result> GetAllGoalsOfResource(int ResourceId)
        {
            try
            {
                return DbContext.GetAllGoalsOfResource(ResourceId).ToList();
            }
            finally { }
        }
        public List<GetAssignedGoalDetails_Result> GetAssignedGoalDetails(int AssignGoalId)
        {
            try
            {
                return DbContext.GetAssignedGoalDetails(AssignGoalId).ToList();
            }
            finally { }
        }

        public List<GetResourceGoalDetails_Result> GetResourceGoalDetails(int Resourceid , int GoalId)
        {
            try
            {
                return DbContext.GetResourceGoalDetails(Resourceid, GoalId).ToList();
            }
            finally { }
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

        public AssignGoal GetAssignedGoal(int AssignGoalId)
        {
            var GoalDetail=new AssignGoal();
            try
            {
                var AssignedGoal = GetAssignedGoalDetails(AssignGoalId).FirstOrDefault();
                if (DbContext.Resource_Goal.Any(m => m.Resource_GoalID == AssignedGoal.Goal_MasterID))
                {    
                    Mapper.CreateMap<GetResourceGoalDetails_Result,AssignGoal>();
                    GoalDetail=Mapper.Map<AssignGoal>(AssignedGoal);
                    return GoalDetail;
                }
                this.ValidationErrors.Add("GoalExistance", "No such goal does exist!");
                return GoalDetail;
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("ExceptionGetGoal", "Error occured while Fetching Goal Details!");
                return GoalDetail;
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
                this.ValidationErrors.Add("GoalExistance", "No such goal does exist!");
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
