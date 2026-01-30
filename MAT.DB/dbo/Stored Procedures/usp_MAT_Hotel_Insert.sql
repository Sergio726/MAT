
CREATE PROCEDURE [dbo].[usp_MAT_Hotel_Insert]
									(	@Nombre varchar(50),
										@Direccion varchar(50) = null,
										@CP varchar(50) = null,
										@Telefono varchar(50) = null,
										@Email varchar(50) = null,
										@Contacto varchar(50) = null,
										@CantidadHabitaciones int = null,
										@Categoria int = null,
										@CheckIn varchar(8) = null,
										@CheckOut varchar(8)  = null,
										@GoogleMapHtml varchar(200) = null,
										@Localidad varchar(50) = null,
										@HotelID UNIQUEIDENTIFIER OUTPUT
										)
AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-05-13
-- Description: insert Hotel

  
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

	begin try
		DECLARE @tblHotelID  TABLE (HoteID uniqueidentifier NOT NULL)

		begin tran
			insert into dbo.Hotel (Nombre,Direccion,CP,Telefono,Email,Contacto,CantidadHabitaciones,Categoria,CheckIn,CheckOut,GoogleMapHtml,Localidad)
			output inserted.HotelID into @tblHotelID
			values(@Nombre,@Direccion,@CP,@Telefono,@Email,@Contacto,@CantidadHabitaciones,@Categoria,@CheckIn,@CheckOut,@GoogleMapHtml,@Localidad)

			select @HotelID = h.HoteID from @tblHotelID h
			
		commit;
	end try
	begin catch
		IF @@TRANCOUNT > 0 
        ROLLBACK TRAN 

        DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + Error_line(), 
                @errState = Error_state() 

        RAISERROR (N'Error al agregar el Hotel. MSG: %d',16,@errState,1,@errmsg); 

	end catch


END
