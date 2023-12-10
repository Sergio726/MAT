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
	/// This class is the base class for any <see cref="HabitacionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class HabitacionProviderBaseCore : EntityProviderBase<MAT.Entities.Habitacion, MAT.Entities.HabitacionKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.HabitacionKey key)
		{
			return Delete(transactionManager, key.HabitacionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_habitacionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _habitacionId)
		{
			return Delete(null, _habitacionId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_habitacionId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _habitacionId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Habitacion_Hotel key.
		///		FK_Habitacion_Hotel Description: 
		/// </summary>
		/// <param name="_hotelId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Habitacion objects.</returns>
		public TList<Habitacion> GetByHotelId(System.Guid? _hotelId)
		{
			int count = -1;
			return GetByHotelId(_hotelId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Habitacion_Hotel key.
		///		FK_Habitacion_Hotel Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Habitacion objects.</returns>
		/// <remarks></remarks>
		public TList<Habitacion> GetByHotelId(TransactionManager transactionManager, System.Guid? _hotelId)
		{
			int count = -1;
			return GetByHotelId(transactionManager, _hotelId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Habitacion_Hotel key.
		///		FK_Habitacion_Hotel Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Habitacion objects.</returns>
		public TList<Habitacion> GetByHotelId(TransactionManager transactionManager, System.Guid? _hotelId, int start, int pageLength)
		{
			int count = -1;
			return GetByHotelId(transactionManager, _hotelId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Habitacion_Hotel key.
		///		fkHabitacionHotel Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_hotelId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Habitacion objects.</returns>
		public TList<Habitacion> GetByHotelId(System.Guid? _hotelId, int start, int pageLength)
		{
			int count =  -1;
			return GetByHotelId(null, _hotelId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Habitacion_Hotel key.
		///		fkHabitacionHotel Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_hotelId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Habitacion objects.</returns>
		public TList<Habitacion> GetByHotelId(System.Guid? _hotelId, int start, int pageLength,out int count)
		{
			return GetByHotelId(null, _hotelId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Habitacion_Hotel key.
		///		FK_Habitacion_Hotel Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_hotelId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Habitacion objects.</returns>
		public abstract TList<Habitacion> GetByHotelId(TransactionManager transactionManager, System.Guid? _hotelId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Habitacion Get(TransactionManager transactionManager, MAT.Entities.HabitacionKey key, int start, int pageLength)
		{
			return GetByHabitacionId(transactionManager, key.HabitacionId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Habitacion index.
		/// </summary>
		/// <param name="_habitacionId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Habitacion"/> class.</returns>
		public MAT.Entities.Habitacion GetByHabitacionId(System.Guid _habitacionId)
		{
			int count = -1;
			return GetByHabitacionId(null,_habitacionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Habitacion index.
		/// </summary>
		/// <param name="_habitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Habitacion"/> class.</returns>
		public MAT.Entities.Habitacion GetByHabitacionId(System.Guid _habitacionId, int start, int pageLength)
		{
			int count = -1;
			return GetByHabitacionId(null, _habitacionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Habitacion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_habitacionId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Habitacion"/> class.</returns>
		public MAT.Entities.Habitacion GetByHabitacionId(TransactionManager transactionManager, System.Guid _habitacionId)
		{
			int count = -1;
			return GetByHabitacionId(transactionManager, _habitacionId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Habitacion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_habitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Habitacion"/> class.</returns>
		public MAT.Entities.Habitacion GetByHabitacionId(TransactionManager transactionManager, System.Guid _habitacionId, int start, int pageLength)
		{
			int count = -1;
			return GetByHabitacionId(transactionManager, _habitacionId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Habitacion index.
		/// </summary>
		/// <param name="_habitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Habitacion"/> class.</returns>
		public MAT.Entities.Habitacion GetByHabitacionId(System.Guid _habitacionId, int start, int pageLength, out int count)
		{
			return GetByHabitacionId(null, _habitacionId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Habitacion index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_habitacionId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Habitacion"/> class.</returns>
		public abstract MAT.Entities.Habitacion GetByHabitacionId(TransactionManager transactionManager, System.Guid _habitacionId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Habitacion&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Habitacion&gt;"/></returns>
		public static TList<Habitacion> Fill(IDataReader reader, TList<Habitacion> rows, int start, int pageLength)
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
				
				MAT.Entities.Habitacion c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Habitacion")
					.Append("|").Append((System.Guid)reader[((int)HabitacionColumn.HabitacionId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Habitacion>(
					key.ToString(), // EntityTrackingKey
					"Habitacion",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Habitacion();
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
					c.HabitacionId = (System.Guid)reader[((int)HabitacionColumn.HabitacionId - 1)];
					c.OriginalHabitacionId = c.HabitacionId;
					c.NroHabitacion = (reader.IsDBNull(((int)HabitacionColumn.NroHabitacion - 1)))?null:(System.Int32?)reader[((int)HabitacionColumn.NroHabitacion - 1)];
					c.Tipo = (System.Int32)reader[((int)HabitacionColumn.Tipo - 1)];
					c.HotelId = (reader.IsDBNull(((int)HabitacionColumn.HotelId - 1)))?null:(System.Guid?)reader[((int)HabitacionColumn.HotelId - 1)];
					c.Estado = (System.Int32)reader[((int)HabitacionColumn.Estado - 1)];
					c.Capacidad = (System.Int32)reader[((int)HabitacionColumn.Capacidad - 1)];
					c.Ocupacion = (System.Int32)reader[((int)HabitacionColumn.Ocupacion - 1)];
					c.Nombre = (reader.IsDBNull(((int)HabitacionColumn.Nombre - 1)))?null:(System.String)reader[((int)HabitacionColumn.Nombre - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Habitacion"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Habitacion"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Habitacion entity)
		{
			if (!reader.Read()) return;
			
			entity.HabitacionId = (System.Guid)reader[((int)HabitacionColumn.HabitacionId - 1)];
			entity.OriginalHabitacionId = (System.Guid)reader["HabitacionID"];
			entity.NroHabitacion = (reader.IsDBNull(((int)HabitacionColumn.NroHabitacion - 1)))?null:(System.Int32?)reader[((int)HabitacionColumn.NroHabitacion - 1)];
			entity.Tipo = (System.Int32)reader[((int)HabitacionColumn.Tipo - 1)];
			entity.HotelId = (reader.IsDBNull(((int)HabitacionColumn.HotelId - 1)))?null:(System.Guid?)reader[((int)HabitacionColumn.HotelId - 1)];
			entity.Estado = (System.Int32)reader[((int)HabitacionColumn.Estado - 1)];
			entity.Capacidad = (System.Int32)reader[((int)HabitacionColumn.Capacidad - 1)];
			entity.Ocupacion = (System.Int32)reader[((int)HabitacionColumn.Ocupacion - 1)];
			entity.Nombre = (reader.IsDBNull(((int)HabitacionColumn.Nombre - 1)))?null:(System.String)reader[((int)HabitacionColumn.Nombre - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Habitacion"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Habitacion"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Habitacion entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.HabitacionId = (System.Guid)dataRow["HabitacionID"];
			entity.OriginalHabitacionId = (System.Guid)dataRow["HabitacionID"];
			entity.NroHabitacion = Convert.IsDBNull(dataRow["NroHabitacion"]) ? null : (System.Int32?)dataRow["NroHabitacion"];
			entity.Tipo = (System.Int32)dataRow["Tipo"];
			entity.HotelId = Convert.IsDBNull(dataRow["HotelID"]) ? null : (System.Guid?)dataRow["HotelID"];
			entity.Estado = (System.Int32)dataRow["Estado"];
			entity.Capacidad = (System.Int32)dataRow["Capacidad"];
			entity.Ocupacion = (System.Int32)dataRow["Ocupacion"];
			entity.Nombre = Convert.IsDBNull(dataRow["Nombre"]) ? null : (System.String)dataRow["Nombre"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Habitacion"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Habitacion Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Habitacion entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region HotelIdSource	
			if (CanDeepLoad(entity, "Hotel|HotelIdSource", deepLoadType, innerList) 
				&& entity.HotelIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.HotelId ?? Guid.Empty);
				Hotel tmpEntity = EntityManager.LocateEntity<Hotel>(EntityLocator.ConstructKeyFromPkItems(typeof(Hotel), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.HotelIdSource = tmpEntity;
				else
					entity.HotelIdSource = DataRepository.HotelProvider.GetByHotelId(transactionManager, (entity.HotelId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'HotelIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.HotelIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.HotelProvider.DeepLoad(transactionManager, entity.HotelIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion HotelIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByHabitacionId methods when available
			
			#region ReservaHabitacionCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<ReservaHabitacion>|ReservaHabitacionCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ReservaHabitacionCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ReservaHabitacionCollection = DataRepository.ReservaHabitacionProvider.GetByHabitacionId(transactionManager, entity.HabitacionId);

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
		/// Deep Save the entire object graph of the MAT.Entities.Habitacion object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Habitacion instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Habitacion Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Habitacion entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region HotelIdSource
			if (CanDeepSave(entity, "Hotel|HotelIdSource", deepSaveType, innerList) 
				&& entity.HotelIdSource != null)
			{
				DataRepository.HotelProvider.Save(transactionManager, entity.HotelIdSource);
				entity.HotelId = entity.HotelIdSource.HotelId;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<ReservaHabitacion>
				if (CanDeepSave(entity.ReservaHabitacionCollection, "List<ReservaHabitacion>|ReservaHabitacionCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(ReservaHabitacion child in entity.ReservaHabitacionCollection)
					{
						if(child.HabitacionIdSource != null)
						{
							child.HabitacionId = child.HabitacionIdSource.HabitacionId;
						}
						else
						{
							child.HabitacionId = entity.HabitacionId;
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
	
	#region HabitacionChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Habitacion</c>
	///</summary>
	public enum HabitacionChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Hotel</c> at HotelIdSource
		///</summary>
		[ChildEntityType(typeof(Hotel))]
		Hotel,
		///<summary>
		/// Collection of <c>Habitacion</c> as OneToMany for ReservaHabitacionCollection
		///</summary>
		[ChildEntityType(typeof(TList<ReservaHabitacion>))]
		ReservaHabitacionCollection,
	}
	
	#endregion HabitacionChildEntityTypes
	
	#region HabitacionFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;HabitacionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Habitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionFilterBuilder : SqlFilterBuilder<HabitacionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionFilterBuilder class.
		/// </summary>
		public HabitacionFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the HabitacionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HabitacionFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HabitacionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HabitacionFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HabitacionFilterBuilder
	
	#region HabitacionParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;HabitacionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Habitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionParameterBuilder : ParameterizedSqlFilterBuilder<HabitacionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionParameterBuilder class.
		/// </summary>
		public HabitacionParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the HabitacionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HabitacionParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HabitacionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HabitacionParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HabitacionParameterBuilder
	
	#region HabitacionSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;HabitacionColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Habitacion"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class HabitacionSortBuilder : SqlSortBuilder<HabitacionColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionSqlSortBuilder class.
		/// </summary>
		public HabitacionSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion HabitacionSortBuilder
	
} // end namespace
