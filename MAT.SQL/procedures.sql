
USE [MAT.Intranet]
GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ViajeHotel_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ViajeHotel_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ViajeHotel_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the ViajeHotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ViajeHotel_Get_List

AS


				
				SELECT
					[ViajeHotelID],
					[ViajeID],
					[HotelID],
					[Desde],
					[Hasta],
					[HoraIngreso],
					[HoraSalida]
				FROM
					[dbo].[ViajeHotel]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ViajeHotel_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ViajeHotel_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ViajeHotel_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the ViajeHotel table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ViajeHotel_GetPaged
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
				    [ViajeHotelID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ViajeHotelID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ViajeHotelID]'
				SET @SQL = @SQL + ' FROM [dbo].[ViajeHotel]'
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
				SELECT O.[ViajeHotelID], O.[ViajeID], O.[HotelID], O.[Desde], O.[Hasta], O.[HoraIngreso], O.[HoraSalida]
				FROM
				    [dbo].[ViajeHotel] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ViajeHotelID] = PageIndex.[ViajeHotelID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[ViajeHotel]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ViajeHotel_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ViajeHotel_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ViajeHotel_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the ViajeHotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ViajeHotel_Insert
(

	@ViajeHotelId uniqueidentifier   ,

	@ViajeId uniqueidentifier   ,

	@HotelId uniqueidentifier   ,

	@Desde varchar (10)  ,

	@Hasta varchar (10)  ,

	@HoraIngreso varchar (10)  ,

	@HoraSalida varchar (10)  
)
AS


				
				INSERT INTO [dbo].[ViajeHotel]
					(
					[ViajeHotelID]
					,[ViajeID]
					,[HotelID]
					,[Desde]
					,[Hasta]
					,[HoraIngreso]
					,[HoraSalida]
					)
				VALUES
					(
					@ViajeHotelId
					,@ViajeId
					,@HotelId
					,@Desde
					,@Hasta
					,@HoraIngreso
					,@HoraSalida
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ViajeHotel_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ViajeHotel_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ViajeHotel_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the ViajeHotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ViajeHotel_Update
(

	@ViajeHotelId uniqueidentifier   ,

	@OriginalViajeHotelId uniqueidentifier   ,

	@ViajeId uniqueidentifier   ,

	@HotelId uniqueidentifier   ,

	@Desde varchar (10)  ,

	@Hasta varchar (10)  ,

	@HoraIngreso varchar (10)  ,

	@HoraSalida varchar (10)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[ViajeHotel]
				SET
					[ViajeHotelID] = @ViajeHotelId
					,[ViajeID] = @ViajeId
					,[HotelID] = @HotelId
					,[Desde] = @Desde
					,[Hasta] = @Hasta
					,[HoraIngreso] = @HoraIngreso
					,[HoraSalida] = @HoraSalida
				WHERE
[ViajeHotelID] = @OriginalViajeHotelId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ViajeHotel_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ViajeHotel_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ViajeHotel_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the ViajeHotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ViajeHotel_Delete
(

	@ViajeHotelId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[ViajeHotel] WITH (ROWLOCK) 
				WHERE
					[ViajeHotelID] = @ViajeHotelId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ViajeHotel_GetByHotelId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ViajeHotel_GetByHotelId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ViajeHotel_GetByHotelId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the ViajeHotel table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ViajeHotel_GetByHotelId
(

	@HotelId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ViajeHotelID],
					[ViajeID],
					[HotelID],
					[Desde],
					[Hasta],
					[HoraIngreso],
					[HoraSalida]
				FROM
					[dbo].[ViajeHotel]
				WHERE
					[HotelID] = @HotelId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ViajeHotel_GetByViajeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ViajeHotel_GetByViajeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ViajeHotel_GetByViajeId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the ViajeHotel table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ViajeHotel_GetByViajeId
(

	@ViajeId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ViajeHotelID],
					[ViajeID],
					[HotelID],
					[Desde],
					[Hasta],
					[HoraIngreso],
					[HoraSalida]
				FROM
					[dbo].[ViajeHotel]
				WHERE
					[ViajeID] = @ViajeId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ViajeHotel_GetByViajeHotelId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ViajeHotel_GetByViajeHotelId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ViajeHotel_GetByViajeHotelId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the ViajeHotel table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ViajeHotel_GetByViajeHotelId
(

	@ViajeHotelId uniqueidentifier   
)
AS


				SELECT
					[ViajeHotelID],
					[ViajeID],
					[HotelID],
					[Desde],
					[Hasta],
					[HoraIngreso],
					[HoraSalida]
				FROM
					[dbo].[ViajeHotel]
				WHERE
					[ViajeHotelID] = @ViajeHotelId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ViajeHotel_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ViajeHotel_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ViajeHotel_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the ViajeHotel table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ViajeHotel_Find
(

	@SearchUsingOR bit   = null ,

	@ViajeHotelId uniqueidentifier   = null ,

	@ViajeId uniqueidentifier   = null ,

	@HotelId uniqueidentifier   = null ,

	@Desde varchar (10)  = null ,

	@Hasta varchar (10)  = null ,

	@HoraIngreso varchar (10)  = null ,

	@HoraSalida varchar (10)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ViajeHotelID]
	, [ViajeID]
	, [HotelID]
	, [Desde]
	, [Hasta]
	, [HoraIngreso]
	, [HoraSalida]
    FROM
	[dbo].[ViajeHotel]
    WHERE 
	 ([ViajeHotelID] = @ViajeHotelId OR @ViajeHotelId IS NULL)
	AND ([ViajeID] = @ViajeId OR @ViajeId IS NULL)
	AND ([HotelID] = @HotelId OR @HotelId IS NULL)
	AND ([Desde] = @Desde OR @Desde IS NULL)
	AND ([Hasta] = @Hasta OR @Hasta IS NULL)
	AND ([HoraIngreso] = @HoraIngreso OR @HoraIngreso IS NULL)
	AND ([HoraSalida] = @HoraSalida OR @HoraSalida IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ViajeHotelID]
	, [ViajeID]
	, [HotelID]
	, [Desde]
	, [Hasta]
	, [HoraIngreso]
	, [HoraSalida]
    FROM
	[dbo].[ViajeHotel]
    WHERE 
	 ([ViajeHotelID] = @ViajeHotelId AND @ViajeHotelId is not null)
	OR ([ViajeID] = @ViajeId AND @ViajeId is not null)
	OR ([HotelID] = @HotelId AND @HotelId is not null)
	OR ([Desde] = @Desde AND @Desde is not null)
	OR ([Hasta] = @Hasta AND @Hasta is not null)
	OR ([HoraIngreso] = @HoraIngreso AND @HoraIngreso is not null)
	OR ([HoraSalida] = @HoraSalida AND @HoraSalida is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteServicio_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteServicio_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteServicio_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PaqueteServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteServicio_Get_List

AS


				
				SELECT
					[PaqueteServicioID],
					[ServicioID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteServicio]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteServicio_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteServicio_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteServicio_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PaqueteServicio table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteServicio_GetPaged
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
				    [PaqueteServicioID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PaqueteServicioID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PaqueteServicioID]'
				SET @SQL = @SQL + ' FROM [dbo].[PaqueteServicio]'
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
				SELECT O.[PaqueteServicioID], O.[ServicioID], O.[PaqueteID]
				FROM
				    [dbo].[PaqueteServicio] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PaqueteServicioID] = PageIndex.[PaqueteServicioID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PaqueteServicio]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteServicio_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteServicio_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteServicio_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the PaqueteServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteServicio_Insert
(

	@PaqueteServicioId uniqueidentifier    OUTPUT,

	@ServicioId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PaqueteServicio]
					(
					[PaqueteServicioID]
					,[ServicioID]
					,[PaqueteID]
					)
				VALUES
					(
					@PaqueteServicioId
					,@ServicioId
					,@PaqueteId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteServicio_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteServicio_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteServicio_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the PaqueteServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteServicio_Update
(

	@PaqueteServicioId uniqueidentifier   ,

	@OriginalPaqueteServicioId uniqueidentifier   ,

	@ServicioId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PaqueteServicio]
				SET
					[PaqueteServicioID] = @PaqueteServicioId
					,[ServicioID] = @ServicioId
					,[PaqueteID] = @PaqueteId
				WHERE
[PaqueteServicioID] = @OriginalPaqueteServicioId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteServicio_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteServicio_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteServicio_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the PaqueteServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteServicio_Delete
(

	@PaqueteServicioId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PaqueteServicio] WITH (ROWLOCK) 
				WHERE
					[PaqueteServicioID] = @PaqueteServicioId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteServicio_GetByPaqueteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteServicio_GetByPaqueteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteServicio_GetByPaqueteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaqueteServicio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteServicio_GetByPaqueteId
(

	@PaqueteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaqueteServicioID],
					[ServicioID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteServicio]
				WHERE
					[PaqueteID] = @PaqueteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteServicio_GetByServicioId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteServicio_GetByServicioId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteServicio_GetByServicioId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaqueteServicio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteServicio_GetByServicioId
(

	@ServicioId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaqueteServicioID],
					[ServicioID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteServicio]
				WHERE
					[ServicioID] = @ServicioId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteServicio_GetByPaqueteServicioId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteServicio_GetByPaqueteServicioId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteServicio_GetByPaqueteServicioId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaqueteServicio table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteServicio_GetByPaqueteServicioId
(

	@PaqueteServicioId uniqueidentifier   
)
AS


				SELECT
					[PaqueteServicioID],
					[ServicioID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteServicio]
				WHERE
					[PaqueteServicioID] = @PaqueteServicioId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteServicio_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteServicio_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteServicio_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the PaqueteServicio table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteServicio_Find
(

	@SearchUsingOR bit   = null ,

	@PaqueteServicioId uniqueidentifier   = null ,

	@ServicioId uniqueidentifier   = null ,

	@PaqueteId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaqueteServicioID]
	, [ServicioID]
	, [PaqueteID]
    FROM
	[dbo].[PaqueteServicio]
    WHERE 
	 ([PaqueteServicioID] = @PaqueteServicioId OR @PaqueteServicioId IS NULL)
	AND ([ServicioID] = @ServicioId OR @ServicioId IS NULL)
	AND ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaqueteServicioID]
	, [ServicioID]
	, [PaqueteID]
    FROM
	[dbo].[PaqueteServicio]
    WHERE 
	 ([PaqueteServicioID] = @PaqueteServicioId AND @PaqueteServicioId is not null)
	OR ([ServicioID] = @ServicioId AND @ServicioId is not null)
	OR ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasajero_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasajero_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasajero_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Pasajero table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasajero_Get_List

AS


				
				SELECT
					[PasajeroID],
					[Pasaporte],
					[VencimientoPasaporte],
					[EmisionPasaporte],
					[PaisOrigen]
				FROM
					[dbo].[Pasajero]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasajero_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasajero_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasajero_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Pasajero table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasajero_GetPaged
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
				    [PasajeroID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PasajeroID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PasajeroID]'
				SET @SQL = @SQL + ' FROM [dbo].[Pasajero]'
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
				SELECT O.[PasajeroID], O.[Pasaporte], O.[VencimientoPasaporte], O.[EmisionPasaporte], O.[PaisOrigen]
				FROM
				    [dbo].[Pasajero] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PasajeroID] = PageIndex.[PasajeroID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Pasajero]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasajero_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasajero_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasajero_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Pasajero table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasajero_Insert
(

	@PasajeroId uniqueidentifier   ,

	@Pasaporte varchar (100)  ,

	@VencimientoPasaporte date   ,

	@EmisionPasaporte date   ,

	@PaisOrigen varchar (50)  
)
AS


				
				INSERT INTO [dbo].[Pasajero]
					(
					[PasajeroID]
					,[Pasaporte]
					,[VencimientoPasaporte]
					,[EmisionPasaporte]
					,[PaisOrigen]
					)
				VALUES
					(
					@PasajeroId
					,@Pasaporte
					,@VencimientoPasaporte
					,@EmisionPasaporte
					,@PaisOrigen
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasajero_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasajero_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasajero_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Pasajero table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasajero_Update
(

	@PasajeroId uniqueidentifier   ,

	@OriginalPasajeroId uniqueidentifier   ,

	@Pasaporte varchar (100)  ,

	@VencimientoPasaporte date   ,

	@EmisionPasaporte date   ,

	@PaisOrigen varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Pasajero]
				SET
					[PasajeroID] = @PasajeroId
					,[Pasaporte] = @Pasaporte
					,[VencimientoPasaporte] = @VencimientoPasaporte
					,[EmisionPasaporte] = @EmisionPasaporte
					,[PaisOrigen] = @PaisOrigen
				WHERE
[PasajeroID] = @OriginalPasajeroId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasajero_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasajero_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasajero_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Pasajero table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasajero_Delete
(

	@PasajeroId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Pasajero] WITH (ROWLOCK) 
				WHERE
					[PasajeroID] = @PasajeroId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasajero_GetByPasajeroId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasajero_GetByPasajeroId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasajero_GetByPasajeroId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pasajero table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasajero_GetByPasajeroId
(

	@PasajeroId uniqueidentifier   
)
AS


				SELECT
					[PasajeroID],
					[Pasaporte],
					[VencimientoPasaporte],
					[EmisionPasaporte],
					[PaisOrigen]
				FROM
					[dbo].[Pasajero]
				WHERE
					[PasajeroID] = @PasajeroId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasajero_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasajero_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasajero_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Pasajero table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasajero_Find
(

	@SearchUsingOR bit   = null ,

	@PasajeroId uniqueidentifier   = null ,

	@Pasaporte varchar (100)  = null ,

	@VencimientoPasaporte date   = null ,

	@EmisionPasaporte date   = null ,

	@PaisOrigen varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PasajeroID]
	, [Pasaporte]
	, [VencimientoPasaporte]
	, [EmisionPasaporte]
	, [PaisOrigen]
    FROM
	[dbo].[Pasajero]
    WHERE 
	 ([PasajeroID] = @PasajeroId OR @PasajeroId IS NULL)
	AND ([Pasaporte] = @Pasaporte OR @Pasaporte IS NULL)
	AND ([VencimientoPasaporte] = @VencimientoPasaporte OR @VencimientoPasaporte IS NULL)
	AND ([EmisionPasaporte] = @EmisionPasaporte OR @EmisionPasaporte IS NULL)
	AND ([PaisOrigen] = @PaisOrigen OR @PaisOrigen IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PasajeroID]
	, [Pasaporte]
	, [VencimientoPasaporte]
	, [EmisionPasaporte]
	, [PaisOrigen]
    FROM
	[dbo].[Pasajero]
    WHERE 
	 ([PasajeroID] = @PasajeroId AND @PasajeroId is not null)
	OR ([Pasaporte] = @Pasaporte AND @Pasaporte is not null)
	OR ([VencimientoPasaporte] = @VencimientoPasaporte AND @VencimientoPasaporte is not null)
	OR ([EmisionPasaporte] = @EmisionPasaporte AND @EmisionPasaporte is not null)
	OR ([PaisOrigen] = @PaisOrigen AND @PaisOrigen is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroMenor_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroMenor_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroMenor_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PasajeroMenor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroMenor_Get_List

AS


				
				SELECT
					[id],
					[pasajeid],
					[pasajeroid],
					[menorid]
				FROM
					[dbo].[PasajeroMenor]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroMenor_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroMenor_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroMenor_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PasajeroMenor table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroMenor_GetPaged
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
				    [id] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([id])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [id]'
				SET @SQL = @SQL + ' FROM [dbo].[PasajeroMenor]'
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
				SELECT O.[id], O.[pasajeid], O.[pasajeroid], O.[menorid]
				FROM
				    [dbo].[PasajeroMenor] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[id] = PageIndex.[id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PasajeroMenor]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroMenor_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroMenor_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroMenor_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the PasajeroMenor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroMenor_Insert
(

	@Id int    OUTPUT,

	@Pasajeid uniqueidentifier   ,

	@Pasajeroid uniqueidentifier   ,

	@Menorid uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PasajeroMenor]
					(
					[pasajeid]
					,[pasajeroid]
					,[menorid]
					)
				VALUES
					(
					@Pasajeid
					,@Pasajeroid
					,@Menorid
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroMenor_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroMenor_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroMenor_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the PasajeroMenor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroMenor_Update
(

	@Id int   ,

	@Pasajeid uniqueidentifier   ,

	@Pasajeroid uniqueidentifier   ,

	@Menorid uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PasajeroMenor]
				SET
					[pasajeid] = @Pasajeid
					,[pasajeroid] = @Pasajeroid
					,[menorid] = @Menorid
				WHERE
[id] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroMenor_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroMenor_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroMenor_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the PasajeroMenor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroMenor_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[PasajeroMenor] WITH (ROWLOCK) 
				WHERE
					[id] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroMenor_GetByMenorid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroMenor_GetByMenorid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroMenor_GetByMenorid
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PasajeroMenor table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroMenor_GetByMenorid
(

	@Menorid uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[id],
					[pasajeid],
					[pasajeroid],
					[menorid]
				FROM
					[dbo].[PasajeroMenor]
				WHERE
					[menorid] = @Menorid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroMenor_GetByPasajeroid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroMenor_GetByPasajeroid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroMenor_GetByPasajeroid
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PasajeroMenor table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroMenor_GetByPasajeroid
(

	@Pasajeroid uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[id],
					[pasajeid],
					[pasajeroid],
					[menorid]
				FROM
					[dbo].[PasajeroMenor]
				WHERE
					[pasajeroid] = @Pasajeroid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroMenor_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroMenor_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroMenor_GetById
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PasajeroMenor table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroMenor_GetById
(

	@Id int   
)
AS


				SELECT
					[id],
					[pasajeid],
					[pasajeroid],
					[menorid]
				FROM
					[dbo].[PasajeroMenor]
				WHERE
					[id] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroMenor_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroMenor_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroMenor_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the PasajeroMenor table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroMenor_Find
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@Pasajeid uniqueidentifier   = null ,

	@Pasajeroid uniqueidentifier   = null ,

	@Menorid uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [id]
	, [pasajeid]
	, [pasajeroid]
	, [menorid]
    FROM
	[dbo].[PasajeroMenor]
    WHERE 
	 ([id] = @Id OR @Id IS NULL)
	AND ([pasajeid] = @Pasajeid OR @Pasajeid IS NULL)
	AND ([pasajeroid] = @Pasajeroid OR @Pasajeroid IS NULL)
	AND ([menorid] = @Menorid OR @Menorid IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [id]
	, [pasajeid]
	, [pasajeroid]
	, [menorid]
    FROM
	[dbo].[PasajeroMenor]
    WHERE 
	 ([id] = @Id AND @Id is not null)
	OR ([pasajeid] = @Pasajeid AND @Pasajeid is not null)
	OR ([pasajeroid] = @Pasajeroid AND @Pasajeroid is not null)
	OR ([menorid] = @Menorid AND @Menorid is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Persona_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Persona_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Persona_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Persona table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Persona_Get_List

AS


				
				SELECT
					[PersonaID],
					[Apellido],
					[Nombre],
					[TipoDocumento],
					[NroDocumento],
					[Celular],
					[Telefono],
					[Email],
					[FechaNacimiento],
					[LocalidadID],
					[UserId],
					[Domicilio],
					[Sexo],
					[Ocupacion],
					[Nacionalidad],
					[PaisResidencia],
					[Provincia]
				FROM
					[dbo].[Persona]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Persona_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Persona_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Persona_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Persona table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Persona_GetPaged
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
				    [PersonaID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PersonaID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PersonaID]'
				SET @SQL = @SQL + ' FROM [dbo].[Persona]'
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
				SELECT O.[PersonaID], O.[Apellido], O.[Nombre], O.[TipoDocumento], O.[NroDocumento], O.[Celular], O.[Telefono], O.[Email], O.[FechaNacimiento], O.[LocalidadID], O.[UserId], O.[Domicilio], O.[Sexo], O.[Ocupacion], O.[Nacionalidad], O.[PaisResidencia], O.[Provincia]
				FROM
				    [dbo].[Persona] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PersonaID] = PageIndex.[PersonaID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Persona]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Persona_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Persona_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Persona_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Persona table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Persona_Insert
(

	@PersonaId uniqueidentifier   ,

	@Apellido varchar (100)  ,

	@Nombre varchar (100)  ,

	@TipoDocumento int   ,

	@NroDocumento varchar (50)  ,

	@Celular varchar (50)  ,

	@Telefono varchar (50)  ,

	@Email varchar (50)  ,

	@FechaNacimiento date   ,

	@LocalidadId int   ,

	@UserId int   ,

	@Domicilio varchar (100)  ,

	@Sexo int   ,

	@Ocupacion varchar (50)  ,

	@Nacionalidad varchar (50)  ,

	@PaisResidencia varchar (50)  ,

	@Provincia int   
)
AS


				
				INSERT INTO [dbo].[Persona]
					(
					[PersonaID]
					,[Apellido]
					,[Nombre]
					,[TipoDocumento]
					,[NroDocumento]
					,[Celular]
					,[Telefono]
					,[Email]
					,[FechaNacimiento]
					,[LocalidadID]
					,[UserId]
					,[Domicilio]
					,[Sexo]
					,[Ocupacion]
					,[Nacionalidad]
					,[PaisResidencia]
					,[Provincia]
					)
				VALUES
					(
					@PersonaId
					,@Apellido
					,@Nombre
					,@TipoDocumento
					,@NroDocumento
					,@Celular
					,@Telefono
					,@Email
					,@FechaNacimiento
					,@LocalidadId
					,@UserId
					,@Domicilio
					,@Sexo
					,@Ocupacion
					,@Nacionalidad
					,@PaisResidencia
					,@Provincia
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Persona_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Persona_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Persona_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Persona table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Persona_Update
(

	@PersonaId uniqueidentifier   ,

	@OriginalPersonaId uniqueidentifier   ,

	@Apellido varchar (100)  ,

	@Nombre varchar (100)  ,

	@TipoDocumento int   ,

	@NroDocumento varchar (50)  ,

	@Celular varchar (50)  ,

	@Telefono varchar (50)  ,

	@Email varchar (50)  ,

	@FechaNacimiento date   ,

	@LocalidadId int   ,

	@UserId int   ,

	@Domicilio varchar (100)  ,

	@Sexo int   ,

	@Ocupacion varchar (50)  ,

	@Nacionalidad varchar (50)  ,

	@PaisResidencia varchar (50)  ,

	@Provincia int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Persona]
				SET
					[PersonaID] = @PersonaId
					,[Apellido] = @Apellido
					,[Nombre] = @Nombre
					,[TipoDocumento] = @TipoDocumento
					,[NroDocumento] = @NroDocumento
					,[Celular] = @Celular
					,[Telefono] = @Telefono
					,[Email] = @Email
					,[FechaNacimiento] = @FechaNacimiento
					,[LocalidadID] = @LocalidadId
					,[UserId] = @UserId
					,[Domicilio] = @Domicilio
					,[Sexo] = @Sexo
					,[Ocupacion] = @Ocupacion
					,[Nacionalidad] = @Nacionalidad
					,[PaisResidencia] = @PaisResidencia
					,[Provincia] = @Provincia
				WHERE
[PersonaID] = @OriginalPersonaId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Persona_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Persona_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Persona_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Persona table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Persona_Delete
(

	@PersonaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Persona] WITH (ROWLOCK) 
				WHERE
					[PersonaID] = @PersonaId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Persona_GetByPersonaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Persona_GetByPersonaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Persona_GetByPersonaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Persona table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Persona_GetByPersonaId
(

	@PersonaId uniqueidentifier   
)
AS


				SELECT
					[PersonaID],
					[Apellido],
					[Nombre],
					[TipoDocumento],
					[NroDocumento],
					[Celular],
					[Telefono],
					[Email],
					[FechaNacimiento],
					[LocalidadID],
					[UserId],
					[Domicilio],
					[Sexo],
					[Ocupacion],
					[Nacionalidad],
					[PaisResidencia],
					[Provincia]
				FROM
					[dbo].[Persona]
				WHERE
					[PersonaID] = @PersonaId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Persona_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Persona_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Persona_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Persona table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Persona_Find
(

	@SearchUsingOR bit   = null ,

	@PersonaId uniqueidentifier   = null ,

	@Apellido varchar (100)  = null ,

	@Nombre varchar (100)  = null ,

	@TipoDocumento int   = null ,

	@NroDocumento varchar (50)  = null ,

	@Celular varchar (50)  = null ,

	@Telefono varchar (50)  = null ,

	@Email varchar (50)  = null ,

	@FechaNacimiento date   = null ,

	@LocalidadId int   = null ,

	@UserId int   = null ,

	@Domicilio varchar (100)  = null ,

	@Sexo int   = null ,

	@Ocupacion varchar (50)  = null ,

	@Nacionalidad varchar (50)  = null ,

	@PaisResidencia varchar (50)  = null ,

	@Provincia int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PersonaID]
	, [Apellido]
	, [Nombre]
	, [TipoDocumento]
	, [NroDocumento]
	, [Celular]
	, [Telefono]
	, [Email]
	, [FechaNacimiento]
	, [LocalidadID]
	, [UserId]
	, [Domicilio]
	, [Sexo]
	, [Ocupacion]
	, [Nacionalidad]
	, [PaisResidencia]
	, [Provincia]
    FROM
	[dbo].[Persona]
    WHERE 
	 ([PersonaID] = @PersonaId OR @PersonaId IS NULL)
	AND ([Apellido] = @Apellido OR @Apellido IS NULL)
	AND ([Nombre] = @Nombre OR @Nombre IS NULL)
	AND ([TipoDocumento] = @TipoDocumento OR @TipoDocumento IS NULL)
	AND ([NroDocumento] = @NroDocumento OR @NroDocumento IS NULL)
	AND ([Celular] = @Celular OR @Celular IS NULL)
	AND ([Telefono] = @Telefono OR @Telefono IS NULL)
	AND ([Email] = @Email OR @Email IS NULL)
	AND ([FechaNacimiento] = @FechaNacimiento OR @FechaNacimiento IS NULL)
	AND ([LocalidadID] = @LocalidadId OR @LocalidadId IS NULL)
	AND ([UserId] = @UserId OR @UserId IS NULL)
	AND ([Domicilio] = @Domicilio OR @Domicilio IS NULL)
	AND ([Sexo] = @Sexo OR @Sexo IS NULL)
	AND ([Ocupacion] = @Ocupacion OR @Ocupacion IS NULL)
	AND ([Nacionalidad] = @Nacionalidad OR @Nacionalidad IS NULL)
	AND ([PaisResidencia] = @PaisResidencia OR @PaisResidencia IS NULL)
	AND ([Provincia] = @Provincia OR @Provincia IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PersonaID]
	, [Apellido]
	, [Nombre]
	, [TipoDocumento]
	, [NroDocumento]
	, [Celular]
	, [Telefono]
	, [Email]
	, [FechaNacimiento]
	, [LocalidadID]
	, [UserId]
	, [Domicilio]
	, [Sexo]
	, [Ocupacion]
	, [Nacionalidad]
	, [PaisResidencia]
	, [Provincia]
    FROM
	[dbo].[Persona]
    WHERE 
	 ([PersonaID] = @PersonaId AND @PersonaId is not null)
	OR ([Apellido] = @Apellido AND @Apellido is not null)
	OR ([Nombre] = @Nombre AND @Nombre is not null)
	OR ([TipoDocumento] = @TipoDocumento AND @TipoDocumento is not null)
	OR ([NroDocumento] = @NroDocumento AND @NroDocumento is not null)
	OR ([Celular] = @Celular AND @Celular is not null)
	OR ([Telefono] = @Telefono AND @Telefono is not null)
	OR ([Email] = @Email AND @Email is not null)
	OR ([FechaNacimiento] = @FechaNacimiento AND @FechaNacimiento is not null)
	OR ([LocalidadID] = @LocalidadId AND @LocalidadId is not null)
	OR ([UserId] = @UserId AND @UserId is not null)
	OR ([Domicilio] = @Domicilio AND @Domicilio is not null)
	OR ([Sexo] = @Sexo AND @Sexo is not null)
	OR ([Ocupacion] = @Ocupacion AND @Ocupacion is not null)
	OR ([Nacionalidad] = @Nacionalidad AND @Nacionalidad is not null)
	OR ([PaisResidencia] = @PaisResidencia AND @PaisResidencia is not null)
	OR ([Provincia] = @Provincia AND @Provincia is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Planilla_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Planilla_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Planilla_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Planilla table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Planilla_Get_List

AS


				
				SELECT
					[PlanillaID],
					[ViajeID],
					[FechaRegistro],
					[Total]
				FROM
					[dbo].[Planilla]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Planilla_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Planilla_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Planilla_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Planilla table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Planilla_GetPaged
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
				    [PlanillaID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PlanillaID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PlanillaID]'
				SET @SQL = @SQL + ' FROM [dbo].[Planilla]'
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
				SELECT O.[PlanillaID], O.[ViajeID], O.[FechaRegistro], O.[Total]
				FROM
				    [dbo].[Planilla] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PlanillaID] = PageIndex.[PlanillaID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Planilla]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Planilla_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Planilla_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Planilla_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Planilla table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Planilla_Insert
(

	@PlanillaId uniqueidentifier   ,

	@ViajeId uniqueidentifier   ,

	@FechaRegistro date   ,

	@Total float   
)
AS


				
				INSERT INTO [dbo].[Planilla]
					(
					[PlanillaID]
					,[ViajeID]
					,[FechaRegistro]
					,[Total]
					)
				VALUES
					(
					@PlanillaId
					,@ViajeId
					,@FechaRegistro
					,@Total
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Planilla_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Planilla_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Planilla_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Planilla table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Planilla_Update
(

	@PlanillaId uniqueidentifier   ,

	@OriginalPlanillaId uniqueidentifier   ,

	@ViajeId uniqueidentifier   ,

	@FechaRegistro date   ,

	@Total float   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Planilla]
				SET
					[PlanillaID] = @PlanillaId
					,[ViajeID] = @ViajeId
					,[FechaRegistro] = @FechaRegistro
					,[Total] = @Total
				WHERE
[PlanillaID] = @OriginalPlanillaId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Planilla_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Planilla_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Planilla_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Planilla table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Planilla_Delete
(

	@PlanillaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Planilla] WITH (ROWLOCK) 
				WHERE
					[PlanillaID] = @PlanillaId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Planilla_GetByPlanillaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Planilla_GetByPlanillaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Planilla_GetByPlanillaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Planilla table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Planilla_GetByPlanillaId
(

	@PlanillaId uniqueidentifier   
)
AS


				SELECT
					[PlanillaID],
					[ViajeID],
					[FechaRegistro],
					[Total]
				FROM
					[dbo].[Planilla]
				WHERE
					[PlanillaID] = @PlanillaId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Planilla_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Planilla_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Planilla_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Planilla table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Planilla_Find
(

	@SearchUsingOR bit   = null ,

	@PlanillaId uniqueidentifier   = null ,

	@ViajeId uniqueidentifier   = null ,

	@FechaRegistro date   = null ,

	@Total float   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PlanillaID]
	, [ViajeID]
	, [FechaRegistro]
	, [Total]
    FROM
	[dbo].[Planilla]
    WHERE 
	 ([PlanillaID] = @PlanillaId OR @PlanillaId IS NULL)
	AND ([ViajeID] = @ViajeId OR @ViajeId IS NULL)
	AND ([FechaRegistro] = @FechaRegistro OR @FechaRegistro IS NULL)
	AND ([Total] = @Total OR @Total IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PlanillaID]
	, [ViajeID]
	, [FechaRegistro]
	, [Total]
    FROM
	[dbo].[Planilla]
    WHERE 
	 ([PlanillaID] = @PlanillaId AND @PlanillaId is not null)
	OR ([ViajeID] = @ViajeId AND @ViajeId is not null)
	OR ([FechaRegistro] = @FechaRegistro AND @FechaRegistro is not null)
	OR ([Total] = @Total AND @Total is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaHabitacionItem_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaHabitacionItem_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaHabitacionItem_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PlanillaHabitacionItem table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaHabitacionItem_Get_List

AS


				
				SELECT
					[PlanillaHabitacionItemID],
					[PlanillaID],
					[HabitacionID],
					[Cantidad],
					[Subtotal]
				FROM
					[dbo].[PlanillaHabitacionItem]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaHabitacionItem_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaHabitacionItem_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaHabitacionItem_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PlanillaHabitacionItem table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaHabitacionItem_GetPaged
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
				    [PlanillaHabitacionItemID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PlanillaHabitacionItemID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PlanillaHabitacionItemID]'
				SET @SQL = @SQL + ' FROM [dbo].[PlanillaHabitacionItem]'
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
				SELECT O.[PlanillaHabitacionItemID], O.[PlanillaID], O.[HabitacionID], O.[Cantidad], O.[Subtotal]
				FROM
				    [dbo].[PlanillaHabitacionItem] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PlanillaHabitacionItemID] = PageIndex.[PlanillaHabitacionItemID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PlanillaHabitacionItem]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaHabitacionItem_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaHabitacionItem_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaHabitacionItem_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the PlanillaHabitacionItem table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaHabitacionItem_Insert
(

	@PlanillaHabitacionItemId uniqueidentifier   ,

	@PlanillaId uniqueidentifier   ,

	@HabitacionId uniqueidentifier   ,

	@Cantidad int   ,

	@Subtotal float   
)
AS


				
				INSERT INTO [dbo].[PlanillaHabitacionItem]
					(
					[PlanillaHabitacionItemID]
					,[PlanillaID]
					,[HabitacionID]
					,[Cantidad]
					,[Subtotal]
					)
				VALUES
					(
					@PlanillaHabitacionItemId
					,@PlanillaId
					,@HabitacionId
					,@Cantidad
					,@Subtotal
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaHabitacionItem_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaHabitacionItem_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaHabitacionItem_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the PlanillaHabitacionItem table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaHabitacionItem_Update
(

	@PlanillaHabitacionItemId uniqueidentifier   ,

	@OriginalPlanillaHabitacionItemId uniqueidentifier   ,

	@PlanillaId uniqueidentifier   ,

	@HabitacionId uniqueidentifier   ,

	@Cantidad int   ,

	@Subtotal float   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PlanillaHabitacionItem]
				SET
					[PlanillaHabitacionItemID] = @PlanillaHabitacionItemId
					,[PlanillaID] = @PlanillaId
					,[HabitacionID] = @HabitacionId
					,[Cantidad] = @Cantidad
					,[Subtotal] = @Subtotal
				WHERE
[PlanillaHabitacionItemID] = @OriginalPlanillaHabitacionItemId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaHabitacionItem_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaHabitacionItem_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaHabitacionItem_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the PlanillaHabitacionItem table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaHabitacionItem_Delete
(

	@PlanillaHabitacionItemId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PlanillaHabitacionItem] WITH (ROWLOCK) 
				WHERE
					[PlanillaHabitacionItemID] = @PlanillaHabitacionItemId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaHabitacionItem_GetByPlanillaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaHabitacionItem_GetByPlanillaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaHabitacionItem_GetByPlanillaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PlanillaHabitacionItem table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaHabitacionItem_GetByPlanillaId
(

	@PlanillaId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PlanillaHabitacionItemID],
					[PlanillaID],
					[HabitacionID],
					[Cantidad],
					[Subtotal]
				FROM
					[dbo].[PlanillaHabitacionItem]
				WHERE
					[PlanillaID] = @PlanillaId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaHabitacionItem_GetByPlanillaHabitacionItemId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaHabitacionItem_GetByPlanillaHabitacionItemId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaHabitacionItem_GetByPlanillaHabitacionItemId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PlanillaHabitacionItem table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaHabitacionItem_GetByPlanillaHabitacionItemId
(

	@PlanillaHabitacionItemId uniqueidentifier   
)
AS


				SELECT
					[PlanillaHabitacionItemID],
					[PlanillaID],
					[HabitacionID],
					[Cantidad],
					[Subtotal]
				FROM
					[dbo].[PlanillaHabitacionItem]
				WHERE
					[PlanillaHabitacionItemID] = @PlanillaHabitacionItemId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaHabitacionItem_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaHabitacionItem_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaHabitacionItem_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the PlanillaHabitacionItem table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaHabitacionItem_Find
(

	@SearchUsingOR bit   = null ,

	@PlanillaHabitacionItemId uniqueidentifier   = null ,

	@PlanillaId uniqueidentifier   = null ,

	@HabitacionId uniqueidentifier   = null ,

	@Cantidad int   = null ,

	@Subtotal float   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PlanillaHabitacionItemID]
	, [PlanillaID]
	, [HabitacionID]
	, [Cantidad]
	, [Subtotal]
    FROM
	[dbo].[PlanillaHabitacionItem]
    WHERE 
	 ([PlanillaHabitacionItemID] = @PlanillaHabitacionItemId OR @PlanillaHabitacionItemId IS NULL)
	AND ([PlanillaID] = @PlanillaId OR @PlanillaId IS NULL)
	AND ([HabitacionID] = @HabitacionId OR @HabitacionId IS NULL)
	AND ([Cantidad] = @Cantidad OR @Cantidad IS NULL)
	AND ([Subtotal] = @Subtotal OR @Subtotal IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PlanillaHabitacionItemID]
	, [PlanillaID]
	, [HabitacionID]
	, [Cantidad]
	, [Subtotal]
    FROM
	[dbo].[PlanillaHabitacionItem]
    WHERE 
	 ([PlanillaHabitacionItemID] = @PlanillaHabitacionItemId AND @PlanillaHabitacionItemId is not null)
	OR ([PlanillaID] = @PlanillaId AND @PlanillaId is not null)
	OR ([HabitacionID] = @HabitacionId AND @HabitacionId is not null)
	OR ([Cantidad] = @Cantidad AND @Cantidad is not null)
	OR ([Subtotal] = @Subtotal AND @Subtotal is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaServicioItem_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaServicioItem_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaServicioItem_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PlanillaServicioItem table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaServicioItem_Get_List

AS


				
				SELECT
					[PlanillaServicioItemID],
					[PlanillaID],
					[ServicioID],
					[Cantidad],
					[Subtotal]
				FROM
					[dbo].[PlanillaServicioItem]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaServicioItem_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaServicioItem_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaServicioItem_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PlanillaServicioItem table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaServicioItem_GetPaged
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
				    [PlanillaServicioItemID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PlanillaServicioItemID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PlanillaServicioItemID]'
				SET @SQL = @SQL + ' FROM [dbo].[PlanillaServicioItem]'
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
				SELECT O.[PlanillaServicioItemID], O.[PlanillaID], O.[ServicioID], O.[Cantidad], O.[Subtotal]
				FROM
				    [dbo].[PlanillaServicioItem] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PlanillaServicioItemID] = PageIndex.[PlanillaServicioItemID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PlanillaServicioItem]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaServicioItem_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaServicioItem_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaServicioItem_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the PlanillaServicioItem table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaServicioItem_Insert
(

	@PlanillaServicioItemId uniqueidentifier   ,

	@PlanillaId uniqueidentifier   ,

	@ServicioId uniqueidentifier   ,

	@Cantidad int   ,

	@Subtotal float   
)
AS


				
				INSERT INTO [dbo].[PlanillaServicioItem]
					(
					[PlanillaServicioItemID]
					,[PlanillaID]
					,[ServicioID]
					,[Cantidad]
					,[Subtotal]
					)
				VALUES
					(
					@PlanillaServicioItemId
					,@PlanillaId
					,@ServicioId
					,@Cantidad
					,@Subtotal
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaServicioItem_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaServicioItem_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaServicioItem_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the PlanillaServicioItem table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaServicioItem_Update
(

	@PlanillaServicioItemId uniqueidentifier   ,

	@OriginalPlanillaServicioItemId uniqueidentifier   ,

	@PlanillaId uniqueidentifier   ,

	@ServicioId uniqueidentifier   ,

	@Cantidad int   ,

	@Subtotal float   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PlanillaServicioItem]
				SET
					[PlanillaServicioItemID] = @PlanillaServicioItemId
					,[PlanillaID] = @PlanillaId
					,[ServicioID] = @ServicioId
					,[Cantidad] = @Cantidad
					,[Subtotal] = @Subtotal
				WHERE
[PlanillaServicioItemID] = @OriginalPlanillaServicioItemId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaServicioItem_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaServicioItem_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaServicioItem_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the PlanillaServicioItem table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaServicioItem_Delete
(

	@PlanillaServicioItemId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PlanillaServicioItem] WITH (ROWLOCK) 
				WHERE
					[PlanillaServicioItemID] = @PlanillaServicioItemId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaServicioItem_GetByPlanillaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaServicioItem_GetByPlanillaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaServicioItem_GetByPlanillaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PlanillaServicioItem table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaServicioItem_GetByPlanillaId
(

	@PlanillaId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PlanillaServicioItemID],
					[PlanillaID],
					[ServicioID],
					[Cantidad],
					[Subtotal]
				FROM
					[dbo].[PlanillaServicioItem]
				WHERE
					[PlanillaID] = @PlanillaId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaServicioItem_GetByPlanillaServicioItemId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaServicioItem_GetByPlanillaServicioItemId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaServicioItem_GetByPlanillaServicioItemId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PlanillaServicioItem table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaServicioItem_GetByPlanillaServicioItemId
(

	@PlanillaServicioItemId uniqueidentifier   
)
AS


				SELECT
					[PlanillaServicioItemID],
					[PlanillaID],
					[ServicioID],
					[Cantidad],
					[Subtotal]
				FROM
					[dbo].[PlanillaServicioItem]
				WHERE
					[PlanillaServicioItemID] = @PlanillaServicioItemId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PlanillaServicioItem_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PlanillaServicioItem_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PlanillaServicioItem_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the PlanillaServicioItem table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PlanillaServicioItem_Find
(

	@SearchUsingOR bit   = null ,

	@PlanillaServicioItemId uniqueidentifier   = null ,

	@PlanillaId uniqueidentifier   = null ,

	@ServicioId uniqueidentifier   = null ,

	@Cantidad int   = null ,

	@Subtotal float   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PlanillaServicioItemID]
	, [PlanillaID]
	, [ServicioID]
	, [Cantidad]
	, [Subtotal]
    FROM
	[dbo].[PlanillaServicioItem]
    WHERE 
	 ([PlanillaServicioItemID] = @PlanillaServicioItemId OR @PlanillaServicioItemId IS NULL)
	AND ([PlanillaID] = @PlanillaId OR @PlanillaId IS NULL)
	AND ([ServicioID] = @ServicioId OR @ServicioId IS NULL)
	AND ([Cantidad] = @Cantidad OR @Cantidad IS NULL)
	AND ([Subtotal] = @Subtotal OR @Subtotal IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PlanillaServicioItemID]
	, [PlanillaID]
	, [ServicioID]
	, [Cantidad]
	, [Subtotal]
    FROM
	[dbo].[PlanillaServicioItem]
    WHERE 
	 ([PlanillaServicioItemID] = @PlanillaServicioItemId AND @PlanillaServicioItemId is not null)
	OR ([PlanillaID] = @PlanillaId AND @PlanillaId is not null)
	OR ([ServicioID] = @ServicioId AND @ServicioId is not null)
	OR ([Cantidad] = @Cantidad AND @Cantidad is not null)
	OR ([Subtotal] = @Subtotal AND @Subtotal is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteExcursion_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteExcursion_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteExcursion_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PaqueteExcursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteExcursion_Get_List

AS


				
				SELECT
					[PaqueteExcursionID],
					[ExcursionID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteExcursion]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteExcursion_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteExcursion_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteExcursion_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PaqueteExcursion table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteExcursion_GetPaged
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
				    [PaqueteExcursionID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PaqueteExcursionID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PaqueteExcursionID]'
				SET @SQL = @SQL + ' FROM [dbo].[PaqueteExcursion]'
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
				SELECT O.[PaqueteExcursionID], O.[ExcursionID], O.[PaqueteID]
				FROM
				    [dbo].[PaqueteExcursion] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PaqueteExcursionID] = PageIndex.[PaqueteExcursionID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PaqueteExcursion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteExcursion_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteExcursion_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteExcursion_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the PaqueteExcursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteExcursion_Insert
(

	@PaqueteExcursionId uniqueidentifier    OUTPUT,

	@ExcursionId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PaqueteExcursion]
					(
					[PaqueteExcursionID]
					,[ExcursionID]
					,[PaqueteID]
					)
				VALUES
					(
					@PaqueteExcursionId
					,@ExcursionId
					,@PaqueteId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteExcursion_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteExcursion_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteExcursion_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the PaqueteExcursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteExcursion_Update
(

	@PaqueteExcursionId uniqueidentifier   ,

	@OriginalPaqueteExcursionId uniqueidentifier   ,

	@ExcursionId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PaqueteExcursion]
				SET
					[PaqueteExcursionID] = @PaqueteExcursionId
					,[ExcursionID] = @ExcursionId
					,[PaqueteID] = @PaqueteId
				WHERE
[PaqueteExcursionID] = @OriginalPaqueteExcursionId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteExcursion_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteExcursion_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteExcursion_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the PaqueteExcursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteExcursion_Delete
(

	@PaqueteExcursionId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PaqueteExcursion] WITH (ROWLOCK) 
				WHERE
					[PaqueteExcursionID] = @PaqueteExcursionId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteExcursion_GetByExcursionId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteExcursion_GetByExcursionId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteExcursion_GetByExcursionId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaqueteExcursion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteExcursion_GetByExcursionId
(

	@ExcursionId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaqueteExcursionID],
					[ExcursionID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteExcursion]
				WHERE
					[ExcursionID] = @ExcursionId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteExcursion_GetByPaqueteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteExcursion_GetByPaqueteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteExcursion_GetByPaqueteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaqueteExcursion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteExcursion_GetByPaqueteId
(

	@PaqueteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaqueteExcursionID],
					[ExcursionID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteExcursion]
				WHERE
					[PaqueteID] = @PaqueteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteExcursion_GetByPaqueteExcursionId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteExcursion_GetByPaqueteExcursionId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteExcursion_GetByPaqueteExcursionId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaqueteExcursion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteExcursion_GetByPaqueteExcursionId
(

	@PaqueteExcursionId uniqueidentifier   
)
AS


				SELECT
					[PaqueteExcursionID],
					[ExcursionID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteExcursion]
				WHERE
					[PaqueteExcursionID] = @PaqueteExcursionId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteExcursion_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteExcursion_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteExcursion_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the PaqueteExcursion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteExcursion_Find
(

	@SearchUsingOR bit   = null ,

	@PaqueteExcursionId uniqueidentifier   = null ,

	@ExcursionId uniqueidentifier   = null ,

	@PaqueteId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaqueteExcursionID]
	, [ExcursionID]
	, [PaqueteID]
    FROM
	[dbo].[PaqueteExcursion]
    WHERE 
	 ([PaqueteExcursionID] = @PaqueteExcursionId OR @PaqueteExcursionId IS NULL)
	AND ([ExcursionID] = @ExcursionId OR @ExcursionId IS NULL)
	AND ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaqueteExcursionID]
	, [ExcursionID]
	, [PaqueteID]
    FROM
	[dbo].[PaqueteExcursion]
    WHERE 
	 ([PaqueteExcursionID] = @PaqueteExcursionId AND @PaqueteExcursionId is not null)
	OR ([ExcursionID] = @ExcursionId AND @ExcursionId is not null)
	OR ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Precio_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Precio_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Precio_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Precio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Precio_Get_List

AS


				
				SELECT
					[PrecioID],
					[Monto],
					[Vigencia],
					[Descripcion],
					[Mes]
				FROM
					[dbo].[Precio]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Precio_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Precio_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Precio_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Precio table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Precio_GetPaged
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
				    [PrecioID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PrecioID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PrecioID]'
				SET @SQL = @SQL + ' FROM [dbo].[Precio]'
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
				SELECT O.[PrecioID], O.[Monto], O.[Vigencia], O.[Descripcion], O.[Mes]
				FROM
				    [dbo].[Precio] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PrecioID] = PageIndex.[PrecioID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Precio]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Precio_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Precio_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Precio_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Precio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Precio_Insert
(

	@PrecioId uniqueidentifier    OUTPUT,

	@Monto float   ,

	@Vigencia datetime   ,

	@Descripcion varchar (100)  ,

	@Mes varchar (100)  
)
AS


				
				INSERT INTO [dbo].[Precio]
					(
					[PrecioID]
					,[Monto]
					,[Vigencia]
					,[Descripcion]
					,[Mes]
					)
				VALUES
					(
					@PrecioId
					,@Monto
					,@Vigencia
					,@Descripcion
					,@Mes
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Precio_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Precio_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Precio_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Precio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Precio_Update
(

	@PrecioId uniqueidentifier   ,

	@OriginalPrecioId uniqueidentifier   ,

	@Monto float   ,

	@Vigencia datetime   ,

	@Descripcion varchar (100)  ,

	@Mes varchar (100)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Precio]
				SET
					[PrecioID] = @PrecioId
					,[Monto] = @Monto
					,[Vigencia] = @Vigencia
					,[Descripcion] = @Descripcion
					,[Mes] = @Mes
				WHERE
[PrecioID] = @OriginalPrecioId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Precio_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Precio_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Precio_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Precio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Precio_Delete
(

	@PrecioId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Precio] WITH (ROWLOCK) 
				WHERE
					[PrecioID] = @PrecioId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Precio_GetByPrecioId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Precio_GetByPrecioId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Precio_GetByPrecioId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Precio table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Precio_GetByPrecioId
(

	@PrecioId uniqueidentifier   
)
AS


				SELECT
					[PrecioID],
					[Monto],
					[Vigencia],
					[Descripcion],
					[Mes]
				FROM
					[dbo].[Precio]
				WHERE
					[PrecioID] = @PrecioId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Precio_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Precio_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Precio_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Precio table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Precio_Find
(

	@SearchUsingOR bit   = null ,

	@PrecioId uniqueidentifier   = null ,

	@Monto float   = null ,

	@Vigencia datetime   = null ,

	@Descripcion varchar (100)  = null ,

	@Mes varchar (100)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PrecioID]
	, [Monto]
	, [Vigencia]
	, [Descripcion]
	, [Mes]
    FROM
	[dbo].[Precio]
    WHERE 
	 ([PrecioID] = @PrecioId OR @PrecioId IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
	AND ([Vigencia] = @Vigencia OR @Vigencia IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
	AND ([Mes] = @Mes OR @Mes IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PrecioID]
	, [Monto]
	, [Vigencia]
	, [Descripcion]
	, [Mes]
    FROM
	[dbo].[Precio]
    WHERE 
	 ([PrecioID] = @PrecioId AND @PrecioId is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	OR ([Vigencia] = @Vigencia AND @Vigencia is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	OR ([Mes] = @Mes AND @Mes is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioServicio_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioServicio_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioServicio_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PrecioServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioServicio_Get_List

AS


				
				SELECT
					[PrecioServicioID],
					[ServicioID],
					[FechaRegistro],
					[Activo],
					[Precio]
				FROM
					[dbo].[PrecioServicio]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioServicio_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioServicio_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioServicio_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PrecioServicio table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioServicio_GetPaged
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
				    [PrecioServicioID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PrecioServicioID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PrecioServicioID]'
				SET @SQL = @SQL + ' FROM [dbo].[PrecioServicio]'
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
				SELECT O.[PrecioServicioID], O.[ServicioID], O.[FechaRegistro], O.[Activo], O.[Precio]
				FROM
				    [dbo].[PrecioServicio] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PrecioServicioID] = PageIndex.[PrecioServicioID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PrecioServicio]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioServicio_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioServicio_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioServicio_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the PrecioServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioServicio_Insert
(

	@PrecioServicioId uniqueidentifier   ,

	@ServicioId uniqueidentifier   ,

	@FechaRegistro date   ,

	@Activo bit   ,

	@Precio float   
)
AS


				
				INSERT INTO [dbo].[PrecioServicio]
					(
					[PrecioServicioID]
					,[ServicioID]
					,[FechaRegistro]
					,[Activo]
					,[Precio]
					)
				VALUES
					(
					@PrecioServicioId
					,@ServicioId
					,@FechaRegistro
					,@Activo
					,@Precio
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioServicio_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioServicio_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioServicio_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the PrecioServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioServicio_Update
(

	@PrecioServicioId uniqueidentifier   ,

	@OriginalPrecioServicioId uniqueidentifier   ,

	@ServicioId uniqueidentifier   ,

	@FechaRegistro date   ,

	@Activo bit   ,

	@Precio float   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PrecioServicio]
				SET
					[PrecioServicioID] = @PrecioServicioId
					,[ServicioID] = @ServicioId
					,[FechaRegistro] = @FechaRegistro
					,[Activo] = @Activo
					,[Precio] = @Precio
				WHERE
[PrecioServicioID] = @OriginalPrecioServicioId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioServicio_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioServicio_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioServicio_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the PrecioServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioServicio_Delete
(

	@PrecioServicioId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PrecioServicio] WITH (ROWLOCK) 
				WHERE
					[PrecioServicioID] = @PrecioServicioId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioServicio_GetByPrecioServicioId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioServicio_GetByPrecioServicioId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioServicio_GetByPrecioServicioId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PrecioServicio table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioServicio_GetByPrecioServicioId
(

	@PrecioServicioId uniqueidentifier   
)
AS


				SELECT
					[PrecioServicioID],
					[ServicioID],
					[FechaRegistro],
					[Activo],
					[Precio]
				FROM
					[dbo].[PrecioServicio]
				WHERE
					[PrecioServicioID] = @PrecioServicioId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioServicio_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioServicio_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioServicio_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the PrecioServicio table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioServicio_Find
(

	@SearchUsingOR bit   = null ,

	@PrecioServicioId uniqueidentifier   = null ,

	@ServicioId uniqueidentifier   = null ,

	@FechaRegistro date   = null ,

	@Activo bit   = null ,

	@Precio float   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PrecioServicioID]
	, [ServicioID]
	, [FechaRegistro]
	, [Activo]
	, [Precio]
    FROM
	[dbo].[PrecioServicio]
    WHERE 
	 ([PrecioServicioID] = @PrecioServicioId OR @PrecioServicioId IS NULL)
	AND ([ServicioID] = @ServicioId OR @ServicioId IS NULL)
	AND ([FechaRegistro] = @FechaRegistro OR @FechaRegistro IS NULL)
	AND ([Activo] = @Activo OR @Activo IS NULL)
	AND ([Precio] = @Precio OR @Precio IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PrecioServicioID]
	, [ServicioID]
	, [FechaRegistro]
	, [Activo]
	, [Precio]
    FROM
	[dbo].[PrecioServicio]
    WHERE 
	 ([PrecioServicioID] = @PrecioServicioId AND @PrecioServicioId is not null)
	OR ([ServicioID] = @ServicioId AND @ServicioId is not null)
	OR ([FechaRegistro] = @FechaRegistro AND @FechaRegistro is not null)
	OR ([Activo] = @Activo AND @Activo is not null)
	OR ([Precio] = @Precio AND @Precio is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Proveedor_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Proveedor_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Proveedor_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Proveedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Proveedor_Get_List

AS


				
				SELECT
					[ProveedorID],
					[RazonSocial],
					[Telefono],
					[Fax],
					[Web],
					[Email],
					[Idioma],
					[CondicionIva],
					[Cuit],
					[FormaPago],
					[LocalidadID]
				FROM
					[dbo].[Proveedor]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Proveedor_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Proveedor_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Proveedor_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Proveedor table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Proveedor_GetPaged
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
				    [ProveedorID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ProveedorID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ProveedorID]'
				SET @SQL = @SQL + ' FROM [dbo].[Proveedor]'
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
				SELECT O.[ProveedorID], O.[RazonSocial], O.[Telefono], O.[Fax], O.[Web], O.[Email], O.[Idioma], O.[CondicionIva], O.[Cuit], O.[FormaPago], O.[LocalidadID]
				FROM
				    [dbo].[Proveedor] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ProveedorID] = PageIndex.[ProveedorID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Proveedor]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Proveedor_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Proveedor_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Proveedor_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Proveedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Proveedor_Insert
(

	@ProveedorId uniqueidentifier   ,

	@RazonSocial varchar (50)  ,

	@Telefono varchar (50)  ,

	@Fax varchar (50)  ,

	@Web varchar (50)  ,

	@Email varchar (50)  ,

	@Idioma varchar (50)  ,

	@CondicionIva int   ,

	@Cuit varchar (50)  ,

	@FormaPago int   ,

	@LocalidadId int   
)
AS


				
				INSERT INTO [dbo].[Proveedor]
					(
					[ProveedorID]
					,[RazonSocial]
					,[Telefono]
					,[Fax]
					,[Web]
					,[Email]
					,[Idioma]
					,[CondicionIva]
					,[Cuit]
					,[FormaPago]
					,[LocalidadID]
					)
				VALUES
					(
					@ProveedorId
					,@RazonSocial
					,@Telefono
					,@Fax
					,@Web
					,@Email
					,@Idioma
					,@CondicionIva
					,@Cuit
					,@FormaPago
					,@LocalidadId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Proveedor_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Proveedor_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Proveedor_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Proveedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Proveedor_Update
(

	@ProveedorId uniqueidentifier   ,

	@OriginalProveedorId uniqueidentifier   ,

	@RazonSocial varchar (50)  ,

	@Telefono varchar (50)  ,

	@Fax varchar (50)  ,

	@Web varchar (50)  ,

	@Email varchar (50)  ,

	@Idioma varchar (50)  ,

	@CondicionIva int   ,

	@Cuit varchar (50)  ,

	@FormaPago int   ,

	@LocalidadId int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Proveedor]
				SET
					[ProveedorID] = @ProveedorId
					,[RazonSocial] = @RazonSocial
					,[Telefono] = @Telefono
					,[Fax] = @Fax
					,[Web] = @Web
					,[Email] = @Email
					,[Idioma] = @Idioma
					,[CondicionIva] = @CondicionIva
					,[Cuit] = @Cuit
					,[FormaPago] = @FormaPago
					,[LocalidadID] = @LocalidadId
				WHERE
[ProveedorID] = @OriginalProveedorId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Proveedor_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Proveedor_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Proveedor_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Proveedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Proveedor_Delete
(

	@ProveedorId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Proveedor] WITH (ROWLOCK) 
				WHERE
					[ProveedorID] = @ProveedorId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Proveedor_GetByProveedorId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Proveedor_GetByProveedorId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Proveedor_GetByProveedorId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Proveedor table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Proveedor_GetByProveedorId
(

	@ProveedorId uniqueidentifier   
)
AS


				SELECT
					[ProveedorID],
					[RazonSocial],
					[Telefono],
					[Fax],
					[Web],
					[Email],
					[Idioma],
					[CondicionIva],
					[Cuit],
					[FormaPago],
					[LocalidadID]
				FROM
					[dbo].[Proveedor]
				WHERE
					[ProveedorID] = @ProveedorId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Proveedor_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Proveedor_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Proveedor_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Proveedor table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Proveedor_Find
(

	@SearchUsingOR bit   = null ,

	@ProveedorId uniqueidentifier   = null ,

	@RazonSocial varchar (50)  = null ,

	@Telefono varchar (50)  = null ,

	@Fax varchar (50)  = null ,

	@Web varchar (50)  = null ,

	@Email varchar (50)  = null ,

	@Idioma varchar (50)  = null ,

	@CondicionIva int   = null ,

	@Cuit varchar (50)  = null ,

	@FormaPago int   = null ,

	@LocalidadId int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ProveedorID]
	, [RazonSocial]
	, [Telefono]
	, [Fax]
	, [Web]
	, [Email]
	, [Idioma]
	, [CondicionIva]
	, [Cuit]
	, [FormaPago]
	, [LocalidadID]
    FROM
	[dbo].[Proveedor]
    WHERE 
	 ([ProveedorID] = @ProveedorId OR @ProveedorId IS NULL)
	AND ([RazonSocial] = @RazonSocial OR @RazonSocial IS NULL)
	AND ([Telefono] = @Telefono OR @Telefono IS NULL)
	AND ([Fax] = @Fax OR @Fax IS NULL)
	AND ([Web] = @Web OR @Web IS NULL)
	AND ([Email] = @Email OR @Email IS NULL)
	AND ([Idioma] = @Idioma OR @Idioma IS NULL)
	AND ([CondicionIva] = @CondicionIva OR @CondicionIva IS NULL)
	AND ([Cuit] = @Cuit OR @Cuit IS NULL)
	AND ([FormaPago] = @FormaPago OR @FormaPago IS NULL)
	AND ([LocalidadID] = @LocalidadId OR @LocalidadId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ProveedorID]
	, [RazonSocial]
	, [Telefono]
	, [Fax]
	, [Web]
	, [Email]
	, [Idioma]
	, [CondicionIva]
	, [Cuit]
	, [FormaPago]
	, [LocalidadID]
    FROM
	[dbo].[Proveedor]
    WHERE 
	 ([ProveedorID] = @ProveedorId AND @ProveedorId is not null)
	OR ([RazonSocial] = @RazonSocial AND @RazonSocial is not null)
	OR ([Telefono] = @Telefono AND @Telefono is not null)
	OR ([Fax] = @Fax AND @Fax is not null)
	OR ([Web] = @Web AND @Web is not null)
	OR ([Email] = @Email AND @Email is not null)
	OR ([Idioma] = @Idioma AND @Idioma is not null)
	OR ([CondicionIva] = @CondicionIva AND @CondicionIva is not null)
	OR ([Cuit] = @Cuit AND @Cuit is not null)
	OR ([FormaPago] = @FormaPago AND @FormaPago is not null)
	OR ([LocalidadID] = @LocalidadId AND @LocalidadId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Provincia_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Provincia_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Provincia_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Provincia table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Provincia_Get_List

AS


				
				SELECT
					[ID],
					[Nombre],
					[IdPais]
				FROM
					[dbo].[Provincia]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Provincia_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Provincia_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Provincia_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Provincia table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Provincia_GetPaged
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
				    [ID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ID]'
				SET @SQL = @SQL + ' FROM [dbo].[Provincia]'
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
				SELECT O.[ID], O.[Nombre], O.[IdPais]
				FROM
				    [dbo].[Provincia] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ID] = PageIndex.[ID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Provincia]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Provincia_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Provincia_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Provincia_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Provincia table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Provincia_Insert
(

	@Id int    OUTPUT,

	@Nombre nvarchar (250)  ,

	@IdPais uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[Provincia]
					(
					[Nombre]
					,[IdPais]
					)
				VALUES
					(
					@Nombre
					,@IdPais
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Provincia_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Provincia_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Provincia_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Provincia table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Provincia_Update
(

	@Id int   ,

	@Nombre nvarchar (250)  ,

	@IdPais uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Provincia]
				SET
					[Nombre] = @Nombre
					,[IdPais] = @IdPais
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Provincia_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Provincia_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Provincia_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Provincia table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Provincia_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[Provincia] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Provincia_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Provincia_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Provincia_GetById
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Provincia table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Provincia_GetById
(

	@Id int   
)
AS


				SELECT
					[ID],
					[Nombre],
					[IdPais]
				FROM
					[dbo].[Provincia]
				WHERE
					[ID] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Provincia_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Provincia_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Provincia_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Provincia table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Provincia_Find
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@Nombre nvarchar (250)  = null ,

	@IdPais uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [Nombre]
	, [IdPais]
    FROM
	[dbo].[Provincia]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([Nombre] = @Nombre OR @Nombre IS NULL)
	AND ([IdPais] = @IdPais OR @IdPais IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [Nombre]
	, [IdPais]
    FROM
	[dbo].[Provincia]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([Nombre] = @Nombre AND @Nombre is not null)
	OR ([IdPais] = @IdPais AND @IdPais is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Transporte_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Transporte_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Transporte_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Transporte table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Transporte_Get_List

AS


				
				SELECT
					[TransporteID],
					[NroCoche],
					[MaxPasajeros],
					[KmRecorridos],
					[UltimoService],
					[Matricula]
				FROM
					[dbo].[Transporte]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Transporte_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Transporte_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Transporte_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Transporte table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Transporte_GetPaged
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
				    [TransporteID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([TransporteID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [TransporteID]'
				SET @SQL = @SQL + ' FROM [dbo].[Transporte]'
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
				SELECT O.[TransporteID], O.[NroCoche], O.[MaxPasajeros], O.[KmRecorridos], O.[UltimoService], O.[Matricula]
				FROM
				    [dbo].[Transporte] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[TransporteID] = PageIndex.[TransporteID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Transporte]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Transporte_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Transporte_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Transporte_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Transporte table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Transporte_Insert
(

	@TransporteId uniqueidentifier    OUTPUT,

	@NroCoche varchar (50)  ,

	@MaxPasajeros int   ,

	@KmRecorridos int   ,

	@UltimoService date   ,

	@Matricula varchar (10)  
)
AS


				
				INSERT INTO [dbo].[Transporte]
					(
					[TransporteID]
					,[NroCoche]
					,[MaxPasajeros]
					,[KmRecorridos]
					,[UltimoService]
					,[Matricula]
					)
				VALUES
					(
					@TransporteId
					,@NroCoche
					,@MaxPasajeros
					,@KmRecorridos
					,@UltimoService
					,@Matricula
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Transporte_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Transporte_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Transporte_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Transporte table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Transporte_Update
(

	@TransporteId uniqueidentifier   ,

	@OriginalTransporteId uniqueidentifier   ,

	@NroCoche varchar (50)  ,

	@MaxPasajeros int   ,

	@KmRecorridos int   ,

	@UltimoService date   ,

	@Matricula varchar (10)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Transporte]
				SET
					[TransporteID] = @TransporteId
					,[NroCoche] = @NroCoche
					,[MaxPasajeros] = @MaxPasajeros
					,[KmRecorridos] = @KmRecorridos
					,[UltimoService] = @UltimoService
					,[Matricula] = @Matricula
				WHERE
[TransporteID] = @OriginalTransporteId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Transporte_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Transporte_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Transporte_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Transporte table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Transporte_Delete
(

	@TransporteId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Transporte] WITH (ROWLOCK) 
				WHERE
					[TransporteID] = @TransporteId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Transporte_GetByTransporteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Transporte_GetByTransporteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Transporte_GetByTransporteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Transporte table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Transporte_GetByTransporteId
(

	@TransporteId uniqueidentifier   
)
AS


				SELECT
					[TransporteID],
					[NroCoche],
					[MaxPasajeros],
					[KmRecorridos],
					[UltimoService],
					[Matricula]
				FROM
					[dbo].[Transporte]
				WHERE
					[TransporteID] = @TransporteId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Transporte_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Transporte_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Transporte_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Transporte table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Transporte_Find
(

	@SearchUsingOR bit   = null ,

	@TransporteId uniqueidentifier   = null ,

	@NroCoche varchar (50)  = null ,

	@MaxPasajeros int   = null ,

	@KmRecorridos int   = null ,

	@UltimoService date   = null ,

	@Matricula varchar (10)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [TransporteID]
	, [NroCoche]
	, [MaxPasajeros]
	, [KmRecorridos]
	, [UltimoService]
	, [Matricula]
    FROM
	[dbo].[Transporte]
    WHERE 
	 ([TransporteID] = @TransporteId OR @TransporteId IS NULL)
	AND ([NroCoche] = @NroCoche OR @NroCoche IS NULL)
	AND ([MaxPasajeros] = @MaxPasajeros OR @MaxPasajeros IS NULL)
	AND ([KmRecorridos] = @KmRecorridos OR @KmRecorridos IS NULL)
	AND ([UltimoService] = @UltimoService OR @UltimoService IS NULL)
	AND ([Matricula] = @Matricula OR @Matricula IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [TransporteID]
	, [NroCoche]
	, [MaxPasajeros]
	, [KmRecorridos]
	, [UltimoService]
	, [Matricula]
    FROM
	[dbo].[Transporte]
    WHERE 
	 ([TransporteID] = @TransporteId AND @TransporteId is not null)
	OR ([NroCoche] = @NroCoche AND @NroCoche is not null)
	OR ([MaxPasajeros] = @MaxPasajeros AND @MaxPasajeros is not null)
	OR ([KmRecorridos] = @KmRecorridos AND @KmRecorridos is not null)
	OR ([UltimoService] = @UltimoService AND @UltimoService is not null)
	OR ([Matricula] = @Matricula AND @Matricula is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ReservaHabitacion_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReservaHabitacion_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ReservaHabitacion_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the ReservaHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ReservaHabitacion_Get_List

AS


				
				SELECT
					[ReservaHabitacionID],
					[HabitacionID],
					[PasajeID],
					[FechaReserva],
					[Desde],
					[Hasta],
					[Expiro],
					[HoraIngreso],
					[HoraSalida],
					[PasajeroID],
					[ViajeID]
				FROM
					[dbo].[ReservaHabitacion]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ReservaHabitacion_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReservaHabitacion_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ReservaHabitacion_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the ReservaHabitacion table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ReservaHabitacion_GetPaged
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
				    [ReservaHabitacionID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ReservaHabitacionID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ReservaHabitacionID]'
				SET @SQL = @SQL + ' FROM [dbo].[ReservaHabitacion]'
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
				SELECT O.[ReservaHabitacionID], O.[HabitacionID], O.[PasajeID], O.[FechaReserva], O.[Desde], O.[Hasta], O.[Expiro], O.[HoraIngreso], O.[HoraSalida], O.[PasajeroID], O.[ViajeID]
				FROM
				    [dbo].[ReservaHabitacion] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ReservaHabitacionID] = PageIndex.[ReservaHabitacionID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[ReservaHabitacion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ReservaHabitacion_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReservaHabitacion_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ReservaHabitacion_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the ReservaHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ReservaHabitacion_Insert
(

	@ReservaHabitacionId uniqueidentifier    OUTPUT,

	@HabitacionId uniqueidentifier   ,

	@PasajeId uniqueidentifier   ,

	@FechaReserva date   ,

	@Desde date   ,

	@Hasta date   ,

	@Expiro bit   ,

	@HoraIngreso varchar (10)  ,

	@HoraSalida varchar (10)  ,

	@PasajeroId uniqueidentifier   ,

	@ViajeId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[ReservaHabitacion]
					(
					[ReservaHabitacionID]
					,[HabitacionID]
					,[PasajeID]
					,[FechaReserva]
					,[Desde]
					,[Hasta]
					,[Expiro]
					,[HoraIngreso]
					,[HoraSalida]
					,[PasajeroID]
					,[ViajeID]
					)
				VALUES
					(
					@ReservaHabitacionId
					,@HabitacionId
					,@PasajeId
					,@FechaReserva
					,@Desde
					,@Hasta
					,@Expiro
					,@HoraIngreso
					,@HoraSalida
					,@PasajeroId
					,@ViajeId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ReservaHabitacion_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReservaHabitacion_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ReservaHabitacion_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the ReservaHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ReservaHabitacion_Update
(

	@ReservaHabitacionId uniqueidentifier   ,

	@OriginalReservaHabitacionId uniqueidentifier   ,

	@HabitacionId uniqueidentifier   ,

	@PasajeId uniqueidentifier   ,

	@FechaReserva date   ,

	@Desde date   ,

	@Hasta date   ,

	@Expiro bit   ,

	@HoraIngreso varchar (10)  ,

	@HoraSalida varchar (10)  ,

	@PasajeroId uniqueidentifier   ,

	@ViajeId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[ReservaHabitacion]
				SET
					[ReservaHabitacionID] = @ReservaHabitacionId
					,[HabitacionID] = @HabitacionId
					,[PasajeID] = @PasajeId
					,[FechaReserva] = @FechaReserva
					,[Desde] = @Desde
					,[Hasta] = @Hasta
					,[Expiro] = @Expiro
					,[HoraIngreso] = @HoraIngreso
					,[HoraSalida] = @HoraSalida
					,[PasajeroID] = @PasajeroId
					,[ViajeID] = @ViajeId
				WHERE
[ReservaHabitacionID] = @OriginalReservaHabitacionId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ReservaHabitacion_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReservaHabitacion_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ReservaHabitacion_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the ReservaHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ReservaHabitacion_Delete
(

	@ReservaHabitacionId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[ReservaHabitacion] WITH (ROWLOCK) 
				WHERE
					[ReservaHabitacionID] = @ReservaHabitacionId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ReservaHabitacion_GetByPasajeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReservaHabitacion_GetByPasajeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ReservaHabitacion_GetByPasajeId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the ReservaHabitacion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ReservaHabitacion_GetByPasajeId
(

	@PasajeId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ReservaHabitacionID],
					[HabitacionID],
					[PasajeID],
					[FechaReserva],
					[Desde],
					[Hasta],
					[Expiro],
					[HoraIngreso],
					[HoraSalida],
					[PasajeroID],
					[ViajeID]
				FROM
					[dbo].[ReservaHabitacion]
				WHERE
					[PasajeID] = @PasajeId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ReservaHabitacion_GetByPasajeroId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReservaHabitacion_GetByPasajeroId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ReservaHabitacion_GetByPasajeroId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the ReservaHabitacion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ReservaHabitacion_GetByPasajeroId
(

	@PasajeroId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ReservaHabitacionID],
					[HabitacionID],
					[PasajeID],
					[FechaReserva],
					[Desde],
					[Hasta],
					[Expiro],
					[HoraIngreso],
					[HoraSalida],
					[PasajeroID],
					[ViajeID]
				FROM
					[dbo].[ReservaHabitacion]
				WHERE
					[PasajeroID] = @PasajeroId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ReservaHabitacion_GetByHabitacionId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReservaHabitacion_GetByHabitacionId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ReservaHabitacion_GetByHabitacionId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the ReservaHabitacion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ReservaHabitacion_GetByHabitacionId
(

	@HabitacionId uniqueidentifier   
)
AS


				SELECT
					[ReservaHabitacionID],
					[HabitacionID],
					[PasajeID],
					[FechaReserva],
					[Desde],
					[Hasta],
					[Expiro],
					[HoraIngreso],
					[HoraSalida],
					[PasajeroID],
					[ViajeID]
				FROM
					[dbo].[ReservaHabitacion]
				WHERE
					[HabitacionID] = @HabitacionId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ReservaHabitacion_GetByReservaHabitacionId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReservaHabitacion_GetByReservaHabitacionId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ReservaHabitacion_GetByReservaHabitacionId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the ReservaHabitacion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ReservaHabitacion_GetByReservaHabitacionId
(

	@ReservaHabitacionId uniqueidentifier   
)
AS


				SELECT
					[ReservaHabitacionID],
					[HabitacionID],
					[PasajeID],
					[FechaReserva],
					[Desde],
					[Hasta],
					[Expiro],
					[HoraIngreso],
					[HoraSalida],
					[PasajeroID],
					[ViajeID]
				FROM
					[dbo].[ReservaHabitacion]
				WHERE
					[ReservaHabitacionID] = @ReservaHabitacionId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.ReservaHabitacion_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.ReservaHabitacion_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ReservaHabitacion_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the ReservaHabitacion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.ReservaHabitacion_Find
(

	@SearchUsingOR bit   = null ,

	@ReservaHabitacionId uniqueidentifier   = null ,

	@HabitacionId uniqueidentifier   = null ,

	@PasajeId uniqueidentifier   = null ,

	@FechaReserva date   = null ,

	@Desde date   = null ,

	@Hasta date   = null ,

	@Expiro bit   = null ,

	@HoraIngreso varchar (10)  = null ,

	@HoraSalida varchar (10)  = null ,

	@PasajeroId uniqueidentifier   = null ,

	@ViajeId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ReservaHabitacionID]
	, [HabitacionID]
	, [PasajeID]
	, [FechaReserva]
	, [Desde]
	, [Hasta]
	, [Expiro]
	, [HoraIngreso]
	, [HoraSalida]
	, [PasajeroID]
	, [ViajeID]
    FROM
	[dbo].[ReservaHabitacion]
    WHERE 
	 ([ReservaHabitacionID] = @ReservaHabitacionId OR @ReservaHabitacionId IS NULL)
	AND ([HabitacionID] = @HabitacionId OR @HabitacionId IS NULL)
	AND ([PasajeID] = @PasajeId OR @PasajeId IS NULL)
	AND ([FechaReserva] = @FechaReserva OR @FechaReserva IS NULL)
	AND ([Desde] = @Desde OR @Desde IS NULL)
	AND ([Hasta] = @Hasta OR @Hasta IS NULL)
	AND ([Expiro] = @Expiro OR @Expiro IS NULL)
	AND ([HoraIngreso] = @HoraIngreso OR @HoraIngreso IS NULL)
	AND ([HoraSalida] = @HoraSalida OR @HoraSalida IS NULL)
	AND ([PasajeroID] = @PasajeroId OR @PasajeroId IS NULL)
	AND ([ViajeID] = @ViajeId OR @ViajeId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ReservaHabitacionID]
	, [HabitacionID]
	, [PasajeID]
	, [FechaReserva]
	, [Desde]
	, [Hasta]
	, [Expiro]
	, [HoraIngreso]
	, [HoraSalida]
	, [PasajeroID]
	, [ViajeID]
    FROM
	[dbo].[ReservaHabitacion]
    WHERE 
	 ([ReservaHabitacionID] = @ReservaHabitacionId AND @ReservaHabitacionId is not null)
	OR ([HabitacionID] = @HabitacionId AND @HabitacionId is not null)
	OR ([PasajeID] = @PasajeId AND @PasajeId is not null)
	OR ([FechaReserva] = @FechaReserva AND @FechaReserva is not null)
	OR ([Desde] = @Desde AND @Desde is not null)
	OR ([Hasta] = @Hasta AND @Hasta is not null)
	OR ([Expiro] = @Expiro AND @Expiro is not null)
	OR ([HoraIngreso] = @HoraIngreso AND @HoraIngreso is not null)
	OR ([HoraSalida] = @HoraSalida AND @HoraSalida is not null)
	OR ([PasajeroID] = @PasajeroId AND @PasajeroId is not null)
	OR ([ViajeID] = @ViajeId AND @ViajeId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Servicio_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Servicio_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Servicio_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Servicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Servicio_Get_List

AS


				
				SELECT
					[ServicioID],
					[Descripcion],
					[Precio],
					[Moneda],
					[Iva],
					[Alicuota],
					[Validez],
					[VisibilidadTarifa],
					[ProveedorID],
					[TransporteID],
					[HotelID],
					[TipoServicio]
				FROM
					[dbo].[Servicio]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Servicio_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Servicio_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Servicio_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Servicio table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Servicio_GetPaged
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
				    [ServicioID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ServicioID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ServicioID]'
				SET @SQL = @SQL + ' FROM [dbo].[Servicio]'
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
				SELECT O.[ServicioID], O.[Descripcion], O.[Precio], O.[Moneda], O.[Iva], O.[Alicuota], O.[Validez], O.[VisibilidadTarifa], O.[ProveedorID], O.[TransporteID], O.[HotelID], O.[TipoServicio]
				FROM
				    [dbo].[Servicio] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ServicioID] = PageIndex.[ServicioID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Servicio]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Servicio_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Servicio_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Servicio_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Servicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Servicio_Insert
(

	@ServicioId uniqueidentifier    OUTPUT,

	@Descripcion varchar (100)  ,

	@Precio float   ,

	@Moneda varchar (50)  ,

	@Iva varchar (50)  ,

	@Alicuota float   ,

	@Validez date   ,

	@VisibilidadTarifa int   ,

	@ProveedorId uniqueidentifier   ,

	@TransporteId uniqueidentifier   ,

	@HotelId uniqueidentifier   ,

	@TipoServicio int   
)
AS


				
				INSERT INTO [dbo].[Servicio]
					(
					[ServicioID]
					,[Descripcion]
					,[Precio]
					,[Moneda]
					,[Iva]
					,[Alicuota]
					,[Validez]
					,[VisibilidadTarifa]
					,[ProveedorID]
					,[TransporteID]
					,[HotelID]
					,[TipoServicio]
					)
				VALUES
					(
					@ServicioId
					,@Descripcion
					,@Precio
					,@Moneda
					,@Iva
					,@Alicuota
					,@Validez
					,@VisibilidadTarifa
					,@ProveedorId
					,@TransporteId
					,@HotelId
					,@TipoServicio
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Servicio_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Servicio_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Servicio_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Servicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Servicio_Update
(

	@ServicioId uniqueidentifier   ,

	@OriginalServicioId uniqueidentifier   ,

	@Descripcion varchar (100)  ,

	@Precio float   ,

	@Moneda varchar (50)  ,

	@Iva varchar (50)  ,

	@Alicuota float   ,

	@Validez date   ,

	@VisibilidadTarifa int   ,

	@ProveedorId uniqueidentifier   ,

	@TransporteId uniqueidentifier   ,

	@HotelId uniqueidentifier   ,

	@TipoServicio int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Servicio]
				SET
					[ServicioID] = @ServicioId
					,[Descripcion] = @Descripcion
					,[Precio] = @Precio
					,[Moneda] = @Moneda
					,[Iva] = @Iva
					,[Alicuota] = @Alicuota
					,[Validez] = @Validez
					,[VisibilidadTarifa] = @VisibilidadTarifa
					,[ProveedorID] = @ProveedorId
					,[TransporteID] = @TransporteId
					,[HotelID] = @HotelId
					,[TipoServicio] = @TipoServicio
				WHERE
[ServicioID] = @OriginalServicioId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Servicio_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Servicio_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Servicio_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Servicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Servicio_Delete
(

	@ServicioId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Servicio] WITH (ROWLOCK) 
				WHERE
					[ServicioID] = @ServicioId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Servicio_GetByHotelId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Servicio_GetByHotelId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Servicio_GetByHotelId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Servicio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Servicio_GetByHotelId
(

	@HotelId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ServicioID],
					[Descripcion],
					[Precio],
					[Moneda],
					[Iva],
					[Alicuota],
					[Validez],
					[VisibilidadTarifa],
					[ProveedorID],
					[TransporteID],
					[HotelID],
					[TipoServicio]
				FROM
					[dbo].[Servicio]
				WHERE
					[HotelID] = @HotelId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Servicio_GetByProveedorId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Servicio_GetByProveedorId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Servicio_GetByProveedorId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Servicio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Servicio_GetByProveedorId
(

	@ProveedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ServicioID],
					[Descripcion],
					[Precio],
					[Moneda],
					[Iva],
					[Alicuota],
					[Validez],
					[VisibilidadTarifa],
					[ProveedorID],
					[TransporteID],
					[HotelID],
					[TipoServicio]
				FROM
					[dbo].[Servicio]
				WHERE
					[ProveedorID] = @ProveedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Servicio_GetByTransporteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Servicio_GetByTransporteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Servicio_GetByTransporteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Servicio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Servicio_GetByTransporteId
(

	@TransporteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ServicioID],
					[Descripcion],
					[Precio],
					[Moneda],
					[Iva],
					[Alicuota],
					[Validez],
					[VisibilidadTarifa],
					[ProveedorID],
					[TransporteID],
					[HotelID],
					[TipoServicio]
				FROM
					[dbo].[Servicio]
				WHERE
					[TransporteID] = @TransporteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Servicio_GetByServicioId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Servicio_GetByServicioId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Servicio_GetByServicioId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Servicio table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Servicio_GetByServicioId
(

	@ServicioId uniqueidentifier   
)
AS


				SELECT
					[ServicioID],
					[Descripcion],
					[Precio],
					[Moneda],
					[Iva],
					[Alicuota],
					[Validez],
					[VisibilidadTarifa],
					[ProveedorID],
					[TransporteID],
					[HotelID],
					[TipoServicio]
				FROM
					[dbo].[Servicio]
				WHERE
					[ServicioID] = @ServicioId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Servicio_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Servicio_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Servicio_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Servicio table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Servicio_Find
(

	@SearchUsingOR bit   = null ,

	@ServicioId uniqueidentifier   = null ,

	@Descripcion varchar (100)  = null ,

	@Precio float   = null ,

	@Moneda varchar (50)  = null ,

	@Iva varchar (50)  = null ,

	@Alicuota float   = null ,

	@Validez date   = null ,

	@VisibilidadTarifa int   = null ,

	@ProveedorId uniqueidentifier   = null ,

	@TransporteId uniqueidentifier   = null ,

	@HotelId uniqueidentifier   = null ,

	@TipoServicio int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ServicioID]
	, [Descripcion]
	, [Precio]
	, [Moneda]
	, [Iva]
	, [Alicuota]
	, [Validez]
	, [VisibilidadTarifa]
	, [ProveedorID]
	, [TransporteID]
	, [HotelID]
	, [TipoServicio]
    FROM
	[dbo].[Servicio]
    WHERE 
	 ([ServicioID] = @ServicioId OR @ServicioId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
	AND ([Precio] = @Precio OR @Precio IS NULL)
	AND ([Moneda] = @Moneda OR @Moneda IS NULL)
	AND ([Iva] = @Iva OR @Iva IS NULL)
	AND ([Alicuota] = @Alicuota OR @Alicuota IS NULL)
	AND ([Validez] = @Validez OR @Validez IS NULL)
	AND ([VisibilidadTarifa] = @VisibilidadTarifa OR @VisibilidadTarifa IS NULL)
	AND ([ProveedorID] = @ProveedorId OR @ProveedorId IS NULL)
	AND ([TransporteID] = @TransporteId OR @TransporteId IS NULL)
	AND ([HotelID] = @HotelId OR @HotelId IS NULL)
	AND ([TipoServicio] = @TipoServicio OR @TipoServicio IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ServicioID]
	, [Descripcion]
	, [Precio]
	, [Moneda]
	, [Iva]
	, [Alicuota]
	, [Validez]
	, [VisibilidadTarifa]
	, [ProveedorID]
	, [TransporteID]
	, [HotelID]
	, [TipoServicio]
    FROM
	[dbo].[Servicio]
    WHERE 
	 ([ServicioID] = @ServicioId AND @ServicioId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	OR ([Precio] = @Precio AND @Precio is not null)
	OR ([Moneda] = @Moneda AND @Moneda is not null)
	OR ([Iva] = @Iva AND @Iva is not null)
	OR ([Alicuota] = @Alicuota AND @Alicuota is not null)
	OR ([Validez] = @Validez AND @Validez is not null)
	OR ([VisibilidadTarifa] = @VisibilidadTarifa AND @VisibilidadTarifa is not null)
	OR ([ProveedorID] = @ProveedorId AND @ProveedorId is not null)
	OR ([TransporteID] = @TransporteId AND @TransporteId is not null)
	OR ([HotelID] = @HotelId AND @HotelId is not null)
	OR ([TipoServicio] = @TipoServicio AND @TipoServicio is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Vendedor_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Vendedor_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Vendedor_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Vendedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Vendedor_Get_List

AS


				
				SELECT
					[VendedorID],
					[Descripcion]
				FROM
					[dbo].[Vendedor]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Vendedor_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Vendedor_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Vendedor_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Vendedor table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Vendedor_GetPaged
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
				    [VendedorID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([VendedorID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [VendedorID]'
				SET @SQL = @SQL + ' FROM [dbo].[Vendedor]'
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
				SELECT O.[VendedorID], O.[Descripcion]
				FROM
				    [dbo].[Vendedor] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[VendedorID] = PageIndex.[VendedorID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Vendedor]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Vendedor_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Vendedor_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Vendedor_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Vendedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Vendedor_Insert
(

	@VendedorId uniqueidentifier   ,

	@Descripcion varchar (100)  
)
AS


				
				INSERT INTO [dbo].[Vendedor]
					(
					[VendedorID]
					,[Descripcion]
					)
				VALUES
					(
					@VendedorId
					,@Descripcion
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Vendedor_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Vendedor_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Vendedor_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Vendedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Vendedor_Update
(

	@VendedorId uniqueidentifier   ,

	@OriginalVendedorId uniqueidentifier   ,

	@Descripcion varchar (100)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Vendedor]
				SET
					[VendedorID] = @VendedorId
					,[Descripcion] = @Descripcion
				WHERE
[VendedorID] = @OriginalVendedorId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Vendedor_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Vendedor_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Vendedor_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Vendedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Vendedor_Delete
(

	@VendedorId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Vendedor] WITH (ROWLOCK) 
				WHERE
					[VendedorID] = @VendedorId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Vendedor_GetByVendedorId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Vendedor_GetByVendedorId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Vendedor_GetByVendedorId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Vendedor table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Vendedor_GetByVendedorId
(

	@VendedorId uniqueidentifier   
)
AS


				SELECT
					[VendedorID],
					[Descripcion]
				FROM
					[dbo].[Vendedor]
				WHERE
					[VendedorID] = @VendedorId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Vendedor_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Vendedor_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Vendedor_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Vendedor table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Vendedor_Find
(

	@SearchUsingOR bit   = null ,

	@VendedorId uniqueidentifier   = null ,

	@Descripcion varchar (100)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [VendedorID]
	, [Descripcion]
    FROM
	[dbo].[Vendedor]
    WHERE 
	 ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [VendedorID]
	, [Descripcion]
    FROM
	[dbo].[Vendedor]
    WHERE 
	 ([VendedorID] = @VendedorId AND @VendedorId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Viaje_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Viaje_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Viaje_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Viaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Viaje_Get_List

AS


				
				SELECT
					[ViajeID],
					[PaqueteID],
					[Origen],
					[FechaSalida],
					[HoraSalida],
					[PaisOrigen],
					[PaisDestino],
					[Paso],
					[Medio],
					[BusID],
					[FechaRegreso],
					[HoraRegreso],
					[Descripcion],
					[PrecioSemicama],
					[PrecioCama],
					[PrecioPromocional],
					[FechaPromocion],
					[nDias],
					[nNoches]
				FROM
					[dbo].[Viaje]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Viaje_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Viaje_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Viaje_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Viaje table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Viaje_GetPaged
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
				    [ViajeID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ViajeID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ViajeID]'
				SET @SQL = @SQL + ' FROM [dbo].[Viaje]'
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
				SELECT O.[ViajeID], O.[PaqueteID], O.[Origen], O.[FechaSalida], O.[HoraSalida], O.[PaisOrigen], O.[PaisDestino], O.[Paso], O.[Medio], O.[BusID], O.[FechaRegreso], O.[HoraRegreso], O.[Descripcion], O.[PrecioSemicama], O.[PrecioCama], O.[PrecioPromocional], O.[FechaPromocion], O.[nDias], O.[nNoches]
				FROM
				    [dbo].[Viaje] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ViajeID] = PageIndex.[ViajeID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Viaje]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Viaje_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Viaje_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Viaje_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Viaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Viaje_Insert
(

	@ViajeId uniqueidentifier    OUTPUT,

	@PaqueteId uniqueidentifier   ,

	@Origen varchar (50)  ,

	@FechaSalida date   ,

	@HoraSalida varchar (50)  ,

	@PaisOrigen varchar (50)  ,

	@PaisDestino varchar (50)  ,

	@Paso varchar (50)  ,

	@Medio varchar (50)  ,

	@BusId uniqueidentifier   ,

	@FechaRegreso date   ,

	@HoraRegreso varchar (50)  ,

	@Descripcion varchar (200)  ,

	@PrecioSemicama float   ,

	@PrecioCama float   ,

	@PrecioPromocional float   ,

	@FechaPromocion datetime   ,

	@NDias int   ,

	@NNoches int   
)
AS


				
				INSERT INTO [dbo].[Viaje]
					(
					[ViajeID]
					,[PaqueteID]
					,[Origen]
					,[FechaSalida]
					,[HoraSalida]
					,[PaisOrigen]
					,[PaisDestino]
					,[Paso]
					,[Medio]
					,[BusID]
					,[FechaRegreso]
					,[HoraRegreso]
					,[Descripcion]
					,[PrecioSemicama]
					,[PrecioCama]
					,[PrecioPromocional]
					,[FechaPromocion]
					,[nDias]
					,[nNoches]
					)
				VALUES
					(
					@ViajeId
					,@PaqueteId
					,@Origen
					,@FechaSalida
					,@HoraSalida
					,@PaisOrigen
					,@PaisDestino
					,@Paso
					,@Medio
					,@BusId
					,@FechaRegreso
					,@HoraRegreso
					,@Descripcion
					,@PrecioSemicama
					,@PrecioCama
					,@PrecioPromocional
					,@FechaPromocion
					,@NDias
					,@NNoches
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Viaje_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Viaje_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Viaje_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Viaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Viaje_Update
(

	@ViajeId uniqueidentifier   ,

	@OriginalViajeId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   ,

	@Origen varchar (50)  ,

	@FechaSalida date   ,

	@HoraSalida varchar (50)  ,

	@PaisOrigen varchar (50)  ,

	@PaisDestino varchar (50)  ,

	@Paso varchar (50)  ,

	@Medio varchar (50)  ,

	@BusId uniqueidentifier   ,

	@FechaRegreso date   ,

	@HoraRegreso varchar (50)  ,

	@Descripcion varchar (200)  ,

	@PrecioSemicama float   ,

	@PrecioCama float   ,

	@PrecioPromocional float   ,

	@FechaPromocion datetime   ,

	@NDias int   ,

	@NNoches int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Viaje]
				SET
					[ViajeID] = @ViajeId
					,[PaqueteID] = @PaqueteId
					,[Origen] = @Origen
					,[FechaSalida] = @FechaSalida
					,[HoraSalida] = @HoraSalida
					,[PaisOrigen] = @PaisOrigen
					,[PaisDestino] = @PaisDestino
					,[Paso] = @Paso
					,[Medio] = @Medio
					,[BusID] = @BusId
					,[FechaRegreso] = @FechaRegreso
					,[HoraRegreso] = @HoraRegreso
					,[Descripcion] = @Descripcion
					,[PrecioSemicama] = @PrecioSemicama
					,[PrecioCama] = @PrecioCama
					,[PrecioPromocional] = @PrecioPromocional
					,[FechaPromocion] = @FechaPromocion
					,[nDias] = @NDias
					,[nNoches] = @NNoches
				WHERE
[ViajeID] = @OriginalViajeId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Viaje_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Viaje_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Viaje_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Viaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Viaje_Delete
(

	@ViajeId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Viaje] WITH (ROWLOCK) 
				WHERE
					[ViajeID] = @ViajeId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Viaje_GetByPaqueteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Viaje_GetByPaqueteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Viaje_GetByPaqueteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Viaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Viaje_GetByPaqueteId
(

	@PaqueteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ViajeID],
					[PaqueteID],
					[Origen],
					[FechaSalida],
					[HoraSalida],
					[PaisOrigen],
					[PaisDestino],
					[Paso],
					[Medio],
					[BusID],
					[FechaRegreso],
					[HoraRegreso],
					[Descripcion],
					[PrecioSemicama],
					[PrecioCama],
					[PrecioPromocional],
					[FechaPromocion],
					[nDias],
					[nNoches]
				FROM
					[dbo].[Viaje]
				WHERE
					[PaqueteID] = @PaqueteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Viaje_GetByBusId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Viaje_GetByBusId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Viaje_GetByBusId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Viaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Viaje_GetByBusId
(

	@BusId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ViajeID],
					[PaqueteID],
					[Origen],
					[FechaSalida],
					[HoraSalida],
					[PaisOrigen],
					[PaisDestino],
					[Paso],
					[Medio],
					[BusID],
					[FechaRegreso],
					[HoraRegreso],
					[Descripcion],
					[PrecioSemicama],
					[PrecioCama],
					[PrecioPromocional],
					[FechaPromocion],
					[nDias],
					[nNoches]
				FROM
					[dbo].[Viaje]
				WHERE
					[BusID] = @BusId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Viaje_GetByViajeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Viaje_GetByViajeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Viaje_GetByViajeId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Viaje table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Viaje_GetByViajeId
(

	@ViajeId uniqueidentifier   
)
AS


				SELECT
					[ViajeID],
					[PaqueteID],
					[Origen],
					[FechaSalida],
					[HoraSalida],
					[PaisOrigen],
					[PaisDestino],
					[Paso],
					[Medio],
					[BusID],
					[FechaRegreso],
					[HoraRegreso],
					[Descripcion],
					[PrecioSemicama],
					[PrecioCama],
					[PrecioPromocional],
					[FechaPromocion],
					[nDias],
					[nNoches]
				FROM
					[dbo].[Viaje]
				WHERE
					[ViajeID] = @ViajeId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Viaje_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Viaje_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Viaje_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Viaje table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Viaje_Find
(

	@SearchUsingOR bit   = null ,

	@ViajeId uniqueidentifier   = null ,

	@PaqueteId uniqueidentifier   = null ,

	@Origen varchar (50)  = null ,

	@FechaSalida date   = null ,

	@HoraSalida varchar (50)  = null ,

	@PaisOrigen varchar (50)  = null ,

	@PaisDestino varchar (50)  = null ,

	@Paso varchar (50)  = null ,

	@Medio varchar (50)  = null ,

	@BusId uniqueidentifier   = null ,

	@FechaRegreso date   = null ,

	@HoraRegreso varchar (50)  = null ,

	@Descripcion varchar (200)  = null ,

	@PrecioSemicama float   = null ,

	@PrecioCama float   = null ,

	@PrecioPromocional float   = null ,

	@FechaPromocion datetime   = null ,

	@NDias int   = null ,

	@NNoches int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ViajeID]
	, [PaqueteID]
	, [Origen]
	, [FechaSalida]
	, [HoraSalida]
	, [PaisOrigen]
	, [PaisDestino]
	, [Paso]
	, [Medio]
	, [BusID]
	, [FechaRegreso]
	, [HoraRegreso]
	, [Descripcion]
	, [PrecioSemicama]
	, [PrecioCama]
	, [PrecioPromocional]
	, [FechaPromocion]
	, [nDias]
	, [nNoches]
    FROM
	[dbo].[Viaje]
    WHERE 
	 ([ViajeID] = @ViajeId OR @ViajeId IS NULL)
	AND ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
	AND ([Origen] = @Origen OR @Origen IS NULL)
	AND ([FechaSalida] = @FechaSalida OR @FechaSalida IS NULL)
	AND ([HoraSalida] = @HoraSalida OR @HoraSalida IS NULL)
	AND ([PaisOrigen] = @PaisOrigen OR @PaisOrigen IS NULL)
	AND ([PaisDestino] = @PaisDestino OR @PaisDestino IS NULL)
	AND ([Paso] = @Paso OR @Paso IS NULL)
	AND ([Medio] = @Medio OR @Medio IS NULL)
	AND ([BusID] = @BusId OR @BusId IS NULL)
	AND ([FechaRegreso] = @FechaRegreso OR @FechaRegreso IS NULL)
	AND ([HoraRegreso] = @HoraRegreso OR @HoraRegreso IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
	AND ([PrecioSemicama] = @PrecioSemicama OR @PrecioSemicama IS NULL)
	AND ([PrecioCama] = @PrecioCama OR @PrecioCama IS NULL)
	AND ([PrecioPromocional] = @PrecioPromocional OR @PrecioPromocional IS NULL)
	AND ([FechaPromocion] = @FechaPromocion OR @FechaPromocion IS NULL)
	AND ([nDias] = @NDias OR @NDias IS NULL)
	AND ([nNoches] = @NNoches OR @NNoches IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ViajeID]
	, [PaqueteID]
	, [Origen]
	, [FechaSalida]
	, [HoraSalida]
	, [PaisOrigen]
	, [PaisDestino]
	, [Paso]
	, [Medio]
	, [BusID]
	, [FechaRegreso]
	, [HoraRegreso]
	, [Descripcion]
	, [PrecioSemicama]
	, [PrecioCama]
	, [PrecioPromocional]
	, [FechaPromocion]
	, [nDias]
	, [nNoches]
    FROM
	[dbo].[Viaje]
    WHERE 
	 ([ViajeID] = @ViajeId AND @ViajeId is not null)
	OR ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	OR ([Origen] = @Origen AND @Origen is not null)
	OR ([FechaSalida] = @FechaSalida AND @FechaSalida is not null)
	OR ([HoraSalida] = @HoraSalida AND @HoraSalida is not null)
	OR ([PaisOrigen] = @PaisOrigen AND @PaisOrigen is not null)
	OR ([PaisDestino] = @PaisDestino AND @PaisDestino is not null)
	OR ([Paso] = @Paso AND @Paso is not null)
	OR ([Medio] = @Medio AND @Medio is not null)
	OR ([BusID] = @BusId AND @BusId is not null)
	OR ([FechaRegreso] = @FechaRegreso AND @FechaRegreso is not null)
	OR ([HoraRegreso] = @HoraRegreso AND @HoraRegreso is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	OR ([PrecioSemicama] = @PrecioSemicama AND @PrecioSemicama is not null)
	OR ([PrecioCama] = @PrecioCama AND @PrecioCama is not null)
	OR ([PrecioPromocional] = @PrecioPromocional AND @PrecioPromocional is not null)
	OR ([FechaPromocion] = @FechaPromocion AND @FechaPromocion is not null)
	OR ([nDias] = @NDias AND @NDias is not null)
	OR ([nNoches] = @NNoches AND @NNoches is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Pasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_Get_List

AS


				
				SELECT
					[PasajeID],
					[PasajeroID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[VoucherID],
					[PrecioID]
				FROM
					[dbo].[Pasaje]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Pasaje table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_GetPaged
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
				    [PasajeID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PasajeID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PasajeID]'
				SET @SQL = @SQL + ' FROM [dbo].[Pasaje]'
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
				SELECT O.[PasajeID], O.[PasajeroID], O.[ButacaID], O.[FechaReserva], O.[FechaCompra], O.[ViajeID], O.[FacturaID], O.[EstadoPasaje], O.[VoucherID], O.[PrecioID]
				FROM
				    [dbo].[Pasaje] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PasajeID] = PageIndex.[PasajeID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Pasaje]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Pasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_Insert
(

	@PasajeId uniqueidentifier    OUTPUT,

	@PasajeroId uniqueidentifier   ,

	@ButacaId uniqueidentifier   ,

	@FechaReserva date   ,

	@FechaCompra date   ,

	@ViajeId uniqueidentifier   ,

	@FacturaId uniqueidentifier   ,

	@EstadoPasaje int   ,

	@VoucherId uniqueidentifier   ,

	@PrecioId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[Pasaje]
					(
					[PasajeID]
					,[PasajeroID]
					,[ButacaID]
					,[FechaReserva]
					,[FechaCompra]
					,[ViajeID]
					,[FacturaID]
					,[EstadoPasaje]
					,[VoucherID]
					,[PrecioID]
					)
				VALUES
					(
					@PasajeId
					,@PasajeroId
					,@ButacaId
					,@FechaReserva
					,@FechaCompra
					,@ViajeId
					,@FacturaId
					,@EstadoPasaje
					,@VoucherId
					,@PrecioId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Pasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_Update
(

	@PasajeId uniqueidentifier   ,

	@OriginalPasajeId uniqueidentifier   ,

	@PasajeroId uniqueidentifier   ,

	@ButacaId uniqueidentifier   ,

	@FechaReserva date   ,

	@FechaCompra date   ,

	@ViajeId uniqueidentifier   ,

	@FacturaId uniqueidentifier   ,

	@EstadoPasaje int   ,

	@VoucherId uniqueidentifier   ,

	@PrecioId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Pasaje]
				SET
					[PasajeID] = @PasajeId
					,[PasajeroID] = @PasajeroId
					,[ButacaID] = @ButacaId
					,[FechaReserva] = @FechaReserva
					,[FechaCompra] = @FechaCompra
					,[ViajeID] = @ViajeId
					,[FacturaID] = @FacturaId
					,[EstadoPasaje] = @EstadoPasaje
					,[VoucherID] = @VoucherId
					,[PrecioID] = @PrecioId
				WHERE
[PasajeID] = @OriginalPasajeId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Pasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_Delete
(

	@PasajeId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Pasaje] WITH (ROWLOCK) 
				WHERE
					[PasajeID] = @PasajeId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_GetByButacaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_GetByButacaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_GetByButacaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pasaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_GetByButacaId
(

	@ButacaId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeID],
					[PasajeroID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[VoucherID],
					[PrecioID]
				FROM
					[dbo].[Pasaje]
				WHERE
					[ButacaID] = @ButacaId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_GetByPasajeroId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_GetByPasajeroId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_GetByPasajeroId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pasaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_GetByPasajeroId
(

	@PasajeroId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeID],
					[PasajeroID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[VoucherID],
					[PrecioID]
				FROM
					[dbo].[Pasaje]
				WHERE
					[PasajeroID] = @PasajeroId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_GetByPrecioId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_GetByPrecioId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_GetByPrecioId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pasaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_GetByPrecioId
(

	@PrecioId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeID],
					[PasajeroID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[VoucherID],
					[PrecioID]
				FROM
					[dbo].[Pasaje]
				WHERE
					[PrecioID] = @PrecioId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_GetByViajeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_GetByViajeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_GetByViajeId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pasaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_GetByViajeId
(

	@ViajeId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeID],
					[PasajeroID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[VoucherID],
					[PrecioID]
				FROM
					[dbo].[Pasaje]
				WHERE
					[ViajeID] = @ViajeId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_GetByVoucherId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_GetByVoucherId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_GetByVoucherId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pasaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_GetByVoucherId
(

	@VoucherId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeID],
					[PasajeroID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[VoucherID],
					[PrecioID]
				FROM
					[dbo].[Pasaje]
				WHERE
					[VoucherID] = @VoucherId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_GetByEstadoPasaje procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_GetByEstadoPasaje') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_GetByEstadoPasaje
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pasaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_GetByEstadoPasaje
(

	@EstadoPasaje int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeID],
					[PasajeroID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[VoucherID],
					[PrecioID]
				FROM
					[dbo].[Pasaje]
				WHERE
					[EstadoPasaje] = @EstadoPasaje
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_GetByFacturaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_GetByFacturaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_GetByFacturaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pasaje table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_GetByFacturaId
(

	@FacturaId uniqueidentifier   
)
AS


				SELECT
					[PasajeID],
					[PasajeroID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[VoucherID],
					[PrecioID]
				FROM
					[dbo].[Pasaje]
				WHERE
					[FacturaID] = @FacturaId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_GetByPasajeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_GetByPasajeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_GetByPasajeId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pasaje table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_GetByPasajeId
(

	@PasajeId uniqueidentifier   
)
AS


				SELECT
					[PasajeID],
					[PasajeroID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[VoucherID],
					[PrecioID]
				FROM
					[dbo].[Pasaje]
				WHERE
					[PasajeID] = @PasajeId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pasaje_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pasaje_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pasaje_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Pasaje table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pasaje_Find
(

	@SearchUsingOR bit   = null ,

	@PasajeId uniqueidentifier   = null ,

	@PasajeroId uniqueidentifier   = null ,

	@ButacaId uniqueidentifier   = null ,

	@FechaReserva date   = null ,

	@FechaCompra date   = null ,

	@ViajeId uniqueidentifier   = null ,

	@FacturaId uniqueidentifier   = null ,

	@EstadoPasaje int   = null ,

	@VoucherId uniqueidentifier   = null ,

	@PrecioId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PasajeID]
	, [PasajeroID]
	, [ButacaID]
	, [FechaReserva]
	, [FechaCompra]
	, [ViajeID]
	, [FacturaID]
	, [EstadoPasaje]
	, [VoucherID]
	, [PrecioID]
    FROM
	[dbo].[Pasaje]
    WHERE 
	 ([PasajeID] = @PasajeId OR @PasajeId IS NULL)
	AND ([PasajeroID] = @PasajeroId OR @PasajeroId IS NULL)
	AND ([ButacaID] = @ButacaId OR @ButacaId IS NULL)
	AND ([FechaReserva] = @FechaReserva OR @FechaReserva IS NULL)
	AND ([FechaCompra] = @FechaCompra OR @FechaCompra IS NULL)
	AND ([ViajeID] = @ViajeId OR @ViajeId IS NULL)
	AND ([FacturaID] = @FacturaId OR @FacturaId IS NULL)
	AND ([EstadoPasaje] = @EstadoPasaje OR @EstadoPasaje IS NULL)
	AND ([VoucherID] = @VoucherId OR @VoucherId IS NULL)
	AND ([PrecioID] = @PrecioId OR @PrecioId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PasajeID]
	, [PasajeroID]
	, [ButacaID]
	, [FechaReserva]
	, [FechaCompra]
	, [ViajeID]
	, [FacturaID]
	, [EstadoPasaje]
	, [VoucherID]
	, [PrecioID]
    FROM
	[dbo].[Pasaje]
    WHERE 
	 ([PasajeID] = @PasajeId AND @PasajeId is not null)
	OR ([PasajeroID] = @PasajeroId AND @PasajeroId is not null)
	OR ([ButacaID] = @ButacaId AND @ButacaId is not null)
	OR ([FechaReserva] = @FechaReserva AND @FechaReserva is not null)
	OR ([FechaCompra] = @FechaCompra AND @FechaCompra is not null)
	OR ([ViajeID] = @ViajeId AND @ViajeId is not null)
	OR ([FacturaID] = @FacturaId AND @FacturaId is not null)
	OR ([EstadoPasaje] = @EstadoPasaje AND @EstadoPasaje is not null)
	OR ([VoucherID] = @VoucherId AND @VoucherId is not null)
	OR ([PrecioID] = @PrecioId AND @PrecioId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaquetePrecio_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaquetePrecio_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaquetePrecio_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PaquetePrecio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaquetePrecio_Get_List

AS


				
				SELECT
					[PaquetePrecioID],
					[PaqueteID],
					[PrecioID]
				FROM
					[dbo].[PaquetePrecio]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaquetePrecio_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaquetePrecio_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaquetePrecio_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PaquetePrecio table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaquetePrecio_GetPaged
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
				    [PaquetePrecioID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PaquetePrecioID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PaquetePrecioID]'
				SET @SQL = @SQL + ' FROM [dbo].[PaquetePrecio]'
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
				SELECT O.[PaquetePrecioID], O.[PaqueteID], O.[PrecioID]
				FROM
				    [dbo].[PaquetePrecio] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PaquetePrecioID] = PageIndex.[PaquetePrecioID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PaquetePrecio]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaquetePrecio_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaquetePrecio_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaquetePrecio_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the PaquetePrecio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaquetePrecio_Insert
(

	@PaquetePrecioId uniqueidentifier    OUTPUT,

	@PaqueteId uniqueidentifier   ,

	@PrecioId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PaquetePrecio]
					(
					[PaquetePrecioID]
					,[PaqueteID]
					,[PrecioID]
					)
				VALUES
					(
					@PaquetePrecioId
					,@PaqueteId
					,@PrecioId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaquetePrecio_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaquetePrecio_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaquetePrecio_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the PaquetePrecio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaquetePrecio_Update
(

	@PaquetePrecioId uniqueidentifier   ,

	@OriginalPaquetePrecioId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   ,

	@PrecioId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PaquetePrecio]
				SET
					[PaquetePrecioID] = @PaquetePrecioId
					,[PaqueteID] = @PaqueteId
					,[PrecioID] = @PrecioId
				WHERE
[PaquetePrecioID] = @OriginalPaquetePrecioId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaquetePrecio_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaquetePrecio_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaquetePrecio_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the PaquetePrecio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaquetePrecio_Delete
(

	@PaquetePrecioId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PaquetePrecio] WITH (ROWLOCK) 
				WHERE
					[PaquetePrecioID] = @PaquetePrecioId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaquetePrecio_GetByPaqueteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaquetePrecio_GetByPaqueteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaquetePrecio_GetByPaqueteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaquetePrecio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaquetePrecio_GetByPaqueteId
(

	@PaqueteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaquetePrecioID],
					[PaqueteID],
					[PrecioID]
				FROM
					[dbo].[PaquetePrecio]
				WHERE
					[PaqueteID] = @PaqueteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaquetePrecio_GetByPrecioId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaquetePrecio_GetByPrecioId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaquetePrecio_GetByPrecioId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaquetePrecio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaquetePrecio_GetByPrecioId
(

	@PrecioId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaquetePrecioID],
					[PaqueteID],
					[PrecioID]
				FROM
					[dbo].[PaquetePrecio]
				WHERE
					[PrecioID] = @PrecioId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaquetePrecio_GetByPaquetePrecioId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaquetePrecio_GetByPaquetePrecioId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaquetePrecio_GetByPaquetePrecioId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaquetePrecio table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaquetePrecio_GetByPaquetePrecioId
(

	@PaquetePrecioId uniqueidentifier   
)
AS


				SELECT
					[PaquetePrecioID],
					[PaqueteID],
					[PrecioID]
				FROM
					[dbo].[PaquetePrecio]
				WHERE
					[PaquetePrecioID] = @PaquetePrecioId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaquetePrecio_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaquetePrecio_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaquetePrecio_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the PaquetePrecio table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaquetePrecio_Find
(

	@SearchUsingOR bit   = null ,

	@PaquetePrecioId uniqueidentifier   = null ,

	@PaqueteId uniqueidentifier   = null ,

	@PrecioId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaquetePrecioID]
	, [PaqueteID]
	, [PrecioID]
    FROM
	[dbo].[PaquetePrecio]
    WHERE 
	 ([PaquetePrecioID] = @PaquetePrecioId OR @PaquetePrecioId IS NULL)
	AND ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
	AND ([PrecioID] = @PrecioId OR @PrecioId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaquetePrecioID]
	, [PaqueteID]
	, [PrecioID]
    FROM
	[dbo].[PaquetePrecio]
    WHERE 
	 ([PaquetePrecioID] = @PaquetePrecioId AND @PaquetePrecioId is not null)
	OR ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	OR ([PrecioID] = @PrecioId AND @PrecioId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioHabitacion_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioHabitacion_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioHabitacion_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PrecioHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioHabitacion_Get_List

AS


				
				SELECT
					[PrecioHabitacionID],
					[TipoHabitacion],
					[HotelID],
					[FechaRegistro],
					[Activo],
					[Precio]
				FROM
					[dbo].[PrecioHabitacion]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioHabitacion_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioHabitacion_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioHabitacion_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PrecioHabitacion table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioHabitacion_GetPaged
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
				    [PrecioHabitacionID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PrecioHabitacionID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PrecioHabitacionID]'
				SET @SQL = @SQL + ' FROM [dbo].[PrecioHabitacion]'
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
				SELECT O.[PrecioHabitacionID], O.[TipoHabitacion], O.[HotelID], O.[FechaRegistro], O.[Activo], O.[Precio]
				FROM
				    [dbo].[PrecioHabitacion] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PrecioHabitacionID] = PageIndex.[PrecioHabitacionID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PrecioHabitacion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioHabitacion_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioHabitacion_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioHabitacion_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the PrecioHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioHabitacion_Insert
(

	@PrecioHabitacionId uniqueidentifier   ,

	@TipoHabitacion int   ,

	@HotelId uniqueidentifier   ,

	@FechaRegistro date   ,

	@Activo bit   ,

	@Precio float   
)
AS


				
				INSERT INTO [dbo].[PrecioHabitacion]
					(
					[PrecioHabitacionID]
					,[TipoHabitacion]
					,[HotelID]
					,[FechaRegistro]
					,[Activo]
					,[Precio]
					)
				VALUES
					(
					@PrecioHabitacionId
					,@TipoHabitacion
					,@HotelId
					,@FechaRegistro
					,@Activo
					,@Precio
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioHabitacion_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioHabitacion_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioHabitacion_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the PrecioHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioHabitacion_Update
(

	@PrecioHabitacionId uniqueidentifier   ,

	@OriginalPrecioHabitacionId uniqueidentifier   ,

	@TipoHabitacion int   ,

	@HotelId uniqueidentifier   ,

	@FechaRegistro date   ,

	@Activo bit   ,

	@Precio float   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PrecioHabitacion]
				SET
					[PrecioHabitacionID] = @PrecioHabitacionId
					,[TipoHabitacion] = @TipoHabitacion
					,[HotelID] = @HotelId
					,[FechaRegistro] = @FechaRegistro
					,[Activo] = @Activo
					,[Precio] = @Precio
				WHERE
[PrecioHabitacionID] = @OriginalPrecioHabitacionId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioHabitacion_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioHabitacion_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioHabitacion_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the PrecioHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioHabitacion_Delete
(

	@PrecioHabitacionId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PrecioHabitacion] WITH (ROWLOCK) 
				WHERE
					[PrecioHabitacionID] = @PrecioHabitacionId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioHabitacion_GetByPrecioHabitacionId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioHabitacion_GetByPrecioHabitacionId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioHabitacion_GetByPrecioHabitacionId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PrecioHabitacion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioHabitacion_GetByPrecioHabitacionId
(

	@PrecioHabitacionId uniqueidentifier   
)
AS


				SELECT
					[PrecioHabitacionID],
					[TipoHabitacion],
					[HotelID],
					[FechaRegistro],
					[Activo],
					[Precio]
				FROM
					[dbo].[PrecioHabitacion]
				WHERE
					[PrecioHabitacionID] = @PrecioHabitacionId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PrecioHabitacion_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PrecioHabitacion_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PrecioHabitacion_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the PrecioHabitacion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PrecioHabitacion_Find
(

	@SearchUsingOR bit   = null ,

	@PrecioHabitacionId uniqueidentifier   = null ,

	@TipoHabitacion int   = null ,

	@HotelId uniqueidentifier   = null ,

	@FechaRegistro date   = null ,

	@Activo bit   = null ,

	@Precio float   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PrecioHabitacionID]
	, [TipoHabitacion]
	, [HotelID]
	, [FechaRegistro]
	, [Activo]
	, [Precio]
    FROM
	[dbo].[PrecioHabitacion]
    WHERE 
	 ([PrecioHabitacionID] = @PrecioHabitacionId OR @PrecioHabitacionId IS NULL)
	AND ([TipoHabitacion] = @TipoHabitacion OR @TipoHabitacion IS NULL)
	AND ([HotelID] = @HotelId OR @HotelId IS NULL)
	AND ([FechaRegistro] = @FechaRegistro OR @FechaRegistro IS NULL)
	AND ([Activo] = @Activo OR @Activo IS NULL)
	AND ([Precio] = @Precio OR @Precio IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PrecioHabitacionID]
	, [TipoHabitacion]
	, [HotelID]
	, [FechaRegistro]
	, [Activo]
	, [Precio]
    FROM
	[dbo].[PrecioHabitacion]
    WHERE 
	 ([PrecioHabitacionID] = @PrecioHabitacionId AND @PrecioHabitacionId is not null)
	OR ([TipoHabitacion] = @TipoHabitacion AND @TipoHabitacion is not null)
	OR ([HotelID] = @HotelId AND @HotelId is not null)
	OR ([FechaRegistro] = @FechaRegistro AND @FechaRegistro is not null)
	OR ([Activo] = @Activo AND @Activo is not null)
	OR ([Precio] = @Precio AND @Precio is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Voucher_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Voucher_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Voucher_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Voucher table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Voucher_Get_List

AS


				
				SELECT
					[VoucherID],
					[NroVoucher],
					[FechaEmision],
					[VendedorID]
				FROM
					[dbo].[Voucher]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Voucher_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Voucher_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Voucher_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Voucher table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Voucher_GetPaged
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
				    [VoucherID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([VoucherID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [VoucherID]'
				SET @SQL = @SQL + ' FROM [dbo].[Voucher]'
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
				SELECT O.[VoucherID], O.[NroVoucher], O.[FechaEmision], O.[VendedorID]
				FROM
				    [dbo].[Voucher] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[VoucherID] = PageIndex.[VoucherID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Voucher]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Voucher_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Voucher_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Voucher_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Voucher table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Voucher_Insert
(

	@VoucherId uniqueidentifier    OUTPUT,

	@NroVoucher bigint    OUTPUT,

	@FechaEmision datetime   ,

	@VendedorId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[Voucher]
					(
					[VoucherID]
					,[FechaEmision]
					,[VendedorID]
					)
				VALUES
					(
					@VoucherId
					,@FechaEmision
					,@VendedorId
					)
				
				-- Get the identity value
				SET @NroVoucher = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Voucher_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Voucher_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Voucher_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Voucher table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Voucher_Update
(

	@VoucherId uniqueidentifier   ,

	@OriginalVoucherId uniqueidentifier   ,

	@NroVoucher bigint   ,

	@FechaEmision datetime   ,

	@VendedorId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Voucher]
				SET
					[VoucherID] = @VoucherId
					,[FechaEmision] = @FechaEmision
					,[VendedorID] = @VendedorId
				WHERE
[VoucherID] = @OriginalVoucherId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Voucher_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Voucher_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Voucher_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Voucher table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Voucher_Delete
(

	@VoucherId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Voucher] WITH (ROWLOCK) 
				WHERE
					[VoucherID] = @VoucherId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Voucher_GetByVendedorId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Voucher_GetByVendedorId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Voucher_GetByVendedorId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Voucher table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Voucher_GetByVendedorId
(

	@VendedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[VoucherID],
					[NroVoucher],
					[FechaEmision],
					[VendedorID]
				FROM
					[dbo].[Voucher]
				WHERE
					[VendedorID] = @VendedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Voucher_GetByVoucherId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Voucher_GetByVoucherId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Voucher_GetByVoucherId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Voucher table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Voucher_GetByVoucherId
(

	@VoucherId uniqueidentifier   
)
AS


				SELECT
					[VoucherID],
					[NroVoucher],
					[FechaEmision],
					[VendedorID]
				FROM
					[dbo].[Voucher]
				WHERE
					[VoucherID] = @VoucherId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Voucher_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Voucher_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Voucher_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Voucher table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Voucher_Find
(

	@SearchUsingOR bit   = null ,

	@VoucherId uniqueidentifier   = null ,

	@NroVoucher bigint   = null ,

	@FechaEmision datetime   = null ,

	@VendedorId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [VoucherID]
	, [NroVoucher]
	, [FechaEmision]
	, [VendedorID]
    FROM
	[dbo].[Voucher]
    WHERE 
	 ([VoucherID] = @VoucherId OR @VoucherId IS NULL)
	AND ([NroVoucher] = @NroVoucher OR @NroVoucher IS NULL)
	AND ([FechaEmision] = @FechaEmision OR @FechaEmision IS NULL)
	AND ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [VoucherID]
	, [NroVoucher]
	, [FechaEmision]
	, [VendedorID]
    FROM
	[dbo].[Voucher]
    WHERE 
	 ([VoucherID] = @VoucherId AND @VoucherId is not null)
	OR ([NroVoucher] = @NroVoucher AND @NroVoucher is not null)
	OR ([FechaEmision] = @FechaEmision AND @FechaEmision is not null)
	OR ([VendedorID] = @VendedorId AND @VendedorId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Adicional_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Adicional_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Adicional_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Adicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Adicional_Get_List

AS


				
				SELECT
					[AdicionalID],
					[Monto],
					[Descripcion]
				FROM
					[dbo].[Adicional]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Adicional_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Adicional_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Adicional_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Adicional table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Adicional_GetPaged
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
				    [AdicionalID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([AdicionalID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [AdicionalID]'
				SET @SQL = @SQL + ' FROM [dbo].[Adicional]'
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
				SELECT O.[AdicionalID], O.[Monto], O.[Descripcion]
				FROM
				    [dbo].[Adicional] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[AdicionalID] = PageIndex.[AdicionalID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Adicional]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Adicional_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Adicional_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Adicional_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Adicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Adicional_Insert
(

	@AdicionalId uniqueidentifier    OUTPUT,

	@Monto float   ,

	@Descripcion varchar (MAX)  
)
AS


				
				INSERT INTO [dbo].[Adicional]
					(
					[AdicionalID]
					,[Monto]
					,[Descripcion]
					)
				VALUES
					(
					@AdicionalId
					,@Monto
					,@Descripcion
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Adicional_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Adicional_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Adicional_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Adicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Adicional_Update
(

	@AdicionalId uniqueidentifier   ,

	@OriginalAdicionalId uniqueidentifier   ,

	@Monto float   ,

	@Descripcion varchar (MAX)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Adicional]
				SET
					[AdicionalID] = @AdicionalId
					,[Monto] = @Monto
					,[Descripcion] = @Descripcion
				WHERE
[AdicionalID] = @OriginalAdicionalId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Adicional_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Adicional_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Adicional_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Adicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Adicional_Delete
(

	@AdicionalId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Adicional] WITH (ROWLOCK) 
				WHERE
					[AdicionalID] = @AdicionalId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Adicional_GetByAdicionalId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Adicional_GetByAdicionalId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Adicional_GetByAdicionalId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Adicional table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Adicional_GetByAdicionalId
(

	@AdicionalId uniqueidentifier   
)
AS


				SELECT
					[AdicionalID],
					[Monto],
					[Descripcion]
				FROM
					[dbo].[Adicional]
				WHERE
					[AdicionalID] = @AdicionalId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Adicional_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Adicional_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Adicional_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Adicional table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Adicional_Find
(

	@SearchUsingOR bit   = null ,

	@AdicionalId uniqueidentifier   = null ,

	@Monto float   = null ,

	@Descripcion varchar (MAX)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [AdicionalID]
	, [Monto]
	, [Descripcion]
    FROM
	[dbo].[Adicional]
    WHERE 
	 ([AdicionalID] = @AdicionalId OR @AdicionalId IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [AdicionalID]
	, [Monto]
	, [Descripcion]
    FROM
	[dbo].[Adicional]
    WHERE 
	 ([AdicionalID] = @AdicionalId AND @AdicionalId is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteAdicional_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteAdicional_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteAdicional_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PaqueteAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteAdicional_Get_List

AS


				
				SELECT
					[PaqueteAdicionalID],
					[PaqueteID],
					[AdicionalID]
				FROM
					[dbo].[PaqueteAdicional]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteAdicional_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteAdicional_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteAdicional_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PaqueteAdicional table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteAdicional_GetPaged
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
				    [PaqueteAdicionalID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PaqueteAdicionalID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PaqueteAdicionalID]'
				SET @SQL = @SQL + ' FROM [dbo].[PaqueteAdicional]'
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
				SELECT O.[PaqueteAdicionalID], O.[PaqueteID], O.[AdicionalID]
				FROM
				    [dbo].[PaqueteAdicional] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PaqueteAdicionalID] = PageIndex.[PaqueteAdicionalID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PaqueteAdicional]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteAdicional_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteAdicional_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteAdicional_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the PaqueteAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteAdicional_Insert
(

	@PaqueteAdicionalId uniqueidentifier    OUTPUT,

	@PaqueteId uniqueidentifier   ,

	@AdicionalId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PaqueteAdicional]
					(
					[PaqueteAdicionalID]
					,[PaqueteID]
					,[AdicionalID]
					)
				VALUES
					(
					@PaqueteAdicionalId
					,@PaqueteId
					,@AdicionalId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteAdicional_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteAdicional_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteAdicional_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the PaqueteAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteAdicional_Update
(

	@PaqueteAdicionalId uniqueidentifier   ,

	@OriginalPaqueteAdicionalId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   ,

	@AdicionalId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PaqueteAdicional]
				SET
					[PaqueteAdicionalID] = @PaqueteAdicionalId
					,[PaqueteID] = @PaqueteId
					,[AdicionalID] = @AdicionalId
				WHERE
[PaqueteAdicionalID] = @OriginalPaqueteAdicionalId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteAdicional_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteAdicional_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteAdicional_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the PaqueteAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteAdicional_Delete
(

	@PaqueteAdicionalId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PaqueteAdicional] WITH (ROWLOCK) 
				WHERE
					[PaqueteAdicionalID] = @PaqueteAdicionalId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteAdicional_GetByAdicionalId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteAdicional_GetByAdicionalId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteAdicional_GetByAdicionalId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaqueteAdicional table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteAdicional_GetByAdicionalId
(

	@AdicionalId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaqueteAdicionalID],
					[PaqueteID],
					[AdicionalID]
				FROM
					[dbo].[PaqueteAdicional]
				WHERE
					[AdicionalID] = @AdicionalId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteAdicional_GetByPaqueteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteAdicional_GetByPaqueteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteAdicional_GetByPaqueteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaqueteAdicional table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteAdicional_GetByPaqueteId
(

	@PaqueteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaqueteAdicionalID],
					[PaqueteID],
					[AdicionalID]
				FROM
					[dbo].[PaqueteAdicional]
				WHERE
					[PaqueteID] = @PaqueteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteAdicional_GetByPaqueteAdicionalId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteAdicional_GetByPaqueteAdicionalId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteAdicional_GetByPaqueteAdicionalId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PaqueteAdicional table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteAdicional_GetByPaqueteAdicionalId
(

	@PaqueteAdicionalId uniqueidentifier   
)
AS


				SELECT
					[PaqueteAdicionalID],
					[PaqueteID],
					[AdicionalID]
				FROM
					[dbo].[PaqueteAdicional]
				WHERE
					[PaqueteAdicionalID] = @PaqueteAdicionalId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PaqueteAdicional_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PaqueteAdicional_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PaqueteAdicional_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the PaqueteAdicional table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PaqueteAdicional_Find
(

	@SearchUsingOR bit   = null ,

	@PaqueteAdicionalId uniqueidentifier   = null ,

	@PaqueteId uniqueidentifier   = null ,

	@AdicionalId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaqueteAdicionalID]
	, [PaqueteID]
	, [AdicionalID]
    FROM
	[dbo].[PaqueteAdicional]
    WHERE 
	 ([PaqueteAdicionalID] = @PaqueteAdicionalId OR @PaqueteAdicionalId IS NULL)
	AND ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
	AND ([AdicionalID] = @AdicionalId OR @AdicionalId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaqueteAdicionalID]
	, [PaqueteID]
	, [AdicionalID]
    FROM
	[dbo].[PaqueteAdicional]
    WHERE 
	 ([PaqueteAdicionalID] = @PaqueteAdicionalId AND @PaqueteAdicionalId is not null)
	OR ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	OR ([AdicionalID] = @AdicionalId AND @AdicionalId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AuditFactura_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AuditFactura_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AuditFactura_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the AuditFactura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AuditFactura_Get_List

AS


				
				SELECT
					[ID],
					[FacturaID],
					[PersonaID],
					[VendedorID],
					[Accion],
					[Descripcion],
					[Fecha]
				FROM
					[dbo].[AuditFactura]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AuditFactura_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AuditFactura_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AuditFactura_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the AuditFactura table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AuditFactura_GetPaged
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
				    [ID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ID]'
				SET @SQL = @SQL + ' FROM [dbo].[AuditFactura]'
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
				SELECT O.[ID], O.[FacturaID], O.[PersonaID], O.[VendedorID], O.[Accion], O.[Descripcion], O.[Fecha]
				FROM
				    [dbo].[AuditFactura] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ID] = PageIndex.[ID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[AuditFactura]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AuditFactura_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AuditFactura_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AuditFactura_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the AuditFactura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AuditFactura_Insert
(

	@Id int    OUTPUT,

	@FacturaId uniqueidentifier   ,

	@PersonaId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@Accion varchar (200)  ,

	@Descripcion varchar (200)  ,

	@Fecha datetime   
)
AS


				
				INSERT INTO [dbo].[AuditFactura]
					(
					[FacturaID]
					,[PersonaID]
					,[VendedorID]
					,[Accion]
					,[Descripcion]
					,[Fecha]
					)
				VALUES
					(
					@FacturaId
					,@PersonaId
					,@VendedorId
					,@Accion
					,@Descripcion
					,@Fecha
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AuditFactura_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AuditFactura_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AuditFactura_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the AuditFactura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AuditFactura_Update
(

	@Id int   ,

	@FacturaId uniqueidentifier   ,

	@PersonaId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@Accion varchar (200)  ,

	@Descripcion varchar (200)  ,

	@Fecha datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[AuditFactura]
				SET
					[FacturaID] = @FacturaId
					,[PersonaID] = @PersonaId
					,[VendedorID] = @VendedorId
					,[Accion] = @Accion
					,[Descripcion] = @Descripcion
					,[Fecha] = @Fecha
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AuditFactura_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AuditFactura_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AuditFactura_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the AuditFactura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AuditFactura_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[AuditFactura] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AuditFactura_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AuditFactura_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AuditFactura_GetById
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the AuditFactura table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AuditFactura_GetById
(

	@Id int   
)
AS


				SELECT
					[ID],
					[FacturaID],
					[PersonaID],
					[VendedorID],
					[Accion],
					[Descripcion],
					[Fecha]
				FROM
					[dbo].[AuditFactura]
				WHERE
					[ID] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.AuditFactura_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.AuditFactura_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.AuditFactura_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the AuditFactura table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.AuditFactura_Find
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@FacturaId uniqueidentifier   = null ,

	@PersonaId uniqueidentifier   = null ,

	@VendedorId uniqueidentifier   = null ,

	@Accion varchar (200)  = null ,

	@Descripcion varchar (200)  = null ,

	@Fecha datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [FacturaID]
	, [PersonaID]
	, [VendedorID]
	, [Accion]
	, [Descripcion]
	, [Fecha]
    FROM
	[dbo].[AuditFactura]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([FacturaID] = @FacturaId OR @FacturaId IS NULL)
	AND ([PersonaID] = @PersonaId OR @PersonaId IS NULL)
	AND ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
	AND ([Accion] = @Accion OR @Accion IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
	AND ([Fecha] = @Fecha OR @Fecha IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [FacturaID]
	, [PersonaID]
	, [VendedorID]
	, [Accion]
	, [Descripcion]
	, [Fecha]
    FROM
	[dbo].[AuditFactura]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([FacturaID] = @FacturaId AND @FacturaId is not null)
	OR ([PersonaID] = @PersonaId AND @PersonaId is not null)
	OR ([VendedorID] = @VendedorId AND @VendedorId is not null)
	OR ([Accion] = @Accion AND @Accion is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	OR ([Fecha] = @Fecha AND @Fecha is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Butaca_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Butaca_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Butaca_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Butaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Butaca_Get_List

AS


				
				SELECT
					[ButacaID],
					[NroButaca],
					[Piso],
					[Ubicacion],
					[Tipo],
					[TransporteID],
					[Fila],
					[Posicion],
					[CodigoButaca]
				FROM
					[dbo].[Butaca]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Butaca_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Butaca_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Butaca_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Butaca table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Butaca_GetPaged
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
				    [ButacaID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ButacaID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ButacaID]'
				SET @SQL = @SQL + ' FROM [dbo].[Butaca]'
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
				SELECT O.[ButacaID], O.[NroButaca], O.[Piso], O.[Ubicacion], O.[Tipo], O.[TransporteID], O.[Fila], O.[Posicion], O.[CodigoButaca]
				FROM
				    [dbo].[Butaca] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ButacaID] = PageIndex.[ButacaID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Butaca]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Butaca_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Butaca_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Butaca_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Butaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Butaca_Insert
(

	@ButacaId uniqueidentifier    OUTPUT,

	@NroButaca int   ,

	@Piso int   ,

	@Ubicacion int   ,

	@Tipo int   ,

	@TransporteId uniqueidentifier   ,

	@Fila varchar (2)  ,

	@Posicion varchar (1)  ,

	@CodigoButaca varchar (4)  
)
AS


				
				INSERT INTO [dbo].[Butaca]
					(
					[ButacaID]
					,[NroButaca]
					,[Piso]
					,[Ubicacion]
					,[Tipo]
					,[TransporteID]
					,[Fila]
					,[Posicion]
					,[CodigoButaca]
					)
				VALUES
					(
					@ButacaId
					,@NroButaca
					,@Piso
					,@Ubicacion
					,@Tipo
					,@TransporteId
					,@Fila
					,@Posicion
					,@CodigoButaca
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Butaca_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Butaca_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Butaca_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Butaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Butaca_Update
(

	@ButacaId uniqueidentifier   ,

	@OriginalButacaId uniqueidentifier   ,

	@NroButaca int   ,

	@Piso int   ,

	@Ubicacion int   ,

	@Tipo int   ,

	@TransporteId uniqueidentifier   ,

	@Fila varchar (2)  ,

	@Posicion varchar (1)  ,

	@CodigoButaca varchar (4)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Butaca]
				SET
					[ButacaID] = @ButacaId
					,[NroButaca] = @NroButaca
					,[Piso] = @Piso
					,[Ubicacion] = @Ubicacion
					,[Tipo] = @Tipo
					,[TransporteID] = @TransporteId
					,[Fila] = @Fila
					,[Posicion] = @Posicion
					,[CodigoButaca] = @CodigoButaca
				WHERE
[ButacaID] = @OriginalButacaId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Butaca_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Butaca_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Butaca_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Butaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Butaca_Delete
(

	@ButacaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Butaca] WITH (ROWLOCK) 
				WHERE
					[ButacaID] = @ButacaId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Butaca_GetByTransporteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Butaca_GetByTransporteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Butaca_GetByTransporteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Butaca table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Butaca_GetByTransporteId
(

	@TransporteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ButacaID],
					[NroButaca],
					[Piso],
					[Ubicacion],
					[Tipo],
					[TransporteID],
					[Fila],
					[Posicion],
					[CodigoButaca]
				FROM
					[dbo].[Butaca]
				WHERE
					[TransporteID] = @TransporteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Butaca_GetByButacaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Butaca_GetByButacaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Butaca_GetByButacaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Butaca table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Butaca_GetByButacaId
(

	@ButacaId uniqueidentifier   
)
AS


				SELECT
					[ButacaID],
					[NroButaca],
					[Piso],
					[Ubicacion],
					[Tipo],
					[TransporteID],
					[Fila],
					[Posicion],
					[CodigoButaca]
				FROM
					[dbo].[Butaca]
				WHERE
					[ButacaID] = @ButacaId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Butaca_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Butaca_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Butaca_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Butaca table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Butaca_Find
(

	@SearchUsingOR bit   = null ,

	@ButacaId uniqueidentifier   = null ,

	@NroButaca int   = null ,

	@Piso int   = null ,

	@Ubicacion int   = null ,

	@Tipo int   = null ,

	@TransporteId uniqueidentifier   = null ,

	@Fila varchar (2)  = null ,

	@Posicion varchar (1)  = null ,

	@CodigoButaca varchar (4)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ButacaID]
	, [NroButaca]
	, [Piso]
	, [Ubicacion]
	, [Tipo]
	, [TransporteID]
	, [Fila]
	, [Posicion]
	, [CodigoButaca]
    FROM
	[dbo].[Butaca]
    WHERE 
	 ([ButacaID] = @ButacaId OR @ButacaId IS NULL)
	AND ([NroButaca] = @NroButaca OR @NroButaca IS NULL)
	AND ([Piso] = @Piso OR @Piso IS NULL)
	AND ([Ubicacion] = @Ubicacion OR @Ubicacion IS NULL)
	AND ([Tipo] = @Tipo OR @Tipo IS NULL)
	AND ([TransporteID] = @TransporteId OR @TransporteId IS NULL)
	AND ([Fila] = @Fila OR @Fila IS NULL)
	AND ([Posicion] = @Posicion OR @Posicion IS NULL)
	AND ([CodigoButaca] = @CodigoButaca OR @CodigoButaca IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ButacaID]
	, [NroButaca]
	, [Piso]
	, [Ubicacion]
	, [Tipo]
	, [TransporteID]
	, [Fila]
	, [Posicion]
	, [CodigoButaca]
    FROM
	[dbo].[Butaca]
    WHERE 
	 ([ButacaID] = @ButacaId AND @ButacaId is not null)
	OR ([NroButaca] = @NroButaca AND @NroButaca is not null)
	OR ([Piso] = @Piso AND @Piso is not null)
	OR ([Ubicacion] = @Ubicacion AND @Ubicacion is not null)
	OR ([Tipo] = @Tipo AND @Tipo is not null)
	OR ([TransporteID] = @TransporteId AND @TransporteId is not null)
	OR ([Fila] = @Fila AND @Fila is not null)
	OR ([Posicion] = @Posicion AND @Posicion is not null)
	OR ([CodigoButaca] = @CodigoButaca AND @CodigoButaca is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Ciudad_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Ciudad_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Ciudad_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Ciudad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Ciudad_Get_List

AS


				
				SELECT
					[CiudadID],
					[CiudadNombre],
					[PaisCodigo],
					[CiudadDistrito],
					[CiudadPoblacion]
				FROM
					[dbo].[Ciudad]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Ciudad_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Ciudad_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Ciudad_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Ciudad table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Ciudad_GetPaged
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
				    [CiudadID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([CiudadID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [CiudadID]'
				SET @SQL = @SQL + ' FROM [dbo].[Ciudad]'
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
				SELECT O.[CiudadID], O.[CiudadNombre], O.[PaisCodigo], O.[CiudadDistrito], O.[CiudadPoblacion]
				FROM
				    [dbo].[Ciudad] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[CiudadID] = PageIndex.[CiudadID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Ciudad]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Ciudad_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Ciudad_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Ciudad_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Ciudad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Ciudad_Insert
(

	@CiudadId int   ,

	@CiudadNombre char (35)  ,

	@PaisCodigo char (3)  ,

	@CiudadDistrito char (20)  ,

	@CiudadPoblacion int   
)
AS


				
				INSERT INTO [dbo].[Ciudad]
					(
					[CiudadID]
					,[CiudadNombre]
					,[PaisCodigo]
					,[CiudadDistrito]
					,[CiudadPoblacion]
					)
				VALUES
					(
					@CiudadId
					,@CiudadNombre
					,@PaisCodigo
					,@CiudadDistrito
					,@CiudadPoblacion
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Ciudad_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Ciudad_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Ciudad_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Ciudad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Ciudad_Update
(

	@CiudadId int   ,

	@OriginalCiudadId int   ,

	@CiudadNombre char (35)  ,

	@PaisCodigo char (3)  ,

	@CiudadDistrito char (20)  ,

	@CiudadPoblacion int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Ciudad]
				SET
					[CiudadID] = @CiudadId
					,[CiudadNombre] = @CiudadNombre
					,[PaisCodigo] = @PaisCodigo
					,[CiudadDistrito] = @CiudadDistrito
					,[CiudadPoblacion] = @CiudadPoblacion
				WHERE
[CiudadID] = @OriginalCiudadId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Ciudad_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Ciudad_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Ciudad_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Ciudad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Ciudad_Delete
(

	@CiudadId int   
)
AS


				DELETE FROM [dbo].[Ciudad] WITH (ROWLOCK) 
				WHERE
					[CiudadID] = @CiudadId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Ciudad_GetByCiudadId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Ciudad_GetByCiudadId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Ciudad_GetByCiudadId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Ciudad table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Ciudad_GetByCiudadId
(

	@CiudadId int   
)
AS


				SELECT
					[CiudadID],
					[CiudadNombre],
					[PaisCodigo],
					[CiudadDistrito],
					[CiudadPoblacion]
				FROM
					[dbo].[Ciudad]
				WHERE
					[CiudadID] = @CiudadId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Ciudad_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Ciudad_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Ciudad_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Ciudad table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Ciudad_Find
(

	@SearchUsingOR bit   = null ,

	@CiudadId int   = null ,

	@CiudadNombre char (35)  = null ,

	@PaisCodigo char (3)  = null ,

	@CiudadDistrito char (20)  = null ,

	@CiudadPoblacion int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [CiudadID]
	, [CiudadNombre]
	, [PaisCodigo]
	, [CiudadDistrito]
	, [CiudadPoblacion]
    FROM
	[dbo].[Ciudad]
    WHERE 
	 ([CiudadID] = @CiudadId OR @CiudadId IS NULL)
	AND ([CiudadNombre] = @CiudadNombre OR @CiudadNombre IS NULL)
	AND ([PaisCodigo] = @PaisCodigo OR @PaisCodigo IS NULL)
	AND ([CiudadDistrito] = @CiudadDistrito OR @CiudadDistrito IS NULL)
	AND ([CiudadPoblacion] = @CiudadPoblacion OR @CiudadPoblacion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [CiudadID]
	, [CiudadNombre]
	, [PaisCodigo]
	, [CiudadDistrito]
	, [CiudadPoblacion]
    FROM
	[dbo].[Ciudad]
    WHERE 
	 ([CiudadID] = @CiudadId AND @CiudadId is not null)
	OR ([CiudadNombre] = @CiudadNombre AND @CiudadNombre is not null)
	OR ([PaisCodigo] = @PaisCodigo AND @PaisCodigo is not null)
	OR ([CiudadDistrito] = @CiudadDistrito AND @CiudadDistrito is not null)
	OR ([CiudadPoblacion] = @CiudadPoblacion AND @CiudadPoblacion is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cliente_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cliente_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cliente_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Cliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cliente_Get_List

AS


				
				SELECT
					[ClienteID],
					[RazonSocial],
					[Cuit],
					[Moneda],
					[Empresa],
					[Ocupacion],
					[FormaPago],
					[CondicionIva],
					[VendedorID],
					[Fax],
					[Web],
					[Idioma],
					[Promotor],
					[Observacion],
					[TipoID]
				FROM
					[dbo].[Cliente]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cliente_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cliente_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cliente_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Cliente table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cliente_GetPaged
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
				    [ClienteID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ClienteID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ClienteID]'
				SET @SQL = @SQL + ' FROM [dbo].[Cliente]'
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
				SELECT O.[ClienteID], O.[RazonSocial], O.[Cuit], O.[Moneda], O.[Empresa], O.[Ocupacion], O.[FormaPago], O.[CondicionIva], O.[VendedorID], O.[Fax], O.[Web], O.[Idioma], O.[Promotor], O.[Observacion], O.[TipoID]
				FROM
				    [dbo].[Cliente] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ClienteID] = PageIndex.[ClienteID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Cliente]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cliente_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cliente_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cliente_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Cliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cliente_Insert
(

	@ClienteId uniqueidentifier    OUTPUT,

	@RazonSocial varchar (50)  ,

	@Cuit varchar (50)  ,

	@Moneda varchar (50)  ,

	@Empresa varchar (50)  ,

	@Ocupacion varchar (50)  ,

	@FormaPago int   ,

	@CondicionIva int   ,

	@VendedorId uniqueidentifier   ,

	@Fax varchar (50)  ,

	@Web varchar (50)  ,

	@Idioma varchar (50)  ,

	@Promotor varchar (50)  ,

	@Observacion varchar (250)  ,

	@TipoId int   
)
AS


				
				INSERT INTO [dbo].[Cliente]
					(
					[ClienteID]
					,[RazonSocial]
					,[Cuit]
					,[Moneda]
					,[Empresa]
					,[Ocupacion]
					,[FormaPago]
					,[CondicionIva]
					,[VendedorID]
					,[Fax]
					,[Web]
					,[Idioma]
					,[Promotor]
					,[Observacion]
					,[TipoID]
					)
				VALUES
					(
					@ClienteId
					,@RazonSocial
					,@Cuit
					,@Moneda
					,@Empresa
					,@Ocupacion
					,@FormaPago
					,@CondicionIva
					,@VendedorId
					,@Fax
					,@Web
					,@Idioma
					,@Promotor
					,@Observacion
					,@TipoId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cliente_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cliente_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cliente_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Cliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cliente_Update
(

	@ClienteId uniqueidentifier   ,

	@OriginalClienteId uniqueidentifier   ,

	@RazonSocial varchar (50)  ,

	@Cuit varchar (50)  ,

	@Moneda varchar (50)  ,

	@Empresa varchar (50)  ,

	@Ocupacion varchar (50)  ,

	@FormaPago int   ,

	@CondicionIva int   ,

	@VendedorId uniqueidentifier   ,

	@Fax varchar (50)  ,

	@Web varchar (50)  ,

	@Idioma varchar (50)  ,

	@Promotor varchar (50)  ,

	@Observacion varchar (250)  ,

	@TipoId int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Cliente]
				SET
					[ClienteID] = @ClienteId
					,[RazonSocial] = @RazonSocial
					,[Cuit] = @Cuit
					,[Moneda] = @Moneda
					,[Empresa] = @Empresa
					,[Ocupacion] = @Ocupacion
					,[FormaPago] = @FormaPago
					,[CondicionIva] = @CondicionIva
					,[VendedorID] = @VendedorId
					,[Fax] = @Fax
					,[Web] = @Web
					,[Idioma] = @Idioma
					,[Promotor] = @Promotor
					,[Observacion] = @Observacion
					,[TipoID] = @TipoId
				WHERE
[ClienteID] = @OriginalClienteId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cliente_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cliente_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cliente_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Cliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cliente_Delete
(

	@ClienteId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Cliente] WITH (ROWLOCK) 
				WHERE
					[ClienteID] = @ClienteId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cliente_GetByClienteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cliente_GetByClienteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cliente_GetByClienteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Cliente table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cliente_GetByClienteId
(

	@ClienteId uniqueidentifier   
)
AS


				SELECT
					[ClienteID],
					[RazonSocial],
					[Cuit],
					[Moneda],
					[Empresa],
					[Ocupacion],
					[FormaPago],
					[CondicionIva],
					[VendedorID],
					[Fax],
					[Web],
					[Idioma],
					[Promotor],
					[Observacion],
					[TipoID]
				FROM
					[dbo].[Cliente]
				WHERE
					[ClienteID] = @ClienteId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cliente_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cliente_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cliente_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Cliente table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cliente_Find
(

	@SearchUsingOR bit   = null ,

	@ClienteId uniqueidentifier   = null ,

	@RazonSocial varchar (50)  = null ,

	@Cuit varchar (50)  = null ,

	@Moneda varchar (50)  = null ,

	@Empresa varchar (50)  = null ,

	@Ocupacion varchar (50)  = null ,

	@FormaPago int   = null ,

	@CondicionIva int   = null ,

	@VendedorId uniqueidentifier   = null ,

	@Fax varchar (50)  = null ,

	@Web varchar (50)  = null ,

	@Idioma varchar (50)  = null ,

	@Promotor varchar (50)  = null ,

	@Observacion varchar (250)  = null ,

	@TipoId int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ClienteID]
	, [RazonSocial]
	, [Cuit]
	, [Moneda]
	, [Empresa]
	, [Ocupacion]
	, [FormaPago]
	, [CondicionIva]
	, [VendedorID]
	, [Fax]
	, [Web]
	, [Idioma]
	, [Promotor]
	, [Observacion]
	, [TipoID]
    FROM
	[dbo].[Cliente]
    WHERE 
	 ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
	AND ([RazonSocial] = @RazonSocial OR @RazonSocial IS NULL)
	AND ([Cuit] = @Cuit OR @Cuit IS NULL)
	AND ([Moneda] = @Moneda OR @Moneda IS NULL)
	AND ([Empresa] = @Empresa OR @Empresa IS NULL)
	AND ([Ocupacion] = @Ocupacion OR @Ocupacion IS NULL)
	AND ([FormaPago] = @FormaPago OR @FormaPago IS NULL)
	AND ([CondicionIva] = @CondicionIva OR @CondicionIva IS NULL)
	AND ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
	AND ([Fax] = @Fax OR @Fax IS NULL)
	AND ([Web] = @Web OR @Web IS NULL)
	AND ([Idioma] = @Idioma OR @Idioma IS NULL)
	AND ([Promotor] = @Promotor OR @Promotor IS NULL)
	AND ([Observacion] = @Observacion OR @Observacion IS NULL)
	AND ([TipoID] = @TipoId OR @TipoId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ClienteID]
	, [RazonSocial]
	, [Cuit]
	, [Moneda]
	, [Empresa]
	, [Ocupacion]
	, [FormaPago]
	, [CondicionIva]
	, [VendedorID]
	, [Fax]
	, [Web]
	, [Idioma]
	, [Promotor]
	, [Observacion]
	, [TipoID]
    FROM
	[dbo].[Cliente]
    WHERE 
	 ([ClienteID] = @ClienteId AND @ClienteId is not null)
	OR ([RazonSocial] = @RazonSocial AND @RazonSocial is not null)
	OR ([Cuit] = @Cuit AND @Cuit is not null)
	OR ([Moneda] = @Moneda AND @Moneda is not null)
	OR ([Empresa] = @Empresa AND @Empresa is not null)
	OR ([Ocupacion] = @Ocupacion AND @Ocupacion is not null)
	OR ([FormaPago] = @FormaPago AND @FormaPago is not null)
	OR ([CondicionIva] = @CondicionIva AND @CondicionIva is not null)
	OR ([VendedorID] = @VendedorId AND @VendedorId is not null)
	OR ([Fax] = @Fax AND @Fax is not null)
	OR ([Web] = @Web AND @Web is not null)
	OR ([Idioma] = @Idioma AND @Idioma is not null)
	OR ([Promotor] = @Promotor AND @Promotor is not null)
	OR ([Observacion] = @Observacion AND @Observacion is not null)
	OR ([TipoID] = @TipoId AND @TipoId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cuenta_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cuenta_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cuenta_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Cuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cuenta_Get_List

AS


				
				SELECT
					[CuentaID],
					[ClienteID],
					[Estado]
				FROM
					[dbo].[Cuenta]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cuenta_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cuenta_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cuenta_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Cuenta table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cuenta_GetPaged
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
				    [CuentaID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([CuentaID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [CuentaID]'
				SET @SQL = @SQL + ' FROM [dbo].[Cuenta]'
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
				SELECT O.[CuentaID], O.[ClienteID], O.[Estado]
				FROM
				    [dbo].[Cuenta] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[CuentaID] = PageIndex.[CuentaID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Cuenta]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cuenta_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cuenta_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cuenta_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Cuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cuenta_Insert
(

	@CuentaId uniqueidentifier    OUTPUT,

	@ClienteId uniqueidentifier   ,

	@Estado bit   
)
AS


				
				INSERT INTO [dbo].[Cuenta]
					(
					[CuentaID]
					,[ClienteID]
					,[Estado]
					)
				VALUES
					(
					@CuentaId
					,@ClienteId
					,@Estado
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cuenta_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cuenta_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cuenta_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Cuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cuenta_Update
(

	@CuentaId uniqueidentifier   ,

	@OriginalCuentaId uniqueidentifier   ,

	@ClienteId uniqueidentifier   ,

	@Estado bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Cuenta]
				SET
					[CuentaID] = @CuentaId
					,[ClienteID] = @ClienteId
					,[Estado] = @Estado
				WHERE
[CuentaID] = @OriginalCuentaId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cuenta_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cuenta_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cuenta_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Cuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cuenta_Delete
(

	@CuentaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Cuenta] WITH (ROWLOCK) 
				WHERE
					[CuentaID] = @CuentaId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cuenta_GetByClienteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cuenta_GetByClienteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cuenta_GetByClienteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Cuenta table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cuenta_GetByClienteId
(

	@ClienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[CuentaID],
					[ClienteID],
					[Estado]
				FROM
					[dbo].[Cuenta]
				WHERE
					[ClienteID] = @ClienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cuenta_GetByCuentaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cuenta_GetByCuentaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cuenta_GetByCuentaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Cuenta table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cuenta_GetByCuentaId
(

	@CuentaId uniqueidentifier   
)
AS


				SELECT
					[CuentaID],
					[ClienteID],
					[Estado]
				FROM
					[dbo].[Cuenta]
				WHERE
					[CuentaID] = @CuentaId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Cuenta_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Cuenta_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Cuenta_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Cuenta table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Cuenta_Find
(

	@SearchUsingOR bit   = null ,

	@CuentaId uniqueidentifier   = null ,

	@ClienteId uniqueidentifier   = null ,

	@Estado bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [CuentaID]
	, [ClienteID]
	, [Estado]
    FROM
	[dbo].[Cuenta]
    WHERE 
	 ([CuentaID] = @CuentaId OR @CuentaId IS NULL)
	AND ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
	AND ([Estado] = @Estado OR @Estado IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [CuentaID]
	, [ClienteID]
	, [Estado]
    FROM
	[dbo].[Cuenta]
    WHERE 
	 ([CuentaID] = @CuentaId AND @CuentaId is not null)
	OR ([ClienteID] = @ClienteId AND @ClienteId is not null)
	OR ([Estado] = @Estado AND @Estado is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CuentaCorriente_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CuentaCorriente_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CuentaCorriente_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the CuentaCorriente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CuentaCorriente_Get_List

AS


				
				SELECT
					[CuentaCorrienteID],
					[Fecha],
					[Monto],
					[ClienteID]
				FROM
					[dbo].[CuentaCorriente]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CuentaCorriente_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CuentaCorriente_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CuentaCorriente_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the CuentaCorriente table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CuentaCorriente_GetPaged
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
				    [CuentaCorrienteID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([CuentaCorrienteID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [CuentaCorrienteID]'
				SET @SQL = @SQL + ' FROM [dbo].[CuentaCorriente]'
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
				SELECT O.[CuentaCorrienteID], O.[Fecha], O.[Monto], O.[ClienteID]
				FROM
				    [dbo].[CuentaCorriente] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[CuentaCorrienteID] = PageIndex.[CuentaCorrienteID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[CuentaCorriente]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CuentaCorriente_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CuentaCorriente_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CuentaCorriente_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the CuentaCorriente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CuentaCorriente_Insert
(

	@CuentaCorrienteId uniqueidentifier   ,

	@Fecha datetime   ,

	@Monto float   ,

	@ClienteId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[CuentaCorriente]
					(
					[CuentaCorrienteID]
					,[Fecha]
					,[Monto]
					,[ClienteID]
					)
				VALUES
					(
					@CuentaCorrienteId
					,@Fecha
					,@Monto
					,@ClienteId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CuentaCorriente_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CuentaCorriente_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CuentaCorriente_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the CuentaCorriente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CuentaCorriente_Update
(

	@CuentaCorrienteId uniqueidentifier   ,

	@OriginalCuentaCorrienteId uniqueidentifier   ,

	@Fecha datetime   ,

	@Monto float   ,

	@ClienteId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[CuentaCorriente]
				SET
					[CuentaCorrienteID] = @CuentaCorrienteId
					,[Fecha] = @Fecha
					,[Monto] = @Monto
					,[ClienteID] = @ClienteId
				WHERE
[CuentaCorrienteID] = @OriginalCuentaCorrienteId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CuentaCorriente_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CuentaCorriente_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CuentaCorriente_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the CuentaCorriente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CuentaCorriente_Delete
(

	@CuentaCorrienteId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[CuentaCorriente] WITH (ROWLOCK) 
				WHERE
					[CuentaCorrienteID] = @CuentaCorrienteId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CuentaCorriente_GetByClienteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CuentaCorriente_GetByClienteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CuentaCorriente_GetByClienteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the CuentaCorriente table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CuentaCorriente_GetByClienteId
(

	@ClienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[CuentaCorrienteID],
					[Fecha],
					[Monto],
					[ClienteID]
				FROM
					[dbo].[CuentaCorriente]
				WHERE
					[ClienteID] = @ClienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CuentaCorriente_GetByCuentaCorrienteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CuentaCorriente_GetByCuentaCorrienteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CuentaCorriente_GetByCuentaCorrienteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the CuentaCorriente table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CuentaCorriente_GetByCuentaCorrienteId
(

	@CuentaCorrienteId uniqueidentifier   
)
AS


				SELECT
					[CuentaCorrienteID],
					[Fecha],
					[Monto],
					[ClienteID]
				FROM
					[dbo].[CuentaCorriente]
				WHERE
					[CuentaCorrienteID] = @CuentaCorrienteId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.CuentaCorriente_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.CuentaCorriente_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.CuentaCorriente_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the CuentaCorriente table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.CuentaCorriente_Find
(

	@SearchUsingOR bit   = null ,

	@CuentaCorrienteId uniqueidentifier   = null ,

	@Fecha datetime   = null ,

	@Monto float   = null ,

	@ClienteId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [CuentaCorrienteID]
	, [Fecha]
	, [Monto]
	, [ClienteID]
    FROM
	[dbo].[CuentaCorriente]
    WHERE 
	 ([CuentaCorrienteID] = @CuentaCorrienteId OR @CuentaCorrienteId IS NULL)
	AND ([Fecha] = @Fecha OR @Fecha IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
	AND ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [CuentaCorrienteID]
	, [Fecha]
	, [Monto]
	, [ClienteID]
    FROM
	[dbo].[CuentaCorriente]
    WHERE 
	 ([CuentaCorrienteID] = @CuentaCorrienteId AND @CuentaCorrienteId is not null)
	OR ([Fecha] = @Fecha AND @Fecha is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	OR ([ClienteID] = @ClienteId AND @ClienteId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Debito_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Debito_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Debito_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Debito table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Debito_Get_List

AS


				
				SELECT
					[DebitoID],
					[Fecha],
					[ClienteID],
					[VendedorID],
					[MontoDebito]
				FROM
					[dbo].[Debito]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Debito_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Debito_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Debito_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Debito table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Debito_GetPaged
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
				    [DebitoID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([DebitoID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [DebitoID]'
				SET @SQL = @SQL + ' FROM [dbo].[Debito]'
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
				SELECT O.[DebitoID], O.[Fecha], O.[ClienteID], O.[VendedorID], O.[MontoDebito]
				FROM
				    [dbo].[Debito] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[DebitoID] = PageIndex.[DebitoID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Debito]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Debito_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Debito_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Debito_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Debito table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Debito_Insert
(

	@DebitoId uniqueidentifier   ,

	@Fecha date   ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@MontoDebito float   
)
AS


				
				INSERT INTO [dbo].[Debito]
					(
					[DebitoID]
					,[Fecha]
					,[ClienteID]
					,[VendedorID]
					,[MontoDebito]
					)
				VALUES
					(
					@DebitoId
					,@Fecha
					,@ClienteId
					,@VendedorId
					,@MontoDebito
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Debito_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Debito_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Debito_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Debito table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Debito_Update
(

	@DebitoId uniqueidentifier   ,

	@OriginalDebitoId uniqueidentifier   ,

	@Fecha date   ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@MontoDebito float   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Debito]
				SET
					[DebitoID] = @DebitoId
					,[Fecha] = @Fecha
					,[ClienteID] = @ClienteId
					,[VendedorID] = @VendedorId
					,[MontoDebito] = @MontoDebito
				WHERE
[DebitoID] = @OriginalDebitoId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Debito_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Debito_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Debito_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Debito table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Debito_Delete
(

	@DebitoId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Debito] WITH (ROWLOCK) 
				WHERE
					[DebitoID] = @DebitoId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Debito_GetByDebitoId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Debito_GetByDebitoId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Debito_GetByDebitoId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Debito table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Debito_GetByDebitoId
(

	@DebitoId uniqueidentifier   
)
AS


				SELECT
					[DebitoID],
					[Fecha],
					[ClienteID],
					[VendedorID],
					[MontoDebito]
				FROM
					[dbo].[Debito]
				WHERE
					[DebitoID] = @DebitoId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Debito_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Debito_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Debito_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Debito table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Debito_Find
(

	@SearchUsingOR bit   = null ,

	@DebitoId uniqueidentifier   = null ,

	@Fecha date   = null ,

	@ClienteId uniqueidentifier   = null ,

	@VendedorId uniqueidentifier   = null ,

	@MontoDebito float   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [DebitoID]
	, [Fecha]
	, [ClienteID]
	, [VendedorID]
	, [MontoDebito]
    FROM
	[dbo].[Debito]
    WHERE 
	 ([DebitoID] = @DebitoId OR @DebitoId IS NULL)
	AND ([Fecha] = @Fecha OR @Fecha IS NULL)
	AND ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
	AND ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
	AND ([MontoDebito] = @MontoDebito OR @MontoDebito IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [DebitoID]
	, [Fecha]
	, [ClienteID]
	, [VendedorID]
	, [MontoDebito]
    FROM
	[dbo].[Debito]
    WHERE 
	 ([DebitoID] = @DebitoId AND @DebitoId is not null)
	OR ([Fecha] = @Fecha AND @Fecha is not null)
	OR ([ClienteID] = @ClienteId AND @ClienteId is not null)
	OR ([VendedorID] = @VendedorId AND @VendedorId is not null)
	OR ([MontoDebito] = @MontoDebito AND @MontoDebito is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Departamento_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Departamento_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Departamento_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Departamento table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Departamento_Get_List

AS


				
				SELECT
					[ID],
					[idProvincia],
					[Nombre]
				FROM
					[dbo].[Departamento]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Departamento_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Departamento_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Departamento_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Departamento table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Departamento_GetPaged
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
				    [ID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ID]'
				SET @SQL = @SQL + ' FROM [dbo].[Departamento]'
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
				SELECT O.[ID], O.[idProvincia], O.[Nombre]
				FROM
				    [dbo].[Departamento] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ID] = PageIndex.[ID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Departamento]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Departamento_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Departamento_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Departamento_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Departamento table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Departamento_Insert
(

	@Id int    OUTPUT,

	@IdProvincia int   ,

	@Nombre nvarchar (250)  
)
AS


				
				INSERT INTO [dbo].[Departamento]
					(
					[idProvincia]
					,[Nombre]
					)
				VALUES
					(
					@IdProvincia
					,@Nombre
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Departamento_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Departamento_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Departamento_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Departamento table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Departamento_Update
(

	@Id int   ,

	@IdProvincia int   ,

	@Nombre nvarchar (250)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Departamento]
				SET
					[idProvincia] = @IdProvincia
					,[Nombre] = @Nombre
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Departamento_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Departamento_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Departamento_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Departamento table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Departamento_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[Departamento] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Departamento_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Departamento_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Departamento_GetById
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Departamento table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Departamento_GetById
(

	@Id int   
)
AS


				SELECT
					[ID],
					[idProvincia],
					[Nombre]
				FROM
					[dbo].[Departamento]
				WHERE
					[ID] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Departamento_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Departamento_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Departamento_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Departamento table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Departamento_Find
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
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.EstadoPasaje_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.EstadoPasaje_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.EstadoPasaje_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the EstadoPasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.EstadoPasaje_Get_List

AS


				
				SELECT
					[ID],
					[Descripcion]
				FROM
					[dbo].[EstadoPasaje]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.EstadoPasaje_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.EstadoPasaje_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.EstadoPasaje_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the EstadoPasaje table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.EstadoPasaje_GetPaged
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
				    [ID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ID]'
				SET @SQL = @SQL + ' FROM [dbo].[EstadoPasaje]'
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
				SELECT O.[ID], O.[Descripcion]
				FROM
				    [dbo].[EstadoPasaje] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ID] = PageIndex.[ID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[EstadoPasaje]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.EstadoPasaje_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.EstadoPasaje_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.EstadoPasaje_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the EstadoPasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.EstadoPasaje_Insert
(

	@Id int    OUTPUT,

	@Descripcion varchar (50)  
)
AS


				
				INSERT INTO [dbo].[EstadoPasaje]
					(
					[Descripcion]
					)
				VALUES
					(
					@Descripcion
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.EstadoPasaje_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.EstadoPasaje_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.EstadoPasaje_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the EstadoPasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.EstadoPasaje_Update
(

	@Id int   ,

	@Descripcion varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[EstadoPasaje]
				SET
					[Descripcion] = @Descripcion
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.EstadoPasaje_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.EstadoPasaje_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.EstadoPasaje_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the EstadoPasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.EstadoPasaje_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[EstadoPasaje] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.EstadoPasaje_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.EstadoPasaje_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.EstadoPasaje_GetById
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the EstadoPasaje table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.EstadoPasaje_GetById
(

	@Id int   
)
AS


				SELECT
					[ID],
					[Descripcion]
				FROM
					[dbo].[EstadoPasaje]
				WHERE
					[ID] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.EstadoPasaje_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.EstadoPasaje_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.EstadoPasaje_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the EstadoPasaje table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.EstadoPasaje_Find
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@Descripcion varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ID]
	, [Descripcion]
    FROM
	[dbo].[EstadoPasaje]
    WHERE 
	 ([ID] = @Id OR @Id IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ID]
	, [Descripcion]
    FROM
	[dbo].[EstadoPasaje]
    WHERE 
	 ([ID] = @Id AND @Id is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Paquete_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Paquete_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Paquete_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Paquete table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Paquete_Get_List

AS


				
				SELECT
					[PaqueteID],
					[Descripcion],
					[PrecioCama],
					[Moneda],
					[Iva],
					[Alicuota],
					[Temporada],
					[Cotizacion],
					[Codigo],
					[DestinoID],
					[PrecioSemiCama],
					[Foto],
					[ServiciosParticulares],
					[FechaCreacion],
					[PublicWeb],
					[LastUpdate],
					[ModePublicity]
				FROM
					[dbo].[Paquete]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Paquete_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Paquete_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Paquete_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Paquete table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Paquete_GetPaged
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
				    [PaqueteID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PaqueteID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PaqueteID]'
				SET @SQL = @SQL + ' FROM [dbo].[Paquete]'
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
				SELECT O.[PaqueteID], O.[Descripcion], O.[PrecioCama], O.[Moneda], O.[Iva], O.[Alicuota], O.[Temporada], O.[Cotizacion], O.[Codigo], O.[DestinoID], O.[PrecioSemiCama], O.[Foto], O.[ServiciosParticulares], O.[FechaCreacion], O.[PublicWeb], O.[LastUpdate], O.[ModePublicity]
				FROM
				    [dbo].[Paquete] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PaqueteID] = PageIndex.[PaqueteID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Paquete]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Paquete_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Paquete_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Paquete_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Paquete table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Paquete_Insert
(

	@PaqueteId uniqueidentifier    OUTPUT,

	@Descripcion varchar (100)  ,

	@PrecioCama float   ,

	@Moneda int   ,

	@Iva varchar (50)  ,

	@Alicuota varchar (50)  ,

	@Temporada int   ,

	@Cotizacion float   ,

	@Codigo varchar (50)  ,

	@DestinoId int   ,

	@PrecioSemiCama float   ,

	@Foto varchar (200)  ,

	@ServiciosParticulares varchar (MAX)  ,

	@FechaCreacion datetime   ,

	@PublicWeb bit   ,

	@LastUpdate datetime   ,

	@ModePublicity bit   
)
AS


				
				INSERT INTO [dbo].[Paquete]
					(
					[PaqueteID]
					,[Descripcion]
					,[PrecioCama]
					,[Moneda]
					,[Iva]
					,[Alicuota]
					,[Temporada]
					,[Cotizacion]
					,[Codigo]
					,[DestinoID]
					,[PrecioSemiCama]
					,[Foto]
					,[ServiciosParticulares]
					,[FechaCreacion]
					,[PublicWeb]
					,[LastUpdate]
					,[ModePublicity]
					)
				VALUES
					(
					@PaqueteId
					,@Descripcion
					,@PrecioCama
					,@Moneda
					,@Iva
					,@Alicuota
					,@Temporada
					,@Cotizacion
					,@Codigo
					,@DestinoId
					,@PrecioSemiCama
					,@Foto
					,@ServiciosParticulares
					,@FechaCreacion
					,@PublicWeb
					,@LastUpdate
					,@ModePublicity
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Paquete_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Paquete_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Paquete_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Paquete table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Paquete_Update
(

	@PaqueteId uniqueidentifier   ,

	@OriginalPaqueteId uniqueidentifier   ,

	@Descripcion varchar (100)  ,

	@PrecioCama float   ,

	@Moneda int   ,

	@Iva varchar (50)  ,

	@Alicuota varchar (50)  ,

	@Temporada int   ,

	@Cotizacion float   ,

	@Codigo varchar (50)  ,

	@DestinoId int   ,

	@PrecioSemiCama float   ,

	@Foto varchar (200)  ,

	@ServiciosParticulares varchar (MAX)  ,

	@FechaCreacion datetime   ,

	@PublicWeb bit   ,

	@LastUpdate datetime   ,

	@ModePublicity bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Paquete]
				SET
					[PaqueteID] = @PaqueteId
					,[Descripcion] = @Descripcion
					,[PrecioCama] = @PrecioCama
					,[Moneda] = @Moneda
					,[Iva] = @Iva
					,[Alicuota] = @Alicuota
					,[Temporada] = @Temporada
					,[Cotizacion] = @Cotizacion
					,[Codigo] = @Codigo
					,[DestinoID] = @DestinoId
					,[PrecioSemiCama] = @PrecioSemiCama
					,[Foto] = @Foto
					,[ServiciosParticulares] = @ServiciosParticulares
					,[FechaCreacion] = @FechaCreacion
					,[PublicWeb] = @PublicWeb
					,[LastUpdate] = @LastUpdate
					,[ModePublicity] = @ModePublicity
				WHERE
[PaqueteID] = @OriginalPaqueteId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Paquete_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Paquete_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Paquete_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Paquete table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Paquete_Delete
(

	@PaqueteId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Paquete] WITH (ROWLOCK) 
				WHERE
					[PaqueteID] = @PaqueteId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Paquete_GetByDestinoId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Paquete_GetByDestinoId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Paquete_GetByDestinoId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Paquete table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Paquete_GetByDestinoId
(

	@DestinoId int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaqueteID],
					[Descripcion],
					[PrecioCama],
					[Moneda],
					[Iva],
					[Alicuota],
					[Temporada],
					[Cotizacion],
					[Codigo],
					[DestinoID],
					[PrecioSemiCama],
					[Foto],
					[ServiciosParticulares],
					[FechaCreacion],
					[PublicWeb],
					[LastUpdate],
					[ModePublicity]
				FROM
					[dbo].[Paquete]
				WHERE
					[DestinoID] = @DestinoId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Paquete_GetByPaqueteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Paquete_GetByPaqueteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Paquete_GetByPaqueteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Paquete table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Paquete_GetByPaqueteId
(

	@PaqueteId uniqueidentifier   
)
AS


				SELECT
					[PaqueteID],
					[Descripcion],
					[PrecioCama],
					[Moneda],
					[Iva],
					[Alicuota],
					[Temporada],
					[Cotizacion],
					[Codigo],
					[DestinoID],
					[PrecioSemiCama],
					[Foto],
					[ServiciosParticulares],
					[FechaCreacion],
					[PublicWeb],
					[LastUpdate],
					[ModePublicity]
				FROM
					[dbo].[Paquete]
				WHERE
					[PaqueteID] = @PaqueteId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Paquete_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Paquete_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Paquete_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Paquete table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Paquete_Find
(

	@SearchUsingOR bit   = null ,

	@PaqueteId uniqueidentifier   = null ,

	@Descripcion varchar (100)  = null ,

	@PrecioCama float   = null ,

	@Moneda int   = null ,

	@Iva varchar (50)  = null ,

	@Alicuota varchar (50)  = null ,

	@Temporada int   = null ,

	@Cotizacion float   = null ,

	@Codigo varchar (50)  = null ,

	@DestinoId int   = null ,

	@PrecioSemiCama float   = null ,

	@Foto varchar (200)  = null ,

	@ServiciosParticulares varchar (MAX)  = null ,

	@FechaCreacion datetime   = null ,

	@PublicWeb bit   = null ,

	@LastUpdate datetime   = null ,

	@ModePublicity bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaqueteID]
	, [Descripcion]
	, [PrecioCama]
	, [Moneda]
	, [Iva]
	, [Alicuota]
	, [Temporada]
	, [Cotizacion]
	, [Codigo]
	, [DestinoID]
	, [PrecioSemiCama]
	, [Foto]
	, [ServiciosParticulares]
	, [FechaCreacion]
	, [PublicWeb]
	, [LastUpdate]
	, [ModePublicity]
    FROM
	[dbo].[Paquete]
    WHERE 
	 ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
	AND ([PrecioCama] = @PrecioCama OR @PrecioCama IS NULL)
	AND ([Moneda] = @Moneda OR @Moneda IS NULL)
	AND ([Iva] = @Iva OR @Iva IS NULL)
	AND ([Alicuota] = @Alicuota OR @Alicuota IS NULL)
	AND ([Temporada] = @Temporada OR @Temporada IS NULL)
	AND ([Cotizacion] = @Cotizacion OR @Cotizacion IS NULL)
	AND ([Codigo] = @Codigo OR @Codigo IS NULL)
	AND ([DestinoID] = @DestinoId OR @DestinoId IS NULL)
	AND ([PrecioSemiCama] = @PrecioSemiCama OR @PrecioSemiCama IS NULL)
	AND ([Foto] = @Foto OR @Foto IS NULL)
	AND ([ServiciosParticulares] = @ServiciosParticulares OR @ServiciosParticulares IS NULL)
	AND ([FechaCreacion] = @FechaCreacion OR @FechaCreacion IS NULL)
	AND ([PublicWeb] = @PublicWeb OR @PublicWeb IS NULL)
	AND ([LastUpdate] = @LastUpdate OR @LastUpdate IS NULL)
	AND ([ModePublicity] = @ModePublicity OR @ModePublicity IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaqueteID]
	, [Descripcion]
	, [PrecioCama]
	, [Moneda]
	, [Iva]
	, [Alicuota]
	, [Temporada]
	, [Cotizacion]
	, [Codigo]
	, [DestinoID]
	, [PrecioSemiCama]
	, [Foto]
	, [ServiciosParticulares]
	, [FechaCreacion]
	, [PublicWeb]
	, [LastUpdate]
	, [ModePublicity]
    FROM
	[dbo].[Paquete]
    WHERE 
	 ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	OR ([PrecioCama] = @PrecioCama AND @PrecioCama is not null)
	OR ([Moneda] = @Moneda AND @Moneda is not null)
	OR ([Iva] = @Iva AND @Iva is not null)
	OR ([Alicuota] = @Alicuota AND @Alicuota is not null)
	OR ([Temporada] = @Temporada AND @Temporada is not null)
	OR ([Cotizacion] = @Cotizacion AND @Cotizacion is not null)
	OR ([Codigo] = @Codigo AND @Codigo is not null)
	OR ([DestinoID] = @DestinoId AND @DestinoId is not null)
	OR ([PrecioSemiCama] = @PrecioSemiCama AND @PrecioSemiCama is not null)
	OR ([Foto] = @Foto AND @Foto is not null)
	OR ([ServiciosParticulares] = @ServiciosParticulares AND @ServiciosParticulares is not null)
	OR ([FechaCreacion] = @FechaCreacion AND @FechaCreacion is not null)
	OR ([PublicWeb] = @PublicWeb AND @PublicWeb is not null)
	OR ([LastUpdate] = @LastUpdate AND @LastUpdate is not null)
	OR ([ModePublicity] = @ModePublicity AND @ModePublicity is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Excursion_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Excursion_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Excursion_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Excursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Excursion_Get_List

AS


				
				SELECT
					[ExcursionID],
					[Descripcion],
					[Costo],
					[Observaciones],
					[ProveedorID]
				FROM
					[dbo].[Excursion]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Excursion_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Excursion_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Excursion_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Excursion table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Excursion_GetPaged
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
				    [ExcursionID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ExcursionID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ExcursionID]'
				SET @SQL = @SQL + ' FROM [dbo].[Excursion]'
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
				SELECT O.[ExcursionID], O.[Descripcion], O.[Costo], O.[Observaciones], O.[ProveedorID]
				FROM
				    [dbo].[Excursion] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ExcursionID] = PageIndex.[ExcursionID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Excursion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Excursion_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Excursion_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Excursion_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Excursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Excursion_Insert
(

	@ExcursionId uniqueidentifier    OUTPUT,

	@Descripcion varchar (200)  ,

	@Costo float   ,

	@Observaciones varchar (MAX)  ,

	@ProveedorId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[Excursion]
					(
					[ExcursionID]
					,[Descripcion]
					,[Costo]
					,[Observaciones]
					,[ProveedorID]
					)
				VALUES
					(
					@ExcursionId
					,@Descripcion
					,@Costo
					,@Observaciones
					,@ProveedorId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Excursion_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Excursion_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Excursion_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Excursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Excursion_Update
(

	@ExcursionId uniqueidentifier   ,

	@OriginalExcursionId uniqueidentifier   ,

	@Descripcion varchar (200)  ,

	@Costo float   ,

	@Observaciones varchar (MAX)  ,

	@ProveedorId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Excursion]
				SET
					[ExcursionID] = @ExcursionId
					,[Descripcion] = @Descripcion
					,[Costo] = @Costo
					,[Observaciones] = @Observaciones
					,[ProveedorID] = @ProveedorId
				WHERE
[ExcursionID] = @OriginalExcursionId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Excursion_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Excursion_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Excursion_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Excursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Excursion_Delete
(

	@ExcursionId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Excursion] WITH (ROWLOCK) 
				WHERE
					[ExcursionID] = @ExcursionId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Excursion_GetByProveedorId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Excursion_GetByProveedorId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Excursion_GetByProveedorId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Excursion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Excursion_GetByProveedorId
(

	@ProveedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ExcursionID],
					[Descripcion],
					[Costo],
					[Observaciones],
					[ProveedorID]
				FROM
					[dbo].[Excursion]
				WHERE
					[ProveedorID] = @ProveedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Excursion_GetByExcursionId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Excursion_GetByExcursionId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Excursion_GetByExcursionId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Excursion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Excursion_GetByExcursionId
(

	@ExcursionId uniqueidentifier   
)
AS


				SELECT
					[ExcursionID],
					[Descripcion],
					[Costo],
					[Observaciones],
					[ProveedorID]
				FROM
					[dbo].[Excursion]
				WHERE
					[ExcursionID] = @ExcursionId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Excursion_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Excursion_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Excursion_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Excursion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Excursion_Find
(

	@SearchUsingOR bit   = null ,

	@ExcursionId uniqueidentifier   = null ,

	@Descripcion varchar (200)  = null ,

	@Costo float   = null ,

	@Observaciones varchar (MAX)  = null ,

	@ProveedorId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ExcursionID]
	, [Descripcion]
	, [Costo]
	, [Observaciones]
	, [ProveedorID]
    FROM
	[dbo].[Excursion]
    WHERE 
	 ([ExcursionID] = @ExcursionId OR @ExcursionId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
	AND ([Costo] = @Costo OR @Costo IS NULL)
	AND ([Observaciones] = @Observaciones OR @Observaciones IS NULL)
	AND ([ProveedorID] = @ProveedorId OR @ProveedorId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ExcursionID]
	, [Descripcion]
	, [Costo]
	, [Observaciones]
	, [ProveedorID]
    FROM
	[dbo].[Excursion]
    WHERE 
	 ([ExcursionID] = @ExcursionId AND @ExcursionId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	OR ([Costo] = @Costo AND @Costo is not null)
	OR ([Observaciones] = @Observaciones AND @Observaciones is not null)
	OR ([ProveedorID] = @ProveedorId AND @ProveedorId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Habitacion_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Habitacion_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Habitacion_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Habitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Habitacion_Get_List

AS


				
				SELECT
					[HabitacionID],
					[NroHabitacion],
					[Tipo],
					[HotelID],
					[Estado],
					[Capacidad],
					[Ocupacion],
					[Nombre]
				FROM
					[dbo].[Habitacion]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Habitacion_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Habitacion_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Habitacion_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Habitacion table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Habitacion_GetPaged
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
				    [HabitacionID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([HabitacionID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [HabitacionID]'
				SET @SQL = @SQL + ' FROM [dbo].[Habitacion]'
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
				SELECT O.[HabitacionID], O.[NroHabitacion], O.[Tipo], O.[HotelID], O.[Estado], O.[Capacidad], O.[Ocupacion], O.[Nombre]
				FROM
				    [dbo].[Habitacion] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[HabitacionID] = PageIndex.[HabitacionID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Habitacion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Habitacion_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Habitacion_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Habitacion_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Habitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Habitacion_Insert
(

	@HabitacionId uniqueidentifier    OUTPUT,

	@NroHabitacion int   ,

	@Tipo int   ,

	@HotelId uniqueidentifier   ,

	@Estado int   ,

	@Capacidad int   ,

	@Ocupacion int   ,

	@Nombre varchar (50)  
)
AS


				
				INSERT INTO [dbo].[Habitacion]
					(
					[HabitacionID]
					,[NroHabitacion]
					,[Tipo]
					,[HotelID]
					,[Estado]
					,[Capacidad]
					,[Ocupacion]
					,[Nombre]
					)
				VALUES
					(
					@HabitacionId
					,@NroHabitacion
					,@Tipo
					,@HotelId
					,@Estado
					,@Capacidad
					,@Ocupacion
					,@Nombre
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Habitacion_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Habitacion_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Habitacion_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Habitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Habitacion_Update
(

	@HabitacionId uniqueidentifier   ,

	@OriginalHabitacionId uniqueidentifier   ,

	@NroHabitacion int   ,

	@Tipo int   ,

	@HotelId uniqueidentifier   ,

	@Estado int   ,

	@Capacidad int   ,

	@Ocupacion int   ,

	@Nombre varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Habitacion]
				SET
					[HabitacionID] = @HabitacionId
					,[NroHabitacion] = @NroHabitacion
					,[Tipo] = @Tipo
					,[HotelID] = @HotelId
					,[Estado] = @Estado
					,[Capacidad] = @Capacidad
					,[Ocupacion] = @Ocupacion
					,[Nombre] = @Nombre
				WHERE
[HabitacionID] = @OriginalHabitacionId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Habitacion_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Habitacion_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Habitacion_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Habitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Habitacion_Delete
(

	@HabitacionId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Habitacion] WITH (ROWLOCK) 
				WHERE
					[HabitacionID] = @HabitacionId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Habitacion_GetByHotelId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Habitacion_GetByHotelId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Habitacion_GetByHotelId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Habitacion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Habitacion_GetByHotelId
(

	@HotelId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[HabitacionID],
					[NroHabitacion],
					[Tipo],
					[HotelID],
					[Estado],
					[Capacidad],
					[Ocupacion],
					[Nombre]
				FROM
					[dbo].[Habitacion]
				WHERE
					[HotelID] = @HotelId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Habitacion_GetByHabitacionId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Habitacion_GetByHabitacionId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Habitacion_GetByHabitacionId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Habitacion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Habitacion_GetByHabitacionId
(

	@HabitacionId uniqueidentifier   
)
AS


				SELECT
					[HabitacionID],
					[NroHabitacion],
					[Tipo],
					[HotelID],
					[Estado],
					[Capacidad],
					[Ocupacion],
					[Nombre]
				FROM
					[dbo].[Habitacion]
				WHERE
					[HabitacionID] = @HabitacionId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Habitacion_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Habitacion_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Habitacion_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Habitacion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Habitacion_Find
(

	@SearchUsingOR bit   = null ,

	@HabitacionId uniqueidentifier   = null ,

	@NroHabitacion int   = null ,

	@Tipo int   = null ,

	@HotelId uniqueidentifier   = null ,

	@Estado int   = null ,

	@Capacidad int   = null ,

	@Ocupacion int   = null ,

	@Nombre varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [HabitacionID]
	, [NroHabitacion]
	, [Tipo]
	, [HotelID]
	, [Estado]
	, [Capacidad]
	, [Ocupacion]
	, [Nombre]
    FROM
	[dbo].[Habitacion]
    WHERE 
	 ([HabitacionID] = @HabitacionId OR @HabitacionId IS NULL)
	AND ([NroHabitacion] = @NroHabitacion OR @NroHabitacion IS NULL)
	AND ([Tipo] = @Tipo OR @Tipo IS NULL)
	AND ([HotelID] = @HotelId OR @HotelId IS NULL)
	AND ([Estado] = @Estado OR @Estado IS NULL)
	AND ([Capacidad] = @Capacidad OR @Capacidad IS NULL)
	AND ([Ocupacion] = @Ocupacion OR @Ocupacion IS NULL)
	AND ([Nombre] = @Nombre OR @Nombre IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [HabitacionID]
	, [NroHabitacion]
	, [Tipo]
	, [HotelID]
	, [Estado]
	, [Capacidad]
	, [Ocupacion]
	, [Nombre]
    FROM
	[dbo].[Habitacion]
    WHERE 
	 ([HabitacionID] = @HabitacionId AND @HabitacionId is not null)
	OR ([NroHabitacion] = @NroHabitacion AND @NroHabitacion is not null)
	OR ([Tipo] = @Tipo AND @Tipo is not null)
	OR ([HotelID] = @HotelId AND @HotelId is not null)
	OR ([Estado] = @Estado AND @Estado is not null)
	OR ([Capacidad] = @Capacidad AND @Capacidad is not null)
	OR ([Ocupacion] = @Ocupacion AND @Ocupacion is not null)
	OR ([Nombre] = @Nombre AND @Nombre is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.HabitacionTipo_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.HabitacionTipo_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.HabitacionTipo_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the HabitacionTipo table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.HabitacionTipo_Get_List

AS


				
				SELECT
					[Id],
					[Descripcion]
				FROM
					[dbo].[HabitacionTipo]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.HabitacionTipo_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.HabitacionTipo_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.HabitacionTipo_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the HabitacionTipo table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.HabitacionTipo_GetPaged
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
				    [Id] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[HabitacionTipo]'
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
				SELECT O.[Id], O.[Descripcion]
				FROM
				    [dbo].[HabitacionTipo] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[HabitacionTipo]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.HabitacionTipo_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.HabitacionTipo_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.HabitacionTipo_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the HabitacionTipo table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.HabitacionTipo_Insert
(

	@Id int    OUTPUT,

	@Descripcion varchar (50)  
)
AS


				
				INSERT INTO [dbo].[HabitacionTipo]
					(
					[Descripcion]
					)
				VALUES
					(
					@Descripcion
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.HabitacionTipo_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.HabitacionTipo_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.HabitacionTipo_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the HabitacionTipo table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.HabitacionTipo_Update
(

	@Id int   ,

	@Descripcion varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[HabitacionTipo]
				SET
					[Descripcion] = @Descripcion
				WHERE
[Id] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.HabitacionTipo_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.HabitacionTipo_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.HabitacionTipo_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the HabitacionTipo table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.HabitacionTipo_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[HabitacionTipo] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.HabitacionTipo_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.HabitacionTipo_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.HabitacionTipo_GetById
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the HabitacionTipo table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.HabitacionTipo_GetById
(

	@Id int   
)
AS


				SELECT
					[Id],
					[Descripcion]
				FROM
					[dbo].[HabitacionTipo]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.HabitacionTipo_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.HabitacionTipo_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.HabitacionTipo_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the HabitacionTipo table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.HabitacionTipo_Find
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@Descripcion varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [Descripcion]
    FROM
	[dbo].[HabitacionTipo]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [Descripcion]
    FROM
	[dbo].[HabitacionTipo]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Historial_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Historial_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Historial_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Historial table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Historial_Get_List

AS


				
				SELECT
					[HistorialID],
					[Tabla],
					[Operacion],
					[FechaHoraRegistro],
					[Cliente],
					[Vendedor],
					[Observaciones],
					[Monto]
				FROM
					[dbo].[Historial]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Historial_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Historial_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Historial_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Historial table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Historial_GetPaged
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
				    [HistorialID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([HistorialID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [HistorialID]'
				SET @SQL = @SQL + ' FROM [dbo].[Historial]'
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
				SELECT O.[HistorialID], O.[Tabla], O.[Operacion], O.[FechaHoraRegistro], O.[Cliente], O.[Vendedor], O.[Observaciones], O.[Monto]
				FROM
				    [dbo].[Historial] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[HistorialID] = PageIndex.[HistorialID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Historial]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Historial_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Historial_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Historial_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Historial table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Historial_Insert
(

	@HistorialId uniqueidentifier   ,

	@Tabla int   ,

	@Operacion int   ,

	@FechaHoraRegistro datetime   ,

	@Cliente uniqueidentifier   ,

	@Vendedor uniqueidentifier   ,

	@Observaciones varchar (MAX)  ,

	@Monto float   
)
AS


				
				INSERT INTO [dbo].[Historial]
					(
					[HistorialID]
					,[Tabla]
					,[Operacion]
					,[FechaHoraRegistro]
					,[Cliente]
					,[Vendedor]
					,[Observaciones]
					,[Monto]
					)
				VALUES
					(
					@HistorialId
					,@Tabla
					,@Operacion
					,@FechaHoraRegistro
					,@Cliente
					,@Vendedor
					,@Observaciones
					,@Monto
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Historial_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Historial_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Historial_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Historial table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Historial_Update
(

	@HistorialId uniqueidentifier   ,

	@OriginalHistorialId uniqueidentifier   ,

	@Tabla int   ,

	@Operacion int   ,

	@FechaHoraRegistro datetime   ,

	@Cliente uniqueidentifier   ,

	@Vendedor uniqueidentifier   ,

	@Observaciones varchar (MAX)  ,

	@Monto float   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Historial]
				SET
					[HistorialID] = @HistorialId
					,[Tabla] = @Tabla
					,[Operacion] = @Operacion
					,[FechaHoraRegistro] = @FechaHoraRegistro
					,[Cliente] = @Cliente
					,[Vendedor] = @Vendedor
					,[Observaciones] = @Observaciones
					,[Monto] = @Monto
				WHERE
[HistorialID] = @OriginalHistorialId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Historial_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Historial_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Historial_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Historial table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Historial_Delete
(

	@HistorialId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Historial] WITH (ROWLOCK) 
				WHERE
					[HistorialID] = @HistorialId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Historial_GetByHistorialId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Historial_GetByHistorialId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Historial_GetByHistorialId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Historial table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Historial_GetByHistorialId
(

	@HistorialId uniqueidentifier   
)
AS


				SELECT
					[HistorialID],
					[Tabla],
					[Operacion],
					[FechaHoraRegistro],
					[Cliente],
					[Vendedor],
					[Observaciones],
					[Monto]
				FROM
					[dbo].[Historial]
				WHERE
					[HistorialID] = @HistorialId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Historial_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Historial_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Historial_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Historial table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Historial_Find
(

	@SearchUsingOR bit   = null ,

	@HistorialId uniqueidentifier   = null ,

	@Tabla int   = null ,

	@Operacion int   = null ,

	@FechaHoraRegistro datetime   = null ,

	@Cliente uniqueidentifier   = null ,

	@Vendedor uniqueidentifier   = null ,

	@Observaciones varchar (MAX)  = null ,

	@Monto float   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [HistorialID]
	, [Tabla]
	, [Operacion]
	, [FechaHoraRegistro]
	, [Cliente]
	, [Vendedor]
	, [Observaciones]
	, [Monto]
    FROM
	[dbo].[Historial]
    WHERE 
	 ([HistorialID] = @HistorialId OR @HistorialId IS NULL)
	AND ([Tabla] = @Tabla OR @Tabla IS NULL)
	AND ([Operacion] = @Operacion OR @Operacion IS NULL)
	AND ([FechaHoraRegistro] = @FechaHoraRegistro OR @FechaHoraRegistro IS NULL)
	AND ([Cliente] = @Cliente OR @Cliente IS NULL)
	AND ([Vendedor] = @Vendedor OR @Vendedor IS NULL)
	AND ([Observaciones] = @Observaciones OR @Observaciones IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [HistorialID]
	, [Tabla]
	, [Operacion]
	, [FechaHoraRegistro]
	, [Cliente]
	, [Vendedor]
	, [Observaciones]
	, [Monto]
    FROM
	[dbo].[Historial]
    WHERE 
	 ([HistorialID] = @HistorialId AND @HistorialId is not null)
	OR ([Tabla] = @Tabla AND @Tabla is not null)
	OR ([Operacion] = @Operacion AND @Operacion is not null)
	OR ([FechaHoraRegistro] = @FechaHoraRegistro AND @FechaHoraRegistro is not null)
	OR ([Cliente] = @Cliente AND @Cliente is not null)
	OR ([Vendedor] = @Vendedor AND @Vendedor is not null)
	OR ([Observaciones] = @Observaciones AND @Observaciones is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Localidad_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Localidad_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Localidad_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Localidad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Localidad_Get_List

AS


				
				SELECT
					[ID],
					[idDepartamento],
					[Nombre]
				FROM
					[dbo].[Localidad]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Localidad_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Localidad_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Localidad_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Localidad table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Localidad_GetPaged
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
				    [ID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ID]'
				SET @SQL = @SQL + ' FROM [dbo].[Localidad]'
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
				SELECT O.[ID], O.[idDepartamento], O.[Nombre]
				FROM
				    [dbo].[Localidad] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ID] = PageIndex.[ID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Localidad]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Localidad_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Localidad_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Localidad_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Localidad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Localidad_Insert
(

	@Id int    OUTPUT,

	@IdDepartamento int   ,

	@Nombre nvarchar (250)  
)
AS


				
				INSERT INTO [dbo].[Localidad]
					(
					[idDepartamento]
					,[Nombre]
					)
				VALUES
					(
					@IdDepartamento
					,@Nombre
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Localidad_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Localidad_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Localidad_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Localidad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Localidad_Update
(

	@Id int   ,

	@IdDepartamento int   ,

	@Nombre nvarchar (250)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Localidad]
				SET
					[idDepartamento] = @IdDepartamento
					,[Nombre] = @Nombre
				WHERE
[ID] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Localidad_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Localidad_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Localidad_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Localidad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Localidad_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[Localidad] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Localidad_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Localidad_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Localidad_GetById
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Localidad table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Localidad_GetById
(

	@Id int   
)
AS


				SELECT
					[ID],
					[idDepartamento],
					[Nombre]
				FROM
					[dbo].[Localidad]
				WHERE
					[ID] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Localidad_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Localidad_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Localidad_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Localidad table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Localidad_Find
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
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Hotel_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Hotel_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Hotel_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Hotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Hotel_Get_List

AS


				
				SELECT
					[HotelID],
					[Nombre],
					[Direccion],
					[CP],
					[Telefono],
					[Email],
					[Contacto],
					[CantidadHabitaciones],
					[Categoria],
					[Child1],
					[Child2],
					[ChildHabitacion],
					[CheckIn],
					[CheckOut],
					[GoogleMapHtml],
					[LocalidadID]
				FROM
					[dbo].[Hotel]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Hotel_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Hotel_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Hotel_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Hotel table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Hotel_GetPaged
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
				    [HotelID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([HotelID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [HotelID]'
				SET @SQL = @SQL + ' FROM [dbo].[Hotel]'
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
				SELECT O.[HotelID], O.[Nombre], O.[Direccion], O.[CP], O.[Telefono], O.[Email], O.[Contacto], O.[CantidadHabitaciones], O.[Categoria], O.[Child1], O.[Child2], O.[ChildHabitacion], O.[CheckIn], O.[CheckOut], O.[GoogleMapHtml], O.[LocalidadID]
				FROM
				    [dbo].[Hotel] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[HotelID] = PageIndex.[HotelID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Hotel]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Hotel_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Hotel_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Hotel_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Hotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Hotel_Insert
(

	@HotelId uniqueidentifier    OUTPUT,

	@Nombre varchar (50)  ,

	@Direccion varchar (50)  ,

	@Cp varchar (50)  ,

	@Telefono varchar (50)  ,

	@Email varchar (50)  ,

	@Contacto varchar (50)  ,

	@CantidadHabitaciones int   ,

	@Categoria int   ,

	@Child1 varchar (50)  ,

	@Child2 varchar (50)  ,

	@ChildHabitacion int   ,

	@CheckIn varchar (8)  ,

	@CheckOut varchar (8)  ,

	@GoogleMapHtml varchar (200)  ,

	@LocalidadId int   
)
AS


				
				INSERT INTO [dbo].[Hotel]
					(
					[HotelID]
					,[Nombre]
					,[Direccion]
					,[CP]
					,[Telefono]
					,[Email]
					,[Contacto]
					,[CantidadHabitaciones]
					,[Categoria]
					,[Child1]
					,[Child2]
					,[ChildHabitacion]
					,[CheckIn]
					,[CheckOut]
					,[GoogleMapHtml]
					,[LocalidadID]
					)
				VALUES
					(
					@HotelId
					,@Nombre
					,@Direccion
					,@Cp
					,@Telefono
					,@Email
					,@Contacto
					,@CantidadHabitaciones
					,@Categoria
					,@Child1
					,@Child2
					,@ChildHabitacion
					,@CheckIn
					,@CheckOut
					,@GoogleMapHtml
					,@LocalidadId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Hotel_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Hotel_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Hotel_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Hotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Hotel_Update
(

	@HotelId uniqueidentifier   ,

	@OriginalHotelId uniqueidentifier   ,

	@Nombre varchar (50)  ,

	@Direccion varchar (50)  ,

	@Cp varchar (50)  ,

	@Telefono varchar (50)  ,

	@Email varchar (50)  ,

	@Contacto varchar (50)  ,

	@CantidadHabitaciones int   ,

	@Categoria int   ,

	@Child1 varchar (50)  ,

	@Child2 varchar (50)  ,

	@ChildHabitacion int   ,

	@CheckIn varchar (8)  ,

	@CheckOut varchar (8)  ,

	@GoogleMapHtml varchar (200)  ,

	@LocalidadId int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Hotel]
				SET
					[HotelID] = @HotelId
					,[Nombre] = @Nombre
					,[Direccion] = @Direccion
					,[CP] = @Cp
					,[Telefono] = @Telefono
					,[Email] = @Email
					,[Contacto] = @Contacto
					,[CantidadHabitaciones] = @CantidadHabitaciones
					,[Categoria] = @Categoria
					,[Child1] = @Child1
					,[Child2] = @Child2
					,[ChildHabitacion] = @ChildHabitacion
					,[CheckIn] = @CheckIn
					,[CheckOut] = @CheckOut
					,[GoogleMapHtml] = @GoogleMapHtml
					,[LocalidadID] = @LocalidadId
				WHERE
[HotelID] = @OriginalHotelId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Hotel_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Hotel_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Hotel_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Hotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Hotel_Delete
(

	@HotelId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Hotel] WITH (ROWLOCK) 
				WHERE
					[HotelID] = @HotelId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Hotel_GetByLocalidadId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Hotel_GetByLocalidadId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Hotel_GetByLocalidadId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Hotel table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Hotel_GetByLocalidadId
(

	@LocalidadId int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[HotelID],
					[Nombre],
					[Direccion],
					[CP],
					[Telefono],
					[Email],
					[Contacto],
					[CantidadHabitaciones],
					[Categoria],
					[Child1],
					[Child2],
					[ChildHabitacion],
					[CheckIn],
					[CheckOut],
					[GoogleMapHtml],
					[LocalidadID]
				FROM
					[dbo].[Hotel]
				WHERE
					[LocalidadID] = @LocalidadId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Hotel_GetByHotelId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Hotel_GetByHotelId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Hotel_GetByHotelId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Hotel table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Hotel_GetByHotelId
(

	@HotelId uniqueidentifier   
)
AS


				SELECT
					[HotelID],
					[Nombre],
					[Direccion],
					[CP],
					[Telefono],
					[Email],
					[Contacto],
					[CantidadHabitaciones],
					[Categoria],
					[Child1],
					[Child2],
					[ChildHabitacion],
					[CheckIn],
					[CheckOut],
					[GoogleMapHtml],
					[LocalidadID]
				FROM
					[dbo].[Hotel]
				WHERE
					[HotelID] = @HotelId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Hotel_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Hotel_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Hotel_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Hotel table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Hotel_Find
(

	@SearchUsingOR bit   = null ,

	@HotelId uniqueidentifier   = null ,

	@Nombre varchar (50)  = null ,

	@Direccion varchar (50)  = null ,

	@Cp varchar (50)  = null ,

	@Telefono varchar (50)  = null ,

	@Email varchar (50)  = null ,

	@Contacto varchar (50)  = null ,

	@CantidadHabitaciones int   = null ,

	@Categoria int   = null ,

	@Child1 varchar (50)  = null ,

	@Child2 varchar (50)  = null ,

	@ChildHabitacion int   = null ,

	@CheckIn varchar (8)  = null ,

	@CheckOut varchar (8)  = null ,

	@GoogleMapHtml varchar (200)  = null ,

	@LocalidadId int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [HotelID]
	, [Nombre]
	, [Direccion]
	, [CP]
	, [Telefono]
	, [Email]
	, [Contacto]
	, [CantidadHabitaciones]
	, [Categoria]
	, [Child1]
	, [Child2]
	, [ChildHabitacion]
	, [CheckIn]
	, [CheckOut]
	, [GoogleMapHtml]
	, [LocalidadID]
    FROM
	[dbo].[Hotel]
    WHERE 
	 ([HotelID] = @HotelId OR @HotelId IS NULL)
	AND ([Nombre] = @Nombre OR @Nombre IS NULL)
	AND ([Direccion] = @Direccion OR @Direccion IS NULL)
	AND ([CP] = @Cp OR @Cp IS NULL)
	AND ([Telefono] = @Telefono OR @Telefono IS NULL)
	AND ([Email] = @Email OR @Email IS NULL)
	AND ([Contacto] = @Contacto OR @Contacto IS NULL)
	AND ([CantidadHabitaciones] = @CantidadHabitaciones OR @CantidadHabitaciones IS NULL)
	AND ([Categoria] = @Categoria OR @Categoria IS NULL)
	AND ([Child1] = @Child1 OR @Child1 IS NULL)
	AND ([Child2] = @Child2 OR @Child2 IS NULL)
	AND ([ChildHabitacion] = @ChildHabitacion OR @ChildHabitacion IS NULL)
	AND ([CheckIn] = @CheckIn OR @CheckIn IS NULL)
	AND ([CheckOut] = @CheckOut OR @CheckOut IS NULL)
	AND ([GoogleMapHtml] = @GoogleMapHtml OR @GoogleMapHtml IS NULL)
	AND ([LocalidadID] = @LocalidadId OR @LocalidadId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [HotelID]
	, [Nombre]
	, [Direccion]
	, [CP]
	, [Telefono]
	, [Email]
	, [Contacto]
	, [CantidadHabitaciones]
	, [Categoria]
	, [Child1]
	, [Child2]
	, [ChildHabitacion]
	, [CheckIn]
	, [CheckOut]
	, [GoogleMapHtml]
	, [LocalidadID]
    FROM
	[dbo].[Hotel]
    WHERE 
	 ([HotelID] = @HotelId AND @HotelId is not null)
	OR ([Nombre] = @Nombre AND @Nombre is not null)
	OR ([Direccion] = @Direccion AND @Direccion is not null)
	OR ([CP] = @Cp AND @Cp is not null)
	OR ([Telefono] = @Telefono AND @Telefono is not null)
	OR ([Email] = @Email AND @Email is not null)
	OR ([Contacto] = @Contacto AND @Contacto is not null)
	OR ([CantidadHabitaciones] = @CantidadHabitaciones AND @CantidadHabitaciones is not null)
	OR ([Categoria] = @Categoria AND @Categoria is not null)
	OR ([Child1] = @Child1 AND @Child1 is not null)
	OR ([Child2] = @Child2 AND @Child2 is not null)
	OR ([ChildHabitacion] = @ChildHabitacion AND @ChildHabitacion is not null)
	OR ([CheckIn] = @CheckIn AND @CheckIn is not null)
	OR ([CheckOut] = @CheckOut AND @CheckOut is not null)
	OR ([GoogleMapHtml] = @GoogleMapHtml AND @GoogleMapHtml is not null)
	OR ([LocalidadID] = @LocalidadId AND @LocalidadId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeAdicional_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeAdicional_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeAdicional_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PasajeAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeAdicional_Get_List

AS


				
				SELECT
					[PasajeAdicionalID],
					[PasajeID],
					[AdicionalID]
				FROM
					[dbo].[PasajeAdicional]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeAdicional_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeAdicional_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeAdicional_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PasajeAdicional table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeAdicional_GetPaged
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
				    [PasajeAdicionalID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PasajeAdicionalID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PasajeAdicionalID]'
				SET @SQL = @SQL + ' FROM [dbo].[PasajeAdicional]'
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
				SELECT O.[PasajeAdicionalID], O.[PasajeID], O.[AdicionalID]
				FROM
				    [dbo].[PasajeAdicional] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PasajeAdicionalID] = PageIndex.[PasajeAdicionalID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[PasajeAdicional]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeAdicional_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeAdicional_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeAdicional_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the PasajeAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeAdicional_Insert
(

	@PasajeAdicionalId uniqueidentifier    OUTPUT,

	@PasajeId uniqueidentifier   ,

	@AdicionalId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PasajeAdicional]
					(
					[PasajeAdicionalID]
					,[PasajeID]
					,[AdicionalID]
					)
				VALUES
					(
					@PasajeAdicionalId
					,@PasajeId
					,@AdicionalId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeAdicional_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeAdicional_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeAdicional_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the PasajeAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeAdicional_Update
(

	@PasajeAdicionalId uniqueidentifier   ,

	@OriginalPasajeAdicionalId uniqueidentifier   ,

	@PasajeId uniqueidentifier   ,

	@AdicionalId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PasajeAdicional]
				SET
					[PasajeAdicionalID] = @PasajeAdicionalId
					,[PasajeID] = @PasajeId
					,[AdicionalID] = @AdicionalId
				WHERE
[PasajeAdicionalID] = @OriginalPasajeAdicionalId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeAdicional_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeAdicional_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeAdicional_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the PasajeAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeAdicional_Delete
(

	@PasajeAdicionalId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PasajeAdicional] WITH (ROWLOCK) 
				WHERE
					[PasajeAdicionalID] = @PasajeAdicionalId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeAdicional_GetByAdicionalId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeAdicional_GetByAdicionalId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeAdicional_GetByAdicionalId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PasajeAdicional table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeAdicional_GetByAdicionalId
(

	@AdicionalId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeAdicionalID],
					[PasajeID],
					[AdicionalID]
				FROM
					[dbo].[PasajeAdicional]
				WHERE
					[AdicionalID] = @AdicionalId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeAdicional_GetByPasajeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeAdicional_GetByPasajeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeAdicional_GetByPasajeId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PasajeAdicional table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeAdicional_GetByPasajeId
(

	@PasajeId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeAdicionalID],
					[PasajeID],
					[AdicionalID]
				FROM
					[dbo].[PasajeAdicional]
				WHERE
					[PasajeID] = @PasajeId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeAdicional_GetByPasajeAdicionalId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeAdicional_GetByPasajeAdicionalId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeAdicional_GetByPasajeAdicionalId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the PasajeAdicional table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeAdicional_GetByPasajeAdicionalId
(

	@PasajeAdicionalId uniqueidentifier   
)
AS


				SELECT
					[PasajeAdicionalID],
					[PasajeID],
					[AdicionalID]
				FROM
					[dbo].[PasajeAdicional]
				WHERE
					[PasajeAdicionalID] = @PasajeAdicionalId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeAdicional_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeAdicional_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeAdicional_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the PasajeAdicional table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeAdicional_Find
(

	@SearchUsingOR bit   = null ,

	@PasajeAdicionalId uniqueidentifier   = null ,

	@PasajeId uniqueidentifier   = null ,

	@AdicionalId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PasajeAdicionalID]
	, [PasajeID]
	, [AdicionalID]
    FROM
	[dbo].[PasajeAdicional]
    WHERE 
	 ([PasajeAdicionalID] = @PasajeAdicionalId OR @PasajeAdicionalId IS NULL)
	AND ([PasajeID] = @PasajeId OR @PasajeId IS NULL)
	AND ([AdicionalID] = @AdicionalId OR @AdicionalId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PasajeAdicionalID]
	, [PasajeID]
	, [AdicionalID]
    FROM
	[dbo].[PasajeAdicional]
    WHERE 
	 ([PasajeAdicionalID] = @PasajeAdicionalId AND @PasajeAdicionalId is not null)
	OR ([PasajeID] = @PasajeId AND @PasajeId is not null)
	OR ([AdicionalID] = @AdicionalId AND @AdicionalId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Nota_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Nota_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Nota_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Nota table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Nota_Get_List

AS


				
				SELECT
					[NotaID],
					[PorcentajeRetencion],
					[MontoRetencion],
					[Fecha],
					[Dias],
					[ClienteID],
					[VendedorID],
					[NroNota],
					[MontoNota]
				FROM
					[dbo].[Nota]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Nota_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Nota_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Nota_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Nota table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Nota_GetPaged
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
				SELECT O.[NotaID], O.[PorcentajeRetencion], O.[MontoRetencion], O.[Fecha], O.[Dias], O.[ClienteID], O.[VendedorID], O.[NroNota], O.[MontoNota]
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
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Nota_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Nota_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Nota_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Nota table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Nota_Insert
(

	@NotaId uniqueidentifier   ,

	@PorcentajeRetencion float   ,

	@MontoRetencion float   ,

	@Fecha date   ,

	@Dias int   ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@NroNota varchar (50)  ,

	@MontoNota float   
)
AS


				
				INSERT INTO [dbo].[Nota]
					(
					[NotaID]
					,[PorcentajeRetencion]
					,[MontoRetencion]
					,[Fecha]
					,[Dias]
					,[ClienteID]
					,[VendedorID]
					,[NroNota]
					,[MontoNota]
					)
				VALUES
					(
					@NotaId
					,@PorcentajeRetencion
					,@MontoRetencion
					,@Fecha
					,@Dias
					,@ClienteId
					,@VendedorId
					,@NroNota
					,@MontoNota
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Nota_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Nota_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Nota_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Nota table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Nota_Update
(

	@NotaId uniqueidentifier   ,

	@OriginalNotaId uniqueidentifier   ,

	@PorcentajeRetencion float   ,

	@MontoRetencion float   ,

	@Fecha date   ,

	@Dias int   ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@NroNota varchar (50)  ,

	@MontoNota float   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Nota]
				SET
					[NotaID] = @NotaId
					,[PorcentajeRetencion] = @PorcentajeRetencion
					,[MontoRetencion] = @MontoRetencion
					,[Fecha] = @Fecha
					,[Dias] = @Dias
					,[ClienteID] = @ClienteId
					,[VendedorID] = @VendedorId
					,[NroNota] = @NroNota
					,[MontoNota] = @MontoNota
				WHERE
[NotaID] = @OriginalNotaId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Nota_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Nota_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Nota_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Nota table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Nota_Delete
(

	@NotaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Nota] WITH (ROWLOCK) 
				WHERE
					[NotaID] = @NotaId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Nota_GetByClienteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Nota_GetByClienteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Nota_GetByClienteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Nota table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Nota_GetByClienteId
(

	@ClienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[NotaID],
					[PorcentajeRetencion],
					[MontoRetencion],
					[Fecha],
					[Dias],
					[ClienteID],
					[VendedorID],
					[NroNota],
					[MontoNota]
				FROM
					[dbo].[Nota]
				WHERE
					[ClienteID] = @ClienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Nota_GetByVendedorId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Nota_GetByVendedorId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Nota_GetByVendedorId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Nota table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Nota_GetByVendedorId
(

	@VendedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[NotaID],
					[PorcentajeRetencion],
					[MontoRetencion],
					[Fecha],
					[Dias],
					[ClienteID],
					[VendedorID],
					[NroNota],
					[MontoNota]
				FROM
					[dbo].[Nota]
				WHERE
					[VendedorID] = @VendedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Nota_GetByNotaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Nota_GetByNotaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Nota_GetByNotaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Nota table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Nota_GetByNotaId
(

	@NotaId uniqueidentifier   
)
AS


				SELECT
					[NotaID],
					[PorcentajeRetencion],
					[MontoRetencion],
					[Fecha],
					[Dias],
					[ClienteID],
					[VendedorID],
					[NroNota],
					[MontoNota]
				FROM
					[dbo].[Nota]
				WHERE
					[NotaID] = @NotaId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Nota_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Nota_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Nota_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Nota table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Nota_Find
(

	@SearchUsingOR bit   = null ,

	@NotaId uniqueidentifier   = null ,

	@PorcentajeRetencion float   = null ,

	@MontoRetencion float   = null ,

	@Fecha date   = null ,

	@Dias int   = null ,

	@ClienteId uniqueidentifier   = null ,

	@VendedorId uniqueidentifier   = null ,

	@NroNota varchar (50)  = null ,

	@MontoNota float   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [NotaID]
	, [PorcentajeRetencion]
	, [MontoRetencion]
	, [Fecha]
	, [Dias]
	, [ClienteID]
	, [VendedorID]
	, [NroNota]
	, [MontoNota]
    FROM
	[dbo].[Nota]
    WHERE 
	 ([NotaID] = @NotaId OR @NotaId IS NULL)
	AND ([PorcentajeRetencion] = @PorcentajeRetencion OR @PorcentajeRetencion IS NULL)
	AND ([MontoRetencion] = @MontoRetencion OR @MontoRetencion IS NULL)
	AND ([Fecha] = @Fecha OR @Fecha IS NULL)
	AND ([Dias] = @Dias OR @Dias IS NULL)
	AND ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
	AND ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
	AND ([NroNota] = @NroNota OR @NroNota IS NULL)
	AND ([MontoNota] = @MontoNota OR @MontoNota IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [NotaID]
	, [PorcentajeRetencion]
	, [MontoRetencion]
	, [Fecha]
	, [Dias]
	, [ClienteID]
	, [VendedorID]
	, [NroNota]
	, [MontoNota]
    FROM
	[dbo].[Nota]
    WHERE 
	 ([NotaID] = @NotaId AND @NotaId is not null)
	OR ([PorcentajeRetencion] = @PorcentajeRetencion AND @PorcentajeRetencion is not null)
	OR ([MontoRetencion] = @MontoRetencion AND @MontoRetencion is not null)
	OR ([Fecha] = @Fecha AND @Fecha is not null)
	OR ([Dias] = @Dias AND @Dias is not null)
	OR ([ClienteID] = @ClienteId AND @ClienteId is not null)
	OR ([VendedorID] = @VendedorId AND @VendedorId is not null)
	OR ([NroNota] = @NroNota AND @NroNota is not null)
	OR ([MontoNota] = @MontoNota AND @MontoNota is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pago_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pago_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pago_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Pago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pago_Get_List

AS


				
				SELECT
					[PagoID],
					[FechaPago],
					[Monto],
					[TipoPago],
					[TransaccionID],
					[ClienteId],
					[VendedorId],
					[NroRecibo],
					[EstadoRendicion],
					[CuentaCorrienteID]
				FROM
					[dbo].[Pago]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pago_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pago_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pago_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Pago table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pago_GetPaged
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
				    [PagoID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PagoID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PagoID]'
				SET @SQL = @SQL + ' FROM [dbo].[Pago]'
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
				SELECT O.[PagoID], O.[FechaPago], O.[Monto], O.[TipoPago], O.[TransaccionID], O.[ClienteId], O.[VendedorId], O.[NroRecibo], O.[EstadoRendicion], O.[CuentaCorrienteID]
				FROM
				    [dbo].[Pago] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PagoID] = PageIndex.[PagoID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Pago]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pago_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pago_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pago_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Pago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pago_Insert
(

	@PagoId uniqueidentifier    OUTPUT,

	@FechaPago datetime   ,

	@Monto float   ,

	@TipoPago int   ,

	@TransaccionId varchar (100)  ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@NroRecibo varchar (50)  ,

	@EstadoRendicion int   ,

	@CuentaCorrienteId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[Pago]
					(
					[PagoID]
					,[FechaPago]
					,[Monto]
					,[TipoPago]
					,[TransaccionID]
					,[ClienteId]
					,[VendedorId]
					,[NroRecibo]
					,[EstadoRendicion]
					,[CuentaCorrienteID]
					)
				VALUES
					(
					@PagoId
					,@FechaPago
					,@Monto
					,@TipoPago
					,@TransaccionId
					,@ClienteId
					,@VendedorId
					,@NroRecibo
					,@EstadoRendicion
					,@CuentaCorrienteId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pago_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pago_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pago_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Pago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pago_Update
(

	@PagoId uniqueidentifier   ,

	@OriginalPagoId uniqueidentifier   ,

	@FechaPago datetime   ,

	@Monto float   ,

	@TipoPago int   ,

	@TransaccionId varchar (100)  ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@NroRecibo varchar (50)  ,

	@EstadoRendicion int   ,

	@CuentaCorrienteId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Pago]
				SET
					[PagoID] = @PagoId
					,[FechaPago] = @FechaPago
					,[Monto] = @Monto
					,[TipoPago] = @TipoPago
					,[TransaccionID] = @TransaccionId
					,[ClienteId] = @ClienteId
					,[VendedorId] = @VendedorId
					,[NroRecibo] = @NroRecibo
					,[EstadoRendicion] = @EstadoRendicion
					,[CuentaCorrienteID] = @CuentaCorrienteId
				WHERE
[PagoID] = @OriginalPagoId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pago_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pago_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pago_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Pago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pago_Delete
(

	@PagoId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Pago] WITH (ROWLOCK) 
				WHERE
					[PagoID] = @PagoId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pago_GetByClienteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pago_GetByClienteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pago_GetByClienteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pago table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pago_GetByClienteId
(

	@ClienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PagoID],
					[FechaPago],
					[Monto],
					[TipoPago],
					[TransaccionID],
					[ClienteId],
					[VendedorId],
					[NroRecibo],
					[EstadoRendicion],
					[CuentaCorrienteID]
				FROM
					[dbo].[Pago]
				WHERE
					[ClienteId] = @ClienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pago_GetByCuentaCorrienteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pago_GetByCuentaCorrienteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pago_GetByCuentaCorrienteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pago table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pago_GetByCuentaCorrienteId
(

	@CuentaCorrienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PagoID],
					[FechaPago],
					[Monto],
					[TipoPago],
					[TransaccionID],
					[ClienteId],
					[VendedorId],
					[NroRecibo],
					[EstadoRendicion],
					[CuentaCorrienteID]
				FROM
					[dbo].[Pago]
				WHERE
					[CuentaCorrienteID] = @CuentaCorrienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pago_GetByVendedorId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pago_GetByVendedorId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pago_GetByVendedorId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pago table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pago_GetByVendedorId
(

	@VendedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PagoID],
					[FechaPago],
					[Monto],
					[TipoPago],
					[TransaccionID],
					[ClienteId],
					[VendedorId],
					[NroRecibo],
					[EstadoRendicion],
					[CuentaCorrienteID]
				FROM
					[dbo].[Pago]
				WHERE
					[VendedorId] = @VendedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pago_GetByPagoId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pago_GetByPagoId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pago_GetByPagoId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pago table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pago_GetByPagoId
(

	@PagoId uniqueidentifier   
)
AS


				SELECT
					[PagoID],
					[FechaPago],
					[Monto],
					[TipoPago],
					[TransaccionID],
					[ClienteId],
					[VendedorId],
					[NroRecibo],
					[EstadoRendicion],
					[CuentaCorrienteID]
				FROM
					[dbo].[Pago]
				WHERE
					[PagoID] = @PagoId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pago_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pago_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pago_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Pago table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pago_Find
(

	@SearchUsingOR bit   = null ,

	@PagoId uniqueidentifier   = null ,

	@FechaPago datetime   = null ,

	@Monto float   = null ,

	@TipoPago int   = null ,

	@TransaccionId varchar (100)  = null ,

	@ClienteId uniqueidentifier   = null ,

	@VendedorId uniqueidentifier   = null ,

	@NroRecibo varchar (50)  = null ,

	@EstadoRendicion int   = null ,

	@CuentaCorrienteId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PagoID]
	, [FechaPago]
	, [Monto]
	, [TipoPago]
	, [TransaccionID]
	, [ClienteId]
	, [VendedorId]
	, [NroRecibo]
	, [EstadoRendicion]
	, [CuentaCorrienteID]
    FROM
	[dbo].[Pago]
    WHERE 
	 ([PagoID] = @PagoId OR @PagoId IS NULL)
	AND ([FechaPago] = @FechaPago OR @FechaPago IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
	AND ([TipoPago] = @TipoPago OR @TipoPago IS NULL)
	AND ([TransaccionID] = @TransaccionId OR @TransaccionId IS NULL)
	AND ([ClienteId] = @ClienteId OR @ClienteId IS NULL)
	AND ([VendedorId] = @VendedorId OR @VendedorId IS NULL)
	AND ([NroRecibo] = @NroRecibo OR @NroRecibo IS NULL)
	AND ([EstadoRendicion] = @EstadoRendicion OR @EstadoRendicion IS NULL)
	AND ([CuentaCorrienteID] = @CuentaCorrienteId OR @CuentaCorrienteId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PagoID]
	, [FechaPago]
	, [Monto]
	, [TipoPago]
	, [TransaccionID]
	, [ClienteId]
	, [VendedorId]
	, [NroRecibo]
	, [EstadoRendicion]
	, [CuentaCorrienteID]
    FROM
	[dbo].[Pago]
    WHERE 
	 ([PagoID] = @PagoId AND @PagoId is not null)
	OR ([FechaPago] = @FechaPago AND @FechaPago is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	OR ([TipoPago] = @TipoPago AND @TipoPago is not null)
	OR ([TransaccionID] = @TransaccionId AND @TransaccionId is not null)
	OR ([ClienteId] = @ClienteId AND @ClienteId is not null)
	OR ([VendedorId] = @VendedorId AND @VendedorId is not null)
	OR ([NroRecibo] = @NroRecibo AND @NroRecibo is not null)
	OR ([EstadoRendicion] = @EstadoRendicion AND @EstadoRendicion is not null)
	OR ([CuentaCorrienteID] = @CuentaCorrienteId AND @CuentaCorrienteId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Factura_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Factura_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Factura_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Factura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Factura_Get_List

AS


				
				SELECT
					[FacturaID],
					[NroFactura],
					[Monto],
					[Fecha],
					[Tipo],
					[Estado],
					[ClienteID],
					[VendedorID],
					[DescuentoAplicado],
					[Observaciones],
					[DiasPreReserva]
				FROM
					[dbo].[Factura]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Factura_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Factura_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Factura_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Factura table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Factura_GetPaged
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
				    [FacturaID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([FacturaID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [FacturaID]'
				SET @SQL = @SQL + ' FROM [dbo].[Factura]'
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
				SELECT O.[FacturaID], O.[NroFactura], O.[Monto], O.[Fecha], O.[Tipo], O.[Estado], O.[ClienteID], O.[VendedorID], O.[DescuentoAplicado], O.[Observaciones], O.[DiasPreReserva]
				FROM
				    [dbo].[Factura] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[FacturaID] = PageIndex.[FacturaID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Factura]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Factura_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Factura_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Factura_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Factura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Factura_Insert
(

	@FacturaId uniqueidentifier    OUTPUT,

	@NroFactura varchar (50)  ,

	@Monto float   ,

	@Fecha datetime   ,

	@Tipo int   ,

	@Estado int   ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@DescuentoAplicado float   ,

	@Observaciones varchar (MAX)  ,

	@DiasPreReserva int   
)
AS


				
				INSERT INTO [dbo].[Factura]
					(
					[FacturaID]
					,[NroFactura]
					,[Monto]
					,[Fecha]
					,[Tipo]
					,[Estado]
					,[ClienteID]
					,[VendedorID]
					,[DescuentoAplicado]
					,[Observaciones]
					,[DiasPreReserva]
					)
				VALUES
					(
					@FacturaId
					,@NroFactura
					,@Monto
					,@Fecha
					,@Tipo
					,@Estado
					,@ClienteId
					,@VendedorId
					,@DescuentoAplicado
					,@Observaciones
					,@DiasPreReserva
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Factura_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Factura_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Factura_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Factura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Factura_Update
(

	@FacturaId uniqueidentifier   ,

	@OriginalFacturaId uniqueidentifier   ,

	@NroFactura varchar (50)  ,

	@Monto float   ,

	@Fecha datetime   ,

	@Tipo int   ,

	@Estado int   ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@DescuentoAplicado float   ,

	@Observaciones varchar (MAX)  ,

	@DiasPreReserva int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Factura]
				SET
					[FacturaID] = @FacturaId
					,[NroFactura] = @NroFactura
					,[Monto] = @Monto
					,[Fecha] = @Fecha
					,[Tipo] = @Tipo
					,[Estado] = @Estado
					,[ClienteID] = @ClienteId
					,[VendedorID] = @VendedorId
					,[DescuentoAplicado] = @DescuentoAplicado
					,[Observaciones] = @Observaciones
					,[DiasPreReserva] = @DiasPreReserva
				WHERE
[FacturaID] = @OriginalFacturaId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Factura_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Factura_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Factura_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Factura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Factura_Delete
(

	@FacturaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Factura] WITH (ROWLOCK) 
				WHERE
					[FacturaID] = @FacturaId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Factura_GetByClienteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Factura_GetByClienteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Factura_GetByClienteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Factura table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Factura_GetByClienteId
(

	@ClienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FacturaID],
					[NroFactura],
					[Monto],
					[Fecha],
					[Tipo],
					[Estado],
					[ClienteID],
					[VendedorID],
					[DescuentoAplicado],
					[Observaciones],
					[DiasPreReserva]
				FROM
					[dbo].[Factura]
				WHERE
					[ClienteID] = @ClienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Factura_GetByVendedorId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Factura_GetByVendedorId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Factura_GetByVendedorId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Factura table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Factura_GetByVendedorId
(

	@VendedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FacturaID],
					[NroFactura],
					[Monto],
					[Fecha],
					[Tipo],
					[Estado],
					[ClienteID],
					[VendedorID],
					[DescuentoAplicado],
					[Observaciones],
					[DiasPreReserva]
				FROM
					[dbo].[Factura]
				WHERE
					[VendedorID] = @VendedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Factura_GetByFacturaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Factura_GetByFacturaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Factura_GetByFacturaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Factura table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Factura_GetByFacturaId
(

	@FacturaId uniqueidentifier   
)
AS


				SELECT
					[FacturaID],
					[NroFactura],
					[Monto],
					[Fecha],
					[Tipo],
					[Estado],
					[ClienteID],
					[VendedorID],
					[DescuentoAplicado],
					[Observaciones],
					[DiasPreReserva]
				FROM
					[dbo].[Factura]
				WHERE
					[FacturaID] = @FacturaId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Factura_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Factura_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Factura_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Factura table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Factura_Find
(

	@SearchUsingOR bit   = null ,

	@FacturaId uniqueidentifier   = null ,

	@NroFactura varchar (50)  = null ,

	@Monto float   = null ,

	@Fecha datetime   = null ,

	@Tipo int   = null ,

	@Estado int   = null ,

	@ClienteId uniqueidentifier   = null ,

	@VendedorId uniqueidentifier   = null ,

	@DescuentoAplicado float   = null ,

	@Observaciones varchar (MAX)  = null ,

	@DiasPreReserva int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [FacturaID]
	, [NroFactura]
	, [Monto]
	, [Fecha]
	, [Tipo]
	, [Estado]
	, [ClienteID]
	, [VendedorID]
	, [DescuentoAplicado]
	, [Observaciones]
	, [DiasPreReserva]
    FROM
	[dbo].[Factura]
    WHERE 
	 ([FacturaID] = @FacturaId OR @FacturaId IS NULL)
	AND ([NroFactura] = @NroFactura OR @NroFactura IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
	AND ([Fecha] = @Fecha OR @Fecha IS NULL)
	AND ([Tipo] = @Tipo OR @Tipo IS NULL)
	AND ([Estado] = @Estado OR @Estado IS NULL)
	AND ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
	AND ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
	AND ([DescuentoAplicado] = @DescuentoAplicado OR @DescuentoAplicado IS NULL)
	AND ([Observaciones] = @Observaciones OR @Observaciones IS NULL)
	AND ([DiasPreReserva] = @DiasPreReserva OR @DiasPreReserva IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [FacturaID]
	, [NroFactura]
	, [Monto]
	, [Fecha]
	, [Tipo]
	, [Estado]
	, [ClienteID]
	, [VendedorID]
	, [DescuentoAplicado]
	, [Observaciones]
	, [DiasPreReserva]
    FROM
	[dbo].[Factura]
    WHERE 
	 ([FacturaID] = @FacturaId AND @FacturaId is not null)
	OR ([NroFactura] = @NroFactura AND @NroFactura is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	OR ([Fecha] = @Fecha AND @Fecha is not null)
	OR ([Tipo] = @Tipo AND @Tipo is not null)
	OR ([Estado] = @Estado AND @Estado is not null)
	OR ([ClienteID] = @ClienteId AND @ClienteId is not null)
	OR ([VendedorID] = @VendedorId AND @VendedorId is not null)
	OR ([DescuentoAplicado] = @DescuentoAplicado AND @DescuentoAplicado is not null)
	OR ([Observaciones] = @Observaciones AND @Observaciones is not null)
	OR ([DiasPreReserva] = @DiasPreReserva AND @DiasPreReserva is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the MovimientoCuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_Get_List

AS


				
				SELECT
					[MovimientoID],
					[PagoID],
					[FacturaID],
					[FechaRegistro],
					[CuentaID],
					[NotaID],
					[CuentaCorrienteID],
					[DebitoID]
				FROM
					[dbo].[MovimientoCuenta]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the MovimientoCuenta table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_GetPaged
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
				SELECT O.[MovimientoID], O.[PagoID], O.[FacturaID], O.[FechaRegistro], O.[CuentaID], O.[NotaID], O.[CuentaCorrienteID], O.[DebitoID]
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
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the MovimientoCuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_Insert
(

	@MovimientoId uniqueidentifier    OUTPUT,

	@PagoId uniqueidentifier   ,

	@FacturaId uniqueidentifier   ,

	@FechaRegistro datetime   ,

	@CuentaId uniqueidentifier   ,

	@NotaId uniqueidentifier   ,

	@CuentaCorrienteId uniqueidentifier   ,

	@DebitoId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[MovimientoCuenta]
					(
					[MovimientoID]
					,[PagoID]
					,[FacturaID]
					,[FechaRegistro]
					,[CuentaID]
					,[NotaID]
					,[CuentaCorrienteID]
					,[DebitoID]
					)
				VALUES
					(
					@MovimientoId
					,@PagoId
					,@FacturaId
					,@FechaRegistro
					,@CuentaId
					,@NotaId
					,@CuentaCorrienteId
					,@DebitoId
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the MovimientoCuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_Update
(

	@MovimientoId uniqueidentifier   ,

	@OriginalMovimientoId uniqueidentifier   ,

	@PagoId uniqueidentifier   ,

	@FacturaId uniqueidentifier   ,

	@FechaRegistro datetime   ,

	@CuentaId uniqueidentifier   ,

	@NotaId uniqueidentifier   ,

	@CuentaCorrienteId uniqueidentifier   ,

	@DebitoId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[MovimientoCuenta]
				SET
					[MovimientoID] = @MovimientoId
					,[PagoID] = @PagoId
					,[FacturaID] = @FacturaId
					,[FechaRegistro] = @FechaRegistro
					,[CuentaID] = @CuentaId
					,[NotaID] = @NotaId
					,[CuentaCorrienteID] = @CuentaCorrienteId
					,[DebitoID] = @DebitoId
				WHERE
[MovimientoID] = @OriginalMovimientoId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the MovimientoCuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_Delete
(

	@MovimientoId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[MovimientoCuenta] WITH (ROWLOCK) 
				WHERE
					[MovimientoID] = @MovimientoId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_GetByCuentaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_GetByCuentaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_GetByCuentaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the MovimientoCuenta table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_GetByCuentaId
(

	@CuentaId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[MovimientoID],
					[PagoID],
					[FacturaID],
					[FechaRegistro],
					[CuentaID],
					[NotaID],
					[CuentaCorrienteID],
					[DebitoID]
				FROM
					[dbo].[MovimientoCuenta]
				WHERE
					[CuentaID] = @CuentaId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_GetByCuentaCorrienteId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_GetByCuentaCorrienteId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_GetByCuentaCorrienteId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the MovimientoCuenta table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_GetByCuentaCorrienteId
(

	@CuentaCorrienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[MovimientoID],
					[PagoID],
					[FacturaID],
					[FechaRegistro],
					[CuentaID],
					[NotaID],
					[CuentaCorrienteID],
					[DebitoID]
				FROM
					[dbo].[MovimientoCuenta]
				WHERE
					[CuentaCorrienteID] = @CuentaCorrienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_GetByDebitoId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_GetByDebitoId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_GetByDebitoId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the MovimientoCuenta table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_GetByDebitoId
(

	@DebitoId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[MovimientoID],
					[PagoID],
					[FacturaID],
					[FechaRegistro],
					[CuentaID],
					[NotaID],
					[CuentaCorrienteID],
					[DebitoID]
				FROM
					[dbo].[MovimientoCuenta]
				WHERE
					[DebitoID] = @DebitoId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_GetByFacturaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_GetByFacturaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_GetByFacturaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the MovimientoCuenta table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_GetByFacturaId
(

	@FacturaId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[MovimientoID],
					[PagoID],
					[FacturaID],
					[FechaRegistro],
					[CuentaID],
					[NotaID],
					[CuentaCorrienteID],
					[DebitoID]
				FROM
					[dbo].[MovimientoCuenta]
				WHERE
					[FacturaID] = @FacturaId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_GetByNotaId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_GetByNotaId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_GetByNotaId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the MovimientoCuenta table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_GetByNotaId
(

	@NotaId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[MovimientoID],
					[PagoID],
					[FacturaID],
					[FechaRegistro],
					[CuentaID],
					[NotaID],
					[CuentaCorrienteID],
					[DebitoID]
				FROM
					[dbo].[MovimientoCuenta]
				WHERE
					[NotaID] = @NotaId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_GetByPagoId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_GetByPagoId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_GetByPagoId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the MovimientoCuenta table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_GetByPagoId
(

	@PagoId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[MovimientoID],
					[PagoID],
					[FacturaID],
					[FechaRegistro],
					[CuentaID],
					[NotaID],
					[CuentaCorrienteID],
					[DebitoID]
				FROM
					[dbo].[MovimientoCuenta]
				WHERE
					[PagoID] = @PagoId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_GetByMovimientoId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_GetByMovimientoId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_GetByMovimientoId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the MovimientoCuenta table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_GetByMovimientoId
(

	@MovimientoId uniqueidentifier   
)
AS


				SELECT
					[MovimientoID],
					[PagoID],
					[FacturaID],
					[FechaRegistro],
					[CuentaID],
					[NotaID],
					[CuentaCorrienteID],
					[DebitoID]
				FROM
					[dbo].[MovimientoCuenta]
				WHERE
					[MovimientoID] = @MovimientoId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.MovimientoCuenta_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.MovimientoCuenta_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.MovimientoCuenta_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the MovimientoCuenta table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.MovimientoCuenta_Find
(

	@SearchUsingOR bit   = null ,

	@MovimientoId uniqueidentifier   = null ,

	@PagoId uniqueidentifier   = null ,

	@FacturaId uniqueidentifier   = null ,

	@FechaRegistro datetime   = null ,

	@CuentaId uniqueidentifier   = null ,

	@NotaId uniqueidentifier   = null ,

	@CuentaCorrienteId uniqueidentifier   = null ,

	@DebitoId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [MovimientoID]
	, [PagoID]
	, [FacturaID]
	, [FechaRegistro]
	, [CuentaID]
	, [NotaID]
	, [CuentaCorrienteID]
	, [DebitoID]
    FROM
	[dbo].[MovimientoCuenta]
    WHERE 
	 ([MovimientoID] = @MovimientoId OR @MovimientoId IS NULL)
	AND ([PagoID] = @PagoId OR @PagoId IS NULL)
	AND ([FacturaID] = @FacturaId OR @FacturaId IS NULL)
	AND ([FechaRegistro] = @FechaRegistro OR @FechaRegistro IS NULL)
	AND ([CuentaID] = @CuentaId OR @CuentaId IS NULL)
	AND ([NotaID] = @NotaId OR @NotaId IS NULL)
	AND ([CuentaCorrienteID] = @CuentaCorrienteId OR @CuentaCorrienteId IS NULL)
	AND ([DebitoID] = @DebitoId OR @DebitoId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [MovimientoID]
	, [PagoID]
	, [FacturaID]
	, [FechaRegistro]
	, [CuentaID]
	, [NotaID]
	, [CuentaCorrienteID]
	, [DebitoID]
    FROM
	[dbo].[MovimientoCuenta]
    WHERE 
	 ([MovimientoID] = @MovimientoId AND @MovimientoId is not null)
	OR ([PagoID] = @PagoId AND @PagoId is not null)
	OR ([FacturaID] = @FacturaId AND @FacturaId is not null)
	OR ([FechaRegistro] = @FechaRegistro AND @FechaRegistro is not null)
	OR ([CuentaID] = @CuentaId AND @CuentaId is not null)
	OR ([NotaID] = @NotaId AND @NotaId is not null)
	OR ([CuentaCorrienteID] = @CuentaCorrienteId AND @CuentaCorrienteId is not null)
	OR ([DebitoID] = @DebitoId AND @DebitoId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pais_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pais_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pais_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Pais table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pais_Get_List

AS


				
				SELECT
					[PaisID],
					[Descripcion]
				FROM
					[dbo].[Pais]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pais_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pais_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pais_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Pais table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pais_GetPaged
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
				    [PaisID] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PaisID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PaisID]'
				SET @SQL = @SQL + ' FROM [dbo].[Pais]'
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
				SELECT O.[PaisID], O.[Descripcion]
				FROM
				    [dbo].[Pais] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PaisID] = PageIndex.[PaisID]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Pais]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pais_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pais_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pais_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Pais table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pais_Insert
(

	@PaisId uniqueidentifier    OUTPUT,

	@Descripcion varchar (100)  
)
AS


				
				INSERT INTO [dbo].[Pais]
					(
					[PaisID]
					,[Descripcion]
					)
				VALUES
					(
					@PaisId
					,@Descripcion
					)
				
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pais_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pais_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pais_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Pais table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pais_Update
(

	@PaisId uniqueidentifier   ,

	@OriginalPaisId uniqueidentifier   ,

	@Descripcion varchar (100)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Pais]
				SET
					[PaisID] = @PaisId
					,[Descripcion] = @Descripcion
				WHERE
[PaisID] = @OriginalPaisId 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pais_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pais_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pais_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Pais table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pais_Delete
(

	@PaisId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Pais] WITH (ROWLOCK) 
				WHERE
					[PaisID] = @PaisId
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pais_GetByPaisId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pais_GetByPaisId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pais_GetByPaisId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Pais table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pais_GetByPaisId
(

	@PaisId uniqueidentifier   
)
AS


				SELECT
					[PaisID],
					[Descripcion]
				FROM
					[dbo].[Pais]
				WHERE
					[PaisID] = @PaisId
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Pais_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Pais_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Pais_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Pais table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Pais_Find
(

	@SearchUsingOR bit   = null ,

	@PaisId uniqueidentifier   = null ,

	@Descripcion varchar (100)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaisID]
	, [Descripcion]
    FROM
	[dbo].[Pais]
    WHERE 
	 ([PaisID] = @PaisId OR @PaisId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaisID]
	, [Descripcion]
    FROM
	[dbo].[Pais]
    WHERE 
	 ([PaisID] = @PaisId AND @PaisId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroViaje_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroViaje_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroViaje_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PasajeroViaje view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroViaje_Get_List

AS


                    
                    SELECT
                        [Nro],
                        [ViajeID],
                        [PersonaID],
                        [BusID],
                        [Apellido],
                        [Nombre],
                        [TipoDocumento],
                        [NroDocumento],
                        [Telefono],
                        [Email],
                        [FechaNacimiento],
                        [Sexo],
                        [LocalidadID],
                        [Nacionalidad],
                        [PaisResidencia],
                        [Domicilio],
                        [Ocupacion],
                        [Pasaporte],
                        [VencimientoPasaporte],
                        [EmisionPasaporte],
                        [PaisOrigen]
                    FROM
                        [dbo].[PasajeroViaje]
                        
                    SELECT @@ROWCOUNT			
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PasajeroViaje_Get procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PasajeroViaje_Get') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PasajeroViaje_Get
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PasajeroViaje view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PasajeroViaje_Get
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


                    
                    BEGIN
    
                    -- Build the sql query
                    DECLARE @SQL AS nvarchar(4000)
                    SET @SQL = ' SELECT * FROM [dbo].[PasajeroViaje]'
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
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PersonaCliente_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PersonaCliente_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PersonaCliente_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PersonaCliente view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PersonaCliente_Get_List

AS


                    
                    SELECT
                        [PersonaID],
                        [Apellido],
                        [Nombre],
                        [NroDocumento],
                        [Telefono],
                        [Email],
                        [FechaNacimiento],
                        [Domicilio],
                        [Sexo],
                        [LocalidadID],
                        [ClienteID],
                        [RazonSocial],
                        [Cuit],
                        [Moneda],
                        [Empresa],
                        [Ocupacion],
                        [FormaPago],
                        [CondicionIva],
                        [VendedorID],
                        [Fax],
                        [Web],
                        [Idioma],
                        [Promotor],
                        [Observacion],
                        [TipoID],
                        [TipoDocumento],
                        [Celular],
                        [Nacionalidad],
                        [PaisResidencia],
                        [Provincia]
                    FROM
                        [dbo].[PersonaCliente]
                        
                    SELECT @@ROWCOUNT			
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PersonaCliente_Get procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PersonaCliente_Get') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PersonaCliente_Get
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PersonaCliente view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PersonaCliente_Get
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


                    
                    BEGIN
    
                    -- Build the sql query
                    DECLARE @SQL AS nvarchar(4000)
                    SET @SQL = ' SELECT * FROM [dbo].[PersonaCliente]'
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
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PersonaPasajero_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PersonaPasajero_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PersonaPasajero_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PersonaPasajero view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PersonaPasajero_Get_List

AS


                    
                    SELECT
                        [PersonaID],
                        [Apellido],
                        [Nombre],
                        [NroDocumento],
                        [Telefono],
                        [Domicilio],
                        [Email],
                        [FechaNacimiento],
                        [Sexo],
                        [PasajeroID],
                        [Pasaporte],
                        [VencimientoPasaporte],
                        [EmisionPasaporte],
                        [PaisOrigen],
                        [LocalidadID],
                        [TipoDocumento]
                    FROM
                        [dbo].[PersonaPasajero]
                        
                    SELECT @@ROWCOUNT			
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PersonaPasajero_Get procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PersonaPasajero_Get') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PersonaPasajero_Get
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PersonaPasajero view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PersonaPasajero_Get
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


                    
                    BEGIN
    
                    -- Build the sql query
                    DECLARE @SQL AS nvarchar(4000)
                    SET @SQL = ' SELECT * FROM [dbo].[PersonaPasajero]'
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
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PersonaProveedor_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PersonaProveedor_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PersonaProveedor_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PersonaProveedor view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PersonaProveedor_Get_List

AS


                    
                    SELECT
                        [PersonaID],
                        [Apellido],
                        [Nombre],
                        [NroDocumento],
                        [LocalidadID],
                        [Telefono],
                        [Email],
                        [FechaNacimiento],
                        [Sexo],
                        [Domicilio],
                        [ProveedorID],
                        [RazonSocial],
                        [ProveedorLocalidadID],
                        [ProveedorTelefono],
                        [Fax],
                        [Web],
                        [ProveedorEmail],
                        [Idioma],
                        [CondicionIva],
                        [Cuit],
                        [FormaPago],
                        [TipoDocumento]
                    FROM
                        [dbo].[PersonaProveedor]
                        
                    SELECT @@ROWCOUNT			
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PersonaProveedor_Get procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PersonaProveedor_Get') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PersonaProveedor_Get
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PersonaProveedor view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PersonaProveedor_Get
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


                    
                    BEGIN
    
                    -- Build the sql query
                    DECLARE @SQL AS nvarchar(4000)
                    SET @SQL = ' SELECT * FROM [dbo].[PersonaProveedor]'
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
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PersonaVendedor_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PersonaVendedor_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PersonaVendedor_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the PersonaVendedor view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PersonaVendedor_Get_List

AS


                    
                    SELECT
                        [PersonaID],
                        [Apellido],
                        [Nombre],
                        [NroDocumento],
                        [Domicilio],
                        [Telefono],
                        [Email],
                        [FechaNacimiento],
                        [Sexo],
                        [LocalidadID],
                        [Descripcion],
                        [VendedorID],
                        [TipoDocumento]
                    FROM
                        [dbo].[PersonaVendedor]
                        
                    SELECT @@ROWCOUNT			
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.PersonaVendedor_Get procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.PersonaVendedor_Get') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.PersonaVendedor_Get
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the PersonaVendedor view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.PersonaVendedor_Get
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


                    
                    BEGIN
    
                    -- Build the sql query
                    DECLARE @SQL AS nvarchar(4000)
                    SET @SQL = ' SELECT * FROM [dbo].[PersonaVendedor]'
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
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Reserva_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Reserva_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Reserva_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Reserva view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Reserva_Get_List

AS


                    
                    SELECT
                        [PasajeID],
                        [FechaReserva],
                        [Apellido],
                        [Nombre],
                        [NroDocumento],
                        [ViajeID],
                        [Paquete],
                        [FacturaID],
                        [ClienteID],
                        [TipoCliente]
                    FROM
                        [dbo].[Reserva]
                        
                    SELECT @@ROWCOUNT			
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Reserva_Get procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Reserva_Get') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Reserva_Get
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Reserva view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Reserva_Get
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
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.vConsultaReservaHabitacion_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.vConsultaReservaHabitacion_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.vConsultaReservaHabitacion_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the vConsultaReservaHabitacion view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.vConsultaReservaHabitacion_Get_List

AS


                    
                    SELECT
                        [ReservaHabitacionID],
                        [HotelID],
                        [Expiro],
                        [HabitacionID],
                        [Capacidad],
                        [Ocupacion],
                        [Estado],
                        [Desde],
                        [Hasta]
                    FROM
                        [dbo].[vConsultaReservaHabitacion]
                        
                    SELECT @@ROWCOUNT			
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.vConsultaReservaHabitacion_Get procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.vConsultaReservaHabitacion_Get') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.vConsultaReservaHabitacion_Get
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the vConsultaReservaHabitacion view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.vConsultaReservaHabitacion_Get
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


                    
                    BEGIN
    
                    -- Build the sql query
                    DECLARE @SQL AS nvarchar(4000)
                    SET @SQL = ' SELECT * FROM [dbo].[vConsultaReservaHabitacion]'
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
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.vLocalidad_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.vLocalidad_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.vLocalidad_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the vLocalidad view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.vLocalidad_Get_List

AS


                    
                    SELECT
                        [ID],
                        [Nombre]
                    FROM
                        [dbo].[vLocalidad]
                        
                    SELECT @@ROWCOUNT			
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.vLocalidad_Get procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.vLocalidad_Get') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.vLocalidad_Get
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the vLocalidad view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.vLocalidad_Get
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
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.vPersona_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.vPersona_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.vPersona_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the vPersona view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.vPersona_Get_List

AS


                    
                    SELECT
                        [PersonaID],
                        [Apellido],
                        [Nombre],
                        [TipoDocumento],
                        [NroDocumento],
                        [Telefono],
                        [Email],
                        [FechaNacimiento],
                        [LocalidadID],
                        [UserId],
                        [Domicilio],
                        [Ocupacion],
                        [Nacionalidad],
                        [PaisResidencia],
                        [Sexo]
                    FROM
                        [dbo].[vPersona]
                        
                    SELECT @@ROWCOUNT			
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.vPersona_Get procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.vPersona_Get') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.vPersona_Get
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the vPersona view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.vPersona_Get
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


                    
                    BEGIN
    
                    -- Build the sql query
                    DECLARE @SQL AS nvarchar(4000)
                    SET @SQL = ' SELECT * FROM [dbo].[vPersona]'
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
                

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

