USE [FreightTech]
GO

/****** Object:  Table [dbo].[ApplicationLog]    Script Date: 9/2/2018 3:24:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ApplicationLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[Application] [varchar](20) NOT NULL,
	[Type] [varchar](20) NOT NULL,
	[UserId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Message] [varchar](500) NOT NULL,
	[StackTrace] [varchar](max) NULL,
	[RequestUri] [varchar](500) NULL,
	[InnerException] [varchar](500) NULL,
 CONSTRAINT [PK_ApplicationLogs] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ApplicationLog] ADD  CONSTRAINT [DF_ApplicationLogs_UserId]  DEFAULT ((0)) FOR [UserId]
GO

ALTER TABLE [dbo].[ApplicationLog] ADD  CONSTRAINT [DF_ApplicationLogs_Date]  DEFAULT (getdate()) FOR [Date]
GO


