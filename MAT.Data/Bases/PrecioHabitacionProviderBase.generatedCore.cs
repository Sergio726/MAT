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
	/// This class is the base class for any <see cref="PrecioHabitacionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PrecioHabitacionProviderBaseCore : EntityProviderBase<MAT.Entities.PrecioHabitacion, MAT.Entities.PrecioHabitacionKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PrecioHabitacionKey key)
		{
			return Delete(transactionManager, key.PrecioHabitacionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_precioHabitacionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _precioHabitacionId)
		{
			return Delete(null, _precioHabitacionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioHabitacionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _precioHabitacionId);		
		
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
		public override MAT.Entities.PrecioHabitacion Get(TransactionManager transactionManager, MAT.Entities.PrecioHabitacionKey key, int start, int pageLength)
		{
			return GetByPrecioHabitacionId(transactionManager, key.PrecioHabitacionId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_PrecioHabitacion index.
		/// </summary>
		/// <param name="_precioHabitacionId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioHabitacion"/> class.</returns>
		public MAT.Entities.PrecioHabitacion GetByPrecioHabitacionId(System.Guid _precioHabitacionId)
		{
			int count = -1;
			return GetByPrecioHabitacionId(null,_precioHabitacionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PrecioHabitacion index.
		/// </summary>
		/// <param name="_precioHabitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioHabitacion"/> class.</returns>
		public MAT.Entities.PrecioHabitacion GetByPrecioHabitacionId(System.Guid _precioHabitacionId, int start, int pageLength)
		{
			int count = -1;
			return GetByPrecioHabitacionId(null, _precioHabitacionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PrecioHabitacion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioHabitacionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioHabitacion"/> class.</returns>
		public MAT.Entities.PrecioHabitacion GetByPrecioHabitacionId(TransactionManager transactionManager, System.Guid _precioHabitacionId)
		{
			int count = -1;
			return GetByPrecioHabitacionId(transactionManager, _precioHabitacionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PrecioHabitacion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioHabitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioHabitacion"/> class.</returns>
		public MAT.Entities.PrecioHabitacion GetByPrecioHabitacionId(TransactionManager transactionManager, System.Guid _precioHabitacionId, int start, int pageLength)
		{
			int count = -1;
			return GetByPrecioHabitacionId(transactionManager, _precioHabitacionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PrecioHabitacion index.
		/// </summary>
		/// <param name="_precioHabitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioHabitacion"/> class.</returns>
		public MAT.Entities.PrecioHabitacion GetByPrecioHabitacionId(System.Guid _precioHabitacionId, int start, int pageLength, out int count)
		{
			return GetByPrecioHabitacionId(null, _precioHabitacionId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_PrecioHabitacion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioHabitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.PrecioHabitacion"/> class.</returns>
		public abstract MAT.Entities.PrecioHabitacion GetByPrecioHabitacionId(TransactionManager transactionManager, System.Guid _precioHabitacionId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;PrecioHabitacion&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;PrecioHabitacion&gt;"/></returns>
		public static TList<PrecioHabitacion> Fill(IDataReader reader, TList<PrecioHabitacion> rows, int start, int pageLength)
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
				
				MAT.Entities.PrecioHabitacion c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("PrecioHabitacion")
					.Append("|").Append((System.Guid)reader[((int)PrecioHabitacionColumn.PrecioHabitacionId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<PrecioHabitacion>(
					key.ToString(), // EntityTrackingKey
					"PrecioHabitacion",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.PrecioHabitacion();
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
					c.PrecioHabitacionId = (System.Guid)reader[((int)PrecioHabitacionColumn.PrecioHabitacionId - 1)];
					c.OriginalPrecioHabitacionId = c.PrecioHabitacionId;
					c.TipoHabitacion = (System.Int32)reader[((int)PrecioHabitacionColumn.TipoHabitacion - 1)];
					c.HotelId = (System.Guid)reader[((int)PrecioHabitacionColumn.HotelId - 1)];
					c.FechaRegistro = (System.DateTime)reader[((int)PrecioHabitacionColumn.FechaRegistro - 1)];
					c.Activo = (System.Boolean)reader[((int)PrecioHabitacionColumn.Activo - 1)];
					c.Precio = (System.Double)reader[((int)PrecioHabitacionColumn.Precio - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PrecioHabitacion"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PrecioHabitacion"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.PrecioHabitacion entity)
		{
			if (!reader.Read()) return;
			
			entity.PrecioHabitacionId = (System.Guid)reader[((int)PrecioHabitacionColumn.PrecioHabitacionId - 1)];
			entity.OriginalPrecioHabitacionId = (System.Guid)reader["PrecioHabitacionID"];
			entity.TipoHabitacion = (System.Int32)reader[((int)PrecioHabitacionColumn.TipoHabitacion - 1)];
			entity.HotelId = (System.Guid)reader[((int)PrecioHabitacionColumn.HotelId - 1)];
			entity.FechaRegistro = (System.DateTime)reader[((int)PrecioHabitacionColumn.FechaRegistro - 1)];
			entity.Activo = (System.Boolean)reader[((int)PrecioHabitacionColumn.Activo - 1)];
			entity.Precio = (System.Double)reader[((int)PrecioHabitacionColumn.Precio - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.PrecioHabitacion"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.PrecioHabitacion"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.PrecioHabitacion entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PrecioHabitacionId = (System.Guid)dataRow["PrecioHabitacionID"];
			entity.OriginalPrecioHabitacionId = (System.Guid)dataRow["PrecioHabitacionID"];
			entity.TipoHabitacion = (System.Int32)dataRow["TipoHabitacion"];
			entity.HotelId = (System.Guid)dataRow["HotelID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.PrecioHabitacion"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.PrecioHabitacion Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.PrecioHabitacion entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the MAT.Entities.PrecioHabitacion object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.PrecioHabitacion instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.PrecioHabitacion Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.PrecioHabitacion entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region PrecioHabitacionChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.PrecioHabitacion</c>
	///</summary>
	public enum PrecioHabitacionChildEntityTypes
	{
	}
	
	#endregion PrecioHabitacionChildEntityTypes
	
	#region PrecioHabitacionFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PrecioHabitacionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioHabitacionFilterBuilder : SqlFilterBuilder<PrecioHabitacionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionFilterBuilder class.
		/// </summary>
		public PrecioHabitacionFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioHabitacionFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioHabitacionFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioHabitacionFilterBuilder
	
	#region PrecioHabitacionParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PrecioHabitacionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioHabitacionParameterBuilder : ParameterizedSqlFilterBuilder<PrecioHabitacionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionParameterBuilder class.
		/// </summary>
		public PrecioHabitacionParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioHabitacionParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioHabitacionParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioHabitacionParameterBuilder
	
	#region PrecioHabitacionSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PrecioHabitacionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioHabitacion"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PrecioHabitacionSortBuilder : SqlSortBuilder<PrecioHabitacionColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioHabitacionSqlSortBuilder class.
		/// </summary>
		public PrecioHabitacionSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PrecioHabitacionSortBuilder
	
} // end namespace
