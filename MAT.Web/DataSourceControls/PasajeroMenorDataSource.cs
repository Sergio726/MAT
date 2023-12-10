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
	/// Represents the DataRepository.PasajeroMenorProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PasajeroMenorDataSourceDesigner))]
	public class PasajeroMenorDataSource : ProviderDataSource<PasajeroMenor, PasajeroMenorKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorDataSource class.
		/// </summary>
		public PasajeroMenorDataSource() : base(new PasajeroMenorService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PasajeroMenorDataSourceView used by the PasajeroMenorDataSource.
		/// </summary>
		protected PasajeroMenorDataSourceView PasajeroMenorView
		{
			get { return ( View as PasajeroMenorDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PasajeroMenorDataSource control invokes to retrieve data.
		/// </summary>
		public PasajeroMenorSelectMethod SelectMethod
		{
			get
			{
				PasajeroMenorSelectMethod selectMethod = PasajeroMenorSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PasajeroMenorSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PasajeroMenorDataSourceView class that is to be
		/// used by the PasajeroMenorDataSource.
		/// </summary>
		/// <returns>An instance of the PasajeroMenorDataSourceView class.</returns>
		protected override BaseDataSourceView<PasajeroMenor, PasajeroMenorKey> GetNewDataSourceView()
		{
			return new PasajeroMenorDataSourceView(this, DefaultViewName);
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
	/// Supports the PasajeroMenorDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PasajeroMenorDataSourceView : ProviderDataSourceView<PasajeroMenor, PasajeroMenorKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PasajeroMenorDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PasajeroMenorDataSourceView(PasajeroMenorDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PasajeroMenorDataSource PasajeroMenorOwner
		{
			get { return Owner as PasajeroMenorDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PasajeroMenorSelectMethod SelectMethod
		{
			get { return PasajeroMenorOwner.SelectMethod; }
			set { PasajeroMenorOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PasajeroMenorService PasajeroMenorProvider
		{
			get { return Provider as PasajeroMenorService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<PasajeroMenor> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<PasajeroMenor> results = null;
			PasajeroMenor item;
			count = 0;
			
			System.Int32 _id;
			System.Guid _menorid;
			System.Guid _pasajeroid;

			switch ( SelectMethod )
			{
				case PasajeroMenorSelectMethod.Get:
					PasajeroMenorKey entityKey  = new PasajeroMenorKey();
					entityKey.Load(values);
					item = PasajeroMenorProvider.Get(entityKey);
					results = new TList<PasajeroMenor>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PasajeroMenorSelectMethod.GetAll:
                    results = PasajeroMenorProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PasajeroMenorSelectMethod.GetPaged:
					results = PasajeroMenorProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PasajeroMenorSelectMethod.Find:
					if ( FilterParameters != null )
						results = PasajeroMenorProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PasajeroMenorProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PasajeroMenorSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = PasajeroMenorProvider.GetById(_id);
					results = new TList<PasajeroMenor>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case PasajeroMenorSelectMethod.GetByMenorid:
					_menorid = ( values["Menorid"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Menorid"], typeof(System.Guid)) : Guid.Empty;
					results = PasajeroMenorProvider.GetByMenorid(_menorid, this.StartIndex, this.PageSize, out count);
					break;
				case PasajeroMenorSelectMethod.GetByPasajeroid:
					_pasajeroid = ( values["Pasajeroid"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Pasajeroid"], typeof(System.Guid)) : Guid.Empty;
					results = PasajeroMenorProvider.GetByPasajeroid(_pasajeroid, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == PasajeroMenorSelectMethod.Get || SelectMethod == PasajeroMenorSelectMethod.GetById )
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
				PasajeroMenor entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PasajeroMenorProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<PasajeroMenor> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PasajeroMenorProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PasajeroMenorDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PasajeroMenorDataSource class.
	/// </summary>
	public class PasajeroMenorDataSourceDesigner : ProviderDataSourceDesigner<PasajeroMenor, PasajeroMenorKey>
	{
		/// <summary>
		/// Initializes a new instance of the PasajeroMenorDataSourceDesigner class.
		/// </summary>
		public PasajeroMenorDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PasajeroMenorSelectMethod SelectMethod
		{
			get { return ((PasajeroMenorDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PasajeroMenorDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PasajeroMenorDataSourceActionList

	/// <summary>
	/// Supports the PasajeroMenorDataSourceDesigner class.
	/// </summary>
	internal class PasajeroMenorDataSourceActionList : DesignerActionList
	{
		private PasajeroMenorDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PasajeroMenorDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PasajeroMenorDataSourceActionList(PasajeroMenorDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PasajeroMenorSelectMethod SelectMethod
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

	#endregion PasajeroMenorDataSourceActionList
	
	#endregion PasajeroMenorDataSourceDesigner
	
	#region PasajeroMenorSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PasajeroMenorDataSource.SelectMethod property.
	/// </summary>
	public enum PasajeroMenorSelectMethod
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
		GetById,
		/// <summary>
		/// Represents the GetByMenorid method.
		/// </summary>
		GetByMenorid,
		/// <summary>
		/// Represents the GetByPasajeroid method.
		/// </summary>
		GetByPasajeroid
	}
	
	#endregion PasajeroMenorSelectMethod

	#region PasajeroMenorFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroMenor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroMenorFilter : SqlFilter<PasajeroMenorColumn>
	{
	}
	
	#endregion PasajeroMenorFilter

	#region PasajeroMenorExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroMenor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroMenorExpressionBuilder : SqlExpressionBuilder<PasajeroMenorColumn>
	{
	}
	
	#endregion PasajeroMenorExpressionBuilder	

	#region PasajeroMenorProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PasajeroMenorChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroMenor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroMenorProperty : ChildEntityProperty<PasajeroMenorChildEntityTypes>
	{
	}
	
	#endregion PasajeroMenorProperty
}

