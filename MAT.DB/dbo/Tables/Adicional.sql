CREATE TABLE [dbo].[Adicional] (
    [AdicionalID] UNIQUEIDENTIFIER CONSTRAINT [DF_Adicional_AdicionalID] DEFAULT (newid()) NOT NULL,
    [Monto]       FLOAT (53)       NOT NULL,
    [Descripcion] VARCHAR (MAX)    NOT NULL,
    CONSTRAINT [PK_Adicional] PRIMARY KEY CLUSTERED ([AdicionalID] ASC)
);

