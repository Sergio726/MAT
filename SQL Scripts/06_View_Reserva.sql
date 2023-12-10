use [MAT.Intranet]
go

create view [dbo].[Reserva]
as
SELECT   dbo.Pasaje.PasajeID,Pasaje.FechaReserva,Persona.Apellido, Persona.Nombre, Persona.NroDocumento, dbo.Pasaje.ViajeID, D.Descripcion as Destino,
		 P.Descripcion as Paquete ,dbo.Pasaje.TipoID as TipoPasaje, dbo.Pasaje.FacturaID, (select C.ClienteID from Cliente C inner join 
				CuentaCorriente CC on C.ClienteID= CC.ClienteID inner join
				MovimientoCuenta MV on CC.CuentaCorrienteID = MV.CuentaCorrienteID inner join
				Factura Fac on Fac.FacturaID = MV.FacturaID where Fac.FacturaID =  dbo.Pasaje.FacturaID) AS ClienteID ,
				(select C.TipoID from Cliente C inner join 
				CuentaCorriente CC on C.ClienteID= CC.ClienteID inner join
				MovimientoCuenta MV on CC.CuentaCorrienteID = MV.CuentaCorrienteID inner join
				Factura Fac on Fac.FacturaID = MV.FacturaID where Fac.FacturaID =  dbo.Pasaje.FacturaID) AS TipoCliente
		
FROM            dbo.Persona inner join
				dbo.Pasajero on Persona.PersonaID = Pasajero.PasajeroID inner JOIN
                dbo.Pasaje ON dbo.Pasajero.PasajeroID = dbo.Pasaje.PasajeroID INNER JOIN
                dbo.Viaje V on V.ViajeID = dbo.Pasaje.ViajeID inner join 
				Paquete P on V.PaqueteID = P.PaqueteID inner join
				Destino D on P.DestinoID = D.DestinoID
				
			