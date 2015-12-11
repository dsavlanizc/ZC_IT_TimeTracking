using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.BusinessEntities.Model
{
    public class GenericModel
    {
        #region Goal
        public class GoalMaster
        {
            
            public GoalMaster()
            {
                this.GoalRules = new HashSet<GoalRules>();
                this.ResourceGoal = new HashSet<ResourceGoal>();
            }

            public Int32 Goal_MasterID { get; set; }
            public string GoalTitle { get; set; }
            public string GoalDescription { get; set; }
            public string UnitOfMeasurement { get; set; }
            public double MeasurementValue { get; set; }
            public DateTime Creation_Date { get; set; }
            public bool IsHigherValueGood { get; set; }
            public Int32 QuarterId { get; set; }

            public GoalQuarters GoalQuarter { get; set; }
            
            public ICollection<GoalRules> GoalRules { get; set; }
            
            public ICollection<ResourceGoal> ResourceGoal { get; set; }
        }

        public class Goaldetail
        {
            public string GoalTitle { get; set; }
            public string GoalDescription { get; set; }
            public string UnitOfMeasurement { get; set; }
            public double MeasurementValue { get; set; }
            public DateTime Creation_Date { get; set; }
            public bool IsHigherValueGood { get; set; }
            public Int32 QuarterId { get; set; }
        }

        public class SerarchGoalByTitle
        {
            public Int32 Goal_MasterID { get; set; }
            public string GoalTitle { get; set; }
            public string GoalDescription { get; set; }
            public Int32 GoalQuarter { get; set; }
            public Int32 QuarterYear { get; set; }
            public string UnitOfMeasurement { get; set; }
            public double MeasurementValue { get; set; }
            public DateTime Creation_Date { get; set; }
            public bool IsHigherValueGood { get; set; }
        }

        public class SpecificRecorsOfGoal
        {
            public Int32 Goal_MasterID { get; set; }
            public string GoalTitle { get; set; }
            public string GoalDescription { get; set; }
            public Int32 GoalQuarter { get; set; }
            public Int32 QuarterYear { get; set; }
            public string UnitOfMeasurement { get; set; }
            public double MeasurementValue { get; set; }
            public DateTime Creation_Date { get; set; }
            public bool IsHigherValueGood { get; set; }
        }

        public class GetGoalDescription
        {
            public string GoalDescription { get; set; }
        }
        #endregion

        #region Quarter
        public class GoalQuarters
        {
            
            public GoalQuarters()
            {
                this.GoalMaster = new HashSet<GoalMaster>();
                this.ResourcePerformance = new HashSet<ResourcePerformance>();
            }

            public Int32 QuarterID { get; set; }
            public Int32 GoalQuarter { get; set; }
            public Int32 QuarterYear { get; set; }
            public DateTime GoalCreateFrom { get; set; }
            public DateTime GoalCreateTo { get; set; }

            
            public ICollection<GoalMaster> GoalMaster { get; set; }
            
            public ICollection<ResourcePerformance> ResourcePerformance { get; set; }
        }

        public class QuarterFromYears
        {
            public Int32 QuarterYear { get; set; }
            public DateTime GoalCreateFrom { get; set; }
            public DateTime GoalCreateTo { get; set; }
        }

        public class GoalQuarterDetail
        {
            public Int32 GoalQuarter { get; set; }
            public Int32 QuarterYear { get; set; }
            public DateTime GoalCreateFrom { get; set; }
            public DateTime GoalCreateTo { get; set; }
        }

        public class QuarterFromYear
        {
            public Int32 QuarterID { get; set; }
            public Int32 GoalQuarter { get; set; }
            public DateTime GoalCreateFrom { get; set; }
            public DateTime GoalCreateTo { get; set; }
        }

        public class CheckQuater
        {
            public Int32 QuarterID { get; set; }
            public DateTime GoalCreateFrom { get; set; }
            public DateTime GoalCreateTo { get; set; }
        }
        #endregion

        #region Resource
        public class Resource
        {
            
            public Resource()
            {
                this.Managers = new HashSet<Manager>();
                this.ResourceGoal = new HashSet<ResourceGoal>();
                this.ResourcePerformance = new HashSet<ResourcePerformance>();
                this.TeamLeads = new HashSet<TeamLead>();
            }

            public Int32 ResourceID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Nullable<Int32> RoleID { get; set; }
            public Nullable<Int32> TeamID { get; set; }
            public string EMailID { get; set; }
            public string UserName { get; set; }

            
            public ICollection<Manager> Managers { get; set; }
            
            public ICollection<ResourceGoal> ResourceGoal { get; set; }
            
            public ICollection<ResourcePerformance> ResourcePerformance { get; set; }
            public Role Role { get; set; }
            public Team Team { get; set; }
            
            public ICollection<TeamLead> TeamLeads { get; set; }
        }
        
        public class ResourceGoal
        {
            
            public ResourceGoal()
            {
                this.ResourceGoalPerformance = new HashSet<ResourceGoalPerformance>();
            }

            public Int32 Resource_GoalID { get; set; }
            public Int32 ResourceID { get; set; }
            public Int32 Goal_MasterID { get; set; }
            public Int32 Weight { get; set; }
            public DateTime GoalAssignDate { get; set; }

            public GoalMaster GoalMaster { get; set; }
            public Resource Resource { get; set; }
            
            public ICollection<ResourceGoalPerformance> ResourceGoalPerformance { get; set; }
        }

        public class ResourcePerformance
        {
            public Int32 Resource_PerformanceID { get; set; }
            public Int32 ResourceID { get; set; }
            public Int32 QuaterID { get; set; }
            public double Resource_Performance1 { get; set; }

            public GoalQuarters GoalQuarter { get; set; }
            public Resource Resource { get; set; }
        }

        public class ResourceDetail
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Nullable<Int32> TeamID { get; set; }
            public Nullable<Int32> RoleID { get; set; }
        }

        public class ResourceByTeam
        {
            public Int32 ResourceID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Nullable<Int32> RoleID { get; set; }
        }

        public class ResourceGoalDetail
        {
            public Int32 Resource_GoalID { get; set; }
            public DateTime GoalAssignDate { get; set; }
            public Int32 Weight { get; set; }
        }

        public class AllGoalOfResource
        {
            public Int32 Resource_GoalID { get; set; }
            public Int32 Goal_MasterID { get; set; }
            public string GoalTitle { get; set; }
            public string Name { get; set; }
            public Int32 Weight { get; set; }
        }

        public class AllResourceOfGoal
        {
            public Int32 Resource_GoalID { get; set; }
            public Int32 ResourceID { get; set; }
            public Int32 Goal_MasterID { get; set; }
            public Int32 Weight { get; set; }
        }

        public class AssignGoalDetail
        {
            public Int32 Weight { get; set; }
            public Int32 ResourceID { get; set; }
            public Int32 Goal_MasterID { get; set; }
        }

        public class ResourceGoalPerformance
        {
            public Int32 Resource_Goal_PerformanceID { get; set; }
            public double Resource_Performance { get; set; }
            public Nullable<double> Resource_Rating { get; set; }
            public Int32 Resource_GoalId { get; set; }

            public ResourceGoal ResourceGoal { get; set; }
        }
        #endregion

        #region GoalRule
        public class GoalRules
        {
            public Int32 Goal_RuleID { get; set; }
            public double Performance_RangeFrom { get; set; }
            public double Performance_RangeTo { get; set; }
            public double Rating { get; set; }
            public Int32 GoalId { get; set; }

            public GoalMaster GoalMaster { get; set; }
        }        

        public class GoalRuleDetail
        {
            public Int32 Goal_RuleID { get; set; }
            public double Performance_RangeFrom { get; set; }
            public double Performance_RangeTo { get; set; }
            public double Rating { get; set; }
        }
        #endregion                

        #region Team
        public class Team
        {

            public Team()
            {
                this.Managers = new HashSet<Manager>();
                this.Resources = new HashSet<Resource>();
                this.TeamLeads = new HashSet<TeamLead>();
            }

            public Int32 TeamID { get; set; }
            public string TeamName { get; set; }
            public Int32 DepartmentID { get; set; }

            public Department Department { get; set; }

            public ICollection<Manager> Managers { get; set; }

            public ICollection<Resource> Resources { get; set; }

            public ICollection<TeamLead> TeamLeads { get; set; }
        }

        public class TeamLead
        {
            public Int32 TeamLeadID { get; set; }
            public Nullable<Int32> TeamID { get; set; }
            public Int32 TeamLeadResourceID { get; set; }

            public Resource Resource { get; set; }
            public Team Team { get; set; }
        }

        public class TeamDetail
        {
            public string TeamName { get; set; }
            public string Department_Name { get; set; }
            public Int32 ManagerId { get; set; }
            public Int32 TeamLeadID { get; set; }
            public Nullable<Int32> ManagerResourceID { get; set; }
            public Int32 TeamLeadResourceID { get; set; }
        }
        #endregion

        public class Department
        {

            public Department()
            {
                this.Teams = new HashSet<Team>();
            }

            public Int32 DepartmentID { get; set; }
            public string Department_Name { get; set; }


            public ICollection<Team> Teams { get; set; }
        }

        public class Manager
        {
            public Int32 ManagerId { get; set; }
            public Nullable<Int32> ManagerResourceID { get; set; }
            public Nullable<Int32> TeamID { get; set; }

            public Team Team { get; set; }
            public Resource Resource { get; set; }
        }

        public class Role
        {

            public Role()
            {
                this.Resources = new HashSet<Resource>();
            }

            public Int32 RoleID { get; set; }
            public string RoleName { get; set; }


            public ICollection<Resource> Resources { get; set; }
        }
    }

    

}
