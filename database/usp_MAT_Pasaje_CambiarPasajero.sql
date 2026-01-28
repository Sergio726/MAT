CREATE PROCEDURE [dbo].[usp_MAT_Pasaje_CambiarPasajero]
(
    @PasajeID UNIQUEIDENTIFIER,
    @NuevoPasajeroID UNIQUEIDENTIFIER,
    @Result VARCHAR(100) OUTPUT
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Author:    Seba Garcia
-- Create date: 2026-01-28
-- Description: Cambia el pasajero asignado a un pasaje
-- Parámetros:
--   @PasajeID: ID del pasaje a modificar
--   @NuevoPasajeroID: ID del nuevo pasajero (PersonaID)
--   @Result: Resultado de la operación ("Done." si exitoso, mensaje de error si falla)
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    BEGIN TRY
        -- Validar que el pasaje existe
        IF NOT EXISTS (SELECT 1 FROM dbo.Pasaje WHERE PasajeID = @PasajeID)
        BEGIN
            SET @Result = 'Error: El pasaje especificado no existe.';
            RETURN;
        END
        
        -- Validar que el nuevo pasajero existe en la tabla Persona
        IF NOT EXISTS (SELECT 1 FROM dbo.Persona WHERE PersonaID = @NuevoPasajeroID)
        BEGIN
            SET @Result = 'Error: El pasajero especificado no existe.';
            RETURN;
        END
        
        -- Validar que existe un registro en Pasajero para este PersonaID
        IF NOT EXISTS (SELECT 1 FROM dbo.Pasajero WHERE PasajeroID = @NuevoPasajeroID)
        BEGIN
            SET @Result = 'Error: El pasajero especificado no tiene registro en la tabla Pasajero.';
            RETURN;
        END
        
        -- Actualizar el PasajeroID del pasaje
        UPDATE dbo.Pasaje
        SET PasajeroID = @NuevoPasajeroID
        WHERE PasajeID = @PasajeID;
        
        -- Verificar que se actualizó correctamente
        IF @@ROWCOUNT = 0
        BEGIN
            SET @Result = 'Error: No se pudo actualizar el pasaje.';
            RETURN;
        END
        
        SET @Result = 'Done.';
        
    END TRY
    BEGIN CATCH
        SET @Result = 'Error: ' + ERROR_MESSAGE();
    END CATCH
END
GO
