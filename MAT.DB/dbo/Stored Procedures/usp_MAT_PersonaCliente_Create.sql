CREATE PROCEDURE dbo.usp_MAT_PersonaCliente_Create ( @Apellido VARCHAR(100),
													 @Nombre VARCHAR(100),
													 @NroDocumento VARCHAR(50),
													 @TipoDocumento INT,
													 @Celular VARCHAR(50) = NULL,
													 @Telefono VARCHAR(50) =NULL,
													@Email VARCHAR(50) = NULL,
													@FechaNacimiento DATE,
													@LocalidadID INT,
													@Domicilio VARCHAR(100),
													@Sexo INT,
													@Nacionalidad VARCHAR(50),
													@PaisResidencia VARCHAR(50),
													@Provincia INT,
													@RazonSocial VARCHAR(50) = NULL,
													@Cuit VARCHAR(50) = NULL,
													@Empresa VARCHAR(50) = NULL,
													@Ocupacion VARCHAR(50) = NULL ,
													@FormaPago INT = NULL,
													@CondicionIva INT = NULL,
													@VendedorID uniqueidentifier = NULL, 
													@Fax VARCHAR(50) = NULL,
													@Web VARCHAR(50) = NULL,
													@Idioma VARCHAR(50) = NULL,
													@Promotor VARCHAR(50) = NULL,
													@Observacion VARCHAR(250) = NULL,
													@TipoID INT
													)
/*
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 07/11/2017
  -- Description:  Create Pasajero
  --History
  2018-03-11	Garcia Sergio quit Moneda
  -- ============================================= 
*/
AS
BEGIN
	

	BEGIN TRY
		DECLARE @PersonaClienteID uniqueidentifier
		SET @PersonaClienteID = NEWID()

		BEGIN TRAN
			
			/*INSERT PERSONA*/
			INSERT INTO dbo.Persona (PersonaID,
									 Apellido		 ,
									 Nombre			 ,
									 TipoDocumento	 ,
									 NroDocumento	 ,
									 Celular		 ,
									 Telefono		 ,
									 Email			 ,
									 FechaNacimiento ,
									 LocalidadID	 ,
									 Domicilio		 ,
									 Sexo			 ,
									 Ocupacion		 ,
									 Nacionalidad	 ,
									 PaisResidencia	 ,
									 Provincia		 
									 )
			VALUES
			(
				@PersonaClienteID,
				@Apellido		 ,
				@Nombre			 ,
				@TipoDocumento	 ,
				@NroDocumento	 ,
				@Celular		 ,
				@Telefono		 ,
				@Email			 ,
				@FechaNacimiento ,
				@LocalidadID	 ,
				@Domicilio		 ,
				@Sexo			 ,
				@Ocupacion		 ,
				@Nacionalidad	 ,
				@PaisResidencia	 ,
				@Provincia
			)

			/*INSERT CLIENTE*/
			INSERT INTO	dbo.Cliente
			(
			  ClienteID	  ,
			  RazonSocial ,
			  Cuit		  ,
			  Empresa	  ,
			  Ocupacion	  ,
			  FormaPago	  ,
			  CondicionIva,
			  VendedorID  ,
			  Fax		  ,
			  Web		  ,
			  Idioma	  ,
			  Promotor	  ,
			  Observacion ,
			  TipoID	  ,
			  FechaAlta
			)
			VALUES
			(
				@PersonaClienteID ,
				@RazonSocial ,
				@Cuit		 ,
				@Empresa	 ,
				@Ocupacion	 ,
				@FormaPago	 ,
				@CondicionIva,
				@VendedorID	 ,
				@Fax		 ,
				@Web		 ,
				@Idioma		 ,
				@Promotor	 ,
				@Observacion ,
				@TipoID		 ,
				GETDATE()
			)

			/*INSERT CuentaCorriente*/
			INSERT INTO dbo.Cuenta
			(
			 CuentaID,
			 ClienteID,
			 Estado
			)
			VALUES
			(
				NEWID(),
				@PersonaClienteID,
				1
			)

			/*INSERT PASAJERO*/
			INSERT INTO Pasajero (PasajeroID) VALUES (@PersonaClienteID)

			COMMIT TRAN; 
				
	END TRY
	BEGIN CATCH
			IF @@TRANCOUNT > 0 
			ROLLBACK TRAN

			 DECLARE @errmsg   AS NVARCHAR (2048),
					@errorProc VARCHAR(50),
					@ErrorSeverity INT,
				    @ErrorState INT

		    SELECT 
			    @errmsg = CONCAT('*** ', Error_message(),'Error Line:',ERROR_LINE()),
				@ErrorSeverity = ERROR_SEVERITY(),
				@ErrorState = ERROR_STATE();
           
			 RAISERROR (@errmsg, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );

	END CATCH
END
