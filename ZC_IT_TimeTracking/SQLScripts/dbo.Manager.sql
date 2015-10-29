USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Manager]    Script Date: 29-10-2015 15:16:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Manager](
	[ManagerId] [int] IDENTITY(1,1) NOT NULL,
	[ManagerResourceID] [int] NULL,
	[TeamID] [int] NULL,
 CONSTRAINT [PK_TeamManager] PRIMARY KEY CLUSTERED 
(
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [FK_Manager_TeamID] FOREIGN KEY([TeamID])
REFERENCES [dbo].[Team] ([TeamID])
GO

ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [FK_Manager_TeamID]
GO

ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [FK_TeamManager_Resource] FOREIGN KEY([ManagerResourceID])
REFERENCES [dbo].[Resource] ([ResourceID])
GO

ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [FK_TeamManager_Resource]
GO

