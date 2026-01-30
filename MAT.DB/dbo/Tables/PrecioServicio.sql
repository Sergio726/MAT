CREATE TABLE [dbo].[PrecioServicio] (
    [PrecioServicioID] UNIQUEIDENTIFIER NOT NULL,
    [ServicioID]       UNIQUEIDENTIFIER NOT NULL,
    [FechaRegistro]    DATE             NOT NULL,
    [Activo]           BIT              NOT NULL,
    [Precio]           FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_PrecioServicio] PRIMARY KEY CLUSTERED ([PrecioServicioID] ASC)
);

