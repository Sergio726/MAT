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
	/// This class is the base class for any <see cref="ExcursionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ExcursionProviderBaseCore : EntityProviderBase<MAT.Entities.Excursion, MAT.Entities.ExcursionKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.ExcursionKey key)
		{
			return Delete(transactionManager, key.ExcursionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_excursionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _excursionId)
		{
			return Delete(null, _excursionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_excursionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _excursionId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Excursion_Proveedor key.
		///		FK_Excursion_Proveedor Description: 
		/// </summary>
		/// <param name="_proveedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Excursion objects.</returns>
		public TList<Excursion> GetByProveedorId(System.Guid? _proveedorId)
		{
			int count = -1;
			return GetByProveedorId(_proveedorId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Excursion_Proveedor key.
		///		FK_Excursion_Proveedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_proveedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Excursion objects.</returns>
		/// <remarks></remarks>
		public TList<Excursion> GetByProveedorId(TransactionManager transactionManager, System.Guid? _proveedorId)
		{
			int count = -1;
			return GetByProveedorId(transactionManager, _proveedorId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Excursion_Proveedor key.
		///		FK_Excursion_Proveedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_proveedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Excursion objects.</returns>
		public TList<Excursion> GetByProveedorId(TransactionManager transactionManager, System.Guid? _proveedorId, int start, int pageLength)
		{
			int count = -1;
			return GetByProveedorId(transactionManager, _proveedorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Excursion_Proveedor key.
		///		fkExcursionProveedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_proveedorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Excursion objects.</returns>
		public TList<Excursion> GetByProveedorId(System.Guid? _proveedorId, int start, int pageLength)
		{
			int count =  -1;
			return GetByProveedorId(null, _proveedorId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Excursion_Proveedor key.
		///		fkExcursionProveedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_proveedorId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Excursion objects.</returns>
		public TList<Excursion> GetByProveedorId(System.Guid? _proveedorId, int start, int pageLength,out int count)
		{
			return GetByProveedorId(null, _proveedorId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Excursion_Proveedor key.
		///		FK_Excursion_Proveedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_proveedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Excursion objects.</returns>
		public abstract TList<Excursion> GetByProveedorId(TransactionManager transactionManager, System.Guid? _proveedorId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Excursion Get(TransactionManager transactionManager, MAT.Entities.ExcursionKey key, int start, int pageLength)
		{
			return GetByExcursionId(transactionManager, key.ExcursionId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Excursion index.
		/// </summary>
		/// <param name="_excursionId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Excursion"/> class.</returns>
		public MAT.Entities.Excursion GetByExcursionId(System.Guid _excursionId)
		{
			int count = -1;
			return GetByExcursionId(null,_excursionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Excursion index.
		/// </summary>
		/// <param name="_excursionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Excursion"/> class.</returns>
		public MAT.Entities.Excursion GetByExcursionId(System.Guid _excursionId, int start, int pageLength)
		{
			int count = -1;
			return GetByExcursionId(null, _excursionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Excursion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_excursionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Excursion"/> class.</returns>
		public MAT.Entities.Excursion GetByExcursionId(TransactionManager transactionManager, System.Guid _excursionId)
		{
			int count = -1;
			return GetByExcursionId(transactionManager, _excursionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Excursion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_excursionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Excursion"/> class.</returns>
		public MAT.Entities.Excursion GetByExcursionId(TransactionManager transactionManager, System.Guid _excursionId, int start, int pageLength)
		{
			int count = -1;
			return GetByExcursionId(transactionManager, _excursionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Excursion index.
		/// </summary>
		/// <param name="_excursionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Excursion"/> class.</returns>
		public MAT.Entities.Excursion GetByExcursionId(System.Guid _excursionId, int start, int pageLength, out int count)
		{
			return GetByExcursionId(null, _excursionId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Excursion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_excursionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Excursion"/> class.</returns>
		public abstract MAT.Entities.Excursion GetByExcursionId(TransactionManager transactionManager, System.Guid _excursionId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Excursion&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Excursion&gt;"/></returns>
		public static TList<Excursion> Fill(IDataReader reader, TList<Excursion> rows, int start, int pageLength)
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
				
				MAT.Entities.Excursion c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Excursion")
					.Append("|").Append((System.Guid)reader[((int)ExcursionColumn.ExcursionId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Excursion>(
					key.ToString(), // EntityTrackingKey
					"Excursion",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Excursion();
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
					c.ExcursionId = (System.Guid)reader[((int)ExcursionColumn.ExcursionId - 1)];
					c.OriginalExcursionId = c.ExcursionId;
					c.Descripcion = (System.String)reader[((int)ExcursionColumn.Descripcion - 1)];
					c.Costo = (reader.IsDBNull(((int)ExcursionColumn.Costo - 1)))?null:(System.Double?)reader[((int)ExcursionColumn.Costo - 1)];
					c.Observaciones = (reader.IsDBNull(((int)ExcursionColumn.Observaciones - 1)))?null:(System.String)reader[((int)ExcursionColumn.Observaciones - 1)];
					c.ProveedorId = (reader.IsDBNull(((int)ExcursionColumn.ProveedorId - 1)))?null:(System.Guid?)reader[((int)ExcursionColumn.ProveedorId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Excursion"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Excursion"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Excursion entity)
		{
			if (!reader.Read()) return;
			
			entity.ExcursionId = (System.Guid)reader[((int)ExcursionColumn.ExcursionId - 1)];
			entity.OriginalExcursionId = (System.Guid)reader["ExcursionID"];
			entity.Descripcion = (System.String)reader[((int)ExcursionColumn.Descripcion - 1)];
			entity.Costo = (reader.IsDBNull(((int)ExcursionColumn.Costo - 1)))?null:(System.Double?)reader[((int)ExcursionColumn.Costo - 1)];
			entity.Observaciones = (reader.IsDBNull(((int)ExcursionColumn.Observaciones - 1)))?null:(System.String)reader[((int)ExcursionColumn.Observaciones - 1)];
			entity.ProveedorId = (reader.IsDBNull(((int)ExcursionColumn.ProveedorId - 1)))?null:(System.Guid?)reader[((int)ExcursionColumn.ProveedorId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Excursion"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Excursion"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Excursion entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ExcursionId = (System.Guid)dataRow["ExcursionID"];
			entity.OriginalExcursionId = (System.Guid)dataRow["ExcursionID"];
			entity.Descripcion = (System.String)dataRow["Descripcion"];
			entity.Costo = Convert.IsDBNull(dataRow["Costo"]) ? null : (System.Double?)dataRow["Costo"];
			entity.Observaciones = Convert.IsDBNull(dataRow["Observaciones"]) ? null : (System.String)dataRow["Observaciones"];
			entity.ProveedorId = Convert.IsDBNull(dataRow["ProveedorID"]) ? null : (System.Guid?)dataRow["ProveedorID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Excursion"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Excursion Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Excursion entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

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
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByExcursionId methods when available
			
			#region PaqueteExcursionCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PaqueteExcursion>|PaqueteExcursionCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaqueteExcursionCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PaqueteExcursionCollection = DataRepository.PaqueteExcursionProvider.GetByExcursionId(transactionManager, entity.ExcursionId);

				if (deep && entity.PaqueteExcursionCollection.Count > 0)
				{
					deepHandles.Add("PaqueteExcursionCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PaqueteExcursion>) DataRepository.PaqueteExcursionProvider.DeepLoad,
						new object[] { transactionManager, entity.PaqueteExcursionCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Excursion object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Excursion instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Excursion Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Excursion entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ProveedorIdSource
			if (CanDeepSave(entity, "Proveedor|ProveedorIdSource", deepSaveType, innerList) 
				&& entity.ProveedorIdSource != null)
			{
				DataRepository.ProveedorProvider.Save(transactionManager, entity.ProveedorIdSource);
				entity.ProveedorId = entity.ProveedorIdSource.ProveedorId;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<PaqueteExcursion>
				if (CanDeepSave(entity.PaqueteExcursionCollection, "List<PaqueteExcursion>|PaqueteExcursionCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PaqueteExcursion child in entity.PaqueteExcursionCollection)
					{
						if(child.ExcursionIdSource != null)
						{
							child.ExcursionId = child.ExcursionIdSource.ExcursionId;
						}
						else
						{
							child.ExcursionId = entity.ExcursionId;
						}

					}

					if (entity.PaqueteExcursionCollection.Count > 0 || entity.PaqueteExcursionCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PaqueteExcursionProvider.Save(transactionManager, entity.PaqueteExcursionCollection);
						
						deepHandles.Add("PaqueteExcursionCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PaqueteExcursion >) DataRepository.PaqueteExcursionProvider.DeepSave,
							new object[] { transactionManager, entity.PaqueteExcursionCollection, deepSaveType, childTypes, innerList }
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
	
	#region ExcursionChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Excursion</c>
	///</summary>
	public enum ExcursionChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Proveedor</c> at ProveedorIdSource
		///</summary>
		[ChildEntityType(typeof(Proveedor))]
		Proveedor,
		///<summary>
		/// Collection of <c>Excursion</c> as OneToMany for PaqueteExcursionCollection
		///</summary>
		[ChildEntityType(typeof(TList<PaqueteExcursion>))]
		PaqueteExcursionCollection,
	}
	
	#endregion ExcursionChildEntityTypes
	
	#region ExcursionFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ExcursionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Excursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ExcursionFilterBuilder : SqlFilterBuilder<ExcursionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ExcursionFilterBuilder class.
		/// </summary>
		public ExcursionFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ExcursionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ExcursionFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ExcursionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ExcursionFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ExcursionFilterBuilder
	
	#region ExcursionParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ExcursionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Excursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ExcursionParameterBuilder : ParameterizedSqlFilterBuilder<ExcursionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ExcursionParameterBuilder class.
		/// </summary>
		public ExcursionParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ExcursionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ExcursionParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ExcursionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ExcursionParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ExcursionParameterBuilder
	
	#region ExcursionSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ExcursionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Excursion"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ExcursionSortBuilder : SqlSortBuilder<ExcursionColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ExcursionSqlSortBuilder class.
		/// </summary>
		public ExcursionSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ExcursionSortBuilder
	
} // end namespace
