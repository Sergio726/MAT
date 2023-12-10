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
	/// This class is the base class for any <see cref="LocalidadProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class LocalidadProviderBaseCore : EntityProviderBase<MAT.Entities.Localidad, MAT.Entities.LocalidadKey>
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
		public override bool Delete(TransactionManager transactionManager, MAT.Entities.LocalidadKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _id)
		{
			return Delete(null, _id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _id);		
		
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
		public override MAT.Entities.Localidad Get(TransactionManager transactionManager, MAT.Entities.LocalidadKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Localidad index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Localidad"/> class.</returns>
		public MAT.Entities.Localidad GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Localidad index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Localidad"/> class.</returns>
		public MAT.Entities.Localidad GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Localidad index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Localidad"/> class.</returns>
		public MAT.Entities.Localidad GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Localidad index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Localidad"/> class.</returns>
		public MAT.Entities.Localidad GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Localidad index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Localidad"/> class.</returns>
		public MAT.Entities.Localidad GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Localidad index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MAT.Entities.Localidad"/> class.</returns>
		public abstract MAT.Entities.Localidad GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Localidad&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Localidad&gt;"/></returns>
		public static TList<Localidad> Fill(IDataReader reader, TList<Localidad> rows, int start, int pageLength)
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
				
				MAT.Entities.Localidad c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Localidad")
					.Append("|").Append((System.Int32)reader[((int)LocalidadColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Localidad>(
					key.ToString(), // EntityTrackingKey
					"Localidad",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MAT.Entities.Localidad();
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
					c.Id = (System.Int32)reader[((int)LocalidadColumn.Id - 1)];
					c.IdDepartamento = (System.Int32)reader[((int)LocalidadColumn.IdDepartamento - 1)];
					c.Nombre = (System.String)reader[((int)LocalidadColumn.Nombre - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Localidad"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Localidad"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MAT.Entities.Localidad entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)LocalidadColumn.Id - 1)];
			entity.IdDepartamento = (System.Int32)reader[((int)LocalidadColumn.IdDepartamento - 1)];
			entity.Nombre = (System.String)reader[((int)LocalidadColumn.Nombre - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MAT.Entities.Localidad"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MAT.Entities.Localidad"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MAT.Entities.Localidad entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["ID"];
			entity.IdDepartamento = (System.Int32)dataRow["idDepartamento"];
			entity.Nombre = (System.String)dataRow["Nombre"];
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
		/// <param name="entity">The <see cref="MAT.Entities.Localidad"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MAT.Entities.Localidad Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MAT.Entities.Localidad entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region HotelCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Hotel>|HotelCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'HotelCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.HotelCollection = DataRepository.HotelProvider.GetByLocalidadId(transactionManager, entity.Id);

				if (deep && entity.HotelCollection.Count > 0)
				{
					deepHandles.Add("HotelCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Hotel>) DataRepository.HotelProvider.DeepLoad,
						new object[] { transactionManager, entity.HotelCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region PaqueteCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Paquete>|PaqueteCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PaqueteCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PaqueteCollection = DataRepository.PaqueteProvider.GetByDestinoId(transactionManager, entity.Id);

				if (deep && entity.PaqueteCollection.Count > 0)
				{
					deepHandles.Add("PaqueteCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Paquete>) DataRepository.PaqueteProvider.DeepLoad,
						new object[] { transactionManager, entity.PaqueteCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the MAT.Entities.Localidad object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MAT.Entities.Localidad instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MAT.Entities.Localidad Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MAT.Entities.Localidad entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<Hotel>
				if (CanDeepSave(entity.HotelCollection, "List<Hotel>|HotelCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Hotel child in entity.HotelCollection)
					{
						if(child.LocalidadIdSource != null)
						{
							child.LocalidadId = child.LocalidadIdSource.Id;
						}
						else
						{
							child.LocalidadId = entity.Id;
						}

					}

					if (entity.HotelCollection.Count > 0 || entity.HotelCollection.DeletedItems.Count > 0)
					{
						//DataRepository.HotelProvider.Save(transactionManager, entity.HotelCollection);
						
						deepHandles.Add("HotelCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Hotel >) DataRepository.HotelProvider.DeepSave,
							new object[] { transactionManager, entity.HotelCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Paquete>
				if (CanDeepSave(entity.PaqueteCollection, "List<Paquete>|PaqueteCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Paquete child in entity.PaqueteCollection)
					{
						if(child.DestinoIdSource != null)
						{
							child.DestinoId = child.DestinoIdSource.Id;
						}
						else
						{
							child.DestinoId = entity.Id;
						}

					}

					if (entity.PaqueteCollection.Count > 0 || entity.PaqueteCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PaqueteProvider.Save(transactionManager, entity.PaqueteCollection);
						
						deepHandles.Add("PaqueteCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Paquete >) DataRepository.PaqueteProvider.DeepSave,
							new object[] { transactionManager, entity.PaqueteCollection, deepSaveType, childTypes, innerList }
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
	
	#region LocalidadChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MAT.Entities.Localidad</c>
	///</summary>
	public enum LocalidadChildEntityTypes
	{
		///<summary>
		/// Collection of <c>Localidad</c> as OneToMany for HotelCollection
		///</summary>
		[ChildEntityType(typeof(TList<Hotel>))]
		HotelCollection,
		///<summary>
		/// Collection of <c>Localidad</c> as OneToMany for PaqueteCollection
		///</summary>
		[ChildEntityType(typeof(TList<Paquete>))]
		PaqueteCollection,
	}
	
	#endregion LocalidadChildEntityTypes
	
	#region LocalidadFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;LocalidadColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Localidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class LocalidadFilterBuilder : SqlFilterBuilder<LocalidadColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LocalidadFilterBuilder class.
		/// </summary>
		public LocalidadFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the LocalidadFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public LocalidadFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the LocalidadFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public LocalidadFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion LocalidadFilterBuilder
	
	#region LocalidadParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;LocalidadColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Localidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class LocalidadParameterBuilder : ParameterizedSqlFilterBuilder<LocalidadColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LocalidadParameterBuilder class.
		/// </summary>
		public LocalidadParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the LocalidadParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public LocalidadParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the LocalidadParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public LocalidadParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion LocalidadParameterBuilder
	
	#region LocalidadSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;LocalidadColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Localidad"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class LocalidadSortBuilder : SqlSortBuilder<LocalidadColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LocalidadSqlSortBuilder class.
		/// </summary>
		public LocalidadSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion LocalidadSortBuilder
	
} // end namespace
