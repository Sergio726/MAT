CREATE TABLE [dbo].[Precio] (
    [PrecioID]           UNIQUEIDENTIFIER CONSTRAINT [DF_Precio_PrecioID] DEFAULT (newid()) NOT NULL,
    [Monto]              FLOAT (53)       NOT NULL,
    [Vigencia]           DATETIME         NULL,
    [Descripcion]        VARCHAR (100)    NOT NULL,
    [Mes]                VARCHAR (100)    NULL,
    [DescripcionVoucher] VARCHAR (500)    NULL,
    CONSTRAINT [PK_Precio] PRIMARY KEY CLUSTERED ([PrecioID] ASC)
);

