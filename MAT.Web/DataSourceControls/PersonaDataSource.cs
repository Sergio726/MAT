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
	/// Represents the DataRepository.PersonaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PersonaDataSourceDesigner))]
	public class PersonaDataSource : ProviderDataSource<Persona, PersonaKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaDataSource class.
		/// </summary>
		public PersonaDataSource() : base(new PersonaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PersonaDataSourceView used by the PersonaDataSource.
		/// </summary>
		protected PersonaDataSourceView PersonaView
		{
			get { return ( View as PersonaDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PersonaDataSource control invokes to retrieve data.
		/// </summary>
		public PersonaSelectMethod SelectMethod
		{
			get
			{
				PersonaSelectMethod selectMethod = PersonaSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PersonaSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PersonaDataSourceView class that is to be
		/// used by the PersonaDataSource.
		/// </summary>
		/// <returns>An instance of the PersonaDataSourceView class.</returns>
		protected override BaseDataSourceView<Persona, PersonaKey> GetNewDataSourceView()
		{
			return new PersonaDataSourceView(this, DefaultViewName);
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
	/// Supports the PersonaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PersonaDataSourceView : ProviderDataSourceView<Persona, PersonaKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PersonaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PersonaDataSourceView(PersonaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PersonaDataSource PersonaOwner
		{
			get { return Owner as PersonaDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PersonaSelectMethod SelectMethod
		{
			get { return PersonaOwner.SelectMethod; }
			set { PersonaOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PersonaService PersonaProvider
		{
			get { return Provider as PersonaService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Persona> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Persona> results = null;
			Persona item;
			count = 0;
			
			System.Guid _personaId;

			switch ( SelectMethod )
			{
				case PersonaSelectMethod.Get:
					PersonaKey entityKey  = new PersonaKey();
					entityKey.Load(values);
					item = PersonaProvider.Get(entityKey);
					results = new TList<Persona>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PersonaSelectMethod.GetAll:
                    results = PersonaProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case PersonaSelectMethod.GetPaged:
					results = PersonaProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PersonaSelectMethod.Find:
					if ( FilterParameters != null )
						results = PersonaProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PersonaProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PersonaSelectMethod.GetByPersonaId:
					_personaId = ( values["PersonaId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["PersonaId"], typeof(System.Guid)) : Guid.Empty;
					item = PersonaProvider.GetByPersonaId(_personaId);
					results = new TList<Persona>();
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
			if ( SelectMethod == PersonaSelectMethod.Get || SelectMethod == PersonaSelectMethod.GetByPersonaId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Persona entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.PersonaId == Guid.Empty )
				entity.PersonaId = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Persona entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					PersonaProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Persona> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			PersonaProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PersonaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PersonaDataSource class.
	/// </summary>
	public class PersonaDataSourceDesigner : ProviderDataSourceDesigner<Persona, PersonaKey>
	{
		/// <summary>
		/// Initializes a new instance of the PersonaDataSourceDesigner class.
		/// </summary>
		public PersonaDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PersonaSelectMethod SelectMethod
		{
			get { return ((PersonaDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PersonaDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PersonaDataSourceActionList

	/// <summary>
	/// Supports the PersonaDataSourceDesigner class.
	/// </summary>
	internal class PersonaDataSourceActionList : DesignerActionList
	{
		private PersonaDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PersonaDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PersonaDataSourceActionList(PersonaDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PersonaSelectMethod SelectMethod
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

	#endregion PersonaDataSourceActionList
	
	#endregion PersonaDataSourceDesigner
	
	#region PersonaSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PersonaDataSource.SelectMethod property.
	/// </summary>
	public enum PersonaSelectMethod
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
		/// Represents the GetByPersonaId method.
		/// </summary>
		GetByPersonaId
	}
	
	#endregion PersonaSelectMethod

	#region PersonaFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Persona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaFilter : SqlFilter<PersonaColumn>
	{
	}
	
	#endregion PersonaFilter

	#region PersonaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Persona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaExpressionBuilder : SqlExpressionBuilder<PersonaColumn>
	{
	}
	
	#endregion PersonaExpressionBuilder	

	#region PersonaProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PersonaChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Persona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaProperty : ChildEntityProperty<PersonaChildEntityTypes>
	{
	}
	
	#endregion PersonaProperty
}

