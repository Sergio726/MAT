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
	/// This class is the base class for any <see cref="PagoProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PagoProviderBaseCore : EntityProviderBase<MAT.Entities.Pago, MAT.Entities.PagoKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PagoKey key)
		{
			return Delete(transactionManager, key.PagoId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_pagoId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _pagoId)
		{
			return Delete(null, _pagoId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pagoId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _pagoId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Cliente key.
		///		FK_Pago_Cliente Description: 
		/// </summary>
		/// <param name="_clienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByClienteId(System.Guid? _clienteId)
		{
			int count = -1;
			return GetByClienteId(_clienteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Cliente key.
		///		FK_Pago_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		/// <remarks></remarks>
		public TList<Pago> GetByClienteId(TransactionManager transactionManager, System.Guid? _clienteId)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Cliente key.
		///		FK_Pago_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByClienteId(TransactionManager transactionManager, System.Guid? _clienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByClienteId(transactionManager, _clienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Cliente key.
		///		fkPagoCliente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_clienteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByClienteId(System.Guid? _clienteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByClienteId(null, _clienteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Cliente key.
		///		fkPagoCliente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_clienteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByClienteId(System.Guid? _clienteId, int start, int pageLength,out int count)
		{
			return GetByClienteId(null, _clienteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Cliente key.
		///		FK_Pago_Cliente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_clienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public abstract TList<Pago> GetByClienteId(TransactionManager transactionManager, System.Guid? _clienteId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_CuentaCorriente key.
		///		FK_Pago_CuentaCorriente Description: 
		/// </summary>
		/// <param name="_cuentaCorrienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByCuentaCorrienteId(System.Guid? _cuentaCorrienteId)
		{
			int count = -1;
			return GetByCuentaCorrienteId(_cuentaCorrienteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_CuentaCorriente key.
		///		FK_Pago_CuentaCorriente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		/// <remarks></remarks>
		public TList<Pago> GetByCuentaCorrienteId(TransactionManager transactionManager, System.Guid? _cuentaCorrienteId)
		{
			int count = -1;
			return GetByCuentaCorrienteId(transactionManager, _cuentaCorrienteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_CuentaCorriente key.
		///		FK_Pago_CuentaCorriente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByCuentaCorrienteId(TransactionManager transactionManager, System.Guid? _cuentaCorrienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByCuentaCorrienteId(transactionManager, _cuentaCorrienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_CuentaCorriente key.
		///		fkPagoCuentaCorriente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByCuentaCorrienteId(System.Guid? _cuentaCorrienteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByCuentaCorrienteId(null, _cuentaCorrienteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_CuentaCorriente key.
		///		fkPagoCuentaCorriente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByCuentaCorrienteId(System.Guid? _cuentaCorrienteId, int start, int pageLength,out int count)
		{
			return GetByCuentaCorrienteId(null, _cuentaCorrienteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_CuentaCorriente key.
		///		FK_Pago_CuentaCorriente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public abstract TList<Pago> GetByCuentaCorrienteId(TransactionManager transactionManager, System.Guid? _cuentaCorrienteId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Vendedor key.
		///		FK_Pago_Vendedor Description: 
		/// </summary>
		/// <param name="_vendedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByVendedorId(System.Guid? _vendedorId)
		{
			int count = -1;
			return GetByVendedorId(_vendedorId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Vendedor key.
		///		FK_Pago_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		/// <remarks></remarks>
		public TList<Pago> GetByVendedorId(TransactionManager transactionManager, System.Guid? _vendedorId)
		{
			int count = -1;
			return GetByVendedorId(transactionManager, _vendedorId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Vendedor key.
		///		FK_Pago_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByVendedorId(TransactionManager transactionManager, System.Guid? _vendedorId, int start, int pageLength)
		{
			int count = -1;
			return GetByVendedorId(transactionManager, _vendedorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Vendedor key.
		///		fkPagoVendedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_vendedorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByVendedorId(System.Guid? _vendedorId, int start, int pageLength)
		{
			int count =  -1;
			return GetByVendedorId(null, _vendedorId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Vendedor key.
		///		fkPagoVendedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_vendedorId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public TList<Pago> GetByVendedorId(System.Guid? _vendedorId, int start, int pageLength,out int count)
		{
			return GetByVendedorId(null, _vendedorId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pago_Vendedor key.
		///		FK_Pago_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Pago objects.</returns>
		public abstract TList<Pago> GetByVendedorId(TransactionManager transactionManager, System.Guid? _vendedorId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Pago Get(TransactionManager transactionManager, MAT.Entities.PagoKey key, int start, int pageLength)
		{
			return GetByPagoId(transactionManager, key.PagoId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Pago index.
		/// </summary>
		/// <param name="_pagoId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pago"/> class.</returns>
		public MAT.Entities.Pago GetByPagoId(System.Guid _pagoId)
		{
			int count = -1;
			return GetByPagoId(null,_pagoId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pago index.
		/// </summary>
		/// <param name="_pagoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pago"/> class.</returns>
		public MAT.Entities.Pago GetByPagoId(System.Guid _pagoId, int start, int pageLength)
		{
			int count = -1;
			return GetByPagoId(null, _pagoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pago index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pagoId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pago"/> class.</returns>
		public MAT.Entities.Pago GetByPagoId(TransactionManager transactionManager, System.Guid _pagoId)
		{
			int count = -1;
			return GetByPagoId(transactionManager, _pagoId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pago index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pagoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pago"/> class.</returns>
		public MAT.Entities.Pago GetByPagoId(TransactionManager transactionManager, System.Guid _pagoId, int start, int pageLength)
		{
			int count = -1;
			return GetByPagoId(transactionManager, _pagoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pago index.
		/// </summary>
		/// <param name="_pagoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pago"/> class.</returns>
		public MAT.Entities.Pago GetByPagoId(System.Guid _pagoId, int start, int pageLength, out int count)
		{
			return GetByPagoId(null, _pagoId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pago index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pagoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pago"/> class.</returns>
		public abstract MAT.Entities.Pago GetByPagoId(TransactionManager transactionManager, System.Guid _pagoId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Pago&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Pago&gt;"/></returns>
		public static TList<Pago> Fill(IDataReader reader, TList<Pago> rows, int start, int pageLength)
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
				
				MAT.Entities.Pago c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Pago")
					.Append("|").Append((System.Guid)reader[((int)PagoColumn.PagoId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Pago>(
					key.ToString(), // EntityTrackingKey
					"Pago",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Pago();
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
					c.PagoId = (System.Guid)reader[((int)PagoColumn.PagoId - 1)];
					c.OriginalPagoId = c.PagoId;
					c.FechaPago = (reader.IsDBNull(((int)PagoColumn.FechaPago - 1)))?null:(System.DateTime?)reader[((int)PagoColumn.FechaPago - 1)];
					c.Monto = (reader.IsDBNull(((int)PagoColumn.Monto - 1)))?null:(System.Double?)reader[((int)PagoColumn.Monto - 1)];
					c.TipoPago = (reader.IsDBNull(((int)PagoColumn.TipoPago - 1)))?null:(System.Int32?)reader[((int)PagoColumn.TipoPago - 1)];
					c.TransaccionId = (reader.IsDBNull(((int)PagoColumn.TransaccionId - 1)))?null:(System.String)reader[((int)PagoColumn.TransaccionId - 1)];
					c.ClienteId = (reader.IsDBNull(((int)PagoColumn.ClienteId - 1)))?null:(System.Guid?)reader[((int)PagoColumn.ClienteId - 1)];
					c.VendedorId = (reader.IsDBNull(((int)PagoColumn.VendedorId - 1)))?null:(System.Guid?)reader[((int)PagoColumn.VendedorId - 1)];
					c.NroRecibo = (reader.IsDBNull(((int)PagoColumn.NroRecibo - 1)))?null:(System.String)reader[((int)PagoColumn.NroRecibo - 1)];
					c.EstadoRendicion = (reader.IsDBNull(((int)PagoColumn.EstadoRendicion - 1)))?null:(System.Int32?)reader[((int)PagoColumn.EstadoRendicion - 1)];
					c.CuentaCorrienteId = (reader.IsDBNull(((int)PagoColumn.CuentaCorrienteId - 1)))?null:(System.Guid?)reader[((int)PagoColumn.CuentaCorrienteId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Pago"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Pago"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Pago entity)
		{
			if (!reader.Read()) return;
			
			entity.PagoId = (System.Guid)reader[((int)PagoColumn.PagoId - 1)];
			entity.OriginalPagoId = (System.Guid)reader["PagoID"];
			entity.FechaPago = (reader.IsDBNull(((int)PagoColumn.FechaPago - 1)))?null:(System.DateTime?)reader[((int)PagoColumn.FechaPago - 1)];
			entity.Monto = (reader.IsDBNull(((int)PagoColumn.Monto - 1)))?null:(System.Double?)reader[((int)PagoColumn.Monto - 1)];
			entity.TipoPago = (reader.IsDBNull(((int)PagoColumn.TipoPago - 1)))?null:(System.Int32?)reader[((int)PagoColumn.TipoPago - 1)];
			entity.TransaccionId = (reader.IsDBNull(((int)PagoColumn.TransaccionId - 1)))?null:(System.String)reader[((int)PagoColumn.TransaccionId - 1)];
			entity.ClienteId = (reader.IsDBNull(((int)PagoColumn.ClienteId - 1)))?null:(System.Guid?)reader[((int)PagoColumn.ClienteId - 1)];
			entity.VendedorId = (reader.IsDBNull(((int)PagoColumn.VendedorId - 1)))?null:(System.Guid?)reader[((int)PagoColumn.VendedorId - 1)];
			entity.NroRecibo = (reader.IsDBNull(((int)PagoColumn.NroRecibo - 1)))?null:(System.String)reader[((int)PagoColumn.NroRecibo - 1)];
			entity.EstadoRendicion = (reader.IsDBNull(((int)PagoColumn.EstadoRendicion - 1)))?null:(System.Int32?)reader[((int)PagoColumn.EstadoRendicion - 1)];
			entity.CuentaCorrienteId = (reader.IsDBNull(((int)PagoColumn.CuentaCorrienteId - 1)))?null:(System.Guid?)reader[((int)PagoColumn.CuentaCorrienteId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Pago"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Pago"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Pago entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PagoId = (System.Guid)dataRow["PagoID"];
			entity.OriginalPagoId = (System.Guid)dataRow["PagoID"];
			entity.FechaPago = Convert.IsDBNull(dataRow["FechaPago"]) ? null : (System.DateTime?)dataRow["FechaPago"];
			entity.Monto = Convert.IsDBNull(dataRow["Monto"]) ? null : (System.Double?)dataRow["Monto"];
			entity.TipoPago = Convert.IsDBNull(dataRow["TipoPago"]) ? null : (System.Int32?)dataRow["TipoPago"];
			entity.TransaccionId = Convert.IsDBNull(dataRow["TransaccionID"]) ? null : (System.String)dataRow["TransaccionID"];
			entity.ClienteId = Convert.IsDBNull(dataRow["ClienteId"]) ? null : (System.Guid?)dataRow["ClienteId"];
			entity.VendedorId = Convert.IsDBNull(dataRow["VendedorId"]) ? null : (System.Guid?)dataRow["VendedorId"];
			entity.NroRecibo = Convert.IsDBNull(dataRow["NroRecibo"]) ? null : (System.String)dataRow["NroRecibo"];
			entity.EstadoRendicion = Convert.IsDBNull(dataRow["EstadoRendicion"]) ? null : (System.Int32?)dataRow["EstadoRendicion"];
			entity.CuentaCorrienteId = Convert.IsDBNull(dataRow["CuentaCorrienteID"]) ? null : (System.Guid?)dataRow["CuentaCorrienteID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Pago"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Pago Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Pago entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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

			#region CuentaCorrienteIdSource	
			if (CanDeepLoad(entity, "CuentaCorriente|CuentaCorrienteIdSource", deepLoadType, innerList) 
				&& entity.CuentaCorrienteIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.CuentaCorrienteId ?? Guid.Empty);
				CuentaCorriente tmpEntity = EntityManager.LocateEntity<CuentaCorriente>(EntityLocator.ConstructKeyFromPkItems(typeof(CuentaCorriente), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.CuentaCorrienteIdSource = tmpEntity;
				else
					entity.CuentaCorrienteIdSource = DataRepository.CuentaCorrienteProvider.GetByCuentaCorrienteId(transactionManager, (entity.CuentaCorrienteId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'CuentaCorrienteIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.CuentaCorrienteIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.CuentaCorrienteProvider.DeepLoad(transactionManager, entity.CuentaCorrienteIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion CuentaCorrienteIdSource

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
			// Deep load child collections  - Call GetByPagoId methods when available
			
			#region MovimientoCuentaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<MovimientoCuenta>|MovimientoCuentaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MovimientoCuentaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.MovimientoCuentaCollection = DataRepository.MovimientoCuentaProvider.GetByPagoId(transactionManager, entity.PagoId);

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
		/// Deep Save the entire object graph of the MAT.Entities.Pago object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Pago instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Pago Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Pago entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
			
			#region CuentaCorrienteIdSource
			if (CanDeepSave(entity, "CuentaCorriente|CuentaCorrienteIdSource", deepSaveType, innerList) 
				&& entity.CuentaCorrienteIdSource != null)
			{
				DataRepository.CuentaCorrienteProvider.Save(transactionManager, entity.CuentaCorrienteIdSource);
				entity.CuentaCorrienteId = entity.CuentaCorrienteIdSource.CuentaCorrienteId;
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
						if(child.PagoIdSource != null)
						{
							child.PagoId = child.PagoIdSource.PagoId;
						}
						else
						{
							child.PagoId = entity.PagoId;
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
	
	#region PagoChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Pago</c>
	///</summary>
	public enum PagoChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Cliente</c> at ClienteIdSource
		///</summary>
		[ChildEntityType(typeof(Cliente))]
		Cliente,
		
		///<summary>
		/// Composite Property for <c>CuentaCorriente</c> at CuentaCorrienteIdSource
		///</summary>
		[ChildEntityType(typeof(CuentaCorriente))]
		CuentaCorriente,
		
		///<summary>
		/// Composite Property for <c>Vendedor</c> at VendedorIdSource
		///</summary>
		[ChildEntityType(typeof(Vendedor))]
		Vendedor,
		///<summary>
		/// Collection of <c>Pago</c> as OneToMany for MovimientoCuentaCollection
		///</summary>
		[ChildEntityType(typeof(TList<MovimientoCuenta>))]
		MovimientoCuentaCollection,
	}
	
	#endregion PagoChildEntityTypes
	
	#region PagoFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PagoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pago"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PagoFilterBuilder : SqlFilterBuilder<PagoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PagoFilterBuilder class.
		/// </summary>
		public PagoFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PagoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PagoFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PagoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PagoFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PagoFilterBuilder
	
	#region PagoParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PagoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pago"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PagoParameterBuilder : ParameterizedSqlFilterBuilder<PagoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PagoParameterBuilder class.
		/// </summary>
		public PagoParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PagoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PagoParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PagoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PagoParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PagoParameterBuilder
	
	#region PagoSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PagoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pago"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PagoSortBuilder : SqlSortBuilder<PagoColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PagoSqlSortBuilder class.
		/// </summary>
		public PagoSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PagoSortBuilder
	
} // end namespace
