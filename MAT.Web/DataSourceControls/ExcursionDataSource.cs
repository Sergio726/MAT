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
	/// Represents the DataRepository.ExcursionProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ExcursionDataSourceDesigner))]
	public class ExcursionDataSource : ProviderDataSource<Excursion, ExcursionKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ExcursionDataSource class.
		/// </summary>
		public ExcursionDataSource() : base(new ExcursionService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ExcursionDataSourceView used by the ExcursionDataSource.
		/// </summary>
		protected ExcursionDataSourceView ExcursionView
		{
			get { return ( View as ExcursionDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ExcursionDataSource control invokes to retrieve data.
		/// </summary>
		public ExcursionSelectMethod SelectMethod
		{
			get
			{
				ExcursionSelectMethod selectMethod = ExcursionSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ExcursionSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ExcursionDataSourceView class that is to be
		/// used by the ExcursionDataSource.
		/// </summary>
		/// <returns>An instance of the ExcursionDataSourceView class.</returns>
		protected override BaseDataSourceView<Excursion, ExcursionKey> GetNewDataSourceView()
		{
			return new ExcursionDataSourceView(this, DefaultViewName);
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
	/// Supports the ExcursionDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ExcursionDataSourceView : ProviderDataSourceView<Excursion, ExcursionKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ExcursionDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ExcursionDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ExcursionDataSourceView(ExcursionDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ExcursionDataSource ExcursionOwner
		{
			get { return Owner as ExcursionDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ExcursionSelectMethod SelectMethod
		{
			get { return ExcursionOwner.SelectMethod; }
			set { ExcursionOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ExcursionService ExcursionProvider
		{
			get { return Provider as ExcursionService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Excursion> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Excursion> results = null;
			Excursion item;
			count = 0;
			
			System.Guid _excursionId;
			System.Guid? _proveedorId_nullable;

			switch ( SelectMethod )
			{
				case ExcursionSelectMethod.Get:
					ExcursionKey entityKey  = new ExcursionKey();
					entityKey.Load(values);
					item = ExcursionProvider.Get(entityKey);
					results = new TList<Excursion>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ExcursionSelectMethod.GetAll:
                    results = ExcursionProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case ExcursionSelectMethod.GetPaged:
					results = ExcursionProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ExcursionSelectMethod.Find:
					if ( FilterParameters != null )
						results = ExcursionProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ExcursionProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ExcursionSelectMethod.GetByExcursionId:
					_excursionId = ( values["ExcursionId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ExcursionId"], typeof(System.Guid)) : Guid.Empty;
					item = ExcursionProvider.GetByExcursionId(_excursionId);
					results = new TList<Excursion>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case ExcursionSelectMethod.GetByProveedorId:
					_proveedorId_nullable = (System.Guid?) EntityUtil.ChangeType(values["ProveedorId"], typeof(System.Guid?));
					results = ExcursionProvider.GetByProveedorId(_proveedorId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == ExcursionSelectMethod.Get || SelectMethod == ExcursionSelectMethod.GetByExcursionId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Excursion entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.ExcursionId == Guid.Empty )
				entity.ExcursionId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Excursion entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					ExcursionProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Excursion> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			ExcursionProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ExcursionDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ExcursionDataSource class.
	/// </summary>
	public class ExcursionDataSourceDesigner : ProviderDataSourceDesigner<Excursion, ExcursionKey>
	{
		/// <summary>
		/// Initializes a new instance of the ExcursionDataSourceDesigner class.
		/// </summary>
		public ExcursionDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ExcursionSelectMethod SelectMethod
		{
			get { return ((ExcursionDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ExcursionDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ExcursionDataSourceActionList

	/// <summary>
	/// Supports the ExcursionDataSourceDesigner class.
	/// </summary>
	internal class ExcursionDataSourceActionList : DesignerActionList
	{
		private ExcursionDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ExcursionDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ExcursionDataSourceActionList(ExcursionDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ExcursionSelectMethod SelectMethod
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

	#endregion ExcursionDataSourceActionList
	
	#endregion ExcursionDataSourceDesigner
	
	#region ExcursionSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ExcursionDataSource.SelectMethod property.
	/// </summary>
	public enum ExcursionSelectMethod
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
		/// Represents the GetByExcursionId method.
		/// </summary>
		GetByExcursionId,
		/// <summary>
		/// Represents the GetByProveedorId method.
		/// </summary>
		GetByProveedorId
	}
	
	#endregion ExcursionSelectMethod

	#region ExcursionFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Excursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ExcursionFilter : SqlFilter<ExcursionColumn>
	{
	}
	
	#endregion ExcursionFilter

	#region ExcursionExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Excursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ExcursionExpressionBuilder : SqlExpressionBuilder<ExcursionColumn>
	{
	}
	
	#endregion ExcursionExpressionBuilder	

	#region ExcursionProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ExcursionChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Excursion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ExcursionProperty : ChildEntityProperty<ExcursionChildEntityTypes>
	{
	}
	
	#endregion ExcursionProperty
}

