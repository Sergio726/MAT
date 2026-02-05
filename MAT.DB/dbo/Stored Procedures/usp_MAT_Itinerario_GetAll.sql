CREATE PROCEDURE [dbo].[usp_MAT_Itinerario_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [ItinerarioID],
        [Nombre],
        [Descripcion],
        [Activo]
    FROM [dbo].[Itinerario]
    WHERE [Activo] = 1
    ORDER BY [Nombre];
END
