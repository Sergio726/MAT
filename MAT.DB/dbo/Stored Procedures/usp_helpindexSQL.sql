
create procedure [dbo].[usp_helpindexSQL] (@OBJNAME VARCHAR(128) = '%%')
as

begin

	SELECT SCHEMA_NAME (o.SCHEMA_ID)  SchemaName
	   ,o.name ObjectName
	   ,i.name IndexName
	   ,i.index_id
	   ,i.type_desc
	   ,LEFT(list, ISNULL(splitter-1,LEN(list))) Columns
	   , SUBSTRING(list, indCol.splitter +1, 100) includedColumns   --len(name) - splitter-1) columns
	   , COUNT(1) OVER (PARTITION BY o.object_id) as TotNumIndexes
	FROM sys.indexes i
		JOIN sys.objects o ON i.object_id = o.object_id
		CROSS APPLY ( SELECT NULLIF(CHARINDEX('|',indexCols.list),0) splitter , list
					  FROM (SELECT CAST((
							   SELECT CASE 
										WHEN sc.is_included_column = 1 AND sc.ColPos = 1 
										THEN '|' 
										ELSE '' 
									  END +
									  CASE WHEN sc.ColPos  > 1 
										THEN ', ' 
										ELSE '' 
									  END + name
								 FROM (SELECT sc.is_included_column, index_column_id, name
											, ROW_NUMBER() OVER (PARTITION BY sc.is_included_column
																 ORDER BY sc.index_column_id) ColPos
										FROM sys.index_columns  sc
											JOIN sys.columns  c ON sc.object_id = c.object_id
												AND sc.column_id = c.column_id
									   WHERE sc.index_id = i.index_id
										 AND sc.object_id = i.object_id ) sc
					  ORDER BY sc.is_included_column
								,ColPos
						  FOR XML PATH (''), TYPE) AS VARCHAR(MAX)) list)indexCols ) indCol
	WHERE 1 = 1 
		AND o.name  LIKE @OBJNAME 
	 --   AND i.name NOT LIKE '_dta%'
	ORDER BY SchemaName, ObjectName, IndexName

end


