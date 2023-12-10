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
	/// This class is the base class for any <see cref="HotelProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class HotelProviderBaseCore : EntityProviderBase<MAT.Entities.Hotel, MAT.Entities.HotelKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.HotelKey key)
		{
			return Delete(transactionManager, key.HotelId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_hotelId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _hotelId)
		{
			return Delete(null, _hotelId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _hotelId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Hotel_Localidad key.
		///		FK_Hotel_Localidad Description: 
		/// </summary>
		/// <param name="_localidadId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Hotel objects.</returns>
		public TList<Hotel> GetByLocalidadId(System.Int32? _localidadId)
		{
			int count = -1;
			return GetByLocalidadId(_localidadId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Hotel_Localidad key.
		///		FK_Hotel_Localidad Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_localidadId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Hotel objects.</returns>
		/// <remarks></remarks>
		public TList<Hotel> GetByLocalidadId(TransactionManager transactionManager, System.Int32? _localidadId)
		{
			int count = -1;
			return GetByLocalidadId(transactionManager, _localidadId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Hotel_Localidad key.
		///		FK_Hotel_Localidad Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_localidadId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Hotel objects.</returns>
		public TList<Hotel> GetByLocalidadId(TransactionManager transactionManager, System.Int32? _localidadId, int start, int pageLength)
		{
			int count = -1;
			return GetByLocalidadId(transactionManager, _localidadId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Hotel_Localidad key.
		///		fkHotelLocalidad Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_localidadId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Hotel objects.</returns>
		public TList<Hotel> GetByLocalidadId(System.Int32? _localidadId, int start, int pageLength)
		{
			int count =  -1;
			return GetByLocalidadId(null, _localidadId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Hotel_Localidad key.
		///		fkHotelLocalidad Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_localidadId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Hotel objects.</returns>
		public TList<Hotel> GetByLocalidadId(System.Int32? _localidadId, int start, int pageLength,out int count)
		{
			return GetByLocalidadId(null, _localidadId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Hotel_Localidad key.
		///		FK_Hotel_Localidad Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_localidadId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Hotel objects.</returns>
		public abstract TList<Hotel> GetByLocalidadId(TransactionManager transactionManager, System.Int32? _localidadId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Hotel Get(TransactionManager transactionManager, MAT.Entities.HotelKey key, int start, int pageLength)
		{
			return GetByHotelId(transactionManager, key.HotelId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Hotel index.
		/// </summary>
		/// <param name="_hotelId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Hotel"/> class.</returns>
		public MAT.Entities.Hotel GetByHotelId(System.Guid _hotelId)
		{
			int count = -1;
			return GetByHotelId(null,_hotelId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Hotel index.
		/// </summary>
		/// <param name="_hotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Hotel"/> class.</returns>
		public MAT.Entities.Hotel GetByHotelId(System.Guid _hotelId, int start, int pageLength)
		{
			int count = -1;
			return GetByHotelId(null, _hotelId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Hotel index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Hotel"/> class.</returns>
		public MAT.Entities.Hotel GetByHotelId(TransactionManager transactionManager, System.Guid _hotelId)
		{
			int count = -1;
			return GetByHotelId(transactionManager, _hotelId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Hotel index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Hotel"/> class.</returns>
		public MAT.Entities.Hotel GetByHotelId(TransactionManager transactionManager, System.Guid _hotelId, int start, int pageLength)
		{
			int count = -1;
			return GetByHotelId(transactionManager, _hotelId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Hotel index.
		/// </summary>
		/// <param name="_hotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Hotel"/> class.</returns>
		public MAT.Entities.Hotel GetByHotelId(System.Guid _hotelId, int start, int pageLength, out int count)
		{
			return GetByHotelId(null, _hotelId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Hotel index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Hotel"/> class.</returns>
		public abstract MAT.Entities.Hotel GetByHotelId(TransactionManager transactionManager, System.Guid _hotelId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Hotel&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Hotel&gt;"/></returns>
		public static TList<Hotel> Fill(IDataReader reader, TList<Hotel> rows, int start, int pageLength)
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
				
				MAT.Entities.Hotel c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Hotel")
					.Append("|").Append((System.Guid)reader[((int)HotelColumn.HotelId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Hotel>(
					key.ToString(), // EntityTrackingKey
					"Hotel",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Hotel();
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
					c.HotelId = (System.Guid)reader[((int)HotelColumn.HotelId - 1)];
					c.OriginalHotelId = c.HotelId;
					c.Nombre = (System.String)reader[((int)HotelColumn.Nombre - 1)];
					c.Direccion = (reader.IsDBNull(((int)HotelColumn.Direccion - 1)))?null:(System.String)reader[((int)HotelColumn.Direccion - 1)];
					c.Cp = (reader.IsDBNull(((int)HotelColumn.Cp - 1)))?null:(System.String)reader[((int)HotelColumn.Cp - 1)];
					c.Telefono = (reader.IsDBNull(((int)HotelColumn.Telefono - 1)))?null:(System.String)reader[((int)HotelColumn.Telefono - 1)];
					c.Email = (reader.IsDBNull(((int)HotelColumn.Email - 1)))?null:(System.String)reader[((int)HotelColumn.Email - 1)];
					c.Contacto = (reader.IsDBNull(((int)HotelColumn.Contacto - 1)))?null:(System.String)reader[((int)HotelColumn.Contacto - 1)];
					c.CantidadHabitaciones = (reader.IsDBNull(((int)HotelColumn.CantidadHabitaciones - 1)))?null:(System.Int32?)reader[((int)HotelColumn.CantidadHabitaciones - 1)];
					c.Categoria = (reader.IsDBNull(((int)HotelColumn.Categoria - 1)))?null:(System.Int32?)reader[((int)HotelColumn.Categoria - 1)];
					c.Child1 = (reader.IsDBNull(((int)HotelColumn.Child1 - 1)))?null:(System.String)reader[((int)HotelColumn.Child1 - 1)];
					c.Child2 = (reader.IsDBNull(((int)HotelColumn.Child2 - 1)))?null:(System.String)reader[((int)HotelColumn.Child2 - 1)];
					c.ChildHabitacion = (reader.IsDBNull(((int)HotelColumn.ChildHabitacion - 1)))?null:(System.Int32?)reader[((int)HotelColumn.ChildHabitacion - 1)];
					c.CheckIn = (reader.IsDBNull(((int)HotelColumn.CheckIn - 1)))?null:(System.String)reader[((int)HotelColumn.CheckIn - 1)];
					c.CheckOut = (reader.IsDBNull(((int)HotelColumn.CheckOut - 1)))?null:(System.String)reader[((int)HotelColumn.CheckOut - 1)];
					c.GoogleMapHtml = (reader.IsDBNull(((int)HotelColumn.GoogleMapHtml - 1)))?null:(System.String)reader[((int)HotelColumn.GoogleMapHtml - 1)];
					c.LocalidadId = (reader.IsDBNull(((int)HotelColumn.LocalidadId - 1)))?null:(System.Int32?)reader[((int)HotelColumn.LocalidadId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Hotel"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Hotel"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Hotel entity)
		{
			if (!reader.Read()) return;
			
			entity.HotelId = (System.Guid)reader[((int)HotelColumn.HotelId - 1)];
			entity.OriginalHotelId = (System.Guid)reader["HotelID"];
			entity.Nombre = (System.String)reader[((int)HotelColumn.Nombre - 1)];
			entity.Direccion = (reader.IsDBNull(((int)HotelColumn.Direccion - 1)))?null:(System.String)reader[((int)HotelColumn.Direccion - 1)];
			entity.Cp = (reader.IsDBNull(((int)HotelColumn.Cp - 1)))?null:(System.String)reader[((int)HotelColumn.Cp - 1)];
			entity.Telefono = (reader.IsDBNull(((int)HotelColumn.Telefono - 1)))?null:(System.String)reader[((int)HotelColumn.Telefono - 1)];
			entity.Email = (reader.IsDBNull(((int)HotelColumn.Email - 1)))?null:(System.String)reader[((int)HotelColumn.Email - 1)];
			entity.Contacto = (reader.IsDBNull(((int)HotelColumn.Contacto - 1)))?null:(System.String)reader[((int)HotelColumn.Contacto - 1)];
			entity.CantidadHabitaciones = (reader.IsDBNull(((int)HotelColumn.CantidadHabitaciones - 1)))?null:(System.Int32?)reader[((int)HotelColumn.CantidadHabitaciones - 1)];
			entity.Categoria = (reader.IsDBNull(((int)HotelColumn.Categoria - 1)))?null:(System.Int32?)reader[((int)HotelColumn.Categoria - 1)];
			entity.Child1 = (reader.IsDBNull(((int)HotelColumn.Child1 - 1)))?null:(System.String)reader[((int)HotelColumn.Child1 - 1)];
			entity.Child2 = (reader.IsDBNull(((int)HotelColumn.Child2 - 1)))?null:(System.String)reader[((int)HotelColumn.Child2 - 1)];
			entity.ChildHabitacion = (reader.IsDBNull(((int)HotelColumn.ChildHabitacion - 1)))?null:(System.Int32?)reader[((int)HotelColumn.ChildHabitacion - 1)];
			entity.CheckIn = (reader.IsDBNull(((int)HotelColumn.CheckIn - 1)))?null:(System.String)reader[((int)HotelColumn.CheckIn - 1)];
			entity.CheckOut = (reader.IsDBNull(((int)HotelColumn.CheckOut - 1)))?null:(System.String)reader[((int)HotelColumn.CheckOut - 1)];
			entity.GoogleMapHtml = (reader.IsDBNull(((int)HotelColumn.GoogleMapHtml - 1)))?null:(System.String)reader[((int)HotelColumn.GoogleMapHtml - 1)];
			entity.LocalidadId = (reader.IsDBNull(((int)HotelColumn.LocalidadId - 1)))?null:(System.Int32?)reader[((int)HotelColumn.LocalidadId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Hotel"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Hotel"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Hotel entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.HotelId = (System.Guid)dataRow["HotelID"];
			entity.OriginalHotelId = (System.Guid)dataRow["HotelID"];
			entity.Nombre = (System.String)dataRow["Nombre"];
			entity.Direccion = Convert.IsDBNull(dataRow["Direccion"]) ? null : (System.String)dataRow["Direccion"];
			entity.Cp = Convert.IsDBNull(dataRow["CP"]) ? null : (System.String)dataRow["CP"];
			entity.Telefono = Convert.IsDBNull(dataRow["Telefono"]) ? null : (System.String)dataRow["Telefono"];
			entity.Email = Convert.IsDBNull(dataRow["Email"]) ? null : (System.String)dataRow["Email"];
			entity.Contacto = Convert.IsDBNull(dataRow["Contacto"]) ? null : (System.String)dataRow["Contacto"];
			entity.CantidadHabitaciones = Convert.IsDBNull(dataRow["CantidadHabitaciones"]) ? null : (System.Int32?)dataRow["CantidadHabitaciones"];
			entity.Categoria = Convert.IsDBNull(dataRow["Categoria"]) ? null : (System.Int32?)dataRow["Categoria"];
			entity.Child1 = Convert.IsDBNull(dataRow["Child1"]) ? null : (System.String)dataRow["Child1"];
			entity.Child2 = Convert.IsDBNull(dataRow["Child2"]) ? null : (System.String)dataRow["Child2"];
			entity.ChildHabitacion = Convert.IsDBNull(dataRow["ChildHabitacion"]) ? null : (System.Int32?)dataRow["ChildHabitacion"];
			entity.CheckIn = Convert.IsDBNull(dataRow["CheckIn"]) ? null : (System.String)dataRow["CheckIn"];
			entity.CheckOut = Convert.IsDBNull(dataRow["CheckOut"]) ? null : (System.String)dataRow["CheckOut"];
			entity.GoogleMapHtml = Convert.IsDBNull(dataRow["GoogleMapHtml"]) ? null : (System.String)dataRow["GoogleMapHtml"];
			entity.LocalidadId = Convert.IsDBNull(dataRow["LocalidadID"]) ? null : (System.Int32?)dataRow["LocalidadID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Hotel"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Hotel Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Hotel entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region LocalidadIdSource	
			if (CanDeepLoad(entity, "Localidad|LocalidadIdSource", deepLoadType, innerList) 
				&& entity.LocalidadIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.LocalidadId ?? (int)0);
				Localidad tmpEntity = EntityManager.LocateEntity<Localidad>(EntityLocator.ConstructKeyFromPkItems(typeof(Localidad), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.LocalidadIdSource = tmpEntity;
				else
					entity.LocalidadIdSource = DataRepository.LocalidadProvider.GetById(transactionManager, (entity.LocalidadId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'LocalidadIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.LocalidadIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.LocalidadProvider.DeepLoad(transactionManager, entity.LocalidadIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion LocalidadIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByHotelId methods when available
			
			#region ServicioCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Servicio>|ServicioCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ServicioCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ServicioCollection = DataRepository.ServicioProvider.GetByHotelId(transactionManager, entity.HotelId);

				if (deep && entity.ServicioCollection.Count > 0)
				{
					deepHandles.Add("ServicioCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Servicio>) DataRepository.ServicioProvider.DeepLoad,
						new object[] { transactionManager, entity.ServicioCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region HabitacionCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Habitacion>|HabitacionCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'HabitacionCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.HabitacionCollection = DataRepository.HabitacionProvider.GetByHotelId(transactionManager, entity.HotelId);

				if (deep && entity.HabitacionCollection.Count > 0)
				{
					deepHandles.Add("HabitacionCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Habitacion>) DataRepository.HabitacionProvider.DeepLoad,
						new object[] { transactionManager, entity.HabitacionCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region ViajeHotelCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<ViajeHotel>|ViajeHotelCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ViajeHotelCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ViajeHotelCollection = DataRepository.ViajeHotelProvider.GetByHotelId(transactionManager, entity.HotelId);

				if (deep && entity.ViajeHotelCollection.Count > 0)
				{
					deepHandles.Add("ViajeHotelCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<ViajeHotel>) DataRepository.ViajeHotelProvider.DeepLoad,
						new object[] { transactionManager, entity.ViajeHotelCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Hotel object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Hotel instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Hotel Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Hotel entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region LocalidadIdSource
			if (CanDeepSave(entity, "Localidad|LocalidadIdSource", deepSaveType, innerList) 
				&& entity.LocalidadIdSource != null)
			{
				DataRepository.LocalidadProvider.Save(transactionManager, entity.LocalidadIdSource);
				entity.LocalidadId = entity.LocalidadIdSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<Servicio>
				if (CanDeepSave(entity.ServicioCollection, "List<Servicio>|ServicioCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Servicio child in entity.ServicioCollection)
					{
						if(child.HotelIdSource != null)
						{
							child.HotelId = child.HotelIdSource.HotelId;
						}
						else
						{
							child.HotelId = entity.HotelId;
						}

					}

					if (entity.ServicioCollection.Count > 0 || entity.ServicioCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ServicioProvider.Save(transactionManager, entity.ServicioCollection);
						
						deepHandles.Add("ServicioCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Servicio >) DataRepository.ServicioProvider.DeepSave,
							new object[] { transactionManager, entity.ServicioCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Habitacion>
				if (CanDeepSave(entity.HabitacionCollection, "List<Habitacion>|HabitacionCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Habitacion child in entity.HabitacionCollection)
					{
						if(child.HotelIdSource != null)
						{
							child.HotelId = child.HotelIdSource.HotelId;
						}
						else
						{
							child.HotelId = entity.HotelId;
						}

					}

					if (entity.HabitacionCollection.Count > 0 || entity.HabitacionCollection.DeletedItems.Count > 0)
					{
						//DataRepository.HabitacionProvider.Save(transactionManager, entity.HabitacionCollection);
						
						deepHandles.Add("HabitacionCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Habitacion >) DataRepository.HabitacionProvider.DeepSave,
							new object[] { transactionManager, entity.HabitacionCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<ViajeHotel>
				if (CanDeepSave(entity.ViajeHotelCollection, "List<ViajeHotel>|ViajeHotelCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(ViajeHotel child in entity.ViajeHotelCollection)
					{
						if(child.HotelIdSource != null)
						{
							child.HotelId = child.HotelIdSource.HotelId;
						}
						else
						{
							child.HotelId = entity.HotelId;
						}

					}

					if (entity.ViajeHotelCollection.Count > 0 || entity.ViajeHotelCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ViajeHotelProvider.Save(transactionManager, entity.ViajeHotelCollection);
						
						deepHandles.Add("ViajeHotelCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< ViajeHotel >) DataRepository.ViajeHotelProvider.DeepSave,
							new object[] { transactionManager, entity.ViajeHotelCollection, deepSaveType, childTypes, innerList }
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
	
	#region HotelChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Hotel</c>
	///</summary>
	public enum HotelChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Localidad</c> at LocalidadIdSource
		///</summary>
		[ChildEntityType(typeof(Localidad))]
		Localidad,
		///<summary>
		/// Collection of <c>Hotel</c> as OneToMany for ServicioCollection
		///</summary>
		[ChildEntityType(typeof(TList<Servicio>))]
		ServicioCollection,
		///<summary>
		/// Collection of <c>Hotel</c> as OneToMany for HabitacionCollection
		///</summary>
		[ChildEntityType(typeof(TList<Habitacion>))]
		HabitacionCollection,
		///<summary>
		/// Collection of <c>Hotel</c> as OneToMany for ViajeHotelCollection
		///</summary>
		[ChildEntityType(typeof(TList<ViajeHotel>))]
		ViajeHotelCollection,
	}
	
	#endregion HotelChildEntityTypes
	
	#region HotelFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;HotelColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Hotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HotelFilterBuilder : SqlFilterBuilder<HotelColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HotelFilterBuilder class.
		/// </summary>
		public HotelFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the HotelFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HotelFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HotelFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HotelFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HotelFilterBuilder
	
	#region HotelParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;HotelColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Hotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HotelParameterBuilder : ParameterizedSqlFilterBuilder<HotelColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HotelParameterBuilder class.
		/// </summary>
		public HotelParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the HotelParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HotelParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HotelParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HotelParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HotelParameterBuilder
	
	#region HotelSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;HotelColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Hotel"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class HotelSortBuilder : SqlSortBuilder<HotelColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HotelSqlSortBuilder class.
		/// </summary>
		public HotelSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion HotelSortBuilder
	
} // end namespace
