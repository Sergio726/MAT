
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets records from the Nota table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureNota_GetPaged
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
				    [NotaID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([NotaID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [NotaID]'
				SET @SQL = @SQL + ' FROM [dbo].[Nota]'
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
				SELECT O.[NotaID], O.[PorcentajeRetencion], O.[MontoRetencion], O.[Fecha], O.[Dias], O.[ClienteID], O.[VendedorID], O.[NroNota]
				FROM
				    [dbo].[Nota] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[NotaID] = PageIndex.[NotaID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Nota]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END