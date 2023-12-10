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
	/// Represents the DataRepository.CuentaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(CuentaDataSourceDesigner))]
	public class CuentaDataSource : ProviderDataSource<Cuenta, CuentaKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaDataSource class.
		/// </summary>
		public CuentaDataSource() : base(new CuentaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the CuentaDataSourceView used by the CuentaDataSource.
		/// </summary>
		protected CuentaDataSourceView CuentaView
		{
			get { return ( View as CuentaDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the CuentaDataSource control invokes to retrieve data.
		/// </summary>
		public CuentaSelectMethod SelectMethod
		{
			get
			{
				CuentaSelectMethod selectMethod = CuentaSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (CuentaSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the CuentaDataSourceView class that is to be
		/// used by the CuentaDataSource.
		/// </summary>
		/// <returns>An instance of the CuentaDataSourceView class.</returns>
		protected override BaseDataSourceView<Cuenta, CuentaKey> GetNewDataSourceView()
		{
			return new CuentaDataSourceView(this, DefaultViewName);
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
	/// Supports the CuentaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class CuentaDataSourceView : ProviderDataSourceView<Cuenta, CuentaKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CuentaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the CuentaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public CuentaDataSourceView(CuentaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal CuentaDataSource CuentaOwner
		{
			get { return Owner as CuentaDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal CuentaSelectMethod SelectMethod
		{
			get { return CuentaOwner.SelectMethod; }
			set { CuentaOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal CuentaService CuentaProvider
		{
			get { return Provider as CuentaService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Cuenta> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Cuenta> results = null;
			Cuenta item;
			count = 0;
			
			System.Guid _cuentaId;
			System.Guid _clienteId;

			switch ( SelectMethod )
			{
				case CuentaSelectMethod.Get:
					CuentaKey entityKey  = new CuentaKey();
					entityKey.Load(values);
					item = CuentaProvider.Get(entityKey);
					results = new TList<Cuenta>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case CuentaSelectMethod.GetAll:
                    results = CuentaProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case CuentaSelectMethod.GetPaged:
					results = CuentaProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case CuentaSelectMethod.Find:
					if ( FilterParameters != null )
						results = CuentaProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = CuentaProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case CuentaSelectMethod.GetByCuentaId:
					_cuentaId = ( values["CuentaId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["CuentaId"], typeof(System.Guid)) : Guid.Empty;
					item = CuentaProvider.GetByCuentaId(_cuentaId);
					results = new TList<Cuenta>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case CuentaSelectMethod.GetByClienteId:
					_clienteId = ( values["ClienteId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ClienteId"], typeof(System.Guid)) : Guid.Empty;
					results = CuentaProvider.GetByClienteId(_clienteId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == CuentaSelectMethod.Get || SelectMethod == CuentaSelectMethod.GetByCuentaId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Cuenta entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.CuentaId == Guid.Empty )
				entity.CuentaId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Cuenta entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					CuentaProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Cuenta> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			CuentaProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region CuentaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the CuentaDataSource class.
	/// </summary>
	public class CuentaDataSourceDesigner : ProviderDataSourceDesigner<Cuenta, CuentaKey>
	{
		/// <summary>
		/// Initializes a new instance of the CuentaDataSourceDesigner class.
		/// </summary>
		public CuentaDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public CuentaSelectMethod SelectMethod
		{
			get { return ((CuentaDataSource) DataSource).SelectMethod; }
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
				actions.Add(new CuentaDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region CuentaDataSourceActionList

	/// <summary>
	/// Supports the CuentaDataSourceDesigner class.
	/// </summary>
	internal class CuentaDataSourceActionList : DesignerActionList
	{
		private CuentaDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the CuentaDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public CuentaDataSourceActionList(CuentaDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public CuentaSelectMethod SelectMethod
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

	#endregion CuentaDataSourceActionList
	
	#endregion CuentaDataSourceDesigner
	
	#region CuentaSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the CuentaDataSource.SelectMethod property.
	/// </summary>
	public enum CuentaSelectMethod
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
		/// Represents the GetByCuentaId method.
		/// </summary>
		GetByCuentaId,
		/// <summary>
		/// Represents the GetByClienteId method.
		/// </summary>
		GetByClienteId
	}
	
	#endregion CuentaSelectMethod

	#region CuentaFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaFilter : SqlFilter<CuentaColumn>
	{
	}
	
	#endregion CuentaFilter

	#region CuentaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaExpressionBuilder : SqlExpressionBuilder<CuentaColumn>
	{
	}
	
	#endregion CuentaExpressionBuilder	

	#region CuentaProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;CuentaChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Cuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaProperty : ChildEntityProperty<CuentaChildEntityTypes>
	{
	}
	
	#endregion CuentaProperty
}

