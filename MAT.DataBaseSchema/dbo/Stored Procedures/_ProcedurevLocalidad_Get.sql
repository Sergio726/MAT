
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets records from the vLocalidad view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurevLocalidad_Get
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


                    
                    BEGIN
    
                    -- Build the sql query
                    DECLARE @SQL AS nvarchar(4000)
                    SET @SQL = ' SELECT * FROM [dbo].[vLocalidad]'
                    IF LEN(@WhereClause) > 0
                    BEGIN
                        SET @SQL = @SQL + ' WHERE ' + @WhereClause
                    END
                    IF LEN(@OrderBy) > 0
                    BEGIN
                        SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
                    END
                    
                    -- Execution the query
                    EXEC sp_executesql @SQL
                    
                    -- Return total count
                    SELECT @@ROWCOUNT AS TotalRowCount
                    
                    END