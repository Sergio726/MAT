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
	/// This class is the base class for any <see cref="VendedorProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class VendedorProviderBaseCore : EntityProviderBase<MAT.Entities.Vendedor, MAT.Entities.VendedorKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.VendedorKey key)
		{
			return Delete(transactionManager, key.VendedorId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_vendedorId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _vendedorId)
		{
			return Delete(null, _vendedorId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _vendedorId);		
		
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
		public override MAT.Entities.Vendedor Get(TransactionManager transactionManager, MAT.Entities.VendedorKey key, int start, int pageLength)
		{
			return GetByVendedorId(transactionManager, key.VendedorId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK__Vendedor__2033EECC1832F9CA index.
		/// </summary>
		/// <param name="_vendedorId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Vendedor"/> class.</returns>
		public MAT.Entities.Vendedor GetByVendedorId(System.Guid _vendedorId)
		{
			int count = -1;
			return GetByVendedorId(null,_vendedorId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__Vendedor__2033EECC1832F9CA index.
		/// </summary>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Vendedor"/> class.</returns>
		public MAT.Entities.Vendedor GetByVendedorId(System.Guid _vendedorId, int start, int pageLength)
		{
			int count = -1;
			return GetByVendedorId(null, _vendedorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__Vendedor__2033EECC1832F9CA index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Vendedor"/> class.</returns>
		public MAT.Entities.Vendedor GetByVendedorId(TransactionManager transactionManager, System.Guid _vendedorId)
		{
			int count = -1;
			return GetByVendedorId(transactionManager, _vendedorId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__Vendedor__2033EECC1832F9CA index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Vendedor"/> class.</returns>
		public MAT.Entities.Vendedor GetByVendedorId(TransactionManager transactionManager, System.Guid _vendedorId, int start, int pageLength)
		{
			int count = -1;
			return GetByVendedorId(transactionManager, _vendedorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__Vendedor__2033EECC1832F9CA index.
		/// </summary>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Vendedor"/> class.</returns>
		public MAT.Entities.Vendedor GetByVendedorId(System.Guid _vendedorId, int start, int pageLength, out int count)
		{
			return GetByVendedorId(null, _vendedorId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK__Vendedor__2033EECC1832F9CA index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Vendedor"/> class.</returns>
		public abstract MAT.Entities.Vendedor GetByVendedorId(TransactionManager transactionManager, System.Guid _vendedorId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Vendedor&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Vendedor&gt;"/></returns>
		public static TList<Vendedor> Fill(IDataReader reader, TList<Vendedor> rows, int start, int pageLength)
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
				
				MAT.Entities.Vendedor c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Vendedor")
					.Append("|").Append((System.Guid)reader[((int)VendedorColumn.VendedorId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Vendedor>(
					key.ToString(), // EntityTrackingKey
					"Vendedor",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Vendedor();
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
					c.VendedorId = (System.Guid)reader[((int)VendedorColumn.VendedorId - 1)];
					c.OriginalVendedorId = c.VendedorId;
					c.Descripcion = (reader.IsDBNull(((int)VendedorColumn.Descripcion - 1)))?null:(System.String)reader[((int)VendedorColumn.Descripcion - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Vendedor"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Vendedor"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Vendedor entity)
		{
			if (!reader.Read()) return;
			
			entity.VendedorId = (System.Guid)reader[((int)VendedorColumn.VendedorId - 1)];
			entity.OriginalVendedorId = (System.Guid)reader["VendedorID"];
			entity.Descripcion = (reader.IsDBNull(((int)VendedorColumn.Descripcion - 1)))?null:(System.String)reader[((int)VendedorColumn.Descripcion - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Vendedor"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Vendedor"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Vendedor entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.VendedorId = (System.Guid)dataRow["VendedorID"];
			entity.OriginalVendedorId = (System.Guid)dataRow["VendedorID"];
			entity.Descripcion = Convert.IsDBNull(dataRow["Descripcion"]) ? null : (System.String)dataRow["Descripcion"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Vendedor"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Vendedor Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Vendedor entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region VendedorIdSource	
			if (CanDeepLoad(entity, "Persona|VendedorIdSource", deepLoadType, innerList) 
				&& entity.VendedorIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.VendedorId;
				Persona tmpEntity = EntityManager.LocateEntity<Persona>(EntityLocator.ConstructKeyFromPkItems(typeof(Persona), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.VendedorIdSource = tmpEntity;
				else
					entity.VendedorIdSource = DataRepository.PersonaProvider.GetByPersonaId(transactionManager, entity.VendedorId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'VendedorIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.VendedorIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PersonaProvider.DeepLoad(transactionManager, entity.VendedorIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion VendedorIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByVendedorId methods when available
			
			#region FacturaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Factura>|FacturaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'FacturaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.FacturaCollection = DataRepository.FacturaProvider.GetByVendedorId(transactionManager, entity.VendedorId);

				if (deep && entity.FacturaCollection.Count > 0)
				{
					deepHandles.Add("FacturaCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Factura>) DataRepository.FacturaProvider.DeepLoad,
						new object[] { transactionManager, entity.FacturaCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region NotaCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Nota>|NotaCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'NotaCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.NotaCollection = DataRepository.NotaProvider.GetByVendedorId(transactionManager, entity.VendedorId);

				if (deep && entity.NotaCollection.Count > 0)
				{
					deepHandles.Add("NotaCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Nota>) DataRepository.NotaProvider.DeepLoad,
						new object[] { transactionManager, entity.NotaCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PagoCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Pago>|PagoCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PagoCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PagoCollection = DataRepository.PagoProvider.GetByVendedorId(transactionManager, entity.VendedorId);

				if (deep && entity.PagoCollection.Count > 0)
				{
					deepHandles.Add("PagoCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Pago>) DataRepository.PagoProvider.DeepLoad,
						new object[] { transactionManager, entity.PagoCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region VoucherCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Voucher>|VoucherCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'VoucherCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.VoucherCollection = DataRepository.VoucherProvider.GetByVendedorId(transactionManager, entity.VendedorId);

				if (deep && entity.VoucherCollection.Count > 0)
				{
					deepHandles.Add("VoucherCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Voucher>) DataRepository.VoucherProvider.DeepLoad,
						new object[] { transactionManager, entity.VoucherCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Vendedor object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Vendedor instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Vendedor Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Vendedor entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region VendedorIdSource
			if (CanDeepSave(entity, "Persona|VendedorIdSource", deepSaveType, innerList) 
				&& entity.VendedorIdSource != null)
			{
				DataRepository.PersonaProvider.Save(transactionManager, entity.VendedorIdSource);
				entity.VendedorId = entity.VendedorIdSource.PersonaId;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<Factura>
				if (CanDeepSave(entity.FacturaCollection, "List<Factura>|FacturaCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Factura child in entity.FacturaCollection)
					{
						if(child.VendedorIdSource != null)
						{
							child.VendedorId = child.VendedorIdSource.VendedorId;
						}
						else
						{
							child.VendedorId = entity.VendedorId;
						}

					}

					if (entity.FacturaCollection.Count > 0 || entity.FacturaCollection.DeletedItems.Count > 0)
					{
						//DataRepository.FacturaProvider.Save(transactionManager, entity.FacturaCollection);
						
						deepHandles.Add("FacturaCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Factura >) DataRepository.FacturaProvider.DeepSave,
							new object[] { transactionManager, entity.FacturaCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Nota>
				if (CanDeepSave(entity.NotaCollection, "List<Nota>|NotaCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Nota child in entity.NotaCollection)
					{
						if(child.VendedorIdSource != null)
						{
							child.VendedorId = child.VendedorIdSource.VendedorId;
						}
						else
						{
							child.VendedorId = entity.VendedorId;
						}

					}

					if (entity.NotaCollection.Count > 0 || entity.NotaCollection.DeletedItems.Count > 0)
					{
						//DataRepository.NotaProvider.Save(transactionManager, entity.NotaCollection);
						
						deepHandles.Add("NotaCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Nota >) DataRepository.NotaProvider.DeepSave,
							new object[] { transactionManager, entity.NotaCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Pago>
				if (CanDeepSave(entity.PagoCollection, "List<Pago>|PagoCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Pago child in entity.PagoCollection)
					{
						if(child.VendedorIdSource != null)
						{
							child.VendedorId = child.VendedorIdSource.VendedorId;
						}
						else
						{
							child.VendedorId = entity.VendedorId;
						}

					}

					if (entity.PagoCollection.Count > 0 || entity.PagoCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PagoProvider.Save(transactionManager, entity.PagoCollection);
						
						deepHandles.Add("PagoCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Pago >) DataRepository.PagoProvider.DeepSave,
							new object[] { transactionManager, entity.PagoCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Voucher>
				if (CanDeepSave(entity.VoucherCollection, "List<Voucher>|VoucherCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Voucher child in entity.VoucherCollection)
					{
						if(child.VendedorIdSource != null)
						{
							child.VendedorId = child.VendedorIdSource.VendedorId;
						}
						else
						{
							child.VendedorId = entity.VendedorId;
						}

					}

					if (entity.VoucherCollection.Count > 0 || entity.VoucherCollection.DeletedItems.Count > 0)
					{
						//DataRepository.VoucherProvider.Save(transactionManager, entity.VoucherCollection);
						
						deepHandles.Add("VoucherCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Voucher >) DataRepository.VoucherProvider.DeepSave,
							new object[] { transactionManager, entity.VoucherCollection, deepSaveType, childTypes, innerList }
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
	
	#region VendedorChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Vendedor</c>
	///</summary>
	public enum VendedorChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Persona</c> at VendedorIdSource
		///</summary>
		[ChildEntityType(typeof(Persona))]
		Persona,
		///<summary>
		/// Collection of <c>Vendedor</c> as OneToMany for FacturaCollection
		///</summary>
		[ChildEntityType(typeof(TList<Factura>))]
		FacturaCollection,
		///<summary>
		/// Collection of <c>Vendedor</c> as OneToMany for NotaCollection
		///</summary>
		[ChildEntityType(typeof(TList<Nota>))]
		NotaCollection,
		///<summary>
		/// Collection of <c>Vendedor</c> as OneToMany for PagoCollection
		///</summary>
		[ChildEntityType(typeof(TList<Pago>))]
		PagoCollection,
		///<summary>
		/// Collection of <c>Vendedor</c> as OneToMany for VoucherCollection
		///</summary>
		[ChildEntityType(typeof(TList<Voucher>))]
		VoucherCollection,
	}
	
	#endregion VendedorChildEntityTypes
	
	#region VendedorFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;VendedorColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Vendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VendedorFilterBuilder : SqlFilterBuilder<VendedorColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VendedorFilterBuilder class.
		/// </summary>
		public VendedorFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VendedorFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VendedorFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VendedorFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VendedorFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VendedorFilterBuilder
	
	#region VendedorParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;VendedorColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Vendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VendedorParameterBuilder : ParameterizedSqlFilterBuilder<VendedorColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VendedorParameterBuilder class.
		/// </summary>
		public VendedorParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VendedorParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VendedorParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VendedorParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VendedorParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VendedorParameterBuilder
	
	#region VendedorSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;VendedorColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Vendedor"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class VendedorSortBuilder : SqlSortBuilder<VendedorColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VendedorSqlSortBuilder class.
		/// </summary>
		public VendedorSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion VendedorSortBuilder
	
} // end namespace
