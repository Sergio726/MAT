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
	/// Represents the DataRepository.ReservaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(ReservaDataSourceDesigner))]
	public class ReservaDataSource : ReadOnlyDataSource<Reserva>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaDataSource class.
		/// </summary>
		public ReservaDataSource() : base(new ReservaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ReservaDataSourceView used by the ReservaDataSource.
		/// </summary>
		protected ReservaDataSourceView ReservaView
		{
			get { return ( View as ReservaDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ReservaDataSourceView class that is to be
		/// used by the ReservaDataSource.
		/// </summary>
		/// <returns>An instance of the ReservaDataSourceView class.</returns>
		protected override BaseDataSourceView<Reserva, Object> GetNewDataSourceView()
		{
			return new ReservaDataSourceView(this, DefaultViewName);
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
	/// Supports the ReservaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ReservaDataSourceView : ReadOnlyDataSourceView<Reserva>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ReservaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ReservaDataSourceView(ReservaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ReservaDataSource ReservaOwner
		{
			get { return Owner as ReservaDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ReservaService ReservaProvider
		{
			get { return Provider as ReservaService; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region ReservaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ReservaDataSource class.
	/// </summary>
	public class ReservaDataSourceDesigner : ReadOnlyDataSourceDesigner<Reserva>
	{
	}

	#endregion ReservaDataSourceDesigner

	#region ReservaFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Reserva"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaFilter : SqlFilter<ReservaColumn>
	{
	}

	#endregion ReservaFilter

	#region ReservaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Reserva"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaExpressionBuilder : SqlExpressionBuilder<ReservaColumn>
	{
	}
	
	#endregion ReservaExpressionBuilder		
}

