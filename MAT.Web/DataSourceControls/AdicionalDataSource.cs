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
	/// Represents the DataRepository.AdicionalProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(AdicionalDataSourceDesigner))]
	public class AdicionalDataSource : ProviderDataSource<Adicional, AdicionalKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AdicionalDataSource class.
		/// </summary>
		public AdicionalDataSource() : base(new AdicionalService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the AdicionalDataSourceView used by the AdicionalDataSource.
		/// </summary>
		protected AdicionalDataSourceView AdicionalView
		{
			get { return ( View as AdicionalDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the AdicionalDataSource control invokes to retrieve data.
		/// </summary>
		public AdicionalSelectMethod SelectMethod
		{
			get
			{
				AdicionalSelectMethod selectMethod = AdicionalSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (AdicionalSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the AdicionalDataSourceView class that is to be
		/// used by the AdicionalDataSource.
		/// </summary>
		/// <returns>An instance of the AdicionalDataSourceView class.</returns>
		protected override BaseDataSourceView<Adicional, AdicionalKey> GetNewDataSourceView()
		{
			return new AdicionalDataSourceView(this, DefaultViewName);
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
	/// Supports the AdicionalDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class AdicionalDataSourceView : ProviderDataSourceView<Adicional, AdicionalKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AdicionalDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the AdicionalDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public AdicionalDataSourceView(AdicionalDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal AdicionalDataSource AdicionalOwner
		{
			get { return Owner as AdicionalDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal AdicionalSelectMethod SelectMethod
		{
			get { return AdicionalOwner.SelectMethod; }
			set { AdicionalOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal AdicionalService AdicionalProvider
		{
			get { return Provider as AdicionalService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Adicional> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Adicional> results = null;
			Adicional item;
			count = 0;
			
			System.Guid _adicionalId;

			switch ( SelectMethod )
			{
				case AdicionalSelectMethod.Get:
					AdicionalKey entityKey  = new AdicionalKey();
					entityKey.Load(values);
					item = AdicionalProvider.Get(entityKey);
					results = new TList<Adicional>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case AdicionalSelectMethod.GetAll:
                    results = AdicionalProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case AdicionalSelectMethod.GetPaged:
					results = AdicionalProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case AdicionalSelectMethod.Find:
					if ( FilterParameters != null )
						results = AdicionalProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = AdicionalProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case AdicionalSelectMethod.GetByAdicionalId:
					_adicionalId = ( values["AdicionalId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["AdicionalId"], typeof(System.Guid)) : Guid.Empty;
					item = AdicionalProvider.GetByAdicionalId(_adicionalId);
					results = new TList<Adicional>();
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
			if ( SelectMethod == AdicionalSelectMethod.Get || SelectMethod == AdicionalSelectMethod.GetByAdicionalId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Adicional entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.AdicionalId == Guid.Empty )
				entity.AdicionalId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Adicional entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					AdicionalProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Adicional> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			AdicionalProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region AdicionalDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the AdicionalDataSource class.
	/// </summary>
	public class AdicionalDataSourceDesigner : ProviderDataSourceDesigner<Adicional, AdicionalKey>
	{
		/// <summary>
		/// Initializes a new instance of the AdicionalDataSourceDesigner class.
		/// </summary>
		public AdicionalDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public AdicionalSelectMethod SelectMethod
		{
			get { return ((AdicionalDataSource) DataSource).SelectMethod; }
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
				actions.Add(new AdicionalDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region AdicionalDataSourceActionList

	/// <summary>
	/// Supports the AdicionalDataSourceDesigner class.
	/// </summary>
	internal class AdicionalDataSourceActionList : DesignerActionList
	{
		private AdicionalDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the AdicionalDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public AdicionalDataSourceActionList(AdicionalDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public AdicionalSelectMethod SelectMethod
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

	#endregion AdicionalDataSourceActionList
	
	#endregion AdicionalDataSourceDesigner
	
	#region AdicionalSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the AdicionalDataSource.SelectMethod property.
	/// </summary>
	public enum AdicionalSelectMethod
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
		/// Represents the GetByAdicionalId method.
		/// </summary>
		GetByAdicionalId
	}
	
	#endregion AdicionalSelectMethod

	#region AdicionalFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Adicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AdicionalFilter : SqlFilter<AdicionalColumn>
	{
	}
	
	#endregion AdicionalFilter

	#region AdicionalExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Adicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AdicionalExpressionBuilder : SqlExpressionBuilder<AdicionalColumn>
	{
	}
	
	#endregion AdicionalExpressionBuilder	

	#region AdicionalProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;AdicionalChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Adicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AdicionalProperty : ChildEntityProperty<AdicionalChildEntityTypes>
	{
	}
	
	#endregion AdicionalProperty
}

