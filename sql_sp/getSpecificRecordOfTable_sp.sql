USE [IT-Tracking]
GO

/****** Object:  StoredProcedure [dbo].[getSpecificRecordOftable]    Script Date: 2015-11-09 12:42:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[getSpecificRecordOftable] 
	@startFrom int,
	@NoOfRecords int ,
	@tableName varchar(max)
AS
BEGIN
	DECLARE @sqlQuery varchar(max)

	set @sqlQuery = 'Select * from ' + @TableName +' ORDER BY '+ @tableName+'ID OFFSET '+ cast(@startFrom as varchar) +' ROWS
	FETCH NEXT ' + cast(@NoOfRecords as varchar) +' ROWS ONLY '
	
	Execute(@sqlQuery)

END

GO

