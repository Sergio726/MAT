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
	/// This class is the base class for any <see cref="FacturaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class FacturaProviderBaseCore : EntityProviderBase<MAT.Entities.Factura, MAT.Entities.FacturaKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.FacturaKey key)
		{
			return Delete(transactionManager, key.FacturaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_facturaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _facturaId)
		{
			return Delete(null, _facturaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_facturaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _facturaId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Cliente key.
		///		FK_Factura_Cliente Description: 
		/// </summary>
		/// <param name="_clienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		public TList<Factura> GetByClienteId(System.Guid _clienteId)
		{
			int count = -1;
			return GetByClienteId(_clienteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Cliente key.
		///		FK_Factura_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		/// <remarks></remarks>
		public TList<Factura> GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Cliente key.
		///		FK_Factura_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		public TList<Factura> GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Cliente key.
		///		fkFacturaCliente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_clienteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		public TList<Factura> GetByClienteId(System.Guid _clienteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByClienteId(null, _clienteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Cliente key.
		///		fkFacturaCliente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_clienteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		public TList<Factura> GetByClienteId(System.Guid _clienteId, int start, int pageLength,out int count)
		{
			return GetByClienteId(null, _clienteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Cliente key.
		///		FK_Factura_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		public abstract TList<Factura> GetByClienteId(TransactionManager transactionManager, System.Guid _clienteId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Vendedor key.
		///		FK_Factura_Vendedor Description: 
		/// </summary>
		/// <param name="_vendedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		public TList<Factura> GetByVendedorId(System.Guid _vendedorId)
		{
			int count = -1;
			return GetByVendedorId(_vendedorId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Vendedor key.
		///		FK_Factura_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		/// <remarks></remarks>
		public TList<Factura> GetByVendedorId(TransactionManager transactionManager, System.Guid _vendedorId)
		{
			int count = -1;
			return GetByVendedorId(transactionManager, _vendedorId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Vendedor key.
		///		FK_Factura_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		public TList<Factura> GetByVendedorId(TransactionManager transactionManager, System.Guid _vendedorId, int start, int pageLength)
		{
			int count = -1;
			return GetByVendedorId(transactionManager, _vendedorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Vendedor key.
		///		fkFacturaVendedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_vendedorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		public TList<Factura> GetByVendedorId(System.Guid _vendedorId, int start, int pageLength)
		{
			int count =  -1;
			return GetByVendedorId(null, _vendedorId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Vendedor key.
		///		fkFacturaVendedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_vendedorId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		public TList<Factura> GetByVendedorId(System.Guid _vendedorId, int start, int pageLength,out int count)
		{
			return GetByVendedorId(null, _vendedorId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Factura_Vendedor key.
		///		FK_Factura_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Factura objects.</returns>
		public abstract TList<Factura> GetByVendedorId(TransactionManager transactionManager, System.Guid _vendedorId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Factura Get(TransactionManager transactionManager, MAT.Entities.FacturaKey key, int start, int pageLength)
		{
			return GetByFacturaId(transactionManager, key.FacturaId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Factura index.
		/// </summary>
		/// <param name="_facturaId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Factura"/> class.</returns>
		public MAT.Entities.Factura GetByFacturaId(System.Guid _facturaId)
		{
			int count = -1;
			return GetByFacturaId(null,_facturaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Factura index.
		/// </summary>
		/// <param name="_facturaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Factura"/> class.</returns>
		public MAT.Entities.Factura GetByFacturaId(System.Guid _facturaId, int start, int pageLength)
		{
			int count = -1;
			return GetByFacturaId(null, _facturaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Factura index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_facturaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Factura"/> class.</returns>
		public MAT.Entities.Factura GetByFacturaId(TransactionManager transactionManager, System.Guid _facturaId)
		{
			int count = -1;
			return GetByFacturaId(transactionManager, _facturaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Factura index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_facturaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Factura"/> class.</returns>
		public MAT.Entities.Factura GetByFacturaId(TransactionManager transactionManager, System.Guid _facturaId, int start, int pageLength)
		{
			int count = -1;
			return GetByFacturaId(transactionManager, _facturaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Factura index.
		/// </summary>
		/// <param name="_facturaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Factura"/> class.</returns>
		public MAT.Entities.Factura GetByFacturaId(System.Guid _facturaId, int start, int pageLength, out int count)
		{
			return GetByFacturaId(null, _facturaId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Factura index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_facturaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Factura"/> class.</returns>
		public abstract MAT.Entities.Factura GetByFacturaId(TransactionManager transactionManager, System.Guid _facturaId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Factura&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Factura&gt;"/></returns>
		public static TList<Factura> Fill(IDataReader reader, TList<Factura> rows, int start, int pageLength)
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
				
				MAT.Entities.Factura c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Factura")
					.Append("|").Append((System.Guid)reader[((int)FacturaColumn.FacturaId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Factura>(
					key.ToString(), // EntityTrackingKey
					"Factura",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Factura();
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
					c.FacturaId = (System.Guid)reader[((int)FacturaColumn.FacturaId - 1)];
					c.OriginalFacturaId = c.FacturaId;
					c.NroFactura = (reader.IsDBNull(((int)FacturaColumn.NroFactura - 1)))?null:(System.String)reader[((int)FacturaColumn.NroFactura - 1)];
					c.Monto = (reader.IsDBNull(((int)FacturaColumn.Monto - 1)))?null:(System.Double?)reader[((int)FacturaColumn.Monto - 1)];
					c.Fecha = (reader.IsDBNull(((int)FacturaColumn.Fecha - 1)))?null:(System.DateTime?)reader[((int)FacturaColumn.Fecha - 1)];
					c.Tipo = (reader.IsDBNull(((int)FacturaColumn.Tipo - 1)))?null:(System.Int32?)reader[((int)FacturaColumn.Tipo - 1)];
					c.Estado = (reader.IsDBNull(((int)FacturaColumn.Estado - 1)))?null:(System.Int32?)reader[((int)FacturaColumn.Estado - 1)];
					c.ClienteId = (System.Guid)reader[((int)FacturaColumn.ClienteId - 1)];
					c.VendedorId = (System.Guid)reader[((int)FacturaColumn.VendedorId - 1)];
					c.DescuentoAplicado = (System.Double)reader[((int)FacturaColumn.DescuentoAplicado - 1)];
					c.Observaciones = (reader.IsDBNull(((int)FacturaColumn.Observaciones - 1)))?null:(System.String)reader[((int)FacturaColumn.Observaciones - 1)];
					c.DiasPreReserva = (reader.IsDBNull(((int)FacturaColumn.DiasPreReserva - 1)))?null:(System.Int32?)reader[((int)FacturaColumn.DiasPreReserva - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Factura"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Factura"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Factura entity)
		{
			if (!reader.Read()) return;
			
			entity.FacturaId = (System.Guid)reader[((int)FacturaColumn.FacturaId - 1)];
			entity.OriginalFacturaId = (System.Guid)reader["FacturaID"];
			entity.NroFactura = (reader.IsDBNull(((int)FacturaColumn.NroFactura - 1)))?null:(System.String)reader[((int)FacturaColumn.NroFactura - 1)];
			entity.Monto = (reader.IsDBNull(((int)FacturaColumn.Monto - 1)))?null:(System.Double?)reader[((int)FacturaColumn.Monto - 1)];
			entity.Fecha = (reader.IsDBNull(((int)FacturaColumn.Fecha - 1)))?null:(System.DateTime?)reader[((int)FacturaColumn.Fecha - 1)];
			entity.Tipo = (reader.IsDBNull(((int)FacturaColumn.Tipo - 1)))?null:(System.Int32?)reader[((int)FacturaColumn.Tipo - 1)];
			entity.Estado = (reader.IsDBNull(((int)FacturaColumn.Estado - 1)))?null:(System.Int32?)reader[((int)FacturaColumn.Estado - 1)];
			entity.ClienteId = (System.Guid)reader[((int)FacturaColumn.ClienteId - 1)];
			entity.VendedorId = (System.Guid)reader[((int)FacturaColumn.VendedorId - 1)];
			entity.DescuentoAplicado = (System.Double)reader[((int)FacturaColumn.DescuentoAplicado - 1)];
			entity.Observaciones = (reader.IsDBNull(((int)FacturaColumn.Observaciones - 1)))?null:(System.String)reader[((int)FacturaColumn.Observaciones - 1)];
			entity.DiasPreReserva = (reader.IsDBNull(((int)FacturaColumn.DiasPreReserva - 1)))?null:(System.Int32?)reader[((int)FacturaColumn.DiasPreReserva - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Factura"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Factura"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Factura entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.FacturaId = (System.Guid)dataRow["FacturaID"];
			entity.OriginalFacturaId = (System.Guid)dataRow["FacturaID"];
			entity.NroFactura = Convert.IsDBNull(dataRow["NroFactura"]) ? null : (System.String)dataRow["NroFactura"];
			entity.Monto = Convert.IsDBNull(dataRow["Monto"]) ? null : (System.Double?)dataRow["Monto"];
			entity.Fecha = Convert.IsDBNull(dataRow["Fecha"]) ? null : (System.DateTime?)dataRow["Fecha"];
			entity.Tipo = Convert.IsDBNull(dataRow["Tipo"]) ? null : (System.Int32?)dataRow["Tipo"];
			entity.Estado = Convert.IsDBNull(dataRow["Estado"]) ? null : (System.Int32?)dataRow["Estado"];
			entity.ClienteId = (System.Guid)dataRow["ClienteID"];
			entity.VendedorId = (System.Guid)dataRow["VendedorID"];
			entity.DescuentoAplicado = (System.Double)dataRow["DescuentoAplicado"];
			entity.Observaciones = Convert.IsDBNull(dataRow["Observaciones"]) ? null : (System.String)dataRow["Observaciones"];
			entity.DiasPreReserva = Convert.IsDBNull(dataRow["DiasPreReserva"]) ? null : (System.Int32?)dataRow["DiasPreReserva"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Factura"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Factura Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Factura entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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

			#region VendedorIdSource	
			if (CanDeepLoad(entity, "Vendedor|VendedorIdSource", deepLoadType, innerList) 
				&& entity.VendedorIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.VendedorId;
				Vendedor tmpEntity = EntityManager.LocateEntity<Vendedor>(EntityLocator.ConstructKeyFromPkItems(typeof(Vendedor), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.VendedorIdSource = tmpEntity;
				else
					entity.VendedorIdSource = DataRepository.VendedorProvider.GetByVendedorId(transactionManager, entity.VendedorId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'VendedorIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.VendedorIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.VendedorProvider.DeepLoad(transactionManager, entity.VendedorIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion VendedorIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByFacturaId methods when available
			
			#region MovimientoCuentaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<MovimientoCuenta>|MovimientoCuentaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MovimientoCuentaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.MovimientoCuentaCollection = DataRepository.MovimientoCuentaProvider.GetByFacturaId(transactionManager, entity.FacturaId);

				if (deep && entity.MovimientoCuentaCollection.Count > 0)
				{
					deepHandles.Add("MovimientoCuentaCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<MovimientoCuenta>) DataRepository.MovimientoCuentaProvider.DeepLoad,
						new object[] { transactionManager, entity.MovimientoCuentaCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PasajeCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Pasaje>|PasajeCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PasajeCollection = DataRepository.PasajeProvider.GetByFacturaId(transactionManager, entity.FacturaId);

				if (deep && entity.PasajeCollection.Count > 0)
				{
					deepHandles.Add("PasajeCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Pasaje>) DataRepository.PasajeProvider.DeepLoad,
						new object[] { transactionManager, entity.PasajeCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Factura object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Factura instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Factura Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Factura entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
			
			#region VendedorIdSource
			if (CanDeepSave(entity, "Vendedor|VendedorIdSource", deepSaveType, innerList) 
				&& entity.VendedorIdSource != null)
			{
				DataRepository.VendedorProvider.Save(transactionManager, entity.VendedorIdSource);
				entity.VendedorId = entity.VendedorIdSource.VendedorId;
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
						if(child.FacturaIdSource != null)
						{
							child.FacturaId = child.FacturaIdSource.FacturaId;
						}
						else
						{
							child.FacturaId = entity.FacturaId;
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
				
	
			#region List<Pasaje>
				if (CanDeepSave(entity.PasajeCollection, "List<Pasaje>|PasajeCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Pasaje child in entity.PasajeCollection)
					{
						if(child.FacturaIdSource != null)
						{
							child.FacturaId = child.FacturaIdSource.FacturaId;
						}
						else
						{
							child.FacturaId = entity.FacturaId;
						}

					}

					if (entity.PasajeCollection.Count > 0 || entity.PasajeCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PasajeProvider.Save(transactionManager, entity.PasajeCollection);
						
						deepHandles.Add("PasajeCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Pasaje >) DataRepository.PasajeProvider.DeepSave,
							new object[] { transactionManager, entity.PasajeCollection, deepSaveType, childTypes, innerList }
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
	
	#region FacturaChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Factura</c>
	///</summary>
	public enum FacturaChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Cliente</c> at ClienteIdSource
		///</summary>
		[ChildEntityType(typeof(Cliente))]
		Cliente,
		
		///<summary>
		/// Composite Property for <c>Vendedor</c> at VendedorIdSource
		///</summary>
		[ChildEntityType(typeof(Vendedor))]
		Vendedor,
		///<summary>
		/// Collection of <c>Factura</c> as OneToMany for MovimientoCuentaCollection
		///</summary>
		[ChildEntityType(typeof(TList<MovimientoCuenta>))]
		MovimientoCuentaCollection,
		///<summary>
		/// Collection of <c>Factura</c> as OneToMany for PasajeCollection
		///</summary>
		[ChildEntityType(typeof(TList<Pasaje>))]
		PasajeCollection,
	}
	
	#endregion FacturaChildEntityTypes
	
	#region FacturaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;FacturaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Factura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FacturaFilterBuilder : SqlFilterBuilder<FacturaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FacturaFilterBuilder class.
		/// </summary>
		public FacturaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the FacturaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public FacturaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the FacturaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public FacturaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion FacturaFilterBuilder
	
	#region FacturaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;FacturaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Factura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FacturaParameterBuilder : ParameterizedSqlFilterBuilder<FacturaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FacturaParameterBuilder class.
		/// </summary>
		public FacturaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the FacturaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public FacturaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the FacturaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public FacturaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion FacturaParameterBuilder
	
	#region FacturaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;FacturaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Factura"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class FacturaSortBuilder : SqlSortBuilder<FacturaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FacturaSqlSortBuilder class.
		/// </summary>
		public FacturaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion FacturaSortBuilder
	
} // end namespace
