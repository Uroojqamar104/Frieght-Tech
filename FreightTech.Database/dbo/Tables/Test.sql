CREATE TABLE [dbo].[Test] (
    [TestId]      INT          IDENTITY (1, 1) NOT NULL,
    [OrderId]     INT          NOT NULL,
    [Lat]         VARCHAR (50) NOT NULL,
    [Long]        VARCHAR (50) NOT NULL,
    [DateCreated] DATETIME     CONSTRAINT [DF_Test_DateCreated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED ([TestId] ASC)
);

