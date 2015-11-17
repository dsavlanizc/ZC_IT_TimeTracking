USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[CalculateResourceWeight]    Script Date: 2015-11-10 00:50:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CalculateResourceWeight]
	@ResourceId int,
	@Gquarter int,
	@Gyear int ,
	@GoalId int ,
	@weightEntered int ,
	@valid bit output

AS
BEGIN
    Declare @currentQuarter int 
	Declare @TotalWeight int
	Declare @finalWeight int
	
	set @currentQuarter=(select QuarterID from Goal_Quarter where GoalQuarter=@Gquarter and QuarterYear=@Gyear)
	
	set @TotalWeight= ( select sum(RG.Weight) 
						from Resource_Goal RG , Goal_Master G 
						where   RG.ResourceID=@ResourceId
						and		RG.Goal_MasterID=G.Goal_MasterID
						and		G.QuarterId=@currentQuarter)
	
	set @finalWeight= @TotalWeight+@weightEntered

	if @finalWeight>100
		set @valid = 0 
	else 
		exec [dbo].[AssignGoalToResource] @ResourceId,@GoalId,@weightEntered,getdate

END

GO

