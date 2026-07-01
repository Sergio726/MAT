CREATE PROCEDURE [dbo].[usp_MAT_Excursion_GetById]
    @ExcursionID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Excursión por ID (lookups NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        ExcursionID,
        Descripcion,
        Costo,
        Observaciones,
        ProveedorID
    FROM dbo.Excursion
    WHERE ExcursionID = @ExcursionID;
END
