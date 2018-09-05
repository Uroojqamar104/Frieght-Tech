CREATE TABLE [dbo].[UserProfile] (
    [UserId]    INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (MAX) NOT NULL,
    [LastName]  NVARCHAR (MAX) NOT NULL,
    [EmailId]   NVARCHAR (MAX) NOT NULL,
    [RoleId]    INT            NOT NULL,
    [HasImage]  BIT            NOT NULL,
    [RowState]  INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

