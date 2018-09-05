CREATE TABLE [dbo].[DriverDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DriverId] [int] NOT NULL,
	[Contact] VARCHAR(20) NOT NULL,
	[VehicleNumber] [varchar](20) NOT NULL,
	[StatusId] [int] NOT NULL,
	[IsLoaded] [bit] NOT NULL,
	[IsAccepted] [bit] NOT NULL,
	[CurrentLatitude] VARCHAR(20) NULL,
	[CurrentLongitude] VARCHAR(20) NULL,
	[LastLocationUpdated] [datetime] NULL,
 CONSTRAINT [PK_DriverDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DriverDetail]  WITH CHECK ADD  CONSTRAINT [FK_DriverDetail_UserProfile1] FOREIGN KEY([DriverId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[DriverDetail] CHECK CONSTRAINT [FK_DriverDetail_UserProfile1]