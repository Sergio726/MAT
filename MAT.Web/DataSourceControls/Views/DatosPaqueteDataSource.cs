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
	/// Represents the DataRepository.DatosPaqueteProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(DatosPaqueteDataSourceDesigner))]
	public class DatosPaqueteDataSource : ReadOnlyDataSource<DatosPaquete>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DatosPaqueteDataSource class.
		/// </summary>
		public DatosPaqueteDataSource() : base(new DatosPaqueteService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the DatosPaqueteDataSourceView used by the DatosPaqueteDataSource.
		/// </summary>
		protected DatosPaqueteDataSourceView DatosPaqueteView
		{
			get { return ( View as DatosPaqueteDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the DatosPaqueteDataSourceView class that is to be
		/// used by the DatosPaqueteDataSource.
		/// </summary>
		/// <returns>An instance of the DatosPaqueteDataSourceView class.</returns>
		protected override BaseDataSourceView<DatosPaquete, Object> GetNewDataSourceView()
		{
			return new DatosPaqueteDataSourceView(this, DefaultViewName);
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
	/// Supports the DatosPaqueteDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class DatosPaqueteDataSourceView : ReadOnlyDataSourceView<DatosPaquete>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DatosPaqueteDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the DatosPaqueteDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public DatosPaqueteDataSourceView(DatosPaqueteDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal DatosPaqueteDataSource DatosPaqueteOwner
		{
			get { return Owner as DatosPaqueteDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal DatosPaqueteService DatosPaqueteProvider
		{
			get { return Provider as DatosPaqueteService; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region DatosPaqueteDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the DatosPaqueteDataSource class.
	/// </summary>
	public class DatosPaqueteDataSourceDesigner : ReadOnlyDataSourceDesigner<DatosPaquete>
	{
	}

	#endregion DatosPaqueteDataSourceDesigner

	#region DatosPaqueteFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DatosPaquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DatosPaqueteFilter : SqlFilter<DatosPaqueteColumn>
	{
	}

	#endregion DatosPaqueteFilter

	#region DatosPaqueteExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DatosPaquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DatosPaqueteExpressionBuilder : SqlExpressionBuilder<DatosPaqueteColumn>
	{
	}
	
	#endregion DatosPaqueteExpressionBuilder		
}

