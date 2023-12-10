CREATE TABLE [dbo].[Paquete] (
    [PaqueteID]      UNIQUEIDENTIFIER CONSTRAINT [DF_Paquete_PaqueteID] DEFAULT (newid()) NOT NULL,
    [Descripcion]    VARCHAR (100)    NULL,
    [PrecioCama]     FLOAT (53)       NULL,
    [Moneda]         INT              NULL,
    [Iva]            VARCHAR (50)     NULL,
    [Alicuota]       VARCHAR (50)     NULL,
    [Temporada]      INT              NULL,
    [Cotizacion]     FLOAT (53)       NULL,
    [Codigo]         VARCHAR (50)     NULL,
    [DestinoID]      INT              NULL,
    [PrecioSemiCama] FLOAT (53)       NULL,
    CONSTRAINT [PK_Paquete] PRIMARY KEY CLUSTERED ([PaqueteID] ASC),
    CONSTRAINT [FK_Paquete_Localidad] FOREIGN KEY ([DestinoID]) REFERENCES [dbo].[Localidad] ([ID])
);





