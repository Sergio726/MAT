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
	/// Represents the DataRepository.ProveedorProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ProveedorDataSourceDesigner))]
	public class ProveedorDataSource : ProviderDataSource<Proveedor, ProveedorKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProveedorDataSource class.
		/// </summary>
		public ProveedorDataSource() : base(new ProveedorService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ProveedorDataSourceView used by the ProveedorDataSource.
		/// </summary>
		protected ProveedorDataSourceView ProveedorView
		{
			get { return ( View as ProveedorDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ProveedorDataSource control invokes to retrieve data.
		/// </summary>
		public ProveedorSelectMethod SelectMethod
		{
			get
			{
				ProveedorSelectMethod selectMethod = ProveedorSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ProveedorSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ProveedorDataSourceView class that is to be
		/// used by the ProveedorDataSource.
		/// </summary>
		/// <returns>An instance of the ProveedorDataSourceView class.</returns>
		protected override BaseDataSourceView<Proveedor, ProveedorKey> GetNewDataSourceView()
		{
			return new ProveedorDataSourceView(this, DefaultViewName);
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
	/// Supports the ProveedorDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ProveedorDataSourceView : ProviderDataSourceView<Proveedor, ProveedorKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProveedorDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ProveedorDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ProveedorDataSourceView(ProveedorDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ProveedorDataSource ProveedorOwner
		{
			get { return Owner as ProveedorDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ProveedorSelectMethod SelectMethod
		{
			get { return ProveedorOwner.SelectMethod; }
			set { ProveedorOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ProveedorService ProveedorProvider
		{
			get { return Provider as ProveedorService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Proveedor> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Proveedor> results = null;
			Proveedor item;
			count = 0;
			
			System.Guid _proveedorId;

			switch ( SelectMethod )
			{
				case ProveedorSelectMethod.Get:
					ProveedorKey entityKey  = new ProveedorKey();
					entityKey.Load(values);
					item = ProveedorProvider.Get(entityKey);
					results = new TList<Proveedor>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ProveedorSelectMethod.GetAll:
                    results = ProveedorProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case ProveedorSelectMethod.GetPaged:
					results = ProveedorProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ProveedorSelectMethod.Find:
					if ( FilterParameters != null )
						results = ProveedorProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ProveedorProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ProveedorSelectMethod.GetByProveedorId:
					_proveedorId = ( values["ProveedorId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ProveedorId"], typeof(System.Guid)) : Guid.Empty;
					item = ProveedorProvider.GetByProveedorId(_proveedorId);
					results = new TList<Proveedor>();
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
			if ( SelectMethod == ProveedorSelectMethod.Get || SelectMethod == ProveedorSelectMethod.GetByProveedorId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Proveedor entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.ProveedorId == Guid.Empty )
				entity.ProveedorId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Proveedor entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					ProveedorProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Proveedor> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			ProveedorProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ProveedorDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ProveedorDataSource class.
	/// </summary>
	public class ProveedorDataSourceDesigner : ProviderDataSourceDesigner<Proveedor, ProveedorKey>
	{
		/// <summary>
		/// Initializes a new instance of the ProveedorDataSourceDesigner class.
		/// </summary>
		public ProveedorDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ProveedorSelectMethod SelectMethod
		{
			get { return ((ProveedorDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ProveedorDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ProveedorDataSourceActionList

	/// <summary>
	/// Supports the ProveedorDataSourceDesigner class.
	/// </summary>
	internal class ProveedorDataSourceActionList : DesignerActionList
	{
		private ProveedorDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ProveedorDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ProveedorDataSourceActionList(ProveedorDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ProveedorSelectMethod SelectMethod
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

	#endregion ProveedorDataSourceActionList
	
	#endregion ProveedorDataSourceDesigner
	
	#region ProveedorSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ProveedorDataSource.SelectMethod property.
	/// </summary>
	public enum ProveedorSelectMethod
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
		/// Represents the GetByProveedorId method.
		/// </summary>
		GetByProveedorId
	}
	
	#endregion ProveedorSelectMethod

	#region ProveedorFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Proveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProveedorFilter : SqlFilter<ProveedorColumn>
	{
	}
	
	#endregion ProveedorFilter

	#region ProveedorExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Proveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProveedorExpressionBuilder : SqlExpressionBuilder<ProveedorColumn>
	{
	}
	
	#endregion ProveedorExpressionBuilder	

	#region ProveedorProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ProveedorChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Proveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProveedorProperty : ChildEntityProperty<ProveedorChildEntityTypes>
	{
	}
	
	#endregion ProveedorProperty
}

