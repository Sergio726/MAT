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
	/// Represents the DataRepository.DepartamentoProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(DepartamentoDataSourceDesigner))]
	public class DepartamentoDataSource : ProviderDataSource<Departamento, DepartamentoKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DepartamentoDataSource class.
		/// </summary>
		public DepartamentoDataSource() : base(new DepartamentoService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the DepartamentoDataSourceView used by the DepartamentoDataSource.
		/// </summary>
		protected DepartamentoDataSourceView DepartamentoView
		{
			get { return ( View as DepartamentoDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DepartamentoDataSource control invokes to retrieve data.
		/// </summary>
		public DepartamentoSelectMethod SelectMethod
		{
			get
			{
				DepartamentoSelectMethod selectMethod = DepartamentoSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (DepartamentoSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the DepartamentoDataSourceView class that is to be
		/// used by the DepartamentoDataSource.
		/// </summary>
		/// <returns>An instance of the DepartamentoDataSourceView class.</returns>
		protected override BaseDataSourceView<Departamento, DepartamentoKey> GetNewDataSourceView()
		{
			return new DepartamentoDataSourceView(this, DefaultViewName);
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
	/// Supports the DepartamentoDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class DepartamentoDataSourceView : ProviderDataSourceView<Departamento, DepartamentoKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DepartamentoDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the DepartamentoDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public DepartamentoDataSourceView(DepartamentoDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal DepartamentoDataSource DepartamentoOwner
		{
			get { return Owner as DepartamentoDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal DepartamentoSelectMethod SelectMethod
		{
			get { return DepartamentoOwner.SelectMethod; }
			set { DepartamentoOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal DepartamentoService DepartamentoProvider
		{
			get { return Provider as DepartamentoService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Departamento> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Departamento> results = null;
			Departamento item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case DepartamentoSelectMethod.Get:
					DepartamentoKey entityKey  = new DepartamentoKey();
					entityKey.Load(values);
					item = DepartamentoProvider.Get(entityKey);
					results = new TList<Departamento>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case DepartamentoSelectMethod.GetAll:
                    results = DepartamentoProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case DepartamentoSelectMethod.GetPaged:
					results = DepartamentoProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case DepartamentoSelectMethod.Find:
					if ( FilterParameters != null )
						results = DepartamentoProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = DepartamentoProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case DepartamentoSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = DepartamentoProvider.GetById(_id);
					results = new TList<Departamento>();
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
			if ( SelectMethod == DepartamentoSelectMethod.Get || SelectMethod == DepartamentoSelectMethod.GetById )
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
				Departamento entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					DepartamentoProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Departamento> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			DepartamentoProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region DepartamentoDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the DepartamentoDataSource class.
	/// </summary>
	public class DepartamentoDataSourceDesigner : ProviderDataSourceDesigner<Departamento, DepartamentoKey>
	{
		/// <summary>
		/// Initializes a new instance of the DepartamentoDataSourceDesigner class.
		/// </summary>
		public DepartamentoDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DepartamentoSelectMethod SelectMethod
		{
			get { return ((DepartamentoDataSource) DataSource).SelectMethod; }
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
				actions.Add(new DepartamentoDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region DepartamentoDataSourceActionList

	/// <summary>
	/// Supports the DepartamentoDataSourceDesigner class.
	/// </summary>
	internal class DepartamentoDataSourceActionList : DesignerActionList
	{
		private DepartamentoDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the DepartamentoDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public DepartamentoDataSourceActionList(DepartamentoDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DepartamentoSelectMethod SelectMethod
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

	#endregion DepartamentoDataSourceActionList
	
	#endregion DepartamentoDataSourceDesigner
	
	#region DepartamentoSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the DepartamentoDataSource.SelectMethod property.
	/// </summary>
	public enum DepartamentoSelectMethod
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
	
	#endregion DepartamentoSelectMethod

	#region DepartamentoFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Departamento"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DepartamentoFilter : SqlFilter<DepartamentoColumn>
	{
	}
	
	#endregion DepartamentoFilter

	#region DepartamentoExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Departamento"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DepartamentoExpressionBuilder : SqlExpressionBuilder<DepartamentoColumn>
	{
	}
	
	#endregion DepartamentoExpressionBuilder	

	#region DepartamentoProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;DepartamentoChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Departamento"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DepartamentoProperty : ChildEntityProperty<DepartamentoChildEntityTypes>
	{
	}
	
	#endregion DepartamentoProperty
}

