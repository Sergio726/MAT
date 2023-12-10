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
	/// Represents the DataRepository.ReservaHabitacionProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ReservaHabitacionDataSourceDesigner))]
	public class ReservaHabitacionDataSource : ProviderDataSource<ReservaHabitacion, ReservaHabitacionKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionDataSource class.
		/// </summary>
		public ReservaHabitacionDataSource() : base(new ReservaHabitacionService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ReservaHabitacionDataSourceView used by the ReservaHabitacionDataSource.
		/// </summary>
		protected ReservaHabitacionDataSourceView ReservaHabitacionView
		{
			get { return ( View as ReservaHabitacionDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ReservaHabitacionDataSource control invokes to retrieve data.
		/// </summary>
		public ReservaHabitacionSelectMethod SelectMethod
		{
			get
			{
				ReservaHabitacionSelectMethod selectMethod = ReservaHabitacionSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ReservaHabitacionSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ReservaHabitacionDataSourceView class that is to be
		/// used by the ReservaHabitacionDataSource.
		/// </summary>
		/// <returns>An instance of the ReservaHabitacionDataSourceView class.</returns>
		protected override BaseDataSourceView<ReservaHabitacion, ReservaHabitacionKey> GetNewDataSourceView()
		{
			return new ReservaHabitacionDataSourceView(this, DefaultViewName);
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
	/// Supports the ReservaHabitacionDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ReservaHabitacionDataSourceView : ProviderDataSourceView<ReservaHabitacion, ReservaHabitacionKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ReservaHabitacionDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ReservaHabitacionDataSourceView(ReservaHabitacionDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ReservaHabitacionDataSource ReservaHabitacionOwner
		{
			get { return Owner as ReservaHabitacionDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ReservaHabitacionSelectMethod SelectMethod
		{
			get { return ReservaHabitacionOwner.SelectMethod; }
			set { ReservaHabitacionOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ReservaHabitacionService ReservaHabitacionProvider
		{
			get { return Provider as ReservaHabitacionService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<ReservaHabitacion> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<ReservaHabitacion> results = null;
			ReservaHabitacion item;
			count = 0;
			
			System.Guid? _habitacionId_nullable;
			System.Guid _reservaHabitacionId;
			System.Guid? _pasajeId_nullable;
			System.Guid? _pasajeroId_nullable;

			switch ( SelectMethod )
			{
				case ReservaHabitacionSelectMethod.Get:
					ReservaHabitacionKey entityKey  = new ReservaHabitacionKey();
					entityKey.Load(values);
					item = ReservaHabitacionProvider.Get(entityKey);
					results = new TList<ReservaHabitacion>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ReservaHabitacionSelectMethod.GetAll:
                    results = ReservaHabitacionProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case ReservaHabitacionSelectMethod.GetPaged:
					results = ReservaHabitacionProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ReservaHabitacionSelectMethod.Find:
					if ( FilterParameters != null )
						results = ReservaHabitacionProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ReservaHabitacionProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ReservaHabitacionSelectMethod.GetByReservaHabitacionId:
					_reservaHabitacionId = ( values["ReservaHabitacionId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ReservaHabitacionId"], typeof(System.Guid)) : Guid.Empty;
					item = ReservaHabitacionProvider.GetByReservaHabitacionId(_reservaHabitacionId);
					results = new TList<ReservaHabitacion>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case ReservaHabitacionSelectMethod.GetByHabitacionId:
					_habitacionId_nullable = (System.Guid?) EntityUtil.ChangeType(values["HabitacionId"], typeof(System.Guid?));
					results = ReservaHabitacionProvider.GetByHabitacionId(_habitacionId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				case ReservaHabitacionSelectMethod.GetByPasajeId:
					_pasajeId_nullable = (System.Guid?) EntityUtil.ChangeType(values["PasajeId"], typeof(System.Guid?));
					results = ReservaHabitacionProvider.GetByPasajeId(_pasajeId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case ReservaHabitacionSelectMethod.GetByPasajeroId:
					_pasajeroId_nullable = (System.Guid?) EntityUtil.ChangeType(values["PasajeroId"], typeof(System.Guid?));
					results = ReservaHabitacionProvider.GetByPasajeroId(_pasajeroId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == ReservaHabitacionSelectMethod.Get || SelectMethod == ReservaHabitacionSelectMethod.GetByReservaHabitacionId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(ReservaHabitacion entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.ReservaHabitacionId == Guid.Empty )
				entity.ReservaHabitacionId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				ReservaHabitacion entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					ReservaHabitacionProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<ReservaHabitacion> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			ReservaHabitacionProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ReservaHabitacionDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ReservaHabitacionDataSource class.
	/// </summary>
	public class ReservaHabitacionDataSourceDesigner : ProviderDataSourceDesigner<ReservaHabitacion, ReservaHabitacionKey>
	{
		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionDataSourceDesigner class.
		/// </summary>
		public ReservaHabitacionDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ReservaHabitacionSelectMethod SelectMethod
		{
			get { return ((ReservaHabitacionDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ReservaHabitacionDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ReservaHabitacionDataSourceActionList

	/// <summary>
	/// Supports the ReservaHabitacionDataSourceDesigner class.
	/// </summary>
	internal class ReservaHabitacionDataSourceActionList : DesignerActionList
	{
		private ReservaHabitacionDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ReservaHabitacionDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ReservaHabitacionDataSourceActionList(ReservaHabitacionDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ReservaHabitacionSelectMethod SelectMethod
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

	#endregion ReservaHabitacionDataSourceActionList
	
	#endregion ReservaHabitacionDataSourceDesigner
	
	#region ReservaHabitacionSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ReservaHabitacionDataSource.SelectMethod property.
	/// </summary>
	public enum ReservaHabitacionSelectMethod
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
		/// Represents the GetByReservaHabitacionId method.
		/// </summary>
		GetByReservaHabitacionId,
		/// <summary>
		/// Represents the GetByPasajeId method.
		/// </summary>
		GetByPasajeId,
		/// <summary>
		/// Represents the GetByPasajeroId method.
		/// </summary>
		GetByPasajeroId
	}
	
	#endregion ReservaHabitacionSelectMethod

	#region ReservaHabitacionFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaHabitacionFilter : SqlFilter<ReservaHabitacionColumn>
	{
	}
	
	#endregion ReservaHabitacionFilter

	#region ReservaHabitacionExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="ReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaHabitacionExpressionBuilder : SqlExpressionBuilder<ReservaHabitacionColumn>
	{
	}
	
	#endregion ReservaHabitacionExpressionBuilder	

	#region ReservaHabitacionProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ReservaHabitacionChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="ReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaHabitacionProperty : ChildEntityProperty<ReservaHabitacionChildEntityTypes>
	{
	}
	
	#endregion ReservaHabitacionProperty
}

