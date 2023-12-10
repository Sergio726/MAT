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
	/// This class is the base class for any <see cref="VoucherProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class VoucherProviderBaseCore : EntityProviderBase<MAT.Entities.Voucher, MAT.Entities.VoucherKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.VoucherKey key)
		{
			return Delete(transactionManager, key.VoucherId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_voucherId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _voucherId)
		{
			return Delete(null, _voucherId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_voucherId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _voucherId);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Voucher_Vendedor key.
		///		FK_Voucher_Vendedor Description: 
		/// </summary>
		/// <param name="_vendedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Voucher objects.</returns>
		public TList<Voucher> GetByVendedorId(System.Guid? _vendedorId)
		{
			int count = -1;
			return GetByVendedorId(_vendedorId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Voucher_Vendedor key.
		///		FK_Voucher_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <returns>Returns a typed collection of MAT.Entities.Voucher objects.</returns>
		/// <remarks></remarks>
		public TList<Voucher> GetByVendedorId(TransactionManager transactionManager, System.Guid? _vendedorId)
		{
			int count = -1;
			return GetByVendedorId(transactionManager, _vendedorId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Voucher_Vendedor key.
		///		FK_Voucher_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Voucher objects.</returns>
		public TList<Voucher> GetByVendedorId(TransactionManager transactionManager, System.Guid? _vendedorId, int start, int pageLength)
		{
			int count = -1;
			return GetByVendedorId(transactionManager, _vendedorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Voucher_Vendedor key.
		///		fkVoucherVendedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_vendedorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Voucher objects.</returns>
		public TList<Voucher> GetByVendedorId(System.Guid? _vendedorId, int start, int pageLength)
		{
			int count =  -1;
			return GetByVendedorId(null, _vendedorId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Voucher_Vendedor key.
		///		fkVoucherVendedor Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_vendedorId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MAT.Entities.Voucher objects.</returns>
		public TList<Voucher> GetByVendedorId(System.Guid? _vendedorId, int start, int pageLength,out int count)
		{
			return GetByVendedorId(null, _vendedorId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Voucher_Vendedor key.
		///		FK_Voucher_Vendedor Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_vendedorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MAT.Entities.Voucher objects.</returns>
		public abstract TList<Voucher> GetByVendedorId(TransactionManager transactionManager, System.Guid? _vendedorId, int start, int pageLength, out int count);
		
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
		public override MAT.Entities.Voucher Get(TransactionManager transactionManager, MAT.Entities.VoucherKey key, int start, int pageLength)
		{
			return GetByVoucherId(transactionManager, key.VoucherId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Voucher index.
		/// </summary>
		/// <param name="_voucherId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Voucher"/> class.</returns>
		public MAT.Entities.Voucher GetByVoucherId(System.Guid _voucherId)
		{
			int count = -1;
			return GetByVoucherId(null,_voucherId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Voucher index.
		/// </summary>
		/// <param name="_voucherId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Voucher"/> class.</returns>
		public MAT.Entities.Voucher GetByVoucherId(System.Guid _voucherId, int start, int pageLength)
		{
			int count = -1;
			return GetByVoucherId(null, _voucherId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Voucher index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_voucherId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Voucher"/> class.</returns>
		public MAT.Entities.Voucher GetByVoucherId(TransactionManager transactionManager, System.Guid _voucherId)
		{
			int count = -1;
			return GetByVoucherId(transactionManager, _voucherId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Voucher index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_voucherId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Voucher"/> class.</returns>
		public MAT.Entities.Voucher GetByVoucherId(TransactionManager transactionManager, System.Guid _voucherId, int start, int pageLength)
		{
			int count = -1;
			return GetByVoucherId(transactionManager, _voucherId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Voucher index.
		/// </summary>
		/// <param name="_voucherId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Voucher"/> class.</returns>
		public MAT.Entities.Voucher GetByVoucherId(System.Guid _voucherId, int start, int pageLength, out int count)
		{
			return GetByVoucherId(null, _voucherId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Voucher index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_voucherId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Voucher"/> class.</returns>
		public abstract MAT.Entities.Voucher GetByVoucherId(TransactionManager transactionManager, System.Guid _voucherId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Voucher&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Voucher&gt;"/></returns>
		public static TList<Voucher> Fill(IDataReader reader, TList<Voucher> rows, int start, int pageLength)
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
				
				MAT.Entities.Voucher c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Voucher")
					.Append("|").Append((System.Guid)reader[((int)VoucherColumn.VoucherId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Voucher>(
					key.ToString(), // EntityTrackingKey
					"Voucher",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Voucher();
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
					c.VoucherId = (System.Guid)reader[((int)VoucherColumn.VoucherId - 1)];
					c.OriginalVoucherId = c.VoucherId;
					c.NroVoucher = (System.Int64)reader[((int)VoucherColumn.NroVoucher - 1)];
					c.FechaEmision = (reader.IsDBNull(((int)VoucherColumn.FechaEmision - 1)))?null:(System.DateTime?)reader[((int)VoucherColumn.FechaEmision - 1)];
					c.VendedorId = (reader.IsDBNull(((int)VoucherColumn.VendedorId - 1)))?null:(System.Guid?)reader[((int)VoucherColumn.VendedorId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Voucher"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Voucher"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Voucher entity)
		{
			if (!reader.Read()) return;
			
			entity.VoucherId = (System.Guid)reader[((int)VoucherColumn.VoucherId - 1)];
			entity.OriginalVoucherId = (System.Guid)reader["VoucherID"];
			entity.NroVoucher = (System.Int64)reader[((int)VoucherColumn.NroVoucher - 1)];
			entity.FechaEmision = (reader.IsDBNull(((int)VoucherColumn.FechaEmision - 1)))?null:(System.DateTime?)reader[((int)VoucherColumn.FechaEmision - 1)];
			entity.VendedorId = (reader.IsDBNull(((int)VoucherColumn.VendedorId - 1)))?null:(System.Guid?)reader[((int)VoucherColumn.VendedorId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Voucher"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Voucher"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Voucher entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.VoucherId = (System.Guid)dataRow["VoucherID"];
			entity.OriginalVoucherId = (System.Guid)dataRow["VoucherID"];
			entity.NroVoucher = (System.Int64)dataRow["NroVoucher"];
			entity.FechaEmision = Convert.IsDBNull(dataRow["FechaEmision"]) ? null : (System.DateTime?)dataRow["FechaEmision"];
			entity.VendedorId = Convert.IsDBNull(dataRow["VendedorID"]) ? null : (System.Guid?)dataRow["VendedorID"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Voucher"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Voucher Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Voucher entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region VendedorIdSource	
			if (CanDeepLoad(entity, "Vendedor|VendedorIdSource", deepLoadType, innerList) 
				&& entity.VendedorIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.VendedorId ?? Guid.Empty);
				Vendedor tmpEntity = EntityManager.LocateEntity<Vendedor>(EntityLocator.ConstructKeyFromPkItems(typeof(Vendedor), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.VendedorIdSource = tmpEntity;
				else
					entity.VendedorIdSource = DataRepository.VendedorProvider.GetByVendedorId(transactionManager, (entity.VendedorId ?? Guid.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'VendedorIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.VendedorIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.VendedorProvider.DeepLoad(transactionManager, entity.VendedorIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion VendedorIdSource

			#region VoucherIdSource	
			if (CanDeepLoad(entity, "Voucher|VoucherIdSource", deepLoadType, innerList) 
				&& entity.VoucherIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.VoucherId;
				Voucher tmpEntity = EntityManager.LocateEntity<Voucher>(EntityLocator.ConstructKeyFromPkItems(typeof(Voucher), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.VoucherIdSource = tmpEntity;
				else
					entity.VoucherIdSource = DataRepository.VoucherProvider.GetByVoucherId(transactionManager, entity.VoucherId);		
				
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
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByVoucherId methods when available
			
			#region PasajeCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Pasaje>|PasajeCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PasajeCollection = DataRepository.PasajeProvider.GetByVoucherId(transactionManager, entity.VoucherId);

				if (deep && entity.PasajeCollection.Count > 0)
				{
					deepHandles.Add("PasajeCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Pasaje>) DataRepository.PasajeProvider.DeepLoad,
						new object[] { transactionManager, entity.PasajeCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region Voucher
			// RelationshipType.OneToOne
			if (CanDeepLoad(entity, "Voucher|Voucher", deepLoadType, innerList))
			{
				entity.Voucher = DataRepository.VoucherProvider.GetByVoucherId(transactionManager, entity.VoucherId);
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'Voucher' loaded. key " + entity.EntityTrackingKey);
				#endif 

				if (deep && entity.Voucher != null)
				{
					deepHandles.Add("Voucher",
						new KeyValuePair<Delegate, object>((DeepLoadSingleHandle< Voucher >) DataRepository.VoucherProvider.DeepLoad,
						new object[] { transactionManager, entity.Voucher, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Voucher object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Voucher instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Voucher Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Voucher entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region VendedorIdSource
			if (CanDeepSave(entity, "Vendedor|VendedorIdSource", deepSaveType, innerList) 
				&& entity.VendedorIdSource != null)
			{
				DataRepository.VendedorProvider.Save(transactionManager, entity.VendedorIdSource);
				entity.VendedorId = entity.VendedorIdSource.VendedorId;
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
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();

			#region Voucher
			if (CanDeepSave(entity.Voucher, "Voucher|Voucher", deepSaveType, innerList))
			{

				if (entity.Voucher != null)
				{
					// update each child parent id with the real parent id (mostly used on insert)

					entity.Voucher.VoucherId = entity.VoucherId;
					//DataRepository.VoucherProvider.Save(transactionManager, entity.Voucher);
					deepHandles.Add("Voucher",
						new KeyValuePair<Delegate, object>((DeepSaveSingleHandle< Voucher >) DataRepository.VoucherProvider.DeepSave,
						new object[] { transactionManager, entity.Voucher, deepSaveType, childTypes, innerList }
					));
				}
			} 
			#endregion 
	
			#region List<Pasaje>
				if (CanDeepSave(entity.PasajeCollection, "List<Pasaje>|PasajeCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Pasaje child in entity.PasajeCollection)
					{
						if(child.VoucherIdSource != null)
						{
							child.VoucherId = child.VoucherIdSource.VoucherId;
						}
						else
						{
							child.VoucherId = entity.VoucherId;
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
	
	#region VoucherChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Voucher</c>
	///</summary>
	public enum VoucherChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Vendedor</c> at VendedorIdSource
		///</summary>
		[ChildEntityType(typeof(Vendedor))]
		Vendedor,
		
		///<summary>
		/// Composite Property for <c>Voucher</c> at VoucherIdSource
		///</summary>
		[ChildEntityType(typeof(Voucher))]
		Voucher,
		///<summary>
		/// Collection of <c>Voucher</c> as OneToMany for PasajeCollection
		///</summary>
		[ChildEntityType(typeof(TList<Pasaje>))]
		PasajeCollection,
	}
	
	#endregion VoucherChildEntityTypes
	
	#region VoucherFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;VoucherColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Voucher"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VoucherFilterBuilder : SqlFilterBuilder<VoucherColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VoucherFilterBuilder class.
		/// </summary>
		public VoucherFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VoucherFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VoucherFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VoucherFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VoucherFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VoucherFilterBuilder
	
	#region VoucherParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;VoucherColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Voucher"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VoucherParameterBuilder : ParameterizedSqlFilterBuilder<VoucherColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VoucherParameterBuilder class.
		/// </summary>
		public VoucherParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VoucherParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VoucherParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VoucherParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VoucherParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VoucherParameterBuilder
	
	#region VoucherSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;VoucherColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Voucher"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class VoucherSortBuilder : SqlSortBuilder<VoucherColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VoucherSqlSortBuilder class.
		/// </summary>
		public VoucherSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion VoucherSortBuilder
	
} // end namespace
