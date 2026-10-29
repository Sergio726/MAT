CREATE PROCEDURE [dbo].[usp_MAT_ClienteContacto_Insert]
(
    @ClienteID   UNIQUEIDENTIFIER,
    @VendedorID  UNIQUEIDENTIFIER = NULL,
    @Usuario     VARCHAR(100)     = NULL,
    @Resultado   INT,
    @Observacion VARCHAR(500)     = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Registra un contacto con el cliente (saludo, oferta, resultado).
  --              Resultado: 1 Saludo cumpleaños, 2 Ofrecido viaje, 3 Interesado, 4 Compró,
  --              5 No le interesa, 6 No contesta, 7 Número equivocado, 8 No volver a contactar.
  --              Devuelve el ID creado.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;

    DECLARE @ID UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.ClienteContacto (ClienteContactoID, ClienteID, VendedorID, Usuario, Resultado, Observacion)
    VALUES (@ID, @ClienteID, @VendedorID, @Usuario, @Resultado, NULLIF(LTRIM(RTRIM(@Observacion)), ''));

    SELECT @ID AS ClienteContactoID;
END
