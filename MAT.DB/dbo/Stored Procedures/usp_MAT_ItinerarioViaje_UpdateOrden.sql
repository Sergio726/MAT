CREATE PROCEDURE [dbo].[usp_MAT_ItinerarioViaje_UpdateOrden]
    @ItinerarioViajeID UNIQUEIDENTIFIER,
    @NewOrden INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ViajeID UNIQUEIDENTIFIER;
    DECLARE @CurrentOrden INT;

    -- Get current values
    SELECT @ViajeID = [ViajeID], @CurrentOrden = [Orden]
    FROM [dbo].[ItinerarioViaje]
    WHERE [ItinerarioViajeID] = @ItinerarioViajeID;

    IF @CurrentOrden <> @NewOrden
    BEGIN
        IF @NewOrden > @CurrentOrden
        BEGIN
            -- Moving down: decrease order of items in between
            UPDATE [dbo].[ItinerarioViaje]
            SET [Orden] = [Orden] - 1
            WHERE [ViajeID] = @ViajeID
              AND [Orden] > @CurrentOrden
              AND [Orden] <= @NewOrden;
        END
        ELSE
        BEGIN
            -- Moving up: increase order of items in between
            UPDATE [dbo].[ItinerarioViaje]
            SET [Orden] = [Orden] + 1
            WHERE [ViajeID] = @ViajeID
              AND [Orden] >= @NewOrden
              AND [Orden] < @CurrentOrden;
        END

        -- Update the item's order
        UPDATE [dbo].[ItinerarioViaje]
        SET [Orden] = @NewOrden
        WHERE [ItinerarioViajeID] = @ItinerarioViajeID;
    END

    SELECT
        ItinerarioViajeID = @ItinerarioViajeID,
        Result = 'Done.'
END
