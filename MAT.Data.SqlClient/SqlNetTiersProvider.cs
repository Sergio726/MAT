#region Using directives

using System;
using System.Collections;
using System.Collections.Specialized;


using System.Web.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using MAT.Entities;
using MAT.Data;
using MAT.Data.Bases;

#endregion

namespace MAT.Data.SqlClient
{
	/// <summary>
	/// This class is the Sql implementation of the NetTiersProvider.
	/// </summary>
	public sealed class SqlNetTiersProvider : MAT.Data.Bases.NetTiersProvider
	{
		private static object syncRoot = new Object();
		private string _applicationName;
        private string _connectionString;
        private bool _useStoredProcedure;
        string _providerInvariantName;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlNetTiersProvider"/> class.
		///</summary>
		public SqlNetTiersProvider()
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


            #region "Initialize UseStoredProcedure"
            string storedProcedure  = config["useStoredProcedure"];
           	if (string.IsNullOrEmpty(storedProcedure))
            {
                throw new ProviderException("Empty or missing useStoredProcedure");
            }
            this._useStoredProcedure = Convert.ToBoolean(config["useStoredProcedure"]);
            config.Remove("useStoredProcedure");
            #endregion

			#region ConnectionString

			// Initialize _connectionString
			_connectionString = config["connectionString"];
			config.Remove("connectionString");

			string connect = config["connectionStringName"];
			config.Remove("connectionStringName");

			if ( String.IsNullOrEmpty(_connectionString) )
			{
				if ( String.IsNullOrEmpty(connect) )
				{
					throw new ProviderException("Empty or missing connectionStringName");
				}

				if ( DataRepository.ConnectionStrings[connect] == null )
				{
					throw new ProviderException("Missing connection string");
				}

				_connectionString = DataRepository.ConnectionStrings[connect].ConnectionString;
			}

            if ( String.IsNullOrEmpty(_connectionString) )
            {
                throw new ProviderException("Empty connection string");
			}

			#endregion
            
             #region "_providerInvariantName"

            // initialize _providerInvariantName
            this._providerInvariantName = config["providerInvariantName"];

            if (String.IsNullOrEmpty(_providerInvariantName))
            {
                throw new ProviderException("Empty or missing providerInvariantName");
            }
            config.Remove("providerInvariantName");

            #endregion

        }
		
		/// <summary>
		/// Creates a new <see cref="TransactionManager"/> instance from the current datasource.
		/// </summary>
		/// <returns></returns>
		public override TransactionManager CreateTransaction()
		{
			return new TransactionManager(this._connectionString);
		}
		
		/// <summary>
		/// Gets a value indicating whether to use stored procedure or not.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this repository use stored procedures; otherwise, <c>false</c>.
		/// </value>
		public bool UseStoredProcedure
		{
			get {return this._useStoredProcedure;}
			set {this._useStoredProcedure = value;}
		}
		
		 /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
		public string ConnectionString
		{
			get {return this._connectionString;}
			set {this._connectionString = value;}
		}
		
		/// <summary>
	    /// Gets or sets the invariant provider name listed in the DbProviderFactories machine.config section.
	    /// </summary>
	    /// <value>The name of the provider invariant.</value>
	    public string ProviderInvariantName
	    {
	        get { return this._providerInvariantName; }
	        set { this._providerInvariantName = value; }
	    }		
		
		///<summary>
		/// Indicates if the current <see cref="NetTiersProvider"/> implementation supports Transacton.
		///</summary>
		public override bool IsTransactionSupported
		{
			get
			{
				return true;
			}
		}

		
		#region "ViajeHotelProvider"
			
		private SqlViajeHotelProvider innerSqlViajeHotelProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="ViajeHotel"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ViajeHotelProviderBase ViajeHotelProvider
		{
			get
			{
				if (innerSqlViajeHotelProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlViajeHotelProvider == null)
						{
							this.innerSqlViajeHotelProvider = new SqlViajeHotelProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlViajeHotelProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlViajeHotelProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlViajeHotelProvider SqlViajeHotelProvider
		{
			get {return ViajeHotelProvider as SqlViajeHotelProvider;}
		}
		
		#endregion
		
		
		#region "PaqueteServicioProvider"
			
		private SqlPaqueteServicioProvider innerSqlPaqueteServicioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PaqueteServicio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaqueteServicioProviderBase PaqueteServicioProvider
		{
			get
			{
				if (innerSqlPaqueteServicioProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPaqueteServicioProvider == null)
						{
							this.innerSqlPaqueteServicioProvider = new SqlPaqueteServicioProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPaqueteServicioProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPaqueteServicioProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPaqueteServicioProvider SqlPaqueteServicioProvider
		{
			get {return PaqueteServicioProvider as SqlPaqueteServicioProvider;}
		}
		
		#endregion
		
		
		#region "PasajeroProvider"
			
		private SqlPasajeroProvider innerSqlPasajeroProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Pasajero"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PasajeroProviderBase PasajeroProvider
		{
			get
			{
				if (innerSqlPasajeroProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPasajeroProvider == null)
						{
							this.innerSqlPasajeroProvider = new SqlPasajeroProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPasajeroProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPasajeroProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPasajeroProvider SqlPasajeroProvider
		{
			get {return PasajeroProvider as SqlPasajeroProvider;}
		}
		
		#endregion
		
		
		#region "PasajeroMenorProvider"
			
		private SqlPasajeroMenorProvider innerSqlPasajeroMenorProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PasajeroMenor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PasajeroMenorProviderBase PasajeroMenorProvider
		{
			get
			{
				if (innerSqlPasajeroMenorProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPasajeroMenorProvider == null)
						{
							this.innerSqlPasajeroMenorProvider = new SqlPasajeroMenorProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPasajeroMenorProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPasajeroMenorProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPasajeroMenorProvider SqlPasajeroMenorProvider
		{
			get {return PasajeroMenorProvider as SqlPasajeroMenorProvider;}
		}
		
		#endregion
		
		
		#region "PersonaProvider"
			
		private SqlPersonaProvider innerSqlPersonaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Persona"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PersonaProviderBase PersonaProvider
		{
			get
			{
				if (innerSqlPersonaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPersonaProvider == null)
						{
							this.innerSqlPersonaProvider = new SqlPersonaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPersonaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPersonaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPersonaProvider SqlPersonaProvider
		{
			get {return PersonaProvider as SqlPersonaProvider;}
		}
		
		#endregion
		
		
		#region "PlanillaProvider"
			
		private SqlPlanillaProvider innerSqlPlanillaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Planilla"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PlanillaProviderBase PlanillaProvider
		{
			get
			{
				if (innerSqlPlanillaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPlanillaProvider == null)
						{
							this.innerSqlPlanillaProvider = new SqlPlanillaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPlanillaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPlanillaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPlanillaProvider SqlPlanillaProvider
		{
			get {return PlanillaProvider as SqlPlanillaProvider;}
		}
		
		#endregion
		
		
		#region "PlanillaHabitacionItemProvider"
			
		private SqlPlanillaHabitacionItemProvider innerSqlPlanillaHabitacionItemProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PlanillaHabitacionItem"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PlanillaHabitacionItemProviderBase PlanillaHabitacionItemProvider
		{
			get
			{
				if (innerSqlPlanillaHabitacionItemProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPlanillaHabitacionItemProvider == null)
						{
							this.innerSqlPlanillaHabitacionItemProvider = new SqlPlanillaHabitacionItemProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPlanillaHabitacionItemProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPlanillaHabitacionItemProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPlanillaHabitacionItemProvider SqlPlanillaHabitacionItemProvider
		{
			get {return PlanillaHabitacionItemProvider as SqlPlanillaHabitacionItemProvider;}
		}
		
		#endregion
		
		
		#region "PlanillaServicioItemProvider"
			
		private SqlPlanillaServicioItemProvider innerSqlPlanillaServicioItemProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PlanillaServicioItem"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PlanillaServicioItemProviderBase PlanillaServicioItemProvider
		{
			get
			{
				if (innerSqlPlanillaServicioItemProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPlanillaServicioItemProvider == null)
						{
							this.innerSqlPlanillaServicioItemProvider = new SqlPlanillaServicioItemProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPlanillaServicioItemProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPlanillaServicioItemProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPlanillaServicioItemProvider SqlPlanillaServicioItemProvider
		{
			get {return PlanillaServicioItemProvider as SqlPlanillaServicioItemProvider;}
		}
		
		#endregion
		
		
		#region "PaqueteExcursionProvider"
			
		private SqlPaqueteExcursionProvider innerSqlPaqueteExcursionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PaqueteExcursion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaqueteExcursionProviderBase PaqueteExcursionProvider
		{
			get
			{
				if (innerSqlPaqueteExcursionProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPaqueteExcursionProvider == null)
						{
							this.innerSqlPaqueteExcursionProvider = new SqlPaqueteExcursionProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPaqueteExcursionProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPaqueteExcursionProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPaqueteExcursionProvider SqlPaqueteExcursionProvider
		{
			get {return PaqueteExcursionProvider as SqlPaqueteExcursionProvider;}
		}
		
		#endregion
		
		
		#region "PrecioProvider"
			
		private SqlPrecioProvider innerSqlPrecioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Precio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PrecioProviderBase PrecioProvider
		{
			get
			{
				if (innerSqlPrecioProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPrecioProvider == null)
						{
							this.innerSqlPrecioProvider = new SqlPrecioProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPrecioProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPrecioProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPrecioProvider SqlPrecioProvider
		{
			get {return PrecioProvider as SqlPrecioProvider;}
		}
		
		#endregion
		
		
		#region "PrecioServicioProvider"
			
		private SqlPrecioServicioProvider innerSqlPrecioServicioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PrecioServicio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PrecioServicioProviderBase PrecioServicioProvider
		{
			get
			{
				if (innerSqlPrecioServicioProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPrecioServicioProvider == null)
						{
							this.innerSqlPrecioServicioProvider = new SqlPrecioServicioProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPrecioServicioProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPrecioServicioProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPrecioServicioProvider SqlPrecioServicioProvider
		{
			get {return PrecioServicioProvider as SqlPrecioServicioProvider;}
		}
		
		#endregion
		
		
		#region "ProveedorProvider"
			
		private SqlProveedorProvider innerSqlProveedorProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Proveedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ProveedorProviderBase ProveedorProvider
		{
			get
			{
				if (innerSqlProveedorProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlProveedorProvider == null)
						{
							this.innerSqlProveedorProvider = new SqlProveedorProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlProveedorProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlProveedorProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlProveedorProvider SqlProveedorProvider
		{
			get {return ProveedorProvider as SqlProveedorProvider;}
		}
		
		#endregion
		
		
		#region "ProvinciaProvider"
			
		private SqlProvinciaProvider innerSqlProvinciaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Provincia"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ProvinciaProviderBase ProvinciaProvider
		{
			get
			{
				if (innerSqlProvinciaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlProvinciaProvider == null)
						{
							this.innerSqlProvinciaProvider = new SqlProvinciaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlProvinciaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlProvinciaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlProvinciaProvider SqlProvinciaProvider
		{
			get {return ProvinciaProvider as SqlProvinciaProvider;}
		}
		
		#endregion
		
		
		#region "TransporteProvider"
			
		private SqlTransporteProvider innerSqlTransporteProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Transporte"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override TransporteProviderBase TransporteProvider
		{
			get
			{
				if (innerSqlTransporteProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlTransporteProvider == null)
						{
							this.innerSqlTransporteProvider = new SqlTransporteProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlTransporteProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlTransporteProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlTransporteProvider SqlTransporteProvider
		{
			get {return TransporteProvider as SqlTransporteProvider;}
		}
		
		#endregion
		
		
		#region "ReservaHabitacionProvider"
			
		private SqlReservaHabitacionProvider innerSqlReservaHabitacionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="ReservaHabitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ReservaHabitacionProviderBase ReservaHabitacionProvider
		{
			get
			{
				if (innerSqlReservaHabitacionProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlReservaHabitacionProvider == null)
						{
							this.innerSqlReservaHabitacionProvider = new SqlReservaHabitacionProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlReservaHabitacionProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlReservaHabitacionProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlReservaHabitacionProvider SqlReservaHabitacionProvider
		{
			get {return ReservaHabitacionProvider as SqlReservaHabitacionProvider;}
		}
		
		#endregion
		
		
		#region "ServicioProvider"
			
		private SqlServicioProvider innerSqlServicioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Servicio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ServicioProviderBase ServicioProvider
		{
			get
			{
				if (innerSqlServicioProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlServicioProvider == null)
						{
							this.innerSqlServicioProvider = new SqlServicioProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlServicioProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlServicioProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlServicioProvider SqlServicioProvider
		{
			get {return ServicioProvider as SqlServicioProvider;}
		}
		
		#endregion
		
		
		#region "VendedorProvider"
			
		private SqlVendedorProvider innerSqlVendedorProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Vendedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override VendedorProviderBase VendedorProvider
		{
			get
			{
				if (innerSqlVendedorProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlVendedorProvider == null)
						{
							this.innerSqlVendedorProvider = new SqlVendedorProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlVendedorProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlVendedorProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlVendedorProvider SqlVendedorProvider
		{
			get {return VendedorProvider as SqlVendedorProvider;}
		}
		
		#endregion
		
		
		#region "ViajeProvider"
			
		private SqlViajeProvider innerSqlViajeProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Viaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ViajeProviderBase ViajeProvider
		{
			get
			{
				if (innerSqlViajeProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlViajeProvider == null)
						{
							this.innerSqlViajeProvider = new SqlViajeProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlViajeProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlViajeProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlViajeProvider SqlViajeProvider
		{
			get {return ViajeProvider as SqlViajeProvider;}
		}
		
		#endregion
		
		
		#region "PasajeProvider"
			
		private SqlPasajeProvider innerSqlPasajeProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Pasaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PasajeProviderBase PasajeProvider
		{
			get
			{
				if (innerSqlPasajeProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPasajeProvider == null)
						{
							this.innerSqlPasajeProvider = new SqlPasajeProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPasajeProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPasajeProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPasajeProvider SqlPasajeProvider
		{
			get {return PasajeProvider as SqlPasajeProvider;}
		}
		
		#endregion
		
		
		#region "PaquetePrecioProvider"
			
		private SqlPaquetePrecioProvider innerSqlPaquetePrecioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PaquetePrecio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaquetePrecioProviderBase PaquetePrecioProvider
		{
			get
			{
				if (innerSqlPaquetePrecioProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPaquetePrecioProvider == null)
						{
							this.innerSqlPaquetePrecioProvider = new SqlPaquetePrecioProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPaquetePrecioProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPaquetePrecioProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPaquetePrecioProvider SqlPaquetePrecioProvider
		{
			get {return PaquetePrecioProvider as SqlPaquetePrecioProvider;}
		}
		
		#endregion
		
		
		#region "PrecioHabitacionProvider"
			
		private SqlPrecioHabitacionProvider innerSqlPrecioHabitacionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PrecioHabitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PrecioHabitacionProviderBase PrecioHabitacionProvider
		{
			get
			{
				if (innerSqlPrecioHabitacionProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPrecioHabitacionProvider == null)
						{
							this.innerSqlPrecioHabitacionProvider = new SqlPrecioHabitacionProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPrecioHabitacionProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPrecioHabitacionProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPrecioHabitacionProvider SqlPrecioHabitacionProvider
		{
			get {return PrecioHabitacionProvider as SqlPrecioHabitacionProvider;}
		}
		
		#endregion
		
		
		#region "VoucherProvider"
			
		private SqlVoucherProvider innerSqlVoucherProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Voucher"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override VoucherProviderBase VoucherProvider
		{
			get
			{
				if (innerSqlVoucherProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlVoucherProvider == null)
						{
							this.innerSqlVoucherProvider = new SqlVoucherProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlVoucherProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlVoucherProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlVoucherProvider SqlVoucherProvider
		{
			get {return VoucherProvider as SqlVoucherProvider;}
		}
		
		#endregion
		
		
		#region "AdicionalProvider"
			
		private SqlAdicionalProvider innerSqlAdicionalProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Adicional"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override AdicionalProviderBase AdicionalProvider
		{
			get
			{
				if (innerSqlAdicionalProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlAdicionalProvider == null)
						{
							this.innerSqlAdicionalProvider = new SqlAdicionalProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlAdicionalProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlAdicionalProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlAdicionalProvider SqlAdicionalProvider
		{
			get {return AdicionalProvider as SqlAdicionalProvider;}
		}
		
		#endregion
		
		
		#region "PaqueteAdicionalProvider"
			
		private SqlPaqueteAdicionalProvider innerSqlPaqueteAdicionalProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PaqueteAdicional"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaqueteAdicionalProviderBase PaqueteAdicionalProvider
		{
			get
			{
				if (innerSqlPaqueteAdicionalProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPaqueteAdicionalProvider == null)
						{
							this.innerSqlPaqueteAdicionalProvider = new SqlPaqueteAdicionalProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPaqueteAdicionalProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPaqueteAdicionalProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPaqueteAdicionalProvider SqlPaqueteAdicionalProvider
		{
			get {return PaqueteAdicionalProvider as SqlPaqueteAdicionalProvider;}
		}
		
		#endregion
		
		
		#region "AuditFacturaProvider"
			
		private SqlAuditFacturaProvider innerSqlAuditFacturaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="AuditFactura"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override AuditFacturaProviderBase AuditFacturaProvider
		{
			get
			{
				if (innerSqlAuditFacturaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlAuditFacturaProvider == null)
						{
							this.innerSqlAuditFacturaProvider = new SqlAuditFacturaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlAuditFacturaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlAuditFacturaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlAuditFacturaProvider SqlAuditFacturaProvider
		{
			get {return AuditFacturaProvider as SqlAuditFacturaProvider;}
		}
		
		#endregion
		
		
		#region "ButacaProvider"
			
		private SqlButacaProvider innerSqlButacaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Butaca"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ButacaProviderBase ButacaProvider
		{
			get
			{
				if (innerSqlButacaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlButacaProvider == null)
						{
							this.innerSqlButacaProvider = new SqlButacaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlButacaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlButacaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlButacaProvider SqlButacaProvider
		{
			get {return ButacaProvider as SqlButacaProvider;}
		}
		
		#endregion
		
		
		#region "CiudadProvider"
			
		private SqlCiudadProvider innerSqlCiudadProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Ciudad"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override CiudadProviderBase CiudadProvider
		{
			get
			{
				if (innerSqlCiudadProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlCiudadProvider == null)
						{
							this.innerSqlCiudadProvider = new SqlCiudadProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlCiudadProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlCiudadProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlCiudadProvider SqlCiudadProvider
		{
			get {return CiudadProvider as SqlCiudadProvider;}
		}
		
		#endregion
		
		
		#region "ClienteProvider"
			
		private SqlClienteProvider innerSqlClienteProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Cliente"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ClienteProviderBase ClienteProvider
		{
			get
			{
				if (innerSqlClienteProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlClienteProvider == null)
						{
							this.innerSqlClienteProvider = new SqlClienteProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlClienteProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlClienteProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlClienteProvider SqlClienteProvider
		{
			get {return ClienteProvider as SqlClienteProvider;}
		}
		
		#endregion
		
		
		#region "CuentaProvider"
			
		private SqlCuentaProvider innerSqlCuentaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Cuenta"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override CuentaProviderBase CuentaProvider
		{
			get
			{
				if (innerSqlCuentaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlCuentaProvider == null)
						{
							this.innerSqlCuentaProvider = new SqlCuentaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlCuentaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlCuentaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlCuentaProvider SqlCuentaProvider
		{
			get {return CuentaProvider as SqlCuentaProvider;}
		}
		
		#endregion
		
		
		#region "CuentaCorrienteProvider"
			
        //private SqlCuentaCorrienteProvider innerSqlCuentaCorrienteProvider;

        /////<summary>
        ///// This class is the Data Access Logic Component for the <see cref="CuentaCorriente"/> business entity.
        ///// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        /////</summary>
        ///// <value></value>
        //public override CuentaCorrienteProviderBase CuentaCorrienteProvider
        //{
        //    get
        //    {
        //        if (innerSqlCuentaCorrienteProvider == null) 
        //        {
        //            lock (syncRoot) 
        //            {
        //                if (innerSqlCuentaCorrienteProvider == null)
        //                {
        //                    this.innerSqlCuentaCorrienteProvider = new SqlCuentaCorrienteProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
        //                }
        //            }
        //        }
        //        return innerSqlCuentaCorrienteProvider;
        //    }
        //}
		
        ///// <summary>
        ///// Gets the current <see cref="SqlCuentaCorrienteProvider"/>.
        ///// </summary>
        ///// <value></value>
        //public SqlCuentaCorrienteProvider SqlCuentaCorrienteProvider
        //{
        //    get {return CuentaCorrienteProvider as SqlCuentaCorrienteProvider;}
        //}
		
		#endregion
		
		
		#region "DebitoProvider"
			
		private SqlDebitoProvider innerSqlDebitoProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Debito"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override DebitoProviderBase DebitoProvider
		{
			get
			{
				if (innerSqlDebitoProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlDebitoProvider == null)
						{
							this.innerSqlDebitoProvider = new SqlDebitoProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlDebitoProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlDebitoProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlDebitoProvider SqlDebitoProvider
		{
			get {return DebitoProvider as SqlDebitoProvider;}
		}
		
		#endregion
		
		
		#region "DepartamentoProvider"
			
		private SqlDepartamentoProvider innerSqlDepartamentoProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Departamento"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override DepartamentoProviderBase DepartamentoProvider
		{
			get
			{
				if (innerSqlDepartamentoProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlDepartamentoProvider == null)
						{
							this.innerSqlDepartamentoProvider = new SqlDepartamentoProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlDepartamentoProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlDepartamentoProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlDepartamentoProvider SqlDepartamentoProvider
		{
			get {return DepartamentoProvider as SqlDepartamentoProvider;}
		}
		
		#endregion
		
		
		#region "EstadoPasajeProvider"
			
		private SqlEstadoPasajeProvider innerSqlEstadoPasajeProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="EstadoPasaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override EstadoPasajeProviderBase EstadoPasajeProvider
		{
			get
			{
				if (innerSqlEstadoPasajeProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlEstadoPasajeProvider == null)
						{
							this.innerSqlEstadoPasajeProvider = new SqlEstadoPasajeProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlEstadoPasajeProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlEstadoPasajeProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlEstadoPasajeProvider SqlEstadoPasajeProvider
		{
			get {return EstadoPasajeProvider as SqlEstadoPasajeProvider;}
		}
		
		#endregion
		
		
		#region "PaqueteProvider"
			
		private SqlPaqueteProvider innerSqlPaqueteProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Paquete"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaqueteProviderBase PaqueteProvider
		{
			get
			{
				if (innerSqlPaqueteProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPaqueteProvider == null)
						{
							this.innerSqlPaqueteProvider = new SqlPaqueteProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPaqueteProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPaqueteProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPaqueteProvider SqlPaqueteProvider
		{
			get {return PaqueteProvider as SqlPaqueteProvider;}
		}
		
		#endregion
		
		
		#region "ExcursionProvider"
			
		private SqlExcursionProvider innerSqlExcursionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Excursion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ExcursionProviderBase ExcursionProvider
		{
			get
			{
				if (innerSqlExcursionProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlExcursionProvider == null)
						{
							this.innerSqlExcursionProvider = new SqlExcursionProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlExcursionProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlExcursionProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlExcursionProvider SqlExcursionProvider
		{
			get {return ExcursionProvider as SqlExcursionProvider;}
		}
		
		#endregion
		
		
		#region "HabitacionProvider"
			
		private SqlHabitacionProvider innerSqlHabitacionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Habitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override HabitacionProviderBase HabitacionProvider
		{
			get
			{
				if (innerSqlHabitacionProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlHabitacionProvider == null)
						{
							this.innerSqlHabitacionProvider = new SqlHabitacionProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlHabitacionProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlHabitacionProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlHabitacionProvider SqlHabitacionProvider
		{
			get {return HabitacionProvider as SqlHabitacionProvider;}
		}
		
		#endregion
		
		
		#region "HabitacionTipoProvider"
			
		private SqlHabitacionTipoProvider innerSqlHabitacionTipoProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="HabitacionTipo"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override HabitacionTipoProviderBase HabitacionTipoProvider
		{
			get
			{
				if (innerSqlHabitacionTipoProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlHabitacionTipoProvider == null)
						{
							this.innerSqlHabitacionTipoProvider = new SqlHabitacionTipoProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlHabitacionTipoProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlHabitacionTipoProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlHabitacionTipoProvider SqlHabitacionTipoProvider
		{
			get {return HabitacionTipoProvider as SqlHabitacionTipoProvider;}
		}
		
		#endregion
		
		
		#region "HistorialProvider"
			
		private SqlHistorialProvider innerSqlHistorialProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Historial"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override HistorialProviderBase HistorialProvider
		{
			get
			{
				if (innerSqlHistorialProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlHistorialProvider == null)
						{
							this.innerSqlHistorialProvider = new SqlHistorialProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlHistorialProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlHistorialProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlHistorialProvider SqlHistorialProvider
		{
			get {return HistorialProvider as SqlHistorialProvider;}
		}
		
		#endregion
		
		
		#region "LocalidadProvider"
			
		private SqlLocalidadProvider innerSqlLocalidadProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Localidad"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override LocalidadProviderBase LocalidadProvider
		{
			get
			{
				if (innerSqlLocalidadProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlLocalidadProvider == null)
						{
							this.innerSqlLocalidadProvider = new SqlLocalidadProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlLocalidadProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlLocalidadProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlLocalidadProvider SqlLocalidadProvider
		{
			get {return LocalidadProvider as SqlLocalidadProvider;}
		}
		
		#endregion
		
		
		#region "HotelProvider"
			
		private SqlHotelProvider innerSqlHotelProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Hotel"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override HotelProviderBase HotelProvider
		{
			get
			{
				if (innerSqlHotelProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlHotelProvider == null)
						{
							this.innerSqlHotelProvider = new SqlHotelProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlHotelProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlHotelProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlHotelProvider SqlHotelProvider
		{
			get {return HotelProvider as SqlHotelProvider;}
		}
		
		#endregion
		
		
		#region "PasajeAdicionalProvider"
			
		private SqlPasajeAdicionalProvider innerSqlPasajeAdicionalProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PasajeAdicional"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PasajeAdicionalProviderBase PasajeAdicionalProvider
		{
			get
			{
				if (innerSqlPasajeAdicionalProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPasajeAdicionalProvider == null)
						{
							this.innerSqlPasajeAdicionalProvider = new SqlPasajeAdicionalProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPasajeAdicionalProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPasajeAdicionalProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPasajeAdicionalProvider SqlPasajeAdicionalProvider
		{
			get {return PasajeAdicionalProvider as SqlPasajeAdicionalProvider;}
		}
		
		#endregion
		
		
		#region "NotaProvider"
			
		private SqlNotaProvider innerSqlNotaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Nota"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override NotaProviderBase NotaProvider
		{
			get
			{
				if (innerSqlNotaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlNotaProvider == null)
						{
							this.innerSqlNotaProvider = new SqlNotaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlNotaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlNotaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlNotaProvider SqlNotaProvider
		{
			get {return NotaProvider as SqlNotaProvider;}
		}
		
		#endregion
		
		
		#region "PagoProvider"
			
		private SqlPagoProvider innerSqlPagoProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Pago"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PagoProviderBase PagoProvider
		{
			get
			{
				if (innerSqlPagoProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPagoProvider == null)
						{
							this.innerSqlPagoProvider = new SqlPagoProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPagoProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPagoProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPagoProvider SqlPagoProvider
		{
			get {return PagoProvider as SqlPagoProvider;}
		}
		
		#endregion
		
		
		#region "FacturaProvider"
			
		private SqlFacturaProvider innerSqlFacturaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Factura"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override FacturaProviderBase FacturaProvider
		{
			get
			{
				if (innerSqlFacturaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlFacturaProvider == null)
						{
							this.innerSqlFacturaProvider = new SqlFacturaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlFacturaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlFacturaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlFacturaProvider SqlFacturaProvider
		{
			get {return FacturaProvider as SqlFacturaProvider;}
		}
		
		#endregion
		
		
		#region "MovimientoCuentaProvider"
			
		private SqlMovimientoCuentaProvider innerSqlMovimientoCuentaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="MovimientoCuenta"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override MovimientoCuentaProviderBase MovimientoCuentaProvider
		{
			get
			{
				if (innerSqlMovimientoCuentaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlMovimientoCuentaProvider == null)
						{
							this.innerSqlMovimientoCuentaProvider = new SqlMovimientoCuentaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlMovimientoCuentaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlMovimientoCuentaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlMovimientoCuentaProvider SqlMovimientoCuentaProvider
		{
			get {return MovimientoCuentaProvider as SqlMovimientoCuentaProvider;}
		}
		
		#endregion
		
		
		#region "PaisProvider"
			
		private SqlPaisProvider innerSqlPaisProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Pais"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PaisProviderBase PaisProvider
		{
			get
			{
				if (innerSqlPaisProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPaisProvider == null)
						{
							this.innerSqlPaisProvider = new SqlPaisProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPaisProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPaisProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPaisProvider SqlPaisProvider
		{
			get {return PaisProvider as SqlPaisProvider;}
		}
		
		#endregion
		
		
		
		#region "PasajeroViajeProvider"
		
		private SqlPasajeroViajeProvider innerSqlPasajeroViajeProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PasajeroViaje"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PasajeroViajeProviderBase PasajeroViajeProvider
		{
			get
			{
				if (innerSqlPasajeroViajeProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPasajeroViajeProvider == null)
						{
							this.innerSqlPasajeroViajeProvider = new SqlPasajeroViajeProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPasajeroViajeProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPasajeroViajeProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPasajeroViajeProvider SqlPasajeroViajeProvider
		{
			get {return PasajeroViajeProvider as SqlPasajeroViajeProvider;}
		}
		
		#endregion
		
		
		#region "PersonaClienteProvider"
		
		private SqlPersonaClienteProvider innerSqlPersonaClienteProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PersonaCliente"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PersonaClienteProviderBase PersonaClienteProvider
		{
			get
			{
				if (innerSqlPersonaClienteProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPersonaClienteProvider == null)
						{
							this.innerSqlPersonaClienteProvider = new SqlPersonaClienteProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPersonaClienteProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPersonaClienteProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPersonaClienteProvider SqlPersonaClienteProvider
		{
			get {return PersonaClienteProvider as SqlPersonaClienteProvider;}
		}
		
		#endregion
		
		
		#region "PersonaPasajeroProvider"
		
		private SqlPersonaPasajeroProvider innerSqlPersonaPasajeroProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PersonaPasajero"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PersonaPasajeroProviderBase PersonaPasajeroProvider
		{
			get
			{
				if (innerSqlPersonaPasajeroProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPersonaPasajeroProvider == null)
						{
							this.innerSqlPersonaPasajeroProvider = new SqlPersonaPasajeroProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPersonaPasajeroProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPersonaPasajeroProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPersonaPasajeroProvider SqlPersonaPasajeroProvider
		{
			get {return PersonaPasajeroProvider as SqlPersonaPasajeroProvider;}
		}
		
		#endregion
		
		
		#region "PersonaProveedorProvider"
		
		private SqlPersonaProveedorProvider innerSqlPersonaProveedorProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PersonaProveedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PersonaProveedorProviderBase PersonaProveedorProvider
		{
			get
			{
				if (innerSqlPersonaProveedorProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPersonaProveedorProvider == null)
						{
							this.innerSqlPersonaProveedorProvider = new SqlPersonaProveedorProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPersonaProveedorProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPersonaProveedorProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPersonaProveedorProvider SqlPersonaProveedorProvider
		{
			get {return PersonaProveedorProvider as SqlPersonaProveedorProvider;}
		}
		
		#endregion
		
		
		#region "PersonaVendedorProvider"
		
		private SqlPersonaVendedorProvider innerSqlPersonaVendedorProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="PersonaVendedor"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PersonaVendedorProviderBase PersonaVendedorProvider
		{
			get
			{
				if (innerSqlPersonaVendedorProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPersonaVendedorProvider == null)
						{
							this.innerSqlPersonaVendedorProvider = new SqlPersonaVendedorProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPersonaVendedorProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlPersonaVendedorProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPersonaVendedorProvider SqlPersonaVendedorProvider
		{
			get {return PersonaVendedorProvider as SqlPersonaVendedorProvider;}
		}
		
		#endregion
		
		
		#region "ReservaProvider"
		
		private SqlReservaProvider innerSqlReservaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Reserva"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ReservaProviderBase ReservaProvider
		{
			get
			{
				if (innerSqlReservaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlReservaProvider == null)
						{
							this.innerSqlReservaProvider = new SqlReservaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlReservaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlReservaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlReservaProvider SqlReservaProvider
		{
			get {return ReservaProvider as SqlReservaProvider;}
		}
		
		#endregion
		
		
		#region "VConsultaReservaHabitacionProvider"
		
		private SqlVConsultaReservaHabitacionProvider innerSqlVConsultaReservaHabitacionProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="VConsultaReservaHabitacion"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override VConsultaReservaHabitacionProviderBase VConsultaReservaHabitacionProvider
		{
			get
			{
				if (innerSqlVConsultaReservaHabitacionProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlVConsultaReservaHabitacionProvider == null)
						{
							this.innerSqlVConsultaReservaHabitacionProvider = new SqlVConsultaReservaHabitacionProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlVConsultaReservaHabitacionProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlVConsultaReservaHabitacionProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlVConsultaReservaHabitacionProvider SqlVConsultaReservaHabitacionProvider
		{
			get {return VConsultaReservaHabitacionProvider as SqlVConsultaReservaHabitacionProvider;}
		}
		
		#endregion
		
		
		#region "VLocalidadProvider"
		
		private SqlVLocalidadProvider innerSqlVLocalidadProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="VLocalidad"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override VLocalidadProviderBase VLocalidadProvider
		{
			get
			{
				if (innerSqlVLocalidadProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlVLocalidadProvider == null)
						{
							this.innerSqlVLocalidadProvider = new SqlVLocalidadProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlVLocalidadProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlVLocalidadProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlVLocalidadProvider SqlVLocalidadProvider
		{
			get {return VLocalidadProvider as SqlVLocalidadProvider;}
		}
		
		#endregion
		
		
		#region "VPersonaProvider"
		
		private SqlVPersonaProvider innerSqlVPersonaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="VPersona"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override VPersonaProviderBase VPersonaProvider
		{
			get
			{
				if (innerSqlVPersonaProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlVPersonaProvider == null)
						{
							this.innerSqlVPersonaProvider = new SqlVPersonaProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlVPersonaProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlVPersonaProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlVPersonaProvider SqlVPersonaProvider
		{
			get {return VPersonaProvider as SqlVPersonaProvider;}
		}
		
		#endregion
		
		
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
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteNonQuery(storedProcedureName, parameterValues);	
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
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteNonQuery(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		public override void ExecuteNonQuery(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			database.ExecuteNonQuery(commandWrapper);	
			
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		public override void ExecuteNonQuery(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			database.ExecuteNonQuery(commandWrapper, transactionManager.TransactionObject);	
		}


		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteNonQuery(commandType, commandText);	
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
			Database database = transactionManager.Database;			
			return database.ExecuteNonQuery(transactionManager.TransactionObject , commandType, commandText);				
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
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteReader(storedProcedureName, parameterValues);	
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
			Database database = transactionManager.Database;
			return database.ExecuteReader(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteReader(commandWrapper);	
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			Database database = transactionManager.Database;
			return database.ExecuteReader(commandWrapper, transactionManager.TransactionObject);	
		}


		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteReader(commandType, commandText);	
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
			Database database = transactionManager.Database;			
			return database.ExecuteReader(transactionManager.TransactionObject , commandType, commandText);				
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
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteDataSet(storedProcedureName, parameterValues);	
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
			Database database = transactionManager.Database;
			return database.ExecuteDataSet(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteDataSet(commandWrapper);	
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			Database database = transactionManager.Database;
			return database.ExecuteDataSet(commandWrapper, transactionManager.TransactionObject);	
		}


		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteDataSet(commandType, commandText);	
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
			Database database = transactionManager.Database;			
			return database.ExecuteDataSet(transactionManager.TransactionObject , commandType, commandText);				
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
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteScalar(storedProcedureName, parameterValues);	
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
			Database database = transactionManager.Database;
			return database.ExecuteScalar(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override object ExecuteScalar(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteScalar(commandWrapper);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			Database database = transactionManager.Database;
			return database.ExecuteScalar(commandWrapper, transactionManager.TransactionObject);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override object ExecuteScalar(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteScalar(commandType, commandText);	
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
			Database database = transactionManager.Database;			
			return database.ExecuteScalar(transactionManager.TransactionObject , commandType, commandText);				
		}
		#endregion

		#endregion


	}
}
