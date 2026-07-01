CREATE PROCEDURE [dbo].[usp_MAT_Pais_GetAll]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-01
 -- Description: Lista todos los países (NetTiers F2)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT PaisID, Descripcion
    FROM dbo.Pais
    ORDER BY Descripcion;
END
