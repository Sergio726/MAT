CREATE TABLE [dbo].[Seat] (
    [Id]             INT         IDENTITY (1, 1) NOT NULL,
    [NumberSeat]     INT         NOT NULL,
    [IdSeatFloor]    INT         NOT NULL,
    [IdSeatLocation] INT         NOT NULL,
    [IdSeatType]     INT         NOT NULL,
    [IdTransport]    INT         NOT NULL,
    [Row]            VARCHAR (2) NULL,
    [IdSeatPosition] INT         NOT NULL,
    CONSTRAINT [PK_Seat] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Seat_SeatFloor] FOREIGN KEY ([IdSeatFloor]) REFERENCES [dbo].[SeatFloor] ([Id]),
    CONSTRAINT [FK_Seat_SeatLocation] FOREIGN KEY ([IdSeatLocation]) REFERENCES [dbo].[SeatLocation] ([Id]),
    CONSTRAINT [FK_Seat_SeatPosition] FOREIGN KEY ([IdSeatPosition]) REFERENCES [dbo].[SeatPosition] ([Id]),
    CONSTRAINT [FK_Seat_SeatType] FOREIGN KEY ([IdSeatType]) REFERENCES [dbo].[SeatType] ([Id]),
    CONSTRAINT [FK_Seat_Transport] FOREIGN KEY ([IdTransport]) REFERENCES [dbo].[Transport] ([Id])
);

