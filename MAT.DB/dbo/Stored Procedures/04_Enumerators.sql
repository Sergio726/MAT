CREATE PROCEDURE [dbo].[04_Enumerators]
AS
/*======================================================================
 Created By: Ruben Tejerina
 Comments: 
 
 History
 ----------------------------------------------------------------------
 09/09/2024	Ruben Tejerina	Migrate table from enum types
 ======================================================================*/
SET NOCOUNT
	,XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

BEGIN
	BEGIN TRY
		BEGIN TRAN

		IF (EXISTS(SELECT * FROM dbo.PropertyType))
			

		/* Property type */
		SET IDENTITY_INSERT PropertyType ON
		INSERT INTO dbo.PropertyType(Id, [Name], [Description])
		VALUES (1, 'Hotel','Tipo Hotel')
		SET IDENTITY_INSERT PropertyType OFF

		/* Property Category */
		SET IDENTITY_INSERT PropertyCategory ON
		INSERT INTO dbo.PropertyCategory(Id, [Name], [Description])
		VALUES (1, '1 Estrella',''),
		(2, '2 Estrellas',''),
		(3, '3 Estrellas',''),
		(4, '4 Estrellas',''),
		(5, '5 Estrellas','')
		SET IDENTITY_INSERT PropertyCategory OFF
		
		/* Property Category */
		SET IDENTITY_INSERT PropertyCategory ON
		INSERT INTO dbo.PropertyCategory(Id, [Name], [Description])
		VALUES (1, '1 Estrella',''),
		(2, '2 Estrellas',''),
		(3, '3 Estrellas',''),
		(4, '4 Estrellas',''),
		(5, '5 Estrellas','')
		SET IDENTITY_INSERT PropertyCategory OFF
		
		declare @output table (old_Id int, new_Id INT);

		
		with temp as 
		(
			select dep.ID, dep.Nombre, pro.mg_ProvinceId, dep.mg_DepartmentId
			from dbo.Departamento dep
			inner join Provincia pro on pro.ID = dep.idProvincia
		)

		merge dbo.Department as s
		using temp t
			on t.mg_DepartmentId = s.Id
		when not matched
			then
				insert (Name, IdProvince)
				values (Nombre, mg_ProvinceId)
			output t.ID, INSERTED.Id
			INTO @output (old_Id, new_Id);

		update s
		set s.mg_DepartmentId = t.new_Id
		from dbo.Departamento s
		inner join @output t on t.old_Id = s.ID

		SELECT @@ROWCOUNT AS 'Department INSERTED'

		COMMIT
	END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        EXEC dbo.errorHandler;
    END CATCH; 
END