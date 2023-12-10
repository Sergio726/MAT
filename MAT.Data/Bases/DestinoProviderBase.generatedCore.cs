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
	/// This class is the base class for any <see cref="DestinoProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class DestinoProviderBaseCore : EntityProviderBase<MAT.Entities.Destino, MAT.Entities.DestinoKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.DestinoKey key)
		{
			return Delete(transactionManager, key.DestinoId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_destinoId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _destinoId)
		{
			return Delete(null, _destinoId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_destinoId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _destinoId);		
		
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
		public override MAT.Entities.Destino Get(TransactionManager transactionManager, MAT.Entities.DestinoKey key, int start, int pageLength)
		{
			return GetByDestinoId(transactionManager, key.DestinoId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Destino index.
		/// </summary>
		/// <param name="_destinoId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Destino"/> class.</returns>
		public MAT.Entities.Destino GetByDestinoId(System.Guid _destinoId)
		{
			int count = -1;
			return GetByDestinoId(null,_destinoId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Destino index.
		/// </summary>
		/// <param name="_destinoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Destino"/> class.</returns>
		public MAT.Entities.Destino GetByDestinoId(System.Guid _destinoId, int start, int pageLength)
		{
			int count = -1;
			return GetByDestinoId(null, _destinoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Destino index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_destinoId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Destino"/> class.</returns>
		public MAT.Entities.Destino GetByDestinoId(TransactionManager transactionManager, System.Guid _destinoId)
		{
			int count = -1;
			return GetByDestinoId(transactionManager, _destinoId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Destino index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_destinoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Destino"/> class.</returns>
		public MAT.Entities.Destino GetByDestinoId(TransactionManager transactionManager, System.Guid _destinoId, int start, int pageLength)
		{
			int count = -1;
			return GetByDestinoId(transactionManager, _destinoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Destino index.
		/// </summary>
		/// <param name="_destinoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Destino"/> class.</returns>
		public MAT.Entities.Destino GetByDestinoId(System.Guid _destinoId, int start, int pageLength, out int count)
		{
			return GetByDestinoId(null, _destinoId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Destino index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_destinoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Destino"/> class.</returns>
		public abstract MAT.Entities.Destino GetByDestinoId(TransactionManager transactionManager, System.Guid _destinoId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Destino&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Destino&gt;"/></returns>
		public static TList<Destino> Fill(IDataReader reader, TList<Destino> rows, int start, int pageLength)
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
				
				MAT.Entities.Destino c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Destino")
					.Append("|").Append((System.Guid)reader[((int)DestinoColumn.DestinoId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Destino>(
					key.ToString(), // EntityTrackingKey
					"Destino",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Destino();
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
					c.DestinoId = (System.Guid)reader[((int)DestinoColumn.DestinoId - 1)];
					c.OriginalDestinoId = c.DestinoId;
					c.LocalidadId = (reader.IsDBNull(((int)DestinoColumn.LocalidadId - 1)))?null:(System.Int32?)reader[((int)DestinoColumn.LocalidadId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Destino"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Destino"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Destino entity)
		{
			if (!reader.Read()) return;
			
			entity.DestinoId = (System.Guid)reader[((int)DestinoColumn.DestinoId - 1)];
			entity.OriginalDestinoId = (System.Guid)reader["DestinoID"];
			entity.LocalidadId = (reader.IsDBNull(((int)DestinoColumn.LocalidadId - 1)))?null:(System.Int32?)reader[((int)DestinoColumn.LocalidadId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Destino"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Destino"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Destino entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.DestinoId = (System.Guid)dataRow["DestinoID"];
			entity.OriginalDestinoId = (System.Guid)dataRow["DestinoID"];
			entity.LocalidadId = Convert.IsDBNull(dataRow["LocalidadID"]) ? null : (System.Int32?)dataRow["LocalidadID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Destino"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Destino Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Destino entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByDestinoId methods when available
			
			#region PaqueteCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Paquete>|PaqueteCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaqueteCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PaqueteCollection = DataRepository.PaqueteProvider.GetByDestinoId(transactionManager, entity.DestinoId);

				if (deep && entity.PaqueteCollection.Count > 0)
				{
					deepHandles.Add("PaqueteCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Paquete>) DataRepository.PaqueteProvider.DeepLoad,
						new object[] { transactionManager, entity.PaqueteCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Destino object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Destino instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Destino Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Destino entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<Paquete>
				if (CanDeepSave(entity.PaqueteCollection, "List<Paquete>|PaqueteCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Paquete child in entity.PaqueteCollection)
					{
						if(child.DestinoIdSource != null)
						{
							child.DestinoId = child.DestinoIdSource.DestinoId;
						}
						else
						{
							child.DestinoId = entity.DestinoId;
						}

					}

					if (entity.PaqueteCollection.Count > 0 || entity.PaqueteCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PaqueteProvider.Save(transactionManager, entity.PaqueteCollection);
						
						deepHandles.Add("PaqueteCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Paquete >) DataRepository.PaqueteProvider.DeepSave,
							new object[] { transactionManager, entity.PaqueteCollection, deepSaveType, childTypes, innerList }
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
	
	#region DestinoChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Destino</c>
	///</summary>
	public enum DestinoChildEntityTypes
	{
		///<summary>
		/// Collection of <c>Destino</c> as OneToMany for PaqueteCollection
		///</summary>
		[ChildEntityType(typeof(TList<Paquete>))]
		PaqueteCollection,
	}
	
	#endregion DestinoChildEntityTypes
	
	#region DestinoFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;DestinoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Destino"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DestinoFilterBuilder : SqlFilterBuilder<DestinoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DestinoFilterBuilder class.
		/// </summary>
		public DestinoFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DestinoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DestinoFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DestinoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DestinoFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DestinoFilterBuilder
	
	#region DestinoParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;DestinoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Destino"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DestinoParameterBuilder : ParameterizedSqlFilterBuilder<DestinoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DestinoParameterBuilder class.
		/// </summary>
		public DestinoParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DestinoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DestinoParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DestinoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DestinoParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DestinoParameterBuilder
	
	#region DestinoSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;DestinoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Destino"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class DestinoSortBuilder : SqlSortBuilder<DestinoColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DestinoSqlSortBuilder class.
		/// </summary>
		public DestinoSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion DestinoSortBuilder
	
} // end namespace
