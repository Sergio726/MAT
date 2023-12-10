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
	/// Represents the DataRepository.LocalidadProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(LocalidadDataSourceDesigner))]
	public class LocalidadDataSource : ProviderDataSource<Localidad, LocalidadKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LocalidadDataSource class.
		/// </summary>
		public LocalidadDataSource() : base(new LocalidadService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the LocalidadDataSourceView used by the LocalidadDataSource.
		/// </summary>
		protected LocalidadDataSourceView LocalidadView
		{
			get { return ( View as LocalidadDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the LocalidadDataSource control invokes to retrieve data.
		/// </summary>
		public LocalidadSelectMethod SelectMethod
		{
			get
			{
				LocalidadSelectMethod selectMethod = LocalidadSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (LocalidadSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the LocalidadDataSourceView class that is to be
		/// used by the LocalidadDataSource.
		/// </summary>
		/// <returns>An instance of the LocalidadDataSourceView class.</returns>
		protected override BaseDataSourceView<Localidad, LocalidadKey> GetNewDataSourceView()
		{
			return new LocalidadDataSourceView(this, DefaultViewName);
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
	/// Supports the LocalidadDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class LocalidadDataSourceView : ProviderDataSourceView<Localidad, LocalidadKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LocalidadDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the LocalidadDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public LocalidadDataSourceView(LocalidadDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal LocalidadDataSource LocalidadOwner
		{
			get { return Owner as LocalidadDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal LocalidadSelectMethod SelectMethod
		{
			get { return LocalidadOwner.SelectMethod; }
			set { LocalidadOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal LocalidadService LocalidadProvider
		{
			get { return Provider as LocalidadService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Localidad> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Localidad> results = null;
			Localidad item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case LocalidadSelectMethod.Get:
					LocalidadKey entityKey  = new LocalidadKey();
					entityKey.Load(values);
					item = LocalidadProvider.Get(entityKey);
					results = new TList<Localidad>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case LocalidadSelectMethod.GetAll:
                    results = LocalidadProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case LocalidadSelectMethod.GetPaged:
					results = LocalidadProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case LocalidadSelectMethod.Find:
					if ( FilterParameters != null )
						results = LocalidadProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = LocalidadProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case LocalidadSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = LocalidadProvider.GetById(_id);
					results = new TList<Localidad>();
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
			if ( SelectMethod == LocalidadSelectMethod.Get || SelectMethod == LocalidadSelectMethod.GetById )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Localidad entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					LocalidadProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Localidad> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			LocalidadProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region LocalidadDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the LocalidadDataSource class.
	/// </summary>
	public class LocalidadDataSourceDesigner : ProviderDataSourceDesigner<Localidad, LocalidadKey>
	{
		/// <summary>
		/// Initializes a new instance of the LocalidadDataSourceDesigner class.
		/// </summary>
		public LocalidadDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public LocalidadSelectMethod SelectMethod
		{
			get { return ((LocalidadDataSource) DataSource).SelectMethod; }
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
				actions.Add(new LocalidadDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region LocalidadDataSourceActionList

	/// <summary>
	/// Supports the LocalidadDataSourceDesigner class.
	/// </summary>
	internal class LocalidadDataSourceActionList : DesignerActionList
	{
		private LocalidadDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the LocalidadDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public LocalidadDataSourceActionList(LocalidadDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public LocalidadSelectMethod SelectMethod
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

	#endregion LocalidadDataSourceActionList
	
	#endregion LocalidadDataSourceDesigner
	
	#region LocalidadSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the LocalidadDataSource.SelectMethod property.
	/// </summary>
	public enum LocalidadSelectMethod
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
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion LocalidadSelectMethod

	#region LocalidadFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Localidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class LocalidadFilter : SqlFilter<LocalidadColumn>
	{
	}
	
	#endregion LocalidadFilter

	#region LocalidadExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Localidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class LocalidadExpressionBuilder : SqlExpressionBuilder<LocalidadColumn>
	{
	}
	
	#endregion LocalidadExpressionBuilder	

	#region LocalidadProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;LocalidadChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Localidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class LocalidadProperty : ChildEntityProperty<LocalidadChildEntityTypes>
	{
	}
	
	#endregion LocalidadProperty
}

