CREATE PROCEDURE [dbo].[usp_MAT_ListaEspera_InsertNew] (@ViajeID UNIQUEIDENTIFIER,
														@ClienteID VARCHAR(36) = '',
														@UsuarioID INT,
														@Observacion VARCHAR(300) = '',
														@PasajeroTemporal VARCHAR(100) = ''
														)
AS 
  /*-- =============================================  
  -- Author:    Garcia Sergio  
  -- Create date: 08/02/2023
  -- Description:  insert new item in to ListaEspera 
  --History 
  08-02-2023  Garcia Sergio: create store procedure
  -- ============================================= */ 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	  DECLARE @ID INT
	 
      BEGIN try 
          BEGIN TRAN 
			INSERT INTO ListaEspera (ViajeID, ClienteID, UsuarioID, Observacion, PasajeroTemporal)
			VALUES (@ViajeID, TRY_CAST(@ClienteID AS UNIQUEIDENTIFIER), @UsuarioID, @Observacion, @PasajeroTemporal)
			
			SELECT  @ID = SCOPE_IDENTITY();
			 
          COMMIT TRAN; 

          SELECT @ViajeID AS ID, 
                 'Done.'  AS Result 
      END try 

      BEGIN catch 
          IF @@TRANCOUNT > 0 
            ROLLBACK TRAN 

          DECLARE @errmsg AS NVARCHAR (2048) 

          SELECT @errmsg = N'Error al insertar en lista de espera' + Error_message() 

          SELECT 0      AS ID, 
                 @errmsg AS Result 
      END catch 
  END 