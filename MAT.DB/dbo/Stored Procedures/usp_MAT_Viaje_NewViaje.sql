CREATE PROCEDURE [dbo].[usp_MAT_Viaje_NewViaje](@PaqueteID           VARCHAR(36), 
                                                @Origen              VARCHAR(50) = NULL, 
                                                @FechaSalida         DATE = NULL, 
                                                @HoraSalida          VARCHAR(50) = NULL, 
                                                @PaisOrigen          VARCHAR(50) = NULL, 
                                                @PaisDestino         VARCHAR(50) = NULL, 
                                                @Paso                VARCHAR(50) = NULL, 
                                                @Medio               VARCHAR(50) = NULL, 
                                                @BusID               VARCHAR(36) , 
                                                @FechaRegreso        DATE = NULL, 
                                                @HoraRegreso         VARCHAR(50) = NULL, 
                                                @TiempoConsentracion INT = 30, 
                                                @Descripcion         VARCHAR(50) = NULL, 
                                                @MonedaTipo          INT = 1,
                                                @PrecioSemicama      FLOAT = NULL, 
                                                @PrecioCama          FLOAT = NULL, 
                                                @PrecioPromocional   FLOAT = NULL, 
                                                @FechaPromocion      DATETIME = NULL, 
                                                @nDias               INT = NULL, 
                                                @nNoches             INT = NULL,
												@Observaciones		 VARCHAR(500) = NULL,
                                                @IsPublicWeb         BIT = 1,
												@VendedorID			 UNIQUEIDENTIFIER) 
AS 
  /*-- =============================================  
  -- Author:    Garcia Sergio  
  -- Create date: 04/24/2017 
  -- Description:  insert new viaje 
  --History 
  05-20-2017  Garcia Sergio: create pasaje 
  2018-05-30  Garcia Sergio: Quit prefio from Pasaje 
  2019-04-17  Garcia Sergio: add @TiempoConsentracion
  2019-05-04	Garcia Sergio: add @Observaciones
  2025-03-28 Garcia Sergio: add @MonedaTipo
  2025-03-30
  -- ============================================= */ 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

      BEGIN try 
          BEGIN TRAN 

          DECLARE @ViajeID UNIQUEIDENTIFIER 

          SELECT @ViajeID = Newid() 

          INSERT INTO dbo.Viaje 
                      (ViajeID, 
                       PaqueteID, 
                       Origen, 
                       FechaSalida, 
                       HoraSalida, 
                       PaisOrigen, 
                       PaisDestino, 
                       paso, 
                       medio, 
                       busid, 
                       fecharegreso, 
                       horaregreso, 
                       tiempoconsentracion, 
                       descripcion, 
                       MonedaTipo,
                       preciosemicama, 
                       preciocama, 
                       preciopromocional, 
                       fechapromocion, 
                       ndias, 
                       nnoches,
					   Observaciones,
                       IsPublicWeb) 
          VALUES      ( @ViajeID, 
                        @PaqueteID, 
                        @Origen, 
                        @FechaSalida, 
                        @HoraSalida, 
                        @PaisOrigen, 
                        @PaisDestino, 
                        @Paso, 
                        @Medio, 
                        @BusID, 
                        @FechaRegreso, 
                        @HoraRegreso, 
                        @TiempoConsentracion, 
                        @Descripcion, 
                        @MonedaTipo,
                        @PrecioSemicama, 
                        @PrecioCama, 
                        @PrecioPromocional, 
                        @FechaPromocion, 
                        @nDias, 
                        @nNoches,
						@Observaciones,
                        @IsPublicWeb) 

          /*Insert Lis Pasaje*/ 
          INSERT INTO Pasaje 
                      (pasajeid, 
                       pasajeroid, 
                       butacaid, 
                       fechareserva, 
                       fechacompra, 
                       viajeid, 
                       facturaid, 
                       estadopasaje, 
                       voucherid) 
          SELECT Newid(), 
                 NULL, 
                 b.butacaid, 
                 NULL, 
                 NULL, 
                 @ViajeID, 
                 NULL, 
                 1,--state disponible 
                 NULL 
          FROM   dbo.Butaca b 
          WHERE  b.transporteid = @BusID 


		  --audit insert
		  DECLARE @CreateOn DATETIME = GETDATE(),
				  @CreateUser VARCHAR(50);

		  
			SELECT @CreateUser = p.Apellido + ' ' + p.Nombre
			FROM dbo.Vendedor v
				 INNER JOIN dbo.Persona p ON v.VendedorID = p.PersonaID
				 INNER JOIN [MAT.Session].dbo.UserProfile u ON p.UserId = u.UserId
			WHERE v.VendedorID = @VendedorID;

		  exec usp_MAT_ViajeAudit_Insert @ViajeId = @ViajeId, @ViajeNombre = @Descripcion, @CreateOn = @CreateOn, @CreateUserId = @VendedorID, @CreateUser = @CreateUser

          COMMIT TRAN; 

          SELECT @ViajeID AS ViajeID, 
                 'Done.'  AS Result 
      END try 

      BEGIN catch 
          IF @@TRANCOUNT > 0 
            ROLLBACK TRAN 

          DECLARE @errmsg AS NVARCHAR (2048) 

          SELECT @errmsg = N'Error al insertar el viaje' + Error_message() 

          SELECT ''      AS ViajeID, 
                 @errmsg AS Result 
      END catch 
  END 