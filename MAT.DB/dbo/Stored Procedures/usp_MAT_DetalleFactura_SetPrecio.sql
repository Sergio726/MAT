
CREATE PROCEDURE [dbo].[usp_MAT_DetalleFactura_SetPrecio](@DetalleFacturaId int, @PrecioId UniqueIdentifier)
AS
/*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2018-05-30
  -- Description:  SET PRECIO FROM Detalle Factura
  
  --2018-05-30	Garcia Sergio: Quit Precio Id
  =================================================
  -- */
 BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	  BEGIN TRY 
			BEGIN TRAN
						
				declare @Monto float,
						@Descripcion varchar(100),
						@FacturaID UniqueIdentifier

				select @Monto = p.Monto,
					   @Descripcion = p.Descripcion
				from dbo.Precio p
				where p.PrecioID = @PrecioId

				

				update df
				set df.Detalle = 'PAQUETE ' + @Descripcion,
					df.Precio = @Monto,
					df.Fecha = GETDATE()
				from dbo.DetalleFactura df
				WHERE df.Id = @DetalleFacturaId
			  			
				/******update state factura and pasaje******/
				select @FacturaID = df.FacturaID
				from dbo.DetalleFactura df
				where df.Id = @DetalleFacturaId

				exec dbo.usp_MAT_Reserva_ActualizarEstados @FacturaID = @FacturaID;

				/*******end update*******/

			COMMIT TRAN; 
	  END TRY

	BEGIN CATCH
	IF @@TRANCOUNT > 0 
		ROLLBACK TRAN


		DECLARE @errmsg   AS NVARCHAR (2048),
				@errState int
		select  @errmsg = Error_message() + ERROR_LINE(), @errState = ERROR_STATE()
				RAISERROR (@errmsg,16,@errState);  
	END CATCH

END
