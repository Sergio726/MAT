
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
	
	///<summary>
	/// An component type implementation of the 'PasajeroViaje' table.
	///</summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class PasajeroViajeService : MAT.Services.PasajeroViajeServiceBase
	{
		/// <summary>
		/// Initializes a new instance of the PasajeroViajeService class.
		/// </summary>
		public PasajeroViajeService() : base()
		{
		}
		
	}//End Class


} // end namespace
