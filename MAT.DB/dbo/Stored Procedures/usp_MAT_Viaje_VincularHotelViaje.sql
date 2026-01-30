CREATE PROCEDURE [dbo].[usp_MAT_Viaje_VincularHotelViaje](@ViajeID VARCHAR(36),
												  @HotelID VARCHAR(36))

AS
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 07/05/2017
  -- Description:  link Viaje with Hotel
  -- ============================================= 
  */
BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


        BEGIN TRY 
            BEGIN TRAN
            
            INSERT INTO dbo.ViajeHotel
            ( 
				ViajeHotelID,
				ViajeID,
				HotelID
            )
            VALUES
            (
			 NEWID(),
			 @ViajeID,
			 @HotelID
            )

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


