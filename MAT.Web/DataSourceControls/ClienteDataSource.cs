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
	/// Represents the DataRepository.ClienteProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ClienteDataSourceDesigner))]
	public class ClienteDataSource : ProviderDataSource<Cliente, ClienteKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ClienteDataSource class.
		/// </summary>
		public ClienteDataSource() : base(new ClienteService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ClienteDataSourceView used by the ClienteDataSource.
		/// </summary>
		protected ClienteDataSourceView ClienteView
		{
			get { return ( View as ClienteDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ClienteDataSource control invokes to retrieve data.
		/// </summary>
		public ClienteSelectMethod SelectMethod
		{
			get
			{
				ClienteSelectMethod selectMethod = ClienteSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ClienteSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ClienteDataSourceView class that is to be
		/// used by the ClienteDataSource.
		/// </summary>
		/// <returns>An instance of the ClienteDataSourceView class.</returns>
		protected override BaseDataSourceView<Cliente, ClienteKey> GetNewDataSourceView()
		{
			return new ClienteDataSourceView(this, DefaultViewName);
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
	/// Supports the ClienteDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ClienteDataSourceView : ProviderDataSourceView<Cliente, ClienteKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ClienteDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ClienteDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ClienteDataSourceView(ClienteDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ClienteDataSource ClienteOwner
		{
			get { return Owner as ClienteDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ClienteSelectMethod SelectMethod
		{
			get { return ClienteOwner.SelectMethod; }
			set { ClienteOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ClienteService ClienteProvider
		{
			get { return Provider as ClienteService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Cliente> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Cliente> results = null;
			Cliente item;
			count = 0;
			
			System.Guid _clienteId;

			switch ( SelectMethod )
			{
				case ClienteSelectMethod.Get:
					ClienteKey entityKey  = new ClienteKey();
					entityKey.Load(values);
					item = ClienteProvider.Get(entityKey);
					results = new TList<Cliente>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ClienteSelectMethod.GetAll:
                    results = ClienteProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case ClienteSelectMethod.GetPaged:
					results = ClienteProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ClienteSelectMethod.Find:
					if ( FilterParameters != null )
						results = ClienteProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ClienteProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ClienteSelectMethod.GetByClienteId:
					_clienteId = ( values["ClienteId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ClienteId"], typeof(System.Guid)) : Guid.Empty;
					item = ClienteProvider.GetByClienteId(_clienteId);
					results = new TList<Cliente>();
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
			if ( SelectMethod == ClienteSelectMethod.Get || SelectMethod == ClienteSelectMethod.GetByClienteId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Cliente entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.ClienteId == Guid.Empty )
				entity.ClienteId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Cliente entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					ClienteProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Cliente> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			ClienteProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ClienteDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ClienteDataSource class.
	/// </summary>
	public class ClienteDataSourceDesigner : ProviderDataSourceDesigner<Cliente, ClienteKey>
	{
		/// <summary>
		/// Initializes a new instance of the ClienteDataSourceDesigner class.
		/// </summary>
		public ClienteDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ClienteSelectMethod SelectMethod
		{
			get { return ((ClienteDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ClienteDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ClienteDataSourceActionList

	/// <summary>
	/// Supports the ClienteDataSourceDesigner class.
	/// </summary>
	internal class ClienteDataSourceActionList : DesignerActionList
	{
		private ClienteDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ClienteDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ClienteDataSourceActionList(ClienteDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ClienteSelectMethod SelectMethod
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

	#endregion ClienteDataSourceActionList
	
	#endregion ClienteDataSourceDesigner
	
	#region ClienteSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ClienteDataSource.SelectMethod property.
	/// </summary>
	public enum ClienteSelectMethod
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
		/// Represents the GetByClienteId method.
		/// </summary>
		GetByClienteId
	}
	
	#endregion ClienteSelectMethod

	#region ClienteFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ClienteFilter : SqlFilter<ClienteColumn>
	{
	}
	
	#endregion ClienteFilter

	#region ClienteExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Cliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ClienteExpressionBuilder : SqlExpressionBuilder<ClienteColumn>
	{
	}
	
	#endregion ClienteExpressionBuilder	

	#region ClienteProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ClienteChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Cliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ClienteProperty : ChildEntityProperty<ClienteChildEntityTypes>
	{
	}
	
	#endregion ClienteProperty
}

