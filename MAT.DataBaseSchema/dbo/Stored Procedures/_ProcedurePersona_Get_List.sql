
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Persona table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePersona_Get_List

AS


				
				SELECT
					[PersonaID],
					[Apellido],
					[Nombre],
					[TipoDocumento],
					[NroDocumento],
					[Telefono],
					[Email],
					[FechaNacimiento],
					[LocalidadID],
					[UserId],
					[Domicilio],
					[Sexo],
					[Ocupacion],
					[Nacionalidad],
					[PaisResidencia]
				FROM
					[dbo].[Persona]
					
				SELECT @@ROWCOUNT
			

