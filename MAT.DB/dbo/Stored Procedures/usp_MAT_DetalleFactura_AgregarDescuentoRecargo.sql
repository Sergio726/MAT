CREATE PROCEDURE [dbo].[usp_MAT_DetalleFactura_AgregarDescuentoRecargo]
	@FacturaID varchar(36),
	@Detalle varchar(200),
	@Monto money,
	@IsDescuento bit = 1
as
/*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 09-17-2017 
  -- Description:  add descuento or recargo
  
  -- */
begin
 SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted;		

	begin try

		if (@IsDescuento = 1)
			SET @Monto = -@Monto

		begin tran
			insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad)
			values (@FacturaID,@Detalle,@Monto,1)
	
		commit
	end try

	begin catch
		IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048),
					@errState int
			select  @errmsg = Error_message() + ERROR_LINE(), @errState = ERROR_STATE()
					RAISERROR (@errmsg,16,@errState);  

	end catch

end

