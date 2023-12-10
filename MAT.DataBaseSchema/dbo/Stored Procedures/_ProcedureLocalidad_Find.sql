
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Localidad table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureLocalidad_Find
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@IdDepartamento int   = null ,

	@Nombre nvarchar (250)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [idDepartamento]
	, [Nombre]
    FROM
	[dbo].[Localidad]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([idDepartamento] = @IdDepartamento OR @IdDepartamento IS NULL)
	AND ([Nombre] = @Nombre OR @Nombre IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [idDepartamento]
	, [Nombre]
    FROM
	[dbo].[Localidad]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([idDepartamento] = @IdDepartamento AND @IdDepartamento is not null)
	OR ([Nombre] = @Nombre AND @Nombre is not null)
	SELECT @@ROWCOUNT			
  END
				

