
--5)
CREATE PROCEDURE [dbo].[usp_MAT_Hotel_Update]
									(	@HotelID UNIQUEIDENTIFIER,
										@Nombre varchar(50),
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
										@Localidad varchar(50) = null
										
										)
AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-05-14
-- Description: update Hotel

  
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

	begin try
		
		begin tran
			update dbo.Hotel 
			set Nombre = @Nombre,
			    Direccion = @Direccion,
				CP = @CP,
				Telefono = @Telefono,
				Email = @Email,
				Contacto = @Contacto,
				CantidadHabitaciones = @CantidadHabitaciones,
				Categoria = @Categoria,
				CheckIn = @CheckIn,
				CheckOut = @CheckOut,
				GoogleMapHtml = @GoogleMapHtml,
				Localidad = @Localidad
			where HotelID = @HotelID

		commit;
	end try
	begin catch
		IF @@TRANCOUNT > 0 
        ROLLBACK TRAN 

        DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + Error_line(), 
                @errState = Error_state() 

        RAISERROR (N'Error al actualizar el Hotel. MSG: %d',16,@errState,1,@errmsg); 

	end catch


END
