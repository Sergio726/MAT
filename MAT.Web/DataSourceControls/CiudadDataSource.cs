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
	/// Represents the DataRepository.CiudadProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(CiudadDataSourceDesigner))]
	public class CiudadDataSource : ProviderDataSource<Ciudad, CiudadKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CiudadDataSource class.
		/// </summary>
		public CiudadDataSource() : base(new CiudadService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the CiudadDataSourceView used by the CiudadDataSource.
		/// </summary>
		protected CiudadDataSourceView CiudadView
		{
			get { return ( View as CiudadDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the CiudadDataSource control invokes to retrieve data.
		/// </summary>
		public CiudadSelectMethod SelectMethod
		{
			get
			{
				CiudadSelectMethod selectMethod = CiudadSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (CiudadSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the CiudadDataSourceView class that is to be
		/// used by the CiudadDataSource.
		/// </summary>
		/// <returns>An instance of the CiudadDataSourceView class.</returns>
		protected override BaseDataSourceView<Ciudad, CiudadKey> GetNewDataSourceView()
		{
			return new CiudadDataSourceView(this, DefaultViewName);
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
	/// Supports the CiudadDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class CiudadDataSourceView : ProviderDataSourceView<Ciudad, CiudadKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CiudadDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the CiudadDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public CiudadDataSourceView(CiudadDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal CiudadDataSource CiudadOwner
		{
			get { return Owner as CiudadDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal CiudadSelectMethod SelectMethod
		{
			get { return CiudadOwner.SelectMethod; }
			set { CiudadOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal CiudadService CiudadProvider
		{
			get { return Provider as CiudadService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Ciudad> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Ciudad> results = null;
			Ciudad item;
			count = 0;
			
			System.Int32 _ciudadId;

			switch ( SelectMethod )
			{
				case CiudadSelectMethod.Get:
					CiudadKey entityKey  = new CiudadKey();
					entityKey.Load(values);
					item = CiudadProvider.Get(entityKey);
					results = new TList<Ciudad>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case CiudadSelectMethod.GetAll:
                    results = CiudadProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case CiudadSelectMethod.GetPaged:
					results = CiudadProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case CiudadSelectMethod.Find:
					if ( FilterParameters != null )
						results = CiudadProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = CiudadProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case CiudadSelectMethod.GetByCiudadId:
					_ciudadId = ( values["CiudadId"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["CiudadId"], typeof(System.Int32)) : (int)0;
					item = CiudadProvider.GetByCiudadId(_ciudadId);
					results = new TList<Ciudad>();
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
			if ( SelectMethod == CiudadSelectMethod.Get || SelectMethod == CiudadSelectMethod.GetByCiudadId )
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
				Ciudad entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					CiudadProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Ciudad> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			CiudadProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region CiudadDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the CiudadDataSource class.
	/// </summary>
	public class CiudadDataSourceDesigner : ProviderDataSourceDesigner<Ciudad, CiudadKey>
	{
		/// <summary>
		/// Initializes a new instance of the CiudadDataSourceDesigner class.
		/// </summary>
		public CiudadDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public CiudadSelectMethod SelectMethod
		{
			get { return ((CiudadDataSource) DataSource).SelectMethod; }
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
				actions.Add(new CiudadDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region CiudadDataSourceActionList

	/// <summary>
	/// Supports the CiudadDataSourceDesigner class.
	/// </summary>
	internal class CiudadDataSourceActionList : DesignerActionList
	{
		private CiudadDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the CiudadDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public CiudadDataSourceActionList(CiudadDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public CiudadSelectMethod SelectMethod
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

	#endregion CiudadDataSourceActionList
	
	#endregion CiudadDataSourceDesigner
	
	#region CiudadSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the CiudadDataSource.SelectMethod property.
	/// </summary>
	public enum CiudadSelectMethod
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
		/// Represents the GetByCiudadId method.
		/// </summary>
		GetByCiudadId
	}
	
	#endregion CiudadSelectMethod

	#region CiudadFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Ciudad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CiudadFilter : SqlFilter<CiudadColumn>
	{
	}
	
	#endregion CiudadFilter

	#region CiudadExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Ciudad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CiudadExpressionBuilder : SqlExpressionBuilder<CiudadColumn>
	{
	}
	
	#endregion CiudadExpressionBuilder	

	#region CiudadProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;CiudadChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Ciudad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CiudadProperty : ChildEntityProperty<CiudadChildEntityTypes>
	{
	}
	
	#endregion CiudadProperty
}

