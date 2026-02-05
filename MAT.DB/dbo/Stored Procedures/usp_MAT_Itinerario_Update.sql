CREATE PROCEDURE [dbo].[usp_MAT_Itinerario_Update]
    @ItinerarioID UNIQUEIDENTIFIER,
    @Nombre VARCHAR(200),
    @Descripcion VARCHAR(500) = NULL
AS
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
