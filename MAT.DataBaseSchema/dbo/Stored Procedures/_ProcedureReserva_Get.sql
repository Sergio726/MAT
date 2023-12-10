
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets records from the Reserva view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureReserva_Get]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


                    
                    BEGIN
    
                    -- Build the sql query
                    DECLARE @SQL AS nvarchar(4000)
                    SET @SQL = ' SELECT * FROM [dbo].[Reserva]'
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
                


