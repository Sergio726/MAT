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
	/// Represents the DataRepository.PersonaProveedorProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(PersonaProveedorDataSourceDesigner))]
	public class PersonaProveedorDataSource : ReadOnlyDataSource<PersonaProveedor>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorDataSource class.
		/// </summary>
		public PersonaProveedorDataSource() : base(new PersonaProveedorService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PersonaProveedorDataSourceView used by the PersonaProveedorDataSource.
		/// </summary>
		protected PersonaProveedorDataSourceView PersonaProveedorView
		{
			get { return ( View as PersonaProveedorDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PersonaProveedorDataSourceView class that is to be
		/// used by the PersonaProveedorDataSource.
		/// </summary>
		/// <returns>An instance of the PersonaProveedorDataSourceView class.</returns>
		protected override BaseDataSourceView<PersonaProveedor, Object> GetNewDataSourceView()
		{
			return new PersonaProveedorDataSourceView(this, DefaultViewName);
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
	/// Supports the PersonaProveedorDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PersonaProveedorDataSourceView : ReadOnlyDataSourceView<PersonaProveedor>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PersonaProveedorDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PersonaProveedorDataSourceView(PersonaProveedorDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PersonaProveedorDataSource PersonaProveedorOwner
		{
			get { return Owner as PersonaProveedorDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PersonaProveedorService PersonaProveedorProvider
		{
			get { return Provider as PersonaProveedorService; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region PersonaProveedorDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PersonaProveedorDataSource class.
	/// </summary>
	public class PersonaProveedorDataSourceDesigner : ReadOnlyDataSourceDesigner<PersonaProveedor>
	{
	}

	#endregion PersonaProveedorDataSourceDesigner

	#region PersonaProveedorFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaProveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaProveedorFilter : SqlFilter<PersonaProveedorColumn>
	{
	}

	#endregion PersonaProveedorFilter

	#region PersonaProveedorExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaProveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaProveedorExpressionBuilder : SqlExpressionBuilder<PersonaProveedorColumn>
	{
	}
	
	#endregion PersonaProveedorExpressionBuilder		
}

