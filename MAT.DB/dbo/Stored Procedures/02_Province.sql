CREATE PROCEDURE [dbo].[02_Province]
AS
/*======================================================================
 Created By: Ruben Tejerina
 Comments: 
 
 History
 ----------------------------------------------------------------------
 09/09/2024	Ruben Tejerina	Migrate table
 ======================================================================*/
SET NOCOUNT
	,XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

BEGIN
	BEGIN TRY
		BEGIN TRAN
		
		declare @output table (old_Id int, new_Id INT);

		with temp as 
		(
			select pro.ID, pro.Nombre, pro.[mg_ProvinceId], p.PaisID, p.mg_CountryId
			from dbo.Provincia pro
			inner join dbo.Pais p on p.PaisID = pro.IdPais

		)

		merge dbo.Province as s
		using temp t
			on t.[mg_ProvinceId] = s.Id
		when not matched
			then
				insert (Name, IdCountry)
				values (Nombre, mg_CountryId)
			output t.ID, INSERTED.Id
			INTO @output (old_Id, new_Id);

		update s
		set s.[mg_ProvinceId] = t.new_Id
		from dbo.Provincia s
		inner join @output t on t.old_Id = s.ID

		SELECT @@ROWCOUNT AS 'Province INSERTED'

		COMMIT
	END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        EXEC dbo.errorHandler;
    END CATCH; 
END