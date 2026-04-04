/*
  Migración: dbo.Cliente — agregar columna FechaAlta (DATETIME NULL).
  Paridad con MAT.DB\dbo\Tables\Cliente.sql
  Ejecutar contra la base configurada en MAT.Data.ConnectionString (p. ej. MAT.Intranet).

  ORDEN DE EJECUCIÓN: este script debe ejecutarse ANTES de usp_MAT_Reportes_ClientesNuevos.
*/
SET NOCOUNT ON;
GO

IF NOT EXISTS (
    SELECT 1
    FROM   sys.columns
    WHERE  object_id = OBJECT_ID(N'dbo.Cliente')
      AND  name      = N'FechaAlta'
)
BEGIN
    ALTER TABLE [dbo].[Cliente]
        ADD [FechaAlta] DATETIME NULL;

    PRINT 'Columna FechaAlta agregada a dbo.Cliente.';
END
ELSE
BEGIN
    PRINT 'Columna FechaAlta ya existe en dbo.Cliente — sin cambios.';
END
GO
