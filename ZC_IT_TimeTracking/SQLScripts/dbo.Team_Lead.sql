USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[TeamLead]    Script Date: 29-10-2015 15:16:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TeamLead](
	[TeamLeadID] [int] NOT NULL,
	[TeamID] [int] NULL,
	[TeamLeadResourceID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_TeamLead] PRIMARY KEY CLUSTERED 
(
	[TeamLeadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TeamLead]  WITH CHECK ADD  CONSTRAINT [FK_TeamLead_Resource] FOREIGN KEY([TeamLeadResourceID])
REFERENCES [dbo].[Resource] ([ResourceID])
GO

ALTER TABLE [dbo].[TeamLead] CHECK CONSTRAINT [FK_TeamLead_Resource]
GO

ALTER TABLE [dbo].[TeamLead]  WITH CHECK ADD  CONSTRAINT [FK_TeamLead_TeamID] FOREIGN KEY([TeamID])
REFERENCES [dbo].[Team] ([TeamID])
GO

ALTER TABLE [dbo].[TeamLead] CHECK CONSTRAINT [FK_TeamLead_TeamID]
GO

