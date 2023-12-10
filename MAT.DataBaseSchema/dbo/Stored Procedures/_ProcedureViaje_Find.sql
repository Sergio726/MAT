
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Viaje table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureViaje_Find
(

	@SearchUsingOR bit   = null ,

	@ViajeId uniqueidentifier   = null ,

	@PaqueteId uniqueidentifier   = null ,

	@Origen varchar (50)  = null ,

	@FechaSalida date   = null ,

	@HoraSalida varchar (50)  = null ,

	@PaisOrigen varchar (50)  = null ,

	@PaisDestino varchar (50)  = null ,

	@Paso varchar (50)  = null ,

	@Medio varchar (50)  = null ,

	@BusId uniqueidentifier   = null ,

	@FechaRegreso date   = null ,

	@HoraRegreso varchar (50)  = null ,

	@Descripcion varchar (200)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ViajeID]
	, [PaqueteID]
	, [Origen]
	, [FechaSalida]
	, [HoraSalida]
	, [PaisOrigen]
	, [PaisDestino]
	, [Paso]
	, [Medio]
	, [BusID]
	, [FechaRegreso]
	, [HoraRegreso]
	, [Descripcion]
    FROM
	[dbo].[Viaje]
    WHERE 
	 ([ViajeID] = @ViajeId OR @ViajeId IS NULL)
	AND ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
	AND ([Origen] = @Origen OR @Origen IS NULL)
	AND ([FechaSalida] = @FechaSalida OR @FechaSalida IS NULL)
	AND ([HoraSalida] = @HoraSalida OR @HoraSalida IS NULL)
	AND ([PaisOrigen] = @PaisOrigen OR @PaisOrigen IS NULL)
	AND ([PaisDestino] = @PaisDestino OR @PaisDestino IS NULL)
	AND ([Paso] = @Paso OR @Paso IS NULL)
	AND ([Medio] = @Medio OR @Medio IS NULL)
	AND ([BusID] = @BusId OR @BusId IS NULL)
	AND ([FechaRegreso] = @FechaRegreso OR @FechaRegreso IS NULL)
	AND ([HoraRegreso] = @HoraRegreso OR @HoraRegreso IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ViajeID]
	, [PaqueteID]
	, [Origen]
	, [FechaSalida]
	, [HoraSalida]
	, [PaisOrigen]
	, [PaisDestino]
	, [Paso]
	, [Medio]
	, [BusID]
	, [FechaRegreso]
	, [HoraRegreso]
	, [Descripcion]
    FROM
	[dbo].[Viaje]
    WHERE 
	 ([ViajeID] = @ViajeId AND @ViajeId is not null)
	OR ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	OR ([Origen] = @Origen AND @Origen is not null)
	OR ([FechaSalida] = @FechaSalida AND @FechaSalida is not null)
	OR ([HoraSalida] = @HoraSalida AND @HoraSalida is not null)
	OR ([PaisOrigen] = @PaisOrigen AND @PaisOrigen is not null)
	OR ([PaisDestino] = @PaisDestino AND @PaisDestino is not null)
	OR ([Paso] = @Paso AND @Paso is not null)
	OR ([Medio] = @Medio AND @Medio is not null)
	OR ([BusID] = @BusId AND @BusId is not null)
	OR ([FechaRegreso] = @FechaRegreso AND @FechaRegreso is not null)
	OR ([HoraRegreso] = @HoraRegreso AND @HoraRegreso is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				

