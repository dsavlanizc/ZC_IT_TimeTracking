USE [master]
GO

/****** Object:  Database [IT-Tracking]    Script Date: 29-10-2015 18:32:37 ******/
CREATE DATABASE [IT-Tracking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IT-Tracking', FILENAME = N'C:\Users\tpanchal\IT-Tracking.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'IT-Tracking_log', FILENAME = N'C:\Users\tpanchal\IT-Tracking_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [IT-Tracking] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IT-Tracking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [IT-Tracking] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [IT-Tracking] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [IT-Tracking] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [IT-Tracking] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [IT-Tracking] SET ARITHABORT OFF 
GO

ALTER DATABASE [IT-Tracking] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [IT-Tracking] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [IT-Tracking] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [IT-Tracking] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [IT-Tracking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [IT-Tracking] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [IT-Tracking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [IT-Tracking] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [IT-Tracking] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [IT-Tracking] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [IT-Tracking] SET  DISABLE_BROKER 
GO

ALTER DATABASE [IT-Tracking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [IT-Tracking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [IT-Tracking] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [IT-Tracking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [IT-Tracking] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [IT-Tracking] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [IT-Tracking] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [IT-Tracking] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [IT-Tracking] SET  MULTI_USER 
GO

ALTER DATABASE [IT-Tracking] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [IT-Tracking] SET DB_CHAINING OFF 
GO

ALTER DATABASE [IT-Tracking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [IT-Tracking] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [IT-Tracking] SET  READ_WRITE 
GO

USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 29-10-2015 18:35:38 ******/
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



USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Department]    Script Date: 29-10-2015 18:33:30 ******/
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

USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Team]    Script Date: 29-10-2015 18:36:36 ******/
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

ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_DepartmentID] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
GO

ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_DepartmentID]
GO
USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Manager]    Script Date: 29-10-2015 18:38:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Resource]    Script Date: 29-10-2015 18:39:28 ******/
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
	[TeamID] [int] NOT NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[ResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
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

USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[TeamLead]    Script Date: 29-10-2015 18:38:59 ******/
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


USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Goal_Quater]    Script Date: 29-10-2015 18:37:53 ******/
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



USE [IT-Tracking]
GO

USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Goal_Master]    Script Date: 2015-10-30 14:52:19 ******/
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



SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Goal_Master]  WITH CHECK ADD  CONSTRAINT [FK_Goal_Master_QuaterID] FOREIGN KEY([QuaterId])
REFERENCES [dbo].[Goal_Quater] ([QuaterID])
GO

ALTER TABLE [dbo].[Goal_Master] CHECK CONSTRAINT [FK_Goal_Master_QuaterID]
GO

USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Goal_Rules]    Script Date: 29-10-2015 18:37:34 ******/
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

ALTER TABLE [dbo].[Goal_Rules]  WITH CHECK ADD  CONSTRAINT [FK_Goal_Rules_Goal_Master] FOREIGN KEY([GoalId])
REFERENCES [dbo].[Goal_Master] ([Goal_MasterID])
GO

ALTER TABLE [dbo].[Goal_Rules] CHECK CONSTRAINT [FK_Goal_Rules_Goal_Master]
GO

USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Resource_Goal]    Script Date: 29-10-2015 18:40:08 ******/
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

USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Resource_Goal_Performance]    Script Date: 29-10-2015 18:40:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Resource_Goal_Performance](
	[Resource_Goal_PerformanceID] [int] IDENTITY(1,1) NOT NULL,
	[Resource_Performance] [float] NOT NULL,
	[Resource_Rating] [float] NOT NULL,
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

USE [IT-Tracking]
GO

/****** Object:  Table [dbo].[Resource_Performance]    Script Date: 29-10-2015 18:40:33 ******/
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

