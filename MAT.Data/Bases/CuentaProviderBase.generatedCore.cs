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
	/// This class is the base class for any <see cref="CuentaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CuentaProviderBaseCore : EntityProviderBase<MAT.Entities.Cuenta, MAT.Entities.CuentaKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.CuentaKey key)
		{
			return Delete(transactionManager, key.CuentaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_cuentaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _cuentaId)
		{
			return Delete(null, _cuentaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _cuentaId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Cuenta_Cliente key.
		///		FK_Cuenta_Cliente Description: 
		/// </summary>
		/// <param name="_clienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Cuenta objects.</returns>
		public TList<Cuenta> GetByClienteId(System.Guid _clienteId)
		{
			int count = -1;
			return GetByClienteId(_clienteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Cuenta_Cliente key.
		///		FK_Cuenta_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Cuenta objects.</returns>
		/// <remarks></remarks>
		public TList<Cuenta> GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Cuenta_Cliente key.
		///		FK_Cuenta_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Cuenta objects.</returns>
		public TList<Cuenta> GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Cuenta_Cliente key.
		///		fkCuentaCliente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_clienteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Cuenta objects.</returns>
		public TList<Cuenta> GetByClienteId(System.Guid _clienteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByClienteId(null, _clienteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Cuenta_Cliente key.
		///		fkCuentaCliente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_clienteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Cuenta objects.</returns>
		public TList<Cuenta> GetByClienteId(System.Guid _clienteId, int start, int pageLength,out int count)
		{
			return GetByClienteId(null, _clienteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Cuenta_Cliente key.
		///		FK_Cuenta_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Cuenta objects.</returns>
		public abstract TList<Cuenta> GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Cuenta Get(TransactionManager transactionManager, MAT.Entities.CuentaKey key, int start, int pageLength)
		{
			return GetByCuentaId(transactionManager, key.CuentaId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_CuentaCorriente index.
		/// </summary>
		/// <param name="_cuentaId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cuenta"/> class.</returns>
		public MAT.Entities.Cuenta GetByCuentaId(System.Guid _cuentaId)
		{
			int count = -1;
			return GetByCuentaId(null,_cuentaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CuentaCorriente index.
		/// </summary>
		/// <param name="_cuentaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cuenta"/> class.</returns>
		public MAT.Entities.Cuenta GetByCuentaId(System.Guid _cuentaId, int start, int pageLength)
		{
			int count = -1;
			return GetByCuentaId(null, _cuentaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CuentaCorriente index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cuenta"/> class.</returns>
		public MAT.Entities.Cuenta GetByCuentaId(TransactionManager transactionManager, System.Guid _cuentaId)
		{
			int count = -1;
			return GetByCuentaId(transactionManager, _cuentaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CuentaCorriente index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cuenta"/> class.</returns>
		public MAT.Entities.Cuenta GetByCuentaId(TransactionManager transactionManager, System.Guid _cuentaId, int start, int pageLength)
		{
			int count = -1;
			return GetByCuentaId(transactionManager, _cuentaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CuentaCorriente index.
		/// </summary>
		/// <param name="_cuentaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cuenta"/> class.</returns>
		public MAT.Entities.Cuenta GetByCuentaId(System.Guid _cuentaId, int start, int pageLength, out int count)
		{
			return GetByCuentaId(null, _cuentaId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CuentaCorriente index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cuenta"/> class.</returns>
		public abstract MAT.Entities.Cuenta GetByCuentaId(TransactionManager transactionManager, System.Guid _cuentaId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Cuenta&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Cuenta&gt;"/></returns>
		public static TList<Cuenta> Fill(IDataReader reader, TList<Cuenta> rows, int start, int pageLength)
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
				
				MAT.Entities.Cuenta c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Cuenta")
					.Append("|").Append((System.Guid)reader[((int)CuentaColumn.CuentaId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Cuenta>(
					key.ToString(), // EntityTrackingKey
					"Cuenta",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Cuenta();
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
					c.CuentaId = (System.Guid)reader[((int)CuentaColumn.CuentaId - 1)];
					c.OriginalCuentaId = c.CuentaId;
					c.ClienteId = (System.Guid)reader[((int)CuentaColumn.ClienteId - 1)];
					c.Estado = (System.Boolean)reader[((int)CuentaColumn.Estado - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Cuenta"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Cuenta"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Cuenta entity)
		{
			if (!reader.Read()) return;
			
			entity.CuentaId = (System.Guid)reader[((int)CuentaColumn.CuentaId - 1)];
			entity.OriginalCuentaId = (System.Guid)reader["CuentaID"];
			entity.ClienteId = (System.Guid)reader[((int)CuentaColumn.ClienteId - 1)];
			entity.Estado = (System.Boolean)reader[((int)CuentaColumn.Estado - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Cuenta"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Cuenta"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Cuenta entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.CuentaId = (System.Guid)dataRow["CuentaID"];
			entity.OriginalCuentaId = (System.Guid)dataRow["CuentaID"];
			entity.ClienteId = (System.Guid)dataRow["ClienteID"];
			entity.Estado = (System.Boolean)dataRow["Estado"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Cuenta"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Cuenta Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Cuenta entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ClienteIdSource	
			if (CanDeepLoad(entity, "Cliente|ClienteIdSource", deepLoadType, innerList) 
				&& entity.ClienteIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.ClienteId;
				Cliente tmpEntity = EntityManager.LocateEntity<Cliente>(EntityLocator.ConstructKeyFromPkItems(typeof(Cliente), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ClienteIdSource = tmpEntity;
				else
					entity.ClienteIdSource = DataRepository.ClienteProvider.GetByClienteId(transactionManager, entity.ClienteId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ClienteIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ClienteIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ClienteProvider.DeepLoad(transactionManager, entity.ClienteIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ClienteIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByCuentaId methods when available
			
			#region MovimientoCuentaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<MovimientoCuenta>|MovimientoCuentaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MovimientoCuentaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.MovimientoCuentaCollection = DataRepository.MovimientoCuentaProvider.GetByCuentaId(transactionManager, entity.CuentaId);

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
		/// Deep Save the entire object graph of the MAT.Entities.Cuenta object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Cuenta instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Cuenta Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Cuenta entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ClienteIdSource
			if (CanDeepSave(entity, "Cliente|ClienteIdSource", deepSaveType, innerList) 
				&& entity.ClienteIdSource != null)
			{
				DataRepository.ClienteProvider.Save(transactionManager, entity.ClienteIdSource);
				entity.ClienteId = entity.ClienteIdSource.ClienteId;
			}
			#endregion 
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
						if(child.CuentaIdSource != null)
						{
							child.CuentaId = child.CuentaIdSource.CuentaId;
						}
						else
						{
							child.CuentaId = entity.CuentaId;
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
	
	#region CuentaChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Cuenta</c>
	///</summary>
	public enum CuentaChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Cliente</c> at ClienteIdSource
		///</summary>
		[ChildEntityType(typeof(Cliente))]
		Cliente,
		///<summary>
		/// Collection of <c>Cuenta</c> as OneToMany for MovimientoCuentaCollection
		///</summary>
		[ChildEntityType(typeof(TList<MovimientoCuenta>))]
		MovimientoCuentaCollection,
	}
	
	#endregion CuentaChildEntityTypes
	
	#region CuentaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;CuentaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaFilterBuilder : SqlFilterBuilder<CuentaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaFilterBuilder class.
		/// </summary>
		public CuentaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CuentaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CuentaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CuentaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CuentaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CuentaFilterBuilder
	
	#region CuentaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;CuentaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaParameterBuilder : ParameterizedSqlFilterBuilder<CuentaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaParameterBuilder class.
		/// </summary>
		public CuentaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CuentaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CuentaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CuentaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CuentaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CuentaParameterBuilder
	
	#region CuentaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;CuentaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cuenta"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class CuentaSortBuilder : SqlSortBuilder<CuentaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaSqlSortBuilder class.
		/// </summary>
		public CuentaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion CuentaSortBuilder
	
} // end namespace
