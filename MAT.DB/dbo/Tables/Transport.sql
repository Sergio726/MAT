CREATE TABLE [dbo].[Transport] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [Number]          VARCHAR (50) NOT NULL,
    [MaxPassengers]   INT          NULL,
    [LastServiceDate] DATE         NULL,
    [LicensePlate]    VARCHAR (20) NULL,
    [IdTransportType] INT          NOT NULL,
    [IdProvider]      INT          NOT NULL,
    CONSTRAINT [PK_Transport] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transport_Provider] FOREIGN KEY ([IdProvider]) REFERENCES [dbo].[Provider] ([Id]),
    CONSTRAINT [FK_Transport_TransportType] FOREIGN KEY ([IdTransportType]) REFERENCES [dbo].[TransportType] ([Id])
);

