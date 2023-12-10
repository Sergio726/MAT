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
	/// This class is the base class for any <see cref="ReservaHabitacionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ReservaHabitacionProviderBaseCore : EntityProviderBase<MAT.Entities.ReservaHabitacion, MAT.Entities.ReservaHabitacionKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.ReservaHabitacionKey key)
		{
			return Delete(transactionManager, key.ReservaHabitacionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_reservaHabitacionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _reservaHabitacionId)
		{
			return Delete(null, _reservaHabitacionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_reservaHabitacionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _reservaHabitacionId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Pasaje key.
		///		FK_ReservaHabitacion_Pasaje Description: 
		/// </summary>
		/// <param name="_pasajeId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		public TList<ReservaHabitacion> GetByPasajeId(System.Guid? _pasajeId)
		{
			int count = -1;
			return GetByPasajeId(_pasajeId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Pasaje key.
		///		FK_ReservaHabitacion_Pasaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		/// <remarks></remarks>
		public TList<ReservaHabitacion> GetByPasajeId(TransactionManager transactionManager, System.Guid? _pasajeId)
		{
			int count = -1;
			return GetByPasajeId(transactionManager, _pasajeId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Pasaje key.
		///		FK_ReservaHabitacion_Pasaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		public TList<ReservaHabitacion> GetByPasajeId(TransactionManager transactionManager, System.Guid? _pasajeId, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeId(transactionManager, _pasajeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Pasaje key.
		///		fkReservaHabitacionPasaje Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pasajeId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		public TList<ReservaHabitacion> GetByPasajeId(System.Guid? _pasajeId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPasajeId(null, _pasajeId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Pasaje key.
		///		fkReservaHabitacionPasaje Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pasajeId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		public TList<ReservaHabitacion> GetByPasajeId(System.Guid? _pasajeId, int start, int pageLength,out int count)
		{
			return GetByPasajeId(null, _pasajeId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Pasaje key.
		///		FK_ReservaHabitacion_Pasaje Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		public abstract TList<ReservaHabitacion> GetByPasajeId(TransactionManager transactionManager, System.Guid? _pasajeId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Persona key.
		///		FK_ReservaHabitacion_Persona Description: 
		/// </summary>
		/// <param name="_pasajeroId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		public TList<ReservaHabitacion> GetByPasajeroId(System.Guid? _pasajeroId)
		{
			int count = -1;
			return GetByPasajeroId(_pasajeroId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Persona key.
		///		FK_ReservaHabitacion_Persona Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		/// <remarks></remarks>
		public TList<ReservaHabitacion> GetByPasajeroId(TransactionManager transactionManager, System.Guid? _pasajeroId)
		{
			int count = -1;
			return GetByPasajeroId(transactionManager, _pasajeroId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Persona key.
		///		FK_ReservaHabitacion_Persona Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		public TList<ReservaHabitacion> GetByPasajeroId(TransactionManager transactionManager, System.Guid? _pasajeroId, int start, int pageLength)
		{
			int count = -1;
			return GetByPasajeroId(transactionManager, _pasajeroId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Persona key.
		///		fkReservaHabitacionPersona Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pasajeroId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		public TList<ReservaHabitacion> GetByPasajeroId(System.Guid? _pasajeroId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPasajeroId(null, _pasajeroId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Persona key.
		///		fkReservaHabitacionPersona Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_pasajeroId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		public TList<ReservaHabitacion> GetByPasajeroId(System.Guid? _pasajeroId, int start, int pageLength,out int count)
		{
			return GetByPasajeroId(null, _pasajeroId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_ReservaHabitacion_Persona key.
		///		FK_ReservaHabitacion_Persona Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_pasajeroId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.ReservaHabitacion objects.</returns>
		public abstract TList<ReservaHabitacion> GetByPasajeroId(TransactionManager transactionManager, System.Guid? _pasajeroId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.ReservaHabitacion Get(TransactionManager transactionManager, MAT.Entities.ReservaHabitacionKey key, int start, int pageLength)
		{
			return GetByReservaHabitacionId(transactionManager, key.ReservaHabitacionId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_ReservaHabitacion__HabitacionID index.
		/// </summary>
		/// <param name="_habitacionId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;ReservaHabitacion&gt;"/> class.</returns>
		public TList<ReservaHabitacion> GetByHabitacionId(System.Guid? _habitacionId)
		{
			int count = -1;
			return GetByHabitacionId(null,_habitacionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_ReservaHabitacion__HabitacionID index.
		/// </summary>
		/// <param name="_habitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;ReservaHabitacion&gt;"/> class.</returns>
		public TList<ReservaHabitacion> GetByHabitacionId(System.Guid? _habitacionId, int start, int pageLength)
		{
			int count = -1;
			return GetByHabitacionId(null, _habitacionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_ReservaHabitacion__HabitacionID index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_habitacionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;ReservaHabitacion&gt;"/> class.</returns>
		public TList<ReservaHabitacion> GetByHabitacionId(TransactionManager transactionManager, System.Guid? _habitacionId)
		{
			int count = -1;
			return GetByHabitacionId(transactionManager, _habitacionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_ReservaHabitacion__HabitacionID index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_habitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;ReservaHabitacion&gt;"/> class.</returns>
		public TList<ReservaHabitacion> GetByHabitacionId(TransactionManager transactionManager, System.Guid? _habitacionId, int start, int pageLength)
		{
			int count = -1;
			return GetByHabitacionId(transactionManager, _habitacionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_ReservaHabitacion__HabitacionID index.
		/// </summary>
		/// <param name="_habitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;ReservaHabitacion&gt;"/> class.</returns>
		public TList<ReservaHabitacion> GetByHabitacionId(System.Guid? _habitacionId, int start, int pageLength, out int count)
		{
			return GetByHabitacionId(null, _habitacionId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_ReservaHabitacion__HabitacionID index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_habitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;ReservaHabitacion&gt;"/> class.</returns>
		public abstract TList<ReservaHabitacion> GetByHabitacionId(TransactionManager transactionManager, System.Guid? _habitacionId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_ReservaHabitacion index.
		/// </summary>
		/// <param name="_reservaHabitacionId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ReservaHabitacion"/> class.</returns>
		public MAT.Entities.ReservaHabitacion GetByReservaHabitacionId(System.Guid _reservaHabitacionId)
		{
			int count = -1;
			return GetByReservaHabitacionId(null,_reservaHabitacionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ReservaHabitacion index.
		/// </summary>
		/// <param name="_reservaHabitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ReservaHabitacion"/> class.</returns>
		public MAT.Entities.ReservaHabitacion GetByReservaHabitacionId(System.Guid _reservaHabitacionId, int start, int pageLength)
		{
			int count = -1;
			return GetByReservaHabitacionId(null, _reservaHabitacionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ReservaHabitacion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_reservaHabitacionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ReservaHabitacion"/> class.</returns>
		public MAT.Entities.ReservaHabitacion GetByReservaHabitacionId(TransactionManager transactionManager, System.Guid _reservaHabitacionId)
		{
			int count = -1;
			return GetByReservaHabitacionId(transactionManager, _reservaHabitacionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ReservaHabitacion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_reservaHabitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ReservaHabitacion"/> class.</returns>
		public MAT.Entities.ReservaHabitacion GetByReservaHabitacionId(TransactionManager transactionManager, System.Guid _reservaHabitacionId, int start, int pageLength)
		{
			int count = -1;
			return GetByReservaHabitacionId(transactionManager, _reservaHabitacionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ReservaHabitacion index.
		/// </summary>
		/// <param name="_reservaHabitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ReservaHabitacion"/> class.</returns>
		public MAT.Entities.ReservaHabitacion GetByReservaHabitacionId(System.Guid _reservaHabitacionId, int start, int pageLength, out int count)
		{
			return GetByReservaHabitacionId(null, _reservaHabitacionId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_ReservaHabitacion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_reservaHabitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.ReservaHabitacion"/> class.</returns>
		public abstract MAT.Entities.ReservaHabitacion GetByReservaHabitacionId(TransactionManager transactionManager, System.Guid _reservaHabitacionId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;ReservaHabitacion&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;ReservaHabitacion&gt;"/></returns>
		public static TList<ReservaHabitacion> Fill(IDataReader reader, TList<ReservaHabitacion> rows, int start, int pageLength)
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
				
				MAT.Entities.ReservaHabitacion c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("ReservaHabitacion")
					.Append("|").Append((System.Guid)reader[((int)ReservaHabitacionColumn.ReservaHabitacionId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<ReservaHabitacion>(
					key.ToString(), // EntityTrackingKey
					"ReservaHabitacion",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.ReservaHabitacion();
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
					c.ReservaHabitacionId = (System.Guid)reader[((int)ReservaHabitacionColumn.ReservaHabitacionId - 1)];
					c.OriginalReservaHabitacionId = c.ReservaHabitacionId;
					c.HabitacionId = (reader.IsDBNull(((int)ReservaHabitacionColumn.HabitacionId - 1)))?null:(System.Guid?)reader[((int)ReservaHabitacionColumn.HabitacionId - 1)];
					c.PasajeId = (reader.IsDBNull(((int)ReservaHabitacionColumn.PasajeId - 1)))?null:(System.Guid?)reader[((int)ReservaHabitacionColumn.PasajeId - 1)];
					c.FechaReserva = (reader.IsDBNull(((int)ReservaHabitacionColumn.FechaReserva - 1)))?null:(System.DateTime?)reader[((int)ReservaHabitacionColumn.FechaReserva - 1)];
					c.Desde = (reader.IsDBNull(((int)ReservaHabitacionColumn.Desde - 1)))?null:(System.DateTime?)reader[((int)ReservaHabitacionColumn.Desde - 1)];
					c.Hasta = (reader.IsDBNull(((int)ReservaHabitacionColumn.Hasta - 1)))?null:(System.DateTime?)reader[((int)ReservaHabitacionColumn.Hasta - 1)];
					c.Expiro = (reader.IsDBNull(((int)ReservaHabitacionColumn.Expiro - 1)))?null:(System.Boolean?)reader[((int)ReservaHabitacionColumn.Expiro - 1)];
					c.HoraIngreso = (reader.IsDBNull(((int)ReservaHabitacionColumn.HoraIngreso - 1)))?null:(System.String)reader[((int)ReservaHabitacionColumn.HoraIngreso - 1)];
					c.HoraSalida = (reader.IsDBNull(((int)ReservaHabitacionColumn.HoraSalida - 1)))?null:(System.String)reader[((int)ReservaHabitacionColumn.HoraSalida - 1)];
					c.PasajeroId = (reader.IsDBNull(((int)ReservaHabitacionColumn.PasajeroId - 1)))?null:(System.Guid?)reader[((int)ReservaHabitacionColumn.PasajeroId - 1)];
					c.ViajeId = (reader.IsDBNull(((int)ReservaHabitacionColumn.ViajeId - 1)))?null:(System.Guid?)reader[((int)ReservaHabitacionColumn.ViajeId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.ReservaHabitacion"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.ReservaHabitacion"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.ReservaHabitacion entity)
		{
			if (!reader.Read()) return;
			
			entity.ReservaHabitacionId = (System.Guid)reader[((int)ReservaHabitacionColumn.ReservaHabitacionId - 1)];
			entity.OriginalReservaHabitacionId = (System.Guid)reader["ReservaHabitacionID"];
			entity.HabitacionId = (reader.IsDBNull(((int)ReservaHabitacionColumn.HabitacionId - 1)))?null:(System.Guid?)reader[((int)ReservaHabitacionColumn.HabitacionId - 1)];
			entity.PasajeId = (reader.IsDBNull(((int)ReservaHabitacionColumn.PasajeId - 1)))?null:(System.Guid?)reader[((int)ReservaHabitacionColumn.PasajeId - 1)];
			entity.FechaReserva = (reader.IsDBNull(((int)ReservaHabitacionColumn.FechaReserva - 1)))?null:(System.DateTime?)reader[((int)ReservaHabitacionColumn.FechaReserva - 1)];
			entity.Desde = (reader.IsDBNull(((int)ReservaHabitacionColumn.Desde - 1)))?null:(System.DateTime?)reader[((int)ReservaHabitacionColumn.Desde - 1)];
			entity.Hasta = (reader.IsDBNull(((int)ReservaHabitacionColumn.Hasta - 1)))?null:(System.DateTime?)reader[((int)ReservaHabitacionColumn.Hasta - 1)];
			entity.Expiro = (reader.IsDBNull(((int)ReservaHabitacionColumn.Expiro - 1)))?null:(System.Boolean?)reader[((int)ReservaHabitacionColumn.Expiro - 1)];
			entity.HoraIngreso = (reader.IsDBNull(((int)ReservaHabitacionColumn.HoraIngreso - 1)))?null:(System.String)reader[((int)ReservaHabitacionColumn.HoraIngreso - 1)];
			entity.HoraSalida = (reader.IsDBNull(((int)ReservaHabitacionColumn.HoraSalida - 1)))?null:(System.String)reader[((int)ReservaHabitacionColumn.HoraSalida - 1)];
			entity.PasajeroId = (reader.IsDBNull(((int)ReservaHabitacionColumn.PasajeroId - 1)))?null:(System.Guid?)reader[((int)ReservaHabitacionColumn.PasajeroId - 1)];
			entity.ViajeId = (reader.IsDBNull(((int)ReservaHabitacionColumn.ViajeId - 1)))?null:(System.Guid?)reader[((int)ReservaHabitacionColumn.ViajeId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.ReservaHabitacion"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.ReservaHabitacion"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.ReservaHabitacion entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ReservaHabitacionId = (System.Guid)dataRow["ReservaHabitacionID"];
			entity.OriginalReservaHabitacionId = (System.Guid)dataRow["ReservaHabitacionID"];
			entity.HabitacionId = Convert.IsDBNull(dataRow["HabitacionID"]) ? null : (System.Guid?)dataRow["HabitacionID"];
			entity.PasajeId = Convert.IsDBNull(dataRow["PasajeID"]) ? null : (System.Guid?)dataRow["PasajeID"];
			entity.FechaReserva = Convert.IsDBNull(dataRow["FechaReserva"]) ? null : (System.DateTime?)dataRow["FechaReserva"];
			entity.Desde = Convert.IsDBNull(dataRow["Desde"]) ? null : (System.DateTime?)dataRow["Desde"];
			entity.Hasta = Convert.IsDBNull(dataRow["Hasta"]) ? null : (System.DateTime?)dataRow["Hasta"];
			entity.Expiro = Convert.IsDBNull(dataRow["Expiro"]) ? null : (System.Boolean?)dataRow["Expiro"];
			entity.HoraIngreso = Convert.IsDBNull(dataRow["HoraIngreso"]) ? null : (System.String)dataRow["HoraIngreso"];
			entity.HoraSalida = Convert.IsDBNull(dataRow["HoraSalida"]) ? null : (System.String)dataRow["HoraSalida"];
			entity.PasajeroId = Convert.IsDBNull(dataRow["PasajeroID"]) ? null : (System.Guid?)dataRow["PasajeroID"];
			entity.ViajeId = Convert.IsDBNull(dataRow["ViajeID"]) ? null : (System.Guid?)dataRow["ViajeID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.ReservaHabitacion"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.ReservaHabitacion Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.ReservaHabitacion entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region HabitacionIdSource	
			if (CanDeepLoad(entity, "Habitacion|HabitacionIdSource", deepLoadType, innerList) 
				&& entity.HabitacionIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.HabitacionId ?? Guid.Empty);
				Habitacion tmpEntity = EntityManager.LocateEntity<Habitacion>(EntityLocator.ConstructKeyFromPkItems(typeof(Habitacion), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.HabitacionIdSource = tmpEntity;
				else
					entity.HabitacionIdSource = DataRepository.HabitacionProvider.GetByHabitacionId(transactionManager, (entity.HabitacionId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'HabitacionIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.HabitacionIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.HabitacionProvider.DeepLoad(transactionManager, entity.HabitacionIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion HabitacionIdSource

			#region PasajeIdSource	
			if (CanDeepLoad(entity, "Pasaje|PasajeIdSource", deepLoadType, innerList) 
				&& entity.PasajeIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.PasajeId ?? Guid.Empty);
				Pasaje tmpEntity = EntityManager.LocateEntity<Pasaje>(EntityLocator.ConstructKeyFromPkItems(typeof(Pasaje), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PasajeIdSource = tmpEntity;
				else
					entity.PasajeIdSource = DataRepository.PasajeProvider.GetByPasajeId(transactionManager, (entity.PasajeId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PasajeIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PasajeProvider.DeepLoad(transactionManager, entity.PasajeIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PasajeIdSource

			#region PasajeroIdSource	
			if (CanDeepLoad(entity, "Persona|PasajeroIdSource", deepLoadType, innerList) 
				&& entity.PasajeroIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.PasajeroId ?? Guid.Empty);
				Persona tmpEntity = EntityManager.LocateEntity<Persona>(EntityLocator.ConstructKeyFromPkItems(typeof(Persona), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PasajeroIdSource = tmpEntity;
				else
					entity.PasajeroIdSource = DataRepository.PersonaProvider.GetByPersonaId(transactionManager, (entity.PasajeroId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeroIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PasajeroIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PersonaProvider.DeepLoad(transactionManager, entity.PasajeroIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PasajeroIdSource
			
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
		/// Deep Save the entire object graph of the MAT.Entities.ReservaHabitacion object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.ReservaHabitacion instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.ReservaHabitacion Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.ReservaHabitacion entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region HabitacionIdSource
			if (CanDeepSave(entity, "Habitacion|HabitacionIdSource", deepSaveType, innerList) 
				&& entity.HabitacionIdSource != null)
			{
				DataRepository.HabitacionProvider.Save(transactionManager, entity.HabitacionIdSource);
				entity.HabitacionId = entity.HabitacionIdSource.HabitacionId;
			}
			#endregion 
			
			#region PasajeIdSource
			if (CanDeepSave(entity, "Pasaje|PasajeIdSource", deepSaveType, innerList) 
				&& entity.PasajeIdSource != null)
			{
				DataRepository.PasajeProvider.Save(transactionManager, entity.PasajeIdSource);
				entity.PasajeId = entity.PasajeIdSource.PasajeId;
			}
			#endregion 
			
			#region PasajeroIdSource
			if (CanDeepSave(entity, "Persona|PasajeroIdSource", deepSaveType, innerList) 
				&& entity.PasajeroIdSource != null)
			{
				DataRepository.PersonaProvider.Save(transactionManager, entity.PasajeroIdSource);
				entity.PasajeroId = entity.PasajeroIdSource.PersonaId;
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
	
	#region ReservaHabitacionChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.ReservaHabitacion</c>
	///</summary>
	public enum ReservaHabitacionChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Habitacion</c> at HabitacionIdSource
		///</summary>
		[ChildEntityType(typeof(Habitacion))]
		Habitacion,
		
		///<summary>
		/// Composite Property for <c>Pasaje</c> at PasajeIdSource
		///</summary>
		[ChildEntityType(typeof(Pasaje))]
		Pasaje,
		
		///<summary>
		/// Composite Property for <c>Persona</c> at PasajeroIdSource
		///</summary>
		[ChildEntityType(typeof(Persona))]
		Persona,
	}
	
	#endregion ReservaHabitacionChildEntityTypes
	
	#region ReservaHabitacionFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ReservaHabitacionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaHabitacionFilterBuilder : SqlFilterBuilder<ReservaHabitacionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionFilterBuilder class.
		/// </summary>
		public ReservaHabitacionFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ReservaHabitacionFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ReservaHabitacionFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ReservaHabitacionFilterBuilder
	
	#region ReservaHabitacionParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ReservaHabitacionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaHabitacionParameterBuilder : ParameterizedSqlFilterBuilder<ReservaHabitacionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionParameterBuilder class.
		/// </summary>
		public ReservaHabitacionParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ReservaHabitacionParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ReservaHabitacionParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ReservaHabitacionParameterBuilder
	
	#region ReservaHabitacionSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ReservaHabitacionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ReservaHabitacion"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ReservaHabitacionSortBuilder : SqlSortBuilder<ReservaHabitacionColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionSqlSortBuilder class.
		/// </summary>
		public ReservaHabitacionSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ReservaHabitacionSortBuilder
	
} // end namespace
