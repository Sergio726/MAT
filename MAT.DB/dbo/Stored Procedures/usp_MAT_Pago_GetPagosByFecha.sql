CREATE PROCEDURE [dbo].[usp_MAT_Pago_GetPagosByFecha](@Fecha datetime)
AS
/*-----------------------------------------------------------
Author:    Garcia Sergio 
Create date: 2017-10-06
Description:  Get Pagos by FacturaID

10-21-2017	Garcia Sergio:	add Cliente
-----------------------------------------------------------*/

	SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 
BEGIN
	select  p.PagoID,
		p.FechaPago,
		p.Monto,
		p.NroRecibo,
		p.TransaccionID,
		p.TipoPago,
		TipoPagoDescripcion = tp.Descripcion,
		Vendedor = per.Nombre + ' ' + per.Apellido,
		ClienteNombre = cli.Nombre,
		ClienteApellido = cli.Apellido
	from dbo.MovimientoCuenta mc
	inner join dbo.Pago p
		on mc.PagoID = p.PagoID
	inner join dbo.PagoTipo tp
		on p.TipoPago = tp.Id
	left join dbo.Persona per
		on p.VendedorId = per.PersonaID
	inner join dbo.Cuenta cu
		on cu.CuentaID = mc.CuentaID
	inner join dbo.Persona cli
		on cli.PersonaID = cu.ClienteID
	where p.FechaPago >= @Fecha 
	and p.FechaPago < DATEADD(dd,1,@Fecha)

END
