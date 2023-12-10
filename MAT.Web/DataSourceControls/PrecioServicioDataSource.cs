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
	/// Represents the DataRepository.PrecioServicioProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PrecioServicioDataSourceDesigner))]
	public class PrecioServicioDataSource : ProviderDataSource<PrecioServicio, PrecioServicioKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioServicioDataSource class.
		/// </summary>
		public PrecioServicioDataSource() : base(new PrecioServicioService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PrecioServicioDataSourceView used by the PrecioServicioDataSource.
		/// </summary>
		protected PrecioServicioDataSourceView PrecioServicioView
		{
			get { return ( View as PrecioServicioDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PrecioServicioDataSource control invokes to retrieve data.
		/// </summary>
		public PrecioServicioSelectMethod SelectMethod
		{
			get
			{
				PrecioServicioSelectMethod selectMethod = PrecioServicioSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PrecioServicioSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PrecioServicioDataSourceView class that is to be
		/// used by the PrecioServicioDataSource.
		/// </summary>
		/// <returns>An instance of the PrecioServicioDataSourceView class.</returns>
		protected override BaseDataSourceView<PrecioServicio, PrecioServicioKey> GetNewDataSourceView()
		{
			return new PrecioServicioDataSourceView(this, DefaultViewName);
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
	/// Supports the PrecioServicioDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PrecioServicioDataSourceView : ProviderDataSourceView<PrecioServicio, PrecioServicioKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PrecioServicioDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PrecioServicioDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PrecioServicioDataSourceView(PrecioServicioDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PrecioServicioDataSource PrecioServicioOwner
		{
			get { return Owner as PrecioServicioDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PrecioServicioSelectMethod SelectMethod
		{
			get { return PrecioServicioOwner.SelectMethod; }
			set { PrecioServicioOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PrecioServicioService PrecioServicioProvider
		{
			get { return Provider as PrecioServicioService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<PrecioServicio> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<PrecioServicio> results = null;
			PrecioServicio item;
			count = 0;
			
			System.Guid _precioServicioId;

			switch ( SelectMethod )
			{
				case PrecioServicioSelectMethod.Get:
					PrecioServicioKey entityKey  = new PrecioServicioKey();
					entityKey.Load(values);
					item = PrecioServicioProvider.Get(entityKey);
					results = new TList<PrecioServicio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PrecioServicioSelectMethod.GetAll:
                    results = PrecioServicioProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PrecioServicioSelectMethod.GetPaged:
					results = PrecioServicioProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PrecioServicioSelectMethod.Find:
					if ( FilterParameters != null )
						results = PrecioServicioProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PrecioServicioProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PrecioServicioSelectMethod.GetByPrecioServicioId:
					_precioServicioId = ( values["PrecioServicioId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PrecioServicioId"], typeof(System.Guid)) : Guid.Empty;
					item = PrecioServicioProvider.GetByPrecioServicioId(_precioServicioId);
					results = new TList<PrecioServicio>();
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
			if ( SelectMethod == PrecioServicioSelectMethod.Get || SelectMethod == PrecioServicioSelectMethod.GetByPrecioServicioId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(PrecioServicio entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PrecioServicioId == Guid.Empty )
				entity.PrecioServicioId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				PrecioServicio entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PrecioServicioProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<PrecioServicio> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PrecioServicioProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PrecioServicioDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PrecioServicioDataSource class.
	/// </summary>
	public class PrecioServicioDataSourceDesigner : ProviderDataSourceDesigner<PrecioServicio, PrecioServicioKey>
	{
		/// <summary>
		/// Initializes a new instance of the PrecioServicioDataSourceDesigner class.
		/// </summary>
		public PrecioServicioDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PrecioServicioSelectMethod SelectMethod
		{
			get { return ((PrecioServicioDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PrecioServicioDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PrecioServicioDataSourceActionList

	/// <summary>
	/// Supports the PrecioServicioDataSourceDesigner class.
	/// </summary>
	internal class PrecioServicioDataSourceActionList : DesignerActionList
	{
		private PrecioServicioDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PrecioServicioDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PrecioServicioDataSourceActionList(PrecioServicioDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PrecioServicioSelectMethod SelectMethod
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

	#endregion PrecioServicioDataSourceActionList
	
	#endregion PrecioServicioDataSourceDesigner
	
	#region PrecioServicioSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PrecioServicioDataSource.SelectMethod property.
	/// </summary>
	public enum PrecioServicioSelectMethod
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
		/// Represents the GetByPrecioServicioId method.
		/// </summary>
		GetByPrecioServicioId
	}
	
	#endregion PrecioServicioSelectMethod

	#region PrecioServicioFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioServicioFilter : SqlFilter<PrecioServicioColumn>
	{
	}
	
	#endregion PrecioServicioFilter

	#region PrecioServicioExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioServicioExpressionBuilder : SqlExpressionBuilder<PrecioServicioColumn>
	{
	}
	
	#endregion PrecioServicioExpressionBuilder	

	#region PrecioServicioProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PrecioServicioChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="PrecioServicio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PrecioServicioProperty : ChildEntityProperty<PrecioServicioChildEntityTypes>
	{
	}
	
	#endregion PrecioServicioProperty
}

