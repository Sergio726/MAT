CREATE TABLE [dbo].[PaquetePrecio] (
    [PaquetePrecioID] UNIQUEIDENTIFIER CONSTRAINT [DF_PaquetePrecio_PaquetePrecioID] DEFAULT (newid()) NOT NULL,
    [PaqueteID]       UNIQUEIDENTIFIER NULL,
    [PrecioID]        UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_PaquetePrecio] PRIMARY KEY CLUSTERED ([PaquetePrecioID] ASC),
    CONSTRAINT [FK_PaquetePrecio_Paquete] FOREIGN KEY ([PaqueteID]) REFERENCES [dbo].[Paquete] ([PaqueteID]),
    CONSTRAINT [FK_PaquetePrecio_Precio] FOREIGN KEY ([PrecioID]) REFERENCES [dbo].[Precio] ([PrecioID])
);

