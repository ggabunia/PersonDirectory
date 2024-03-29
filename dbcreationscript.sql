USE [master]
GO
/****** Object:  Database [PersonDiretoryDB]    Script Date: 09.09.2019 15:53:34 ******/
CREATE DATABASE [PersonDiretoryDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PersonDiretoryDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PersonDiretoryDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PersonDiretoryDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PersonDiretoryDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PersonDiretoryDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PersonDiretoryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PersonDiretoryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PersonDiretoryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PersonDiretoryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PersonDiretoryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PersonDiretoryDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET RECOVERY FULL 
GO
ALTER DATABASE [PersonDiretoryDB] SET  MULTI_USER 
GO
ALTER DATABASE [PersonDiretoryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PersonDiretoryDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PersonDiretoryDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PersonDiretoryDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PersonDiretoryDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PersonDiretoryDB', N'ON'
GO
ALTER DATABASE [PersonDiretoryDB] SET QUERY_STORE = OFF
GO
USE [PersonDiretoryDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [PersonDiretoryDB]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 09.09.2019 15:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConnectionTypes]    Script Date: 09.09.2019 15:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConnectionTypes](
	[ID] [int] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ConnectionTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLogs]    Script Date: 09.09.2019 15:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime2](7) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
 CONSTRAINT [PK_ErrorLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genders]    Script Date: 09.09.2019 15:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[ID] [int] NOT NULL,
	[GenderName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Genders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonConnections]    Script Date: 09.09.2019 15:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonConnections](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OriginatorID] [int] NOT NULL,
	[TargetID] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
 CONSTRAINT [PK_PersonConnections] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 09.09.2019 15:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[GenderID] [int] NOT NULL,
	[PersonalNr] [char](11) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[CityID] [int] NOT NULL,
	[Photo] [nvarchar](260) NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phones]    Script Date: 09.09.2019 15:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phones](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
	[Number] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhoneTypes]    Script Date: 09.09.2019 15:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneTypes](
	[ID] [int] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PhoneTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PersonConnections]  WITH CHECK ADD  CONSTRAINT [FK_PersonConnections_ConnectionTypes] FOREIGN KEY([TypeID])
REFERENCES [dbo].[ConnectionTypes] ([ID])
GO
ALTER TABLE [dbo].[PersonConnections] CHECK CONSTRAINT [FK_PersonConnections_ConnectionTypes]
GO
ALTER TABLE [dbo].[PersonConnections]  WITH CHECK ADD  CONSTRAINT [FK_PersonConnections_Persons] FOREIGN KEY([OriginatorID])
REFERENCES [dbo].[Persons] ([ID])
GO
ALTER TABLE [dbo].[PersonConnections] CHECK CONSTRAINT [FK_PersonConnections_Persons]
GO
ALTER TABLE [dbo].[PersonConnections]  WITH CHECK ADD  CONSTRAINT [FK_PersonConnections_Persons1] FOREIGN KEY([TargetID])
REFERENCES [dbo].[Persons] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PersonConnections] CHECK CONSTRAINT [FK_PersonConnections_Persons1]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_Cities] FOREIGN KEY([CityID])
REFERENCES [dbo].[Cities] ([ID])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_Cities]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_Genders] FOREIGN KEY([GenderID])
REFERENCES [dbo].[Genders] ([ID])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_Genders]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK_Phones_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Persons] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK_Phones_Persons]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK_Phones_PhoneTypes] FOREIGN KEY([TypeID])
REFERENCES [dbo].[PhoneTypes] ([ID])
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK_Phones_PhoneTypes]
GO
USE [master]
GO
ALTER DATABASE [PersonDiretoryDB] SET  READ_WRITE 
GO



INSERT INTO [dbo].[PhoneTypes] VALUES (1, N'მობილური'), (2,N'ოფისის'),(3,N'სახლის'); 
GO

INSERT INTO [dbo].[ConnectionTypes] VALUES (1,N'კოლეგა'), (2,N'ნაცნობი'), (3,N'ნათესავი'), (4,N'სხვა');
GO

INSERT INTO [dbo].[Cities] VALUES (N'თბილისი'), (N'ბათუმი'),(N'ქუთაისი'),(N'რუსთავი'),(N'ზუგდიდი'),(N'თელავი');
GO

INSERT INTO [dbo].[Genders] VALUES (1, N'მამრობითი'), (2,N'მდედრობითი');
GO