
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Nota table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureNota_GetByNotaId
(

	@NotaId uniqueidentifier   
)
AS


				SELECT
					[NotaID],
					[PorcentajeRetencion],
					[MontoRetencion],
					[Fecha],
					[Dias],
					[ClienteID],
					[VendedorID],
					[NroNota]
				FROM
					[dbo].[Nota]
				WHERE
					[NotaID] = @NotaId
				SELECT @@ROWCOUNT