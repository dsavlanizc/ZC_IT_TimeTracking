USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[UpdateGoalRules]    Script Date: 2015-10-30 14:48:53 ******/
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

