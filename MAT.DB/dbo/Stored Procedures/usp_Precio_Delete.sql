
create PROCEDURE [dbo].[usp_Precio_Delete] (@PrecioID uniqueidentifier)
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2018-12-10
  -- Description: delete precio by id
  --History
  
  -- ============================================= */

  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	   DECLARE @errmsg   AS NVARCHAR (2048), 
               @errState INT 

	begin try

		if not exists (select * from dbo.PaquetePrecio where PrecioID = @PrecioID) and not exists( select * from Pasaje p where p.PrecioID = @PrecioID)	    
		begin
			begin tran
				delete dbo.Precio where PrecioID = @PrecioID
			commit
			
		end
		else
		begin
			SELECT @errmsg = Error_message() + Error_line(), 
                   @errState = Error_state()
			
			RAISERROR (N'No se puede eliminar el precio seleccionado porque esta vinculado a un paquete o pasaje.: %d',16,@errState,1,@errmsg); 

		end
	end try
	begin catch
		 IF @@TRANCOUNT > 0 
            ROLLBACK TRAN 

          SELECT @errmsg = Error_message() + Error_line(), 
                 @errState = Error_state() 

          RAISERROR (N'Error al eleminar el precio: %d',16,@errState,1,@errmsg); 
	end catch
			
  END

