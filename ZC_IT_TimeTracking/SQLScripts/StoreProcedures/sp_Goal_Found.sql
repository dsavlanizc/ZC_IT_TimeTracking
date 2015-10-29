
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
use [IT-Tracking]
GO

Create PROCEDURE GoalFound
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
