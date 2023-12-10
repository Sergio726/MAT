
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Pago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePago_Insert
(

	@PagoId uniqueidentifier    OUTPUT,

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


				
				INSERT INTO [dbo].[Pago]
					(
					[PagoID]
					,[FechaPago]
					,[Monto]
					,[TipoPago]
					,[TransaccionID]
					,[ClienteId]
					,[VendedorId]
					,[NroRecibo]
					,[EstadoRendicion]
					,[CuentaCorrienteID]
					)
				VALUES
					(
					@PagoId
					,@FechaPago
					,@Monto
					,@TipoPago
					,@TransaccionId
					,@ClienteId
					,@VendedorId
					,@NroRecibo
					,@EstadoRendicion
					,@CuentaCorrienteId
					)
				
									
							
			

