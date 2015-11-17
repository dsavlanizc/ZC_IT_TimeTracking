USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[SearchGoalByTitle]    Script Date: 2015-11-09 15:42:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SearchGoalByTitle]
	@title Varchar(150),
	@startFrom int,
	@NoOfRecords int ,
	@records int output

AS
BEGIN
	SELECT	 G.Goal_MasterID
			,G.GoalTitle
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
	AND G.GoalTitle like '%'+@title+'%'
	ORDER BY g.Goal_MasterID ASC
	OFFSET @startFrom ROWS
	FETCH NEXT @NoOfRecords ROWS ONLY 

set @records=(select count(*) from Goal_Master
				where  GoalTitle like '%'+@title+'%' )
END

GO

