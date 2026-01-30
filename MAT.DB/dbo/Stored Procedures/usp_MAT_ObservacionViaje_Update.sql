
CREATE PROCEDURE [dbo].[usp_MAT_ObservacionViaje_Update](@ObservacionViajeID INT,
														 @VendedorID INT,
														 @PasajerosID VARCHAR(MAX) = NULL,
														 @Detalle VARCHAR(MAX),
														 @CategoriaID INT
														 )

AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-08-07
-- Description: update ObservacionViaje

  
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

	begin try
		
		if exists(select * 
				  from dbo.ObservacionViaje ov 
				  where ov.Id = @ObservacionViajeID
				  and (ov.VendedorID = @VendedorID or @VendedorID = 1)
				 )
		begin

		begin tran
			update dbo.ObservacionViaje
			set [PasajerosID] = @PasajerosID,
				[Detalle] = @Detalle,
				[Update] = getdate(),
				[UpdateVendedorID] = @VendedorID,
				[CategoriaId] = @CategoriaID
			where Id = @ObservacionViajeID


			select 'Done.'
			
		commit;
		end
		else
		begin
			select 'No se puede realizar la modificacion de la observacion, ya que el registro no existe o el usuario no tiene los permisos suficientes.'
		end

	end try
	begin catch
		IF @@TRANCOUNT > 0 
        ROLLBACK TRAN 

        DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + CONVERT(nvarchar(20),Error_line()), 
                @errState = Error_state() 

        RAISERROR (N'Error al agregar una observacion del viaje. MSG: %d',16,@errState,1,@errmsg); 

	end catch


END