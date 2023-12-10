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
	/// Represents the DataRepository.PersonaVendedorProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(PersonaVendedorDataSourceDesigner))]
	public class PersonaVendedorDataSource : ReadOnlyDataSource<PersonaVendedor>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorDataSource class.
		/// </summary>
		public PersonaVendedorDataSource() : base(new PersonaVendedorService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PersonaVendedorDataSourceView used by the PersonaVendedorDataSource.
		/// </summary>
		protected PersonaVendedorDataSourceView PersonaVendedorView
		{
			get { return ( View as PersonaVendedorDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PersonaVendedorDataSourceView class that is to be
		/// used by the PersonaVendedorDataSource.
		/// </summary>
		/// <returns>An instance of the PersonaVendedorDataSourceView class.</returns>
		protected override BaseDataSourceView<PersonaVendedor, Object> GetNewDataSourceView()
		{
			return new PersonaVendedorDataSourceView(this, DefaultViewName);
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
	/// Supports the PersonaVendedorDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PersonaVendedorDataSourceView : ReadOnlyDataSourceView<PersonaVendedor>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PersonaVendedorDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PersonaVendedorDataSourceView(PersonaVendedorDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PersonaVendedorDataSource PersonaVendedorOwner
		{
			get { return Owner as PersonaVendedorDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PersonaVendedorService PersonaVendedorProvider
		{
			get { return Provider as PersonaVendedorService; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region PersonaVendedorDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PersonaVendedorDataSource class.
	/// </summary>
	public class PersonaVendedorDataSourceDesigner : ReadOnlyDataSourceDesigner<PersonaVendedor>
	{
	}

	#endregion PersonaVendedorDataSourceDesigner

	#region PersonaVendedorFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaVendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaVendedorFilter : SqlFilter<PersonaVendedorColumn>
	{
	}

	#endregion PersonaVendedorFilter

	#region PersonaVendedorExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaVendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaVendedorExpressionBuilder : SqlExpressionBuilder<PersonaVendedorColumn>
	{
	}
	
	#endregion PersonaVendedorExpressionBuilder		
}

