CREATE PROCEDURE [dbo].[usp_TransHotelHabitacionViaje_Insert]
(
	@HotelID UNIQUEIDENTIFIER, 
	@ViajeID UNIQUEIDENTIFIER, 
	@Fecha DATE, 
	@Tipo dbo.tvp_int readonly,
	@HabPrecio DECIMAL(12,2),
	@HabDescripcion VARCHAR(500)
)
	
AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-05-20
-- Description: insert Habitacion and insert TransHotelHabitacionViaje

-- 2024-09-19 Ruben Tejerina add precio and descripcion for Habitacion
  
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

	begin try
		
		begin tran
			
		declare @tblID table (HabitacionID uniqueidentifier)

		insert into Habitacion (Tipo,Estado,Capacidad, Ocupacion, Precio, Descripcion)
		output inserted.HabitacionID into @tblID
		select ht.Id,0,ht.CapacidadNormal, 0, @HabPrecio, @HabDescripcion 
		from @Tipo t
		inner join dbo.HabitacionTipo ht
			on t.Id = ht.Id

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