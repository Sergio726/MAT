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
	/// This class is the base class for any <see cref="AdicionalProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class AdicionalProviderBaseCore : EntityProviderBase<MAT.Entities.Adicional, MAT.Entities.AdicionalKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.AdicionalKey key)
		{
			return Delete(transactionManager, key.AdicionalId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_adicionalId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _adicionalId)
		{
			return Delete(null, _adicionalId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_adicionalId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _adicionalId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
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
		public override MAT.Entities.Adicional Get(TransactionManager transactionManager, MAT.Entities.AdicionalKey key, int start, int pageLength)
		{
			return GetByAdicionalId(transactionManager, key.AdicionalId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Adicional index.
		/// </summary>
		/// <param name="_adicionalId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Adicional"/> class.</returns>
		public MAT.Entities.Adicional GetByAdicionalId(System.Guid _adicionalId)
		{
			int count = -1;
			return GetByAdicionalId(null,_adicionalId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Adicional index.
		/// </summary>
		/// <param name="_adicionalId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Adicional"/> class.</returns>
		public MAT.Entities.Adicional GetByAdicionalId(System.Guid _adicionalId, int start, int pageLength)
		{
			int count = -1;
			return GetByAdicionalId(null, _adicionalId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Adicional index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_adicionalId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Adicional"/> class.</returns>
		public MAT.Entities.Adicional GetByAdicionalId(TransactionManager transactionManager, System.Guid _adicionalId)
		{
			int count = -1;
			return GetByAdicionalId(transactionManager, _adicionalId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Adicional index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_adicionalId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Adicional"/> class.</returns>
		public MAT.Entities.Adicional GetByAdicionalId(TransactionManager transactionManager, System.Guid _adicionalId, int start, int pageLength)
		{
			int count = -1;
			return GetByAdicionalId(transactionManager, _adicionalId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Adicional index.
		/// </summary>
		/// <param name="_adicionalId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Adicional"/> class.</returns>
		public MAT.Entities.Adicional GetByAdicionalId(System.Guid _adicionalId, int start, int pageLength, out int count)
		{
			return GetByAdicionalId(null, _adicionalId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Adicional index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_adicionalId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Adicional"/> class.</returns>
		public abstract MAT.Entities.Adicional GetByAdicionalId(TransactionManager transactionManager, System.Guid _adicionalId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Adicional&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Adicional&gt;"/></returns>
		public static TList<Adicional> Fill(IDataReader reader, TList<Adicional> rows, int start, int pageLength)
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
				
				MAT.Entities.Adicional c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Adicional")
					.Append("|").Append((System.Guid)reader[((int)AdicionalColumn.AdicionalId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Adicional>(
					key.ToString(), // EntityTrackingKey
					"Adicional",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Adicional();
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
					c.AdicionalId = (System.Guid)reader[((int)AdicionalColumn.AdicionalId - 1)];
					c.OriginalAdicionalId = c.AdicionalId;
					c.Monto = (System.Double)reader[((int)AdicionalColumn.Monto - 1)];
					c.Descripcion = (System.String)reader[((int)AdicionalColumn.Descripcion - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Adicional"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Adicional"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Adicional entity)
		{
			if (!reader.Read()) return;
			
			entity.AdicionalId = (System.Guid)reader[((int)AdicionalColumn.AdicionalId - 1)];
			entity.OriginalAdicionalId = (System.Guid)reader["AdicionalID"];
			entity.Monto = (System.Double)reader[((int)AdicionalColumn.Monto - 1)];
			entity.Descripcion = (System.String)reader[((int)AdicionalColumn.Descripcion - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Adicional"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Adicional"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Adicional entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.AdicionalId = (System.Guid)dataRow["AdicionalID"];
			entity.OriginalAdicionalId = (System.Guid)dataRow["AdicionalID"];
			entity.Monto = (System.Double)dataRow["Monto"];
			entity.Descripcion = (System.String)dataRow["Descripcion"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Adicional"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Adicional Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Adicional entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByAdicionalId methods when available
			
			#region PasajeAdicionalCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PasajeAdicional>|PasajeAdicionalCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeAdicionalCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PasajeAdicionalCollection = DataRepository.PasajeAdicionalProvider.GetByAdicionalId(transactionManager, entity.AdicionalId);

				if (deep && entity.PasajeAdicionalCollection.Count > 0)
				{
					deepHandles.Add("PasajeAdicionalCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PasajeAdicional>) DataRepository.PasajeAdicionalProvider.DeepLoad,
						new object[] { transactionManager, entity.PasajeAdicionalCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PaqueteAdicionalCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PaqueteAdicional>|PaqueteAdicionalCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaqueteAdicionalCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PaqueteAdicionalCollection = DataRepository.PaqueteAdicionalProvider.GetByAdicionalId(transactionManager, entity.AdicionalId);

				if (deep && entity.PaqueteAdicionalCollection.Count > 0)
				{
					deepHandles.Add("PaqueteAdicionalCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PaqueteAdicional>) DataRepository.PaqueteAdicionalProvider.DeepLoad,
						new object[] { transactionManager, entity.PaqueteAdicionalCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Adicional object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Adicional instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Adicional Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Adicional entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<PasajeAdicional>
				if (CanDeepSave(entity.PasajeAdicionalCollection, "List<PasajeAdicional>|PasajeAdicionalCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PasajeAdicional child in entity.PasajeAdicionalCollection)
					{
						if(child.AdicionalIdSource != null)
						{
							child.AdicionalId = child.AdicionalIdSource.AdicionalId;
						}
						else
						{
							child.AdicionalId = entity.AdicionalId;
						}

					}

					if (entity.PasajeAdicionalCollection.Count > 0 || entity.PasajeAdicionalCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PasajeAdicionalProvider.Save(transactionManager, entity.PasajeAdicionalCollection);
						
						deepHandles.Add("PasajeAdicionalCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PasajeAdicional >) DataRepository.PasajeAdicionalProvider.DeepSave,
							new object[] { transactionManager, entity.PasajeAdicionalCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<PaqueteAdicional>
				if (CanDeepSave(entity.PaqueteAdicionalCollection, "List<PaqueteAdicional>|PaqueteAdicionalCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PaqueteAdicional child in entity.PaqueteAdicionalCollection)
					{
						if(child.AdicionalIdSource != null)
						{
							child.AdicionalId = child.AdicionalIdSource.AdicionalId;
						}
						else
						{
							child.AdicionalId = entity.AdicionalId;
						}

					}

					if (entity.PaqueteAdicionalCollection.Count > 0 || entity.PaqueteAdicionalCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PaqueteAdicionalProvider.Save(transactionManager, entity.PaqueteAdicionalCollection);
						
						deepHandles.Add("PaqueteAdicionalCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PaqueteAdicional >) DataRepository.PaqueteAdicionalProvider.DeepSave,
							new object[] { transactionManager, entity.PaqueteAdicionalCollection, deepSaveType, childTypes, innerList }
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
	
	#region AdicionalChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Adicional</c>
	///</summary>
	public enum AdicionalChildEntityTypes
	{
		///<summary>
		/// Collection of <c>Adicional</c> as OneToMany for PasajeAdicionalCollection
		///</summary>
		[ChildEntityType(typeof(TList<PasajeAdicional>))]
		PasajeAdicionalCollection,
		///<summary>
		/// Collection of <c>Adicional</c> as OneToMany for PaqueteAdicionalCollection
		///</summary>
		[ChildEntityType(typeof(TList<PaqueteAdicional>))]
		PaqueteAdicionalCollection,
	}
	
	#endregion AdicionalChildEntityTypes
	
	#region AdicionalFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;AdicionalColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Adicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AdicionalFilterBuilder : SqlFilterBuilder<AdicionalColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AdicionalFilterBuilder class.
		/// </summary>
		public AdicionalFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the AdicionalFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AdicionalFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AdicionalFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AdicionalFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AdicionalFilterBuilder
	
	#region AdicionalParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;AdicionalColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Adicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AdicionalParameterBuilder : ParameterizedSqlFilterBuilder<AdicionalColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AdicionalParameterBuilder class.
		/// </summary>
		public AdicionalParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the AdicionalParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AdicionalParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AdicionalParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AdicionalParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AdicionalParameterBuilder
	
	#region AdicionalSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;AdicionalColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Adicional"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class AdicionalSortBuilder : SqlSortBuilder<AdicionalColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AdicionalSqlSortBuilder class.
		/// </summary>
		public AdicionalSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion AdicionalSortBuilder
	
} // end namespace
