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
	/// Represents the DataRepository.ViajeProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ViajeDataSourceDesigner))]
	public class ViajeDataSource : ProviderDataSource<Viaje, ViajeKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeDataSource class.
		/// </summary>
		public ViajeDataSource() : base(new ViajeService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ViajeDataSourceView used by the ViajeDataSource.
		/// </summary>
		protected ViajeDataSourceView ViajeView
		{
			get { return ( View as ViajeDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ViajeDataSource control invokes to retrieve data.
		/// </summary>
		public ViajeSelectMethod SelectMethod
		{
			get
			{
				ViajeSelectMethod selectMethod = ViajeSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ViajeSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ViajeDataSourceView class that is to be
		/// used by the ViajeDataSource.
		/// </summary>
		/// <returns>An instance of the ViajeDataSourceView class.</returns>
		protected override BaseDataSourceView<Viaje, ViajeKey> GetNewDataSourceView()
		{
			return new ViajeDataSourceView(this, DefaultViewName);
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
	/// Supports the ViajeDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ViajeDataSourceView : ProviderDataSourceView<Viaje, ViajeKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ViajeDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ViajeDataSourceView(ViajeDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ViajeDataSource ViajeOwner
		{
			get { return Owner as ViajeDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ViajeSelectMethod SelectMethod
		{
			get { return ViajeOwner.SelectMethod; }
			set { ViajeOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ViajeService ViajeProvider
		{
			get { return Provider as ViajeService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Viaje> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Viaje> results = null;
			Viaje item;
			count = 0;
			
			System.Guid _viajeId;
			System.Guid? _paqueteId_nullable;
			System.Guid? _busId_nullable;

			switch ( SelectMethod )
			{
				case ViajeSelectMethod.Get:
					ViajeKey entityKey  = new ViajeKey();
					entityKey.Load(values);
					item = ViajeProvider.Get(entityKey);
					results = new TList<Viaje>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ViajeSelectMethod.GetAll:
                    results = ViajeProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case ViajeSelectMethod.GetPaged:
					results = ViajeProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ViajeSelectMethod.Find:
					if ( FilterParameters != null )
						results = ViajeProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ViajeProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ViajeSelectMethod.GetByViajeId:
					_viajeId = ( values["ViajeId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ViajeId"], typeof(System.Guid)) : Guid.Empty;
					item = ViajeProvider.GetByViajeId(_viajeId);
					results = new TList<Viaje>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case ViajeSelectMethod.GetByPaqueteId:
					_paqueteId_nullable = (System.Guid?) EntityUtil.ChangeType(values["PaqueteId"], typeof(System.Guid?));
					results = ViajeProvider.GetByPaqueteId(_paqueteId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case ViajeSelectMethod.GetByBusId:
					_busId_nullable = (System.Guid?) EntityUtil.ChangeType(values["BusId"], typeof(System.Guid?));
					results = ViajeProvider.GetByBusId(_busId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == ViajeSelectMethod.Get || SelectMethod == ViajeSelectMethod.GetByViajeId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Viaje entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.ViajeId == Guid.Empty )
				entity.ViajeId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Viaje entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					ViajeProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Viaje> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			ViajeProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ViajeDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ViajeDataSource class.
	/// </summary>
	public class ViajeDataSourceDesigner : ProviderDataSourceDesigner<Viaje, ViajeKey>
	{
		/// <summary>
		/// Initializes a new instance of the ViajeDataSourceDesigner class.
		/// </summary>
		public ViajeDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ViajeSelectMethod SelectMethod
		{
			get { return ((ViajeDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ViajeDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ViajeDataSourceActionList

	/// <summary>
	/// Supports the ViajeDataSourceDesigner class.
	/// </summary>
	internal class ViajeDataSourceActionList : DesignerActionList
	{
		private ViajeDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ViajeDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ViajeDataSourceActionList(ViajeDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ViajeSelectMethod SelectMethod
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

	#endregion ViajeDataSourceActionList
	
	#endregion ViajeDataSourceDesigner
	
	#region ViajeSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ViajeDataSource.SelectMethod property.
	/// </summary>
	public enum ViajeSelectMethod
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
		/// Represents the GetByViajeId method.
		/// </summary>
		GetByViajeId,
		/// <summary>
		/// Represents the GetByPaqueteId method.
		/// </summary>
		GetByPaqueteId,
		/// <summary>
		/// Represents the GetByBusId method.
		/// </summary>
		GetByBusId
	}
	
	#endregion ViajeSelectMethod

	#region ViajeFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Viaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeFilter : SqlFilter<ViajeColumn>
	{
	}
	
	#endregion ViajeFilter

	#region ViajeExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Viaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeExpressionBuilder : SqlExpressionBuilder<ViajeColumn>
	{
	}
	
	#endregion ViajeExpressionBuilder	

	#region ViajeProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ViajeChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Viaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeProperty : ChildEntityProperty<ViajeChildEntityTypes>
	{
	}
	
	#endregion ViajeProperty
}

