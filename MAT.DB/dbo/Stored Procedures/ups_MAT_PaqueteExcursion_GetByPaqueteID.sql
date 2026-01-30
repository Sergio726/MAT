
create PROCEDURE [dbo].[ups_MAT_PaqueteExcursion_GetByPaqueteID] @PaqueteID UNIQUEIDENTIFIER
AS 
  /*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 11-05-2018   
  -- Description:  get excursiones by PaqueteID
    
  -- =============================================*/ 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	  select pe.PaqueteExcursionID, pe.IsOpcional, pe.PaqueteID, ex.Descripcion
	  from dbo.Excursion ex
	  inner join dbo.PaqueteExcursion pe 
		  on ex.ExcursionID = pe.ExcursionID
	  where pe.PaqueteID = @PaqueteID
	
	

  END

