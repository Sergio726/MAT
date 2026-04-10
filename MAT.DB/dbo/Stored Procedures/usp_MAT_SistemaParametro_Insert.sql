-- =============================================
-- Author:    Sebastian Garcia
-- Create date: 2026-04-10
-- Description: Inserta un nuevo parámetro del sistema
-- =============================================
CREATE PROCEDURE [dbo].[usp_MAT_SistemaParametro_Insert]
    @Clave VARCHAR(100),
    @Valor NVARCHAR(MAX),
    @Descripcion NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM SistemaParametro WHERE Clave = @Clave)
    BEGIN
        RAISERROR('Ya existe un parámetro con la clave especificada.', 16, 1);
        RETURN;
    END

    INSERT INTO SistemaParametro (Clave, Valor, Descripcion, EstaActivo, FechaCreacion)
    VALUES (@Clave, @Valor, @Descripcion, 1, GETDATE());

    SELECT SCOPE_IDENTITY() AS Id;
END