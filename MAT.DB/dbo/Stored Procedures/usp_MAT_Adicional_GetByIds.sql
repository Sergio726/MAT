CREATE PROCEDURE [dbo].[usp_MAT_Adicional_GetByIds] (@AdicionalIDs dbo.tvp_AdicionalIDTableType READONLY)
AS 
  -- Author:    Garcia Sergio 
  -- Create date: 02-04-2025
  -- Description: get adicionales by ids tvp_AdicionalIDTableType
  --History
  --
  -- ============================================= */
BEGIN
    SET NOCOUNT ON;

    SELECT 
        a.AdicionalID,
        SUM(a.Monto * ai.Cantidad) AS MontoTotal,  -- Multiplica por la cantidad
        a.Descripcion
    FROM dbo.Adicional a
    INNER JOIN @AdicionalIDs ai ON a.AdicionalID = ai.Id
    GROUP BY a.AdicionalID, a.Descripcion;
END;