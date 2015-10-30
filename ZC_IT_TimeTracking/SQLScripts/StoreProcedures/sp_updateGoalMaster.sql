USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[UpdateGoalMaster]    Script Date: 2015-10-30 14:48:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Trushna
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[UpdateGoalMaster] 
	@Goal_Id int,
	@Goal_Title varchar(150), 
    @Goal_Description varchar(Max),
	@Unit_Of_Measurement varchar(20),
	@Measurement_Value float,
	@CreateDate date,
	@Is_HigherValueGood bit,
	@QuaterID int,
	@Update_Successful int OUTPUT 
AS
BEGIN
	
	SET NOCOUNT ON;
	
UPDATE [dbo].[Goal_Master]
   SET [GoalTitle] = @Goal_Title
      ,[GoalDescription] = @Goal_Description
      ,[UnitOfMeasurement] = @Unit_Of_Measurement
	  ,[Creation_Date]=@CreateDate
      ,[MeasurementValue] = MeasurementValue
      ,[IsHigherValueGood] = IsHigherValueGood
      ,[QuaterId] = @QuaterID
 WHERE [Goal_MasterID]=@Goal_Id

END


GO

