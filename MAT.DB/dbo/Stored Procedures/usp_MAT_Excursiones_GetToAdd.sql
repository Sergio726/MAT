
CREATE PROCEDURE [dbo].[usp_MAT_Excursiones_GetToAdd](@PaqueteID uniqueidentifier)
as
 /*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 11-01-2018 
  -- Description:  Get pasajes with pay seña incompleta
        
  -- =============================================*/ 
begin
 SET nocount, xact_abort ON; 
 SET TRANSACTION isolation level READ uncommitted; 

	select Descripcion = upper(ex.Descripcion),
		   ex.ExcursionID
	from dbo.Excursion ex
	left join dbo.PaqueteExcursion pe 
		on ex.ExcursionID = pe.ExcursionID
		and pe.PaqueteID = @PaqueteID
	where pe.ExcursionID is null
		
end

