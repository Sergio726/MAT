CREATE PROCEDURE [dbo].[usp_MAT_Excursion_Update]
    @ExcursionID UNIQUEIDENTIFIER,
    @Descripcion VARCHAR (200),
    @Costo FLOAT (53) = NULL,
    @Observaciones VARCHAR (MAX) = NULL,
    @ProveedorID UNIQUEIDENTIFIER = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de excursion (NetTiers F4 - reemplaza ExcursionService.Update)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Excursion
    SET Descripcion = @Descripcion,
        Costo = @Costo,
        Observaciones = @Observaciones,
        ProveedorID = @ProveedorID
    WHERE ExcursionID = @ExcursionID;
END
