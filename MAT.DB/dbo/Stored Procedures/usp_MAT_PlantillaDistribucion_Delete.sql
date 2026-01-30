
CREATE PROCEDURE [dbo].[usp_MAT_PlantillaDistribucion_Delete](@PlantillaId int)
AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-05-29
-- Description: delete plantilla de distribucion

  
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

	begin try

		begin tran

			delete tp
			from dbo.PlantillaDistribucionHabitacion p
			inner join dbo.TransPlantillaDistribucionHabitacion tp
				on p.Id = tp.PlantillaDistribucionHabitacionID
			where p.Id = @PlantillaId

			delete p
			from dbo.PlantillaDistribucionHabitacion p
			where p.Id = @PlantillaId

		commit;
	end try
	begin catch
		IF @@TRANCOUNT > 0 
        ROLLBACK TRAN 

        DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + Error_line(), 
                @errState = Error_state() 

        RAISERROR (N'Error al intentar eliminar plantilla. MSG: %d',16,@errState,1,@errmsg); 

	end catch


END