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
	/// This class is the base class for any <see cref="AuditFacturaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class AuditFacturaProviderBaseCore : EntityProviderBase<MAT.Entities.AuditFactura, MAT.Entities.AuditFacturaKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.AuditFacturaKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _id)
		{
			return Delete(null, _id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _id);		
		
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
		public override MAT.Entities.AuditFactura Get(TransactionManager transactionManager, MAT.Entities.AuditFacturaKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK__AuditFac__3214EC271C034ED4 index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.AuditFactura"/> class.</returns>
		public MAT.Entities.AuditFactura GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__AuditFac__3214EC271C034ED4 index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.AuditFactura"/> class.</returns>
		public MAT.Entities.AuditFactura GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__AuditFac__3214EC271C034ED4 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.AuditFactura"/> class.</returns>
		public MAT.Entities.AuditFactura GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__AuditFac__3214EC271C034ED4 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.AuditFactura"/> class.</returns>
		public MAT.Entities.AuditFactura GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__AuditFac__3214EC271C034ED4 index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.AuditFactura"/> class.</returns>
		public MAT.Entities.AuditFactura GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__AuditFac__3214EC271C034ED4 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.AuditFactura"/> class.</returns>
		public abstract MAT.Entities.AuditFactura GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;AuditFactura&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;AuditFactura&gt;"/></returns>
		public static TList<AuditFactura> Fill(IDataReader reader, TList<AuditFactura> rows, int start, int pageLength)
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
				
				MAT.Entities.AuditFactura c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("AuditFactura")
					.Append("|").Append((System.Int32)reader[((int)AuditFacturaColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<AuditFactura>(
					key.ToString(), // EntityTrackingKey
					"AuditFactura",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.AuditFactura();
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
					c.Id = (System.Int32)reader[((int)AuditFacturaColumn.Id - 1)];
					c.FacturaId = (reader.IsDBNull(((int)AuditFacturaColumn.FacturaId - 1)))?null:(System.Guid?)reader[((int)AuditFacturaColumn.FacturaId - 1)];
					c.PersonaId = (reader.IsDBNull(((int)AuditFacturaColumn.PersonaId - 1)))?null:(System.Guid?)reader[((int)AuditFacturaColumn.PersonaId - 1)];
					c.VendedorId = (System.Guid)reader[((int)AuditFacturaColumn.VendedorId - 1)];
					c.Accion = (reader.IsDBNull(((int)AuditFacturaColumn.Accion - 1)))?null:(System.String)reader[((int)AuditFacturaColumn.Accion - 1)];
					c.Descripcion = (reader.IsDBNull(((int)AuditFacturaColumn.Descripcion - 1)))?null:(System.String)reader[((int)AuditFacturaColumn.Descripcion - 1)];
					c.Fecha = (System.DateTime)reader[((int)AuditFacturaColumn.Fecha - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.AuditFactura"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.AuditFactura"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.AuditFactura entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)AuditFacturaColumn.Id - 1)];
			entity.FacturaId = (reader.IsDBNull(((int)AuditFacturaColumn.FacturaId - 1)))?null:(System.Guid?)reader[((int)AuditFacturaColumn.FacturaId - 1)];
			entity.PersonaId = (reader.IsDBNull(((int)AuditFacturaColumn.PersonaId - 1)))?null:(System.Guid?)reader[((int)AuditFacturaColumn.PersonaId - 1)];
			entity.VendedorId = (System.Guid)reader[((int)AuditFacturaColumn.VendedorId - 1)];
			entity.Accion = (reader.IsDBNull(((int)AuditFacturaColumn.Accion - 1)))?null:(System.String)reader[((int)AuditFacturaColumn.Accion - 1)];
			entity.Descripcion = (reader.IsDBNull(((int)AuditFacturaColumn.Descripcion - 1)))?null:(System.String)reader[((int)AuditFacturaColumn.Descripcion - 1)];
			entity.Fecha = (System.DateTime)reader[((int)AuditFacturaColumn.Fecha - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.AuditFactura"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.AuditFactura"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.AuditFactura entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["ID"];
			entity.FacturaId = Convert.IsDBNull(dataRow["FacturaID"]) ? null : (System.Guid?)dataRow["FacturaID"];
			entity.PersonaId = Convert.IsDBNull(dataRow["PersonaID"]) ? null : (System.Guid?)dataRow["PersonaID"];
			entity.VendedorId = (System.Guid)dataRow["VendedorID"];
			entity.Accion = Convert.IsDBNull(dataRow["Accion"]) ? null : (System.String)dataRow["Accion"];
			entity.Descripcion = Convert.IsDBNull(dataRow["Descripcion"]) ? null : (System.String)dataRow["Descripcion"];
			entity.Fecha = (System.DateTime)dataRow["Fecha"];
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
		/// <param name="entity">The <see cref="MAT.Entities.AuditFactura"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.AuditFactura Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.AuditFactura entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the MAT.Entities.AuditFactura object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.AuditFactura instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.AuditFactura Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.AuditFactura entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region AuditFacturaChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.AuditFactura</c>
	///</summary>
	public enum AuditFacturaChildEntityTypes
	{
	}
	
	#endregion AuditFacturaChildEntityTypes
	
	#region AuditFacturaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;AuditFacturaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AuditFactura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AuditFacturaFilterBuilder : SqlFilterBuilder<AuditFacturaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AuditFacturaFilterBuilder class.
		/// </summary>
		public AuditFacturaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the AuditFacturaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AuditFacturaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AuditFacturaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AuditFacturaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AuditFacturaFilterBuilder
	
	#region AuditFacturaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;AuditFacturaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AuditFactura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AuditFacturaParameterBuilder : ParameterizedSqlFilterBuilder<AuditFacturaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AuditFacturaParameterBuilder class.
		/// </summary>
		public AuditFacturaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the AuditFacturaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AuditFacturaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AuditFacturaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AuditFacturaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AuditFacturaParameterBuilder
	
	#region AuditFacturaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;AuditFacturaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AuditFactura"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class AuditFacturaSortBuilder : SqlSortBuilder<AuditFacturaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AuditFacturaSqlSortBuilder class.
		/// </summary>
		public AuditFacturaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion AuditFacturaSortBuilder
	
} // end namespace
