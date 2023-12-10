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
	/// Represents the DataRepository.PlanillaServicioItemProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PlanillaServicioItemDataSourceDesigner))]
	public class PlanillaServicioItemDataSource : ProviderDataSource<PlanillaServicioItem, PlanillaServicioItemKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemDataSource class.
		/// </summary>
		public PlanillaServicioItemDataSource() : base(new PlanillaServicioItemService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PlanillaServicioItemDataSourceView used by the PlanillaServicioItemDataSource.
		/// </summary>
		protected PlanillaServicioItemDataSourceView PlanillaServicioItemView
		{
			get { return ( View as PlanillaServicioItemDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PlanillaServicioItemDataSource control invokes to retrieve data.
		/// </summary>
		public PlanillaServicioItemSelectMethod SelectMethod
		{
			get
			{
				PlanillaServicioItemSelectMethod selectMethod = PlanillaServicioItemSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PlanillaServicioItemSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PlanillaServicioItemDataSourceView class that is to be
		/// used by the PlanillaServicioItemDataSource.
		/// </summary>
		/// <returns>An instance of the PlanillaServicioItemDataSourceView class.</returns>
		protected override BaseDataSourceView<PlanillaServicioItem, PlanillaServicioItemKey> GetNewDataSourceView()
		{
			return new PlanillaServicioItemDataSourceView(this, DefaultViewName);
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
	/// Supports the PlanillaServicioItemDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PlanillaServicioItemDataSourceView : ProviderDataSourceView<PlanillaServicioItem, PlanillaServicioItemKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PlanillaServicioItemDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PlanillaServicioItemDataSourceView(PlanillaServicioItemDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PlanillaServicioItemDataSource PlanillaServicioItemOwner
		{
			get { return Owner as PlanillaServicioItemDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PlanillaServicioItemSelectMethod SelectMethod
		{
			get { return PlanillaServicioItemOwner.SelectMethod; }
			set { PlanillaServicioItemOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PlanillaServicioItemService PlanillaServicioItemProvider
		{
			get { return Provider as PlanillaServicioItemService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<PlanillaServicioItem> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<PlanillaServicioItem> results = null;
			PlanillaServicioItem item;
			count = 0;
			
			System.Guid _planillaServicioItemId;
			System.Guid _planillaId;

			switch ( SelectMethod )
			{
				case PlanillaServicioItemSelectMethod.Get:
					PlanillaServicioItemKey entityKey  = new PlanillaServicioItemKey();
					entityKey.Load(values);
					item = PlanillaServicioItemProvider.Get(entityKey);
					results = new TList<PlanillaServicioItem>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PlanillaServicioItemSelectMethod.GetAll:
                    results = PlanillaServicioItemProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PlanillaServicioItemSelectMethod.GetPaged:
					results = PlanillaServicioItemProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PlanillaServicioItemSelectMethod.Find:
					if ( FilterParameters != null )
						results = PlanillaServicioItemProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PlanillaServicioItemProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PlanillaServicioItemSelectMethod.GetByPlanillaServicioItemId:
					_planillaServicioItemId = ( values["PlanillaServicioItemId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PlanillaServicioItemId"], typeof(System.Guid)) : Guid.Empty;
					item = PlanillaServicioItemProvider.GetByPlanillaServicioItemId(_planillaServicioItemId);
					results = new TList<PlanillaServicioItem>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case PlanillaServicioItemSelectMethod.GetByPlanillaId:
					_planillaId = ( values["PlanillaId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PlanillaId"], typeof(System.Guid)) : Guid.Empty;
					results = PlanillaServicioItemProvider.GetByPlanillaId(_planillaId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == PlanillaServicioItemSelectMethod.Get || SelectMethod == PlanillaServicioItemSelectMethod.GetByPlanillaServicioItemId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(PlanillaServicioItem entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PlanillaServicioItemId == Guid.Empty )
				entity.PlanillaServicioItemId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				PlanillaServicioItem entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PlanillaServicioItemProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<PlanillaServicioItem> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PlanillaServicioItemProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PlanillaServicioItemDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PlanillaServicioItemDataSource class.
	/// </summary>
	public class PlanillaServicioItemDataSourceDesigner : ProviderDataSourceDesigner<PlanillaServicioItem, PlanillaServicioItemKey>
	{
		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemDataSourceDesigner class.
		/// </summary>
		public PlanillaServicioItemDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PlanillaServicioItemSelectMethod SelectMethod
		{
			get { return ((PlanillaServicioItemDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PlanillaServicioItemDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PlanillaServicioItemDataSourceActionList

	/// <summary>
	/// Supports the PlanillaServicioItemDataSourceDesigner class.
	/// </summary>
	internal class PlanillaServicioItemDataSourceActionList : DesignerActionList
	{
		private PlanillaServicioItemDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioItemDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PlanillaServicioItemDataSourceActionList(PlanillaServicioItemDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PlanillaServicioItemSelectMethod SelectMethod
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

	#endregion PlanillaServicioItemDataSourceActionList
	
	#endregion PlanillaServicioItemDataSourceDesigner
	
	#region PlanillaServicioItemSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PlanillaServicioItemDataSource.SelectMethod property.
	/// </summary>
	public enum PlanillaServicioItemSelectMethod
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
		/// Represents the GetByPlanillaServicioItemId method.
		/// </summary>
		GetByPlanillaServicioItemId,
		/// <summary>
		/// Represents the GetByPlanillaId method.
		/// </summary>
		GetByPlanillaId
	}
	
	#endregion PlanillaServicioItemSelectMethod

	#region PlanillaServicioItemFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicioItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioItemFilter : SqlFilter<PlanillaServicioItemColumn>
	{
	}
	
	#endregion PlanillaServicioItemFilter

	#region PlanillaServicioItemExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicioItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioItemExpressionBuilder : SqlExpressionBuilder<PlanillaServicioItemColumn>
	{
	}
	
	#endregion PlanillaServicioItemExpressionBuilder	

	#region PlanillaServicioItemProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PlanillaServicioItemChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicioItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioItemProperty : ChildEntityProperty<PlanillaServicioItemChildEntityTypes>
	{
	}
	
	#endregion PlanillaServicioItemProperty
}

