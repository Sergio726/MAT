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
	/// This class is the base class for any <see cref="PrecioServicioProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PrecioServicioProviderBaseCore : EntityProviderBase<MAT.Entities.PrecioServicio, MAT.Entities.PrecioServicioKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PrecioServicioKey key)
		{
			return Delete(transactionManager, key.PrecioServicioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_precioServicioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _precioServicioId)
		{
			return Delete(null, _precioServicioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioServicioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _precioServicioId);		
		
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
		public override MAT.Entities.PrecioServicio Get(TransactionManager transactionManager, MAT.Entities.PrecioServicioKey key, int start, int pageLength)
		{
			return GetByPrecioServicioId(transactionManager, key.PrecioServicioId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_PrecioServicio index.
		/// </summary>
		/// <param name="_precioServicioId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioServicio"/> class.</returns>
		public MAT.Entities.PrecioServicio GetByPrecioServicioId(System.Guid _precioServicioId)
		{
			int count = -1;
			return GetByPrecioServicioId(null,_precioServicioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PrecioServicio index.
		/// </summary>
		/// <param name="_precioServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioServicio"/> class.</returns>
		public MAT.Entities.PrecioServicio GetByPrecioServicioId(System.Guid _precioServicioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPrecioServicioId(null, _precioServicioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PrecioServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioServicioId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioServicio"/> class.</returns>
		public MAT.Entities.PrecioServicio GetByPrecioServicioId(TransactionManager transactionManager, System.Guid _precioServicioId)
		{
			int count = -1;
			return GetByPrecioServicioId(transactionManager, _precioServicioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PrecioServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioServicio"/> class.</returns>
		public MAT.Entities.PrecioServicio GetByPrecioServicioId(TransactionManager transactionManager, System.Guid _precioServicioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPrecioServicioId(transactionManager, _precioServicioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PrecioServicio index.
		/// </summary>
		/// <param name="_precioServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioServicio"/> class.</returns>
		public MAT.Entities.PrecioServicio GetByPrecioServicioId(System.Guid _precioServicioId, int start, int pageLength, out int count)
		{
			return GetByPrecioServicioId(null, _precioServicioId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PrecioServicio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioServicioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioServicio"/> class.</returns>
		public abstract MAT.Entities.PrecioServicio GetByPrecioServicioId(TransactionManager transactionManager, System.Guid _precioServicioId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;PrecioServicio&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;PrecioServicio&gt;"/></returns>
		public static TList<PrecioServicio> Fill(IDataReader reader, TList<PrecioServicio> rows, int start, int pageLength)
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
				
				MAT.Entities.PrecioServicio c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("PrecioServicio")
					.Append("|").Append((System.Guid)reader[((int)PrecioServicioColumn.PrecioServicioId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<PrecioServicio>(
					key.ToString(), // EntityTrackingKey
					"PrecioServicio",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.PrecioServicio();
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
					c.PrecioServicioId = (System.Guid)reader[((int)PrecioServicioColumn.PrecioServicioId - 1)];
					c.OriginalPrecioServicioId = c.PrecioServicioId;
					c.ServicioId = (System.Guid)reader[((int)PrecioServicioColumn.ServicioId - 1)];
					c.FechaRegistro = (System.DateTime)reader[((int)PrecioServicioColumn.FechaRegistro - 1)];
					c.Activo = (System.Boolean)reader[((int)PrecioServicioColumn.Activo - 1)];
					c.Precio = (System.Double)reader[((int)PrecioServicioColumn.Precio - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PrecioServicio"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PrecioServicio"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.PrecioServicio entity)
		{
			if (!reader.Read()) return;
			
			entity.PrecioServicioId = (System.Guid)reader[((int)PrecioServicioColumn.PrecioServicioId - 1)];
			entity.OriginalPrecioServicioId = (System.Guid)reader["PrecioServicioID"];
			entity.ServicioId = (System.Guid)reader[((int)PrecioServicioColumn.ServicioId - 1)];
			entity.FechaRegistro = (System.DateTime)reader[((int)PrecioServicioColumn.FechaRegistro - 1)];
			entity.Activo = (System.Boolean)reader[((int)PrecioServicioColumn.Activo - 1)];
			entity.Precio = (System.Double)reader[((int)PrecioServicioColumn.Precio - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PrecioServicio"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PrecioServicio"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.PrecioServicio entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PrecioServicioId = (System.Guid)dataRow["PrecioServicioID"];
			entity.OriginalPrecioServicioId = (System.Guid)dataRow["PrecioServicioID"];
			entity.ServicioId = (System.Guid)dataRow["ServicioID"];
			entity.FechaRegistro = (System.DateTime)dataRow["FechaRegistro"];
			entity.Activo = (System.Boolean)dataRow["Activo"];
			entity.Precio = (System.Double)dataRow["Precio"];
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
		/// <param name="entity">The <see cref="MAT.Entities.PrecioServicio"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.PrecioServicio Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.PrecioServicio entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
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
		/// Deep Save the entire object graph of the MAT.Entities.PrecioServicio object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.PrecioServicio instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.PrecioServicio Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.PrecioServicio entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region PrecioServicioChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.PrecioServicio</c>
	///</summary>
	public enum PrecioServicioChildEntityTypes
	{
	}
	
	#endregion PrecioServicioChildEntityTypes
	
	#region PrecioServicioFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PrecioServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioServicioFilterBuilder : SqlFilterBuilder<PrecioServicioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioServicioFilterBuilder class.
		/// </summary>
		public PrecioServicioFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioServicioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioServicioFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioServicioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioServicioFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioServicioFilterBuilder
	
	#region PrecioServicioParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PrecioServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioServicioParameterBuilder : ParameterizedSqlFilterBuilder<PrecioServicioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioServicioParameterBuilder class.
		/// </summary>
		public PrecioServicioParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioServicioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioServicioParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioServicioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioServicioParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioServicioParameterBuilder
	
	#region PrecioServicioSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PrecioServicioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioServicio"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PrecioServicioSortBuilder : SqlSortBuilder<PrecioServicioColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioServicioSqlSortBuilder class.
		/// </summary>
		public PrecioServicioSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PrecioServicioSortBuilder
	
} // end namespace
