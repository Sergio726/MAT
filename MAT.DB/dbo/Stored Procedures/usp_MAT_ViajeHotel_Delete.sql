
CREATE PROCEDURE [dbo].[usp_MAT_ViajeHotel_Delete] @ViajeID UNIQUEIDENTIFIER, @HotelID UNIQUEIDENTIFIER
	AS 
  /*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 11-06-2018   
  -- Description:  delete ViajeHotel
    
  -- =============================================*/ 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

      BEGIN try 
	
		begin tran
			delete dbo.ViajeHotel where ViajeID = @ViajeID and HotelID = @HotelID

		commit

      END try 

      BEGIN catch 
          IF @@TRANCOUNT > 0 
            ROLLBACK TRAN 

          DECLARE @errmsg   AS NVARCHAR (2048), 
                  @errState INT 

          SELECT @errmsg = Error_message() + Error_line(), 
                 @errState = Error_state() 

          RAISERROR (N'Error al eleminar vinculo de hotel: %d',16,@errState,1,@errmsg); 
      END catch 
  END 


