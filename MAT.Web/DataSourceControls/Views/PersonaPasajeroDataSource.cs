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
	/// Represents the DataRepository.PersonaPasajeroProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(PersonaPasajeroDataSourceDesigner))]
	public class PersonaPasajeroDataSource : ReadOnlyDataSource<PersonaPasajero>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroDataSource class.
		/// </summary>
		public PersonaPasajeroDataSource() : base(new PersonaPasajeroService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PersonaPasajeroDataSourceView used by the PersonaPasajeroDataSource.
		/// </summary>
		protected PersonaPasajeroDataSourceView PersonaPasajeroView
		{
			get { return ( View as PersonaPasajeroDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PersonaPasajeroDataSourceView class that is to be
		/// used by the PersonaPasajeroDataSource.
		/// </summary>
		/// <returns>An instance of the PersonaPasajeroDataSourceView class.</returns>
		protected override BaseDataSourceView<PersonaPasajero, Object> GetNewDataSourceView()
		{
			return new PersonaPasajeroDataSourceView(this, DefaultViewName);
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
	/// Supports the PersonaPasajeroDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PersonaPasajeroDataSourceView : ReadOnlyDataSourceView<PersonaPasajero>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PersonaPasajeroDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PersonaPasajeroDataSourceView(PersonaPasajeroDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PersonaPasajeroDataSource PersonaPasajeroOwner
		{
			get { return Owner as PersonaPasajeroDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PersonaPasajeroService PersonaPasajeroProvider
		{
			get { return Provider as PersonaPasajeroService; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region PersonaPasajeroDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PersonaPasajeroDataSource class.
	/// </summary>
	public class PersonaPasajeroDataSourceDesigner : ReadOnlyDataSourceDesigner<PersonaPasajero>
	{
	}

	#endregion PersonaPasajeroDataSourceDesigner

	#region PersonaPasajeroFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaPasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaPasajeroFilter : SqlFilter<PersonaPasajeroColumn>
	{
	}

	#endregion PersonaPasajeroFilter

	#region PersonaPasajeroExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaPasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaPasajeroExpressionBuilder : SqlExpressionBuilder<PersonaPasajeroColumn>
	{
	}
	
	#endregion PersonaPasajeroExpressionBuilder		
}

