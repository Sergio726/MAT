CREATE TABLE [dbo].[PasajeSeleccionado] (
    [Id]               UNIQUEIDENTIFIER CONSTRAINT [DF_PasajeSeleccionado_Id] DEFAULT (newid()) NOT NULL,
    [ReservaId]        UNIQUEIDENTIFIER NULL,
    [PasajeId]         UNIQUEIDENTIFIER NULL,
    [PasajeroId]       UNIQUEIDENTIFIER NULL,
    [ButacaId]         UNIQUEIDENTIFIER NULL,
    [ButacaCodigo]     VARCHAR (4)      NULL,
    [ButacaPrecio]     FLOAT (53)       NULL,
    [AdicionalesIds]   VARCHAR (MAX)    NULL,
    [HabitacionId]     UNIQUEIDENTIFIER NULL,
    [PasajeroAdultoId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_PasajeSeleccionado] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

