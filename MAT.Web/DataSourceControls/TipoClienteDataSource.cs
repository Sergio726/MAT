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
	/// Represents the DataRepository.TipoClienteProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(TipoClienteDataSourceDesigner))]
	public class TipoClienteDataSource : ProviderDataSource<TipoCliente, TipoClienteKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TipoClienteDataSource class.
		/// </summary>
		public TipoClienteDataSource() : base(new TipoClienteService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the TipoClienteDataSourceView used by the TipoClienteDataSource.
		/// </summary>
		protected TipoClienteDataSourceView TipoClienteView
		{
			get { return ( View as TipoClienteDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the TipoClienteDataSource control invokes to retrieve data.
		/// </summary>
		public TipoClienteSelectMethod SelectMethod
		{
			get
			{
				TipoClienteSelectMethod selectMethod = TipoClienteSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (TipoClienteSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the TipoClienteDataSourceView class that is to be
		/// used by the TipoClienteDataSource.
		/// </summary>
		/// <returns>An instance of the TipoClienteDataSourceView class.</returns>
		protected override BaseDataSourceView<TipoCliente, TipoClienteKey> GetNewDataSourceView()
		{
			return new TipoClienteDataSourceView(this, DefaultViewName);
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
	/// Supports the TipoClienteDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class TipoClienteDataSourceView : ProviderDataSourceView<TipoCliente, TipoClienteKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TipoClienteDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the TipoClienteDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public TipoClienteDataSourceView(TipoClienteDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal TipoClienteDataSource TipoClienteOwner
		{
			get { return Owner as TipoClienteDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal TipoClienteSelectMethod SelectMethod
		{
			get { return TipoClienteOwner.SelectMethod; }
			set { TipoClienteOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal TipoClienteService TipoClienteProvider
		{
			get { return Provider as TipoClienteService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<TipoCliente> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<TipoCliente> results = null;
			TipoCliente item;
			count = 0;
			
			System.String _descripcion_nullable;
			System.Int32 _tipoId;

			switch ( SelectMethod )
			{
				case TipoClienteSelectMethod.Get:
					TipoClienteKey entityKey  = new TipoClienteKey();
					entityKey.Load(values);
					item = TipoClienteProvider.Get(entityKey);
					results = new TList<TipoCliente>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case TipoClienteSelectMethod.GetAll:
                    results = TipoClienteProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case TipoClienteSelectMethod.GetPaged:
					results = TipoClienteProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case TipoClienteSelectMethod.Find:
					if ( FilterParameters != null )
						results = TipoClienteProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = TipoClienteProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case TipoClienteSelectMethod.GetByTipoId:
					_tipoId = ( values["TipoId"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["TipoId"], typeof(System.Int32)) : (int)0;
					item = TipoClienteProvider.GetByTipoId(_tipoId);
					results = new TList<TipoCliente>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case TipoClienteSelectMethod.GetByDescripcion:
					_descripcion_nullable = (System.String) EntityUtil.ChangeType(values["Descripcion"], typeof(System.String));
					item = TipoClienteProvider.GetByDescripcion(_descripcion_nullable);
					results = new TList<TipoCliente>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
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
			if ( SelectMethod == TipoClienteSelectMethod.Get || SelectMethod == TipoClienteSelectMethod.GetByTipoId )
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
				TipoCliente entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					TipoClienteProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<TipoCliente> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			TipoClienteProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region TipoClienteDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the TipoClienteDataSource class.
	/// </summary>
	public class TipoClienteDataSourceDesigner : ProviderDataSourceDesigner<TipoCliente, TipoClienteKey>
	{
		/// <summary>
		/// Initializes a new instance of the TipoClienteDataSourceDesigner class.
		/// </summary>
		public TipoClienteDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public TipoClienteSelectMethod SelectMethod
		{
			get { return ((TipoClienteDataSource) DataSource).SelectMethod; }
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
				actions.Add(new TipoClienteDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region TipoClienteDataSourceActionList

	/// <summary>
	/// Supports the TipoClienteDataSourceDesigner class.
	/// </summary>
	internal class TipoClienteDataSourceActionList : DesignerActionList
	{
		private TipoClienteDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the TipoClienteDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public TipoClienteDataSourceActionList(TipoClienteDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public TipoClienteSelectMethod SelectMethod
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

	#endregion TipoClienteDataSourceActionList
	
	#endregion TipoClienteDataSourceDesigner
	
	#region TipoClienteSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the TipoClienteDataSource.SelectMethod property.
	/// </summary>
	public enum TipoClienteSelectMethod
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
		/// Represents the GetByDescripcion method.
		/// </summary>
		GetByDescripcion,
		/// <summary>
		/// Represents the GetByTipoId method.
		/// </summary>
		GetByTipoId
	}
	
	#endregion TipoClienteSelectMethod

	#region TipoClienteFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="TipoCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TipoClienteFilter : SqlFilter<TipoClienteColumn>
	{
	}
	
	#endregion TipoClienteFilter

	#region TipoClienteExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="TipoCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TipoClienteExpressionBuilder : SqlExpressionBuilder<TipoClienteColumn>
	{
	}
	
	#endregion TipoClienteExpressionBuilder	

	#region TipoClienteProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;TipoClienteChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="TipoCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TipoClienteProperty : ChildEntityProperty<TipoClienteChildEntityTypes>
	{
	}
	
	#endregion TipoClienteProperty
}

