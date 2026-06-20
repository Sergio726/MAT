CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Pasaje_CambiarPasajero]
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
-- 2026-06-19: Validar conflicto por FechaSalida en otros viajes.
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Pasaje WHERE PasajeID = @PasajeID)
        BEGIN
            SET @Result = 'Error: El pasaje especificado no existe.';
            RETURN;
        END
        
        IF NOT EXISTS (SELECT 1 FROM dbo.Persona WHERE PersonaID = @NuevoPasajeroID)
        BEGIN
            SET @Result = 'Error: El pasajero especificado no existe.';
            RETURN;
        END
        
        IF NOT EXISTS (SELECT 1 FROM dbo.Pasajero WHERE PasajeroID = @NuevoPasajeroID)
        BEGIN
            SET @Result = 'Error: El pasajero especificado no tiene registro en la tabla Pasajero.';
            RETURN;
        END

        DECLARE @ViajeID UNIQUEIDENTIFIER;

        SELECT @ViajeID = p.ViajeID
        FROM dbo.Pasaje p
        WHERE p.PasajeID = @PasajeID;

        IF dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(@NuevoPasajeroID, @ViajeID, @PasajeID) = 1
        BEGIN
            SET @Result = 'Error: El pasajero ya está registrado en otro viaje con la misma fecha de salida.';
            RETURN;
        END
        
        UPDATE dbo.Pasaje
        SET PasajeroID = @NuevoPasajeroID
        WHERE PasajeID = @PasajeID;
        
        IF @@ROWCOUNT = 0
        BEGIN
            SET @Result = 'Error: No se pudo actualizar el pasaje.';
            RETURN;
        END

        UPDATE dbo.ReservaHabitacion
        SET PasajeroID = @NuevoPasajeroID
        WHERE PasajeID = @PasajeID;
        
        SET @Result = 'Done.';
        
    END TRY
    BEGIN CATCH
        SET @Result = 'Error: ' + ERROR_MESSAGE();
    END CATCH
END
GO
