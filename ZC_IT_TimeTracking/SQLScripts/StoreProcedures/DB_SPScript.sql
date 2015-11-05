USE [master]
GO
/****** Object:  Database [IT-Tracking]    Script Date: 3/11/2015 10:50:37 AM ******/
CREATE DATABASE [IT-Tracking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IT-Tracking', FILENAME = N'C:\Users\dsavlani\IT-Tracking.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'IT-Tracking_log', FILENAME = N'C:\Users\dsavlani\IT-Tracking_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [IT-Tracking] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IT-Tracking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IT-Tracking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IT-Tracking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IT-Tracking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IT-Tracking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IT-Tracking] SET ARITHABORT OFF 
GO
ALTER DATABASE [IT-Tracking] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [IT-Tracking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IT-Tracking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IT-Tracking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IT-Tracking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IT-Tracking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IT-Tracking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IT-Tracking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IT-Tracking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IT-Tracking] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IT-Tracking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IT-Tracking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IT-Tracking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IT-Tracking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IT-Tracking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IT-Tracking] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IT-Tracking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IT-Tracking] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [IT-Tracking] SET  MULTI_USER 
GO
ALTER DATABASE [IT-Tracking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IT-Tracking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IT-Tracking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IT-Tracking] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [IT-Tracking]
GO
/****** Object:  StoredProcedure [dbo].[AddResource]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddResource]
	@FirstName varchar(255),
	@LastName varchar(255),
	@RoleId int,
	@TeamId int
AS
BEGIN
INSERT INTO [dbo].[Resource]
           ([FirstName]
           ,[LastName]
           ,[RoleID]
           ,[TeamID])
     VALUES
           (@FirstName
           ,@LastName
           ,@RoleId
           ,@TeamId)

END


GO
/****** Object:  StoredProcedure [dbo].[AssignGoalToResource]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AssignGoalToResource]
@ResourceId int,
@GoalId int,
@weight int,
@CurrentInsertedId int OUTPUT
AS
BEGIN
	INSERT INTO [dbo].[Resource_Goal]
           ([ResourceID]
           ,[Goal_MasterID]
           ,[Weight])
     VALUES
           (@ResourceId
           ,@GoalId
           ,@weight)
		   SET @CurrentInsertedId = SCOPE_IDENTITY();
END


GO
/****** Object:  StoredProcedure [dbo].[CheckQuater]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CheckQuater] 
	
	@Quater int  , 
	@QYear int 

AS
BEGIN
SET NOCOUNT ON;

 select  QuarterID,GoalCreateFrom,GoalCreateTo  FROM dbo.Goal_Quarter 
	where 
	GoalQuarter = @Quater AND  QuarterYear=@QYear

END



GO
/****** Object:  StoredProcedure [dbo].[Delete_AllRulesOfGoal]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Delete_AllRulesOfGoal]
	@goalID int
AS
BEGIN
	DELETE FROM [dbo].[Goal_Rules]
      WHERE [GoalId]=@goalID
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteGoalMaster]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteGoalMaster] 

	@GoalId int
	
AS
BEGIN
SET NOCOUNT ON;
DECLARE @resourceGoalID int	

EXEC Delete_AllRulesOfGoal @goalId

SELECT @resourceGoalID= [Resource_GoalID]    
  FROM [dbo].[Resource_Goal]
  WHERE @GoalId=[Goal_MasterID]

EXEC DeleteResourceGoal @resourceGoalID

DELETE FROM [dbo].[Goal_Master]
      WHERE @GoalId=[Goal_MasterID]
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteGoalRule]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteGoalRule]
	@GoalRuleId int
AS
BEGIN
	
DELETE FROM [dbo].[Goal_Rules]
      WHERE [Goal_RuleID]=@GoalRuleId

END


GO
/****** Object:  StoredProcedure [dbo].[DeleteResourceGoal]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteResourceGoal] 
	@resource_GoalId int
AS
BEGIN

DELETE FROM [dbo].[Resource_Goal_Performance]
      WHERE [Resource_GoalId]=@resource_GoalId

DELETE FROM [dbo].[Resource_Goal]
      WHERE [Resource_GoalID]=@resource_GoalId

END


GO
/****** Object:  StoredProcedure [dbo].[GetAllGoalsOfResource]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllGoalsOfResource] 
	@ResourceId int 
AS
BEGIN

SELECT [Resource_GoalID]
      ,[ResourceID]
      ,[Goal_MasterID]
      ,[Weight]
  FROM [dbo].[Resource_Goal]
  WHERE [ResourceID]=@ResourceId

END


GO
/****** Object:  StoredProcedure [dbo].[GetAllResourceForGoal]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllResourceForGoal] 
	@Goal_id int 
AS
BEGIN
	SELECT [Resource_GoalID]
      ,[ResourceID]
      ,[Goal_MasterID]
      ,[Weight]
  FROM [dbo].[Resource_Goal]
  WHERE [Goal_MasterID]=@Goal_id
END


GO
/****** Object:  StoredProcedure [dbo].[GetGoalDetails]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetGoalDetails] 
	@Goal_Id int
	
AS
BEGIN
	
SET NOCOUNT ON;

SELECT 
      [GoalTitle]
      ,[GoalDescription]
      ,[UnitOfMeasurement]
      ,[MeasurementValue]
      ,[Creation_Date]
      ,[IsHigherValueGood]
      ,[QuarterId]
  FROM [dbo].[Goal_Master]
  WHERE @Goal_Id=[Goal_MasterID]
END



GO
/****** Object:  StoredProcedure [dbo].[GetGoalRuleDetails]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGoalRuleDetails]
	-- Add the parameters for the stored procedure here
	@GoalId int

AS
BEGIN	
	
	SELECT [Goal_RuleID],[Performance_RangeFrom] ,[Performance_RangeTo] ,[Rating] 
	FROM Goal_Rules 
	WHERE @GoalID=[GoalID]
	
END


GO
/****** Object:  StoredProcedure [dbo].[GetQuarterDetails]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetQuarterDetails]
	@QuaterId int
AS
BEGIN
SELECT [GoalQuarter]
      ,[QuarterYear]
      ,[GoalCreateFrom]
      ,[GoalCreateTo]
  FROM [dbo].[Goal_Quarter]
  where QuarterID=@QuaterId
	
END


GO
/****** Object:  StoredProcedure [dbo].[GetResouceDetails]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetResouceDetails] 
	@resourceID int
AS
BEGIN
	SELECT	FirstName,
			LastName,
			TeamID,
			RoleID
	FROM [dbo].[Resource]
	WHERE ResourceID=@resourceID
END


GO
/****** Object:  StoredProcedure [dbo].[GetResourceByTeam]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetResourceByTeam] 
	@TeamId int 
AS
BEGIN
	SELECT ResourceID , 
			FirstName ,
			LastName,
			RoleID
	FROM Resource
	where TeamID=@TeamId
END


GO
/****** Object:  StoredProcedure [dbo].[GetTeamDetails]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTeamDetails] 
	@teamId int
AS
BEGIN
  SELECT team.TeamName ,
         department.Department_Name,
		 manager.ManagerId , 
		 teamLead.TeamLeadID ,
		 manager.ManagerResourceID as ManagerResourceID ,
		 teamLead.TeamLeadResourceID as TeamLeadResourceID 
	FROM Team team, 
		Manager manager ,
		TeamLead teamLead ,
		Resource teamResource,
		Department department
	WHERE team.TeamID=manager.TeamID
		and team.TeamID=teamLead.TeamID
		and team.DepartmentID=department.DepartmentID
		and manager.ManagerResourceID=teamResource.ResourceID
		and teamLead.TeamLeadResourceID= teamResource.ResourceID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertGoalMaster]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertGoalMaster] 
	@Goal_Title varchar(150), 
    @Goal_Description varchar(Max),
	@Unit_Of_Measurement varchar(20),
	@Measurement_Value float,
	@Is_HigherValueGood bit,
	@CreationDate date,
	@QuarterID int,
	@CurrentInsertedId int OUTPUT
AS
BEGIN

INSERT INTO [dbo].[Goal_Master]
           ([GoalTitle]
           ,[GoalDescription]
           ,[UnitOfMeasurement]
           ,[MeasurementValue]
           ,[Creation_Date]
           ,[IsHigherValueGood]
           ,[QuarterId])
     VALUES
           (@Goal_Title
           ,@Goal_Description
           ,@Unit_Of_Measurement
           ,@Measurement_Value
           ,@CreationDate
           ,@Is_HigherValueGood
           ,@QuarterID)
SET @CurrentInsertedId = SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[InsertGoalQuarter]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[InsertGoalQuarter]

	@Quater int, 
	@Year INT,
	@GoalCreate_From date,
	@GoalCreate_To date

AS
BEGIN
	INSERT INTO [dbo].[Goal_Quarter]
           ([GoalQuarter]
           ,[QuarterYear]
           ,[GoalCreateFrom]
           ,[GoalCreateTo])
     VALUES
           ( @Quater,@Year,@GoalCreate_From,@GoalCreate_To)
         
END


GO
/****** Object:  StoredProcedure [dbo].[InsertGoalRules]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertGoalRules] 
	
	@PerformanceRangeFrom int , 
	@PerformanceRangeTo int ,
	@Rating float,
	@GoalID int
AS
BEGIN
	
	SET NOCOUNT ON
	INSERT INTO [dbo].[Goal_Rules]
           ([Performance_RangeFrom]
           ,[Performance_RangeTo]
           ,[Rating]
           ,[GoalId])
     VALUES
           (@PerformanceRangeFrom
           ,@PerformanceRangeTo
           ,@Rating
           ,@GoalID)

END



GO
/****** Object:  StoredProcedure [dbo].[UpdateGoalMaster]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateGoalMaster] 
	@Goal_Id int,
	@Goal_Title varchar(150), 
    @Goal_Description varchar(Max),
	@Unit_Of_Measurement varchar(20),
	@Measurement_Value float,
	@CreateDate date,
	@Is_HigherValueGood bit,
	@QuaterID int

AS
BEGIN
	
	SET NOCOUNT ON;
	
UPDATE [dbo].[Goal_Master]
   SET [GoalTitle] = @Goal_Title
      ,[GoalDescription] = @Goal_Description
      ,[UnitOfMeasurement] = @Unit_Of_Measurement
	  ,[Creation_Date]=@CreateDate
      ,[MeasurementValue] = @Measurement_Value
      ,[IsHigherValueGood] = @Is_HigherValueGood
      ,[QuarterId] = @QuaterID
 WHERE [Goal_MasterID]=@Goal_Id

END



GO
/****** Object:  StoredProcedure [dbo].[UpdateGoalRules]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[UpdateGoalRules] 
	
	@PerformanceRangeFrom int , 
	@PerformanceRangeTo int ,
	@Rating float,
	@GoalID int
AS
BEGIN
	
	SET NOCOUNT ON;
UPDATE [dbo].[Goal_Rules]
   SET [Performance_RangeFrom] = @PerformanceRangeFrom
      ,[Performance_RangeTo] = @PerformanceRangeTo
      ,[Rating] = @Rating

 WHERE [GoalID] = @GoalID
 END




GO
/****** Object:  StoredProcedure [dbo].[UpdateResourceDetails]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateResourceDetails]
	@resourceId int ,
	@firstName varchar(255),
	@lastName varchar(255),
	@roleId int ,
	@teamID int
AS
BEGIN
	UPDATE [dbo].[Resource]
   SET [FirstName] = @firstName
      ,[LastName] = @lastName
      ,[RoleID] = @roleId
      ,[TeamID] = @teamID
 WHERE [ResourceID]=@resourceId

END


GO
/****** Object:  StoredProcedure [dbo].[UpdateResourceGoal]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateResourceGoal] 
	@ResourceId int,
	@GoalId int ,
	@weight int
AS
BEGIN
UPDATE [dbo].[Resource_Goal]
   SET
      [Weight] =@weight
 WHERE [ResourceID]=@ResourceId 
		AND
		[Goal_MasterID]=@GoalId
		
END


GO

USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[GetRating]    Script Date: 2015-11-03 16:52:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetRating] 
@goalId int ,
@performance float,
@rating float OUTPUT

AS
BEGIN
SET NOCOUNT ON;
SET @rating=(select Rating
			from Goal_Rules
			where GoalId=@goalId and 
				@performance>=Performance_RangeFrom and
				@performance<=Performance_RangeTo)		 			  
END

GO

USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[calculateResourceGoalRating]    Script Date: 2015-11-03 17:21:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[calculateResourceGoalRating]
	@ResourceId int ,
	@GoalId int ,
	@ResourcePerformance float
AS
BEGIN
DECLARE @resourceGoalId int
DECLARE @rating float
DECLARE @calcPerformance float 
DECLARE @variation float
DECLARE @higherValGood bit
DECLARE @measurementVal float 

	SELECT  @higherValGood= IsHigherValueGood ,
			@measurementVal=MeasurementValue
		FROM Goal_Master
		WHERE Goal_MasterID=@GoalId

IF @higherValGood=1
		SET @variation=(@ResourcePerformance-@measurementVal)
ELSE
		SET @variation= (@measurementVal-@ResourcePerformance)

IF @variation>=0
	SET @calcPerformance=100
ELSE
	SET @calcPerformance=CEILING(100.0 + ((100.0 * @variation) / @measurementVal))

 exec [dbo].[GetRating] @goalId,@calcPerformance, @rating out

SET @resourceGoalId=(SELECT Resource_GoalID 
					FROM Resource_Goal
					WHERE ResourceID=@ResourceId
					and Goal_MasterID=@GoalId)

INSERT INTO [dbo].[Resource_Goal_Performance]
           ([Resource_Performance]
           ,[Resource_Rating]
           ,[Resource_GoalId])
     VALUES
           ( @ResourcePerformance
           ,@rating
           ,@resourceGoalId)

END

GO


