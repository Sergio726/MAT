CREATE PROCEDURE [dbo].[usp_TransHotelHabitacionViaje_InsertByPlantilla]
															(
															  @HotelID UNIQUEIDENTIFIER, 
															  @ViajeID UNIQUEIDENTIFIER, 
															  @Fecha DATE,
															  @PlantillaId INT
															 )
	
AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-05-29
-- Description: insert Habitacion and insert TransHotelHabitacionViaje by Plantilla

-- 2024-09-19 Ruben Tejerina Add precio and descripcion  
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

	begin try
		
		begin tran
			
		declare @tblID table (HabitacionID uniqueidentifier)

		insert into Habitacion (Tipo,Estado,Capacidad, Ocupacion,NroHabitacion,Nombre, Precio, Descripcion)
		output inserted.HabitacionID into @tblID
		select t.TipoHabitacion,0,t.Capacidad, 0, t.NroHabitacion, t.Nombre, t.Precio, t.Descripcion
		from PlantillaDistribucionHabitacion p
		inner join dbo.TransPlantillaDistribucionHabitacion t
			on p.Id = t.PlantillaDistribucionHabitacionID
			
		where p.Id = @PlantillaId

		insert into dbo.TransHotelHabitacionViaje (HotelID,HabitacionID,ViajeID,Fecha)
		select @HotelID,h.HabitacionID, @ViajeID, @Fecha
		from @tblID h


		commit;
	end try
	begin catch
		IF @@TRANCOUNT > 0 
        ROLLBACK TRAN 

        DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + Error_line(), 
                @errState = Error_state() 

        RAISERROR (N'Error al agregar registros  MSG: %d',16,@errState,1,@errmsg); 

	end catch


END