CREATE TABLE [dbo].[Cliente] (
    [ClienteID]    UNIQUEIDENTIFIER CONSTRAINT [DF_Cliente_ClienteID] DEFAULT (newid()) NOT NULL,
    [RazonSocial]  VARCHAR (50)     NULL,
    [Cuit]         VARCHAR (50)     NULL,
    [Moneda]       VARCHAR (50)     NULL,
    [Empresa]      VARCHAR (50)     NULL,
    [Ocupacion]    VARCHAR (50)     NULL,
    [FormaPago]    INT              NULL,
    [CondicionIva] INT              NULL,
    [VendedorID]   UNIQUEIDENTIFIER NULL,
    [Fax]          VARCHAR (50)     NULL,
    [Web]          VARCHAR (50)     NULL,
    [Idioma]       VARCHAR (50)     NULL,
    [Promotor]     VARCHAR (50)     NULL,
    [Observacion]  VARCHAR (250)    NULL,
    [TipoID]       INT              NOT NULL,
    [FechaAlta]    DATETIME         NULL,
    -- Baja lógica (módulo Clientes F6, 2026-11-04): los dados de baja no aparecen en listados, reservas ni Seguimiento.
    [Activo]       BIT              CONSTRAINT [DF_Cliente_Activo] DEFAULT ((1)) NOT NULL,
    [FechaBaja]    DATETIME         NULL,
    [MotivoBaja]   VARCHAR (250)    NULL,
    [BajaUsuario]  VARCHAR (100)    NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([ClienteID] ASC),
    CONSTRAINT [FK_Cliente_Persona] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Persona] ([PersonaID])
);

