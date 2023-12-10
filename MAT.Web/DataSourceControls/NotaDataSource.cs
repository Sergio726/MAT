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
	/// Represents the DataRepository.NotaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(NotaDataSourceDesigner))]
	public class NotaDataSource : ProviderDataSource<Nota, NotaKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NotaDataSource class.
		/// </summary>
		public NotaDataSource() : base(new NotaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the NotaDataSourceView used by the NotaDataSource.
		/// </summary>
		protected NotaDataSourceView NotaView
		{
			get { return ( View as NotaDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the NotaDataSource control invokes to retrieve data.
		/// </summary>
		public NotaSelectMethod SelectMethod
		{
			get
			{
				NotaSelectMethod selectMethod = NotaSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (NotaSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the NotaDataSourceView class that is to be
		/// used by the NotaDataSource.
		/// </summary>
		/// <returns>An instance of the NotaDataSourceView class.</returns>
		protected override BaseDataSourceView<Nota, NotaKey> GetNewDataSourceView()
		{
			return new NotaDataSourceView(this, DefaultViewName);
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
	/// Supports the NotaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class NotaDataSourceView : ProviderDataSourceView<Nota, NotaKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NotaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the NotaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public NotaDataSourceView(NotaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal NotaDataSource NotaOwner
		{
			get { return Owner as NotaDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal NotaSelectMethod SelectMethod
		{
			get { return NotaOwner.SelectMethod; }
			set { NotaOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal NotaService NotaProvider
		{
			get { return Provider as NotaService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Nota> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Nota> results = null;
			Nota item;
			count = 0;
			
			System.Guid _notaId;
			System.Guid? _clienteId_nullable;
			System.Guid? _vendedorId_nullable;

			switch ( SelectMethod )
			{
				case NotaSelectMethod.Get:
					NotaKey entityKey  = new NotaKey();
					entityKey.Load(values);
					item = NotaProvider.Get(entityKey);
					results = new TList<Nota>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case NotaSelectMethod.GetAll:
                    results = NotaProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case NotaSelectMethod.GetPaged:
					results = NotaProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case NotaSelectMethod.Find:
					if ( FilterParameters != null )
						results = NotaProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = NotaProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case NotaSelectMethod.GetByNotaId:
					_notaId = ( values["NotaId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["NotaId"], typeof(System.Guid)) : Guid.Empty;
					item = NotaProvider.GetByNotaId(_notaId);
					results = new TList<Nota>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case NotaSelectMethod.GetByClienteId:
					_clienteId_nullable = (System.Guid?) EntityUtil.ChangeType(values["ClienteId"], typeof(System.Guid?));
					results = NotaProvider.GetByClienteId(_clienteId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case NotaSelectMethod.GetByVendedorId:
					_vendedorId_nullable = (System.Guid?) EntityUtil.ChangeType(values["VendedorId"], typeof(System.Guid?));
					results = NotaProvider.GetByVendedorId(_vendedorId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == NotaSelectMethod.Get || SelectMethod == NotaSelectMethod.GetByNotaId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Nota entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.NotaId == Guid.Empty )
				entity.NotaId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Nota entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					NotaProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Nota> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			NotaProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region NotaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the NotaDataSource class.
	/// </summary>
	public class NotaDataSourceDesigner : ProviderDataSourceDesigner<Nota, NotaKey>
	{
		/// <summary>
		/// Initializes a new instance of the NotaDataSourceDesigner class.
		/// </summary>
		public NotaDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public NotaSelectMethod SelectMethod
		{
			get { return ((NotaDataSource) DataSource).SelectMethod; }
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
				actions.Add(new NotaDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region NotaDataSourceActionList

	/// <summary>
	/// Supports the NotaDataSourceDesigner class.
	/// </summary>
	internal class NotaDataSourceActionList : DesignerActionList
	{
		private NotaDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the NotaDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public NotaDataSourceActionList(NotaDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public NotaSelectMethod SelectMethod
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

	#endregion NotaDataSourceActionList
	
	#endregion NotaDataSourceDesigner
	
	#region NotaSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the NotaDataSource.SelectMethod property.
	/// </summary>
	public enum NotaSelectMethod
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
		/// Represents the GetByNotaId method.
		/// </summary>
		GetByNotaId,
		/// <summary>
		/// Represents the GetByClienteId method.
		/// </summary>
		GetByClienteId,
		/// <summary>
		/// Represents the GetByVendedorId method.
		/// </summary>
		GetByVendedorId
	}
	
	#endregion NotaSelectMethod

	#region NotaFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Nota"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NotaFilter : SqlFilter<NotaColumn>
	{
	}
	
	#endregion NotaFilter

	#region NotaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Nota"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NotaExpressionBuilder : SqlExpressionBuilder<NotaColumn>
	{
	}
	
	#endregion NotaExpressionBuilder	

	#region NotaProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;NotaChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Nota"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NotaProperty : ChildEntityProperty<NotaChildEntityTypes>
	{
	}
	
	#endregion NotaProperty
}

