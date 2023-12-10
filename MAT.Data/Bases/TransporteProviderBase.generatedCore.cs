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
	/// This class is the base class for any <see cref="TransporteProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class TransporteProviderBaseCore : EntityProviderBase<MAT.Entities.Transporte, MAT.Entities.TransporteKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.TransporteKey key)
		{
			return Delete(transactionManager, key.TransporteId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_transporteId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _transporteId)
		{
			return Delete(null, _transporteId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_transporteId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _transporteId);		
		
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
		public override MAT.Entities.Transporte Get(TransactionManager transactionManager, MAT.Entities.TransporteKey key, int start, int pageLength)
		{
			return GetByTransporteId(transactionManager, key.TransporteId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Transporte index.
		/// </summary>
		/// <param name="_transporteId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Transporte"/> class.</returns>
		public MAT.Entities.Transporte GetByTransporteId(System.Guid _transporteId)
		{
			int count = -1;
			return GetByTransporteId(null,_transporteId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Transporte index.
		/// </summary>
		/// <param name="_transporteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Transporte"/> class.</returns>
		public MAT.Entities.Transporte GetByTransporteId(System.Guid _transporteId, int start, int pageLength)
		{
			int count = -1;
			return GetByTransporteId(null, _transporteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Transporte index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_transporteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Transporte"/> class.</returns>
		public MAT.Entities.Transporte GetByTransporteId(TransactionManager transactionManager, System.Guid _transporteId)
		{
			int count = -1;
			return GetByTransporteId(transactionManager, _transporteId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Transporte index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_transporteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Transporte"/> class.</returns>
		public MAT.Entities.Transporte GetByTransporteId(TransactionManager transactionManager, System.Guid _transporteId, int start, int pageLength)
		{
			int count = -1;
			return GetByTransporteId(transactionManager, _transporteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Transporte index.
		/// </summary>
		/// <param name="_transporteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Transporte"/> class.</returns>
		public MAT.Entities.Transporte GetByTransporteId(System.Guid _transporteId, int start, int pageLength, out int count)
		{
			return GetByTransporteId(null, _transporteId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Transporte index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_transporteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Transporte"/> class.</returns>
		public abstract MAT.Entities.Transporte GetByTransporteId(TransactionManager transactionManager, System.Guid _transporteId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Transporte&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Transporte&gt;"/></returns>
		public static TList<Transporte> Fill(IDataReader reader, TList<Transporte> rows, int start, int pageLength)
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
				
				MAT.Entities.Transporte c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Transporte")
					.Append("|").Append((System.Guid)reader[((int)TransporteColumn.TransporteId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Transporte>(
					key.ToString(), // EntityTrackingKey
					"Transporte",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Transporte();
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
					c.TransporteId = (System.Guid)reader[((int)TransporteColumn.TransporteId - 1)];
					c.OriginalTransporteId = c.TransporteId;
					c.NroCoche = (reader.IsDBNull(((int)TransporteColumn.NroCoche - 1)))?null:(System.String)reader[((int)TransporteColumn.NroCoche - 1)];
					c.MaxPasajeros = (reader.IsDBNull(((int)TransporteColumn.MaxPasajeros - 1)))?null:(System.Int32?)reader[((int)TransporteColumn.MaxPasajeros - 1)];
					c.KmRecorridos = (reader.IsDBNull(((int)TransporteColumn.KmRecorridos - 1)))?null:(System.Int32?)reader[((int)TransporteColumn.KmRecorridos - 1)];
					c.UltimoService = (reader.IsDBNull(((int)TransporteColumn.UltimoService - 1)))?null:(System.DateTime?)reader[((int)TransporteColumn.UltimoService - 1)];
					c.Matricula = (reader.IsDBNull(((int)TransporteColumn.Matricula - 1)))?null:(System.String)reader[((int)TransporteColumn.Matricula - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Transporte"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Transporte"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Transporte entity)
		{
			if (!reader.Read()) return;
			
			entity.TransporteId = (System.Guid)reader[((int)TransporteColumn.TransporteId - 1)];
			entity.OriginalTransporteId = (System.Guid)reader["TransporteID"];
			entity.NroCoche = (reader.IsDBNull(((int)TransporteColumn.NroCoche - 1)))?null:(System.String)reader[((int)TransporteColumn.NroCoche - 1)];
			entity.MaxPasajeros = (reader.IsDBNull(((int)TransporteColumn.MaxPasajeros - 1)))?null:(System.Int32?)reader[((int)TransporteColumn.MaxPasajeros - 1)];
			entity.KmRecorridos = (reader.IsDBNull(((int)TransporteColumn.KmRecorridos - 1)))?null:(System.Int32?)reader[((int)TransporteColumn.KmRecorridos - 1)];
			entity.UltimoService = (reader.IsDBNull(((int)TransporteColumn.UltimoService - 1)))?null:(System.DateTime?)reader[((int)TransporteColumn.UltimoService - 1)];
			entity.Matricula = (reader.IsDBNull(((int)TransporteColumn.Matricula - 1)))?null:(System.String)reader[((int)TransporteColumn.Matricula - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Transporte"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Transporte"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Transporte entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.TransporteId = (System.Guid)dataRow["TransporteID"];
			entity.OriginalTransporteId = (System.Guid)dataRow["TransporteID"];
			entity.NroCoche = Convert.IsDBNull(dataRow["NroCoche"]) ? null : (System.String)dataRow["NroCoche"];
			entity.MaxPasajeros = Convert.IsDBNull(dataRow["MaxPasajeros"]) ? null : (System.Int32?)dataRow["MaxPasajeros"];
			entity.KmRecorridos = Convert.IsDBNull(dataRow["KmRecorridos"]) ? null : (System.Int32?)dataRow["KmRecorridos"];
			entity.UltimoService = Convert.IsDBNull(dataRow["UltimoService"]) ? null : (System.DateTime?)dataRow["UltimoService"];
			entity.Matricula = Convert.IsDBNull(dataRow["Matricula"]) ? null : (System.String)dataRow["Matricula"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Transporte"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Transporte Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Transporte entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByTransporteId methods when available
			
			#region ViajeCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Viaje>|ViajeCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ViajeCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ViajeCollection = DataRepository.ViajeProvider.GetByBusId(transactionManager, entity.TransporteId);

				if (deep && entity.ViajeCollection.Count > 0)
				{
					deepHandles.Add("ViajeCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Viaje>) DataRepository.ViajeProvider.DeepLoad,
						new object[] { transactionManager, entity.ViajeCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region ServicioCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Servicio>|ServicioCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ServicioCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ServicioCollection = DataRepository.ServicioProvider.GetByTransporteId(transactionManager, entity.TransporteId);

				if (deep && entity.ServicioCollection.Count > 0)
				{
					deepHandles.Add("ServicioCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Servicio>) DataRepository.ServicioProvider.DeepLoad,
						new object[] { transactionManager, entity.ServicioCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region ButacaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Butaca>|ButacaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ButacaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ButacaCollection = DataRepository.ButacaProvider.GetByTransporteId(transactionManager, entity.TransporteId);

				if (deep && entity.ButacaCollection.Count > 0)
				{
					deepHandles.Add("ButacaCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Butaca>) DataRepository.ButacaProvider.DeepLoad,
						new object[] { transactionManager, entity.ButacaCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Transporte object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Transporte instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Transporte Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Transporte entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<Viaje>
				if (CanDeepSave(entity.ViajeCollection, "List<Viaje>|ViajeCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Viaje child in entity.ViajeCollection)
					{
						if(child.BusIdSource != null)
						{
							child.BusId = child.BusIdSource.TransporteId;
						}
						else
						{
							child.BusId = entity.TransporteId;
						}

					}

					if (entity.ViajeCollection.Count > 0 || entity.ViajeCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ViajeProvider.Save(transactionManager, entity.ViajeCollection);
						
						deepHandles.Add("ViajeCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Viaje >) DataRepository.ViajeProvider.DeepSave,
							new object[] { transactionManager, entity.ViajeCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Servicio>
				if (CanDeepSave(entity.ServicioCollection, "List<Servicio>|ServicioCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Servicio child in entity.ServicioCollection)
					{
						if(child.TransporteIdSource != null)
						{
							child.TransporteId = child.TransporteIdSource.TransporteId;
						}
						else
						{
							child.TransporteId = entity.TransporteId;
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
				
	
			#region List<Butaca>
				if (CanDeepSave(entity.ButacaCollection, "List<Butaca>|ButacaCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Butaca child in entity.ButacaCollection)
					{
						if(child.TransporteIdSource != null)
						{
							child.TransporteId = child.TransporteIdSource.TransporteId;
						}
						else
						{
							child.TransporteId = entity.TransporteId;
						}

					}

					if (entity.ButacaCollection.Count > 0 || entity.ButacaCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ButacaProvider.Save(transactionManager, entity.ButacaCollection);
						
						deepHandles.Add("ButacaCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Butaca >) DataRepository.ButacaProvider.DeepSave,
							new object[] { transactionManager, entity.ButacaCollection, deepSaveType, childTypes, innerList }
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
	
	#region TransporteChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Transporte</c>
	///</summary>
	public enum TransporteChildEntityTypes
	{
		///<summary>
		/// Collection of <c>Transporte</c> as OneToMany for ViajeCollection
		///</summary>
		[ChildEntityType(typeof(TList<Viaje>))]
		ViajeCollection,
		///<summary>
		/// Collection of <c>Transporte</c> as OneToMany for ServicioCollection
		///</summary>
		[ChildEntityType(typeof(TList<Servicio>))]
		ServicioCollection,
		///<summary>
		/// Collection of <c>Transporte</c> as OneToMany for ButacaCollection
		///</summary>
		[ChildEntityType(typeof(TList<Butaca>))]
		ButacaCollection,
	}
	
	#endregion TransporteChildEntityTypes
	
	#region TransporteFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;TransporteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Transporte"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TransporteFilterBuilder : SqlFilterBuilder<TransporteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TransporteFilterBuilder class.
		/// </summary>
		public TransporteFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the TransporteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public TransporteFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the TransporteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public TransporteFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion TransporteFilterBuilder
	
	#region TransporteParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;TransporteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Transporte"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TransporteParameterBuilder : ParameterizedSqlFilterBuilder<TransporteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TransporteParameterBuilder class.
		/// </summary>
		public TransporteParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the TransporteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public TransporteParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the TransporteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public TransporteParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion TransporteParameterBuilder
	
	#region TransporteSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;TransporteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Transporte"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class TransporteSortBuilder : SqlSortBuilder<TransporteColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TransporteSqlSortBuilder class.
		/// </summary>
		public TransporteSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion TransporteSortBuilder
	
} // end namespace
