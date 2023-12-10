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
	/// This class is the base class for any <see cref="ProveedorProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ProveedorProviderBaseCore : EntityProviderBase<MAT.Entities.Proveedor, MAT.Entities.ProveedorKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.ProveedorKey key)
		{
			return Delete(transactionManager, key.ProveedorId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_proveedorId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _proveedorId)
		{
			return Delete(null, _proveedorId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_proveedorId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _proveedorId);		
		
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
		public override MAT.Entities.Proveedor Get(TransactionManager transactionManager, MAT.Entities.ProveedorKey key, int start, int pageLength)
		{
			return GetByProveedorId(transactionManager, key.ProveedorId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Proveedor index.
		/// </summary>
		/// <param name="_proveedorId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Proveedor"/> class.</returns>
		public MAT.Entities.Proveedor GetByProveedorId(System.Guid _proveedorId)
		{
			int count = -1;
			return GetByProveedorId(null,_proveedorId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Proveedor index.
		/// </summary>
		/// <param name="_proveedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Proveedor"/> class.</returns>
		public MAT.Entities.Proveedor GetByProveedorId(System.Guid _proveedorId, int start, int pageLength)
		{
			int count = -1;
			return GetByProveedorId(null, _proveedorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Proveedor index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_proveedorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Proveedor"/> class.</returns>
		public MAT.Entities.Proveedor GetByProveedorId(TransactionManager transactionManager, System.Guid _proveedorId)
		{
			int count = -1;
			return GetByProveedorId(transactionManager, _proveedorId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Proveedor index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_proveedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Proveedor"/> class.</returns>
		public MAT.Entities.Proveedor GetByProveedorId(TransactionManager transactionManager, System.Guid _proveedorId, int start, int pageLength)
		{
			int count = -1;
			return GetByProveedorId(transactionManager, _proveedorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Proveedor index.
		/// </summary>
		/// <param name="_proveedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Proveedor"/> class.</returns>
		public MAT.Entities.Proveedor GetByProveedorId(System.Guid _proveedorId, int start, int pageLength, out int count)
		{
			return GetByProveedorId(null, _proveedorId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Proveedor index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_proveedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Proveedor"/> class.</returns>
		public abstract MAT.Entities.Proveedor GetByProveedorId(TransactionManager transactionManager, System.Guid _proveedorId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Proveedor&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Proveedor&gt;"/></returns>
		public static TList<Proveedor> Fill(IDataReader reader, TList<Proveedor> rows, int start, int pageLength)
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
				
				MAT.Entities.Proveedor c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Proveedor")
					.Append("|").Append((System.Guid)reader[((int)ProveedorColumn.ProveedorId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Proveedor>(
					key.ToString(), // EntityTrackingKey
					"Proveedor",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Proveedor();
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
					c.ProveedorId = (System.Guid)reader[((int)ProveedorColumn.ProveedorId - 1)];
					c.OriginalProveedorId = c.ProveedorId;
					c.RazonSocial = (reader.IsDBNull(((int)ProveedorColumn.RazonSocial - 1)))?null:(System.String)reader[((int)ProveedorColumn.RazonSocial - 1)];
					c.Telefono = (reader.IsDBNull(((int)ProveedorColumn.Telefono - 1)))?null:(System.String)reader[((int)ProveedorColumn.Telefono - 1)];
					c.Fax = (reader.IsDBNull(((int)ProveedorColumn.Fax - 1)))?null:(System.String)reader[((int)ProveedorColumn.Fax - 1)];
					c.Web = (reader.IsDBNull(((int)ProveedorColumn.Web - 1)))?null:(System.String)reader[((int)ProveedorColumn.Web - 1)];
					c.Email = (reader.IsDBNull(((int)ProveedorColumn.Email - 1)))?null:(System.String)reader[((int)ProveedorColumn.Email - 1)];
					c.Idioma = (reader.IsDBNull(((int)ProveedorColumn.Idioma - 1)))?null:(System.String)reader[((int)ProveedorColumn.Idioma - 1)];
					c.CondicionIva = (reader.IsDBNull(((int)ProveedorColumn.CondicionIva - 1)))?null:(System.Int32?)reader[((int)ProveedorColumn.CondicionIva - 1)];
					c.Cuit = (reader.IsDBNull(((int)ProveedorColumn.Cuit - 1)))?null:(System.String)reader[((int)ProveedorColumn.Cuit - 1)];
					c.FormaPago = (reader.IsDBNull(((int)ProveedorColumn.FormaPago - 1)))?null:(System.Int32?)reader[((int)ProveedorColumn.FormaPago - 1)];
					c.LocalidadId = (reader.IsDBNull(((int)ProveedorColumn.LocalidadId - 1)))?null:(System.Int32?)reader[((int)ProveedorColumn.LocalidadId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Proveedor"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Proveedor"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Proveedor entity)
		{
			if (!reader.Read()) return;
			
			entity.ProveedorId = (System.Guid)reader[((int)ProveedorColumn.ProveedorId - 1)];
			entity.OriginalProveedorId = (System.Guid)reader["ProveedorID"];
			entity.RazonSocial = (reader.IsDBNull(((int)ProveedorColumn.RazonSocial - 1)))?null:(System.String)reader[((int)ProveedorColumn.RazonSocial - 1)];
			entity.Telefono = (reader.IsDBNull(((int)ProveedorColumn.Telefono - 1)))?null:(System.String)reader[((int)ProveedorColumn.Telefono - 1)];
			entity.Fax = (reader.IsDBNull(((int)ProveedorColumn.Fax - 1)))?null:(System.String)reader[((int)ProveedorColumn.Fax - 1)];
			entity.Web = (reader.IsDBNull(((int)ProveedorColumn.Web - 1)))?null:(System.String)reader[((int)ProveedorColumn.Web - 1)];
			entity.Email = (reader.IsDBNull(((int)ProveedorColumn.Email - 1)))?null:(System.String)reader[((int)ProveedorColumn.Email - 1)];
			entity.Idioma = (reader.IsDBNull(((int)ProveedorColumn.Idioma - 1)))?null:(System.String)reader[((int)ProveedorColumn.Idioma - 1)];
			entity.CondicionIva = (reader.IsDBNull(((int)ProveedorColumn.CondicionIva - 1)))?null:(System.Int32?)reader[((int)ProveedorColumn.CondicionIva - 1)];
			entity.Cuit = (reader.IsDBNull(((int)ProveedorColumn.Cuit - 1)))?null:(System.String)reader[((int)ProveedorColumn.Cuit - 1)];
			entity.FormaPago = (reader.IsDBNull(((int)ProveedorColumn.FormaPago - 1)))?null:(System.Int32?)reader[((int)ProveedorColumn.FormaPago - 1)];
			entity.LocalidadId = (reader.IsDBNull(((int)ProveedorColumn.LocalidadId - 1)))?null:(System.Int32?)reader[((int)ProveedorColumn.LocalidadId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Proveedor"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Proveedor"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Proveedor entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ProveedorId = (System.Guid)dataRow["ProveedorID"];
			entity.OriginalProveedorId = (System.Guid)dataRow["ProveedorID"];
			entity.RazonSocial = Convert.IsDBNull(dataRow["RazonSocial"]) ? null : (System.String)dataRow["RazonSocial"];
			entity.Telefono = Convert.IsDBNull(dataRow["Telefono"]) ? null : (System.String)dataRow["Telefono"];
			entity.Fax = Convert.IsDBNull(dataRow["Fax"]) ? null : (System.String)dataRow["Fax"];
			entity.Web = Convert.IsDBNull(dataRow["Web"]) ? null : (System.String)dataRow["Web"];
			entity.Email = Convert.IsDBNull(dataRow["Email"]) ? null : (System.String)dataRow["Email"];
			entity.Idioma = Convert.IsDBNull(dataRow["Idioma"]) ? null : (System.String)dataRow["Idioma"];
			entity.CondicionIva = Convert.IsDBNull(dataRow["CondicionIva"]) ? null : (System.Int32?)dataRow["CondicionIva"];
			entity.Cuit = Convert.IsDBNull(dataRow["Cuit"]) ? null : (System.String)dataRow["Cuit"];
			entity.FormaPago = Convert.IsDBNull(dataRow["FormaPago"]) ? null : (System.Int32?)dataRow["FormaPago"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Proveedor"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Proveedor Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Proveedor entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ProveedorIdSource	
			if (CanDeepLoad(entity, "Persona|ProveedorIdSource", deepLoadType, innerList) 
				&& entity.ProveedorIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.ProveedorId;
				Persona tmpEntity = EntityManager.LocateEntity<Persona>(EntityLocator.ConstructKeyFromPkItems(typeof(Persona), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ProveedorIdSource = tmpEntity;
				else
					entity.ProveedorIdSource = DataRepository.PersonaProvider.GetByPersonaId(transactionManager, entity.ProveedorId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ProveedorIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ProveedorIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PersonaProvider.DeepLoad(transactionManager, entity.ProveedorIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ProveedorIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByProveedorId methods when available
			
			#region ServicioCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Servicio>|ServicioCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ServicioCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ServicioCollection = DataRepository.ServicioProvider.GetByProveedorId(transactionManager, entity.ProveedorId);

				if (deep && entity.ServicioCollection.Count > 0)
				{
					deepHandles.Add("ServicioCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Servicio>) DataRepository.ServicioProvider.DeepLoad,
						new object[] { transactionManager, entity.ServicioCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region ExcursionCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Excursion>|ExcursionCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ExcursionCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ExcursionCollection = DataRepository.ExcursionProvider.GetByProveedorId(transactionManager, entity.ProveedorId);

				if (deep && entity.ExcursionCollection.Count > 0)
				{
					deepHandles.Add("ExcursionCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Excursion>) DataRepository.ExcursionProvider.DeepLoad,
						new object[] { transactionManager, entity.ExcursionCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Proveedor object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Proveedor instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Proveedor Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Proveedor entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ProveedorIdSource
			if (CanDeepSave(entity, "Persona|ProveedorIdSource", deepSaveType, innerList) 
				&& entity.ProveedorIdSource != null)
			{
				DataRepository.PersonaProvider.Save(transactionManager, entity.ProveedorIdSource);
				entity.ProveedorId = entity.ProveedorIdSource.PersonaId;
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
						if(child.ProveedorIdSource != null)
						{
							child.ProveedorId = child.ProveedorIdSource.ProveedorId;
						}
						else
						{
							child.ProveedorId = entity.ProveedorId;
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
				
	
			#region List<Excursion>
				if (CanDeepSave(entity.ExcursionCollection, "List<Excursion>|ExcursionCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Excursion child in entity.ExcursionCollection)
					{
						if(child.ProveedorIdSource != null)
						{
							child.ProveedorId = child.ProveedorIdSource.ProveedorId;
						}
						else
						{
							child.ProveedorId = entity.ProveedorId;
						}

					}

					if (entity.ExcursionCollection.Count > 0 || entity.ExcursionCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ExcursionProvider.Save(transactionManager, entity.ExcursionCollection);
						
						deepHandles.Add("ExcursionCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Excursion >) DataRepository.ExcursionProvider.DeepSave,
							new object[] { transactionManager, entity.ExcursionCollection, deepSaveType, childTypes, innerList }
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
	
	#region ProveedorChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Proveedor</c>
	///</summary>
	public enum ProveedorChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Persona</c> at ProveedorIdSource
		///</summary>
		[ChildEntityType(typeof(Persona))]
		Persona,
		///<summary>
		/// Collection of <c>Proveedor</c> as OneToMany for ServicioCollection
		///</summary>
		[ChildEntityType(typeof(TList<Servicio>))]
		ServicioCollection,
		///<summary>
		/// Collection of <c>Proveedor</c> as OneToMany for ExcursionCollection
		///</summary>
		[ChildEntityType(typeof(TList<Excursion>))]
		ExcursionCollection,
	}
	
	#endregion ProveedorChildEntityTypes
	
	#region ProveedorFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ProveedorColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Proveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProveedorFilterBuilder : SqlFilterBuilder<ProveedorColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProveedorFilterBuilder class.
		/// </summary>
		public ProveedorFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ProveedorFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ProveedorFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ProveedorFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ProveedorFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ProveedorFilterBuilder
	
	#region ProveedorParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ProveedorColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Proveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProveedorParameterBuilder : ParameterizedSqlFilterBuilder<ProveedorColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProveedorParameterBuilder class.
		/// </summary>
		public ProveedorParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ProveedorParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ProveedorParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ProveedorParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ProveedorParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ProveedorParameterBuilder
	
	#region ProveedorSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ProveedorColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Proveedor"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ProveedorSortBuilder : SqlSortBuilder<ProveedorColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProveedorSqlSortBuilder class.
		/// </summary>
		public ProveedorSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ProveedorSortBuilder
	
} // end namespace
