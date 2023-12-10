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
	/// This class is the base class for any <see cref="PrecioProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PrecioProviderBaseCore : EntityProviderBase<MAT.Entities.Precio, MAT.Entities.PrecioKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.PrecioKey key)
		{
			return Delete(transactionManager, key.PrecioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_precioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _precioId)
		{
			return Delete(null, _precioId);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioId">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _precioId);		
		
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
		public override MAT.Entities.Precio Get(TransactionManager transactionManager, MAT.Entities.PrecioKey key, int start, int pageLength)
		{
			return GetByPrecioId(transactionManager, key.PrecioId, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Precio index.
		/// </summary>
		/// <param name="_precioId"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Precio"/> class.</returns>
		public MAT.Entities.Precio GetByPrecioId(System.Guid _precioId)
		{
			int count = -1;
			return GetByPrecioId(null,_precioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Precio index.
		/// </summary>
		/// <param name="_precioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Precio"/> class.</returns>
		public MAT.Entities.Precio GetByPrecioId(System.Guid _precioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPrecioId(null, _precioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Precio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Precio"/> class.</returns>
		public MAT.Entities.Precio GetByPrecioId(TransactionManager transactionManager, System.Guid _precioId)
		{
			int count = -1;
			return GetByPrecioId(transactionManager, _precioId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Precio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Precio"/> class.</returns>
		public MAT.Entities.Precio GetByPrecioId(TransactionManager transactionManager, System.Guid _precioId, int start, int pageLength)
		{
			int count = -1;
			return GetByPrecioId(transactionManager, _precioId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Precio index.
		/// </summary>
		/// <param name="_precioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Precio"/> class.</returns>
		public MAT.Entities.Precio GetByPrecioId(System.Guid _precioId, int start, int pageLength, out int count)
		{
			return GetByPrecioId(null, _precioId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Precio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_precioId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Precio"/> class.</returns>
		public abstract MAT.Entities.Precio GetByPrecioId(TransactionManager transactionManager, System.Guid _precioId, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Precio&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Precio&gt;"/></returns>
		public static TList<Precio> Fill(IDataReader reader, TList<Precio> rows, int start, int pageLength)
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
				
				MAT.Entities.Precio c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Precio")
					.Append("|").Append((System.Guid)reader[((int)PrecioColumn.PrecioId - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Precio>(
					key.ToString(), // EntityTrackingKey
					"Precio",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Precio();
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
					c.PrecioId = (System.Guid)reader[((int)PrecioColumn.PrecioId - 1)];
					c.OriginalPrecioId = c.PrecioId;
					c.Monto = (System.Double)reader[((int)PrecioColumn.Monto - 1)];
					c.Vigencia = (reader.IsDBNull(((int)PrecioColumn.Vigencia - 1)))?null:(System.DateTime?)reader[((int)PrecioColumn.Vigencia - 1)];
					c.Descripcion = (System.String)reader[((int)PrecioColumn.Descripcion - 1)];
					c.Mes = (reader.IsDBNull(((int)PrecioColumn.Mes - 1)))?null:(System.String)reader[((int)PrecioColumn.Mes - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Precio"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Precio"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Precio entity)
		{
			if (!reader.Read()) return;
			
			entity.PrecioId = (System.Guid)reader[((int)PrecioColumn.PrecioId - 1)];
			entity.OriginalPrecioId = (System.Guid)reader["PrecioID"];
			entity.Monto = (System.Double)reader[((int)PrecioColumn.Monto - 1)];
			entity.Vigencia = (reader.IsDBNull(((int)PrecioColumn.Vigencia - 1)))?null:(System.DateTime?)reader[((int)PrecioColumn.Vigencia - 1)];
			entity.Descripcion = (System.String)reader[((int)PrecioColumn.Descripcion - 1)];
			entity.Mes = (reader.IsDBNull(((int)PrecioColumn.Mes - 1)))?null:(System.String)reader[((int)PrecioColumn.Mes - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Precio"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Precio"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Precio entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PrecioId = (System.Guid)dataRow["PrecioID"];
			entity.OriginalPrecioId = (System.Guid)dataRow["PrecioID"];
			entity.Monto = (System.Double)dataRow["Monto"];
			entity.Vigencia = Convert.IsDBNull(dataRow["Vigencia"]) ? null : (System.DateTime?)dataRow["Vigencia"];
			entity.Descripcion = (System.String)dataRow["Descripcion"];
			entity.Mes = Convert.IsDBNull(dataRow["Mes"]) ? null : (System.String)dataRow["Mes"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Precio"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Precio Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Precio entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByPrecioId methods when available
			
			#region PaquetePrecioCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<PaquetePrecio>|PaquetePrecioCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaquetePrecioCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PaquetePrecioCollection = DataRepository.PaquetePrecioProvider.GetByPrecioId(transactionManager, entity.PrecioId);

				if (deep && entity.PaquetePrecioCollection.Count > 0)
				{
					deepHandles.Add("PaquetePrecioCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<PaquetePrecio>) DataRepository.PaquetePrecioProvider.DeepLoad,
						new object[] { transactionManager, entity.PaquetePrecioCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PasajeCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Pasaje>|PasajeCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PasajeCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PasajeCollection = DataRepository.PasajeProvider.GetByPrecioId(transactionManager, entity.PrecioId);

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
		/// Deep Save the entire object graph of the MAT.Entities.Precio object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Precio instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Precio Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Precio entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<PaquetePrecio>
				if (CanDeepSave(entity.PaquetePrecioCollection, "List<PaquetePrecio>|PaquetePrecioCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(PaquetePrecio child in entity.PaquetePrecioCollection)
					{
						if(child.PrecioIdSource != null)
						{
							child.PrecioId = child.PrecioIdSource.PrecioId;
						}
						else
						{
							child.PrecioId = entity.PrecioId;
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
				
	
			#region List<Pasaje>
				if (CanDeepSave(entity.PasajeCollection, "List<Pasaje>|PasajeCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Pasaje child in entity.PasajeCollection)
					{
						if(child.PrecioIdSource != null)
						{
							child.PrecioId = child.PrecioIdSource.PrecioId;
						}
						else
						{
							child.PrecioId = entity.PrecioId;
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
	
	#region PrecioChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Precio</c>
	///</summary>
	public enum PrecioChildEntityTypes
	{
		///<summary>
		/// Collection of <c>Precio</c> as OneToMany for PaquetePrecioCollection
		///</summary>
		[ChildEntityType(typeof(TList<PaquetePrecio>))]
		PaquetePrecioCollection,
		///<summary>
		/// Collection of <c>Precio</c> as OneToMany for PasajeCollection
		///</summary>
		[ChildEntityType(typeof(TList<Pasaje>))]
		PasajeCollection,
	}
	
	#endregion PrecioChildEntityTypes
	
	#region PrecioFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PrecioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Precio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioFilterBuilder : SqlFilterBuilder<PrecioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioFilterBuilder class.
		/// </summary>
		public PrecioFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioFilterBuilder
	
	#region PrecioParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PrecioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Precio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioParameterBuilder : ParameterizedSqlFilterBuilder<PrecioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioParameterBuilder class.
		/// </summary>
		public PrecioParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PrecioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PrecioParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PrecioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PrecioParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PrecioParameterBuilder
	
	#region PrecioSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PrecioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Precio"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PrecioSortBuilder : SqlSortBuilder<PrecioColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioSqlSortBuilder class.
		/// </summary>
		public PrecioSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PrecioSortBuilder
	
} // end namespace
