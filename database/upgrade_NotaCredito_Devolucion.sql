-- =============================================
-- Upgrade: Nota de crédito - Devolución y PagoTipo
-- Ejecutar en la base de datos antes de desplegar la aplicación
-- =============================================

-- 1) Columna MontoDevolucion en Nota (si no existe)
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.Nota') AND name = 'MontoDevolucion'
)
BEGIN
    ALTER TABLE dbo.Nota ADD MontoDevolucion MONEY NULL;
END
GO

-- 2) Tipo de pago "Devolución" (si no existe)
IF NOT EXISTS (SELECT 1 FROM dbo.PagoTipo WHERE Descripcion = 'Devolución')
BEGIN
    INSERT INTO dbo.PagoTipo (Descripcion)
    VALUES ('Devolución');
END
GO
