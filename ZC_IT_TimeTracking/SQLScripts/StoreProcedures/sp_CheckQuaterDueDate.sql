USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[CheckQuaterDueDate]    Script Date: 29-10-2015 18:09:03 ******/
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

	@Quater_Id int OUTPUT
AS
BEGIN
SET NOCOUNT ON;

 set @Quater_Id= (select  QuaterID  FROM dbo.Goal_Quater 
	where 
	Quater = @_Quater AND  Year=@_Year
	and SYSDATETIME() >= GoalCreateFrom
	and SYSDATETIME() <= GoalCreateTo)
	
END

GO

