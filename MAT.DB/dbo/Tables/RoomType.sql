CREATE TABLE [dbo].[RoomType] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (50) NOT NULL,
    [NormalCapacity] INT          NULL,
    CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

