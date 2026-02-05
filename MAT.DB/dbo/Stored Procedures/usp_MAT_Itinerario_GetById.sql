CREATE PROCEDURE [dbo].[usp_MAT_Itinerario_GetById]
    @ItinerarioID UNIQUEIDENTIFIER
AS
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
