
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the MovimientoCuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureMovimientoCuenta_Insert]
(

	@MovimientoId uniqueidentifier    OUTPUT,

	@PagoId uniqueidentifier   ,

	@FacturaId uniqueidentifier   ,

	@FechaRegistro datetime   ,

	@CuentaId uniqueidentifier   ,

	@NotaId uniqueidentifier   ,

	@CuentaCorrienteId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[MovimientoCuenta]
					(
					[MovimientoID]
					,[PagoID]
					,[FacturaID]
					,[FechaRegistro]
					,[CuentaID]
					,[NotaID]
					,[CuentaCorrienteID]
					)
				VALUES
					(
					@MovimientoId
					,@PagoId
					,@FacturaId
					,@FechaRegistro
					,@CuentaId
					,@NotaId
					,@CuentaCorrienteId
					)
				
									
							
			



