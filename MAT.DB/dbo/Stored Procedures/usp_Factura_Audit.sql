  CREATE PROCEDURE [dbo].[usp_Factura_Audit](@FacturaID varchar(36),
										     @PersonaID varchar(36),
										     @VendedorID varchar(36),
										     @Accion varchar(200),
										     @Descripcion varchar(200)
										     )
  AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 19/05/2017
  -- Description:  Audit table Factura
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		BEGIN TRY 
		BEGIN TRAN

		INSERT INTO AuditFactura 
		(FacturaID, 
		PersonaID, 
		VendedorID, 
		Accion,
		Descripcion) 
		VALUES     ( @FacturaID, 
		@PersonaID, 
		@VendedorID, 
		@Accion, 
		@Descripcion ) 

		COMMIT TRAN; 
		SELECT 'Done.' AS Result

		END TRY

		BEGIN CATCH
		IF @@TRANCOUNT > 0 
		ROLLBACK TRAN


		DECLARE @errmsg   AS NVARCHAR (2048)
		SELECT @errmsg = Error_message()

		SELECT @errmsg AS Result
		END CATCH
		
  END 
  
  
