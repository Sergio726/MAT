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
	/// This class is the base class for any <see cref="ClienteProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ClienteProviderBaseCore : EntityProviderBase<MAT.Entities.Cliente, MAT.Entities.ClienteKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.ClienteKey key)
		{
			return Delete(transactionManager, key.ClienteId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_clienteId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _clienteId)
		{
			return Delete(null, _clienteId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _clienteId);		
		
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
		public override MAT.Entities.Cliente Get(TransactionManager transactionManager, MAT.Entities.ClienteKey key, int start, int pageLength)
		{
			return GetByClienteId(transactionManager, key.ClienteId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Cliente index.
		/// </summary>
		/// <param name="_clienteId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cliente"/> class.</returns>
		public MAT.Entities.Cliente GetByClienteId(System.Guid _clienteId)
		{
			int count = -1;
			return GetByClienteId(null,_clienteId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Cliente index.
		/// </summary>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cliente"/> class.</returns>
		public MAT.Entities.Cliente GetByClienteId(System.Guid _clienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByClienteId(null, _clienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Cliente index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cliente"/> class.</returns>
		public MAT.Entities.Cliente GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Cliente index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cliente"/> class.</returns>
		public MAT.Entities.Cliente GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Cliente index.
		/// </summary>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cliente"/> class.</returns>
		public MAT.Entities.Cliente GetByClienteId(System.Guid _clienteId, int start, int pageLength, out int count)
		{
			return GetByClienteId(null, _clienteId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Cliente index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Cliente"/> class.</returns>
		public abstract MAT.Entities.Cliente GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Cliente&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Cliente&gt;"/></returns>
		public static TList<Cliente> Fill(IDataReader reader, TList<Cliente> rows, int start, int pageLength)
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
				
				MAT.Entities.Cliente c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Cliente")
					.Append("|").Append((System.Guid)reader[((int)ClienteColumn.ClienteId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Cliente>(
					key.ToString(), // EntityTrackingKey
					"Cliente",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Cliente();
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
					c.ClienteId = (System.Guid)reader[((int)ClienteColumn.ClienteId - 1)];
					c.OriginalClienteId = c.ClienteId;
					c.RazonSocial = (reader.IsDBNull(((int)ClienteColumn.RazonSocial - 1)))?null:(System.String)reader[((int)ClienteColumn.RazonSocial - 1)];
					c.Cuit = (reader.IsDBNull(((int)ClienteColumn.Cuit - 1)))?null:(System.String)reader[((int)ClienteColumn.Cuit - 1)];
					c.Moneda = (reader.IsDBNull(((int)ClienteColumn.Moneda - 1)))?null:(System.String)reader[((int)ClienteColumn.Moneda - 1)];
					c.Empresa = (reader.IsDBNull(((int)ClienteColumn.Empresa - 1)))?null:(System.String)reader[((int)ClienteColumn.Empresa - 1)];
					c.Ocupacion = (reader.IsDBNull(((int)ClienteColumn.Ocupacion - 1)))?null:(System.String)reader[((int)ClienteColumn.Ocupacion - 1)];
					c.FormaPago = (reader.IsDBNull(((int)ClienteColumn.FormaPago - 1)))?null:(System.Int32?)reader[((int)ClienteColumn.FormaPago - 1)];
					c.CondicionIva = (reader.IsDBNull(((int)ClienteColumn.CondicionIva - 1)))?null:(System.Int32?)reader[((int)ClienteColumn.CondicionIva - 1)];
					c.VendedorId = (reader.IsDBNull(((int)ClienteColumn.VendedorId - 1)))?null:(System.Guid?)reader[((int)ClienteColumn.VendedorId - 1)];
					c.Fax = (reader.IsDBNull(((int)ClienteColumn.Fax - 1)))?null:(System.String)reader[((int)ClienteColumn.Fax - 1)];
					c.Web = (reader.IsDBNull(((int)ClienteColumn.Web - 1)))?null:(System.String)reader[((int)ClienteColumn.Web - 1)];
					c.Idioma = (reader.IsDBNull(((int)ClienteColumn.Idioma - 1)))?null:(System.String)reader[((int)ClienteColumn.Idioma - 1)];
					c.Promotor = (reader.IsDBNull(((int)ClienteColumn.Promotor - 1)))?null:(System.String)reader[((int)ClienteColumn.Promotor - 1)];
					c.Observacion = (reader.IsDBNull(((int)ClienteColumn.Observacion - 1)))?null:(System.String)reader[((int)ClienteColumn.Observacion - 1)];
					c.TipoId = (System.Int32)reader[((int)ClienteColumn.TipoId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Cliente"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Cliente"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Cliente entity)
		{
			if (!reader.Read()) return;
			
			entity.ClienteId = (System.Guid)reader[((int)ClienteColumn.ClienteId - 1)];
			entity.OriginalClienteId = (System.Guid)reader["ClienteID"];
			entity.RazonSocial = (reader.IsDBNull(((int)ClienteColumn.RazonSocial - 1)))?null:(System.String)reader[((int)ClienteColumn.RazonSocial - 1)];
			entity.Cuit = (reader.IsDBNull(((int)ClienteColumn.Cuit - 1)))?null:(System.String)reader[((int)ClienteColumn.Cuit - 1)];
			entity.Moneda = (reader.IsDBNull(((int)ClienteColumn.Moneda - 1)))?null:(System.String)reader[((int)ClienteColumn.Moneda - 1)];
			entity.Empresa = (reader.IsDBNull(((int)ClienteColumn.Empresa - 1)))?null:(System.String)reader[((int)ClienteColumn.Empresa - 1)];
			entity.Ocupacion = (reader.IsDBNull(((int)ClienteColumn.Ocupacion - 1)))?null:(System.String)reader[((int)ClienteColumn.Ocupacion - 1)];
			entity.FormaPago = (reader.IsDBNull(((int)ClienteColumn.FormaPago - 1)))?null:(System.Int32?)reader[((int)ClienteColumn.FormaPago - 1)];
			entity.CondicionIva = (reader.IsDBNull(((int)ClienteColumn.CondicionIva - 1)))?null:(System.Int32?)reader[((int)ClienteColumn.CondicionIva - 1)];
			entity.VendedorId = (reader.IsDBNull(((int)ClienteColumn.VendedorId - 1)))?null:(System.Guid?)reader[((int)ClienteColumn.VendedorId - 1)];
			entity.Fax = (reader.IsDBNull(((int)ClienteColumn.Fax - 1)))?null:(System.String)reader[((int)ClienteColumn.Fax - 1)];
			entity.Web = (reader.IsDBNull(((int)ClienteColumn.Web - 1)))?null:(System.String)reader[((int)ClienteColumn.Web - 1)];
			entity.Idioma = (reader.IsDBNull(((int)ClienteColumn.Idioma - 1)))?null:(System.String)reader[((int)ClienteColumn.Idioma - 1)];
			entity.Promotor = (reader.IsDBNull(((int)ClienteColumn.Promotor - 1)))?null:(System.String)reader[((int)ClienteColumn.Promotor - 1)];
			entity.Observacion = (reader.IsDBNull(((int)ClienteColumn.Observacion - 1)))?null:(System.String)reader[((int)ClienteColumn.Observacion - 1)];
			entity.TipoId = (System.Int32)reader[((int)ClienteColumn.TipoId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Cliente"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Cliente"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Cliente entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ClienteId = (System.Guid)dataRow["ClienteID"];
			entity.OriginalClienteId = (System.Guid)dataRow["ClienteID"];
			entity.RazonSocial = Convert.IsDBNull(dataRow["RazonSocial"]) ? null : (System.String)dataRow["RazonSocial"];
			entity.Cuit = Convert.IsDBNull(dataRow["Cuit"]) ? null : (System.String)dataRow["Cuit"];
			entity.Moneda = Convert.IsDBNull(dataRow["Moneda"]) ? null : (System.String)dataRow["Moneda"];
			entity.Empresa = Convert.IsDBNull(dataRow["Empresa"]) ? null : (System.String)dataRow["Empresa"];
			entity.Ocupacion = Convert.IsDBNull(dataRow["Ocupacion"]) ? null : (System.String)dataRow["Ocupacion"];
			entity.FormaPago = Convert.IsDBNull(dataRow["FormaPago"]) ? null : (System.Int32?)dataRow["FormaPago"];
			entity.CondicionIva = Convert.IsDBNull(dataRow["CondicionIva"]) ? null : (System.Int32?)dataRow["CondicionIva"];
			entity.VendedorId = Convert.IsDBNull(dataRow["VendedorID"]) ? null : (System.Guid?)dataRow["VendedorID"];
			entity.Fax = Convert.IsDBNull(dataRow["Fax"]) ? null : (System.String)dataRow["Fax"];
			entity.Web = Convert.IsDBNull(dataRow["Web"]) ? null : (System.String)dataRow["Web"];
			entity.Idioma = Convert.IsDBNull(dataRow["Idioma"]) ? null : (System.String)dataRow["Idioma"];
			entity.Promotor = Convert.IsDBNull(dataRow["Promotor"]) ? null : (System.String)dataRow["Promotor"];
			entity.Observacion = Convert.IsDBNull(dataRow["Observacion"]) ? null : (System.String)dataRow["Observacion"];
			entity.TipoId = (System.Int32)dataRow["TipoID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Cliente"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Cliente Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Cliente entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByClienteId methods when available
			
			#region PagoCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Pago>|PagoCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PagoCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PagoCollection = DataRepository.PagoProvider.GetByClienteId(transactionManager, entity.ClienteId);

				if (deep && entity.PagoCollection.Count > 0)
				{
					deepHandles.Add("PagoCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Pago>) DataRepository.PagoProvider.DeepLoad,
						new object[] { transactionManager, entity.PagoCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region NotaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Nota>|NotaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'NotaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.NotaCollection = DataRepository.NotaProvider.GetByClienteId(transactionManager, entity.ClienteId);

				if (deep && entity.NotaCollection.Count > 0)
				{
					deepHandles.Add("NotaCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Nota>) DataRepository.NotaProvider.DeepLoad,
						new object[] { transactionManager, entity.NotaCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PasajeroMenorCollectionGetByMenorid
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PasajeroMenor>|PasajeroMenorCollectionGetByMenorid", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeroMenorCollectionGetByMenorid' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PasajeroMenorCollectionGetByMenorid = DataRepository.PasajeroMenorProvider.GetByMenorid(transactionManager, entity.ClienteId);

				if (deep && entity.PasajeroMenorCollectionGetByMenorid.Count > 0)
				{
					deepHandles.Add("PasajeroMenorCollectionGetByMenorid",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PasajeroMenor>) DataRepository.PasajeroMenorProvider.DeepLoad,
						new object[] { transactionManager, entity.PasajeroMenorCollectionGetByMenorid, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region FacturaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Factura>|FacturaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'FacturaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.FacturaCollection = DataRepository.FacturaProvider.GetByClienteId(transactionManager, entity.ClienteId);

				if (deep && entity.FacturaCollection.Count > 0)
				{
					deepHandles.Add("FacturaCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Factura>) DataRepository.FacturaProvider.DeepLoad,
						new object[] { transactionManager, entity.FacturaCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PasajeroMenorCollectionGetByPasajeroid
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PasajeroMenor>|PasajeroMenorCollectionGetByPasajeroid", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeroMenorCollectionGetByPasajeroid' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PasajeroMenorCollectionGetByPasajeroid = DataRepository.PasajeroMenorProvider.GetByPasajeroid(transactionManager, entity.ClienteId);

				if (deep && entity.PasajeroMenorCollectionGetByPasajeroid.Count > 0)
				{
					deepHandles.Add("PasajeroMenorCollectionGetByPasajeroid",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PasajeroMenor>) DataRepository.PasajeroMenorProvider.DeepLoad,
						new object[] { transactionManager, entity.PasajeroMenorCollectionGetByPasajeroid, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region CuentaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Cuenta>|CuentaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'CuentaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.CuentaCollection = DataRepository.CuentaProvider.GetByClienteId(transactionManager, entity.ClienteId);

				if (deep && entity.CuentaCollection.Count > 0)
				{
					deepHandles.Add("CuentaCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Cuenta>) DataRepository.CuentaProvider.DeepLoad,
						new object[] { transactionManager, entity.CuentaCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region CuentaCorrienteCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<CuentaCorriente>|CuentaCorrienteCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'CuentaCorrienteCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.CuentaCorrienteCollection = DataRepository.CuentaCorrienteProvider.GetByClienteId(transactionManager, entity.ClienteId);

				if (deep && entity.CuentaCorrienteCollection.Count > 0)
				{
					deepHandles.Add("CuentaCorrienteCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<CuentaCorriente>) DataRepository.CuentaCorrienteProvider.DeepLoad,
						new object[] { transactionManager, entity.CuentaCorrienteCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Cliente object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Cliente instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Cliente Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Cliente entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<Pago>
				if (CanDeepSave(entity.PagoCollection, "List<Pago>|PagoCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Pago child in entity.PagoCollection)
					{
						if(child.ClienteIdSource != null)
						{
							child.ClienteId = child.ClienteIdSource.ClienteId;
						}
						else
						{
							child.ClienteId = entity.ClienteId;
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
				
	
			#region List<Nota>
				if (CanDeepSave(entity.NotaCollection, "List<Nota>|NotaCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Nota child in entity.NotaCollection)
					{
						if(child.ClienteIdSource != null)
						{
							child.ClienteId = child.ClienteIdSource.ClienteId;
						}
						else
						{
							child.ClienteId = entity.ClienteId;
						}

					}

					if (entity.NotaCollection.Count > 0 || entity.NotaCollection.DeletedItems.Count > 0)
					{
						//DataRepository.NotaProvider.Save(transactionManager, entity.NotaCollection);
						
						deepHandles.Add("NotaCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Nota >) DataRepository.NotaProvider.DeepSave,
							new object[] { transactionManager, entity.NotaCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<PasajeroMenor>
				if (CanDeepSave(entity.PasajeroMenorCollectionGetByMenorid, "List<PasajeroMenor>|PasajeroMenorCollectionGetByMenorid", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PasajeroMenor child in entity.PasajeroMenorCollectionGetByMenorid)
					{
						if(child.MenoridSource != null)
						{
							child.Menorid = child.MenoridSource.ClienteId;
						}
						else
						{
							child.Menorid = entity.ClienteId;
						}

					}

					if (entity.PasajeroMenorCollectionGetByMenorid.Count > 0 || entity.PasajeroMenorCollectionGetByMenorid.DeletedItems.Count > 0)
					{
						//DataRepository.PasajeroMenorProvider.Save(transactionManager, entity.PasajeroMenorCollectionGetByMenorid);
						
						deepHandles.Add("PasajeroMenorCollectionGetByMenorid",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PasajeroMenor >) DataRepository.PasajeroMenorProvider.DeepSave,
							new object[] { transactionManager, entity.PasajeroMenorCollectionGetByMenorid, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Factura>
				if (CanDeepSave(entity.FacturaCollection, "List<Factura>|FacturaCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Factura child in entity.FacturaCollection)
					{
						if(child.ClienteIdSource != null)
						{
							child.ClienteId = child.ClienteIdSource.ClienteId;
						}
						else
						{
							child.ClienteId = entity.ClienteId;
						}

					}

					if (entity.FacturaCollection.Count > 0 || entity.FacturaCollection.DeletedItems.Count > 0)
					{
						//DataRepository.FacturaProvider.Save(transactionManager, entity.FacturaCollection);
						
						deepHandles.Add("FacturaCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Factura >) DataRepository.FacturaProvider.DeepSave,
							new object[] { transactionManager, entity.FacturaCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<PasajeroMenor>
				if (CanDeepSave(entity.PasajeroMenorCollectionGetByPasajeroid, "List<PasajeroMenor>|PasajeroMenorCollectionGetByPasajeroid", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PasajeroMenor child in entity.PasajeroMenorCollectionGetByPasajeroid)
					{
						if(child.PasajeroidSource != null)
						{
							child.Pasajeroid = child.PasajeroidSource.ClienteId;
						}
						else
						{
							child.Pasajeroid = entity.ClienteId;
						}

					}

					if (entity.PasajeroMenorCollectionGetByPasajeroid.Count > 0 || entity.PasajeroMenorCollectionGetByPasajeroid.DeletedItems.Count > 0)
					{
						//DataRepository.PasajeroMenorProvider.Save(transactionManager, entity.PasajeroMenorCollectionGetByPasajeroid);
						
						deepHandles.Add("PasajeroMenorCollectionGetByPasajeroid",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PasajeroMenor >) DataRepository.PasajeroMenorProvider.DeepSave,
							new object[] { transactionManager, entity.PasajeroMenorCollectionGetByPasajeroid, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Cuenta>
				if (CanDeepSave(entity.CuentaCollection, "List<Cuenta>|CuentaCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Cuenta child in entity.CuentaCollection)
					{
						if(child.ClienteIdSource != null)
						{
							child.ClienteId = child.ClienteIdSource.ClienteId;
						}
						else
						{
							child.ClienteId = entity.ClienteId;
						}

					}

					if (entity.CuentaCollection.Count > 0 || entity.CuentaCollection.DeletedItems.Count > 0)
					{
						//DataRepository.CuentaProvider.Save(transactionManager, entity.CuentaCollection);
						
						deepHandles.Add("CuentaCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Cuenta >) DataRepository.CuentaProvider.DeepSave,
							new object[] { transactionManager, entity.CuentaCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<CuentaCorriente>
				if (CanDeepSave(entity.CuentaCorrienteCollection, "List<CuentaCorriente>|CuentaCorrienteCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(CuentaCorriente child in entity.CuentaCorrienteCollection)
					{
						if(child.ClienteIdSource != null)
						{
							child.ClienteId = child.ClienteIdSource.ClienteId;
						}
						else
						{
							child.ClienteId = entity.ClienteId;
						}

					}

					if (entity.CuentaCorrienteCollection.Count > 0 || entity.CuentaCorrienteCollection.DeletedItems.Count > 0)
					{
						//DataRepository.CuentaCorrienteProvider.Save(transactionManager, entity.CuentaCorrienteCollection);
						
						deepHandles.Add("CuentaCorrienteCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< CuentaCorriente >) DataRepository.CuentaCorrienteProvider.DeepSave,
							new object[] { transactionManager, entity.CuentaCorrienteCollection, deepSaveType, childTypes, innerList }
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
	
	#region ClienteChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Cliente</c>
	///</summary>
	public enum ClienteChildEntityTypes
	{
		///<summary>
		/// Collection of <c>Cliente</c> as OneToMany for PagoCollection
		///</summary>
		[ChildEntityType(typeof(TList<Pago>))]
		PagoCollection,
		///<summary>
		/// Collection of <c>Cliente</c> as OneToMany for NotaCollection
		///</summary>
		[ChildEntityType(typeof(TList<Nota>))]
		NotaCollection,
		///<summary>
		/// Collection of <c>Cliente</c> as OneToMany for PasajeroMenorCollection
		///</summary>
		[ChildEntityType(typeof(TList<PasajeroMenor>))]
		PasajeroMenorCollectionGetByMenorid,
		///<summary>
		/// Collection of <c>Cliente</c> as OneToMany for FacturaCollection
		///</summary>
		[ChildEntityType(typeof(TList<Factura>))]
		FacturaCollection,
		///<summary>
		/// Collection of <c>Cliente</c> as OneToMany for PasajeroMenorCollection
		///</summary>
		[ChildEntityType(typeof(TList<PasajeroMenor>))]
		PasajeroMenorCollectionGetByPasajeroid,
		///<summary>
		/// Collection of <c>Cliente</c> as OneToMany for CuentaCollection
		///</summary>
		[ChildEntityType(typeof(TList<Cuenta>))]
		CuentaCollection,
		///<summary>
		/// Collection of <c>Cliente</c> as OneToMany for CuentaCorrienteCollection
		///</summary>
		[ChildEntityType(typeof(TList<CuentaCorriente>))]
		CuentaCorrienteCollection,
	}
	
	#endregion ClienteChildEntityTypes
	
	#region ClienteFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ClienteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ClienteFilterBuilder : SqlFilterBuilder<ClienteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ClienteFilterBuilder class.
		/// </summary>
		public ClienteFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ClienteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ClienteFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ClienteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ClienteFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ClienteFilterBuilder
	
	#region ClienteParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ClienteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ClienteParameterBuilder : ParameterizedSqlFilterBuilder<ClienteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ClienteParameterBuilder class.
		/// </summary>
		public ClienteParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ClienteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ClienteParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ClienteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ClienteParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ClienteParameterBuilder
	
	#region ClienteSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ClienteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cliente"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ClienteSortBuilder : SqlSortBuilder<ClienteColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ClienteSqlSortBuilder class.
		/// </summary>
		public ClienteSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ClienteSortBuilder
	
} // end namespace
