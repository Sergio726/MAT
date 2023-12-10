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
	/// This class is the base class for any <see cref="PasajeProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PasajeProviderBaseCore : EntityProviderBase<MAT.Entities.Pasaje, MAT.Entities.PasajeKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PasajeKey key)
		{
			return Delete(transactionManager, key.PasajeId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_pasajeId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _pasajeId)
		{
			return Delete(null, _pasajeId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _pasajeId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Butaca key.
		///		FK_Pasaje_Butaca Description: 
		/// </summary>
		/// <param name="_butacaId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByButacaId(System.Guid? _butacaId)
		{
			int count = -1;
			return GetByButacaId(_butacaId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Butaca key.
		///		FK_Pasaje_Butaca Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_butacaId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		/// <remarks></remarks>
		public TList<Pasaje> GetByButacaId(TransactionManager transactionManager, System.Guid? _butacaId)
		{
			int count = -1;
			return GetByButacaId(transactionManager, _butacaId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Butaca key.
		///		FK_Pasaje_Butaca Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_butacaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByButacaId(TransactionManager transactionManager, System.Guid? _butacaId, int start, int pageLength)
		{
			int count = -1;
			return GetByButacaId(transactionManager, _butacaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Butaca key.
		///		fkPasajeButaca Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_butacaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByButacaId(System.Guid? _butacaId, int start, int pageLength)
		{
			int count =  -1;
			return GetByButacaId(null, _butacaId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Butaca key.
		///		fkPasajeButaca Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_butacaId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByButacaId(System.Guid? _butacaId, int start, int pageLength,out int count)
		{
			return GetByButacaId(null, _butacaId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Butaca key.
		///		FK_Pasaje_Butaca Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_butacaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public abstract TList<Pasaje> GetByButacaId(TransactionManager transactionManager, System.Guid? _butacaId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Pasajero key.
		///		FK_Pasaje_Pasajero Description: 
		/// </summary>
		/// <param name="_pasajeroId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByPasajeroId(System.Guid? _pasajeroId)
		{
			int count = -1;
			return GetByPasajeroId(_pasajeroId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Pasajero key.
		///		FK_Pasaje_Pasajero Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		/// <remarks></remarks>
		public TList<Pasaje> GetByPasajeroId(TransactionManager transactionManager, System.Guid? _pasajeroId)
		{
			int count = -1;
			return GetByPasajeroId(transactionManager, _pasajeroId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Pasajero key.
		///		FK_Pasaje_Pasajero Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByPasajeroId(TransactionManager transactionManager, System.Guid? _pasajeroId, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeroId(transactionManager, _pasajeroId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Pasajero key.
		///		fkPasajePasajero Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pasajeroId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByPasajeroId(System.Guid? _pasajeroId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPasajeroId(null, _pasajeroId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Pasajero key.
		///		fkPasajePasajero Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pasajeroId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByPasajeroId(System.Guid? _pasajeroId, int start, int pageLength,out int count)
		{
			return GetByPasajeroId(null, _pasajeroId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Pasajero key.
		///		FK_Pasaje_Pasajero Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public abstract TList<Pasaje> GetByPasajeroId(TransactionManager transactionManager, System.Guid? _pasajeroId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Precio key.
		///		FK_Pasaje_Precio Description: 
		/// </summary>
		/// <param name="_precioId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByPrecioId(System.Guid? _precioId)
		{
			int count = -1;
			return GetByPrecioId(_precioId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Precio key.
		///		FK_Pasaje_Precio Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		/// <remarks></remarks>
		public TList<Pasaje> GetByPrecioId(TransactionManager transactionManager, System.Guid? _precioId)
		{
			int count = -1;
			return GetByPrecioId(transactionManager, _precioId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Precio key.
		///		FK_Pasaje_Precio Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByPrecioId(TransactionManager transactionManager, System.Guid? _precioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPrecioId(transactionManager, _precioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Precio key.
		///		fkPasajePrecio Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_precioId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByPrecioId(System.Guid? _precioId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPrecioId(null, _precioId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Precio key.
		///		fkPasajePrecio Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_precioId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByPrecioId(System.Guid? _precioId, int start, int pageLength,out int count)
		{
			return GetByPrecioId(null, _precioId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Precio key.
		///		FK_Pasaje_Precio Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public abstract TList<Pasaje> GetByPrecioId(TransactionManager transactionManager, System.Guid? _precioId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Viaje key.
		///		FK_Pasaje_Viaje Description: 
		/// </summary>
		/// <param name="_viajeId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByViajeId(System.Guid? _viajeId)
		{
			int count = -1;
			return GetByViajeId(_viajeId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Viaje key.
		///		FK_Pasaje_Viaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		/// <remarks></remarks>
		public TList<Pasaje> GetByViajeId(TransactionManager transactionManager, System.Guid? _viajeId)
		{
			int count = -1;
			return GetByViajeId(transactionManager, _viajeId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Viaje key.
		///		FK_Pasaje_Viaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByViajeId(TransactionManager transactionManager, System.Guid? _viajeId, int start, int pageLength)
		{
			int count = -1;
			return GetByViajeId(transactionManager, _viajeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Viaje key.
		///		fkPasajeViaje Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_viajeId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByViajeId(System.Guid? _viajeId, int start, int pageLength)
		{
			int count =  -1;
			return GetByViajeId(null, _viajeId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Viaje key.
		///		fkPasajeViaje Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_viajeId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByViajeId(System.Guid? _viajeId, int start, int pageLength,out int count)
		{
			return GetByViajeId(null, _viajeId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Viaje key.
		///		FK_Pasaje_Viaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_viajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public abstract TList<Pasaje> GetByViajeId(TransactionManager transactionManager, System.Guid? _viajeId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Voucher key.
		///		FK_Pasaje_Voucher Description: 
		/// </summary>
		/// <param name="_voucherId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByVoucherId(System.Guid? _voucherId)
		{
			int count = -1;
			return GetByVoucherId(_voucherId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Voucher key.
		///		FK_Pasaje_Voucher Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_voucherId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		/// <remarks></remarks>
		public TList<Pasaje> GetByVoucherId(TransactionManager transactionManager, System.Guid? _voucherId)
		{
			int count = -1;
			return GetByVoucherId(transactionManager, _voucherId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Voucher key.
		///		FK_Pasaje_Voucher Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_voucherId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByVoucherId(TransactionManager transactionManager, System.Guid? _voucherId, int start, int pageLength)
		{
			int count = -1;
			return GetByVoucherId(transactionManager, _voucherId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Voucher key.
		///		fkPasajeVoucher Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_voucherId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByVoucherId(System.Guid? _voucherId, int start, int pageLength)
		{
			int count =  -1;
			return GetByVoucherId(null, _voucherId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Voucher key.
		///		fkPasajeVoucher Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_voucherId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByVoucherId(System.Guid? _voucherId, int start, int pageLength,out int count)
		{
			return GetByVoucherId(null, _voucherId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Pasaje_Voucher key.
		///		FK_Pasaje_Voucher Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_voucherId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public abstract TList<Pasaje> GetByVoucherId(TransactionManager transactionManager, System.Guid? _voucherId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeEstadoPasaje key.
		///		FK_PasajeEstadoPasaje Description: 
		/// </summary>
		/// <param name="_estadoPasaje"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByEstadoPasaje(System.Int32 _estadoPasaje)
		{
			int count = -1;
			return GetByEstadoPasaje(_estadoPasaje, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeEstadoPasaje key.
		///		FK_PasajeEstadoPasaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_estadoPasaje"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		/// <remarks></remarks>
		public TList<Pasaje> GetByEstadoPasaje(TransactionManager transactionManager, System.Int32 _estadoPasaje)
		{
			int count = -1;
			return GetByEstadoPasaje(transactionManager, _estadoPasaje, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeEstadoPasaje key.
		///		FK_PasajeEstadoPasaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_estadoPasaje"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByEstadoPasaje(TransactionManager transactionManager, System.Int32 _estadoPasaje, int start, int pageLength)
		{
			int count = -1;
			return GetByEstadoPasaje(transactionManager, _estadoPasaje, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeEstadoPasaje key.
		///		fkPasajeEstadoPasaje Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_estadoPasaje"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByEstadoPasaje(System.Int32 _estadoPasaje, int start, int pageLength)
		{
			int count =  -1;
			return GetByEstadoPasaje(null, _estadoPasaje, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeEstadoPasaje key.
		///		fkPasajeEstadoPasaje Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_estadoPasaje"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public TList<Pasaje> GetByEstadoPasaje(System.Int32 _estadoPasaje, int start, int pageLength,out int count)
		{
			return GetByEstadoPasaje(null, _estadoPasaje, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_PasajeEstadoPasaje key.
		///		FK_PasajeEstadoPasaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_estadoPasaje"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Pasaje objects.</returns>
		public abstract TList<Pasaje> GetByEstadoPasaje(TransactionManager transactionManager, System.Int32 _estadoPasaje, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Pasaje Get(TransactionManager transactionManager, MAT.Entities.PasajeKey key, int start, int pageLength)
		{
			return GetByPasajeId(transactionManager, key.PasajeId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Pasaje__FacturaID index.
		/// </summary>
		/// <param name="_facturaId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Pasaje&gt;"/> class.</returns>
		public TList<Pasaje> GetByFacturaId(System.Guid? _facturaId)
		{
			int count = -1;
			return GetByFacturaId(null,_facturaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Pasaje__FacturaID index.
		/// </summary>
		/// <param name="_facturaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Pasaje&gt;"/> class.</returns>
		public TList<Pasaje> GetByFacturaId(System.Guid? _facturaId, int start, int pageLength)
		{
			int count = -1;
			return GetByFacturaId(null, _facturaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Pasaje__FacturaID index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_facturaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Pasaje&gt;"/> class.</returns>
		public TList<Pasaje> GetByFacturaId(TransactionManager transactionManager, System.Guid? _facturaId)
		{
			int count = -1;
			return GetByFacturaId(transactionManager, _facturaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Pasaje__FacturaID index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_facturaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Pasaje&gt;"/> class.</returns>
		public TList<Pasaje> GetByFacturaId(TransactionManager transactionManager, System.Guid? _facturaId, int start, int pageLength)
		{
			int count = -1;
			return GetByFacturaId(transactionManager, _facturaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Pasaje__FacturaID index.
		/// </summary>
		/// <param name="_facturaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Pasaje&gt;"/> class.</returns>
		public TList<Pasaje> GetByFacturaId(System.Guid? _facturaId, int start, int pageLength, out int count)
		{
			return GetByFacturaId(null, _facturaId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Pasaje__FacturaID index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_facturaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Pasaje&gt;"/> class.</returns>
		public abstract TList<Pasaje> GetByFacturaId(TransactionManager transactionManager, System.Guid? _facturaId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Pasaje index.
		/// </summary>
		/// <param name="_pasajeId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasaje"/> class.</returns>
		public MAT.Entities.Pasaje GetByPasajeId(System.Guid _pasajeId)
		{
			int count = -1;
			return GetByPasajeId(null,_pasajeId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pasaje index.
		/// </summary>
		/// <param name="_pasajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasaje"/> class.</returns>
		public MAT.Entities.Pasaje GetByPasajeId(System.Guid _pasajeId, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeId(null, _pasajeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pasaje index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasaje"/> class.</returns>
		public MAT.Entities.Pasaje GetByPasajeId(TransactionManager transactionManager, System.Guid _pasajeId)
		{
			int count = -1;
			return GetByPasajeId(transactionManager, _pasajeId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pasaje index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasaje"/> class.</returns>
		public MAT.Entities.Pasaje GetByPasajeId(TransactionManager transactionManager, System.Guid _pasajeId, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeId(transactionManager, _pasajeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pasaje index.
		/// </summary>
		/// <param name="_pasajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasaje"/> class.</returns>
		public MAT.Entities.Pasaje GetByPasajeId(System.Guid _pasajeId, int start, int pageLength, out int count)
		{
			return GetByPasajeId(null, _pasajeId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Pasaje index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Pasaje"/> class.</returns>
		public abstract MAT.Entities.Pasaje GetByPasajeId(TransactionManager transactionManager, System.Guid _pasajeId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Pasaje&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Pasaje&gt;"/></returns>
		public static TList<Pasaje> Fill(IDataReader reader, TList<Pasaje> rows, int start, int pageLength)
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
				
				MAT.Entities.Pasaje c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Pasaje")
					.Append("|").Append((System.Guid)reader[((int)PasajeColumn.PasajeId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Pasaje>(
					key.ToString(), // EntityTrackingKey
					"Pasaje",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Pasaje();
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
					c.PasajeId = (System.Guid)reader[((int)PasajeColumn.PasajeId - 1)];
					c.OriginalPasajeId = c.PasajeId;
					c.PasajeroId = (reader.IsDBNull(((int)PasajeColumn.PasajeroId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.PasajeroId - 1)];
					c.ButacaId = (reader.IsDBNull(((int)PasajeColumn.ButacaId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.ButacaId - 1)];
					c.FechaReserva = (reader.IsDBNull(((int)PasajeColumn.FechaReserva - 1)))?null:(System.DateTime?)reader[((int)PasajeColumn.FechaReserva - 1)];
					c.FechaCompra = (reader.IsDBNull(((int)PasajeColumn.FechaCompra - 1)))?null:(System.DateTime?)reader[((int)PasajeColumn.FechaCompra - 1)];
					c.ViajeId = (reader.IsDBNull(((int)PasajeColumn.ViajeId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.ViajeId - 1)];
					c.FacturaId = (reader.IsDBNull(((int)PasajeColumn.FacturaId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.FacturaId - 1)];
					c.EstadoPasaje = (System.Int32)reader[((int)PasajeColumn.EstadoPasaje - 1)];
					c.VoucherId = (reader.IsDBNull(((int)PasajeColumn.VoucherId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.VoucherId - 1)];
					c.PrecioId = (reader.IsDBNull(((int)PasajeColumn.PrecioId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.PrecioId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Pasaje"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Pasaje"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Pasaje entity)
		{
			if (!reader.Read()) return;
			
			entity.PasajeId = (System.Guid)reader[((int)PasajeColumn.PasajeId - 1)];
			entity.OriginalPasajeId = (System.Guid)reader["PasajeID"];
			entity.PasajeroId = (reader.IsDBNull(((int)PasajeColumn.PasajeroId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.PasajeroId - 1)];
			entity.ButacaId = (reader.IsDBNull(((int)PasajeColumn.ButacaId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.ButacaId - 1)];
			entity.FechaReserva = (reader.IsDBNull(((int)PasajeColumn.FechaReserva - 1)))?null:(System.DateTime?)reader[((int)PasajeColumn.FechaReserva - 1)];
			entity.FechaCompra = (reader.IsDBNull(((int)PasajeColumn.FechaCompra - 1)))?null:(System.DateTime?)reader[((int)PasajeColumn.FechaCompra - 1)];
			entity.ViajeId = (reader.IsDBNull(((int)PasajeColumn.ViajeId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.ViajeId - 1)];
			entity.FacturaId = (reader.IsDBNull(((int)PasajeColumn.FacturaId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.FacturaId - 1)];
			entity.EstadoPasaje = (System.Int32)reader[((int)PasajeColumn.EstadoPasaje - 1)];
			entity.VoucherId = (reader.IsDBNull(((int)PasajeColumn.VoucherId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.VoucherId - 1)];
			entity.PrecioId = (reader.IsDBNull(((int)PasajeColumn.PrecioId - 1)))?null:(System.Guid?)reader[((int)PasajeColumn.PrecioId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Pasaje"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Pasaje"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Pasaje entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PasajeId = (System.Guid)dataRow["PasajeID"];
			entity.OriginalPasajeId = (System.Guid)dataRow["PasajeID"];
			entity.PasajeroId = Convert.IsDBNull(dataRow["PasajeroID"]) ? null : (System.Guid?)dataRow["PasajeroID"];
			entity.ButacaId = Convert.IsDBNull(dataRow["ButacaID"]) ? null : (System.Guid?)dataRow["ButacaID"];
			entity.FechaReserva = Convert.IsDBNull(dataRow["FechaReserva"]) ? null : (System.DateTime?)dataRow["FechaReserva"];
			entity.FechaCompra = Convert.IsDBNull(dataRow["FechaCompra"]) ? null : (System.DateTime?)dataRow["FechaCompra"];
			entity.ViajeId = Convert.IsDBNull(dataRow["ViajeID"]) ? null : (System.Guid?)dataRow["ViajeID"];
			entity.FacturaId = Convert.IsDBNull(dataRow["FacturaID"]) ? null : (System.Guid?)dataRow["FacturaID"];
			entity.EstadoPasaje = (System.Int32)dataRow["EstadoPasaje"];
			entity.VoucherId = Convert.IsDBNull(dataRow["VoucherID"]) ? null : (System.Guid?)dataRow["VoucherID"];
			entity.PrecioId = Convert.IsDBNull(dataRow["PrecioID"]) ? null : (System.Guid?)dataRow["PrecioID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Pasaje"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Pasaje Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Pasaje entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ButacaIdSource	
			if (CanDeepLoad(entity, "Butaca|ButacaIdSource", deepLoadType, innerList) 
				&& entity.ButacaIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.ButacaId ?? Guid.Empty);
				Butaca tmpEntity = EntityManager.LocateEntity<Butaca>(EntityLocator.ConstructKeyFromPkItems(typeof(Butaca), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ButacaIdSource = tmpEntity;
				else
					entity.ButacaIdSource = DataRepository.ButacaProvider.GetByButacaId(transactionManager, (entity.ButacaId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ButacaIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ButacaIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ButacaProvider.DeepLoad(transactionManager, entity.ButacaIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ButacaIdSource

			#region FacturaIdSource	
			if (CanDeepLoad(entity, "Factura|FacturaIdSource", deepLoadType, innerList) 
				&& entity.FacturaIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.FacturaId ?? Guid.Empty);
				Factura tmpEntity = EntityManager.LocateEntity<Factura>(EntityLocator.ConstructKeyFromPkItems(typeof(Factura), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.FacturaIdSource = tmpEntity;
				else
					entity.FacturaIdSource = DataRepository.FacturaProvider.GetByFacturaId(transactionManager, (entity.FacturaId ?? Guid.Empty));		
				
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

			#region PasajeroIdSource	
			if (CanDeepLoad(entity, "Pasajero|PasajeroIdSource", deepLoadType, innerList) 
				&& entity.PasajeroIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.PasajeroId ?? Guid.Empty);
				Pasajero tmpEntity = EntityManager.LocateEntity<Pasajero>(EntityLocator.ConstructKeyFromPkItems(typeof(Pasajero), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PasajeroIdSource = tmpEntity;
				else
					entity.PasajeroIdSource = DataRepository.PasajeroProvider.GetByPasajeroId(transactionManager, (entity.PasajeroId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeroIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PasajeroIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PasajeroProvider.DeepLoad(transactionManager, entity.PasajeroIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PasajeroIdSource

			#region PrecioIdSource	
			if (CanDeepLoad(entity, "Precio|PrecioIdSource", deepLoadType, innerList) 
				&& entity.PrecioIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.PrecioId ?? Guid.Empty);
				Precio tmpEntity = EntityManager.LocateEntity<Precio>(EntityLocator.ConstructKeyFromPkItems(typeof(Precio), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PrecioIdSource = tmpEntity;
				else
					entity.PrecioIdSource = DataRepository.PrecioProvider.GetByPrecioId(transactionManager, (entity.PrecioId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PrecioIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PrecioIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PrecioProvider.DeepLoad(transactionManager, entity.PrecioIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PrecioIdSource

			#region ViajeIdSource	
			if (CanDeepLoad(entity, "Viaje|ViajeIdSource", deepLoadType, innerList) 
				&& entity.ViajeIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.ViajeId ?? Guid.Empty);
				Viaje tmpEntity = EntityManager.LocateEntity<Viaje>(EntityLocator.ConstructKeyFromPkItems(typeof(Viaje), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ViajeIdSource = tmpEntity;
				else
					entity.ViajeIdSource = DataRepository.ViajeProvider.GetByViajeId(transactionManager, (entity.ViajeId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ViajeIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ViajeIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ViajeProvider.DeepLoad(transactionManager, entity.ViajeIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ViajeIdSource

			#region VoucherIdSource	
			if (CanDeepLoad(entity, "Voucher|VoucherIdSource", deepLoadType, innerList) 
				&& entity.VoucherIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.VoucherId ?? Guid.Empty);
				Voucher tmpEntity = EntityManager.LocateEntity<Voucher>(EntityLocator.ConstructKeyFromPkItems(typeof(Voucher), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.VoucherIdSource = tmpEntity;
				else
					entity.VoucherIdSource = DataRepository.VoucherProvider.GetByVoucherId(transactionManager, (entity.VoucherId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'VoucherIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.VoucherIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.VoucherProvider.DeepLoad(transactionManager, entity.VoucherIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion VoucherIdSource

			#region EstadoPasajeSource	
			if (CanDeepLoad(entity, "EstadoPasaje|EstadoPasajeSource", deepLoadType, innerList) 
				&& entity.EstadoPasajeSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.EstadoPasaje;
				EstadoPasaje tmpEntity = EntityManager.LocateEntity<EstadoPasaje>(EntityLocator.ConstructKeyFromPkItems(typeof(EstadoPasaje), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.EstadoPasajeSource = tmpEntity;
				else
					entity.EstadoPasajeSource = DataRepository.EstadoPasajeProvider.GetById(transactionManager, entity.EstadoPasaje);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'EstadoPasajeSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.EstadoPasajeSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.EstadoPasajeProvider.DeepLoad(transactionManager, entity.EstadoPasajeSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion EstadoPasajeSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByPasajeId methods when available
			
			#region PasajeAdicionalCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PasajeAdicional>|PasajeAdicionalCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeAdicionalCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PasajeAdicionalCollection = DataRepository.PasajeAdicionalProvider.GetByPasajeId(transactionManager, entity.PasajeId);

				if (deep && entity.PasajeAdicionalCollection.Count > 0)
				{
					deepHandles.Add("PasajeAdicionalCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PasajeAdicional>) DataRepository.PasajeAdicionalProvider.DeepLoad,
						new object[] { transactionManager, entity.PasajeAdicionalCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region ReservaHabitacionCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<ReservaHabitacion>|ReservaHabitacionCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ReservaHabitacionCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ReservaHabitacionCollection = DataRepository.ReservaHabitacionProvider.GetByPasajeId(transactionManager, entity.PasajeId);

				if (deep && entity.ReservaHabitacionCollection.Count > 0)
				{
					deepHandles.Add("ReservaHabitacionCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<ReservaHabitacion>) DataRepository.ReservaHabitacionProvider.DeepLoad,
						new object[] { transactionManager, entity.ReservaHabitacionCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Pasaje object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Pasaje instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Pasaje Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Pasaje entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ButacaIdSource
			if (CanDeepSave(entity, "Butaca|ButacaIdSource", deepSaveType, innerList) 
				&& entity.ButacaIdSource != null)
			{
				DataRepository.ButacaProvider.Save(transactionManager, entity.ButacaIdSource);
				entity.ButacaId = entity.ButacaIdSource.ButacaId;
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
			
			#region PasajeroIdSource
			if (CanDeepSave(entity, "Pasajero|PasajeroIdSource", deepSaveType, innerList) 
				&& entity.PasajeroIdSource != null)
			{
				DataRepository.PasajeroProvider.Save(transactionManager, entity.PasajeroIdSource);
				entity.PasajeroId = entity.PasajeroIdSource.PasajeroId;
			}
			#endregion 
			
			#region PrecioIdSource
			if (CanDeepSave(entity, "Precio|PrecioIdSource", deepSaveType, innerList) 
				&& entity.PrecioIdSource != null)
			{
				DataRepository.PrecioProvider.Save(transactionManager, entity.PrecioIdSource);
				entity.PrecioId = entity.PrecioIdSource.PrecioId;
			}
			#endregion 
			
			#region ViajeIdSource
			if (CanDeepSave(entity, "Viaje|ViajeIdSource", deepSaveType, innerList) 
				&& entity.ViajeIdSource != null)
			{
				DataRepository.ViajeProvider.Save(transactionManager, entity.ViajeIdSource);
				entity.ViajeId = entity.ViajeIdSource.ViajeId;
			}
			#endregion 
			
			#region VoucherIdSource
			if (CanDeepSave(entity, "Voucher|VoucherIdSource", deepSaveType, innerList) 
				&& entity.VoucherIdSource != null)
			{
				DataRepository.VoucherProvider.Save(transactionManager, entity.VoucherIdSource);
				entity.VoucherId = entity.VoucherIdSource.VoucherId;
			}
			#endregion 
			
			#region EstadoPasajeSource
			if (CanDeepSave(entity, "EstadoPasaje|EstadoPasajeSource", deepSaveType, innerList) 
				&& entity.EstadoPasajeSource != null)
			{
				DataRepository.EstadoPasajeProvider.Save(transactionManager, entity.EstadoPasajeSource);
				entity.EstadoPasaje = entity.EstadoPasajeSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<PasajeAdicional>
				if (CanDeepSave(entity.PasajeAdicionalCollection, "List<PasajeAdicional>|PasajeAdicionalCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PasajeAdicional child in entity.PasajeAdicionalCollection)
					{
						if(child.PasajeIdSource != null)
						{
							child.PasajeId = child.PasajeIdSource.PasajeId;
						}
						else
						{
							child.PasajeId = entity.PasajeId;
						}

					}

					if (entity.PasajeAdicionalCollection.Count > 0 || entity.PasajeAdicionalCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PasajeAdicionalProvider.Save(transactionManager, entity.PasajeAdicionalCollection);
						
						deepHandles.Add("PasajeAdicionalCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PasajeAdicional >) DataRepository.PasajeAdicionalProvider.DeepSave,
							new object[] { transactionManager, entity.PasajeAdicionalCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<ReservaHabitacion>
				if (CanDeepSave(entity.ReservaHabitacionCollection, "List<ReservaHabitacion>|ReservaHabitacionCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(ReservaHabitacion child in entity.ReservaHabitacionCollection)
					{
						if(child.PasajeIdSource != null)
						{
							child.PasajeId = child.PasajeIdSource.PasajeId;
						}
						else
						{
							child.PasajeId = entity.PasajeId;
						}

					}

					if (entity.ReservaHabitacionCollection.Count > 0 || entity.ReservaHabitacionCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ReservaHabitacionProvider.Save(transactionManager, entity.ReservaHabitacionCollection);
						
						deepHandles.Add("ReservaHabitacionCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< ReservaHabitacion >) DataRepository.ReservaHabitacionProvider.DeepSave,
							new object[] { transactionManager, entity.ReservaHabitacionCollection, deepSaveType, childTypes, innerList }
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
	
	#region PasajeChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Pasaje</c>
	///</summary>
	public enum PasajeChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Butaca</c> at ButacaIdSource
		///</summary>
		[ChildEntityType(typeof(Butaca))]
		Butaca,
		
		///<summary>
		/// Composite Property for <c>Factura</c> at FacturaIdSource
		///</summary>
		[ChildEntityType(typeof(Factura))]
		Factura,
		
		///<summary>
		/// Composite Property for <c>Pasajero</c> at PasajeroIdSource
		///</summary>
		[ChildEntityType(typeof(Pasajero))]
		Pasajero,
		
		///<summary>
		/// Composite Property for <c>Precio</c> at PrecioIdSource
		///</summary>
		[ChildEntityType(typeof(Precio))]
		Precio,
		
		///<summary>
		/// Composite Property for <c>Viaje</c> at ViajeIdSource
		///</summary>
		[ChildEntityType(typeof(Viaje))]
		Viaje,
		
		///<summary>
		/// Composite Property for <c>Voucher</c> at VoucherIdSource
		///</summary>
		[ChildEntityType(typeof(Voucher))]
		Voucher,
		
		///<summary>
		/// Composite Property for <c>EstadoPasaje</c> at EstadoPasajeSource
		///</summary>
		[ChildEntityType(typeof(EstadoPasaje))]
		EstadoPasaje,
		///<summary>
		/// Collection of <c>Pasaje</c> as OneToMany for PasajeAdicionalCollection
		///</summary>
		[ChildEntityType(typeof(TList<PasajeAdicional>))]
		PasajeAdicionalCollection,
		///<summary>
		/// Collection of <c>Pasaje</c> as OneToMany for ReservaHabitacionCollection
		///</summary>
		[ChildEntityType(typeof(TList<ReservaHabitacion>))]
		ReservaHabitacionCollection,
	}
	
	#endregion PasajeChildEntityTypes
	
	#region PasajeFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PasajeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeFilterBuilder : SqlFilterBuilder<PasajeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeFilterBuilder class.
		/// </summary>
		public PasajeFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeFilterBuilder
	
	#region PasajeParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PasajeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeParameterBuilder : ParameterizedSqlFilterBuilder<PasajeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeParameterBuilder class.
		/// </summary>
		public PasajeParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeParameterBuilder
	
	#region PasajeSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PasajeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasaje"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PasajeSortBuilder : SqlSortBuilder<PasajeColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeSqlSortBuilder class.
		/// </summary>
		public PasajeSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PasajeSortBuilder
	
} // end namespace
