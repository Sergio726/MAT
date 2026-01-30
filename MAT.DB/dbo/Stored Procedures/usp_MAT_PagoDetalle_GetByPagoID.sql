
CREATE PROCEDURE [dbo].[usp_MAT_PagoDetalle_GetByPagoID](@PagoID uniqueidentifier)
AS
/*-----------------------------------------------------------
Author:    Garcia Sergio 
Create date: 2018-05-02
Description:  Get PagoDetalle by PagoID

-----------------------------------------------------------*/
BEGIN
select pd.MontoRecibido,
	   MonedaRecibida = mtRecibido.Codigo,
	   pd.MontoEquivalente,
	   MonedaEquivalente = mtEquivalente.Codigo,
	   Cotizacion = pd.MontoEquivalenteCotizacion,
	   Fecha = CONVERT(varchar(50), pd.Fecha,103)
from dbo.PagoDetalle pd
inner join dbo.MonedaTipo mtRecibido
	on mtRecibido.Id = pd.MontoRecibidoMonedaTipo
inner join dbo.MonedaTipo mtEquivalente
	on mtEquivalente.Id = pd.MontoEquivalenteMonedaTipo
where pd.PagoID = @PagoID

END

