CREATE PROCEDURE [dbo].[usp_MAT_Voucher_Insert]
    @VoucherID UNIQUEIDENTIFIER,
    @FechaEmision DATETIME = NULL,
    @VendedorID UNIQUEIDENTIFIER = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Alta de voucher (NetTiers F4 - reemplaza VoucherService.Insert).
 --              NroVoucher es IDENTITY y NrPrint toma su default (0).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.Voucher (VoucherID, FechaEmision, VendedorID)
    VALUES (@VoucherID, @FechaEmision, @VendedorID);
END
