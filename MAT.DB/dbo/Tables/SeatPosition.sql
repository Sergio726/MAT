CREATE TABLE [dbo].[SeatPosition] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SeatPosition] PRIMARY KEY CLUSTERED ([Id] ASC)
);

