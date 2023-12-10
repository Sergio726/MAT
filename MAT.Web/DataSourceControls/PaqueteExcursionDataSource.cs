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
	/// Represents the DataRepository.PaqueteExcursionProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PaqueteExcursionDataSourceDesigner))]
	public class PaqueteExcursionDataSource : ProviderDataSource<PaqueteExcursion, PaqueteExcursionKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionDataSource class.
		/// </summary>
		public PaqueteExcursionDataSource() : base(new PaqueteExcursionService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PaqueteExcursionDataSourceView used by the PaqueteExcursionDataSource.
		/// </summary>
		protected PaqueteExcursionDataSourceView PaqueteExcursionView
		{
			get { return ( View as PaqueteExcursionDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PaqueteExcursionDataSource control invokes to retrieve data.
		/// </summary>
		public PaqueteExcursionSelectMethod SelectMethod
		{
			get
			{
				PaqueteExcursionSelectMethod selectMethod = PaqueteExcursionSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PaqueteExcursionSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PaqueteExcursionDataSourceView class that is to be
		/// used by the PaqueteExcursionDataSource.
		/// </summary>
		/// <returns>An instance of the PaqueteExcursionDataSourceView class.</returns>
		protected override BaseDataSourceView<PaqueteExcursion, PaqueteExcursionKey> GetNewDataSourceView()
		{
			return new PaqueteExcursionDataSourceView(this, DefaultViewName);
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
	/// Supports the PaqueteExcursionDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PaqueteExcursionDataSourceView : ProviderDataSourceView<PaqueteExcursion, PaqueteExcursionKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PaqueteExcursionDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PaqueteExcursionDataSourceView(PaqueteExcursionDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PaqueteExcursionDataSource PaqueteExcursionOwner
		{
			get { return Owner as PaqueteExcursionDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PaqueteExcursionSelectMethod SelectMethod
		{
			get { return PaqueteExcursionOwner.SelectMethod; }
			set { PaqueteExcursionOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PaqueteExcursionService PaqueteExcursionProvider
		{
			get { return Provider as PaqueteExcursionService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<PaqueteExcursion> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<PaqueteExcursion> results = null;
			PaqueteExcursion item;
			count = 0;
			
			System.Guid _paqueteExcursionId;
			System.Guid _excursionId;
			System.Guid _paqueteId;

			switch ( SelectMethod )
			{
				case PaqueteExcursionSelectMethod.Get:
					PaqueteExcursionKey entityKey  = new PaqueteExcursionKey();
					entityKey.Load(values);
					item = PaqueteExcursionProvider.Get(entityKey);
					results = new TList<PaqueteExcursion>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PaqueteExcursionSelectMethod.GetAll:
                    results = PaqueteExcursionProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PaqueteExcursionSelectMethod.GetPaged:
					results = PaqueteExcursionProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PaqueteExcursionSelectMethod.Find:
					if ( FilterParameters != null )
						results = PaqueteExcursionProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PaqueteExcursionProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PaqueteExcursionSelectMethod.GetByPaqueteExcursionId:
					_paqueteExcursionId = ( values["PaqueteExcursionId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PaqueteExcursionId"], typeof(System.Guid)) : Guid.Empty;
					item = PaqueteExcursionProvider.GetByPaqueteExcursionId(_paqueteExcursionId);
					results = new TList<PaqueteExcursion>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case PaqueteExcursionSelectMethod.GetByExcursionId:
					_excursionId = ( values["ExcursionId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ExcursionId"], typeof(System.Guid)) : Guid.Empty;
					results = PaqueteExcursionProvider.GetByExcursionId(_excursionId, this.StartIndex, this.PageSize, out count);
					break;
				case PaqueteExcursionSelectMethod.GetByPaqueteId:
					_paqueteId = ( values["PaqueteId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PaqueteId"], typeof(System.Guid)) : Guid.Empty;
					results = PaqueteExcursionProvider.GetByPaqueteId(_paqueteId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == PaqueteExcursionSelectMethod.Get || SelectMethod == PaqueteExcursionSelectMethod.GetByPaqueteExcursionId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(PaqueteExcursion entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PaqueteExcursionId == Guid.Empty )
				entity.PaqueteExcursionId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				PaqueteExcursion entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PaqueteExcursionProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<PaqueteExcursion> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PaqueteExcursionProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PaqueteExcursionDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PaqueteExcursionDataSource class.
	/// </summary>
	public class PaqueteExcursionDataSourceDesigner : ProviderDataSourceDesigner<PaqueteExcursion, PaqueteExcursionKey>
	{
		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionDataSourceDesigner class.
		/// </summary>
		public PaqueteExcursionDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PaqueteExcursionSelectMethod SelectMethod
		{
			get { return ((PaqueteExcursionDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PaqueteExcursionDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PaqueteExcursionDataSourceActionList

	/// <summary>
	/// Supports the PaqueteExcursionDataSourceDesigner class.
	/// </summary>
	internal class PaqueteExcursionDataSourceActionList : DesignerActionList
	{
		private PaqueteExcursionDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PaqueteExcursionDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PaqueteExcursionDataSourceActionList(PaqueteExcursionDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PaqueteExcursionSelectMethod SelectMethod
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

	#endregion PaqueteExcursionDataSourceActionList
	
	#endregion PaqueteExcursionDataSourceDesigner
	
	#region PaqueteExcursionSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PaqueteExcursionDataSource.SelectMethod property.
	/// </summary>
	public enum PaqueteExcursionSelectMethod
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
		/// Represents the GetByPaqueteExcursionId method.
		/// </summary>
		GetByPaqueteExcursionId,
		/// <summary>
		/// Represents the GetByExcursionId method.
		/// </summary>
		GetByExcursionId,
		/// <summary>
		/// Represents the GetByPaqueteId method.
		/// </summary>
		GetByPaqueteId
	}
	
	#endregion PaqueteExcursionSelectMethod

	#region PaqueteExcursionFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteExcursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteExcursionFilter : SqlFilter<PaqueteExcursionColumn>
	{
	}
	
	#endregion PaqueteExcursionFilter

	#region PaqueteExcursionExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteExcursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteExcursionExpressionBuilder : SqlExpressionBuilder<PaqueteExcursionColumn>
	{
	}
	
	#endregion PaqueteExcursionExpressionBuilder	

	#region PaqueteExcursionProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PaqueteExcursionChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteExcursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteExcursionProperty : ChildEntityProperty<PaqueteExcursionChildEntityTypes>
	{
	}
	
	#endregion PaqueteExcursionProperty
}

