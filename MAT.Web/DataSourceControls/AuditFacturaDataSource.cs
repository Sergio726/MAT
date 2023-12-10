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
	/// Represents the DataRepository.AuditFacturaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(AuditFacturaDataSourceDesigner))]
	public class AuditFacturaDataSource : ProviderDataSource<AuditFactura, AuditFacturaKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AuditFacturaDataSource class.
		/// </summary>
		public AuditFacturaDataSource() : base(new AuditFacturaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the AuditFacturaDataSourceView used by the AuditFacturaDataSource.
		/// </summary>
		protected AuditFacturaDataSourceView AuditFacturaView
		{
			get { return ( View as AuditFacturaDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the AuditFacturaDataSource control invokes to retrieve data.
		/// </summary>
		public AuditFacturaSelectMethod SelectMethod
		{
			get
			{
				AuditFacturaSelectMethod selectMethod = AuditFacturaSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (AuditFacturaSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the AuditFacturaDataSourceView class that is to be
		/// used by the AuditFacturaDataSource.
		/// </summary>
		/// <returns>An instance of the AuditFacturaDataSourceView class.</returns>
		protected override BaseDataSourceView<AuditFactura, AuditFacturaKey> GetNewDataSourceView()
		{
			return new AuditFacturaDataSourceView(this, DefaultViewName);
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
	/// Supports the AuditFacturaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class AuditFacturaDataSourceView : ProviderDataSourceView<AuditFactura, AuditFacturaKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AuditFacturaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the AuditFacturaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public AuditFacturaDataSourceView(AuditFacturaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal AuditFacturaDataSource AuditFacturaOwner
		{
			get { return Owner as AuditFacturaDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal AuditFacturaSelectMethod SelectMethod
		{
			get { return AuditFacturaOwner.SelectMethod; }
			set { AuditFacturaOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal AuditFacturaService AuditFacturaProvider
		{
			get { return Provider as AuditFacturaService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<AuditFactura> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<AuditFactura> results = null;
			AuditFactura item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case AuditFacturaSelectMethod.Get:
					AuditFacturaKey entityKey  = new AuditFacturaKey();
					entityKey.Load(values);
					item = AuditFacturaProvider.Get(entityKey);
					results = new TList<AuditFactura>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case AuditFacturaSelectMethod.GetAll:
                    results = AuditFacturaProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case AuditFacturaSelectMethod.GetPaged:
					results = AuditFacturaProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case AuditFacturaSelectMethod.Find:
					if ( FilterParameters != null )
						results = AuditFacturaProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = AuditFacturaProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case AuditFacturaSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = AuditFacturaProvider.GetById(_id);
					results = new TList<AuditFactura>();
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
			if ( SelectMethod == AuditFacturaSelectMethod.Get || SelectMethod == AuditFacturaSelectMethod.GetById )
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
				AuditFactura entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					AuditFacturaProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<AuditFactura> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			AuditFacturaProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region AuditFacturaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the AuditFacturaDataSource class.
	/// </summary>
	public class AuditFacturaDataSourceDesigner : ProviderDataSourceDesigner<AuditFactura, AuditFacturaKey>
	{
		/// <summary>
		/// Initializes a new instance of the AuditFacturaDataSourceDesigner class.
		/// </summary>
		public AuditFacturaDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public AuditFacturaSelectMethod SelectMethod
		{
			get { return ((AuditFacturaDataSource) DataSource).SelectMethod; }
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
				actions.Add(new AuditFacturaDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region AuditFacturaDataSourceActionList

	/// <summary>
	/// Supports the AuditFacturaDataSourceDesigner class.
	/// </summary>
	internal class AuditFacturaDataSourceActionList : DesignerActionList
	{
		private AuditFacturaDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the AuditFacturaDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public AuditFacturaDataSourceActionList(AuditFacturaDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public AuditFacturaSelectMethod SelectMethod
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

	#endregion AuditFacturaDataSourceActionList
	
	#endregion AuditFacturaDataSourceDesigner
	
	#region AuditFacturaSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the AuditFacturaDataSource.SelectMethod property.
	/// </summary>
	public enum AuditFacturaSelectMethod
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
	
	#endregion AuditFacturaSelectMethod

	#region AuditFacturaFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AuditFactura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AuditFacturaFilter : SqlFilter<AuditFacturaColumn>
	{
	}
	
	#endregion AuditFacturaFilter

	#region AuditFacturaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AuditFactura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AuditFacturaExpressionBuilder : SqlExpressionBuilder<AuditFacturaColumn>
	{
	}
	
	#endregion AuditFacturaExpressionBuilder	

	#region AuditFacturaProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;AuditFacturaChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="AuditFactura"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AuditFacturaProperty : ChildEntityProperty<AuditFacturaChildEntityTypes>
	{
	}
	
	#endregion AuditFacturaProperty
}

