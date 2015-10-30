USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[DeleteGoalRule]    Script Date: 2015-10-30 14:46:03 ******/
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

