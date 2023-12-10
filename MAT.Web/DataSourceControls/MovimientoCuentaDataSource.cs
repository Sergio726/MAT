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
	/// Represents the DataRepository.MovimientoCuentaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(MovimientoCuentaDataSourceDesigner))]
	public class MovimientoCuentaDataSource : ProviderDataSource<MovimientoCuenta, MovimientoCuentaKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaDataSource class.
		/// </summary>
		public MovimientoCuentaDataSource() : base(new MovimientoCuentaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the MovimientoCuentaDataSourceView used by the MovimientoCuentaDataSource.
		/// </summary>
		protected MovimientoCuentaDataSourceView MovimientoCuentaView
		{
			get { return ( View as MovimientoCuentaDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the MovimientoCuentaDataSource control invokes to retrieve data.
		/// </summary>
		public MovimientoCuentaSelectMethod SelectMethod
		{
			get
			{
				MovimientoCuentaSelectMethod selectMethod = MovimientoCuentaSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (MovimientoCuentaSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the MovimientoCuentaDataSourceView class that is to be
		/// used by the MovimientoCuentaDataSource.
		/// </summary>
		/// <returns>An instance of the MovimientoCuentaDataSourceView class.</returns>
		protected override BaseDataSourceView<MovimientoCuenta, MovimientoCuentaKey> GetNewDataSourceView()
		{
			return new MovimientoCuentaDataSourceView(this, DefaultViewName);
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
	/// Supports the MovimientoCuentaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class MovimientoCuentaDataSourceView : ProviderDataSourceView<MovimientoCuenta, MovimientoCuentaKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the MovimientoCuentaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public MovimientoCuentaDataSourceView(MovimientoCuentaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal MovimientoCuentaDataSource MovimientoCuentaOwner
		{
			get { return Owner as MovimientoCuentaDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal MovimientoCuentaSelectMethod SelectMethod
		{
			get { return MovimientoCuentaOwner.SelectMethod; }
			set { MovimientoCuentaOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal MovimientoCuentaService MovimientoCuentaProvider
		{
			get { return Provider as MovimientoCuentaService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<MovimientoCuenta> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<MovimientoCuenta> results = null;
			MovimientoCuenta item;
			count = 0;
			
			System.Guid _movimientoId;
			System.Guid _cuentaId;
			System.Guid? _cuentaCorrienteId_nullable;
			System.Guid? _debitoId_nullable;
			System.Guid _facturaId;
			System.Guid? _notaId_nullable;
			System.Guid? _pagoId_nullable;

			switch ( SelectMethod )
			{
				case MovimientoCuentaSelectMethod.Get:
					MovimientoCuentaKey entityKey  = new MovimientoCuentaKey();
					entityKey.Load(values);
					item = MovimientoCuentaProvider.Get(entityKey);
					results = new TList<MovimientoCuenta>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case MovimientoCuentaSelectMethod.GetAll:
                    results = MovimientoCuentaProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case MovimientoCuentaSelectMethod.GetPaged:
					results = MovimientoCuentaProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case MovimientoCuentaSelectMethod.Find:
					if ( FilterParameters != null )
						results = MovimientoCuentaProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = MovimientoCuentaProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case MovimientoCuentaSelectMethod.GetByMovimientoId:
					_movimientoId = ( values["MovimientoId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["MovimientoId"], typeof(System.Guid)) : Guid.Empty;
					item = MovimientoCuentaProvider.GetByMovimientoId(_movimientoId);
					results = new TList<MovimientoCuenta>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case MovimientoCuentaSelectMethod.GetByCuentaId:
					_cuentaId = ( values["CuentaId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["CuentaId"], typeof(System.Guid)) : Guid.Empty;
					results = MovimientoCuentaProvider.GetByCuentaId(_cuentaId, this.StartIndex, this.PageSize, out count);
					break;
				case MovimientoCuentaSelectMethod.GetByCuentaCorrienteId:
					_cuentaCorrienteId_nullable = (System.Guid?) EntityUtil.ChangeType(values["CuentaCorrienteId"], typeof(System.Guid?));
					results = MovimientoCuentaProvider.GetByCuentaCorrienteId(_cuentaCorrienteId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case MovimientoCuentaSelectMethod.GetByDebitoId:
					_debitoId_nullable = (System.Guid?) EntityUtil.ChangeType(values["DebitoId"], typeof(System.Guid?));
					results = MovimientoCuentaProvider.GetByDebitoId(_debitoId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case MovimientoCuentaSelectMethod.GetByFacturaId:
					_facturaId = ( values["FacturaId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["FacturaId"], typeof(System.Guid)) : Guid.Empty;
					results = MovimientoCuentaProvider.GetByFacturaId(_facturaId, this.StartIndex, this.PageSize, out count);
					break;
				case MovimientoCuentaSelectMethod.GetByNotaId:
					_notaId_nullable = (System.Guid?) EntityUtil.ChangeType(values["NotaId"], typeof(System.Guid?));
					results = MovimientoCuentaProvider.GetByNotaId(_notaId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case MovimientoCuentaSelectMethod.GetByPagoId:
					_pagoId_nullable = (System.Guid?) EntityUtil.ChangeType(values["PagoId"], typeof(System.Guid?));
					results = MovimientoCuentaProvider.GetByPagoId(_pagoId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == MovimientoCuentaSelectMethod.Get || SelectMethod == MovimientoCuentaSelectMethod.GetByMovimientoId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(MovimientoCuenta entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.MovimientoId == Guid.Empty )
				entity.MovimientoId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				MovimientoCuenta entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					MovimientoCuentaProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<MovimientoCuenta> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			MovimientoCuentaProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region MovimientoCuentaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the MovimientoCuentaDataSource class.
	/// </summary>
	public class MovimientoCuentaDataSourceDesigner : ProviderDataSourceDesigner<MovimientoCuenta, MovimientoCuentaKey>
	{
		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaDataSourceDesigner class.
		/// </summary>
		public MovimientoCuentaDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MovimientoCuentaSelectMethod SelectMethod
		{
			get { return ((MovimientoCuentaDataSource) DataSource).SelectMethod; }
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
				actions.Add(new MovimientoCuentaDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region MovimientoCuentaDataSourceActionList

	/// <summary>
	/// Supports the MovimientoCuentaDataSourceDesigner class.
	/// </summary>
	internal class MovimientoCuentaDataSourceActionList : DesignerActionList
	{
		private MovimientoCuentaDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the MovimientoCuentaDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public MovimientoCuentaDataSourceActionList(MovimientoCuentaDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MovimientoCuentaSelectMethod SelectMethod
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

	#endregion MovimientoCuentaDataSourceActionList
	
	#endregion MovimientoCuentaDataSourceDesigner
	
	#region MovimientoCuentaSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the MovimientoCuentaDataSource.SelectMethod property.
	/// </summary>
	public enum MovimientoCuentaSelectMethod
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
		/// Represents the GetByMovimientoId method.
		/// </summary>
		GetByMovimientoId,
		/// <summary>
		/// Represents the GetByCuentaId method.
		/// </summary>
		GetByCuentaId,
		/// <summary>
		/// Represents the GetByCuentaCorrienteId method.
		/// </summary>
		GetByCuentaCorrienteId,
		/// <summary>
		/// Represents the GetByDebitoId method.
		/// </summary>
		GetByDebitoId,
		/// <summary>
		/// Represents the GetByFacturaId method.
		/// </summary>
		GetByFacturaId,
		/// <summary>
		/// Represents the GetByNotaId method.
		/// </summary>
		GetByNotaId,
		/// <summary>
		/// Represents the GetByPagoId method.
		/// </summary>
		GetByPagoId
	}
	
	#endregion MovimientoCuentaSelectMethod

	#region MovimientoCuentaFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MovimientoCuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MovimientoCuentaFilter : SqlFilter<MovimientoCuentaColumn>
	{
	}
	
	#endregion MovimientoCuentaFilter

	#region MovimientoCuentaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MovimientoCuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MovimientoCuentaExpressionBuilder : SqlExpressionBuilder<MovimientoCuentaColumn>
	{
	}
	
	#endregion MovimientoCuentaExpressionBuilder	

	#region MovimientoCuentaProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;MovimientoCuentaChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="MovimientoCuenta"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MovimientoCuentaProperty : ChildEntityProperty<MovimientoCuentaChildEntityTypes>
	{
	}
	
	#endregion MovimientoCuentaProperty
}

