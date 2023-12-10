
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the MovimientoCuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureMovimientoCuenta_Update
(

	@MovimientoId uniqueidentifier   ,

	@OriginalMovimientoId uniqueidentifier   ,

	@PagoId uniqueidentifier   ,

	@FacturaId uniqueidentifier   ,

	@FechaRegistro datetime   ,

	@CuentaId uniqueidentifier   ,

	@NotaId uniqueidentifier   ,

	@CuentaCorrienteId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[MovimientoCuenta]
				SET
					[MovimientoID] = @MovimientoId
					,[PagoID] = @PagoId
					,[FacturaID] = @FacturaId
					,[FechaRegistro] = @FechaRegistro
					,[CuentaID] = @CuentaId
					,[NotaID] = @NotaId
					,[CuentaCorrienteID] = @CuentaCorrienteId
				WHERE
[MovimientoID] = @OriginalMovimientoId 
				
			

