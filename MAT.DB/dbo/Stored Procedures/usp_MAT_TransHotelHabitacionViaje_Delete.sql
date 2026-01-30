CREATE PROCEDURE [dbo].[usp_MAT_TransHotelHabitacionViaje_Delete]	(@TransHotelHabitacionViajeID int,
																	 @Status int OUTPUT )
AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-05-20
-- Description: delete record TransHotelHabitacionViaje and Habitacion

  2019-07-28	Garcia Sergio: fix. add condition in where, @TransHotelHabitacionViajeID
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

	begin try
			
		if not exists(
		select * 
		from dbo.ReservaHabitacion rh
		inner join dbo.TransHotelHabitacionViaje th
			on rh.HabitacionID = th.HabitacionID
		where th.Id = @TransHotelHabitacionViajeID
		)
		begin

			delete TransHotelHabitacionViaje where Id = @TransHotelHabitacionViajeID

			delete h
			from dbo.Habitacion h
			inner join dbo.TransHotelHabitacionViaje th
				on h.HabitacionID = th.HabitacionID
			where th.Id = @TransHotelHabitacionViajeID
						

			set @Status = 1
		end
		else
		begin
			set @Status = 0
		end


	end try
	begin catch
		
        DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + Error_line(), 
                @errState = Error_state() 

        RAISERROR (N'Error al intentar eliminar habitacion. MSG: %d',16,@errState,1,@errmsg); 

	end catch


END