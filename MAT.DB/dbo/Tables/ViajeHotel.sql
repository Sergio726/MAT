CREATE TABLE [dbo].[ViajeHotel] (
    [ViajeHotelID] UNIQUEIDENTIFIER NOT NULL,
    [ViajeID]      UNIQUEIDENTIFIER NOT NULL,
    [HotelID]      UNIQUEIDENTIFIER NOT NULL,
    [Desde]        VARCHAR (10)     NULL,
    [Hasta]        VARCHAR (10)     NULL,
    [HoraIngreso]  VARCHAR (10)     NULL,
    [HoraSalida]   VARCHAR (10)     NULL,
    [Comentario]   VARCHAR (50)     NULL,
    CONSTRAINT [PK_ViajeHotel] PRIMARY KEY CLUSTERED ([ViajeHotelID] ASC),
    CONSTRAINT [FK_ViajeHotel_Hotel] FOREIGN KEY ([HotelID]) REFERENCES [dbo].[Hotel] ([HotelID]),
    CONSTRAINT [FK_ViajeHotel_Viaje] FOREIGN KEY ([ViajeID]) REFERENCES [dbo].[Viaje] ([ViajeID])
);

