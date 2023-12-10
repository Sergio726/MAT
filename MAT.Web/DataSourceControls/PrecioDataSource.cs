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
	/// Represents the DataRepository.PrecioProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PrecioDataSourceDesigner))]
	public class PrecioDataSource : ProviderDataSource<Precio, PrecioKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioDataSource class.
		/// </summary>
		public PrecioDataSource() : base(new PrecioService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PrecioDataSourceView used by the PrecioDataSource.
		/// </summary>
		protected PrecioDataSourceView PrecioView
		{
			get { return ( View as PrecioDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PrecioDataSource control invokes to retrieve data.
		/// </summary>
		public PrecioSelectMethod SelectMethod
		{
			get
			{
				PrecioSelectMethod selectMethod = PrecioSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PrecioSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PrecioDataSourceView class that is to be
		/// used by the PrecioDataSource.
		/// </summary>
		/// <returns>An instance of the PrecioDataSourceView class.</returns>
		protected override BaseDataSourceView<Precio, PrecioKey> GetNewDataSourceView()
		{
			return new PrecioDataSourceView(this, DefaultViewName);
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
	/// Supports the PrecioDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PrecioDataSourceView : ProviderDataSourceView<Precio, PrecioKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PrecioDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PrecioDataSourceView(PrecioDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PrecioDataSource PrecioOwner
		{
			get { return Owner as PrecioDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PrecioSelectMethod SelectMethod
		{
			get { return PrecioOwner.SelectMethod; }
			set { PrecioOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PrecioService PrecioProvider
		{
			get { return Provider as PrecioService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Precio> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Precio> results = null;
			Precio item;
			count = 0;
			
			System.Guid _precioId;

			switch ( SelectMethod )
			{
				case PrecioSelectMethod.Get:
					PrecioKey entityKey  = new PrecioKey();
					entityKey.Load(values);
					item = PrecioProvider.Get(entityKey);
					results = new TList<Precio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PrecioSelectMethod.GetAll:
                    results = PrecioProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PrecioSelectMethod.GetPaged:
					results = PrecioProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PrecioSelectMethod.Find:
					if ( FilterParameters != null )
						results = PrecioProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PrecioProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PrecioSelectMethod.GetByPrecioId:
					_precioId = ( values["PrecioId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PrecioId"], typeof(System.Guid)) : Guid.Empty;
					item = PrecioProvider.GetByPrecioId(_precioId);
					results = new TList<Precio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
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
			if ( SelectMethod == PrecioSelectMethod.Get || SelectMethod == PrecioSelectMethod.GetByPrecioId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Precio entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PrecioId == Guid.Empty )
				entity.PrecioId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Precio entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PrecioProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Precio> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PrecioProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PrecioDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PrecioDataSource class.
	/// </summary>
	public class PrecioDataSourceDesigner : ProviderDataSourceDesigner<Precio, PrecioKey>
	{
		/// <summary>
		/// Initializes a new instance of the PrecioDataSourceDesigner class.
		/// </summary>
		public PrecioDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PrecioSelectMethod SelectMethod
		{
			get { return ((PrecioDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PrecioDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PrecioDataSourceActionList

	/// <summary>
	/// Supports the PrecioDataSourceDesigner class.
	/// </summary>
	internal class PrecioDataSourceActionList : DesignerActionList
	{
		private PrecioDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PrecioDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PrecioDataSourceActionList(PrecioDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PrecioSelectMethod SelectMethod
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

	#endregion PrecioDataSourceActionList
	
	#endregion PrecioDataSourceDesigner
	
	#region PrecioSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PrecioDataSource.SelectMethod property.
	/// </summary>
	public enum PrecioSelectMethod
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
		/// Represents the GetByPrecioId method.
		/// </summary>
		GetByPrecioId
	}
	
	#endregion PrecioSelectMethod

	#region PrecioFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Precio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioFilter : SqlFilter<PrecioColumn>
	{
	}
	
	#endregion PrecioFilter

	#region PrecioExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Precio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioExpressionBuilder : SqlExpressionBuilder<PrecioColumn>
	{
	}
	
	#endregion PrecioExpressionBuilder	

	#region PrecioProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PrecioChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Precio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioProperty : ChildEntityProperty<PrecioChildEntityTypes>
	{
	}
	
	#endregion PrecioProperty
}

