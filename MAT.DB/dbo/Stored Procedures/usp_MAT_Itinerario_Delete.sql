CREATE PROCEDURE [dbo].[usp_MAT_Itinerario_Delete]
    @ItinerarioID UNIQUEIDENTIFIER
AS
/*-- =============================================
-- Author:    Mati Terradas
-- Create date: 05-02-2026
-- Description:  Elimina logicamente una parada (soft delete)

-- =============================================*/
BEGIN
    SET NOCOUNT ON;

    -- Soft delete: set Activo = 0
    UPDATE [dbo].[Itinerario]
    SET [Activo] = 0
    WHERE [ItinerarioID] = @ItinerarioID;

    SELECT
        ItinerarioID = @ItinerarioID,
        Result = 'Done.'
END
