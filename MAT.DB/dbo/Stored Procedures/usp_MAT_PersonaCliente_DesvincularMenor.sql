CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_DesvincularMenor] (@FacturaID varchar(max) = '')
AS 
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 28-01-2017
-- Description:	eliminar los menores vinculados al tutor
-- =============================================
BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

BEGIN try 
    BEGIN TRAN; 

			delete pm
			from Pasaje p
			inner join PasajeroMenor pm 
				on pm.pasajeid = p.PasajeID
			where p.FacturaID = @FacturaID
            SELECT 1  AS Id, 'Se desvinculo correctamete el menor.' AS ErrorMsg 

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
 
