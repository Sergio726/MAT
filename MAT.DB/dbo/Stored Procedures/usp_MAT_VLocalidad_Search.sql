CREATE PROCEDURE [dbo].[usp_MAT_VLocalidad_Search]
    @Query NVARCHAR(250) = ''
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-01
 -- Description: Búsqueda de localidades por nombre (NetTiers F2). Mínimo 3 caracteres.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT ID, Nombre
    FROM dbo.vLocalidad
    WHERE LEN(LTRIM(RTRIM(@Query))) >= 3
      AND Nombre LIKE '%' + @Query + '%'
    ORDER BY Nombre;
END
