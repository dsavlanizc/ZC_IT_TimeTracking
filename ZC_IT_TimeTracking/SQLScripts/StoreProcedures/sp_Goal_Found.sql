USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[GoalFound]    Script Date: 2015-10-30 14:47:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[GoalFound]
	@GoalID int,
	@GoalFound INT OUTPUT 
AS
BEGIN

	SET NOCOUNT ON;
if @GoalID = (SELECT [Goal_MasterID]
  FROM [dbo].[Goal_Master]
 WHERE @GoalID= [Goal_MasterID])
	
	BEGIN 
	set @GoalFound=1
	RETURN @GoalFound
	END

ELSE 
 BEGIN 
	set @GoalFound=0
	RETURN @GoalFound
	END 

END

GO

