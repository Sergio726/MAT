
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Cliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureCliente_Get_List]

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
					
				SELECT @@ROWCOUNT
			



