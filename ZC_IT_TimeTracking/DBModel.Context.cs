﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZC_IT_TimeTracking
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DatabaseEntities : DbContext
    {
        public DatabaseEntities()
            : base("name=DatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Goal_Master> Goal_Master { get; set; }
        public virtual DbSet<Goal_Quarter> Goal_Quarter { get; set; }
        public virtual DbSet<Goal_Rules> Goal_Rules { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Resource_Goal> Resource_Goal { get; set; }
        public virtual DbSet<Resource_Goal_Performance> Resource_Goal_Performance { get; set; }
        public virtual DbSet<Resource_Performance> Resource_Performance { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamLead> TeamLeads { get; set; }
    
        public virtual int AddResource(string firstName, string lastName, Nullable<int> roleId, Nullable<int> teamId)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(int));
    
            var teamIdParameter = teamId.HasValue ?
                new ObjectParameter("TeamId", teamId) :
                new ObjectParameter("TeamId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddResource", firstNameParameter, lastNameParameter, roleIdParameter, teamIdParameter);
        }
    
        public virtual int AssignGoalToResource(Nullable<int> resourceId, Nullable<int> goalId, Nullable<int> weight, ObjectParameter currentInsertedId)
        {
            var resourceIdParameter = resourceId.HasValue ?
                new ObjectParameter("ResourceId", resourceId) :
                new ObjectParameter("ResourceId", typeof(int));
    
            var goalIdParameter = goalId.HasValue ?
                new ObjectParameter("GoalId", goalId) :
                new ObjectParameter("GoalId", typeof(int));
    
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AssignGoalToResource", resourceIdParameter, goalIdParameter, weightParameter, currentInsertedId);
        }
    
        public virtual int calculateResourceGoalRating(Nullable<int> resourceId, Nullable<int> goalId, Nullable<double> resourcePerformance)
        {
            var resourceIdParameter = resourceId.HasValue ?
                new ObjectParameter("ResourceId", resourceId) :
                new ObjectParameter("ResourceId", typeof(int));
    
            var goalIdParameter = goalId.HasValue ?
                new ObjectParameter("GoalId", goalId) :
                new ObjectParameter("GoalId", typeof(int));
    
            var resourcePerformanceParameter = resourcePerformance.HasValue ?
                new ObjectParameter("ResourcePerformance", resourcePerformance) :
                new ObjectParameter("ResourcePerformance", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("calculateResourceGoalRating", resourceIdParameter, goalIdParameter, resourcePerformanceParameter);
        }
    
        public virtual ObjectResult<CheckQuater_Result> CheckQuater(Nullable<int> quater, Nullable<int> qYear)
        {
            var quaterParameter = quater.HasValue ?
                new ObjectParameter("Quater", quater) :
                new ObjectParameter("Quater", typeof(int));
    
            var qYearParameter = qYear.HasValue ?
                new ObjectParameter("QYear", qYear) :
                new ObjectParameter("QYear", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CheckQuater_Result>("CheckQuater", quaterParameter, qYearParameter);
        }
    
        public virtual int Delete_AllRulesOfGoal(Nullable<int> goalID)
        {
            var goalIDParameter = goalID.HasValue ?
                new ObjectParameter("goalID", goalID) :
                new ObjectParameter("goalID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_AllRulesOfGoal", goalIDParameter);
        }
    
        public virtual int DeleteGoalMaster(Nullable<int> goalId)
        {
            var goalIdParameter = goalId.HasValue ?
                new ObjectParameter("GoalId", goalId) :
                new ObjectParameter("GoalId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteGoalMaster", goalIdParameter);
        }
    
        public virtual int DeleteGoalRule(Nullable<int> goalRuleId)
        {
            var goalRuleIdParameter = goalRuleId.HasValue ?
                new ObjectParameter("GoalRuleId", goalRuleId) :
                new ObjectParameter("GoalRuleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteGoalRule", goalRuleIdParameter);
        }
    
        public virtual int DeleteResourceGoal(Nullable<int> resource_GoalId)
        {
            var resource_GoalIdParameter = resource_GoalId.HasValue ?
                new ObjectParameter("resource_GoalId", resource_GoalId) :
                new ObjectParameter("resource_GoalId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteResourceGoal", resource_GoalIdParameter);
        }
    
        public virtual ObjectResult<GetAllGoalsOfResource_Result> GetAllGoalsOfResource(Nullable<int> resourceId)
        {
            var resourceIdParameter = resourceId.HasValue ?
                new ObjectParameter("ResourceId", resourceId) :
                new ObjectParameter("ResourceId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllGoalsOfResource_Result>("GetAllGoalsOfResource", resourceIdParameter);
        }
    
        public virtual ObjectResult<GetAllResourceForGoal_Result> GetAllResourceForGoal(Nullable<int> goal_id)
        {
            var goal_idParameter = goal_id.HasValue ?
                new ObjectParameter("Goal_id", goal_id) :
                new ObjectParameter("Goal_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllResourceForGoal_Result>("GetAllResourceForGoal", goal_idParameter);
        }
    
        public virtual ObjectResult<GetGoalDetails_Result> GetGoalDetails(Nullable<int> goal_Id)
        {
            var goal_IdParameter = goal_Id.HasValue ?
                new ObjectParameter("Goal_Id", goal_Id) :
                new ObjectParameter("Goal_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetGoalDetails_Result>("GetGoalDetails", goal_IdParameter);
        }
    
        public virtual ObjectResult<GetGoalRuleDetails_Result> GetGoalRuleDetails(Nullable<int> goalId)
        {
            var goalIdParameter = goalId.HasValue ?
                new ObjectParameter("GoalId", goalId) :
                new ObjectParameter("GoalId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetGoalRuleDetails_Result>("GetGoalRuleDetails", goalIdParameter);
        }
    
        public virtual ObjectResult<GetQuarterDetails_Result> GetQuarterDetails(Nullable<int> quaterId)
        {
            var quaterIdParameter = quaterId.HasValue ?
                new ObjectParameter("QuaterId", quaterId) :
                new ObjectParameter("QuaterId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetQuarterDetails_Result>("GetQuarterDetails", quaterIdParameter);
        }
    
        public virtual int GetRating(Nullable<int> goalId, Nullable<double> performance, ObjectParameter rating)
        {
            var goalIdParameter = goalId.HasValue ?
                new ObjectParameter("GoalId", goalId) :
                new ObjectParameter("GoalId", typeof(int));
    
            var performanceParameter = performance.HasValue ?
                new ObjectParameter("Performance", performance) :
                new ObjectParameter("Performance", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetRating", goalIdParameter, performanceParameter, rating);
        }
    
        public virtual ObjectResult<GetResouceDetails_Result> GetResouceDetails(Nullable<int> resourceID)
        {
            var resourceIDParameter = resourceID.HasValue ?
                new ObjectParameter("resourceID", resourceID) :
                new ObjectParameter("resourceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetResouceDetails_Result>("GetResouceDetails", resourceIDParameter);
        }
    
        public virtual ObjectResult<GetResourceByTeam_Result> GetResourceByTeam(Nullable<int> teamId)
        {
            var teamIdParameter = teamId.HasValue ?
                new ObjectParameter("TeamId", teamId) :
                new ObjectParameter("TeamId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetResourceByTeam_Result>("GetResourceByTeam", teamIdParameter);
        }
    
        public virtual ObjectResult<GetResourceGoalDetails_Result> GetResourceGoalDetails(Nullable<int> resourceId, Nullable<int> goalId)
        {
            var resourceIdParameter = resourceId.HasValue ?
                new ObjectParameter("resourceId", resourceId) :
                new ObjectParameter("resourceId", typeof(int));
    
            var goalIdParameter = goalId.HasValue ?
                new ObjectParameter("GoalId", goalId) :
                new ObjectParameter("GoalId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetResourceGoalDetails_Result>("GetResourceGoalDetails", resourceIdParameter, goalIdParameter);
        }
    
        public virtual ObjectResult<GetTeamDetails_Result> GetTeamDetails(Nullable<int> teamId)
        {
            var teamIdParameter = teamId.HasValue ?
                new ObjectParameter("teamId", teamId) :
                new ObjectParameter("teamId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTeamDetails_Result>("GetTeamDetails", teamIdParameter);
        }
    
        public virtual int InsertGoalMaster(string goal_Title, string goal_Description, string unit_Of_Measurement, Nullable<double> measurement_Value, Nullable<bool> is_HigherValueGood, Nullable<System.DateTime> creationDate, Nullable<int> quarterID, ObjectParameter currentInsertedId)
        {
            var goal_TitleParameter = goal_Title != null ?
                new ObjectParameter("Goal_Title", goal_Title) :
                new ObjectParameter("Goal_Title", typeof(string));
    
            var goal_DescriptionParameter = goal_Description != null ?
                new ObjectParameter("Goal_Description", goal_Description) :
                new ObjectParameter("Goal_Description", typeof(string));
    
            var unit_Of_MeasurementParameter = unit_Of_Measurement != null ?
                new ObjectParameter("Unit_Of_Measurement", unit_Of_Measurement) :
                new ObjectParameter("Unit_Of_Measurement", typeof(string));
    
            var measurement_ValueParameter = measurement_Value.HasValue ?
                new ObjectParameter("Measurement_Value", measurement_Value) :
                new ObjectParameter("Measurement_Value", typeof(double));
    
            var is_HigherValueGoodParameter = is_HigherValueGood.HasValue ?
                new ObjectParameter("Is_HigherValueGood", is_HigherValueGood) :
                new ObjectParameter("Is_HigherValueGood", typeof(bool));
    
            var creationDateParameter = creationDate.HasValue ?
                new ObjectParameter("CreationDate", creationDate) :
                new ObjectParameter("CreationDate", typeof(System.DateTime));
    
            var quarterIDParameter = quarterID.HasValue ?
                new ObjectParameter("QuarterID", quarterID) :
                new ObjectParameter("QuarterID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertGoalMaster", goal_TitleParameter, goal_DescriptionParameter, unit_Of_MeasurementParameter, measurement_ValueParameter, is_HigherValueGoodParameter, creationDateParameter, quarterIDParameter, currentInsertedId);
        }
    
        public virtual int InsertGoalQuarter(Nullable<int> quater, Nullable<int> year, Nullable<System.DateTime> goalCreate_From, Nullable<System.DateTime> goalCreate_To)
        {
            var quaterParameter = quater.HasValue ?
                new ObjectParameter("Quater", quater) :
                new ObjectParameter("Quater", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var goalCreate_FromParameter = goalCreate_From.HasValue ?
                new ObjectParameter("GoalCreate_From", goalCreate_From) :
                new ObjectParameter("GoalCreate_From", typeof(System.DateTime));
    
            var goalCreate_ToParameter = goalCreate_To.HasValue ?
                new ObjectParameter("GoalCreate_To", goalCreate_To) :
                new ObjectParameter("GoalCreate_To", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertGoalQuarter", quaterParameter, yearParameter, goalCreate_FromParameter, goalCreate_ToParameter);
        }
    
        public virtual int InsertGoalRules(Nullable<int> performanceRangeFrom, Nullable<int> performanceRangeTo, Nullable<double> rating, Nullable<int> goalID)
        {
            var performanceRangeFromParameter = performanceRangeFrom.HasValue ?
                new ObjectParameter("PerformanceRangeFrom", performanceRangeFrom) :
                new ObjectParameter("PerformanceRangeFrom", typeof(int));
    
            var performanceRangeToParameter = performanceRangeTo.HasValue ?
                new ObjectParameter("PerformanceRangeTo", performanceRangeTo) :
                new ObjectParameter("PerformanceRangeTo", typeof(int));
    
            var ratingParameter = rating.HasValue ?
                new ObjectParameter("Rating", rating) :
                new ObjectParameter("Rating", typeof(double));
    
            var goalIDParameter = goalID.HasValue ?
                new ObjectParameter("GoalID", goalID) :
                new ObjectParameter("GoalID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertGoalRules", performanceRangeFromParameter, performanceRangeToParameter, ratingParameter, goalIDParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int UpdateGoalMaster(Nullable<int> goal_Id, string goal_Title, string goal_Description, string unit_Of_Measurement, Nullable<double> measurement_Value, Nullable<System.DateTime> createDate, Nullable<bool> is_HigherValueGood, Nullable<int> quaterID)
        {
            var goal_IdParameter = goal_Id.HasValue ?
                new ObjectParameter("Goal_Id", goal_Id) :
                new ObjectParameter("Goal_Id", typeof(int));
    
            var goal_TitleParameter = goal_Title != null ?
                new ObjectParameter("Goal_Title", goal_Title) :
                new ObjectParameter("Goal_Title", typeof(string));
    
            var goal_DescriptionParameter = goal_Description != null ?
                new ObjectParameter("Goal_Description", goal_Description) :
                new ObjectParameter("Goal_Description", typeof(string));
    
            var unit_Of_MeasurementParameter = unit_Of_Measurement != null ?
                new ObjectParameter("Unit_Of_Measurement", unit_Of_Measurement) :
                new ObjectParameter("Unit_Of_Measurement", typeof(string));
    
            var measurement_ValueParameter = measurement_Value.HasValue ?
                new ObjectParameter("Measurement_Value", measurement_Value) :
                new ObjectParameter("Measurement_Value", typeof(double));
    
            var createDateParameter = createDate.HasValue ?
                new ObjectParameter("CreateDate", createDate) :
                new ObjectParameter("CreateDate", typeof(System.DateTime));
    
            var is_HigherValueGoodParameter = is_HigherValueGood.HasValue ?
                new ObjectParameter("Is_HigherValueGood", is_HigherValueGood) :
                new ObjectParameter("Is_HigherValueGood", typeof(bool));
    
            var quaterIDParameter = quaterID.HasValue ?
                new ObjectParameter("QuaterID", quaterID) :
                new ObjectParameter("QuaterID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateGoalMaster", goal_IdParameter, goal_TitleParameter, goal_DescriptionParameter, unit_Of_MeasurementParameter, measurement_ValueParameter, createDateParameter, is_HigherValueGoodParameter, quaterIDParameter);
        }
    
        public virtual int UpdateGoalRules(Nullable<int> performanceRangeFrom, Nullable<int> performanceRangeTo, Nullable<double> rating, Nullable<int> goalID)
        {
            var performanceRangeFromParameter = performanceRangeFrom.HasValue ?
                new ObjectParameter("PerformanceRangeFrom", performanceRangeFrom) :
                new ObjectParameter("PerformanceRangeFrom", typeof(int));
    
            var performanceRangeToParameter = performanceRangeTo.HasValue ?
                new ObjectParameter("PerformanceRangeTo", performanceRangeTo) :
                new ObjectParameter("PerformanceRangeTo", typeof(int));
    
            var ratingParameter = rating.HasValue ?
                new ObjectParameter("Rating", rating) :
                new ObjectParameter("Rating", typeof(double));
    
            var goalIDParameter = goalID.HasValue ?
                new ObjectParameter("GoalID", goalID) :
                new ObjectParameter("GoalID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateGoalRules", performanceRangeFromParameter, performanceRangeToParameter, ratingParameter, goalIDParameter);
        }
    
        public virtual int UpdateResourceDetails(Nullable<int> resourceId, string firstName, string lastName, Nullable<int> roleId, Nullable<int> teamID)
        {
            var resourceIdParameter = resourceId.HasValue ?
                new ObjectParameter("resourceId", resourceId) :
                new ObjectParameter("resourceId", typeof(int));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("firstName", firstName) :
                new ObjectParameter("firstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("lastName", lastName) :
                new ObjectParameter("lastName", typeof(string));
    
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("roleId", roleId) :
                new ObjectParameter("roleId", typeof(int));
    
            var teamIDParameter = teamID.HasValue ?
                new ObjectParameter("teamID", teamID) :
                new ObjectParameter("teamID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateResourceDetails", resourceIdParameter, firstNameParameter, lastNameParameter, roleIdParameter, teamIDParameter);
        }
    
        public virtual int UpdateResourceGoal(Nullable<int> resourceId, Nullable<int> goalId, Nullable<int> weight)
        {
            var resourceIdParameter = resourceId.HasValue ?
                new ObjectParameter("ResourceId", resourceId) :
                new ObjectParameter("ResourceId", typeof(int));
    
            var goalIdParameter = goalId.HasValue ?
                new ObjectParameter("GoalId", goalId) :
                new ObjectParameter("GoalId", typeof(int));
    
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateResourceGoal", resourceIdParameter, goalIdParameter, weightParameter);
        }
    
        public virtual ObjectResult<getQuarterFormQuarterYear_Result> getQuarterFormQuarterYear(Nullable<int> quarter)
        {
            var quarterParameter = quarter.HasValue ?
                new ObjectParameter("quarter", quarter) :
                new ObjectParameter("quarter", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getQuarterFormQuarterYear_Result>("getQuarterFormQuarterYear", quarterParameter);
        }
    
        public virtual ObjectResult<GetQuarterFromYear_Result> GetQuarterFromYear(Nullable<int> qyear)
        {
            var qyearParameter = qyear.HasValue ?
                new ObjectParameter("qyear", qyear) :
                new ObjectParameter("qyear", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetQuarterFromYear_Result>("GetQuarterFromYear", qyearParameter);
        }
    }
}
