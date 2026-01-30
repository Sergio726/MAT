
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Transporte table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureTransporte_Update]
(

	@TransporteId uniqueidentifier   ,

	@OriginalTransporteId uniqueidentifier   ,

	@NroCoche varchar (50)  ,

	@MaxPasajeros int   ,

	@KmRecorridos varchar (50)  ,

	@UltimoService date   ,

	@Matricula varchar (10)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Transporte]
				SET
					[TransporteID] = @TransporteId
					,[NroCoche] = @NroCoche
					,[MaxPasajeros] = @MaxPasajeros
					,[KmRecorridos] = @KmRecorridos
					,[UltimoService] = @UltimoService
					,[Matricula] = @Matricula
				WHERE
[TransporteID] = @OriginalTransporteId 
				
			



