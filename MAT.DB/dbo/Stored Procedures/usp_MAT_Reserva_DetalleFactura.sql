CREATE procedure [dbo].[usp_MAT_Reserva_DetalleFactura](@FacturaID varchar(36),
												@AdicionalesIDs varchar(max),
												@PrecioID varchar(36))
as
/*-- =============================================
  -- Author:    Garcia Sergio
  -- Create date: 09-04-2017
  -- Description: Carga DetalleFactura: renglón del paquete (precio) y un renglón por cada
  --              adicional de @AdicionalesIDs (lista de Guid separados por ";" o ","; los
  --              repetidos generan renglones repetidos, p. ej. un seguro de menor por menor).
  -- Historial:
  --   2026-09-30  Sebastian Garcia  BUG P1 menores: acepta "," además de ";", descarta tokens no Guid
  --                                 y corrige el CATCH (Error_message() + ERROR_LINE() fallaba por conversión).
  ============================================= */
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

		--insert adicional (separador ";" o ","; se ignoran tokens no Guid)
		if (ISNULL(@AdicionalesIDs, '') != '')
		begin

			insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad,AdicionalID)
			select @FacturaID,a.Descripcion, a.Monto,1,a.AdicionalID
			from dbo.Split(REPLACE(@AdicionalesIDs, ',', ';'), ';') sp
			inner join dbo.Adicional a
				on a.AdicionalID = TRY_CONVERT(uniqueidentifier, LTRIM(RTRIM(sp.Item)))

		end

		commit
	end try

	begin catch
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

			DECLARE @errmsg   AS NVARCHAR (2048),
					@errState int
			SELECT  @errmsg = ERROR_MESSAGE() + ' (línea ' + CAST(ERROR_LINE() AS nvarchar(10)) + ')',
					@errState = ERROR_STATE()
			RAISERROR (@errmsg, 16, @errState);

	end catch

end
