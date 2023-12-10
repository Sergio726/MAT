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
	/// Represents the DataRepository.PlanillaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PlanillaDataSourceDesigner))]
	public class PlanillaDataSource : ProviderDataSource<Planilla, PlanillaKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaDataSource class.
		/// </summary>
		public PlanillaDataSource() : base(new PlanillaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PlanillaDataSourceView used by the PlanillaDataSource.
		/// </summary>
		protected PlanillaDataSourceView PlanillaView
		{
			get { return ( View as PlanillaDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PlanillaDataSource control invokes to retrieve data.
		/// </summary>
		public PlanillaSelectMethod SelectMethod
		{
			get
			{
				PlanillaSelectMethod selectMethod = PlanillaSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PlanillaSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PlanillaDataSourceView class that is to be
		/// used by the PlanillaDataSource.
		/// </summary>
		/// <returns>An instance of the PlanillaDataSourceView class.</returns>
		protected override BaseDataSourceView<Planilla, PlanillaKey> GetNewDataSourceView()
		{
			return new PlanillaDataSourceView(this, DefaultViewName);
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
	/// Supports the PlanillaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PlanillaDataSourceView : ProviderDataSourceView<Planilla, PlanillaKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PlanillaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PlanillaDataSourceView(PlanillaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PlanillaDataSource PlanillaOwner
		{
			get { return Owner as PlanillaDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PlanillaSelectMethod SelectMethod
		{
			get { return PlanillaOwner.SelectMethod; }
			set { PlanillaOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PlanillaService PlanillaProvider
		{
			get { return Provider as PlanillaService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Planilla> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Planilla> results = null;
			Planilla item;
			count = 0;
			
			System.Guid _planillaId;

			switch ( SelectMethod )
			{
				case PlanillaSelectMethod.Get:
					PlanillaKey entityKey  = new PlanillaKey();
					entityKey.Load(values);
					item = PlanillaProvider.Get(entityKey);
					results = new TList<Planilla>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PlanillaSelectMethod.GetAll:
                    results = PlanillaProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PlanillaSelectMethod.GetPaged:
					results = PlanillaProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PlanillaSelectMethod.Find:
					if ( FilterParameters != null )
						results = PlanillaProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PlanillaProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PlanillaSelectMethod.GetByPlanillaId:
					_planillaId = ( values["PlanillaId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PlanillaId"], typeof(System.Guid)) : Guid.Empty;
					item = PlanillaProvider.GetByPlanillaId(_planillaId);
					results = new TList<Planilla>();
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
			if ( SelectMethod == PlanillaSelectMethod.Get || SelectMethod == PlanillaSelectMethod.GetByPlanillaId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Planilla entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PlanillaId == Guid.Empty )
				entity.PlanillaId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Planilla entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PlanillaProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Planilla> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PlanillaProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PlanillaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PlanillaDataSource class.
	/// </summary>
	public class PlanillaDataSourceDesigner : ProviderDataSourceDesigner<Planilla, PlanillaKey>
	{
		/// <summary>
		/// Initializes a new instance of the PlanillaDataSourceDesigner class.
		/// </summary>
		public PlanillaDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PlanillaSelectMethod SelectMethod
		{
			get { return ((PlanillaDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PlanillaDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PlanillaDataSourceActionList

	/// <summary>
	/// Supports the PlanillaDataSourceDesigner class.
	/// </summary>
	internal class PlanillaDataSourceActionList : DesignerActionList
	{
		private PlanillaDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PlanillaDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PlanillaDataSourceActionList(PlanillaDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PlanillaSelectMethod SelectMethod
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

	#endregion PlanillaDataSourceActionList
	
	#endregion PlanillaDataSourceDesigner
	
	#region PlanillaSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PlanillaDataSource.SelectMethod property.
	/// </summary>
	public enum PlanillaSelectMethod
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
		/// Represents the GetByPlanillaId method.
		/// </summary>
		GetByPlanillaId
	}
	
	#endregion PlanillaSelectMethod

	#region PlanillaFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Planilla"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaFilter : SqlFilter<PlanillaColumn>
	{
	}
	
	#endregion PlanillaFilter

	#region PlanillaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Planilla"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaExpressionBuilder : SqlExpressionBuilder<PlanillaColumn>
	{
	}
	
	#endregion PlanillaExpressionBuilder	

	#region PlanillaProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PlanillaChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Planilla"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaProperty : ChildEntityProperty<PlanillaChildEntityTypes>
	{
	}
	
	#endregion PlanillaProperty
}

