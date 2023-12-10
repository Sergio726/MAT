#region Using directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web;
using MAT.Entities;
using MAT.Data;
using MAT.Data.Bases;

#endregion

namespace MAT.Data
{
	/// <summary>
	/// This class represents the Data source repository and gives access to all the underlying providers.
	/// </summary>
	[CLSCompliant(true)]
	public sealed class DataRepository 
	{
		private static volatile NetTiersProvider _provider = null;
        private static volatile NetTiersProviderCollection _providers = null;
		private static volatile NetTiersServiceSection _section = null;
		private static volatile Configuration _config = null;
        
        private static object SyncRoot = new object();
				
		private DataRepository()
		{
		}
		
		#region Public LoadProvider
		/// <summary>
        /// Enables the DataRepository to programatically create and 
        /// pass in a <c>NetTiersProvider</c> during runtime.
        /// </summary>
        /// <param name="provider">An instatiated NetTiersProvider.</param>
        public static void LoadProvider(NetTiersProvider provider)
        {
			LoadProvider(provider, false);
        }
		
		/// <summary>
        /// Enables the DataRepository to programatically create and 
        /// pass in a <c>NetTiersProvider</c> during runtime.
        /// </summary>
        /// <param name="provider">An instatiated NetTiersProvider.</param>
        /// <param name="setAsDefault">ability to set any valid provider as the default provider for the DataRepository.</param>
		public static void LoadProvider(NetTiersProvider provider, bool setAsDefault)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (_providers == null)
			{
				lock(SyncRoot)
				{
            		if (_providers == null)
						_providers = new NetTiersProviderCollection();
				}
			}
			
            if (_providers[provider.Name] == null)
            {
                lock (_providers.SyncRoot)
                {
                    _providers.Add(provider);
                }
            }

            if (_provider == null || setAsDefault)
            {
                lock (SyncRoot)
                {
                    if(_provider == null || setAsDefault)
                         _provider = provider;
                }
            }
        }
		#endregion 
		
		///<summary>
		/// Configuration based provider loading, will load the providers on first call.
		///</summary>
		private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_provider == null)
            {
                lock (SyncRoot)
                {
                    // Do this again to make sure _provider is still null
                    if (_provider == null)
                    {
                        // Load registered providers and point _provider to the default provider
                        _providers = new NetTiersProviderCollection();

                        ProvidersHelper.InstantiateProviders(NetTiersSection.Providers, _providers, typeof(NetTiersProvider));
						_provider = _providers[NetTiersSection.DefaultProvider];

                        if (_provider == null)
                        {
                            throw new ProviderException("Unable to load default NetTiersProvider");
                        }
                    }
                }
            }
        }

		/// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public static NetTiersProvider Provider
        {
            get { LoadProviders(); return _provider; }
        }

		/// <summary>
        /// Gets the provider collection.
        /// </summary>
        /// <value>The providers.</value>
        public static NetTiersProviderCollection Providers
        {
            get { LoadProviders(); return _providers; }
        }
		
		/// <summary>
		/// Creates a new <see cref="TransactionManager"/> instance from the current datasource.
		/// </summary>
		/// <returns></returns>
		public TransactionManager CreateTransaction()
		{
			return _provider.CreateTransaction();
		}

		#region Configuration

		/// <summary>
		/// Gets a reference to the configured NetTiersServiceSection object.
		/// </summary>
		public static NetTiersServiceSection NetTiersSection
		{
			get
			{
				// Try to get a reference to the default <netTiersService> section
				_section = WebConfigurationManager.GetSection("netTiersService") as NetTiersServiceSection;

				if ( _section == null )
				{
					// otherwise look for section based on the assembly name
					_section = WebConfigurationManager.GetSection("MAT.Data") as NetTiersServiceSection;
				}

				#region Design-Time Support

				if ( _section == null )
				{
					// lastly, try to find the specific NetTiersServiceSection for this assembly
					foreach ( ConfigurationSection temp in Configuration.Sections )
					{
						if ( temp is NetTiersServiceSection )
						{
							_section = temp as NetTiersServiceSection;
							break;
						}
					}
				}

				#endregion Design-Time Support
				
				if ( _section == null )
				{
					throw new ProviderException("Unable to load NetTiersServiceSection");
				}

				return _section;
			}
		}

		#region Design-Time Support

		/// <summary>
		/// Gets a reference to the application configuration object.
		/// </summary>
		public static Configuration Configuration
		{
			get
			{
				if ( _config == null )
				{
					// load specific config file
					if ( HttpContext.Current != null )
					{
						_config = WebConfigurationManager.OpenWebConfiguration("~");
					}
					else
					{
						String configFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".config", "").Replace(".temp", "");

						// check for design mode
						if ( configFile.ToLower().Contains("devenv.exe") )
						{
							_config = GetDesignTimeConfig();
						}
						else
						{
							_config = ConfigurationManager.OpenExeConfiguration(configFile);
						}
					}
				}

				return _config;
			}
		}

		private static Configuration GetDesignTimeConfig()
		{
			ExeConfigurationFileMap configMap = null;
			Configuration config = null;
			String path = null;

			// Get an instance of the currently running Visual Studio IDE.
			EnvDTE80.DTE2 dte = (EnvDTE80.DTE2) System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.10.0");
			
			if ( dte != null )
			{
				dte.SuppressUI = true;

				EnvDTE.ProjectItem item = dte.Solution.FindProjectItem("web.config");
				if ( item != null )
				{
					if (!item.ContainingProject.FullName.ToLower().StartsWith("http:"))
               {
                  System.IO.FileInfo info = new System.IO.FileInfo(item.ContainingProject.FullName);
                  path = String.Format("{0}\\{1}", info.Directory.FullName, item.Name);
                  configMap = new ExeConfigurationFileMap();
                  configMap.ExeConfigFilename = path;
               }
               else
               {
                  configMap = new ExeConfigurationFileMap();
                  configMap.ExeConfigFilename = item.get_FileNames(0);
               }}

				/*
				Array projects = (Array) dte2.ActiveSolutionProjects;
				EnvDTE.Project project = (EnvDTE.Project) projects.GetValue(0);
				System.IO.FileInfo info;

				foreach ( EnvDTE.ProjectItem item in project.ProjectItems )
				{
					if ( String.Compare(item.Name, "web.config", true) == 0 )
					{
						info = new System.IO.FileInfo(project.FullName);
						path = String.Format("{0}\\{1}", info.Directory.FullName, item.Name);
						configMap = new ExeConfigurationFileMap();
						configMap.ExeConfigFilename = path;
						break;
					}
				}
				*/
			}

			config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
			return config;
		}

		#endregion Design-Time Support

		#endregion Configuration

		#region Connections

		/// <summary>
		/// Gets a reference to the ConnectionStringSettings collection.
		/// </summary>
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
					// use default ConnectionStrings if _section has already been discovered
					if ( _config == null && _section != null )
					{
						return WebConfigurationManager.ConnectionStrings;
					}
					
					return Configuration.ConnectionStrings.ConnectionStrings;
			}
		}

		// dictionary of connection providers
		private static Dictionary<String, ConnectionProvider> _connections;

		/// <summary>
		/// Gets the dictionary of connection providers.
		/// </summary>
		public static Dictionary<String, ConnectionProvider> Connections
		{
			get
			{
				if ( _connections == null )
				{
					lock (SyncRoot)
                	{
						if (_connections == null)
						{
							_connections = new Dictionary<String, ConnectionProvider>();
		
							// add a connection provider for each configured connection string
							foreach ( ConnectionStringSettings conn in ConnectionStrings )
							{
								_connections.Add(conn.Name, new ConnectionProvider(conn.Name, conn.ConnectionString));
							}
						}
					}
				}

				return _connections;
			}
		}

		/// <summary>
		/// Adds the specified connection string to the map of connection strings.
		/// </summary>
		/// <param name="connectionStringName">The connection string name.</param>
		/// <param name="connectionString">The provider specific connection information.</param>
		public static void AddConnection(String connectionStringName, String connectionString)
		{
			lock (SyncRoot)
            {
				Connections.Remove(connectionStringName);
				ConnectionProvider connection = new ConnectionProvider(connectionStringName, connectionString);
				Connections.Add(connectionStringName, connection);
			}
		}

		/// <summary>
		/// Provides ability to switch connection string at runtime.
		/// </summary>
		public sealed class ConnectionProvider
		{
			private NetTiersProvider _provider;
			private NetTiersProviderCollection _providers;
			private String _connectionStringName;
			private String _connectionString;


			/// <summary>
			/// Initializes a new instance of the ConnectionProvider class.
			/// </summary>
			/// <param name="connectionStringName">The connection string name.</param>
			/// <param name="connectionString">The provider specific connection information.</param>
			public ConnectionProvider(String connectionStringName, String connectionString)
			{
				_connectionString = connectionString;
				_connectionStringName = connectionStringName;
			}

			/// <summary>
			/// Gets the provider.
			/// </summary>
			public NetTiersProvider Provider
			{
				get { LoadProviders(); return _provider; }
			}

			/// <summary>
			/// Gets the provider collection.
			/// </summary>
			public NetTiersProviderCollection Providers
			{
				get { LoadProviders(); return _providers; }
			}

			/// <summary>
			/// Instantiates the configured providers based on the supplied connection string.
			/// </summary>
			private void LoadProviders()
			{
				DataRepository.LoadProviders();

				// Avoid claiming lock if providers are already loaded
				if ( _providers == null )
				{
					lock ( SyncRoot )
					{
						// Do this again to make sure _provider is still null
						if ( _providers == null )
						{
							// apply connection information to each provider
							for ( int i = 0; i < NetTiersSection.Providers.Count; i++ )
							{
								NetTiersSection.Providers[i].Parameters["connectionStringName"] = _connectionStringName;
								// remove previous connection string, if any
								NetTiersSection.Providers[i].Parameters.Remove("connectionString");

								if ( !String.IsNullOrEmpty(_connectionString) )
								{
									NetTiersSection.Providers[i].Parameters["connectionString"] = _connectionString;
								}
							}

							// Load registered providers and point _provider to the default provider
							_providers = new NetTiersProviderCollection();

							ProvidersHelper.InstantiateProviders(NetTiersSection.Providers, _providers, typeof(NetTiersProvider));
							_provider = _providers[NetTiersSection.DefaultProvider];
						}
					}
				}
			}
		}

		#endregion Connections

		#region Static properties
		
		#region ViajeHotelProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="ViajeHotel"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ViajeHotelProviderBase ViajeHotelProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ViajeHotelProvider;
			}
		}
		
		#endregion
		
		#region PaqueteServicioProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PaqueteServicio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PaqueteServicioProviderBase PaqueteServicioProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PaqueteServicioProvider;
			}
		}
		
		#endregion
		
		#region PasajeroProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Pasajero"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PasajeroProviderBase PasajeroProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PasajeroProvider;
			}
		}
		
		#endregion
		
		#region PasajeroMenorProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PasajeroMenor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PasajeroMenorProviderBase PasajeroMenorProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PasajeroMenorProvider;
			}
		}
		
		#endregion
		
		#region PersonaProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Persona"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PersonaProviderBase PersonaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PersonaProvider;
			}
		}
		
		#endregion
		
		#region PlanillaProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Planilla"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PlanillaProviderBase PlanillaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PlanillaProvider;
			}
		}
		
		#endregion
		
		#region PlanillaHabitacionItemProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PlanillaHabitacionItem"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PlanillaHabitacionItemProviderBase PlanillaHabitacionItemProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PlanillaHabitacionItemProvider;
			}
		}
		
		#endregion
		
		#region PlanillaServicioItemProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PlanillaServicioItem"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PlanillaServicioItemProviderBase PlanillaServicioItemProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PlanillaServicioItemProvider;
			}
		}
		
		#endregion
		
		#region PaqueteExcursionProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PaqueteExcursion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PaqueteExcursionProviderBase PaqueteExcursionProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PaqueteExcursionProvider;
			}
		}
		
		#endregion
		
		#region PrecioProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Precio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PrecioProviderBase PrecioProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PrecioProvider;
			}
		}
		
		#endregion
		
		#region PrecioServicioProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PrecioServicio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PrecioServicioProviderBase PrecioServicioProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PrecioServicioProvider;
			}
		}
		
		#endregion
		
		#region ProveedorProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Proveedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ProveedorProviderBase ProveedorProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ProveedorProvider;
			}
		}
		
		#endregion
		
		#region ProvinciaProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Provincia"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ProvinciaProviderBase ProvinciaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ProvinciaProvider;
			}
		}
		
		#endregion
		
		#region TransporteProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Transporte"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static TransporteProviderBase TransporteProvider
		{
			get 
			{
				LoadProviders();
				return _provider.TransporteProvider;
			}
		}
		
		#endregion
		
		#region ReservaHabitacionProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="ReservaHabitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ReservaHabitacionProviderBase ReservaHabitacionProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ReservaHabitacionProvider;
			}
		}
		
		#endregion
		
		#region ServicioProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Servicio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ServicioProviderBase ServicioProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ServicioProvider;
			}
		}
		
		#endregion
		
		#region VendedorProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Vendedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static VendedorProviderBase VendedorProvider
		{
			get 
			{
				LoadProviders();
				return _provider.VendedorProvider;
			}
		}
		
		#endregion
		
		#region ViajeProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Viaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ViajeProviderBase ViajeProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ViajeProvider;
			}
		}
		
		#endregion
		
		#region PasajeProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Pasaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PasajeProviderBase PasajeProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PasajeProvider;
			}
		}
		
		#endregion
		
		#region PaquetePrecioProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PaquetePrecio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PaquetePrecioProviderBase PaquetePrecioProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PaquetePrecioProvider;
			}
		}
		
		#endregion
		
		#region PrecioHabitacionProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PrecioHabitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PrecioHabitacionProviderBase PrecioHabitacionProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PrecioHabitacionProvider;
			}
		}
		
		#endregion
		
		#region VoucherProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Voucher"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static VoucherProviderBase VoucherProvider
		{
			get 
			{
				LoadProviders();
				return _provider.VoucherProvider;
			}
		}
		
		#endregion
		
		#region AdicionalProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Adicional"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static AdicionalProviderBase AdicionalProvider
		{
			get 
			{
				LoadProviders();
				return _provider.AdicionalProvider;
			}
		}
		
		#endregion
		
		#region PaqueteAdicionalProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PaqueteAdicional"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PaqueteAdicionalProviderBase PaqueteAdicionalProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PaqueteAdicionalProvider;
			}
		}
		
		#endregion
		
		#region AuditFacturaProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="AuditFactura"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static AuditFacturaProviderBase AuditFacturaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.AuditFacturaProvider;
			}
		}
		
		#endregion
		
		#region ButacaProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Butaca"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ButacaProviderBase ButacaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ButacaProvider;
			}
		}
		
		#endregion
		
		#region CiudadProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Ciudad"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static CiudadProviderBase CiudadProvider
		{
			get 
			{
				LoadProviders();
				return _provider.CiudadProvider;
			}
		}
		
		#endregion
		
		#region ClienteProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Cliente"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ClienteProviderBase ClienteProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ClienteProvider;
			}
		}
		
		#endregion
		
		#region CuentaProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Cuenta"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static CuentaProviderBase CuentaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.CuentaProvider;
			}
		}
		
		#endregion
		
		#region CuentaCorrienteProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="CuentaCorriente"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static CuentaCorrienteProviderBase CuentaCorrienteProvider
		{
			get 
			{
				LoadProviders();
				return _provider.CuentaCorrienteProvider;
			}
		}
		
		#endregion
		
		#region DebitoProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Debito"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static DebitoProviderBase DebitoProvider
		{
			get 
			{
				LoadProviders();
				return _provider.DebitoProvider;
			}
		}
		
		#endregion
		
		#region DepartamentoProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Departamento"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static DepartamentoProviderBase DepartamentoProvider
		{
			get 
			{
				LoadProviders();
				return _provider.DepartamentoProvider;
			}
		}
		
		#endregion
		
		#region EstadoPasajeProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="EstadoPasaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static EstadoPasajeProviderBase EstadoPasajeProvider
		{
			get 
			{
				LoadProviders();
				return _provider.EstadoPasajeProvider;
			}
		}
		
		#endregion
		
		#region PaqueteProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Paquete"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PaqueteProviderBase PaqueteProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PaqueteProvider;
			}
		}
		
		#endregion
		
		#region ExcursionProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Excursion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ExcursionProviderBase ExcursionProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ExcursionProvider;
			}
		}
		
		#endregion
		
		#region HabitacionProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Habitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static HabitacionProviderBase HabitacionProvider
		{
			get 
			{
				LoadProviders();
				return _provider.HabitacionProvider;
			}
		}
		
		#endregion
		
		#region HabitacionTipoProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="HabitacionTipo"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static HabitacionTipoProviderBase HabitacionTipoProvider
		{
			get 
			{
				LoadProviders();
				return _provider.HabitacionTipoProvider;
			}
		}
		
		#endregion
		
		#region HistorialProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Historial"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static HistorialProviderBase HistorialProvider
		{
			get 
			{
				LoadProviders();
				return _provider.HistorialProvider;
			}
		}
		
		#endregion
		
		#region LocalidadProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Localidad"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static LocalidadProviderBase LocalidadProvider
		{
			get 
			{
				LoadProviders();
				return _provider.LocalidadProvider;
			}
		}
		
		#endregion
		
		#region HotelProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Hotel"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static HotelProviderBase HotelProvider
		{
			get 
			{
				LoadProviders();
				return _provider.HotelProvider;
			}
		}
		
		#endregion
		
		#region PasajeAdicionalProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PasajeAdicional"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PasajeAdicionalProviderBase PasajeAdicionalProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PasajeAdicionalProvider;
			}
		}
		
		#endregion
		
		#region NotaProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Nota"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static NotaProviderBase NotaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.NotaProvider;
			}
		}
		
		#endregion
		
		#region PagoProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Pago"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PagoProviderBase PagoProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PagoProvider;
			}
		}
		
		#endregion
		
		#region FacturaProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Factura"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static FacturaProviderBase FacturaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.FacturaProvider;
			}
		}
		
		#endregion
		
		#region MovimientoCuentaProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="MovimientoCuenta"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static MovimientoCuentaProviderBase MovimientoCuentaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.MovimientoCuentaProvider;
			}
		}
		
		#endregion
		
		#region PaisProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Pais"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PaisProviderBase PaisProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PaisProvider;
			}
		}
		
		#endregion
		
		
		#region PasajeroViajeProvider
		
		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PasajeroViaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PasajeroViajeProviderBase PasajeroViajeProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PasajeroViajeProvider;
			}
		}
		
		#endregion
		
		#region PersonaClienteProvider
		
		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PersonaCliente"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PersonaClienteProviderBase PersonaClienteProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PersonaClienteProvider;
			}
		}
		
		#endregion
		
		#region PersonaPasajeroProvider
		
		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PersonaPasajero"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PersonaPasajeroProviderBase PersonaPasajeroProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PersonaPasajeroProvider;
			}
		}
		
		#endregion
		
		#region PersonaProveedorProvider
		
		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PersonaProveedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PersonaProveedorProviderBase PersonaProveedorProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PersonaProveedorProvider;
			}
		}
		
		#endregion
		
		#region PersonaVendedorProvider
		
		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="PersonaVendedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PersonaVendedorProviderBase PersonaVendedorProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PersonaVendedorProvider;
			}
		}
		
		#endregion
		
		#region ReservaProvider
		
		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Reserva"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ReservaProviderBase ReservaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ReservaProvider;
			}
		}
		
		#endregion
		
		#region VConsultaReservaHabitacionProvider
		
		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="VConsultaReservaHabitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static VConsultaReservaHabitacionProviderBase VConsultaReservaHabitacionProvider
		{
			get 
			{
				LoadProviders();
				return _provider.VConsultaReservaHabitacionProvider;
			}
		}
		
		#endregion
		
		#region VLocalidadProvider
		
		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="VLocalidad"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static VLocalidadProviderBase VLocalidadProvider
		{
			get 
			{
				LoadProviders();
				return _provider.VLocalidadProvider;
			}
		}
		
		#endregion
		
		#region VPersonaProvider
		
		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="VPersona"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static VPersonaProviderBase VPersonaProvider
		{
			get 
			{
				LoadProviders();
				return _provider.VPersonaProvider;
			}
		}
		
		#endregion
		
		#endregion
	}
	
	#region Query/Filters
		
	#region ViajeHotelFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ViajeHotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeHotelFilters : ViajeHotelFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeHotelFilters class.
		/// </summary>
		public ViajeHotelFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ViajeHotelFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ViajeHotelFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ViajeHotelFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ViajeHotelFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ViajeHotelFilters
	
	#region ViajeHotelQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ViajeHotelParameterBuilder"/> class
	/// that is used exclusively with a <see cref="ViajeHotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeHotelQuery : ViajeHotelParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeHotelQuery class.
		/// </summary>
		public ViajeHotelQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ViajeHotelQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ViajeHotelQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ViajeHotelQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ViajeHotelQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ViajeHotelQuery
		
	#region PaqueteServicioFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteServicioFilters : PaqueteServicioFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioFilters class.
		/// </summary>
		public PaqueteServicioFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteServicioFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteServicioFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteServicioFilters
	
	#region PaqueteServicioQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PaqueteServicioParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PaqueteServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteServicioQuery : PaqueteServicioParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioQuery class.
		/// </summary>
		public PaqueteServicioQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteServicioQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteServicioQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteServicioQuery
		
	#region PasajeroFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroFilters : PasajeroFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroFilters class.
		/// </summary>
		public PasajeroFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroFilters
	
	#region PasajeroQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PasajeroParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Pasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroQuery : PasajeroParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroQuery class.
		/// </summary>
		public PasajeroQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroQuery
		
	#region PasajeroMenorFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroMenor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroMenorFilters : PasajeroMenorFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorFilters class.
		/// </summary>
		public PasajeroMenorFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroMenorFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroMenorFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroMenorFilters
	
	#region PasajeroMenorQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PasajeroMenorParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PasajeroMenor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroMenorQuery : PasajeroMenorParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorQuery class.
		/// </summary>
		public PasajeroMenorQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroMenorQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroMenorQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroMenorQuery
		
	#region PersonaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Persona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaFilters : PersonaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaFilters class.
		/// </summary>
		public PersonaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaFilters
	
	#region PersonaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PersonaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Persona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaQuery : PersonaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaQuery class.
		/// </summary>
		public PersonaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaQuery
		
	#region PlanillaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Planilla"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaFilters : PlanillaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaFilters class.
		/// </summary>
		public PlanillaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaFilters
	
	#region PlanillaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PlanillaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Planilla"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaQuery : PlanillaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaQuery class.
		/// </summary>
		public PlanillaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaQuery
		
	#region PlanillaHabitacionItemFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaHabitacionItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaHabitacionItemFilters : PlanillaHabitacionItemFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaHabitacionItemFilters class.
		/// </summary>
		public PlanillaHabitacionItemFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaHabitacionItemFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaHabitacionItemFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaHabitacionItemFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaHabitacionItemFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaHabitacionItemFilters
	
	#region PlanillaHabitacionItemQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PlanillaHabitacionItemParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PlanillaHabitacionItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaHabitacionItemQuery : PlanillaHabitacionItemParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaHabitacionItemQuery class.
		/// </summary>
		public PlanillaHabitacionItemQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaHabitacionItemQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaHabitacionItemQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaHabitacionItemQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaHabitacionItemQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaHabitacionItemQuery
		
	#region PlanillaServicioItemFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicioItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioItemFilters : PlanillaServicioItemFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemFilters class.
		/// </summary>
		public PlanillaServicioItemFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaServicioItemFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaServicioItemFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaServicioItemFilters
	
	#region PlanillaServicioItemQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PlanillaServicioItemParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PlanillaServicioItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioItemQuery : PlanillaServicioItemParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemQuery class.
		/// </summary>
		public PlanillaServicioItemQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaServicioItemQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaServicioItemQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaServicioItemQuery
		
	#region PaqueteExcursionFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteExcursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteExcursionFilters : PaqueteExcursionFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionFilters class.
		/// </summary>
		public PaqueteExcursionFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteExcursionFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteExcursionFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteExcursionFilters
	
	#region PaqueteExcursionQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PaqueteExcursionParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PaqueteExcursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteExcursionQuery : PaqueteExcursionParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionQuery class.
		/// </summary>
		public PaqueteExcursionQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteExcursionQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteExcursionQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteExcursionQuery
		
	#region PrecioFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Precio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioFilters : PrecioFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioFilters class.
		/// </summary>
		public PrecioFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioFilters
	
	#region PrecioQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PrecioParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Precio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioQuery : PrecioParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioQuery class.
		/// </summary>
		public PrecioQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioQuery
		
	#region PrecioServicioFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioServicioFilters : PrecioServicioFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioServicioFilters class.
		/// </summary>
		public PrecioServicioFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioServicioFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioServicioFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioServicioFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioServicioFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioServicioFilters
	
	#region PrecioServicioQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PrecioServicioParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PrecioServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioServicioQuery : PrecioServicioParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioServicioQuery class.
		/// </summary>
		public PrecioServicioQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioServicioQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioServicioQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioServicioQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioServicioQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioServicioQuery
		
	#region ProveedorFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Proveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProveedorFilters : ProveedorFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProveedorFilters class.
		/// </summary>
		public ProveedorFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ProveedorFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ProveedorFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ProveedorFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ProveedorFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ProveedorFilters
	
	#region ProveedorQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ProveedorParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Proveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProveedorQuery : ProveedorParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProveedorQuery class.
		/// </summary>
		public ProveedorQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ProveedorQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ProveedorQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ProveedorQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ProveedorQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ProveedorQuery
		
	#region ProvinciaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Provincia"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProvinciaFilters : ProvinciaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProvinciaFilters class.
		/// </summary>
		public ProvinciaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ProvinciaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ProvinciaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ProvinciaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ProvinciaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ProvinciaFilters
	
	#region ProvinciaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ProvinciaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Provincia"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProvinciaQuery : ProvinciaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProvinciaQuery class.
		/// </summary>
		public ProvinciaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ProvinciaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ProvinciaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ProvinciaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ProvinciaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ProvinciaQuery
		
	#region TransporteFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Transporte"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TransporteFilters : TransporteFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TransporteFilters class.
		/// </summary>
		public TransporteFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the TransporteFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public TransporteFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the TransporteFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public TransporteFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion TransporteFilters
	
	#region TransporteQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="TransporteParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Transporte"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TransporteQuery : TransporteParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TransporteQuery class.
		/// </summary>
		public TransporteQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the TransporteQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public TransporteQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the TransporteQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public TransporteQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion TransporteQuery
		
	#region ReservaHabitacionFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaHabitacionFilters : ReservaHabitacionFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionFilters class.
		/// </summary>
		public ReservaHabitacionFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ReservaHabitacionFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ReservaHabitacionFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ReservaHabitacionFilters
	
	#region ReservaHabitacionQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ReservaHabitacionParameterBuilder"/> class
	/// that is used exclusively with a <see cref="ReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaHabitacionQuery : ReservaHabitacionParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionQuery class.
		/// </summary>
		public ReservaHabitacionQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ReservaHabitacionQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ReservaHabitacionQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ReservaHabitacionQuery
		
	#region ServicioFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Servicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicioFilters : ServicioFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicioFilters class.
		/// </summary>
		public ServicioFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ServicioFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ServicioFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ServicioFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ServicioFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ServicioFilters
	
	#region ServicioQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ServicioParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Servicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicioQuery : ServicioParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicioQuery class.
		/// </summary>
		public ServicioQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ServicioQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ServicioQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ServicioQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ServicioQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ServicioQuery
		
	#region VendedorFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Vendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VendedorFilters : VendedorFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VendedorFilters class.
		/// </summary>
		public VendedorFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the VendedorFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VendedorFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VendedorFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VendedorFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VendedorFilters
	
	#region VendedorQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="VendedorParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Vendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VendedorQuery : VendedorParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VendedorQuery class.
		/// </summary>
		public VendedorQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the VendedorQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VendedorQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VendedorQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VendedorQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VendedorQuery
		
	#region ViajeFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Viaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeFilters : ViajeFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeFilters class.
		/// </summary>
		public ViajeFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ViajeFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ViajeFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ViajeFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ViajeFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ViajeFilters
	
	#region ViajeQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ViajeParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Viaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeQuery : ViajeParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeQuery class.
		/// </summary>
		public ViajeQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ViajeQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ViajeQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ViajeQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ViajeQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ViajeQuery
		
	#region PasajeFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeFilters : PasajeFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeFilters class.
		/// </summary>
		public PasajeFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeFilters
	
	#region PasajeQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PasajeParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Pasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeQuery : PasajeParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeQuery class.
		/// </summary>
		public PasajeQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeQuery
		
	#region PaquetePrecioFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaquetePrecio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaquetePrecioFilters : PaquetePrecioFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioFilters class.
		/// </summary>
		public PaquetePrecioFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaquetePrecioFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaquetePrecioFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaquetePrecioFilters
	
	#region PaquetePrecioQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PaquetePrecioParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PaquetePrecio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaquetePrecioQuery : PaquetePrecioParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioQuery class.
		/// </summary>
		public PaquetePrecioQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaquetePrecioQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaquetePrecioQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaquetePrecioQuery
		
	#region PrecioHabitacionFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioHabitacionFilters : PrecioHabitacionFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionFilters class.
		/// </summary>
		public PrecioHabitacionFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioHabitacionFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioHabitacionFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioHabitacionFilters
	
	#region PrecioHabitacionQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PrecioHabitacionParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PrecioHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioHabitacionQuery : PrecioHabitacionParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionQuery class.
		/// </summary>
		public PrecioHabitacionQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioHabitacionQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioHabitacionQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioHabitacionQuery
		
	#region VoucherFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Voucher"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VoucherFilters : VoucherFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VoucherFilters class.
		/// </summary>
		public VoucherFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the VoucherFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VoucherFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VoucherFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VoucherFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VoucherFilters
	
	#region VoucherQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="VoucherParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Voucher"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VoucherQuery : VoucherParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VoucherQuery class.
		/// </summary>
		public VoucherQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the VoucherQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VoucherQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VoucherQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VoucherQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VoucherQuery
		
	#region AdicionalFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Adicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AdicionalFilters : AdicionalFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AdicionalFilters class.
		/// </summary>
		public AdicionalFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the AdicionalFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AdicionalFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AdicionalFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AdicionalFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AdicionalFilters
	
	#region AdicionalQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="AdicionalParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Adicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AdicionalQuery : AdicionalParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AdicionalQuery class.
		/// </summary>
		public AdicionalQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the AdicionalQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AdicionalQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AdicionalQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AdicionalQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AdicionalQuery
		
	#region PaqueteAdicionalFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteAdicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteAdicionalFilters : PaqueteAdicionalFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteAdicionalFilters class.
		/// </summary>
		public PaqueteAdicionalFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteAdicionalFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteAdicionalFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteAdicionalFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteAdicionalFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteAdicionalFilters
	
	#region PaqueteAdicionalQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PaqueteAdicionalParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PaqueteAdicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteAdicionalQuery : PaqueteAdicionalParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteAdicionalQuery class.
		/// </summary>
		public PaqueteAdicionalQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteAdicionalQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteAdicionalQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteAdicionalQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteAdicionalQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteAdicionalQuery
		
	#region AuditFacturaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AuditFactura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AuditFacturaFilters : AuditFacturaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AuditFacturaFilters class.
		/// </summary>
		public AuditFacturaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the AuditFacturaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AuditFacturaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AuditFacturaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AuditFacturaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AuditFacturaFilters
	
	#region AuditFacturaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="AuditFacturaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="AuditFactura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AuditFacturaQuery : AuditFacturaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AuditFacturaQuery class.
		/// </summary>
		public AuditFacturaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the AuditFacturaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AuditFacturaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AuditFacturaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AuditFacturaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AuditFacturaQuery
		
	#region ButacaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Butaca"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ButacaFilters : ButacaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ButacaFilters class.
		/// </summary>
		public ButacaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ButacaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ButacaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ButacaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ButacaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ButacaFilters
	
	#region ButacaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ButacaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Butaca"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ButacaQuery : ButacaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ButacaQuery class.
		/// </summary>
		public ButacaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ButacaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ButacaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ButacaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ButacaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ButacaQuery
		
	#region CiudadFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Ciudad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CiudadFilters : CiudadFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CiudadFilters class.
		/// </summary>
		public CiudadFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the CiudadFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CiudadFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CiudadFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CiudadFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CiudadFilters
	
	#region CiudadQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="CiudadParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Ciudad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CiudadQuery : CiudadParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CiudadQuery class.
		/// </summary>
		public CiudadQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the CiudadQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CiudadQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CiudadQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CiudadQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CiudadQuery
		
	#region ClienteFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ClienteFilters : ClienteFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ClienteFilters class.
		/// </summary>
		public ClienteFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ClienteFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ClienteFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ClienteFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ClienteFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ClienteFilters
	
	#region ClienteQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ClienteParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Cliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ClienteQuery : ClienteParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ClienteQuery class.
		/// </summary>
		public ClienteQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ClienteQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ClienteQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ClienteQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ClienteQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ClienteQuery
		
	#region CuentaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaFilters : CuentaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaFilters class.
		/// </summary>
		public CuentaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the CuentaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CuentaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CuentaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CuentaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CuentaFilters
	
	#region CuentaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="CuentaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Cuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaQuery : CuentaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaQuery class.
		/// </summary>
		public CuentaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the CuentaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CuentaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CuentaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CuentaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CuentaQuery
		
	#region CuentaCorrienteFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CuentaCorriente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaCorrienteFilters : CuentaCorrienteFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteFilters class.
		/// </summary>
		public CuentaCorrienteFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CuentaCorrienteFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CuentaCorrienteFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CuentaCorrienteFilters
	
	#region CuentaCorrienteQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="CuentaCorrienteParameterBuilder"/> class
	/// that is used exclusively with a <see cref="CuentaCorriente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaCorrienteQuery : CuentaCorrienteParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteQuery class.
		/// </summary>
		public CuentaCorrienteQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CuentaCorrienteQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CuentaCorrienteQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CuentaCorrienteQuery
		
	#region DebitoFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Debito"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DebitoFilters : DebitoFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DebitoFilters class.
		/// </summary>
		public DebitoFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the DebitoFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DebitoFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DebitoFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DebitoFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DebitoFilters
	
	#region DebitoQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="DebitoParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Debito"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DebitoQuery : DebitoParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DebitoQuery class.
		/// </summary>
		public DebitoQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the DebitoQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DebitoQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DebitoQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DebitoQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DebitoQuery
		
	#region DepartamentoFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Departamento"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DepartamentoFilters : DepartamentoFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DepartamentoFilters class.
		/// </summary>
		public DepartamentoFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the DepartamentoFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DepartamentoFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DepartamentoFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DepartamentoFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DepartamentoFilters
	
	#region DepartamentoQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="DepartamentoParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Departamento"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DepartamentoQuery : DepartamentoParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DepartamentoQuery class.
		/// </summary>
		public DepartamentoQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the DepartamentoQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DepartamentoQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DepartamentoQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DepartamentoQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DepartamentoQuery
		
	#region EstadoPasajeFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="EstadoPasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class EstadoPasajeFilters : EstadoPasajeFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the EstadoPasajeFilters class.
		/// </summary>
		public EstadoPasajeFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the EstadoPasajeFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public EstadoPasajeFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the EstadoPasajeFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public EstadoPasajeFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion EstadoPasajeFilters
	
	#region EstadoPasajeQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="EstadoPasajeParameterBuilder"/> class
	/// that is used exclusively with a <see cref="EstadoPasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class EstadoPasajeQuery : EstadoPasajeParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the EstadoPasajeQuery class.
		/// </summary>
		public EstadoPasajeQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the EstadoPasajeQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public EstadoPasajeQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the EstadoPasajeQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public EstadoPasajeQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion EstadoPasajeQuery
		
	#region PaqueteFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Paquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteFilters : PaqueteFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteFilters class.
		/// </summary>
		public PaqueteFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteFilters
	
	#region PaqueteQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PaqueteParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Paquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteQuery : PaqueteParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteQuery class.
		/// </summary>
		public PaqueteQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteQuery
		
	#region ExcursionFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Excursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ExcursionFilters : ExcursionFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ExcursionFilters class.
		/// </summary>
		public ExcursionFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ExcursionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ExcursionFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ExcursionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ExcursionFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ExcursionFilters
	
	#region ExcursionQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ExcursionParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Excursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ExcursionQuery : ExcursionParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ExcursionQuery class.
		/// </summary>
		public ExcursionQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ExcursionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ExcursionQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ExcursionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ExcursionQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ExcursionQuery
		
	#region HabitacionFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Habitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionFilters : HabitacionFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionFilters class.
		/// </summary>
		public HabitacionFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the HabitacionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HabitacionFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HabitacionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HabitacionFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HabitacionFilters
	
	#region HabitacionQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="HabitacionParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Habitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionQuery : HabitacionParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionQuery class.
		/// </summary>
		public HabitacionQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the HabitacionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HabitacionQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HabitacionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HabitacionQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HabitacionQuery
		
	#region HabitacionTipoFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="HabitacionTipo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionTipoFilters : HabitacionTipoFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionTipoFilters class.
		/// </summary>
		public HabitacionTipoFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the HabitacionTipoFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HabitacionTipoFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HabitacionTipoFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HabitacionTipoFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HabitacionTipoFilters
	
	#region HabitacionTipoQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="HabitacionTipoParameterBuilder"/> class
	/// that is used exclusively with a <see cref="HabitacionTipo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionTipoQuery : HabitacionTipoParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionTipoQuery class.
		/// </summary>
		public HabitacionTipoQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the HabitacionTipoQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HabitacionTipoQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HabitacionTipoQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HabitacionTipoQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HabitacionTipoQuery
		
	#region HistorialFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Historial"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HistorialFilters : HistorialFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HistorialFilters class.
		/// </summary>
		public HistorialFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the HistorialFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HistorialFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HistorialFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HistorialFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HistorialFilters
	
	#region HistorialQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="HistorialParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Historial"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HistorialQuery : HistorialParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HistorialQuery class.
		/// </summary>
		public HistorialQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the HistorialQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HistorialQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HistorialQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HistorialQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HistorialQuery
		
	#region LocalidadFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Localidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class LocalidadFilters : LocalidadFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LocalidadFilters class.
		/// </summary>
		public LocalidadFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the LocalidadFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public LocalidadFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the LocalidadFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public LocalidadFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion LocalidadFilters
	
	#region LocalidadQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="LocalidadParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Localidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class LocalidadQuery : LocalidadParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LocalidadQuery class.
		/// </summary>
		public LocalidadQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the LocalidadQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public LocalidadQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the LocalidadQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public LocalidadQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion LocalidadQuery
		
	#region HotelFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Hotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HotelFilters : HotelFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HotelFilters class.
		/// </summary>
		public HotelFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the HotelFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HotelFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HotelFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HotelFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HotelFilters
	
	#region HotelQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="HotelParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Hotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HotelQuery : HotelParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HotelQuery class.
		/// </summary>
		public HotelQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the HotelQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HotelQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HotelQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HotelQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HotelQuery
		
	#region PasajeAdicionalFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeAdicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeAdicionalFilters : PasajeAdicionalFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalFilters class.
		/// </summary>
		public PasajeAdicionalFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeAdicionalFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeAdicionalFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeAdicionalFilters
	
	#region PasajeAdicionalQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PasajeAdicionalParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PasajeAdicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeAdicionalQuery : PasajeAdicionalParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalQuery class.
		/// </summary>
		public PasajeAdicionalQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeAdicionalQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeAdicionalQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeAdicionalQuery
		
	#region NotaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Nota"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NotaFilters : NotaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NotaFilters class.
		/// </summary>
		public NotaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the NotaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public NotaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the NotaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public NotaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion NotaFilters
	
	#region NotaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="NotaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Nota"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NotaQuery : NotaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NotaQuery class.
		/// </summary>
		public NotaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the NotaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public NotaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the NotaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public NotaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion NotaQuery
		
	#region PagoFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pago"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PagoFilters : PagoFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PagoFilters class.
		/// </summary>
		public PagoFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PagoFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PagoFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PagoFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PagoFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PagoFilters
	
	#region PagoQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PagoParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Pago"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PagoQuery : PagoParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PagoQuery class.
		/// </summary>
		public PagoQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PagoQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PagoQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PagoQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PagoQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PagoQuery
		
	#region FacturaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Factura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FacturaFilters : FacturaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FacturaFilters class.
		/// </summary>
		public FacturaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the FacturaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public FacturaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the FacturaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public FacturaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion FacturaFilters
	
	#region FacturaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="FacturaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Factura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FacturaQuery : FacturaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FacturaQuery class.
		/// </summary>
		public FacturaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the FacturaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public FacturaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the FacturaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public FacturaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion FacturaQuery
		
	#region MovimientoCuentaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MovimientoCuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MovimientoCuentaFilters : MovimientoCuentaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaFilters class.
		/// </summary>
		public MovimientoCuentaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MovimientoCuentaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MovimientoCuentaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MovimientoCuentaFilters
	
	#region MovimientoCuentaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="MovimientoCuentaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="MovimientoCuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MovimientoCuentaQuery : MovimientoCuentaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaQuery class.
		/// </summary>
		public MovimientoCuentaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MovimientoCuentaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MovimientoCuentaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MovimientoCuentaQuery
		
	#region PaisFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pais"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaisFilters : PaisFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaisFilters class.
		/// </summary>
		public PaisFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaisFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaisFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaisFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaisFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaisFilters
	
	#region PaisQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PaisParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Pais"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaisQuery : PaisParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaisQuery class.
		/// </summary>
		public PaisQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaisQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaisQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaisQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaisQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaisQuery
		
	#region PasajeroViajeFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroViaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroViajeFilters : PasajeroViajeFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeFilters class.
		/// </summary>
		public PasajeroViajeFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroViajeFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroViajeFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroViajeFilters
	
	#region PasajeroViajeQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PasajeroViajeParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PasajeroViaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroViajeQuery : PasajeroViajeParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeQuery class.
		/// </summary>
		public PasajeroViajeQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroViajeQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroViajeQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroViajeQuery
		
	#region PersonaClienteFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaClienteFilters : PersonaClienteFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaClienteFilters class.
		/// </summary>
		public PersonaClienteFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaClienteFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaClienteFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaClienteFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaClienteFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaClienteFilters
	
	#region PersonaClienteQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PersonaClienteParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PersonaCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaClienteQuery : PersonaClienteParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaClienteQuery class.
		/// </summary>
		public PersonaClienteQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaClienteQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaClienteQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaClienteQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaClienteQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaClienteQuery
		
	#region PersonaPasajeroFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaPasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaPasajeroFilters : PersonaPasajeroFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroFilters class.
		/// </summary>
		public PersonaPasajeroFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaPasajeroFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaPasajeroFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaPasajeroFilters
	
	#region PersonaPasajeroQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PersonaPasajeroParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PersonaPasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaPasajeroQuery : PersonaPasajeroParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroQuery class.
		/// </summary>
		public PersonaPasajeroQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaPasajeroQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaPasajeroQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaPasajeroQuery
		
	#region PersonaProveedorFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaProveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaProveedorFilters : PersonaProveedorFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorFilters class.
		/// </summary>
		public PersonaProveedorFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaProveedorFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaProveedorFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaProveedorFilters
	
	#region PersonaProveedorQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PersonaProveedorParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PersonaProveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaProveedorQuery : PersonaProveedorParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorQuery class.
		/// </summary>
		public PersonaProveedorQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaProveedorQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaProveedorQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaProveedorQuery
		
	#region PersonaVendedorFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaVendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaVendedorFilters : PersonaVendedorFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorFilters class.
		/// </summary>
		public PersonaVendedorFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaVendedorFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaVendedorFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaVendedorFilters
	
	#region PersonaVendedorQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PersonaVendedorParameterBuilder"/> class
	/// that is used exclusively with a <see cref="PersonaVendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaVendedorQuery : PersonaVendedorParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorQuery class.
		/// </summary>
		public PersonaVendedorQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaVendedorQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaVendedorQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaVendedorQuery
		
	#region ReservaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Reserva"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaFilters : ReservaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaFilters class.
		/// </summary>
		public ReservaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ReservaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ReservaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ReservaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ReservaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ReservaFilters
	
	#region ReservaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ReservaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Reserva"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaQuery : ReservaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaQuery class.
		/// </summary>
		public ReservaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ReservaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ReservaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ReservaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ReservaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ReservaQuery
		
	#region VConsultaReservaHabitacionFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VConsultaReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VConsultaReservaHabitacionFilters : VConsultaReservaHabitacionFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionFilters class.
		/// </summary>
		public VConsultaReservaHabitacionFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VConsultaReservaHabitacionFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VConsultaReservaHabitacionFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VConsultaReservaHabitacionFilters
	
	#region VConsultaReservaHabitacionQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="VConsultaReservaHabitacionParameterBuilder"/> class
	/// that is used exclusively with a <see cref="VConsultaReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VConsultaReservaHabitacionQuery : VConsultaReservaHabitacionParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionQuery class.
		/// </summary>
		public VConsultaReservaHabitacionQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VConsultaReservaHabitacionQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VConsultaReservaHabitacionQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VConsultaReservaHabitacionQuery
		
	#region VLocalidadFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VLocalidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VLocalidadFilters : VLocalidadFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VLocalidadFilters class.
		/// </summary>
		public VLocalidadFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the VLocalidadFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VLocalidadFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VLocalidadFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VLocalidadFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VLocalidadFilters
	
	#region VLocalidadQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="VLocalidadParameterBuilder"/> class
	/// that is used exclusively with a <see cref="VLocalidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VLocalidadQuery : VLocalidadParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VLocalidadQuery class.
		/// </summary>
		public VLocalidadQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the VLocalidadQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VLocalidadQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VLocalidadQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VLocalidadQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VLocalidadQuery
		
	#region VPersonaFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VPersona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VPersonaFilters : VPersonaFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VPersonaFilters class.
		/// </summary>
		public VPersonaFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the VPersonaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VPersonaFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VPersonaFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VPersonaFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VPersonaFilters
	
	#region VPersonaQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="VPersonaParameterBuilder"/> class
	/// that is used exclusively with a <see cref="VPersona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VPersonaQuery : VPersonaParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VPersonaQuery class.
		/// </summary>
		public VPersonaQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the VPersonaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VPersonaQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VPersonaQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VPersonaQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VPersonaQuery
	#endregion

	
}
