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
	/// Represents the DataRepository.VendedorProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(VendedorDataSourceDesigner))]
	public class VendedorDataSource : ProviderDataSource<Vendedor, VendedorKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VendedorDataSource class.
		/// </summary>
		public VendedorDataSource() : base(new VendedorService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the VendedorDataSourceView used by the VendedorDataSource.
		/// </summary>
		protected VendedorDataSourceView VendedorView
		{
			get { return ( View as VendedorDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the VendedorDataSource control invokes to retrieve data.
		/// </summary>
		public VendedorSelectMethod SelectMethod
		{
			get
			{
				VendedorSelectMethod selectMethod = VendedorSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (VendedorSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the VendedorDataSourceView class that is to be
		/// used by the VendedorDataSource.
		/// </summary>
		/// <returns>An instance of the VendedorDataSourceView class.</returns>
		protected override BaseDataSourceView<Vendedor, VendedorKey> GetNewDataSourceView()
		{
			return new VendedorDataSourceView(this, DefaultViewName);
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
	/// Supports the VendedorDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class VendedorDataSourceView : ProviderDataSourceView<Vendedor, VendedorKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VendedorDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the VendedorDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public VendedorDataSourceView(VendedorDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal VendedorDataSource VendedorOwner
		{
			get { return Owner as VendedorDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal VendedorSelectMethod SelectMethod
		{
			get { return VendedorOwner.SelectMethod; }
			set { VendedorOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal VendedorService VendedorProvider
		{
			get { return Provider as VendedorService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Vendedor> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Vendedor> results = null;
			Vendedor item;
			count = 0;
			
			System.Guid _vendedorId;

			switch ( SelectMethod )
			{
				case VendedorSelectMethod.Get:
					VendedorKey entityKey  = new VendedorKey();
					entityKey.Load(values);
					item = VendedorProvider.Get(entityKey);
					results = new TList<Vendedor>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case VendedorSelectMethod.GetAll:
                    results = VendedorProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case VendedorSelectMethod.GetPaged:
					results = VendedorProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case VendedorSelectMethod.Find:
					if ( FilterParameters != null )
						results = VendedorProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = VendedorProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case VendedorSelectMethod.GetByVendedorId:
					_vendedorId = ( values["VendedorId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["VendedorId"], typeof(System.Guid)) : Guid.Empty;
					item = VendedorProvider.GetByVendedorId(_vendedorId);
					results = new TList<Vendedor>();
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
			if ( SelectMethod == VendedorSelectMethod.Get || SelectMethod == VendedorSelectMethod.GetByVendedorId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Vendedor entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.VendedorId == Guid.Empty )
				entity.VendedorId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Vendedor entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					VendedorProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Vendedor> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			VendedorProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region VendedorDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the VendedorDataSource class.
	/// </summary>
	public class VendedorDataSourceDesigner : ProviderDataSourceDesigner<Vendedor, VendedorKey>
	{
		/// <summary>
		/// Initializes a new instance of the VendedorDataSourceDesigner class.
		/// </summary>
		public VendedorDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public VendedorSelectMethod SelectMethod
		{
			get { return ((VendedorDataSource) DataSource).SelectMethod; }
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
				actions.Add(new VendedorDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region VendedorDataSourceActionList

	/// <summary>
	/// Supports the VendedorDataSourceDesigner class.
	/// </summary>
	internal class VendedorDataSourceActionList : DesignerActionList
	{
		private VendedorDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the VendedorDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public VendedorDataSourceActionList(VendedorDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public VendedorSelectMethod SelectMethod
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

	#endregion VendedorDataSourceActionList
	
	#endregion VendedorDataSourceDesigner
	
	#region VendedorSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the VendedorDataSource.SelectMethod property.
	/// </summary>
	public enum VendedorSelectMethod
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
		/// Represents the GetByVendedorId method.
		/// </summary>
		GetByVendedorId
	}
	
	#endregion VendedorSelectMethod

	#region VendedorFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Vendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VendedorFilter : SqlFilter<VendedorColumn>
	{
	}
	
	#endregion VendedorFilter

	#region VendedorExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Vendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VendedorExpressionBuilder : SqlExpressionBuilder<VendedorColumn>
	{
	}
	
	#endregion VendedorExpressionBuilder	

	#region VendedorProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;VendedorChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Vendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VendedorProperty : ChildEntityProperty<VendedorChildEntityTypes>
	{
	}
	
	#endregion VendedorProperty
}

