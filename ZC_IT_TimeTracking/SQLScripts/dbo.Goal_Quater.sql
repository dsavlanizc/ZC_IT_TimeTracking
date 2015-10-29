USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Goal_Quater]    Script Date: 29-10-2015 15:12:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Goal_Quater](
	[QuaterID] [int] IDENTITY(1,1) NOT NULL,
	[Quater] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[GoalCreateFrom] [date] NOT NULL,
	[GoalCreateTo] [date] NOT NULL,
 CONSTRAINT [PK_Goal_Quater] PRIMARY KEY CLUSTERED 
(
	[QuaterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

