#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using MAT.Entities;
using MAT.Data;
using MAT.Data.Bases;
using MAT.Services;
#endregion

namespace MAT.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.PersonaClienteProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(PersonaClienteDataSourceDesigner))]
	public class PersonaClienteDataSource : ReadOnlyDataSource<PersonaCliente>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaClienteDataSource class.
		/// </summary>
		public PersonaClienteDataSource() : base(new PersonaClienteService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PersonaClienteDataSourceView used by the PersonaClienteDataSource.
		/// </summary>
		protected PersonaClienteDataSourceView PersonaClienteView
		{
			get { return ( View as PersonaClienteDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PersonaClienteDataSourceView class that is to be
		/// used by the PersonaClienteDataSource.
		/// </summary>
		/// <returns>An instance of the PersonaClienteDataSourceView class.</returns>
		protected override BaseDataSourceView<PersonaCliente, Object> GetNewDataSourceView()
		{
			return new PersonaClienteDataSourceView(this, DefaultViewName);
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
	/// Supports the PersonaClienteDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PersonaClienteDataSourceView : ReadOnlyDataSourceView<PersonaCliente>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaClienteDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PersonaClienteDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PersonaClienteDataSourceView(PersonaClienteDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PersonaClienteDataSource PersonaClienteOwner
		{
			get { return Owner as PersonaClienteDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PersonaClienteService PersonaClienteProvider
		{
			get { return Provider as PersonaClienteService; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region PersonaClienteDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PersonaClienteDataSource class.
	/// </summary>
	public class PersonaClienteDataSourceDesigner : ReadOnlyDataSourceDesigner<PersonaCliente>
	{
	}

	#endregion PersonaClienteDataSourceDesigner

	#region PersonaClienteFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaClienteFilter : SqlFilter<PersonaClienteColumn>
	{
	}

	#endregion PersonaClienteFilter

	#region PersonaClienteExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaClienteExpressionBuilder : SqlExpressionBuilder<PersonaClienteColumn>
	{
	}
	
	#endregion PersonaClienteExpressionBuilder		
}

