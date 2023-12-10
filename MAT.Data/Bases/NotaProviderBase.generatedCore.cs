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
	/// This class is the base class for any <see cref="NotaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class NotaProviderBaseCore : EntityProviderBase<MAT.Entities.Nota, MAT.Entities.NotaKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.NotaKey key)
		{
			return Delete(transactionManager, key.NotaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_notaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _notaId)
		{
			return Delete(null, _notaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_notaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _notaId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Cliente key.
		///		FK_Nota_Cliente Description: 
		/// </summary>
		/// <param name="_clienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		public TList<Nota> GetByClienteId(System.Guid? _clienteId)
		{
			int count = -1;
			return GetByClienteId(_clienteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Cliente key.
		///		FK_Nota_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		/// <remarks></remarks>
		public TList<Nota> GetByClienteId(TransactionManager transactionManager, System.Guid? _clienteId)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Cliente key.
		///		FK_Nota_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		public TList<Nota> GetByClienteId(TransactionManager transactionManager, System.Guid? _clienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Cliente key.
		///		fkNotaCliente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_clienteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		public TList<Nota> GetByClienteId(System.Guid? _clienteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByClienteId(null, _clienteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Cliente key.
		///		fkNotaCliente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_clienteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		public TList<Nota> GetByClienteId(System.Guid? _clienteId, int start, int pageLength,out int count)
		{
			return GetByClienteId(null, _clienteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Cliente key.
		///		FK_Nota_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		public abstract TList<Nota> GetByClienteId(TransactionManager transactionManager, System.Guid? _clienteId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Vendedor key.
		///		FK_Nota_Vendedor Description: 
		/// </summary>
		/// <param name="_vendedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		public TList<Nota> GetByVendedorId(System.Guid? _vendedorId)
		{
			int count = -1;
			return GetByVendedorId(_vendedorId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Vendedor key.
		///		FK_Nota_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		/// <remarks></remarks>
		public TList<Nota> GetByVendedorId(TransactionManager transactionManager, System.Guid? _vendedorId)
		{
			int count = -1;
			return GetByVendedorId(transactionManager, _vendedorId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Vendedor key.
		///		FK_Nota_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		public TList<Nota> GetByVendedorId(TransactionManager transactionManager, System.Guid? _vendedorId, int start, int pageLength)
		{
			int count = -1;
			return GetByVendedorId(transactionManager, _vendedorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Vendedor key.
		///		fkNotaVendedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_vendedorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		public TList<Nota> GetByVendedorId(System.Guid? _vendedorId, int start, int pageLength)
		{
			int count =  -1;
			return GetByVendedorId(null, _vendedorId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Vendedor key.
		///		fkNotaVendedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_vendedorId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		public TList<Nota> GetByVendedorId(System.Guid? _vendedorId, int start, int pageLength,out int count)
		{
			return GetByVendedorId(null, _vendedorId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Nota_Vendedor key.
		///		FK_Nota_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Nota objects.</returns>
		public abstract TList<Nota> GetByVendedorId(TransactionManager transactionManager, System.Guid? _vendedorId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Nota Get(TransactionManager transactionManager, MAT.Entities.NotaKey key, int start, int pageLength)
		{
			return GetByNotaId(transactionManager, key.NotaId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Nota index.
		/// </summary>
		/// <param name="_notaId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Nota"/> class.</returns>
		public MAT.Entities.Nota GetByNotaId(System.Guid _notaId)
		{
			int count = -1;
			return GetByNotaId(null,_notaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Nota index.
		/// </summary>
		/// <param name="_notaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Nota"/> class.</returns>
		public MAT.Entities.Nota GetByNotaId(System.Guid _notaId, int start, int pageLength)
		{
			int count = -1;
			return GetByNotaId(null, _notaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Nota index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_notaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Nota"/> class.</returns>
		public MAT.Entities.Nota GetByNotaId(TransactionManager transactionManager, System.Guid _notaId)
		{
			int count = -1;
			return GetByNotaId(transactionManager, _notaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Nota index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_notaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Nota"/> class.</returns>
		public MAT.Entities.Nota GetByNotaId(TransactionManager transactionManager, System.Guid _notaId, int start, int pageLength)
		{
			int count = -1;
			return GetByNotaId(transactionManager, _notaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Nota index.
		/// </summary>
		/// <param name="_notaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Nota"/> class.</returns>
		public MAT.Entities.Nota GetByNotaId(System.Guid _notaId, int start, int pageLength, out int count)
		{
			return GetByNotaId(null, _notaId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Nota index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_notaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Nota"/> class.</returns>
		public abstract MAT.Entities.Nota GetByNotaId(TransactionManager transactionManager, System.Guid _notaId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Nota&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Nota&gt;"/></returns>
		public static TList<Nota> Fill(IDataReader reader, TList<Nota> rows, int start, int pageLength)
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
				
				MAT.Entities.Nota c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Nota")
					.Append("|").Append((System.Guid)reader[((int)NotaColumn.NotaId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Nota>(
					key.ToString(), // EntityTrackingKey
					"Nota",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Nota();
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
					c.NotaId = (System.Guid)reader[((int)NotaColumn.NotaId - 1)];
					c.OriginalNotaId = c.NotaId;
					c.PorcentajeRetencion = (reader.IsDBNull(((int)NotaColumn.PorcentajeRetencion - 1)))?null:(System.Double?)reader[((int)NotaColumn.PorcentajeRetencion - 1)];
					c.MontoRetencion = (reader.IsDBNull(((int)NotaColumn.MontoRetencion - 1)))?null:(System.Double?)reader[((int)NotaColumn.MontoRetencion - 1)];
					c.Fecha = (reader.IsDBNull(((int)NotaColumn.Fecha - 1)))?null:(System.DateTime?)reader[((int)NotaColumn.Fecha - 1)];
					c.Dias = (reader.IsDBNull(((int)NotaColumn.Dias - 1)))?null:(System.Int32?)reader[((int)NotaColumn.Dias - 1)];
					c.ClienteId = (reader.IsDBNull(((int)NotaColumn.ClienteId - 1)))?null:(System.Guid?)reader[((int)NotaColumn.ClienteId - 1)];
					c.VendedorId = (reader.IsDBNull(((int)NotaColumn.VendedorId - 1)))?null:(System.Guid?)reader[((int)NotaColumn.VendedorId - 1)];
					c.NroNota = (reader.IsDBNull(((int)NotaColumn.NroNota - 1)))?null:(System.String)reader[((int)NotaColumn.NroNota - 1)];
					c.MontoNota = (reader.IsDBNull(((int)NotaColumn.MontoNota - 1)))?null:(System.Double?)reader[((int)NotaColumn.MontoNota - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Nota"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Nota"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Nota entity)
		{
			if (!reader.Read()) return;
			
			entity.NotaId = (System.Guid)reader[((int)NotaColumn.NotaId - 1)];
			entity.OriginalNotaId = (System.Guid)reader["NotaID"];
			entity.PorcentajeRetencion = (reader.IsDBNull(((int)NotaColumn.PorcentajeRetencion - 1)))?null:(System.Double?)reader[((int)NotaColumn.PorcentajeRetencion - 1)];
			entity.MontoRetencion = (reader.IsDBNull(((int)NotaColumn.MontoRetencion - 1)))?null:(System.Double?)reader[((int)NotaColumn.MontoRetencion - 1)];
			entity.Fecha = (reader.IsDBNull(((int)NotaColumn.Fecha - 1)))?null:(System.DateTime?)reader[((int)NotaColumn.Fecha - 1)];
			entity.Dias = (reader.IsDBNull(((int)NotaColumn.Dias - 1)))?null:(System.Int32?)reader[((int)NotaColumn.Dias - 1)];
			entity.ClienteId = (reader.IsDBNull(((int)NotaColumn.ClienteId - 1)))?null:(System.Guid?)reader[((int)NotaColumn.ClienteId - 1)];
			entity.VendedorId = (reader.IsDBNull(((int)NotaColumn.VendedorId - 1)))?null:(System.Guid?)reader[((int)NotaColumn.VendedorId - 1)];
			entity.NroNota = (reader.IsDBNull(((int)NotaColumn.NroNota - 1)))?null:(System.String)reader[((int)NotaColumn.NroNota - 1)];
			entity.MontoNota = (reader.IsDBNull(((int)NotaColumn.MontoNota - 1)))?null:(System.Double?)reader[((int)NotaColumn.MontoNota - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Nota"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Nota"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Nota entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.NotaId = (System.Guid)dataRow["NotaID"];
			entity.OriginalNotaId = (System.Guid)dataRow["NotaID"];
			entity.PorcentajeRetencion = Convert.IsDBNull(dataRow["PorcentajeRetencion"]) ? null : (System.Double?)dataRow["PorcentajeRetencion"];
			entity.MontoRetencion = Convert.IsDBNull(dataRow["MontoRetencion"]) ? null : (System.Double?)dataRow["MontoRetencion"];
			entity.Fecha = Convert.IsDBNull(dataRow["Fecha"]) ? null : (System.DateTime?)dataRow["Fecha"];
			entity.Dias = Convert.IsDBNull(dataRow["Dias"]) ? null : (System.Int32?)dataRow["Dias"];
			entity.ClienteId = Convert.IsDBNull(dataRow["ClienteID"]) ? null : (System.Guid?)dataRow["ClienteID"];
			entity.VendedorId = Convert.IsDBNull(dataRow["VendedorID"]) ? null : (System.Guid?)dataRow["VendedorID"];
			entity.NroNota = Convert.IsDBNull(dataRow["NroNota"]) ? null : (System.String)dataRow["NroNota"];
			entity.MontoNota = Convert.IsDBNull(dataRow["MontoNota"]) ? null : (System.Double?)dataRow["MontoNota"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Nota"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Nota Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Nota entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ClienteIdSource	
			if (CanDeepLoad(entity, "Cliente|ClienteIdSource", deepLoadType, innerList) 
				&& entity.ClienteIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.ClienteId ?? Guid.Empty);
				Cliente tmpEntity = EntityManager.LocateEntity<Cliente>(EntityLocator.ConstructKeyFromPkItems(typeof(Cliente), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ClienteIdSource = tmpEntity;
				else
					entity.ClienteIdSource = DataRepository.ClienteProvider.GetByClienteId(transactionManager, (entity.ClienteId ?? Guid.Empty));		
				
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
				pkItems[0] = (entity.VendedorId ?? Guid.Empty);
				Vendedor tmpEntity = EntityManager.LocateEntity<Vendedor>(EntityLocator.ConstructKeyFromPkItems(typeof(Vendedor), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.VendedorIdSource = tmpEntity;
				else
					entity.VendedorIdSource = DataRepository.VendedorProvider.GetByVendedorId(transactionManager, (entity.VendedorId ?? Guid.Empty));		
				
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
			// Deep load child collections  - Call GetByNotaId methods when available
			
			#region MovimientoCuentaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<MovimientoCuenta>|MovimientoCuentaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MovimientoCuentaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.MovimientoCuentaCollection = DataRepository.MovimientoCuentaProvider.GetByNotaId(transactionManager, entity.NotaId);

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
		/// Deep Save the entire object graph of the MAT.Entities.Nota object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Nota instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Nota Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Nota entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
						if(child.NotaIdSource != null)
						{
							child.NotaId = child.NotaIdSource.NotaId;
						}
						else
						{
							child.NotaId = entity.NotaId;
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
	
	#region NotaChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Nota</c>
	///</summary>
	public enum NotaChildEntityTypes
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
		/// Collection of <c>Nota</c> as OneToMany for MovimientoCuentaCollection
		///</summary>
		[ChildEntityType(typeof(TList<MovimientoCuenta>))]
		MovimientoCuentaCollection,
	}
	
	#endregion NotaChildEntityTypes
	
	#region NotaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;NotaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Nota"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NotaFilterBuilder : SqlFilterBuilder<NotaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NotaFilterBuilder class.
		/// </summary>
		public NotaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the NotaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public NotaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the NotaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public NotaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion NotaFilterBuilder
	
	#region NotaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;NotaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Nota"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NotaParameterBuilder : ParameterizedSqlFilterBuilder<NotaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NotaParameterBuilder class.
		/// </summary>
		public NotaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the NotaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public NotaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the NotaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public NotaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion NotaParameterBuilder
	
	#region NotaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;NotaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Nota"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class NotaSortBuilder : SqlSortBuilder<NotaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NotaSqlSortBuilder class.
		/// </summary>
		public NotaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion NotaSortBuilder
	
} // end namespace
