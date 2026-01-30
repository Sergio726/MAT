CREATE PROCEDURE [dbo].[usp_MAT_ListaEspera_Delete](@ID INT, @UsuarioID INT)
AS 
  /*-- =============================================  
  -- Author:    Garcia Sergio  
  -- Create date: 09/02/2023
  -- Description:  delete item to ListaEspera 
  --History 
  09-02-2023  Garcia Sergio: create store procedure
  -- ============================================= */ 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	  IF EXISTS (SELECT * FROM DBO.ListaEspera WHERE Id = @ID AND UsuarioID = @UsuarioID)
	  
      BEGIN try 
          BEGIN TRAN 
			DELETE ListaEspera WHERE Id = @ID

          COMMIT TRAN; 

          SELECT 'Done.'  AS Result 
      END try 

      BEGIN catch 
          IF @@TRANCOUNT > 0 
            ROLLBACK TRAN 

          DECLARE @errmsg AS NVARCHAR (2048) 

          SELECT @errmsg = N'Error al eliminar elemento' + Error_message() 

          SELECT @errmsg AS Result 
      END catch 

	  ELSE
		SELECT @errmsg = N'El registro solo puede ser eliminado por la persona que lo creo.'

	SELECT @errmsg AS Result 
  END 
