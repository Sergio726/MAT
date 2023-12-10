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
	/// This class is the base class for any <see cref="PersonaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PersonaProviderBaseCore : EntityProviderBase<MAT.Entities.Persona, MAT.Entities.PersonaKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PersonaKey key)
		{
			return Delete(transactionManager, key.PersonaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_personaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _personaId)
		{
			return Delete(null, _personaId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_personaId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _personaId);		
		
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
		public override MAT.Entities.Persona Get(TransactionManager transactionManager, MAT.Entities.PersonaKey key, int start, int pageLength)
		{
			return GetByPersonaId(transactionManager, key.PersonaId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Persona index.
		/// </summary>
		/// <param name="_personaId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Persona"/> class.</returns>
		public MAT.Entities.Persona GetByPersonaId(System.Guid _personaId)
		{
			int count = -1;
			return GetByPersonaId(null,_personaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Persona index.
		/// </summary>
		/// <param name="_personaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Persona"/> class.</returns>
		public MAT.Entities.Persona GetByPersonaId(System.Guid _personaId, int start, int pageLength)
		{
			int count = -1;
			return GetByPersonaId(null, _personaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Persona index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_personaId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Persona"/> class.</returns>
		public MAT.Entities.Persona GetByPersonaId(TransactionManager transactionManager, System.Guid _personaId)
		{
			int count = -1;
			return GetByPersonaId(transactionManager, _personaId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Persona index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_personaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Persona"/> class.</returns>
		public MAT.Entities.Persona GetByPersonaId(TransactionManager transactionManager, System.Guid _personaId, int start, int pageLength)
		{
			int count = -1;
			return GetByPersonaId(transactionManager, _personaId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Persona index.
		/// </summary>
		/// <param name="_personaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Persona"/> class.</returns>
		public MAT.Entities.Persona GetByPersonaId(System.Guid _personaId, int start, int pageLength, out int count)
		{
			return GetByPersonaId(null, _personaId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Persona index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_personaId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Persona"/> class.</returns>
		public abstract MAT.Entities.Persona GetByPersonaId(TransactionManager transactionManager, System.Guid _personaId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Persona&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Persona&gt;"/></returns>
		public static TList<Persona> Fill(IDataReader reader, TList<Persona> rows, int start, int pageLength)
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
				
				MAT.Entities.Persona c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Persona")
					.Append("|").Append((System.Guid)reader[((int)PersonaColumn.PersonaId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Persona>(
					key.ToString(), // EntityTrackingKey
					"Persona",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Persona();
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
					c.PersonaId = (System.Guid)reader[((int)PersonaColumn.PersonaId - 1)];
					c.OriginalPersonaId = c.PersonaId;
					c.Apellido = (reader.IsDBNull(((int)PersonaColumn.Apellido - 1)))?null:(System.String)reader[((int)PersonaColumn.Apellido - 1)];
					c.Nombre = (reader.IsDBNull(((int)PersonaColumn.Nombre - 1)))?null:(System.String)reader[((int)PersonaColumn.Nombre - 1)];
					c.TipoDocumento = (reader.IsDBNull(((int)PersonaColumn.TipoDocumento - 1)))?null:(System.Int32?)reader[((int)PersonaColumn.TipoDocumento - 1)];
					c.NroDocumento = (reader.IsDBNull(((int)PersonaColumn.NroDocumento - 1)))?null:(System.String)reader[((int)PersonaColumn.NroDocumento - 1)];
					c.Celular = (reader.IsDBNull(((int)PersonaColumn.Celular - 1)))?null:(System.String)reader[((int)PersonaColumn.Celular - 1)];
					c.Telefono = (reader.IsDBNull(((int)PersonaColumn.Telefono - 1)))?null:(System.String)reader[((int)PersonaColumn.Telefono - 1)];
					c.Email = (reader.IsDBNull(((int)PersonaColumn.Email - 1)))?null:(System.String)reader[((int)PersonaColumn.Email - 1)];
					c.FechaNacimiento = (reader.IsDBNull(((int)PersonaColumn.FechaNacimiento - 1)))?null:(System.DateTime?)reader[((int)PersonaColumn.FechaNacimiento - 1)];
					c.LocalidadId = (reader.IsDBNull(((int)PersonaColumn.LocalidadId - 1)))?null:(System.Int32?)reader[((int)PersonaColumn.LocalidadId - 1)];
					c.UserId = (reader.IsDBNull(((int)PersonaColumn.UserId - 1)))?null:(System.Int32?)reader[((int)PersonaColumn.UserId - 1)];
					c.Domicilio = (reader.IsDBNull(((int)PersonaColumn.Domicilio - 1)))?null:(System.String)reader[((int)PersonaColumn.Domicilio - 1)];
					c.Sexo = (reader.IsDBNull(((int)PersonaColumn.Sexo - 1)))?null:(System.Int32?)reader[((int)PersonaColumn.Sexo - 1)];
					c.Ocupacion = (reader.IsDBNull(((int)PersonaColumn.Ocupacion - 1)))?null:(System.String)reader[((int)PersonaColumn.Ocupacion - 1)];
					c.Nacionalidad = (reader.IsDBNull(((int)PersonaColumn.Nacionalidad - 1)))?null:(System.String)reader[((int)PersonaColumn.Nacionalidad - 1)];
					c.PaisResidencia = (reader.IsDBNull(((int)PersonaColumn.PaisResidencia - 1)))?null:(System.String)reader[((int)PersonaColumn.PaisResidencia - 1)];
					c.Provincia = (reader.IsDBNull(((int)PersonaColumn.Provincia - 1)))?null:(System.Int32?)reader[((int)PersonaColumn.Provincia - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Persona"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Persona"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Persona entity)
		{
			if (!reader.Read()) return;
			
			entity.PersonaId = (System.Guid)reader[((int)PersonaColumn.PersonaId - 1)];
			entity.OriginalPersonaId = (System.Guid)reader["PersonaID"];
			entity.Apellido = (reader.IsDBNull(((int)PersonaColumn.Apellido - 1)))?null:(System.String)reader[((int)PersonaColumn.Apellido - 1)];
			entity.Nombre = (reader.IsDBNull(((int)PersonaColumn.Nombre - 1)))?null:(System.String)reader[((int)PersonaColumn.Nombre - 1)];
			entity.TipoDocumento = (reader.IsDBNull(((int)PersonaColumn.TipoDocumento - 1)))?null:(System.Int32?)reader[((int)PersonaColumn.TipoDocumento - 1)];
			entity.NroDocumento = (reader.IsDBNull(((int)PersonaColumn.NroDocumento - 1)))?null:(System.String)reader[((int)PersonaColumn.NroDocumento - 1)];
			entity.Celular = (reader.IsDBNull(((int)PersonaColumn.Celular - 1)))?null:(System.String)reader[((int)PersonaColumn.Celular - 1)];
			entity.Telefono = (reader.IsDBNull(((int)PersonaColumn.Telefono - 1)))?null:(System.String)reader[((int)PersonaColumn.Telefono - 1)];
			entity.Email = (reader.IsDBNull(((int)PersonaColumn.Email - 1)))?null:(System.String)reader[((int)PersonaColumn.Email - 1)];
			entity.FechaNacimiento = (reader.IsDBNull(((int)PersonaColumn.FechaNacimiento - 1)))?null:(System.DateTime?)reader[((int)PersonaColumn.FechaNacimiento - 1)];
			entity.LocalidadId = (reader.IsDBNull(((int)PersonaColumn.LocalidadId - 1)))?null:(System.Int32?)reader[((int)PersonaColumn.LocalidadId - 1)];
			entity.UserId = (reader.IsDBNull(((int)PersonaColumn.UserId - 1)))?null:(System.Int32?)reader[((int)PersonaColumn.UserId - 1)];
			entity.Domicilio = (reader.IsDBNull(((int)PersonaColumn.Domicilio - 1)))?null:(System.String)reader[((int)PersonaColumn.Domicilio - 1)];
			entity.Sexo = (reader.IsDBNull(((int)PersonaColumn.Sexo - 1)))?null:(System.Int32?)reader[((int)PersonaColumn.Sexo - 1)];
			entity.Ocupacion = (reader.IsDBNull(((int)PersonaColumn.Ocupacion - 1)))?null:(System.String)reader[((int)PersonaColumn.Ocupacion - 1)];
			entity.Nacionalidad = (reader.IsDBNull(((int)PersonaColumn.Nacionalidad - 1)))?null:(System.String)reader[((int)PersonaColumn.Nacionalidad - 1)];
			entity.PaisResidencia = (reader.IsDBNull(((int)PersonaColumn.PaisResidencia - 1)))?null:(System.String)reader[((int)PersonaColumn.PaisResidencia - 1)];
			entity.Provincia = (reader.IsDBNull(((int)PersonaColumn.Provincia - 1)))?null:(System.Int32?)reader[((int)PersonaColumn.Provincia - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Persona"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Persona"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Persona entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PersonaId = (System.Guid)dataRow["PersonaID"];
			entity.OriginalPersonaId = (System.Guid)dataRow["PersonaID"];
			entity.Apellido = Convert.IsDBNull(dataRow["Apellido"]) ? null : (System.String)dataRow["Apellido"];
			entity.Nombre = Convert.IsDBNull(dataRow["Nombre"]) ? null : (System.String)dataRow["Nombre"];
			entity.TipoDocumento = Convert.IsDBNull(dataRow["TipoDocumento"]) ? null : (System.Int32?)dataRow["TipoDocumento"];
			entity.NroDocumento = Convert.IsDBNull(dataRow["NroDocumento"]) ? null : (System.String)dataRow["NroDocumento"];
			entity.Celular = Convert.IsDBNull(dataRow["Celular"]) ? null : (System.String)dataRow["Celular"];
			entity.Telefono = Convert.IsDBNull(dataRow["Telefono"]) ? null : (System.String)dataRow["Telefono"];
			entity.Email = Convert.IsDBNull(dataRow["Email"]) ? null : (System.String)dataRow["Email"];
			entity.FechaNacimiento = Convert.IsDBNull(dataRow["FechaNacimiento"]) ? null : (System.DateTime?)dataRow["FechaNacimiento"];
			entity.LocalidadId = Convert.IsDBNull(dataRow["LocalidadID"]) ? null : (System.Int32?)dataRow["LocalidadID"];
			entity.UserId = Convert.IsDBNull(dataRow["UserId"]) ? null : (System.Int32?)dataRow["UserId"];
			entity.Domicilio = Convert.IsDBNull(dataRow["Domicilio"]) ? null : (System.String)dataRow["Domicilio"];
			entity.Sexo = Convert.IsDBNull(dataRow["Sexo"]) ? null : (System.Int32?)dataRow["Sexo"];
			entity.Ocupacion = Convert.IsDBNull(dataRow["Ocupacion"]) ? null : (System.String)dataRow["Ocupacion"];
			entity.Nacionalidad = Convert.IsDBNull(dataRow["Nacionalidad"]) ? null : (System.String)dataRow["Nacionalidad"];
			entity.PaisResidencia = Convert.IsDBNull(dataRow["PaisResidencia"]) ? null : (System.String)dataRow["PaisResidencia"];
			entity.Provincia = Convert.IsDBNull(dataRow["Provincia"]) ? null : (System.Int32?)dataRow["Provincia"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Persona"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Persona Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Persona entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByPersonaId methods when available
			
			#region ReservaHabitacionCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<ReservaHabitacion>|ReservaHabitacionCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ReservaHabitacionCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ReservaHabitacionCollection = DataRepository.ReservaHabitacionProvider.GetByPasajeroId(transactionManager, entity.PersonaId);

				if (deep && entity.ReservaHabitacionCollection.Count > 0)
				{
					deepHandles.Add("ReservaHabitacionCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<ReservaHabitacion>) DataRepository.ReservaHabitacionProvider.DeepLoad,
						new object[] { transactionManager, entity.ReservaHabitacionCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region Proveedor
			// RelationshipType.OneToOne
			if (CanDeepLoad(entity, "Proveedor|Proveedor", deepLoadType, innerList))
			{
				entity.Proveedor = DataRepository.ProveedorProvider.GetByProveedorId(transactionManager, entity.PersonaId);
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'Proveedor' loaded. key " + entity.EntityTrackingKey);
				#endif 

				if (deep && entity.Proveedor != null)
				{
					deepHandles.Add("Proveedor",
						new KeyValuePair<Delegate, object>((DeepLoadSingleHandle< Proveedor >) DataRepository.ProveedorProvider.DeepLoad,
						new object[] { transactionManager, entity.Proveedor, deep, deepLoadType, childTypes, innerList }
					));
				}
			}
			#endregion 
			
			
			
			#region Pasajero
			// RelationshipType.OneToOne
			if (CanDeepLoad(entity, "Pasajero|Pasajero", deepLoadType, innerList))
			{
				entity.Pasajero = DataRepository.PasajeroProvider.GetByPasajeroId(transactionManager, entity.PersonaId);
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'Pasajero' loaded. key " + entity.EntityTrackingKey);
				#endif 

				if (deep && entity.Pasajero != null)
				{
					deepHandles.Add("Pasajero",
						new KeyValuePair<Delegate, object>((DeepLoadSingleHandle< Pasajero >) DataRepository.PasajeroProvider.DeepLoad,
						new object[] { transactionManager, entity.Pasajero, deep, deepLoadType, childTypes, innerList }
					));
				}
			}
			#endregion 
			
			
			
			#region Vendedor
			// RelationshipType.OneToOne
			if (CanDeepLoad(entity, "Vendedor|Vendedor", deepLoadType, innerList))
			{
				entity.Vendedor = DataRepository.VendedorProvider.GetByVendedorId(transactionManager, entity.PersonaId);
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'Vendedor' loaded. key " + entity.EntityTrackingKey);
				#endif 

				if (deep && entity.Vendedor != null)
				{
					deepHandles.Add("Vendedor",
						new KeyValuePair<Delegate, object>((DeepLoadSingleHandle< Vendedor >) DataRepository.VendedorProvider.DeepLoad,
						new object[] { transactionManager, entity.Vendedor, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Persona object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Persona instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Persona Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Persona entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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

			#region Proveedor
			if (CanDeepSave(entity.Proveedor, "Proveedor|Proveedor", deepSaveType, innerList))
			{

				if (entity.Proveedor != null)
				{
					// update each child parent id with the real parent id (mostly used on insert)

					entity.Proveedor.ProveedorId = entity.PersonaId;
					//DataRepository.ProveedorProvider.Save(transactionManager, entity.Proveedor);
					deepHandles.Add("Proveedor",
						new KeyValuePair<Delegate, object>((DeepSaveSingleHandle< Proveedor >) DataRepository.ProveedorProvider.DeepSave,
						new object[] { transactionManager, entity.Proveedor, deepSaveType, childTypes, innerList }
					));
				}
			} 
			#endregion 

			#region Pasajero
			if (CanDeepSave(entity.Pasajero, "Pasajero|Pasajero", deepSaveType, innerList))
			{

				if (entity.Pasajero != null)
				{
					// update each child parent id with the real parent id (mostly used on insert)

					entity.Pasajero.PasajeroId = entity.PersonaId;
					//DataRepository.PasajeroProvider.Save(transactionManager, entity.Pasajero);
					deepHandles.Add("Pasajero",
						new KeyValuePair<Delegate, object>((DeepSaveSingleHandle< Pasajero >) DataRepository.PasajeroProvider.DeepSave,
						new object[] { transactionManager, entity.Pasajero, deepSaveType, childTypes, innerList }
					));
				}
			} 
			#endregion 

			#region Vendedor
			if (CanDeepSave(entity.Vendedor, "Vendedor|Vendedor", deepSaveType, innerList))
			{

				if (entity.Vendedor != null)
				{
					// update each child parent id with the real parent id (mostly used on insert)

					entity.Vendedor.VendedorId = entity.PersonaId;
					//DataRepository.VendedorProvider.Save(transactionManager, entity.Vendedor);
					deepHandles.Add("Vendedor",
						new KeyValuePair<Delegate, object>((DeepSaveSingleHandle< Vendedor >) DataRepository.VendedorProvider.DeepSave,
						new object[] { transactionManager, entity.Vendedor, deepSaveType, childTypes, innerList }
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
						if(child.PasajeroIdSource != null)
						{
							child.PasajeroId = child.PasajeroIdSource.PersonaId;
						}
						else
						{
							child.PasajeroId = entity.PersonaId;
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
	
	#region PersonaChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Persona</c>
	///</summary>
	public enum PersonaChildEntityTypes
	{
		///<summary>
		/// Collection of <c>Persona</c> as OneToMany for ReservaHabitacionCollection
		///</summary>
		[ChildEntityType(typeof(TList<ReservaHabitacion>))]
		ReservaHabitacionCollection,
		///<summary>
		/// Entity <c>Proveedor</c> as OneToOne for Proveedor
		///</summary>
		[ChildEntityType(typeof(Proveedor))]
		Proveedor,
		///<summary>
		/// Entity <c>Pasajero</c> as OneToOne for Pasajero
		///</summary>
		[ChildEntityType(typeof(Pasajero))]
		Pasajero,
		///<summary>
		/// Entity <c>Vendedor</c> as OneToOne for Vendedor
		///</summary>
		[ChildEntityType(typeof(Vendedor))]
		Vendedor,
	}
	
	#endregion PersonaChildEntityTypes
	
	#region PersonaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PersonaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Persona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaFilterBuilder : SqlFilterBuilder<PersonaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaFilterBuilder class.
		/// </summary>
		public PersonaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaFilterBuilder
	
	#region PersonaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PersonaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Persona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaParameterBuilder : ParameterizedSqlFilterBuilder<PersonaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaParameterBuilder class.
		/// </summary>
		public PersonaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaParameterBuilder
	
	#region PersonaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PersonaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Persona"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PersonaSortBuilder : SqlSortBuilder<PersonaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaSqlSortBuilder class.
		/// </summary>
		public PersonaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PersonaSortBuilder
	
} // end namespace
