USE [master]
GO
/****** Object:  Database [IT-Tracking]    Script Date: 2015-11-05 17:26:35 ******/
CREATE DATABASE [IT-Tracking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IT-Tracking', FILENAME = N'C:\Users\tpanchal\IT-Tracking.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'IT-Tracking_log', FILENAME = N'C:\Users\tpanchal\IT-Tracking_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
ALTER DATABASE [IT-Tracking] SET AUTO_CREATE_STATISTICS ON 
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
/****** Object:  StoredProcedure [dbo].[AddResource]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
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
/****** Object:  StoredProcedure [dbo].[AssignGoalToResource]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
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
/****** Object:  StoredProcedure [dbo].[calculateResourceGoalRating]    Script Date: 2015-11-05 17:26:35 ******/
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
	SET @calcPerformance=cast((100.00 + ((100.00 * @variation) / @measurementVal)) as float)

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

SELECT @calcPerformance
END


GO
/****** Object:  StoredProcedure [dbo].[CheckQuater]    Script Date: 2015-11-05 17:26:35 ******/
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
/****** Object:  StoredProcedure [dbo].[Delete_AllRulesOfGoal]    Script Date: 2015-11-05 17:26:35 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteGoalMaster]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		trushna
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[DeleteGoalMaster] 
	-- Add the parameters for the stored procedure here
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
return @@rowcount
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteGoalRule]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteGoalRule]
	@GoalRuleId int
AS
BEGIN
	
DELETE FROM [dbo].[Goal_Rules]
      WHERE [Goal_RuleID]=@GoalRuleId

END


GO
/****** Object:  StoredProcedure [dbo].[DeleteResourceGoal]    Script Date: 2015-11-05 17:26:35 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllGoalsOfResource]    Script Date: 2015-11-05 17:26:35 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllResourceForGoal]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
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
/****** Object:  StoredProcedure [dbo].[GetGoalDetails]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Trushna
-- Description:	give all goal details
-- =============================================
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
/****** Object:  StoredProcedure [dbo].[GetGoalRuleDetails]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
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
/****** Object:  StoredProcedure [dbo].[GetQuarterDetails]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQuarterDetails]
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
/****** Object:  StoredProcedure [dbo].[GetQuarterFromYear]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[GetQuarterFromYear]
	@qyear int
AS
BEGIN
	SELECT QuarterID, GoalQuarter, GoalCreateFrom, GoalCreateTo
	FROM Goal_Quarter
	where QuarterYear = @qyear
END

GO
/****** Object:  StoredProcedure [dbo].[GetRating]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRating] 
@goalId int ,
@performance float,
@rating float OUT

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
/****** Object:  StoredProcedure [dbo].[GetResouceDetails]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
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
/****** Object:  StoredProcedure [dbo].[GetResourceByTeam]    Script Date: 2015-11-05 17:26:35 ******/
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
/****** Object:  StoredProcedure [dbo].[GetResourceGoalDetails]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetResourceGoalDetails]
	@resourceId int ,
	@GoalId int 
AS
BEGIN
DECLARE @weight int
SET NOCOUNT ON;
 SELECT  [Resource_GoalID]
		,[Weight]
  FROM [dbo].[Resource_Goal]
  WHERE ResourceID=@resourceId
		AND
		Goal_MasterID=@GoalId
END


GO
/****** Object:  StoredProcedure [dbo].[GetTeamDetails]    Script Date: 2015-11-05 17:26:35 ******/
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
/****** Object:  StoredProcedure [dbo].[getYearForQuarter]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getYearForQuarter]
	@quarter int
AS
BEGIN
	SELECT QuarterYear,GoalCreateFrom,GoalCreateTo
	FROM Goal_Quarter
	where GoalQuarter=@quarter
		  AND 
		  QuarterYear= YEAR(GETDATE())
END


GO
/****** Object:  StoredProcedure [dbo].[InsertGoalMaster]    Script Date: 2015-11-05 17:26:35 ******/
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
	@creationDate date,
	@QuarterID int,
	@CurrentInsertedId int OUTPUT
AS
BEGIN

INSERT INTO [dbo].[Goal_Master]
           ([GoalTitle]
           ,[GoalDescription]
           ,[UnitOfMeasurement]
           ,[MeasurementValue]
           ,[IsHigherValueGood]
		   ,[Creation_Date]
           ,[QuarterId])
     VALUES
           (@Goal_Title
           ,@Goal_Description
           ,@Unit_Of_Measurement
           ,@Measurement_Value
           ,@Is_HigherValueGood
		   ,@creationDate
           ,@QuarterID)

SET @CurrentInsertedId = SCOPE_IDENTITY();
END



GO
/****** Object:  StoredProcedure [dbo].[InsertGoalQuarter]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertGoalQuarter]

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
/****** Object:  StoredProcedure [dbo].[InsertGoalRules]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Trushna
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[InsertGoalRules] 
	-- Add the parameters for the stored procedure here
	@PerformanceRangeFrom float , 
	@PerformanceRangeTo float ,
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
/****** Object:  StoredProcedure [dbo].[UpdateGoalMaster]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Trushna
-- Create date: 
-- Description:	
-- =============================================
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
/****** Object:  StoredProcedure [dbo].[UpdateGoalRules]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Trushana
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[UpdateGoalRules] 
	-- Add the parameters for the stored procedure here
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
/****** Object:  StoredProcedure [dbo].[UpdateResourceDetails]    Script Date: 2015-11-05 17:26:35 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateResourceGoal]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
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
/****** Object:  Table [dbo].[Department]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[Department_Name] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Goal_Master]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Goal_Master](
	[Goal_MasterID] [int] IDENTITY(1,1) NOT NULL,
	[GoalTitle] [varchar](150) NOT NULL,
	[GoalDescription] [varchar](max) NULL,
	[UnitOfMeasurement] [varchar](20) NOT NULL,
	[MeasurementValue] [float] NOT NULL,
	[Creation_Date] [date] NULL,
	[IsHigherValueGood] [bit] NOT NULL,
	[QuarterId] [int] NOT NULL,
 CONSTRAINT [PK_Goal_Master] PRIMARY KEY CLUSTERED 
(
	[Goal_MasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Goal_Quarter]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goal_Quarter](
	[QuarterID] [int] IDENTITY(1,1) NOT NULL,
	[GoalQuarter] [int] NOT NULL,
	[QuarterYear] [int] NOT NULL,
	[GoalCreateFrom] [date] NULL,
	[GoalCreateTo] [date] NULL,
 CONSTRAINT [PK_Goal_Quater] PRIMARY KEY CLUSTERED 
(
	[QuarterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Goal_Rules]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goal_Rules](
	[Goal_RuleID] [int] IDENTITY(1,1) NOT NULL,
	[Performance_RangeFrom] [float] NOT NULL,
	[Performance_RangeTo] [float] NOT NULL,
	[Rating] [float] NOT NULL,
	[GoalId] [int] NOT NULL,
 CONSTRAINT [PK_Goal_Rules] PRIMARY KEY CLUSTERED 
(
	[Goal_RuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Manager]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[ManagerId] [int] IDENTITY(1,1) NOT NULL,
	[ManagerResourceID] [int] NULL,
	[TeamID] [int] NULL,
 CONSTRAINT [PK_TeamManager] PRIMARY KEY CLUSTERED 
(
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Resource]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Resource](
	[ResourceID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](255) NOT NULL,
	[LastName] [varchar](255) NOT NULL,
	[RoleID] [int] NULL,
	[TeamID] [int] NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[ResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Resource_Goal]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource_Goal](
	[Resource_GoalID] [int] IDENTITY(1,1) NOT NULL,
	[ResourceID] [int] NOT NULL,
	[Goal_MasterID] [int] NOT NULL,
	[Weight] [int] NOT NULL,
 CONSTRAINT [PK_Resource_Goal] PRIMARY KEY CLUSTERED 
(
	[Resource_GoalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Resource_Goal_Performance]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource_Goal_Performance](
	[Resource_Goal_PerformanceID] [int] IDENTITY(1,1) NOT NULL,
	[Resource_Performance] [float] NOT NULL,
	[Resource_Rating] [float] NULL,
	[Resource_GoalId] [int] NOT NULL,
 CONSTRAINT [PK_Resource_Goal_Performance] PRIMARY KEY CLUSTERED 
(
	[Resource_Goal_PerformanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Resource_Performance]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource_Performance](
	[Resource_PerformanceID] [int] IDENTITY(1,1) NOT NULL,
	[ResourceID] [int] NOT NULL,
	[QuaterID] [int] NOT NULL,
	[Resource_Performance] [float] NOT NULL,
 CONSTRAINT [PK_Resource_Performance] PRIMARY KEY CLUSTERED 
(
	[Resource_PerformanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Team]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Team](
	[TeamID] [int] IDENTITY(1,1) NOT NULL,
	[TeamName] [varchar](150) NOT NULL,
	[DepartmentID] [int] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[TeamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TeamLead]    Script Date: 2015-11-05 17:26:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamLead](
	[TeamLeadID] [int] NOT NULL,
	[TeamID] [int] NULL,
	[TeamLeadResourceID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_TeamLead] PRIMARY KEY CLUSTERED 
(
	[TeamLeadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Resource_Goal_Performance] ADD  CONSTRAINT [DF_Resource_Goal_Performance_Resource_Rating]  DEFAULT ((0)) FOR [Resource_Rating]
GO
ALTER TABLE [dbo].[Goal_Master]  WITH CHECK ADD  CONSTRAINT [FK_Goal_Master_QuaeterID] FOREIGN KEY([QuarterId])
REFERENCES [dbo].[Goal_Quarter] ([QuarterID])
GO
ALTER TABLE [dbo].[Goal_Master] CHECK CONSTRAINT [FK_Goal_Master_QuaeterID]
GO
ALTER TABLE [dbo].[Goal_Rules]  WITH CHECK ADD  CONSTRAINT [FK_Goal_Rules_Goal_Master] FOREIGN KEY([GoalId])
REFERENCES [dbo].[Goal_Master] ([Goal_MasterID])
GO
ALTER TABLE [dbo].[Goal_Rules] CHECK CONSTRAINT [FK_Goal_Rules_Goal_Master]
GO
ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [FK_Manager_TeamID] FOREIGN KEY([TeamID])
REFERENCES [dbo].[Team] ([TeamID])
GO
ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [FK_Manager_TeamID]
GO
ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [FK_TeamManager_Resource] FOREIGN KEY([ManagerResourceID])
REFERENCES [dbo].[Resource] ([ResourceID])
GO
ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [FK_TeamManager_Resource]
GO
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_RoleID]
GO
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_TeamID] FOREIGN KEY([TeamID])
REFERENCES [dbo].[Team] ([TeamID])
GO
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_TeamID]
GO
ALTER TABLE [dbo].[Resource_Goal]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Goal_Goal_Master] FOREIGN KEY([Goal_MasterID])
REFERENCES [dbo].[Goal_Master] ([Goal_MasterID])
GO
ALTER TABLE [dbo].[Resource_Goal] CHECK CONSTRAINT [FK_Resource_Goal_Goal_Master]
GO
ALTER TABLE [dbo].[Resource_Goal]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Goal_Resource] FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Resource] ([ResourceID])
GO
ALTER TABLE [dbo].[Resource_Goal] CHECK CONSTRAINT [FK_Resource_Goal_Resource]
GO
ALTER TABLE [dbo].[Resource_Goal_Performance]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Goal_Performance_Resource_GoalId] FOREIGN KEY([Resource_GoalId])
REFERENCES [dbo].[Resource_Goal] ([Resource_GoalID])
GO
ALTER TABLE [dbo].[Resource_Goal_Performance] CHECK CONSTRAINT [FK_Resource_Goal_Performance_Resource_GoalId]
GO
ALTER TABLE [dbo].[Resource_Performance]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Performance_Goal_Quater] FOREIGN KEY([QuaterID])
REFERENCES [dbo].[Goal_Quarter] ([QuarterID])
GO
ALTER TABLE [dbo].[Resource_Performance] CHECK CONSTRAINT [FK_Resource_Performance_Goal_Quater]
GO
ALTER TABLE [dbo].[Resource_Performance]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Performance_Resource] FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Resource] ([ResourceID])
GO
ALTER TABLE [dbo].[Resource_Performance] CHECK CONSTRAINT [FK_Resource_Performance_Resource]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_DepartmentID] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_DepartmentID]
GO
ALTER TABLE [dbo].[TeamLead]  WITH CHECK ADD  CONSTRAINT [FK_TeamLead_Resource] FOREIGN KEY([TeamLeadResourceID])
REFERENCES [dbo].[Resource] ([ResourceID])
GO
ALTER TABLE [dbo].[TeamLead] CHECK CONSTRAINT [FK_TeamLead_Resource]
GO
ALTER TABLE [dbo].[TeamLead]  WITH CHECK ADD  CONSTRAINT [FK_TeamLead_TeamID] FOREIGN KEY([TeamID])
REFERENCES [dbo].[Team] ([TeamID])
GO
ALTER TABLE [dbo].[TeamLead] CHECK CONSTRAINT [FK_TeamLead_TeamID]
GO
USE [master]
GO
ALTER DATABASE [IT-Tracking] SET  READ_WRITE 
GO
