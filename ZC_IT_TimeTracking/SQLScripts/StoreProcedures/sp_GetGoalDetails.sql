USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[GetGoalDetails]    Script Date: 2015-10-30 14:46:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Trushna
-- Description:	give all goal details
-- =============================================
CREATE PROCEDURE [dbo].[GetGoalDetails] 
	@Goal_Id int,
	@Goal_Title varchar(150) OUTPUT, 
    @Goal_Description varchar(Max) OUTPUT,
	@Unit_Of_Measurement varchar(20) OUTPUT,
	@Measurement_Value float OUTPUT,
	@CreationDate date,
	@Is_HigherValueGood bit OUTPUT,
	@QuaterID int OUTPUT,
	@Goal_Quater int OUTPUT,
	@Goal_Year int OUTPUT
AS
BEGIN
	
SET NOCOUNT ON;

SELECT 
      @Goal_Title=[GoalTitle]
      ,@Goal_Description=[GoalDescription]
      ,@Unit_Of_Measurement=[UnitOfMeasurement]
      ,@Measurement_Value=[MeasurementValue]
      ,@CreationDate=[Creation_Date]
      ,@Is_HigherValueGood=[IsHigherValueGood]
      ,@QuaterID=[QuaterId]
  FROM [dbo].[Goal_Master]
  WHERE @Goal_Id=[Goal_MasterID]

SELECT @Goal_Quater=[Quater] ,
		@Goal_Year=[YEAR]
	FROM [dbo].[Goal_Quater]
	WHERE @QuaterID=[QuaterId]


END


GO

