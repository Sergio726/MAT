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
	/// Represents the DataRepository.ButacaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ButacaDataSourceDesigner))]
	public class ButacaDataSource : ProviderDataSource<Butaca, ButacaKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ButacaDataSource class.
		/// </summary>
		public ButacaDataSource() : base(new ButacaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ButacaDataSourceView used by the ButacaDataSource.
		/// </summary>
		protected ButacaDataSourceView ButacaView
		{
			get { return ( View as ButacaDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ButacaDataSource control invokes to retrieve data.
		/// </summary>
		public ButacaSelectMethod SelectMethod
		{
			get
			{
				ButacaSelectMethod selectMethod = ButacaSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ButacaSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ButacaDataSourceView class that is to be
		/// used by the ButacaDataSource.
		/// </summary>
		/// <returns>An instance of the ButacaDataSourceView class.</returns>
		protected override BaseDataSourceView<Butaca, ButacaKey> GetNewDataSourceView()
		{
			return new ButacaDataSourceView(this, DefaultViewName);
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
	/// Supports the ButacaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ButacaDataSourceView : ProviderDataSourceView<Butaca, ButacaKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ButacaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ButacaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ButacaDataSourceView(ButacaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ButacaDataSource ButacaOwner
		{
			get { return Owner as ButacaDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ButacaSelectMethod SelectMethod
		{
			get { return ButacaOwner.SelectMethod; }
			set { ButacaOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ButacaService ButacaProvider
		{
			get { return Provider as ButacaService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Butaca> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Butaca> results = null;
			Butaca item;
			count = 0;
			
			System.Guid _butacaId;
			System.Guid? _transporteId_nullable;

			switch ( SelectMethod )
			{
				case ButacaSelectMethod.Get:
					ButacaKey entityKey  = new ButacaKey();
					entityKey.Load(values);
					item = ButacaProvider.Get(entityKey);
					results = new TList<Butaca>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ButacaSelectMethod.GetAll:
                    results = ButacaProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case ButacaSelectMethod.GetPaged:
					results = ButacaProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ButacaSelectMethod.Find:
					if ( FilterParameters != null )
						results = ButacaProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ButacaProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ButacaSelectMethod.GetByButacaId:
					_butacaId = ( values["ButacaId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ButacaId"], typeof(System.Guid)) : Guid.Empty;
					item = ButacaProvider.GetByButacaId(_butacaId);
					results = new TList<Butaca>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case ButacaSelectMethod.GetByTransporteId:
					_transporteId_nullable = (System.Guid?) EntityUtil.ChangeType(values["TransporteId"], typeof(System.Guid?));
					results = ButacaProvider.GetByTransporteId(_transporteId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == ButacaSelectMethod.Get || SelectMethod == ButacaSelectMethod.GetByButacaId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Butaca entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.ButacaId == Guid.Empty )
				entity.ButacaId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Butaca entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					ButacaProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Butaca> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			ButacaProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ButacaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ButacaDataSource class.
	/// </summary>
	public class ButacaDataSourceDesigner : ProviderDataSourceDesigner<Butaca, ButacaKey>
	{
		/// <summary>
		/// Initializes a new instance of the ButacaDataSourceDesigner class.
		/// </summary>
		public ButacaDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ButacaSelectMethod SelectMethod
		{
			get { return ((ButacaDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ButacaDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ButacaDataSourceActionList

	/// <summary>
	/// Supports the ButacaDataSourceDesigner class.
	/// </summary>
	internal class ButacaDataSourceActionList : DesignerActionList
	{
		private ButacaDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ButacaDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ButacaDataSourceActionList(ButacaDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ButacaSelectMethod SelectMethod
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

	#endregion ButacaDataSourceActionList
	
	#endregion ButacaDataSourceDesigner
	
	#region ButacaSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ButacaDataSource.SelectMethod property.
	/// </summary>
	public enum ButacaSelectMethod
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
		/// Represents the GetByButacaId method.
		/// </summary>
		GetByButacaId,
		/// <summary>
		/// Represents the GetByTransporteId method.
		/// </summary>
		GetByTransporteId
	}
	
	#endregion ButacaSelectMethod

	#region ButacaFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Butaca"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ButacaFilter : SqlFilter<ButacaColumn>
	{
	}
	
	#endregion ButacaFilter

	#region ButacaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Butaca"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ButacaExpressionBuilder : SqlExpressionBuilder<ButacaColumn>
	{
	}
	
	#endregion ButacaExpressionBuilder	

	#region ButacaProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ButacaChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Butaca"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ButacaProperty : ChildEntityProperty<ButacaChildEntityTypes>
	{
	}
	
	#endregion ButacaProperty
}

