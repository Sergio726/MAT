
CREATE FUNCTION [dbo].[FilteredIndexFragmentation](
     @DatabaseID                INT
    , @ObjectID                    INT
    , @IndexID                        INT
    , @PartitionNumber        INT=NULL
    , @AverageFragmentation INT =0
    , @FragmentCount        BIGINT =0)
--— Author:                         Javier Loria, Solid Quality Mentors
--— Create date:                    5/Dic/2008
--— Description:                    Funcion que lista los indices con un porcentaje de fragmentacion LOGICA mayor al indicado,
--— y con una cantidad mayor de fragmentos.
--— Encapsula dm_db_index_physical_stats., se requiere para poder hacer CROSS APPLY.
--— No reporta fragmentacion de tablas sin indices, indices XML o Geograficos.
--— Emplea el modo limitado ‘LIMITED’, por el alto costo y mal desempeno del modo ‘DETAILED’
RETURNS @IndexStats TABLE(
     DatabaseID                    SMALLINT
    , ObjectID                        INT
    , IndexID                            INT
    , PartitionNumber            INT
    , IndexDepth                    TINYINT
    , FragmentationRate        FLOAT
    , FragmentCount            BIGINT
    , AverageFragmentSize FLOAT
    , PageCount                    BIGINT)
BEGIN
    INSERT INTO @IndexStats(DatabaseID, ObjectID, IndexID, PartitionNumber, IndexDepth,FragmentationRate
        , FragmentCount, AverageFragmentSize, PageCount)
    SELECT database_id, object_id, index_id, partition_number,
         index_depth, avg_fragmentation_in_percent, fragment_count, avg_fragment_size_in_pages, page_count
    FROM sys.dm_db_index_physical_stats (@DatabaseID, @ObjectID, @IndexID, @PartitionNumber, 'LIMITED' )
    WHERE index_type_desc IN('CLUSTERED INDEX', 'NONCLUSTERED INDEX')
    AND avg_fragmentation_in_percent > @AverageFragmentation
    AND fragment_count>@FragmentCount
RETURN
END

