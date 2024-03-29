USE [master]
GO
/****** Object:  Database [finalCase]    Script Date: 22/02/2023 17:09:28 ******/
CREATE DATABASE [finalCase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'finalCase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\finalCase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'finalCase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\finalCase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [finalCase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [finalCase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [finalCase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [finalCase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [finalCase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [finalCase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [finalCase] SET ARITHABORT OFF 
GO
ALTER DATABASE [finalCase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [finalCase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [finalCase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [finalCase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [finalCase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [finalCase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [finalCase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [finalCase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [finalCase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [finalCase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [finalCase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [finalCase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [finalCase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [finalCase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [finalCase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [finalCase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [finalCase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [finalCase] SET RECOVERY FULL 
GO
ALTER DATABASE [finalCase] SET  MULTI_USER 
GO
ALTER DATABASE [finalCase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [finalCase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [finalCase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [finalCase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [finalCase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [finalCase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'finalCase', N'ON'
GO
ALTER DATABASE [finalCase] SET QUERY_STORE = OFF
GO
USE [finalCase]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 22/02/2023 17:09:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShoppingListID] [int] NOT NULL,
	[Name] [nvarchar](25) NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 22/02/2023 17:09:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShoppingListID] [int] NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Unit] [nvarchar](20) NULL,
	[Amount] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingList]    Script Date: 22/02/2023 17:09:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[isBought] [bit] NULL,
	[CreationDate] [datetime2](7) NULL,
	[CompletedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [ShoppingListID], [Name], [Description]) VALUES (1, 1, N'Gıda', N'Bizim Market')
INSERT [dbo].[Category] ([ID], [ShoppingListID], [Name], [Description]) VALUES (2, 2, N'Kırtasiye', N'Küçük çocuk')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [ShoppingListID], [Name], [Unit], [Amount]) VALUES (1, 1, N'Patates', N'kg', 1)
INSERT [dbo].[Product] ([ID], [ShoppingListID], [Name], [Unit], [Amount]) VALUES (2, 1, N'Domates', N'kg', 2)
INSERT [dbo].[Product] ([ID], [ShoppingListID], [Name], [Unit], [Amount]) VALUES (3, 2, N'Kalem', N'adet', 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ShoppingList] ON 

INSERT [dbo].[ShoppingList] ([ID], [isBought], [CreationDate], [CompletedDate]) VALUES (1, 0, CAST(N'2022-10-10T00:00:00.0000000' AS DateTime2), CAST(N'2022-11-11T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ShoppingList] ([ID], [isBought], [CreationDate], [CompletedDate]) VALUES (2, 0, CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-02-02T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[ShoppingList] OFF
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_ShoppingList] FOREIGN KEY([ShoppingListID])
REFERENCES [dbo].[ShoppingList] ([ID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_ShoppingList]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ShoppingList] FOREIGN KEY([ShoppingListID])
REFERENCES [dbo].[ShoppingList] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ShoppingList]
GO
USE [master]
GO
ALTER DATABASE [finalCase] SET  READ_WRITE 
GO
