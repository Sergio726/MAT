CREATE PROCEDURE [dbo].[usp_MAT_ViajeAudit_Cancel] (@ViajeId UNIQUEIDENTIFIER,
													@ViajeNombre VARCHAR(100),
													@DeleteOn DATETIME,
													@DeleteByUserId UNIQUEIDENTIFIER,
													@DeleteUser VARCHAR(50),
													@DeleteDetalle VARCHAR(500))
AS 
  /*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 08-26-2020   
  -- Description:  insert audit viaje
     
  -- =============================================*/ 

BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    BEGIN TRY
        
        BEGIN TRAN;
			
			INSERT INTO dbo.ViajeAudit
			(ViajeId, 
			 ViajeNombre, 
			 DeleteOn, 
			 DeleteByUserId, 
			 DeleteUser,
			 DeleteDetalle
			)
			VALUES
			(@ViajeId, 
			 @ViajeNombre, 
			 @DeleteOn, 
			 @DeleteByUserId, 
			 @DeleteUser,
			 @DeleteDetalle
			);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        DECLARE @errmsg AS NVARCHAR(2048), @errState INT;
        SELECT @errmsg = ERROR_MESSAGE() +  convert(nvarchar(10),ERROR_LINE()), 
               @errState = ERROR_STATE();
        RAISERROR(N'Error al agregar auditoria para cancelar el viaje: %d', 16, @errState, 1, @errmsg);
    END CATCH;
END;