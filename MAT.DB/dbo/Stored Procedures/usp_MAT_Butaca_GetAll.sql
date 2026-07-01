CREATE PROCEDURE [dbo].[usp_MAT_Butaca_GetAll]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Lista todas las butacas (NetTiers F3)
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
    ORDER BY TransporteID, NroButaca DESC;
END
