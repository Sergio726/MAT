CREATE PROCEDURE [dbo].[04_Location]
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
			select loc.ID, loc.Nombre, dep.mg_DepartmentId, loc.mg_LocationId
			from dbo.Localidad loc
			inner join Departamento dep on dep.ID = loc.idDepartamento
		)

		merge dbo.[Location] as s
		using temp t
			on t.mg_LocationId = s.Id
		when not matched
			then
				insert (Name, IdDepartment)
				values (Nombre, mg_DepartmentId)
			output t.ID, INSERTED.Id
			INTO @output (old_Id, new_Id);

		update s
		set s.mg_LocationId = t.new_Id
		from dbo.Localidad s
		inner join @output t on t.old_Id = s.ID

		SELECT @@ROWCOUNT AS 'Location INSERTED'

		COMMIT
	END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        EXEC dbo.errorHandler;
    END CATCH; 
END