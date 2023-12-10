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
	/// This class is the base class for any <see cref="PasajeroProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PasajeroProviderBaseCore : EntityProviderBase<MAT.Entities.Pasajero, MAT.Entities.PasajeroKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PasajeroKey key)
		{
			return Delete(transactionManager, key.PasajeroId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_pasajeroId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _pasajeroId)
		{
			return Delete(null, _pasajeroId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _pasajeroId);		
		
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
		public override MAT.Entities.Pasajero Get(TransactionManager transactionManager, MAT.Entities.PasajeroKey key, int start, int pageLength)
		{
			return GetByPasajeroId(transactionManager, key.PasajeroId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Pasajero index.
		/// </summary>
		/// <param name="_pasajeroId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasajero"/> class.</returns>
		public MAT.Entities.Pasajero GetByPasajeroId(System.Guid _pasajeroId)
		{
			int count = -1;
			return GetByPasajeroId(null,_pasajeroId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pasajero index.
		/// </summary>
		/// <param name="_pasajeroId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasajero"/> class.</returns>
		public MAT.Entities.Pasajero GetByPasajeroId(System.Guid _pasajeroId, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeroId(null, _pasajeroId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pasajero index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasajero"/> class.</returns>
		public MAT.Entities.Pasajero GetByPasajeroId(TransactionManager transactionManager, System.Guid _pasajeroId)
		{
			int count = -1;
			return GetByPasajeroId(transactionManager, _pasajeroId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pasajero index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasajero"/> class.</returns>
		public MAT.Entities.Pasajero GetByPasajeroId(TransactionManager transactionManager, System.Guid _pasajeroId, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeroId(transactionManager, _pasajeroId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pasajero index.
		/// </summary>
		/// <param name="_pasajeroId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasajero"/> class.</returns>
		public MAT.Entities.Pasajero GetByPasajeroId(System.Guid _pasajeroId, int start, int pageLength, out int count)
		{
			return GetByPasajeroId(null, _pasajeroId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pasajero index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasajero"/> class.</returns>
		public abstract MAT.Entities.Pasajero GetByPasajeroId(TransactionManager transactionManager, System.Guid _pasajeroId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Pasajero&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Pasajero&gt;"/></returns>
		public static TList<Pasajero> Fill(IDataReader reader, TList<Pasajero> rows, int start, int pageLength)
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
				
				MAT.Entities.Pasajero c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Pasajero")
					.Append("|").Append((System.Guid)reader[((int)PasajeroColumn.PasajeroId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Pasajero>(
					key.ToString(), // EntityTrackingKey
					"Pasajero",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Pasajero();
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
					c.PasajeroId = (System.Guid)reader[((int)PasajeroColumn.PasajeroId - 1)];
					c.OriginalPasajeroId = c.PasajeroId;
					c.Pasaporte = (reader.IsDBNull(((int)PasajeroColumn.Pasaporte - 1)))?null:(System.String)reader[((int)PasajeroColumn.Pasaporte - 1)];
					c.VencimientoPasaporte = (reader.IsDBNull(((int)PasajeroColumn.VencimientoPasaporte - 1)))?null:(System.DateTime?)reader[((int)PasajeroColumn.VencimientoPasaporte - 1)];
					c.EmisionPasaporte = (reader.IsDBNull(((int)PasajeroColumn.EmisionPasaporte - 1)))?null:(System.DateTime?)reader[((int)PasajeroColumn.EmisionPasaporte - 1)];
					c.PaisOrigen = (reader.IsDBNull(((int)PasajeroColumn.PaisOrigen - 1)))?null:(System.String)reader[((int)PasajeroColumn.PaisOrigen - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Pasajero"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Pasajero"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Pasajero entity)
		{
			if (!reader.Read()) return;
			
			entity.PasajeroId = (System.Guid)reader[((int)PasajeroColumn.PasajeroId - 1)];
			entity.OriginalPasajeroId = (System.Guid)reader["PasajeroID"];
			entity.Pasaporte = (reader.IsDBNull(((int)PasajeroColumn.Pasaporte - 1)))?null:(System.String)reader[((int)PasajeroColumn.Pasaporte - 1)];
			entity.VencimientoPasaporte = (reader.IsDBNull(((int)PasajeroColumn.VencimientoPasaporte - 1)))?null:(System.DateTime?)reader[((int)PasajeroColumn.VencimientoPasaporte - 1)];
			entity.EmisionPasaporte = (reader.IsDBNull(((int)PasajeroColumn.EmisionPasaporte - 1)))?null:(System.DateTime?)reader[((int)PasajeroColumn.EmisionPasaporte - 1)];
			entity.PaisOrigen = (reader.IsDBNull(((int)PasajeroColumn.PaisOrigen - 1)))?null:(System.String)reader[((int)PasajeroColumn.PaisOrigen - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Pasajero"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Pasajero"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Pasajero entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PasajeroId = (System.Guid)dataRow["PasajeroID"];
			entity.OriginalPasajeroId = (System.Guid)dataRow["PasajeroID"];
			entity.Pasaporte = Convert.IsDBNull(dataRow["Pasaporte"]) ? null : (System.String)dataRow["Pasaporte"];
			entity.VencimientoPasaporte = Convert.IsDBNull(dataRow["VencimientoPasaporte"]) ? null : (System.DateTime?)dataRow["VencimientoPasaporte"];
			entity.EmisionPasaporte = Convert.IsDBNull(dataRow["EmisionPasaporte"]) ? null : (System.DateTime?)dataRow["EmisionPasaporte"];
			entity.PaisOrigen = Convert.IsDBNull(dataRow["PaisOrigen"]) ? null : (System.String)dataRow["PaisOrigen"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Pasajero"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Pasajero Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Pasajero entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region PasajeroIdSource	
			if (CanDeepLoad(entity, "Persona|PasajeroIdSource", deepLoadType, innerList) 
				&& entity.PasajeroIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.PasajeroId;
				Persona tmpEntity = EntityManager.LocateEntity<Persona>(EntityLocator.ConstructKeyFromPkItems(typeof(Persona), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PasajeroIdSource = tmpEntity;
				else
					entity.PasajeroIdSource = DataRepository.PersonaProvider.GetByPersonaId(transactionManager, entity.PasajeroId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeroIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PasajeroIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PersonaProvider.DeepLoad(transactionManager, entity.PasajeroIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PasajeroIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByPasajeroId methods when available
			
			#region PasajeCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Pasaje>|PasajeCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PasajeCollection = DataRepository.PasajeProvider.GetByPasajeroId(transactionManager, entity.PasajeroId);

				if (deep && entity.PasajeCollection.Count > 0)
				{
					deepHandles.Add("PasajeCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Pasaje>) DataRepository.PasajeProvider.DeepLoad,
						new object[] { transactionManager, entity.PasajeCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Pasajero object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Pasajero instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Pasajero Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Pasajero entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region PasajeroIdSource
			if (CanDeepSave(entity, "Persona|PasajeroIdSource", deepSaveType, innerList) 
				&& entity.PasajeroIdSource != null)
			{
				DataRepository.PersonaProvider.Save(transactionManager, entity.PasajeroIdSource);
				entity.PasajeroId = entity.PasajeroIdSource.PersonaId;
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
						if(child.PasajeroIdSource != null)
						{
							child.PasajeroId = child.PasajeroIdSource.PasajeroId;
						}
						else
						{
							child.PasajeroId = entity.PasajeroId;
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
	
	#region PasajeroChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Pasajero</c>
	///</summary>
	public enum PasajeroChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Persona</c> at PasajeroIdSource
		///</summary>
		[ChildEntityType(typeof(Persona))]
		Persona,
		///<summary>
		/// Collection of <c>Pasajero</c> as OneToMany for PasajeCollection
		///</summary>
		[ChildEntityType(typeof(TList<Pasaje>))]
		PasajeCollection,
	}
	
	#endregion PasajeroChildEntityTypes
	
	#region PasajeroFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PasajeroColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroFilterBuilder : SqlFilterBuilder<PasajeroColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroFilterBuilder class.
		/// </summary>
		public PasajeroFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroFilterBuilder
	
	#region PasajeroParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PasajeroColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroParameterBuilder : ParameterizedSqlFilterBuilder<PasajeroColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroParameterBuilder class.
		/// </summary>
		public PasajeroParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroParameterBuilder
	
	#region PasajeroSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PasajeroColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasajero"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PasajeroSortBuilder : SqlSortBuilder<PasajeroColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroSqlSortBuilder class.
		/// </summary>
		public PasajeroSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PasajeroSortBuilder
	
} // end namespace
