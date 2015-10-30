USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[InsertGoalMaster]    Script Date: 2015-10-30 14:47:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertGoalMaster] 
	@Goal_Title varchar(150), 
    @Goal_Description varchar(Max),
	@Unit_Of_Measurement varchar(20),
	@Measurement_Value float,
	@Is_HigherValueGood bit,
	@CreationDate date,
	@QuaterID int
AS
BEGIN

INSERT INTO [dbo].[Goal_Master]
           ([GoalTitle]
           ,[GoalDescription]
           ,[UnitOfMeasurement]
           ,[MeasurementValue]
           ,[Creation_Date]
           ,[IsHigherValueGood]
           ,[QuaterId])
     VALUES
           (@Goal_Description
           ,@Goal_Description
           ,@Unit_Of_Measurement
           ,@Measurement_Value
           ,@CreationDate
           ,@Is_HigherValueGood
           ,@QuaterID)

END


GO
