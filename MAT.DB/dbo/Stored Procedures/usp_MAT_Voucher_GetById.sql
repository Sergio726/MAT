CREATE PROCEDURE [dbo].[usp_MAT_Voucher_GetById]
    @VoucherID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Voucher por ID (NetTiers F4 - reemplaza VoucherService.GetByVoucherId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        VoucherID,
        NroVoucher,
        FechaEmision,
        VendedorID
    FROM dbo.Voucher
    WHERE VoucherID = @VoucherID;
END
