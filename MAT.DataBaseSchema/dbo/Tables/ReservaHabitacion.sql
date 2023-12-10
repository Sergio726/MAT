CREATE TABLE [dbo].[ReservaHabitacion] (
    [ReservaHabitacionID] UNIQUEIDENTIFIER CONSTRAINT [DF_ReservaHabitacion_ReservaHabitacionID] DEFAULT (newid()) NOT NULL,
    [HabitacionID]        UNIQUEIDENTIFIER NULL,
    [PasajeID]            UNIQUEIDENTIFIER NULL,
    [FechaReserva]        DATE             NULL,
    [Desde]               DATE             NULL,
    [Hasta]               DATE             NULL,
    [Expiro]              BIT              NULL,
    [HoraIngreso]         VARCHAR (10)     NULL,
    [HoraSalida]          VARCHAR (10)     NULL,
    CONSTRAINT [PK_ReservaHabitacion] PRIMARY KEY CLUSTERED ([ReservaHabitacionID] ASC),
    CONSTRAINT [FK_ReservaHabitacion_Habitacion] FOREIGN KEY ([HabitacionID]) REFERENCES [dbo].[Habitacion] ([HabitacionID]),
    CONSTRAINT [FK_ReservaHabitacion_Pasaje] FOREIGN KEY ([PasajeID]) REFERENCES [dbo].[Pasaje] ([PasajeID])
);



