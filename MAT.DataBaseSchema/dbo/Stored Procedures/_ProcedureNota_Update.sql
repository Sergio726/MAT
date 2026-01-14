
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Nota table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureNota_Update
(

	@NotaId uniqueidentifier   ,

	@OriginalNotaId uniqueidentifier   ,

	@PorcentajeRetencion float   ,

	@MontoRetencion float   ,

	@Fecha date   ,

	@Dias int   ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@NroNota varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Nota]
				SET
					[NotaID] = @NotaId
					,[PorcentajeRetencion] = @PorcentajeRetencion
					,[MontoRetencion] = @MontoRetencion
					,[Fecha] = @Fecha
					,[Dias] = @Dias
					,[ClienteID] = @ClienteId
					,[VendedorID] = @VendedorId
					,[NroNota] = @NroNota
				WHERE
[NotaID] = @OriginalNotaId