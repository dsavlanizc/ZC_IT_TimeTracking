
USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[CheckQuater]    Script Date: 2015-11-02 12:33:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CheckQuater] 
	
	@_Quater int  , 
	@_Year int 

AS
BEGIN
SET NOCOUNT ON;

 select  QuaterID,GoalCreateFrom,GoalCreateTo  FROM dbo.Goal_Quater 
	where 
	Quater = @_Quater AND  Year=@_Year

END

go
USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[DeleteGoalMaster]    Script Date: 2015-10-30 18:19:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteGoalMaster] 

	@GoalId int
	
AS
BEGIN
SET NOCOUNT ON;
	
DELETE FROM [dbo].[Goal_Master]
      WHERE @GoalId=[Goal_MasterID]
DELETE FROM [dbo].[Goal_Rules]
	WHERE @GoalId=[GoalId]

END

GO

USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[DeleteGoalRule]    Script Date: 2015-10-30 18:19:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteGoalRule]
	@GoalRuleId int
AS
BEGIN
	
DELETE FROM [dbo].[Goal_Rules]
      WHERE [Goal_RuleID]=@GoalRuleId

END

GO

USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[GetGoalDetails]    Script Date: 2015-10-30 18:19:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[GetGoalDetails] 
	@Goal_Id int
	
AS
BEGIN
	
SET NOCOUNT ON;

SELECT 
      [GoalTitle]
      ,[GoalDescription]
      ,[UnitOfMeasurement]
      ,[MeasurementValue]
      ,[Creation_Date]
      ,[IsHigherValueGood]
      ,[QuaterId]
  FROM [dbo].[Goal_Master]
  WHERE @Goal_Id=[Goal_MasterID]
END


GO
USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[GetGoalRuleDetails]    Script Date: 2015-10-30 18:19:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetGoalRuleDetails]
	
	@GoalId int

AS
BEGIN	
	
	SELECT [Goal_RuleID],[Performance_RangeFrom] ,[Performance_RangeTo] ,[Rating] 
	FROM Goal_Rules 
	WHERE @GoalID=[GoalID]
	
END

GO

USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[GetQuaterDetails]    Script Date: 2015-10-30 18:20:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[GetQuaterDetails]
	@QuaterId int
AS
BEGIN
 select [Quater],[YEAR],[GoalCreateFrom],[GoalCreateTo]
 FROM Goal_Quater
 where QuaterID=@QuaterId
	
END

GO

USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[InsertGoalMaster]    Script Date: 2015-10-30 18:20:17 ******/
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
USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[InsertGoalQuater]    Script Date: 2015-10-30 18:20:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertGoalQuater]

	@Quater int, 
	@Year INT,
	@GoalCreate_From date,
	@GoalCreate_To date

AS
BEGIN
	INSERT INTO [dbo].[Goal_Quater]
           ([Quater]
           ,[Year]
           ,[GoalCreateFrom]
           ,[GoalCreateTo])
     VALUES
           ( @Quater,@Year,@GoalCreate_From,@GoalCreate_To)
         
END

GO

USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[InsertGoalRules]    Script Date: 2015-10-30 18:20:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[InsertGoalRules] 

	@PerformanceRangeFrom int , 
	@PerformanceRangeTo int ,
	@Rating float,
	@GoalID int
AS
BEGIN
	
	SET NOCOUNT ON
	INSERT INTO [dbo].[Goal_Rules]
           ([Performance_RangeFrom]
           ,[Performance_RangeTo]
           ,[Rating]
           ,[GoalId])
     VALUES
           (@PerformanceRangeFrom
           ,@PerformanceRangeTo
           ,@Rating
           ,@GoalID)

END


GO


USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[UpdateGoalMaster]    Script Date: 2015-10-30 18:20:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[UpdateGoalMaster] 
	@Goal_Id int,
	@Goal_Title varchar(150), 
    @Goal_Description varchar(Max),
	@Unit_Of_Measurement varchar(20),
	@Measurement_Value float,
	@CreateDate date,
	@Is_HigherValueGood bit,
	@QuaterID int
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
USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[UpdateGoalRules]    Script Date: 2015-10-30 18:20:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[UpdateGoalRules] 
	@PerformanceRangeFrom int , 
	@PerformanceRangeTo int ,
	@Rating float,
	@GoalID int
AS
BEGIN
	
	SET NOCOUNT ON;
UPDATE [dbo].[Goal_Rules]
   SET [Performance_RangeFrom] = @PerformanceRangeFrom
      ,[Performance_RangeTo] = @PerformanceRangeTo
      ,[Rating] = @Rating

 WHERE [GoalID] = @GoalID
 END



GO





