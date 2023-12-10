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
	/// This class is the base class for any <see cref="PasajeroMenorProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PasajeroMenorProviderBaseCore : EntityProviderBase<MAT.Entities.PasajeroMenor, MAT.Entities.PasajeroMenorKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PasajeroMenorKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _id)
		{
			return Delete(null, _id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_menor key.
		///		fk_cliente_menor Description: 
		/// </summary>
		/// <param name="_menorid"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		public TList<PasajeroMenor> GetByMenorid(System.Guid _menorid)
		{
			int count = -1;
			return GetByMenorid(_menorid, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_menor key.
		///		fk_cliente_menor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_menorid"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		/// <remarks></remarks>
		public TList<PasajeroMenor> GetByMenorid(TransactionManager transactionManager, System.Guid _menorid)
		{
			int count = -1;
			return GetByMenorid(transactionManager, _menorid, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_menor key.
		///		fk_cliente_menor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_menorid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		public TList<PasajeroMenor> GetByMenorid(TransactionManager transactionManager, System.Guid _menorid, int start, int pageLength)
		{
			int count = -1;
			return GetByMenorid(transactionManager, _menorid, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_menor key.
		///		fkClienteMenor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_menorid"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		public TList<PasajeroMenor> GetByMenorid(System.Guid _menorid, int start, int pageLength)
		{
			int count =  -1;
			return GetByMenorid(null, _menorid, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_menor key.
		///		fkClienteMenor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_menorid"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		public TList<PasajeroMenor> GetByMenorid(System.Guid _menorid, int start, int pageLength,out int count)
		{
			return GetByMenorid(null, _menorid, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_menor key.
		///		fk_cliente_menor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_menorid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		public abstract TList<PasajeroMenor> GetByMenorid(TransactionManager transactionManager, System.Guid _menorid, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_pasajero key.
		///		fk_cliente_pasajero Description: 
		/// </summary>
		/// <param name="_pasajeroid"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		public TList<PasajeroMenor> GetByPasajeroid(System.Guid _pasajeroid)
		{
			int count = -1;
			return GetByPasajeroid(_pasajeroid, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_pasajero key.
		///		fk_cliente_pasajero Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroid"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		/// <remarks></remarks>
		public TList<PasajeroMenor> GetByPasajeroid(TransactionManager transactionManager, System.Guid _pasajeroid)
		{
			int count = -1;
			return GetByPasajeroid(transactionManager, _pasajeroid, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_pasajero key.
		///		fk_cliente_pasajero Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		public TList<PasajeroMenor> GetByPasajeroid(TransactionManager transactionManager, System.Guid _pasajeroid, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeroid(transactionManager, _pasajeroid, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_pasajero key.
		///		fkClientePasajero Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pasajeroid"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		public TList<PasajeroMenor> GetByPasajeroid(System.Guid _pasajeroid, int start, int pageLength)
		{
			int count =  -1;
			return GetByPasajeroid(null, _pasajeroid, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_pasajero key.
		///		fkClientePasajero Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pasajeroid"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		public TList<PasajeroMenor> GetByPasajeroid(System.Guid _pasajeroid, int start, int pageLength,out int count)
		{
			return GetByPasajeroid(null, _pasajeroid, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the fk_cliente_pasajero key.
		///		fk_cliente_pasajero Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeroMenor objects.</returns>
		public abstract TList<PasajeroMenor> GetByPasajeroid(TransactionManager transactionManager, System.Guid _pasajeroid, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.PasajeroMenor Get(TransactionManager transactionManager, MAT.Entities.PasajeroMenorKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key pk_pasajeromenor index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeroMenor"/> class.</returns>
		public MAT.Entities.PasajeroMenor GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the pk_pasajeromenor index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeroMenor"/> class.</returns>
		public MAT.Entities.PasajeroMenor GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the pk_pasajeromenor index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeroMenor"/> class.</returns>
		public MAT.Entities.PasajeroMenor GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the pk_pasajeromenor index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeroMenor"/> class.</returns>
		public MAT.Entities.PasajeroMenor GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the pk_pasajeromenor index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeroMenor"/> class.</returns>
		public MAT.Entities.PasajeroMenor GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the pk_pasajeromenor index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeroMenor"/> class.</returns>
		public abstract MAT.Entities.PasajeroMenor GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;PasajeroMenor&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;PasajeroMenor&gt;"/></returns>
		public static TList<PasajeroMenor> Fill(IDataReader reader, TList<PasajeroMenor> rows, int start, int pageLength)
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
				
				MAT.Entities.PasajeroMenor c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("PasajeroMenor")
					.Append("|").Append((System.Int32)reader[((int)PasajeroMenorColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<PasajeroMenor>(
					key.ToString(), // EntityTrackingKey
					"PasajeroMenor",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.PasajeroMenor();
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
					c.Id = (System.Int32)reader[((int)PasajeroMenorColumn.Id - 1)];
					c.Pasajeid = (System.Guid)reader[((int)PasajeroMenorColumn.Pasajeid - 1)];
					c.Pasajeroid = (System.Guid)reader[((int)PasajeroMenorColumn.Pasajeroid - 1)];
					c.Menorid = (System.Guid)reader[((int)PasajeroMenorColumn.Menorid - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PasajeroMenor"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PasajeroMenor"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.PasajeroMenor entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)PasajeroMenorColumn.Id - 1)];
			entity.Pasajeid = (System.Guid)reader[((int)PasajeroMenorColumn.Pasajeid - 1)];
			entity.Pasajeroid = (System.Guid)reader[((int)PasajeroMenorColumn.Pasajeroid - 1)];
			entity.Menorid = (System.Guid)reader[((int)PasajeroMenorColumn.Menorid - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PasajeroMenor"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PasajeroMenor"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.PasajeroMenor entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["id"];
			entity.Pasajeid = (System.Guid)dataRow["pasajeid"];
			entity.Pasajeroid = (System.Guid)dataRow["pasajeroid"];
			entity.Menorid = (System.Guid)dataRow["menorid"];
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
		/// <param name="entity">The <see cref="MAT.Entities.PasajeroMenor"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.PasajeroMenor Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.PasajeroMenor entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region MenoridSource	
			if (CanDeepLoad(entity, "Cliente|MenoridSource", deepLoadType, innerList) 
				&& entity.MenoridSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.Menorid;
				Cliente tmpEntity = EntityManager.LocateEntity<Cliente>(EntityLocator.ConstructKeyFromPkItems(typeof(Cliente), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.MenoridSource = tmpEntity;
				else
					entity.MenoridSource = DataRepository.ClienteProvider.GetByClienteId(transactionManager, entity.Menorid);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MenoridSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.MenoridSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ClienteProvider.DeepLoad(transactionManager, entity.MenoridSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion MenoridSource

			#region PasajeroidSource	
			if (CanDeepLoad(entity, "Cliente|PasajeroidSource", deepLoadType, innerList) 
				&& entity.PasajeroidSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.Pasajeroid;
				Cliente tmpEntity = EntityManager.LocateEntity<Cliente>(EntityLocator.ConstructKeyFromPkItems(typeof(Cliente), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PasajeroidSource = tmpEntity;
				else
					entity.PasajeroidSource = DataRepository.ClienteProvider.GetByClienteId(transactionManager, entity.Pasajeroid);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeroidSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PasajeroidSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ClienteProvider.DeepLoad(transactionManager, entity.PasajeroidSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PasajeroidSource
			
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
		/// Deep Save the entire object graph of the MAT.Entities.PasajeroMenor object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.PasajeroMenor instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.PasajeroMenor Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.PasajeroMenor entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region MenoridSource
			if (CanDeepSave(entity, "Cliente|MenoridSource", deepSaveType, innerList) 
				&& entity.MenoridSource != null)
			{
				DataRepository.ClienteProvider.Save(transactionManager, entity.MenoridSource);
				entity.Menorid = entity.MenoridSource.ClienteId;
			}
			#endregion 
			
			#region PasajeroidSource
			if (CanDeepSave(entity, "Cliente|PasajeroidSource", deepSaveType, innerList) 
				&& entity.PasajeroidSource != null)
			{
				DataRepository.ClienteProvider.Save(transactionManager, entity.PasajeroidSource);
				entity.Pasajeroid = entity.PasajeroidSource.ClienteId;
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
	
	#region PasajeroMenorChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.PasajeroMenor</c>
	///</summary>
	public enum PasajeroMenorChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Cliente</c> at MenoridSource
		///</summary>
		[ChildEntityType(typeof(Cliente))]
		Cliente,
	}
	
	#endregion PasajeroMenorChildEntityTypes
	
	#region PasajeroMenorFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PasajeroMenorColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroMenor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroMenorFilterBuilder : SqlFilterBuilder<PasajeroMenorColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorFilterBuilder class.
		/// </summary>
		public PasajeroMenorFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroMenorFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroMenorFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroMenorFilterBuilder
	
	#region PasajeroMenorParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PasajeroMenorColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroMenor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroMenorParameterBuilder : ParameterizedSqlFilterBuilder<PasajeroMenorColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorParameterBuilder class.
		/// </summary>
		public PasajeroMenorParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroMenorParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroMenorParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroMenorParameterBuilder
	
	#region PasajeroMenorSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PasajeroMenorColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroMenor"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PasajeroMenorSortBuilder : SqlSortBuilder<PasajeroMenorColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorSqlSortBuilder class.
		/// </summary>
		public PasajeroMenorSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PasajeroMenorSortBuilder
	
} // end namespace
