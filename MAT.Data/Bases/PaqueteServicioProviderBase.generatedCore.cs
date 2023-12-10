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
	/// This class is the base class for any <see cref="PaqueteServicioProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PaqueteServicioProviderBaseCore : EntityProviderBase<MAT.Entities.PaqueteServicio, MAT.Entities.PaqueteServicioKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PaqueteServicioKey key)
		{
			return Delete(transactionManager, key.PaqueteServicioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_paqueteServicioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _paqueteServicioId)
		{
			return Delete(null, _paqueteServicioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteServicioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _paqueteServicioId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Paquete key.
		///		FK_PaqueteServicio_Paquete Description: 
		/// </summary>
		/// <param name="_paqueteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		public TList<PaqueteServicio> GetByPaqueteId(System.Guid? _paqueteId)
		{
			int count = -1;
			return GetByPaqueteId(_paqueteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Paquete key.
		///		FK_PaqueteServicio_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		/// <remarks></remarks>
		public TList<PaqueteServicio> GetByPaqueteId(TransactionManager transactionManager, System.Guid? _paqueteId)
		{
			int count = -1;
			return GetByPaqueteId(transactionManager, _paqueteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Paquete key.
		///		FK_PaqueteServicio_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		public TList<PaqueteServicio> GetByPaqueteId(TransactionManager transactionManager, System.Guid? _paqueteId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaqueteId(transactionManager, _paqueteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Paquete key.
		///		fkPaqueteServicioPaquete Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_paqueteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		public TList<PaqueteServicio> GetByPaqueteId(System.Guid? _paqueteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPaqueteId(null, _paqueteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Paquete key.
		///		fkPaqueteServicioPaquete Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_paqueteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		public TList<PaqueteServicio> GetByPaqueteId(System.Guid? _paqueteId, int start, int pageLength,out int count)
		{
			return GetByPaqueteId(null, _paqueteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Paquete key.
		///		FK_PaqueteServicio_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		public abstract TList<PaqueteServicio> GetByPaqueteId(TransactionManager transactionManager, System.Guid? _paqueteId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Servicio key.
		///		FK_PaqueteServicio_Servicio Description: 
		/// </summary>
		/// <param name="_servicioId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		public TList<PaqueteServicio> GetByServicioId(System.Guid? _servicioId)
		{
			int count = -1;
			return GetByServicioId(_servicioId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Servicio key.
		///		FK_PaqueteServicio_Servicio Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicioId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		/// <remarks></remarks>
		public TList<PaqueteServicio> GetByServicioId(TransactionManager transactionManager, System.Guid? _servicioId)
		{
			int count = -1;
			return GetByServicioId(transactionManager, _servicioId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Servicio key.
		///		FK_PaqueteServicio_Servicio Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		public TList<PaqueteServicio> GetByServicioId(TransactionManager transactionManager, System.Guid? _servicioId, int start, int pageLength)
		{
			int count = -1;
			return GetByServicioId(transactionManager, _servicioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Servicio key.
		///		fkPaqueteServicioServicio Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_servicioId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		public TList<PaqueteServicio> GetByServicioId(System.Guid? _servicioId, int start, int pageLength)
		{
			int count =  -1;
			return GetByServicioId(null, _servicioId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Servicio key.
		///		fkPaqueteServicioServicio Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_servicioId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		public TList<PaqueteServicio> GetByServicioId(System.Guid? _servicioId, int start, int pageLength,out int count)
		{
			return GetByServicioId(null, _servicioId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteServicio_Servicio key.
		///		FK_PaqueteServicio_Servicio Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteServicio objects.</returns>
		public abstract TList<PaqueteServicio> GetByServicioId(TransactionManager transactionManager, System.Guid? _servicioId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.PaqueteServicio Get(TransactionManager transactionManager, MAT.Entities.PaqueteServicioKey key, int start, int pageLength)
		{
			return GetByPaqueteServicioId(transactionManager, key.PaqueteServicioId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_PaqueteServicio index.
		/// </summary>
		/// <param name="_paqueteServicioId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteServicio"/> class.</returns>
		public MAT.Entities.PaqueteServicio GetByPaqueteServicioId(System.Guid _paqueteServicioId)
		{
			int count = -1;
			return GetByPaqueteServicioId(null,_paqueteServicioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaqueteServicio index.
		/// </summary>
		/// <param name="_paqueteServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteServicio"/> class.</returns>
		public MAT.Entities.PaqueteServicio GetByPaqueteServicioId(System.Guid _paqueteServicioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaqueteServicioId(null, _paqueteServicioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaqueteServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteServicioId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteServicio"/> class.</returns>
		public MAT.Entities.PaqueteServicio GetByPaqueteServicioId(TransactionManager transactionManager, System.Guid _paqueteServicioId)
		{
			int count = -1;
			return GetByPaqueteServicioId(transactionManager, _paqueteServicioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaqueteServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteServicio"/> class.</returns>
		public MAT.Entities.PaqueteServicio GetByPaqueteServicioId(TransactionManager transactionManager, System.Guid _paqueteServicioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaqueteServicioId(transactionManager, _paqueteServicioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaqueteServicio index.
		/// </summary>
		/// <param name="_paqueteServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteServicio"/> class.</returns>
		public MAT.Entities.PaqueteServicio GetByPaqueteServicioId(System.Guid _paqueteServicioId, int start, int pageLength, out int count)
		{
			return GetByPaqueteServicioId(null, _paqueteServicioId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaqueteServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteServicio"/> class.</returns>
		public abstract MAT.Entities.PaqueteServicio GetByPaqueteServicioId(TransactionManager transactionManager, System.Guid _paqueteServicioId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;PaqueteServicio&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;PaqueteServicio&gt;"/></returns>
		public static TList<PaqueteServicio> Fill(IDataReader reader, TList<PaqueteServicio> rows, int start, int pageLength)
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
				
				MAT.Entities.PaqueteServicio c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("PaqueteServicio")
					.Append("|").Append((System.Guid)reader[((int)PaqueteServicioColumn.PaqueteServicioId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<PaqueteServicio>(
					key.ToString(), // EntityTrackingKey
					"PaqueteServicio",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.PaqueteServicio();
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
					c.PaqueteServicioId = (System.Guid)reader[((int)PaqueteServicioColumn.PaqueteServicioId - 1)];
					c.OriginalPaqueteServicioId = c.PaqueteServicioId;
					c.ServicioId = (reader.IsDBNull(((int)PaqueteServicioColumn.ServicioId - 1)))?null:(System.Guid?)reader[((int)PaqueteServicioColumn.ServicioId - 1)];
					c.PaqueteId = (reader.IsDBNull(((int)PaqueteServicioColumn.PaqueteId - 1)))?null:(System.Guid?)reader[((int)PaqueteServicioColumn.PaqueteId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PaqueteServicio"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PaqueteServicio"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.PaqueteServicio entity)
		{
			if (!reader.Read()) return;
			
			entity.PaqueteServicioId = (System.Guid)reader[((int)PaqueteServicioColumn.PaqueteServicioId - 1)];
			entity.OriginalPaqueteServicioId = (System.Guid)reader["PaqueteServicioID"];
			entity.ServicioId = (reader.IsDBNull(((int)PaqueteServicioColumn.ServicioId - 1)))?null:(System.Guid?)reader[((int)PaqueteServicioColumn.ServicioId - 1)];
			entity.PaqueteId = (reader.IsDBNull(((int)PaqueteServicioColumn.PaqueteId - 1)))?null:(System.Guid?)reader[((int)PaqueteServicioColumn.PaqueteId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PaqueteServicio"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PaqueteServicio"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.PaqueteServicio entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PaqueteServicioId = (System.Guid)dataRow["PaqueteServicioID"];
			entity.OriginalPaqueteServicioId = (System.Guid)dataRow["PaqueteServicioID"];
			entity.ServicioId = Convert.IsDBNull(dataRow["ServicioID"]) ? null : (System.Guid?)dataRow["ServicioID"];
			entity.PaqueteId = Convert.IsDBNull(dataRow["PaqueteID"]) ? null : (System.Guid?)dataRow["PaqueteID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.PaqueteServicio"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.PaqueteServicio Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.PaqueteServicio entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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

			#region ServicioIdSource	
			if (CanDeepLoad(entity, "Servicio|ServicioIdSource", deepLoadType, innerList) 
				&& entity.ServicioIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.ServicioId ?? Guid.Empty);
				Servicio tmpEntity = EntityManager.LocateEntity<Servicio>(EntityLocator.ConstructKeyFromPkItems(typeof(Servicio), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ServicioIdSource = tmpEntity;
				else
					entity.ServicioIdSource = DataRepository.ServicioProvider.GetByServicioId(transactionManager, (entity.ServicioId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ServicioIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ServicioIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ServicioProvider.DeepLoad(transactionManager, entity.ServicioIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ServicioIdSource
			
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
		/// Deep Save the entire object graph of the MAT.Entities.PaqueteServicio object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.PaqueteServicio instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.PaqueteServicio Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.PaqueteServicio entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
			
			#region ServicioIdSource
			if (CanDeepSave(entity, "Servicio|ServicioIdSource", deepSaveType, innerList) 
				&& entity.ServicioIdSource != null)
			{
				DataRepository.ServicioProvider.Save(transactionManager, entity.ServicioIdSource);
				entity.ServicioId = entity.ServicioIdSource.ServicioId;
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
	
	#region PaqueteServicioChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.PaqueteServicio</c>
	///</summary>
	public enum PaqueteServicioChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Paquete</c> at PaqueteIdSource
		///</summary>
		[ChildEntityType(typeof(Paquete))]
		Paquete,
		
		///<summary>
		/// Composite Property for <c>Servicio</c> at ServicioIdSource
		///</summary>
		[ChildEntityType(typeof(Servicio))]
		Servicio,
	}
	
	#endregion PaqueteServicioChildEntityTypes
	
	#region PaqueteServicioFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PaqueteServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteServicioFilterBuilder : SqlFilterBuilder<PaqueteServicioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioFilterBuilder class.
		/// </summary>
		public PaqueteServicioFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteServicioFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteServicioFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteServicioFilterBuilder
	
	#region PaqueteServicioParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PaqueteServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteServicioParameterBuilder : ParameterizedSqlFilterBuilder<PaqueteServicioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioParameterBuilder class.
		/// </summary>
		public PaqueteServicioParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteServicioParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteServicioParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteServicioParameterBuilder
	
	#region PaqueteServicioSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PaqueteServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteServicio"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PaqueteServicioSortBuilder : SqlSortBuilder<PaqueteServicioColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioSqlSortBuilder class.
		/// </summary>
		public PaqueteServicioSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PaqueteServicioSortBuilder
	
} // end namespace
