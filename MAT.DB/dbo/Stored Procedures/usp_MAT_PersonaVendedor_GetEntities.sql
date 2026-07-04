CREATE PROCEDURE [dbo].[usp_MAT_PersonaVendedor_GetEntities]
    @PersonaID UNIQUEIDENTIFIER = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Filas de la vista PersonaVendedor (NetTiers F7 - reemplaza PersonaVendedorService.GetAll).
 --              @PersonaID NULL devuelve todas; en caso contrario filtra por PersonaID.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT *
    FROM dbo.PersonaVendedor
    WHERE (@PersonaID IS NULL OR PersonaID = @PersonaID);
END
