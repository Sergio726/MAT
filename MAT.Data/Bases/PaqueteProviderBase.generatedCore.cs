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
	/// This class is the base class for any <see cref="PaqueteProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PaqueteProviderBaseCore : EntityProviderBase<MAT.Entities.Paquete, MAT.Entities.PaqueteKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PaqueteKey key)
		{
			return Delete(transactionManager, key.PaqueteId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_paqueteId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _paqueteId)
		{
			return Delete(null, _paqueteId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _paqueteId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Paquete_Localidad key.
		///		FK_Paquete_Localidad Description: 
		/// </summary>
		/// <param name="_destinoId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Paquete objects.</returns>
		public TList<Paquete> GetByDestinoId(System.Int32 _destinoId)
		{
			int count = -1;
			return GetByDestinoId(_destinoId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Paquete_Localidad key.
		///		FK_Paquete_Localidad Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_destinoId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Paquete objects.</returns>
		/// <remarks></remarks>
		public TList<Paquete> GetByDestinoId(TransactionManager transactionManager, System.Int32 _destinoId)
		{
			int count = -1;
			return GetByDestinoId(transactionManager, _destinoId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Paquete_Localidad key.
		///		FK_Paquete_Localidad Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_destinoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Paquete objects.</returns>
		public TList<Paquete> GetByDestinoId(TransactionManager transactionManager, System.Int32 _destinoId, int start, int pageLength)
		{
			int count = -1;
			return GetByDestinoId(transactionManager, _destinoId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Paquete_Localidad key.
		///		fkPaqueteLocalidad Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_destinoId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Paquete objects.</returns>
		public TList<Paquete> GetByDestinoId(System.Int32 _destinoId, int start, int pageLength)
		{
			int count =  -1;
			return GetByDestinoId(null, _destinoId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Paquete_Localidad key.
		///		fkPaqueteLocalidad Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_destinoId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Paquete objects.</returns>
		public TList<Paquete> GetByDestinoId(System.Int32 _destinoId, int start, int pageLength,out int count)
		{
			return GetByDestinoId(null, _destinoId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Paquete_Localidad key.
		///		FK_Paquete_Localidad Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_destinoId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Paquete objects.</returns>
		public abstract TList<Paquete> GetByDestinoId(TransactionManager transactionManager, System.Int32 _destinoId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Paquete Get(TransactionManager transactionManager, MAT.Entities.PaqueteKey key, int start, int pageLength)
		{
			return GetByPaqueteId(transactionManager, key.PaqueteId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Paquete index.
		/// </summary>
		/// <param name="_paqueteId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Paquete"/> class.</returns>
		public MAT.Entities.Paquete GetByPaqueteId(System.Guid _paqueteId)
		{
			int count = -1;
			return GetByPaqueteId(null,_paqueteId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Paquete index.
		/// </summary>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Paquete"/> class.</returns>
		public MAT.Entities.Paquete GetByPaqueteId(System.Guid _paqueteId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaqueteId(null, _paqueteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Paquete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Paquete"/> class.</returns>
		public MAT.Entities.Paquete GetByPaqueteId(TransactionManager transactionManager, System.Guid _paqueteId)
		{
			int count = -1;
			return GetByPaqueteId(transactionManager, _paqueteId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Paquete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Paquete"/> class.</returns>
		public MAT.Entities.Paquete GetByPaqueteId(TransactionManager transactionManager, System.Guid _paqueteId, int start, int pageLength)
		{
			int count = -1;
			return GetByPaqueteId(transactionManager, _paqueteId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Paquete index.
		/// </summary>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Paquete"/> class.</returns>
		public MAT.Entities.Paquete GetByPaqueteId(System.Guid _paqueteId, int start, int pageLength, out int count)
		{
			return GetByPaqueteId(null, _paqueteId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Paquete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_paqueteId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Paquete"/> class.</returns>
		public abstract MAT.Entities.Paquete GetByPaqueteId(TransactionManager transactionManager, System.Guid _paqueteId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Paquete&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Paquete&gt;"/></returns>
		public static TList<Paquete> Fill(IDataReader reader, TList<Paquete> rows, int start, int pageLength)
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
				
				MAT.Entities.Paquete c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Paquete")
					.Append("|").Append((System.Guid)reader[((int)PaqueteColumn.PaqueteId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Paquete>(
					key.ToString(), // EntityTrackingKey
					"Paquete",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Paquete();
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
					c.PaqueteId = (System.Guid)reader[((int)PaqueteColumn.PaqueteId - 1)];
					c.OriginalPaqueteId = c.PaqueteId;
					c.Descripcion = (System.String)reader[((int)PaqueteColumn.Descripcion - 1)];
					c.PrecioCama = (reader.IsDBNull(((int)PaqueteColumn.PrecioCama - 1)))?null:(System.Double?)reader[((int)PaqueteColumn.PrecioCama - 1)];
					c.Moneda = (reader.IsDBNull(((int)PaqueteColumn.Moneda - 1)))?null:(System.Int32?)reader[((int)PaqueteColumn.Moneda - 1)];
					c.Iva = (reader.IsDBNull(((int)PaqueteColumn.Iva - 1)))?null:(System.String)reader[((int)PaqueteColumn.Iva - 1)];
					c.Alicuota = (reader.IsDBNull(((int)PaqueteColumn.Alicuota - 1)))?null:(System.String)reader[((int)PaqueteColumn.Alicuota - 1)];
					c.Temporada = (reader.IsDBNull(((int)PaqueteColumn.Temporada - 1)))?null:(System.Int32?)reader[((int)PaqueteColumn.Temporada - 1)];
					c.Cotizacion = (reader.IsDBNull(((int)PaqueteColumn.Cotizacion - 1)))?null:(System.Double?)reader[((int)PaqueteColumn.Cotizacion - 1)];
					c.Codigo = (reader.IsDBNull(((int)PaqueteColumn.Codigo - 1)))?null:(System.String)reader[((int)PaqueteColumn.Codigo - 1)];
					c.DestinoId = (System.Int32)reader[((int)PaqueteColumn.DestinoId - 1)];
					c.PrecioSemiCama = (reader.IsDBNull(((int)PaqueteColumn.PrecioSemiCama - 1)))?null:(System.Double?)reader[((int)PaqueteColumn.PrecioSemiCama - 1)];
					c.Foto = (reader.IsDBNull(((int)PaqueteColumn.Foto - 1)))?null:(System.String)reader[((int)PaqueteColumn.Foto - 1)];
					c.ServiciosParticulares = (reader.IsDBNull(((int)PaqueteColumn.ServiciosParticulares - 1)))?null:(System.String)reader[((int)PaqueteColumn.ServiciosParticulares - 1)];
					c.FechaCreacion = (reader.IsDBNull(((int)PaqueteColumn.FechaCreacion - 1)))?null:(System.DateTime?)reader[((int)PaqueteColumn.FechaCreacion - 1)];
					c.PublicWeb = (System.Boolean)reader[((int)PaqueteColumn.PublicWeb - 1)];
					c.LastUpdate = (System.DateTime)reader[((int)PaqueteColumn.LastUpdate - 1)];
					c.ModePublicity = (reader.IsDBNull(((int)PaqueteColumn.ModePublicity - 1)))?null:(System.Boolean?)reader[((int)PaqueteColumn.ModePublicity - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Paquete"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Paquete"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Paquete entity)
		{
			if (!reader.Read()) return;
			
			entity.PaqueteId = (System.Guid)reader[((int)PaqueteColumn.PaqueteId - 1)];
			entity.OriginalPaqueteId = (System.Guid)reader["PaqueteID"];
			entity.Descripcion = (System.String)reader[((int)PaqueteColumn.Descripcion - 1)];
			entity.PrecioCama = (reader.IsDBNull(((int)PaqueteColumn.PrecioCama - 1)))?null:(System.Double?)reader[((int)PaqueteColumn.PrecioCama - 1)];
			entity.Moneda = (reader.IsDBNull(((int)PaqueteColumn.Moneda - 1)))?null:(System.Int32?)reader[((int)PaqueteColumn.Moneda - 1)];
			entity.Iva = (reader.IsDBNull(((int)PaqueteColumn.Iva - 1)))?null:(System.String)reader[((int)PaqueteColumn.Iva - 1)];
			entity.Alicuota = (reader.IsDBNull(((int)PaqueteColumn.Alicuota - 1)))?null:(System.String)reader[((int)PaqueteColumn.Alicuota - 1)];
			entity.Temporada = (reader.IsDBNull(((int)PaqueteColumn.Temporada - 1)))?null:(System.Int32?)reader[((int)PaqueteColumn.Temporada - 1)];
			entity.Cotizacion = (reader.IsDBNull(((int)PaqueteColumn.Cotizacion - 1)))?null:(System.Double?)reader[((int)PaqueteColumn.Cotizacion - 1)];
			entity.Codigo = (reader.IsDBNull(((int)PaqueteColumn.Codigo - 1)))?null:(System.String)reader[((int)PaqueteColumn.Codigo - 1)];
			entity.DestinoId = (System.Int32)reader[((int)PaqueteColumn.DestinoId - 1)];
			entity.PrecioSemiCama = (reader.IsDBNull(((int)PaqueteColumn.PrecioSemiCama - 1)))?null:(System.Double?)reader[((int)PaqueteColumn.PrecioSemiCama - 1)];
			entity.Foto = (reader.IsDBNull(((int)PaqueteColumn.Foto - 1)))?null:(System.String)reader[((int)PaqueteColumn.Foto - 1)];
			entity.ServiciosParticulares = (reader.IsDBNull(((int)PaqueteColumn.ServiciosParticulares - 1)))?null:(System.String)reader[((int)PaqueteColumn.ServiciosParticulares - 1)];
			entity.FechaCreacion = (reader.IsDBNull(((int)PaqueteColumn.FechaCreacion - 1)))?null:(System.DateTime?)reader[((int)PaqueteColumn.FechaCreacion - 1)];
			entity.PublicWeb = (System.Boolean)reader[((int)PaqueteColumn.PublicWeb - 1)];
			entity.LastUpdate = (System.DateTime)reader[((int)PaqueteColumn.LastUpdate - 1)];
			entity.ModePublicity = (reader.IsDBNull(((int)PaqueteColumn.ModePublicity - 1)))?null:(System.Boolean?)reader[((int)PaqueteColumn.ModePublicity - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Paquete"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Paquete"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Paquete entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PaqueteId = (System.Guid)dataRow["PaqueteID"];
			entity.OriginalPaqueteId = (System.Guid)dataRow["PaqueteID"];
			entity.Descripcion = (System.String)dataRow["Descripcion"];
			entity.PrecioCama = Convert.IsDBNull(dataRow["PrecioCama"]) ? null : (System.Double?)dataRow["PrecioCama"];
			entity.Moneda = Convert.IsDBNull(dataRow["Moneda"]) ? null : (System.Int32?)dataRow["Moneda"];
			entity.Iva = Convert.IsDBNull(dataRow["Iva"]) ? null : (System.String)dataRow["Iva"];
			entity.Alicuota = Convert.IsDBNull(dataRow["Alicuota"]) ? null : (System.String)dataRow["Alicuota"];
			entity.Temporada = Convert.IsDBNull(dataRow["Temporada"]) ? null : (System.Int32?)dataRow["Temporada"];
			entity.Cotizacion = Convert.IsDBNull(dataRow["Cotizacion"]) ? null : (System.Double?)dataRow["Cotizacion"];
			entity.Codigo = Convert.IsDBNull(dataRow["Codigo"]) ? null : (System.String)dataRow["Codigo"];
			entity.DestinoId = (System.Int32)dataRow["DestinoID"];
			entity.PrecioSemiCama = Convert.IsDBNull(dataRow["PrecioSemiCama"]) ? null : (System.Double?)dataRow["PrecioSemiCama"];
			entity.Foto = Convert.IsDBNull(dataRow["Foto"]) ? null : (System.String)dataRow["Foto"];
			entity.ServiciosParticulares = Convert.IsDBNull(dataRow["ServiciosParticulares"]) ? null : (System.String)dataRow["ServiciosParticulares"];
			entity.FechaCreacion = Convert.IsDBNull(dataRow["FechaCreacion"]) ? null : (System.DateTime?)dataRow["FechaCreacion"];
			entity.PublicWeb = (System.Boolean)dataRow["PublicWeb"];
			entity.LastUpdate = (System.DateTime)dataRow["LastUpdate"];
			entity.ModePublicity = Convert.IsDBNull(dataRow["ModePublicity"]) ? null : (System.Boolean?)dataRow["ModePublicity"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Paquete"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Paquete Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Paquete entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region DestinoIdSource	
			if (CanDeepLoad(entity, "Localidad|DestinoIdSource", deepLoadType, innerList) 
				&& entity.DestinoIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.DestinoId;
				Localidad tmpEntity = EntityManager.LocateEntity<Localidad>(EntityLocator.ConstructKeyFromPkItems(typeof(Localidad), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.DestinoIdSource = tmpEntity;
				else
					entity.DestinoIdSource = DataRepository.LocalidadProvider.GetById(transactionManager, entity.DestinoId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DestinoIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.DestinoIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.LocalidadProvider.DeepLoad(transactionManager, entity.DestinoIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion DestinoIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByPaqueteId methods when available
			
			#region PaquetePrecioCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PaquetePrecio>|PaquetePrecioCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaquetePrecioCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PaquetePrecioCollection = DataRepository.PaquetePrecioProvider.GetByPaqueteId(transactionManager, entity.PaqueteId);

				if (deep && entity.PaquetePrecioCollection.Count > 0)
				{
					deepHandles.Add("PaquetePrecioCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PaquetePrecio>) DataRepository.PaquetePrecioProvider.DeepLoad,
						new object[] { transactionManager, entity.PaquetePrecioCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PaqueteAdicionalCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PaqueteAdicional>|PaqueteAdicionalCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaqueteAdicionalCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PaqueteAdicionalCollection = DataRepository.PaqueteAdicionalProvider.GetByPaqueteId(transactionManager, entity.PaqueteId);

				if (deep && entity.PaqueteAdicionalCollection.Count > 0)
				{
					deepHandles.Add("PaqueteAdicionalCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PaqueteAdicional>) DataRepository.PaqueteAdicionalProvider.DeepLoad,
						new object[] { transactionManager, entity.PaqueteAdicionalCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PaqueteExcursionCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PaqueteExcursion>|PaqueteExcursionCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaqueteExcursionCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PaqueteExcursionCollection = DataRepository.PaqueteExcursionProvider.GetByPaqueteId(transactionManager, entity.PaqueteId);

				if (deep && entity.PaqueteExcursionCollection.Count > 0)
				{
					deepHandles.Add("PaqueteExcursionCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PaqueteExcursion>) DataRepository.PaqueteExcursionProvider.DeepLoad,
						new object[] { transactionManager, entity.PaqueteExcursionCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region ViajeCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Viaje>|ViajeCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ViajeCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ViajeCollection = DataRepository.ViajeProvider.GetByPaqueteId(transactionManager, entity.PaqueteId);

				if (deep && entity.ViajeCollection.Count > 0)
				{
					deepHandles.Add("ViajeCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Viaje>) DataRepository.ViajeProvider.DeepLoad,
						new object[] { transactionManager, entity.ViajeCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PaqueteServicioCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PaqueteServicio>|PaqueteServicioCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaqueteServicioCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PaqueteServicioCollection = DataRepository.PaqueteServicioProvider.GetByPaqueteId(transactionManager, entity.PaqueteId);

				if (deep && entity.PaqueteServicioCollection.Count > 0)
				{
					deepHandles.Add("PaqueteServicioCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PaqueteServicio>) DataRepository.PaqueteServicioProvider.DeepLoad,
						new object[] { transactionManager, entity.PaqueteServicioCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Paquete object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Paquete instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Paquete Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Paquete entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region DestinoIdSource
			if (CanDeepSave(entity, "Localidad|DestinoIdSource", deepSaveType, innerList) 
				&& entity.DestinoIdSource != null)
			{
				DataRepository.LocalidadProvider.Save(transactionManager, entity.DestinoIdSource);
				entity.DestinoId = entity.DestinoIdSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<PaquetePrecio>
				if (CanDeepSave(entity.PaquetePrecioCollection, "List<PaquetePrecio>|PaquetePrecioCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PaquetePrecio child in entity.PaquetePrecioCollection)
					{
						if(child.PaqueteIdSource != null)
						{
							child.PaqueteId = child.PaqueteIdSource.PaqueteId;
						}
						else
						{
							child.PaqueteId = entity.PaqueteId;
						}

					}

					if (entity.PaquetePrecioCollection.Count > 0 || entity.PaquetePrecioCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PaquetePrecioProvider.Save(transactionManager, entity.PaquetePrecioCollection);
						
						deepHandles.Add("PaquetePrecioCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PaquetePrecio >) DataRepository.PaquetePrecioProvider.DeepSave,
							new object[] { transactionManager, entity.PaquetePrecioCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<PaqueteAdicional>
				if (CanDeepSave(entity.PaqueteAdicionalCollection, "List<PaqueteAdicional>|PaqueteAdicionalCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PaqueteAdicional child in entity.PaqueteAdicionalCollection)
					{
						if(child.PaqueteIdSource != null)
						{
							child.PaqueteId = child.PaqueteIdSource.PaqueteId;
						}
						else
						{
							child.PaqueteId = entity.PaqueteId;
						}

					}

					if (entity.PaqueteAdicionalCollection.Count > 0 || entity.PaqueteAdicionalCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PaqueteAdicionalProvider.Save(transactionManager, entity.PaqueteAdicionalCollection);
						
						deepHandles.Add("PaqueteAdicionalCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PaqueteAdicional >) DataRepository.PaqueteAdicionalProvider.DeepSave,
							new object[] { transactionManager, entity.PaqueteAdicionalCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<PaqueteExcursion>
				if (CanDeepSave(entity.PaqueteExcursionCollection, "List<PaqueteExcursion>|PaqueteExcursionCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PaqueteExcursion child in entity.PaqueteExcursionCollection)
					{
						if(child.PaqueteIdSource != null)
						{
							child.PaqueteId = child.PaqueteIdSource.PaqueteId;
						}
						else
						{
							child.PaqueteId = entity.PaqueteId;
						}

					}

					if (entity.PaqueteExcursionCollection.Count > 0 || entity.PaqueteExcursionCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PaqueteExcursionProvider.Save(transactionManager, entity.PaqueteExcursionCollection);
						
						deepHandles.Add("PaqueteExcursionCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PaqueteExcursion >) DataRepository.PaqueteExcursionProvider.DeepSave,
							new object[] { transactionManager, entity.PaqueteExcursionCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Viaje>
				if (CanDeepSave(entity.ViajeCollection, "List<Viaje>|ViajeCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Viaje child in entity.ViajeCollection)
					{
						if(child.PaqueteIdSource != null)
						{
							child.PaqueteId = child.PaqueteIdSource.PaqueteId;
						}
						else
						{
							child.PaqueteId = entity.PaqueteId;
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
				
	
			#region List<PaqueteServicio>
				if (CanDeepSave(entity.PaqueteServicioCollection, "List<PaqueteServicio>|PaqueteServicioCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PaqueteServicio child in entity.PaqueteServicioCollection)
					{
						if(child.PaqueteIdSource != null)
						{
							child.PaqueteId = child.PaqueteIdSource.PaqueteId;
						}
						else
						{
							child.PaqueteId = entity.PaqueteId;
						}

					}

					if (entity.PaqueteServicioCollection.Count > 0 || entity.PaqueteServicioCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PaqueteServicioProvider.Save(transactionManager, entity.PaqueteServicioCollection);
						
						deepHandles.Add("PaqueteServicioCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< PaqueteServicio >) DataRepository.PaqueteServicioProvider.DeepSave,
							new object[] { transactionManager, entity.PaqueteServicioCollection, deepSaveType, childTypes, innerList }
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
	
	#region PaqueteChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Paquete</c>
	///</summary>
	public enum PaqueteChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Localidad</c> at DestinoIdSource
		///</summary>
		[ChildEntityType(typeof(Localidad))]
		Localidad,
		///<summary>
		/// Collection of <c>Paquete</c> as OneToMany for PaquetePrecioCollection
		///</summary>
		[ChildEntityType(typeof(TList<PaquetePrecio>))]
		PaquetePrecioCollection,
		///<summary>
		/// Collection of <c>Paquete</c> as OneToMany for PaqueteAdicionalCollection
		///</summary>
		[ChildEntityType(typeof(TList<PaqueteAdicional>))]
		PaqueteAdicionalCollection,
		///<summary>
		/// Collection of <c>Paquete</c> as OneToMany for PaqueteExcursionCollection
		///</summary>
		[ChildEntityType(typeof(TList<PaqueteExcursion>))]
		PaqueteExcursionCollection,
		///<summary>
		/// Collection of <c>Paquete</c> as OneToMany for ViajeCollection
		///</summary>
		[ChildEntityType(typeof(TList<Viaje>))]
		ViajeCollection,
		///<summary>
		/// Collection of <c>Paquete</c> as OneToMany for PaqueteServicioCollection
		///</summary>
		[ChildEntityType(typeof(TList<PaqueteServicio>))]
		PaqueteServicioCollection,
	}
	
	#endregion PaqueteChildEntityTypes
	
	#region PaqueteFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PaqueteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Paquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteFilterBuilder : SqlFilterBuilder<PaqueteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteFilterBuilder class.
		/// </summary>
		public PaqueteFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteFilterBuilder
	
	#region PaqueteParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PaqueteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Paquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteParameterBuilder : ParameterizedSqlFilterBuilder<PaqueteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteParameterBuilder class.
		/// </summary>
		public PaqueteParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PaqueteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PaqueteParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PaqueteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PaqueteParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PaqueteParameterBuilder
	
	#region PaqueteSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PaqueteColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Paquete"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PaqueteSortBuilder : SqlSortBuilder<PaqueteColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteSqlSortBuilder class.
		/// </summary>
		public PaqueteSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PaqueteSortBuilder
	
} // end namespace
