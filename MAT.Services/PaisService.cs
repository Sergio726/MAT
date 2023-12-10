
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
	/// An component type implementation of the 'Pais' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class PaisService : MAT.Services.PaisServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the PaisService class.
		/// </summary>
		public PaisService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace
