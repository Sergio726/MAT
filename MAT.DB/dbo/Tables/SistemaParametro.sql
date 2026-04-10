CREATE TABLE [dbo].[SistemaParametro] (
    [Id]                INT IDENTITY(1,1)   NOT NULL,
    [Clave]             VARCHAR(100)        NOT NULL,
    [Valor]             NVARCHAR(MAX)       NOT NULL,
    [Descripcion]       NVARCHAR(500)       NULL,
    [EstaActivo]        BIT                 NOT NULL CONSTRAINT [DF_SistemaParametro_EstaActivo] DEFAULT (1),
    [FechaCreacion]     DATETIME            NOT NULL CONSTRAINT [DF_SistemaParametro_FechaCreacion] DEFAULT (GETDATE()),
    [FechaModificacion] DATETIME            NULL,
    CONSTRAINT [PK_SistemaParametro] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_SistemaParametro_Clave] UNIQUE NONCLUSTERED ([Clave] ASC)
);