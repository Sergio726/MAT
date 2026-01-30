CREATE PROCEDURE [dbo].[usp_MAT_Pago_GetTotalPagoByFacturaID](  @FacturaID	uniqueidentifier )
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2018-06-02
  -- Description:  calcula el total de pagos hechos en una factura
  -- History:
  --
  -- ============================================= 
  */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	declare @Pagos as table(Monto money)
	
	--only get pagos on Argentinos
		
	insert into @Pagos
	select p.Monto
	from Pago p
	inner join dbo.movimientocuenta mc
		on mc.PagoID = p.PagoID
	left join PagoDetalle pd
		on p.PagoID = pd.PagoID
	where mc.FacturaID = @FacturaID
	and pd.Id is null


	insert into @Pagos
	select Monto = iif(pd.MontoRecibidoMonedaTipo = 1, pd.MontoRecibido, pd.MontoEquivalente)--1 = argentino
	from Pago p
	inner join dbo.movimientocuenta mc
		on mc.PagoID = p.PagoID
	inner join PagoDetalle pd
		on p.PagoID = pd.PagoID
	where mc.FacturaID = @FacturaID


	select TotalPagos = sum(Monto) from @Pagos

END;

