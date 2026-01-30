
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Pais table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePais_Find]
(

	@SearchUsingOR bit   = null ,

	@PaisId uniqueidentifier   = null ,

	@Descripcion varchar (100)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaisID]
	, [Descripcion]
    FROM
	[dbo].[Pais]
    WHERE 
	 ([PaisID] = @PaisId OR @PaisId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaisID]
	, [Descripcion]
    FROM
	[dbo].[Pais]
    WHERE 
	 ([PaisID] = @PaisId AND @PaisId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				



