-- Unifica eliminar/cancelar viaje en un solo SP con auditoría obligatoria.
-- Reemplaza usp_MAT_Viaje_CancelViaje (si se publicó) y amplía usp_MAT_Viaje_DeleteViaje.

IF OBJECT_ID(N'dbo.usp_MAT_Viaje_CancelViaje', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Viaje_CancelViaje;
GO

IF OBJECT_ID(N'dbo.usp_MAT_Viaje_DeleteViaje', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.usp_MAT_Viaje_DeleteViaje AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE [dbo].[usp_MAT_Viaje_DeleteViaje]
    @ViajeID       UNIQUEIDENTIFIER,
    @VendedorId    UNIQUEIDENTIFIER,
    @DeleteDetalle VARCHAR(500)
AS
/*-- =============================================
 -- Author:    Garcia Sergio (original 2018-05-19)
 -- Updated:   Sebastian Garcia 2026-07-07
 -- Description: Elimina un viaje y dependencias, con auditoría obligatoria
 --              en ViajeAudit (unifica cancelar/eliminar).
 ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    BEGIN TRY
        IF @VendedorId IS NULL
        BEGIN
            RAISERROR(N'Debe indicar el vendedor que realiza la operación.', 16, 1);
            RETURN;
        END;

        IF NULLIF(LTRIM(RTRIM(@DeleteDetalle)), '') IS NULL
        BEGIN
            RAISERROR(N'Debe indicar el motivo de la eliminación.', 16, 1);
            RETURN;
        END;

        DECLARE @ViajeNombre VARCHAR(100),
                @DeleteOn    DATETIME = GETDATE(),
                @DeleteUser  VARCHAR(50);

        SELECT @ViajeNombre = ISNULL(NULLIF(LTRIM(RTRIM(v.Descripcion)), ''), '(sin descripción)')
        FROM dbo.Viaje v
        WHERE v.ViajeID = @ViajeID;

        IF @ViajeNombre IS NULL
        BEGIN
            RAISERROR(N'El viaje indicado no existe.', 16, 1);
            RETURN;
        END;

        SELECT @DeleteUser = LTRIM(RTRIM(p.Apellido + ' ' + p.Nombre))
        FROM dbo.Vendedor v
             INNER JOIN dbo.Persona p ON v.VendedorID = p.PersonaID
        WHERE v.VendedorID = @VendedorId;

        IF @DeleteUser IS NULL OR @DeleteUser = ''
            SET @DeleteUser = 'DESCONOCIDO';

        BEGIN TRAN;

        EXEC dbo.usp_MAT_ViajeAudit_Cancel
            @ViajeId = @ViajeID,
            @ViajeNombre = @ViajeNombre,
            @DeleteOn = @DeleteOn,
            @DeleteByUserId = @VendedorId,
            @DeleteUser = @DeleteUser,
            @DeleteDetalle = @DeleteDetalle;

        DELETE rh
        FROM dbo.ReservaHabitacion rh
             INNER JOIN dbo.Pasaje p ON rh.PasajeID = p.PasajeID
        WHERE p.ViajeID = @ViajeID;

        DELETE p
        FROM dbo.Pasaje p
        WHERE p.ViajeID = @ViajeID;

        DELETE thv
        FROM dbo.TransHotelHabitacionViaje thv
        WHERE thv.ViajeID = @ViajeID;

        DELETE vh
        FROM dbo.ViajeHotel vh
        WHERE vh.ViajeID = @ViajeID;

        DELETE FROM dbo.Viaje
        WHERE ViajeID = @ViajeID;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        DECLARE @errmsg NVARCHAR(2048) = ERROR_MESSAGE();
        RAISERROR(N'Error al eliminar el viaje: %s', 16, 1, @errmsg);
    END CATCH;
END;
GO
