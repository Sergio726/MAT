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
	/// This class is the base class for any <see cref="CuentaCorrienteProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CuentaCorrienteProviderBaseCore : EntityProviderBase<MAT.Entities.CuentaCorriente, MAT.Entities.CuentaCorrienteKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.CuentaCorrienteKey key)
		{
			return Delete(transactionManager, key.CuentaCorrienteId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_cuentaCorrienteId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _cuentaCorrienteId)
		{
			return Delete(null, _cuentaCorrienteId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaCorrienteId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _cuentaCorrienteId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CuentaCorriente_Cliente key.
		///		FK_CuentaCorriente_Cliente Description: 
		/// </summary>
		/// <param name="_clienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.CuentaCorriente objects.</returns>
		public TList<CuentaCorriente> GetByClienteId(System.Guid _clienteId)
		{
			int count = -1;
			return GetByClienteId(_clienteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CuentaCorriente_Cliente key.
		///		FK_CuentaCorriente_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.CuentaCorriente objects.</returns>
		/// <remarks></remarks>
		public TList<CuentaCorriente> GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_CuentaCorriente_Cliente key.
		///		FK_CuentaCorriente_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.CuentaCorriente objects.</returns>
		public TList<CuentaCorriente> GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CuentaCorriente_Cliente key.
		///		fkCuentaCorrienteCliente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_clienteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.CuentaCorriente objects.</returns>
		public TList<CuentaCorriente> GetByClienteId(System.Guid _clienteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByClienteId(null, _clienteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CuentaCorriente_Cliente key.
		///		fkCuentaCorrienteCliente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_clienteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.CuentaCorriente objects.</returns>
		public TList<CuentaCorriente> GetByClienteId(System.Guid _clienteId, int start, int pageLength,out int count)
		{
			return GetByClienteId(null, _clienteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_CuentaCorriente_Cliente key.
		///		FK_CuentaCorriente_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.CuentaCorriente objects.</returns>
		public abstract TList<CuentaCorriente> GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.CuentaCorriente Get(TransactionManager transactionManager, MAT.Entities.CuentaCorrienteKey key, int start, int pageLength)
		{
			return GetByCuentaCorrienteId(transactionManager, key.CuentaCorrienteId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_CuentaCorriente_1 index.
		/// </summary>
		/// <param name="_cuentaCorrienteId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.CuentaCorriente"/> class.</returns>
		public MAT.Entities.CuentaCorriente GetByCuentaCorrienteId(System.Guid _cuentaCorrienteId)
		{
			int count = -1;
			return GetByCuentaCorrienteId(null,_cuentaCorrienteId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CuentaCorriente_1 index.
		/// </summary>
		/// <param name="_cuentaCorrienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.CuentaCorriente"/> class.</returns>
		public MAT.Entities.CuentaCorriente GetByCuentaCorrienteId(System.Guid _cuentaCorrienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByCuentaCorrienteId(null, _cuentaCorrienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CuentaCorriente_1 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.CuentaCorriente"/> class.</returns>
		public MAT.Entities.CuentaCorriente GetByCuentaCorrienteId(TransactionManager transactionManager, System.Guid _cuentaCorrienteId)
		{
			int count = -1;
			return GetByCuentaCorrienteId(transactionManager, _cuentaCorrienteId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CuentaCorriente_1 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.CuentaCorriente"/> class.</returns>
		public MAT.Entities.CuentaCorriente GetByCuentaCorrienteId(TransactionManager transactionManager, System.Guid _cuentaCorrienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByCuentaCorrienteId(transactionManager, _cuentaCorrienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CuentaCorriente_1 index.
		/// </summary>
		/// <param name="_cuentaCorrienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.CuentaCorriente"/> class.</returns>
		public MAT.Entities.CuentaCorriente GetByCuentaCorrienteId(System.Guid _cuentaCorrienteId, int start, int pageLength, out int count)
		{
			return GetByCuentaCorrienteId(null, _cuentaCorrienteId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_CuentaCorriente_1 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.CuentaCorriente"/> class.</returns>
		public abstract MAT.Entities.CuentaCorriente GetByCuentaCorrienteId(TransactionManager transactionManager, System.Guid _cuentaCorrienteId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;CuentaCorriente&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;CuentaCorriente&gt;"/></returns>
		public static TList<CuentaCorriente> Fill(IDataReader reader, TList<CuentaCorriente> rows, int start, int pageLength)
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
				
				MAT.Entities.CuentaCorriente c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("CuentaCorriente")
					.Append("|").Append((System.Guid)reader[((int)CuentaCorrienteColumn.CuentaCorrienteId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<CuentaCorriente>(
					key.ToString(), // EntityTrackingKey
					"CuentaCorriente",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.CuentaCorriente();
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
					c.CuentaCorrienteId = (System.Guid)reader[((int)CuentaCorrienteColumn.CuentaCorrienteId - 1)];
					c.OriginalCuentaCorrienteId = c.CuentaCorrienteId;
					c.Fecha = (reader.IsDBNull(((int)CuentaCorrienteColumn.Fecha - 1)))?null:(System.DateTime?)reader[((int)CuentaCorrienteColumn.Fecha - 1)];
					c.Monto = (reader.IsDBNull(((int)CuentaCorrienteColumn.Monto - 1)))?null:(System.Double?)reader[((int)CuentaCorrienteColumn.Monto - 1)];
					c.ClienteId = (System.Guid)reader[((int)CuentaCorrienteColumn.ClienteId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.CuentaCorriente"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.CuentaCorriente"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.CuentaCorriente entity)
		{
			if (!reader.Read()) return;
			
			entity.CuentaCorrienteId = (System.Guid)reader[((int)CuentaCorrienteColumn.CuentaCorrienteId - 1)];
			entity.OriginalCuentaCorrienteId = (System.Guid)reader["CuentaCorrienteID"];
			entity.Fecha = (reader.IsDBNull(((int)CuentaCorrienteColumn.Fecha - 1)))?null:(System.DateTime?)reader[((int)CuentaCorrienteColumn.Fecha - 1)];
			entity.Monto = (reader.IsDBNull(((int)CuentaCorrienteColumn.Monto - 1)))?null:(System.Double?)reader[((int)CuentaCorrienteColumn.Monto - 1)];
			entity.ClienteId = (System.Guid)reader[((int)CuentaCorrienteColumn.ClienteId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.CuentaCorriente"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.CuentaCorriente"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.CuentaCorriente entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.CuentaCorrienteId = (System.Guid)dataRow["CuentaCorrienteID"];
			entity.OriginalCuentaCorrienteId = (System.Guid)dataRow["CuentaCorrienteID"];
			entity.Fecha = Convert.IsDBNull(dataRow["Fecha"]) ? null : (System.DateTime?)dataRow["Fecha"];
			entity.Monto = Convert.IsDBNull(dataRow["Monto"]) ? null : (System.Double?)dataRow["Monto"];
			entity.ClienteId = (System.Guid)dataRow["ClienteID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.CuentaCorriente"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.CuentaCorriente Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.CuentaCorriente entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
			// Deep load child collections  - Call GetByCuentaCorrienteId methods when available
			
			#region MovimientoCuentaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<MovimientoCuenta>|MovimientoCuentaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MovimientoCuentaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.MovimientoCuentaCollection = DataRepository.MovimientoCuentaProvider.GetByCuentaCorrienteId(transactionManager, entity.CuentaCorrienteId);

				if (deep && entity.MovimientoCuentaCollection.Count > 0)
				{
					deepHandles.Add("MovimientoCuentaCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<MovimientoCuenta>) DataRepository.MovimientoCuentaProvider.DeepLoad,
						new object[] { transactionManager, entity.MovimientoCuentaCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PagoCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Pago>|PagoCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PagoCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PagoCollection = DataRepository.PagoProvider.GetByCuentaCorrienteId(transactionManager, entity.CuentaCorrienteId);

				if (deep && entity.PagoCollection.Count > 0)
				{
					deepHandles.Add("PagoCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Pago>) DataRepository.PagoProvider.DeepLoad,
						new object[] { transactionManager, entity.PagoCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.CuentaCorriente object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.CuentaCorriente instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.CuentaCorriente Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.CuentaCorriente entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
						if(child.CuentaCorrienteIdSource != null)
						{
							child.CuentaCorrienteId = child.CuentaCorrienteIdSource.CuentaCorrienteId;
						}
						else
						{
							child.CuentaCorrienteId = entity.CuentaCorrienteId;
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
				
	
			#region List<Pago>
				if (CanDeepSave(entity.PagoCollection, "List<Pago>|PagoCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Pago child in entity.PagoCollection)
					{
						if(child.CuentaCorrienteIdSource != null)
						{
							child.CuentaCorrienteId = child.CuentaCorrienteIdSource.CuentaCorrienteId;
						}
						else
						{
							child.CuentaCorrienteId = entity.CuentaCorrienteId;
						}

					}

					if (entity.PagoCollection.Count > 0 || entity.PagoCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PagoProvider.Save(transactionManager, entity.PagoCollection);
						
						deepHandles.Add("PagoCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Pago >) DataRepository.PagoProvider.DeepSave,
							new object[] { transactionManager, entity.PagoCollection, deepSaveType, childTypes, innerList }
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
	
	#region CuentaCorrienteChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.CuentaCorriente</c>
	///</summary>
	public enum CuentaCorrienteChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Cliente</c> at ClienteIdSource
		///</summary>
		[ChildEntityType(typeof(Cliente))]
		Cliente,
		///<summary>
		/// Collection of <c>CuentaCorriente</c> as OneToMany for MovimientoCuentaCollection
		///</summary>
		[ChildEntityType(typeof(TList<MovimientoCuenta>))]
		MovimientoCuentaCollection,
		///<summary>
		/// Collection of <c>CuentaCorriente</c> as OneToMany for PagoCollection
		///</summary>
		[ChildEntityType(typeof(TList<Pago>))]
		PagoCollection,
	}
	
	#endregion CuentaCorrienteChildEntityTypes
	
	#region CuentaCorrienteFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;CuentaCorrienteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CuentaCorriente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaCorrienteFilterBuilder : SqlFilterBuilder<CuentaCorrienteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteFilterBuilder class.
		/// </summary>
		public CuentaCorrienteFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CuentaCorrienteFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CuentaCorrienteFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CuentaCorrienteFilterBuilder
	
	#region CuentaCorrienteParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;CuentaCorrienteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CuentaCorriente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaCorrienteParameterBuilder : ParameterizedSqlFilterBuilder<CuentaCorrienteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteParameterBuilder class.
		/// </summary>
		public CuentaCorrienteParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CuentaCorrienteParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CuentaCorrienteParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CuentaCorrienteParameterBuilder
	
	#region CuentaCorrienteSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;CuentaCorrienteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CuentaCorriente"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class CuentaCorrienteSortBuilder : SqlSortBuilder<CuentaCorrienteColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaCorrienteSqlSortBuilder class.
		/// </summary>
		public CuentaCorrienteSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion CuentaCorrienteSortBuilder
	
} // end namespace
