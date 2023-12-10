#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using MAT.Entities;
using MAT.Data;

#endregion

namespace MAT.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="ServicioProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ServicioProviderBaseCore : EntityProviderBase<MAT.Entities.Servicio, MAT.Entities.ServicioKey>
	{		
		#region Get from Many To Many Relationship Functions
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.ServicioKey key)
		{
			return Delete(transactionManager, key.ServicioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_servicioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _servicioId)
		{
			return Delete(null, _servicioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _servicioId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Hotel key.
		///		FK_Servicio_Hotel Description: 
		/// </summary>
		/// <param name="_hotelId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByHotelId(System.Guid? _hotelId)
		{
			int count = -1;
			return GetByHotelId(_hotelId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Hotel key.
		///		FK_Servicio_Hotel Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		/// <remarks></remarks>
		public TList<Servicio> GetByHotelId(TransactionManager transactionManager, System.Guid? _hotelId)
		{
			int count = -1;
			return GetByHotelId(transactionManager, _hotelId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Hotel key.
		///		FK_Servicio_Hotel Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByHotelId(TransactionManager transactionManager, System.Guid? _hotelId, int start, int pageLength)
		{
			int count = -1;
			return GetByHotelId(transactionManager, _hotelId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Hotel key.
		///		fkServicioHotel Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_hotelId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByHotelId(System.Guid? _hotelId, int start, int pageLength)
		{
			int count =  -1;
			return GetByHotelId(null, _hotelId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Hotel key.
		///		fkServicioHotel Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_hotelId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByHotelId(System.Guid? _hotelId, int start, int pageLength,out int count)
		{
			return GetByHotelId(null, _hotelId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Hotel key.
		///		FK_Servicio_Hotel Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public abstract TList<Servicio> GetByHotelId(TransactionManager transactionManager, System.Guid? _hotelId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Proveedor key.
		///		FK_Servicio_Proveedor Description: 
		/// </summary>
		/// <param name="_proveedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByProveedorId(System.Guid? _proveedorId)
		{
			int count = -1;
			return GetByProveedorId(_proveedorId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Proveedor key.
		///		FK_Servicio_Proveedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_proveedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		/// <remarks></remarks>
		public TList<Servicio> GetByProveedorId(TransactionManager transactionManager, System.Guid? _proveedorId)
		{
			int count = -1;
			return GetByProveedorId(transactionManager, _proveedorId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Proveedor key.
		///		FK_Servicio_Proveedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_proveedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByProveedorId(TransactionManager transactionManager, System.Guid? _proveedorId, int start, int pageLength)
		{
			int count = -1;
			return GetByProveedorId(transactionManager, _proveedorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Proveedor key.
		///		fkServicioProveedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_proveedorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByProveedorId(System.Guid? _proveedorId, int start, int pageLength)
		{
			int count =  -1;
			return GetByProveedorId(null, _proveedorId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Proveedor key.
		///		fkServicioProveedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_proveedorId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByProveedorId(System.Guid? _proveedorId, int start, int pageLength,out int count)
		{
			return GetByProveedorId(null, _proveedorId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Proveedor key.
		///		FK_Servicio_Proveedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_proveedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public abstract TList<Servicio> GetByProveedorId(TransactionManager transactionManager, System.Guid? _proveedorId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Transporte key.
		///		FK_Servicio_Transporte Description: 
		/// </summary>
		/// <param name="_transporteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByTransporteId(System.Guid? _transporteId)
		{
			int count = -1;
			return GetByTransporteId(_transporteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Transporte key.
		///		FK_Servicio_Transporte Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_transporteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		/// <remarks></remarks>
		public TList<Servicio> GetByTransporteId(TransactionManager transactionManager, System.Guid? _transporteId)
		{
			int count = -1;
			return GetByTransporteId(transactionManager, _transporteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Transporte key.
		///		FK_Servicio_Transporte Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_transporteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByTransporteId(TransactionManager transactionManager, System.Guid? _transporteId, int start, int pageLength)
		{
			int count = -1;
			return GetByTransporteId(transactionManager, _transporteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Transporte key.
		///		fkServicioTransporte Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_transporteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByTransporteId(System.Guid? _transporteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByTransporteId(null, _transporteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Transporte key.
		///		fkServicioTransporte Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_transporteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public TList<Servicio> GetByTransporteId(System.Guid? _transporteId, int start, int pageLength,out int count)
		{
			return GetByTransporteId(null, _transporteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Servicio_Transporte key.
		///		FK_Servicio_Transporte Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_transporteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Servicio objects.</returns>
		public abstract TList<Servicio> GetByTransporteId(TransactionManager transactionManager, System.Guid? _transporteId, int start, int pageLength, out int count);
		
		#endregion

		#region Get By Index Functions
		
		/// <summary>
		/// 	Gets a row from the DataSource based on its primary key.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to retrieve.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <returns>Returns an instance of the Entity class.</returns>
		public override MAT.Entities.Servicio Get(TransactionManager transactionManager, MAT.Entities.ServicioKey key, int start, int pageLength)
		{
			return GetByServicioId(transactionManager, key.ServicioId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Servicio index.
		/// </summary>
		/// <param name="_servicioId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Servicio"/> class.</returns>
		public MAT.Entities.Servicio GetByServicioId(System.Guid _servicioId)
		{
			int count = -1;
			return GetByServicioId(null,_servicioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Servicio index.
		/// </summary>
		/// <param name="_servicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Servicio"/> class.</returns>
		public MAT.Entities.Servicio GetByServicioId(System.Guid _servicioId, int start, int pageLength)
		{
			int count = -1;
			return GetByServicioId(null, _servicioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Servicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicioId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Servicio"/> class.</returns>
		public MAT.Entities.Servicio GetByServicioId(TransactionManager transactionManager, System.Guid _servicioId)
		{
			int count = -1;
			return GetByServicioId(transactionManager, _servicioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Servicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Servicio"/> class.</returns>
		public MAT.Entities.Servicio GetByServicioId(TransactionManager transactionManager, System.Guid _servicioId, int start, int pageLength)
		{
			int count = -1;
			return GetByServicioId(transactionManager, _servicioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Servicio index.
		/// </summary>
		/// <param name="_servicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Servicio"/> class.</returns>
		public MAT.Entities.Servicio GetByServicioId(System.Guid _servicioId, int start, int pageLength, out int count)
		{
			return GetByServicioId(null, _servicioId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Servicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Servicio"/> class.</returns>
		public abstract MAT.Entities.Servicio GetByServicioId(TransactionManager transactionManager, System.Guid _servicioId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Servicio&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Servicio&gt;"/></returns>
		public static TList<Servicio> Fill(IDataReader reader, TList<Servicio> rows, int start, int pageLength)
		{
			NetTiersProvider currentProvider = DataRepository.Provider;
            bool useEntityFactory = currentProvider.UseEntityFactory;
            bool enableEntityTracking = currentProvider.EnableEntityTracking;
            LoadPolicy currentLoadPolicy = currentProvider.CurrentLoadPolicy;
			Type entityCreationFactoryType = currentProvider.EntityCreationalFactoryType;
			
			// advance to the starting row
			for (int i = 0; i < start; i++)
			{
				if (!reader.Read())
				return rows; // not enough rows, just return
			}
			for (int i = 0; i < pageLength; i++)
			{
				if (!reader.Read())
					break; // we are done
					
				string key = null;
				
				MAT.Entities.Servicio c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Servicio")
					.Append("|").Append((System.Guid)reader[((int)ServicioColumn.ServicioId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Servicio>(
					key.ToString(), // EntityTrackingKey
					"Servicio",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Servicio();
				}
				
				if (!enableEntityTracking ||
					c.EntityState == EntityState.Added ||
					(enableEntityTracking &&
					
						(
							(currentLoadPolicy == LoadPolicy.PreserveChanges && c.EntityState == EntityState.Unchanged) ||
							(currentLoadPolicy == LoadPolicy.DiscardChanges && c.EntityState != EntityState.Unchanged)
						)
					))
				{
					c.SuppressEntityEvents = true;
					c.ServicioId = (System.Guid)reader[((int)ServicioColumn.ServicioId - 1)];
					c.OriginalServicioId = c.ServicioId;
					c.Descripcion = (System.String)reader[((int)ServicioColumn.Descripcion - 1)];
					c.Precio = (reader.IsDBNull(((int)ServicioColumn.Precio - 1)))?null:(System.Double?)reader[((int)ServicioColumn.Precio - 1)];
					c.Moneda = (reader.IsDBNull(((int)ServicioColumn.Moneda - 1)))?null:(System.String)reader[((int)ServicioColumn.Moneda - 1)];
					c.Iva = (reader.IsDBNull(((int)ServicioColumn.Iva - 1)))?null:(System.String)reader[((int)ServicioColumn.Iva - 1)];
					c.Alicuota = (reader.IsDBNull(((int)ServicioColumn.Alicuota - 1)))?null:(System.Double?)reader[((int)ServicioColumn.Alicuota - 1)];
					c.Validez = (reader.IsDBNull(((int)ServicioColumn.Validez - 1)))?null:(System.DateTime?)reader[((int)ServicioColumn.Validez - 1)];
					c.VisibilidadTarifa = (reader.IsDBNull(((int)ServicioColumn.VisibilidadTarifa - 1)))?null:(System.Int32?)reader[((int)ServicioColumn.VisibilidadTarifa - 1)];
					c.ProveedorId = (reader.IsDBNull(((int)ServicioColumn.ProveedorId - 1)))?null:(System.Guid?)reader[((int)ServicioColumn.ProveedorId - 1)];
					c.TransporteId = (reader.IsDBNull(((int)ServicioColumn.TransporteId - 1)))?null:(System.Guid?)reader[((int)ServicioColumn.TransporteId - 1)];
					c.HotelId = (reader.IsDBNull(((int)ServicioColumn.HotelId - 1)))?null:(System.Guid?)reader[((int)ServicioColumn.HotelId - 1)];
					c.TipoServicio = (reader.IsDBNull(((int)ServicioColumn.TipoServicio - 1)))?null:(System.Int32?)reader[((int)ServicioColumn.TipoServicio - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Servicio"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Servicio"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Servicio entity)
		{
			if (!reader.Read()) return;
			
			entity.ServicioId = (System.Guid)reader[((int)ServicioColumn.ServicioId - 1)];
			entity.OriginalServicioId = (System.Guid)reader["ServicioID"];
			entity.Descripcion = (System.String)reader[((int)ServicioColumn.Descripcion - 1)];
			entity.Precio = (reader.IsDBNull(((int)ServicioColumn.Precio - 1)))?null:(System.Double?)reader[((int)ServicioColumn.Precio - 1)];
			entity.Moneda = (reader.IsDBNull(((int)ServicioColumn.Moneda - 1)))?null:(System.String)reader[((int)ServicioColumn.Moneda - 1)];
			entity.Iva = (reader.IsDBNull(((int)ServicioColumn.Iva - 1)))?null:(System.String)reader[((int)ServicioColumn.Iva - 1)];
			entity.Alicuota = (reader.IsDBNull(((int)ServicioColumn.Alicuota - 1)))?null:(System.Double?)reader[((int)ServicioColumn.Alicuota - 1)];
			entity.Validez = (reader.IsDBNull(((int)ServicioColumn.Validez - 1)))?null:(System.DateTime?)reader[((int)ServicioColumn.Validez - 1)];
			entity.VisibilidadTarifa = (reader.IsDBNull(((int)ServicioColumn.VisibilidadTarifa - 1)))?null:(System.Int32?)reader[((int)ServicioColumn.VisibilidadTarifa - 1)];
			entity.ProveedorId = (reader.IsDBNull(((int)ServicioColumn.ProveedorId - 1)))?null:(System.Guid?)reader[((int)ServicioColumn.ProveedorId - 1)];
			entity.TransporteId = (reader.IsDBNull(((int)ServicioColumn.TransporteId - 1)))?null:(System.Guid?)reader[((int)ServicioColumn.TransporteId - 1)];
			entity.HotelId = (reader.IsDBNull(((int)ServicioColumn.HotelId - 1)))?null:(System.Guid?)reader[((int)ServicioColumn.HotelId - 1)];
			entity.TipoServicio = (reader.IsDBNull(((int)ServicioColumn.TipoServicio - 1)))?null:(System.Int32?)reader[((int)ServicioColumn.TipoServicio - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Servicio"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Servicio"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Servicio entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ServicioId = (System.Guid)dataRow["ServicioID"];
			entity.OriginalServicioId = (System.Guid)dataRow["ServicioID"];
			entity.Descripcion = (System.String)dataRow["Descripcion"];
			entity.Precio = Convert.IsDBNull(dataRow["Precio"]) ? null : (System.Double?)dataRow["Precio"];
			entity.Moneda = Convert.IsDBNull(dataRow["Moneda"]) ? null : (System.String)dataRow["Moneda"];
			entity.Iva = Convert.IsDBNull(dataRow["Iva"]) ? null : (System.String)dataRow["Iva"];
			entity.Alicuota = Convert.IsDBNull(dataRow["Alicuota"]) ? null : (System.Double?)dataRow["Alicuota"];
			entity.Validez = Convert.IsDBNull(dataRow["Validez"]) ? null : (System.DateTime?)dataRow["Validez"];
			entity.VisibilidadTarifa = Convert.IsDBNull(dataRow["VisibilidadTarifa"]) ? null : (System.Int32?)dataRow["VisibilidadTarifa"];
			entity.ProveedorId = Convert.IsDBNull(dataRow["ProveedorID"]) ? null : (System.Guid?)dataRow["ProveedorID"];
			entity.TransporteId = Convert.IsDBNull(dataRow["TransporteID"]) ? null : (System.Guid?)dataRow["TransporteID"];
			entity.HotelId = Convert.IsDBNull(dataRow["HotelID"]) ? null : (System.Guid?)dataRow["HotelID"];
			entity.TipoServicio = Convert.IsDBNull(dataRow["TipoServicio"]) ? null : (System.Int32?)dataRow["TipoServicio"];
			entity.AcceptChanges();
		}
		#endregion 
		
		#region DeepLoad Methods
		/// <summary>
		/// Deep Loads the <see cref="IEntity"/> object with criteria based of the child 
		/// property collections only N Levels Deep based on the <see cref="DeepLoadType"/>.
		/// </summary>
		/// <remarks>
		/// Use this method with caution as it is possible to DeepLoad with Recursion and traverse an entire object graph.
		/// </remarks>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">The <see cref="MAT.Entities.Servicio"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Servicio Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Servicio entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region HotelIdSource	
			if (CanDeepLoad(entity, "Hotel|HotelIdSource", deepLoadType, innerList) 
				&& entity.HotelIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.HotelId ?? Guid.Empty);
				Hotel tmpEntity = EntityManager.LocateEntity<Hotel>(EntityLocator.ConstructKeyFromPkItems(typeof(Hotel), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.HotelIdSource = tmpEntity;
				else
					entity.HotelIdSource = DataRepository.HotelProvider.GetByHotelId(transactionManager, (entity.HotelId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'HotelIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.HotelIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.HotelProvider.DeepLoad(transactionManager, entity.HotelIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion HotelIdSource

			#region ProveedorIdSource	
			if (CanDeepLoad(entity, "Proveedor|ProveedorIdSource", deepLoadType, innerList) 
				&& entity.ProveedorIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.ProveedorId ?? Guid.Empty);
				Proveedor tmpEntity = EntityManager.LocateEntity<Proveedor>(EntityLocator.ConstructKeyFromPkItems(typeof(Proveedor), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ProveedorIdSource = tmpEntity;
				else
					entity.ProveedorIdSource = DataRepository.ProveedorProvider.GetByProveedorId(transactionManager, (entity.ProveedorId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ProveedorIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ProveedorIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ProveedorProvider.DeepLoad(transactionManager, entity.ProveedorIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ProveedorIdSource

			#region TransporteIdSource	
			if (CanDeepLoad(entity, "Transporte|TransporteIdSource", deepLoadType, innerList) 
				&& entity.TransporteIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.TransporteId ?? Guid.Empty);
				Transporte tmpEntity = EntityManager.LocateEntity<Transporte>(EntityLocator.ConstructKeyFromPkItems(typeof(Transporte), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.TransporteIdSource = tmpEntity;
				else
					entity.TransporteIdSource = DataRepository.TransporteProvider.GetByTransporteId(transactionManager, (entity.TransporteId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'TransporteIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.TransporteIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.TransporteProvider.DeepLoad(transactionManager, entity.TransporteIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion TransporteIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByServicioId methods when available
			
			#region PaqueteServicioCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PaqueteServicio>|PaqueteServicioCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaqueteServicioCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PaqueteServicioCollection = DataRepository.PaqueteServicioProvider.GetByServicioId(transactionManager, entity.ServicioId);

				if (deep && entity.PaqueteServicioCollection.Count > 0)
				{
					deepHandles.Add("PaqueteServicioCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PaqueteServicio>) DataRepository.PaqueteServicioProvider.DeepLoad,
						new object[] { transactionManager, entity.PaqueteServicioCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			//Fire all DeepLoad Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			deepHandles = null;
		}
		
		#endregion 
		
		#region DeepSave Methods

		/// <summary>
		/// Deep Save the entire object graph of the MAT.Entities.Servicio object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Servicio instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Servicio Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Servicio entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region HotelIdSource
			if (CanDeepSave(entity, "Hotel|HotelIdSource", deepSaveType, innerList) 
				&& entity.HotelIdSource != null)
			{
				DataRepository.HotelProvider.Save(transactionManager, entity.HotelIdSource);
				entity.HotelId = entity.HotelIdSource.HotelId;
			}
			#endregion 
			
			#region ProveedorIdSource
			if (CanDeepSave(entity, "Proveedor|ProveedorIdSource", deepSaveType, innerList) 
				&& entity.ProveedorIdSource != null)
			{
				DataRepository.ProveedorProvider.Save(transactionManager, entity.ProveedorIdSource);
				entity.ProveedorId = entity.ProveedorIdSource.ProveedorId;
			}
			#endregion 
			
			#region TransporteIdSource
			if (CanDeepSave(entity, "Transporte|TransporteIdSource", deepSaveType, innerList) 
				&& entity.TransporteIdSource != null)
			{
				DataRepository.TransporteProvider.Save(transactionManager, entity.TransporteIdSource);
				entity.TransporteId = entity.TransporteIdSource.TransporteId;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<PaqueteServicio>
				if (CanDeepSave(entity.PaqueteServicioCollection, "List<PaqueteServicio>|PaqueteServicioCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PaqueteServicio child in entity.PaqueteServicioCollection)
					{
						if(child.ServicioIdSource != null)
						{
							child.ServicioId = child.ServicioIdSource.ServicioId;
						}
						else
						{
							child.ServicioId = entity.ServicioId;
						}

					}

					if (entity.PaqueteServicioCollection.Count > 0 || entity.PaqueteServicioCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PaqueteServicioProvider.Save(transactionManager, entity.PaqueteServicioCollection);
						
						deepHandles.Add("PaqueteServicioCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PaqueteServicio >) DataRepository.PaqueteServicioProvider.DeepSave,
							new object[] { transactionManager, entity.PaqueteServicioCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
			//Fire all DeepSave Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			
			// Save Root Entity through Provider, if not already saved in delete mode
			if (entity.IsDeleted)
				this.Save(transactionManager, entity);
				

			deepHandles = null;
						
			return true;
		}
		#endregion
	} // end class
	
	#region ServicioChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Servicio</c>
	///</summary>
	public enum ServicioChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Hotel</c> at HotelIdSource
		///</summary>
		[ChildEntityType(typeof(Hotel))]
		Hotel,
		
		///<summary>
		/// Composite Property for <c>Proveedor</c> at ProveedorIdSource
		///</summary>
		[ChildEntityType(typeof(Proveedor))]
		Proveedor,
		
		///<summary>
		/// Composite Property for <c>Transporte</c> at TransporteIdSource
		///</summary>
		[ChildEntityType(typeof(Transporte))]
		Transporte,
		///<summary>
		/// Collection of <c>Servicio</c> as OneToMany for PaqueteServicioCollection
		///</summary>
		[ChildEntityType(typeof(TList<PaqueteServicio>))]
		PaqueteServicioCollection,
	}
	
	#endregion ServicioChildEntityTypes
	
	#region ServicioFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Servicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicioFilterBuilder : SqlFilterBuilder<ServicioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicioFilterBuilder class.
		/// </summary>
		public ServicioFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ServicioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ServicioFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ServicioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ServicioFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ServicioFilterBuilder
	
	#region ServicioParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Servicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicioParameterBuilder : ParameterizedSqlFilterBuilder<ServicioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicioParameterBuilder class.
		/// </summary>
		public ServicioParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ServicioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ServicioParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ServicioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ServicioParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ServicioParameterBuilder
	
	#region ServicioSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Servicio"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ServicioSortBuilder : SqlSortBuilder<ServicioColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicioSqlSortBuilder class.
		/// </summary>
		public ServicioSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ServicioSortBuilder
	
} // end namespace
