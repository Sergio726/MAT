
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Pago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePago_Update]
(

	@PagoId uniqueidentifier   ,

	@OriginalPagoId uniqueidentifier   ,

	@FechaPago datetime   ,

	@Monto float   ,

	@TipoPago int   ,

	@TransaccionId varchar (100)  ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@NroRecibo varchar (50)  ,

	@EstadoRendicion int   ,

	@CuentaCorrienteId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Pago]
				SET
					[PagoID] = @PagoId
					,[FechaPago] = @FechaPago
					,[Monto] = @Monto
					,[TipoPago] = @TipoPago
					,[VendedorId] = @VendedorId
					,[NroRecibo] = @NroRecibo
				WHERE
[PagoID] = @OriginalPagoId

