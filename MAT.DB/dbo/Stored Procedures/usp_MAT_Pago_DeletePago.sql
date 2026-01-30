
CREATE PROCEDURE [dbo].[usp_MAT_Pago_DeletePago](@PagoID uniqueidentifier, @VendedorID uniqueidentifier)
AS 
/*-- =============================================  
-- Author:    Garcia Sergio  
-- Create date: 09-27-2017  
-- Description:  delete pago 
-- 2018-03-15	Garcia Sergio update estadopasaje
-- 2018-04-29	Garcia Sergio Delete table PagoDetalle
-- =============================================*/ 
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

    BEGIN try 
        DECLARE @TipoPago  INT, 
                @Monto     MONEY, 
                @Recibo    VARCHAR(50), 
                @ClienteID UNIQUEIDENTIFIER,
				@FacturaID UNIQUEIDENTIFIER,
				@Saldo	   MONEY,
				@TotalFactura  money 

        SELECT  @TipoPago = p.TipoPago, 
                @Monto = p.Monto, 
                @ClienteID = c.ClienteID,
				@FacturaID = mc.FacturaID
        FROM   dbo.Pago p 
		INNER JOIN dbo.MovimientoCuenta mc
			on p.PagoID = mc.PagoID
		INNER JOIN dbo.Cuenta c
			on mc.CuentaID = c.CuentaID
        WHERE  p.PagoID = @PagoID 

        IF ( @TipoPago = 4 ) -----------Nota de Crédito 
        BEGIN 
            INSERT INTO dbo.CreditoCliente 
                        (VendedorID, 
                            ClienteID, 
                            Monto, 
                            Descripcion, 
                            IsInput) 
            VALUES      (@VendedorID, 
                            @ClienteID, 
                            @Monto, 
                            'PAGO CANCELADO - NRO RECIBO ' + @Recibo, 
                            1) 
        END 


        BEGIN TRAN 

		/*----------------Delete Pago------------------*/
        DELETE mc 
        FROM   dbo.MovimientoCuenta mc 
                INNER JOIN dbo.Pago p 
                        ON mc.PagoID = p.PagoID 
        WHERE  p.PagoID = @PagoID 

		delete dp
		from dbo.PagoDetalle dp
		inner join dbo.Pago p
			on dp.PagoID = p.PagoID
		where p.PagoID = @PagoID

        DELETE p 
        FROM   dbo.Pago p 
        WHERE  p.PagoID = @PagoID 

        COMMIT 
		/*----------------End Delete Pago---------------*/

		/*Actualizar estado de factura y pasaje*/ 
			/*calcular saldo de factura*/ 
			SELECT @Saldo = [dbo].[fn_MAT_SaldoFactura](@FacturaID)
			select @TotalFactura = sum(df.Precio * df.Cantidad) from dbo.DetalleFactura df where df.FacturaID = @FacturaID

			IF((@Saldo < @TotalFactura) and (@Saldo > 0)) 
			  BEGIN 
				--3  Señado 
				begin tran
				  UPDATE dbo.Factura 
				  SET    Estado = 3 
				  WHERE  FacturaID = @FacturaID 


				  UPDATE p 
				  SET    p.estadopasaje = (case p.estadopasaje
											when 6 then 8 -- Pagado - Con Hotel ==>Señado-ConHotel
											when 8 then 8
											else 3 
										   end )
				  FROM   pasaje p 
				  WHERE  p.facturaid = @FacturaID 
				  	
				commit
			  END 
			  else if (@Saldo = @TotalFactura)
			  begin
				  UPDATE dbo.Factura 
				  SET    Estado = 2 --Pre reserva
				  WHERE  FacturaID = @FacturaID 


				  UPDATE p 
				  SET    p.estadopasaje = ( CASE p.estadopasaje 
											  WHEN 6 THEN 9 --Pagado - Con Hotel ==> Pre-Reserva - Con Hotel
											  WHEN 8 THEN 9 --Señado - Con Hotel ==> Pre-Reserva - Con Hotel
											  WHEN 9 THEN 9 --Pre-Reserva - Con Hotel ==> Pre-Reserva - Con Hotel
											  ELSE 5 --Pre-reserva
											END ) 
				  FROM   dbo.Pasaje p 
				  WHERE  p.FacturaID = @FacturaID 
			  end
		/*-------------End Actualizar estado de factura y pasaje------------*/


    END try 

    BEGIN catch 
        IF @@TRANCOUNT > 0 
        ROLLBACK TRAN 

        DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + Error_line(), 
                @errState = Error_state() 

        RAISERROR (N'Error al eliminar el pago: %d',16,@errState,1,@errmsg); 
    END catch 
END
