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
	/// This class is the base class for any <see cref="PlanillaServicioProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PlanillaServicioProviderBaseCore : EntityProviderBase<MAT.Entities.PlanillaServicio, MAT.Entities.PlanillaServicioKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PlanillaServicioKey key)
		{
			return Delete(transactionManager, key.PlanillaServicioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_planillaServicioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _planillaServicioId)
		{
			return Delete(null, _planillaServicioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaServicioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _planillaServicioId);		
		
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
		public override MAT.Entities.PlanillaServicio Get(TransactionManager transactionManager, MAT.Entities.PlanillaServicioKey key, int start, int pageLength)
		{
			return GetByPlanillaServicioId(transactionManager, key.PlanillaServicioId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_PlanillaServicio index.
		/// </summary>
		/// <param name="_planillaServicioId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicio"/> class.</returns>
		public MAT.Entities.PlanillaServicio GetByPlanillaServicioId(System.Guid _planillaServicioId)
		{
			int count = -1;
			return GetByPlanillaServicioId(null,_planillaServicioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicio index.
		/// </summary>
		/// <param name="_planillaServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicio"/> class.</returns>
		public MAT.Entities.PlanillaServicio GetByPlanillaServicioId(System.Guid _planillaServicioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPlanillaServicioId(null, _planillaServicioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaServicioId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicio"/> class.</returns>
		public MAT.Entities.PlanillaServicio GetByPlanillaServicioId(TransactionManager transactionManager, System.Guid _planillaServicioId)
		{
			int count = -1;
			return GetByPlanillaServicioId(transactionManager, _planillaServicioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicio"/> class.</returns>
		public MAT.Entities.PlanillaServicio GetByPlanillaServicioId(TransactionManager transactionManager, System.Guid _planillaServicioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPlanillaServicioId(transactionManager, _planillaServicioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicio index.
		/// </summary>
		/// <param name="_planillaServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicio"/> class.</returns>
		public MAT.Entities.PlanillaServicio GetByPlanillaServicioId(System.Guid _planillaServicioId, int start, int pageLength, out int count)
		{
			return GetByPlanillaServicioId(null, _planillaServicioId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicio"/> class.</returns>
		public abstract MAT.Entities.PlanillaServicio GetByPlanillaServicioId(TransactionManager transactionManager, System.Guid _planillaServicioId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;PlanillaServicio&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;PlanillaServicio&gt;"/></returns>
		public static TList<PlanillaServicio> Fill(IDataReader reader, TList<PlanillaServicio> rows, int start, int pageLength)
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
				
				MAT.Entities.PlanillaServicio c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("PlanillaServicio")
					.Append("|").Append((System.Guid)reader[((int)PlanillaServicioColumn.PlanillaServicioId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<PlanillaServicio>(
					key.ToString(), // EntityTrackingKey
					"PlanillaServicio",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.PlanillaServicio();
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
					c.PlanillaServicioId = (System.Guid)reader[((int)PlanillaServicioColumn.PlanillaServicioId - 1)];
					c.OriginalPlanillaServicioId = c.PlanillaServicioId;
					c.ViajeId = (System.Guid)reader[((int)PlanillaServicioColumn.ViajeId - 1)];
					c.FechaRegistro = (System.DateTime)reader[((int)PlanillaServicioColumn.FechaRegistro - 1)];
					c.Total = (System.Double)reader[((int)PlanillaServicioColumn.Total - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PlanillaServicio"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PlanillaServicio"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.PlanillaServicio entity)
		{
			if (!reader.Read()) return;
			
			entity.PlanillaServicioId = (System.Guid)reader[((int)PlanillaServicioColumn.PlanillaServicioId - 1)];
			entity.OriginalPlanillaServicioId = (System.Guid)reader["PlanillaServicioID"];
			entity.ViajeId = (System.Guid)reader[((int)PlanillaServicioColumn.ViajeId - 1)];
			entity.FechaRegistro = (System.DateTime)reader[((int)PlanillaServicioColumn.FechaRegistro - 1)];
			entity.Total = (System.Double)reader[((int)PlanillaServicioColumn.Total - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PlanillaServicio"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PlanillaServicio"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.PlanillaServicio entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PlanillaServicioId = (System.Guid)dataRow["PlanillaServicioID"];
			entity.OriginalPlanillaServicioId = (System.Guid)dataRow["PlanillaServicioID"];
			entity.ViajeId = (System.Guid)dataRow["ViajeID"];
			entity.FechaRegistro = (System.DateTime)dataRow["FechaRegistro"];
			entity.Total = (System.Double)dataRow["Total"];
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
		/// <param name="entity">The <see cref="MAT.Entities.PlanillaServicio"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.PlanillaServicio Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.PlanillaServicio entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByPlanillaServicioId methods when available
			
			#region PlanillaServicioItemCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PlanillaServicioItem>|PlanillaServicioItemCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PlanillaServicioItemCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PlanillaServicioItemCollection = DataRepository.PlanillaServicioItemProvider.GetByPlanillaServicioId(transactionManager, entity.PlanillaServicioId);

				if (deep && entity.PlanillaServicioItemCollection.Count > 0)
				{
					deepHandles.Add("PlanillaServicioItemCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PlanillaServicioItem>) DataRepository.PlanillaServicioItemProvider.DeepLoad,
						new object[] { transactionManager, entity.PlanillaServicioItemCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.PlanillaServicio object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.PlanillaServicio instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.PlanillaServicio Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.PlanillaServicio entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<PlanillaServicioItem>
				if (CanDeepSave(entity.PlanillaServicioItemCollection, "List<PlanillaServicioItem>|PlanillaServicioItemCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PlanillaServicioItem child in entity.PlanillaServicioItemCollection)
					{
						if(child.PlanillaServicioIdSource != null)
						{
							child.PlanillaServicioId = child.PlanillaServicioIdSource.PlanillaServicioId;
						}
						else
						{
							child.PlanillaServicioId = entity.PlanillaServicioId;
						}

					}

					if (entity.PlanillaServicioItemCollection.Count > 0 || entity.PlanillaServicioItemCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PlanillaServicioItemProvider.Save(transactionManager, entity.PlanillaServicioItemCollection);
						
						deepHandles.Add("PlanillaServicioItemCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PlanillaServicioItem >) DataRepository.PlanillaServicioItemProvider.DeepSave,
							new object[] { transactionManager, entity.PlanillaServicioItemCollection, deepSaveType, childTypes, innerList }
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
	
	#region PlanillaServicioChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.PlanillaServicio</c>
	///</summary>
	public enum PlanillaServicioChildEntityTypes
	{
		///<summary>
		/// Collection of <c>PlanillaServicio</c> as OneToMany for PlanillaServicioItemCollection
		///</summary>
		[ChildEntityType(typeof(TList<PlanillaServicioItem>))]
		PlanillaServicioItemCollection,
	}
	
	#endregion PlanillaServicioChildEntityTypes
	
	#region PlanillaServicioFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PlanillaServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioFilterBuilder : SqlFilterBuilder<PlanillaServicioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioFilterBuilder class.
		/// </summary>
		public PlanillaServicioFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaServicioFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaServicioFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaServicioFilterBuilder
	
	#region PlanillaServicioParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PlanillaServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioParameterBuilder : ParameterizedSqlFilterBuilder<PlanillaServicioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioParameterBuilder class.
		/// </summary>
		public PlanillaServicioParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaServicioParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaServicioParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaServicioParameterBuilder
	
	#region PlanillaServicioSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PlanillaServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicio"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PlanillaServicioSortBuilder : SqlSortBuilder<PlanillaServicioColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioSqlSortBuilder class.
		/// </summary>
		public PlanillaServicioSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PlanillaServicioSortBuilder
	
} // end namespace
