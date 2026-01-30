
CREATE PROCEDURE [dbo].[usp_MAT_Paquete_Update](  @PaqueteID VARCHAR(36), 
												 @Descripcion varchar(100) = NULL,
												 @Moneda INT = NULL,
												 @Iva varchar(50) = NULL,
												 @Alicuota varchar(50) = NULL,
												 @Temporada int = NULL,
												 @Cotizacion float = NULL,
												 @Codigo varchar(50) = NULL,
												 @DestinoID int = NULL,
												 @Foto varchar(200) = NULL
												 )
AS 
 /* -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 09/05/2017
  -- Description:  Update Paquete
  History

  06-11-2017	Garcia Sergio add ModePublicity
  2025-03-29	Garcia Sergio   remove field ModePublicity and PublicWeb
  -- ============================================= */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


	BEGIN TRY 
	BEGIN TRAN

		UPDATE dbo.Paquete
		SET
			Descripcion = CASE WHEN @Descripcion IS NULL THEN Descripcion ELSE @Descripcion END,
			Moneda = @Moneda,
			Iva =  @Iva,
			Alicuota = @Alicuota,
			Temporada = @Temporada,
			Cotizacion = @Cotizacion,
			Codigo = @Codigo,
			DestinoID = @DestinoID,
			Foto =	CASE WHEN @Foto IS NULL THEN Foto ELSE @Foto END,
			LastUpdate = GETDATE()
			
			WHERE PaqueteID = @PaqueteID 

		COMMIT TRAN; 
		SELECT @PaqueteID AS PaqueteID, 'Done.' AS Result

	END TRY

	BEGIN CATCH
	IF @@TRANCOUNT > 0 
	ROLLBACK TRAN


	DECLARE @errmsg   AS NVARCHAR (2048)
	SELECT @errmsg = Error_message()

	SELECT ''  AS PaqueteID, @errmsg AS Result
	END CATCH

    

  END