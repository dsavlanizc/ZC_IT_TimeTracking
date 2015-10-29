USE [IT-Tracking]
GO
/****** Object:  StoredProcedure [dbo].[Insert_Goal_Master]    Script Date: 29-10-2015 17:50:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Insert_Goal_Master] 
	@Goal_Title varchar(150), 
    @Goal_Description varchar(Max),
	@Unit_Of_Measurement varchar(20),
	@Measurement_Value float,
	@Is_HigherValueGood bit,
	@QuaterID int
AS
BEGIN
INSERT INTO [dbo].[Goal_Master]
           ([GoalTitle]
           ,[GoalDescription]
           ,[UnitOfMeasurement]
           ,[MeasurementValue]
           ,[IsHigherValueGood]
           ,[QuaterId])
     VALUES
           (@Goal_Title,
		   @Goal_Description,
		   @Unit_Of_Measurement,
		   @Measurement_Value,
		   @Is_HigherValueGood,
		   @QuaterID)
	
END