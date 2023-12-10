
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets records from the MovimientoCuenta table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureMovimientoCuenta_GetPaged
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [MovimientoID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([MovimientoID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [MovimientoID]'
				SET @SQL = @SQL + ' FROM [dbo].[MovimientoCuenta]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[MovimientoID], O.[PagoID], O.[FacturaID], O.[FechaRegistro], O.[CuentaID], O.[NotaID], O.[CuentaCorrienteID]
				FROM
				    [dbo].[MovimientoCuenta] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[MovimientoID] = PageIndex.[MovimientoID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[MovimientoCuenta]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

