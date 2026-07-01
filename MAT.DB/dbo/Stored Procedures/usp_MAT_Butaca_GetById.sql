CREATE PROCEDURE [dbo].[usp_MAT_Butaca_GetById]
    @ButacaID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Butaca por ID (NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        ButacaID,
        NroButaca,
        Piso,
        Ubicacion,
        Tipo,
        TransporteID,
        Fila,
        Posicion,
        CodigoButaca
    FROM dbo.Butaca
    WHERE ButacaID = @ButacaID;
END
