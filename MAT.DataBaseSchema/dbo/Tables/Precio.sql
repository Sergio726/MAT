CREATE TABLE [dbo].[Precio] (
    [PrecioID]    UNIQUEIDENTIFIER CONSTRAINT [DF_Precio_PrecioID] DEFAULT (newid()) NOT NULL,
    [Monto]       FLOAT (53)       NULL,
    [Vigencia]    DATETIME         NULL,
    [Descripcion] VARCHAR (100)    NULL,
    [Mes]         VARCHAR (100)    NULL,
    CONSTRAINT [PK_Precio] PRIMARY KEY CLUSTERED ([PrecioID] ASC)
);

