-- Fix: preservar ReservaHabitacion / badge H al cambiar butaca.
-- Publicar en cada entorno antes de validar en Reserva/Index o DistribucionCoche.

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Reserva_CambioButacas](@AdicionalesIDs varchar(1000),@OldPasaje uniqueidentifier, @NewPasaje uniqueidentifier)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-15
  -- Description: Cambio de butaca: mueve pasaje, sincroniza DetalleFactura (línea Butaca),
  --              adicionales CAMA, Monto de factura, estados y auditoría.
  --              Preserva ReservaHabitacion (y badge H / estados 6-8-9) al recalcular estados.
  -- Historial:
  --   2017-02-10  Garcia Sergio   Creación del SP (cambio de butaca).
  --   2018-01-28  Garcia Sergio   Elimina adicional butaca cama al bajar de piso;
  --                               actualiza ReservaHabitacion al nuevo PasajeID.
  --   2018-05-30  Garcia Sergio   Quita PrecioID del flujo de Pasaje.
  --   2019-07-31  Garcia Sergio   Inserta adicional solo si no existe en DetalleFactura.
  --   2026-06-16  Sebastian Garcia Sincroniza línea Butaca en DetalleFactura (update/insert);
  --                               recalcula Factura.Monto; inserta AuditFactura (CAMBIO BUTACA);
  --                               ejecuta usp_MAT_Reserva_ActualizarEstados tras el cambio.
  --   2026-07-15  Sebastian Garcia Mueve UPDATE ReservaHabitacion antes de ActualizarEstados
  --                               (dentro de la misma transacción) para no perder estado hotel.
  ============================================= */
		SET nocount, xact_abort ON; 
		SET TRANSACTION isolation level READ uncommitted; 
begin
	BEGIN TRY

		declare @FacturaID uniqueidentifier,
				@ClienteID uniqueidentifier,
				@VendedorID uniqueidentifier,
				@OldCodigoButaca varchar(50),
				@NewCodigoButaca varchar(50),
				@OldDetalle varchar(300),
				@NewDetalle varchar(300),
				@AuditDesc varchar(200)

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

		select @FacturaID = t.FacturaID from #tOldPasaje t

		if @FacturaID is null
			RAISERROR('No se encontró la factura asociada al pasaje original.', 16, 1)

		select @ClienteID = f.ClienteID,
			   @VendedorID = f.VendedorID
		from dbo.Factura f
		where f.FacturaID = @FacturaID

		select @OldCodigoButaca = isnull(b.CodigoButaca, '')
		from dbo.Pasaje p
		inner join dbo.Butaca b on b.ButacaID = p.ButacaID
		where p.PasajeID = @OldPasaje

		select @NewCodigoButaca = isnull(b.CodigoButaca, '')
		from dbo.Pasaje p
		inner join dbo.Butaca b on b.ButacaID = p.ButacaID
		where p.PasajeID = @NewPasaje

		set @OldDetalle = 'Butaca ' + @OldCodigoButaca
		set @NewDetalle = 'Butaca ' + @NewCodigoButaca

		BEGIN TRAN

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

		/* Sincronizar línea Butaca en DetalleFactura */
		if @NewCodigoButaca <> ''
		begin
			update df
			set df.Detalle = @NewDetalle
			from dbo.DetalleFactura df
			where df.FacturaID = @FacturaID
			  and df.Detalle = @OldDetalle

			if @@ROWCOUNT = 0
			begin
				insert into dbo.DetalleFactura (FacturaID, Detalle, Precio, Cantidad)
				values (@FacturaID, @NewDetalle, 0, 1)
			end
		end

		/* Recalcular Monto de factura */
		update dbo.Factura
		set Monto = isnull((
			select sum(df.Precio * df.Cantidad)
			from dbo.DetalleFactura df
			where df.FacturaID = @FacturaID
		), 0)
		where FacturaID = @FacturaID

		/* Auditoría */
		set @AuditDesc = left(
			'Pasaje ' + convert(varchar(36), @OldPasaje) + ' -> ' + convert(varchar(36), @NewPasaje)
			+ ' Butaca: ' + @OldCodigoButaca + ' -> ' + @NewCodigoButaca
			+ case when @AdicionalesIDs <> '' then ' Adic:' + @AdicionalesIDs else '' end,
			200)

		insert into dbo.AuditFactura (FacturaID, PersonaID, VendedorID, Accion, Descripcion)
		values (@FacturaID, @ClienteID, @VendedorID, 'CAMBIO BUTACA', @AuditDesc)

		/* Repuntar habitación al pasaje nuevo ANTES de recalcular estados (preserva H / 6-8-9) */
		update rh
		set rh.PasajeID = op.NewPasajeID
		from dbo.ReservaHabitacion rh
		inner join #tOldPasaje op
			on rh.PasajeID = op.PasajeID

		exec dbo.usp_MAT_Reserva_ActualizarEstados @FacturaID = @FacturaID

		COMMIT

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
GO
