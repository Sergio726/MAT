
#region Using Directives
using System;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using System.Data;

using MAT.Entities;
using MAT.Entities.Validation;

using MAT.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace MAT.Services
{		
	/// <summary>
	/// An component type implementation of the 'Provincia' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class ProvinciaService : MAT.Services.ProvinciaServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the ProvinciaService class.
		/// </summary>
		public ProvinciaService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace
