CREATE PROCEDURE [dbo].[01_Country]	
AS
/*======================================================================
 Created By: Ruben Tejerina
 Comments: 
 
 History
 ----------------------------------------------------------------------
 09/09/2024	Ruben Tejerina	Migrate Pais to Country
 ======================================================================*/
SET NOCOUNT
	,XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

BEGIN
	BEGIN TRY
		BEGIN TRAN

		declare @output table (old_Id UNIQUEIDENTIFIER, new_Id INT);

		merge dbo.Country as c
		using dbo.Pais p
			on p.mg_CountryID = c.Id
		when not matched
			then
				insert (Name)
				values (Descripcion)
			output p.PaisID, INSERTED.Id
			INTO @output (old_Id, new_Id);

		update s
		set s.mg_CountryID = t.new_Id
		from dbo.Pais s
		inner join @output t on t.old_Id = s.PaisID

		SELECT @@ROWCOUNT AS 'Countries INSERTED'

		COMMIT
	END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        EXEC dbo.errorHandler;
    END CATCH; 
END