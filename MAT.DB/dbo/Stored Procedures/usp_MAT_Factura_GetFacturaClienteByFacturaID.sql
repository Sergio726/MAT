
CREATE PROCEDURE [dbo].[usp_MAT_Factura_GetFacturaClienteByFacturaID]
(

@FacturaId uniqueidentifier   
)
AS
-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 01/04/2017
-- Description:  trae los datos de una factura segun el id
--2017-09-17	Garcia Sergio: drop column descuento aplicado
--2017-09-17	Garcia Sergio: update precio and saldo
--2017-11-19	Garcia Sergio: Add Vendedor
--2018-04-30	Garcia Sergio: Add MonedaTipo
-- =============================================
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;
BEGIN 

	DECLARE @SaldoFactura MONEY = 0
	SELECT @SaldoFactura = dbo.fn_MAT_SaldoFactura(@FacturaId)

	SELECT
		f.[FacturaID],
		f.[NroFactura],
		[Monto] = sum(df.Precio),
		f.[Fecha],
		f.[Tipo],
		f.[Estado],
		f.[ClienteID],
		f.[VendedorID],
		f.Observaciones,
		isnull(p.Nombre, '') as Nombre,
		isnull(p.Apellido,'') as Apellido,
		isnull(v.Nombre, '') as VendedorNombre,
		isnull(v.Apellido,'') as VendedorApellido,
		[Saldo] = @SaldoFactura
	FROM
		[dbo].[Factura] f
	INNER JOIN dbo.Persona p
		on f.ClienteID = p.PersonaID
	INNER JOIN dbo.Persona v
		on f.VendedorID = v.PersonaID
	left join dbo.DetalleFactura df
		on f.FacturaID = df.FacturaID
	WHERE
		f.[FacturaID] = @FacturaId
	group by 
	f.[FacturaID],
		f.[NroFactura],
		f.[Monto],
		f.[Fecha],
		f.[Tipo],
		f.[Estado],
		f.[ClienteID],
		f.[VendedorID],
		f.Observaciones,
		p.Nombre,
		p.Apellido,
		v.Nombre,
		v.Apellido


	/*Paquete*/
    select top 1
		   pa.PaqueteID as PaqueteID,
		   isnull(pa.Descripcion,'') as PaqueteDescripcion,
		   MonedaTipo = pa.Moneda
	from dbo.Pasaje pj
	inner join dbo.Viaje v
		on pj.ViajeID = v.ViajeID
	inner join dbo.Paquete pa
		on v.PaqueteID = pa.PaqueteID
	where pj.FacturaID = @FacturaId

END