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
	/// Represents the DataRepository.ViajeHotelProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ViajeHotelDataSourceDesigner))]
	public class ViajeHotelDataSource : ProviderDataSource<ViajeHotel, ViajeHotelKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeHotelDataSource class.
		/// </summary>
		public ViajeHotelDataSource() : base(new ViajeHotelService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ViajeHotelDataSourceView used by the ViajeHotelDataSource.
		/// </summary>
		protected ViajeHotelDataSourceView ViajeHotelView
		{
			get { return ( View as ViajeHotelDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ViajeHotelDataSource control invokes to retrieve data.
		/// </summary>
		public ViajeHotelSelectMethod SelectMethod
		{
			get
			{
				ViajeHotelSelectMethod selectMethod = ViajeHotelSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ViajeHotelSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ViajeHotelDataSourceView class that is to be
		/// used by the ViajeHotelDataSource.
		/// </summary>
		/// <returns>An instance of the ViajeHotelDataSourceView class.</returns>
		protected override BaseDataSourceView<ViajeHotel, ViajeHotelKey> GetNewDataSourceView()
		{
			return new ViajeHotelDataSourceView(this, DefaultViewName);
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
	/// Supports the ViajeHotelDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ViajeHotelDataSourceView : ProviderDataSourceView<ViajeHotel, ViajeHotelKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ViajeHotelDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ViajeHotelDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ViajeHotelDataSourceView(ViajeHotelDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ViajeHotelDataSource ViajeHotelOwner
		{
			get { return Owner as ViajeHotelDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ViajeHotelSelectMethod SelectMethod
		{
			get { return ViajeHotelOwner.SelectMethod; }
			set { ViajeHotelOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ViajeHotelService ViajeHotelProvider
		{
			get { return Provider as ViajeHotelService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<ViajeHotel> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<ViajeHotel> results = null;
			ViajeHotel item;
			count = 0;
			
			System.Guid _viajeHotelId;
			System.Guid _hotelId;
			System.Guid _viajeId;

			switch ( SelectMethod )
			{
				case ViajeHotelSelectMethod.Get:
					ViajeHotelKey entityKey  = new ViajeHotelKey();
					entityKey.Load(values);
					item = ViajeHotelProvider.Get(entityKey);
					results = new TList<ViajeHotel>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ViajeHotelSelectMethod.GetAll:
                    results = ViajeHotelProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case ViajeHotelSelectMethod.GetPaged:
					results = ViajeHotelProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ViajeHotelSelectMethod.Find:
					if ( FilterParameters != null )
						results = ViajeHotelProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ViajeHotelProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ViajeHotelSelectMethod.GetByViajeHotelId:
					_viajeHotelId = ( values["ViajeHotelId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ViajeHotelId"], typeof(System.Guid)) : Guid.Empty;
					item = ViajeHotelProvider.GetByViajeHotelId(_viajeHotelId);
					results = new TList<ViajeHotel>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case ViajeHotelSelectMethod.GetByHotelId:
					_hotelId = ( values["HotelId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["HotelId"], typeof(System.Guid)) : Guid.Empty;
					results = ViajeHotelProvider.GetByHotelId(_hotelId, this.StartIndex, this.PageSize, out count);
					break;
				case ViajeHotelSelectMethod.GetByViajeId:
					_viajeId = ( values["ViajeId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ViajeId"], typeof(System.Guid)) : Guid.Empty;
					results = ViajeHotelProvider.GetByViajeId(_viajeId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == ViajeHotelSelectMethod.Get || SelectMethod == ViajeHotelSelectMethod.GetByViajeHotelId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(ViajeHotel entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.ViajeHotelId == Guid.Empty )
				entity.ViajeHotelId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				ViajeHotel entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					ViajeHotelProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<ViajeHotel> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			ViajeHotelProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ViajeHotelDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ViajeHotelDataSource class.
	/// </summary>
	public class ViajeHotelDataSourceDesigner : ProviderDataSourceDesigner<ViajeHotel, ViajeHotelKey>
	{
		/// <summary>
		/// Initializes a new instance of the ViajeHotelDataSourceDesigner class.
		/// </summary>
		public ViajeHotelDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ViajeHotelSelectMethod SelectMethod
		{
			get { return ((ViajeHotelDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ViajeHotelDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ViajeHotelDataSourceActionList

	/// <summary>
	/// Supports the ViajeHotelDataSourceDesigner class.
	/// </summary>
	internal class ViajeHotelDataSourceActionList : DesignerActionList
	{
		private ViajeHotelDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ViajeHotelDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ViajeHotelDataSourceActionList(ViajeHotelDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ViajeHotelSelectMethod SelectMethod
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

	#endregion ViajeHotelDataSourceActionList
	
	#endregion ViajeHotelDataSourceDesigner
	
	#region ViajeHotelSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ViajeHotelDataSource.SelectMethod property.
	/// </summary>
	public enum ViajeHotelSelectMethod
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
		/// Represents the GetByViajeHotelId method.
		/// </summary>
		GetByViajeHotelId,
		/// <summary>
		/// Represents the GetByHotelId method.
		/// </summary>
		GetByHotelId,
		/// <summary>
		/// Represents the GetByViajeId method.
		/// </summary>
		GetByViajeId
	}
	
	#endregion ViajeHotelSelectMethod

	#region ViajeHotelFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ViajeHotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeHotelFilter : SqlFilter<ViajeHotelColumn>
	{
	}
	
	#endregion ViajeHotelFilter

	#region ViajeHotelExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ViajeHotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeHotelExpressionBuilder : SqlExpressionBuilder<ViajeHotelColumn>
	{
	}
	
	#endregion ViajeHotelExpressionBuilder	

	#region ViajeHotelProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ViajeHotelChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="ViajeHotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ViajeHotelProperty : ChildEntityProperty<ViajeHotelChildEntityTypes>
	{
	}
	
	#endregion ViajeHotelProperty
}

