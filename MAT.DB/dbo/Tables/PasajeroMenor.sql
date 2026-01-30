CREATE TABLE [dbo].[PasajeroMenor] (
    [id]         INT              IDENTITY (1, 1) NOT NULL,
    [pasajeid]   UNIQUEIDENTIFIER NOT NULL,
    [pasajeroid] UNIQUEIDENTIFIER NOT NULL,
    [menorid]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [pk_pasajeromenor] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [fk_cliente_menor] FOREIGN KEY ([menorid]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [fk_cliente_pasajero] FOREIGN KEY ([pasajeroid]) REFERENCES [dbo].[Cliente] ([ClienteID])
);

