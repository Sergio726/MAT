CREATE PROCEDURE [dbo].[usp_MAT_Butaca_Update]
    @ButacaID     UNIQUEIDENTIFIER,
    @NroButaca    INT              = NULL,
    @Piso         INT              = NULL,
    @Ubicacion    INT              = NULL,
    @Tipo         INT              = NULL,
    @TransporteID UNIQUEIDENTIFIER = NULL,
    @Fila         VARCHAR(2)       = NULL,
    @Posicion     VARCHAR(1)       = NULL,
    @CodigoButaca VARCHAR(4)       = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Actualiza butaca (NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Butaca
    SET NroButaca    = @NroButaca,
        Piso         = @Piso,
        Ubicacion    = @Ubicacion,
        Tipo         = @Tipo,
        TransporteID = @TransporteID,
        Fila         = @Fila,
        Posicion     = @Posicion,
        CodigoButaca = @CodigoButaca
    WHERE ButacaID = @ButacaID;

    IF @@ROWCOUNT = 0
        RAISERROR('Butaca no encontrada.', 16, 1);
END
