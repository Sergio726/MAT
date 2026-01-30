
CREATE PROCEDURE [dbo].[usp_MAT_ObservacionViajeCategoria_GetAll]
AS 
/*-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 2019/11/04 
-- Description:  get categoria
   
-- =============================================*/
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;

BEGIN 
	select Id, 
	   Categoria
	from dbo.ObservacionViajeCategoria
    
END
