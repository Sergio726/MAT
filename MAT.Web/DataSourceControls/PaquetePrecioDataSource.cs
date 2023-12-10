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
	/// Represents the DataRepository.PaquetePrecioProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PaquetePrecioDataSourceDesigner))]
	public class PaquetePrecioDataSource : ProviderDataSource<PaquetePrecio, PaquetePrecioKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioDataSource class.
		/// </summary>
		public PaquetePrecioDataSource() : base(new PaquetePrecioService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PaquetePrecioDataSourceView used by the PaquetePrecioDataSource.
		/// </summary>
		protected PaquetePrecioDataSourceView PaquetePrecioView
		{
			get { return ( View as PaquetePrecioDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PaquetePrecioDataSource control invokes to retrieve data.
		/// </summary>
		public PaquetePrecioSelectMethod SelectMethod
		{
			get
			{
				PaquetePrecioSelectMethod selectMethod = PaquetePrecioSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PaquetePrecioSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PaquetePrecioDataSourceView class that is to be
		/// used by the PaquetePrecioDataSource.
		/// </summary>
		/// <returns>An instance of the PaquetePrecioDataSourceView class.</returns>
		protected override BaseDataSourceView<PaquetePrecio, PaquetePrecioKey> GetNewDataSourceView()
		{
			return new PaquetePrecioDataSourceView(this, DefaultViewName);
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
	/// Supports the PaquetePrecioDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PaquetePrecioDataSourceView : ProviderDataSourceView<PaquetePrecio, PaquetePrecioKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PaquetePrecioDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PaquetePrecioDataSourceView(PaquetePrecioDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PaquetePrecioDataSource PaquetePrecioOwner
		{
			get { return Owner as PaquetePrecioDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PaquetePrecioSelectMethod SelectMethod
		{
			get { return PaquetePrecioOwner.SelectMethod; }
			set { PaquetePrecioOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PaquetePrecioService PaquetePrecioProvider
		{
			get { return Provider as PaquetePrecioService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<PaquetePrecio> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<PaquetePrecio> results = null;
			PaquetePrecio item;
			count = 0;
			
			System.Guid _paquetePrecioId;
			System.Guid? _paqueteId_nullable;
			System.Guid? _precioId_nullable;

			switch ( SelectMethod )
			{
				case PaquetePrecioSelectMethod.Get:
					PaquetePrecioKey entityKey  = new PaquetePrecioKey();
					entityKey.Load(values);
					item = PaquetePrecioProvider.Get(entityKey);
					results = new TList<PaquetePrecio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PaquetePrecioSelectMethod.GetAll:
                    results = PaquetePrecioProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PaquetePrecioSelectMethod.GetPaged:
					results = PaquetePrecioProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PaquetePrecioSelectMethod.Find:
					if ( FilterParameters != null )
						results = PaquetePrecioProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PaquetePrecioProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PaquetePrecioSelectMethod.GetByPaquetePrecioId:
					_paquetePrecioId = ( values["PaquetePrecioId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PaquetePrecioId"], typeof(System.Guid)) : Guid.Empty;
					item = PaquetePrecioProvider.GetByPaquetePrecioId(_paquetePrecioId);
					results = new TList<PaquetePrecio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case PaquetePrecioSelectMethod.GetByPaqueteId:
					_paqueteId_nullable = (System.Guid?) EntityUtil.ChangeType(values["PaqueteId"], typeof(System.Guid?));
					results = PaquetePrecioProvider.GetByPaqueteId(_paqueteId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case PaquetePrecioSelectMethod.GetByPrecioId:
					_precioId_nullable = (System.Guid?) EntityUtil.ChangeType(values["PrecioId"], typeof(System.Guid?));
					results = PaquetePrecioProvider.GetByPrecioId(_precioId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == PaquetePrecioSelectMethod.Get || SelectMethod == PaquetePrecioSelectMethod.GetByPaquetePrecioId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(PaquetePrecio entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PaquetePrecioId == Guid.Empty )
				entity.PaquetePrecioId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				PaquetePrecio entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PaquetePrecioProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<PaquetePrecio> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PaquetePrecioProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PaquetePrecioDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PaquetePrecioDataSource class.
	/// </summary>
	public class PaquetePrecioDataSourceDesigner : ProviderDataSourceDesigner<PaquetePrecio, PaquetePrecioKey>
	{
		/// <summary>
		/// Initializes a new instance of the PaquetePrecioDataSourceDesigner class.
		/// </summary>
		public PaquetePrecioDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PaquetePrecioSelectMethod SelectMethod
		{
			get { return ((PaquetePrecioDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PaquetePrecioDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PaquetePrecioDataSourceActionList

	/// <summary>
	/// Supports the PaquetePrecioDataSourceDesigner class.
	/// </summary>
	internal class PaquetePrecioDataSourceActionList : DesignerActionList
	{
		private PaquetePrecioDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PaquetePrecioDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PaquetePrecioDataSourceActionList(PaquetePrecioDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PaquetePrecioSelectMethod SelectMethod
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

	#endregion PaquetePrecioDataSourceActionList
	
	#endregion PaquetePrecioDataSourceDesigner
	
	#region PaquetePrecioSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PaquetePrecioDataSource.SelectMethod property.
	/// </summary>
	public enum PaquetePrecioSelectMethod
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
		/// Represents the GetByPaquetePrecioId method.
		/// </summary>
		GetByPaquetePrecioId,
		/// <summary>
		/// Represents the GetByPaqueteId method.
		/// </summary>
		GetByPaqueteId,
		/// <summary>
		/// Represents the GetByPrecioId method.
		/// </summary>
		GetByPrecioId
	}
	
	#endregion PaquetePrecioSelectMethod

	#region PaquetePrecioFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaquetePrecio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaquetePrecioFilter : SqlFilter<PaquetePrecioColumn>
	{
	}
	
	#endregion PaquetePrecioFilter

	#region PaquetePrecioExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaquetePrecio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaquetePrecioExpressionBuilder : SqlExpressionBuilder<PaquetePrecioColumn>
	{
	}
	
	#endregion PaquetePrecioExpressionBuilder	

	#region PaquetePrecioProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PaquetePrecioChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="PaquetePrecio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaquetePrecioProperty : ChildEntityProperty<PaquetePrecioChildEntityTypes>
	{
	}
	
	#endregion PaquetePrecioProperty
}

