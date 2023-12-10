CREATE TABLE [dbo].[Adicional] (
    [AdicionalID] UNIQUEIDENTIFIER CONSTRAINT [DF_Adicional_AdicionalID] DEFAULT (newid()) NOT NULL,
    [Monto]       FLOAT (53)       NULL,
    [Descripcion] VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_Adicional] PRIMARY KEY CLUSTERED ([AdicionalID] ASC)
);

