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
	/// This class is the base class for any <see cref="MovimientoCuentaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class MovimientoCuentaProviderBaseCore : EntityProviderBase<MAT.Entities.MovimientoCuenta, MAT.Entities.MovimientoCuentaKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.MovimientoCuentaKey key)
		{
			return Delete(transactionManager, key.MovimientoId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_movimientoId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _movimientoId)
		{
			return Delete(null, _movimientoId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_movimientoId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _movimientoId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Cuenta key.
		///		FK_MovimientoCuenta_Cuenta Description: 
		/// </summary>
		/// <param name="_cuentaId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByCuentaId(System.Guid _cuentaId)
		{
			int count = -1;
			return GetByCuentaId(_cuentaId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Cuenta key.
		///		FK_MovimientoCuenta_Cuenta Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		/// <remarks></remarks>
		public TList<MovimientoCuenta> GetByCuentaId(TransactionManager transactionManager, System.Guid _cuentaId)
		{
			int count = -1;
			return GetByCuentaId(transactionManager, _cuentaId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Cuenta key.
		///		FK_MovimientoCuenta_Cuenta Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByCuentaId(TransactionManager transactionManager, System.Guid _cuentaId, int start, int pageLength)
		{
			int count = -1;
			return GetByCuentaId(transactionManager, _cuentaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Cuenta key.
		///		fkMovimientoCuentaCuenta Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_cuentaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByCuentaId(System.Guid _cuentaId, int start, int pageLength)
		{
			int count =  -1;
			return GetByCuentaId(null, _cuentaId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Cuenta key.
		///		fkMovimientoCuentaCuenta Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_cuentaId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByCuentaId(System.Guid _cuentaId, int start, int pageLength,out int count)
		{
			return GetByCuentaId(null, _cuentaId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Cuenta key.
		///		FK_MovimientoCuenta_Cuenta Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public abstract TList<MovimientoCuenta> GetByCuentaId(TransactionManager transactionManager, System.Guid _cuentaId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_CuentaCorriente key.
		///		FK_MovimientoCuenta_CuentaCorriente Description: 
		/// </summary>
		/// <param name="_cuentaCorrienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByCuentaCorrienteId(System.Guid? _cuentaCorrienteId)
		{
			int count = -1;
			return GetByCuentaCorrienteId(_cuentaCorrienteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_CuentaCorriente key.
		///		FK_MovimientoCuenta_CuentaCorriente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		/// <remarks></remarks>
		public TList<MovimientoCuenta> GetByCuentaCorrienteId(TransactionManager transactionManager, System.Guid? _cuentaCorrienteId)
		{
			int count = -1;
			return GetByCuentaCorrienteId(transactionManager, _cuentaCorrienteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_CuentaCorriente key.
		///		FK_MovimientoCuenta_CuentaCorriente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByCuentaCorrienteId(TransactionManager transactionManager, System.Guid? _cuentaCorrienteId, int start, int pageLength)
		{
			int count = -1;
			return GetByCuentaCorrienteId(transactionManager, _cuentaCorrienteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_CuentaCorriente key.
		///		fkMovimientoCuentaCuentaCorriente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByCuentaCorrienteId(System.Guid? _cuentaCorrienteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByCuentaCorrienteId(null, _cuentaCorrienteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_CuentaCorriente key.
		///		fkMovimientoCuentaCuentaCorriente Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByCuentaCorrienteId(System.Guid? _cuentaCorrienteId, int start, int pageLength,out int count)
		{
			return GetByCuentaCorrienteId(null, _cuentaCorrienteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_CuentaCorriente key.
		///		FK_MovimientoCuenta_CuentaCorriente Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_cuentaCorrienteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public abstract TList<MovimientoCuenta> GetByCuentaCorrienteId(TransactionManager transactionManager, System.Guid? _cuentaCorrienteId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Debito key.
		///		FK_MovimientoCuenta_Debito Description: 
		/// </summary>
		/// <param name="_debitoId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByDebitoId(System.Guid? _debitoId)
		{
			int count = -1;
			return GetByDebitoId(_debitoId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Debito key.
		///		FK_MovimientoCuenta_Debito Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_debitoId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		/// <remarks></remarks>
		public TList<MovimientoCuenta> GetByDebitoId(TransactionManager transactionManager, System.Guid? _debitoId)
		{
			int count = -1;
			return GetByDebitoId(transactionManager, _debitoId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Debito key.
		///		FK_MovimientoCuenta_Debito Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_debitoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByDebitoId(TransactionManager transactionManager, System.Guid? _debitoId, int start, int pageLength)
		{
			int count = -1;
			return GetByDebitoId(transactionManager, _debitoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Debito key.
		///		fkMovimientoCuentaDebito Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_debitoId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByDebitoId(System.Guid? _debitoId, int start, int pageLength)
		{
			int count =  -1;
			return GetByDebitoId(null, _debitoId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Debito key.
		///		fkMovimientoCuentaDebito Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_debitoId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByDebitoId(System.Guid? _debitoId, int start, int pageLength,out int count)
		{
			return GetByDebitoId(null, _debitoId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Debito key.
		///		FK_MovimientoCuenta_Debito Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_debitoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public abstract TList<MovimientoCuenta> GetByDebitoId(TransactionManager transactionManager, System.Guid? _debitoId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Factura key.
		///		FK_MovimientoCuenta_Factura Description: 
		/// </summary>
		/// <param name="_facturaId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByFacturaId(System.Guid _facturaId)
		{
			int count = -1;
			return GetByFacturaId(_facturaId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Factura key.
		///		FK_MovimientoCuenta_Factura Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_facturaId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		/// <remarks></remarks>
		public TList<MovimientoCuenta> GetByFacturaId(TransactionManager transactionManager, System.Guid _facturaId)
		{
			int count = -1;
			return GetByFacturaId(transactionManager, _facturaId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Factura key.
		///		FK_MovimientoCuenta_Factura Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_facturaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByFacturaId(TransactionManager transactionManager, System.Guid _facturaId, int start, int pageLength)
		{
			int count = -1;
			return GetByFacturaId(transactionManager, _facturaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Factura key.
		///		fkMovimientoCuentaFactura Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_facturaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByFacturaId(System.Guid _facturaId, int start, int pageLength)
		{
			int count =  -1;
			return GetByFacturaId(null, _facturaId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Factura key.
		///		fkMovimientoCuentaFactura Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_facturaId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByFacturaId(System.Guid _facturaId, int start, int pageLength,out int count)
		{
			return GetByFacturaId(null, _facturaId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Factura key.
		///		FK_MovimientoCuenta_Factura Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_facturaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public abstract TList<MovimientoCuenta> GetByFacturaId(TransactionManager transactionManager, System.Guid _facturaId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Nota key.
		///		FK_MovimientoCuenta_Nota Description: 
		/// </summary>
		/// <param name="_notaId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByNotaId(System.Guid? _notaId)
		{
			int count = -1;
			return GetByNotaId(_notaId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Nota key.
		///		FK_MovimientoCuenta_Nota Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_notaId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		/// <remarks></remarks>
		public TList<MovimientoCuenta> GetByNotaId(TransactionManager transactionManager, System.Guid? _notaId)
		{
			int count = -1;
			return GetByNotaId(transactionManager, _notaId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Nota key.
		///		FK_MovimientoCuenta_Nota Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_notaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByNotaId(TransactionManager transactionManager, System.Guid? _notaId, int start, int pageLength)
		{
			int count = -1;
			return GetByNotaId(transactionManager, _notaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Nota key.
		///		fkMovimientoCuentaNota Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_notaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByNotaId(System.Guid? _notaId, int start, int pageLength)
		{
			int count =  -1;
			return GetByNotaId(null, _notaId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Nota key.
		///		fkMovimientoCuentaNota Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_notaId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByNotaId(System.Guid? _notaId, int start, int pageLength,out int count)
		{
			return GetByNotaId(null, _notaId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Nota key.
		///		FK_MovimientoCuenta_Nota Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_notaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public abstract TList<MovimientoCuenta> GetByNotaId(TransactionManager transactionManager, System.Guid? _notaId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Pago key.
		///		FK_MovimientoCuenta_Pago Description: 
		/// </summary>
		/// <param name="_pagoId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByPagoId(System.Guid? _pagoId)
		{
			int count = -1;
			return GetByPagoId(_pagoId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Pago key.
		///		FK_MovimientoCuenta_Pago Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pagoId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		/// <remarks></remarks>
		public TList<MovimientoCuenta> GetByPagoId(TransactionManager transactionManager, System.Guid? _pagoId)
		{
			int count = -1;
			return GetByPagoId(transactionManager, _pagoId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Pago key.
		///		FK_MovimientoCuenta_Pago Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pagoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByPagoId(TransactionManager transactionManager, System.Guid? _pagoId, int start, int pageLength)
		{
			int count = -1;
			return GetByPagoId(transactionManager, _pagoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Pago key.
		///		fkMovimientoCuentaPago Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pagoId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByPagoId(System.Guid? _pagoId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPagoId(null, _pagoId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Pago key.
		///		fkMovimientoCuentaPago Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pagoId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public TList<MovimientoCuenta> GetByPagoId(System.Guid? _pagoId, int start, int pageLength,out int count)
		{
			return GetByPagoId(null, _pagoId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_MovimientoCuenta_Pago key.
		///		FK_MovimientoCuenta_Pago Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pagoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.MovimientoCuenta objects.</returns>
		public abstract TList<MovimientoCuenta> GetByPagoId(TransactionManager transactionManager, System.Guid? _pagoId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.MovimientoCuenta Get(TransactionManager transactionManager, MAT.Entities.MovimientoCuentaKey key, int start, int pageLength)
		{
			return GetByMovimientoId(transactionManager, key.MovimientoId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_MovimientoCuenta index.
		/// </summary>
		/// <param name="_movimientoId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.MovimientoCuenta"/> class.</returns>
		public MAT.Entities.MovimientoCuenta GetByMovimientoId(System.Guid _movimientoId)
		{
			int count = -1;
			return GetByMovimientoId(null,_movimientoId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_MovimientoCuenta index.
		/// </summary>
		/// <param name="_movimientoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.MovimientoCuenta"/> class.</returns>
		public MAT.Entities.MovimientoCuenta GetByMovimientoId(System.Guid _movimientoId, int start, int pageLength)
		{
			int count = -1;
			return GetByMovimientoId(null, _movimientoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_MovimientoCuenta index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_movimientoId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.MovimientoCuenta"/> class.</returns>
		public MAT.Entities.MovimientoCuenta GetByMovimientoId(TransactionManager transactionManager, System.Guid _movimientoId)
		{
			int count = -1;
			return GetByMovimientoId(transactionManager, _movimientoId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_MovimientoCuenta index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_movimientoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.MovimientoCuenta"/> class.</returns>
		public MAT.Entities.MovimientoCuenta GetByMovimientoId(TransactionManager transactionManager, System.Guid _movimientoId, int start, int pageLength)
		{
			int count = -1;
			return GetByMovimientoId(transactionManager, _movimientoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_MovimientoCuenta index.
		/// </summary>
		/// <param name="_movimientoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.MovimientoCuenta"/> class.</returns>
		public MAT.Entities.MovimientoCuenta GetByMovimientoId(System.Guid _movimientoId, int start, int pageLength, out int count)
		{
			return GetByMovimientoId(null, _movimientoId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_MovimientoCuenta index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_movimientoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.MovimientoCuenta"/> class.</returns>
		public abstract MAT.Entities.MovimientoCuenta GetByMovimientoId(TransactionManager transactionManager, System.Guid _movimientoId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;MovimientoCuenta&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;MovimientoCuenta&gt;"/></returns>
		public static TList<MovimientoCuenta> Fill(IDataReader reader, TList<MovimientoCuenta> rows, int start, int pageLength)
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
				
				MAT.Entities.MovimientoCuenta c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("MovimientoCuenta")
					.Append("|").Append((System.Guid)reader[((int)MovimientoCuentaColumn.MovimientoId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<MovimientoCuenta>(
					key.ToString(), // EntityTrackingKey
					"MovimientoCuenta",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.MovimientoCuenta();
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
					c.MovimientoId = (System.Guid)reader[((int)MovimientoCuentaColumn.MovimientoId - 1)];
					c.OriginalMovimientoId = c.MovimientoId;
					c.PagoId = (reader.IsDBNull(((int)MovimientoCuentaColumn.PagoId - 1)))?null:(System.Guid?)reader[((int)MovimientoCuentaColumn.PagoId - 1)];
					c.FacturaId = (System.Guid)reader[((int)MovimientoCuentaColumn.FacturaId - 1)];
					c.FechaRegistro = (reader.IsDBNull(((int)MovimientoCuentaColumn.FechaRegistro - 1)))?null:(System.DateTime?)reader[((int)MovimientoCuentaColumn.FechaRegistro - 1)];
					c.CuentaId = (System.Guid)reader[((int)MovimientoCuentaColumn.CuentaId - 1)];
					c.NotaId = (reader.IsDBNull(((int)MovimientoCuentaColumn.NotaId - 1)))?null:(System.Guid?)reader[((int)MovimientoCuentaColumn.NotaId - 1)];
					c.CuentaCorrienteId = (reader.IsDBNull(((int)MovimientoCuentaColumn.CuentaCorrienteId - 1)))?null:(System.Guid?)reader[((int)MovimientoCuentaColumn.CuentaCorrienteId - 1)];
					c.DebitoId = (reader.IsDBNull(((int)MovimientoCuentaColumn.DebitoId - 1)))?null:(System.Guid?)reader[((int)MovimientoCuentaColumn.DebitoId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.MovimientoCuenta"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.MovimientoCuenta"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.MovimientoCuenta entity)
		{
			if (!reader.Read()) return;
			
			entity.MovimientoId = (System.Guid)reader[((int)MovimientoCuentaColumn.MovimientoId - 1)];
			entity.OriginalMovimientoId = (System.Guid)reader["MovimientoID"];
			entity.PagoId = (reader.IsDBNull(((int)MovimientoCuentaColumn.PagoId - 1)))?null:(System.Guid?)reader[((int)MovimientoCuentaColumn.PagoId - 1)];
			entity.FacturaId = (System.Guid)reader[((int)MovimientoCuentaColumn.FacturaId - 1)];
			entity.FechaRegistro = (reader.IsDBNull(((int)MovimientoCuentaColumn.FechaRegistro - 1)))?null:(System.DateTime?)reader[((int)MovimientoCuentaColumn.FechaRegistro - 1)];
			entity.CuentaId = (System.Guid)reader[((int)MovimientoCuentaColumn.CuentaId - 1)];
			entity.NotaId = (reader.IsDBNull(((int)MovimientoCuentaColumn.NotaId - 1)))?null:(System.Guid?)reader[((int)MovimientoCuentaColumn.NotaId - 1)];
			entity.CuentaCorrienteId = (reader.IsDBNull(((int)MovimientoCuentaColumn.CuentaCorrienteId - 1)))?null:(System.Guid?)reader[((int)MovimientoCuentaColumn.CuentaCorrienteId - 1)];
			entity.DebitoId = (reader.IsDBNull(((int)MovimientoCuentaColumn.DebitoId - 1)))?null:(System.Guid?)reader[((int)MovimientoCuentaColumn.DebitoId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.MovimientoCuenta"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.MovimientoCuenta"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.MovimientoCuenta entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.MovimientoId = (System.Guid)dataRow["MovimientoID"];
			entity.OriginalMovimientoId = (System.Guid)dataRow["MovimientoID"];
			entity.PagoId = Convert.IsDBNull(dataRow["PagoID"]) ? null : (System.Guid?)dataRow["PagoID"];
			entity.FacturaId = (System.Guid)dataRow["FacturaID"];
			entity.FechaRegistro = Convert.IsDBNull(dataRow["FechaRegistro"]) ? null : (System.DateTime?)dataRow["FechaRegistro"];
			entity.CuentaId = (System.Guid)dataRow["CuentaID"];
			entity.NotaId = Convert.IsDBNull(dataRow["NotaID"]) ? null : (System.Guid?)dataRow["NotaID"];
			entity.CuentaCorrienteId = Convert.IsDBNull(dataRow["CuentaCorrienteID"]) ? null : (System.Guid?)dataRow["CuentaCorrienteID"];
			entity.DebitoId = Convert.IsDBNull(dataRow["DebitoID"]) ? null : (System.Guid?)dataRow["DebitoID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.MovimientoCuenta"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.MovimientoCuenta Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.MovimientoCuenta entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region CuentaIdSource	
			if (CanDeepLoad(entity, "Cuenta|CuentaIdSource", deepLoadType, innerList) 
				&& entity.CuentaIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.CuentaId;
				Cuenta tmpEntity = EntityManager.LocateEntity<Cuenta>(EntityLocator.ConstructKeyFromPkItems(typeof(Cuenta), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.CuentaIdSource = tmpEntity;
				else
					entity.CuentaIdSource = DataRepository.CuentaProvider.GetByCuentaId(transactionManager, entity.CuentaId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'CuentaIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.CuentaIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.CuentaProvider.DeepLoad(transactionManager, entity.CuentaIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion CuentaIdSource

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

			#region DebitoIdSource	
			if (CanDeepLoad(entity, "Debito|DebitoIdSource", deepLoadType, innerList) 
				&& entity.DebitoIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.DebitoId ?? Guid.Empty);
				Debito tmpEntity = EntityManager.LocateEntity<Debito>(EntityLocator.ConstructKeyFromPkItems(typeof(Debito), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.DebitoIdSource = tmpEntity;
				else
					entity.DebitoIdSource = DataRepository.DebitoProvider.GetByDebitoId(transactionManager, (entity.DebitoId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DebitoIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.DebitoIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.DebitoProvider.DeepLoad(transactionManager, entity.DebitoIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion DebitoIdSource

			#region FacturaIdSource	
			if (CanDeepLoad(entity, "Factura|FacturaIdSource", deepLoadType, innerList) 
				&& entity.FacturaIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.FacturaId;
				Factura tmpEntity = EntityManager.LocateEntity<Factura>(EntityLocator.ConstructKeyFromPkItems(typeof(Factura), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.FacturaIdSource = tmpEntity;
				else
					entity.FacturaIdSource = DataRepository.FacturaProvider.GetByFacturaId(transactionManager, entity.FacturaId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'FacturaIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.FacturaIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.FacturaProvider.DeepLoad(transactionManager, entity.FacturaIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion FacturaIdSource

			#region NotaIdSource	
			if (CanDeepLoad(entity, "Nota|NotaIdSource", deepLoadType, innerList) 
				&& entity.NotaIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.NotaId ?? Guid.Empty);
				Nota tmpEntity = EntityManager.LocateEntity<Nota>(EntityLocator.ConstructKeyFromPkItems(typeof(Nota), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.NotaIdSource = tmpEntity;
				else
					entity.NotaIdSource = DataRepository.NotaProvider.GetByNotaId(transactionManager, (entity.NotaId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'NotaIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.NotaIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.NotaProvider.DeepLoad(transactionManager, entity.NotaIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion NotaIdSource

			#region PagoIdSource	
			if (CanDeepLoad(entity, "Pago|PagoIdSource", deepLoadType, innerList) 
				&& entity.PagoIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.PagoId ?? Guid.Empty);
				Pago tmpEntity = EntityManager.LocateEntity<Pago>(EntityLocator.ConstructKeyFromPkItems(typeof(Pago), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PagoIdSource = tmpEntity;
				else
					entity.PagoIdSource = DataRepository.PagoProvider.GetByPagoId(transactionManager, (entity.PagoId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PagoIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PagoIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PagoProvider.DeepLoad(transactionManager, entity.PagoIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PagoIdSource
			
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
		/// Deep Save the entire object graph of the MAT.Entities.MovimientoCuenta object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.MovimientoCuenta instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.MovimientoCuenta Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.MovimientoCuenta entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region CuentaIdSource
			if (CanDeepSave(entity, "Cuenta|CuentaIdSource", deepSaveType, innerList) 
				&& entity.CuentaIdSource != null)
			{
				DataRepository.CuentaProvider.Save(transactionManager, entity.CuentaIdSource);
				entity.CuentaId = entity.CuentaIdSource.CuentaId;
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
			
			#region DebitoIdSource
			if (CanDeepSave(entity, "Debito|DebitoIdSource", deepSaveType, innerList) 
				&& entity.DebitoIdSource != null)
			{
				DataRepository.DebitoProvider.Save(transactionManager, entity.DebitoIdSource);
				entity.DebitoId = entity.DebitoIdSource.DebitoId;
			}
			#endregion 
			
			#region FacturaIdSource
			if (CanDeepSave(entity, "Factura|FacturaIdSource", deepSaveType, innerList) 
				&& entity.FacturaIdSource != null)
			{
				DataRepository.FacturaProvider.Save(transactionManager, entity.FacturaIdSource);
				entity.FacturaId = entity.FacturaIdSource.FacturaId;
			}
			#endregion 
			
			#region NotaIdSource
			if (CanDeepSave(entity, "Nota|NotaIdSource", deepSaveType, innerList) 
				&& entity.NotaIdSource != null)
			{
				DataRepository.NotaProvider.Save(transactionManager, entity.NotaIdSource);
				entity.NotaId = entity.NotaIdSource.NotaId;
			}
			#endregion 
			
			#region PagoIdSource
			if (CanDeepSave(entity, "Pago|PagoIdSource", deepSaveType, innerList) 
				&& entity.PagoIdSource != null)
			{
				DataRepository.PagoProvider.Save(transactionManager, entity.PagoIdSource);
				entity.PagoId = entity.PagoIdSource.PagoId;
			}
			#endregion 
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
	
	#region MovimientoCuentaChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.MovimientoCuenta</c>
	///</summary>
	public enum MovimientoCuentaChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Cuenta</c> at CuentaIdSource
		///</summary>
		[ChildEntityType(typeof(Cuenta))]
		Cuenta,
		
		///<summary>
		/// Composite Property for <c>CuentaCorriente</c> at CuentaCorrienteIdSource
		///</summary>
		[ChildEntityType(typeof(CuentaCorriente))]
		CuentaCorriente,
		
		///<summary>
		/// Composite Property for <c>Debito</c> at DebitoIdSource
		///</summary>
		[ChildEntityType(typeof(Debito))]
		Debito,
		
		///<summary>
		/// Composite Property for <c>Factura</c> at FacturaIdSource
		///</summary>
		[ChildEntityType(typeof(Factura))]
		Factura,
		
		///<summary>
		/// Composite Property for <c>Nota</c> at NotaIdSource
		///</summary>
		[ChildEntityType(typeof(Nota))]
		Nota,
		
		///<summary>
		/// Composite Property for <c>Pago</c> at PagoIdSource
		///</summary>
		[ChildEntityType(typeof(Pago))]
		Pago,
	}
	
	#endregion MovimientoCuentaChildEntityTypes
	
	#region MovimientoCuentaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;MovimientoCuentaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MovimientoCuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MovimientoCuentaFilterBuilder : SqlFilterBuilder<MovimientoCuentaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaFilterBuilder class.
		/// </summary>
		public MovimientoCuentaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MovimientoCuentaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MovimientoCuentaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MovimientoCuentaFilterBuilder
	
	#region MovimientoCuentaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;MovimientoCuentaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MovimientoCuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MovimientoCuentaParameterBuilder : ParameterizedSqlFilterBuilder<MovimientoCuentaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaParameterBuilder class.
		/// </summary>
		public MovimientoCuentaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MovimientoCuentaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MovimientoCuentaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MovimientoCuentaParameterBuilder
	
	#region MovimientoCuentaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;MovimientoCuentaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MovimientoCuenta"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class MovimientoCuentaSortBuilder : SqlSortBuilder<MovimientoCuentaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaSqlSortBuilder class.
		/// </summary>
		public MovimientoCuentaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion MovimientoCuentaSortBuilder
	
} // end namespace
