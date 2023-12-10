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
	/// Represents the DataRepository.PasajeroViajeProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(PasajeroViajeDataSourceDesigner))]
	public class PasajeroViajeDataSource : ReadOnlyDataSource<PasajeroViaje>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeDataSource class.
		/// </summary>
		public PasajeroViajeDataSource() : base(new PasajeroViajeService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PasajeroViajeDataSourceView used by the PasajeroViajeDataSource.
		/// </summary>
		protected PasajeroViajeDataSourceView PasajeroViajeView
		{
			get { return ( View as PasajeroViajeDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PasajeroViajeDataSourceView class that is to be
		/// used by the PasajeroViajeDataSource.
		/// </summary>
		/// <returns>An instance of the PasajeroViajeDataSourceView class.</returns>
		protected override BaseDataSourceView<PasajeroViaje, Object> GetNewDataSourceView()
		{
			return new PasajeroViajeDataSourceView(this, DefaultViewName);
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
	/// Supports the PasajeroViajeDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PasajeroViajeDataSourceView : ReadOnlyDataSourceView<PasajeroViaje>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PasajeroViajeDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PasajeroViajeDataSourceView(PasajeroViajeDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PasajeroViajeDataSource PasajeroViajeOwner
		{
			get { return Owner as PasajeroViajeDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PasajeroViajeService PasajeroViajeProvider
		{
			get { return Provider as PasajeroViajeService; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region PasajeroViajeDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PasajeroViajeDataSource class.
	/// </summary>
	public class PasajeroViajeDataSourceDesigner : ReadOnlyDataSourceDesigner<PasajeroViaje>
	{
	}

	#endregion PasajeroViajeDataSourceDesigner

	#region PasajeroViajeFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroViaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroViajeFilter : SqlFilter<PasajeroViajeColumn>
	{
	}

	#endregion PasajeroViajeFilter

	#region PasajeroViajeExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroViaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroViajeExpressionBuilder : SqlExpressionBuilder<PasajeroViajeColumn>
	{
	}
	
	#endregion PasajeroViajeExpressionBuilder		
}

