USE [master]
GO
/****** Object:  Database [HopuGoods]    Script Date: 2021/11/18 14:27:44 ******/
CREATE DATABASE [HopuGoods]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HopuGoods', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HopuGoods.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HopuGoods_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HopuGoods_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HopuGoods] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HopuGoods].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HopuGoods] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HopuGoods] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HopuGoods] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HopuGoods] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HopuGoods] SET ARITHABORT OFF 
GO
ALTER DATABASE [HopuGoods] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HopuGoods] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HopuGoods] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HopuGoods] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HopuGoods] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HopuGoods] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HopuGoods] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HopuGoods] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HopuGoods] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HopuGoods] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HopuGoods] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HopuGoods] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HopuGoods] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HopuGoods] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HopuGoods] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HopuGoods] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HopuGoods] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HopuGoods] SET RECOVERY FULL 
GO
ALTER DATABASE [HopuGoods] SET  MULTI_USER 
GO
ALTER DATABASE [HopuGoods] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HopuGoods] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HopuGoods] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HopuGoods] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [HopuGoods] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HopuGoods] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HopuGoods', N'ON'
GO
ALTER DATABASE [HopuGoods] SET QUERY_STORE = OFF
GO
USE [HopuGoods]
GO
/****** Object:  Table [dbo].[DepartmentInfo]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [nvarchar](16) NOT NULL,
	[DepartmentName] [nvarchar](16) NOT NULL,
	[LeaderId] [nvarchar](16) NOT NULL,
	[ParentId] [nvarchar](16) NOT NULL,
	[AddTime] [datetime] NULL,
 CONSTRAINT [PK_DepartmentInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Goods_Category]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goods_Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](16) NOT NULL,
	[Description] [nvarchar](32) NULL,
 CONSTRAINT [PK_Goods_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Goods_ConsumableInfo]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goods_ConsumableInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GoodsId] [nvarchar](16) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[Specification] [nvarchar](32) NULL,
	[Num] [float] NOT NULL,
	[Unit] [nvarchar](8) NOT NULL,
	[Money] [money] NULL,
	[WarningNum] [float] NULL,
	[DelFlag] [tinyint] NOT NULL,
	[AddTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Goods_Info] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Goods_ConsumableInput]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goods_ConsumableInput](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GoodsId] [nvarchar](16) NOT NULL,
	[Num] [float] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_Goods_ConsumableInput] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Goods_EquipmentInfo]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goods_EquipmentInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GoodsId] [nvarchar](16) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[Specification] [nvarchar](32) NULL,
	[Num] [float] NOT NULL,
	[Unit] [nvarchar](8) NOT NULL,
	[Money] [money] NULL,
	[DelFlag] [tinyint] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[Description] [nvarchar](16) NULL,
 CONSTRAINT [PK_Goods_EquipmentInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Goods_EquipmentInput]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goods_EquipmentInput](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GoodsId] [nvarchar](16) NOT NULL,
	[Num] [float] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_Goods_EquipmentInput] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Decription] [nvarchar](128) NULL,
	[AddTime] [datetime] NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PowerInfo]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PowerInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PowerId] [nvarchar](16) NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[Description] [nvarchar](32) NULL,
	[Sort] [float] NULL,
	[ActionUrl] [nvarchar](128) NOT NULL,
	[ParentId] [nvarchar](16) NULL,
	[MenuIconUrl] [nvarchar](32) NULL,
	[HttpMethod] [nvarchar](4) NULL,
 CONSTRAINT [PK_PowerInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[R_RoleInfo_PowerInfo]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[R_RoleInfo_PowerInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](16) NOT NULL,
	[PowerId] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_R_RoleInfo_PowerInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[R_UserInfo_RoleInfo]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[R_UserInfo_RoleInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](16) NOT NULL,
	[RoleId] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_R_UserInfo_RoleInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleInfo]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](16) NOT NULL,
	[RoleName] [nvarchar](16) NOT NULL,
	[Description] [nvarchar](32) NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[DelFlag] [tinyint] NOT NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_RoleInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](16) NOT NULL,
	[UserName] [nvarchar](16) NOT NULL,
	[PhoneNum] [nvarchar](16) NULL,
	[Email] [nvarchar](32) NULL,
	[DepartmentId] [nvarchar](16) NOT NULL,
	[Sex] [tinyint] NOT NULL,
	[PassWord] [char](32) NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[DelFlag] [tinyint] NOT NULL,
	[DelTime] [datetime] NULL,
	[Url] [varchar](50) NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkFlow_Instance]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkFlow_Instance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstanceId] [nvarchar](64) NOT NULL,
	[ModelId] [int] NOT NULL,
	[UserId] [nvarchar](16) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[Description] [nvarchar](64) NULL,
	[Reason] [nvarchar](64) NULL,
	[NextReviewer] [nvarchar](16) NULL,
	[AddTime] [datetime] NOT NULL,
	[OutNum] [float] NOT NULL,
	[OutGoodsId] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_WorkFlow_Instance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkFlow_InstanceStep]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkFlow_InstanceStep](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstanceId] [nvarchar](64) NOT NULL,
	[ReviewerId] [nvarchar](16) NOT NULL,
	[ReviewReason] [nvarchar](64) NULL,
	[ReviewStatus] [tinyint] NOT NULL,
	[ReviewTime] [datetime] NULL,
	[NextReviewerId] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_WorkFlow_InstanceStep] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkFlow_Model]    Script Date: 2021/11/18 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkFlow_Model](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](32) NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[DelFlag] [tinyint] NOT NULL,
	[Description] [nvarchar](64) NULL,
 CONSTRAINT [PK_WorkFlow_Model] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DepartmentInfo] ON 

INSERT [dbo].[DepartmentInfo] ([ID], [DepartmentId], [DepartmentName], [LeaderId], [ParentId], [AddTime]) VALUES (1, N'D1001', N'总裁合伙人', N'U1001', N'D1001', CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[DepartmentInfo] ([ID], [DepartmentId], [DepartmentName], [LeaderId], [ParentId], [AddTime]) VALUES (2, N'D1002', N'实训中心学术部', N'U1002', N'D1001', CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[DepartmentInfo] ([ID], [DepartmentId], [DepartmentName], [LeaderId], [ParentId], [AddTime]) VALUES (4, N'D1003', N'实训中心财务部', N'U1008', N'D1001', CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[DepartmentInfo] ([ID], [DepartmentId], [DepartmentName], [LeaderId], [ParentId], [AddTime]) VALUES (6, N'D1004', N'实训中心就业部', N'U1010', N'D1001', CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[DepartmentInfo] ([ID], [DepartmentId], [DepartmentName], [LeaderId], [ParentId], [AddTime]) VALUES (7, N'D1005', N'实训中心督导部', N'U1009', N'D1001', CAST(N'2020-03-06T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[DepartmentInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[Goods_Category] ON 

INSERT [dbo].[Goods_Category] ([Id], [CategoryName], [Description]) VALUES (1, N'办公用品', N'办公用品（编号B开头）')
INSERT [dbo].[Goods_Category] ([Id], [CategoryName], [Description]) VALUES (2, N'电子设备', N'电子设备（编号D开头）')
INSERT [dbo].[Goods_Category] ([Id], [CategoryName], [Description]) VALUES (3, N'生活用品', N'生活用品（编号S开头）')
INSERT [dbo].[Goods_Category] ([Id], [CategoryName], [Description]) VALUES (4, N'其他', N'其他（编号O开头）')
SET IDENTITY_INSERT [dbo].[Goods_Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Goods_ConsumableInfo] ON 

INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (1, N'S1001', 3, N'纸巾', N'清风', 89, N'包', 3.0000, 5, 0, CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (3, N'B1002', 1, N'水性笔', N'晨光', 50, N'支', 1.5000, 5, 0, CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (4, N'B1003', 1, N'笔记本', NULL, 38, N'本', 3.0000, 5, 0, CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (5, N'B1004', 1, N'白板笔', NULL, 20, N'支', 5.0000, 5, 0, CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (6, N'B1005', 1, N'便签纸贴', NULL, 20, N'本', 2.0000, 5, 0, CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (7, N'S1002', 3, N'口罩', NULL, 137, N'片', 5.0000, 5, 0, CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (8, N'S1003', 3, N'酒精', N'200ml', 10, N'瓶', 3.0000, 2, 0, CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (9, N'S1004', 3, N'洗手液', N'200ml', 10, N'瓶', 5.0000, 2, 0, CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (10, N'B1006', 1, N'票据本', NULL, 20, N'本', 2.0000, 5, 0, CAST(N'2020-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (11, N'B1007', 1, N'小票据本', N'', 40, N'本', 3.0000, 5, 0, CAST(N'2020-03-12T16:36:36.197' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (12, N'B10078', 1, N'大票据本', N'', 10, N'本', 3.0000, 5, 0, CAST(N'2020-03-12T16:36:36.220' AS DateTime))
INSERT [dbo].[Goods_ConsumableInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [WarningNum], [DelFlag], [AddTime]) VALUES (13, N'B1008', 1, N'大大票据本', NULL, 31, N'本', 0.0000, 5, 0, CAST(N'2020-03-12T16:48:03.773' AS DateTime))
SET IDENTITY_INSERT [dbo].[Goods_ConsumableInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[Goods_ConsumableInput] ON 

INSERT [dbo].[Goods_ConsumableInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (1, N'S1001', 10, CAST(N'2020-03-12T16:54:15.050' AS DateTime), N'U1008')
INSERT [dbo].[Goods_ConsumableInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (2, N'S1002', 10, CAST(N'2020-03-12T16:54:15.080' AS DateTime), N'U1008')
INSERT [dbo].[Goods_ConsumableInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (3, N'B1007', 10, CAST(N'2020-03-12T16:54:15.083' AS DateTime), N'U1008')
INSERT [dbo].[Goods_ConsumableInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (4, N'B1008', 10, CAST(N'2020-03-12T16:54:15.083' AS DateTime), N'U1008')
INSERT [dbo].[Goods_ConsumableInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (5, N'S1001', 10, CAST(N'2020-03-12T16:54:28.587' AS DateTime), N'U1008')
INSERT [dbo].[Goods_ConsumableInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (6, N'S1002', 10, CAST(N'2020-03-12T16:54:28.590' AS DateTime), N'U1008')
INSERT [dbo].[Goods_ConsumableInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (7, N'B1007', 10, CAST(N'2020-03-12T16:54:28.597' AS DateTime), N'U1008')
INSERT [dbo].[Goods_ConsumableInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (8, N'B1008', 10, CAST(N'2020-03-12T16:54:28.597' AS DateTime), N'U1008')
SET IDENTITY_INSERT [dbo].[Goods_ConsumableInput] OFF
GO
SET IDENTITY_INSERT [dbo].[Goods_EquipmentInfo] ON 

INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (1, N'F1001', 2, N'赛扬电脑主机', NULL, 188, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.080' AS DateTime), NULL)
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (2, N'F1015', 2, N'键盘', N'', 249, N'个', 0.0000, 0, CAST(N'2020-03-13T14:51:47.147' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (3, N'F1014', 2, N'鼠标', N'', 256, N'个', 0.0000, 0, CAST(N'2020-03-13T14:51:47.147' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (4, N'F1018', 3, N'电风扇', N'', 24, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.150' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (5, N'F1013', 2, N'交换机', N'', 6, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.147' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (6, N'F1024', 3, N'电吸尘器', N'', 1, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.153' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (7, N'F1012', 2, N'路由器', N'', 6, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.147' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (8, N'F1011', 2, N'音响', N'', 5, N'套', 0.0000, 0, CAST(N'2020-03-13T14:51:47.143' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (9, N'F1019', 3, N'微波炉', N'', 1, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.150' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (10, N'F1010', 2, N'话筒', N'', 2, N'个', 0.0000, 0, CAST(N'2020-03-13T14:51:47.143' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (11, N'F1009', 2, N'投影仪（包括线）', N'', 1, N'套', 0.0000, 0, CAST(N'2020-03-13T14:51:47.143' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (12, N'F1017', 3, N'电热水壶', N'', 2, N'个', 0.0000, 0, CAST(N'2020-03-13T14:51:47.150' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (13, N'F1008', 2, N'主机电源线', N'', 240, N'条', 0.0000, 0, CAST(N'2020-03-13T14:51:47.143' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (14, N'F1020', 2, N'打印机', N'', 1, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.150' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (15, N'F1007', 2, N'显示器电源线', N'', 233, N'条', 0.0000, 0, CAST(N'2020-03-13T14:51:47.140' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (16, N'F1002', 2, N'I3电脑', N'', 42, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.130' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (17, N'F1023', 2, N'笔记本电源线', N'', 14, N'条', 0.0000, 0, CAST(N'2020-03-13T14:51:47.153' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (18, N'F1003', 2, N'新I3电脑', N'', 9, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.133' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (19, N'F1022', 2, N'主机电源线', N'', 4, N'个', 0.0000, 0, CAST(N'2020-03-13T14:51:47.153' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (20, N'F1004', 2, N'笔记本电脑', N'', 16, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.133' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (21, N'F1006', 2, N'vga线', N'', 238, N'条', 0.0000, 0, CAST(N'2020-03-13T14:51:47.140' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (22, N'F1021', 2, N'散装固态银盘', N'', 4, N'个', 0.0000, 0, CAST(N'2020-03-13T14:51:47.153' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (23, N'F1005', 2, N'显示器', N'', 246, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.140' AS DateTime), N'')
INSERT [dbo].[Goods_EquipmentInfo] ([Id], [GoodsId], [CategoryId], [Name], [Specification], [Num], [Unit], [Money], [DelFlag], [AddTime], [Description]) VALUES (24, N'F1016', 3, N'热水器', N'', 1, N'台', 0.0000, 0, CAST(N'2020-03-13T14:51:47.150' AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[Goods_EquipmentInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[Goods_EquipmentInput] ON 

INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (1, N'F1021', 4, CAST(N'2020-03-13T14:51:47.153' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (2, N'F1018', 24, CAST(N'2020-03-13T14:51:47.150' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (3, N'F1020', 1, CAST(N'2020-03-13T14:51:47.150' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (4, N'F1022', 4, CAST(N'2020-03-13T14:51:47.153' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (5, N'F1019', 1, CAST(N'2020-03-13T14:51:47.150' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (6, N'F1017', 2, CAST(N'2020-03-13T14:51:47.150' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (7, N'F1023', 14, CAST(N'2020-03-13T14:51:47.153' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (8, N'F1012', 6, CAST(N'2020-03-13T14:51:47.147' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (9, N'F1015', 249, CAST(N'2020-03-13T14:51:47.147' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (10, N'F1014', 256, CAST(N'2020-03-13T14:51:47.147' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (11, N'F1013', 6, CAST(N'2020-03-13T14:51:47.147' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (12, N'F1011', 5, CAST(N'2020-03-13T14:51:47.143' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (13, N'F1010', 2, CAST(N'2020-03-13T14:51:47.143' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (14, N'F1009', 1, CAST(N'2020-03-13T14:51:47.143' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (15, N'F1008', 240, CAST(N'2020-03-13T14:51:47.143' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (16, N'F1007', 233, CAST(N'2020-03-13T14:51:47.140' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (17, N'F1006', 238, CAST(N'2020-03-13T14:51:47.140' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (18, N'F1005', 246, CAST(N'2020-03-13T14:51:47.140' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (19, N'F1004', 16, CAST(N'2020-03-13T14:51:47.133' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (20, N'F1003', 9, CAST(N'2020-03-13T14:51:47.133' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (21, N'F1002', 42, CAST(N'2020-03-13T14:51:47.130' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (22, N'F1001', 188, CAST(N'2020-03-13T14:51:47.120' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (23, N'F1016', 1, CAST(N'2020-03-13T14:51:47.150' AS DateTime), N'U1003')
INSERT [dbo].[Goods_EquipmentInput] ([Id], [GoodsId], [Num], [AddTime], [AddUserId]) VALUES (24, N'F1024', 1, CAST(N'2020-03-13T14:51:47.153' AS DateTime), N'U1003')
SET IDENTITY_INSERT [dbo].[Goods_EquipmentInput] OFF
GO
SET IDENTITY_INSERT [dbo].[PowerInfo] ON 

INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (1, N'1', N'员工信息管理', N'目录,主页父节点为系统信息管理', NULL, N'/User/Index', N'-1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (3, N'2', N'角色信息管理', N'目录,主页父节点为系统信息管理', NULL, N'/Role/RoleIndex', N'-1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (4, N'3', N'部门信息管理', N'目录,主页父节点为系统信息管理', NULL, N'/Department/DepartmentIndex', N'-1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (5, N'4', N'耗材领用', N'目录,主页父节点为物资领用管理', NULL, N'/Goods_Consumable/Index', N'-2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (6, N'5', N'耗材审核', N'目录,主页父节点为物资审核管理', NULL, N'/Goods_Consumable/ReviewIndex', N'-3', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (7, N'6', N'耗材入库', N'目录,主页父节点为物资资源管理', NULL, N'/Goods_Consumable/InputIndex', N'-4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (9, N'7', N'耗材出库', N'目录,主页父节点为物资资源管理', NULL, N'/Goods_Consumable/OutputIndex', N'-4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (11, N'8', N'非耗材借用', N'目录,主页父节点为物资领用管理', NULL, N'/Goods_Equipment/Index', N'-2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (12, N'9', N'非耗材审核', N'目录,主页父节点为物资审核管理', NULL, N'/Goods_Equipment/ReviewIndex', N'-3', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (13, N'10', N'非耗材入库', N'目录,主页父节点为物资资源管理', NULL, N'/Goods_Equipment/InputIndex', N'-4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (14, N'11', N'非耗材出库', N'目录,主页父节点为物资资源管理', NULL, N'/Goods_Equipment/OutputIndex', N'-4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (15, N'12', N'非耗材归还', N'目录,主页父节点为物资资源管理', NULL, N'/Goods_Equipment/ReturnIndex', N'-4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (16, N'13', N'数据分析统计', N'目录,主页父节点为物资资源管理', NULL, N'/Statistics/Index', N'-4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (18, N'14', N'部门信息页面', N'页面', NULL, N'/Department/DepartmentIndex', N'3', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (20, N'15', N'部门信息获取功能', N'非页面', NULL, N'/Department/GetJsonList', N'3', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (22, N'16', N'角色信息页面', N'页面', NULL, N'/Role/RoleIndex', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (23, N'17', N'角色添加页面', N'页面', NULL, N'/Role/RoleAdd', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (24, N'18', N'角色修改页面', N'页面', NULL, N'/Role/UpdateIndex', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (25, N'19', N'角色分配页面', N'页面', NULL, N'/Role/ConfigIndex', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (26, N'20', N'权限分配页面', N'页面', NULL, N'/Role/PowerIndex', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (27, N'21', N'角色信息获取功能(不分页)', N'非页面', NULL, N'/Role/GetJsonList', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (28, N'22', N'分配角色功能', N'非页面', NULL, N'/Role/ConfigRole', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (29, N'23', N'角色信息获取功能', N'非页面', NULL, N'/Role/GetJsonList', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (30, N'24', N'角色添加功能', N'非页面', NULL, N'/Role/Add', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (32, N'25', N'角色更新功能', N'非页面', NULL, N'/Role/Update', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (33, N'26', N'开启/禁用角色功能', N'非页面', NULL, N'/Role/ForbidRole', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (34, N'27', N'判断角色Id是否重复', N'非页面', NULL, N'/Role/CheckRepeat', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (35, N'28', N'获取角色对应的权限', N'非页面', NULL, N'/Role/GetPowerList', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (37, N'29', N'员工信息页面', N'页面', NULL, N'/User/Index', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (38, N'30', N'员工添加页面', N'页面', NULL, N'/User/AddIndex', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (40, N'31', N'员工修改页面', N'页面', NULL, N'/User/UpdateIndex', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (42, N'32', N'员工信息获取功能', N'非页面', NULL, N'/User/GetJsonList', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (43, N'33', N'员工信息添加功能', N'非页面', NULL, N'/User/Add', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (44, N'34', N'员工信息修改功能', N'非页面', NULL, N'/User/Update', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (45, N'35', N'开启/禁用员工功能', N'非页面', NULL, N'/User/ForbidUser', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (46, N'36', N'判断员工Id是否重复', N'非页面', NULL, N'/User/CheckRepeat', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (47, N'37', N'员工密码重置功能', N'非页面', NULL, N'/User/RePwd', N'1', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (48, N'38', N'耗材领用页面', N'页面', NULL, N'/Goods_Consumable/Index', N'4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (49, N'39', N'耗材领用申请页面', N'页面', NULL, N'/Goods_Consumable/ApplyingIndex', N'4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (50, N'40', N'耗材审核页面', N'页面', NULL, N'/Goods_Consumable/ReviewIndex', N'5', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (51, N'41', N'耗材审核提交页面', N'页面', NULL, N'/Goods_Consumable/ReviewingIndex', N'5', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (52, N'42', N'耗材入库页面', N'页面', NULL, N'/Goods_Consumable/InputIndex', N'6', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (53, N'43', N'耗材出库页面', N'页面', NULL, N'/Goods_Consumable/OutputIndex', N'7', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (54, N'44', N'耗材更新页面', N'页面', NULL, N'/Goods_Consumable/UpdateIndex', N'6', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (56, N'45', N'耗材信息获取功能', N'非页面(领用)', NULL, N'/Goods_Consumable/GetJsonList', N'4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (57, N'46', N'耗材信息获取功能', N'非页面(入库)', NULL, N'/Goods_Consumable/GetJsonList', N'6', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (58, N'47', N'耗材Excel导入功能', N'非页面', NULL, N'/Goods_Consumable/ExcelUpload', N'6', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (59, N'48', N'耗材Excel导出功能', N'非页面', NULL, N'/Goods_Consumable/ExcelDownload', N'6', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (60, N'49', N'耗材检查库存功能', N'非页面(领用)', NULL, N'/Goods_Consumable/CheckStore', N'4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (61, N'50', N'耗材检查库存功能', N'非页面(出库)', NULL, N'/Goods_Consumable/CheckStore', N'7', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (63, N'51', N'耗材更新功能', N'非页面', NULL, N'/Goods_Consumable/Update', N'6', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (64, N'52', N'非耗材借用页面', N'页面', NULL, N'/Goods_Equipment/Index', N'8', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (65, N'53', N'非耗材借用申请页面', N'页面', NULL, N'/Goods_Equipment/ApplyingIndex', N'8', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (66, N'54', N'非耗材审核页面', N'页面', NULL, N'/Goods_Equipment/ReviewIndex', N'9', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (67, N'55', N'非耗材审核提交页面', N'页面', NULL, N'/Goods_Equipment/ReviewingIndex', N'9', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (68, N'56', N'非耗材出库页面', N'页面', NULL, N'/Goods_Equipment/OutputIndex', N'11', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (69, N'57', N'非耗材归还页面', N'页面', NULL, N'/Goods_Equipment/ReturnIndex', N'12', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (70, N'58', N'非耗材入库页面', N'页面', NULL, N'/Goods_Equipment/InputIndex', N'10', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (71, N'59', N'非耗材更新页面', N'页面', NULL, N'/Goods_Equipment/UpdateIndex', N'10', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (72, N'60', N'非耗材信息获取功能', N'非页面(借用)', NULL, N'/Goods_Equipment/GetJsonList', N'8', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (73, N'61', N'非耗材信息获取功能', N'非页面(入库)', NULL, N'/Goods_Equipment/GetJsonList', N'10', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (74, N'62', N'非耗材Excel导入功能', N'非页面', NULL, N'/Goods_Equipment/ExcelUpload', N'10', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (75, N'63', N'非耗材Excel导出功能', N'非页面', NULL, N'/Goods_Equipment/ExcelDownload', N'10', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (76, N'64', N'非耗材检查库存功能', N'非页面(借用)', NULL, N'/Goods_Equipment/CheckStore', N'8', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (77, N'65', N'非耗材检查库存功能', N'非页面(出库)', NULL, N'/Goods_Equipment/CheckStore', N'11', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (78, N'66', N'非耗材更新功能', N'非页面', NULL, N'/Goods_Equipment/Update', N'10', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (79, N'67', N'签核步骤查看页面', N'页面(耗材)', NULL, N'/WorkFlow/WorkFlowStepIndex', N'4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (80, N'68', N'签核步骤查看页面', N'页面(非耗材)', NULL, N'/WorkFlow/WorkFlowStepIndex', N'8', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (81, N'69', N'耗材领用流程图页面', N'页面', NULL, N'/WorkFlow/WorkFlowGoods_ConsumableIndex', N'4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (82, N'70', N'非耗材借用流程图页面', N'页面', NULL, N'/WorkFlow/WorkFlowGoods_EquipmentIndex', N'8', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (83, N'71', N'耗材领取记录获取功能', N'非页面', NULL, N'/WorkFlow/GetConsumableApplyByUser', N'4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (84, N'72', N'耗材待审核记录获取功能', N'非页面', NULL, N'/WorkFlow/GetConsumableApplyByReviewer', N'5', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (85, N'73', N'耗材待出库记录获取功能', N'非页面', NULL, N'/WorkFlow/GetConsumableOutput', N'7', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (86, N'74', N'耗材领用申请提交功能', N'非页面', NULL, N'/WorkFlow/ApplyConsumable', N'4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (87, N'75', N'耗材领用申请撤销功能', N'非页面', NULL, N'/WorkFlow/RevokeConsumable', N'4', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (88, N'76', N'耗材审核功能', N'非页面', NULL, N'/WorkFlow/ReviewConsumable', N'5', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (89, N'77', N'耗材出库功能', N'非页面', NULL, N'/WorkFlow/OutpuConsumable', N'7', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (90, N'78', N'非耗材借用记录获取功能', N'非页面', NULL, N'/WorkFlow/GetEquipmentApplyByUser', N'8', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (91, N'79', N'非耗材待审核记录获取', N'非页面', NULL, N'/WorkFlow/GetEquipmentApplyByReviewer', N'9', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (92, N'80', N'非耗材待出库记录获取', N'非页面', NULL, N'/WorkFlow/GetEquipmentOutput', N'11', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (93, N'81', N'非耗材待归还记录获取', N'非页面', NULL, N'/WorkFlow/GetEquipmentIsLent', N'12', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (94, N'82', N'非耗材借用申请提交功能', N'非页面', NULL, N'/WorkFlow/ApplyEquipment', N'8', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (95, N'83', N'非耗材借用申请撤销功能', N'非页面', NULL, N'/WorkFlow/RevokeEquipment', N'8', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (98, N'84', N'非耗材审核功能', N'非页面', NULL, N'/WorkFlow/ReviewEquipment', N'9', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (99, N'85', N'非耗材出库功能', N'非页面', NULL, N'/WorkFlow/OutputEquipment', N'11', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (100, N'86', N'非耗材归还功能', N'非页面', NULL, N'/WorkFlow/ReturnEquipment', N'12', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (101, N'87', N'数据统计页面', N'页面', NULL, N'/Statistics/Index', N'13', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (102, N'88', N'权限分配功能', N'非页面', NULL, N'/Role/SetPower', N'2', NULL, NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (103, N'-1', N'系统信息管理', N'目录', NULL, N' ', N'0', N'layui-icon layui-icon-home', NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (104, N'-2', N'物资领取管理', N'目录', NULL, N' ', N'0', N'layui-icon layui-icon-diamond', NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (105, N'-3', N'物资审核管理', N'目录', NULL, N' ', N'0', N'layui-icon layui-icon-diamond', NULL)
INSERT [dbo].[PowerInfo] ([Id], [PowerId], [Name], [Description], [Sort], [ActionUrl], [ParentId], [MenuIconUrl], [HttpMethod]) VALUES (106, N'-4', N'物资资源管理', N'目录', NULL, N' ', N'0', N'layui-icon layui-icon-diamond', NULL)
SET IDENTITY_INSERT [dbo].[PowerInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[R_RoleInfo_PowerInfo] ON 

INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (304, N'R1004', N'-2')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (305, N'R1004', N'55')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (306, N'R1004', N'54')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (307, N'R1004', N'9')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (308, N'R1004', N'76')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (309, N'R1004', N'72')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (310, N'R1004', N'41')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (311, N'R1004', N'40')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (312, N'R1004', N'5')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (313, N'R1004', N'-3')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (314, N'R1004', N'83')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (315, N'R1004', N'82')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (316, N'R1004', N'78')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (317, N'R1004', N'70')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (318, N'R1004', N'68')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (319, N'R1004', N'64')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (320, N'R1004', N'60')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (321, N'R1004', N'53')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (322, N'R1004', N'52')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (323, N'R1004', N'8')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (324, N'R1004', N'75')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (325, N'R1004', N'74')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (326, N'R1004', N'71')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (327, N'R1004', N'69')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (328, N'R1004', N'67')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (329, N'R1004', N'49')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (330, N'R1004', N'45')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (331, N'R1004', N'39')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (332, N'R1004', N'38')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (333, N'R1004', N'4')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (334, N'R1004', N'79')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (335, N'R1004', N'84')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (336, N'R1003', N'-4')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (337, N'R1003', N'86')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (338, N'R1003', N'81')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (339, N'R1003', N'57')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (340, N'R1003', N'12')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (341, N'R1003', N'85')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (342, N'R1003', N'80')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (343, N'R1003', N'65')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (344, N'R1003', N'56')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (345, N'R1003', N'11')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (346, N'R1003', N'66')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (347, N'R1003', N'63')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (348, N'R1003', N'62')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (349, N'R1003', N'61')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (350, N'R1003', N'13')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (351, N'R1003', N'59')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (352, N'R1003', N'10')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (353, N'R1003', N'77')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (354, N'R1003', N'73')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (355, N'R1003', N'50')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (356, N'R1003', N'43')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (357, N'R1003', N'7')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (358, N'R1003', N'51')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (359, N'R1003', N'48')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (360, N'R1003', N'47')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (361, N'R1003', N'46')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (362, N'R1003', N'44')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (363, N'R1003', N'42')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (364, N'R1003', N'6')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (365, N'R1003', N'58')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (366, N'R1003', N'87')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (367, N'R1002', N'-2')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (368, N'R1002', N'78')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (369, N'R1002', N'70')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (370, N'R1002', N'68')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (371, N'R1002', N'64')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (372, N'R1002', N'60')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (373, N'R1002', N'53')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (374, N'R1002', N'52')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (375, N'R1002', N'8')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (376, N'R1002', N'82')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (377, N'R1002', N'75')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (378, N'R1002', N'71')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (379, N'R1002', N'69')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (380, N'R1002', N'67')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (381, N'R1002', N'49')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (382, N'R1002', N'45')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (383, N'R1002', N'39')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (384, N'R1002', N'38')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (385, N'R1002', N'4')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (386, N'R1002', N'74')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (387, N'R1002', N'83')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (388, N'R1001', N'-1')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (389, N'R1001', N'3')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (390, N'R1001', N'88')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (391, N'R1001', N'28')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (392, N'R1001', N'27')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (393, N'R1001', N'26')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (394, N'R1001', N'25')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (395, N'R1001', N'24')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (396, N'R1001', N'23')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (397, N'R1001', N'20')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (398, N'R1001', N'18')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (399, N'R1001', N'17')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (400, N'R1001', N'16')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (401, N'R1001', N'14')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (402, N'R1001', N'2')
GO
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (403, N'R1001', N'36')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (404, N'R1001', N'35')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (405, N'R1001', N'34')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (406, N'R1001', N'33')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (407, N'R1001', N'32')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (408, N'R1001', N'31')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (409, N'R1001', N'30')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (410, N'R1001', N'29')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (411, N'R1001', N'22')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (412, N'R1001', N'21')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (413, N'R1001', N'19')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (414, N'R1001', N'1')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (415, N'R1001', N'37')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (416, N'R1001', N'15')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (417, N'test2', N'-1')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (418, N'test2', N'1')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (419, N'test2', N'29')
INSERT [dbo].[R_RoleInfo_PowerInfo] ([Id], [RoleId], [PowerId]) VALUES (420, N'test2', N'30')
SET IDENTITY_INSERT [dbo].[R_RoleInfo_PowerInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[R_UserInfo_RoleInfo] ON 

INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (8, N'U1003', N'R1001')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (9, N'U1003', N'R1002')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (10, N'U1003', N'R1003')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (11, N'U1003', N'R1004')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (12, N'U1001', N'R1004')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (13, N'U1002', N'R1004')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (14, N'U1004', N'R1002')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (15, N'U1005', N'R1002')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (16, N'U1006', N'R1002')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (17, N'U1007', N'R1002')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (20, N'U1009', N'R1002')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (21, N'U1009', N'R1004')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (22, N'U1010', N'R1002')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (23, N'U1010', N'R1004')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (24, N'U1008', N'R1002')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (25, N'U1008', N'R1003')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (26, N'U1008', N'R1004')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (27, N'test', N'R1003')
INSERT [dbo].[R_UserInfo_RoleInfo] ([Id], [UserId], [RoleId]) VALUES (28, N'test', N'R1002')
SET IDENTITY_INSERT [dbo].[R_UserInfo_RoleInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleInfo] ON 

INSERT [dbo].[RoleInfo] ([Id], [RoleId], [RoleName], [Description], [AddTime], [DelFlag], [DelTime]) VALUES (1, N'R1001', N'系统管理员', N'应具有部门、用户、角色信息管理的权限', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[RoleInfo] ([Id], [RoleId], [RoleName], [Description], [AddTime], [DelFlag], [DelTime]) VALUES (4, N'R1002', N'普通员工', N'应有耗材/设备申请领取的权限', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[RoleInfo] ([Id], [RoleId], [RoleName], [Description], [AddTime], [DelFlag], [DelTime]) VALUES (5, N'R1003', N'仓库/财务管理员', N'应有耗材/设备管理的权限（包括入库出库）', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[RoleInfo] ([Id], [RoleId], [RoleName], [Description], [AddTime], [DelFlag], [DelTime]) VALUES (6, N'R1004', N'部门领导', N'应有耗材/设备领取及审核的权限', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[RoleInfo] ([Id], [RoleId], [RoleName], [Description], [AddTime], [DelFlag], [DelTime]) VALUES (7, N'Test', N'testq', N'tere1', CAST(N'2020-03-06T09:55:14.453' AS DateTime), 1, CAST(N'2020-03-06T09:55:14.453' AS DateTime))
INSERT [dbo].[RoleInfo] ([Id], [RoleId], [RoleName], [Description], [AddTime], [DelFlag], [DelTime]) VALUES (8, N'test2', N'23', N'23', CAST(N'2020-03-06T10:01:57.290' AS DateTime), 1, CAST(N'2020-03-06T10:01:57.290' AS DateTime))
SET IDENTITY_INSERT [dbo].[RoleInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (1, N'U1001', N'小龚龚', N'15245452245', N'11111@qq.com', N'D1001', 1, N'5fa285e1bebe0a6623e33afc04a1fbd5', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (5, N'U1002', N'小郭郭', NULL, NULL, N'D1002', 1, N'5fa285e1bebe0a6623e33afc04a1fbd5', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (7, N'U1003', N'小高高', NULL, NULL, N'D1002', 1, N'5fa285e1bebe0a6623e33afc04a1fbd5', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (8, N'U1004', N'小黄黄', NULL, NULL, N'D1002', 1, N'5fa285e1bebe0a6623e33afc04a1fbd5', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (10, N'U1005', N'小周周', NULL, NULL, N'D1002', 1, N'5fa285e1bebe0a6623e33afc04a1fbd5', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (11, N'U1006', N'小冯冯', NULL, NULL, N'D1002', 1, N'5fa285e1bebe0a6623e33afc04a1fbd5', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (12, N'U1007', N'小詹詹', NULL, NULL, N'D1002', 2, N'5fa285e1bebe0a6623e33afc04a1fbd5', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (13, N'U1008', N'小张张', NULL, NULL, N'D1003', 2, N'5fa285e1bebe0a6623e33afc04a1fbd5', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (14, N'U1009', N'小韦韦', NULL, NULL, N'D1005', 2, N'5fa285e1bebe0a6623e33afc04a1fbd5', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (15, N'U1010', N'小黎黎', NULL, NULL, N'D1004', 2, N'5fa285e1bebe0a6623e33afc04a1fbd5', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[UserInfo] ([Id], [UserId], [UserName], [PhoneNum], [Email], [DepartmentId], [Sex], [PassWord], [AddTime], [DelFlag], [DelTime], [Url]) VALUES (18, N'test', N'111111111111', N'15211215143', N'123@qq.com', N'D1003', 1, N'202cb962ac59075b964b07152d234b70', CAST(N'2020-03-05T15:23:38.687' AS DateTime), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkFlow_Instance] ON 

INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (1, N'U1003YYYY03DD093222', 2, N'U1003', 2, NULL, N'没有水性笔用啦', N'U1002', CAST(N'2020-03-11T09:32:22.803' AS DateTime), 1, N'B1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (2, N'U100320200311093933', 2, N'U1003', 2, NULL, N'领用半个月的口罩', N'U1002', CAST(N'2020-03-11T09:39:33.513' AS DateTime), 10, N'S1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (3, N'U100320200311094601', 2, N'U1003', 4, NULL, N'颁发优秀员工礼品', N'U1008', CAST(N'2020-03-11T09:46:01.660' AS DateTime), 10, N'B1003')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (4, N'U100320200311094920', 2, N'U1003', 3, NULL, N'已用完', N'U1002', CAST(N'2020-03-11T09:49:20.373' AS DateTime), 1, N'B1004')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (5, N'U100320200311095054', 2, N'U1003', 4, NULL, N'每月领一包', N'U1008', CAST(N'2020-03-11T09:50:54.060' AS DateTime), 1, N'S1001')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (6, N'U100320200311163255', 2, N'U1003', 3, NULL, N'领来用用', N'U1002', CAST(N'2020-03-11T16:32:55.613' AS DateTime), 1, N'S1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (7, N'U100220200311170517', 2, N'U1002', 4, NULL, N'领一个', N'U1008', CAST(N'2020-03-11T17:05:17.977' AS DateTime), 1, N'S1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (8, N'U100220200311170542', 2, N'U1002', 4, NULL, N'领10个', N'U1008', CAST(N'2020-03-11T17:05:42.527' AS DateTime), 10, N'S1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (9, N'U100120200311170753', 2, N'U1001', 4, NULL, N'拿', N'U1008', CAST(N'2020-03-11T17:07:53.537' AS DateTime), 1, N'B1003')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (10, N'U100220200312100704', 2, N'U1002', 4, NULL, N'口罩1个', N'U1008', CAST(N'2020-03-12T10:07:04.410' AS DateTime), 1, N'S1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (11, N'U100320200313105939', 2, N'U1003', 4, NULL, N'领一本', N'U1008', CAST(N'2020-03-13T10:59:39.830' AS DateTime), 1, N'B1003')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (12, N'U100320200313110008', 2, N'U1003', 4, NULL, N'领10个', N'U1008', CAST(N'2020-03-13T11:00:08.313' AS DateTime), 10, N'S1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (13, N'U100320200313154652', 3, N'U1003', 5, NULL, N'做服务器', N'U1008', CAST(N'2020-03-13T15:46:52.447' AS DateTime), 1, N'F1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (14, N'U100320200313154721', 3, N'U1003', 2, NULL, N'借来！', N'U1002', CAST(N'2020-03-13T15:47:21.197' AS DateTime), 1, N'F1001')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (15, N'U100220200313155449', 3, N'U1002', 2, NULL, N'来3台', N'U1002', CAST(N'2020-03-13T15:54:49.277' AS DateTime), 3, N'F1001')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (16, N'U100420200320161706', 3, N'U1004', 0, NULL, N'服务器配置', N'U1002', CAST(N'2020-03-20T16:17:06.457' AS DateTime), 1, N'F1001')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (17, N'U100420200320161727', 2, N'U1004', 4, NULL, N'来一个', N'U1008', CAST(N'2020-03-20T16:17:27.350' AS DateTime), 1, N'S1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (18, N'U100420200320161736', 2, N'U1004', 4, NULL, N'来11个', N'U1008', CAST(N'2020-03-20T16:17:36.417' AS DateTime), 11, N'S1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (19, N'U100320200325110234', 2, N'U1003', 0, NULL, N'没有口罩可用', N'U1002', CAST(N'2020-03-25T11:02:34.957' AS DateTime), 1, N'S1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (20, N'U100320200325110305', 2, N'U1003', 4, NULL, N'没有口罩可用😷', N'U1008', CAST(N'2020-03-25T11:03:05.447' AS DateTime), 1, N'S1002')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (21, N'U100320200325111533', 3, N'U1003', 0, NULL, N'借一台', N'U1002', CAST(N'2020-03-25T11:15:33.747' AS DateTime), 1, N'F1001')
INSERT [dbo].[WorkFlow_Instance] ([Id], [InstanceId], [ModelId], [UserId], [Status], [Description], [Reason], [NextReviewer], [AddTime], [OutNum], [OutGoodsId]) VALUES (22, N'U100320200325112125', 3, N'U1003', 5, NULL, N'搭建简单服务器', N'U1008', CAST(N'2020-03-25T11:21:25.220' AS DateTime), 1, N'F1001')
SET IDENTITY_INSERT [dbo].[WorkFlow_Instance] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkFlow_InstanceStep] ON 

INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (1, N'U1003YYYY03DD093222', N'U1002', NULL, 0, NULL, N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (2, N'U100320200311093933', N'U1002', NULL, 0, NULL, N'U1001')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (3, N'U100320200311093933', N'U1001', NULL, 0, NULL, N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (4, N'U100320200311094601', N'U1002', N'ok', 1, CAST(N'2020-03-11T16:31:27.180' AS DateTime), N'U1001')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (5, N'U100320200311094601', N'U1001', N'可以', 1, CAST(N'2020-03-11T17:06:32.260' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (6, N'U100320200311094920', N'U1002', N'不可以', 2, CAST(N'2020-03-11T16:30:15.313' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (7, N'U100320200311095054', N'U1002', N'可以', 1, CAST(N'2020-03-11T16:28:16.690' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (8, N'U100320200311163255', N'U1002', N'NO', 2, CAST(N'2020-03-20T16:23:47.553' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (9, N'U100220200311170517', N'U1002', N'OK', 1, CAST(N'2020-03-11T17:09:35.427' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (10, N'U100220200311170542', N'U1002', N'OK', 1, CAST(N'2020-03-11T17:09:56.037' AS DateTime), N'U1001')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (11, N'U100220200311170542', N'U1001', N'ok', 1, CAST(N'2020-03-13T11:05:04.257' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (12, N'U100120200311170753', N'U1001', NULL, 1, NULL, N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (13, N'U100120200311170753', N'U1008', N'出库', 1, CAST(N'2020-03-12T10:02:30.313' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (14, N'U100320200311094601', N'U1008', N'出库', 1, CAST(N'2020-03-12T10:03:35.583' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (15, N'U100320200311095054', N'U1008', N'出库', 1, CAST(N'2020-03-12T10:03:35.587' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (16, N'U100220200311170517', N'U1008', N'出库', 1, CAST(N'2020-03-12T10:03:35.590' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (17, N'U100220200312100704', N'U1002', N'ok', 1, CAST(N'2020-03-12T10:07:26.643' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (18, N'U100220200312100704', N'U1008', N'出库', 1, CAST(N'2020-03-12T10:07:49.740' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (19, N'U100320200313105939', N'U1002', N'ok', 1, CAST(N'2020-03-13T11:01:41.093' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (20, N'U100320200313110008', N'U1002', N'ok', 1, CAST(N'2020-03-13T11:01:24.600' AS DateTime), N'U1001')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (21, N'U100320200313110008', N'U1001', N'OK', 1, CAST(N'2020-03-13T11:02:28.857' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (22, N'U100320200313105939', N'U1008', N'出库', 1, CAST(N'2020-03-13T11:03:01.557' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (23, N'U100320200313110008', N'U1008', N'出库', 1, CAST(N'2020-03-13T11:03:01.560' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (24, N'U100220200311170542', N'U1008', N'出库', 1, CAST(N'2020-03-13T11:05:35.810' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (25, N'U100320200313154652', N'U1002', N'OK', 1, CAST(N'2020-03-13T15:55:14.960' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (26, N'U100320200313154721', N'U1002', NULL, 0, NULL, N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (27, N'U100220200313155449', N'U1002', NULL, 0, NULL, N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (28, N'U100320200313154652', N'U1008', N'已借出', 1, CAST(N'2020-03-13T15:56:01.640' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (29, N'U100320200313154652', N'U1008', N'已归还', 1, CAST(N'2020-03-16T15:45:47.500' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (30, N'U100420200320161706', N'U1002', NULL, 0, NULL, N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (31, N'U100420200320161727', N'U1002', N'OK', 1, CAST(N'2020-03-20T16:23:30.363' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (32, N'U100420200320161736', N'U1002', N'OK', 1, CAST(N'2020-03-20T16:23:36.443' AS DateTime), N'U1001')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (33, N'U100420200320161736', N'U1001', N'ok', 1, CAST(N'2020-03-20T16:26:02.753' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (34, N'U100420200320161727', N'U1008', N'出库', 1, CAST(N'2020-03-20T16:25:20.840' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (35, N'U100420200320161736', N'U1008', N'出库', 1, CAST(N'2020-03-20T16:26:29.510' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (36, N'U100320200325110234', N'U1002', NULL, 0, NULL, N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (37, N'U100320200325110305', N'U1002', N'OK', 1, CAST(N'2020-03-25T15:19:24.153' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (38, N'U100320200325111533', N'U1002', NULL, 0, NULL, N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (39, N'U100320200325112125', N'U1002', N'OK', 1, CAST(N'2020-03-25T15:30:21.570' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (40, N'U100320200325110305', N'U1008', N'出库', 1, CAST(N'2020-03-25T16:26:28.247' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (41, N'U100320200325112125', N'U1008', N'已借出', 1, CAST(N'2020-03-25T16:32:18.037' AS DateTime), N'无')
INSERT [dbo].[WorkFlow_InstanceStep] ([Id], [InstanceId], [ReviewerId], [ReviewReason], [ReviewStatus], [ReviewTime], [NextReviewerId]) VALUES (42, N'U100320200325112125', N'U1008', N'已归还', 1, CAST(N'2020-03-25T16:44:12.613' AS DateTime), N'无')
SET IDENTITY_INSERT [dbo].[WorkFlow_InstanceStep] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkFlow_Model] ON 

INSERT [dbo].[WorkFlow_Model] ([Id], [Title], [AddTime], [DelFlag], [Description]) VALUES (2, N'耗材领用', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, N'会根据审核结果来修改Goods_ConsumableInfo库存记录')
INSERT [dbo].[WorkFlow_Model] ([Id], [Title], [AddTime], [DelFlag], [Description]) VALUES (3, N'设备借取', CAST(N'2020-03-04T00:00:00.000' AS DateTime), 0, N'会根据审核结果来修改Goods_EquipmentInfo设备状态')
SET IDENTITY_INSERT [dbo].[WorkFlow_Model] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DepartmentInfo]    Script Date: 2021/11/18 14:27:45 ******/
ALTER TABLE [dbo].[DepartmentInfo] ADD  CONSTRAINT [IX_DepartmentInfo] UNIQUE NONCLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PowerInfo]    Script Date: 2021/11/18 14:27:45 ******/
ALTER TABLE [dbo].[PowerInfo] ADD  CONSTRAINT [IX_PowerInfo] UNIQUE NONCLUSTERED 
(
	[PowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleInfo]    Script Date: 2021/11/18 14:27:45 ******/
ALTER TABLE [dbo].[RoleInfo] ADD  CONSTRAINT [IX_RoleInfo] UNIQUE NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserInfo]    Script Date: 2021/11/18 14:27:45 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [IX_UserInfo] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [HopuGoods] SET  READ_WRITE 
GO
