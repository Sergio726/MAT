CREATE TABLE [dbo].[FacturaFiscal] (
    [FacturaFiscalID]   UNIQUEIDENTIFIER NOT NULL,
    [Tipo]              INT              NOT NULL,  -- 1=Compra, 2=Venta
    [TipoComprobante]   INT              NOT NULL,  -- 1=FA, 2=FB, 3=FC, 4=NCA, 5=NCB, 6=NCC
    [PuntoVenta]        INT              NOT NULL,
    [Numero]            BIGINT           NOT NULL,
    [FechaEmision]      DATE             NOT NULL,
    [FechaVencimiento]  DATE             NULL,
    [ProveedorID]       UNIQUEIDENTIFIER NULL,
    [ClienteID]         UNIQUEIDENTIFIER NULL,
    [Cuit]              VARCHAR(13)      NOT NULL,
    [CondicionIva]      INT              NOT NULL,
    [Neto]              DECIMAL(18,2)    NOT NULL DEFAULT(0),
    [Iva]               DECIMAL(18,2)    NOT NULL DEFAULT(0),
    [OtrosImpuestos]    DECIMAL(18,2)    NOT NULL DEFAULT(0),
    [Total]             DECIMAL(18,2)    NOT NULL DEFAULT(0),
    [Moneda]            INT              NOT NULL DEFAULT(1),
    [CAE]               VARCHAR(20)      NULL,
    [ArchivoAdjunto]    VARCHAR(500)     NULL,
    [Estado]            INT              NOT NULL DEFAULT(1),  -- 1=Activa, 2=Anulada
    [AlicuotaIva]       INT              NOT NULL DEFAULT(4),  -- default 21%
    [Percepciones]      DECIMAL(18,2)    NOT NULL DEFAULT(0),
    [CondicionVenta]    VARCHAR(100)     NULL,
    [Observaciones]     VARCHAR(500)     NULL,
    [CreatedAt]         DATETIME         NOT NULL DEFAULT(GETDATE()),
    [UpdatedAt]         DATETIME         NULL,
    CONSTRAINT [PK_FacturaFiscal] PRIMARY KEY CLUSTERED ([FacturaFiscalID] ASC),
    CONSTRAINT [FK_FacturaFiscal_Proveedor] FOREIGN KEY ([ProveedorID]) REFERENCES [dbo].[Proveedor]([ProveedorID]),
    CONSTRAINT [UQ_FacturaFiscal_Comprobante] UNIQUE ([Cuit], [TipoComprobante], [PuntoVenta], [Numero])
);
GO

CREATE NONCLUSTERED INDEX [IX_FacturaFiscal_TipoFecha] ON [dbo].[FacturaFiscal] ([Tipo], [FechaEmision]);
GO
CREATE NONCLUSTERED INDEX [IX_FacturaFiscal_ProveedorID] ON [dbo].[FacturaFiscal] ([ProveedorID]);
GO
CREATE NONCLUSTERED INDEX [IX_FacturaFiscal_ClienteID] ON [dbo].[FacturaFiscal] ([ClienteID]);
GO
CREATE NONCLUSTERED INDEX [IX_FacturaFiscal_Estado] ON [dbo].[FacturaFiscal] ([Estado]);
GO
