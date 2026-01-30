
CREATE PROCEDURE [dbo].[ups_MAT_PaqueteExcursion_InsertNew](@ExcursionID UNIQUEIDENTIFIER,
															@PaqueteID UNIQUEIDENTIFIER,
															@IsOpcional bit = 0)
	
AS 
  /*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 11-05-2018   
  -- Description:  insert PaqueteExcursion
    
  -- =============================================*/ 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

      BEGIN try 
	
		begin tran
			insert into dbo.PaqueteExcursion (ExcursionID,PaqueteID,IsOpcional)
			values (@ExcursionID,@PaqueteID,@IsOpcional)

		commit

      END try 

      BEGIN catch 
          IF @@TRANCOUNT > 0 
            ROLLBACK TRAN 

          DECLARE @errmsg   AS NVARCHAR (2048), 
                  @errState INT 

          SELECT @errmsg = Error_message() + Error_line(), 
                 @errState = Error_state() 

          RAISERROR (N'Error al agregar vincular Excursion: %d',16,@errState,1,@errmsg); 
      END catch 
  END 


