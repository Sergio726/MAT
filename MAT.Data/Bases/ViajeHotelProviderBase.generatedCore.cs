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
	/// This class is the base class for any <see cref="ViajeHotelProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ViajeHotelProviderBaseCore : EntityProviderBase<MAT.Entities.ViajeHotel, MAT.Entities.ViajeHotelKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.ViajeHotelKey key)
		{
			return Delete(transactionManager, key.ViajeHotelId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_viajeHotelId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _viajeHotelId)
		{
			return Delete(null, _viajeHotelId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeHotelId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _viajeHotelId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Hotel key.
		///		FK_ViajeHotel_Hotel Description: 
		/// </summary>
		/// <param name="_hotelId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		public TList<ViajeHotel> GetByHotelId(System.Guid _hotelId)
		{
			int count = -1;
			return GetByHotelId(_hotelId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Hotel key.
		///		FK_ViajeHotel_Hotel Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		/// <remarks></remarks>
		public TList<ViajeHotel> GetByHotelId(TransactionManager transactionManager, System.Guid _hotelId)
		{
			int count = -1;
			return GetByHotelId(transactionManager, _hotelId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Hotel key.
		///		FK_ViajeHotel_Hotel Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		public TList<ViajeHotel> GetByHotelId(TransactionManager transactionManager, System.Guid _hotelId, int start, int pageLength)
		{
			int count = -1;
			return GetByHotelId(transactionManager, _hotelId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Hotel key.
		///		fkViajeHotelHotel Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_hotelId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		public TList<ViajeHotel> GetByHotelId(System.Guid _hotelId, int start, int pageLength)
		{
			int count =  -1;
			return GetByHotelId(null, _hotelId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Hotel key.
		///		fkViajeHotelHotel Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_hotelId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		public TList<ViajeHotel> GetByHotelId(System.Guid _hotelId, int start, int pageLength,out int count)
		{
			return GetByHotelId(null, _hotelId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Hotel key.
		///		FK_ViajeHotel_Hotel Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		public abstract TList<ViajeHotel> GetByHotelId(TransactionManager transactionManager, System.Guid _hotelId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Viaje key.
		///		FK_ViajeHotel_Viaje Description: 
		/// </summary>
		/// <param name="_viajeId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		public TList<ViajeHotel> GetByViajeId(System.Guid _viajeId)
		{
			int count = -1;
			return GetByViajeId(_viajeId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Viaje key.
		///		FK_ViajeHotel_Viaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		/// <remarks></remarks>
		public TList<ViajeHotel> GetByViajeId(TransactionManager transactionManager, System.Guid _viajeId)
		{
			int count = -1;
			return GetByViajeId(transactionManager, _viajeId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Viaje key.
		///		FK_ViajeHotel_Viaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		public TList<ViajeHotel> GetByViajeId(TransactionManager transactionManager, System.Guid _viajeId, int start, int pageLength)
		{
			int count = -1;
			return GetByViajeId(transactionManager, _viajeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Viaje key.
		///		fkViajeHotelViaje Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_viajeId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		public TList<ViajeHotel> GetByViajeId(System.Guid _viajeId, int start, int pageLength)
		{
			int count =  -1;
			return GetByViajeId(null, _viajeId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Viaje key.
		///		fkViajeHotelViaje Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_viajeId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		public TList<ViajeHotel> GetByViajeId(System.Guid _viajeId, int start, int pageLength,out int count)
		{
			return GetByViajeId(null, _viajeId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ViajeHotel_Viaje key.
		///		FK_ViajeHotel_Viaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.ViajeHotel objects.</returns>
		public abstract TList<ViajeHotel> GetByViajeId(TransactionManager transactionManager, System.Guid _viajeId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.ViajeHotel Get(TransactionManager transactionManager, MAT.Entities.ViajeHotelKey key, int start, int pageLength)
		{
			return GetByViajeHotelId(transactionManager, key.ViajeHotelId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_ViajeHotel index.
		/// </summary>
		/// <param name="_viajeHotelId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ViajeHotel"/> class.</returns>
		public MAT.Entities.ViajeHotel GetByViajeHotelId(System.Guid _viajeHotelId)
		{
			int count = -1;
			return GetByViajeHotelId(null,_viajeHotelId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ViajeHotel index.
		/// </summary>
		/// <param name="_viajeHotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ViajeHotel"/> class.</returns>
		public MAT.Entities.ViajeHotel GetByViajeHotelId(System.Guid _viajeHotelId, int start, int pageLength)
		{
			int count = -1;
			return GetByViajeHotelId(null, _viajeHotelId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ViajeHotel index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeHotelId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ViajeHotel"/> class.</returns>
		public MAT.Entities.ViajeHotel GetByViajeHotelId(TransactionManager transactionManager, System.Guid _viajeHotelId)
		{
			int count = -1;
			return GetByViajeHotelId(transactionManager, _viajeHotelId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ViajeHotel index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeHotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ViajeHotel"/> class.</returns>
		public MAT.Entities.ViajeHotel GetByViajeHotelId(TransactionManager transactionManager, System.Guid _viajeHotelId, int start, int pageLength)
		{
			int count = -1;
			return GetByViajeHotelId(transactionManager, _viajeHotelId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ViajeHotel index.
		/// </summary>
		/// <param name="_viajeHotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ViajeHotel"/> class.</returns>
		public MAT.Entities.ViajeHotel GetByViajeHotelId(System.Guid _viajeHotelId, int start, int pageLength, out int count)
		{
			return GetByViajeHotelId(null, _viajeHotelId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ViajeHotel index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeHotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ViajeHotel"/> class.</returns>
		public abstract MAT.Entities.ViajeHotel GetByViajeHotelId(TransactionManager transactionManager, System.Guid _viajeHotelId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;ViajeHotel&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;ViajeHotel&gt;"/></returns>
		public static TList<ViajeHotel> Fill(IDataReader reader, TList<ViajeHotel> rows, int start, int pageLength)
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
				
				MAT.Entities.ViajeHotel c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("ViajeHotel")
					.Append("|").Append((System.Guid)reader[((int)ViajeHotelColumn.ViajeHotelId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<ViajeHotel>(
					key.ToString(), // EntityTrackingKey
					"ViajeHotel",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.ViajeHotel();
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
					c.ViajeHotelId = (System.Guid)reader[((int)ViajeHotelColumn.ViajeHotelId - 1)];
					c.OriginalViajeHotelId = c.ViajeHotelId;
					c.ViajeId = (System.Guid)reader[((int)ViajeHotelColumn.ViajeId - 1)];
					c.HotelId = (System.Guid)reader[((int)ViajeHotelColumn.HotelId - 1)];
					c.Desde = (reader.IsDBNull(((int)ViajeHotelColumn.Desde - 1)))?null:(System.String)reader[((int)ViajeHotelColumn.Desde - 1)];
					c.Hasta = (reader.IsDBNull(((int)ViajeHotelColumn.Hasta - 1)))?null:(System.String)reader[((int)ViajeHotelColumn.Hasta - 1)];
					c.HoraIngreso = (reader.IsDBNull(((int)ViajeHotelColumn.HoraIngreso - 1)))?null:(System.String)reader[((int)ViajeHotelColumn.HoraIngreso - 1)];
					c.HoraSalida = (reader.IsDBNull(((int)ViajeHotelColumn.HoraSalida - 1)))?null:(System.String)reader[((int)ViajeHotelColumn.HoraSalida - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.ViajeHotel"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.ViajeHotel"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.ViajeHotel entity)
		{
			if (!reader.Read()) return;
			
			entity.ViajeHotelId = (System.Guid)reader[((int)ViajeHotelColumn.ViajeHotelId - 1)];
			entity.OriginalViajeHotelId = (System.Guid)reader["ViajeHotelID"];
			entity.ViajeId = (System.Guid)reader[((int)ViajeHotelColumn.ViajeId - 1)];
			entity.HotelId = (System.Guid)reader[((int)ViajeHotelColumn.HotelId - 1)];
			entity.Desde = (reader.IsDBNull(((int)ViajeHotelColumn.Desde - 1)))?null:(System.String)reader[((int)ViajeHotelColumn.Desde - 1)];
			entity.Hasta = (reader.IsDBNull(((int)ViajeHotelColumn.Hasta - 1)))?null:(System.String)reader[((int)ViajeHotelColumn.Hasta - 1)];
			entity.HoraIngreso = (reader.IsDBNull(((int)ViajeHotelColumn.HoraIngreso - 1)))?null:(System.String)reader[((int)ViajeHotelColumn.HoraIngreso - 1)];
			entity.HoraSalida = (reader.IsDBNull(((int)ViajeHotelColumn.HoraSalida - 1)))?null:(System.String)reader[((int)ViajeHotelColumn.HoraSalida - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.ViajeHotel"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.ViajeHotel"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.ViajeHotel entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ViajeHotelId = (System.Guid)dataRow["ViajeHotelID"];
			entity.OriginalViajeHotelId = (System.Guid)dataRow["ViajeHotelID"];
			entity.ViajeId = (System.Guid)dataRow["ViajeID"];
			entity.HotelId = (System.Guid)dataRow["HotelID"];
			entity.Desde = Convert.IsDBNull(dataRow["Desde"]) ? null : (System.String)dataRow["Desde"];
			entity.Hasta = Convert.IsDBNull(dataRow["Hasta"]) ? null : (System.String)dataRow["Hasta"];
			entity.HoraIngreso = Convert.IsDBNull(dataRow["HoraIngreso"]) ? null : (System.String)dataRow["HoraIngreso"];
			entity.HoraSalida = Convert.IsDBNull(dataRow["HoraSalida"]) ? null : (System.String)dataRow["HoraSalida"];
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
		/// <param name="entity">The <see cref="MAT.Entities.ViajeHotel"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.ViajeHotel Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.ViajeHotel entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region HotelIdSource	
			if (CanDeepLoad(entity, "Hotel|HotelIdSource", deepLoadType, innerList) 
				&& entity.HotelIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.HotelId;
				Hotel tmpEntity = EntityManager.LocateEntity<Hotel>(EntityLocator.ConstructKeyFromPkItems(typeof(Hotel), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.HotelIdSource = tmpEntity;
				else
					entity.HotelIdSource = DataRepository.HotelProvider.GetByHotelId(transactionManager, entity.HotelId);		
				
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

			#region ViajeIdSource	
			if (CanDeepLoad(entity, "Viaje|ViajeIdSource", deepLoadType, innerList) 
				&& entity.ViajeIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.ViajeId;
				Viaje tmpEntity = EntityManager.LocateEntity<Viaje>(EntityLocator.ConstructKeyFromPkItems(typeof(Viaje), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ViajeIdSource = tmpEntity;
				else
					entity.ViajeIdSource = DataRepository.ViajeProvider.GetByViajeId(transactionManager, entity.ViajeId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ViajeIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ViajeIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ViajeProvider.DeepLoad(transactionManager, entity.ViajeIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ViajeIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			
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
		/// Deep Save the entire object graph of the MAT.Entities.ViajeHotel object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.ViajeHotel instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.ViajeHotel Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.ViajeHotel entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
			
			#region ViajeIdSource
			if (CanDeepSave(entity, "Viaje|ViajeIdSource", deepSaveType, innerList) 
				&& entity.ViajeIdSource != null)
			{
				DataRepository.ViajeProvider.Save(transactionManager, entity.ViajeIdSource);
				entity.ViajeId = entity.ViajeIdSource.ViajeId;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
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
	
	#region ViajeHotelChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.ViajeHotel</c>
	///</summary>
	public enum ViajeHotelChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Hotel</c> at HotelIdSource
		///</summary>
		[ChildEntityType(typeof(Hotel))]
		Hotel,
		
		///<summary>
		/// Composite Property for <c>Viaje</c> at ViajeIdSource
		///</summary>
		[ChildEntityType(typeof(Viaje))]
		Viaje,
	}
	
	#endregion ViajeHotelChildEntityTypes
	
	#region ViajeHotelFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ViajeHotelColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ViajeHotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeHotelFilterBuilder : SqlFilterBuilder<ViajeHotelColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeHotelFilterBuilder class.
		/// </summary>
		public ViajeHotelFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ViajeHotelFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ViajeHotelFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ViajeHotelFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ViajeHotelFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ViajeHotelFilterBuilder
	
	#region ViajeHotelParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ViajeHotelColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ViajeHotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeHotelParameterBuilder : ParameterizedSqlFilterBuilder<ViajeHotelColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeHotelParameterBuilder class.
		/// </summary>
		public ViajeHotelParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ViajeHotelParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ViajeHotelParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ViajeHotelParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ViajeHotelParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ViajeHotelParameterBuilder
	
	#region ViajeHotelSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ViajeHotelColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ViajeHotel"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ViajeHotelSortBuilder : SqlSortBuilder<ViajeHotelColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeHotelSqlSortBuilder class.
		/// </summary>
		public ViajeHotelSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ViajeHotelSortBuilder
	
} // end namespace
