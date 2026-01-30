CREATE TABLE [dbo].[Room] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Number]      VARCHAR (4)    NOT NULL,
    [Name]        VARCHAR (100)  NULL,
    [Description] VARCHAR (2000) NULL,
    [Size]        INT            NULL,
    [IdRoomType]  INT            NULL,
    [IdProperty]  INT            NULL,
    CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Room_Property] FOREIGN KEY ([IdProperty]) REFERENCES [dbo].[Property] ([Id]),
    CONSTRAINT [FK_Room_RoomType] FOREIGN KEY ([IdRoomType]) REFERENCES [dbo].[RoomType] ([Id])
);

