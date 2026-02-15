/*
-- Migración: Agregar columnas de contacto alternativo a Presupuesto
-- Fecha: 2026-02-15
-- Descripción: Permite crear presupuestos sin DNI cuando el cliente deja teléfono o email
-- Ejecutar en bases de datos existentes antes de desplegar la nueva versión
*/
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Presupuesto') AND name = 'NombreCliente')
BEGIN
    ALTER TABLE [dbo].[Presupuesto] ADD [NombreCliente] VARCHAR(200) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Presupuesto') AND name = 'TelefonoCliente')
BEGIN
    ALTER TABLE [dbo].[Presupuesto] ADD [TelefonoCliente] VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Presupuesto') AND name = 'EmailCliente')
BEGIN
    ALTER TABLE [dbo].[Presupuesto] ADD [EmailCliente] VARCHAR(100) NULL;
END
GO

IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Presupuesto') AND name = 'DniCliente')
BEGIN
    ALTER TABLE [dbo].[Presupuesto] ALTER COLUMN [DniCliente] VARCHAR(50) NULL;
END
GO
