USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[Get_Goal_Details]    Script Date: 29-10-2015 18:27:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Trushna
-- Description:	give all goal details
-- =============================================
CREATE PROCEDURE [dbo].[Get_Goal_Details] 
	@Goal_Id int,
	@Goal_Title varchar(150) OUTPUT, 
    @Goal_Description varchar(Max) OUTPUT,
	@Unit_Of_Measurement varchar(20) OUTPUT,
	@Measurement_Value float OUTPUT,
	@Is_HigherValueGood bit OUTPUT,
	@QuaterID int OUTPUT,
	@Goal_Quater int OUTPUT,
	@Goal_Year int OUTPUT
AS
BEGIN
	
SET NOCOUNT ON;
set @Goal_Title = (SELECT [GoalTitle]
  FROM [dbo].[Goal_Master] 
   WHERE [Goal_MasterID]=@Goal_Id)
set @Goal_Description = (SELECT [GoalDescription]
  FROM [dbo].[Goal_Master] 
   WHERE [Goal_MasterID]=@Goal_Id)

set @Unit_Of_Measurement = (SELECT [UnitOfMeasurement]
  FROM [dbo].[Goal_Master] 
   WHERE [Goal_MasterID]=@Goal_Id)

set @Measurement_Value = (SELECT [MeasurementValue]
  FROM [dbo].[Goal_Master] 
   WHERE [Goal_MasterID]=@Goal_Id)

set @Is_HigherValueGood = (SELECT [IsHigherValueGood]
  FROM [dbo].[Goal_Master] 
   WHERE [Goal_MasterID]=@Goal_Id)

set @QuaterID = (SELECT [QuaterId]
  FROM [dbo].[Goal_Master] 
   WHERE [Goal_MasterID]=@Goal_Id)

set @Goal_Quater = (SELECT [Quater]
  FROM [dbo].[Goal_Quater] 
   WHERE [QuaterID]=@QuaterID)

set @Goal_Year = (SELECT [Year]
  FROM [dbo].[Goal_Quater] 
   WHERE [QuaterID]=@QuaterID)

RETURN
END


GO

