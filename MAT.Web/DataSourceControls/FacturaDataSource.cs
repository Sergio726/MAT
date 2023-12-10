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
	/// Represents the DataRepository.FacturaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(FacturaDataSourceDesigner))]
	public class FacturaDataSource : ProviderDataSource<Factura, FacturaKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FacturaDataSource class.
		/// </summary>
		public FacturaDataSource() : base(new FacturaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the FacturaDataSourceView used by the FacturaDataSource.
		/// </summary>
		protected FacturaDataSourceView FacturaView
		{
			get { return ( View as FacturaDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the FacturaDataSource control invokes to retrieve data.
		/// </summary>
		public FacturaSelectMethod SelectMethod
		{
			get
			{
				FacturaSelectMethod selectMethod = FacturaSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (FacturaSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the FacturaDataSourceView class that is to be
		/// used by the FacturaDataSource.
		/// </summary>
		/// <returns>An instance of the FacturaDataSourceView class.</returns>
		protected override BaseDataSourceView<Factura, FacturaKey> GetNewDataSourceView()
		{
			return new FacturaDataSourceView(this, DefaultViewName);
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
	/// Supports the FacturaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class FacturaDataSourceView : ProviderDataSourceView<Factura, FacturaKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FacturaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the FacturaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public FacturaDataSourceView(FacturaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal FacturaDataSource FacturaOwner
		{
			get { return Owner as FacturaDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal FacturaSelectMethod SelectMethod
		{
			get { return FacturaOwner.SelectMethod; }
			set { FacturaOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal FacturaService FacturaProvider
		{
			get { return Provider as FacturaService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Factura> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Factura> results = null;
			Factura item;
			count = 0;
			
			System.Guid _facturaId;
			System.Guid _clienteId;
			System.Guid _vendedorId;

			switch ( SelectMethod )
			{
				case FacturaSelectMethod.Get:
					FacturaKey entityKey  = new FacturaKey();
					entityKey.Load(values);
					item = FacturaProvider.Get(entityKey);
					results = new TList<Factura>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case FacturaSelectMethod.GetAll:
                    results = FacturaProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case FacturaSelectMethod.GetPaged:
					results = FacturaProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case FacturaSelectMethod.Find:
					if ( FilterParameters != null )
						results = FacturaProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = FacturaProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case FacturaSelectMethod.GetByFacturaId:
					_facturaId = ( values["FacturaId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["FacturaId"], typeof(System.Guid)) : Guid.Empty;
					item = FacturaProvider.GetByFacturaId(_facturaId);
					results = new TList<Factura>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case FacturaSelectMethod.GetByClienteId:
					_clienteId = ( values["ClienteId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ClienteId"], typeof(System.Guid)) : Guid.Empty;
					results = FacturaProvider.GetByClienteId(_clienteId, this.StartIndex, this.PageSize, out count);
					break;
				case FacturaSelectMethod.GetByVendedorId:
					_vendedorId = ( values["VendedorId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["VendedorId"], typeof(System.Guid)) : Guid.Empty;
					results = FacturaProvider.GetByVendedorId(_vendedorId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == FacturaSelectMethod.Get || SelectMethod == FacturaSelectMethod.GetByFacturaId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Factura entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.FacturaId == Guid.Empty )
				entity.FacturaId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Factura entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					FacturaProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Factura> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			FacturaProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region FacturaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the FacturaDataSource class.
	/// </summary>
	public class FacturaDataSourceDesigner : ProviderDataSourceDesigner<Factura, FacturaKey>
	{
		/// <summary>
		/// Initializes a new instance of the FacturaDataSourceDesigner class.
		/// </summary>
		public FacturaDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public FacturaSelectMethod SelectMethod
		{
			get { return ((FacturaDataSource) DataSource).SelectMethod; }
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
				actions.Add(new FacturaDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region FacturaDataSourceActionList

	/// <summary>
	/// Supports the FacturaDataSourceDesigner class.
	/// </summary>
	internal class FacturaDataSourceActionList : DesignerActionList
	{
		private FacturaDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the FacturaDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public FacturaDataSourceActionList(FacturaDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public FacturaSelectMethod SelectMethod
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

	#endregion FacturaDataSourceActionList
	
	#endregion FacturaDataSourceDesigner
	
	#region FacturaSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the FacturaDataSource.SelectMethod property.
	/// </summary>
	public enum FacturaSelectMethod
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
		/// Represents the GetByFacturaId method.
		/// </summary>
		GetByFacturaId,
		/// <summary>
		/// Represents the GetByClienteId method.
		/// </summary>
		GetByClienteId,
		/// <summary>
		/// Represents the GetByVendedorId method.
		/// </summary>
		GetByVendedorId
	}
	
	#endregion FacturaSelectMethod

	#region FacturaFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Factura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FacturaFilter : SqlFilter<FacturaColumn>
	{
	}
	
	#endregion FacturaFilter

	#region FacturaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Factura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FacturaExpressionBuilder : SqlExpressionBuilder<FacturaColumn>
	{
	}
	
	#endregion FacturaExpressionBuilder	

	#region FacturaProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;FacturaChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Factura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FacturaProperty : ChildEntityProperty<FacturaChildEntityTypes>
	{
	}
	
	#endregion FacturaProperty
}

