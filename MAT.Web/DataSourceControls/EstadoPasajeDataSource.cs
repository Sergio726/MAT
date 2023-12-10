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
	/// Represents the DataRepository.EstadoPasajeProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(EstadoPasajeDataSourceDesigner))]
	public class EstadoPasajeDataSource : ProviderDataSource<EstadoPasaje, EstadoPasajeKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the EstadoPasajeDataSource class.
		/// </summary>
		public EstadoPasajeDataSource() : base(new EstadoPasajeService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the EstadoPasajeDataSourceView used by the EstadoPasajeDataSource.
		/// </summary>
		protected EstadoPasajeDataSourceView EstadoPasajeView
		{
			get { return ( View as EstadoPasajeDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the EstadoPasajeDataSource control invokes to retrieve data.
		/// </summary>
		public EstadoPasajeSelectMethod SelectMethod
		{
			get
			{
				EstadoPasajeSelectMethod selectMethod = EstadoPasajeSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (EstadoPasajeSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the EstadoPasajeDataSourceView class that is to be
		/// used by the EstadoPasajeDataSource.
		/// </summary>
		/// <returns>An instance of the EstadoPasajeDataSourceView class.</returns>
		protected override BaseDataSourceView<EstadoPasaje, EstadoPasajeKey> GetNewDataSourceView()
		{
			return new EstadoPasajeDataSourceView(this, DefaultViewName);
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
	/// Supports the EstadoPasajeDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class EstadoPasajeDataSourceView : ProviderDataSourceView<EstadoPasaje, EstadoPasajeKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the EstadoPasajeDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the EstadoPasajeDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public EstadoPasajeDataSourceView(EstadoPasajeDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal EstadoPasajeDataSource EstadoPasajeOwner
		{
			get { return Owner as EstadoPasajeDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal EstadoPasajeSelectMethod SelectMethod
		{
			get { return EstadoPasajeOwner.SelectMethod; }
			set { EstadoPasajeOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal EstadoPasajeService EstadoPasajeProvider
		{
			get { return Provider as EstadoPasajeService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<EstadoPasaje> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<EstadoPasaje> results = null;
			EstadoPasaje item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case EstadoPasajeSelectMethod.Get:
					EstadoPasajeKey entityKey  = new EstadoPasajeKey();
					entityKey.Load(values);
					item = EstadoPasajeProvider.Get(entityKey);
					results = new TList<EstadoPasaje>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case EstadoPasajeSelectMethod.GetAll:
                    results = EstadoPasajeProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case EstadoPasajeSelectMethod.GetPaged:
					results = EstadoPasajeProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case EstadoPasajeSelectMethod.Find:
					if ( FilterParameters != null )
						results = EstadoPasajeProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = EstadoPasajeProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case EstadoPasajeSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = EstadoPasajeProvider.GetById(_id);
					results = new TList<EstadoPasaje>();
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
			if ( SelectMethod == EstadoPasajeSelectMethod.Get || SelectMethod == EstadoPasajeSelectMethod.GetById )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				EstadoPasaje entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					EstadoPasajeProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<EstadoPasaje> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			EstadoPasajeProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region EstadoPasajeDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the EstadoPasajeDataSource class.
	/// </summary>
	public class EstadoPasajeDataSourceDesigner : ProviderDataSourceDesigner<EstadoPasaje, EstadoPasajeKey>
	{
		/// <summary>
		/// Initializes a new instance of the EstadoPasajeDataSourceDesigner class.
		/// </summary>
		public EstadoPasajeDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public EstadoPasajeSelectMethod SelectMethod
		{
			get { return ((EstadoPasajeDataSource) DataSource).SelectMethod; }
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
				actions.Add(new EstadoPasajeDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region EstadoPasajeDataSourceActionList

	/// <summary>
	/// Supports the EstadoPasajeDataSourceDesigner class.
	/// </summary>
	internal class EstadoPasajeDataSourceActionList : DesignerActionList
	{
		private EstadoPasajeDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the EstadoPasajeDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public EstadoPasajeDataSourceActionList(EstadoPasajeDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public EstadoPasajeSelectMethod SelectMethod
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

	#endregion EstadoPasajeDataSourceActionList
	
	#endregion EstadoPasajeDataSourceDesigner
	
	#region EstadoPasajeSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the EstadoPasajeDataSource.SelectMethod property.
	/// </summary>
	public enum EstadoPasajeSelectMethod
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
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion EstadoPasajeSelectMethod

	#region EstadoPasajeFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="EstadoPasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class EstadoPasajeFilter : SqlFilter<EstadoPasajeColumn>
	{
	}
	
	#endregion EstadoPasajeFilter

	#region EstadoPasajeExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="EstadoPasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class EstadoPasajeExpressionBuilder : SqlExpressionBuilder<EstadoPasajeColumn>
	{
	}
	
	#endregion EstadoPasajeExpressionBuilder	

	#region EstadoPasajeProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;EstadoPasajeChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="EstadoPasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class EstadoPasajeProperty : ChildEntityProperty<EstadoPasajeChildEntityTypes>
	{
	}
	
	#endregion EstadoPasajeProperty
}

