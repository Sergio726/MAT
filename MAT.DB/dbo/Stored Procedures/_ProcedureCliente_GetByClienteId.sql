
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Cliente table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureCliente_GetByClienteId]
(

	@ClienteId uniqueidentifier   
)
AS


				SELECT
					[ClienteID],
					[RazonSocial],
					[Cuit],
					[Moneda],
					[Empresa],
					[Ocupacion],
					[FormaPago],
					[CondicionIva],
					[VendedorID],
					[Fax],
					[Web],
					[Idioma],
					[Promotor],
					[Observacion],
					[TipoID]
				FROM
					[dbo].[Cliente]
				WHERE
					[ClienteID] = @ClienteId
				SELECT @@ROWCOUNT
					
			



