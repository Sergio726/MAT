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
	/// This class is the base class for any <see cref="PaquetePrecioProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PaquetePrecioProviderBaseCore : EntityProviderBase<MAT.Entities.PaquetePrecio, MAT.Entities.PaquetePrecioKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PaquetePrecioKey key)
		{
			return Delete(transactionManager, key.PaquetePrecioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_paquetePrecioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _paquetePrecioId)
		{
			return Delete(null, _paquetePrecioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paquetePrecioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _paquetePrecioId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Paquete key.
		///		FK_PaquetePrecio_Paquete Description: 
		/// </summary>
		/// <param name="_paqueteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		public TList<PaquetePrecio> GetByPaqueteId(System.Guid? _paqueteId)
		{
			int count = -1;
			return GetByPaqueteId(_paqueteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Paquete key.
		///		FK_PaquetePrecio_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		/// <remarks></remarks>
		public TList<PaquetePrecio> GetByPaqueteId(TransactionManager transactionManager, System.Guid? _paqueteId)
		{
			int count = -1;
			return GetByPaqueteId(transactionManager, _paqueteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Paquete key.
		///		FK_PaquetePrecio_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		public TList<PaquetePrecio> GetByPaqueteId(TransactionManager transactionManager, System.Guid? _paqueteId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaqueteId(transactionManager, _paqueteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Paquete key.
		///		fkPaquetePrecioPaquete Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_paqueteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		public TList<PaquetePrecio> GetByPaqueteId(System.Guid? _paqueteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPaqueteId(null, _paqueteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Paquete key.
		///		fkPaquetePrecioPaquete Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_paqueteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		public TList<PaquetePrecio> GetByPaqueteId(System.Guid? _paqueteId, int start, int pageLength,out int count)
		{
			return GetByPaqueteId(null, _paqueteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Paquete key.
		///		FK_PaquetePrecio_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		public abstract TList<PaquetePrecio> GetByPaqueteId(TransactionManager transactionManager, System.Guid? _paqueteId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Precio key.
		///		FK_PaquetePrecio_Precio Description: 
		/// </summary>
		/// <param name="_precioId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		public TList<PaquetePrecio> GetByPrecioId(System.Guid? _precioId)
		{
			int count = -1;
			return GetByPrecioId(_precioId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Precio key.
		///		FK_PaquetePrecio_Precio Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		/// <remarks></remarks>
		public TList<PaquetePrecio> GetByPrecioId(TransactionManager transactionManager, System.Guid? _precioId)
		{
			int count = -1;
			return GetByPrecioId(transactionManager, _precioId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Precio key.
		///		FK_PaquetePrecio_Precio Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		public TList<PaquetePrecio> GetByPrecioId(TransactionManager transactionManager, System.Guid? _precioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPrecioId(transactionManager, _precioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Precio key.
		///		fkPaquetePrecioPrecio Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_precioId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		public TList<PaquetePrecio> GetByPrecioId(System.Guid? _precioId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPrecioId(null, _precioId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Precio key.
		///		fkPaquetePrecioPrecio Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_precioId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		public TList<PaquetePrecio> GetByPrecioId(System.Guid? _precioId, int start, int pageLength,out int count)
		{
			return GetByPrecioId(null, _precioId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaquetePrecio_Precio key.
		///		FK_PaquetePrecio_Precio Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PaquetePrecio objects.</returns>
		public abstract TList<PaquetePrecio> GetByPrecioId(TransactionManager transactionManager, System.Guid? _precioId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.PaquetePrecio Get(TransactionManager transactionManager, MAT.Entities.PaquetePrecioKey key, int start, int pageLength)
		{
			return GetByPaquetePrecioId(transactionManager, key.PaquetePrecioId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_PaquetePrecio index.
		/// </summary>
		/// <param name="_paquetePrecioId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaquetePrecio"/> class.</returns>
		public MAT.Entities.PaquetePrecio GetByPaquetePrecioId(System.Guid _paquetePrecioId)
		{
			int count = -1;
			return GetByPaquetePrecioId(null,_paquetePrecioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaquetePrecio index.
		/// </summary>
		/// <param name="_paquetePrecioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaquetePrecio"/> class.</returns>
		public MAT.Entities.PaquetePrecio GetByPaquetePrecioId(System.Guid _paquetePrecioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaquetePrecioId(null, _paquetePrecioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaquetePrecio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paquetePrecioId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaquetePrecio"/> class.</returns>
		public MAT.Entities.PaquetePrecio GetByPaquetePrecioId(TransactionManager transactionManager, System.Guid _paquetePrecioId)
		{
			int count = -1;
			return GetByPaquetePrecioId(transactionManager, _paquetePrecioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaquetePrecio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paquetePrecioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaquetePrecio"/> class.</returns>
		public MAT.Entities.PaquetePrecio GetByPaquetePrecioId(TransactionManager transactionManager, System.Guid _paquetePrecioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaquetePrecioId(transactionManager, _paquetePrecioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaquetePrecio index.
		/// </summary>
		/// <param name="_paquetePrecioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaquetePrecio"/> class.</returns>
		public MAT.Entities.PaquetePrecio GetByPaquetePrecioId(System.Guid _paquetePrecioId, int start, int pageLength, out int count)
		{
			return GetByPaquetePrecioId(null, _paquetePrecioId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaquetePrecio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paquetePrecioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaquetePrecio"/> class.</returns>
		public abstract MAT.Entities.PaquetePrecio GetByPaquetePrecioId(TransactionManager transactionManager, System.Guid _paquetePrecioId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;PaquetePrecio&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;PaquetePrecio&gt;"/></returns>
		public static TList<PaquetePrecio> Fill(IDataReader reader, TList<PaquetePrecio> rows, int start, int pageLength)
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
				
				MAT.Entities.PaquetePrecio c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("PaquetePrecio")
					.Append("|").Append((System.Guid)reader[((int)PaquetePrecioColumn.PaquetePrecioId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<PaquetePrecio>(
					key.ToString(), // EntityTrackingKey
					"PaquetePrecio",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.PaquetePrecio();
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
					c.PaquetePrecioId = (System.Guid)reader[((int)PaquetePrecioColumn.PaquetePrecioId - 1)];
					c.OriginalPaquetePrecioId = c.PaquetePrecioId;
					c.PaqueteId = (reader.IsDBNull(((int)PaquetePrecioColumn.PaqueteId - 1)))?null:(System.Guid?)reader[((int)PaquetePrecioColumn.PaqueteId - 1)];
					c.PrecioId = (reader.IsDBNull(((int)PaquetePrecioColumn.PrecioId - 1)))?null:(System.Guid?)reader[((int)PaquetePrecioColumn.PrecioId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PaquetePrecio"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PaquetePrecio"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.PaquetePrecio entity)
		{
			if (!reader.Read()) return;
			
			entity.PaquetePrecioId = (System.Guid)reader[((int)PaquetePrecioColumn.PaquetePrecioId - 1)];
			entity.OriginalPaquetePrecioId = (System.Guid)reader["PaquetePrecioID"];
			entity.PaqueteId = (reader.IsDBNull(((int)PaquetePrecioColumn.PaqueteId - 1)))?null:(System.Guid?)reader[((int)PaquetePrecioColumn.PaqueteId - 1)];
			entity.PrecioId = (reader.IsDBNull(((int)PaquetePrecioColumn.PrecioId - 1)))?null:(System.Guid?)reader[((int)PaquetePrecioColumn.PrecioId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PaquetePrecio"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PaquetePrecio"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.PaquetePrecio entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PaquetePrecioId = (System.Guid)dataRow["PaquetePrecioID"];
			entity.OriginalPaquetePrecioId = (System.Guid)dataRow["PaquetePrecioID"];
			entity.PaqueteId = Convert.IsDBNull(dataRow["PaqueteID"]) ? null : (System.Guid?)dataRow["PaqueteID"];
			entity.PrecioId = Convert.IsDBNull(dataRow["PrecioID"]) ? null : (System.Guid?)dataRow["PrecioID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.PaquetePrecio"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.PaquetePrecio Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.PaquetePrecio entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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

			#region PrecioIdSource	
			if (CanDeepLoad(entity, "Precio|PrecioIdSource", deepLoadType, innerList) 
				&& entity.PrecioIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.PrecioId ?? Guid.Empty);
				Precio tmpEntity = EntityManager.LocateEntity<Precio>(EntityLocator.ConstructKeyFromPkItems(typeof(Precio), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PrecioIdSource = tmpEntity;
				else
					entity.PrecioIdSource = DataRepository.PrecioProvider.GetByPrecioId(transactionManager, (entity.PrecioId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PrecioIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PrecioIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PrecioProvider.DeepLoad(transactionManager, entity.PrecioIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PrecioIdSource
			
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
		/// Deep Save the entire object graph of the MAT.Entities.PaquetePrecio object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.PaquetePrecio instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.PaquetePrecio Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.PaquetePrecio entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
			
			#region PrecioIdSource
			if (CanDeepSave(entity, "Precio|PrecioIdSource", deepSaveType, innerList) 
				&& entity.PrecioIdSource != null)
			{
				DataRepository.PrecioProvider.Save(transactionManager, entity.PrecioIdSource);
				entity.PrecioId = entity.PrecioIdSource.PrecioId;
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
	
	#region PaquetePrecioChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.PaquetePrecio</c>
	///</summary>
	public enum PaquetePrecioChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Paquete</c> at PaqueteIdSource
		///</summary>
		[ChildEntityType(typeof(Paquete))]
		Paquete,
		
		///<summary>
		/// Composite Property for <c>Precio</c> at PrecioIdSource
		///</summary>
		[ChildEntityType(typeof(Precio))]
		Precio,
	}
	
	#endregion PaquetePrecioChildEntityTypes
	
	#region PaquetePrecioFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PaquetePrecioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaquetePrecio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaquetePrecioFilterBuilder : SqlFilterBuilder<PaquetePrecioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioFilterBuilder class.
		/// </summary>
		public PaquetePrecioFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaquetePrecioFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaquetePrecioFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaquetePrecioFilterBuilder
	
	#region PaquetePrecioParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PaquetePrecioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaquetePrecio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaquetePrecioParameterBuilder : ParameterizedSqlFilterBuilder<PaquetePrecioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioParameterBuilder class.
		/// </summary>
		public PaquetePrecioParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaquetePrecioParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaquetePrecioParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaquetePrecioParameterBuilder
	
	#region PaquetePrecioSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PaquetePrecioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaquetePrecio"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PaquetePrecioSortBuilder : SqlSortBuilder<PaquetePrecioColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioSqlSortBuilder class.
		/// </summary>
		public PaquetePrecioSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PaquetePrecioSortBuilder
	
} // end namespace
