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
	/// This class is the base class for any <see cref="ButacaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ButacaProviderBaseCore : EntityProviderBase<MAT.Entities.Butaca, MAT.Entities.ButacaKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.ButacaKey key)
		{
			return Delete(transactionManager, key.ButacaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_butacaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _butacaId)
		{
			return Delete(null, _butacaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_butacaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _butacaId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Butaca_Transporte key.
		///		FK_Butaca_Transporte Description: 
		/// </summary>
		/// <param name="_transporteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Butaca objects.</returns>
		public TList<Butaca> GetByTransporteId(System.Guid? _transporteId)
		{
			int count = -1;
			return GetByTransporteId(_transporteId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Butaca_Transporte key.
		///		FK_Butaca_Transporte Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_transporteId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Butaca objects.</returns>
		/// <remarks></remarks>
		public TList<Butaca> GetByTransporteId(TransactionManager transactionManager, System.Guid? _transporteId)
		{
			int count = -1;
			return GetByTransporteId(transactionManager, _transporteId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Butaca_Transporte key.
		///		FK_Butaca_Transporte Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_transporteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Butaca objects.</returns>
		public TList<Butaca> GetByTransporteId(TransactionManager transactionManager, System.Guid? _transporteId, int start, int pageLength)
		{
			int count = -1;
			return GetByTransporteId(transactionManager, _transporteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Butaca_Transporte key.
		///		fkButacaTransporte Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_transporteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Butaca objects.</returns>
		public TList<Butaca> GetByTransporteId(System.Guid? _transporteId, int start, int pageLength)
		{
			int count =  -1;
			return GetByTransporteId(null, _transporteId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Butaca_Transporte key.
		///		fkButacaTransporte Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_transporteId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Butaca objects.</returns>
		public TList<Butaca> GetByTransporteId(System.Guid? _transporteId, int start, int pageLength,out int count)
		{
			return GetByTransporteId(null, _transporteId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Butaca_Transporte key.
		///		FK_Butaca_Transporte Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_transporteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Butaca objects.</returns>
		public abstract TList<Butaca> GetByTransporteId(TransactionManager transactionManager, System.Guid? _transporteId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Butaca Get(TransactionManager transactionManager, MAT.Entities.ButacaKey key, int start, int pageLength)
		{
			return GetByButacaId(transactionManager, key.ButacaId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Butaca index.
		/// </summary>
		/// <param name="_butacaId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Butaca"/> class.</returns>
		public MAT.Entities.Butaca GetByButacaId(System.Guid _butacaId)
		{
			int count = -1;
			return GetByButacaId(null,_butacaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Butaca index.
		/// </summary>
		/// <param name="_butacaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Butaca"/> class.</returns>
		public MAT.Entities.Butaca GetByButacaId(System.Guid _butacaId, int start, int pageLength)
		{
			int count = -1;
			return GetByButacaId(null, _butacaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Butaca index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_butacaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Butaca"/> class.</returns>
		public MAT.Entities.Butaca GetByButacaId(TransactionManager transactionManager, System.Guid _butacaId)
		{
			int count = -1;
			return GetByButacaId(transactionManager, _butacaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Butaca index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_butacaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Butaca"/> class.</returns>
		public MAT.Entities.Butaca GetByButacaId(TransactionManager transactionManager, System.Guid _butacaId, int start, int pageLength)
		{
			int count = -1;
			return GetByButacaId(transactionManager, _butacaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Butaca index.
		/// </summary>
		/// <param name="_butacaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Butaca"/> class.</returns>
		public MAT.Entities.Butaca GetByButacaId(System.Guid _butacaId, int start, int pageLength, out int count)
		{
			return GetByButacaId(null, _butacaId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Butaca index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_butacaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Butaca"/> class.</returns>
		public abstract MAT.Entities.Butaca GetByButacaId(TransactionManager transactionManager, System.Guid _butacaId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Butaca&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Butaca&gt;"/></returns>
		public static TList<Butaca> Fill(IDataReader reader, TList<Butaca> rows, int start, int pageLength)
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
				
				MAT.Entities.Butaca c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Butaca")
					.Append("|").Append((System.Guid)reader[((int)ButacaColumn.ButacaId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Butaca>(
					key.ToString(), // EntityTrackingKey
					"Butaca",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Butaca();
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
					c.ButacaId = (System.Guid)reader[((int)ButacaColumn.ButacaId - 1)];
					c.OriginalButacaId = c.ButacaId;
					c.NroButaca = (reader.IsDBNull(((int)ButacaColumn.NroButaca - 1)))?null:(System.Int32?)reader[((int)ButacaColumn.NroButaca - 1)];
					c.Piso = (reader.IsDBNull(((int)ButacaColumn.Piso - 1)))?null:(System.Int32?)reader[((int)ButacaColumn.Piso - 1)];
					c.Ubicacion = (reader.IsDBNull(((int)ButacaColumn.Ubicacion - 1)))?null:(System.Int32?)reader[((int)ButacaColumn.Ubicacion - 1)];
					c.Tipo = (reader.IsDBNull(((int)ButacaColumn.Tipo - 1)))?null:(System.Int32?)reader[((int)ButacaColumn.Tipo - 1)];
					c.TransporteId = (reader.IsDBNull(((int)ButacaColumn.TransporteId - 1)))?null:(System.Guid?)reader[((int)ButacaColumn.TransporteId - 1)];
					c.Fila = (reader.IsDBNull(((int)ButacaColumn.Fila - 1)))?null:(System.String)reader[((int)ButacaColumn.Fila - 1)];
					c.Posicion = (reader.IsDBNull(((int)ButacaColumn.Posicion - 1)))?null:(System.String)reader[((int)ButacaColumn.Posicion - 1)];
					c.CodigoButaca = (reader.IsDBNull(((int)ButacaColumn.CodigoButaca - 1)))?null:(System.String)reader[((int)ButacaColumn.CodigoButaca - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Butaca"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Butaca"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Butaca entity)
		{
			if (!reader.Read()) return;
			
			entity.ButacaId = (System.Guid)reader[((int)ButacaColumn.ButacaId - 1)];
			entity.OriginalButacaId = (System.Guid)reader["ButacaID"];
			entity.NroButaca = (reader.IsDBNull(((int)ButacaColumn.NroButaca - 1)))?null:(System.Int32?)reader[((int)ButacaColumn.NroButaca - 1)];
			entity.Piso = (reader.IsDBNull(((int)ButacaColumn.Piso - 1)))?null:(System.Int32?)reader[((int)ButacaColumn.Piso - 1)];
			entity.Ubicacion = (reader.IsDBNull(((int)ButacaColumn.Ubicacion - 1)))?null:(System.Int32?)reader[((int)ButacaColumn.Ubicacion - 1)];
			entity.Tipo = (reader.IsDBNull(((int)ButacaColumn.Tipo - 1)))?null:(System.Int32?)reader[((int)ButacaColumn.Tipo - 1)];
			entity.TransporteId = (reader.IsDBNull(((int)ButacaColumn.TransporteId - 1)))?null:(System.Guid?)reader[((int)ButacaColumn.TransporteId - 1)];
			entity.Fila = (reader.IsDBNull(((int)ButacaColumn.Fila - 1)))?null:(System.String)reader[((int)ButacaColumn.Fila - 1)];
			entity.Posicion = (reader.IsDBNull(((int)ButacaColumn.Posicion - 1)))?null:(System.String)reader[((int)ButacaColumn.Posicion - 1)];
			entity.CodigoButaca = (reader.IsDBNull(((int)ButacaColumn.CodigoButaca - 1)))?null:(System.String)reader[((int)ButacaColumn.CodigoButaca - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Butaca"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Butaca"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Butaca entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ButacaId = (System.Guid)dataRow["ButacaID"];
			entity.OriginalButacaId = (System.Guid)dataRow["ButacaID"];
			entity.NroButaca = Convert.IsDBNull(dataRow["NroButaca"]) ? null : (System.Int32?)dataRow["NroButaca"];
			entity.Piso = Convert.IsDBNull(dataRow["Piso"]) ? null : (System.Int32?)dataRow["Piso"];
			entity.Ubicacion = Convert.IsDBNull(dataRow["Ubicacion"]) ? null : (System.Int32?)dataRow["Ubicacion"];
			entity.Tipo = Convert.IsDBNull(dataRow["Tipo"]) ? null : (System.Int32?)dataRow["Tipo"];
			entity.TransporteId = Convert.IsDBNull(dataRow["TransporteID"]) ? null : (System.Guid?)dataRow["TransporteID"];
			entity.Fila = Convert.IsDBNull(dataRow["Fila"]) ? null : (System.String)dataRow["Fila"];
			entity.Posicion = Convert.IsDBNull(dataRow["Posicion"]) ? null : (System.String)dataRow["Posicion"];
			entity.CodigoButaca = Convert.IsDBNull(dataRow["CodigoButaca"]) ? null : (System.String)dataRow["CodigoButaca"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Butaca"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Butaca Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Butaca entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region TransporteIdSource	
			if (CanDeepLoad(entity, "Transporte|TransporteIdSource", deepLoadType, innerList) 
				&& entity.TransporteIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.TransporteId ?? Guid.Empty);
				Transporte tmpEntity = EntityManager.LocateEntity<Transporte>(EntityLocator.ConstructKeyFromPkItems(typeof(Transporte), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.TransporteIdSource = tmpEntity;
				else
					entity.TransporteIdSource = DataRepository.TransporteProvider.GetByTransporteId(transactionManager, (entity.TransporteId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'TransporteIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.TransporteIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.TransporteProvider.DeepLoad(transactionManager, entity.TransporteIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion TransporteIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByButacaId methods when available
			
			#region PasajeCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Pasaje>|PasajeCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PasajeCollection = DataRepository.PasajeProvider.GetByButacaId(transactionManager, entity.ButacaId);

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
		/// Deep Save the entire object graph of the MAT.Entities.Butaca object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Butaca instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Butaca Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Butaca entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region TransporteIdSource
			if (CanDeepSave(entity, "Transporte|TransporteIdSource", deepSaveType, innerList) 
				&& entity.TransporteIdSource != null)
			{
				DataRepository.TransporteProvider.Save(transactionManager, entity.TransporteIdSource);
				entity.TransporteId = entity.TransporteIdSource.TransporteId;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<Pasaje>
				if (CanDeepSave(entity.PasajeCollection, "List<Pasaje>|PasajeCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Pasaje child in entity.PasajeCollection)
					{
						if(child.ButacaIdSource != null)
						{
							child.ButacaId = child.ButacaIdSource.ButacaId;
						}
						else
						{
							child.ButacaId = entity.ButacaId;
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
	
	#region ButacaChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Butaca</c>
	///</summary>
	public enum ButacaChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Transporte</c> at TransporteIdSource
		///</summary>
		[ChildEntityType(typeof(Transporte))]
		Transporte,
		///<summary>
		/// Collection of <c>Butaca</c> as OneToMany for PasajeCollection
		///</summary>
		[ChildEntityType(typeof(TList<Pasaje>))]
		PasajeCollection,
	}
	
	#endregion ButacaChildEntityTypes
	
	#region ButacaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ButacaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Butaca"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ButacaFilterBuilder : SqlFilterBuilder<ButacaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ButacaFilterBuilder class.
		/// </summary>
		public ButacaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ButacaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ButacaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ButacaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ButacaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ButacaFilterBuilder
	
	#region ButacaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ButacaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Butaca"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ButacaParameterBuilder : ParameterizedSqlFilterBuilder<ButacaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ButacaParameterBuilder class.
		/// </summary>
		public ButacaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ButacaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ButacaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ButacaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ButacaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ButacaParameterBuilder
	
	#region ButacaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ButacaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Butaca"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ButacaSortBuilder : SqlSortBuilder<ButacaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ButacaSqlSortBuilder class.
		/// </summary>
		public ButacaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ButacaSortBuilder
	
} // end namespace
