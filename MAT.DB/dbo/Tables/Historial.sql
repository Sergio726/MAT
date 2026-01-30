CREATE TABLE [dbo].[Historial] (
    [HistorialID]       UNIQUEIDENTIFIER NOT NULL,
    [Tabla]             INT              NOT NULL,
    [Operacion]         INT              NOT NULL,
    [FechaHoraRegistro] DATETIME         NOT NULL,
    [Cliente]           UNIQUEIDENTIFIER NOT NULL,
    [Vendedor]          UNIQUEIDENTIFIER NOT NULL,
    [Observaciones]     VARCHAR (MAX)    NULL,
    [Monto]             FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_Historial] PRIMARY KEY CLUSTERED ([HistorialID] ASC)
);

