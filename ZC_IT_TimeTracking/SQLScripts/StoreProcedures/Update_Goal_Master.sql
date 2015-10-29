
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trushna
 
-- Description:	update the goal_master table based on goal_masterid
-- =============================================
CREATE PROCEDURE Update_Goal_Master 
	@Goal_Id int,
	@Goal_Title varchar(150), 
    @Goal_Description varchar(Max),
	@Unit_Of_Measurement varchar(20),
	@Measurement_Value float,
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
      ,[MeasurementValue] = MeasurementValue
      ,[IsHigherValueGood] = IsHigherValueGood
      ,[QuaterId] = @QuaterID
 WHERE [Goal_MasterID]=@Goal_Id
 
END
GO
