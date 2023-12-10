#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.Design;

using MAT.Entities;
using MAT.Data;
using MAT.Data.Bases;
using MAT.Services;
#endregion

namespace MAT.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.ServicioProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ServicioDataSourceDesigner))]
	public class ServicioDataSource : ProviderDataSource<Servicio, ServicioKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicioDataSource class.
		/// </summary>
		public ServicioDataSource() : base(new ServicioService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ServicioDataSourceView used by the ServicioDataSource.
		/// </summary>
		protected ServicioDataSourceView ServicioView
		{
			get { return ( View as ServicioDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ServicioDataSource control invokes to retrieve data.
		/// </summary>
		public ServicioSelectMethod SelectMethod
		{
			get
			{
				ServicioSelectMethod selectMethod = ServicioSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ServicioSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ServicioDataSourceView class that is to be
		/// used by the ServicioDataSource.
		/// </summary>
		/// <returns>An instance of the ServicioDataSourceView class.</returns>
		protected override BaseDataSourceView<Servicio, ServicioKey> GetNewDataSourceView()
		{
			return new ServicioDataSourceView(this, DefaultViewName);
		}
		
		/// <summary>
        /// Creates a cache hashing key based on the startIndex, pageSize and the SelectMethod being used.
        /// </summary>
        /// <param name="startIndex">The current start row index.</param>
        /// <param name="pageSize">The current page size.</param>
        /// <returns>A string that can be used as a key for caching purposes.</returns>
		protected override string CacheHashKey(int startIndex, int pageSize)
        {
			return String.Format("{0}:{1}:{2}", SelectMethod, startIndex, pageSize);
        }
		
		#endregion Methods
	}
	
	/// <summary>
	/// Supports the ServicioDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ServicioDataSourceView : ProviderDataSourceView<Servicio, ServicioKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicioDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ServicioDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ServicioDataSourceView(ServicioDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ServicioDataSource ServicioOwner
		{
			get { return Owner as ServicioDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ServicioSelectMethod SelectMethod
		{
			get { return ServicioOwner.SelectMethod; }
			set { ServicioOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ServicioService ServicioProvider
		{
			get { return Provider as ServicioService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Servicio> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Servicio> results = null;
			Servicio item;
			count = 0;
			
			System.Guid _servicioId;
			System.Guid? _hotelId_nullable;
			System.Guid? _proveedorId_nullable;
			System.Guid? _transporteId_nullable;

			switch ( SelectMethod )
			{
				case ServicioSelectMethod.Get:
					ServicioKey entityKey  = new ServicioKey();
					entityKey.Load(values);
					item = ServicioProvider.Get(entityKey);
					results = new TList<Servicio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ServicioSelectMethod.GetAll:
                    results = ServicioProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case ServicioSelectMethod.GetPaged:
					results = ServicioProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ServicioSelectMethod.Find:
					if ( FilterParameters != null )
						results = ServicioProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ServicioProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ServicioSelectMethod.GetByServicioId:
					_servicioId = ( values["ServicioId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ServicioId"], typeof(System.Guid)) : Guid.Empty;
					item = ServicioProvider.GetByServicioId(_servicioId);
					results = new TList<Servicio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case ServicioSelectMethod.GetByHotelId:
					_hotelId_nullable = (System.Guid?) EntityUtil.ChangeType(values["HotelId"], typeof(System.Guid?));
					results = ServicioProvider.GetByHotelId(_hotelId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case ServicioSelectMethod.GetByProveedorId:
					_proveedorId_nullable = (System.Guid?) EntityUtil.ChangeType(values["ProveedorId"], typeof(System.Guid?));
					results = ServicioProvider.GetByProveedorId(_proveedorId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case ServicioSelectMethod.GetByTransporteId:
					_transporteId_nullable = (System.Guid?) EntityUtil.ChangeType(values["TransporteId"], typeof(System.Guid?));
					results = ServicioProvider.GetByTransporteId(_transporteId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// M:M
				// Custom
				default:
					break;
			}

			if ( results != null && count < 1 )
			{
				count = results.Count;

				if ( !String.IsNullOrEmpty(CustomMethodRecordCountParamName) )
				{
					object objCustomCount = EntityUtil.ChangeType(customOutput[CustomMethodRecordCountParamName], typeof(Int32));
					
					if ( objCustomCount != null )
					{
						count = (int) objCustomCount;
					}
				}
			}
			
			return results;
		}
		
		/// <summary>
		/// Gets the values of any supplied parameters for internal caching.
		/// </summary>
		/// <param name="values">An IDictionary object of name/value pairs.</param>
		protected override void GetSelectParameters(IDictionary values)
		{
			if ( SelectMethod == ServicioSelectMethod.Get || SelectMethod == ServicioSelectMethod.GetByServicioId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Servicio entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.ServicioId == Guid.Empty )
				entity.ServicioId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Servicio entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					ServicioProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
					// set loaded flag
					IsDeepLoaded = true;
				}
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation on the specified entity collection.
		/// </summary>
		/// <param name="entityList"></param>
		/// <param name="properties"></param>
		internal override void DeepLoad(TList<Servicio> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			ServicioProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ServicioDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ServicioDataSource class.
	/// </summary>
	public class ServicioDataSourceDesigner : ProviderDataSourceDesigner<Servicio, ServicioKey>
	{
		/// <summary>
		/// Initializes a new instance of the ServicioDataSourceDesigner class.
		/// </summary>
		public ServicioDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ServicioSelectMethod SelectMethod
		{
			get { return ((ServicioDataSource) DataSource).SelectMethod; }
			set { SetPropertyValue("SelectMethod", value); }
		}

		/// <summary>Gets the designer action list collection for this designer.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection"/>
		/// associated with this designer.</returns>
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection actions = new DesignerActionListCollection();
				actions.Add(new ServicioDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ServicioDataSourceActionList

	/// <summary>
	/// Supports the ServicioDataSourceDesigner class.
	/// </summary>
	internal class ServicioDataSourceActionList : DesignerActionList
	{
		private ServicioDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ServicioDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ServicioDataSourceActionList(ServicioDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ServicioSelectMethod SelectMethod
		{
			get { return _designer.SelectMethod; }
			set { _designer.SelectMethod = value; }
		}

		/// <summary>
		/// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// objects contained in the list.
		/// </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// array that contains the items in this list.</returns>
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection items = new DesignerActionItemCollection();
			items.Add(new DesignerActionPropertyItem("SelectMethod", "Select Method", "Methods"));
			return items;
		}
	}

	#endregion ServicioDataSourceActionList
	
	#endregion ServicioDataSourceDesigner
	
	#region ServicioSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ServicioDataSource.SelectMethod property.
	/// </summary>
	public enum ServicioSelectMethod
	{
		/// <summary>
		/// Represents the Get method.
		/// </summary>
		Get,
		/// <summary>
		/// Represents the GetAll method.
		/// </summary>
		GetAll,
		/// <summary>
		/// Represents the GetPaged method.
		/// </summary>
		GetPaged,
		/// <summary>
		/// Represents the Find method.
		/// </summary>
		Find,
		/// <summary>
		/// Represents the GetByServicioId method.
		/// </summary>
		GetByServicioId,
		/// <summary>
		/// Represents the GetByHotelId method.
		/// </summary>
		GetByHotelId,
		/// <summary>
		/// Represents the GetByProveedorId method.
		/// </summary>
		GetByProveedorId,
		/// <summary>
		/// Represents the GetByTransporteId method.
		/// </summary>
		GetByTransporteId
	}
	
	#endregion ServicioSelectMethod

	#region ServicioFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Servicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicioFilter : SqlFilter<ServicioColumn>
	{
	}
	
	#endregion ServicioFilter

	#region ServicioExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Servicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicioExpressionBuilder : SqlExpressionBuilder<ServicioColumn>
	{
	}
	
	#endregion ServicioExpressionBuilder	

	#region ServicioProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ServicioChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Servicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicioProperty : ChildEntityProperty<ServicioChildEntityTypes>
	{
	}
	
	#endregion ServicioProperty
}

