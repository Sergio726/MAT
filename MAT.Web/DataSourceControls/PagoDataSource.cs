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
	/// Represents the DataRepository.PagoProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PagoDataSourceDesigner))]
	public class PagoDataSource : ProviderDataSource<Pago, PagoKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PagoDataSource class.
		/// </summary>
		public PagoDataSource() : base(new PagoService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PagoDataSourceView used by the PagoDataSource.
		/// </summary>
		protected PagoDataSourceView PagoView
		{
			get { return ( View as PagoDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PagoDataSource control invokes to retrieve data.
		/// </summary>
		public PagoSelectMethod SelectMethod
		{
			get
			{
				PagoSelectMethod selectMethod = PagoSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PagoSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PagoDataSourceView class that is to be
		/// used by the PagoDataSource.
		/// </summary>
		/// <returns>An instance of the PagoDataSourceView class.</returns>
		protected override BaseDataSourceView<Pago, PagoKey> GetNewDataSourceView()
		{
			return new PagoDataSourceView(this, DefaultViewName);
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
	/// Supports the PagoDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PagoDataSourceView : ProviderDataSourceView<Pago, PagoKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PagoDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PagoDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PagoDataSourceView(PagoDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PagoDataSource PagoOwner
		{
			get { return Owner as PagoDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PagoSelectMethod SelectMethod
		{
			get { return PagoOwner.SelectMethod; }
			set { PagoOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PagoService PagoProvider
		{
			get { return Provider as PagoService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Pago> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Pago> results = null;
			Pago item;
			count = 0;
			
			System.Guid _pagoId;
			System.Guid? _clienteId_nullable;
			System.Guid? _cuentaCorrienteId_nullable;
			System.Guid? _vendedorId_nullable;

			switch ( SelectMethod )
			{
				case PagoSelectMethod.Get:
					PagoKey entityKey  = new PagoKey();
					entityKey.Load(values);
					item = PagoProvider.Get(entityKey);
					results = new TList<Pago>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PagoSelectMethod.GetAll:
                    results = PagoProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PagoSelectMethod.GetPaged:
					results = PagoProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PagoSelectMethod.Find:
					if ( FilterParameters != null )
						results = PagoProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PagoProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PagoSelectMethod.GetByPagoId:
					_pagoId = ( values["PagoId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PagoId"], typeof(System.Guid)) : Guid.Empty;
					item = PagoProvider.GetByPagoId(_pagoId);
					results = new TList<Pago>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case PagoSelectMethod.GetByClienteId:
					_clienteId_nullable = (System.Guid?) EntityUtil.ChangeType(values["ClienteId"], typeof(System.Guid?));
					results = PagoProvider.GetByClienteId(_clienteId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case PagoSelectMethod.GetByCuentaCorrienteId:
					_cuentaCorrienteId_nullable = (System.Guid?) EntityUtil.ChangeType(values["CuentaCorrienteId"], typeof(System.Guid?));
					results = PagoProvider.GetByCuentaCorrienteId(_cuentaCorrienteId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case PagoSelectMethod.GetByVendedorId:
					_vendedorId_nullable = (System.Guid?) EntityUtil.ChangeType(values["VendedorId"], typeof(System.Guid?));
					results = PagoProvider.GetByVendedorId(_vendedorId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == PagoSelectMethod.Get || SelectMethod == PagoSelectMethod.GetByPagoId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Pago entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PagoId == Guid.Empty )
				entity.PagoId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Pago entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PagoProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Pago> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PagoProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PagoDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PagoDataSource class.
	/// </summary>
	public class PagoDataSourceDesigner : ProviderDataSourceDesigner<Pago, PagoKey>
	{
		/// <summary>
		/// Initializes a new instance of the PagoDataSourceDesigner class.
		/// </summary>
		public PagoDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PagoSelectMethod SelectMethod
		{
			get { return ((PagoDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PagoDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PagoDataSourceActionList

	/// <summary>
	/// Supports the PagoDataSourceDesigner class.
	/// </summary>
	internal class PagoDataSourceActionList : DesignerActionList
	{
		private PagoDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PagoDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PagoDataSourceActionList(PagoDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PagoSelectMethod SelectMethod
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

	#endregion PagoDataSourceActionList
	
	#endregion PagoDataSourceDesigner
	
	#region PagoSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PagoDataSource.SelectMethod property.
	/// </summary>
	public enum PagoSelectMethod
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
		/// Represents the GetByPagoId method.
		/// </summary>
		GetByPagoId,
		/// <summary>
		/// Represents the GetByClienteId method.
		/// </summary>
		GetByClienteId,
		/// <summary>
		/// Represents the GetByCuentaCorrienteId method.
		/// </summary>
		GetByCuentaCorrienteId,
		/// <summary>
		/// Represents the GetByVendedorId method.
		/// </summary>
		GetByVendedorId
	}
	
	#endregion PagoSelectMethod

	#region PagoFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pago"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PagoFilter : SqlFilter<PagoColumn>
	{
	}
	
	#endregion PagoFilter

	#region PagoExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pago"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PagoExpressionBuilder : SqlExpressionBuilder<PagoColumn>
	{
	}
	
	#endregion PagoExpressionBuilder	

	#region PagoProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PagoChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Pago"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PagoProperty : ChildEntityProperty<PagoChildEntityTypes>
	{
	}
	
	#endregion PagoProperty
}

