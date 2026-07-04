-- NetTiers F6 — Paridad de esquema + SPs de Factura, Pasaje, Pago, MovimientoCuenta
-- Publicar en SQL Server antes de usar MVC migrado en cada entorno.
-- Fuente de verdad: MAT.DB (archivos individuales en dbo/Stored Procedures/ y dbo/Tables/)

-- ============================================================================
-- 1) PARIDAD DE ESQUEMA (MAT.DB estaba desincronizado con la BD real)
-- Las entidades NetTiers (generadas desde la BD) y el SQL dinamico del provider
-- (useStoredProcedure="false") prueban que estas columnas existen en la BD
-- operativa pero faltaban en el proyecto SSDT. En una BD que ya las tenga,
-- estos ALTER son no-op (guardas IF NOT EXISTS).
-- ============================================================================

IF COL_LENGTH('dbo.Factura', 'DescuentoAplicado') IS NULL
    ALTER TABLE dbo.Factura ADD DescuentoAplicado FLOAT (53) CONSTRAINT DF_Factura_DescuentoAplicado DEFAULT ((0)) NOT NULL;

IF COL_LENGTH('dbo.Pago', 'ClienteID') IS NULL
    ALTER TABLE dbo.Pago ADD ClienteID UNIQUEIDENTIFIER NULL;

IF COL_LENGTH('dbo.Pago', 'EstadoRendicion') IS NULL
    ALTER TABLE dbo.Pago ADD EstadoRendicion INT NULL;

IF COL_LENGTH('dbo.Pago', 'CuentaCorrienteID') IS NULL
    ALTER TABLE dbo.Pago ADD CuentaCorrienteID UNIQUEIDENTIFIER NULL;

IF COL_LENGTH('dbo.MovimientoCuenta', 'DebitoID') IS NULL
    ALTER TABLE dbo.MovimientoCuenta ADD DebitoID UNIQUEIDENTIFIER NULL;
GO

-- ============================================================================
-- 2) SPs nuevos (ver archivos individuales en MAT.DB)
-- usp_MAT_Factura_GetEntityById.sql, _GetEntitiesByClienteId.sql, _UpdateEntity.sql
-- usp_MAT_Factura_GetSaldoInfo.sql (Monto + TotalPagos + TotalDebitos set-based)
-- usp_MAT_Pasaje_GetEntityById.sql, _GetByFacturaId.sql, _GetEntitiesByViajeId.sql,
--   _GetByPasajeroId.sql, _UpdateEntity.sql
-- usp_MAT_Pago_GetEntityById.sql, _GetByVendedorId.sql, _GetByFacturaId.sql, _UpdateEntity.sql
-- usp_MAT_MovimientoCuenta_GetByPagoId.sql
-- ============================================================================
