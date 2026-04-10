-- =============================================
-- Author:    Sebastian Garcia
-- Create date: 2026-04-10
-- Description: Migración de códigos de confirmación de Web.config a BD
--              Tabla SistemaParametro + datos iniciales
-- =============================================

-- Crear tabla (si no existe por SSDT)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SistemaParametro]') AND type in (N'U'))
BEGIN
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
END
GO

-- Insertar datos iniciales (códigos de confirmación para eliminar ventas/pasajeros)
IF NOT EXISTS (SELECT 1 FROM SistemaParametro WHERE Clave = 'CodigoConfirmacion_1')
BEGIN
    INSERT INTO SistemaParametro (Clave, Valor, Descripcion, EstaActivo)
    VALUES ('CodigoConfirmacion_1', 'Tinto.29', 'Código de confirmación para eliminar ventas/pasajeros', 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM SistemaParametro WHERE Clave = 'CodigoConfirmacion_2')
BEGIN
    INSERT INTO SistemaParametro (Clave, Valor, Descripcion, EstaActivo)
    VALUES ('CodigoConfirmacion_2', 'Martes.2025', 'Código de confirmación para eliminar ventas/pasajeros', 1);
END
GO

-- Stored Procedures (si no existen por SSDT)
IF OBJECT_ID('dbo.usp_MAT_SistemaParametro_GetByClave', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE [dbo].[usp_MAT_SistemaParametro_GetByClave]
        @Clave VARCHAR(100)
    AS
    BEGIN
        SET NOCOUNT ON;
        SELECT Id, Clave, Valor, Descripcion, EstaActivo, FechaCreacion, FechaModificacion
        FROM SistemaParametro
        WHERE Clave = @Clave AND EstaActivo = 1;
    END
    ');
END
GO

IF OBJECT_ID('dbo.usp_MAT_SistemaParametro_GetAll', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE [dbo].[usp_MAT_SistemaParametro_GetAll]
    AS
    BEGIN
        SET NOCOUNT ON;
        SELECT Id, Clave, Valor, Descripcion, EstaActivo, FechaCreacion, FechaModificacion
        FROM SistemaParametro
        ORDER BY FechaCreacion DESC;
    END
    ');
END
GO

IF OBJECT_ID('dbo.usp_MAT_SistemaParametro_Insert', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE [dbo].[usp_MAT_SistemaParametro_Insert]
        @Clave VARCHAR(100),
        @Valor NVARCHAR(MAX),
        @Descripcion NVARCHAR(500) = NULL
    AS
    BEGIN
        SET NOCOUNT ON;
        IF EXISTS (SELECT 1 FROM SistemaParametro WHERE Clave = @Clave)
        BEGIN
            RAISERROR(''Ya existe un parámetro con la clave especificada.'', 16, 1);
            RETURN;
        END
        INSERT INTO SistemaParametro (Clave, Valor, Descripcion, EstaActivo, FechaCreacion)
        VALUES (@Clave, @Valor, @Descripcion, 1, GETDATE());
        SELECT SCOPE_IDENTITY() AS Id;
    END
    ');
END
GO

IF OBJECT_ID('dbo.usp_MAT_SistemaParametro_Update', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE [dbo].[usp_MAT_SistemaParametro_Update]
        @Id INT,
        @Clave VARCHAR(100),
        @Valor NVARCHAR(MAX),
        @Descripcion NVARCHAR(500) = NULL
    AS
    BEGIN
        SET NOCOUNT ON;
        IF NOT EXISTS (SELECT 1 FROM SistemaParametro WHERE Id = @Id)
        BEGIN
            RAISERROR(''No existe el parámetro especificado.'', 16, 1);
            RETURN;
        END
        IF EXISTS (SELECT 1 FROM SistemaParametro WHERE Clave = @Clave AND Id <> @Id)
        BEGIN
            RAISERROR(''Ya existe otro parámetro con la clave especificada.'', 16, 1);
            RETURN;
        END
        UPDATE SistemaParametro
        SET Clave = @Clave, Valor = @Valor, Descripcion = @Descripcion, FechaModificacion = GETDATE()
        WHERE Id = @Id;
        SELECT 1 AS Result;
    END
    ');
END
GO

IF OBJECT_ID('dbo.usp_MAT_SistemaParametro_Toggle', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE [dbo].[usp_MAT_SistemaParametro_Toggle]
        @Id INT
    AS
    BEGIN
        SET NOCOUNT ON;
        IF NOT EXISTS (SELECT 1 FROM SistemaParametro WHERE Id = @Id)
        BEGIN
            RAISERROR(''No existe el parámetro especificado.'', 16, 1);
            RETURN;
        END
        UPDATE SistemaParametro
        SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END, FechaModificacion = GETDATE()
        WHERE Id = @Id;
        SELECT EstaActivo FROM SistemaParametro WHERE Id = @Id;
    END
    ');
END
GO

PRINT 'Migración SistemaParametro completada correctamente.';
SELECT * FROM SistemaParametro;