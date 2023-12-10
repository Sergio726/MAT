
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
	/// An component type implementation of the 'PersonaVendedor' table.
	///</summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class PersonaVendedorService : MAT.Services.PersonaVendedorServiceBase
	{
		/// <summary>
		/// Initializes a new instance of the PersonaVendedorService class.
		/// </summary>
		public PersonaVendedorService() : base()
		{
		}
		
	}//End Class


} // end namespace
