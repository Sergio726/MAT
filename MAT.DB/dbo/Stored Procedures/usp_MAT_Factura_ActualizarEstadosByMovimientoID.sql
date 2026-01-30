CREATE PROCEDURE [dbo].[usp_MAT_Factura_ActualizarEstadosByMovimientoID]( @MovimientoID uniqueidentifier,
														              @NroFactura varchar(50) = null )
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 12/04/2017
  -- Description:  check state of Factura and Pasajes
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		BEGIN TRY 
			BEGIN TRAN
			DECLARE @Saldo             FLOAT,
					@FacturaID	       uniqueidentifier

			/*Actualizar estado de factura y pasaje*/ 
			/*calcular saldo de factura*/ 
			SELECT @FacturaID = mv.FacturaID
			FROM dbo.MovimientoCuenta  mv
			WHERE mv.MovimientoID = @MovimientoID

			SELECT @Saldo = ( f.monto - Sum(p.monto) ) 
			FROM   dbo.Factura f 
				   INNER JOIN dbo.MovimientoCuenta mc 
						   ON f.facturaid = mc.facturaid 
				   INNER JOIN dbo.Pago p 
						   ON mc.pagoid = p.pagoid 
			WHERE  f.FacturaID = @FacturaID
			group by f.monto


			IF( @Saldo <= 0 ) 
			  BEGIN 
				  --4  Pagado 
				  UPDATE factura 
				  SET    estado = 4, 
						 nrofactura = case when @NroFactura is null then NroFactura else @NroFactura end
				  WHERE  facturaid = @FacturaID 

				  SELECT p.pasajeid, 
						 p.estadopasaje 
				  INTO   #tmp
				  FROM   pasaje p 
				  WHERE  p.facturaid = @FacturaID 

				  UPDATE p 
				  SET    p.estadopasaje = ( CASE p.estadopasaje 
											  WHEN 8 THEN 6 --Reserva-Hotel 
											  WHEN 9 THEN 6 --Reserva-Hotel 
											  WHEN 7 THEN 7 --Anulado 
											  ELSE 4 
											END )--Pagado 
				  FROM   dbo.pasaje p 
						 INNER JOIN #tmp t 
								 ON p.pasajeid = t.pasajeid 
			
			  END 
			ELSE 
			  BEGIN 
				  --3  Señado 
				  UPDATE factura 
				  SET    estado = 3 
				  WHERE  facturaid = @FacturaID 

				  UPDATE p 
				  SET    p.estadopasaje = ( CASE p.estadopasaje 
											  WHEN 9 THEN 8 
											  WHEN 8 THEN 8 --Pasaje y Hotel prereservados 
											  ELSE 3 
											END ) 
				  FROM   pasaje p 
				  WHERE  p.facturaid = @FacturaID 

				 DELETE v 
				 FROM   dbo.voucher v 
				 		INNER JOIN pasaje p 
				 				ON v.voucherid = p.voucherid 
				 WHERE  p.facturaid = @FacturaID 


			  END 

			SELECT 1 AS ID, 
				   'Done.'            AS Result 
	
			COMMIT TRAN; 
		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048)
			SELECT @errmsg = Error_message()

			select -1 as ID, @errmsg AS Result
			
		END CATCH




     

  END

