USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Goal_Master]    Script Date: 29-10-2015 15:10:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Goal_Master](
	[Goal_MasterID] [int] IDENTITY(1,1) NOT NULL,
	[GoalTitle] [varchar](150) NOT NULL,
	[GoalDescription] [varchar](max) NULL,
	[UnitOfMeasurement] [varchar](20) NOT NULL,
	[MeasurementValue] [float] NOT NULL,
	[IsHigherValueGood] [bit] NOT NULL,
	[QuaterId] [int] NOT NULL,
 CONSTRAINT [PK_Goal_Master] PRIMARY KEY CLUSTERED 
(
	[Goal_MasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Goal_Master]  WITH CHECK ADD  CONSTRAINT [FK_Goal_Master_QuaterID] FOREIGN KEY([QuaterId])
REFERENCES [dbo].[Goal_Quater] ([QuaterID])
GO

ALTER TABLE [dbo].[Goal_Master] CHECK CONSTRAINT [FK_Goal_Master_QuaterID]
GO


