CREATE PROCEDURE [dbo].[usp_MAT_Reserva_GetPaymentPreferences]
(
	@ReservaId UNIQUEIDENTIFIER
)

AS 
/*
============================================= 
Author:    Ruben Tejerina 
Create date: 19/11/2024
Description:  Get payment preferences for MP

History:

2024-11-26 Ruben Tejerina Create SP
============================================= 
*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

    Select 
        pr.Id as ReservaId,
        pr.ClienteId, 
        pr.ViajeId, 
        pr.FacturaId,
        pr.ExpirationOn,
        v.Descripcion,
        v.FechaSalida,
        cliente.Nombre,
        cliente.Apellido,
        cliente.Email,
        cliente.NroDocumento,
        cliente.TipoDocumento,
        tCostos.TotalFactura
    from PedidoReserva pr
    inner join Viaje v on v.ViajeID = pr.ViajeId
    inner join Persona cliente on cliente.PersonaID = pr.ClienteId
    cross apply (
        select sum(df.Cantidad * df.Precio) as TotalFactura
        from DetalleFactura df
        where df.FacturaID = pr.FacturaId
    ) as tCostos
        
    where
        pr.Id = @ReservaId
END