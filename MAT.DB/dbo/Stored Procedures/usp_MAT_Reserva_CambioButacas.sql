CREATE PROCEDURE [dbo].[usp_MAT_Reserva_CambioButacas](@AdicionalesIDs varchar(1000),@OldPasaje uniqueidentifier, @NewPasaje uniqueidentifier)
AS
/*-- =============================================   
-- Author:    Garcia Sergio   
-- Create date: 10-02-2017   
-- Description:  CHANGE BUTACA
   2018-01-28	Garcia Sergio: delete adiccional butaca cama when user go butaca change;
							   update reserva habitacion
--2018-05-30	Garcia Sergio: Quit Precio Id from Pasaje   
--2019-07-31	Garcia Sergio: Fix, only insert  adicional when adicional not exists in Detalle factura    
-- =============================================*/ 
		SET nocount, xact_abort ON; 
		SET TRANSACTION isolation level READ uncommitted; 
begin
	BEGIN TRY

		declare @FacturaID uniqueidentifier 
		select p.PasajeID,
			   p.PasajeroID,
			   p.FechaReserva,
			   p.FechaCompra,
			   p.FacturaID,
			   p.VoucherID,
			   p.EstadoPasaje,
			   NewPasajeID = @NewPasaje
		into #tOldPasaje
		from dbo.Pasaje p
		where p.PasajeID = @OldPasaje	

	
		--insert adicional
		BEGIN TRAN

		select @FacturaID = t.FacturaID from #tOldPasaje t
		if (@AdicionalesIDs != '')
		begin
			insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad,AdicionalID)
			select @FacturaID,a.Descripcion + ' - CAMBIO DE BUTACA', a.Monto,1,a.AdicionalID
			from dbo.Adicional a
			inner join dbo.Split(@AdicionalesIDs, ';') sp
				on a.AdicionalID = sp.Item
			where not exists (select * from dbo.DetalleFactura df where df.FacturaID = @FacturaID and df.AdicionalID = sp.Item)
				
		end
		else
		begin
			delete dbo.DetalleFactura
			where DetalleFactura.Id  = (
										select top 1 df.Id
										from dbo.DetalleFactura df
										inner join dbo.Adicional a
											on df.AdicionalID = a.AdicionalID
										where a.Descripcion	like 'BUTACA CAMA%'
										and df.FacturaID = @FacturaID
										)
			
			
		end

		/*update Old pasaje*/
		update p
		set p.EstadoPasaje = 1,
			p.FechaReserva = null,
			p.FechaCompra = null,
			p.FacturaID = null,
			p.VoucherID = null,
			p.PasajeroID = null
		from dbo.Pasaje p
		inner join #tOldPasaje t
			on p.FacturaID = t.FacturaID
			and p.PasajeID = t.PasajeID

		/*update New Pasaje*/
		update p
		set p.EstadoPasaje = t.EstadoPasaje,
			p.FechaReserva = t.FechaReserva,
			p.FechaCompra = t.FechaCompra,
			p.FacturaID = t.FacturaID,
			p.VoucherID = t.VoucherID,
			p.PasajeroID = t.PasajeroID
		from dbo.Pasaje p
		inner join #tOldPasaje t
			on p.PasajeID = t.NewPasajeID
		COMMIT

		/*update reserva habitacion*/
		update rh
		set rh.PasajeID = op.NewPasajeID
		from ReservaHabitacion rh
		inner join #tOldPasaje op
			on rh.PasajeID = op.PasajeID
		

		drop table #tOldPasaje

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 
			ROLLBACK TRAN

			DECLARE @errmsg   AS NVARCHAR (2048),
					@errState int
			select  @errmsg = Error_message() + ERROR_LINE(), @errState = ERROR_STATE()
					RAISERROR (@errmsg,16,@errState); 
	END CATCH
end

