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
	/// Represents the DataRepository.VLocalidadProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(VLocalidadDataSourceDesigner))]
	public class VLocalidadDataSource : ReadOnlyDataSource<VLocalidad>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VLocalidadDataSource class.
		/// </summary>
		public VLocalidadDataSource() : base(new VLocalidadService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the VLocalidadDataSourceView used by the VLocalidadDataSource.
		/// </summary>
		protected VLocalidadDataSourceView VLocalidadView
		{
			get { return ( View as VLocalidadDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the VLocalidadDataSourceView class that is to be
		/// used by the VLocalidadDataSource.
		/// </summary>
		/// <returns>An instance of the VLocalidadDataSourceView class.</returns>
		protected override BaseDataSourceView<VLocalidad, Object> GetNewDataSourceView()
		{
			return new VLocalidadDataSourceView(this, DefaultViewName);
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
	/// Supports the VLocalidadDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class VLocalidadDataSourceView : ReadOnlyDataSourceView<VLocalidad>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VLocalidadDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the VLocalidadDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public VLocalidadDataSourceView(VLocalidadDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal VLocalidadDataSource VLocalidadOwner
		{
			get { return Owner as VLocalidadDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal VLocalidadService VLocalidadProvider
		{
			get { return Provider as VLocalidadService; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region VLocalidadDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the VLocalidadDataSource class.
	/// </summary>
	public class VLocalidadDataSourceDesigner : ReadOnlyDataSourceDesigner<VLocalidad>
	{
	}

	#endregion VLocalidadDataSourceDesigner

	#region VLocalidadFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VLocalidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VLocalidadFilter : SqlFilter<VLocalidadColumn>
	{
	}

	#endregion VLocalidadFilter

	#region VLocalidadExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VLocalidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VLocalidadExpressionBuilder : SqlExpressionBuilder<VLocalidadColumn>
	{
	}
	
	#endregion VLocalidadExpressionBuilder		
}

