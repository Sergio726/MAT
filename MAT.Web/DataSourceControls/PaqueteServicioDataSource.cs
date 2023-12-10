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
	/// Represents the DataRepository.PaqueteServicioProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PaqueteServicioDataSourceDesigner))]
	public class PaqueteServicioDataSource : ProviderDataSource<PaqueteServicio, PaqueteServicioKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioDataSource class.
		/// </summary>
		public PaqueteServicioDataSource() : base(new PaqueteServicioService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PaqueteServicioDataSourceView used by the PaqueteServicioDataSource.
		/// </summary>
		protected PaqueteServicioDataSourceView PaqueteServicioView
		{
			get { return ( View as PaqueteServicioDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PaqueteServicioDataSource control invokes to retrieve data.
		/// </summary>
		public PaqueteServicioSelectMethod SelectMethod
		{
			get
			{
				PaqueteServicioSelectMethod selectMethod = PaqueteServicioSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PaqueteServicioSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PaqueteServicioDataSourceView class that is to be
		/// used by the PaqueteServicioDataSource.
		/// </summary>
		/// <returns>An instance of the PaqueteServicioDataSourceView class.</returns>
		protected override BaseDataSourceView<PaqueteServicio, PaqueteServicioKey> GetNewDataSourceView()
		{
			return new PaqueteServicioDataSourceView(this, DefaultViewName);
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
	/// Supports the PaqueteServicioDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PaqueteServicioDataSourceView : ProviderDataSourceView<PaqueteServicio, PaqueteServicioKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PaqueteServicioDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PaqueteServicioDataSourceView(PaqueteServicioDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PaqueteServicioDataSource PaqueteServicioOwner
		{
			get { return Owner as PaqueteServicioDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PaqueteServicioSelectMethod SelectMethod
		{
			get { return PaqueteServicioOwner.SelectMethod; }
			set { PaqueteServicioOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PaqueteServicioService PaqueteServicioProvider
		{
			get { return Provider as PaqueteServicioService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<PaqueteServicio> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<PaqueteServicio> results = null;
			PaqueteServicio item;
			count = 0;
			
			System.Guid _paqueteServicioId;
			System.Guid? _paqueteId_nullable;
			System.Guid? _servicioId_nullable;

			switch ( SelectMethod )
			{
				case PaqueteServicioSelectMethod.Get:
					PaqueteServicioKey entityKey  = new PaqueteServicioKey();
					entityKey.Load(values);
					item = PaqueteServicioProvider.Get(entityKey);
					results = new TList<PaqueteServicio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PaqueteServicioSelectMethod.GetAll:
                    results = PaqueteServicioProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PaqueteServicioSelectMethod.GetPaged:
					results = PaqueteServicioProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PaqueteServicioSelectMethod.Find:
					if ( FilterParameters != null )
						results = PaqueteServicioProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PaqueteServicioProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PaqueteServicioSelectMethod.GetByPaqueteServicioId:
					_paqueteServicioId = ( values["PaqueteServicioId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PaqueteServicioId"], typeof(System.Guid)) : Guid.Empty;
					item = PaqueteServicioProvider.GetByPaqueteServicioId(_paqueteServicioId);
					results = new TList<PaqueteServicio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case PaqueteServicioSelectMethod.GetByPaqueteId:
					_paqueteId_nullable = (System.Guid?) EntityUtil.ChangeType(values["PaqueteId"], typeof(System.Guid?));
					results = PaqueteServicioProvider.GetByPaqueteId(_paqueteId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case PaqueteServicioSelectMethod.GetByServicioId:
					_servicioId_nullable = (System.Guid?) EntityUtil.ChangeType(values["ServicioId"], typeof(System.Guid?));
					results = PaqueteServicioProvider.GetByServicioId(_servicioId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == PaqueteServicioSelectMethod.Get || SelectMethod == PaqueteServicioSelectMethod.GetByPaqueteServicioId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(PaqueteServicio entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PaqueteServicioId == Guid.Empty )
				entity.PaqueteServicioId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				PaqueteServicio entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PaqueteServicioProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<PaqueteServicio> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PaqueteServicioProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PaqueteServicioDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PaqueteServicioDataSource class.
	/// </summary>
	public class PaqueteServicioDataSourceDesigner : ProviderDataSourceDesigner<PaqueteServicio, PaqueteServicioKey>
	{
		/// <summary>
		/// Initializes a new instance of the PaqueteServicioDataSourceDesigner class.
		/// </summary>
		public PaqueteServicioDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PaqueteServicioSelectMethod SelectMethod
		{
			get { return ((PaqueteServicioDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PaqueteServicioDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PaqueteServicioDataSourceActionList

	/// <summary>
	/// Supports the PaqueteServicioDataSourceDesigner class.
	/// </summary>
	internal class PaqueteServicioDataSourceActionList : DesignerActionList
	{
		private PaqueteServicioDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PaqueteServicioDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PaqueteServicioDataSourceActionList(PaqueteServicioDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PaqueteServicioSelectMethod SelectMethod
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

	#endregion PaqueteServicioDataSourceActionList
	
	#endregion PaqueteServicioDataSourceDesigner
	
	#region PaqueteServicioSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PaqueteServicioDataSource.SelectMethod property.
	/// </summary>
	public enum PaqueteServicioSelectMethod
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
		/// Represents the GetByPaqueteServicioId method.
		/// </summary>
		GetByPaqueteServicioId,
		/// <summary>
		/// Represents the GetByPaqueteId method.
		/// </summary>
		GetByPaqueteId,
		/// <summary>
		/// Represents the GetByServicioId method.
		/// </summary>
		GetByServicioId
	}
	
	#endregion PaqueteServicioSelectMethod

	#region PaqueteServicioFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteServicioFilter : SqlFilter<PaqueteServicioColumn>
	{
	}
	
	#endregion PaqueteServicioFilter

	#region PaqueteServicioExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteServicioExpressionBuilder : SqlExpressionBuilder<PaqueteServicioColumn>
	{
	}
	
	#endregion PaqueteServicioExpressionBuilder	

	#region PaqueteServicioProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PaqueteServicioChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="PaqueteServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteServicioProperty : ChildEntityProperty<PaqueteServicioChildEntityTypes>
	{
	}
	
	#endregion PaqueteServicioProperty
}

