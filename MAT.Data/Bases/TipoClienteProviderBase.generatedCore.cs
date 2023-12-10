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
	/// This class is the base class for any <see cref="TipoClienteProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class TipoClienteProviderBaseCore : EntityProviderBase<MAT.Entities.TipoCliente, MAT.Entities.TipoClienteKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.TipoClienteKey key)
		{
			return Delete(transactionManager, key.TipoId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_tipoId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _tipoId)
		{
			return Delete(null, _tipoId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_tipoId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _tipoId);		
		
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
		public override MAT.Entities.TipoCliente Get(TransactionManager transactionManager, MAT.Entities.TipoClienteKey key, int start, int pageLength)
		{
			return GetByTipoId(transactionManager, key.TipoId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key AK_Descripcion index.
		/// </summary>
		/// <param name="_descripcion"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public MAT.Entities.TipoCliente GetByDescripcion(System.String _descripcion)
		{
			int count = -1;
			return GetByDescripcion(null,_descripcion, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the AK_Descripcion index.
		/// </summary>
		/// <param name="_descripcion"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public MAT.Entities.TipoCliente GetByDescripcion(System.String _descripcion, int start, int pageLength)
		{
			int count = -1;
			return GetByDescripcion(null, _descripcion, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the AK_Descripcion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_descripcion"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public MAT.Entities.TipoCliente GetByDescripcion(TransactionManager transactionManager, System.String _descripcion)
		{
			int count = -1;
			return GetByDescripcion(transactionManager, _descripcion, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the AK_Descripcion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_descripcion"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public MAT.Entities.TipoCliente GetByDescripcion(TransactionManager transactionManager, System.String _descripcion, int start, int pageLength)
		{
			int count = -1;
			return GetByDescripcion(transactionManager, _descripcion, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the AK_Descripcion index.
		/// </summary>
		/// <param name="_descripcion"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public MAT.Entities.TipoCliente GetByDescripcion(System.String _descripcion, int start, int pageLength, out int count)
		{
			return GetByDescripcion(null, _descripcion, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the AK_Descripcion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_descripcion"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public abstract MAT.Entities.TipoCliente GetByDescripcion(TransactionManager transactionManager, System.String _descripcion, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK__TipoClie__97099E97D44FBFE9 index.
		/// </summary>
		/// <param name="_tipoId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public MAT.Entities.TipoCliente GetByTipoId(System.Int32 _tipoId)
		{
			int count = -1;
			return GetByTipoId(null,_tipoId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__TipoClie__97099E97D44FBFE9 index.
		/// </summary>
		/// <param name="_tipoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public MAT.Entities.TipoCliente GetByTipoId(System.Int32 _tipoId, int start, int pageLength)
		{
			int count = -1;
			return GetByTipoId(null, _tipoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__TipoClie__97099E97D44FBFE9 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_tipoId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public MAT.Entities.TipoCliente GetByTipoId(TransactionManager transactionManager, System.Int32 _tipoId)
		{
			int count = -1;
			return GetByTipoId(transactionManager, _tipoId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__TipoClie__97099E97D44FBFE9 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_tipoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public MAT.Entities.TipoCliente GetByTipoId(TransactionManager transactionManager, System.Int32 _tipoId, int start, int pageLength)
		{
			int count = -1;
			return GetByTipoId(transactionManager, _tipoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__TipoClie__97099E97D44FBFE9 index.
		/// </summary>
		/// <param name="_tipoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public MAT.Entities.TipoCliente GetByTipoId(System.Int32 _tipoId, int start, int pageLength, out int count)
		{
			return GetByTipoId(null, _tipoId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__TipoClie__97099E97D44FBFE9 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_tipoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.TipoCliente"/> class.</returns>
		public abstract MAT.Entities.TipoCliente GetByTipoId(TransactionManager transactionManager, System.Int32 _tipoId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;TipoCliente&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;TipoCliente&gt;"/></returns>
		public static TList<TipoCliente> Fill(IDataReader reader, TList<TipoCliente> rows, int start, int pageLength)
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
				
				MAT.Entities.TipoCliente c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("TipoCliente")
					.Append("|").Append((System.Int32)reader[((int)TipoClienteColumn.TipoId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<TipoCliente>(
					key.ToString(), // EntityTrackingKey
					"TipoCliente",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.TipoCliente();
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
					c.TipoId = (System.Int32)reader[((int)TipoClienteColumn.TipoId - 1)];
					c.OriginalTipoId = c.TipoId;
					c.Descripcion = (reader.IsDBNull(((int)TipoClienteColumn.Descripcion - 1)))?null:(System.String)reader[((int)TipoClienteColumn.Descripcion - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.TipoCliente"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.TipoCliente"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.TipoCliente entity)
		{
			if (!reader.Read()) return;
			
			entity.TipoId = (System.Int32)reader[((int)TipoClienteColumn.TipoId - 1)];
			entity.OriginalTipoId = (System.Int32)reader["TipoID"];
			entity.Descripcion = (reader.IsDBNull(((int)TipoClienteColumn.Descripcion - 1)))?null:(System.String)reader[((int)TipoClienteColumn.Descripcion - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.TipoCliente"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.TipoCliente"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.TipoCliente entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.TipoId = (System.Int32)dataRow["TipoID"];
			entity.OriginalTipoId = (System.Int32)dataRow["TipoID"];
			entity.Descripcion = Convert.IsDBNull(dataRow["Descripcion"]) ? null : (System.String)dataRow["Descripcion"];
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
		/// <param name="entity">The <see cref="MAT.Entities.TipoCliente"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.TipoCliente Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.TipoCliente entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByTipoId methods when available
			
			#region ClienteCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Cliente>|ClienteCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ClienteCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ClienteCollection = DataRepository.ClienteProvider.GetByTipoId(transactionManager, entity.TipoId);

				if (deep && entity.ClienteCollection.Count > 0)
				{
					deepHandles.Add("ClienteCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Cliente>) DataRepository.ClienteProvider.DeepLoad,
						new object[] { transactionManager, entity.ClienteCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.TipoCliente object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.TipoCliente instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.TipoCliente Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.TipoCliente entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<Cliente>
				if (CanDeepSave(entity.ClienteCollection, "List<Cliente>|ClienteCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Cliente child in entity.ClienteCollection)
					{
						if(child.TipoIdSource != null)
						{
							child.TipoId = child.TipoIdSource.TipoId;
						}
						else
						{
							child.TipoId = entity.TipoId;
						}

					}

					if (entity.ClienteCollection.Count > 0 || entity.ClienteCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ClienteProvider.Save(transactionManager, entity.ClienteCollection);
						
						deepHandles.Add("ClienteCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Cliente >) DataRepository.ClienteProvider.DeepSave,
							new object[] { transactionManager, entity.ClienteCollection, deepSaveType, childTypes, innerList }
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
	
	#region TipoClienteChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.TipoCliente</c>
	///</summary>
	public enum TipoClienteChildEntityTypes
	{
		///<summary>
		/// Collection of <c>TipoCliente</c> as OneToMany for ClienteCollection
		///</summary>
		[ChildEntityType(typeof(TList<Cliente>))]
		ClienteCollection,
	}
	
	#endregion TipoClienteChildEntityTypes
	
	#region TipoClienteFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;TipoClienteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="TipoCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TipoClienteFilterBuilder : SqlFilterBuilder<TipoClienteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TipoClienteFilterBuilder class.
		/// </summary>
		public TipoClienteFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the TipoClienteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public TipoClienteFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the TipoClienteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public TipoClienteFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion TipoClienteFilterBuilder
	
	#region TipoClienteParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;TipoClienteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="TipoCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TipoClienteParameterBuilder : ParameterizedSqlFilterBuilder<TipoClienteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TipoClienteParameterBuilder class.
		/// </summary>
		public TipoClienteParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the TipoClienteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public TipoClienteParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the TipoClienteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public TipoClienteParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion TipoClienteParameterBuilder
	
	#region TipoClienteSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;TipoClienteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="TipoCliente"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class TipoClienteSortBuilder : SqlSortBuilder<TipoClienteColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TipoClienteSqlSortBuilder class.
		/// </summary>
		public TipoClienteSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion TipoClienteSortBuilder
	
} // end namespace
