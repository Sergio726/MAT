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
	/// This class is the base class for any <see cref="PaqueteExcursionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PaqueteExcursionProviderBaseCore : EntityProviderBase<MAT.Entities.PaqueteExcursion, MAT.Entities.PaqueteExcursionKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PaqueteExcursionKey key)
		{
			return Delete(transactionManager, key.PaqueteExcursionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_paqueteExcursionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _paqueteExcursionId)
		{
			return Delete(null, _paqueteExcursionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteExcursionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _paqueteExcursionId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Excursion key.
		///		FK_PaqueteExcursion_Excursion Description: 
		/// </summary>
		/// <param name="_excursionId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		public TList<PaqueteExcursion> GetByExcursionId(System.Guid _excursionId)
		{
			int count = -1;
			return GetByExcursionId(_excursionId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Excursion key.
		///		FK_PaqueteExcursion_Excursion Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_excursionId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		/// <remarks></remarks>
		public TList<PaqueteExcursion> GetByExcursionId(TransactionManager transactionManager, System.Guid _excursionId)
		{
			int count = -1;
			return GetByExcursionId(transactionManager, _excursionId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Excursion key.
		///		FK_PaqueteExcursion_Excursion Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_excursionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		public TList<PaqueteExcursion> GetByExcursionId(TransactionManager transactionManager, System.Guid _excursionId, int start, int pageLength)
		{
			int count = -1;
			return GetByExcursionId(transactionManager, _excursionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Excursion key.
		///		fkPaqueteExcursionExcursion Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_excursionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		public TList<PaqueteExcursion> GetByExcursionId(System.Guid _excursionId, int start, int pageLength)
		{
			int count =  -1;
			return GetByExcursionId(null, _excursionId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Excursion key.
		///		fkPaqueteExcursionExcursion Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_excursionId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		public TList<PaqueteExcursion> GetByExcursionId(System.Guid _excursionId, int start, int pageLength,out int count)
		{
			return GetByExcursionId(null, _excursionId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Excursion key.
		///		FK_PaqueteExcursion_Excursion Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_excursionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		public abstract TList<PaqueteExcursion> GetByExcursionId(TransactionManager transactionManager, System.Guid _excursionId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Paquete key.
		///		FK_PaqueteExcursion_Paquete Description: 
		/// </summary>
		/// <param name="_paqueteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		public TList<PaqueteExcursion> GetByPaqueteId(System.Guid _paqueteId)
		{
			int count = -1;
			return GetByPaqueteId(_paqueteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Paquete key.
		///		FK_PaqueteExcursion_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		/// <remarks></remarks>
		public TList<PaqueteExcursion> GetByPaqueteId(TransactionManager transactionManager, System.Guid _paqueteId)
		{
			int count = -1;
			return GetByPaqueteId(transactionManager, _paqueteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Paquete key.
		///		FK_PaqueteExcursion_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		public TList<PaqueteExcursion> GetByPaqueteId(TransactionManager transactionManager, System.Guid _paqueteId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaqueteId(transactionManager, _paqueteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Paquete key.
		///		fkPaqueteExcursionPaquete Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_paqueteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		public TList<PaqueteExcursion> GetByPaqueteId(System.Guid _paqueteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPaqueteId(null, _paqueteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Paquete key.
		///		fkPaqueteExcursionPaquete Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_paqueteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		public TList<PaqueteExcursion> GetByPaqueteId(System.Guid _paqueteId, int start, int pageLength,out int count)
		{
			return GetByPaqueteId(null, _paqueteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PaqueteExcursion_Paquete key.
		///		FK_PaqueteExcursion_Paquete Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PaqueteExcursion objects.</returns>
		public abstract TList<PaqueteExcursion> GetByPaqueteId(TransactionManager transactionManager, System.Guid _paqueteId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.PaqueteExcursion Get(TransactionManager transactionManager, MAT.Entities.PaqueteExcursionKey key, int start, int pageLength)
		{
			return GetByPaqueteExcursionId(transactionManager, key.PaqueteExcursionId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_PaqueteExcursion index.
		/// </summary>
		/// <param name="_paqueteExcursionId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteExcursion"/> class.</returns>
		public MAT.Entities.PaqueteExcursion GetByPaqueteExcursionId(System.Guid _paqueteExcursionId)
		{
			int count = -1;
			return GetByPaqueteExcursionId(null,_paqueteExcursionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaqueteExcursion index.
		/// </summary>
		/// <param name="_paqueteExcursionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteExcursion"/> class.</returns>
		public MAT.Entities.PaqueteExcursion GetByPaqueteExcursionId(System.Guid _paqueteExcursionId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaqueteExcursionId(null, _paqueteExcursionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaqueteExcursion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteExcursionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteExcursion"/> class.</returns>
		public MAT.Entities.PaqueteExcursion GetByPaqueteExcursionId(TransactionManager transactionManager, System.Guid _paqueteExcursionId)
		{
			int count = -1;
			return GetByPaqueteExcursionId(transactionManager, _paqueteExcursionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaqueteExcursion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteExcursionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteExcursion"/> class.</returns>
		public MAT.Entities.PaqueteExcursion GetByPaqueteExcursionId(TransactionManager transactionManager, System.Guid _paqueteExcursionId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaqueteExcursionId(transactionManager, _paqueteExcursionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaqueteExcursion index.
		/// </summary>
		/// <param name="_paqueteExcursionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteExcursion"/> class.</returns>
		public MAT.Entities.PaqueteExcursion GetByPaqueteExcursionId(System.Guid _paqueteExcursionId, int start, int pageLength, out int count)
		{
			return GetByPaqueteExcursionId(null, _paqueteExcursionId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PaqueteExcursion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteExcursionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PaqueteExcursion"/> class.</returns>
		public abstract MAT.Entities.PaqueteExcursion GetByPaqueteExcursionId(TransactionManager transactionManager, System.Guid _paqueteExcursionId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;PaqueteExcursion&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;PaqueteExcursion&gt;"/></returns>
		public static TList<PaqueteExcursion> Fill(IDataReader reader, TList<PaqueteExcursion> rows, int start, int pageLength)
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
				
				MAT.Entities.PaqueteExcursion c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("PaqueteExcursion")
					.Append("|").Append((System.Guid)reader[((int)PaqueteExcursionColumn.PaqueteExcursionId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<PaqueteExcursion>(
					key.ToString(), // EntityTrackingKey
					"PaqueteExcursion",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.PaqueteExcursion();
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
					c.PaqueteExcursionId = (System.Guid)reader[((int)PaqueteExcursionColumn.PaqueteExcursionId - 1)];
					c.OriginalPaqueteExcursionId = c.PaqueteExcursionId;
					c.ExcursionId = (System.Guid)reader[((int)PaqueteExcursionColumn.ExcursionId - 1)];
					c.PaqueteId = (System.Guid)reader[((int)PaqueteExcursionColumn.PaqueteId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PaqueteExcursion"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PaqueteExcursion"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.PaqueteExcursion entity)
		{
			if (!reader.Read()) return;
			
			entity.PaqueteExcursionId = (System.Guid)reader[((int)PaqueteExcursionColumn.PaqueteExcursionId - 1)];
			entity.OriginalPaqueteExcursionId = (System.Guid)reader["PaqueteExcursionID"];
			entity.ExcursionId = (System.Guid)reader[((int)PaqueteExcursionColumn.ExcursionId - 1)];
			entity.PaqueteId = (System.Guid)reader[((int)PaqueteExcursionColumn.PaqueteId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PaqueteExcursion"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PaqueteExcursion"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.PaqueteExcursion entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PaqueteExcursionId = (System.Guid)dataRow["PaqueteExcursionID"];
			entity.OriginalPaqueteExcursionId = (System.Guid)dataRow["PaqueteExcursionID"];
			entity.ExcursionId = (System.Guid)dataRow["ExcursionID"];
			entity.PaqueteId = (System.Guid)dataRow["PaqueteID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.PaqueteExcursion"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.PaqueteExcursion Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.PaqueteExcursion entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ExcursionIdSource	
			if (CanDeepLoad(entity, "Excursion|ExcursionIdSource", deepLoadType, innerList) 
				&& entity.ExcursionIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.ExcursionId;
				Excursion tmpEntity = EntityManager.LocateEntity<Excursion>(EntityLocator.ConstructKeyFromPkItems(typeof(Excursion), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ExcursionIdSource = tmpEntity;
				else
					entity.ExcursionIdSource = DataRepository.ExcursionProvider.GetByExcursionId(transactionManager, entity.ExcursionId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ExcursionIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ExcursionIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ExcursionProvider.DeepLoad(transactionManager, entity.ExcursionIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ExcursionIdSource

			#region PaqueteIdSource	
			if (CanDeepLoad(entity, "Paquete|PaqueteIdSource", deepLoadType, innerList) 
				&& entity.PaqueteIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.PaqueteId;
				Paquete tmpEntity = EntityManager.LocateEntity<Paquete>(EntityLocator.ConstructKeyFromPkItems(typeof(Paquete), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PaqueteIdSource = tmpEntity;
				else
					entity.PaqueteIdSource = DataRepository.PaqueteProvider.GetByPaqueteId(transactionManager, entity.PaqueteId);		
				
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
		/// Deep Save the entire object graph of the MAT.Entities.PaqueteExcursion object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.PaqueteExcursion instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.PaqueteExcursion Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.PaqueteExcursion entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ExcursionIdSource
			if (CanDeepSave(entity, "Excursion|ExcursionIdSource", deepSaveType, innerList) 
				&& entity.ExcursionIdSource != null)
			{
				DataRepository.ExcursionProvider.Save(transactionManager, entity.ExcursionIdSource);
				entity.ExcursionId = entity.ExcursionIdSource.ExcursionId;
			}
			#endregion 
			
			#region PaqueteIdSource
			if (CanDeepSave(entity, "Paquete|PaqueteIdSource", deepSaveType, innerList) 
				&& entity.PaqueteIdSource != null)
			{
				DataRepository.PaqueteProvider.Save(transactionManager, entity.PaqueteIdSource);
				entity.PaqueteId = entity.PaqueteIdSource.PaqueteId;
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
	
	#region PaqueteExcursionChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.PaqueteExcursion</c>
	///</summary>
	public enum PaqueteExcursionChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Excursion</c> at ExcursionIdSource
		///</summary>
		[ChildEntityType(typeof(Excursion))]
		Excursion,
		
		///<summary>
		/// Composite Property for <c>Paquete</c> at PaqueteIdSource
		///</summary>
		[ChildEntityType(typeof(Paquete))]
		Paquete,
	}
	
	#endregion PaqueteExcursionChildEntityTypes
	
	#region PaqueteExcursionFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PaqueteExcursionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteExcursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteExcursionFilterBuilder : SqlFilterBuilder<PaqueteExcursionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionFilterBuilder class.
		/// </summary>
		public PaqueteExcursionFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteExcursionFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteExcursionFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteExcursionFilterBuilder
	
	#region PaqueteExcursionParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PaqueteExcursionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteExcursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteExcursionParameterBuilder : ParameterizedSqlFilterBuilder<PaqueteExcursionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionParameterBuilder class.
		/// </summary>
		public PaqueteExcursionParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteExcursionParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteExcursionParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteExcursionParameterBuilder
	
	#region PaqueteExcursionSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PaqueteExcursionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteExcursion"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PaqueteExcursionSortBuilder : SqlSortBuilder<PaqueteExcursionColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionSqlSortBuilder class.
		/// </summary>
		public PaqueteExcursionSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PaqueteExcursionSortBuilder
	
} // end namespace
