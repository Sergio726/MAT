CREATE PROCEDURE [dbo].[usp_MAT_Paquetes_VinculosByPaqueteID]
 @PaqueteID uniqueidentifier
AS
 /*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-04
  -- Description: Vinculos de un paquete (Servicios, Excursiones, Precios,
  --              Adicionales) en un solo resultset.
  --              History:
  --                2025-03-31 Garcia Sergio: version original (UNION de 4 tipos).
  --                2026-07-04 Sebastian Garcia: se agrega VinculoRowId (PK de la
  --                tabla de vinculo) para poder desvincular excursiones sin una
  --                segunda consulta; asi la vista Vinculos usa un unico SP.
  ============================================= */
BEGIN
    SET NOCOUNT ON;

    SELECT
        ps.PaqueteID,
        ps.ServicioID as ID,
        VinculoRowId = ps.PaqueteServicioID,
        NULL as IsOpcional,
        s.Precio as Precio,
        s.Descripcion as Descripcion,
        'Servicio' as Tipo
    FROM dbo.PaqueteServicio ps
    inner join Servicio s
        on ps.ServicioID = s.ServicioID
    WHERE ps.PaqueteID = @PaqueteID

    UNION ALL

    SELECT
        pe.PaqueteID,
        pe.ExcursionID as ID,
        VinculoRowId = pe.PaqueteExcursionID,
        pe.IsOpcional,
        e.Costo as Precio,
        e.Descripcion as Descripcion,
        'Excursion' as Tipo
    FROM dbo.PaqueteExcursion pe
    inner join Excursion e
        on pe.ExcursionID = e.ExcursionID
    WHERE pe.PaqueteID = @PaqueteID

    UNION ALL

    SELECT
        pp.PaqueteID,
        pp.PrecioID as ID,
        VinculoRowId = pp.PaquetePrecioID,
        NULL as IsOpcional,
        p.Monto as Precio,
        p.Descripcion as Descripcion,
        'Precio' as Tipo
    FROM dbo.PaquetePrecio pp
    inner join dbo.Precio p
        on pp.PrecioID = p.PrecioID
    WHERE pp.PaqueteID = @PaqueteID

    UNION ALL

    SELECT
        pa.PaqueteID,
        pa.AdicionalID as ID,
        VinculoRowId = pa.PaqueteAdicionalID,
        NULL as IsOpcional,
        a.Monto as Precio,
        a.Descripcion as Descripcion,
        'Adicional' as Tipo
    FROM dbo.PaqueteAdicional pa
    inner join dbo.Adicional a
        on pa.AdicionalID = a.AdicionalID
    WHERE pa.PaqueteID = @PaqueteID

    ORDER BY Tipo, Descripcion
END
