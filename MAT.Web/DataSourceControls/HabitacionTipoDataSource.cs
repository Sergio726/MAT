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
	/// Represents the DataRepository.HabitacionTipoProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(HabitacionTipoDataSourceDesigner))]
	public class HabitacionTipoDataSource : ProviderDataSource<HabitacionTipo, HabitacionTipoKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionTipoDataSource class.
		/// </summary>
		public HabitacionTipoDataSource() : base(new HabitacionTipoService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the HabitacionTipoDataSourceView used by the HabitacionTipoDataSource.
		/// </summary>
		protected HabitacionTipoDataSourceView HabitacionTipoView
		{
			get { return ( View as HabitacionTipoDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the HabitacionTipoDataSource control invokes to retrieve data.
		/// </summary>
		public HabitacionTipoSelectMethod SelectMethod
		{
			get
			{
				HabitacionTipoSelectMethod selectMethod = HabitacionTipoSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (HabitacionTipoSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the HabitacionTipoDataSourceView class that is to be
		/// used by the HabitacionTipoDataSource.
		/// </summary>
		/// <returns>An instance of the HabitacionTipoDataSourceView class.</returns>
		protected override BaseDataSourceView<HabitacionTipo, HabitacionTipoKey> GetNewDataSourceView()
		{
			return new HabitacionTipoDataSourceView(this, DefaultViewName);
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
	/// Supports the HabitacionTipoDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class HabitacionTipoDataSourceView : ProviderDataSourceView<HabitacionTipo, HabitacionTipoKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionTipoDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the HabitacionTipoDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public HabitacionTipoDataSourceView(HabitacionTipoDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal HabitacionTipoDataSource HabitacionTipoOwner
		{
			get { return Owner as HabitacionTipoDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal HabitacionTipoSelectMethod SelectMethod
		{
			get { return HabitacionTipoOwner.SelectMethod; }
			set { HabitacionTipoOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal HabitacionTipoService HabitacionTipoProvider
		{
			get { return Provider as HabitacionTipoService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<HabitacionTipo> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<HabitacionTipo> results = null;
			HabitacionTipo item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case HabitacionTipoSelectMethod.Get:
					HabitacionTipoKey entityKey  = new HabitacionTipoKey();
					entityKey.Load(values);
					item = HabitacionTipoProvider.Get(entityKey);
					results = new TList<HabitacionTipo>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case HabitacionTipoSelectMethod.GetAll:
                    results = HabitacionTipoProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case HabitacionTipoSelectMethod.GetPaged:
					results = HabitacionTipoProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case HabitacionTipoSelectMethod.Find:
					if ( FilterParameters != null )
						results = HabitacionTipoProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = HabitacionTipoProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case HabitacionTipoSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = HabitacionTipoProvider.GetById(_id);
					results = new TList<HabitacionTipo>();
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
			if ( SelectMethod == HabitacionTipoSelectMethod.Get || SelectMethod == HabitacionTipoSelectMethod.GetById )
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
				HabitacionTipo entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					HabitacionTipoProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<HabitacionTipo> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			HabitacionTipoProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region HabitacionTipoDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the HabitacionTipoDataSource class.
	/// </summary>
	public class HabitacionTipoDataSourceDesigner : ProviderDataSourceDesigner<HabitacionTipo, HabitacionTipoKey>
	{
		/// <summary>
		/// Initializes a new instance of the HabitacionTipoDataSourceDesigner class.
		/// </summary>
		public HabitacionTipoDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public HabitacionTipoSelectMethod SelectMethod
		{
			get { return ((HabitacionTipoDataSource) DataSource).SelectMethod; }
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
				actions.Add(new HabitacionTipoDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region HabitacionTipoDataSourceActionList

	/// <summary>
	/// Supports the HabitacionTipoDataSourceDesigner class.
	/// </summary>
	internal class HabitacionTipoDataSourceActionList : DesignerActionList
	{
		private HabitacionTipoDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the HabitacionTipoDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public HabitacionTipoDataSourceActionList(HabitacionTipoDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public HabitacionTipoSelectMethod SelectMethod
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

	#endregion HabitacionTipoDataSourceActionList
	
	#endregion HabitacionTipoDataSourceDesigner
	
	#region HabitacionTipoSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the HabitacionTipoDataSource.SelectMethod property.
	/// </summary>
	public enum HabitacionTipoSelectMethod
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
	
	#endregion HabitacionTipoSelectMethod

	#region HabitacionTipoFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="HabitacionTipo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionTipoFilter : SqlFilter<HabitacionTipoColumn>
	{
	}
	
	#endregion HabitacionTipoFilter

	#region HabitacionTipoExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="HabitacionTipo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionTipoExpressionBuilder : SqlExpressionBuilder<HabitacionTipoColumn>
	{
	}
	
	#endregion HabitacionTipoExpressionBuilder	

	#region HabitacionTipoProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;HabitacionTipoChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="HabitacionTipo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionTipoProperty : ChildEntityProperty<HabitacionTipoChildEntityTypes>
	{
	}
	
	#endregion HabitacionTipoProperty
}

