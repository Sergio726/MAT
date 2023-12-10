
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
	/// An component type implementation of the 'PasajeroMenor' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class PasajeroMenorService : MAT.Services.PasajeroMenorServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the PasajeroMenorService class.
		/// </summary>
		public PasajeroMenorService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace
