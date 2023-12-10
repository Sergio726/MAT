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
	/// Represents the DataRepository.VPersonaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(VPersonaDataSourceDesigner))]
	public class VPersonaDataSource : ReadOnlyDataSource<VPersona>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VPersonaDataSource class.
		/// </summary>
		public VPersonaDataSource() : base(new VPersonaService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the VPersonaDataSourceView used by the VPersonaDataSource.
		/// </summary>
		protected VPersonaDataSourceView VPersonaView
		{
			get { return ( View as VPersonaDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the VPersonaDataSourceView class that is to be
		/// used by the VPersonaDataSource.
		/// </summary>
		/// <returns>An instance of the VPersonaDataSourceView class.</returns>
		protected override BaseDataSourceView<VPersona, Object> GetNewDataSourceView()
		{
			return new VPersonaDataSourceView(this, DefaultViewName);
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
	/// Supports the VPersonaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class VPersonaDataSourceView : ReadOnlyDataSourceView<VPersona>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VPersonaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the VPersonaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public VPersonaDataSourceView(VPersonaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal VPersonaDataSource VPersonaOwner
		{
			get { return Owner as VPersonaDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal VPersonaService VPersonaProvider
		{
			get { return Provider as VPersonaService; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region VPersonaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the VPersonaDataSource class.
	/// </summary>
	public class VPersonaDataSourceDesigner : ReadOnlyDataSourceDesigner<VPersona>
	{
	}

	#endregion VPersonaDataSourceDesigner

	#region VPersonaFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VPersona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VPersonaFilter : SqlFilter<VPersonaColumn>
	{
	}

	#endregion VPersonaFilter

	#region VPersonaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VPersona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VPersonaExpressionBuilder : SqlExpressionBuilder<VPersonaColumn>
	{
	}
	
	#endregion VPersonaExpressionBuilder		
}

