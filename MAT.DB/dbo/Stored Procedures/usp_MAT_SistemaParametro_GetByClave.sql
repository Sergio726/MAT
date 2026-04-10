-- =============================================
-- Author:    Sebastian Garcia
-- Create date: 2026-04-10
-- Description: Obtiene un parámetro del sistema por su clave (para validación de códigos)
-- =============================================
CREATE PROCEDURE [dbo].[usp_MAT_SistemaParametro_GetByClave]
    @Clave VARCHAR(100)
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
    WHERE Clave = @Clave AND EstaActivo = 1;
END