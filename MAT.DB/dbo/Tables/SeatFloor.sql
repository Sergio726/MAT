CREATE TABLE [dbo].[SeatFloor] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SeatFloor] PRIMARY KEY CLUSTERED ([Id] ASC)
);

