
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the Transporte table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTransporte_Find
(

	@SearchUsingOR bit   = null ,

	@TransporteId uniqueidentifier   = null ,

	@NroCoche varchar (50)  = null ,

	@MaxPasajeros int   = null ,

	@KmRecorridos varchar (50)  = null ,

	@UltimoService date   = null ,

	@Matricula varchar (10)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [TransporteID]
	, [NroCoche]
	, [MaxPasajeros]
	, [KmRecorridos]
	, [UltimoService]
	, [Matricula]
    FROM
	[dbo].[Transporte]
    WHERE 
	 ([TransporteID] = @TransporteId OR @TransporteId IS NULL)
	AND ([NroCoche] = @NroCoche OR @NroCoche IS NULL)
	AND ([MaxPasajeros] = @MaxPasajeros OR @MaxPasajeros IS NULL)
	AND ([KmRecorridos] = @KmRecorridos OR @KmRecorridos IS NULL)
	AND ([UltimoService] = @UltimoService OR @UltimoService IS NULL)
	AND ([Matricula] = @Matricula OR @Matricula IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [TransporteID]
	, [NroCoche]
	, [MaxPasajeros]
	, [KmRecorridos]
	, [UltimoService]
	, [Matricula]
    FROM
	[dbo].[Transporte]
    WHERE 
	 ([TransporteID] = @TransporteId AND @TransporteId is not null)
	OR ([NroCoche] = @NroCoche AND @NroCoche is not null)
	OR ([MaxPasajeros] = @MaxPasajeros AND @MaxPasajeros is not null)
	OR ([KmRecorridos] = @KmRecorridos AND @KmRecorridos is not null)
	OR ([UltimoService] = @UltimoService AND @UltimoService is not null)
	OR ([Matricula] = @Matricula AND @Matricula is not null)
	SELECT @@ROWCOUNT			
  END
				

