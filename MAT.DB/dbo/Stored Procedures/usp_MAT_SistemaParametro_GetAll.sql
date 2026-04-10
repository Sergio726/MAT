-- =============================================
-- Author:    Sebastian Garcia
-- Create date: 2026-04-10
-- Description: Obtiene todos los parámetros del sistema (para UI admin)
-- =============================================
CREATE PROCEDURE [dbo].[usp_MAT_SistemaParametro_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Clave,
        Valor,
        Descripcion,
        EstaActivo,
        FechaCreacion,
        FechaModificacion
    FROM SistemaParametro
    ORDER BY FechaCreacion DESC;
END