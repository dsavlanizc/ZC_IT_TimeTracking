USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Resource_Goal_Performance]    Script Date: 29-10-2015 15:14:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Resource_Goal_Performance](
	[Resource_Goal_PerformanceID] [int] IDENTITY(1,1) NOT NULL,
	[Resource_Performance] [int] NOT NULL,
	[Resource_Rating] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[Goal_MasterID] [int] NOT NULL,
 CONSTRAINT [PK_Resource_Goal_Performance] PRIMARY KEY CLUSTERED 
(
	[Resource_Goal_PerformanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Resource_Goal_Performance] ADD  CONSTRAINT [DF_Resource_Goal_Performance_Resource_Rating]  DEFAULT ((0)) FOR [Resource_Rating]
GO

ALTER TABLE [dbo].[Resource_Goal_Performance]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Goal_Performance_Goal_Master] FOREIGN KEY([Goal_MasterID])
REFERENCES [dbo].[Goal_Master] ([Goal_MasterID])
GO

ALTER TABLE [dbo].[Resource_Goal_Performance] CHECK CONSTRAINT [FK_Resource_Goal_Performance_Goal_Master]
GO

ALTER TABLE [dbo].[Resource_Goal_Performance]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Goal_Performance_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([ResourceID])
GO

ALTER TABLE [dbo].[Resource_Goal_Performance] CHECK CONSTRAINT [FK_Resource_Goal_Performance_Resource]
GO

