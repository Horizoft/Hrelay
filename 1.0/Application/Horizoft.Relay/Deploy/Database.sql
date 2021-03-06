USE [master]
GO
/****** Object:  Database [HorizoftRelay]    Script Date: 4/7/17 4:08:58 PM ******/
CREATE DATABASE [HorizoftRelay]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HorizoftRelay', FILENAME = N'D:\Database\MSSQL\DATA\HorizoftRelay.mdf' , SIZE = 14336KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HorizoftRelay_log', FILENAME = N'D:\Database\MSSQL\DATA\HorizoftRelay_log.ldf' , SIZE = 3136KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [HorizoftRelay] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HorizoftRelay].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HorizoftRelay] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HorizoftRelay] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HorizoftRelay] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HorizoftRelay] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HorizoftRelay] SET ARITHABORT OFF 
GO
ALTER DATABASE [HorizoftRelay] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HorizoftRelay] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HorizoftRelay] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HorizoftRelay] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HorizoftRelay] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HorizoftRelay] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HorizoftRelay] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HorizoftRelay] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HorizoftRelay] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HorizoftRelay] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HorizoftRelay] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HorizoftRelay] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HorizoftRelay] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HorizoftRelay] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HorizoftRelay] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HorizoftRelay] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HorizoftRelay] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HorizoftRelay] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HorizoftRelay] SET  MULTI_USER 
GO
ALTER DATABASE [HorizoftRelay] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HorizoftRelay] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HorizoftRelay] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HorizoftRelay] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [HorizoftRelay] SET DELAYED_DURABILITY = DISABLED 
GO
USE [HorizoftRelay]
GO
/****** Object:  Table [dbo].[TemperatureData]    Script Date: 4/7/17 4:08:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemperatureData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MonitoredDateTime] [datetime] NULL,
	[MonitoredDate] [date] NULL,
	[MonitoredTime] [time](0) NULL,
	[PlaceId] [int] NULL,
	[AreaId] [int] NULL,
	[I1] [int] NULL,
	[I2] [int] NULL,
	[R1] [int] NULL,
	[R2] [int] NULL,
	[R3] [int] NULL,
	[R4] [int] NULL,
	[R5] [int] NULL,
	[R6] [int] NULL,
	[R7] [int] NULL,
	[R8] [int] NULL,
	[R9] [int] NULL,
	[R10] [int] NULL,
	[S1] [decimal](18, 5) NULL,
	[S2] [decimal](18, 5) NULL,
	[S3] [decimal](18, 5) NULL,
	[Source] [nchar](255) NULL,
 CONSTRAINT [PK_TemperatureData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[SPTemperatureReport]    Script Date: 4/7/17 4:08:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPTemperatureReport]
@placeId int,
@areaId int,
@date DATE
AS
SELECT MonitoredDate, MonitoredTime, ROUND(SUM(S1)/COUNT(S1),1) AS AverageTemperature
FROM
(
SELECT  MonitoredDate, 
		CAST(DATEADD(Minute, (DATEDIFF(Minute, 0, MonitoredTime) + 5) / 10 * 10, 0) AS TIME(0)) AS MonitoredTime, 
		I1, I2, R1, R2, R3, R4, R5, R6, 
        R7, R8, R9, R10, S1, S2, S3, Source
FROM    dbo.TemperatureData
WHERE   (PlaceId = @placeId) AND (AreaId = @areaId) 
		AND (DATEADD(DAY, DATEDIFF(DAY, 0, MonitoredDateTime), 0) = @date)
) AS T1
GROUP BY MonitoredDate, MonitoredTime
ORDER BY MonitoredDate, MonitoredTime


GO
USE [master]
GO
ALTER DATABASE [HorizoftRelay] SET  READ_WRITE 
GO
