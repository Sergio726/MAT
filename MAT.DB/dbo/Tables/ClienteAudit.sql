-- Historial de cambios del cliente (módulo Clientes F6, 2026-11-04): alta, modificación (con los campos cambiados),
-- baja y reactivación; quién y cuándo. Se ve en la ficha, pestaña "Historial de cambios" (solo administradores).
CREATE TABLE [dbo].[ClienteAudit] (
    [ID]        INT              IDENTITY (1, 1) NOT NULL,
    [ClienteID] UNIQUEIDENTIFIER NOT NULL,
    [Fecha]     DATETIME         NOT NULL,
    [Usuario]   VARCHAR (100)    NULL,
    -- Alta | Modificacion | Baja | Reactivacion
    [Accion]    VARCHAR (20)     NOT NULL,
    -- Una línea por campo: "Celular: 387 4111111 → 387 4222222". En la baja, el motivo.
    [Detalle]   NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ClienteAudit] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ClienteAudit_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [CK_ClienteAudit_Accion] CHECK ([Accion] IN ('Alta', 'Modificacion', 'Baja', 'Reactivacion'))
);


GO
CREATE NONCLUSTERED INDEX [IX_ClienteAudit_ClienteID_Fecha]
    ON [dbo].[ClienteAudit]([ClienteID] ASC, [Fecha] DESC);
