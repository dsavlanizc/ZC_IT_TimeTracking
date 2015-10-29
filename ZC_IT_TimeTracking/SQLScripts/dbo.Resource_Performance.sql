USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Resource_Performance]    Script Date: 29-10-2015 15:14:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Resource_Performance](
	[Resource_PerformanceID] [int] IDENTITY(1,1) NOT NULL,
	[ResourceID] [int] NOT NULL,
	[QuaterID] [int] NOT NULL,
	[Resource_Performance] [int] NOT NULL,
 CONSTRAINT [PK_Resource_Performance] PRIMARY KEY CLUSTERED 
(
	[Resource_PerformanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Resource_Performance]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Performance_Goal_Quater] FOREIGN KEY([QuaterID])
REFERENCES [dbo].[Goal_Quater] ([QuaterID])
GO

ALTER TABLE [dbo].[Resource_Performance] CHECK CONSTRAINT [FK_Resource_Performance_Goal_Quater]
GO

ALTER TABLE [dbo].[Resource_Performance]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Performance_Resource] FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Resource] ([ResourceID])
GO

ALTER TABLE [dbo].[Resource_Performance] CHECK CONSTRAINT [FK_Resource_Performance_Resource]
GO

