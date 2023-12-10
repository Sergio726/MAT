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
	/// Represents the DataRepository.PasajeProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PasajeDataSourceDesigner))]
	public class PasajeDataSource : ProviderDataSource<Pasaje, PasajeKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeDataSource class.
		/// </summary>
		public PasajeDataSource() : base(new PasajeService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PasajeDataSourceView used by the PasajeDataSource.
		/// </summary>
		protected PasajeDataSourceView PasajeView
		{
			get { return ( View as PasajeDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PasajeDataSource control invokes to retrieve data.
		/// </summary>
		public PasajeSelectMethod SelectMethod
		{
			get
			{
				PasajeSelectMethod selectMethod = PasajeSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PasajeSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PasajeDataSourceView class that is to be
		/// used by the PasajeDataSource.
		/// </summary>
		/// <returns>An instance of the PasajeDataSourceView class.</returns>
		protected override BaseDataSourceView<Pasaje, PasajeKey> GetNewDataSourceView()
		{
			return new PasajeDataSourceView(this, DefaultViewName);
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
	/// Supports the PasajeDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PasajeDataSourceView : ProviderDataSourceView<Pasaje, PasajeKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PasajeDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PasajeDataSourceView(PasajeDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PasajeDataSource PasajeOwner
		{
			get { return Owner as PasajeDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PasajeSelectMethod SelectMethod
		{
			get { return PasajeOwner.SelectMethod; }
			set { PasajeOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PasajeService PasajeProvider
		{
			get { return Provider as PasajeService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Pasaje> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Pasaje> results = null;
			Pasaje item;
			count = 0;
			
			System.Guid? _facturaId_nullable;
			System.Guid _pasajeId;
			System.Guid? _butacaId_nullable;
			System.Guid? _pasajeroId_nullable;
			System.Guid? _precioId_nullable;
			System.Guid? _viajeId_nullable;
			System.Guid? _voucherId_nullable;
			System.Int32 _estadoPasaje;

			switch ( SelectMethod )
			{
				case PasajeSelectMethod.Get:
					PasajeKey entityKey  = new PasajeKey();
					entityKey.Load(values);
					item = PasajeProvider.Get(entityKey);
					results = new TList<Pasaje>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PasajeSelectMethod.GetAll:
                    results = PasajeProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PasajeSelectMethod.GetPaged:
					results = PasajeProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PasajeSelectMethod.Find:
					if ( FilterParameters != null )
						results = PasajeProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PasajeProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PasajeSelectMethod.GetByPasajeId:
					_pasajeId = ( values["PasajeId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PasajeId"], typeof(System.Guid)) : Guid.Empty;
					item = PasajeProvider.GetByPasajeId(_pasajeId);
					results = new TList<Pasaje>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case PasajeSelectMethod.GetByFacturaId:
					_facturaId_nullable = (System.Guid?) EntityUtil.ChangeType(values["FacturaId"], typeof(System.Guid?));
					results = PasajeProvider.GetByFacturaId(_facturaId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				case PasajeSelectMethod.GetByButacaId:
					_butacaId_nullable = (System.Guid?) EntityUtil.ChangeType(values["ButacaId"], typeof(System.Guid?));
					results = PasajeProvider.GetByButacaId(_butacaId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case PasajeSelectMethod.GetByPasajeroId:
					_pasajeroId_nullable = (System.Guid?) EntityUtil.ChangeType(values["PasajeroId"], typeof(System.Guid?));
					results = PasajeProvider.GetByPasajeroId(_pasajeroId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case PasajeSelectMethod.GetByPrecioId:
					_precioId_nullable = (System.Guid?) EntityUtil.ChangeType(values["PrecioId"], typeof(System.Guid?));
					results = PasajeProvider.GetByPrecioId(_precioId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case PasajeSelectMethod.GetByViajeId:
					_viajeId_nullable = (System.Guid?) EntityUtil.ChangeType(values["ViajeId"], typeof(System.Guid?));
					results = PasajeProvider.GetByViajeId(_viajeId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case PasajeSelectMethod.GetByVoucherId:
					_voucherId_nullable = (System.Guid?) EntityUtil.ChangeType(values["VoucherId"], typeof(System.Guid?));
					results = PasajeProvider.GetByVoucherId(_voucherId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case PasajeSelectMethod.GetByEstadoPasaje:
					_estadoPasaje = ( values["EstadoPasaje"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["EstadoPasaje"], typeof(System.Int32)) : (int)0;
					results = PasajeProvider.GetByEstadoPasaje(_estadoPasaje, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == PasajeSelectMethod.Get || SelectMethod == PasajeSelectMethod.GetByPasajeId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Pasaje entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PasajeId == Guid.Empty )
				entity.PasajeId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Pasaje entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PasajeProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Pasaje> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PasajeProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PasajeDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PasajeDataSource class.
	/// </summary>
	public class PasajeDataSourceDesigner : ProviderDataSourceDesigner<Pasaje, PasajeKey>
	{
		/// <summary>
		/// Initializes a new instance of the PasajeDataSourceDesigner class.
		/// </summary>
		public PasajeDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PasajeSelectMethod SelectMethod
		{
			get { return ((PasajeDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PasajeDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PasajeDataSourceActionList

	/// <summary>
	/// Supports the PasajeDataSourceDesigner class.
	/// </summary>
	internal class PasajeDataSourceActionList : DesignerActionList
	{
		private PasajeDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PasajeDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PasajeDataSourceActionList(PasajeDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PasajeSelectMethod SelectMethod
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

	#endregion PasajeDataSourceActionList
	
	#endregion PasajeDataSourceDesigner
	
	#region PasajeSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PasajeDataSource.SelectMethod property.
	/// </summary>
	public enum PasajeSelectMethod
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
		/// Represents the GetByPasajeId method.
		/// </summary>
		GetByPasajeId,
		/// <summary>
		/// Represents the GetByButacaId method.
		/// </summary>
		GetByButacaId,
		/// <summary>
		/// Represents the GetByPasajeroId method.
		/// </summary>
		GetByPasajeroId,
		/// <summary>
		/// Represents the GetByPrecioId method.
		/// </summary>
		GetByPrecioId,
		/// <summary>
		/// Represents the GetByViajeId method.
		/// </summary>
		GetByViajeId,
		/// <summary>
		/// Represents the GetByVoucherId method.
		/// </summary>
		GetByVoucherId,
		/// <summary>
		/// Represents the GetByEstadoPasaje method.
		/// </summary>
		GetByEstadoPasaje
	}
	
	#endregion PasajeSelectMethod

	#region PasajeFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeFilter : SqlFilter<PasajeColumn>
	{
	}
	
	#endregion PasajeFilter

	#region PasajeExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeExpressionBuilder : SqlExpressionBuilder<PasajeColumn>
	{
	}
	
	#endregion PasajeExpressionBuilder	

	#region PasajeProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PasajeChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Pasaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeProperty : ChildEntityProperty<PasajeChildEntityTypes>
	{
	}
	
	#endregion PasajeProperty
}

