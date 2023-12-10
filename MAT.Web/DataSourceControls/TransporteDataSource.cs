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
	/// Represents the DataRepository.TransporteProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(TransporteDataSourceDesigner))]
	public class TransporteDataSource : ProviderDataSource<Transporte, TransporteKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TransporteDataSource class.
		/// </summary>
		public TransporteDataSource() : base(new TransporteService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the TransporteDataSourceView used by the TransporteDataSource.
		/// </summary>
		protected TransporteDataSourceView TransporteView
		{
			get { return ( View as TransporteDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the TransporteDataSource control invokes to retrieve data.
		/// </summary>
		public TransporteSelectMethod SelectMethod
		{
			get
			{
				TransporteSelectMethod selectMethod = TransporteSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (TransporteSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the TransporteDataSourceView class that is to be
		/// used by the TransporteDataSource.
		/// </summary>
		/// <returns>An instance of the TransporteDataSourceView class.</returns>
		protected override BaseDataSourceView<Transporte, TransporteKey> GetNewDataSourceView()
		{
			return new TransporteDataSourceView(this, DefaultViewName);
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
	/// Supports the TransporteDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class TransporteDataSourceView : ProviderDataSourceView<Transporte, TransporteKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TransporteDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the TransporteDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public TransporteDataSourceView(TransporteDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal TransporteDataSource TransporteOwner
		{
			get { return Owner as TransporteDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal TransporteSelectMethod SelectMethod
		{
			get { return TransporteOwner.SelectMethod; }
			set { TransporteOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal TransporteService TransporteProvider
		{
			get { return Provider as TransporteService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Transporte> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Transporte> results = null;
			Transporte item;
			count = 0;
			
			System.Guid _transporteId;

			switch ( SelectMethod )
			{
				case TransporteSelectMethod.Get:
					TransporteKey entityKey  = new TransporteKey();
					entityKey.Load(values);
					item = TransporteProvider.Get(entityKey);
					results = new TList<Transporte>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case TransporteSelectMethod.GetAll:
                    results = TransporteProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case TransporteSelectMethod.GetPaged:
					results = TransporteProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case TransporteSelectMethod.Find:
					if ( FilterParameters != null )
						results = TransporteProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = TransporteProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case TransporteSelectMethod.GetByTransporteId:
					_transporteId = ( values["TransporteId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["TransporteId"], typeof(System.Guid)) : Guid.Empty;
					item = TransporteProvider.GetByTransporteId(_transporteId);
					results = new TList<Transporte>();
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
			if ( SelectMethod == TransporteSelectMethod.Get || SelectMethod == TransporteSelectMethod.GetByTransporteId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Transporte entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.TransporteId == Guid.Empty )
				entity.TransporteId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Transporte entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					TransporteProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Transporte> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			TransporteProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region TransporteDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the TransporteDataSource class.
	/// </summary>
	public class TransporteDataSourceDesigner : ProviderDataSourceDesigner<Transporte, TransporteKey>
	{
		/// <summary>
		/// Initializes a new instance of the TransporteDataSourceDesigner class.
		/// </summary>
		public TransporteDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public TransporteSelectMethod SelectMethod
		{
			get { return ((TransporteDataSource) DataSource).SelectMethod; }
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
				actions.Add(new TransporteDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region TransporteDataSourceActionList

	/// <summary>
	/// Supports the TransporteDataSourceDesigner class.
	/// </summary>
	internal class TransporteDataSourceActionList : DesignerActionList
	{
		private TransporteDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the TransporteDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public TransporteDataSourceActionList(TransporteDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public TransporteSelectMethod SelectMethod
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

	#endregion TransporteDataSourceActionList
	
	#endregion TransporteDataSourceDesigner
	
	#region TransporteSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the TransporteDataSource.SelectMethod property.
	/// </summary>
	public enum TransporteSelectMethod
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
		/// Represents the GetByTransporteId method.
		/// </summary>
		GetByTransporteId
	}
	
	#endregion TransporteSelectMethod

	#region TransporteFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Transporte"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TransporteFilter : SqlFilter<TransporteColumn>
	{
	}
	
	#endregion TransporteFilter

	#region TransporteExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Transporte"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TransporteExpressionBuilder : SqlExpressionBuilder<TransporteColumn>
	{
	}
	
	#endregion TransporteExpressionBuilder	

	#region TransporteProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;TransporteChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Transporte"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class TransporteProperty : ChildEntityProperty<TransporteChildEntityTypes>
	{
	}
	
	#endregion TransporteProperty
}

