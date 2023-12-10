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
	/// Represents the DataRepository.PaqueteProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PaqueteDataSourceDesigner))]
	public class PaqueteDataSource : ProviderDataSource<Paquete, PaqueteKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteDataSource class.
		/// </summary>
		public PaqueteDataSource() : base(new PaqueteService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PaqueteDataSourceView used by the PaqueteDataSource.
		/// </summary>
		protected PaqueteDataSourceView PaqueteView
		{
			get { return ( View as PaqueteDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PaqueteDataSource control invokes to retrieve data.
		/// </summary>
		public PaqueteSelectMethod SelectMethod
		{
			get
			{
				PaqueteSelectMethod selectMethod = PaqueteSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PaqueteSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PaqueteDataSourceView class that is to be
		/// used by the PaqueteDataSource.
		/// </summary>
		/// <returns>An instance of the PaqueteDataSourceView class.</returns>
		protected override BaseDataSourceView<Paquete, PaqueteKey> GetNewDataSourceView()
		{
			return new PaqueteDataSourceView(this, DefaultViewName);
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
	/// Supports the PaqueteDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PaqueteDataSourceView : ProviderDataSourceView<Paquete, PaqueteKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PaqueteDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PaqueteDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PaqueteDataSourceView(PaqueteDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PaqueteDataSource PaqueteOwner
		{
			get { return Owner as PaqueteDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PaqueteSelectMethod SelectMethod
		{
			get { return PaqueteOwner.SelectMethod; }
			set { PaqueteOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PaqueteService PaqueteProvider
		{
			get { return Provider as PaqueteService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Paquete> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Paquete> results = null;
			Paquete item;
			count = 0;
			
			System.Guid _paqueteId;
			System.Int32 _destinoId;

			switch ( SelectMethod )
			{
				case PaqueteSelectMethod.Get:
					PaqueteKey entityKey  = new PaqueteKey();
					entityKey.Load(values);
					item = PaqueteProvider.Get(entityKey);
					results = new TList<Paquete>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PaqueteSelectMethod.GetAll:
                    results = PaqueteProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PaqueteSelectMethod.GetPaged:
					results = PaqueteProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PaqueteSelectMethod.Find:
					if ( FilterParameters != null )
						results = PaqueteProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PaqueteProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PaqueteSelectMethod.GetByPaqueteId:
					_paqueteId = ( values["PaqueteId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PaqueteId"], typeof(System.Guid)) : Guid.Empty;
					item = PaqueteProvider.GetByPaqueteId(_paqueteId);
					results = new TList<Paquete>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case PaqueteSelectMethod.GetByDestinoId:
					_destinoId = ( values["DestinoId"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["DestinoId"], typeof(System.Int32)) : (int)0;
					results = PaqueteProvider.GetByDestinoId(_destinoId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == PaqueteSelectMethod.Get || SelectMethod == PaqueteSelectMethod.GetByPaqueteId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Paquete entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PaqueteId == Guid.Empty )
				entity.PaqueteId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Paquete entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PaqueteProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Paquete> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PaqueteProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PaqueteDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PaqueteDataSource class.
	/// </summary>
	public class PaqueteDataSourceDesigner : ProviderDataSourceDesigner<Paquete, PaqueteKey>
	{
		/// <summary>
		/// Initializes a new instance of the PaqueteDataSourceDesigner class.
		/// </summary>
		public PaqueteDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PaqueteSelectMethod SelectMethod
		{
			get { return ((PaqueteDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PaqueteDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PaqueteDataSourceActionList

	/// <summary>
	/// Supports the PaqueteDataSourceDesigner class.
	/// </summary>
	internal class PaqueteDataSourceActionList : DesignerActionList
	{
		private PaqueteDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PaqueteDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PaqueteDataSourceActionList(PaqueteDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PaqueteSelectMethod SelectMethod
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

	#endregion PaqueteDataSourceActionList
	
	#endregion PaqueteDataSourceDesigner
	
	#region PaqueteSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PaqueteDataSource.SelectMethod property.
	/// </summary>
	public enum PaqueteSelectMethod
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
		/// Represents the GetByPaqueteId method.
		/// </summary>
		GetByPaqueteId,
		/// <summary>
		/// Represents the GetByDestinoId method.
		/// </summary>
		GetByDestinoId
	}
	
	#endregion PaqueteSelectMethod

	#region PaqueteFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Paquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteFilter : SqlFilter<PaqueteColumn>
	{
	}
	
	#endregion PaqueteFilter

	#region PaqueteExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Paquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteExpressionBuilder : SqlExpressionBuilder<PaqueteColumn>
	{
	}
	
	#endregion PaqueteExpressionBuilder	

	#region PaqueteProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PaqueteChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Paquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PaqueteProperty : ChildEntityProperty<PaqueteChildEntityTypes>
	{
	}
	
	#endregion PaqueteProperty
}

