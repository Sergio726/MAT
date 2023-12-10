
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Destino table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureDestino_Find
(

	@SearchUsingOR bit   = null ,

	@DestinoId uniqueidentifier   = null ,

	@LocalidadId int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [DestinoID]
	, [LocalidadID]
    FROM
	[dbo].[Destino]
    WHERE 
	 ([DestinoID] = @DestinoId OR @DestinoId IS NULL)
	AND ([LocalidadID] = @LocalidadId OR @LocalidadId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [DestinoID]
	, [LocalidadID]
    FROM
	[dbo].[Destino]
    WHERE 
	 ([DestinoID] = @DestinoId AND @DestinoId is not null)
	OR ([LocalidadID] = @LocalidadId AND @LocalidadId is not null)
	SELECT @@ROWCOUNT			
  END
				

