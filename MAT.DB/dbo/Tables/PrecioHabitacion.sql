CREATE TABLE [dbo].[PrecioHabitacion] (
    [PrecioHabitacionID] UNIQUEIDENTIFIER NOT NULL,
    [TipoHabitacion]     INT              NOT NULL,
    [HotelID]            UNIQUEIDENTIFIER NOT NULL,
    [FechaRegistro]      DATE             NOT NULL,
    [Activo]             BIT              NOT NULL,
    [Precio]             FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_PrecioHabitacion] PRIMARY KEY CLUSTERED ([PrecioHabitacionID] ASC)
);

