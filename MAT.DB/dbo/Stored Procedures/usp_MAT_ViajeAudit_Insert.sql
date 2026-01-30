  CREATE PROCEDURE [dbo].[usp_MAT_ViajeAudit_Insert] (@ViajeId UNIQUEIDENTIFIER,
													@ViajeNombre VARCHAR(100),
													@CreateOn DATETIME,
													@CreateUserId UNIQUEIDENTIFIER,
													@CreateUser VARCHAR(50))

	
AS 
  /*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 08-26-2020   
  -- Description:  create audit viaje
     
  -- =============================================*/ 

BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    BEGIN TRY
        
        BEGIN TRAN;
			
			INSERT INTO dbo.ViajeAudit
			(ViajeId, 
			 ViajeNombre, 
			 CreateOn,
			 CreateUserId,
			 CreateUser
			)
			VALUES
			(@ViajeId,
			 @ViajeNombre,
			 @CreateOn,
			 @CreateUserId,
			 @CreateUser
			);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        DECLARE @errmsg AS NVARCHAR(2048), @errState INT;
        SELECT @errmsg = ERROR_MESSAGE() +  convert(nvarchar(10),ERROR_LINE()), 
               @errState = ERROR_STATE();
        RAISERROR(N'Error al agregar auditoria para crear el viaje: %d', 16, @errState, 1, @errmsg);
    END CATCH;
END;

