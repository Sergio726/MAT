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
	/// This class is the base class for any <see cref="PlanillaServicioItemProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PlanillaServicioItemProviderBaseCore : EntityProviderBase<MAT.Entities.PlanillaServicioItem, MAT.Entities.PlanillaServicioItemKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PlanillaServicioItemKey key)
		{
			return Delete(transactionManager, key.PlanillaServicioItemId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_planillaServicioItemId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _planillaServicioItemId)
		{
			return Delete(null, _planillaServicioItemId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaServicioItemId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _planillaServicioItemId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PlanillaServicioItem_Planilla key.
		///		FK_PlanillaServicioItem_Planilla Description: 
		/// </summary>
		/// <param name="_planillaId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PlanillaServicioItem objects.</returns>
		public TList<PlanillaServicioItem> GetByPlanillaId(System.Guid _planillaId)
		{
			int count = -1;
			return GetByPlanillaId(_planillaId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PlanillaServicioItem_Planilla key.
		///		FK_PlanillaServicioItem_Planilla Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.PlanillaServicioItem objects.</returns>
		/// <remarks></remarks>
		public TList<PlanillaServicioItem> GetByPlanillaId(TransactionManager transactionManager, System.Guid _planillaId)
		{
			int count = -1;
			return GetByPlanillaId(transactionManager, _planillaId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_PlanillaServicioItem_Planilla key.
		///		FK_PlanillaServicioItem_Planilla Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PlanillaServicioItem objects.</returns>
		public TList<PlanillaServicioItem> GetByPlanillaId(TransactionManager transactionManager, System.Guid _planillaId, int start, int pageLength)
		{
			int count = -1;
			return GetByPlanillaId(transactionManager, _planillaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PlanillaServicioItem_Planilla key.
		///		fkPlanillaServicioItemPlanilla Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_planillaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PlanillaServicioItem objects.</returns>
		public TList<PlanillaServicioItem> GetByPlanillaId(System.Guid _planillaId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPlanillaId(null, _planillaId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PlanillaServicioItem_Planilla key.
		///		fkPlanillaServicioItemPlanilla Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_planillaId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.PlanillaServicioItem objects.</returns>
		public TList<PlanillaServicioItem> GetByPlanillaId(System.Guid _planillaId, int start, int pageLength,out int count)
		{
			return GetByPlanillaId(null, _planillaId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PlanillaServicioItem_Planilla key.
		///		FK_PlanillaServicioItem_Planilla Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.PlanillaServicioItem objects.</returns>
		public abstract TList<PlanillaServicioItem> GetByPlanillaId(TransactionManager transactionManager, System.Guid _planillaId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.PlanillaServicioItem Get(TransactionManager transactionManager, MAT.Entities.PlanillaServicioItemKey key, int start, int pageLength)
		{
			return GetByPlanillaServicioItemId(transactionManager, key.PlanillaServicioItemId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_PlanillaServicioItem index.
		/// </summary>
		/// <param name="_planillaServicioItemId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicioItem"/> class.</returns>
		public MAT.Entities.PlanillaServicioItem GetByPlanillaServicioItemId(System.Guid _planillaServicioItemId)
		{
			int count = -1;
			return GetByPlanillaServicioItemId(null,_planillaServicioItemId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicioItem index.
		/// </summary>
		/// <param name="_planillaServicioItemId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicioItem"/> class.</returns>
		public MAT.Entities.PlanillaServicioItem GetByPlanillaServicioItemId(System.Guid _planillaServicioItemId, int start, int pageLength)
		{
			int count = -1;
			return GetByPlanillaServicioItemId(null, _planillaServicioItemId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicioItem index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaServicioItemId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicioItem"/> class.</returns>
		public MAT.Entities.PlanillaServicioItem GetByPlanillaServicioItemId(TransactionManager transactionManager, System.Guid _planillaServicioItemId)
		{
			int count = -1;
			return GetByPlanillaServicioItemId(transactionManager, _planillaServicioItemId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicioItem index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaServicioItemId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicioItem"/> class.</returns>
		public MAT.Entities.PlanillaServicioItem GetByPlanillaServicioItemId(TransactionManager transactionManager, System.Guid _planillaServicioItemId, int start, int pageLength)
		{
			int count = -1;
			return GetByPlanillaServicioItemId(transactionManager, _planillaServicioItemId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicioItem index.
		/// </summary>
		/// <param name="_planillaServicioItemId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicioItem"/> class.</returns>
		public MAT.Entities.PlanillaServicioItem GetByPlanillaServicioItemId(System.Guid _planillaServicioItemId, int start, int pageLength, out int count)
		{
			return GetByPlanillaServicioItemId(null, _planillaServicioItemId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PlanillaServicioItem index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_planillaServicioItemId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PlanillaServicioItem"/> class.</returns>
		public abstract MAT.Entities.PlanillaServicioItem GetByPlanillaServicioItemId(TransactionManager transactionManager, System.Guid _planillaServicioItemId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;PlanillaServicioItem&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;PlanillaServicioItem&gt;"/></returns>
		public static TList<PlanillaServicioItem> Fill(IDataReader reader, TList<PlanillaServicioItem> rows, int start, int pageLength)
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
				
				MAT.Entities.PlanillaServicioItem c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("PlanillaServicioItem")
					.Append("|").Append((System.Guid)reader[((int)PlanillaServicioItemColumn.PlanillaServicioItemId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<PlanillaServicioItem>(
					key.ToString(), // EntityTrackingKey
					"PlanillaServicioItem",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.PlanillaServicioItem();
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
					c.PlanillaServicioItemId = (System.Guid)reader[((int)PlanillaServicioItemColumn.PlanillaServicioItemId - 1)];
					c.OriginalPlanillaServicioItemId = c.PlanillaServicioItemId;
					c.PlanillaId = (System.Guid)reader[((int)PlanillaServicioItemColumn.PlanillaId - 1)];
					c.ServicioId = (System.Guid)reader[((int)PlanillaServicioItemColumn.ServicioId - 1)];
					c.Cantidad = (System.Int32)reader[((int)PlanillaServicioItemColumn.Cantidad - 1)];
					c.Subtotal = (System.Double)reader[((int)PlanillaServicioItemColumn.Subtotal - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PlanillaServicioItem"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PlanillaServicioItem"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.PlanillaServicioItem entity)
		{
			if (!reader.Read()) return;
			
			entity.PlanillaServicioItemId = (System.Guid)reader[((int)PlanillaServicioItemColumn.PlanillaServicioItemId - 1)];
			entity.OriginalPlanillaServicioItemId = (System.Guid)reader["PlanillaServicioItemID"];
			entity.PlanillaId = (System.Guid)reader[((int)PlanillaServicioItemColumn.PlanillaId - 1)];
			entity.ServicioId = (System.Guid)reader[((int)PlanillaServicioItemColumn.ServicioId - 1)];
			entity.Cantidad = (System.Int32)reader[((int)PlanillaServicioItemColumn.Cantidad - 1)];
			entity.Subtotal = (System.Double)reader[((int)PlanillaServicioItemColumn.Subtotal - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PlanillaServicioItem"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PlanillaServicioItem"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.PlanillaServicioItem entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PlanillaServicioItemId = (System.Guid)dataRow["PlanillaServicioItemID"];
			entity.OriginalPlanillaServicioItemId = (System.Guid)dataRow["PlanillaServicioItemID"];
			entity.PlanillaId = (System.Guid)dataRow["PlanillaID"];
			entity.ServicioId = (System.Guid)dataRow["ServicioID"];
			entity.Cantidad = (System.Int32)dataRow["Cantidad"];
			entity.Subtotal = (System.Double)dataRow["Subtotal"];
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
		/// <param name="entity">The <see cref="MAT.Entities.PlanillaServicioItem"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.PlanillaServicioItem Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.PlanillaServicioItem entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region PlanillaIdSource	
			if (CanDeepLoad(entity, "Planilla|PlanillaIdSource", deepLoadType, innerList) 
				&& entity.PlanillaIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.PlanillaId;
				Planilla tmpEntity = EntityManager.LocateEntity<Planilla>(EntityLocator.ConstructKeyFromPkItems(typeof(Planilla), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PlanillaIdSource = tmpEntity;
				else
					entity.PlanillaIdSource = DataRepository.PlanillaProvider.GetByPlanillaId(transactionManager, entity.PlanillaId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PlanillaIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PlanillaIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PlanillaProvider.DeepLoad(transactionManager, entity.PlanillaIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PlanillaIdSource
			
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
		/// Deep Save the entire object graph of the MAT.Entities.PlanillaServicioItem object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.PlanillaServicioItem instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.PlanillaServicioItem Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.PlanillaServicioItem entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region PlanillaIdSource
			if (CanDeepSave(entity, "Planilla|PlanillaIdSource", deepSaveType, innerList) 
				&& entity.PlanillaIdSource != null)
			{
				DataRepository.PlanillaProvider.Save(transactionManager, entity.PlanillaIdSource);
				entity.PlanillaId = entity.PlanillaIdSource.PlanillaId;
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
	
	#region PlanillaServicioItemChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.PlanillaServicioItem</c>
	///</summary>
	public enum PlanillaServicioItemChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Planilla</c> at PlanillaIdSource
		///</summary>
		[ChildEntityType(typeof(Planilla))]
		Planilla,
	}
	
	#endregion PlanillaServicioItemChildEntityTypes
	
	#region PlanillaServicioItemFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PlanillaServicioItemColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicioItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioItemFilterBuilder : SqlFilterBuilder<PlanillaServicioItemColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemFilterBuilder class.
		/// </summary>
		public PlanillaServicioItemFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaServicioItemFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaServicioItemFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaServicioItemFilterBuilder
	
	#region PlanillaServicioItemParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PlanillaServicioItemColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicioItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioItemParameterBuilder : ParameterizedSqlFilterBuilder<PlanillaServicioItemColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemParameterBuilder class.
		/// </summary>
		public PlanillaServicioItemParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PlanillaServicioItemParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PlanillaServicioItemParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PlanillaServicioItemParameterBuilder
	
	#region PlanillaServicioItemSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PlanillaServicioItemColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicioItem"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PlanillaServicioItemSortBuilder : SqlSortBuilder<PlanillaServicioItemColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemSqlSortBuilder class.
		/// </summary>
		public PlanillaServicioItemSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PlanillaServicioItemSortBuilder
	
} // end namespace
