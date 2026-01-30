
CREATE PROCEDURE [dbo].[usp_MAT_HabitacionTipo_GetAll]
AS 
/* ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 28/03/2017 
-- Description:  get HabitacionTipo
HISTORY
2019-05-18	Garcia Sergio: add CapacidadNormal

-- ===========================================*/
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;

BEGIN 
  SELECT ht.Id,
		 ht.Descripcion,
		 CapacidadNormal = isnull(ht.CapacidadNormal,1)
  FROM dbo.HabitacionTipo ht
    
END