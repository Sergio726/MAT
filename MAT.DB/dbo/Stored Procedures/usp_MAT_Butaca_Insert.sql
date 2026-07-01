CREATE PROCEDURE [dbo].[usp_MAT_Butaca_Insert]
    @ButacaID     UNIQUEIDENTIFIER = NULL,
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
 -- Description: Inserta butaca (NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @ButacaID IS NULL OR @ButacaID = '00000000-0000-0000-0000-000000000000'
        SET @ButacaID = NEWID();

    INSERT INTO dbo.Butaca (
        ButacaID, NroButaca, Piso, Ubicacion, Tipo,
        TransporteID, Fila, Posicion, CodigoButaca
    )
    VALUES (
        @ButacaID, @NroButaca, @Piso, @Ubicacion, @Tipo,
        @TransporteID, @Fila, @Posicion, @CodigoButaca
    );

    SELECT @ButacaID AS ButacaID;
END
