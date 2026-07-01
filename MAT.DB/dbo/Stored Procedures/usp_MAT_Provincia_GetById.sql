CREATE PROCEDURE [dbo].[usp_MAT_Provincia_GetById]
    @ID INT
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-01
 -- Description: Provincia por ID (NetTiers F2)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT ID, Nombre, IdPais
    FROM dbo.Provincia
    WHERE ID = @ID;
END
