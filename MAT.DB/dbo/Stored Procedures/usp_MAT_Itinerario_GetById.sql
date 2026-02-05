CREATE PROCEDURE [dbo].[usp_MAT_Itinerario_GetById]
    @ItinerarioID UNIQUEIDENTIFIER
AS
/*-- =============================================
-- Author:    Mati Terradas
-- Create date: 05-02-2026
-- Description:  Obtiene una parada de itinerario por su ID

-- =============================================*/
BEGIN
    SET NOCOUNT ON;

    SELECT
        [ItinerarioID],
        [Nombre],
        [Descripcion],
        [Activo]
    FROM [dbo].[Itinerario]
    WHERE [ItinerarioID] = @ItinerarioID;
END
