CREATE PROCEDURE [dbo].[usp_MAT_Paquetes_VinculosByPaqueteID]
 @PaqueteID uniqueidentifier
AS
 /* ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date:31/03/2025
  -- Description:  get adicionales from paquete

  -- ============================================= */
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ps.PaqueteID,
        ps.ServicioID as ID,
        NULL as IsOpcional,
        s.Precio as Precio,
        s.Descripcion as Descripcion,
        'Servicio' as Tipo
    FROM dbo.PaqueteServicio ps
    inner join Servicio s
        on ps.ServicioID = s.ServicioID
    WHERE PaqueteID = @PaqueteID

    UNION ALL

    SELECT 
        pe.PaqueteID,
        pe.ExcursionID as ID,
        pe.IsOpcional,
        e.Costo as Precio,
        e.Descripcion as Descripcion,
        'Excursion' as Tipo
    FROM dbo.PaqueteExcursion pe
    inner join Excursion e 
        on pe.ExcursionID = e.ExcursionID
    WHERE PaqueteID = @PaqueteID

    UNION ALL

    SELECT 
        PaqueteID,
        pp.PrecioID as ID,
        NULL as IsOpcional,
        p.Monto as Precio,
        Descripcion as Descripcion,
        'Precio' as Tipo
    FROM dbo.PaquetePrecio pp
    inner join dbo.Precio p
        on pp.PrecioID = p.PrecioID
    WHERE PaqueteID = @PaqueteID

    UNION ALL

    SELECT 
        PaqueteID,
        pa.AdicionalID as ID,
        NULL as IsOpcional,
        a.Monto as Precio,
        a.Descripcion as Descripcion,
        'Adicional' as Tipo
    FROM dbo.PaqueteAdicional pa
    inner join dbo.Adicional a
        on pa.AdicionalID = a.AdicionalID
    WHERE PaqueteID = @PaqueteID

    ORDER BY Tipo, ID
END