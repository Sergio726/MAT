CREATE PROCEDURE [dbo].[usp_MAT_Precio_GetByIds] (@PrecioIDs dbo.tvp_PrecioIDTableType READONLY)
AS 
  -- Author:    Garcia Sergio 
  -- Create date: 02-03-2025
  -- Description: get precios by ids tvp_PrecioIDTableType
  --History
  --
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.PrecioID,
        SUM(p.Monto * pi.Cantidad) AS MontoTotal,  -- Multiplica por la cantidad
        p.Vigencia,
        p.Descripcion,
        p.DescripcionVoucher,
        p.Mes
    FROM dbo.Precio p
    INNER JOIN @PrecioIDs pi ON p.PrecioID = pi.Id
    GROUP BY p.PrecioID, p.Vigencia, p.Descripcion, p.DescripcionVoucher, p.Mes;
END;