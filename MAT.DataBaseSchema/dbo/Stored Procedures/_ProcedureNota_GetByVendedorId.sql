
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Nota table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureNota_GetByVendedorId
(

	@VendedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
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
					[VendedorID] = @VendedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON