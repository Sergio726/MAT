

CREATE PROCEDURE [dbo].[usp_MAT_Reserva_UpdatePasajeAdicionalesVoucher](@PasajeID uniqueidentifier,
															    @FacturaID uniqueidentifier,
															    @PasajeroID uniqueidentifier,
															    @AdicionalesIDs varchar(max),
															    @EstadoFactura INT,
															    @VendedorID uniqueidentifier = null)

AS
/*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 09-05-2017 
  -- Description:  Update PasajeAdicionalesVoucher
  
  --2018-05-30	Garcia Sergio: Quit Precio Id
  -- */
 BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	  BEGIN TRY 
			BEGIN TRAN

			DECLARE @ViajeID UNIQUEIDENTIFIER;

			SELECT @ViajeID = p.ViajeID
			FROM dbo.Pasaje p
			WHERE p.PasajeID = @PasajeID;

			IF dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(@PasajeroID, @ViajeID, @PasajeID) = 1
			BEGIN
				RAISERROR('El pasajero ya está registrado en otro viaje con la misma fecha de salida.', 16, 1);
				RETURN;
			END

			/*update pasaje*/
			UPDATE Pasaje 
			SET   FacturaID = @FacturaID,
				  PasajeroID = @PasajeroID,
				  EstadoPasaje = @EstadoFactura
			WHERE PasajeID = @PasajeID

			
			/*Insert Adicionales*/
			IF (@AdicionalesIDs != '')
			BEGIN
				INSERT INTO dbo.PasajeAdicional 
				(pasajeadicionalid, 
				 pasajeid, 
				 adicionalid
				) 
			SELECT Newid()   AS PasajeAdicionalID, 
				   @PasajeID AS PasajeID, 
				   item      AS AdicionalID 
			FROM   dbo.Split(@AdicionalesIDs, ',') 
			END
			
			
			/*4	Pagado, inserta un nuevo voucher*/
			IF ( @EstadoFactura = 4 ) 
			  BEGIN 
				  DECLARE @VoucherID UNIQUEIDENTIFIER 

				  SET @VoucherID = Newid() 

				  INSERT INTO dbo.Voucher 
							  (VoucherID, 
							   FechaEmision, 
							   VendedorID) 
				  VALUES      (@VoucherID, 
							   Getdate(), 
							   @VendedorID) 

				  UPDATE dbo.Pasaje 
				  SET    voucherid = @VoucherID 
				  WHERE  pasajeid = @PasajeID 
			  END 
			  			
			COMMIT TRAN; 
	  END TRY

	  BEGIN CATCH
	  	IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048),
					@errState int
			select  @errmsg = Error_message() + ERROR_LINE(), @errState = ERROR_STATE()
					RAISERROR (@errmsg,16,@errState);  
	  END CATCH




     

  END

