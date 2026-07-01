CREATE PROCEDURE [dbo].[usp_MAT_Localidad_GetById]
    @ID INT
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-01
 -- Description: Localidad por ID (NetTiers F2)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT ID, idDepartamento AS IdDepartamento, Nombre
    FROM dbo.Localidad
    WHERE ID = @ID;
END
