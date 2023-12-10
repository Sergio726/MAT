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
	/// Represents the DataRepository.HabitacionProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(HabitacionDataSourceDesigner))]
	public class HabitacionDataSource : ProviderDataSource<Habitacion, HabitacionKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionDataSource class.
		/// </summary>
		public HabitacionDataSource() : base(new HabitacionService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the HabitacionDataSourceView used by the HabitacionDataSource.
		/// </summary>
		protected HabitacionDataSourceView HabitacionView
		{
			get { return ( View as HabitacionDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the HabitacionDataSource control invokes to retrieve data.
		/// </summary>
		public HabitacionSelectMethod SelectMethod
		{
			get
			{
				HabitacionSelectMethod selectMethod = HabitacionSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (HabitacionSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the HabitacionDataSourceView class that is to be
		/// used by the HabitacionDataSource.
		/// </summary>
		/// <returns>An instance of the HabitacionDataSourceView class.</returns>
		protected override BaseDataSourceView<Habitacion, HabitacionKey> GetNewDataSourceView()
		{
			return new HabitacionDataSourceView(this, DefaultViewName);
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
	/// Supports the HabitacionDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class HabitacionDataSourceView : ProviderDataSourceView<Habitacion, HabitacionKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HabitacionDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the HabitacionDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public HabitacionDataSourceView(HabitacionDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal HabitacionDataSource HabitacionOwner
		{
			get { return Owner as HabitacionDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal HabitacionSelectMethod SelectMethod
		{
			get { return HabitacionOwner.SelectMethod; }
			set { HabitacionOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal HabitacionService HabitacionProvider
		{
			get { return Provider as HabitacionService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Habitacion> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Habitacion> results = null;
			Habitacion item;
			count = 0;
			
			System.Guid _habitacionId;
			System.Guid? _hotelId_nullable;

			switch ( SelectMethod )
			{
				case HabitacionSelectMethod.Get:
					HabitacionKey entityKey  = new HabitacionKey();
					entityKey.Load(values);
					item = HabitacionProvider.Get(entityKey);
					results = new TList<Habitacion>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case HabitacionSelectMethod.GetAll:
                    results = HabitacionProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case HabitacionSelectMethod.GetPaged:
					results = HabitacionProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case HabitacionSelectMethod.Find:
					if ( FilterParameters != null )
						results = HabitacionProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = HabitacionProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case HabitacionSelectMethod.GetByHabitacionId:
					_habitacionId = ( values["HabitacionId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["HabitacionId"], typeof(System.Guid)) : Guid.Empty;
					item = HabitacionProvider.GetByHabitacionId(_habitacionId);
					results = new TList<Habitacion>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case HabitacionSelectMethod.GetByHotelId:
					_hotelId_nullable = (System.Guid?) EntityUtil.ChangeType(values["HotelId"], typeof(System.Guid?));
					results = HabitacionProvider.GetByHotelId(_hotelId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == HabitacionSelectMethod.Get || SelectMethod == HabitacionSelectMethod.GetByHabitacionId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Habitacion entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.HabitacionId == Guid.Empty )
				entity.HabitacionId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Habitacion entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					HabitacionProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Habitacion> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			HabitacionProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region HabitacionDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the HabitacionDataSource class.
	/// </summary>
	public class HabitacionDataSourceDesigner : ProviderDataSourceDesigner<Habitacion, HabitacionKey>
	{
		/// <summary>
		/// Initializes a new instance of the HabitacionDataSourceDesigner class.
		/// </summary>
		public HabitacionDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public HabitacionSelectMethod SelectMethod
		{
			get { return ((HabitacionDataSource) DataSource).SelectMethod; }
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
				actions.Add(new HabitacionDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region HabitacionDataSourceActionList

	/// <summary>
	/// Supports the HabitacionDataSourceDesigner class.
	/// </summary>
	internal class HabitacionDataSourceActionList : DesignerActionList
	{
		private HabitacionDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the HabitacionDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public HabitacionDataSourceActionList(HabitacionDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public HabitacionSelectMethod SelectMethod
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

	#endregion HabitacionDataSourceActionList
	
	#endregion HabitacionDataSourceDesigner
	
	#region HabitacionSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the HabitacionDataSource.SelectMethod property.
	/// </summary>
	public enum HabitacionSelectMethod
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
		/// Represents the GetByHabitacionId method.
		/// </summary>
		GetByHabitacionId,
		/// <summary>
		/// Represents the GetByHotelId method.
		/// </summary>
		GetByHotelId
	}
	
	#endregion HabitacionSelectMethod

	#region HabitacionFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Habitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionFilter : SqlFilter<HabitacionColumn>
	{
	}
	
	#endregion HabitacionFilter

	#region HabitacionExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Habitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionExpressionBuilder : SqlExpressionBuilder<HabitacionColumn>
	{
	}
	
	#endregion HabitacionExpressionBuilder	

	#region HabitacionProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;HabitacionChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Habitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HabitacionProperty : ChildEntityProperty<HabitacionChildEntityTypes>
	{
	}
	
	#endregion HabitacionProperty
}

