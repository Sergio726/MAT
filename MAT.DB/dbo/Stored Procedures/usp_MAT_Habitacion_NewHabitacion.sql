
	
CREATE PROCEDURE [dbo].[usp_MAT_Habitacion_NewHabitacion](@NroHabitacion int,
														  @Tipo int,
														  @HotelID uniqueidentifier,
														  @Estado int,
														  @Capacidad int,
														  @Ocupacion int = 0,
														  @Nombre varchar(50) = null)

AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 06/17/2017
  -- Description:  insert new viaje
  --History
  --2017-06-17 Garcia Sergio: create habitacion
  -- ============================================= 

  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

        BEGIN TRY 
            BEGIN TRAN
				
				DECLARE @HabitacionID UNIQUEIDENTIFIER 

				SELECT @HabitacionID = Newid() 

				INSERT INTO dbo.habitacion 
							(habitacionid, 
							 nrohabitacion, 
							 tipo, 
							 hotelid, 
							 estado, 
							 capacidad, 
							 ocupacion, 
							 nombre) 
				VALUES      (@HabitacionID, 
							 @NroHabitacion, 
							 @Tipo, 
							 @HotelID, 
							 @Estado, 
							 @Capacidad, 
							 @Ocupacion, 
							 UPPER(@Nombre) ) 


            COMMIT TRAN; 

            SELECT @HabitacionID AS HabitacionId, 'Done.' AS Result



        END TRY
        BEGIN CATCH

            IF @@TRANCOUNT > 0 
				ROLLBACK TRAN

            DECLARE @errmsg   AS NVARCHAR (2048)
            SELECT @errmsg = Error_message()
            SELECT ''  AS HabitacionId, @errmsg AS Result

        END CATCH
  END 


