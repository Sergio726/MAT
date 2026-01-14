
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the Transporte table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTransporte_Insert
(

	@TransporteId uniqueidentifier    OUTPUT,

	@NroCoche varchar (50)  ,

	@MaxPasajeros int   ,

	@KmRecorridos varchar (50)  ,

	@UltimoService date   ,

	@Matricula varchar (10)  
)
AS


				
				INSERT INTO [dbo].[Transporte]
					(
					[TransporteID]
					,[NroCoche]
					,[MaxPasajeros]
					,[KmRecorridos]
					,[UltimoService]
					,[Matricula]
					)
				VALUES
					(
					@TransporteId
					,@NroCoche
					,@MaxPasajeros
					,@KmRecorridos
					,@UltimoService
					,@Matricula
					)
				
									
							
			

