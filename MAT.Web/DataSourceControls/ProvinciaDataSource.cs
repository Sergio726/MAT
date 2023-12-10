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
	/// Represents the DataRepository.ProvinciaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ProvinciaDataSourceDesigner))]
	public class ProvinciaDataSource : ProviderDataSource<Provincia, ProvinciaKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProvinciaDataSource class.
		/// </summary>
		public ProvinciaDataSource() : base(new ProvinciaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ProvinciaDataSourceView used by the ProvinciaDataSource.
		/// </summary>
		protected ProvinciaDataSourceView ProvinciaView
		{
			get { return ( View as ProvinciaDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ProvinciaDataSource control invokes to retrieve data.
		/// </summary>
		public ProvinciaSelectMethod SelectMethod
		{
			get
			{
				ProvinciaSelectMethod selectMethod = ProvinciaSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ProvinciaSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ProvinciaDataSourceView class that is to be
		/// used by the ProvinciaDataSource.
		/// </summary>
		/// <returns>An instance of the ProvinciaDataSourceView class.</returns>
		protected override BaseDataSourceView<Provincia, ProvinciaKey> GetNewDataSourceView()
		{
			return new ProvinciaDataSourceView(this, DefaultViewName);
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
	/// Supports the ProvinciaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ProvinciaDataSourceView : ProviderDataSourceView<Provincia, ProvinciaKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProvinciaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ProvinciaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ProvinciaDataSourceView(ProvinciaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ProvinciaDataSource ProvinciaOwner
		{
			get { return Owner as ProvinciaDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ProvinciaSelectMethod SelectMethod
		{
			get { return ProvinciaOwner.SelectMethod; }
			set { ProvinciaOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ProvinciaService ProvinciaProvider
		{
			get { return Provider as ProvinciaService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Provincia> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Provincia> results = null;
			Provincia item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case ProvinciaSelectMethod.Get:
					ProvinciaKey entityKey  = new ProvinciaKey();
					entityKey.Load(values);
					item = ProvinciaProvider.Get(entityKey);
					results = new TList<Provincia>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ProvinciaSelectMethod.GetAll:
                    results = ProvinciaProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case ProvinciaSelectMethod.GetPaged:
					results = ProvinciaProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ProvinciaSelectMethod.Find:
					if ( FilterParameters != null )
						results = ProvinciaProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ProvinciaProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ProvinciaSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = ProvinciaProvider.GetById(_id);
					results = new TList<Provincia>();
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
			if ( SelectMethod == ProvinciaSelectMethod.Get || SelectMethod == ProvinciaSelectMethod.GetById )
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
				Provincia entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					ProvinciaProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Provincia> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			ProvinciaProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ProvinciaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ProvinciaDataSource class.
	/// </summary>
	public class ProvinciaDataSourceDesigner : ProviderDataSourceDesigner<Provincia, ProvinciaKey>
	{
		/// <summary>
		/// Initializes a new instance of the ProvinciaDataSourceDesigner class.
		/// </summary>
		public ProvinciaDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ProvinciaSelectMethod SelectMethod
		{
			get { return ((ProvinciaDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ProvinciaDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ProvinciaDataSourceActionList

	/// <summary>
	/// Supports the ProvinciaDataSourceDesigner class.
	/// </summary>
	internal class ProvinciaDataSourceActionList : DesignerActionList
	{
		private ProvinciaDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ProvinciaDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ProvinciaDataSourceActionList(ProvinciaDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ProvinciaSelectMethod SelectMethod
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

	#endregion ProvinciaDataSourceActionList
	
	#endregion ProvinciaDataSourceDesigner
	
	#region ProvinciaSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ProvinciaDataSource.SelectMethod property.
	/// </summary>
	public enum ProvinciaSelectMethod
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
	
	#endregion ProvinciaSelectMethod

	#region ProvinciaFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Provincia"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProvinciaFilter : SqlFilter<ProvinciaColumn>
	{
	}
	
	#endregion ProvinciaFilter

	#region ProvinciaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Provincia"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProvinciaExpressionBuilder : SqlExpressionBuilder<ProvinciaColumn>
	{
	}
	
	#endregion ProvinciaExpressionBuilder	

	#region ProvinciaProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ProvinciaChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Provincia"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ProvinciaProperty : ChildEntityProperty<ProvinciaChildEntityTypes>
	{
	}
	
	#endregion ProvinciaProperty
}

