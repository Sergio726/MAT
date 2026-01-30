CREATE PROCEDURE [dbo].[usp_MAT_Reserva_VincularMenorByViajeID] (@ViajeID varchar(max) = '',
														         @PasajeroID varchar(max) = '',
														         @MenorID varchar(max) = ''
														         )
AS 
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 17-01-2017
-- Description:	vincular un menor al un pasajero a un determinado viaje
-- =============================================
BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	  DECLARE @PasajeID VARCHAR(MAX)
	  SELECT TOP 1 @PasajeID = p.PasajeID
	  FROM dbo.Pasaje p
	  WHERE p.PasajeroID = @PasajeroID
	  and p.ViajeID = @ViajeID
		

      IF ( @PasajeID != '' 
           AND @PasajeroID != '' 
           AND @MenorID != '' ) 
        BEGIN 
            BEGIN try 
                BEGIN TRAN; 

				Select item as MenorID into #tblMenorID 
				from dbo.Split(@MenorID,',')

                IF NOT EXISTS(SELECT 1 
                              FROM   dbo.pasajeromenor PM 
								inner join #tblMenorID tblM on pm.menorid = tblM.MenorID
                              WHERE  PM.pasajeid = @PasajeID 
                                     AND PM.pasajeroid = @PasajeroID 
                                     
									 ) 
                  BEGIN 
                      INSERT INTO dbo.pasajeromenor 
                                  (pasajeid, 
                                   pasajeroid, 
                                   menorid) 
					  Select @PasajeID,  @PasajeroID, MenorID from #tblMenorID 

                      SELECT 1                                   AS Id, 
                             'Se vinculo correctamete el o los menores.' AS ErrorMsg 
                  END 
                ELSE 
                  BEGIN 
                      SELECT 0 
                             AS Id 
                             , 
  'El menor seleccionado ya ha sido vinculado con el pasajero seleccionado.' 
         AS 
  ErrorMsg 

  drop table #tblMenorID
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
 
