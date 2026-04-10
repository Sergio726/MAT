-- =============================================
-- Author:    Sebastian Garcia
-- Create date: 2026-04-10
-- Description: Activa o desactiva un parámetro del sistema
-- =============================================
CREATE PROCEDURE [dbo].[usp_MAT_SistemaParametro_Toggle]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM SistemaParametro WHERE Id = @Id)
    BEGIN
        RAISERROR('No existe el parámetro especificado.', 16, 1);
        RETURN;
    END

    UPDATE SistemaParametro
    SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END,
        FechaModificacion = GETDATE()
    WHERE Id = @Id;

    SELECT EstaActivo FROM SistemaParametro WHERE Id = @Id;
END