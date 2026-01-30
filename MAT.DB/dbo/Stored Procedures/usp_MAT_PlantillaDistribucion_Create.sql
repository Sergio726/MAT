CREATE PROCEDURE [dbo].[usp_MAT_PlantillaDistribucion_Create]
									(@Nombre varchar(50),	
									 @ViajeID uniqueidentifier,
									 @HotelID uniqueidentifier,
									 @Status int OUTPUT
									)
AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-05-28
-- Description: create plantilla de distribucion

-- 2024-09-19 Ruben Tejerina Add precio and descripcion  
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

	begin try
		declare @Id int;

		begin tran

			if not exists(select * from dbo.PlantillaDistribucionHabitacion where Nombre = @Nombre)
			begin
				insert into dbo.PlantillaDistribucionHabitacion (Nombre) values (@Nombre);
				set @Id = SCOPE_IDENTITY();

				insert into dbo.TransPlantillaDistribucionHabitacion 
				(PlantillaDistribucionHabitacionID,NroHabitacion,TipoHabitacion, Capacidad, Nombre, Precio, Descripcion)
				select PlantillaDistribucionHabitacionID = @Id, 
					   NroHabitacion = h.NroHabitacion,
					   TipoHabitacion = h.Tipo,
					   Capacidad = h.Capacidad,
					   Nombre = h.Nombre,
					   Precio = h.Precio,
					   Descripcion = h.Descripcion
				from dbo.TransHotelHabitacionViaje th
				inner join dbo.Habitacion h
					on th.HabitacionID = h.HabitacionID
				where th.ViajeID = @ViajeID
				and   th.HotelID = @HotelID

				SET @Status = 0
			end
			else
				SET @Status =  1

		commit;
	end try
	begin catch
		IF @@TRANCOUNT > 0 
        ROLLBACK TRAN 

        DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + Error_line(), 
                @errState = Error_state() 

        RAISERROR (N'Error al intentar guardar plantilla. MSG: %d',16,@errState,1,@errmsg); 

	end catch


END