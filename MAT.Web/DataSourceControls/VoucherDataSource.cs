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
	/// Represents the DataRepository.VoucherProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(VoucherDataSourceDesigner))]
	public class VoucherDataSource : ProviderDataSource<Voucher, VoucherKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VoucherDataSource class.
		/// </summary>
		public VoucherDataSource() : base(new VoucherService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the VoucherDataSourceView used by the VoucherDataSource.
		/// </summary>
		protected VoucherDataSourceView VoucherView
		{
			get { return ( View as VoucherDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the VoucherDataSource control invokes to retrieve data.
		/// </summary>
		public VoucherSelectMethod SelectMethod
		{
			get
			{
				VoucherSelectMethod selectMethod = VoucherSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (VoucherSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the VoucherDataSourceView class that is to be
		/// used by the VoucherDataSource.
		/// </summary>
		/// <returns>An instance of the VoucherDataSourceView class.</returns>
		protected override BaseDataSourceView<Voucher, VoucherKey> GetNewDataSourceView()
		{
			return new VoucherDataSourceView(this, DefaultViewName);
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
	/// Supports the VoucherDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class VoucherDataSourceView : ProviderDataSourceView<Voucher, VoucherKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VoucherDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the VoucherDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public VoucherDataSourceView(VoucherDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal VoucherDataSource VoucherOwner
		{
			get { return Owner as VoucherDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal VoucherSelectMethod SelectMethod
		{
			get { return VoucherOwner.SelectMethod; }
			set { VoucherOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal VoucherService VoucherProvider
		{
			get { return Provider as VoucherService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Voucher> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Voucher> results = null;
			Voucher item;
			count = 0;
			
			System.Guid _voucherId;
			System.Guid? _vendedorId_nullable;

			switch ( SelectMethod )
			{
				case VoucherSelectMethod.Get:
					VoucherKey entityKey  = new VoucherKey();
					entityKey.Load(values);
					item = VoucherProvider.Get(entityKey);
					results = new TList<Voucher>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case VoucherSelectMethod.GetAll:
                    results = VoucherProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case VoucherSelectMethod.GetPaged:
					results = VoucherProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case VoucherSelectMethod.Find:
					if ( FilterParameters != null )
						results = VoucherProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = VoucherProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case VoucherSelectMethod.GetByVoucherId:
					_voucherId = ( values["VoucherId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["VoucherId"], typeof(System.Guid)) : Guid.Empty;
					item = VoucherProvider.GetByVoucherId(_voucherId);
					results = new TList<Voucher>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case VoucherSelectMethod.GetByVendedorId:
					_vendedorId_nullable = (System.Guid?) EntityUtil.ChangeType(values["VendedorId"], typeof(System.Guid?));
					results = VoucherProvider.GetByVendedorId(_vendedorId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == VoucherSelectMethod.Get || SelectMethod == VoucherSelectMethod.GetByVoucherId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Voucher entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.VoucherId == Guid.Empty )
				entity.VoucherId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Voucher entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					VoucherProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Voucher> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			VoucherProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region VoucherDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the VoucherDataSource class.
	/// </summary>
	public class VoucherDataSourceDesigner : ProviderDataSourceDesigner<Voucher, VoucherKey>
	{
		/// <summary>
		/// Initializes a new instance of the VoucherDataSourceDesigner class.
		/// </summary>
		public VoucherDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public VoucherSelectMethod SelectMethod
		{
			get { return ((VoucherDataSource) DataSource).SelectMethod; }
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
				actions.Add(new VoucherDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region VoucherDataSourceActionList

	/// <summary>
	/// Supports the VoucherDataSourceDesigner class.
	/// </summary>
	internal class VoucherDataSourceActionList : DesignerActionList
	{
		private VoucherDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the VoucherDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public VoucherDataSourceActionList(VoucherDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public VoucherSelectMethod SelectMethod
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

	#endregion VoucherDataSourceActionList
	
	#endregion VoucherDataSourceDesigner
	
	#region VoucherSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the VoucherDataSource.SelectMethod property.
	/// </summary>
	public enum VoucherSelectMethod
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
		/// Represents the GetByVoucherId method.
		/// </summary>
		GetByVoucherId,
		/// <summary>
		/// Represents the GetByVendedorId method.
		/// </summary>
		GetByVendedorId
	}
	
	#endregion VoucherSelectMethod

	#region VoucherFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Voucher"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VoucherFilter : SqlFilter<VoucherColumn>
	{
	}
	
	#endregion VoucherFilter

	#region VoucherExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Voucher"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VoucherExpressionBuilder : SqlExpressionBuilder<VoucherColumn>
	{
	}
	
	#endregion VoucherExpressionBuilder	

	#region VoucherProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;VoucherChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Voucher"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VoucherProperty : ChildEntityProperty<VoucherChildEntityTypes>
	{
	}
	
	#endregion VoucherProperty
}

