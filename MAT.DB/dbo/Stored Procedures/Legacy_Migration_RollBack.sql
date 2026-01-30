CREATE PROCEDURE [dbo].[Legacy_Migration_RollBack]
AS
/*======================================================================
 Created By: Ruben Tejerina
 Comments: 
 
 History
 ----------------------------------------------------------------------
 09/09/2024	Ruben Tejerina	Rollback migration
 ======================================================================*/
SET NOCOUNT
	,XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

BEGIN
    BEGIN TRY
        BEGIN TRAN



        DROP TABLE dbo.Country
        UPDATE dbo.Pais
        SET mg_CountryId = NULL


	    COMMIT

    COMMIT
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        EXEC dbo.errorHandler;
    END CATCH;
END