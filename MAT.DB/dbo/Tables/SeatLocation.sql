CREATE TABLE [dbo].[SeatLocation] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SeatLocation] PRIMARY KEY CLUSTERED ([Id] ASC)
);

