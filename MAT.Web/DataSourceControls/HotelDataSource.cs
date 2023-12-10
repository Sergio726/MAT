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
	/// Represents the DataRepository.HotelProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(HotelDataSourceDesigner))]
	public class HotelDataSource : ProviderDataSource<Hotel, HotelKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HotelDataSource class.
		/// </summary>
		public HotelDataSource() : base(new HotelService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the HotelDataSourceView used by the HotelDataSource.
		/// </summary>
		protected HotelDataSourceView HotelView
		{
			get { return ( View as HotelDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the HotelDataSource control invokes to retrieve data.
		/// </summary>
		public HotelSelectMethod SelectMethod
		{
			get
			{
				HotelSelectMethod selectMethod = HotelSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (HotelSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the HotelDataSourceView class that is to be
		/// used by the HotelDataSource.
		/// </summary>
		/// <returns>An instance of the HotelDataSourceView class.</returns>
		protected override BaseDataSourceView<Hotel, HotelKey> GetNewDataSourceView()
		{
			return new HotelDataSourceView(this, DefaultViewName);
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
	/// Supports the HotelDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class HotelDataSourceView : ProviderDataSourceView<Hotel, HotelKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HotelDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the HotelDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public HotelDataSourceView(HotelDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal HotelDataSource HotelOwner
		{
			get { return Owner as HotelDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal HotelSelectMethod SelectMethod
		{
			get { return HotelOwner.SelectMethod; }
			set { HotelOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal HotelService HotelProvider
		{
			get { return Provider as HotelService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Hotel> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Hotel> results = null;
			Hotel item;
			count = 0;
			
			System.Guid _hotelId;
			System.Int32? _localidadId_nullable;

			switch ( SelectMethod )
			{
				case HotelSelectMethod.Get:
					HotelKey entityKey  = new HotelKey();
					entityKey.Load(values);
					item = HotelProvider.Get(entityKey);
					results = new TList<Hotel>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case HotelSelectMethod.GetAll:
                    results = HotelProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case HotelSelectMethod.GetPaged:
					results = HotelProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case HotelSelectMethod.Find:
					if ( FilterParameters != null )
						results = HotelProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = HotelProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case HotelSelectMethod.GetByHotelId:
					_hotelId = ( values["HotelId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["HotelId"], typeof(System.Guid)) : Guid.Empty;
					item = HotelProvider.GetByHotelId(_hotelId);
					results = new TList<Hotel>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case HotelSelectMethod.GetByLocalidadId:
					_localidadId_nullable = (System.Int32?) EntityUtil.ChangeType(values["LocalidadId"], typeof(System.Int32?));
					results = HotelProvider.GetByLocalidadId(_localidadId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == HotelSelectMethod.Get || SelectMethod == HotelSelectMethod.GetByHotelId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Hotel entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.HotelId == Guid.Empty )
				entity.HotelId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Hotel entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					HotelProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Hotel> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			HotelProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region HotelDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the HotelDataSource class.
	/// </summary>
	public class HotelDataSourceDesigner : ProviderDataSourceDesigner<Hotel, HotelKey>
	{
		/// <summary>
		/// Initializes a new instance of the HotelDataSourceDesigner class.
		/// </summary>
		public HotelDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public HotelSelectMethod SelectMethod
		{
			get { return ((HotelDataSource) DataSource).SelectMethod; }
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
				actions.Add(new HotelDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region HotelDataSourceActionList

	/// <summary>
	/// Supports the HotelDataSourceDesigner class.
	/// </summary>
	internal class HotelDataSourceActionList : DesignerActionList
	{
		private HotelDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the HotelDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public HotelDataSourceActionList(HotelDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public HotelSelectMethod SelectMethod
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

	#endregion HotelDataSourceActionList
	
	#endregion HotelDataSourceDesigner
	
	#region HotelSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the HotelDataSource.SelectMethod property.
	/// </summary>
	public enum HotelSelectMethod
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
		/// Represents the GetByHotelId method.
		/// </summary>
		GetByHotelId,
		/// <summary>
		/// Represents the GetByLocalidadId method.
		/// </summary>
		GetByLocalidadId
	}
	
	#endregion HotelSelectMethod

	#region HotelFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Hotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HotelFilter : SqlFilter<HotelColumn>
	{
	}
	
	#endregion HotelFilter

	#region HotelExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Hotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HotelExpressionBuilder : SqlExpressionBuilder<HotelColumn>
	{
	}
	
	#endregion HotelExpressionBuilder	

	#region HotelProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;HotelChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Hotel"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HotelProperty : ChildEntityProperty<HotelChildEntityTypes>
	{
	}
	
	#endregion HotelProperty
}

