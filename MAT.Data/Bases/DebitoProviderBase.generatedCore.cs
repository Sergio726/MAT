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
	/// This class is the base class for any <see cref="DebitoProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class DebitoProviderBaseCore : EntityProviderBase<MAT.Entities.Debito, MAT.Entities.DebitoKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.DebitoKey key)
		{
			return Delete(transactionManager, key.DebitoId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_debitoId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _debitoId)
		{
			return Delete(null, _debitoId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_debitoId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _debitoId);		
		
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
		public override MAT.Entities.Debito Get(TransactionManager transactionManager, MAT.Entities.DebitoKey key, int start, int pageLength)
		{
			return GetByDebitoId(transactionManager, key.DebitoId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Debito index.
		/// </summary>
		/// <param name="_debitoId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Debito"/> class.</returns>
		public MAT.Entities.Debito GetByDebitoId(System.Guid _debitoId)
		{
			int count = -1;
			return GetByDebitoId(null,_debitoId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Debito index.
		/// </summary>
		/// <param name="_debitoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Debito"/> class.</returns>
		public MAT.Entities.Debito GetByDebitoId(System.Guid _debitoId, int start, int pageLength)
		{
			int count = -1;
			return GetByDebitoId(null, _debitoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Debito index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_debitoId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Debito"/> class.</returns>
		public MAT.Entities.Debito GetByDebitoId(TransactionManager transactionManager, System.Guid _debitoId)
		{
			int count = -1;
			return GetByDebitoId(transactionManager, _debitoId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Debito index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_debitoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Debito"/> class.</returns>
		public MAT.Entities.Debito GetByDebitoId(TransactionManager transactionManager, System.Guid _debitoId, int start, int pageLength)
		{
			int count = -1;
			return GetByDebitoId(transactionManager, _debitoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Debito index.
		/// </summary>
		/// <param name="_debitoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Debito"/> class.</returns>
		public MAT.Entities.Debito GetByDebitoId(System.Guid _debitoId, int start, int pageLength, out int count)
		{
			return GetByDebitoId(null, _debitoId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Debito index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_debitoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Debito"/> class.</returns>
		public abstract MAT.Entities.Debito GetByDebitoId(TransactionManager transactionManager, System.Guid _debitoId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Debito&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Debito&gt;"/></returns>
		public static TList<Debito> Fill(IDataReader reader, TList<Debito> rows, int start, int pageLength)
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
				
				MAT.Entities.Debito c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Debito")
					.Append("|").Append((System.Guid)reader[((int)DebitoColumn.DebitoId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Debito>(
					key.ToString(), // EntityTrackingKey
					"Debito",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Debito();
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
					c.DebitoId = (System.Guid)reader[((int)DebitoColumn.DebitoId - 1)];
					c.OriginalDebitoId = c.DebitoId;
					c.Fecha = (reader.IsDBNull(((int)DebitoColumn.Fecha - 1)))?null:(System.DateTime?)reader[((int)DebitoColumn.Fecha - 1)];
					c.ClienteId = (reader.IsDBNull(((int)DebitoColumn.ClienteId - 1)))?null:(System.Guid?)reader[((int)DebitoColumn.ClienteId - 1)];
					c.VendedorId = (reader.IsDBNull(((int)DebitoColumn.VendedorId - 1)))?null:(System.Guid?)reader[((int)DebitoColumn.VendedorId - 1)];
					c.MontoDebito = (reader.IsDBNull(((int)DebitoColumn.MontoDebito - 1)))?null:(System.Double?)reader[((int)DebitoColumn.MontoDebito - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Debito"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Debito"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Debito entity)
		{
			if (!reader.Read()) return;
			
			entity.DebitoId = (System.Guid)reader[((int)DebitoColumn.DebitoId - 1)];
			entity.OriginalDebitoId = (System.Guid)reader["DebitoID"];
			entity.Fecha = (reader.IsDBNull(((int)DebitoColumn.Fecha - 1)))?null:(System.DateTime?)reader[((int)DebitoColumn.Fecha - 1)];
			entity.ClienteId = (reader.IsDBNull(((int)DebitoColumn.ClienteId - 1)))?null:(System.Guid?)reader[((int)DebitoColumn.ClienteId - 1)];
			entity.VendedorId = (reader.IsDBNull(((int)DebitoColumn.VendedorId - 1)))?null:(System.Guid?)reader[((int)DebitoColumn.VendedorId - 1)];
			entity.MontoDebito = (reader.IsDBNull(((int)DebitoColumn.MontoDebito - 1)))?null:(System.Double?)reader[((int)DebitoColumn.MontoDebito - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Debito"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Debito"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Debito entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.DebitoId = (System.Guid)dataRow["DebitoID"];
			entity.OriginalDebitoId = (System.Guid)dataRow["DebitoID"];
			entity.Fecha = Convert.IsDBNull(dataRow["Fecha"]) ? null : (System.DateTime?)dataRow["Fecha"];
			entity.ClienteId = Convert.IsDBNull(dataRow["ClienteID"]) ? null : (System.Guid?)dataRow["ClienteID"];
			entity.VendedorId = Convert.IsDBNull(dataRow["VendedorID"]) ? null : (System.Guid?)dataRow["VendedorID"];
			entity.MontoDebito = Convert.IsDBNull(dataRow["MontoDebito"]) ? null : (System.Double?)dataRow["MontoDebito"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Debito"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Debito Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Debito entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByDebitoId methods when available
			
			#region MovimientoCuentaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<MovimientoCuenta>|MovimientoCuentaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MovimientoCuentaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.MovimientoCuentaCollection = DataRepository.MovimientoCuentaProvider.GetByDebitoId(transactionManager, entity.DebitoId);

				if (deep && entity.MovimientoCuentaCollection.Count > 0)
				{
					deepHandles.Add("MovimientoCuentaCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<MovimientoCuenta>) DataRepository.MovimientoCuentaProvider.DeepLoad,
						new object[] { transactionManager, entity.MovimientoCuentaCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Debito object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Debito instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Debito Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Debito entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<MovimientoCuenta>
				if (CanDeepSave(entity.MovimientoCuentaCollection, "List<MovimientoCuenta>|MovimientoCuentaCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(MovimientoCuenta child in entity.MovimientoCuentaCollection)
					{
						if(child.DebitoIdSource != null)
						{
							child.DebitoId = child.DebitoIdSource.DebitoId;
						}
						else
						{
							child.DebitoId = entity.DebitoId;
						}

					}

					if (entity.MovimientoCuentaCollection.Count > 0 || entity.MovimientoCuentaCollection.DeletedItems.Count > 0)
					{
						//DataRepository.MovimientoCuentaProvider.Save(transactionManager, entity.MovimientoCuentaCollection);
						
						deepHandles.Add("MovimientoCuentaCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< MovimientoCuenta >) DataRepository.MovimientoCuentaProvider.DeepSave,
							new object[] { transactionManager, entity.MovimientoCuentaCollection, deepSaveType, childTypes, innerList }
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
	
	#region DebitoChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Debito</c>
	///</summary>
	public enum DebitoChildEntityTypes
	{
		///<summary>
		/// Collection of <c>Debito</c> as OneToMany for MovimientoCuentaCollection
		///</summary>
		[ChildEntityType(typeof(TList<MovimientoCuenta>))]
		MovimientoCuentaCollection,
	}
	
	#endregion DebitoChildEntityTypes
	
	#region DebitoFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;DebitoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Debito"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DebitoFilterBuilder : SqlFilterBuilder<DebitoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DebitoFilterBuilder class.
		/// </summary>
		public DebitoFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DebitoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DebitoFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DebitoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DebitoFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DebitoFilterBuilder
	
	#region DebitoParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;DebitoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Debito"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DebitoParameterBuilder : ParameterizedSqlFilterBuilder<DebitoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DebitoParameterBuilder class.
		/// </summary>
		public DebitoParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DebitoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DebitoParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DebitoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DebitoParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DebitoParameterBuilder
	
	#region DebitoSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;DebitoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Debito"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class DebitoSortBuilder : SqlSortBuilder<DebitoColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DebitoSqlSortBuilder class.
		/// </summary>
		public DebitoSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion DebitoSortBuilder
	
} // end namespace
