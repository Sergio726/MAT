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
	/// Represents the DataRepository.DestinoProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(DestinoDataSourceDesigner))]
	public class DestinoDataSource : ProviderDataSource<Destino, DestinoKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DestinoDataSource class.
		/// </summary>
		public DestinoDataSource() : base(new DestinoService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the DestinoDataSourceView used by the DestinoDataSource.
		/// </summary>
		protected DestinoDataSourceView DestinoView
		{
			get { return ( View as DestinoDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DestinoDataSource control invokes to retrieve data.
		/// </summary>
		public DestinoSelectMethod SelectMethod
		{
			get
			{
				DestinoSelectMethod selectMethod = DestinoSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (DestinoSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the DestinoDataSourceView class that is to be
		/// used by the DestinoDataSource.
		/// </summary>
		/// <returns>An instance of the DestinoDataSourceView class.</returns>
		protected override BaseDataSourceView<Destino, DestinoKey> GetNewDataSourceView()
		{
			return new DestinoDataSourceView(this, DefaultViewName);
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
	/// Supports the DestinoDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class DestinoDataSourceView : ProviderDataSourceView<Destino, DestinoKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DestinoDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the DestinoDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public DestinoDataSourceView(DestinoDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal DestinoDataSource DestinoOwner
		{
			get { return Owner as DestinoDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal DestinoSelectMethod SelectMethod
		{
			get { return DestinoOwner.SelectMethod; }
			set { DestinoOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal DestinoService DestinoProvider
		{
			get { return Provider as DestinoService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Destino> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Destino> results = null;
			Destino item;
			count = 0;
			
			System.Guid _destinoId;

			switch ( SelectMethod )
			{
				case DestinoSelectMethod.Get:
					DestinoKey entityKey  = new DestinoKey();
					entityKey.Load(values);
					item = DestinoProvider.Get(entityKey);
					results = new TList<Destino>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case DestinoSelectMethod.GetAll:
                    results = DestinoProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case DestinoSelectMethod.GetPaged:
					results = DestinoProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case DestinoSelectMethod.Find:
					if ( FilterParameters != null )
						results = DestinoProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = DestinoProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case DestinoSelectMethod.GetByDestinoId:
					_destinoId = ( values["DestinoId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["DestinoId"], typeof(System.Guid)) : Guid.NewGuid();
					item = DestinoProvider.GetByDestinoId(_destinoId);
					results = new TList<Destino>();
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
			if ( SelectMethod == DestinoSelectMethod.Get || SelectMethod == DestinoSelectMethod.GetByDestinoId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Destino entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.DestinoId == Guid.Empty )
				entity.DestinoId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Destino entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					DestinoProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Destino> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			DestinoProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region DestinoDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the DestinoDataSource class.
	/// </summary>
	public class DestinoDataSourceDesigner : ProviderDataSourceDesigner<Destino, DestinoKey>
	{
		/// <summary>
		/// Initializes a new instance of the DestinoDataSourceDesigner class.
		/// </summary>
		public DestinoDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DestinoSelectMethod SelectMethod
		{
			get { return ((DestinoDataSource) DataSource).SelectMethod; }
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
				actions.Add(new DestinoDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region DestinoDataSourceActionList

	/// <summary>
	/// Supports the DestinoDataSourceDesigner class.
	/// </summary>
	internal class DestinoDataSourceActionList : DesignerActionList
	{
		private DestinoDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the DestinoDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public DestinoDataSourceActionList(DestinoDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DestinoSelectMethod SelectMethod
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

	#endregion DestinoDataSourceActionList
	
	#endregion DestinoDataSourceDesigner
	
	#region DestinoSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the DestinoDataSource.SelectMethod property.
	/// </summary>
	public enum DestinoSelectMethod
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
		/// Represents the GetByDestinoId method.
		/// </summary>
		GetByDestinoId
	}
	
	#endregion DestinoSelectMethod

	#region DestinoFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Destino"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DestinoFilter : SqlFilter<DestinoColumn>
	{
	}
	
	#endregion DestinoFilter

	#region DestinoExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Destino"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DestinoExpressionBuilder : SqlExpressionBuilder<DestinoColumn>
	{
	}
	
	#endregion DestinoExpressionBuilder	

	#region DestinoProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;DestinoChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Destino"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DestinoProperty : ChildEntityProperty<DestinoChildEntityTypes>
	{
	}
	
	#endregion DestinoProperty
}

