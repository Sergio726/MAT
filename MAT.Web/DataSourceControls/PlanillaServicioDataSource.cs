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
	/// Represents the DataRepository.PlanillaServicioProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PlanillaServicioDataSourceDesigner))]
	public class PlanillaServicioDataSource : ProviderDataSource<PlanillaServicio, PlanillaServicioKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioDataSource class.
		/// </summary>
		public PlanillaServicioDataSource() : base(new PlanillaServicioService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PlanillaServicioDataSourceView used by the PlanillaServicioDataSource.
		/// </summary>
		protected PlanillaServicioDataSourceView PlanillaServicioView
		{
			get { return ( View as PlanillaServicioDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PlanillaServicioDataSource control invokes to retrieve data.
		/// </summary>
		public PlanillaServicioSelectMethod SelectMethod
		{
			get
			{
				PlanillaServicioSelectMethod selectMethod = PlanillaServicioSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PlanillaServicioSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PlanillaServicioDataSourceView class that is to be
		/// used by the PlanillaServicioDataSource.
		/// </summary>
		/// <returns>An instance of the PlanillaServicioDataSourceView class.</returns>
		protected override BaseDataSourceView<PlanillaServicio, PlanillaServicioKey> GetNewDataSourceView()
		{
			return new PlanillaServicioDataSourceView(this, DefaultViewName);
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
	/// Supports the PlanillaServicioDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PlanillaServicioDataSourceView : ProviderDataSourceView<PlanillaServicio, PlanillaServicioKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PlanillaServicioDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PlanillaServicioDataSourceView(PlanillaServicioDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PlanillaServicioDataSource PlanillaServicioOwner
		{
			get { return Owner as PlanillaServicioDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PlanillaServicioSelectMethod SelectMethod
		{
			get { return PlanillaServicioOwner.SelectMethod; }
			set { PlanillaServicioOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PlanillaServicioService PlanillaServicioProvider
		{
			get { return Provider as PlanillaServicioService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<PlanillaServicio> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<PlanillaServicio> results = null;
			PlanillaServicio item;
			count = 0;
			
			System.Guid _planillaServicioId;

			switch ( SelectMethod )
			{
				case PlanillaServicioSelectMethod.Get:
					PlanillaServicioKey entityKey  = new PlanillaServicioKey();
					entityKey.Load(values);
					item = PlanillaServicioProvider.Get(entityKey);
					results = new TList<PlanillaServicio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PlanillaServicioSelectMethod.GetAll:
                    results = PlanillaServicioProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PlanillaServicioSelectMethod.GetPaged:
					results = PlanillaServicioProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PlanillaServicioSelectMethod.Find:
					if ( FilterParameters != null )
						results = PlanillaServicioProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PlanillaServicioProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PlanillaServicioSelectMethod.GetByPlanillaServicioId:
					_planillaServicioId = ( values["PlanillaServicioId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PlanillaServicioId"], typeof(System.Guid)) : Guid.Empty;
					item = PlanillaServicioProvider.GetByPlanillaServicioId(_planillaServicioId);
					results = new TList<PlanillaServicio>();
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
			if ( SelectMethod == PlanillaServicioSelectMethod.Get || SelectMethod == PlanillaServicioSelectMethod.GetByPlanillaServicioId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(PlanillaServicio entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PlanillaServicioId == Guid.Empty )
				entity.PlanillaServicioId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				PlanillaServicio entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PlanillaServicioProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<PlanillaServicio> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PlanillaServicioProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PlanillaServicioDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PlanillaServicioDataSource class.
	/// </summary>
	public class PlanillaServicioDataSourceDesigner : ProviderDataSourceDesigner<PlanillaServicio, PlanillaServicioKey>
	{
		/// <summary>
		/// Initializes a new instance of the PlanillaServicioDataSourceDesigner class.
		/// </summary>
		public PlanillaServicioDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PlanillaServicioSelectMethod SelectMethod
		{
			get { return ((PlanillaServicioDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PlanillaServicioDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PlanillaServicioDataSourceActionList

	/// <summary>
	/// Supports the PlanillaServicioDataSourceDesigner class.
	/// </summary>
	internal class PlanillaServicioDataSourceActionList : DesignerActionList
	{
		private PlanillaServicioDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PlanillaServicioDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PlanillaServicioDataSourceActionList(PlanillaServicioDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PlanillaServicioSelectMethod SelectMethod
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

	#endregion PlanillaServicioDataSourceActionList
	
	#endregion PlanillaServicioDataSourceDesigner
	
	#region PlanillaServicioSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PlanillaServicioDataSource.SelectMethod property.
	/// </summary>
	public enum PlanillaServicioSelectMethod
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
		/// Represents the GetByPlanillaServicioId method.
		/// </summary>
		GetByPlanillaServicioId
	}
	
	#endregion PlanillaServicioSelectMethod

	#region PlanillaServicioFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioFilter : SqlFilter<PlanillaServicioColumn>
	{
	}
	
	#endregion PlanillaServicioFilter

	#region PlanillaServicioExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioExpressionBuilder : SqlExpressionBuilder<PlanillaServicioColumn>
	{
	}
	
	#endregion PlanillaServicioExpressionBuilder	

	#region PlanillaServicioProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PlanillaServicioChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="PlanillaServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PlanillaServicioProperty : ChildEntityProperty<PlanillaServicioChildEntityTypes>
	{
	}
	
	#endregion PlanillaServicioProperty
}

