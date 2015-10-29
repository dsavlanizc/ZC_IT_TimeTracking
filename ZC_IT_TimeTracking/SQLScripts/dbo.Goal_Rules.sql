USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Goal_Rules]    Script Date: 29-10-2015 15:13:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Goal_Rules](
	[Goal_RuleID] [int] IDENTITY(1,1) NOT NULL,
	[Performance_RangeFrom] [int] NOT NULL,
	[Performance_RangeTo] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[GoalId] [int] NOT NULL,
 CONSTRAINT [PK_Goal_Rules] PRIMARY KEY CLUSTERED 
(
	[Goal_RuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Goal_Rules]  WITH CHECK ADD  CONSTRAINT [FK_Goal_Rules_Goal_Master] FOREIGN KEY([GoalId])
REFERENCES [dbo].[Goal_Master] ([Goal_MasterID])
GO

ALTER TABLE [dbo].[Goal_Rules] CHECK CONSTRAINT [FK_Goal_Rules_Goal_Master]
GO

