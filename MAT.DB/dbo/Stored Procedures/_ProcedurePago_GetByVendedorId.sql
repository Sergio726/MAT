
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Pago table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePago_GetByVendedorId]
(

	@VendedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PagoID],
					[FechaPago],
					[Monto],
					[TipoPago],
					[VendedorId],
					[NroRecibo]
				FROM
					[dbo].[Pago]
				WHERE
					[VendedorId] = @VendedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON

