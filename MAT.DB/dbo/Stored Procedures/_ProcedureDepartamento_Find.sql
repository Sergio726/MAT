
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Departamento table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureDepartamento_Find]
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@IdProvincia int   = null ,

	@Nombre nvarchar (250)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [idProvincia]
	, [Nombre]
    FROM
	[dbo].[Departamento]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([idProvincia] = @IdProvincia OR @IdProvincia IS NULL)
	AND ([Nombre] = @Nombre OR @Nombre IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [idProvincia]
	, [Nombre]
    FROM
	[dbo].[Departamento]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([idProvincia] = @IdProvincia AND @IdProvincia is not null)
	OR ([Nombre] = @Nombre AND @Nombre is not null)
	SELECT @@ROWCOUNT			
  END
				



