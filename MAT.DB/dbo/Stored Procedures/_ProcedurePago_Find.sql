
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Pago table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePago_Find]
(

	@SearchUsingOR bit   = null ,

	@PagoId uniqueidentifier   = null ,

	@FechaPago datetime   = null ,

	@Monto float   = null ,

	@TipoPago int   = null ,

	@TransaccionId varchar (100)  = null ,

	@ClienteId uniqueidentifier   = null ,

	@VendedorId uniqueidentifier   = null ,

	@NroRecibo varchar (50)  = null ,

	@EstadoRendicion int   = null ,

	@CuentaCorrienteId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PagoID]
	, [FechaPago]
	, [Monto]
	, [TipoPago]
	, [VendedorId]
	, [NroRecibo]
    FROM
	[dbo].[Pago]
    WHERE 
	 ([PagoID] = @PagoId OR @PagoId IS NULL)
	AND ([FechaPago] = @FechaPago OR @FechaPago IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
	AND ([TipoPago] = @TipoPago OR @TipoPago IS NULL)
	AND ([VendedorId] = @VendedorId OR @VendedorId IS NULL)
	AND ([NroRecibo] = @NroRecibo OR @NroRecibo IS NULL)
					
  END
  ELSE
  BEGIN
    SELECT
	  [PagoID]
	, [FechaPago]
	, [Monto]
	, [TipoPago]
	, [VendedorId]
	, [NroRecibo]
    FROM
	[dbo].[Pago]
    WHERE 
	 ([PagoID] = @PagoId AND @PagoId is not null)
	OR ([FechaPago] = @FechaPago AND @FechaPago is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	OR ([TipoPago] = @TipoPago AND @TipoPago is not null)
	OR ([VendedorId] = @VendedorId AND @VendedorId is not null)
	OR ([NroRecibo] = @NroRecibo AND @NroRecibo is not null)
	SELECT @@ROWCOUNT			
  END

