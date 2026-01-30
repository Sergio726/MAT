CREATE TABLE [dbo].[SeatType] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SeatType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

