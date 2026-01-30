CREATE PROCEDURE [dbo].[usp_MAT_Reserva_EliminarFactura]
(
	@ReservaId UNIQUEIDENTIFIER	
)
AS 
/*
============================================= 
Author:    Ruben Tejerina 
Create date: 19/11/2024
Description:  Eliminar la factura de la reserva
Basado en los SPs: 
	usp_MAT_PersonaCliente_DesvincularMenor
	usp_MAT_PersonaCliente_EliminarVenta_LiberarHabitaciones
	usp_MAT_Factura_DeleteFactura

History:

2024-11-19 Ruben Tejerina Create SP
============================================= 
*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

    BEGIN TRY 
		BEGIN TRAN

		DECLARE @FacturaId varchar(40),
			@VendedorId varchar(40)

		SELECT 
			@FacturaId = p.FacturaId,
			@VendedorId = p.VendedorId
		FROM PedidoReserva p
		WHERE p.Id = @ReservaId

		EXEC usp_MAT_PersonaCliente_DesvincularMenor @FacturaId
		EXEC usp_MAT_PersonaCliente_EliminarVenta_LiberarHabitaciones @FacturaId
		EXEC usp_MAT_Factura_DeleteFactura @FacturaId, @VendedorId


		COMMIT TRAN; 
		--ROLLBACK TRAN;
	END TRY
	BEGIN CATCH
		
		DECLARE @errmsg   AS NVARCHAR (2048)
		SELECT @errmsg ='Error in usp_MAT_Reserva_EliminarFactura. Message:' + Error_message() + ' Error Line:' + STR(ERROR_LINE())
		print @errmsg
		
		-- Deshacemos la transación
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN;

		--DECLARE @errmsg   AS NVARCHAR (2048)
		--SELECT @errmsg ='Error in usp_MAT_Reserva_RegistrarPago. Message:' + Error_message() + ' Error Line:' + STR(ERROR_LINE())

		RAISERROR(@errmsg, 16, 1)

	END CATCH
END