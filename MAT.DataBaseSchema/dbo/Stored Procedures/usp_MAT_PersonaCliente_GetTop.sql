CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_GetTop]
(
    @SearchTerm NVARCHAR(200) = '',
    @TopCount INT = 10
)
AS 
/*
----------------------------------------------------------------------------------------------------
-- Author:    Seba Garcia
-- Create date: 2026-01-08
-- Description: Obtiene los TOP N clientes con búsqueda optimizada
-- Optimizaciones:
--   - Usa TOP para limitar resultados
--   - Búsqueda con LIKE optimizado en múltiples campos
--   - Optimiza IsTituarFactura usando EXISTS en lugar de subconsulta correlacionada
--   - Usa READ UNCOMMITTED para mejor rendimiento en lecturas
----------------------------------------------------------------------------------------------------
*/
BEGIN 
    SET NOCOUNT, XACT_ABORT ON; 
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; 

    -- Limpiar y preparar término de búsqueda
    DECLARE @SearchPattern NVARCHAR(202) = '';
    IF LEN(LTRIM(RTRIM(@SearchTerm))) > 0
    BEGIN
        SET @SearchPattern = '%' + LTRIM(RTRIM(@SearchTerm)) + '%';
    END

    -- Validar TopCount
    IF @TopCount <= 0 OR @TopCount > 100
        SET @TopCount = 10;

    -- Query optimizado
    SELECT TOP (@TopCount)
        p.PersonaID,
        p.Apellido, 
        p.Nombre, 
        p.NroDocumento, 
        p.Telefono, 
        p.Celular,
        LocalidadNombre = ISNULL(l.Nombre, ''), 
        p.Nacionalidad, 
        p.PaisResidencia,
        -- Optimización: usar EXISTS en lugar de subconsulta correlacionada
        IsTituarFactura = CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM dbo.Factura f WITH (NOLOCK)
                WHERE f.ClienteID = p.PersonaID
            ) THEN 1 
            ELSE 0 
        END
    FROM dbo.Persona p WITH (NOLOCK)
    LEFT JOIN dbo.Localidad l WITH (NOLOCK)
        ON p.LocalidadID = l.ID
    WHERE 
        -- Si no hay término de búsqueda, devolver todos (limitado por TOP)
        (@SearchPattern = '' OR 
        -- Búsqueda en múltiples campos
        p.Apellido LIKE @SearchPattern OR
        p.Nombre LIKE @SearchPattern OR
        p.NroDocumento LIKE @SearchPattern OR
        p.Telefono LIKE @SearchPattern OR
        p.Celular LIKE @SearchPattern OR
        ISNULL(l.Nombre, '') LIKE @SearchPattern)
    ORDER BY 
        p.Apellido ASC,
        p.Nombre ASC;

END

GO

/*
-- Scripts recomendados para índices (ejecutar por separado si no existen):

-- Índice para búsqueda por nombre y apellido
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Persona_Apellido_Nombre' AND object_id = OBJECT_ID('dbo.Persona'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Persona_Apellido_Nombre]
    ON [dbo].[Persona] ([Apellido], [Nombre])
    INCLUDE ([PersonaID], [NroDocumento], [Telefono], [Celular], [LocalidadID], [Nacionalidad], [PaisResidencia]);
END
GO

-- Índice para búsqueda por documento
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Persona_NroDocumento' AND object_id = OBJECT_ID('dbo.Persona'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Persona_NroDocumento]
    ON [dbo].[Persona] ([NroDocumento])
    WHERE [NroDocumento] IS NOT NULL;
END
GO

-- Índice para búsqueda en teléfonos
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Persona_Telefono' AND object_id = OBJECT_ID('dbo.Persona'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Persona_Telefono]
    ON [dbo].[Persona] ([Telefono], [Celular])
    WHERE [Telefono] IS NOT NULL OR [Celular] IS NOT NULL;
END
GO

-- Índice para Factura (optimización de EXISTS)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Factura_ClienteID' AND object_id = OBJECT_ID('dbo.Factura'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Factura_ClienteID]
    ON [dbo].[Factura] ([ClienteID]);
END
GO
*/

