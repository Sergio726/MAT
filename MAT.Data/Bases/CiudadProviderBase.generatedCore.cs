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
	/// This class is the base class for any <see cref="CiudadProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CiudadProviderBaseCore : EntityProviderBase<MAT.Entities.Ciudad, MAT.Entities.CiudadKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.CiudadKey key)
		{
			return Delete(transactionManager, key.CiudadId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_ciudadId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _ciudadId)
		{
			return Delete(null, _ciudadId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_ciudadId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _ciudadId);		
		
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
		public override MAT.Entities.Ciudad Get(TransactionManager transactionManager, MAT.Entities.CiudadKey key, int start, int pageLength)
		{
			return GetByCiudadId(transactionManager, key.CiudadId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK__Ciudad__E826E79045C14345 index.
		/// </summary>
		/// <param name="_ciudadId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Ciudad"/> class.</returns>
		public MAT.Entities.Ciudad GetByCiudadId(System.Int32 _ciudadId)
		{
			int count = -1;
			return GetByCiudadId(null,_ciudadId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__Ciudad__E826E79045C14345 index.
		/// </summary>
		/// <param name="_ciudadId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Ciudad"/> class.</returns>
		public MAT.Entities.Ciudad GetByCiudadId(System.Int32 _ciudadId, int start, int pageLength)
		{
			int count = -1;
			return GetByCiudadId(null, _ciudadId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__Ciudad__E826E79045C14345 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_ciudadId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Ciudad"/> class.</returns>
		public MAT.Entities.Ciudad GetByCiudadId(TransactionManager transactionManager, System.Int32 _ciudadId)
		{
			int count = -1;
			return GetByCiudadId(transactionManager, _ciudadId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__Ciudad__E826E79045C14345 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_ciudadId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Ciudad"/> class.</returns>
		public MAT.Entities.Ciudad GetByCiudadId(TransactionManager transactionManager, System.Int32 _ciudadId, int start, int pageLength)
		{
			int count = -1;
			return GetByCiudadId(transactionManager, _ciudadId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__Ciudad__E826E79045C14345 index.
		/// </summary>
		/// <param name="_ciudadId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Ciudad"/> class.</returns>
		public MAT.Entities.Ciudad GetByCiudadId(System.Int32 _ciudadId, int start, int pageLength, out int count)
		{
			return GetByCiudadId(null, _ciudadId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__Ciudad__E826E79045C14345 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_ciudadId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Ciudad"/> class.</returns>
		public abstract MAT.Entities.Ciudad GetByCiudadId(TransactionManager transactionManager, System.Int32 _ciudadId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Ciudad&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Ciudad&gt;"/></returns>
		public static TList<Ciudad> Fill(IDataReader reader, TList<Ciudad> rows, int start, int pageLength)
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
				
				MAT.Entities.Ciudad c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Ciudad")
					.Append("|").Append((System.Int32)reader[((int)CiudadColumn.CiudadId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Ciudad>(
					key.ToString(), // EntityTrackingKey
					"Ciudad",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Ciudad();
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
					c.CiudadId = (System.Int32)reader[((int)CiudadColumn.CiudadId - 1)];
					c.OriginalCiudadId = c.CiudadId;
					c.CiudadNombre = (System.String)reader[((int)CiudadColumn.CiudadNombre - 1)];
					c.PaisCodigo = (System.String)reader[((int)CiudadColumn.PaisCodigo - 1)];
					c.CiudadDistrito = (System.String)reader[((int)CiudadColumn.CiudadDistrito - 1)];
					c.CiudadPoblacion = (System.Int32)reader[((int)CiudadColumn.CiudadPoblacion - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Ciudad"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Ciudad"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Ciudad entity)
		{
			if (!reader.Read()) return;
			
			entity.CiudadId = (System.Int32)reader[((int)CiudadColumn.CiudadId - 1)];
			entity.OriginalCiudadId = (System.Int32)reader["CiudadID"];
			entity.CiudadNombre = (System.String)reader[((int)CiudadColumn.CiudadNombre - 1)];
			entity.PaisCodigo = (System.String)reader[((int)CiudadColumn.PaisCodigo - 1)];
			entity.CiudadDistrito = (System.String)reader[((int)CiudadColumn.CiudadDistrito - 1)];
			entity.CiudadPoblacion = (System.Int32)reader[((int)CiudadColumn.CiudadPoblacion - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Ciudad"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Ciudad"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Ciudad entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.CiudadId = (System.Int32)dataRow["CiudadID"];
			entity.OriginalCiudadId = (System.Int32)dataRow["CiudadID"];
			entity.CiudadNombre = (System.String)dataRow["CiudadNombre"];
			entity.PaisCodigo = (System.String)dataRow["PaisCodigo"];
			entity.CiudadDistrito = (System.String)dataRow["CiudadDistrito"];
			entity.CiudadPoblacion = (System.Int32)dataRow["CiudadPoblacion"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Ciudad"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Ciudad Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Ciudad entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the MAT.Entities.Ciudad object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Ciudad instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Ciudad Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Ciudad entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region CiudadChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Ciudad</c>
	///</summary>
	public enum CiudadChildEntityTypes
	{
	}
	
	#endregion CiudadChildEntityTypes
	
	#region CiudadFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;CiudadColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Ciudad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CiudadFilterBuilder : SqlFilterBuilder<CiudadColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CiudadFilterBuilder class.
		/// </summary>
		public CiudadFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CiudadFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CiudadFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CiudadFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CiudadFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CiudadFilterBuilder
	
	#region CiudadParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;CiudadColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Ciudad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CiudadParameterBuilder : ParameterizedSqlFilterBuilder<CiudadColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CiudadParameterBuilder class.
		/// </summary>
		public CiudadParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CiudadParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CiudadParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CiudadParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CiudadParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CiudadParameterBuilder
	
	#region CiudadSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;CiudadColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Ciudad"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class CiudadSortBuilder : SqlSortBuilder<CiudadColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CiudadSqlSortBuilder class.
		/// </summary>
		public CiudadSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion CiudadSortBuilder
	
} // end namespace
