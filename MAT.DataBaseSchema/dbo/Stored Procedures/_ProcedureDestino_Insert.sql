
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Destino table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureDestino_Insert
(

	@DestinoId uniqueidentifier    OUTPUT,

	@LocalidadId int   
)
AS


				
				INSERT INTO [dbo].[Destino]
					(
					[DestinoID]
					,[LocalidadID]
					)
				VALUES
					(
					@DestinoId
					,@LocalidadId
					)
				
									
							
			

