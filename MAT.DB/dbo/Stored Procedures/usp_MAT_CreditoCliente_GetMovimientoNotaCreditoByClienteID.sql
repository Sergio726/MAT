CREATE PROCEDURE [dbo].[usp_MAT_CreditoCliente_GetMovimientoNotaCreditoByClienteID] (@ClienteID uniqueidentifier)
as
-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 09/30/2017
-- Description:  get i/o creditocliente
--2018-03-03 Garcia Sergio add column Vendedor
-- =============================================
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;
BEGIN 
	select ccl.NotaCreditoID,
		   ccl.Monto,
		   Descripcion = upper(ccl.Descripcion),
		   ccl.Fecha,
		   Vendedor = p.FullName,
		   ccl.PagoID,
		   ccl.FacturaID,
		   NroRecibo = pag.NroRecibo,
		   NroFactura = f.NroFactura
	from dbo.CreditoCliente ccl
	inner join dbo.Persona p
		on ccl.VendedorID = p.PersonaID
	left join dbo.Pago pag on pag.PagoID = ccl.PagoID
	left join dbo.Factura f on f.FacturaID = ccl.FacturaID
	where ccl.ClienteID = @ClienteID
	order by ccl.Id desc
	
	select Cliente = p.FullName from Persona p where p.PersonaID = @ClienteID

END
