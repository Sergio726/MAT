CREATE PROCEDURE [dbo].[usp_MAT_Itinerario_GetAll]
AS
/*-- =============================================
-- Author:    Mati Terradas
-- Create date: 05-02-2026
-- Description:  Obtiene todas las paradas de itinerario activas

-- =============================================*/
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
