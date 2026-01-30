CREATE procedure [dbo].[usp_MAT_Reserva_DetalleFactura](@FacturaID varchar(36),
												@AdicionalesIDs varchar(max),
												@PrecioID varchar(36))
as
/*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 09-04-2017 
  -- Description:  load DetalleFactura
  
  -- */
begin
 SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted;		

	begin try

		declare @Detalle varchar(1000),
				@Precio money

		--inert pasaje
		select @Precio =  p.Monto,
			   @Detalle = 'PAQUETE ' + upper(p.Descripcion)
		from dbo.Precio p 
		where PrecioID = @PrecioID

		begin tran
			insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad)
			values (@FacturaID,@Detalle,@Precio,1)
		
		--insert adicional
		if (@AdicionalesIDs != '')
		begin
			
			insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad,AdicionalID)
			select @FacturaID,a.Descripcion, a.Monto,1,a.AdicionalID
			from dbo.Adicional a
			inner join dbo.Split(@AdicionalesIDs, ';') sp
				on a.AdicionalID = sp.Item
			
		end
	
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

