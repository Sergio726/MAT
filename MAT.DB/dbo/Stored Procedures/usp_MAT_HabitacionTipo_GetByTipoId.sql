

CREATE PROCEDURE [dbo].[usp_MAT_HabitacionTipo_GetByTipoId](@TipoId int)
AS 
-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 2017/06/17 
-- Description:  get HabitacionTipo by TipoId
-- =============================================
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;

BEGIN 
  SELECT ht.Id,
		 ht.Descripcion
  FROM dbo.HabitacionTipo ht
  WHERE ht.Id = @TipoId
    
END


