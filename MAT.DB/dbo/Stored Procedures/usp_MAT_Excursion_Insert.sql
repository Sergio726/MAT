CREATE PROCEDURE [dbo].[usp_MAT_Excursion_Insert]
    @ExcursionID UNIQUEIDENTIFIER,
    @Descripcion VARCHAR (200),
    @Costo FLOAT (53) = NULL,
    @Observaciones VARCHAR (MAX) = NULL,
    @ProveedorID UNIQUEIDENTIFIER = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Alta de excursion (NetTiers F4 - reemplaza ExcursionService.Insert)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.Excursion (ExcursionID, Descripcion, Costo, Observaciones, ProveedorID)
    VALUES (@ExcursionID, @Descripcion, @Costo, @Observaciones, @ProveedorID);
END
