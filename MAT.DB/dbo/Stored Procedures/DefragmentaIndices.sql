
	CREATE PROCEDURE [dbo].[DefragmentaIndices](
--— Author:                             sergio garcia
--— Create date:                    5/Dic/2008
--— Description:                    Procedimiento que defragmenta indices, de una base de datos, de acuerdo al tamaño del indices.
--— Indices Grandes:         10 Indices de cualquier tamaño, con mas de 30% de Fragmentacion y 10 o más segmentos
--— Indices Medianos:        10 Indices entre 8192 y 32 páginas, mas del 20% de Fragmentacion y 3 o más segmentos
--— Indices Pequenos: 100 Indices entre 256 y 32 páginas, , mas del 20% de Fragmentacion y 3 o más segmentos
--— Parametros:                    @Tipo= Grandes, Medianos y Pequenos. Default=Grandes
    @Tipo    VARCHAR(10)='Grandes'       -- — Medianos, Pequenos
)
AS
DECLARE @db_id         INT;
DECLARE @NumPages     BIGINT;
DECLARE @NumIndexes INT;
 
DECLARE @Comando NVARCHAR(MAX);
DECLARE @DB INT
SET NOCOUNT ON;
SET @DB=DB_ID()       -- -–Requerido por modo de compatibilidad 80.
 
IF (@Tipo NOT IN('Grandes', 'Medianos', 'Pequenos'))
    BEGIN
    RAISERROR('Parametro @Tipo Invalido, use: Grandes, Medianos o Pequenos', 16,1);
    RETURN;
    END
SET @db_id = DB_ID(N'Adam');
SET @Comando='';
 
IF @Tipo='Grandes'
    BEGIN
    --— Reindexa las 10 mas grandes sin importar el tamano
    SELECT TOP 10 @Comando=@Comando+CHAR(13)+CHAR(10)+'ALTER INDEX '
        + Indexes.Name
        +' ON '+OBJECT_NAME(ObjectID)+' REBUILD;'
    FROM FilteredIndexFragmentation(@DB, NULL, NULL, NULL, 30,10) AS FIF
    JOIN SYS.INDEXES AS Indexes
        ON INDEXES.OBJECT_ID=ObjectID
            AND INDEXES.INDEX_ID=IndexID
    ORDER BY (IndexDepth*IndexDepth*FragmentationRate*FragmentCount/100) DESC
    END
ELSE
    BEGIN
    --— Reindexa las 10 si es Medianos, 50 si es Pequenos
    SELECT TOP (CASE WHEN @Tipo='Medianos' THEN 10 ELSE 50 END)
        @Comando=@Comando+CHAR(13)+CHAR(10)+'ALTER INDEX '
        + IndexPages.Name
        +' ON '+OBJECT_NAME(ObjectID)+' REBUILD;'
    FROM (SELECT indexes.object_id
                , indexes.index_id
                , Indexes.Name
                , sum(allocation_units.total_pages) as totalPages
            FROM sys.indexes AS indexes
            JOIN sys.partitions AS partitions
            ON indexes.object_id = partitions.object_id
                    and indexes.index_id = partitions.index_id
            JOIN sys.allocation_units AS allocation_units
            ON partitions.partition_id = allocation_units.container_id
            WHERE indexes.index_id >0
             AND allocation_units.total_pages>0
            GROUP BY indexes.object_id, indexes.index_id, Indexes.Name
            HAVING sum(allocation_units.total_pages) BETWEEN 32 AND
                (CASE WHEN @Tipo='Medianos' THEN 8192 ELSE 256 END)        
--— Medianos si tienen menos de 8192 paginas, Pequenos si tienen menos de 256 paginas
           ) AS IndexPages
--— No se emplea el CROSS APPLY por compatibilidad con nivel de compatibilidad 80 (SQL 2000),
--— es posible que tenga un importante impacto en desempeno usar el CROSS APPLY.
--— se recomienda usar CROSS APPLY para compatibilidad 90 o 100.
--—    CROSS APPLY FilteredIndexFragmentation(@DB, IndexPages.object_id, IndexPages.index_id, NULL, 20,3) AS FIF
    JOIN FilteredIndexFragmentation(@DB, NULL, NULL, NULL, 20,3) AS FIF
    ON IndexPages.object_id=FIF.ObjectID
        AND IndexPages.index_id=FIF.IndexID
    -- La columna IndexDepth esta deliberadamente 2 veces, para dar prioridad a indices mas profundos.
    ORDER BY (IndexDepth*IndexDepth*FragmentationRate*FragmentCount/100) DESC
 
    END
EXEC sp_executesql @Comando

