CREATE PROCEDURE [dbo].[05_RoomType]
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
			select Id, Descripcion, CapacidadNormal, mg_RoomTypeId
			from dbo.HabitacionTipo ht			
		)

		merge dbo.[RoomType] as s
		using temp t
			on t.mg_RoomTypeId = s.Id
		when not matched
			then
				insert (Name, NormalCapacity)
				values (Descripcion, CapacidadNormal)
			output t.Id, INSERTED.Id
			INTO @output (old_Id, new_Id);

		update s
		set s.mg_RoomTypeId = t.new_Id
		from dbo.HabitacionTipo s
		inner join @output t on t.old_Id = s.ID

		SELECT @@ROWCOUNT AS 'RoomType INSERTED'

		COMMIT
	END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        EXEC dbo.errorHandler;
    END CATCH; 
END