-- =============================================
-- Author:    Sebastian Garcia
-- Create date: 2026-04-10
-- Description: Actualiza un parámetro del sistema
-- =============================================
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
        RAISERROR('No existe el parámetro especificado.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM SistemaParametro WHERE Clave = @Clave AND Id <> @Id)
    BEGIN
        RAISERROR('Ya existe otro parámetro con la clave especificada.', 16, 1);
        RETURN;
    END

    UPDATE SistemaParametro
    SET Clave = @Clave, 
        Valor = @Valor, 
        Descripcion = @Descripcion,
        FechaModificacion = GETDATE()
    WHERE Id = @Id;

    SELECT 1 AS Result;
END