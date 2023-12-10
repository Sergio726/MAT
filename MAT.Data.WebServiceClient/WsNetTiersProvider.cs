
#region Using directives

using System;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using MAT.Entities;
using MAT.Data.Bases;

#endregion

namespace MAT.Data.WebServiceClient
{
	/// <summary>
	/// The WebService client data provider.
	/// </summary>
	public sealed class WsNetTiersProvider : MAT.Data.Bases.NetTiersProvider
	{
		private static object syncRoot = new Object();
		private string _applicationName;
		private string url;
        
		/// <summary>
		/// Initializes a new instance of the <see cref="WsNetTiersProvider"/> class.
		///</summary>
		public WsNetTiersProvider()
		{			
		}
		
		/// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"></see> on a provider after the provider has already been initialized.</exception>
        /// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
		public override void Initialize(string name, NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Assign the provider a default name if it doesn't have one
            if (String.IsNullOrEmpty(name))
            {
                name = "SqlNetTiersProvider";
            }

            // Add a default "description" attribute to config if the
            // attribute doesn't exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "NetTiers Sql provider");
            }

            // Call the base class's Initialize method
            base.Initialize(name, config);

            // Initialize _applicationName
            _applicationName = config["applicationName"];

            if (string.IsNullOrEmpty(_applicationName))
            {
                _applicationName = "/";
            }
            config.Remove("applicationName");


            #region Initialize Url
            string url  = config["url"];
           	if (string.IsNullOrEmpty(url))
            {
                throw new ProviderException("Empty or missing url");
            }
            this.url = url;
            config.Remove("url");
            #endregion

        }
        
		/// <summary>
		/// Current Url for WebService EndPoint
		/// </summary>
        public string Url
        {
        	get {return this.url;}
        	set {this.url = value;}
        }
		
		/// <summary>
		/// Creates a new <see cref="TransactionManager"/> instance from the current datasource.
		/// </summary>
		/// <returns></returns>
		public override TransactionManager CreateTransaction()
		{
			throw new NotSupportedException("Transactions are not supported by the webservice client.");
		}
		
		///<summary>
		/// Indicates if the current <see cref="NetTiersProvider"/> implementation supports Transacton.
		///</summary>
		public override bool IsTransactionSupported
		{
			get
			{
				return false;
			}
		}

			
		private WsViajeHotelProvider innerViajeHotelProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="ViajeHotel"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ViajeHotelProviderBase ViajeHotelProvider
		{
			get
			{
				if (innerViajeHotelProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerViajeHotelProvider == null)
						{
							this.innerViajeHotelProvider = new WsViajeHotelProvider(this.url);
						}
					}
				}
				return innerViajeHotelProvider;
			}
		}
		
			
		private WsPaqueteServicioProvider innerPaqueteServicioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PaqueteServicio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaqueteServicioProviderBase PaqueteServicioProvider
		{
			get
			{
				if (innerPaqueteServicioProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPaqueteServicioProvider == null)
						{
							this.innerPaqueteServicioProvider = new WsPaqueteServicioProvider(this.url);
						}
					}
				}
				return innerPaqueteServicioProvider;
			}
		}
		
			
		private WsPasajeroProvider innerPasajeroProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Pasajero"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PasajeroProviderBase PasajeroProvider
		{
			get
			{
				if (innerPasajeroProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPasajeroProvider == null)
						{
							this.innerPasajeroProvider = new WsPasajeroProvider(this.url);
						}
					}
				}
				return innerPasajeroProvider;
			}
		}
		
			
		private WsPasajeroMenorProvider innerPasajeroMenorProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PasajeroMenor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PasajeroMenorProviderBase PasajeroMenorProvider
		{
			get
			{
				if (innerPasajeroMenorProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPasajeroMenorProvider == null)
						{
							this.innerPasajeroMenorProvider = new WsPasajeroMenorProvider(this.url);
						}
					}
				}
				return innerPasajeroMenorProvider;
			}
		}
		
			
		private WsPersonaProvider innerPersonaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Persona"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PersonaProviderBase PersonaProvider
		{
			get
			{
				if (innerPersonaProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPersonaProvider == null)
						{
							this.innerPersonaProvider = new WsPersonaProvider(this.url);
						}
					}
				}
				return innerPersonaProvider;
			}
		}
		
			
		private WsPlanillaProvider innerPlanillaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Planilla"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PlanillaProviderBase PlanillaProvider
		{
			get
			{
				if (innerPlanillaProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPlanillaProvider == null)
						{
							this.innerPlanillaProvider = new WsPlanillaProvider(this.url);
						}
					}
				}
				return innerPlanillaProvider;
			}
		}
		
			
		private WsPlanillaHabitacionItemProvider innerPlanillaHabitacionItemProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PlanillaHabitacionItem"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PlanillaHabitacionItemProviderBase PlanillaHabitacionItemProvider
		{
			get
			{
				if (innerPlanillaHabitacionItemProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPlanillaHabitacionItemProvider == null)
						{
							this.innerPlanillaHabitacionItemProvider = new WsPlanillaHabitacionItemProvider(this.url);
						}
					}
				}
				return innerPlanillaHabitacionItemProvider;
			}
		}
		
			
		private WsPlanillaServicioItemProvider innerPlanillaServicioItemProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PlanillaServicioItem"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PlanillaServicioItemProviderBase PlanillaServicioItemProvider
		{
			get
			{
				if (innerPlanillaServicioItemProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPlanillaServicioItemProvider == null)
						{
							this.innerPlanillaServicioItemProvider = new WsPlanillaServicioItemProvider(this.url);
						}
					}
				}
				return innerPlanillaServicioItemProvider;
			}
		}
		
			
		private WsPaqueteExcursionProvider innerPaqueteExcursionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PaqueteExcursion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaqueteExcursionProviderBase PaqueteExcursionProvider
		{
			get
			{
				if (innerPaqueteExcursionProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPaqueteExcursionProvider == null)
						{
							this.innerPaqueteExcursionProvider = new WsPaqueteExcursionProvider(this.url);
						}
					}
				}
				return innerPaqueteExcursionProvider;
			}
		}
		
			
		private WsPrecioProvider innerPrecioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Precio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PrecioProviderBase PrecioProvider
		{
			get
			{
				if (innerPrecioProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPrecioProvider == null)
						{
							this.innerPrecioProvider = new WsPrecioProvider(this.url);
						}
					}
				}
				return innerPrecioProvider;
			}
		}
		
			
		private WsPrecioServicioProvider innerPrecioServicioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PrecioServicio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PrecioServicioProviderBase PrecioServicioProvider
		{
			get
			{
				if (innerPrecioServicioProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPrecioServicioProvider == null)
						{
							this.innerPrecioServicioProvider = new WsPrecioServicioProvider(this.url);
						}
					}
				}
				return innerPrecioServicioProvider;
			}
		}
		
			
		private WsProveedorProvider innerProveedorProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Proveedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ProveedorProviderBase ProveedorProvider
		{
			get
			{
				if (innerProveedorProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerProveedorProvider == null)
						{
							this.innerProveedorProvider = new WsProveedorProvider(this.url);
						}
					}
				}
				return innerProveedorProvider;
			}
		}
		
			
		private WsProvinciaProvider innerProvinciaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Provincia"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ProvinciaProviderBase ProvinciaProvider
		{
			get
			{
				if (innerProvinciaProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerProvinciaProvider == null)
						{
							this.innerProvinciaProvider = new WsProvinciaProvider(this.url);
						}
					}
				}
				return innerProvinciaProvider;
			}
		}
		
			
		private WsTransporteProvider innerTransporteProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Transporte"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override TransporteProviderBase TransporteProvider
		{
			get
			{
				if (innerTransporteProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerTransporteProvider == null)
						{
							this.innerTransporteProvider = new WsTransporteProvider(this.url);
						}
					}
				}
				return innerTransporteProvider;
			}
		}
		
			
		private WsReservaHabitacionProvider innerReservaHabitacionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="ReservaHabitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ReservaHabitacionProviderBase ReservaHabitacionProvider
		{
			get
			{
				if (innerReservaHabitacionProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerReservaHabitacionProvider == null)
						{
							this.innerReservaHabitacionProvider = new WsReservaHabitacionProvider(this.url);
						}
					}
				}
				return innerReservaHabitacionProvider;
			}
		}
		
			
		private WsServicioProvider innerServicioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Servicio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ServicioProviderBase ServicioProvider
		{
			get
			{
				if (innerServicioProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerServicioProvider == null)
						{
							this.innerServicioProvider = new WsServicioProvider(this.url);
						}
					}
				}
				return innerServicioProvider;
			}
		}
		
			
		private WsVendedorProvider innerVendedorProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Vendedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override VendedorProviderBase VendedorProvider
		{
			get
			{
				if (innerVendedorProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerVendedorProvider == null)
						{
							this.innerVendedorProvider = new WsVendedorProvider(this.url);
						}
					}
				}
				return innerVendedorProvider;
			}
		}
		
			
		private WsViajeProvider innerViajeProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Viaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ViajeProviderBase ViajeProvider
		{
			get
			{
				if (innerViajeProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerViajeProvider == null)
						{
							this.innerViajeProvider = new WsViajeProvider(this.url);
						}
					}
				}
				return innerViajeProvider;
			}
		}
		
			
		private WsPasajeProvider innerPasajeProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Pasaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PasajeProviderBase PasajeProvider
		{
			get
			{
				if (innerPasajeProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPasajeProvider == null)
						{
							this.innerPasajeProvider = new WsPasajeProvider(this.url);
						}
					}
				}
				return innerPasajeProvider;
			}
		}
		
			
		private WsPaquetePrecioProvider innerPaquetePrecioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PaquetePrecio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaquetePrecioProviderBase PaquetePrecioProvider
		{
			get
			{
				if (innerPaquetePrecioProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPaquetePrecioProvider == null)
						{
							this.innerPaquetePrecioProvider = new WsPaquetePrecioProvider(this.url);
						}
					}
				}
				return innerPaquetePrecioProvider;
			}
		}
		
			
		private WsPrecioHabitacionProvider innerPrecioHabitacionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PrecioHabitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PrecioHabitacionProviderBase PrecioHabitacionProvider
		{
			get
			{
				if (innerPrecioHabitacionProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPrecioHabitacionProvider == null)
						{
							this.innerPrecioHabitacionProvider = new WsPrecioHabitacionProvider(this.url);
						}
					}
				}
				return innerPrecioHabitacionProvider;
			}
		}
		
			
		private WsVoucherProvider innerVoucherProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Voucher"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override VoucherProviderBase VoucherProvider
		{
			get
			{
				if (innerVoucherProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerVoucherProvider == null)
						{
							this.innerVoucherProvider = new WsVoucherProvider(this.url);
						}
					}
				}
				return innerVoucherProvider;
			}
		}
		
			
		private WsAdicionalProvider innerAdicionalProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Adicional"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override AdicionalProviderBase AdicionalProvider
		{
			get
			{
				if (innerAdicionalProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerAdicionalProvider == null)
						{
							this.innerAdicionalProvider = new WsAdicionalProvider(this.url);
						}
					}
				}
				return innerAdicionalProvider;
			}
		}
		
			
		private WsPaqueteAdicionalProvider innerPaqueteAdicionalProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PaqueteAdicional"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaqueteAdicionalProviderBase PaqueteAdicionalProvider
		{
			get
			{
				if (innerPaqueteAdicionalProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPaqueteAdicionalProvider == null)
						{
							this.innerPaqueteAdicionalProvider = new WsPaqueteAdicionalProvider(this.url);
						}
					}
				}
				return innerPaqueteAdicionalProvider;
			}
		}
		
			
		private WsAuditFacturaProvider innerAuditFacturaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="AuditFactura"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override AuditFacturaProviderBase AuditFacturaProvider
		{
			get
			{
				if (innerAuditFacturaProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerAuditFacturaProvider == null)
						{
							this.innerAuditFacturaProvider = new WsAuditFacturaProvider(this.url);
						}
					}
				}
				return innerAuditFacturaProvider;
			}
		}
		
			
		private WsButacaProvider innerButacaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Butaca"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ButacaProviderBase ButacaProvider
		{
			get
			{
				if (innerButacaProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerButacaProvider == null)
						{
							this.innerButacaProvider = new WsButacaProvider(this.url);
						}
					}
				}
				return innerButacaProvider;
			}
		}
		
			
		private WsCiudadProvider innerCiudadProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Ciudad"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override CiudadProviderBase CiudadProvider
		{
			get
			{
				if (innerCiudadProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerCiudadProvider == null)
						{
							this.innerCiudadProvider = new WsCiudadProvider(this.url);
						}
					}
				}
				return innerCiudadProvider;
			}
		}
		
			
		private WsClienteProvider innerClienteProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Cliente"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ClienteProviderBase ClienteProvider
		{
			get
			{
				if (innerClienteProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerClienteProvider == null)
						{
							this.innerClienteProvider = new WsClienteProvider(this.url);
						}
					}
				}
				return innerClienteProvider;
			}
		}
		
			
		private WsCuentaProvider innerCuentaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Cuenta"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override CuentaProviderBase CuentaProvider
		{
			get
			{
				if (innerCuentaProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerCuentaProvider == null)
						{
							this.innerCuentaProvider = new WsCuentaProvider(this.url);
						}
					}
				}
				return innerCuentaProvider;
			}
		}
		
			
		private WsCuentaCorrienteProvider innerCuentaCorrienteProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="CuentaCorriente"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override CuentaCorrienteProviderBase CuentaCorrienteProvider
		{
			get
			{
				if (innerCuentaCorrienteProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerCuentaCorrienteProvider == null)
						{
							this.innerCuentaCorrienteProvider = new WsCuentaCorrienteProvider(this.url);
						}
					}
				}
				return innerCuentaCorrienteProvider;
			}
		}
		
			
		private WsDebitoProvider innerDebitoProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Debito"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override DebitoProviderBase DebitoProvider
		{
			get
			{
				if (innerDebitoProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerDebitoProvider == null)
						{
							this.innerDebitoProvider = new WsDebitoProvider(this.url);
						}
					}
				}
				return innerDebitoProvider;
			}
		}
		
			
		private WsDepartamentoProvider innerDepartamentoProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Departamento"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override DepartamentoProviderBase DepartamentoProvider
		{
			get
			{
				if (innerDepartamentoProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerDepartamentoProvider == null)
						{
							this.innerDepartamentoProvider = new WsDepartamentoProvider(this.url);
						}
					}
				}
				return innerDepartamentoProvider;
			}
		}
		
			
		private WsEstadoPasajeProvider innerEstadoPasajeProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="EstadoPasaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override EstadoPasajeProviderBase EstadoPasajeProvider
		{
			get
			{
				if (innerEstadoPasajeProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerEstadoPasajeProvider == null)
						{
							this.innerEstadoPasajeProvider = new WsEstadoPasajeProvider(this.url);
						}
					}
				}
				return innerEstadoPasajeProvider;
			}
		}
		
			
		private WsPaqueteProvider innerPaqueteProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Paquete"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaqueteProviderBase PaqueteProvider
		{
			get
			{
				if (innerPaqueteProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPaqueteProvider == null)
						{
							this.innerPaqueteProvider = new WsPaqueteProvider(this.url);
						}
					}
				}
				return innerPaqueteProvider;
			}
		}
		
			
		private WsExcursionProvider innerExcursionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Excursion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ExcursionProviderBase ExcursionProvider
		{
			get
			{
				if (innerExcursionProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerExcursionProvider == null)
						{
							this.innerExcursionProvider = new WsExcursionProvider(this.url);
						}
					}
				}
				return innerExcursionProvider;
			}
		}
		
			
		private WsHabitacionProvider innerHabitacionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Habitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override HabitacionProviderBase HabitacionProvider
		{
			get
			{
				if (innerHabitacionProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerHabitacionProvider == null)
						{
							this.innerHabitacionProvider = new WsHabitacionProvider(this.url);
						}
					}
				}
				return innerHabitacionProvider;
			}
		}
		
			
		private WsHabitacionTipoProvider innerHabitacionTipoProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="HabitacionTipo"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override HabitacionTipoProviderBase HabitacionTipoProvider
		{
			get
			{
				if (innerHabitacionTipoProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerHabitacionTipoProvider == null)
						{
							this.innerHabitacionTipoProvider = new WsHabitacionTipoProvider(this.url);
						}
					}
				}
				return innerHabitacionTipoProvider;
			}
		}
		
			
		private WsHistorialProvider innerHistorialProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Historial"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override HistorialProviderBase HistorialProvider
		{
			get
			{
				if (innerHistorialProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerHistorialProvider == null)
						{
							this.innerHistorialProvider = new WsHistorialProvider(this.url);
						}
					}
				}
				return innerHistorialProvider;
			}
		}
		
			
		private WsLocalidadProvider innerLocalidadProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Localidad"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override LocalidadProviderBase LocalidadProvider
		{
			get
			{
				if (innerLocalidadProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerLocalidadProvider == null)
						{
							this.innerLocalidadProvider = new WsLocalidadProvider(this.url);
						}
					}
				}
				return innerLocalidadProvider;
			}
		}
		
			
		private WsHotelProvider innerHotelProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Hotel"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override HotelProviderBase HotelProvider
		{
			get
			{
				if (innerHotelProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerHotelProvider == null)
						{
							this.innerHotelProvider = new WsHotelProvider(this.url);
						}
					}
				}
				return innerHotelProvider;
			}
		}
		
			
		private WsPasajeAdicionalProvider innerPasajeAdicionalProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PasajeAdicional"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PasajeAdicionalProviderBase PasajeAdicionalProvider
		{
			get
			{
				if (innerPasajeAdicionalProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPasajeAdicionalProvider == null)
						{
							this.innerPasajeAdicionalProvider = new WsPasajeAdicionalProvider(this.url);
						}
					}
				}
				return innerPasajeAdicionalProvider;
			}
		}
		
			
		private WsNotaProvider innerNotaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Nota"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override NotaProviderBase NotaProvider
		{
			get
			{
				if (innerNotaProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerNotaProvider == null)
						{
							this.innerNotaProvider = new WsNotaProvider(this.url);
						}
					}
				}
				return innerNotaProvider;
			}
		}
		
			
		private WsPagoProvider innerPagoProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Pago"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PagoProviderBase PagoProvider
		{
			get
			{
				if (innerPagoProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPagoProvider == null)
						{
							this.innerPagoProvider = new WsPagoProvider(this.url);
						}
					}
				}
				return innerPagoProvider;
			}
		}
		
			
		private WsFacturaProvider innerFacturaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Factura"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override FacturaProviderBase FacturaProvider
		{
			get
			{
				if (innerFacturaProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerFacturaProvider == null)
						{
							this.innerFacturaProvider = new WsFacturaProvider(this.url);
						}
					}
				}
				return innerFacturaProvider;
			}
		}
		
			
		private WsMovimientoCuentaProvider innerMovimientoCuentaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="MovimientoCuenta"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override MovimientoCuentaProviderBase MovimientoCuentaProvider
		{
			get
			{
				if (innerMovimientoCuentaProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerMovimientoCuentaProvider == null)
						{
							this.innerMovimientoCuentaProvider = new WsMovimientoCuentaProvider(this.url);
						}
					}
				}
				return innerMovimientoCuentaProvider;
			}
		}
		
			
		private WsPaisProvider innerPaisProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Pais"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaisProviderBase PaisProvider
		{
			get
			{
				if (innerPaisProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPaisProvider == null)
						{
							this.innerPaisProvider = new WsPaisProvider(this.url);
						}
					}
				}
				return innerPaisProvider;
			}
		}
		
		
			
		private WsPasajeroViajeProvider innerPasajeroViajeProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PasajeroViaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PasajeroViajeProviderBase PasajeroViajeProvider
		{
			get
			{
				if (innerPasajeroViajeProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerPasajeroViajeProvider == null)
						{
							this.innerPasajeroViajeProvider = new WsPasajeroViajeProvider(this.url);
						}
					}
				}
				return innerPasajeroViajeProvider;
			}
		}
		
			
		private WsPersonaClienteProvider innerPersonaClienteProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PersonaCliente"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PersonaClienteProviderBase PersonaClienteProvider
		{
			get
			{
				if (innerPersonaClienteProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerPersonaClienteProvider == null)
						{
							this.innerPersonaClienteProvider = new WsPersonaClienteProvider(this.url);
						}
					}
				}
				return innerPersonaClienteProvider;
			}
		}
		
			
		private WsPersonaPasajeroProvider innerPersonaPasajeroProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PersonaPasajero"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PersonaPasajeroProviderBase PersonaPasajeroProvider
		{
			get
			{
				if (innerPersonaPasajeroProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerPersonaPasajeroProvider == null)
						{
							this.innerPersonaPasajeroProvider = new WsPersonaPasajeroProvider(this.url);
						}
					}
				}
				return innerPersonaPasajeroProvider;
			}
		}
		
			
		private WsPersonaProveedorProvider innerPersonaProveedorProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PersonaProveedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PersonaProveedorProviderBase PersonaProveedorProvider
		{
			get
			{
				if (innerPersonaProveedorProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerPersonaProveedorProvider == null)
						{
							this.innerPersonaProveedorProvider = new WsPersonaProveedorProvider(this.url);
						}
					}
				}
				return innerPersonaProveedorProvider;
			}
		}
		
			
		private WsPersonaVendedorProvider innerPersonaVendedorProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PersonaVendedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PersonaVendedorProviderBase PersonaVendedorProvider
		{
			get
			{
				if (innerPersonaVendedorProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerPersonaVendedorProvider == null)
						{
							this.innerPersonaVendedorProvider = new WsPersonaVendedorProvider(this.url);
						}
					}
				}
				return innerPersonaVendedorProvider;
			}
		}
		
			
		private WsReservaProvider innerReservaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Reserva"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ReservaProviderBase ReservaProvider
		{
			get
			{
				if (innerReservaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerReservaProvider == null)
						{
							this.innerReservaProvider = new WsReservaProvider(this.url);
						}
					}
				}
				return innerReservaProvider;
			}
		}
		
			
		private WsVConsultaReservaHabitacionProvider innerVConsultaReservaHabitacionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="VConsultaReservaHabitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override VConsultaReservaHabitacionProviderBase VConsultaReservaHabitacionProvider
		{
			get
			{
				if (innerVConsultaReservaHabitacionProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerVConsultaReservaHabitacionProvider == null)
						{
							this.innerVConsultaReservaHabitacionProvider = new WsVConsultaReservaHabitacionProvider(this.url);
						}
					}
				}
				return innerVConsultaReservaHabitacionProvider;
			}
		}
		
			
		private WsVLocalidadProvider innerVLocalidadProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="VLocalidad"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override VLocalidadProviderBase VLocalidadProvider
		{
			get
			{
				if (innerVLocalidadProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerVLocalidadProvider == null)
						{
							this.innerVLocalidadProvider = new WsVLocalidadProvider(this.url);
						}
					}
				}
				return innerVLocalidadProvider;
			}
		}
		
			
		private WsVPersonaProvider innerVPersonaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="VPersona"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override VPersonaProviderBase VPersonaProvider
		{
			get
			{
				if (innerVPersonaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerVPersonaProvider == null)
						{
							this.innerVPersonaProvider = new WsVPersonaProvider(this.url);
						}
					}
				}
				return innerVPersonaProvider;
			}
		}
		
		
		#region "General data access methods"

		#region "ExecuteNonQuery"
		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
		{
			WsProxy.MATServices proxy = new WsProxy.MATServices();
			proxy.Url = this.url;
			return proxy.ExecuteNonQuery(storedProcedureName, parameterValues);
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		public override void ExecuteNonQuery(DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		public override void ExecuteNonQuery(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}


		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(CommandType commandType, string commandText)
		{
			WsProxy.MATServices proxy = new WsProxy.MATServices();
			proxy.Url = this.url;
			return proxy.ExecuteNonQuery((WsProxy.CommandType)Enum.Parse(typeof(WsProxy.CommandType), commandType.ToString(), false), commandText);
		}
		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");
		}
		#endregion

		#region "ExecuteDataReader"
		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(DbCommand commandWrapper)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}


		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(CommandType commandType, string commandText)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}
		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}
		#endregion

		#region "ExecuteDataSet"
		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues)
		{
			WsProxy.MATServices proxy = new WsProxy.MATServices();
			proxy.Url = this.url;
			return proxy.ExecuteDataSet(storedProcedureName, parameterValues);
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}


		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(CommandType commandType, string commandText)
		{
			WsProxy.MATServices proxy = new WsProxy.MATServices();
			proxy.Url = this.url;
			return proxy.ExecuteDataSet((WsProxy.CommandType)Enum.Parse(typeof(WsProxy.CommandType), commandType.ToString(), false), commandText);
		}
		
		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");			
		}
		#endregion

		#region "ExecuteScalar"
		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override object ExecuteScalar(string storedProcedureName, params object[] parameterValues)
		{
			WsProxy.MATServices proxy = new WsProxy.MATServices();
			proxy.Url = this.url;
			return proxy.ExecuteScalar(storedProcedureName, parameterValues);
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override object ExecuteScalar(DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override object ExecuteScalar(CommandType commandType, string commandText)
		{
			WsProxy.MATServices proxy = new WsProxy.MATServices();
			proxy.Url = this.url;
			return proxy.ExecuteScalar((WsProxy.CommandType)Enum.Parse(typeof(WsProxy.CommandType), commandType.ToString(), false), commandText);	
		}
		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");		
		}
		#endregion

		#endregion
	}
}
