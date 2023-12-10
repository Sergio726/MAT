CREATE TABLE [dbo].[PaqueteServicio] (
    [PaqueteServicioID] UNIQUEIDENTIFIER CONSTRAINT [DF_PaqueteServicio_PaqueteServicioID] DEFAULT (newid()) NOT NULL,
    [ServicioID]        UNIQUEIDENTIFIER NULL,
    [PaqueteID]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_PaqueteServicio] PRIMARY KEY CLUSTERED ([PaqueteServicioID] ASC),
    CONSTRAINT [FK_PaqueteServicio_Paquete] FOREIGN KEY ([PaqueteID]) REFERENCES [dbo].[Paquete] ([PaqueteID]),
    CONSTRAINT [FK_PaqueteServicio_Servicio] FOREIGN KEY ([ServicioID]) REFERENCES [dbo].[Servicio] ([ServicioID])
);

