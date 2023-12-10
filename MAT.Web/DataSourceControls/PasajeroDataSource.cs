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
	/// Represents the DataRepository.PasajeroProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PasajeroDataSourceDesigner))]
	public class PasajeroDataSource : ProviderDataSource<Pasajero, PasajeroKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroDataSource class.
		/// </summary>
		public PasajeroDataSource() : base(new PasajeroService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PasajeroDataSourceView used by the PasajeroDataSource.
		/// </summary>
		protected PasajeroDataSourceView PasajeroView
		{
			get { return ( View as PasajeroDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PasajeroDataSource control invokes to retrieve data.
		/// </summary>
		public PasajeroSelectMethod SelectMethod
		{
			get
			{
				PasajeroSelectMethod selectMethod = PasajeroSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PasajeroSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PasajeroDataSourceView class that is to be
		/// used by the PasajeroDataSource.
		/// </summary>
		/// <returns>An instance of the PasajeroDataSourceView class.</returns>
		protected override BaseDataSourceView<Pasajero, PasajeroKey> GetNewDataSourceView()
		{
			return new PasajeroDataSourceView(this, DefaultViewName);
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
	/// Supports the PasajeroDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PasajeroDataSourceView : ProviderDataSourceView<Pasajero, PasajeroKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PasajeroDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PasajeroDataSourceView(PasajeroDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PasajeroDataSource PasajeroOwner
		{
			get { return Owner as PasajeroDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PasajeroSelectMethod SelectMethod
		{
			get { return PasajeroOwner.SelectMethod; }
			set { PasajeroOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PasajeroService PasajeroProvider
		{
			get { return Provider as PasajeroService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Pasajero> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Pasajero> results = null;
			Pasajero item;
			count = 0;
			
			System.Guid _pasajeroId;

			switch ( SelectMethod )
			{
				case PasajeroSelectMethod.Get:
					PasajeroKey entityKey  = new PasajeroKey();
					entityKey.Load(values);
					item = PasajeroProvider.Get(entityKey);
					results = new TList<Pasajero>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PasajeroSelectMethod.GetAll:
                    results = PasajeroProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PasajeroSelectMethod.GetPaged:
					results = PasajeroProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PasajeroSelectMethod.Find:
					if ( FilterParameters != null )
						results = PasajeroProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PasajeroProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PasajeroSelectMethod.GetByPasajeroId:
					_pasajeroId = ( values["PasajeroId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PasajeroId"], typeof(System.Guid)) : Guid.Empty;
					item = PasajeroProvider.GetByPasajeroId(_pasajeroId);
					results = new TList<Pasajero>();
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
			if ( SelectMethod == PasajeroSelectMethod.Get || SelectMethod == PasajeroSelectMethod.GetByPasajeroId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Pasajero entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PasajeroId == Guid.Empty )
				entity.PasajeroId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Pasajero entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PasajeroProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Pasajero> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PasajeroProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PasajeroDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PasajeroDataSource class.
	/// </summary>
	public class PasajeroDataSourceDesigner : ProviderDataSourceDesigner<Pasajero, PasajeroKey>
	{
		/// <summary>
		/// Initializes a new instance of the PasajeroDataSourceDesigner class.
		/// </summary>
		public PasajeroDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PasajeroSelectMethod SelectMethod
		{
			get { return ((PasajeroDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PasajeroDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PasajeroDataSourceActionList

	/// <summary>
	/// Supports the PasajeroDataSourceDesigner class.
	/// </summary>
	internal class PasajeroDataSourceActionList : DesignerActionList
	{
		private PasajeroDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PasajeroDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PasajeroDataSourceActionList(PasajeroDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PasajeroSelectMethod SelectMethod
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

	#endregion PasajeroDataSourceActionList
	
	#endregion PasajeroDataSourceDesigner
	
	#region PasajeroSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PasajeroDataSource.SelectMethod property.
	/// </summary>
	public enum PasajeroSelectMethod
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
		/// Represents the GetByPasajeroId method.
		/// </summary>
		GetByPasajeroId
	}
	
	#endregion PasajeroSelectMethod

	#region PasajeroFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroFilter : SqlFilter<PasajeroColumn>
	{
	}
	
	#endregion PasajeroFilter

	#region PasajeroExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Pasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroExpressionBuilder : SqlExpressionBuilder<PasajeroColumn>
	{
	}
	
	#endregion PasajeroExpressionBuilder	

	#region PasajeroProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PasajeroChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Pasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroProperty : ChildEntityProperty<PasajeroChildEntityTypes>
	{
	}
	
	#endregion PasajeroProperty
}

