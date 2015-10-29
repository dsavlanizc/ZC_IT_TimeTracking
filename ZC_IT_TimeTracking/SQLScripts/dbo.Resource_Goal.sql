USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Resource_Goal]    Script Date: 29-10-2015 15:14:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Resource_Goal](
	[Resource_GoalID] [int] IDENTITY(1,1) NOT NULL,
	[ResourceID] [int] NOT NULL,
	[Goal_MasterID] [int] NOT NULL,
	[Weight] [int] NOT NULL,
 CONSTRAINT [PK_Resource_Goal] PRIMARY KEY CLUSTERED 
(
	[Resource_GoalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Resource_Goal]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Goal_Goal_Master] FOREIGN KEY([Goal_MasterID])
REFERENCES [dbo].[Goal_Master] ([Goal_MasterID])
GO

ALTER TABLE [dbo].[Resource_Goal] CHECK CONSTRAINT [FK_Resource_Goal_Goal_Master]
GO

ALTER TABLE [dbo].[Resource_Goal]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Goal_Resource] FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Resource] ([ResourceID])
GO

ALTER TABLE [dbo].[Resource_Goal] CHECK CONSTRAINT [FK_Resource_Goal_Resource]
GO

