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
	/// This class is the base class for any <see cref="HistorialProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class HistorialProviderBaseCore : EntityProviderBase<MAT.Entities.Historial, MAT.Entities.HistorialKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.HistorialKey key)
		{
			return Delete(transactionManager, key.HistorialId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_historialId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _historialId)
		{
			return Delete(null, _historialId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_historialId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _historialId);		
		
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
		public override MAT.Entities.Historial Get(TransactionManager transactionManager, MAT.Entities.HistorialKey key, int start, int pageLength)
		{
			return GetByHistorialId(transactionManager, key.HistorialId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Historial index.
		/// </summary>
		/// <param name="_historialId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Historial"/> class.</returns>
		public MAT.Entities.Historial GetByHistorialId(System.Guid _historialId)
		{
			int count = -1;
			return GetByHistorialId(null,_historialId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Historial index.
		/// </summary>
		/// <param name="_historialId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Historial"/> class.</returns>
		public MAT.Entities.Historial GetByHistorialId(System.Guid _historialId, int start, int pageLength)
		{
			int count = -1;
			return GetByHistorialId(null, _historialId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Historial index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_historialId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Historial"/> class.</returns>
		public MAT.Entities.Historial GetByHistorialId(TransactionManager transactionManager, System.Guid _historialId)
		{
			int count = -1;
			return GetByHistorialId(transactionManager, _historialId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Historial index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_historialId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Historial"/> class.</returns>
		public MAT.Entities.Historial GetByHistorialId(TransactionManager transactionManager, System.Guid _historialId, int start, int pageLength)
		{
			int count = -1;
			return GetByHistorialId(transactionManager, _historialId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Historial index.
		/// </summary>
		/// <param name="_historialId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Historial"/> class.</returns>
		public MAT.Entities.Historial GetByHistorialId(System.Guid _historialId, int start, int pageLength, out int count)
		{
			return GetByHistorialId(null, _historialId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Historial index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_historialId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Historial"/> class.</returns>
		public abstract MAT.Entities.Historial GetByHistorialId(TransactionManager transactionManager, System.Guid _historialId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Historial&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Historial&gt;"/></returns>
		public static TList<Historial> Fill(IDataReader reader, TList<Historial> rows, int start, int pageLength)
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
				
				MAT.Entities.Historial c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Historial")
					.Append("|").Append((System.Guid)reader[((int)HistorialColumn.HistorialId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Historial>(
					key.ToString(), // EntityTrackingKey
					"Historial",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Historial();
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
					c.HistorialId = (System.Guid)reader[((int)HistorialColumn.HistorialId - 1)];
					c.OriginalHistorialId = c.HistorialId;
					c.Tabla = (System.Int32)reader[((int)HistorialColumn.Tabla - 1)];
					c.Operacion = (System.Int32)reader[((int)HistorialColumn.Operacion - 1)];
					c.FechaHoraRegistro = (System.DateTime)reader[((int)HistorialColumn.FechaHoraRegistro - 1)];
					c.Cliente = (System.Guid)reader[((int)HistorialColumn.Cliente - 1)];
					c.Vendedor = (System.Guid)reader[((int)HistorialColumn.Vendedor - 1)];
					c.Observaciones = (reader.IsDBNull(((int)HistorialColumn.Observaciones - 1)))?null:(System.String)reader[((int)HistorialColumn.Observaciones - 1)];
					c.Monto = (System.Double)reader[((int)HistorialColumn.Monto - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Historial"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Historial"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Historial entity)
		{
			if (!reader.Read()) return;
			
			entity.HistorialId = (System.Guid)reader[((int)HistorialColumn.HistorialId - 1)];
			entity.OriginalHistorialId = (System.Guid)reader["HistorialID"];
			entity.Tabla = (System.Int32)reader[((int)HistorialColumn.Tabla - 1)];
			entity.Operacion = (System.Int32)reader[((int)HistorialColumn.Operacion - 1)];
			entity.FechaHoraRegistro = (System.DateTime)reader[((int)HistorialColumn.FechaHoraRegistro - 1)];
			entity.Cliente = (System.Guid)reader[((int)HistorialColumn.Cliente - 1)];
			entity.Vendedor = (System.Guid)reader[((int)HistorialColumn.Vendedor - 1)];
			entity.Observaciones = (reader.IsDBNull(((int)HistorialColumn.Observaciones - 1)))?null:(System.String)reader[((int)HistorialColumn.Observaciones - 1)];
			entity.Monto = (System.Double)reader[((int)HistorialColumn.Monto - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Historial"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Historial"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Historial entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.HistorialId = (System.Guid)dataRow["HistorialID"];
			entity.OriginalHistorialId = (System.Guid)dataRow["HistorialID"];
			entity.Tabla = (System.Int32)dataRow["Tabla"];
			entity.Operacion = (System.Int32)dataRow["Operacion"];
			entity.FechaHoraRegistro = (System.DateTime)dataRow["FechaHoraRegistro"];
			entity.Cliente = (System.Guid)dataRow["Cliente"];
			entity.Vendedor = (System.Guid)dataRow["Vendedor"];
			entity.Observaciones = Convert.IsDBNull(dataRow["Observaciones"]) ? null : (System.String)dataRow["Observaciones"];
			entity.Monto = (System.Double)dataRow["Monto"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Historial"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Historial Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Historial entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the MAT.Entities.Historial object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Historial instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Historial Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Historial entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region HistorialChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Historial</c>
	///</summary>
	public enum HistorialChildEntityTypes
	{
	}
	
	#endregion HistorialChildEntityTypes
	
	#region HistorialFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;HistorialColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Historial"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HistorialFilterBuilder : SqlFilterBuilder<HistorialColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HistorialFilterBuilder class.
		/// </summary>
		public HistorialFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the HistorialFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HistorialFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HistorialFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HistorialFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HistorialFilterBuilder
	
	#region HistorialParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;HistorialColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Historial"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HistorialParameterBuilder : ParameterizedSqlFilterBuilder<HistorialColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HistorialParameterBuilder class.
		/// </summary>
		public HistorialParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the HistorialParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HistorialParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HistorialParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HistorialParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HistorialParameterBuilder
	
	#region HistorialSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;HistorialColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Historial"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class HistorialSortBuilder : SqlSortBuilder<HistorialColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HistorialSqlSortBuilder class.
		/// </summary>
		public HistorialSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion HistorialSortBuilder
	
} // end namespace
