CREATE PROCEDURE [dbo].[usp_MAT_Paquete_NewPaquete]( @Descripcion varchar(100),
													 @Moneda INT = NULL,
													 @Iva varchar(50) = NULL,
													 @Alicuota varchar(50) = NULL,
													 @Temporada int = NULL,
													 @Cotizacion float = NULL,
													 @Codigo varchar(50) = NULL,
													 @DestinoID int,
													 @Foto varchar(200) = NULL)
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 09/05/2017
  -- Description:  Create Paquete
  History
  06-11-2017 Garcia Sergio	add ModePublicity
  2025-03-29	Garcia Sergio   remove field ModePublicity and PublicWeb
  -- ============================================= */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


	BEGIN TRY 
	BEGIN TRAN

		DECLARE @PaqueteID uniqueidentifier
		SELECT @PaqueteID = NEWID()

		INSERT dbo.Paquete 
		(PaqueteID,
		Descripcion,
		Moneda,
		Iva,
		Alicuota,
		Temporada,
		Cotizacion,
		Codigo,
		DestinoID,
		Foto
		)
		VALUES
		(@PaqueteID,
		UPPER(@Descripcion),
		@Moneda,
		@Iva,
		@Alicuota,
		@Temporada,
		@Cotizacion,
		UPPER(@Codigo),
		@DestinoID,
		@Foto)
		
			

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