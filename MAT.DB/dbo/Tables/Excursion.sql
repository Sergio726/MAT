CREATE TABLE [dbo].[Excursion] (
    [ExcursionID]   UNIQUEIDENTIFIER CONSTRAINT [DF_Excursion_ExcursionID] DEFAULT (newid()) NOT NULL,
    [Descripcion]   VARCHAR (200)    NOT NULL,
    [Costo]         FLOAT (53)       NULL,
    [Observaciones] VARCHAR (MAX)    NULL,
    [ProveedorID]   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Excursion] PRIMARY KEY CLUSTERED ([ExcursionID] ASC),
    CONSTRAINT [FK_Excursion_Proveedor] FOREIGN KEY ([ProveedorID]) REFERENCES [dbo].[Proveedor] ([ProveedorID])
);

