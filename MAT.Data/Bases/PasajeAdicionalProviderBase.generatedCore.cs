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
	/// This class is the base class for any <see cref="PasajeAdicionalProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PasajeAdicionalProviderBaseCore : EntityProviderBase<MAT.Entities.PasajeAdicional, MAT.Entities.PasajeAdicionalKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PasajeAdicionalKey key)
		{
			return Delete(transactionManager, key.PasajeAdicionalId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_pasajeAdicionalId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _pasajeAdicionalId)
		{
			return Delete(null, _pasajeAdicionalId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeAdicionalId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _pasajeAdicionalId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Adicional key.
		///		FK_PasajeAdicional_Adicional Description: 
		/// </summary>
		/// <param name="_adicionalId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		public TList<PasajeAdicional> GetByAdicionalId(System.Guid? _adicionalId)
		{
			int count = -1;
			return GetByAdicionalId(_adicionalId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Adicional key.
		///		FK_PasajeAdicional_Adicional Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_adicionalId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		/// <remarks></remarks>
		public TList<PasajeAdicional> GetByAdicionalId(TransactionManager transactionManager, System.Guid? _adicionalId)
		{
			int count = -1;
			return GetByAdicionalId(transactionManager, _adicionalId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Adicional key.
		///		FK_PasajeAdicional_Adicional Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_adicionalId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		public TList<PasajeAdicional> GetByAdicionalId(TransactionManager transactionManager, System.Guid? _adicionalId, int start, int pageLength)
		{
			int count = -1;
			return GetByAdicionalId(transactionManager, _adicionalId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Adicional key.
		///		fkPasajeAdicionalAdicional Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_adicionalId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		public TList<PasajeAdicional> GetByAdicionalId(System.Guid? _adicionalId, int start, int pageLength)
		{
			int count =  -1;
			return GetByAdicionalId(null, _adicionalId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Adicional key.
		///		fkPasajeAdicionalAdicional Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_adicionalId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		public TList<PasajeAdicional> GetByAdicionalId(System.Guid? _adicionalId, int start, int pageLength,out int count)
		{
			return GetByAdicionalId(null, _adicionalId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Adicional key.
		///		FK_PasajeAdicional_Adicional Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_adicionalId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		public abstract TList<PasajeAdicional> GetByAdicionalId(TransactionManager transactionManager, System.Guid? _adicionalId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Pasaje key.
		///		FK_PasajeAdicional_Pasaje Description: 
		/// </summary>
		/// <param name="_pasajeId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		public TList<PasajeAdicional> GetByPasajeId(System.Guid? _pasajeId)
		{
			int count = -1;
			return GetByPasajeId(_pasajeId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Pasaje key.
		///		FK_PasajeAdicional_Pasaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		/// <remarks></remarks>
		public TList<PasajeAdicional> GetByPasajeId(TransactionManager transactionManager, System.Guid? _pasajeId)
		{
			int count = -1;
			return GetByPasajeId(transactionManager, _pasajeId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Pasaje key.
		///		FK_PasajeAdicional_Pasaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		public TList<PasajeAdicional> GetByPasajeId(TransactionManager transactionManager, System.Guid? _pasajeId, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeId(transactionManager, _pasajeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Pasaje key.
		///		fkPasajeAdicionalPasaje Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pasajeId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		public TList<PasajeAdicional> GetByPasajeId(System.Guid? _pasajeId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPasajeId(null, _pasajeId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Pasaje key.
		///		fkPasajeAdicionalPasaje Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pasajeId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		public TList<PasajeAdicional> GetByPasajeId(System.Guid? _pasajeId, int start, int pageLength,out int count)
		{
			return GetByPasajeId(null, _pasajeId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeAdicional_Pasaje key.
		///		FK_PasajeAdicional_Pasaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PasajeAdicional objects.</returns>
		public abstract TList<PasajeAdicional> GetByPasajeId(TransactionManager transactionManager, System.Guid? _pasajeId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.PasajeAdicional Get(TransactionManager transactionManager, MAT.Entities.PasajeAdicionalKey key, int start, int pageLength)
		{
			return GetByPasajeAdicionalId(transactionManager, key.PasajeAdicionalId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_PasajeAdicional index.
		/// </summary>
		/// <param name="_pasajeAdicionalId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeAdicional"/> class.</returns>
		public MAT.Entities.PasajeAdicional GetByPasajeAdicionalId(System.Guid _pasajeAdicionalId)
		{
			int count = -1;
			return GetByPasajeAdicionalId(null,_pasajeAdicionalId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PasajeAdicional index.
		/// </summary>
		/// <param name="_pasajeAdicionalId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeAdicional"/> class.</returns>
		public MAT.Entities.PasajeAdicional GetByPasajeAdicionalId(System.Guid _pasajeAdicionalId, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeAdicionalId(null, _pasajeAdicionalId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PasajeAdicional index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeAdicionalId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeAdicional"/> class.</returns>
		public MAT.Entities.PasajeAdicional GetByPasajeAdicionalId(TransactionManager transactionManager, System.Guid _pasajeAdicionalId)
		{
			int count = -1;
			return GetByPasajeAdicionalId(transactionManager, _pasajeAdicionalId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PasajeAdicional index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeAdicionalId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeAdicional"/> class.</returns>
		public MAT.Entities.PasajeAdicional GetByPasajeAdicionalId(TransactionManager transactionManager, System.Guid _pasajeAdicionalId, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeAdicionalId(transactionManager, _pasajeAdicionalId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PasajeAdicional index.
		/// </summary>
		/// <param name="_pasajeAdicionalId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeAdicional"/> class.</returns>
		public MAT.Entities.PasajeAdicional GetByPasajeAdicionalId(System.Guid _pasajeAdicionalId, int start, int pageLength, out int count)
		{
			return GetByPasajeAdicionalId(null, _pasajeAdicionalId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PasajeAdicional index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeAdicionalId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PasajeAdicional"/> class.</returns>
		public abstract MAT.Entities.PasajeAdicional GetByPasajeAdicionalId(TransactionManager transactionManager, System.Guid _pasajeAdicionalId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;PasajeAdicional&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;PasajeAdicional&gt;"/></returns>
		public static TList<PasajeAdicional> Fill(IDataReader reader, TList<PasajeAdicional> rows, int start, int pageLength)
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
				
				MAT.Entities.PasajeAdicional c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("PasajeAdicional")
					.Append("|").Append((System.Guid)reader[((int)PasajeAdicionalColumn.PasajeAdicionalId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<PasajeAdicional>(
					key.ToString(), // EntityTrackingKey
					"PasajeAdicional",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.PasajeAdicional();
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
					c.PasajeAdicionalId = (System.Guid)reader[((int)PasajeAdicionalColumn.PasajeAdicionalId - 1)];
					c.OriginalPasajeAdicionalId = c.PasajeAdicionalId;
					c.PasajeId = (reader.IsDBNull(((int)PasajeAdicionalColumn.PasajeId - 1)))?null:(System.Guid?)reader[((int)PasajeAdicionalColumn.PasajeId - 1)];
					c.AdicionalId = (reader.IsDBNull(((int)PasajeAdicionalColumn.AdicionalId - 1)))?null:(System.Guid?)reader[((int)PasajeAdicionalColumn.AdicionalId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PasajeAdicional"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PasajeAdicional"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.PasajeAdicional entity)
		{
			if (!reader.Read()) return;
			
			entity.PasajeAdicionalId = (System.Guid)reader[((int)PasajeAdicionalColumn.PasajeAdicionalId - 1)];
			entity.OriginalPasajeAdicionalId = (System.Guid)reader["PasajeAdicionalID"];
			entity.PasajeId = (reader.IsDBNull(((int)PasajeAdicionalColumn.PasajeId - 1)))?null:(System.Guid?)reader[((int)PasajeAdicionalColumn.PasajeId - 1)];
			entity.AdicionalId = (reader.IsDBNull(((int)PasajeAdicionalColumn.AdicionalId - 1)))?null:(System.Guid?)reader[((int)PasajeAdicionalColumn.AdicionalId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PasajeAdicional"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PasajeAdicional"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.PasajeAdicional entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PasajeAdicionalId = (System.Guid)dataRow["PasajeAdicionalID"];
			entity.OriginalPasajeAdicionalId = (System.Guid)dataRow["PasajeAdicionalID"];
			entity.PasajeId = Convert.IsDBNull(dataRow["PasajeID"]) ? null : (System.Guid?)dataRow["PasajeID"];
			entity.AdicionalId = Convert.IsDBNull(dataRow["AdicionalID"]) ? null : (System.Guid?)dataRow["AdicionalID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.PasajeAdicional"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.PasajeAdicional Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.PasajeAdicional entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region AdicionalIdSource	
			if (CanDeepLoad(entity, "Adicional|AdicionalIdSource", deepLoadType, innerList) 
				&& entity.AdicionalIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.AdicionalId ?? Guid.Empty);
				Adicional tmpEntity = EntityManager.LocateEntity<Adicional>(EntityLocator.ConstructKeyFromPkItems(typeof(Adicional), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.AdicionalIdSource = tmpEntity;
				else
					entity.AdicionalIdSource = DataRepository.AdicionalProvider.GetByAdicionalId(transactionManager, (entity.AdicionalId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'AdicionalIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.AdicionalIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.AdicionalProvider.DeepLoad(transactionManager, entity.AdicionalIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion AdicionalIdSource

			#region PasajeIdSource	
			if (CanDeepLoad(entity, "Pasaje|PasajeIdSource", deepLoadType, innerList) 
				&& entity.PasajeIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.PasajeId ?? Guid.Empty);
				Pasaje tmpEntity = EntityManager.LocateEntity<Pasaje>(EntityLocator.ConstructKeyFromPkItems(typeof(Pasaje), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PasajeIdSource = tmpEntity;
				else
					entity.PasajeIdSource = DataRepository.PasajeProvider.GetByPasajeId(transactionManager, (entity.PasajeId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PasajeIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PasajeProvider.DeepLoad(transactionManager, entity.PasajeIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PasajeIdSource
			
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
		/// Deep Save the entire object graph of the MAT.Entities.PasajeAdicional object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.PasajeAdicional instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.PasajeAdicional Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.PasajeAdicional entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region AdicionalIdSource
			if (CanDeepSave(entity, "Adicional|AdicionalIdSource", deepSaveType, innerList) 
				&& entity.AdicionalIdSource != null)
			{
				DataRepository.AdicionalProvider.Save(transactionManager, entity.AdicionalIdSource);
				entity.AdicionalId = entity.AdicionalIdSource.AdicionalId;
			}
			#endregion 
			
			#region PasajeIdSource
			if (CanDeepSave(entity, "Pasaje|PasajeIdSource", deepSaveType, innerList) 
				&& entity.PasajeIdSource != null)
			{
				DataRepository.PasajeProvider.Save(transactionManager, entity.PasajeIdSource);
				entity.PasajeId = entity.PasajeIdSource.PasajeId;
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
	
	#region PasajeAdicionalChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.PasajeAdicional</c>
	///</summary>
	public enum PasajeAdicionalChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Adicional</c> at AdicionalIdSource
		///</summary>
		[ChildEntityType(typeof(Adicional))]
		Adicional,
		
		///<summary>
		/// Composite Property for <c>Pasaje</c> at PasajeIdSource
		///</summary>
		[ChildEntityType(typeof(Pasaje))]
		Pasaje,
	}
	
	#endregion PasajeAdicionalChildEntityTypes
	
	#region PasajeAdicionalFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PasajeAdicionalColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeAdicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeAdicionalFilterBuilder : SqlFilterBuilder<PasajeAdicionalColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalFilterBuilder class.
		/// </summary>
		public PasajeAdicionalFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeAdicionalFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeAdicionalFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeAdicionalFilterBuilder
	
	#region PasajeAdicionalParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PasajeAdicionalColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeAdicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeAdicionalParameterBuilder : ParameterizedSqlFilterBuilder<PasajeAdicionalColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalParameterBuilder class.
		/// </summary>
		public PasajeAdicionalParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeAdicionalParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeAdicionalParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeAdicionalParameterBuilder
	
	#region PasajeAdicionalSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PasajeAdicionalColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeAdicional"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PasajeAdicionalSortBuilder : SqlSortBuilder<PasajeAdicionalColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalSqlSortBuilder class.
		/// </summary>
		public PasajeAdicionalSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PasajeAdicionalSortBuilder
	
} // end namespace
