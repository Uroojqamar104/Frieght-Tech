
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[DriverId] [int] NULL,
	[StatusId] [int] NOT NULL,
	[PickupLocation] [varchar](200) NOT NULL,
	[DropoffLocation] [varchar](200) NOT NULL,
	[PickupLatitude] [varchar](50) NOT NULL,
	[PickupLongitude] [varchar](50) NOT NULL,
	[DropoffLatitude] [varchar](50) NOT NULL,
	[DropoffLongitude] [varchar](50) NOT NULL,
	[LoadType] [varchar](50) NOT NULL,
	[Commodity] [varchar](50) NOT NULL,
	[Weight] [decimal](18, 2) NOT NULL,
	[ContactNumber] [varchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_DriverId] FOREIGN KEY([DriverId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_DriverId]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_UserProfile] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_UserProfile]
GO


