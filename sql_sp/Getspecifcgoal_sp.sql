USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[GetSpecificRecordsOfGoal]    Script Date: 2015-11-09 16:10:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetSpecificRecordsOfGoal]
	@startFrom int,
	@NoOfRecords int,
	@totalRecords int output 
AS
BEGIN
	SELECT	G.Goal_MasterID 
			, G.GoalTitle
			, G.GoalDescription
			, Q.GoalQuarter
			, Q.QuarterYear
			, G.UnitOfMeasurement
			, G.MeasurementValue
			, G.Creation_Date
			, G.IsHigherValueGood
			
	FROM [dbo].[Goal_Master] G ,
		 [dbo].[Goal_Quarter] Q
	where G.QuarterId=q.QuarterID
	ORDER BY g.Goal_MasterID 
	OFFSET @startFrom ROWS
	FETCH NEXT @NoOfRecords ROWS ONLY 
 
 set @totalRecords=(select count(*) from Goal_Master)
END

GO

