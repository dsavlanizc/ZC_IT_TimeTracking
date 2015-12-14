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

        public List<GetResourceByTeam_Result> GetResourceByTeam(int teamId)
        {
            try
            {
                var data = DbContext.GetResourceByTeam(teamId).ToList();
                if (data.Count != 0)
                {
                    return data;
                }
                else
                {
                    this.ValidationErrors.Add("NO_TEAM_EXIST", "No Any Team Member Exist");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching record");
                return null;
            }
        }

        //public List<ResourcesByTeam> GetResourceByTeam(int teamId)
        //{
        //    var lst = new List<ResourcesByTeam>();
        //    try
        //    {
        //        var data = DbContext.GetResourceByTeam(teamId).Select(s => new { s.ResourceID, Name = s.FirstName + " " + s.LastName }).ToList();
        //        Mapper.CreateMap<GetResourceByTeam_Result, ResourcesByTeam>();
        //        lst = Mapper.Map<List<ResourcesByTeam>>(data);
        //        return lst;
        //    }
        //    catch
        //    {
        //        this.ValidationErrors.Add("NO_TEAM_EXIST", "Error While fetching record");
        //        return null;
        //    }
        //}

        public List<Team> GetTeam()
        {
            try
            {
                var team = DbContext.Teams.ToList();
                return team;
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching record");
                return null;
            }
        }
        //ResourceModel
        public List<GetAllGoalsOfResource_Result> GetAllGoalsOfResource(int ResourceId)
        {
            try
            {
                var AGOR = DbContext.GetAllGoalsOfResource(ResourceId).ToList();
                if(AGOR.Count != 0)
                    return AGOR;
                else
                {
                    this.ValidationErrors.Add("NO_RES_GOAL_EXIST", "Resource Goal is Not exist!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Resource Goal");
                return null;
            }
        }

        //Completed
        public List<GetAssignedGoalDetails_Result> GetAssignedGoalDetails(int AssignGoalId)
        {
            try
            {
                var GAGDR = DbContext.GetAssignedGoalDetails(AssignGoalId).ToList();
                if (GAGDR.Count != 0)
                    return GAGDR;
                else
                {
                    this.ValidationErrors.Add("NO_ASGN_GOAL", "No Assigned Goal Record found");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Assigned Goal Record");
                return null;
            }
        }
        //Completed
        public List<GetResourceGoalDetails_Result> GetResourceGoalDetails(int Resourceid, int GoalId)
        {
            try
            {
                var ResourceGoal =DbContext.GetResourceGoalDetails(Resourceid, GoalId).ToList();
                if (ResourceGoal.Count != 0)
                    return ResourceGoal;
                else
                {
                    this.ValidationErrors.Add("NO_RES_GOAL", "No Resouce Goal Detail Available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Resouce Goal Record");
                return null;
            }
        }

        public bool AssignGoal(AssignGoal AssignData)
        {
            try
            {
                int count = 0;
                bool MissingRes = false;
                foreach (int id in AssignData.ResourceID)
                {
                    var v = GetResourceGoalDetails(id, AssignData.Goal_MasterID);
                    var ResGoal = GetAllGoalsOfResource(id);
                    int TotalWeight = 0;
                    if (ResGoal != null) 
                    {
                        TotalWeight = ResGoal.Sum(s => s.Weight) + AssignData.Weight;
                    }
                    if (v == null)
                    {
                        if (TotalWeight >= 100)
                        {
                            MissingRes = true;
                            count++;
                        }
                        else
                        {
                            ObjectParameter insertedId = new ObjectParameter("CurrentInsertedId", typeof(int));
                            var AssignGoal = DbContext.AssignGoalToResource(id, AssignData.Goal_MasterID, AssignData.Weight, DateTime.Now.Date, insertedId);
                            count++;
                        }                        
                    }
                }
                if (count == AssignData.ResourceID.Count())
                {
                    if(MissingRes)
                    {
                        this.ClearValidationErrors();
                        this.ValidationErrors.Add("ERR_GRT_TRGT", "Some Resouce weight is greter than 100 !");
                        return false;
                    }
                    return true;
                }
                else
                {
                    this.ClearValidationErrors();
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

        public List<AssignGoal> ViewAssignGoalToResource(int ResourceId)
        {
            var GoalDetail = new List<AssignGoal>();
            try
            {
                var AssignedGoals = GetAllGoalsOfResource(ResourceId);
                if (AssignedGoals.Count()>0)
                {
                    Mapper.CreateMap<GetAllGoalsOfResource_Result, AssignGoal>();
                    GoalDetail = Mapper.Map<List<AssignGoal>>(AssignedGoals);
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
                    foreach (var i in ResGoal)
                        GoalDetails.Add(i);
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
