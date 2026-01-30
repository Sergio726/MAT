CREATE PROCEDURE [dbo].[usp_MAT_Factura_GetDetalleByFacturaID](@FacturaId uniqueidentifier)
/*-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 2017-09-17
-- Description:  get detalleFactura

-- 2018-05-30	Garcia Sergio: add Id
-- =============================================*/
AS
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;

BEGIN
	SELECT df.Id,
	       df.Fecha,
		   df.Detalle,
		   df.Cantidad,
		   df.Precio
	FROM dbo.DetalleFactura df
	WHERE df.FacturaID = @FacturaId

END

