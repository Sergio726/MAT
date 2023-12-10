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
	/// Represents the DataRepository.PasajeAdicionalProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PasajeAdicionalDataSourceDesigner))]
	public class PasajeAdicionalDataSource : ProviderDataSource<PasajeAdicional, PasajeAdicionalKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalDataSource class.
		/// </summary>
		public PasajeAdicionalDataSource() : base(new PasajeAdicionalService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PasajeAdicionalDataSourceView used by the PasajeAdicionalDataSource.
		/// </summary>
		protected PasajeAdicionalDataSourceView PasajeAdicionalView
		{
			get { return ( View as PasajeAdicionalDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PasajeAdicionalDataSource control invokes to retrieve data.
		/// </summary>
		public PasajeAdicionalSelectMethod SelectMethod
		{
			get
			{
				PasajeAdicionalSelectMethod selectMethod = PasajeAdicionalSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PasajeAdicionalSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PasajeAdicionalDataSourceView class that is to be
		/// used by the PasajeAdicionalDataSource.
		/// </summary>
		/// <returns>An instance of the PasajeAdicionalDataSourceView class.</returns>
		protected override BaseDataSourceView<PasajeAdicional, PasajeAdicionalKey> GetNewDataSourceView()
		{
			return new PasajeAdicionalDataSourceView(this, DefaultViewName);
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
	/// Supports the PasajeAdicionalDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PasajeAdicionalDataSourceView : ProviderDataSourceView<PasajeAdicional, PasajeAdicionalKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PasajeAdicionalDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PasajeAdicionalDataSourceView(PasajeAdicionalDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PasajeAdicionalDataSource PasajeAdicionalOwner
		{
			get { return Owner as PasajeAdicionalDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PasajeAdicionalSelectMethod SelectMethod
		{
			get { return PasajeAdicionalOwner.SelectMethod; }
			set { PasajeAdicionalOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PasajeAdicionalService PasajeAdicionalProvider
		{
			get { return Provider as PasajeAdicionalService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<PasajeAdicional> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<PasajeAdicional> results = null;
			PasajeAdicional item;
			count = 0;
			
			System.Guid _pasajeAdicionalId;
			System.Guid? _adicionalId_nullable;
			System.Guid? _pasajeId_nullable;

			switch ( SelectMethod )
			{
				case PasajeAdicionalSelectMethod.Get:
					PasajeAdicionalKey entityKey  = new PasajeAdicionalKey();
					entityKey.Load(values);
					item = PasajeAdicionalProvider.Get(entityKey);
					results = new TList<PasajeAdicional>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PasajeAdicionalSelectMethod.GetAll:
                    results = PasajeAdicionalProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PasajeAdicionalSelectMethod.GetPaged:
					results = PasajeAdicionalProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PasajeAdicionalSelectMethod.Find:
					if ( FilterParameters != null )
						results = PasajeAdicionalProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PasajeAdicionalProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PasajeAdicionalSelectMethod.GetByPasajeAdicionalId:
					_pasajeAdicionalId = ( values["PasajeAdicionalId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PasajeAdicionalId"], typeof(System.Guid)) : Guid.Empty;
					item = PasajeAdicionalProvider.GetByPasajeAdicionalId(_pasajeAdicionalId);
					results = new TList<PasajeAdicional>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case PasajeAdicionalSelectMethod.GetByAdicionalId:
					_adicionalId_nullable = (System.Guid?) EntityUtil.ChangeType(values["AdicionalId"], typeof(System.Guid?));
					results = PasajeAdicionalProvider.GetByAdicionalId(_adicionalId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case PasajeAdicionalSelectMethod.GetByPasajeId:
					_pasajeId_nullable = (System.Guid?) EntityUtil.ChangeType(values["PasajeId"], typeof(System.Guid?));
					results = PasajeAdicionalProvider.GetByPasajeId(_pasajeId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == PasajeAdicionalSelectMethod.Get || SelectMethod == PasajeAdicionalSelectMethod.GetByPasajeAdicionalId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(PasajeAdicional entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PasajeAdicionalId == Guid.Empty )
				entity.PasajeAdicionalId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				PasajeAdicional entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PasajeAdicionalProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<PasajeAdicional> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PasajeAdicionalProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PasajeAdicionalDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PasajeAdicionalDataSource class.
	/// </summary>
	public class PasajeAdicionalDataSourceDesigner : ProviderDataSourceDesigner<PasajeAdicional, PasajeAdicionalKey>
	{
		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalDataSourceDesigner class.
		/// </summary>
		public PasajeAdicionalDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PasajeAdicionalSelectMethod SelectMethod
		{
			get { return ((PasajeAdicionalDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PasajeAdicionalDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PasajeAdicionalDataSourceActionList

	/// <summary>
	/// Supports the PasajeAdicionalDataSourceDesigner class.
	/// </summary>
	internal class PasajeAdicionalDataSourceActionList : DesignerActionList
	{
		private PasajeAdicionalDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PasajeAdicionalDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PasajeAdicionalDataSourceActionList(PasajeAdicionalDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PasajeAdicionalSelectMethod SelectMethod
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

	#endregion PasajeAdicionalDataSourceActionList
	
	#endregion PasajeAdicionalDataSourceDesigner
	
	#region PasajeAdicionalSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PasajeAdicionalDataSource.SelectMethod property.
	/// </summary>
	public enum PasajeAdicionalSelectMethod
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
		/// Represents the GetByPasajeAdicionalId method.
		/// </summary>
		GetByPasajeAdicionalId,
		/// <summary>
		/// Represents the GetByAdicionalId method.
		/// </summary>
		GetByAdicionalId,
		/// <summary>
		/// Represents the GetByPasajeId method.
		/// </summary>
		GetByPasajeId
	}
	
	#endregion PasajeAdicionalSelectMethod

	#region PasajeAdicionalFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeAdicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeAdicionalFilter : SqlFilter<PasajeAdicionalColumn>
	{
	}
	
	#endregion PasajeAdicionalFilter

	#region PasajeAdicionalExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeAdicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeAdicionalExpressionBuilder : SqlExpressionBuilder<PasajeAdicionalColumn>
	{
	}
	
	#endregion PasajeAdicionalExpressionBuilder	

	#region PasajeAdicionalProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PasajeAdicionalChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeAdicional"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeAdicionalProperty : ChildEntityProperty<PasajeAdicionalChildEntityTypes>
	{
	}
	
	#endregion PasajeAdicionalProperty
}

