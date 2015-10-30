USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[InsertGoalRules]    Script Date: 2015-10-30 14:48:05 ******/
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

