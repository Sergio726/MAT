CREATE PROCEDURE [dbo].[usp_MAT_Habitacion_EditHabitacion](
    @HabitacionID uniqueidentifier,
    @NroHabitacion int,
    @Tipo int,
    @Estado int,
    @Capacidad int,
    @Nombre varchar(100) = null,
    @HabitacionPrecio decimal(12,2) = 0,
    @HabitacionDescripcion varchar(500) = null
)

AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 06/17/2017
  -- Description:  edit habitacion
  --History
  --2017-06-17 Garcia Sergio: edit habitacion
  --2019-05-22 Garcia Sergio: quit hotelID
  --2024-09-18 Ruben Tejerina: add Precio and Descripcion
  -- ============================================= 

  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

        BEGIN TRY 
            BEGIN TRAN
				
				update dbo.Habitacion
				set NroHabitacion = @NroHabitacion,
					Tipo = @Tipo,
					Estado = @Estado,
					Capacidad = @Capacidad,
					Nombre = @Nombre,
                    Precio = @HabitacionPrecio,
                    Descripcion = @HabitacionDescripcion
				where HabitacionID = @HabitacionID			

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