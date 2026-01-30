
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Nota table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureNota_Insert]
(

	@NotaId uniqueidentifier   ,

	@PorcentajeRetencion float   ,

	@MontoRetencion float   ,

	@Fecha date   ,

	@Dias int   ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@NroNota varchar (50)  
)
AS


				
				INSERT INTO [dbo].[Nota]
					(
					[NotaID]
					,[PorcentajeRetencion]
					,[MontoRetencion]
					,[Fecha]
					,[Dias]
					,[ClienteID]
					,[VendedorID]
					,[NroNota]
					)
				VALUES
					(
					@NotaId
					,@PorcentajeRetencion
					,@MontoRetencion
					,@Fecha
					,@Dias
					,@ClienteId
					,@VendedorId
					,@NroNota
					)
				
									
							
			



