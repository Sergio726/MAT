
CREATE PROCEDURE [dbo].[usp_MAT_PlantillaDistribucionHabitacion_GetAll]
AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-05-29
-- Description: get plantillas

  
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 
	
	select pd.Id,
		   pd.Nombre
	from dbo.PlantillaDistribucionHabitacion pd

END	