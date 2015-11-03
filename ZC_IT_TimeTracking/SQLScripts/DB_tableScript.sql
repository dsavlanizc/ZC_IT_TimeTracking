/****** Object:  Table [dbo].[Department]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[Department_Name] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Goal_Master]    Script Date: 3/11/2015 10:50:37 AM ******/
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
	[Creation_Date] [date] NULL,
	[IsHigherValueGood] [bit] NOT NULL,
	[QuarterId] [int] NOT NULL,
 CONSTRAINT [PK_Goal_Master] PRIMARY KEY CLUSTERED 
(
	[Goal_MasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Goal_Quarter]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goal_Quarter](
	[QuarterID] [int] IDENTITY(1,1) NOT NULL,
	[GoalQuarter] [int] NOT NULL,
	[QuarterYear] [int] NOT NULL,
	[GoalCreateFrom] [date] NOT NULL,
	[GoalCreateTo] [date] NOT NULL,
 CONSTRAINT [PK_Goal_Quater] PRIMARY KEY CLUSTERED 
(
	[QuarterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Goal_Rules]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goal_Rules](
	[Goal_RuleID] [int] IDENTITY(1,1) NOT NULL,
	[Performance_RangeFrom] [int] NOT NULL,
	[Performance_RangeTo] [int] NOT NULL,
	[Rating] [float] NOT NULL,
	[GoalId] [int] NOT NULL,
 CONSTRAINT [PK_Goal_Rules] PRIMARY KEY CLUSTERED 
(
	[Goal_RuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Manager]    Script Date: 3/11/2015 10:50:37 AM ******/
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
/****** Object:  Table [dbo].[Resource]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Resource](
	[ResourceID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](255) NOT NULL,
	[LastName] [varchar](255) NOT NULL,
	[RoleID] [int] NULL,
	[TeamID] [int] NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[ResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Resource_Goal]    Script Date: 3/11/2015 10:50:37 AM ******/
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
/****** Object:  Table [dbo].[Resource_Goal_Performance]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource_Goal_Performance](
	[Resource_Goal_PerformanceID] [int] IDENTITY(1,1) NOT NULL,
	[Resource_Performance] [float] NOT NULL,
	[Resource_Rating] [float] NULL,
	[Resource_GoalId] [int] NOT NULL,
 CONSTRAINT [PK_Resource_Goal_Performance] PRIMARY KEY CLUSTERED 
(
	[Resource_Goal_PerformanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Resource_Performance]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource_Performance](
	[Resource_PerformanceID] [int] IDENTITY(1,1) NOT NULL,
	[ResourceID] [int] NOT NULL,
	[QuaterID] [int] NOT NULL,
	[Resource_Performance] [float] NOT NULL,
 CONSTRAINT [PK_Resource_Performance] PRIMARY KEY CLUSTERED 
(
	[Resource_PerformanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Team]    Script Date: 3/11/2015 10:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Team](
	[TeamID] [int] IDENTITY(1,1) NOT NULL,
	[TeamName] [varchar](150) NOT NULL,
	[DepartmentID] [int] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[TeamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TeamLead]    Script Date: 3/11/2015 10:50:37 AM ******/
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
ALTER TABLE [dbo].[Resource_Goal_Performance] ADD  CONSTRAINT [DF_Resource_Goal_Performance_Resource_Rating]  DEFAULT ((0)) FOR [Resource_Rating]
GO
ALTER TABLE [dbo].[Goal_Master]  WITH CHECK ADD  CONSTRAINT [FK_Goal_Master_QuaeterID] FOREIGN KEY([QuarterId])
REFERENCES [dbo].[Goal_Quarter] ([QuarterID])
GO
ALTER TABLE [dbo].[Goal_Master] CHECK CONSTRAINT [FK_Goal_Master_QuaeterID]
GO
ALTER TABLE [dbo].[Goal_Rules]  WITH CHECK ADD  CONSTRAINT [FK_Goal_Rules_Goal_Master] FOREIGN KEY([GoalId])
REFERENCES [dbo].[Goal_Master] ([Goal_MasterID])
GO
ALTER TABLE [dbo].[Goal_Rules] CHECK CONSTRAINT [FK_Goal_Rules_Goal_Master]
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
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_RoleID]
GO
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_TeamID] FOREIGN KEY([TeamID])
REFERENCES [dbo].[Team] ([TeamID])
GO
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_TeamID]
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
ALTER TABLE [dbo].[Resource_Goal_Performance]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Goal_Performance_Resource_GoalId] FOREIGN KEY([Resource_GoalId])
REFERENCES [dbo].[Resource_Goal] ([Resource_GoalID])
GO
ALTER TABLE [dbo].[Resource_Goal_Performance] CHECK CONSTRAINT [FK_Resource_Goal_Performance_Resource_GoalId]
GO
ALTER TABLE [dbo].[Resource_Performance]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Performance_Goal_Quater] FOREIGN KEY([QuaterID])
REFERENCES [dbo].[Goal_Quarter] ([QuarterID])
GO
ALTER TABLE [dbo].[Resource_Performance] CHECK CONSTRAINT [FK_Resource_Performance_Goal_Quater]
GO
ALTER TABLE [dbo].[Resource_Performance]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Performance_Resource] FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Resource] ([ResourceID])
GO
ALTER TABLE [dbo].[Resource_Performance] CHECK CONSTRAINT [FK_Resource_Performance_Resource]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_DepartmentID] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_DepartmentID]
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
USE [master]
GO
ALTER DATABASE [IT-Tracking] SET  READ_WRITE 
GO
