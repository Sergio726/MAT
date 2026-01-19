/*
  Migración: Soft delete para dbo.ListaEspera
  - Agrega columnas de auditoría
  - Crea índices para mantener rápida la consulta por ViajeID

  Ejecutar 1 vez en la DB (prod/stage) con permisos de DDL.
*/

/* 1) Columnas (si no existen) */
IF COL_LENGTH('dbo.ListaEspera', 'IsDeleted') IS NULL
BEGIN
    ALTER TABLE dbo.ListaEspera
        ADD IsDeleted BIT NOT NULL
            CONSTRAINT DF_ListaEspera_IsDeleted DEFAULT (0);
END
GO

IF COL_LENGTH('dbo.ListaEspera', 'DeletedAt') IS NULL
BEGIN
    ALTER TABLE dbo.ListaEspera
        ADD DeletedAt DATETIME NULL;
END
GO

IF COL_LENGTH('dbo.ListaEspera', 'DeletedBy') IS NULL
BEGIN
    ALTER TABLE dbo.ListaEspera
        ADD DeletedBy INT NULL;
END
GO

/* 2) Índice principal para listar por viaje (filtra IsDeleted) */
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ListaEspera_ViajeID_IsDeleted'
      AND object_id = OBJECT_ID('dbo.ListaEspera')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_ListaEspera_ViajeID_IsDeleted
    ON dbo.ListaEspera (ViajeID, IsDeleted)
    INCLUDE (Fecha, UsuarioID, ClienteID, Observacion, PasajeroTemporal);
END
GO

/* 3) Índice auxiliar para validar permisos rápido en delete (si el PK/cluster NO es por Id) */
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ListaEspera_Id_Usuario_IsDeleted'
      AND object_id = OBJECT_ID('dbo.ListaEspera')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_ListaEspera_Id_Usuario_IsDeleted
    ON dbo.ListaEspera (Id, UsuarioID, IsDeleted);
END
GO

