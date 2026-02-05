CREATE PROCEDURE [dbo].[usp_MAT_ItinerarioViaje_Delete]
    @ItinerarioViajeID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ViajeID UNIQUEIDENTIFIER;
    DECLARE @DeletedOrden INT;

    -- Get the ViajeID and Orden before deleting
    SELECT @ViajeID = [ViajeID], @DeletedOrden = [Orden]
    FROM [dbo].[ItinerarioViaje]
    WHERE [ItinerarioViajeID] = @ItinerarioViajeID;

    -- Delete the record
    DELETE FROM [dbo].[ItinerarioViaje]
    WHERE [ItinerarioViajeID] = @ItinerarioViajeID;

    -- Reorder remaining items
    UPDATE [dbo].[ItinerarioViaje]
    SET [Orden] = [Orden] - 1
    WHERE [ViajeID] = @ViajeID
      AND [Orden] > @DeletedOrden;

    SELECT
        ItinerarioViajeID = @ItinerarioViajeID,
        Result = 'Done.'
END
