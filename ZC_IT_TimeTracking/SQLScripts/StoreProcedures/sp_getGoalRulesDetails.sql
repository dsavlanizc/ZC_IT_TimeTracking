USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[GetGoalRuleDetails]    Script Date: 2015-10-30 14:46:51 ******/
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
	@Goal_RuleID int,
	@PerFormanceRangeFrom int OUTPUT,
	@PerformanceRangeTo int OUTPUT,
	@Rating float OUTPUT
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT @PerFormanceRangeFrom=[Performance_RangeFrom] , @PerformanceRangeTo=[Performance_RangeTo] , @Rating =[Rating] 
	FROM Goal_Rules 
	WHERE @Goal_RuleID=[Goal_RuleID]
	
END

GO

