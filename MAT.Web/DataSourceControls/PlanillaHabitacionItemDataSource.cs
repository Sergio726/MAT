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
	/// Represents the DataRepository.PlanillaHabitacionItemProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PlanillaHabitacionItemDataSourceDesigner))]
	public class PlanillaHabitacionItemDataSource : ProviderDataSource<PlanillaHabitacionItem, PlanillaHabitacionItemKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaHabitacionItemDataSource class.
		/// </summary>
		public PlanillaHabitacionItemDataSource() : base(new PlanillaHabitacionItemService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PlanillaHabitacionItemDataSourceView used by the PlanillaHabitacionItemDataSource.
		/// </summary>
		protected PlanillaHabitacionItemDataSourceView PlanillaHabitacionItemView
		{
			get { return ( View as PlanillaHabitacionItemDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PlanillaHabitacionItemDataSource control invokes to retrieve data.
		/// </summary>
		public PlanillaHabitacionItemSelectMethod SelectMethod
		{
			get
			{
				PlanillaHabitacionItemSelectMethod selectMethod = PlanillaHabitacionItemSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PlanillaHabitacionItemSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PlanillaHabitacionItemDataSourceView class that is to be
		/// used by the PlanillaHabitacionItemDataSource.
		/// </summary>
		/// <returns>An instance of the PlanillaHabitacionItemDataSourceView class.</returns>
		protected override BaseDataSourceView<PlanillaHabitacionItem, PlanillaHabitacionItemKey> GetNewDataSourceView()
		{
			return new PlanillaHabitacionItemDataSourceView(this, DefaultViewName);
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
	/// Supports the PlanillaHabitacionItemDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PlanillaHabitacionItemDataSourceView : ProviderDataSourceView<PlanillaHabitacionItem, PlanillaHabitacionItemKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaHabitacionItemDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PlanillaHabitacionItemDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PlanillaHabitacionItemDataSourceView(PlanillaHabitacionItemDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PlanillaHabitacionItemDataSource PlanillaHabitacionItemOwner
		{
			get { return Owner as PlanillaHabitacionItemDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PlanillaHabitacionItemSelectMethod SelectMethod
		{
			get { return PlanillaHabitacionItemOwner.SelectMethod; }
			set { PlanillaHabitacionItemOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PlanillaHabitacionItemService PlanillaHabitacionItemProvider
		{
			get { return Provider as PlanillaHabitacionItemService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<PlanillaHabitacionItem> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<PlanillaHabitacionItem> results = null;
			PlanillaHabitacionItem item;
			count = 0;
			
			System.Guid _planillaHabitacionItemId;
			System.Guid _planillaId;

			switch ( SelectMethod )
			{
				case PlanillaHabitacionItemSelectMethod.Get:
					PlanillaHabitacionItemKey entityKey  = new PlanillaHabitacionItemKey();
					entityKey.Load(values);
					item = PlanillaHabitacionItemProvider.Get(entityKey);
					results = new TList<PlanillaHabitacionItem>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PlanillaHabitacionItemSelectMethod.GetAll:
                    results = PlanillaHabitacionItemProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PlanillaHabitacionItemSelectMethod.GetPaged:
					results = PlanillaHabitacionItemProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PlanillaHabitacionItemSelectMethod.Find:
					if ( FilterParameters != null )
						results = PlanillaHabitacionItemProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PlanillaHabitacionItemProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PlanillaHabitacionItemSelectMethod.GetByPlanillaHabitacionItemId:
					_planillaHabitacionItemId = ( values["PlanillaHabitacionItemId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PlanillaHabitacionItemId"], typeof(System.Guid)) : Guid.Empty;
					item = PlanillaHabitacionItemProvider.GetByPlanillaHabitacionItemId(_planillaHabitacionItemId);
					results = new TList<PlanillaHabitacionItem>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case PlanillaHabitacionItemSelectMethod.GetByPlanillaId:
					_planillaId = ( values["PlanillaId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PlanillaId"], typeof(System.Guid)) : Guid.Empty;
					results = PlanillaHabitacionItemProvider.GetByPlanillaId(_planillaId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == PlanillaHabitacionItemSelectMethod.Get || SelectMethod == PlanillaHabitacionItemSelectMethod.GetByPlanillaHabitacionItemId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(PlanillaHabitacionItem entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PlanillaHabitacionItemId == Guid.Empty )
				entity.PlanillaHabitacionItemId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				PlanillaHabitacionItem entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PlanillaHabitacionItemProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<PlanillaHabitacionItem> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PlanillaHabitacionItemProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PlanillaHabitacionItemDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PlanillaHabitacionItemDataSource class.
	/// </summary>
	public class PlanillaHabitacionItemDataSourceDesigner : ProviderDataSourceDesigner<PlanillaHabitacionItem, PlanillaHabitacionItemKey>
	{
		/// <summary>
		/// Initializes a new instance of the PlanillaHabitacionItemDataSourceDesigner class.
		/// </summary>
		public PlanillaHabitacionItemDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PlanillaHabitacionItemSelectMethod SelectMethod
		{
			get { return ((PlanillaHabitacionItemDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PlanillaHabitacionItemDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PlanillaHabitacionItemDataSourceActionList

	/// <summary>
	/// Supports the PlanillaHabitacionItemDataSourceDesigner class.
	/// </summary>
	internal class PlanillaHabitacionItemDataSourceActionList : DesignerActionList
	{
		private PlanillaHabitacionItemDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PlanillaHabitacionItemDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PlanillaHabitacionItemDataSourceActionList(PlanillaHabitacionItemDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PlanillaHabitacionItemSelectMethod SelectMethod
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

	#endregion PlanillaHabitacionItemDataSourceActionList
	
	#endregion PlanillaHabitacionItemDataSourceDesigner
	
	#region PlanillaHabitacionItemSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PlanillaHabitacionItemDataSource.SelectMethod property.
	/// </summary>
	public enum PlanillaHabitacionItemSelectMethod
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
		/// Represents the GetByPlanillaHabitacionItemId method.
		/// </summary>
		GetByPlanillaHabitacionItemId,
		/// <summary>
		/// Represents the GetByPlanillaId method.
		/// </summary>
		GetByPlanillaId
	}
	
	#endregion PlanillaHabitacionItemSelectMethod

	#region PlanillaHabitacionItemFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaHabitacionItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaHabitacionItemFilter : SqlFilter<PlanillaHabitacionItemColumn>
	{
	}
	
	#endregion PlanillaHabitacionItemFilter

	#region PlanillaHabitacionItemExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaHabitacionItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaHabitacionItemExpressionBuilder : SqlExpressionBuilder<PlanillaHabitacionItemColumn>
	{
	}
	
	#endregion PlanillaHabitacionItemExpressionBuilder	

	#region PlanillaHabitacionItemProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PlanillaHabitacionItemChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaHabitacionItem"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaHabitacionItemProperty : ChildEntityProperty<PlanillaHabitacionItemChildEntityTypes>
	{
	}
	
	#endregion PlanillaHabitacionItemProperty
}

