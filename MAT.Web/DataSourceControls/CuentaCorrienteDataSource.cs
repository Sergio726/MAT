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
	/// Represents the DataRepository.CuentaCorrienteProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
    //[Designer(typeof(CuentaCorrienteDataSourceDesigner))]
    //public class CuentaCorrienteDataSource : ProviderDataSource<CuentaCorriente, CuentaCorrienteKey>
    //{
    //    #region Constructors

    //    /// <summary>
    //    /// Initializes a new instance of the CuentaCorrienteDataSource class.
    //    /// </summary>
    //    public CuentaCorrienteDataSource() : base(new CuentaCorrienteService())
    //    {
    //    }

    //    #endregion Constructors
		
    //    #region Properties
		
    //    /// <summary>
    //    /// Gets a reference to the CuentaCorrienteDataSourceView used by the CuentaCorrienteDataSource.
    //    /// </summary>
    //    protected CuentaCorrienteDataSourceView CuentaCorrienteView
    //    {
    //        get { return ( View as CuentaCorrienteDataSourceView ); }
    //    }
		
    //    /// <summary>
    //    /// Gets or sets the name of the method or function that
    //    /// the CuentaCorrienteDataSource control invokes to retrieve data.
    //    /// </summary>
    //    public CuentaCorrienteSelectMethod SelectMethod
    //    {
    //        get
    //        {
    //            CuentaCorrienteSelectMethod selectMethod = CuentaCorrienteSelectMethod.GetAll;
    //            Object method = ViewState["SelectMethod"];
    //            if ( method != null )
    //            {
    //                selectMethod = (CuentaCorrienteSelectMethod) method;
    //            }
    //            return selectMethod;
    //        }
    //        set { ViewState["SelectMethod"] = value; }
    //    }

    //    #endregion Properties
		
    //    #region Methods

    //    /// <summary>
    //    /// Creates a new instance of the CuentaCorrienteDataSourceView class that is to be
    //    /// used by the CuentaCorrienteDataSource.
    //    /// </summary>
    //    /// <returns>An instance of the CuentaCorrienteDataSourceView class.</returns>
    //    protected override BaseDataSourceView<CuentaCorriente, CuentaCorrienteKey> GetNewDataSourceView()
    //    {
    //        return new CuentaCorrienteDataSourceView(this, DefaultViewName);
    //    }
		
    //    /// <summary>
    //    /// Creates a cache hashing key based on the startIndex, pageSize and the SelectMethod being used.
    //    /// </summary>
    //    /// <param name="startIndex">The current start row index.</param>
    //    /// <param name="pageSize">The current page size.</param>
    //    /// <returns>A string that can be used as a key for caching purposes.</returns>
    //    protected override string CacheHashKey(int startIndex, int pageSize)
    //    {
    //        return String.Format("{0}:{1}:{2}", SelectMethod, startIndex, pageSize);
    //    }
		
    //    #endregion Methods
    //}
	
	/// <summary>
	/// Supports the CuentaCorrienteDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
    //public class CuentaCorrienteDataSourceView : ProviderDataSourceView<CuentaCorriente, CuentaCorrienteKey>
    //{
    //    #region Declarations

    //    #endregion Declarations
		
    //    #region Constructors

    //    /// <summary>
    //    /// Initializes a new instance of the CuentaCorrienteDataSourceView class.
    //    /// </summary>
    //    /// <param name="owner">A reference to the CuentaCorrienteDataSource which created this instance.</param>
    //    /// <param name="viewName">The name of the view.</param>
    //    public CuentaCorrienteDataSourceView(CuentaCorrienteDataSource owner, String viewName)
    //        : base(owner, viewName)
    //    {
    //    }
		
    //    #endregion Constructors
		
    //    #region Properties

    //    /// <summary>
    //    /// Gets a strongly-typed reference to the Owner property.
    //    /// </summary>
    //    internal CuentaCorrienteDataSource CuentaCorrienteOwner
    //    {
    //        get { return Owner as CuentaCorrienteDataSource; }
    //    }

    //    /// <summary>
    //    /// Gets or sets the name of the method or function that
    //    /// the DataSource control invokes to retrieve data.
    //    /// </summary>
    //    internal CuentaCorrienteSelectMethod SelectMethod
    //    {
    //        get { return CuentaCorrienteOwner.SelectMethod; }
    //        set { CuentaCorrienteOwner.SelectMethod = value; }
    //    }

    //    /// <summary>
    //    /// Gets a strongly typed reference to the Provider property.
    //    /// </summary>
    //    //internal CuentaCorrienteService CuentaCorrienteProvider
    //    //{
    //    //    get { return Provider as CuentaCorrienteService; }
    //    //}

    //    #endregion Properties
		
    //    #region Methods
		 
    //    /// <summary>
    //    /// Gets a collection of Entity objects based on the value of the SelectMethod property.
    //    /// </summary>
    //    /// <param name="count">The total number of rows in the DataSource.</param>
    //    /// <param name="values"></param>
    //    /// <returns>A collection of Entity objects.</returns>
    //    protected override IList<CuentaCorriente> GetSelectData(IDictionary values, out int count)
    //    {
    //        if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
    //        Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
    //        IList<CuentaCorriente> results = null;
    //        CuentaCorriente item;
    //        count = 0;
			
    //        System.Guid _cuentaCorrienteId;
    //        System.Guid _clienteId;

    //        switch ( SelectMethod )
    //        {
    //            case CuentaCorrienteSelectMethod.Get:
    //                CuentaCorrienteKey entityKey  = new CuentaCorrienteKey();
    //                entityKey.Load(values);
    //                item = CuentaCorrienteProvider.Get(entityKey);
    //                results = new TList<CuentaCorriente>();
    //                if ( item != null ) results.Add(item);
    //                count = results.Count;
    //                break;
    //            case CuentaCorrienteSelectMethod.GetAll:
    //                results = CuentaCorrienteProvider.GetAll(StartIndex, PageSize, out count);
    //                break;
    //            case CuentaCorrienteSelectMethod.GetPaged:
    //                results = CuentaCorrienteProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
    //                break;
    //            case CuentaCorrienteSelectMethod.Find:
    //                if ( FilterParameters != null )
    //                    results = CuentaCorrienteProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
    //                else
    //                    results = CuentaCorrienteProvider.Find(WhereClause, StartIndex, PageSize, out count);
    //                break;
    //            // PK
    //            case CuentaCorrienteSelectMethod.GetByCuentaCorrienteId:
    //                _cuentaCorrienteId = ( values["CuentaCorrienteId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["CuentaCorrienteId"], typeof(System.Guid)) : Guid.Empty;
    //                item = CuentaCorrienteProvider.GetByCuentaCorrienteId(_cuentaCorrienteId);
    //                results = new TList<CuentaCorriente>();
    //                if ( item != null ) results.Add(item);
    //                count = results.Count;
    //                break;
    //            // IX
    //            // FK
    //            case CuentaCorrienteSelectMethod.GetByClienteId:
    //                _clienteId = ( values["ClienteId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["ClienteId"], typeof(System.Guid)) : Guid.Empty;
    //                results = CuentaCorrienteProvider.GetByClienteId(_clienteId, this.StartIndex, this.PageSize, out count);
    //                break;
    //            // M:M
    //            // Custom
    //            default:
    //                break;
    //        }

    //        if ( results != null && count < 1 )
    //        {
    //            count = results.Count;

    //            if ( !String.IsNullOrEmpty(CustomMethodRecordCountParamName) )
    //            {
    //                object objCustomCount = EntityUtil.ChangeType(customOutput[CustomMethodRecordCountParamName], typeof(Int32));
					
    //                if ( objCustomCount != null )
    //                {
    //                    count = (int) objCustomCount;
    //                }
    //            }
    //        }
			
    //        return results;
    //    }
		
    //    /// <summary>
    //    /// Gets the values of any supplied parameters for internal caching.
    //    /// </summary>
    //    /// <param name="values">An IDictionary object of name/value pairs.</param>
    //    protected override void GetSelectParameters(IDictionary values)
    //    {
    //        if ( SelectMethod == CuentaCorrienteSelectMethod.Get || SelectMethod == CuentaCorrienteSelectMethod.GetByCuentaCorrienteId )
    //        {
    //            EntityId = GetEntityKey(values);
    //        }
    //    }

    //    /// <summary>
    //    /// Sets the primary key values of the specified Entity object.
    //    /// </summary>
    //    /// <param name="entity">The Entity object to update.</param>
    //    protected override void SetEntityKeyValues(CuentaCorriente entity)
    //    {
    //        base.SetEntityKeyValues(entity);
			
    //        // make sure primary key column(s) have been set
    //        if ( entity.CuentaCorrienteId == Guid.Empty )
    //            entity.CuentaCorrienteId = Guid.NewGuid();
    //    }
		
    //    /// <summary>
    //    /// Performs a DeepLoad operation for the current entity if it has
    //    /// not already been performed.
    //    /// </summary>
    //    internal override void DeepLoad()
    //    {
    //        if ( !IsDeepLoaded )
    //        {
    //            CuentaCorriente entity = GetCurrentEntity();
				
    //            if ( entity != null )
    //            {
    //                // init transaction manager
    //                GetTransactionManager();
    //                // execute deep load method
    //                CuentaCorrienteProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
    //                // set loaded flag
    //                IsDeepLoaded = true;
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Performs a DeepLoad operation on the specified entity collection.
    //    /// </summary>
    //    /// <param name="entityList"></param>
    //    /// <param name="properties"></param>
    //    internal override void DeepLoad(TList<CuentaCorriente> entityList, ProviderDataSourceDeepLoadList properties)
    //    {
    //        // init transaction manager
    //        GetTransactionManager();
    //        // execute deep load method
    //        CuentaCorrienteProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
    //    }

    //    #endregion Select Methods
    //}
	
	#region CuentaCorrienteDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the CuentaCorrienteDataSource class.
	/// </summary>
    //public class CuentaCorrienteDataSourceDesigner : ProviderDataSourceDesigner<CuentaCorriente, CuentaCorrienteKey>
    //{
    //    /// <summary>
    //    /// Initializes a new instance of the CuentaCorrienteDataSourceDesigner class.
    //    /// </summary>
    //    public CuentaCorrienteDataSourceDesigner()
    //    {
    //    }

    //    /// <summary>
    //    /// Gets or sets the SelectMethod property.
    //    /// </summary>
    //    public CuentaCorrienteSelectMethod SelectMethod
    //    {
    //        get { return ((CuentaCorrienteDataSource) DataSource).SelectMethod; }
    //        set { SetPropertyValue("SelectMethod", value); }
    //    }

    //    /// <summary>Gets the designer action list collection for this designer.</summary>
    //    /// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection"/>
    //    /// associated with this designer.</returns>
    //    public override DesignerActionListCollection ActionLists
    //    {
    //        get
    //        {
    //            DesignerActionListCollection actions = new DesignerActionListCollection();
    //            actions.Add(new CuentaCorrienteDataSourceActionList(this));
    //            actions.AddRange(base.ActionLists);
    //            return actions;
    //        }
    //    }
    //}

	#region CuentaCorrienteDataSourceActionList

	/// <summary>
	/// Supports the CuentaCorrienteDataSourceDesigner class.
	/// </summary>
    //internal class CuentaCorrienteDataSourceActionList : DesignerActionList
    //{
    //    private CuentaCorrienteDataSourceDesigner _designer;

    //    /// <summary>
    //    /// Initializes a new instance of the CuentaCorrienteDataSourceActionList class.
    //    /// </summary>
    //    /// <param name="designer"></param>
    //    public CuentaCorrienteDataSourceActionList(CuentaCorrienteDataSourceDesigner designer) : base(designer.Component)
    //    {
    //        _designer = designer;
    //    }

    //    /// <summary>
    //    /// Gets or sets the SelectMethod property.
    //    /// </summary>
    //    public CuentaCorrienteSelectMethod SelectMethod
    //    {
    //        get { return _designer.SelectMethod; }
    //        set { _designer.SelectMethod = value; }
    //    }

    //    /// <summary>
    //    /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
    //    /// objects contained in the list.
    //    /// </summary>
    //    /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
    //    /// array that contains the items in this list.</returns>
    //    public override DesignerActionItemCollection GetSortedActionItems()
    //    {
    //        DesignerActionItemCollection items = new DesignerActionItemCollection();
    //        items.Add(new DesignerActionPropertyItem("SelectMethod", "Select Method", "Methods"));
    //        return items;
    //    }
    //}

	#endregion CuentaCorrienteDataSourceActionList
	
	#endregion CuentaCorrienteDataSourceDesigner
	
	#region CuentaCorrienteSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the CuentaCorrienteDataSource.SelectMethod property.
	/// </summary>
	public enum CuentaCorrienteSelectMethod
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
		/// Represents the GetByCuentaCorrienteId method.
		/// </summary>
		GetByCuentaCorrienteId,
		/// <summary>
		/// Represents the GetByClienteId method.
		/// </summary>
		GetByClienteId
	}
	
	#endregion CuentaCorrienteSelectMethod

	#region CuentaCorrienteFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CuentaCorriente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaCorrienteFilter : SqlFilter<CuentaCorrienteColumn>
	{
	}
	
	#endregion CuentaCorrienteFilter

	#region CuentaCorrienteExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CuentaCorriente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaCorrienteExpressionBuilder : SqlExpressionBuilder<CuentaCorrienteColumn>
	{
	}
	
	#endregion CuentaCorrienteExpressionBuilder	

	#region CuentaCorrienteProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;CuentaCorrienteChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="CuentaCorriente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CuentaCorrienteProperty : ChildEntityProperty<CuentaCorrienteChildEntityTypes>
	{
	}
	
	#endregion CuentaCorrienteProperty
}

