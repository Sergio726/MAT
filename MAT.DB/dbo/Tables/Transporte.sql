CREATE TABLE [dbo].[Transporte] (
    [TransporteID]  UNIQUEIDENTIFIER CONSTRAINT [DF_Transporte_TransporteID] DEFAULT (newid()) NOT NULL,
    [NroCoche]      VARCHAR (50)     NULL,
    [MaxPasajeros]  INT              NULL,
    [KmRecorridos]  INT              NULL,
    [UltimoService] DATE             NULL,
    [Matricula]     VARCHAR (10)     NULL,
    [Tipo]          NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Transporte] PRIMARY KEY CLUSTERED ([TransporteID] ASC)
);

