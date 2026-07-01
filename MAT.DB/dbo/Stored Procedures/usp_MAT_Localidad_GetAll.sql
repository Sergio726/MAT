CREATE PROCEDURE [dbo].[usp_MAT_Localidad_GetAll]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-01
 -- Description: Todas las localidades ID/Nombre para dropdowns (NetTiers F2)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT ID, Nombre
    FROM dbo.Localidad
    ORDER BY Nombre;
END
