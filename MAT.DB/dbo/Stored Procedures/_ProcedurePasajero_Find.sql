
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Pasajero table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePasajero_Find]
(

	@SearchUsingOR bit   = null ,

	@PasajeroId uniqueidentifier   = null ,

	@Pasaporte varchar (100)  = null ,

	@VencimientoPasaporte date   = null ,

	@EmisionPasaporte date   = null ,

	@PaisOrigen varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PasajeroID]
	, [Pasaporte]
	, [VencimientoPasaporte]
	, [EmisionPasaporte]
	, [PaisOrigen]
    FROM
	[dbo].[Pasajero]
    WHERE 
	 ([PasajeroID] = @PasajeroId OR @PasajeroId IS NULL)
	AND ([Pasaporte] = @Pasaporte OR @Pasaporte IS NULL)
	AND ([VencimientoPasaporte] = @VencimientoPasaporte OR @VencimientoPasaporte IS NULL)
	AND ([EmisionPasaporte] = @EmisionPasaporte OR @EmisionPasaporte IS NULL)
	AND ([PaisOrigen] = @PaisOrigen OR @PaisOrigen IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PasajeroID]
	, [Pasaporte]
	, [VencimientoPasaporte]
	, [EmisionPasaporte]
	, [PaisOrigen]
    FROM
	[dbo].[Pasajero]
    WHERE 
	 ([PasajeroID] = @PasajeroId AND @PasajeroId is not null)
	OR ([Pasaporte] = @Pasaporte AND @Pasaporte is not null)
	OR ([VencimientoPasaporte] = @VencimientoPasaporte AND @VencimientoPasaporte is not null)
	OR ([EmisionPasaporte] = @EmisionPasaporte AND @EmisionPasaporte is not null)
	OR ([PaisOrigen] = @PaisOrigen AND @PaisOrigen is not null)
	SELECT @@ROWCOUNT			
  END
				



