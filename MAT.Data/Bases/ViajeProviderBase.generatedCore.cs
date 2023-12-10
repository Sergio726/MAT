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
	/// This class is the base class for any <see cref="ViajeProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ViajeProviderBaseCore : EntityProviderBase<MAT.Entities.Viaje, MAT.Entities.ViajeKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.ViajeKey key)
		{
			return Delete(transactionManager, key.ViajeId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_viajeId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _viajeId)
		{
			return Delete(null, _viajeId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _viajeId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Paquete key.
		///		FK_Viaje_Paquete Description: 
		/// </summary>
		/// <param name="_paqueteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		public TList<Viaje> GetByPaqueteId(System.Guid? _paqueteId)
		{
			int count = -1;
			return GetByPaqueteId(_paqueteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Paquete key.
		///		FK_Viaje_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		/// <remarks></remarks>
		public TList<Viaje> GetByPaqueteId(TransactionManager transactionManager, System.Guid? _paqueteId)
		{
			int count = -1;
			return GetByPaqueteId(transactionManager, _paqueteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Paquete key.
		///		FK_Viaje_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		public TList<Viaje> GetByPaqueteId(TransactionManager transactionManager, System.Guid? _paqueteId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaqueteId(transactionManager, _paqueteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Paquete key.
		///		fkViajePaquete Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_paqueteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		public TList<Viaje> GetByPaqueteId(System.Guid? _paqueteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPaqueteId(null, _paqueteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Paquete key.
		///		fkViajePaquete Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_paqueteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		public TList<Viaje> GetByPaqueteId(System.Guid? _paqueteId, int start, int pageLength,out int count)
		{
			return GetByPaqueteId(null, _paqueteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Paquete key.
		///		FK_Viaje_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		public abstract TList<Viaje> GetByPaqueteId(TransactionManager transactionManager, System.Guid? _paqueteId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Transporte key.
		///		FK_Viaje_Transporte Description: 
		/// </summary>
		/// <param name="_busId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		public TList<Viaje> GetByBusId(System.Guid? _busId)
		{
			int count = -1;
			return GetByBusId(_busId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Transporte key.
		///		FK_Viaje_Transporte Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_busId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		/// <remarks></remarks>
		public TList<Viaje> GetByBusId(TransactionManager transactionManager, System.Guid? _busId)
		{
			int count = -1;
			return GetByBusId(transactionManager, _busId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Transporte key.
		///		FK_Viaje_Transporte Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_busId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		public TList<Viaje> GetByBusId(TransactionManager transactionManager, System.Guid? _busId, int start, int pageLength)
		{
			int count = -1;
			return GetByBusId(transactionManager, _busId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Transporte key.
		///		fkViajeTransporte Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_busId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		public TList<Viaje> GetByBusId(System.Guid? _busId, int start, int pageLength)
		{
			int count =  -1;
			return GetByBusId(null, _busId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Transporte key.
		///		fkViajeTransporte Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_busId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		public TList<Viaje> GetByBusId(System.Guid? _busId, int start, int pageLength,out int count)
		{
			return GetByBusId(null, _busId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Viaje_Transporte key.
		///		FK_Viaje_Transporte Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_busId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Viaje objects.</returns>
		public abstract TList<Viaje> GetByBusId(TransactionManager transactionManager, System.Guid? _busId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Viaje Get(TransactionManager transactionManager, MAT.Entities.ViajeKey key, int start, int pageLength)
		{
			return GetByViajeId(transactionManager, key.ViajeId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Viaje index.
		/// </summary>
		/// <param name="_viajeId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Viaje"/> class.</returns>
		public MAT.Entities.Viaje GetByViajeId(System.Guid _viajeId)
		{
			int count = -1;
			return GetByViajeId(null,_viajeId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Viaje index.
		/// </summary>
		/// <param name="_viajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Viaje"/> class.</returns>
		public MAT.Entities.Viaje GetByViajeId(System.Guid _viajeId, int start, int pageLength)
		{
			int count = -1;
			return GetByViajeId(null, _viajeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Viaje index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Viaje"/> class.</returns>
		public MAT.Entities.Viaje GetByViajeId(TransactionManager transactionManager, System.Guid _viajeId)
		{
			int count = -1;
			return GetByViajeId(transactionManager, _viajeId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Viaje index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Viaje"/> class.</returns>
		public MAT.Entities.Viaje GetByViajeId(TransactionManager transactionManager, System.Guid _viajeId, int start, int pageLength)
		{
			int count = -1;
			return GetByViajeId(transactionManager, _viajeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Viaje index.
		/// </summary>
		/// <param name="_viajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Viaje"/> class.</returns>
		public MAT.Entities.Viaje GetByViajeId(System.Guid _viajeId, int start, int pageLength, out int count)
		{
			return GetByViajeId(null, _viajeId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Viaje index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Viaje"/> class.</returns>
		public abstract MAT.Entities.Viaje GetByViajeId(TransactionManager transactionManager, System.Guid _viajeId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Viaje&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Viaje&gt;"/></returns>
		public static TList<Viaje> Fill(IDataReader reader, TList<Viaje> rows, int start, int pageLength)
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
				
				MAT.Entities.Viaje c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Viaje")
					.Append("|").Append((System.Guid)reader[((int)ViajeColumn.ViajeId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Viaje>(
					key.ToString(), // EntityTrackingKey
					"Viaje",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Viaje();
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
					c.ViajeId = (System.Guid)reader[((int)ViajeColumn.ViajeId - 1)];
					c.OriginalViajeId = c.ViajeId;
					c.PaqueteId = (reader.IsDBNull(((int)ViajeColumn.PaqueteId - 1)))?null:(System.Guid?)reader[((int)ViajeColumn.PaqueteId - 1)];
					c.Origen = (reader.IsDBNull(((int)ViajeColumn.Origen - 1)))?null:(System.String)reader[((int)ViajeColumn.Origen - 1)];
					c.FechaSalida = (reader.IsDBNull(((int)ViajeColumn.FechaSalida - 1)))?null:(System.DateTime?)reader[((int)ViajeColumn.FechaSalida - 1)];
					c.HoraSalida = (reader.IsDBNull(((int)ViajeColumn.HoraSalida - 1)))?null:(System.String)reader[((int)ViajeColumn.HoraSalida - 1)];
					c.PaisOrigen = (reader.IsDBNull(((int)ViajeColumn.PaisOrigen - 1)))?null:(System.String)reader[((int)ViajeColumn.PaisOrigen - 1)];
					c.PaisDestino = (reader.IsDBNull(((int)ViajeColumn.PaisDestino - 1)))?null:(System.String)reader[((int)ViajeColumn.PaisDestino - 1)];
					c.Paso = (reader.IsDBNull(((int)ViajeColumn.Paso - 1)))?null:(System.String)reader[((int)ViajeColumn.Paso - 1)];
					c.Medio = (reader.IsDBNull(((int)ViajeColumn.Medio - 1)))?null:(System.String)reader[((int)ViajeColumn.Medio - 1)];
					c.BusId = (reader.IsDBNull(((int)ViajeColumn.BusId - 1)))?null:(System.Guid?)reader[((int)ViajeColumn.BusId - 1)];
					c.FechaRegreso = (reader.IsDBNull(((int)ViajeColumn.FechaRegreso - 1)))?null:(System.DateTime?)reader[((int)ViajeColumn.FechaRegreso - 1)];
					c.HoraRegreso = (reader.IsDBNull(((int)ViajeColumn.HoraRegreso - 1)))?null:(System.String)reader[((int)ViajeColumn.HoraRegreso - 1)];
					c.Descripcion = (reader.IsDBNull(((int)ViajeColumn.Descripcion - 1)))?null:(System.String)reader[((int)ViajeColumn.Descripcion - 1)];
					c.PrecioSemicama = (reader.IsDBNull(((int)ViajeColumn.PrecioSemicama - 1)))?null:(System.Double?)reader[((int)ViajeColumn.PrecioSemicama - 1)];
					c.PrecioCama = (reader.IsDBNull(((int)ViajeColumn.PrecioCama - 1)))?null:(System.Double?)reader[((int)ViajeColumn.PrecioCama - 1)];
					c.PrecioPromocional = (reader.IsDBNull(((int)ViajeColumn.PrecioPromocional - 1)))?null:(System.Double?)reader[((int)ViajeColumn.PrecioPromocional - 1)];
					c.FechaPromocion = (reader.IsDBNull(((int)ViajeColumn.FechaPromocion - 1)))?null:(System.DateTime?)reader[((int)ViajeColumn.FechaPromocion - 1)];
					c.NDias = (reader.IsDBNull(((int)ViajeColumn.NDias - 1)))?null:(System.Int32?)reader[((int)ViajeColumn.NDias - 1)];
					c.NNoches = (reader.IsDBNull(((int)ViajeColumn.NNoches - 1)))?null:(System.Int32?)reader[((int)ViajeColumn.NNoches - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Viaje"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Viaje"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Viaje entity)
		{
			if (!reader.Read()) return;
			
			entity.ViajeId = (System.Guid)reader[((int)ViajeColumn.ViajeId - 1)];
			entity.OriginalViajeId = (System.Guid)reader["ViajeID"];
			entity.PaqueteId = (reader.IsDBNull(((int)ViajeColumn.PaqueteId - 1)))?null:(System.Guid?)reader[((int)ViajeColumn.PaqueteId - 1)];
			entity.Origen = (reader.IsDBNull(((int)ViajeColumn.Origen - 1)))?null:(System.String)reader[((int)ViajeColumn.Origen - 1)];
			entity.FechaSalida = (reader.IsDBNull(((int)ViajeColumn.FechaSalida - 1)))?null:(System.DateTime?)reader[((int)ViajeColumn.FechaSalida - 1)];
			entity.HoraSalida = (reader.IsDBNull(((int)ViajeColumn.HoraSalida - 1)))?null:(System.String)reader[((int)ViajeColumn.HoraSalida - 1)];
			entity.PaisOrigen = (reader.IsDBNull(((int)ViajeColumn.PaisOrigen - 1)))?null:(System.String)reader[((int)ViajeColumn.PaisOrigen - 1)];
			entity.PaisDestino = (reader.IsDBNull(((int)ViajeColumn.PaisDestino - 1)))?null:(System.String)reader[((int)ViajeColumn.PaisDestino - 1)];
			entity.Paso = (reader.IsDBNull(((int)ViajeColumn.Paso - 1)))?null:(System.String)reader[((int)ViajeColumn.Paso - 1)];
			entity.Medio = (reader.IsDBNull(((int)ViajeColumn.Medio - 1)))?null:(System.String)reader[((int)ViajeColumn.Medio - 1)];
			entity.BusId = (reader.IsDBNull(((int)ViajeColumn.BusId - 1)))?null:(System.Guid?)reader[((int)ViajeColumn.BusId - 1)];
			entity.FechaRegreso = (reader.IsDBNull(((int)ViajeColumn.FechaRegreso - 1)))?null:(System.DateTime?)reader[((int)ViajeColumn.FechaRegreso - 1)];
			entity.HoraRegreso = (reader.IsDBNull(((int)ViajeColumn.HoraRegreso - 1)))?null:(System.String)reader[((int)ViajeColumn.HoraRegreso - 1)];
			entity.Descripcion = (reader.IsDBNull(((int)ViajeColumn.Descripcion - 1)))?null:(System.String)reader[((int)ViajeColumn.Descripcion - 1)];
			entity.PrecioSemicama = (reader.IsDBNull(((int)ViajeColumn.PrecioSemicama - 1)))?null:(System.Double?)reader[((int)ViajeColumn.PrecioSemicama - 1)];
			entity.PrecioCama = (reader.IsDBNull(((int)ViajeColumn.PrecioCama - 1)))?null:(System.Double?)reader[((int)ViajeColumn.PrecioCama - 1)];
			entity.PrecioPromocional = (reader.IsDBNull(((int)ViajeColumn.PrecioPromocional - 1)))?null:(System.Double?)reader[((int)ViajeColumn.PrecioPromocional - 1)];
			entity.FechaPromocion = (reader.IsDBNull(((int)ViajeColumn.FechaPromocion - 1)))?null:(System.DateTime?)reader[((int)ViajeColumn.FechaPromocion - 1)];
			entity.NDias = (reader.IsDBNull(((int)ViajeColumn.NDias - 1)))?null:(System.Int32?)reader[((int)ViajeColumn.NDias - 1)];
			entity.NNoches = (reader.IsDBNull(((int)ViajeColumn.NNoches - 1)))?null:(System.Int32?)reader[((int)ViajeColumn.NNoches - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Viaje"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Viaje"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Viaje entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ViajeId = (System.Guid)dataRow["ViajeID"];
			entity.OriginalViajeId = (System.Guid)dataRow["ViajeID"];
			entity.PaqueteId = Convert.IsDBNull(dataRow["PaqueteID"]) ? null : (System.Guid?)dataRow["PaqueteID"];
			entity.Origen = Convert.IsDBNull(dataRow["Origen"]) ? null : (System.String)dataRow["Origen"];
			entity.FechaSalida = Convert.IsDBNull(dataRow["FechaSalida"]) ? null : (System.DateTime?)dataRow["FechaSalida"];
			entity.HoraSalida = Convert.IsDBNull(dataRow["HoraSalida"]) ? null : (System.String)dataRow["HoraSalida"];
			entity.PaisOrigen = Convert.IsDBNull(dataRow["PaisOrigen"]) ? null : (System.String)dataRow["PaisOrigen"];
			entity.PaisDestino = Convert.IsDBNull(dataRow["PaisDestino"]) ? null : (System.String)dataRow["PaisDestino"];
			entity.Paso = Convert.IsDBNull(dataRow["Paso"]) ? null : (System.String)dataRow["Paso"];
			entity.Medio = Convert.IsDBNull(dataRow["Medio"]) ? null : (System.String)dataRow["Medio"];
			entity.BusId = Convert.IsDBNull(dataRow["BusID"]) ? null : (System.Guid?)dataRow["BusID"];
			entity.FechaRegreso = Convert.IsDBNull(dataRow["FechaRegreso"]) ? null : (System.DateTime?)dataRow["FechaRegreso"];
			entity.HoraRegreso = Convert.IsDBNull(dataRow["HoraRegreso"]) ? null : (System.String)dataRow["HoraRegreso"];
			entity.Descripcion = Convert.IsDBNull(dataRow["Descripcion"]) ? null : (System.String)dataRow["Descripcion"];
			entity.PrecioSemicama = Convert.IsDBNull(dataRow["PrecioSemicama"]) ? null : (System.Double?)dataRow["PrecioSemicama"];
			entity.PrecioCama = Convert.IsDBNull(dataRow["PrecioCama"]) ? null : (System.Double?)dataRow["PrecioCama"];
			entity.PrecioPromocional = Convert.IsDBNull(dataRow["PrecioPromocional"]) ? null : (System.Double?)dataRow["PrecioPromocional"];
			entity.FechaPromocion = Convert.IsDBNull(dataRow["FechaPromocion"]) ? null : (System.DateTime?)dataRow["FechaPromocion"];
			entity.NDias = Convert.IsDBNull(dataRow["nDias"]) ? null : (System.Int32?)dataRow["nDias"];
			entity.NNoches = Convert.IsDBNull(dataRow["nNoches"]) ? null : (System.Int32?)dataRow["nNoches"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Viaje"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Viaje Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Viaje entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region PaqueteIdSource	
			if (CanDeepLoad(entity, "Paquete|PaqueteIdSource", deepLoadType, innerList) 
				&& entity.PaqueteIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.PaqueteId ?? Guid.Empty);
				Paquete tmpEntity = EntityManager.LocateEntity<Paquete>(EntityLocator.ConstructKeyFromPkItems(typeof(Paquete), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PaqueteIdSource = tmpEntity;
				else
					entity.PaqueteIdSource = DataRepository.PaqueteProvider.GetByPaqueteId(transactionManager, (entity.PaqueteId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaqueteIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PaqueteIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PaqueteProvider.DeepLoad(transactionManager, entity.PaqueteIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PaqueteIdSource

			#region BusIdSource	
			if (CanDeepLoad(entity, "Transporte|BusIdSource", deepLoadType, innerList) 
				&& entity.BusIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.BusId ?? Guid.Empty);
				Transporte tmpEntity = EntityManager.LocateEntity<Transporte>(EntityLocator.ConstructKeyFromPkItems(typeof(Transporte), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.BusIdSource = tmpEntity;
				else
					entity.BusIdSource = DataRepository.TransporteProvider.GetByTransporteId(transactionManager, (entity.BusId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'BusIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.BusIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.TransporteProvider.DeepLoad(transactionManager, entity.BusIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion BusIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByViajeId methods when available
			
			#region PasajeCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Pasaje>|PasajeCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PasajeCollection = DataRepository.PasajeProvider.GetByViajeId(transactionManager, entity.ViajeId);

				if (deep && entity.PasajeCollection.Count > 0)
				{
					deepHandles.Add("PasajeCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Pasaje>) DataRepository.PasajeProvider.DeepLoad,
						new object[] { transactionManager, entity.PasajeCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region ViajeHotelCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<ViajeHotel>|ViajeHotelCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ViajeHotelCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ViajeHotelCollection = DataRepository.ViajeHotelProvider.GetByViajeId(transactionManager, entity.ViajeId);

				if (deep && entity.ViajeHotelCollection.Count > 0)
				{
					deepHandles.Add("ViajeHotelCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<ViajeHotel>) DataRepository.ViajeHotelProvider.DeepLoad,
						new object[] { transactionManager, entity.ViajeHotelCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Viaje object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Viaje instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Viaje Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Viaje entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region PaqueteIdSource
			if (CanDeepSave(entity, "Paquete|PaqueteIdSource", deepSaveType, innerList) 
				&& entity.PaqueteIdSource != null)
			{
				DataRepository.PaqueteProvider.Save(transactionManager, entity.PaqueteIdSource);
				entity.PaqueteId = entity.PaqueteIdSource.PaqueteId;
			}
			#endregion 
			
			#region BusIdSource
			if (CanDeepSave(entity, "Transporte|BusIdSource", deepSaveType, innerList) 
				&& entity.BusIdSource != null)
			{
				DataRepository.TransporteProvider.Save(transactionManager, entity.BusIdSource);
				entity.BusId = entity.BusIdSource.TransporteId;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<Pasaje>
				if (CanDeepSave(entity.PasajeCollection, "List<Pasaje>|PasajeCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Pasaje child in entity.PasajeCollection)
					{
						if(child.ViajeIdSource != null)
						{
							child.ViajeId = child.ViajeIdSource.ViajeId;
						}
						else
						{
							child.ViajeId = entity.ViajeId;
						}

					}

					if (entity.PasajeCollection.Count > 0 || entity.PasajeCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PasajeProvider.Save(transactionManager, entity.PasajeCollection);
						
						deepHandles.Add("PasajeCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Pasaje >) DataRepository.PasajeProvider.DeepSave,
							new object[] { transactionManager, entity.PasajeCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<ViajeHotel>
				if (CanDeepSave(entity.ViajeHotelCollection, "List<ViajeHotel>|ViajeHotelCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(ViajeHotel child in entity.ViajeHotelCollection)
					{
						if(child.ViajeIdSource != null)
						{
							child.ViajeId = child.ViajeIdSource.ViajeId;
						}
						else
						{
							child.ViajeId = entity.ViajeId;
						}

					}

					if (entity.ViajeHotelCollection.Count > 0 || entity.ViajeHotelCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ViajeHotelProvider.Save(transactionManager, entity.ViajeHotelCollection);
						
						deepHandles.Add("ViajeHotelCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< ViajeHotel >) DataRepository.ViajeHotelProvider.DeepSave,
							new object[] { transactionManager, entity.ViajeHotelCollection, deepSaveType, childTypes, innerList }
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
	
	#region ViajeChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Viaje</c>
	///</summary>
	public enum ViajeChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Paquete</c> at PaqueteIdSource
		///</summary>
		[ChildEntityType(typeof(Paquete))]
		Paquete,
		
		///<summary>
		/// Composite Property for <c>Transporte</c> at BusIdSource
		///</summary>
		[ChildEntityType(typeof(Transporte))]
		Transporte,
		///<summary>
		/// Collection of <c>Viaje</c> as OneToMany for PasajeCollection
		///</summary>
		[ChildEntityType(typeof(TList<Pasaje>))]
		PasajeCollection,
		///<summary>
		/// Collection of <c>Viaje</c> as OneToMany for ViajeHotelCollection
		///</summary>
		[ChildEntityType(typeof(TList<ViajeHotel>))]
		ViajeHotelCollection,
	}
	
	#endregion ViajeChildEntityTypes
	
	#region ViajeFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ViajeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Viaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeFilterBuilder : SqlFilterBuilder<ViajeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeFilterBuilder class.
		/// </summary>
		public ViajeFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ViajeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ViajeFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ViajeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ViajeFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ViajeFilterBuilder
	
	#region ViajeParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ViajeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Viaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeParameterBuilder : ParameterizedSqlFilterBuilder<ViajeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeParameterBuilder class.
		/// </summary>
		public ViajeParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ViajeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ViajeParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ViajeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ViajeParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ViajeParameterBuilder
	
	#region ViajeSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ViajeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Viaje"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ViajeSortBuilder : SqlSortBuilder<ViajeColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeSqlSortBuilder class.
		/// </summary>
		public ViajeSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ViajeSortBuilder
	
} // end namespace
