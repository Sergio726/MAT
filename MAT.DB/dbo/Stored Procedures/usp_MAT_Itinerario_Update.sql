CREATE PROCEDURE [dbo].[usp_MAT_Itinerario_Update]
    @ItinerarioID UNIQUEIDENTIFIER,
    @Nombre VARCHAR(200),
    @Descripcion VARCHAR(500) = NULL
AS
/*-- =============================================
-- Author:    Mati Terradas
-- Create date: 05-02-2026
-- Description:  Actualiza una parada de itinerario existente

-- =============================================*/
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Itinerario]
    SET [Nombre] = @Nombre,
        [Descripcion] = @Descripcion
    WHERE [ItinerarioID] = @ItinerarioID;

    SELECT
        ItinerarioID = @ItinerarioID,
        Result = 'Done.'
END
