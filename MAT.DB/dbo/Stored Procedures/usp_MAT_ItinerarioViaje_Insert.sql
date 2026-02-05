CREATE PROCEDURE [dbo].[usp_MAT_ItinerarioViaje_Insert]
    @ViajeID UNIQUEIDENTIFIER,
    @ItinerarioID UNIQUEIDENTIFIER,
    @Orden INT,
    @HoraAprox VARCHAR(10) = NULL,
    @DuracionMin INT = NULL,
    @Observacion VARCHAR(200) = NULL
AS
/*-- =============================================
-- Author:    Mati Terradas
-- Create date: 05-02-2026
-- Description:  Agrega una parada al itinerario de un viaje

-- =============================================*/
BEGIN
    SET NOCOUNT ON;

    DECLARE @ItinerarioViajeID UNIQUEIDENTIFIER = NEWID();

    -- If Orden is 0 or NULL, calculate the next order number
    IF @Orden IS NULL OR @Orden = 0
    BEGIN
        SELECT @Orden = ISNULL(MAX([Orden]), 0) + 1
        FROM [dbo].[ItinerarioViaje]
        WHERE [ViajeID] = @ViajeID;
    END

    INSERT INTO [dbo].[ItinerarioViaje]
        ([ItinerarioViajeID], [ViajeID], [ItinerarioID], [Orden], [HoraAprox], [DuracionMin], [Observacion])
    VALUES
        (@ItinerarioViajeID, @ViajeID, @ItinerarioID, @Orden, @HoraAprox, @DuracionMin, @Observacion);

    SELECT
        ItinerarioViajeID = @ItinerarioViajeID,
        Result = 'Done.'
END
