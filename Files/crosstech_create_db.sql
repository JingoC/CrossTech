USE [master]
GO

/****** Object:  Database [crosstech]    Script Date: 15-Mar-20 16:17:01 ******/
CREATE DATABASE [crosstech]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'crosstech', FILENAME = N'D:\crosstech.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'crosstech_log', FILENAME = N'D:\crosstech_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [crosstech].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [crosstech] SET ANSI_NULL_DEFAULT OFF 
ALTER DATABASE [crosstech] SET ANSI_NULLS OFF 
ALTER DATABASE [crosstech] SET ANSI_PADDING OFF 
ALTER DATABASE [crosstech] SET ANSI_WARNINGS OFF 
ALTER DATABASE [crosstech] SET ARITHABORT OFF 
ALTER DATABASE [crosstech] SET AUTO_CLOSE OFF 
ALTER DATABASE [crosstech] SET AUTO_SHRINK OFF 
ALTER DATABASE [crosstech] SET AUTO_UPDATE_STATISTICS ON 
ALTER DATABASE [crosstech] SET CURSOR_CLOSE_ON_COMMIT OFF 
ALTER DATABASE [crosstech] SET CURSOR_DEFAULT  GLOBAL 
ALTER DATABASE [crosstech] SET CONCAT_NULL_YIELDS_NULL OFF 
ALTER DATABASE [crosstech] SET NUMERIC_ROUNDABORT OFF 
ALTER DATABASE [crosstech] SET QUOTED_IDENTIFIER OFF 
ALTER DATABASE [crosstech] SET RECURSIVE_TRIGGERS OFF 
ALTER DATABASE [crosstech] SET  DISABLE_BROKER 
ALTER DATABASE [crosstech] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
ALTER DATABASE [crosstech] SET DATE_CORRELATION_OPTIMIZATION OFF 
ALTER DATABASE [crosstech] SET TRUSTWORTHY OFF 
ALTER DATABASE [crosstech] SET ALLOW_SNAPSHOT_ISOLATION OFF 
ALTER DATABASE [crosstech] SET PARAMETERIZATION SIMPLE 
ALTER DATABASE [crosstech] SET READ_COMMITTED_SNAPSHOT OFF 
ALTER DATABASE [crosstech] SET HONOR_BROKER_PRIORITY OFF 
ALTER DATABASE [crosstech] SET RECOVERY SIMPLE 
ALTER DATABASE [crosstech] SET  MULTI_USER 
ALTER DATABASE [crosstech] SET PAGE_VERIFY CHECKSUM  
ALTER DATABASE [crosstech] SET DB_CHAINING OFF 
ALTER DATABASE [crosstech] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
ALTER DATABASE [crosstech] SET TARGET_RECOVERY_TIME = 60 SECONDS 
ALTER DATABASE [crosstech] SET DELAYED_DURABILITY = DISABLED 
ALTER DATABASE [crosstech] SET QUERY_STORE = OFF
ALTER DATABASE [crosstech] SET  READ_WRITE 

USE [crosstech]
GO

CREATE TABLE [dbo].[Users]([Id] [int] IDENTITY(1,1) NOT NULL,[Login] [nvarchar](100) NOT NULL,[Password] [nvarchar](100) NOT NULL,[AccessToken] [nvarchar](100) NOT NULL, CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ( [Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
CREATE TABLE [dbo].[Claims]([Id] [int] IDENTITY(1,1) NOT NULL,[Name] [nvarchar](100) NOT NULL,CONSTRAINT [PK_Claims] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
CREATE TABLE [dbo].[UserClaims]([Id] [int] IDENTITY(1,1) NOT NULL,[UserId] [int] NOT NULL,[ClaimId] [int] NOT NULL,CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
CREATE TABLE [dbo].[Positions]([Id] [int] IDENTITY(1,1) NOT NULL,[Name] [nvarchar](50) NOT NULL,CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
CREATE TABLE [dbo].[Employees]([Id] [int] IDENTITY(1,1) NOT NULL,[FirstName] [nvarchar](50) NOT NULL,[FatherName] [nvarchar](50) NULL,[LastName] [nvarchar](50) NOT NULL,[Sex] [int] NOT NULL,[PositionId] [int] NOT NULL,[Birthday] [datetime2](7) NOT NULL,[Phone] [nvarchar](20) NULL,CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
GO

insert Claims values ('Admin'), ('User')
insert Users values ('admin', '111', 't1234'), ('user1', '222', 't2345');
insert UserClaims values (1, 1),(2, 2)
insert Positions values ('Инженер'),('Бухгалтер'),('Водитель');
insert Employees values
('Сергей',		'Данилович',		'Емельянов',	0, 1, '1989-09-12', '79577481093'),
('Филипп',		'Вадимович',		'Кравченко',	0, 2, '1963-02-04', '79226564987'),
('Лукиллиан',	'Александрович',	'Жданов',		0, 3, '1971-03-03', '79461901028'),
('Прохор',		'Богданович',		'Логинов',		0, 2, '1984-11-23', '79113433879'),
('Орест',		'Семенович',		'Васильев',		0, 3, '1945-10-11', '79453625996'),
('Ярослава',	'Максимовна',		'Рожкова',		1, 2, '1993-10-25', '79808849612'),
('Валерия',		'Анатолиевна',		'Михайлова',	1, 1, '2002-04-05', '79642655148'),
('Ульяна',		'Эдуардовна',		'Фадеева',		1, 2, '1981-06-08', '79000609401');
 

select * from [crosstech].[dbo].[Users]
select * from [crosstech].[dbo].[Claims]
select * from [crosstech].[dbo].[UserClaims]
select * from [crosstech].[dbo].[Positions]
select * from [crosstech].[dbo].[Employees]