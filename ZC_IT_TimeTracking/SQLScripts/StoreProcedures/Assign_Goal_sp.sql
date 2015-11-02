USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[AddResource]    Script Date: 2015-10-30 19:00:12 ******/
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
USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[AssignGoalToResource]    Script Date: 2015-10-30 19:00:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AssignGoalToResource]
@ResourceId int,
@GoalId int,
@weight int
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
END

GO



USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[UpdateResourceGoal]    Script Date: 2015-10-30 19:01:05 ******/
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

/****** Object:  StoredProcedure [dbo].[DeleteResourceGoal]    Script Date: 2015-10-30 19:01:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteResourceGoal] 
	@ResourceId int,
	@GoalId int
AS
BEGIN

DELETE FROM [dbo].[Resource_Goal]
      WHERE [ResourceID]=@ResourceId 
				AND
				[Goal_MasterID]=@GoalId

END

GO


USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[GetAllGoalsOfResource]    Script Date: 2015-10-30 19:08:09 ******/
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

USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[GetAllResourceForGoal]    Script Date: 2015-10-30 19:08:25 ******/
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

