
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Provincia table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureProvincia_Find]
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@Nombre nvarchar (250)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [Nombre]
    FROM
	[dbo].[Provincia]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([Nombre] = @Nombre OR @Nombre IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [Nombre]
    FROM
	[dbo].[Provincia]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([Nombre] = @Nombre AND @Nombre is not null)
	SELECT @@ROWCOUNT			
  END
				



