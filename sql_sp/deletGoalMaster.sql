USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[DeleteGoalMaster]    Script Date: 2015-10-30 18:18:22 ******/
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
	
DELETE FROM [dbo].[Goal_Master]
      WHERE @GoalId=[Goal_MasterID]
DELETE FROM [dbo].[Goal_Rules]
	WHERE @GoalId=[GoalId]

END

GO

