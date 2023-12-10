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
	/// This class is the base class for any <see cref="PlanillaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PlanillaProviderBaseCore : EntityProviderBase<MAT.Entities.Planilla, MAT.Entities.PlanillaKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PlanillaKey key)
		{
			return Delete(transactionManager, key.PlanillaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_planillaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _planillaId)
		{
			return Delete(null, _planillaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _planillaId);		
		
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
		public override MAT.Entities.Planilla Get(TransactionManager transactionManager, MAT.Entities.PlanillaKey key, int start, int pageLength)
		{
			return GetByPlanillaId(transactionManager, key.PlanillaId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_PlanillaServicio index.
		/// </summary>
		/// <param name="_planillaId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Planilla"/> class.</returns>
		public MAT.Entities.Planilla GetByPlanillaId(System.Guid _planillaId)
		{
			int count = -1;
			return GetByPlanillaId(null,_planillaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicio index.
		/// </summary>
		/// <param name="_planillaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Planilla"/> class.</returns>
		public MAT.Entities.Planilla GetByPlanillaId(System.Guid _planillaId, int start, int pageLength)
		{
			int count = -1;
			return GetByPlanillaId(null, _planillaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Planilla"/> class.</returns>
		public MAT.Entities.Planilla GetByPlanillaId(TransactionManager transactionManager, System.Guid _planillaId)
		{
			int count = -1;
			return GetByPlanillaId(transactionManager, _planillaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Planilla"/> class.</returns>
		public MAT.Entities.Planilla GetByPlanillaId(TransactionManager transactionManager, System.Guid _planillaId, int start, int pageLength)
		{
			int count = -1;
			return GetByPlanillaId(transactionManager, _planillaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicio index.
		/// </summary>
		/// <param name="_planillaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Planilla"/> class.</returns>
		public MAT.Entities.Planilla GetByPlanillaId(System.Guid _planillaId, int start, int pageLength, out int count)
		{
			return GetByPlanillaId(null, _planillaId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Planilla"/> class.</returns>
		public abstract MAT.Entities.Planilla GetByPlanillaId(TransactionManager transactionManager, System.Guid _planillaId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Planilla&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Planilla&gt;"/></returns>
		public static TList<Planilla> Fill(IDataReader reader, TList<Planilla> rows, int start, int pageLength)
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
				
				MAT.Entities.Planilla c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Planilla")
					.Append("|").Append((System.Guid)reader[((int)PlanillaColumn.PlanillaId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Planilla>(
					key.ToString(), // EntityTrackingKey
					"Planilla",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Planilla();
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
					c.PlanillaId = (System.Guid)reader[((int)PlanillaColumn.PlanillaId - 1)];
					c.OriginalPlanillaId = c.PlanillaId;
					c.ViajeId = (System.Guid)reader[((int)PlanillaColumn.ViajeId - 1)];
					c.FechaRegistro = (System.DateTime)reader[((int)PlanillaColumn.FechaRegistro - 1)];
					c.Total = (System.Double)reader[((int)PlanillaColumn.Total - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Planilla"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Planilla"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Planilla entity)
		{
			if (!reader.Read()) return;
			
			entity.PlanillaId = (System.Guid)reader[((int)PlanillaColumn.PlanillaId - 1)];
			entity.OriginalPlanillaId = (System.Guid)reader["PlanillaID"];
			entity.ViajeId = (System.Guid)reader[((int)PlanillaColumn.ViajeId - 1)];
			entity.FechaRegistro = (System.DateTime)reader[((int)PlanillaColumn.FechaRegistro - 1)];
			entity.Total = (System.Double)reader[((int)PlanillaColumn.Total - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Planilla"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Planilla"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Planilla entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PlanillaId = (System.Guid)dataRow["PlanillaID"];
			entity.OriginalPlanillaId = (System.Guid)dataRow["PlanillaID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Planilla"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Planilla Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Planilla entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByPlanillaId methods when available
			
			#region PlanillaServicioItemCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PlanillaServicioItem>|PlanillaServicioItemCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PlanillaServicioItemCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PlanillaServicioItemCollection = DataRepository.PlanillaServicioItemProvider.GetByPlanillaId(transactionManager, entity.PlanillaId);

				if (deep && entity.PlanillaServicioItemCollection.Count > 0)
				{
					deepHandles.Add("PlanillaServicioItemCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PlanillaServicioItem>) DataRepository.PlanillaServicioItemProvider.DeepLoad,
						new object[] { transactionManager, entity.PlanillaServicioItemCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PlanillaHabitacionItemCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PlanillaHabitacionItem>|PlanillaHabitacionItemCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PlanillaHabitacionItemCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PlanillaHabitacionItemCollection = DataRepository.PlanillaHabitacionItemProvider.GetByPlanillaId(transactionManager, entity.PlanillaId);

				if (deep && entity.PlanillaHabitacionItemCollection.Count > 0)
				{
					deepHandles.Add("PlanillaHabitacionItemCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PlanillaHabitacionItem>) DataRepository.PlanillaHabitacionItemProvider.DeepLoad,
						new object[] { transactionManager, entity.PlanillaHabitacionItemCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Planilla object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Planilla instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Planilla Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Planilla entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
						if(child.PlanillaIdSource != null)
						{
							child.PlanillaId = child.PlanillaIdSource.PlanillaId;
						}
						else
						{
							child.PlanillaId = entity.PlanillaId;
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
				
	
			#region List<PlanillaHabitacionItem>
				if (CanDeepSave(entity.PlanillaHabitacionItemCollection, "List<PlanillaHabitacionItem>|PlanillaHabitacionItemCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PlanillaHabitacionItem child in entity.PlanillaHabitacionItemCollection)
					{
						if(child.PlanillaIdSource != null)
						{
							child.PlanillaId = child.PlanillaIdSource.PlanillaId;
						}
						else
						{
							child.PlanillaId = entity.PlanillaId;
						}

					}

					if (entity.PlanillaHabitacionItemCollection.Count > 0 || entity.PlanillaHabitacionItemCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PlanillaHabitacionItemProvider.Save(transactionManager, entity.PlanillaHabitacionItemCollection);
						
						deepHandles.Add("PlanillaHabitacionItemCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PlanillaHabitacionItem >) DataRepository.PlanillaHabitacionItemProvider.DeepSave,
							new object[] { transactionManager, entity.PlanillaHabitacionItemCollection, deepSaveType, childTypes, innerList }
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
	
	#region PlanillaChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Planilla</c>
	///</summary>
	public enum PlanillaChildEntityTypes
	{
		///<summary>
		/// Collection of <c>Planilla</c> as OneToMany for PlanillaServicioItemCollection
		///</summary>
		[ChildEntityType(typeof(TList<PlanillaServicioItem>))]
		PlanillaServicioItemCollection,
		///<summary>
		/// Collection of <c>Planilla</c> as OneToMany for PlanillaHabitacionItemCollection
		///</summary>
		[ChildEntityType(typeof(TList<PlanillaHabitacionItem>))]
		PlanillaHabitacionItemCollection,
	}
	
	#endregion PlanillaChildEntityTypes
	
	#region PlanillaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PlanillaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Planilla"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaFilterBuilder : SqlFilterBuilder<PlanillaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaFilterBuilder class.
		/// </summary>
		public PlanillaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaFilterBuilder
	
	#region PlanillaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PlanillaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Planilla"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaParameterBuilder : ParameterizedSqlFilterBuilder<PlanillaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaParameterBuilder class.
		/// </summary>
		public PlanillaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaParameterBuilder
	
	#region PlanillaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PlanillaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Planilla"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PlanillaSortBuilder : SqlSortBuilder<PlanillaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaSqlSortBuilder class.
		/// </summary>
		public PlanillaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PlanillaSortBuilder
	
} // end namespace
