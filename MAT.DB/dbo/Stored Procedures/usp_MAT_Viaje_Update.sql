
CREATE PROCEDURE [dbo].[usp_MAT_Viaje_Update](  @ViajeID	VARCHAR(36), 
												@PaqueteID	VARCHAR(36),
											    @Origen	VARCHAR(50),
											    @FechaSalida	date,
											    @HoraSalida	varchar(50),
											    @PaisOrigen	varchar(50),
											    @PaisDestino	varchar(50),
											    @Paso	varchar(50) = NULL,
											    @Medio	varchar(50),
											    @BusID	VARCHAR(36) = NULL,
											    @FechaRegreso	date  = NULL,
											    @HoraRegreso	varchar(50)  = NULL,
												@TiempoConsentracion int = 30,
											    @Descripcion	varchar(50)  = NULL,
											    @MonedaTipo int = 1,
												@PrecioSemicama	float  = NULL,
											    @PrecioCama	float  = NULL,
											    @PrecioPromocional	float  = NULL,
											    @FechaPromocion	datetime = NULL,
												@nDias INT  = NULL,
												@nNoches INT  = NULL,
												@Observaciones VARCHAR(500) = NULL,
												@IsPublicWeb BIT = 1)
AS 
  /* ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 01/05/2017
  -- Description:  Update Viaje

  2019/04/17	Garcia Sergio: add @TiempoConsentracion
  2019/05/04	Garcia Sergio: add @Observaciones
  2025/03/26	Garcia Sergio: add MonedaTipo
  2025/03/30	Garcia Sergio: add IsPublicWeb
  -- ============================================*/
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		BEGIN TRY 
			BEGIN TRAN
		

			UPDATE dbo.Viaje
			SET
			  ViajeID			  = @ViajeID
			 ,PaqueteID			  = @PaqueteID
			 ,Origen			  = @Origen
			 ,FechaSalida		  = @FechaSalida
			 ,HoraSalida		  = @HoraSalida
			 ,PaisOrigen		  = @PaisOrigen
			 ,PaisDestino		  = @PaisDestino
			 ,Paso				  = @Paso
			 ,Medio				  = @Medio
			 ,BusID				  = @BusID
			 ,FechaRegreso		  = @FechaRegreso
			 ,HoraRegreso		  = @HoraRegreso
			 ,TiempoConsentracion = @TiempoConsentracion
			 ,MonedaTipo		  = @MonedaTipo
			 ,Descripcion		  = @Descripcion
			 ,PrecioSemicama	  = @PrecioSemicama
			 ,PrecioCama		  = @PrecioCama
			 ,PrecioPromocional	  = @PrecioPromocional
			 ,FechaPromocion	  = @FechaPromocion
			 ,nDias				  = @nDias
			 ,nNoches			  = @nNoches
			 ,Observaciones		  = @Observaciones
			 ,IsPublicWeb		  = @IsPublicWeb
			WHERE ViajeID = @ViajeID	

			COMMIT TRAN; 
			
			SELECT @ViajeID AS ViajeID, 'Done.' AS Result

		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048)
			SELECT @errmsg = Error_message()

			SELECT ''  AS ViajeID, @errmsg AS Result
			
		END CATCH

		     

  END