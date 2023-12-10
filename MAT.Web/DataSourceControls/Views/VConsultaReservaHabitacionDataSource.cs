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
	/// Represents the DataRepository.VConsultaReservaHabitacionProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(VConsultaReservaHabitacionDataSourceDesigner))]
	public class VConsultaReservaHabitacionDataSource : ReadOnlyDataSource<VConsultaReservaHabitacion>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionDataSource class.
		/// </summary>
		public VConsultaReservaHabitacionDataSource() : base(new VConsultaReservaHabitacionService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the VConsultaReservaHabitacionDataSourceView used by the VConsultaReservaHabitacionDataSource.
		/// </summary>
		protected VConsultaReservaHabitacionDataSourceView VConsultaReservaHabitacionView
		{
			get { return ( View as VConsultaReservaHabitacionDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the VConsultaReservaHabitacionDataSourceView class that is to be
		/// used by the VConsultaReservaHabitacionDataSource.
		/// </summary>
		/// <returns>An instance of the VConsultaReservaHabitacionDataSourceView class.</returns>
		protected override BaseDataSourceView<VConsultaReservaHabitacion, Object> GetNewDataSourceView()
		{
			return new VConsultaReservaHabitacionDataSourceView(this, DefaultViewName);
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
	/// Supports the VConsultaReservaHabitacionDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class VConsultaReservaHabitacionDataSourceView : ReadOnlyDataSourceView<VConsultaReservaHabitacion>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the VConsultaReservaHabitacionDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public VConsultaReservaHabitacionDataSourceView(VConsultaReservaHabitacionDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal VConsultaReservaHabitacionDataSource VConsultaReservaHabitacionOwner
		{
			get { return Owner as VConsultaReservaHabitacionDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal VConsultaReservaHabitacionService VConsultaReservaHabitacionProvider
		{
			get { return Provider as VConsultaReservaHabitacionService; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region VConsultaReservaHabitacionDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the VConsultaReservaHabitacionDataSource class.
	/// </summary>
	public class VConsultaReservaHabitacionDataSourceDesigner : ReadOnlyDataSourceDesigner<VConsultaReservaHabitacion>
	{
	}

	#endregion VConsultaReservaHabitacionDataSourceDesigner

	#region VConsultaReservaHabitacionFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VConsultaReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VConsultaReservaHabitacionFilter : SqlFilter<VConsultaReservaHabitacionColumn>
	{
	}

	#endregion VConsultaReservaHabitacionFilter

	#region VConsultaReservaHabitacionExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VConsultaReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VConsultaReservaHabitacionExpressionBuilder : SqlExpressionBuilder<VConsultaReservaHabitacionColumn>
	{
	}
	
	#endregion VConsultaReservaHabitacionExpressionBuilder		
}

