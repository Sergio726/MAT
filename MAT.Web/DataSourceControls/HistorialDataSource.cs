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
	/// Represents the DataRepository.HistorialProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(HistorialDataSourceDesigner))]
	public class HistorialDataSource : ProviderDataSource<Historial, HistorialKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HistorialDataSource class.
		/// </summary>
		public HistorialDataSource() : base(new HistorialService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the HistorialDataSourceView used by the HistorialDataSource.
		/// </summary>
		protected HistorialDataSourceView HistorialView
		{
			get { return ( View as HistorialDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the HistorialDataSource control invokes to retrieve data.
		/// </summary>
		public HistorialSelectMethod SelectMethod
		{
			get
			{
				HistorialSelectMethod selectMethod = HistorialSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (HistorialSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the HistorialDataSourceView class that is to be
		/// used by the HistorialDataSource.
		/// </summary>
		/// <returns>An instance of the HistorialDataSourceView class.</returns>
		protected override BaseDataSourceView<Historial, HistorialKey> GetNewDataSourceView()
		{
			return new HistorialDataSourceView(this, DefaultViewName);
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
	/// Supports the HistorialDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class HistorialDataSourceView : ProviderDataSourceView<Historial, HistorialKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HistorialDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the HistorialDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public HistorialDataSourceView(HistorialDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal HistorialDataSource HistorialOwner
		{
			get { return Owner as HistorialDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal HistorialSelectMethod SelectMethod
		{
			get { return HistorialOwner.SelectMethod; }
			set { HistorialOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal HistorialService HistorialProvider
		{
			get { return Provider as HistorialService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Historial> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Historial> results = null;
			Historial item;
			count = 0;
			
			System.Guid _historialId;

			switch ( SelectMethod )
			{
				case HistorialSelectMethod.Get:
					HistorialKey entityKey  = new HistorialKey();
					entityKey.Load(values);
					item = HistorialProvider.Get(entityKey);
					results = new TList<Historial>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case HistorialSelectMethod.GetAll:
                    results = HistorialProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case HistorialSelectMethod.GetPaged:
					results = HistorialProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case HistorialSelectMethod.Find:
					if ( FilterParameters != null )
						results = HistorialProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = HistorialProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case HistorialSelectMethod.GetByHistorialId:
					_historialId = ( values["HistorialId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["HistorialId"], typeof(System.Guid)) : Guid.Empty;
					item = HistorialProvider.GetByHistorialId(_historialId);
					results = new TList<Historial>();
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
			if ( SelectMethod == HistorialSelectMethod.Get || SelectMethod == HistorialSelectMethod.GetByHistorialId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Historial entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.HistorialId == Guid.Empty )
				entity.HistorialId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Historial entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					HistorialProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Historial> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			HistorialProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region HistorialDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the HistorialDataSource class.
	/// </summary>
	public class HistorialDataSourceDesigner : ProviderDataSourceDesigner<Historial, HistorialKey>
	{
		/// <summary>
		/// Initializes a new instance of the HistorialDataSourceDesigner class.
		/// </summary>
		public HistorialDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public HistorialSelectMethod SelectMethod
		{
			get { return ((HistorialDataSource) DataSource).SelectMethod; }
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
				actions.Add(new HistorialDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region HistorialDataSourceActionList

	/// <summary>
	/// Supports the HistorialDataSourceDesigner class.
	/// </summary>
	internal class HistorialDataSourceActionList : DesignerActionList
	{
		private HistorialDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the HistorialDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public HistorialDataSourceActionList(HistorialDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public HistorialSelectMethod SelectMethod
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

	#endregion HistorialDataSourceActionList
	
	#endregion HistorialDataSourceDesigner
	
	#region HistorialSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the HistorialDataSource.SelectMethod property.
	/// </summary>
	public enum HistorialSelectMethod
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
		/// Represents the GetByHistorialId method.
		/// </summary>
		GetByHistorialId
	}
	
	#endregion HistorialSelectMethod

	#region HistorialFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Historial"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HistorialFilter : SqlFilter<HistorialColumn>
	{
	}
	
	#endregion HistorialFilter

	#region HistorialExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Historial"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HistorialExpressionBuilder : SqlExpressionBuilder<HistorialColumn>
	{
	}
	
	#endregion HistorialExpressionBuilder	

	#region HistorialProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;HistorialChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Historial"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HistorialProperty : ChildEntityProperty<HistorialChildEntityTypes>
	{
	}
	
	#endregion HistorialProperty
}

