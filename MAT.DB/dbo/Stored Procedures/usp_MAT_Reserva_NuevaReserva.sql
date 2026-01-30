

CREATE PROCEDURE [dbo].[usp_MAT_Reserva_NuevaReserva](  @ClienteID	uniqueidentifier	,
														@VendedorID	uniqueidentifier	,
														@Observaciones	varchar(8000) = null,
														@Condicion varchar(50) = null,
														@MonedaTipo int
													   )
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 22-03-2017 
  -- Description:  registrar una reserva  
  -- History:
  -- 05-21-2017	Garcia Sergio: Audit Factura
	 05-31-2017	Garcia Sergio: add dias reserva in the table Factura
	 09/13/2017	Garcia Sergio: register detalles de factura
	 11/19/2017	Garcia Sergio: set DiasPreReserva to 7 days
	 05/01/2018 Garcia Sergio: add @MonedaTipo
  -- ============================================= 
  */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		BEGIN TRY 
			BEGIN TRAN
				 DECLARE @FacturaID         UNIQUEIDENTIFIER, 
						 @Fecha             DATETIME,
						 @PrecioFinal		 float	(8),
						 @DiasPreReserva	int = 0

			  IF (@Condicion = 'Cuenta Corriente')
				SET @DiasPreReserva = 7

			  SELECT @FacturaID = Newid(), 
					 @Fecha = Getdate()
			
			  /*Insert factura*/ 
			  INSERT INTO dbo.Factura 
						  (FacturaID, 
						   Monto, 
						   Fecha, 
						   Estado, 
						   ClienteID, 
						   VendedorID, 
						   Observaciones,
						   DiasPreReserva,
						   MonedaTipo) 
			  VALUES      (@FacturaID, 
						   @PrecioFinal, 
						   @Fecha, 
						   2,  --preserva - ccte
						   @ClienteID, 
						   @VendedorID, 
						   @Observaciones,
						   @DiasPreReserva,
						   @MonedaTipo) 

			  /*Audit Factura*/
			  CREATE TABLE #TEMP(Result VARCHAR(2048))
			  INSERT INTO #TEMP
			  EXEC usp_Factura_Audit @FacturaID, @ClienteID, @VendedorID, 'Insert', 'Nueva Reserva'
			  DROP TABLE #TEMP
			  
			  select @FacturaID as FacturaID, 'Done.' AS Result
			 
			COMMIT TRAN; 
		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048)
			SELECT @errmsg = Error_message()

			select '' as FacturaID, @errmsg AS Result
		END CATCH

     

  END