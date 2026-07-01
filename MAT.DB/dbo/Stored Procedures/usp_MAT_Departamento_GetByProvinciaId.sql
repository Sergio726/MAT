CREATE PROCEDURE [dbo].[usp_MAT_Departamento_GetByProvinciaId]
    @IdProvincia INT
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-01
 -- Description: Departamentos por provincia (NetTiers F2)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT ID, idProvincia AS IdProvincia, Nombre
    FROM dbo.Departamento
    WHERE idProvincia = @IdProvincia
    ORDER BY Nombre;
END
