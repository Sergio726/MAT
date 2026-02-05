CREATE PROCEDURE [dbo].[usp_MAT_ItinerarioViaje_Update]
    @ItinerarioViajeID UNIQUEIDENTIFIER,
    @ItinerarioID UNIQUEIDENTIFIER = NULL,
    @Orden INT = NULL,
    @HoraAprox VARCHAR(10) = NULL,
    @DuracionMin INT = NULL,
    @Observacion VARCHAR(200) = NULL
AS
/*-- =============================================
-- Author:    Mati Terradas
-- Create date: 05-02-2026
-- Description:  Actualiza una parada del itinerario de un viaje

-- =============================================*/
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ItinerarioViaje]
    SET [ItinerarioID] = ISNULL(@ItinerarioID, [ItinerarioID]),
        [Orden] = ISNULL(@Orden, [Orden]),
        [HoraAprox] = @HoraAprox,
        [DuracionMin] = @DuracionMin,
        [Observacion] = @Observacion
    WHERE [ItinerarioViajeID] = @ItinerarioViajeID;

    SELECT
        ItinerarioViajeID = @ItinerarioViajeID,
        Result = 'Done.'
END
