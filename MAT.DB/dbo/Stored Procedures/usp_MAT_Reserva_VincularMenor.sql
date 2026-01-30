							 
CREATE PROCEDURE [dbo].[usp_MAT_Reserva_VincularMenor] (@PasajeID varchar(max) = '',
														@PasajeroID varchar(max) = '',
														@MenorID varchar(max) = ''
																 )
AS 
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 08-01-2017
-- Description:	vincular un menor al un pasajero 
-- =============================================
BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

      IF ( @PasajeID != '' 
           AND @PasajeroID != '' 
           AND @MenorID != '' ) 
        BEGIN 
            BEGIN try 
                BEGIN TRAN; 

                IF NOT EXISTS(SELECT 1 
                              FROM   dbo.pasajeromenor PM 
                              WHERE  PM.pasajeid = @PasajeID 
                                     AND PM.pasajeroid = @PasajeroID 
                                     AND PM.menorid = @MenorID) 
                  BEGIN 
                      INSERT INTO dbo.pasajeromenor 
                                  (pasajeid, 
                                   pasajeroid, 
                                   menorid) 
                      VALUES      (@PasajeID, 
                                   @PasajeroID, 
                                   @MenorID) 

                      SELECT 1                                   AS Id, 
                             'Se vinculo correctamete el menor.' AS ErrorMsg 
                  END 
                ELSE 
                  BEGIN 
                      SELECT 0 
                             AS Id 
                             , 
  'El menor seleccionado ya ha sido vinculado con el pasajero seleccionado.' 
         AS 
  ErrorMsg 
  END 

      COMMIT TRAN; 
  END try 

  BEGIN catch 
      IF @@TRANCOUNT > 0 
        ROLLBACK TRANSACTION 

      DECLARE @errmsg   AS NVARCHAR (2048), 
              @severity AS TINYINT, 
              @state    AS TINYINT, 
              @errno    AS INT, 
              @proc     AS SYSNAME, 
              @lineno   AS INT; 

      SELECT @errmsg = Error_message(), 
             @severity = Error_severity(), 
             @state = Error_state(), 
             @errno = Error_number(), 
             @proc = Error_procedure(), 
             @lineno = Error_line(); 

      IF @errno IS NULL 
        RETURN; 

      IF @errmsg NOT LIKE '***%' 
        BEGIN 
            SET @errmsg = '*** ' 
                          + COALESCE (Quotename(@proc), '<dynamic SQL>') 
                          + ', ' + Ltrim(Str(@lineno)) + '. Errno ' 
                          + Ltrim(Str(@errno)) + ': ' + @errmsg; 
        END 

      SELECT -1      AS Id, 
             @errmsg AS ErrorMsg 
  END catch 
  END 
 END 
