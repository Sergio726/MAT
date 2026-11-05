-- Catálogo de ocupaciones con nombres neutros (módulo Clientes F4, 2026-11-04).
-- Persona.Ocupacion guarda el Nombre (texto): así no cambia ningún reporte ni SP que ya la lee.
CREATE TABLE [dbo].[Ocupacion] (
    [ID]     INT          IDENTITY (1, 1) NOT NULL,
    [Nombre] VARCHAR (50) NOT NULL,
    [Activo] BIT          CONSTRAINT [DF_Ocupacion_Activo] DEFAULT ((1)) NOT NULL,
    [Orden]  INT          CONSTRAINT [DF_Ocupacion_Orden] DEFAULT ((100)) NOT NULL,
    CONSTRAINT [PK_Ocupacion] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_Ocupacion_Nombre] UNIQUE NONCLUSTERED ([Nombre] ASC)
);
