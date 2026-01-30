
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Cliente table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureCliente_GetByTipoId]
(

	@TipoId int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ClienteID],
					[RazonSocial],
					[Cuit],
					[Moneda],
					[Empresa],
					[FormaPago],
					[CondicionIva],
					[VendedorID],
					[Fax],
					[Web],
					[Idioma],
					[Promotor],
					[Observacion],
					[TipoID],
					[Ocupacion]
				FROM
					[dbo].[Cliente]
				WHERE
					[TipoID] = @TipoId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			





















