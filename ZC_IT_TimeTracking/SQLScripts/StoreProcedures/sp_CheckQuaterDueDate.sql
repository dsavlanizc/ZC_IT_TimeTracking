USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[CheckQuaterDueDate]    Script Date: 2015-10-30 14:43:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Trushna
-- Create date: 
-- Description:	Check due date of quater
-- =============================================
CREATE PROCEDURE [dbo].[CheckQuaterDueDate] 
	
	@_Quater int  , 
	@_Year int ,
	@IsValid int OUTPUT,
	@Quater_Id int OUTPUT
AS
BEGIN
SET NOCOUNT ON;

 set @Quater_Id= (select  QuaterID  FROM dbo.Goal_Quater 
	where 
	Quater = @_Quater AND  Year=@_Year)

If 	GETDATE() > ( SELECT GoalCreateFrom FROM Goal_Quater) AND GETDATE() < ( SELECT GoalCreatetO FROM Goal_Quater)
	SET @IsValid= 1
ELSE
	SET @IsValid = 0

RETURN	@IsValid
END


GO

